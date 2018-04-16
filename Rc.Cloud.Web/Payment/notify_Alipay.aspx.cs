using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Common.StrUtility;

/// <summary>
/// 功能：服务器异步通知页面
/// 版本：3.3
/// 日期：2012-07-10
/// 说明：
/// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
/// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
/// 
/// ///////////////////页面功能说明///////////////////
/// 创建该页面文件时，请留心该页面文件中无任何HTML代码及空格。
/// 该页面不能在本机电脑测试，请到服务器上做测试。请确保外部可以访问该页面。
/// 该页面调试工具请使用写文本函数logResult。
/// 如果没有收到该页面返回的 success 信息，支付宝会在24小时内按一定的时间策略重发通知
/// </summary>
namespace Com.Alipay
{
    public partial class notify_Alipay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号

                    string out_trade_no = Request.Form["out_trade_no"].Filter();

                    //支付宝交易号

                    string trade_no = Request.Form["trade_no"].Filter();

                    //交易状态
                    string trade_status = Request.Form["trade_status"].Filter();

                    //买家支付宝账号
                    string buyer_email = Request.Form["buyer_email"];

                    int orderStatus = 1;// (int)XHZ.Model.UOrderStatus.等待付款;
                    if (trade_status.ToUpper() == "TRADE_FINISHED")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
                        orderStatus = (int)UOrderStatus.付款成功;
                    }
                    else if (trade_status.ToUpper() == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //付款完成后，支付宝系统发送该交易状态通知
                        orderStatus = (int)UOrderStatus.付款成功;
                    }
                    else
                    {
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    Rc.Web.Payment.Core.LogHandler.WriteLogForPay(out_trade_no, "付款成功", trade_no);

                    #region 付款成功 异步更新订单表 author：Ethan
                    Model_UserOrder model = new Model_UserOrder();
                    BLL_UserOrder bll = new BLL_UserOrder();
                    model = bll.GetModelByOrderNo(out_trade_no);

                    if (orderStatus == (int)UOrderStatus.付款成功 && model.UserOrder_Status == (int)UOrderStatus.待付款)
                    {
                        model.UserOrder_Paytool = UserOrder_PaytoolEnum.ALIPAY.ToString();
                        model.UsreOrder_Buyeremail = buyer_email;
                        model.trade_no = trade_no;
                        model.trade_status = trade_status;
                        model.UserOrder_Status = (int)UOrderStatus.完成;
                        model.UserOrder_FinishTime = DateTime.Now;

                        bool executeFlag = false;
                        if (new BLL_UserBuyResources().GetRecordCount(string.Format("UserId='{0}' and Book_id='{1}' ", model.UserId, model.Book_Id)) == 0)
                        {
                            #region 用户购买资源表
                            Model_UserBuyResources buyModel = new Model_UserBuyResources();
                            buyModel.UserBuyResources_ID = Guid.NewGuid().ToString();
                            buyModel.UserId = model.UserId;
                            buyModel.Book_id = model.Book_Id;
                            buyModel.BookPrice = model.Book_Price;
                            buyModel.BuyType = UserOrder_PaytoolEnum.ALIPAY.ToString();
                            buyModel.CreateTime = DateTime.Now;
                            buyModel.CreateUser = "AlipayNotify";
                            #endregion
                            executeFlag = bll.UpdateAndAddUserBuyResources(model, buyModel);
                        }
                        else
                        {
                            executeFlag = bll.Update(model);
                        }

                        if (executeFlag)
                        {
                            //try
                            //{
                            //    P_User userModel = new P_UserBLL().GetModel(model.user_id);
                            //    pfunction.SendSMS(userModel.moblie, "您的订单" + model.OrderNo + "已支付成功");
                            //}
                            //catch
                            //{

                            //}
                            //if (new U_OrdersHandleBLL().GetRecordCount("OrderId='" + model.OrderId + "' and OperateUser='支付接口异步回调'") == 0)
                            //{
                            //    pfunction.InsertOrderHandle(model.OrderId, "您已" + XHZ.Common.EnumService.GetDescription<UOrderStatus>(orderStatus), "支付接口异步回调");
                            //}
                            new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS(Request.Url.ToString(), string.Format("支付宝回调。购买资源成功，购买人：{0}，资源标识：{1}"
                            , model.UserId, model.Book_Id));
                            Response.Write("success");  //请不要修改或删除
                        }
                    }
                    #endregion

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }
}