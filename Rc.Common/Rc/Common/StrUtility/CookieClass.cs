namespace Rc.Common.StrUtility
{
    using System;
    using System.Web;

    public class CookieClass
    {
        public static string GetCookie(string CookieName)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                return cookie.Value;
            }
            return null;
        }

        public static string GetCookie(string CookieName, string CookieName2)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                return cookie[CookieName2];
            }
            return null;
        }

        public static void RemoveCookie(string cookie_name)
        {
            HttpCookie cookie = new HttpCookie(cookie_name);
            cookie.Expires = DateTime.Now.AddHours(-1.0);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SetCookie(string cookie_name, string cookie_value)
        {
            HttpCookie cookie = new HttpCookie(cookie_name);
            DateTime now = DateTime.Now;
            cookie.Value = cookie_value;
            cookie.Expires = now.AddHours(6.0);
            if (HttpContext.Current.Response.Cookies[cookie_name] != null)
            {
                HttpContext.Current.Response.Cookies.Remove(cookie_name);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}

