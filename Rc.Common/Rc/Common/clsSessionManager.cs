namespace Rc.Common
{

    using Rc.Cloud.Model;
    using System;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.SessionState;
    using System.Web.UI;

    public class clsSessionManager
    {
        public static string CutDangerousHtmlElement(string html)
        {
            Regex regex = new Regex("<script[^>]*>[^>]*<[^>]script[^>]*>", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex("<iframe[^>]*>[^>]*<[^>]iframe[^>]*>", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex("<frameset[^>]*>[^>]*<[^>]frameset[^>]*>", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex("<img (.+?) />", RegexOptions.IgnoreCase);
            html = regex.Replace(html, "");
            html = regex2.Replace(html, "");
            html = regex3.Replace(html, "");
            html = regex4.Replace(html, "");
            return html;
        }

        public static void ErrorDispose(int errorType, bool currently)
        {
            string str = HttpContext.Current.Request.Url.AbsoluteUri.Replace("?", "~~").Replace("&", "^");
            if (currently)
            {
                HttpContext.Current.Response.Redirect(string.Concat(new object[] { "~/ErrorPage.aspx?errorType=", errorType, "&rurl=", str }));
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/ErrorPage.aspx?errorType=" + errorType);
                HttpContext.Current.Response.End();
            }
        }

        public static void ErrorDispose(Page page, int errorType, bool currently)
        {
            string str = page.Request.Url.AbsoluteUri.Replace("?", "~~").Replace("&", "^");
            if (currently)
            {
                page.Response.Redirect(string.Concat(new object[] { "~/ErrorPage.aspx?errorType=", errorType, "&rurl=", str }));
                page.Response.End();
            }
            else
            {
                page.Response.Redirect("~/ErrorPage.aspx?errorType=" + errorType);
                page.Response.End();
            }
        }

        public static string GoodStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            string str2 = str;
            return str2.Replace("'", "''").Trim();
        }

        public static Model_VSysUserRole IsPageFlag()
        {
            Model_VSysUserRole role = null;
            if (HttpContext.Current.Session["LoginUser"] != null)
            {
                role = (Model_VSysUserRole) HttpContext.Current.Session["LoginUser"];
                HttpContext.Current.Session["LoginUser"] = role;
                return role;
            }
            ErrorDispose(1, true);
            HttpContext.Current.Response.End();
            return role;
        }

        public static Rc.Model.Resources.Model_F_User IsPageFlag(Page page)
        {
            Rc.Model.Resources.Model_F_User user = null;
            if (page.Session["FLoginUser"] != null)
            {
                user = (Rc.Model.Resources.Model_F_User) page.Session["FLoginUser"];
                page.Session["FLoginUser"] = user;
                return user;
            }
            ErrorDispose(page, 1, true);
            page.Response.End();
            return user;
        }

        public static bool IsSessionExisted(HttpSessionState Session, string sessionKey)
        {
            string str2 = sessionKey.ToLower();
            if (Session != null)
            {
                for (int i = 0; i < Session.Count; i++)
                {
                    if (Session.Keys[i].ToLower() == str2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

