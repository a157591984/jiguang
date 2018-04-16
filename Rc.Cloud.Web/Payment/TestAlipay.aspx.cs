using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Alipay;

namespace Rc.Cloud.Web.Payment
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAlipay_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //支付类型
            string payment_type = "1";
            //必填，不能修改
            //服务器异步通知页面路径
            string notify_url = "http://商户网关地址/create_direct_pay_by_user-CSHARP-UTF-8/notify_url.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            string return_url = "http://商户网关地址/create_direct_pay_by_user-CSHARP-UTF-8/return_url.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            //商户订单号
            string out_trade_no = WIDout_trade_no.Text.Trim();
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            string subject = WIDsubject.Text.Trim();
            //必填

            //付款金额
            string total_fee = WIDtotal_fee.Text.Trim();
            //必填

            //订单描述

            string body = WIDbody.Text.Trim();
            //默认支付方式
            string paymethod = "bankPay";
            //必填
            //默认网银
            string defaultbank = WIDdefaultbank.Text.Trim();
            //必填，银行简码请参考接口技术文档

            //商品展示地址
            string show_url = WIDshow_url.Text.Trim();
            //需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //若要使用请调用类文件submit中的query_timestamp函数

            //客户端的IP地址
            string exter_invoke_ip = "";
            //非局域网的外网IP地址，如：221.0.0.1


            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("seller_email", Config.Seller_email);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("paymethod", paymethod);
            sParaTemp.Add("defaultbank", defaultbank);
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            Response.Write(sHtmlText);

        }

        protected void btnDDD_Click(object sender, EventArgs e)
        {
            string para = "out_trade_no=" + WIDout_trade_no.Text + "&trade_no=111122223333&trade_status=TRADE_SUCCESS&buyer_email=111@tt.com";
            IDictionary<string, string> iDic = new Dictionary<string, string>();
            iDic.Add("out_trade_no", "161215125824953448");
            iDic.Add("trade_no", "111122223333");
            iDic.Add("trade_status", "TRADE_SUCCESS");
            iDic.Add("buyer_email", "111@tt.com");
            //Rc.Common.RemotWeb.PostDataToServer("http://localhost:1528/Payment/notify_alipay.aspx", iDic, "post", System.Text.Encoding.UTF8);
            string result = Rc.Common.RemotWeb.PostDataToServer("http://localhost:1528/Payment/notify_alipay.aspx", para, System.Text.Encoding.UTF8, "Post");
            Response.Write(result);
        }

    }
}