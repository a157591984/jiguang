using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WxPayAPI
{
    /// <summary>
    /// 支付结果通知回调处理类
    /// 负责接收微信支付后台发送的支付结果并对订单有效性进行验证，将验证结果反馈给微信支付后台
    /// </summary>
    public class ResultNotify : Notify
    {
        public ResultNotify(Page page)
            : base(page)
        {
        }

        public override void ProcessNotify()
        {
            WxPayData notifyData = GetNotifyData();

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                Log.Error(this.GetType().ToString(), "The Pay result is error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                Log.Error(this.GetType().ToString(), "Order query failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
            //查询订单成功
            else
            {
                Log.Info(this.GetType().ToString(), "订单支付成功，异步回调");

                #region 支付成功，异步更新订单表2016-06-30TS
                Model_UserOrder model = new Model_UserOrder();
                BLL_UserOrder bll = new BLL_UserOrder();
                model = bll.GetModelByOrderNo(notifyData.GetValue("out_trade_no").ToString());

                if (model.UserOrder_Status == (int)UOrderStatus.待付款 || string.IsNullOrEmpty(model.trade_no) || string.IsNullOrEmpty(model.trade_status))
                {
                    model.UserOrder_Paytool = UserOrder_PaytoolEnum.WXPAY.ToString();
                    model.UsreOrder_Buyeremail = notifyData.GetValue("openid").ToString();
                    model.trade_no = notifyData.GetValue("transaction_id").ToString();
                    model.trade_status = notifyData.GetValue("return_code").ToString();
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
                        buyModel.BuyType = UserOrder_PaytoolEnum.WXPAY.ToString();
                        buyModel.CreateTime = DateTime.Now;
                        buyModel.CreateUser = "WXNotify";
                        #endregion
                        executeFlag = bll.UpdateAndAddUserBuyResources(model, buyModel);
                    }
                    else
                    {
                        executeFlag = bll.Update(model);
                    }
                    if (executeFlag)
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS(page.Request.Url.ToString(), string.Format("微信回调。购买资源成功，购买人：{0}，资源标识：{1}"
                            , model.UserId, model.Book_Id));
                    }
                }
                #endregion

                WxPayData res = new WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");

                page.Response.Write(res.ToXml());
                page.Response.End();
            }
        }

        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}