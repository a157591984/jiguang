using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Text;
using ThoughtWorks;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using Rc.Common.StrUtility;

namespace WxPayAPI
{
    public partial class NativePayPage : System.Web.UI.Page
    {
        string rid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string rid = Request.QueryString["rid"].Filter();
                NativePay nativePay = new NativePay();

                //生成扫码支付模式二url
                string url = nativePay.GetPayUrl(rid);
                if (string.IsNullOrEmpty(url))
                {
                    ltlTips.Text = "支付异常，请刷新页面重试";
                    Image2.Visible = false;
                }
                else
                {
                    //将url生成二维码图片
                    Image2.ImageUrl = "MakeQRCode.aspx?data=" + HttpUtility.UrlEncode(url);
                }
            }
            catch (Exception)
            {
                ltlTips.Text = "支付异常，请刷新页面重试err";
                Image2.Visible = false;
            }
        }
    }
}