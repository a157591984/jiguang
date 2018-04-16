using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace Rc.Interface
{
    public static class AuthAPI_pfunction
    {
        /// <summary>
        /// 模块显示控制 180108TS
        /// </summary>
        public static bool GetClientMdlIsShow(string xPath)
        {
            bool flag = false;
            try
            {
                #region 读取xml
                XmlDocument xd = new XmlDocument();
                xd.Load(HttpContext.Current.Server.MapPath("/Common/mdl.xml"));
                //XmlNodeList root = xd.DocumentElement.SelectNodes(type);
                XmlNode xn = xd.DocumentElement.SelectSingleNode("/root/client/" + xPath);
                if (Convert.ToBoolean(xn.Attributes["isshow"].Value))
                {
                    flag = true;
                }

                #endregion

            }
            catch (Exception)
            {

            }
            return flag;
        }
        /// <summary>
        /// 解析/继续强化训练URL 17-09-18TS
        /// </summary>
        /// <param name="hostUrl">域名/ip地址（示例http://192.168.1.1/）</param>
        /// <param name="basicUrl">解析/继续强化训练接口请求路径（示例/authApi/?key=getTestAttr&resourceId=xxx&testId=xxx&attrType=AnalyzeData）</param>
        /// <returns></returns>
        public static string GetAnalyzeTrainUrl(string hostUrl, string basicUrl)
        {
            string temp = string.Empty;
            try
            {
                temp = hostUrl + basicUrl;
                //temp = "{0}" + basicUrl.Replace("/authApi/", "/");
            }
            catch (Exception)
            {

            }
            return temp;
        }

        /// <summary>
        /// 获得公网IP  
        /// </summary>
        /// <returns></returns>
        public static string GetRealIP()
        {
            string ip = string.Empty;
            try
            {
                HttpRequest request = HttpContext.Current.Request;

                if (request.ServerVariables["HTTP_VIA"] != null && request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch (Exception e)
            {

            }
            if (ip == "::1") ip = "127.0.0.1";
            return ip;
        }

        /// <summary>
        /// 得到站点域名包括端口
        /// </summary>
        /// <returns></returns>
        public static string getHostPath()
        {
            string hostPath = string.Empty;
            hostPath = "http://" + HttpContext.Current.Request.ServerVariables["Http_Host"].ToString();

            //hostPath = "http://" + HttpContext.Current.Request.Url.Host;
            //if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Port"]))
            //{
            //    hostPath += ":" + ConfigurationManager.AppSettings["Port"];
            //}
            //else
            //{
            //    if (!String.IsNullOrEmpty(HttpContext.Current.Request.Url.Host))
            //    {
            //        if (HttpContext.Current.Request.Url.Port != 80) hostPath += ":" + HttpContext.Current.Request.Url.Port;
            //    }
            //}
            return hostPath;
        }

        /// <summary>
        /// 转化成短日期yyyy-MM-dd
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToShortDate(string str)
        {
            string strTemp = str;
            DateTime dt = new DateTime();

            if (DateTime.TryParse(str, out dt))
            {
                strTemp = dt.ToString("yyyy-MM-dd");
            }
            return strTemp;
        }
        /// <summary>
        /// 转换时间 yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToLongDateTime(string str)
        {
            string strTemp = str;
            try
            {
                DateTime dt = new DateTime();
                if (DateTime.TryParse(str, out dt))
                {
                    strTemp = dt.ToString("yyyy-MM-dd HH:mm").Replace("1900-01-01 00:00", "");
                }
            }
            catch (Exception)
            {

            }

            return strTemp;
        }
        public static string ConvertToLongDateTime(string str, string format)
        {
            string strTemp = str;
            try
            {
                DateTime dt = new DateTime();
                if (DateTime.TryParse(str, out dt))
                {
                    string ftime = Convert.ToDateTime("1900-01-01 00:00:00").ToString(format);
                    strTemp = dt.ToString(format).Replace(ftime, "");
                }
            }
            catch (Exception)
            {

            }

            return strTemp;
        }

        /// <summary>
        /// 写入图片
        /// </summary>
        /// <param name="strFilepath">文件路径</param>
        /// <param name="strFileContent">内容</param>
        public static void WriteToImage(string strFilepath, string strFileContent)
        {
            try
            {
                strFilepath = strFilepath.Replace("/", "\\");
                string folder = strFilepath.Substring(0, strFilepath.LastIndexOf("\\") + 1);
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                byte[] bytes = Convert.FromBase64String(strFileContent);
                // 生成jpg图片
                File.WriteAllBytes(strFilepath, bytes);
            }
            finally
            {

            }

        }

        /// <summary>
        /// 写入文件
        /// </summary>
        public static void WriteToFile(string strFilepath, string strFileContent, bool isCover)
        {
            FileStream fs = null;
            try
            {
                strFilepath = strFilepath.Replace("/", "\\");
                string folder = strFilepath.Substring(0, strFilepath.LastIndexOf("\\") + 1);
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                if (!isCover && File.Exists(strFilepath))
                {
                    fs = new FileStream(strFilepath, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.WriteLine(strFileContent);
                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    File.WriteAllText(strFilepath, strFileContent, Encoding.UTF8);
                }
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

        }

        /// <summary>
        /// 从文件读取内容 17-09-22TS
        /// </summary>
        public static string ReadAllText(string strFilepath)
        {
            try
            {
                if (File.Exists(strFilepath))
                {
                    return File.ReadAllText(strFilepath, Encoding.Default);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>  
        /// 获取文件生成  FromBase64String 数据
        /// </summary>  
        /// <param name="serverfilpath"></param>  
        /// <param name="filename"></param>  
        public static void ToDownloadBase64(string serverfilpath, string filename)
        {
            FileStream fileStream = new FileStream(serverfilpath, FileMode.Open, FileAccess.Read, FileShare.Read);
            long fileSize = fileStream.Length;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + Uri.EscapeDataString(filename));
            ////attachment --- 作为附件下载  
            ////inline --- 在线打开  
            HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
            byte[] fileBuffer = new byte[fileSize];
            fileStream.Read(fileBuffer, 0, (int)fileSize);
            HttpContext.Current.Response.BinaryWrite(fileBuffer);
            fileStream.Close();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 第三方用户登录认证 17-12-18TS
        /// </summary>
        public static bool AuthUserLoginByIF(Model_ConfigSchool modelCS, string schoolCode, string userName, string password)
        {
            bool flag = false;

            if (schoolCode == ThirdPartyEnum.yzlyxx.ToString())
            {
                #region 扬州旅游商贸学校 用户认证
                string para = string.Format("UserName={0}&Password={1}", userName, password);
                if (Rc.Common.RemotWeb.PostDataToServer(modelCS.D_PublicValue + "/AuthApi/yzlyxx/auth.aspx", para, Encoding.UTF8, "POST") == "successful")
                {
                    flag = true;
                }

                #endregion
            }
            else if (schoolCode == ThirdPartyEnum.ahjzvs.ToString())
            {
                #region 安徽金寨职业学校
                object objUser = new BLL_F_User().GetModel(userName, Rc.Common.StrUtility.DESEncryptLogin.EncryptString(password));
                if (objUser != null)
                {
                    flag = true;
                }
                #endregion
            }

            return flag;
        }

    }
}
