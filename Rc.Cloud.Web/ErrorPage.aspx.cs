using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strLoginUrl = string.Empty;
            string errorString = string.Empty;

            if (!String.IsNullOrEmpty(Request.QueryString["errorString"]))
            {  
                errorString = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["errorString"].ToString().Filter());

            }
            if (Request["rurl"] != null)
            {
                strLoginUrl = "<div class=\"jump\"><a href='/rcadmin/index.aspx?iurl=" + Server.UrlEncode(Request["rurl"].ToString().Filter()) + "'>[重新登录]</a></div>";
            }
            else
            {
                strLoginUrl = "<div class=\"jump\"><a href='/rcadmin/index.aspx'>[重新登录]</a></div>";
            }

            //Response.Redirect("login.aspx");
            ltErrorInfo.Text = "<div class=\"error\">对不起页面发生错误</div><div class=\"error_info\">" + errorString + "</div>" + strLoginUrl;
            if (Request["errorType"] != null)
            {
                switch (Request["errorType"].ToString())
                {
                    case "1":
                        ltErrorInfo.Text = "<div class=\"error\">您已长时间未使用系统</div>" + strLoginUrl;
                        break;

                    case "2":
                        ltErrorInfo.Text = "<div class=\"error\">对不起您没有权限访问此页面</div>" + strLoginUrl;
                        break;
                    case "3":
                        ltErrorInfo.Text = "<div class=\"error\">非法操作</div><div class=\"error_info\">" + errorString + "</div>" + strLoginUrl;
                        break;
                    case "6":
                        ltErrorInfo.Text = "<div class=\"error\">对不起，您现在还没有访问本系统的权限，请联系管理员</div>" + strLoginUrl;
                        break; 
                    case "9":
                        ltErrorInfo.Text = "<div class=\"error\">页面跳转失败</div>" + strLoginUrl;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}