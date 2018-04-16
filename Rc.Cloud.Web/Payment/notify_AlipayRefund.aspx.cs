using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace Com.Alipay
{
    public partial class notify_AlipayRefund : System.Web.UI.Page
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

                    //批次号

                    string batch_no = Request.Form["batch_no"];

                    //批量退款数据中转账成功的笔数

                    string success_num = Request.Form["success_num"];

                    //批量退款数据中的详细信息
                    string result_details = Request.Form["result_details"];


                    //判断是否在商户网站中已经做过了这次通知返回的处理
                    //如果没有做过处理，那么执行商户的业务程序
                    //如果有做过处理，那么不执行商户的业务程序

                    #region 退款 异步更新数据 author：Ethan
                    //XHZ.BLL.U_OrdersBLL bll = new XHZ.BLL.U_OrdersBLL();
                    //XHZ.Model.U_Orders model = new XHZ.Model.U_Orders();
                    //model = bll.GetmodelByBatchNo(batch_no);
                    //if (model.OrderStatus == (int)XHZ.Model.UOrderStatus.已退款)
                    //{
                    //    Response.Write("success");  //请不要修改或删除
                    //}
                    //else
                    //{
                    //    model.OrderStatus = (int)XHZ.Model.UOrderStatus.已退款;
                    //    model.result_details = string.Format("退款明细[{0}],退款时间[{1}]", result_details, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    //    XHZ.Web.Payment.Core.LogHandler.WriteLogForPay(model.OrderNo, "退款成功", model.result_details);

                    //    if (bll.Update(model))
                    //    {
                    //        try
                    //        {
                    //            P_User userModel = new P_UserBLL().GetModel(model.user_id);
                    //            pfunction.SendSMS(userModel.moblie, "您的订单" + model.OrderNo + "已退款成功");
                    //        }
                    //        catch
                    //        {

                    //        }
                    //        XHZ.Web.Common.pfunction.InsertOrderHandle(model.OrderId, "您的订单退款成功", "支付宝退款接口异步调用");
                    //        XHZ.Common.SYS_LOG.AddCommonLog(Request.Url.AbsolutePath, "【退款成功】", "支付宝退款异接口步调用");
                            
                    //        Response.Write("success");  //请不要修改或删除
                    //    }
                    //    else
                    //    {
                    //        XHZ.Common.SYS_LOG.AddCommonLog(Request.Url.AbsolutePath, "【退款失败】", "支付宝退款接口异步调用");
                    //    }
                    //}
                    #endregion

                    //Response.Write("success");  //请不要修改或删除

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

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