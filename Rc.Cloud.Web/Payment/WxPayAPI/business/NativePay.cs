using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Web;

namespace WxPayAPI
{
    public class NativePay
    {
        /**
        * 生成扫描支付模式一URL
        * @param productId 商品ID
        * @return 模式一URL
        */
        public string GetPrePayUrl(string productId)
        {
            Log.Info(this.GetType().ToString(), "Native pay mode 1 url is producing...");

            WxPayData data = new WxPayData();
            data.SetValue("appid", WxPayConfig.APPID);//公众帐号id
            data.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            data.SetValue("time_stamp", WxPayApi.GenerateTimeStamp());//时间戳
            data.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            data.SetValue("product_id", productId);//商品ID
            data.SetValue("sign", data.MakeSign());//签名
            string str = ToUrlParams(data.GetValues());//转换为URL串
            string url = "weixin://wxpay/bizpayurl?" + str;

            Log.Info(this.GetType().ToString(), "Get native pay mode 1 url : " + url);
            return url;
        }

        /**
        * 生成直接支付url，支付url有效期为2小时,模式二
        * @param rId 订单ID加密
        * @return 模式二URL
        */
        public string GetPayUrl(string rId)
        {
            //Log.Info(this.GetType().ToString(), "Native pay mode 2 url is producing...");

            rId = Rc.Common.DBUtility.DESEncrypt.Decrypt(rId);
            Model_UserOrder model = new Model_UserOrder();
            BLL_UserOrder bll = new BLL_UserOrder();
            model = bll.GetModelByOrderNo(rId);
            int total_fee = Convert.ToInt32(model.UserOrder_Amount * 100);

            WxPayData data = new WxPayData();
            data.SetValue("body", Rc.Cloud.Web.Common.pfunction.GetSubstring(model.Book_Name, 20, false));//商品描述 示例值：腾讯充值中心-QQ会员充值
            //data.SetValue("detail", model.Book_Name); // 商品详情
            //data.SetValue("attach", "test");//附加数据
            data.SetValue("out_trade_no", model.UserOrder_No);//订单号 //随机字符串WxPayApi.GenerateOutTradeNo()
            data.SetValue("total_fee", total_fee);//总金额
            //data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            //data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            //data.SetValue("goods_tag", "jjj");//商品标记
            data.SetValue("trade_type", "NATIVE");//交易类型
            data.SetValue("product_id", model.Book_Id);//商品ID
            data.SetValue("notify_url", string.Format("{0}/Payment/WxPayAPI/ResultNotifyPage.aspx", Rc.Cloud.Web.Common.pfunction.getHostPath()));//异步通知url

            WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口
            //Log.Info(this.GetType().ToString(), "调用统一下单接口返回数据 : " + result.ToXml());
            string url = string.Empty;
            if (result.GetValue("return_code").ToString() == "SUCCESS" && result.GetValue("result_code").ToString() == "SUCCESS")
            {
                url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
            }
            else
            {
                Log.Error("Get native pay mode 2 url ", result.ToXml());
            }

            //Log.Info(this.GetType().ToString(), "Get native pay mode 2 url : " + url);
            return url;
        }

        /**
        * 参数数组转换为url格式
        * @param map 参数名与参数值的映射表
        * @return URL字符串
        */
        private string ToUrlParams(SortedDictionary<string, object> map)
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in map)
            {
                buff += pair.Key + "=" + pair.Value + "&";
            }
            buff = buff.Trim('&');
            return buff;
        }
    }
}