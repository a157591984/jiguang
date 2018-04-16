using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;

namespace Com.Alipay
{
    public partial class AlipayPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string rid = Request.QueryString["rid"].Filter();
                string key = Request.QueryString["key"].Filter();
                string bankId = Request.QueryString["defaultbank"].Filter();
                rid = Rc.Common.DBUtility.DESEncrypt.Decrypt(rid);
                Model_UserOrder model = new Model_UserOrder();
                BLL_UserOrder bll = new BLL_UserOrder();
                model = bll.GetModelByOrderNo(rid);
                string domain = Rc.Cloud.Web.Common.pfunction.getHostPath();

                ////////////////////////////////////////////请求参数////////////////////////////////////////////

                //支付类型
                string payment_type = "1";
                //必填，不能修改
                //服务器异步通知页面路径
                string notify_url = string.Format("{0}/Payment/notify_Alipay.aspx", domain);
                //需http://格式的完整路径，不能加?id=123这类自定义参数

                //页面跳转同步通知页面路径
                string return_url = string.Format("{0}/Payment/return_Alipay.aspx", domain);

                //商户订单号
                string out_trade_no = model.UserOrder_No;
                //商户网站订单系统中唯一订单号，必填

                //订单名称
                string subject = model.Book_Name;
                //必填

                //付款金额
                string total_fee = model.UserOrder_Amount.ToString();
                //必填

                //订单描述
                string body = model.Book_Name;

                //默认支付方式
                string paymethod = "bankPay";
                //必填
                //默认网银
                string defaultbank = bankId;
                //必填，银行简码请参考接口技术文档

                //商品展示地址
                string show_url = "";// string.Format("http://{0}/Show.aspx", domain);
                //需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html

                //防钓鱼时间戳
                string anti_phishing_key = Submit.Query_timestamp();
                //若要使用请调用类文件submit中的query_timestamp函数

                //客户端的IP地址
                string exter_invoke_ip = Rc.Cloud.Web.Common.pfunction.GetRealIP();
                //非局域网的外网IP地址，如：221.0.0.1

                //把请求参数打包成数组
                SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                if (key == "platform")
                {
                    #region 即时到账交易
                    sParaTemp.Add("service", "create_direct_pay_by_user");
                    sParaTemp.Add("partner", Config.Partner);
                    sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
                    sParaTemp.Add("notify_url", notify_url);
                    sParaTemp.Add("return_url", return_url);
                    //业务参数
                    sParaTemp.Add("out_trade_no", out_trade_no);
                    sParaTemp.Add("subject", subject);
                    sParaTemp.Add("payment_type", payment_type);
                    sParaTemp.Add("total_fee", total_fee);
                    sParaTemp.Add("seller_id", Config.Partner);
                    sParaTemp.Add("seller_email", Config.Seller_email);
                    sParaTemp.Add("body", body);
                    sParaTemp.Add("show_url", show_url);
                    sParaTemp.Add("anti_phishing_key", anti_phishing_key);
                    sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);
                    sParaTemp.Add("qr_pay_mode", "4");
                    sParaTemp.Add("qrcode_width", "200");
                    #endregion
                }
                else if (key == "bank")
                {
                    #region 网银支付
                    sParaTemp.Add("service", "create_direct_pay_by_user");
                    sParaTemp.Add("partner", Config.Partner);
                    sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
                    sParaTemp.Add("notify_url", notify_url);
                    sParaTemp.Add("return_url", return_url);
                    //业务参数
                    sParaTemp.Add("out_trade_no", out_trade_no);
                    sParaTemp.Add("subject", subject);
                    sParaTemp.Add("payment_type", payment_type);
                    sParaTemp.Add("total_fee", total_fee);
                    sParaTemp.Add("seller_id", Config.Partner);
                    sParaTemp.Add("seller_email", Config.Seller_email);
                    sParaTemp.Add("body", body);
                    sParaTemp.Add("paymethod", paymethod);
                    sParaTemp.Add("defaultbank", defaultbank);
                    sParaTemp.Add("show_url", show_url);
                    sParaTemp.Add("anti_phishing_key", anti_phishing_key);
                    sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);
                    #endregion
                }


                //建立请求
                string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
                Response.Write(sHtmlText);
            }
            catch (Exception)
            {
                Response.Write("<script language=\"javascript\">alert('请求失败,请返回重新操作');</script>");
            }
        }
    }
}