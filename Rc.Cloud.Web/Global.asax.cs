using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Rc.Cloud.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            #region 定义定时器（同步数据、资源文件）

            int intSeconds = 1000 * 30;
            System.Timers.Timer myTimer = new System.Timers.Timer(intSeconds);

            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);

            myTimer.Enabled = true;

            myTimer.AutoReset = true;
            #endregion

            #region 定义定时器（自动提交学生作业）
            int intSeconds_Submit = 10;
            try
            {
                intSeconds_Submit = Rc.Common.ConfigHelper.GetConfigInt("AutoSubmitSeconds");
            }
            catch (Exception)
            {
                intSeconds_Submit = 10;
            }
            int intSeconds_Stu = 1000 * intSeconds_Submit;
            System.Timers.Timer myTimer_Stu = new System.Timers.Timer(intSeconds_Stu);

            myTimer_Stu.Elapsed += new ElapsedEventHandler(AutoSubmitStuHW_Elapsed);

            myTimer_Stu.Enabled = true;

            myTimer_Stu.AutoReset = true;
            #endregion

            #region 定义定时器（自动检测学校公网IP）
            int intSeconds_Url = 10;
            try
            {
                intSeconds_Url = Rc.Common.ConfigHelper.GetConfigInt("VerifySchoolUrlSeconds");
            }
            catch (Exception)
            {
                intSeconds_Url = 10;
            }
            System.Timers.Timer myTimer_Url = new System.Timers.Timer(1000 * intSeconds_Url);

            myTimer_Url.Elapsed += new ElapsedEventHandler(VerifySchoolUrl_Elapsed);

            myTimer_Url.Enabled = true;

            myTimer_Url.AutoReset = true;
            #endregion
        }

        /// <summary>
        /// 自动同步
        /// </summary>
        void myTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                //只在生产平台开启自动同步
                if (Rc.Common.ConfigHelper.GetConfigBool("SyncMode"))
                {
                    AddLogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":自动同步AutoTask is Working!", true);
                    YourTask();
                }
            }
            catch (Exception ee)
            {
                AddLogInfo(ee.Message.ToString(), true);
            }

        }

        /// <summary>
        /// 自动提交学生作业 17-05-16
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        void AutoSubmitStuHW_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                //运营平台 开启自动提交学生作业
                if (Rc.Common.ConfigHelper.GetConfigBool("AutoSubmitStuHW"))
                {
                    AddLogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":自动提交学生作业AutoTask is Working!", true, "log_stu.txt");
                    string url = Rc.Common.ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl") + "/AuthApi/VerifyStudentAnswer.aspx";//运营平台地址
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                }
            }
            catch (Exception ee)
            {
                AddLogInfo(ee.Message.ToString(), true, "log_stu.txt");
            }

        }

        /// <summary>
        /// 自动检测学校公网IP 17-08-02TS
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        void VerifySchoolUrl_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                //只在运营平台开启自动检测学校公网IP
                if (Rc.Common.ConfigHelper.GetConfigBool("VerifySchoolUrl"))
                {
                    AddLogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":自动检测学校公网IPAutoTask is Working!", true, "log_url.txt");
                    string url = Rc.Common.ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl") + "/ReCloudMgr/VerifySchoolPublicUrl.aspx";//运营平台地址
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                }
            }
            catch (Exception ee)
            {
                AddLogInfo(ee.Message.ToString(), true, "log_url.txt");
            }

        }

        /// <summary>
        /// 自动同步
        /// </summary>
        void YourTask()
        {
            if (DateTime.Now.ToString("HH:mm") == Rc.Common.ConfigHelper.GetConfigString("SyncDataTime"))
            {
                //同步数据（生产平台启动URL同步）
                string url = Rc.Common.ConfigHelper.GetConfigString("ProductPublicUrl") + "ReCloudMgr/SyncData.aspx";//生产平台地址
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            }

            if (DateTime.Now.ToString("HH:mm") == Rc.Common.ConfigHelper.GetConfigString("SyncPlanTime"))
            {
                //同步教案（生产平台启动，调用运营平台URL下载）
                string urlPlan = Rc.Common.ConfigHelper.GetConfigString("SynTeachingPlanWebSiteUrl") + "ReCloudMgr/FileSyncPlan_Auto.aspx";//运营平台地址
                HttpWebRequest myHttpWebRequestPlan = (HttpWebRequest)WebRequest.Create(urlPlan);
                HttpWebResponse myHttpWebResponsePlan = (HttpWebResponse)myHttpWebRequestPlan.GetResponse();

                //同步习题集（生产平台启动，调用运营平台URL下载）
                string urlTestpaper = Rc.Common.ConfigHelper.GetConfigString("SynTestWebSiteUrl") + "ReCloudMgr/FileSyncTestPaper_Auto.aspx";//运营平台地址
                HttpWebRequest myHttpWebRequestTestpaper = (HttpWebRequest)WebRequest.Create(urlTestpaper);
                HttpWebResponse myHttpWebResponseTestpaper = (HttpWebResponse)myHttpWebRequestTestpaper.GetResponse();

            }
            if (DateTime.Now.ToString("HH:mm") == Rc.Common.ConfigHelper.GetConfigString("SyncPlanSchoolTime"))
            {
                //同步教案（学校）（生产平台启动，获取运营平台学校URL地址，调用学校对外URL下载）
                DataTable dt = new Rc.BLL.Resources.BLL_ConfigSchool().GetOperateList("D_PublicValue<>''").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string url = item["D_PublicValue"].ToString() + "ReCloudMgr/FileSyncSchool_Auto.aspx?SchoolId=" + item["School_ID"];//学校对外URL地址
                        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                    }
                }

                //string urlSchool = Rc.Common.ConfigHelper.GetConfigString("ProductPublicUrl") + "ReCloudMgr/FileSyncSchoolUrl.aspx";//生产平台地址
                //HttpWebRequest myHttpWebRequestSchool = (HttpWebRequest)WebRequest.Create(urlSchool);
                //HttpWebResponse myHttpWebResponseSchool = (HttpWebResponse)myHttpWebRequestSchool.GetResponse();
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            
            // 生产平台
            if (Rc.Common.ConfigHelper.GetConfigBool("SyncMode"))
            {
                AddLogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":生产平台Application End!", true);

                //下面的代码是关键，可解决IIS应用程序池自动回收的问题
                Thread.Sleep(1000);

                //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start
                string url = Rc.Common.ConfigHelper.GetConfigString("ProductPublicUrl") + "index.aspx";
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                //Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流
                AddLogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":生产平台Application Restart!", true);
            }

            // 运营平台 自动提交学生作业
            if (Rc.Common.ConfigHelper.GetConfigBool("AutoSubmitStuHW"))
            {
                AddLogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":运营平台Application End!", true, "log_stu.txt");

                //下面的代码是关键，可解决IIS应用程序池自动回收的问题
                Thread.Sleep(1000);

                //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start
                string url_Operate = Rc.Common.ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl") + "index.aspx";
                HttpWebRequest myHttpWebRequest_Operate = (HttpWebRequest)WebRequest.Create(url_Operate);
                HttpWebResponse myHttpWebResponse_Operate = (HttpWebResponse)myHttpWebRequest_Operate.GetResponse();

                //Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流
                AddLogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":运营平台Application Restart!", true, "log_stu.txt");
            }

            // 运营平台 检测学校公网IP
            if (Rc.Common.ConfigHelper.GetConfigBool("VerifySchoolUrl"))
            {
                AddLogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":运营平台学校公网URL Application End!", true, "log_url.txt");

                //下面的代码是关键，可解决IIS应用程序池自动回收的问题
                Thread.Sleep(1000);

                //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start
                string url_Operate = Rc.Common.ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl") + "index.aspx";
                HttpWebRequest myHttpWebRequest_Operate = (HttpWebRequest)WebRequest.Create(url_Operate);
                HttpWebResponse myHttpWebResponse_Operate = (HttpWebResponse)myHttpWebRequest_Operate.GetResponse();

                //Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流
                AddLogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":运营平台学校公网URL Application Restart!", true, "log_url.txt");
            }
            
        }

        public void AddLogInfo(string txt, bool isAppend)
        {
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "\\Upload\\GlobalLog\\";
            DateTime CurrTime = DateTime.Now;

            //拼接日志完整路径
            logPath = logPath + CurrTime.ToString("yyyy-MM-dd") + "\\log.txt";
            string strDirecory = logPath.Substring(0, logPath.LastIndexOf('\\'));
            if (!Directory.Exists(strDirecory))
            {
                Directory.CreateDirectory(strDirecory);
            }
            if (!File.Exists(logPath))
            {
                File.Create(logPath).Dispose();
            }

            StreamWriter fs;
            if (isAppend) fs = File.AppendText(logPath);
            else fs = File.CreateText(logPath);

            fs.WriteLine(txt);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        public void AddLogInfo(string txt, bool isAppend, string fileName)
        {
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "\\Upload\\GlobalLog\\";
            DateTime CurrTime = DateTime.Now;
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "log_stu.txt";
            }
            //拼接日志完整路径
            logPath = logPath + CurrTime.ToString("yyyy-MM-dd") + "\\" + fileName;
            string strDirecory = logPath.Substring(0, logPath.LastIndexOf('\\'));
            if (!Directory.Exists(strDirecory))
            {
                Directory.CreateDirectory(strDirecory);
            }
            if (!File.Exists(logPath))
            {
                File.Create(logPath).Dispose();
            }

            StreamWriter fs;
            if (isAppend) fs = File.AppendText(logPath);
            else fs = File.CreateText(logPath);

            fs.WriteLine(txt);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

    }
}