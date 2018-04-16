using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.AuthApi.yzlyxx
{
    public partial class index : System.Web.UI.Page
    {
        string iurl = string.Empty;
        string schoolCode = Rc.Model.Resources.ThirdPartyEnum.yzlyxx.ToString();

        String key = "aDfG6dFllpgaiMdpeeaae";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //认证代理服务器会将用户登录信息和认证服务器地址放入到HTTP_HEADER中
                String uid = Request.Headers["UID"].Filter();
                String sign = Request.Headers["AP_SEC_SIGN"];
                String authServerURL = Request.Headers["AUTH_SERVER_URL"];

                //构造登录链接
                String redirectURL = authServerURL + "/login?service=" + Get_Current_URL();
                redirectURL = "/";
                String loginHrefHtml = "<a href='" + redirectURL + "'>点击登录</a>";
                //判断用户是否登录了身份认证
                uid = Request.QueryString["uid"].Filter();
                if (uid == null || uid.Equals(""))
                {
                    //没有登录，则以匿名身份认证访问系统的匿名资源，可以通过连接登录身份认证
                    Response.Write("尚未登陆，您是以匿名身份进入本系统中！请" + loginHrefHtml);
                    return;
                }
                else
                {
                    //if (!clsUtility.GetMd5(key + uid).Equals(sign))
                    //{
                    //    Response.Write("非法访问");
                    //    return;
                    //}
                    //登录了身份认证，可以获取用户的更多信息，系统根据当前登录用户UID来显示对应的系统资源
                    String cn = Request.Headers["CN"].Filter();
                    if (string.IsNullOrEmpty(cn))
                    {
                        cn = uid;
                    }

                    iurl = HandelThirdPartyUserInfo.HandelUserInfo(schoolCode, uid, cn);

                    Response.Redirect(iurl, false);
                }
            }
            catch (Exception ex)
            {
                Response.Write("登录异常err");
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", schoolCode + "用户登录-接口", ex.Message.ToString());
            }

        }

        private String Get_Current_URL()
        {
            String requestUrl = Request.Headers["X-REQUEST-URL"];
            String queryStr = HttpContext.Current.Request.Url.Query;
            if (queryStr != null && !queryStr.Equals(""))
            {
                requestUrl = requestUrl + (queryStr.StartsWith("?") ? queryStr : "?" + queryStr);
            }
            return HttpUtility.UrlEncode(requestUrl, System.Text.Encoding.UTF8);
        }

    }
}