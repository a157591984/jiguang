using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.IO.Compression;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Common.Config;
using System.Net;
using Rc.Common;
using Newtonsoft.Json;
using System.Net.Mail;
using Rc.Common.StrUtility;
using System.Xml;

namespace Rc.Cloud.Web.Common
{
    public static class pfunction
    {
        /// <summary>
        /// 模块显示控制 180108TS
        /// </summary>
        public static bool GetWebMdlIsShow(string xPath)
        {
            bool flag = false;
            try
            {
                #region 读取xml
                XmlDocument xd = new XmlDocument();
                xd.Load(HttpContext.Current.Server.MapPath("/Common/mdl.xml"));
                //XmlNodeList root = xd.DocumentElement.SelectNodes(type);
                XmlNode xn = xd.DocumentElement.SelectSingleNode("/root/web/" + xPath);
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
        /// 处理试题分值（.0替换成空，-1不显示） 17-06-08
        /// </summary>
        /// <param name="tq_score"></param>
        /// <returns></returns>
        public static string ConvertTQScore(string tq_score)
        {
            string temp = string.Empty;
            try
            {
                temp = tq_score.Replace(".0", "");
                if (temp == "-1")
                {
                    temp = "";
                }
            }
            catch (Exception)
            {
                temp = tq_score;
            }
            return temp;
        }

        #region 发送信息
        public static string GetSendMesageStatus(string content)
        {
            string str = "发送成功：" + content;
            switch (content)
            {
                case "-1":
                    str = "账号/密码或企业ID错误";
                    break;
                case "-2":
                    str = "缺少企业账号";
                    break;
                case "-3":
                    str = "缺少密码";
                    break;
                case "-4":
                    str = "缺少短信内容";
                    break;
                case "-5":
                    str = "缺少目标号码";
                    break;
                case "-6":
                    str = "用户不是企业用户";
                    break;
                case "-7":
                    str = "短信内容过长";
                    break;
                case "-8":
                    str = "内容中存在敏感词";
                    break;
                case "-9":
                    str = "目标号码格式错误，或者包含错误的手机号码";
                    break;
                case "-10":
                    str = "超过规定发送时间，禁止提交发送";
                    break;
                case "-12":
                    str = "余额不足，请及时充值";
                    break;
                case "-14":
                    str = "号码发送次数超过发送限制次数";
                    break;
                case "-15":
                    str = "发送内容前面需加签名";
                    break;
                case "-16":
                    str = "提交号码数量小于最小提交量限制";
                    break;
                case "-20":
                    str = "未开通接口";
                    break;
                case "-22":
                    str = "短信内容签名不正确";
                    break;
                case "-23":
                    str = "IP鉴权失败";
                    break;
                case "-24":
                    str = "缺少企业ID";
                    break;
                case "-25":
                    str = "签名扩展匹配异常，可能已无可用扩展";
                    break;
                case "-99":
                    str = "连接失败";
                    break;
                case "-100":
                    str = "系统内部错误";
                    break;
            }
            return str;

        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phones">手机号</param>
        /// <param name="msg">短信内容</param>
        /// <returns></returns>
        public static void SendSMS(string mobiles, string msg)
        {
            string content = "0";
            try
            {
                Model_SendMessageTemplate model = new Model_SendMessageTemplate();
                BLL_SendMessageTemplate bll = new BLL_SendMessageTemplate();
                model = bll.GetModelBySType(Rc.Model.Resources.SMSPAYTemplateEnum.SMS.ToString());
                if (model != null && model.IsStart == 1)
                {
                    string userid = model.UserId;
                    string username = model.UserName;
                    string password = model.PassWord;
                    string[] mobileArr = mobiles.Split(',');//发送号码 英文逗号隔开 最多100个
                    string url = model.MsgUrl;
                    string message = model.Content + msg;
                    string Method = model.Method;
                    string param = "userid=" + userid + "&username=" + username + "&passwordMd5=" + Rc.Common.StrUtility.clsUtility.GetMd5(password.ToString()) + "&mobile=" + mobiles + "&message=" + HttpUtility.UrlEncode(message, Encoding.GetEncoding("GBK"));
                    content = Rc.Common.RemotWeb.PostDataToServer(url, param, System.Text.Encoding.UTF8, Method);
                    content = pfunction.GetSendMesageStatus(content);
                    List<Model_SendSMSRecord> list = new List<Model_SendSMSRecord>();
                    Model_SendSMSRecord modelR = new Model_SendSMSRecord();
                    BLL_SendSMSRecord bllR = new BLL_SendSMSRecord();
                    for (int i = 0; i < mobileArr.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(mobileArr[i]))
                        {
                            modelR = new Model_SendSMSRecord();
                            modelR.SendSMSRecordId = Guid.NewGuid().ToString();
                            modelR.Mobile = mobileArr[i];
                            modelR.Content = message;
                            modelR.Status = content;
                            modelR.CTime = DateTime.Now;
                            list.Add(modelR);
                        }
                    }
                    // bllR.AddMultiSMS(list);
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("发送短信失败", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                , ex.TargetSite.Name.ToString(), ex.Message));
            }

        }

        public static void SendSMS_New(string apiType, string mobiles, Dictionary<string, string> Para, string msg, string stype, string status, string School_Id)
        {
            string content = "0";
            try
            {
                Model_SendMessageTemplate model = new Model_SendMessageTemplate();
                BLL_SendMessageTemplate bll = new BLL_SendMessageTemplate();
                model = bll.GetModelBySType(Rc.Model.Resources.SMSPAYTemplateEnum.SMS.ToString());
                int Minus = 0;
                if (model != null && model.IsStart == 1)
                {
                    string[] mobileArr = mobiles.Split(',');//发送号码 英文逗号隔开 最多100个
                    string url = model.MsgUrl + apiType;
                    string Method = model.Method;
                    Para.Add("mobilenum", model.UserName);
                    Para.Add("sys_language_type", "net");
                    Para.Add("tellfrom", mobiles);
                    Para.Add("textqianming", model.Content);

                    YunHuaTong.Lib.Encrypt.MakeKey(Para, model.PassWord);

                    content = Rc.Common.RemotWeb.PostDataToServer(url, Para, Method, System.Text.Encoding.UTF8);
                    ModelSMSResult modelResult = JsonConvert.DeserializeObject<ModelSMSResult>(content);
                    List<Model_SendSMSRecord> list = new List<Model_SendSMSRecord>();
                    Model_SendSMSRecord modelR = new Model_SendSMSRecord();
                    BLL_SendSMSRecord bllR = new BLL_SendSMSRecord();
                    for (int i = 0; i < mobileArr.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(mobileArr[i]))
                        {
                            modelR = new Model_SendSMSRecord();
                            modelR.SendSMSRecordId = Guid.NewGuid().ToString();
                            modelR.Mobile = mobileArr[i];
                            modelR.Content = msg;
                            modelR.ReturnStatus = modelResult.Code;
                            modelR.ReturnContent = UnicodeToGB(modelResult.Mess);
                            modelR.SchoolId = School_Id;
                            if (modelResult.Code == "0")
                            {
                                modelR.Status = "false";
                            }
                            else
                            {
                                Minus++;//发送成功累加
                                modelR.Status = status;
                            }
                            modelR.SType = stype;
                            modelR.CTime = DateTime.Now;
                            list.Add(modelR);
                        }
                    }
                    bllR.AddMultiSMS(list);
                }
                else
                {
                    new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("发送短信失败", "未找到短信配置信息");
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("发送短信失败", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                , ex.TargetSite.Name.ToString(), ex.Message));
            }

        }

        public class ModelSMSResult
        {
            public string Code { get; set; }
            public string Mess { get; set; }
        }
        /// <summary>
        /// Unicode转中文
        /// </summary>
        public static string UnicodeToGB(string text)
        {
            System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(text, "\\\\u([\\w]{4})");
            if (mc != null && mc.Count > 0)
            {
                foreach (System.Text.RegularExpressions.Match m2 in mc)
                {
                    string v = m2.Value;
                    string word = v.Substring(2);
                    byte[] codes = new byte[2];
                    int code = Convert.ToInt32(word.Substring(0, 2), 16);
                    int code2 = Convert.ToInt32(word.Substring(2), 16);
                    codes[0] = (byte)code2;
                    codes[1] = (byte)code;
                    text = text.Replace(v, Encoding.Unicode.GetString(codes));
                }
            }
            else
            {

            }
            return text;
        }


        public static string GetSMSStatus()
        {
            string temp = string.Empty;
            try
            {
                Model_SendMessageTemplate model = new Model_SendMessageTemplate();
                BLL_SendMessageTemplate bll = new BLL_SendMessageTemplate();
                model = bll.GetModelBySType(Rc.Model.Resources.SMSPAYTemplateEnum.SMS.ToString());
                string username = model.UserName;
                string password = model.PassWord;
                string url = string.Empty;
                url = "http://139.129.107.247/sms/xml/rpt/acquire?username=" + username.ToString() + "&passwordMd5=" + Rc.Common.StrUtility.clsUtility.GetMd5(password.ToString());

                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";
                using (WebResponse wr = req.GetResponse())
                {
                    //在这里对接收到的页面内容进行处理
                    Stream stream = wr.GetResponseStream();
                    StreamReader streamReader = new StreamReader(stream, System.Text.Encoding.GetEncoding("UTF-8"));
                    temp = streamReader.ReadToEnd();

                    streamReader.Close();

                }

            }
            catch (Exception)
            {

            }
            return temp;
        }
        #endregion
        public static string SubUserName(string temp)
        {
            try
            {
                if (temp.Length < 3)
                {
                    return temp.Substring(0, 1) + "****";
                }
                else
                {
                    return temp.Substring(0, 2) + "****";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #region 发送邮件
        /// <summary>
        /// 发送邮件
        /// </summary>
        public static bool SendMail(string receiveEmail, string realname, string url)
        {
            bool flag = false;
            try
            {
                //邮箱没加密 这个是问题
                StringBuilder strOut = new StringBuilder();
                string subject = Rc.Common.ConfigHelper.GetConfigString("WebSiteName") + "—重置密码";
                strOut.Append("<div style' font-size:14px;>	<div style='padding: 10px 0px;'><div style=padding: 2px 20px 30px;><p>亲爱的 <span style='color: rgb(196, 0, 0);'>" + SubUserName(realname) + "</span> , 您好！</p><p>请点击下面的链接进行重置密码:</p><p style='overflow: hidden; width: 100%; word-wrap: break-word;'><a title='去重置密码' href='" + url + "' target='_blank' swaped='true'>" + url + "</a><br><span style='color: rgb(153, 153, 153);'>(如果链接无法点击，请将它拷贝到浏览器的地址栏中)</span></p></div></div></div>");
                Model_SendMessageTemplate model = new Model_SendMessageTemplate();
                BLL_SendMessageTemplate bll = new BLL_SendMessageTemplate();
                model = bll.GetModelBySType(Rc.Model.Resources.SMSPAYTemplateEnum.Mail.ToString());
                if (model != null)
                {
                    string smtpHost = model.MsgUrl;
                    string froms = model.UserName;
                    string password = model.PassWord;
                    int intPort = 25;
                    int.TryParse(model.Content, out intPort);

                    SmtpClient client = new SmtpClient(smtpHost);
                    client.Timeout = 10000;
                    client.Port = intPort;
                    client.EnableSsl = true;

                    MailAddress from = new MailAddress(froms, Rc.Common.ConfigHelper.GetConfigString("WebSiteName"));
                    MailAddress to = new MailAddress(receiveEmail);

                    MailMessage message = new MailMessage(from, to);
                    message.Subject = subject;//设置邮件主题
                    message.IsBodyHtml = true;//设置邮件正文为html格式
                    message.Body = strOut.ToString();//设置邮件内容
                    //设置发送邮件身份验证方式
                    client.Credentials = new NetworkCredential(froms, password);
                    client.Send(message);
                    flag = true;
                }
            }
            catch
            {

            }
            return flag;

        }
        #endregion

        /// <summary>
        /// 秒转换成（xx时xx分xx秒） 16-11-29TS
        /// </summary>
        public static string ConvertSecond(string sec)
        {
            try
            {
                int secNum = int.Parse(sec);
                string temp = string.Empty;
                if (secNum / 3600 != 0) temp += (secNum / 3600) + "小时";
                if (secNum % 3600 / 60 != 0) temp += (secNum % 3600 / 60) + "分钟";
                if (secNum % 60 != 0) temp += (secNum % 60) + "秒";
                return temp;
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 过滤关键字
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        //public static string FilterKeyWord(this string sInput)
        //{
        //    if (sInput == null || sInput == "")
        //        return null;
        //    string sInput1 = sInput.ToLower();
        //    string output = sInput;
        //    DataTable dtF = new BLL_SysFilter().GetList("").Tables[0];
        //    if (dtF.Rows.Count > 0)
        //    {
        //        string pattern = dtF.Rows[0]["KeyWord"].ToString().TrimEnd('，');
        //        pattern = pattern.Replace(",", "|").Replace("，", "|");
        //        output = Regex.Replace(sInput, pattern, "", RegexOptions.IgnoreCase);
        //        return output;
        //    }
        //    else
        //    {
        //        return sInput;
        //    }
        //}
        public static bool FilterKeyWords(string sInput)
        {
            if (sInput == null || sInput == "")
                return false;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            DataTable dtF = new BLL_SysFilter().GetFilterByCache().Tables[0];
            if (dtF.Rows.Count > 0)
            {
                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    if (sInput.IndexOf(dtF.Rows[i]["KeyWord"].ToString()) > -1)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// SetDdlYear 填充下拉列表 年 从2011 年开始到 当前年+1
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="startYear">开始年份</param>
        /// <param name="endYear">结束年份</param>
        /// <param name="isShowPleaseSelect"></param>
        public static void SetDdlYear(DropDownList ddl, int startYear, int endYear, bool isShowPleaseSelect, string FristText)
        {
            ListItem item = null;
            ddl.Items.Clear();
            if (isShowPleaseSelect)
            {
                item = new ListItem((FristText != "") ? FristText : "--请选择--", "-1");//请选择
                ddl.Items.Add(item);
            }
            for (int y = startYear; y <= endYear; y++)
            {
                item = new ListItem(y.ToString(), y.ToString());
                ddl.Items.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remotingFile"></param>
        /// <param name="localFile"></param>
        /// <param name="fileName"></param>
        public static string DownLoadFileByWebClient(string remotingFileUrl, string localFilePath, string fileName)
        {
            try
            {
                WebClient myWebClient = new WebClient();
                string strRemotingFile = remotingFileUrl + fileName;
                string strLocalFile = localFilePath + fileName;
                if (!Directory.Exists(localFilePath))
                {
                    Directory.CreateDirectory(localFilePath);
                }
                myWebClient.DownloadFile(strRemotingFile, strLocalFile);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
                //throw;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remotingFile"></param>
        /// <param name="localFile"></param>
        /// <param name="fileName"></param>
        public static string DownLoadFileByWebClient(string remotingFileUrl, string localFilePath, string fileName, string saveFileName)
        {
            try
            {
                WebClient myWebClient = new WebClient();
                string strRemotingFile = remotingFileUrl + fileName;
                string strLocalFile = localFilePath + saveFileName;
                if (!Directory.Exists(localFilePath))
                {
                    Directory.CreateDirectory(localFilePath);
                }
                myWebClient.DownloadFile(strRemotingFile, strLocalFile);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }




        }
        /// <summary>
        /// 判断网页地址是否可以访问
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool UrlExistsUsingHttpWebRequest(this string url)
        {
            try
            {
                System.Net.HttpWebRequest myRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                myRequest.Method = "HEAD";
                myRequest.Timeout = 100;
                System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)myRequest.GetResponse();
                return (res.StatusCode == System.Net.HttpStatusCode.OK);
            }
            catch (System.Net.WebException we)
            {
                System.Diagnostics.Trace.Write(we.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取资源访问地址
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetResourceHost(string key)
        {
            string temp = string.Empty;
            try
            {
                string strUserPublicUrl_Cookie = Rc.Common.StrUtility.CookieClass.GetCookie("UserPublicUrl_Cookie");
                if (!string.IsNullOrEmpty(strUserPublicUrl_Cookie)) // 局域网地址cookie
                {
                    temp = Rc.Common.DBUtility.DESEncrypt.Decrypt(strUserPublicUrl_Cookie);
                }
                else
                {
                    if (HttpContext.Current.Session["UserPublicUrl"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["UserPublicUrl"].ToString()))
                    {
                        temp = HttpContext.Current.Session["UserPublicUrl"].ToString();
                    }
                    else
                    {
                        temp = Rc.Common.ConfigHelper.GetConfigString(key);
                    }
                }
            }
            catch (Exception)
            {
                temp = Rc.Common.ConfigHelper.GetConfigString(key);
            }
            return temp;
        }
        /// <summary>
        /// 获取资源访问地址 不从cookie中获取学校局域网IP 17-09-12TS
        /// </summary>
        public static string GetResourceHost2(string key)
        {
            string temp = string.Empty;
            try
            {
                if (HttpContext.Current.Session["UserPublicUrl"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["UserPublicUrl"].ToString()))
                {
                    temp = HttpContext.Current.Session["UserPublicUrl"].ToString();
                }
                else
                {
                    temp = Rc.Common.ConfigHelper.GetConfigString(key);
                }
            }
            catch (Exception)
            {
                temp = Rc.Common.ConfigHelper.GetConfigString(key);
            }
            return temp;
        }

        /// <summary>
        /// 获取资源访问地址（家长web端查看学生作业） 17-09-14TS
        /// </summary>
        public static string GetResourceHost(string key, string userId)
        {
            string temp = string.Empty;
            try
            {
                string strUserPublicUrl_Cookie = Rc.Common.StrUtility.CookieClass.GetCookie("UserPublicUrl_Cookie" + userId);
                if (!string.IsNullOrEmpty(strUserPublicUrl_Cookie)) // 局域网地址cookie
                {
                    temp = Rc.Common.DBUtility.DESEncrypt.Decrypt(strUserPublicUrl_Cookie);
                }
                else
                {
                    if (HttpContext.Current.Session["UserPublicUrl" + userId] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["UserPublicUrl" + userId].ToString()))
                    {
                        temp = HttpContext.Current.Session["UserPublicUrl" + userId].ToString();
                    }
                    else
                    {
                        temp = Rc.Common.ConfigHelper.GetConfigString(key);
                    }
                }
            }
            catch (Exception)
            {
                temp = Rc.Common.ConfigHelper.GetConfigString(key);
            }
            return temp;

        }

        /// <summary>
        /// 导入excel 验证文件类型
        /// </summary>
        /// <param name="newFile"></param>
        /// <returns></returns>
        public static bool VerifyFileExtensionForImportExcel(string newFile)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(newFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
                string fileclass = "";
                byte buffer;
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
                r.Close();
                fs.Close();
                Dictionary<String, String> ftype = new Dictionary<string, string>();
                //添加允许的文件类型
                ftype.Add("208207", "xls");
                if (ftype.ContainsKey(fileclass))
                {
                    return true;
                }
                else
                {
                    System.IO.File.Delete(newFile);
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据班级Id获取所在学校Id2016-03-30
        /// </summary>
        /// <returns></returns>
        public static string GetSchoolIdByClassId(string classId)
        {
            string temp = string.Empty;
            try
            {
                BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                List<Model_UserGroup_Member> listModelUGCM = bll.GetModelList("MembershipEnum='" + MembershipEnum.classrc + "' and User_Id='" + classId + "'");
                Model_UserGroup_Member modelUGCM = listModelUGCM[0];

                List<Model_UserGroup_Member> listModelUGGM = bll.GetModelList("MembershipEnum='" + MembershipEnum.grade + "' and User_Id='" + modelUGCM.UserGroup_Id + "'");
                temp = listModelUGGM[0].UserGroup_Id;
            }
            catch (Exception)
            {

            }
            return temp;
        }
        /// <summary>
        /// 从群组池获取未使用群组号
        /// </summary>
        /// <returns></returns>
        public static string GetNewUserGroupID()
        {
            BLL_ClassPool bllClassPool = new BLL_ClassPool();
            string strUserGroupId = bllClassPool.GetNewClassID();
            if (string.IsNullOrEmpty(strUserGroupId))
            {
                strUserGroupId = "无可用群组号";
            }
            return strUserGroupId;
        }
        /// <summary>
        /// 根据Type,Name获取通用字典Id 2016-03-30
        /// </summary>
        /// <param name="DicId"></param>
        /// <returns></returns>
        public static string GetCommon_DictId(string D_Type, string D_Name)
        {
            string temp = string.Empty;
            try
            {
                List<Model_Common_Dict> list = new BLL_Common_Dict().GetModelList(" D_Type='" + D_Type + "' and D_Name='" + D_Name + "' ");
                Model_Common_Dict model = list[0];
                temp = model.Common_Dict_ID;
            }
            catch (Exception)
            {

            }
            return temp;
        }
        /// <summary>
        /// 通用字典 名称2016-03-23
        /// </summary>
        /// <param name="DicId"></param>
        /// <returns></returns>
        public static string GetCommon_DictName(string DicId)
        {
            string temp = string.Empty;
            try
            {
                List<Model_Common_Dict> list = new BLL_Common_Dict().GetModelList("Common_Dict_Id='" + DicId + "'");
                Model_Common_Dict model = list[0];
                temp = model.D_Name;
            }
            catch (Exception)
            {

            }
            return temp;
        }
        /// <summary>
        /// 验证用户是否为群组创建者
        /// </summary>
        /// <param name="groupId">群组Id</param>
        /// <param name="loginUserId">登录用户Id</param>
        /// <returns></returns>
        public static bool CheckUserIsGroupOwner(string groupId, string loginUserId)
        {
            bool temp = false;
            try
            {
                //Model_UserGroup modelGroup = new BLL_UserGroup().GetModel(groupId);
                //if (modelGroup != null && modelGroup.User_ID == loginUserId)
                //{
                //    temp = true;
                //}
                //else
                //{
                //    Model_UserGroup_Member model = new BLL_UserGroup_Member().GetModel(groupId, loginUserId);
                //    if (model.MembershipEnum == MembershipEnum.principal.ToString() || model.MembershipEnum == MembershipEnum.gradedirector.ToString() || model.MembershipEnum == MembershipEnum.headmaster.ToString())
                //    {
                //        temp = true;
                //    }
                //}
                Model_UserGroup_Member model = new BLL_UserGroup_Member().GetModel(groupId, loginUserId);
                if (model.MembershipEnum == MembershipEnum.principal.ToString() || model.MembershipEnum == MembershipEnum.gradedirector.ToString() || model.MembershipEnum == MembershipEnum.headmaster.ToString())
                {
                    temp = true;
                }

            }
            catch (Exception)
            {

            }
            return temp;
        }
        /// <summary>
        /// 验证用户超时后，重新登录跳转页面不属于当前登录用户的问题
        /// </summary>
        public static void CheckUserPage(Rc.Model.Resources.Model_F_User loginUser)
        {
            try
            {
                string iurl = HttpContext.Current.Request.Url.ToString().ToLower();
                if (loginUser.UserIdentity == "T")
                {
                    if (loginUser.UserPost == UserPost.校长 || loginUser.UserPost == UserPost.副校长 || loginUser.UserPost == UserPost.教务主任 || loginUser.UserPost == UserPost.教研组长 || loginUser.UserPost == UserPost.年级组长 || loginUser.UserPost == UserPost.备课组长)
                    {
                        if (iurl.IndexOf("/teacher/") == -1
                            && iurl.IndexOf("/principal/") == -1
                            && iurl.IndexOf("/evaluation/") == -1
                            && iurl.IndexOf("/ohomeworkviewtt.aspx") == -1
                            && iurl.IndexOf("/studentanalysisreports.aspx") == -1 && iurl.IndexOf("/teachingplanshow.aspx") == -1
                            && iurl.IndexOf("/commentreport.aspx") == -1) HttpContext.Current.Response.Redirect("/teacher/basicSetting.aspx", false);
                    }
                    else if (loginUser.UserPost == UserPost.普通老师)
                    {
                        if (iurl.IndexOf("/teacher/") == -1
                            && iurl.IndexOf("/evaluation/") == -1
                            && iurl.IndexOf("/ohomeworkviewtt.aspx") == -1) HttpContext.Current.Response.Redirect("/teacher/basicSetting.aspx", false);
                    }
                }
                else if (loginUser.UserIdentity == "S")
                {
                    if (iurl.IndexOf("/student/") == -1
                        && iurl.IndexOf("/studentanalysisreports.aspx") == -1
                        && iurl.IndexOf("/teachingplanshow.aspx") == -1
                        && iurl.IndexOf("/correct_client.aspx") == -1
                        && iurl.IndexOf("/correctt.aspx") == -1
                        && iurl.IndexOf("/homeworkpreviewt.aspx") == -1) HttpContext.Current.Response.Redirect("/student/oHomework.aspx", false);
                }
                else if (loginUser.UserIdentity == "P")
                {
                    if (iurl.IndexOf("/parent/") == -1
                        && iurl.IndexOf("/ohomeworkviewtt.aspx") == -1
                        && iurl.IndexOf("/studentanalysisreports.aspx") == -1 && iurl.IndexOf("/teachingplanshow.aspx") == -1) HttpContext.Current.Response.Redirect("/parent/student.aspx", false);
                }
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("/index.aspx");
            }
        }

        //过滤HTML
        public static string NoHTML(this string Htmlstring)
        {
            Htmlstring = Regex.Replace(Htmlstring, @"<p[^>]*?>.*?</p>", "", RegexOptions.IgnoreCase);
            //Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "style=\"[^\"]*\"", "");
            //Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "face=\"[^\"]*\"", "");
            //Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "color=\"[^\"]*\"", "");
            //Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "font-size=\"[^\"]*\"", "");
            //Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "<(?!font|/font).*?>", "");
            //Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "color:[^\"]*?;", "");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "font-family:[^\"]*?;", "");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "<br.*?>", "");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "(&nbsp;){2,}", "　");
            //Htmlstring.Replace("<br.*?>", "");
            Htmlstring = Htmlstring.Replace("&nbsp;", " ");
            return Htmlstring;
        }
        //过滤HTML
        public static string DeleteHTML(this string Htmlstring)
        {
            Htmlstring = Regex.Replace(Htmlstring, @"<p[^>]*?>.*?</p>", "", RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "style=\"[^\"]*\"", "");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "face=\"[^\"]*\"", "");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "color=\"[^\"]*\"", "");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "font-size=\"[^\"]*\"", "");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "<(?!font|/font).*?>", "");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "color:[^\"]*?;", "");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "font-family:[^\"]*?;", "");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "<br.*?>", "");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, "(&nbsp;){2,}", "　");
            //Htmlstring.Replace("<br.*?>", "");
            Htmlstring = Htmlstring.Replace("&nbsp;", " ");
            return Htmlstring;
        }
        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="strUncompressed">未压缩的字符串</param>
        /// <returns>压缩的字符串</returns>
        public static string StringCompress(string strUncompressed)
        {
            byte[] bytData = System.Text.Encoding.Unicode.GetBytes(strUncompressed);
            MemoryStream ms = new MemoryStream();
            Stream s = new GZipStream(ms, CompressionMode.Compress);
            s.Write(bytData, 0, bytData.Length);
            s.Close();
            byte[] dataCompressed = (byte[])ms.ToArray();
            return System.Convert.ToBase64String(dataCompressed, 0, dataCompressed.Length);
        }
        /// <summary>  
        /// 下载文件  
        /// </summary>  
        /// <param name="serverfilpath"></param>  
        /// <param name="filename"></param>  
        public static void ToDownload(string serverfilpath, string filename)
        {
            FileStream fileStream = new FileStream(serverfilpath, FileMode.Open, FileAccess.Read, FileShare.Read);
            long fileSize = fileStream.Length;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
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
        /// 获取文件生成  FromBase64String 数据 -带权限信息
        /// </summary>  
        /// <param name="serverfilpath"></param>  
        /// <param name="filename"></param>  
        public static void ToDownloadBase64(string serverfilpath, string filename, string strJson)
        {
            FileStream fileStream = new FileStream(serverfilpath, FileMode.Open, FileAccess.Read, FileShare.Read);
            long fileSize = fileStream.Length;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + Uri.EscapeDataString(filename) + ",Json=" + strJson);
            ////attachment --- 作为附件下载  
            ////inline --- 在线打开  
            HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
            byte[] fileBuffer = new byte[fileSize];
            fileStream.Read(fileBuffer, 0, (int)fileSize);
            HttpContext.Current.Response.BinaryWrite(fileBuffer);
            fileStream.Close();
            HttpContext.Current.Response.End();
        }
        public static void ToDownloadE(string strfileStream, string filename)
        {
            byte[] byteArray = System.Convert.FromBase64String(strfileStream);


            long fileSize = byteArray.Length;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + Uri.EscapeDataString(filename));
            ////attachment --- 作为附件下载  
            ////inline --- 在线打开  
            HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
            byte[] fileBuffer = new byte[fileSize];
            // fileStream.Read(fileBuffer, 0, (int)fileSize);
            HttpContext.Current.Response.BinaryWrite(byteArray);
            // fileStream.Close();
            HttpContext.Current.Response.End();
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
        /// 复制文件
        /// </summary>
        /// <param name="strOldFilepath"></param>
        /// <param name="strNewFilepath"></param>
        public static void CopyToFile(string strOldFilepath, string strNewFilepath)
        {
            try
            {
                strNewFilepath = strNewFilepath.Replace("/", "\\");
                string folder = strNewFilepath.Substring(0, strNewFilepath.LastIndexOf("\\") + 1);
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                File.Copy(strOldFilepath, strNewFilepath);
            }
            catch (Exception ex)
            {

            }
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
        public static bool WriteToFileFromBase64(string fileName, string strBase64)
        {
            FileStream pFileStream = null;
            fileName = fileName.Replace("/", "\\");
            string folder = fileName.Substring(0, fileName.LastIndexOf("\\") + 1);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            byte[] pReadByte = Convert.FromBase64String(strBase64);
            try
            {
                pFileStream = new FileStream(fileName, FileMode.OpenOrCreate);
                pFileStream.Write(pReadByte, 0, pReadByte.Length);
            }

            catch
            {
                return false;
            }
            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }

            return true;

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
        /// 转换文件大小单位 (大于512K 以M单位显示)
        /// </summary>
        /// <param name="docSize"></param>
        /// <returns></returns>
        public static string ConvertDocSizeUnit(string docSize)
        {
            string result = "";
            try
            {
                int dSize = 0;
                int.TryParse(docSize, out dSize);
                double newDSize = Math.Round(dSize / 1024.00, 2);
                if (newDSize < 512)
                {
                    result = newDSize + "K";
                }
                else
                {
                    newDSize = Math.Round(newDSize / 1024.00, 2);
                    result = newDSize.ToString();
                    if (result.Substring(result.Length - 1) == "0")
                    {
                        result = result.Substring(0, result.Length - 1);
                    }
                    result += "M";
                }
            }
            catch (Exception)
            {

            }
            return result;
        }
        /// <summary>
        /// 获取文件格式 (格式长度为4位 则去除最后一位保留3位)
        /// </summary>
        /// <param name="docName"></param>
        /// <param name="IsSubstr">是否截取</param>
        /// <returns></returns>
        public static string GetDocFileType(string docName, bool IsSubstr)
        {
            string result = string.Empty;
            try
            {
                if (docName.IndexOf(".") != -1) result = docName.Substring(docName.LastIndexOf(".") + 1);
                if (IsSubstr && result.Length > 3) result = result.Substring(0, 3);
            }
            catch (Exception)
            {

            }
            if (result.ToLower() == "class")
            {
                result = "xpt";
            }
            return result;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="oStr">原字符串</param>
        /// <param name="len">截取长度</param>
        /// <param name="needDot">是否需要...</param>
        /// <returns></returns>
        public static string GetSubstring(string oStr, int len, bool needDot)
        {
            string result = oStr;
            try
            {
                if (result.Length > len)
                {
                    result = result.Substring(0, len);
                    if (needDot) result += "...";
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public static string RedirectDrugDesc(string id)
        //{
        //    string strUrl = string.Empty;
        //    strUrl = "ShareInfo/DrugInfo_ManualView.aspx?id=" + PHHC.Share.StrUtility.clsUtility.Encrypt(id) + "&item1=D896E36A523EFF4E&item2=AAA36AE383DCF8E3";
        //    return PHHC.Share.StrUtility.clsUtility.Encrypt(strUrl);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string RedirectDrugDesc(string DrugInfo_Product_ParentID, string HospitalDrug_ProName, string DrugInfo_Product_Alias)
        {
            string strUrl = string.Empty;
            strUrl = "ShareInfo/DrugInfo_ManualView.aspx?p=" + Rc.Common.StrUtility.clsUtility.Encrypt(DrugInfo_Product_ParentID) + "&n=" + Rc.Common.StrUtility.clsUtility.Encrypt(HospitalDrug_ProName) + "&a=" + Rc.Common.StrUtility.clsUtility.Encrypt(DrugInfo_Product_Alias) + "";
            return Rc.Common.StrUtility.clsUtility.Encrypt(strUrl);
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
        public static string ConvertToLongerDateTime(string str)
        {
            string strTemp = str;
            try
            {
                DateTime dt = new DateTime();
                if (DateTime.TryParse(str, out dt))
                {
                    strTemp = dt.ToString("yyyy-MM-dd HH:mm:ss").Replace("1900-01-01 00:00:00", "");
                }
            }
            catch (Exception)
            {

            }

            return strTemp;
        }
        public static string RedirectDrugDesc(string DrugInfo_Product_ID)
        {
            string strUrl = string.Empty;
            strUrl = "ShareInfo/LookUpProductDrugView.aspx?id=" + DrugInfo_Product_ID;
            return Rc.Common.StrUtility.clsUtility.Encrypt(strUrl);
        }
        /// <summary>
        /// 获取文件名称 (去除后缀名)
        /// </summary>
        /// <param name="docName"></param>
        /// <returns></returns>
        public static string GetDocFileName(string docName)
        {
            string result = string.Empty;
            try
            {
                if (docName.IndexOf(".") != -1)
                {
                    result = docName.Substring(0, docName.LastIndexOf("."));
                }
                else
                {
                    result = docName;
                }
            }
            catch (Exception)
            {

            }
            return result;
        }
        /// <summary>
        /// 得到当前页面的名称加参数
        /// </summary>
        /// <returns></returns>
        public static string GetPageUrl()
        {
            string strTemp = string.Empty;
            if (Rc.Cloud.Web.Common.pfunction.getPageParam() != "")
            {
                strTemp = GetPageName() + "?" + getPageParam();
            }
            else
            {
                strTemp = GetPageName();
            }
            return strTemp;
        }

        /// <summary>
        /// 得到当前页面的名称
        /// </summary>
        /// <returns></returns>
        public static string GetPageName()
        {
            string url = HttpContext.Current.Request.Path.ToString();
            int tag = url.LastIndexOf("/") + 1;
            int mm = url.IndexOf(".aspx") - url.LastIndexOf("/") - 1;
            string urlName = url.Substring(tag, mm);
            return urlName + ".aspx";
        }

        //得到页面参数
        public static string getPageParam()
        {
            string iurl = string.Empty;
            string[] allUrl = System.Web.HttpContext.Current.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
            if (allUrl.Length > 1)
            {
                iurl = allUrl[1];
            }
            return iurl;
        }

        public static string GetCode(string tableName)
        {
            string strCode = DateTime.Now.ToString("yyyy-MM-dd");
            DataTable tb = new Rc.Cloud.BLL.BLL_ExecutionHardware().GetMaxCode(tableName).Tables[0];
            if (tb.Rows[0]["code"].ToString() != "")
            {
                string str = tb.Rows[0]["code"].ToString();
                string str1 = str.Substring(11, 3);
                int n;
                int.TryParse(str1, out n);
                strCode += "-" + string.Format("{0:d3}", n + 1);
            }
            else
            {
                strCode += "-001";

            }

            return strCode;
        }

        /// <summary>
        /// 重组字符串 加‘’
        /// </summary>
        /// <param name="str"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string ReDoString(string str, char split)
        {
            string strTemp = string.Empty;
            string[] strArr = new string[] { };
            if (str != "")
            {
                strArr = str.Split(split);
                for (int i = 0; i < strArr.Length; i++)
                {
                    strTemp += "'" + strArr[i] + "',";
                }
            }
            if (strTemp != "")
            {
                strTemp = strTemp.TrimEnd(',');
            }
            else
            {
                strTemp = "'" + str + "'";
            }
            return strTemp;
        }

        public static string Replace(object obj)
        {
            //Regex reg=new Regex("\n/g");
            //reg.Replace(
            return obj.ToString().Replace("\n", "").Replace("\r", "");
        }

        //得到总页数
        public static int GetPageCount(string rCount, string PageSize)
        {
            return int.Parse(Math.Ceiling((double.Parse(rCount.ToString()) + double.Parse(PageSize.ToString())) / double.Parse(PageSize.ToString())).ToString()) - 1;
        }


        /// <summary>
        /// 把NULL值转换成""值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToTransferredNull(object obj)
        {
            string strTemp = string.Empty;
            if (obj == null)
            {
                strTemp = string.Empty;
            }
            else
            {
                strTemp = obj.ToString().Trim().Replace(@"\n", "");
            }
            return strTemp;
        }
        /// <summary>
        /// 得到点评类型ID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetPrescriptionCommentID(string str)
        {
            //<asp:ListItem Value="0">--全部--</asp:ListItem>
            //               <asp:ListItem Value="1">适应证不匹配</asp:ListItem>
            //               <asp:ListItem Value="2">用药过量</asp:ListItem>
            //               <asp:ListItem Value="3">相互作用</asp:ListItem>
            //               <asp:ListItem Value="4">用药禁忌</asp:ListItem>
            //               <asp:ListItem Value="5">给药途径不合理</asp:ListItem>
            string strTemp = str;
            if (str == "适应证不匹配")
            {
                strTemp = "已审核";
            }

            return strTemp;
        }
        /// <summary>
        /// 得到审核状态
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetAuditStatus(string str)
        {
            string strTemp = "未审核";
            if (str == "0")
            {
                strTemp = "已审核";
            }

            return strTemp;
        }
        #region dataTable转换成Json格式
        /// <summary>      
        /// dataTable转换成Json格式      
        /// </summary>      
        /// <param name="dt"></param>      
        /// <returns></returns>      
        public static string DtToJson(DataTable dt, string id, string name)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName.ToString());
            jsonBuilder.Append("\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                jsonBuilder.Append("\"");
                jsonBuilder.Append("Value");
                jsonBuilder.Append("\":\"");
                jsonBuilder.Append(dt.Rows[i][id].ToString());
                jsonBuilder.Append("\",");
                jsonBuilder.Append("\"");
                jsonBuilder.Append("Text");
                jsonBuilder.Append("\":\"");
                jsonBuilder.Append(dt.Rows[i][name].ToString());
                jsonBuilder.Append("\"");
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        /// <summary>      
        /// dataTable转换成Json格式      
        /// </summary>      
        /// <param name="dt"></param>      
        /// <returns></returns>      
        public static string DtToJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName.ToString());
            jsonBuilder.Append("\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        #endregion
        //根据生日得到年龄
        public static string GetAgeByBirthday(string birthday, string pcreatedate)
        {
            string strTemp = string.Empty;
            string AgeUnit = string.Empty;

            DateTime dtBirthday = new DateTime();
            DateTime dtPcreatedate = DateTime.Now;
            //if (!DateTime.TryParse(birthday, out dtBirthday) || !DateTime.TryParse(pcreatedate, out dtPcreatedate))
            //{
            //    int byear = dtBirthday.Year;
            //    int bmonth = dtBirthday.Month;
            //    int bday = dtBirthday.Day;
            //    int cyear = dtPcreatedate.Year;
            //    int cmonth = dtPcreatedate.Month;
            //    int cday = dtPcreatedate.Day;
            //    return strTemp;
            //}

            if (!DateTime.TryParse(birthday, out dtBirthday) || !DateTime.TryParse(pcreatedate, out dtPcreatedate))
            {
                strTemp = "未知";
                return strTemp;
            }

            double TotalDays = Math.Round((dtPcreatedate - dtBirthday).TotalDays);
            int[] ageTemp;

            if (TotalDays < 0)
            {
                TotalDays = 0;
            }
            if (TotalDays >= 0 && TotalDays < 60)//天
            {
                ageTemp = dateTimeDiff.toResult(dtPcreatedate, dtBirthday, diffResultFormat.dd);
                strTemp = string.Format("{0}天", ageTemp[0]);
            }
            else if (TotalDays >= 60 && TotalDays < 18 * 30)
            {
                ageTemp = dateTimeDiff.toResult(dtPcreatedate, dtBirthday, diffResultFormat.mm);
                strTemp = string.Format("{0}个月", ageTemp[0]);
            }
            else if (TotalDays >= 18 * 30 && TotalDays < 5 * 365)
            {
                ageTemp = dateTimeDiff.toResult(dtPcreatedate, dtBirthday, diffResultFormat.yymm);
                if (ageTemp[1] == 0)
                {
                    strTemp = string.Format("{0}岁", ageTemp[0]);
                }
                else
                {
                    strTemp = string.Format("{0}岁{1}个月", ageTemp[0], ageTemp[1]);
                }
            }
            else
            {
                ageTemp = dateTimeDiff.toResult(dtPcreatedate, dtBirthday, diffResultFormat.yy);
                strTemp = string.Format("{0}岁", ageTemp[0]);
            }

            return strTemp;
        }
        public static string ToTransferred(string str)
        {
            string strTemp = str;
            if (str == "处方集pdf")
            {
                strTemp = "国家处方集";
            }
            else if (str == "新编")
            {
                strTemp = "新编药物学";
            }
            return strTemp;

        }
        /// <summary>
        /// 得到配置的药物字段
        /// </summary>
        public static string GetDrugShowFiled()
        {
            return ConfigurationManager.AppSettings["DrugShowDetail"].ToString();
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string SplitStr(string str, int icount)
        {
            string strTemp = str;
            if (str != null)
            {
                if (str.Length > icount)
                {
                    strTemp = str.Substring(0, icount) + "...";
                }
            }
            return strTemp;
        }
        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="Response"></param>
        /// <param name="Request"></param>
        /// <param name="sw"></param>
        /// <param name="fileName"></param>
        public static void ResponseExcel(HttpResponse Response, HttpRequest Request, StringWriter sw, string fileName)
        {
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            if (browser.Type.ToString().ToLower().IndexOf("firefox") > -1)
            {
                fileName = HttpUtility.UrlDecode(HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            }
            else
            {
                fileName = HttpUtility.UrlEncode(fileName);
            }
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;FileName=" + fileName);
            //Response.Write("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
            Response.Write(sw);
            //Response.Write("</body></html>");
            Response.Flush();
            Response.End();
        }
        public static string GetDaXieShuZi(string str)
        {

            string strTemp = string.Empty;
            strTemp = str;
            if (str == "1") { strTemp = "一"; }
            else if (str == "2") { strTemp = "二"; }
            else if (str == "3") { strTemp = "三"; }
            else if (str == "4") { strTemp = "四"; }
            else if (str == "5") { strTemp = "五"; }
            else if (str == "6") { strTemp = "六"; }
            else if (str == "7") { strTemp = "七"; }
            else if (str == "8") { strTemp = "八"; }
            else if (str == "9") { strTemp = "九"; }
            else if (str == "10") { strTemp = "十"; }
            return strTemp;
        }
        public static int[] RemoveDup(int[] myData)
        {
            if (myData.Length > 0)
            {
                Array.Sort(myData);
                int size = 1;
                for (int i = 1; i < myData.Length; i++)
                    if (myData[i] != myData[i - 1])
                        size++;
                int[] myTempData = new int[size];
                int j = 0;
                myTempData[j++] = myData[0];
                for (int i = 1; i < myData.Length; i++)
                    if (myData[i] != myData[i - 1])
                        myTempData[j++] = myData[i];
                return myTempData;
            }
            return myData;
        }


        //方法1   
        public static int[] GetRandom1(int minValue, int maxValue, int count, int seed)
        {

            Random rnd = new Random(seed);
            int length = maxValue - minValue + 1;
            byte[] keys = new byte[length];
            rnd.NextBytes(keys);
            int[] items = new int[length];
            for (int i = 0; i < length; i++)
            {
                items[i] = i + minValue;
            }
            Array.Sort(keys, items);
            int[] result = new int[count];
            Array.Copy(items, result, count);
            return result;

        }

        //方法2   
        public static int[] GetRandom2(int minValue, int maxValue, int count)
        {
            int[] intList = new int[maxValue];
            for (int i = 0; i < maxValue; i++)
            {
                intList[i] = i + minValue;
            }
            int[] intRet = new int[count];
            int n = maxValue;
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int index = rand.Next(0, n);
                intRet[i] = intList[index];
                intList[index] = intList[--n];
            }

            return intRet;
        }


        /// <summary>
        /// 填充下拉列表
        /// </summary>
        /// <param name="ddl">要填充的下拉列表</param>
        /// <param name="dt"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public static void SetDdl(DropDownList ddl, DataTable dt, string text, string value)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--全部--", "-1"));//--请选择--
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(ToTransferred(dt.Rows[i][text].ToString().Trim()), dt.Rows[i][value].ToString().Trim()));
            }
        }
        /// <summary>
        /// 填充下拉列表
        /// </summary>
        /// <param name="ddl">要填充的下拉列表</param>
        /// <param name="dt"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public static void SetDdl(DropDownList ddl, DataTable dt, string text, string value, bool b)
        {

            ddl.Items.Clear();
            if (b)
            {
                ddl.Items.Add(new ListItem("--请选择--", "-1"));//--请选择--;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(ToTransferred(dt.Rows[i][text].ToString().Trim()), dt.Rows[i][value].ToString().Trim()));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="dt"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="FristText"></param>
        public static void SetDdl(DropDownList ddl, DataTable dt, string text, string value, string FristText)
        {
            ddl.Items.Clear();
            if (FristText != "")
            {
                ddl.Items.Add(new ListItem(FristText, "-1"));//--请选择--
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt.Rows[i][text].ToString().Trim(), dt.Rows[i][value].ToString().Trim()));
            }
        }

        public static void SetDdlEmpty(DropDownList ddl, DataTable dt, string text, string value, string FristText)
        {
            ddl.Items.Clear();
            if (FristText != "")
            {
                ddl.Items.Add(new ListItem(FristText, ""));//--请选择--
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt.Rows[i][text].ToString().Trim(), dt.Rows[i][value].ToString().Trim()));
            }
        }

        /// <summary>
        /// 填充ListBox
        /// </summary>
        /// <param name="lbx">要填充的ListBox</param>
        /// <param name="dt"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public static void SetLbx(ListBox lbx, DataTable dt, string text, string value, bool b)
        {

            lbx.Items.Clear();
            if (b)
            {
                lbx.Items.Add(new ListItem("--全部--", "-1"));//--请选择--;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lbx.Items.Add(new ListItem(ToTransferred(dt.Rows[i][text].ToString().Trim()), dt.Rows[i][value].ToString().Trim()));
            }
        }

        /// <summary>
        /// SetDdlYear 填充下拉列表 年 从2011 年开始到 当前年+1
        /// </summary>
        /// <param name="ddl">要填充的下拉列表</param>
        /// <param name="isShowPleaseSelect">是否显示“请选择”</param>
        public static void SetDdlYear(DropDownList ddl, bool isShowPleaseSelect)
        {
            ListItem item = null;
            ddl.Items.Clear();
            if (isShowPleaseSelect)
            {
                item = new ListItem("--全部--", "-1");//请选择
                ddl.Items.Add(item);
            }
            for (int y = 2011; y <= DateTime.Now.Year + 1; y++)
            {
                item = new ListItem(y.ToString(), y.ToString());
                ddl.Items.Add(item);
            }
        }
        /// <summary>
        /// SetDdlYear 填充下拉列表 年 从2011 年开始到 当前年+1
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="startYear">开始年份</param>
        /// <param name="endYear">结束年份</param>
        /// <param name="isShowPleaseSelect"></param>
        public static void SetDdlYear(DropDownList ddl, int startYear, int endYear, bool isShowPleaseSelect)
        {
            ListItem item = null;
            ddl.Items.Clear();
            if (isShowPleaseSelect)
            {
                item = new ListItem("--请选择--", "-1");//请选择
                ddl.Items.Add(item);
            }
            for (int y = startYear; y <= endYear; y++)
            {
                item = new ListItem(y.ToString(), y.ToString());
                ddl.Items.Add(item);
            }
        }
        /// <summary>
        /// 填充入学年份2016-04-18
        /// </summary>
        public static void SetDdlStartSchoolYear(DropDownList ddl, int startYear, int endYear, bool isShowPleaseSelect)
        {
            ListItem item = null;
            ddl.Items.Clear();
            if (isShowPleaseSelect)
            {
                item = new ListItem("--请选择--", "-1");//请选择
                ddl.Items.Add(item);
            }
            for (int y = startYear; y <= endYear; y++)
            {
                item = new ListItem(y.ToString() + "年", y.ToString());
                ddl.Items.Add(item);
            }
        }
        public static void SetDdlStartSchoolYear(DropDownList ddl, int startYear, int endYear, bool isShowPleaseSelect, string StrText)
        {
            ListItem item = null;
            ddl.Items.Clear();
            if (isShowPleaseSelect)
            {
                if (!string.IsNullOrEmpty(StrText))
                {
                    item = new ListItem(StrText, "-1");//请选择
                }
                else
                {
                    item = new ListItem("--请选择--", "-1");//请选择
                }
                ddl.Items.Add(item);
            }
            for (int y = startYear; y <= endYear; y++)
            {
                item = new ListItem(y.ToString() + "年", y.ToString());
                ddl.Items.Add(item);
            }
        }

        /// <summary>
        /// 填充下拉列表 月 
        /// </summary>
        /// <param name="ddl">要填充的下拉列表</param>
        /// <param name="isShowPleaseSelect">是否显示“请选择”</param>
        public static void SetDdlMonth(DropDownList ddl, bool isShowPleaseSelect)
        {
            ListItem item = null;
            ddl.Items.Clear();
            if (isShowPleaseSelect)
            {
                item = new ListItem("--全部--", "-1");//请选择
                ddl.Items.Add(item);
            }
            for (int m = 1; m <= 12; m++)
            {
                item = new ListItem(m.ToString(), m.ToString());
                ddl.Items.Add(item);
            }
        }
        //填充下拉列表
        public static void SetDdlNoLine(DropDownList ddl, DataTable dt, string text, string value)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--全部--", "-1"));//--请选择--
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt.Rows[i][text].ToString(), dt.Rows[i][value].ToString()));
            }
        }
        //填充复选框
        public static void SetCbl(CheckBoxList cbl, DataTable dt, string text, string value)
        {
            cbl.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbl.Items.Add(new ListItem(dt.Rows[i][text].ToString(), dt.Rows[i][value].ToString()));
            }
        }
        /// <summary>
        /// 填充复选框 截断显示长度
        /// </summary>
        /// <param name="cbl"></param>
        /// <param name="dt"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="len">显示长度</param>
        public static void SetCbl(CheckBoxList cbl, DataTable dt, string text, string value, int len)
        {
            cbl.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbl.Items.Add(new ListItem(SplitStr(dt.Rows[i][text].ToString(), len), dt.Rows[i][value].ToString()));
            }
        }
        ///// <summary>
        ///// 填充复选框
        ///// </summary>
        ///// <param name="cbl">控件ID</param>
        ///// <param name="dt">列表</param>
        ///// <param name="text">显示文本</param>
        ///// <param name="value">绑定值</param>
        ///// <param name="valueNumber">文本值长度</param>
        //public static void SetCbl(CheckBoxList cbl, DataTable dt, string text, string value,int valueNumber)
        //{
        //    cbl.Items.Clear();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (dt.Rows[i][text].ToString().Length>valueNumber)
        //        {
        //            cbl.Items.Add(new ListItem(dt.Rows[i][text].ToString().Substring(0,valueNumber)+"...", dt.Rows[i][value].ToString()));
        //        }
        //        else
        //        {
        //            cbl.Items.Add(new ListItem(dt.Rows[i][text].ToString(), dt.Rows[i][value].ToString()));
        //        }

        //    }
        //}
        //填充单选框
        public static void SetRbl(RadioButtonList rbl, DataTable dt, string text, string value)
        {
            rbl.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rbl.Items.Add(new ListItem(dt.Rows[i][text].ToString(), dt.Rows[i][value].ToString()));
            }
        }
        //填充单选框
        public static void SetRbl(RadioButtonList rbl, DataTable dt, string text, string value, string def)
        {
            rbl.Items.Clear();
            rbl.Items.Add(new ListItem(def, "-1"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rbl.Items.Add(new ListItem(dt.Rows[i][text].ToString(), dt.Rows[i][value].ToString()));
            }
        }
        //填充复选框并选中指定值
        public static void SetCbl(CheckBoxList cbl, DataTable dt, string text, string value, string checkedValue)
        {
            cbl.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbl.Items.Add(new ListItem(dt.Rows[i][text].ToString(), dt.Rows[i][value].ToString()));
                if (dt.Rows[i][checkedValue].ToString() != "-1")
                {
                    cbl.Items[i].Selected = true;
                }

            }
        }

        //复选框 选中指定值
        public static void SetCbl(CheckBoxList cbl, string checkedValue)
        {
            foreach (ListItem item in cbl.Items)
            {
                if (checkedValue.IndexOf(item.Value) >= 0)
                {
                    item.Selected = true;
                }
                //if (item)
                //{
                //    cbl.Items[i].Selected = true;
                //}
            }

        }
        //复选框 选中指定值
        public static void SetCbl(CheckBoxList cbl, string value, DataTable dt)
        {
            string strTemp = string.Empty;
            foreach (DataRow item in dt.Rows)
            {
                strTemp += item[value].ToString() + ",";
            }
            if (strTemp != string.Empty)
            {
                strTemp = strTemp.TrimEnd(',');
            }
            SetCbl(cbl, strTemp);
            //return strTemp;
        }
        /// <summary>
        /// 得到CheckBoxList中选中了的值
        /// <param name="checkList">CheckBoxList</param>
        /// <param name="separator">分割符号</param>
        /// <returns></returns>
        /// </summary>
        public static string GetCblCheckedValue(CheckBoxList checkList, string separator)
        {
            string selval = string.Empty;
            for (int i = 0; i < checkList.Items.Count; i++)
            {
                if (checkList.Items[i].Selected)
                {
                    selval += checkList.Items[i].Value + separator;
                }
            }
            if (selval != string.Empty)
            {
                selval = selval.Remove(selval.Length - separator.Length);
            }
            return selval;
        }

        /// <summary>
        /// 得到CheckBoxList中选中了的VALUE与TEXT
        /// </summary>
        /// <param name="checkList">CheckBoxList对象</param>
        /// <param name="separative1">VALUE与TEXT之间的分隔符</param>
        /// <param name="separative2">选中元素之间的分隔符</param>
        /// <returns></returns>
        public static string GetCblCheckedValueAndText(CheckBoxList checkList, string separative1, string separative2)
        {
            string selval = string.Empty;
            for (int i = 0; i < checkList.Items.Count; i++)
            {
                if (checkList.Items[i].Selected)
                {
                    selval += checkList.Items[i].Value + separative1 + checkList.Items[i].Text + separative2;
                }
            }
            if (selval != string.Empty)
            {
                selval = selval.Remove(selval.Length - separative2.Length);
            }
            return selval;
        }
        /// <summary>
        /// 得到审批类型名称
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetReportTypeText(string str)
        {
            string strTemp = string.Empty;

            if (str != string.Empty)
            {
                for (int i = 0; i < str.Split(',').Length; i++)
                {
                    strTemp += str.Split(',')[i].Split('|')[1] + " ,";
                }
                if (strTemp != string.Empty)
                {
                    strTemp = strTemp.Remove(strTemp.Length - 1);
                }
            }
            return strTemp;
        }
        /// <summary>
        /// 得到站点域名包括端口
        /// </summary>
        /// <returns></returns>
        public static string getHostPath()
        {
            string hostPath = string.Empty;
            //string hostPathTemp = string.Empty;
            //hostPath = HttpContext.Current.Request.Url.AbsoluteUri;
            //hostPathTemp = hostPath.Replace("//", "~~");
            //hostPathTemp = hostPathTemp.Remove(hostPathTemp.IndexOf("/"));
            //hostPath = hostPathTemp.Replace("~~", "//");
            //hostPath = hostPath + ConfigurationManager.AppSettings["VirtualPath"];


            //hostPath = "http://" + HttpContext.Current.Request.Url.Host;
            //if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Port"]))
            //{
            //    hostPath += ":" + ConfigurationManager.AppSettings["Port"];
            //}
            //else
            //{
            //    if (!String.IsNullOrEmpty(HttpContext.Current.Request.Url.Host))
            //    {
            //        //hostPath += ":" + HttpContext.Current.Request.Url.Port;
            //        if (HttpContext.Current.Request.Url.Port != 80) hostPath += ":" + HttpContext.Current.Request.Url.Port;
            //    }
            //}


            hostPath = "http://" + HttpContext.Current.Request.ServerVariables["Http_Host"].ToString();
            // 返回值示例 http://192.168.0.1:8808
            return hostPath;
        }
        /// <summary>
        /// 把分格式代成 时：分
        /// </summary>
        /// <param name="Mi"></param>
        /// <returns></returns>
        public static string ToFomartTime(string Mi)
        {
            double db_temp = 0;
            double.TryParse(Mi, out db_temp);
            if (db_temp < 0)
            {
                db_temp = 0;
            }
            double db_time = db_temp;
            double db_h_temp = 0;
            double db_h = 0;
            double db_m_temp = 0;
            double db_m = 0;
            db_h_temp = db_time / (60);
            db_h = Math.Floor(db_h_temp);
            db_m_temp = db_time % (60);

            db_m = Math.Ceiling(db_m_temp);

            string strTemp = string.Empty;
            strTemp = db_h.ToString("00") + ":" + db_m.ToString("00");

            return strTemp;
        }
        public static string getFomartDate(string strDate)
        {
            string strTemp = string.Empty;
            DateTime dt = new DateTime();
            if (!DateTime.TryParse(strDate, out dt)) { strTemp = ""; }
            else
            {
                strTemp = dt.Month + "月" + dt.Day + "日";
            };

            return strTemp;
        }
        /// <summary>
        /// 根据查询日期 得到所有中间的日期
        /// </summary>
        /// <param name="strStartDate"></param>
        /// <param name="strEndDate"></param>
        /// <returns></returns>
        public static List<string> GetListDate(string strStartDate, string strEndDate)
        {
            List<string> listTemp = new List<string>();
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            if (!DateTime.TryParse(strStartDate, out startDate)) { startDate = DateTime.Now; }
            if (!DateTime.TryParse(strEndDate, out endDate)) { endDate = DateTime.Now; }
            TimeSpan span = endDate - startDate;
            double days = span.TotalDays;
            int iday = int.Parse(days.ToString());
            for (int i = 0; i <= iday; i++)
            {
                listTemp.Add(startDate.AddDays(i).ToString("yyyy-MM-dd"));
            }
            return listTemp;
        }
        /// <summary>
        /// 不够四位补0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToFourStr(string str)
        {
            string strTemp = str;

            strTemp = "0000" + str;
            strTemp = strTemp.Substring(strTemp.Length - 4, 4);
            return strTemp;
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
        /// 得到时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToShortTime(string str)
        {
            string strTemp = str;
            DateTime dt = new DateTime();
            if (DateTime.TryParse(str, out dt))
            {
                strTemp = dt.ToLongTimeString();
            }

            return strTemp;
        }
        /// <summary>
        /// 得到时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToTime(string str)
        {
            string strTemp = str;
            DateTime dt = new DateTime();
            if (DateTime.TryParse(str, out dt))
            {
                strTemp = dt.ToLongTimeString();
            }

            return strTemp;
        }
        /// <summary>
        /// 十进制转成16进制 长度固定为4位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string To16(string str)
        {
            string strTemp = string.Empty;
            int i = 0;
            int.TryParse(str, out i);
            strTemp = Convert.ToString(i, 16);
            string s_1 = string.Empty;
            s_1 = "0000" + strTemp;

            strTemp = s_1.Substring(s_1.Length - 4);
            return strTemp.ToUpper();

        }

        /// <summary>
        /// 获取YY-MM-DD 当月第一天
        /// </summary>
        public static string GetMonthFristDay()
        {
            string strTemp = "";
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            strTemp = year + "-" + month + "-1";
            return strTemp;

        }

        /// <summary>
        /// 获取YY-MM-DD 当月最后一天
        /// </summary>
        public static string GetMonthLastDay()
        {
            string strTemp = "";
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int lastday = DateTime.DaysInMonth(year, month);
            strTemp = year + "-" + month + "-" + lastday;
            return strTemp;

        }
        /// <summary>
        /// 去重并排序
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetDelDoubleAndSort(string str)
        {
            string strTemp = string.Empty;
            List<string> listTemp = new List<string>();
            for (int i = 0; i < str.Length; i++)
            {
                //strTemp = str.Replace(str.Substring(i, 1), "");
                listTemp.Add(str.Substring(i, 1));
            }
            listTemp = listTemp.Distinct().ToList();
            listTemp.Sort();
            for (int i = 0; i < listTemp.Count; i++)
            {
                strTemp += listTemp[i].ToUpper();
            }
            return strTemp;
        }

        /// <summary>
        /// 去重并排序
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CheckImp(string str)
        {
            string strTemp = string.Empty;
            List<string> listTemp = new List<string>();
            for (int i = 0; i < str.Split(',').Length; i++)
            {
                listTemp.Add(str.Split(',')[i]);
            }
            listTemp = listTemp.Distinct().ToList();
            listTemp.Sort();
            for (int i = 0; i < listTemp.Count; i++)
            {
                if (listTemp[i].ToLower() != "")
                {
                    strTemp += listTemp[i].ToLower() + ",";
                }
            }
            return strTemp;
        }
        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="pNum">当前页</param>
        /// <param name="pSize">每页显示的数据条数</param>
        /// <param name="rCount">数据的总条数</param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetPageIndex(int pNum, int pSize, int rCount, int pCount, string url)
        {
            StringBuilder sbHTML = new StringBuilder();
            if (rCount == 0) { return ""; }
            string first = String.Format(url, 1, pSize);
            string back = String.Format(url, pNum - 1, pSize);
            string next = String.Format(url, pNum + 1, pSize);
            string last = String.Format(url, pCount, pSize);
            sbHTML.Append("<table class=\"table_pageindex\" cellpadding=\"0\" cellspacing=\"0\"><tr><td align=\"right\">");
            sbHTML.Append("<table style=\"margin-top:10px;margin-bottom:10px;\">");
            sbHTML.Append("<tr>");
            sbHTML.Append(" <td>");
            if (pNum == 1)
            {
                sbHTML.Append(" <div   class='fenye_btn2'/>首 页</div>");
            }
            else
            {
                sbHTML.Append("<a class=\"aspNetDisabled fenye_btn\" href='");
                sbHTML.Append(first);
                sbHTML.Append("'>首 页</a>");
            }
            sbHTML.Append(" </td>");
            sbHTML.Append("<td>");
            if (pNum == 1)
            {
                sbHTML.Append(" <div  class='fenye_btn2'>上一页</div>");
            }
            else
            {
                sbHTML.Append("<a  class=\"aspNetDisabled fenye_btn\" href='");
                sbHTML.Append(back);
                sbHTML.Append("'>上一页</a>");
            }
            sbHTML.Append("</td>");
            sbHTML.Append("<td>&nbsp;&nbsp;共");
            sbHTML.Append("<span style='color:#3C83AF'>");
            sbHTML.Append(rCount);
            sbHTML.Append("</span>条数据");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("&nbsp;&nbsp;第");
            sbHTML.Append("<span id=\"MainContent_UCPagerTool1_span_page\" style='color:#3C83AF'> ");//
            sbHTML.Append(pNum);
            sbHTML.Append(" </span>页");
            sbHTML.Append("/<span id=\"MainContent_UCPagerTool1_span_curpage\">共");//
            sbHTML.Append("<span style='color:#3C83AF'>");
            sbHTML.Append(pCount);
            sbHTML.Append(" </span>页");
            sbHTML.Append("</span>");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            if (pNum == pCount)
            {
                sbHTML.Append(" <div class='fenye_btn2'>下一页</div>");
            }
            else
            {
                sbHTML.Append("<a class=\"aspNetDisabled fenye_btn\" href='");
                sbHTML.Append(next);
                sbHTML.Append("'>下一页</a>");
            }
            sbHTML.Append(" </td>");
            sbHTML.Append("<td>");
            if (pNum == pCount)
            {
                sbHTML.Append(" <div  class='fenye_btn2'>末 页</div>");
            }
            else
            {
                sbHTML.Append("<a class=\"aspNetDisabled fenye_btn\" href='");
                sbHTML.Append(last);
                sbHTML.Append("'>末 页</a>");
            } sbHTML.Append("</td>");
            sbHTML.Append("<td>&nbsp;&nbsp;显示第</td>");//
            sbHTML.Append("<td>");
            sbHTML.Append(" <select  id=\"select_pageindex\" onchange=\"PageIndexChange(this);\">");
            if (pCount > 100)
            {
                pCount = 100;
            }
            for (int i = 1; i <= pCount; i++)
            {
                sbHTML.Append("<option value='");
                sbHTML.Append(string.Format(url, i, pSize));
                sbHTML.Append("' ");
                if (i == pNum)
                {
                    sbHTML.Append(" selected=selected");
                }
                sbHTML.Append(">");
                sbHTML.Append(i);
                sbHTML.Append("</option>");
            }
            sbHTML.Append("</select>");
            sbHTML.Append(" </td>");
            sbHTML.Append("   <td>页");
            sbHTML.Append(" &nbsp;每页显示数量");
            sbHTML.Append("&nbsp;");
            sbHTML.Append("</td>");
            sbHTML.Append(" <td>");
            sbHTML.Append(" <select  id=\"select_pagesize\" onchange=\"PageSizeChange(this);\"  style=\"font-size:13px;\">");
            int[] arrnum = { 5, 10, 15, 20, 50, 75, 100, 500 };
            foreach (int n in arrnum)
            {
                sbHTML.Append("<option value='");
                sbHTML.Append(string.Format(url, 1, n));
                sbHTML.Append("'");
                if (n == pSize)
                {
                    sbHTML.Append(" selected=selected ");
                }
                sbHTML.Append(">");
                sbHTML.Append(n);
                sbHTML.Append("</option>");
            }
            sbHTML.Append(" </select>");
            sbHTML.Append(" </td>");
            sbHTML.Append("  </tr>");
            sbHTML.Append("</table>");
            sbHTML.Append("</td></tr></table>");
            return sbHTML.ToString();
        }
        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="pNum">当前页</param>
        /// <param name="pSize">每页显示的数据条数</param>
        /// <param name="rCount">数据的总条数</param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetPageIndex(int pNum, int pSize, int rCount, string url)
        {
            int pCount = int.Parse(Math.Ceiling((double.Parse(rCount.ToString()) + double.Parse(pSize.ToString())) / double.Parse(pSize.ToString())).ToString()) - 1;
            StringBuilder sbHTML = new StringBuilder();
            if (rCount == 0) { return ""; }
            string first = String.Format(url, 1, pSize);
            string back = String.Format(url, pNum - 1, pSize);
            string next = String.Format(url, pNum + 1, pSize);
            string last = String.Format(url, pCount, pSize);
            sbHTML.Append("<table class=\"table_pageindex\" cellpadding=\"0\" cellspacing=\"0\"><tr><td align=\"center\">");
            sbHTML.Append("<table style=\"margin-top:10px;margin-bottom:10px;\">");
            sbHTML.Append("<tr>");
            sbHTML.Append(" <td>");
            if (pNum == 1)
            {
                sbHTML.Append(" <div   class='fenye_btn2'/>首 页</div>");
            }
            else
            {
                sbHTML.Append("<a class=\"aspNetDisabled fenye_btn\" href='");
                sbHTML.Append(first);
                sbHTML.Append("'>首 页</a>");
            }
            sbHTML.Append(" </td>");
            sbHTML.Append("<td>");
            if (pNum == 1)
            {
                sbHTML.Append(" <div  class='fenye_btn2'>上一页</div>");
            }
            else
            {
                sbHTML.Append("<a  class=\"aspNetDisabled fenye_btn\" href='");
                sbHTML.Append(back);
                sbHTML.Append("'>上一页</a>");
            }
            sbHTML.Append("</td>");
            sbHTML.Append("<td>&nbsp;&nbsp;共");
            sbHTML.Append("<span style='color:#3C83AF'>");
            sbHTML.Append(rCount);
            sbHTML.Append("</span>条数据");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            sbHTML.Append("&nbsp;&nbsp;第");
            sbHTML.Append("<span id=\"MainContent_UCPagerTool1_span_page\" style='color:#3C83AF'> ");//
            sbHTML.Append(pNum);
            sbHTML.Append(" </span>页");
            sbHTML.Append("/<span id=\"MainContent_UCPagerTool1_span_curpage\">共");//
            sbHTML.Append("<span style='color:#3C83AF'>");
            sbHTML.Append(pCount);
            sbHTML.Append(" </span>页");
            sbHTML.Append("</span>");
            sbHTML.Append("</td>");
            sbHTML.Append("<td>");
            if (pNum == pCount)
            {
                sbHTML.Append(" <div class='fenye_btn2'>下一页</div>");
            }
            else
            {
                sbHTML.Append("<a class=\"aspNetDisabled fenye_btn\" href='");
                sbHTML.Append(next);
                sbHTML.Append("'>下一页</a>");
            }
            sbHTML.Append(" </td>");
            sbHTML.Append("<td>");
            if (pNum == pCount)
            {
                sbHTML.Append(" <div  class='fenye_btn2'>末 页</div>");
            }
            else
            {
                sbHTML.Append("<a class=\"aspNetDisabled fenye_btn\" href='");
                sbHTML.Append(last);
                sbHTML.Append("'>末 页</a>");
            } sbHTML.Append("</td>");
            sbHTML.Append("<td>&nbsp;&nbsp;显示第</td>");//
            sbHTML.Append("<td>");
            sbHTML.Append(" <select  id=\"select_pageindex\" onchange=\"PageIndexChange(this);\">");
            if (pCount > 100)
            {
                pCount = 100;
            }
            for (int i = 1; i <= pCount; i++)
            {
                sbHTML.Append("<option value='");
                sbHTML.Append(string.Format(url, i, pSize));
                sbHTML.Append("' ");
                if (i == pNum)
                {
                    sbHTML.Append(" selected=selected");
                }
                sbHTML.Append(">");
                sbHTML.Append(i);
                sbHTML.Append("</option>");
            }
            sbHTML.Append("</select>");
            sbHTML.Append(" </td>");
            sbHTML.Append("   <td>页");
            sbHTML.Append(" &nbsp;每页显示数量");
            sbHTML.Append("&nbsp;");
            sbHTML.Append("</td>");
            sbHTML.Append(" <td>");
            sbHTML.Append(" <select  id=\"select_pagesize\" onchange=\"PageSizeChange(this);\"  style=\"font-size:13px;\">");
            int[] arrnum = { 5, 10, 15, 20, 50, 75, 100, 500 };
            foreach (int n in arrnum)
            {
                sbHTML.Append("<option value='");
                sbHTML.Append(string.Format(url, 1, n));
                sbHTML.Append("'");
                if (n == pSize)
                {
                    sbHTML.Append(" selected=selected ");
                }
                sbHTML.Append(">");
                sbHTML.Append(n);
                sbHTML.Append("</option>");
            }
            sbHTML.Append(" </select>");
            sbHTML.Append(" </td>");
            sbHTML.Append("  </tr>");
            sbHTML.Append("</table>");
            sbHTML.Append("</td></tr></table>");
            return sbHTML.ToString();
        }

        /// <summary>
        /// 根据UserId获取UserName
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetUserNameByUserId(string userId)
        {
            string temp = string.Empty;
            try
            {
                Rc.Model.Resources.Model_F_User model = new Rc.BLL.Resources.BLL_F_User().GetModel(userId);
                if (model != null)
                {
                    temp = model.UserName;
                }
            }
            catch (Exception)
            {

            }
            return temp;
        }
        /// <summary>
        /// 根据UserId获取TrueName
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetUserTrueNameByUserId(string userId)
        {
            string temp = string.Empty;
            try
            {
                Rc.Model.Resources.Model_F_User model = new Rc.BLL.Resources.BLL_F_User().GetModel(userId);
                if (model != null)
                {
                    temp = model.TrueName;
                    if (string.IsNullOrEmpty(temp)) temp = model.UserName;
                }
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
        /// 计算日期间隔
        /// </summary>
        /// <param name="d1">要参与计算的其中一个日期字符串</param>
        /// <param name="d2">要参与计算的另一个日期字符串</param>
        /// <returns>一个表示日期间隔的TimeSpan类型</returns>
        public static string ExecDateDiff(string d1, string d2)
        {
            try
            {
                DateTime date1 = DateTime.Parse(d1);
                DateTime date2 = DateTime.Parse(d2);
                return dateTimeDiff.toResult(date1, date2).ToString("g");
            }
            catch
            {
                return "";
            }
        }

    }
    public static class dateTimeDiff
    {
        /// <summary>
        /// 计算日期间隔
        /// </summary>
        /// <param name="d1">要参与计算的其中一个日期字符串</param>
        /// <param name="d2">要参与计算的另一个日期字符串</param>
        /// <returns>一个表示日期间隔的TimeSpan类型</returns>
        public static TimeSpan toResult(string d1, string d2)
        {
            try
            {
                DateTime date1 = DateTime.Parse(d1);
                DateTime date2 = DateTime.Parse(d2);
                return toResult(date1, date2);
            }
            catch
            {
                throw new Exception("字符串参数不正确!");
            }
        }
        /// <summary>
        /// 计算日期间隔
        /// </summary>
        /// <param name="d1">要参与计算的其中一个日期</param>
        /// <param name="d2">要参与计算的另一个日期</param>
        /// <returns>一个表示日期间隔的TimeSpan类型</returns>
        public static TimeSpan toResult(DateTime d1, DateTime d2)
        {
            TimeSpan ts;
            if (d1 > d2)
            {
                ts = d1 - d2;
            }
            else
            {
                ts = d2 - d1;
            }
            return ts;
        }

        /// <summary>
        /// 计算日期间隔
        /// </summary>
        /// <param name="d1">要参与计算的其中一个日期字符串</param>
        /// <param name="d2">要参与计算的另一个日期字符串</param>
        /// <param name="drf">决定返回值形式的枚举</param>
        /// <returns>一个代表年月日的int数组，具体数组长度与枚举参数drf有关</returns>
        public static int[] toResult(string d1, string d2, diffResultFormat drf)
        {
            try
            {
                DateTime date1 = DateTime.Parse(d1);
                DateTime date2 = DateTime.Parse(d2);
                return toResult(date1, date2, drf);
            }
            catch
            {
                throw new Exception("字符串参数不正确!");
            }
        }
        /// <summary>
        /// 计算日期间隔
        /// </summary>
        /// <param name="d1">要参与计算的其中一个日期</param>
        /// <param name="d2">要参与计算的另一个日期</param>
        /// <param name="drf">决定返回值形式的枚举</param>
        /// <returns>一个代表年月日的int数组，具体数组长度与枚举参数drf有关</returns>
        public static int[] toResult(DateTime d1, DateTime d2, diffResultFormat drf)
        {
            #region 数据初始化
            DateTime max;
            DateTime min;
            int year;
            int month;
            int tempYear, tempMonth;
            if (d1 > d2)
            {
                max = d1;
                min = d2;
            }
            else
            {
                max = d2;
                min = d1;
            }
            tempYear = max.Year;
            tempMonth = max.Month;
            if (max.Month < min.Month)
            {
                tempYear--;
                tempMonth = tempMonth + 12;
            }
            year = tempYear - min.Year;
            month = tempMonth - min.Month;
            #endregion
            #region 按条件计算
            if (drf == diffResultFormat.dd)
            {
                TimeSpan ts = max - min;
                return new int[] { ts.Days };
            }
            if (drf == diffResultFormat.mm)
            {
                return new int[] { month + year * 12 };
            }
            if (drf == diffResultFormat.yy)
            {
                return new int[] { year };
            }
            return new int[] { year, month };
            #endregion
        }
    }
    /// <summary>
    /// 关于返回值形式的枚举
    /// </summary>
    public enum diffResultFormat
    {
        /// <summary>
        /// 年数和月数
        /// </summary>
        yymm,
        /// <summary>
        /// 年数
        /// </summary>
        yy,
        /// <summary>
        /// 月数
        /// </summary>
        mm,
        /// <summary>
        /// 天数
        /// </summary>
        dd,
    }

    public class localResourceModel
    {
        /// <summary>
        /// 本地资源类型
        /// </summary>
        public string localType { get; set; }
        /// <summary>
        /// 本地资源URL
        /// </summary>
        public string localUrl { get; set; }

    }

}