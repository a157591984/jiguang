using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web
{
    public partial class ErrorPageF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["FLoginUser"] = null;
            string strLoginUrl = string.Empty;
            string errorString = string.Empty;

            if (!String.IsNullOrEmpty(Request.QueryString["errorString"]))
            {
                errorString = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["errorString"].ToString().Filter());

            }
            if (Request["rurl"] != null)
            {
                strLoginUrl = "<div class=\"panel_footer\"><a href='index.aspx?iurl=" + Server.UrlEncode(Request["rurl"].ToString().Filter()) + "' class='btn btn-default'><i class=\"material-icons\">&#xE5C4;</i>返回首页</a></div>";
            }
            else
            {
                strLoginUrl = "<div class=\"panel_footer\"><a href='index.aspx' class='btn btn-default'><i class=\"material-icons\">&#xE5C4;</i>返回首页</a></div>";
            }

            //Response.Redirect("login.aspx");
            ltErrorInfo.Text = "<div class=\"panel_body\"><p>页面发生错误</p><p>" + errorString + "</p></div>" + strLoginUrl;
            if (Request["errorType"] != null)
            {
                switch (Request["errorType"].ToString())
                {
                    case "1":
                        ltErrorInfo.Text = "<div class=\"panel_body\">你已长时间未使用系统</div>" + strLoginUrl;
                        break;

                    case "2":
                        ltErrorInfo.Text = "<div class=\"panel_body\">你没有权限访问此页面</div>" + strLoginUrl;
                        break;
                    case "3":
                        ltErrorInfo.Text = "<div class=\"panel_body\"><p>非法操作</p><p>" + errorString + "</p></div>" + strLoginUrl;
                        break;
                    case "6":
                        ltErrorInfo.Text = "<div class=\"panel_body\">你没有访问本系统的权限，请联系管理员</div>" + strLoginUrl;
                        break;
                    case "9":
                        ltErrorInfo.Text = "<div class=\"panel_body\">页面跳转失败</div>" + strLoginUrl;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}