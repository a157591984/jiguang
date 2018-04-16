using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web
{
    public partial class onlinecheck : System.Web.UI.Page
    {
        protected string iurl = string.Empty; // 跳转地址
        protected string local_url = string.Empty; // 局域网地址
        protected string local_url_en = string.Empty; // 局域网地址（加密）
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                iurl = Request["iurl"].Filter();
                local_url = Request["local_url"].Filter();
                local_url_en = Rc.Common.DBUtility.DESEncrypt.Encrypt(Request["local_url"].Filter());
                if (string.IsNullOrEmpty(iurl)
                    || string.IsNullOrEmpty(local_url))
                {
                    Rc.Common.StrUtility.CookieClass.RemoveCookie("UserPublicUrl_Cookie");
                    Session.Clear();
                    Response.Redirect("index.aspx");
                }
            }
            catch (Exception)
            {
                Rc.Common.StrUtility.CookieClass.RemoveCookie("UserPublicUrl_Cookie");
                Session.Clear();
                Response.Redirect("index.aspx");
            }

        }
    }
}