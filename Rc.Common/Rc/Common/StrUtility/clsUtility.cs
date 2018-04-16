namespace Rc.Common.StrUtility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Security;
    using System.Web.UI;
    using System.Xml;

    public static class clsUtility
    {
        private static string encryptKey = "phhc";

        public static void AddCache(string CacheName, object CacheValue)
        {
            HttpContext.Current.Cache.Insert(CacheName, CacheValue, null, DateTime.Now.AddYears(100), Cache.NoSlidingExpiration);
        }

        public static void AddCache(string CacheName, DateTime dt, object CacheValue)
        {
            HttpContext.Current.Cache.Insert(CacheName, CacheValue, null, dt, Cache.NoSlidingExpiration);
        }

        public static bool CheckDs(DataSet ds)
        {
            return (((ds != null) && (ds.Tables.Count > 0)) && (ds.Tables[0].Rows.Count > 0));
        }

        public static bool CheckDt(DataTable dt)
        {
            return ((dt != null) && (dt.Rows.Count > 0));
        }

        public static bool CheckRows(DataRow[] rows)
        {
            return ((rows != null) && (rows.Length != 0));
        }

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

        public static string CutStr(int length, string src)
        {
            if (length > 0)
            {
                Font font = new Font("Arial", 10f);
                Bitmap image = new Bitmap(1, 1);
                Graphics graphics = Graphics.FromImage(image);
                SizeF ef = new SizeF();
                float num = (length * float.Parse("15.10417")) + float.Parse("4.4401");
                for (int i = 0; i < src.Length; i++)
                {
                    if (graphics.MeasureString(src.Substring(0, src.Length - i), font).Width <= num)
                    {
                        if (i != 0)
                        {
                            return (src.Substring(0, (src.Length - i) - 1) + "...");
                        }
                        return src;
                    }
                }
            }
            return "";
        }

        public static string Decrypt(this string Text)
        {
            if (Text == null)
            {
                return Text;
            }
            return Decrypt(Text, "phhcMSCommon");
        }

        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            int num = Text.Length / 2;
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                int num3 = Convert.ToInt32(Text.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte) num3;
            }
            provider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return Encoding.Default.GetString(stream.ToArray());
        }

        public static void DelCacheKeyByPrefix(string prefix)
        {
            List<string> cacheKeyByPrefix = new List<string>();
            if (prefix == "")
            {
                cacheKeyByPrefix = GetCacheKeyAll();
            }
            cacheKeyByPrefix = GetCacheKeyByPrefix(prefix);
            for (int i = 0; i < cacheKeyByPrefix.Count; i++)
            {
                DeleteCache(cacheKeyByPrefix[i]);
            }
        }

        public static void DeleteCache(string cacheName)
        {
            HttpContext.Current.Cache.Remove(cacheName);
        }

        public static string Encrypt(this string Text)
        {
            if (Text == null)
            {
                return Text;
            }
            return Encrypt(Text, "phhcMSCommon");
        }

        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(Text);
            provider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            return builder.ToString();
        }

        public static void ErrorDispose(int ErrorType, bool currently)
        {
            string absoluteUri = HttpContext.Current.Request.Url.AbsoluteUri;
            absoluteUri = HttpContext.Current.Server.UrlEncode(absoluteUri);
            if (currently)
            {
                HttpContext.Current.Response.Redirect(string.Concat(new object[] { "~/ErrorPage.aspx?errorType=", ErrorType, "&rurl=", absoluteUri }));
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/ErrorPage.aspx?errorType=" + ErrorType);
                HttpContext.Current.Response.End();
            }
        }

        public static void ErrorDispose(Page page, int ErrorType, bool currently)
        {
            string absoluteUri = page.Request.Url.AbsoluteUri;
            absoluteUri = page.Server.UrlEncode(absoluteUri);
            if (currently)
            {
                page.Response.Redirect(string.Concat(new object[] { "~/ErrorPage.aspx?errorType=", ErrorType, "&rurl=", absoluteUri }));
                page.Response.End();
            }
            else
            {
                page.Response.Redirect("~/ErrorPage.aspx?errorType=" + ErrorType);
                page.Response.End();
            }
        }

        public static void ErrorDispose(Page page, int ErrorType, string ErrorString, bool currently)
        {
            string absoluteUri = page.Request.Url.AbsoluteUri;
            absoluteUri = page.Server.UrlEncode(absoluteUri);
            if (currently)
            {
                page.Response.Redirect(string.Concat(new object[] { "~/ErrorPage.aspx?errorType=", ErrorType, "&errorString=", ErrorString.HtmlEncode(), "&rurl=", absoluteUri }));
                page.Response.End();
            }
            else
            {
                page.Response.Redirect(string.Concat(new object[] { "~/ErrorPage.aspx?errorType=", ErrorType, "&errorString=", ErrorString.HtmlEncode() }));
                page.Response.End();
            }
        }

        public static void ErrorDisposeF(Page page, int ErrorType, bool currently)
        {
            string absoluteUri = page.Request.Url.AbsoluteUri;
            absoluteUri = page.Server.UrlEncode(absoluteUri);
            if (currently)
            {
                page.Response.Redirect(string.Concat(new object[] { "~/ErrorPageF.aspx?errorType=", ErrorType, "&rurl=", absoluteUri }));
                page.Response.End();
            }
            else
            {
                page.Response.Redirect("~/ErrorPageF.aspx?errorType=" + ErrorType);
                page.Response.End();
            }
        }

        public static string GenerateByBase64(string encryptString)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(encryptString);
            provider.Key = Encoding.ASCII.GetBytes(encryptKey);
            provider.IV = Encoding.ASCII.GetBytes(encryptKey);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            builder.ToString();
            return builder.ToString();
        }

        public static object GetCache(string cacheName)
        {
            return HttpContext.Current.Cache[cacheName];
        }

        public static List<string> GetCacheKeyAll()
        {
            Cache cache = HttpRuntime.Cache;
            List<string> list = new List<string>();
            IDictionaryEnumerator enumerator = cache.GetEnumerator();
            if (enumerator != null)
            {
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Key.ToString());
                }
            }
            return list;
        }

        public static List<string> GetCacheKeyByPrefix(string prefix)
        {
            List<string> list = new List<string>();
            List<string> cacheKeyAll = GetCacheKeyAll();
            for (int i = 0; i < cacheKeyAll.Count; i++)
            {
                if (cacheKeyAll[i].ToString().IndexOf(prefix) == 0)
                {
                    list.Add(cacheKeyAll[i]);
                }
            }
            return list;
        }

        public static string GetCalculateTemplate(string nodeName)
        {
            string str = "CalculateTemplate.xml";
            XmlDocument document = new XmlDocument();
            XmlNode documentElement = null;
            if (GetConfigByKey("Calculate") == "true")
            {
                if (document == null)
                {
                    string cacheName = "Calculate";
                    object cache = GetCache(cacheName);
                    if (cache == null)
                    {
                        try
                        {
                            document.Load(getHostPath() + "/XMLFile/" + str);
                            cache = document;
                            if (cache != null)
                            {
                                AddCache(cacheName, cache);
                            }
                        }
                        catch
                        {
                        }
                    }
                    document = cache as XmlDocument;
                }
            }
            else
            {
                document.Load(getHostPath() + "/XMLFile/" + str);
            }
            documentElement = document.DocumentElement;
            XmlElement element = null;
            foreach (XmlNode node2 in documentElement.ChildNodes)
            {
                element = node2 as XmlElement;
                if (element.GetAttribute("name") == nodeName)
                {
                    return node2.ChildNodes[0].InnerText;
                }
            }
            return "";
        }

        public static string GetConfigByKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetConfigValue(string key)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
            {
                str = ConfigurationManager.AppSettings[key];
            }
            return str;
        }

        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetFileName()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static string GetFromBase64(string decryptString)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = new byte[decryptString.Length / 2];
            for (int i = 0; i < (decryptString.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(decryptString.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte) num2;
            }
            provider.Key = Encoding.ASCII.GetBytes(encryptKey);
            provider.IV = Encoding.ASCII.GetBytes(encryptKey);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            new StringBuilder();
            return Encoding.Default.GetString(stream.ToArray());
        }

        public static string getHostPath()
        {
            return ("http://" + HttpContext.Current.Request.ServerVariables["Http_Host"].ToString());
        }

        public static List<string> getListSet(List<string> leftList, List<string> rightList)
        {
            List<string> list = new List<string>();
            if (rightList.Count == 0)
            {
                return leftList;
            }
            for (int i = 0; i < leftList.Count; i++)
            {
                if (!rightList.Contains(leftList[i]))
                {
                    list.Add(leftList[i]);
                }
            }
            return list;
        }

        public static string GetMd5(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToLower();
        }

        public static string GetPrescriptionReview(string nodeName)
        {
            string str = "PrescriptionReview.xml";
            XmlDocument document = new XmlDocument();
            XmlNode documentElement = null;
            if (GetConfigByKey("Calculate") == "true")
            {
                if (document == null)
                {
                    string cacheName = "Calculate";
                    object cache = GetCache(cacheName);
                    if (cache == null)
                    {
                        try
                        {
                            document.Load(getHostPath() + "/XMLFile/" + str);
                            cache = document;
                            if (cache != null)
                            {
                                AddCache(cacheName, cache);
                            }
                        }
                        catch
                        {
                        }
                    }
                    document = cache as XmlDocument;
                }
            }
            else
            {
                document.Load(getHostPath() + "/XMLFile/" + str);
            }
            documentElement = document.DocumentElement;
            XmlElement element = null;
            foreach (XmlNode node2 in documentElement.ChildNodes)
            {
                element = node2 as XmlElement;
                if (element.GetAttribute("name") == nodeName)
                {
                    return node2.ChildNodes[0].InnerText;
                }
            }
            return "";
        }

        public static string GetRandomStr(int passwordLen)
        {
            string str = "abcdefghijklmnopqrstuvwxyz0123456789";
            string str2 = string.Empty;
            Random random = new Random();
            for (int i = 0; i < passwordLen; i++)
            {
                int num = random.Next(str.Length);
                str2 = str2 + str[num];
            }
            return str2;
        }

        public static string GetSysCode()
        {
            return GetConfigValue("SYSCODE");
        }

        public static string GetWeek(DateTime date, int enOrch, ref int daysInWeek)
        {
            if (enOrch > 1)
            {
                enOrch = 0;
            }
            List<string> list = new List<string> { "星期一,Monday", "星期二,Tuesday", "星期三,Wednesday", "星期四,Thursday", "星期五,Friday", "星期六,Saturday", "星期日,Sunday" };
            string str = string.Empty;
            string str2 = string.Empty;
            str2 = date.DayOfWeek.ToString();
            for (int i = 0; i < list.Count; i++)
            {
                if (str2 == list[i].Split(new char[] { ',' })[1])
                {
                    str = list[i].Split(new char[] { ',' })[enOrch];
                    daysInWeek = i + 1;
                }
            }
            if (str == string.Empty)
            {
                str = str2;
            }
            return str;
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

        public static object IsFPageFlag()
        {
            object obj2 = null;
            if (HttpContext.Current.Session["FLoginUser"] != null)
            {
                obj2 = HttpContext.Current.Session["FLoginUser"];
                HttpContext.Current.Session["FLoginUser"] = obj2;
                return obj2;
            }
            ErrorDispose(1, true);
            HttpContext.Current.Response.End();
            return obj2;
        }

        public static object IsFPageFlag(Page page)
        {
            object obj2 = null;
            if (page.Session["FLoginUser"] != null)
            {
                obj2 = page.Session["FLoginUser"];
                page.Session["FLoginUser"] = obj2;
                return obj2;
            }
            ErrorDisposeF(page, 1, true);
            page.Response.End();
            return obj2;
        }

        public static object IsPageFlag()
        {
            object obj2 = null;
            if (HttpContext.Current.Session["LoginUser"] != null)
            {
                obj2 = HttpContext.Current.Session["LoginUser"];
                HttpContext.Current.Session["LoginUser"] = obj2;
                return obj2;
            }
            ErrorDispose(1, true);
            HttpContext.Current.Response.End();
            return obj2;
        }

        public static object IsPageFlag(Page page)
        {
            object obj2 = null;
            if (page.Session["LoginUser"] != null)
            {
                obj2 = page.Session["LoginUser"];
                page.Session["LoginUser"] = obj2;
                return obj2;
            }
            ErrorDispose(page, 1, true);
            page.Response.End();
            return obj2;
        }

        public static string ReDoStr(string str, char split)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str2 in str.Split(new char[] { split }))
            {
                builder.AppendFormat("'{0}',", str2);
            }
            return builder.ToString().TrimEnd(new char[] { ',' });
        }

        public static string SetDateFormat(object date)
        {
            if ((date != DBNull.Value) && (date != null))
            {
                return Convert.ToDateTime(date).ToString("yyyy-MM-dd HH:mm");
            }
            return "";
        }

        public static string SetDateFormat(object date, string format)
        {
            if ((date != DBNull.Value) && (date != null))
            {
                return Convert.ToDateTime(date).ToString(format);
            }
            return "";
        }

        public static string SetDateFormatByShort(object date)
        {
            if ((date != DBNull.Value) && (date != null))
            {
                return Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            }
            return "";
        }

        public static string SetOracleDateFormat(object date)
        {
            if ((date != DBNull.Value) && (date != null))
            {
                return ("to_date('" + date + "','yyyy-mm-dd hh24:mi:ss')");
            }
            return "";
        }

        public static string SQLDecrypt(string Text)
        {
            if (Text == null)
            {
                return Text;
            }
            return Decrypt(Text, "!@#$%^&*qwertyuiop1234567890");
        }

        public static string SQLEncrypt(string Text)
        {
            if (Text == null)
            {
                return Text;
            }
            return Encrypt(Text, "!@#$%^&*qwertyuiop1234567890");
        }
    }
}

