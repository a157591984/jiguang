namespace Rc.Common
{
    using Rc.Common.SystemLog;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class RemotWeb
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public static HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            return (request.GetResponse() as HttpWebResponse);
        }

        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemotWeb.CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            if ((parameters != null) && (parameters.Count != 0))
            {
                StringBuilder builder = new StringBuilder();
                int num = 0;
                foreach (string str in parameters.Keys)
                {
                    if (num > 0)
                    {
                        builder.AppendFormat("&{0}={1}", str, parameters[str]);
                    }
                    else
                    {
                        builder.AppendFormat("{0}={1}", str, parameters[str]);
                    }
                    num++;
                }
                byte[] bytes = requestEncoding.GetBytes(builder.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            return (request.GetResponse() as HttpWebResponse);
        }

        public static string PostDataToServer(string url, IDictionary<string, string> parameters, string method, Encoding reqEncoding)
        {
            string str2;
            try
            {
                HttpWebResponse response;
                Encoding requestEncoding = reqEncoding;
                if (method.ToUpper() == "POST")
                {
                    response = CreatePostHttpResponse(url, parameters, 0x1388, null, requestEncoding, null);
                }
                else
                {
                    response = CreateGetHttpResponse(url, 0x1388, null, null);
                }
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, requestEncoding);
                string str = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                response.Close();
                str2 = str;
            }
            catch (Exception exception)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", "", "PostDataToServer2请求失败，url：" + url + "。错误信息：" + exception.Message.ToString());
                throw;
            }
            return str2;
        }

        public static string PostDataToServer(string url, string param, Encoding reqEncoding, string method)
        {
            HttpWebRequest request = null;
            try
            {
                HttpWebResponse response;
                url = url.Replace(@"\", "/");
                request = WebRequest.Create(url) as HttpWebRequest;
                method = method.ToUpper();
                string str3 = method;
                if (str3 != null)
                {
                    if (!(str3 == "GET"))
                    {
                        if (str3 == "POST")
                        {
                            goto Label_0059;
                        }
                    }
                    else
                    {
                        request.Method = "GET";
                    }
                }
                goto Label_0099;
            Label_0059:
                request.Method = "POST";
                byte[] bytes = reqEncoding.GetBytes(param);
                request.ContentType = "application/x-www-form-urlencoded;";
                request.ContentLength = bytes.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            Label_0099:
                response = (HttpWebResponse) request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string str = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                response.Close();
                return str;
            }
            catch (Exception exception)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", "", "PostDataToServer1请求失败，url：" + url + "。错误信息：" + exception.Message.ToString());
                return "";
            }
        }
    }
}

