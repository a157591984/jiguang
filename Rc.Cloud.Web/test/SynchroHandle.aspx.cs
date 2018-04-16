using Rc.Cloud.BLL;
using Rc.Common.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.test
{
    public partial class SynchroHandle : System.Web.UI.Page
    {
        FileStream fs = null;
        StreamWriter sw = null;
        protected void Page_Load(object sender, EventArgs e)
        {


            //Response.Write();
            //string strUrlSWBase64 = "D:\\work\\TeachingPlatform\\RE\\trunk\\Rc.Cloud\\Rc.Cloud.Web\\Upload\\Resource\\8b7cdcaa-fa05-41a4-9d88-67f1c1244c39.txt";
            //string fileName = @"C:\upload.txt";
            //string imgUrl = upLoadPic("http://localhost:1528/AuthApi/UploadHandler.ashx", "目录", strUrlSWBase64);
        }
        /// <summary>
        /// --习题集-学生答案
        /// </summary>
        /// <param name="strDate"></param>
        public void ResSynchroStudentAnswer(string strDate)
        {

            try
            {
                //生产环境web站点存放文件的主目录
                string strWebSitePath = ConfigurationManager.AppSettings["WebSitePath"].ToString();
                //教案存放的地址
                string strReveiveWebSiteUrl = ConfigurationManager.AppSettings["SynStudentAnswerWebSiteUrl"].ToString();
                //教案存放地址的数据接收页面
                string strTeachingPlanWebSitePath = string.Format("{0}/AuthApi/UploadHandler.ashx", strReveiveWebSiteUrl);

                string strSql = string.Empty;
                // string strDate = DateTime.Now.AddDays(-1).ToShortDateString();
                //教案(SW与class)BASE64内容 SW暂时使用此地址
                strSql = string.Format(@"select  ParticularYear,GradeTerm,Resource_Version,Subject,t1.ResourceToResourceFolder_id
,t2.TestQuestions_ID,t2.TestQuestions_Type
,t4.Student_HomeWorkAnswer_id as FileName
,t3,CreateTime as HomeWorkTime
from ResourceToResourceFolder t1 
inner join TestQuestions t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
inner join HomeWork t3 on t1.ResourceToResourceFolder_id=t3.ResourceToResourceFolder_id
inner join Student_HomeWorkAnswer t4 on t3.HomeWork_ID=T4.HomeWork_ID
where 1=1
and Resource_Type in('{0}')
and convert(varchar(10),t2.CreateTime,120)='{1}'
order by t2.Createtime desc", Resource_TypeConst.testPaper类型文件, strDate);
                DataTable dt = new DataTable();

                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strPathCommon = string.Empty;
                    string strPathType = string.Empty;
                    string strFileName = string.Empty;//文件名（没后缀）
                    string strFileNameEx = string.Empty;//文件后缀

                    string strFileNameFull = string.Empty;//文件全名
                    strPathType = "studentAnswer";
                    strFileName = dt.Rows[i]["FileName"].ToString();
                    strFileNameEx = "txt";
                    strPathCommon = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\{5}\\"
                        , strPathType
                        , Rc.Cloud.Web.Common.pfunction.ToShortDate(dt.Rows[i]["HomeWorkTime"].ToString())
                        , dt.Rows[i]["ParticularYear"].ToString()
                        , dt.Rows[i]["GradeTerm"].ToString()
                        , dt.Rows[i]["Resource_Version"].ToString()
                        , dt.Rows[i]["Subject"].ToString());
                    string strFilePath = string.Empty;
                    //学生答案
                    strFilePath = strWebSitePath + strPathCommon + strFileName + "." + strFileNameEx;
                    strFileNameFull = strFileName + "." + strFileNameEx;
                    if (File.Exists(strFilePath))
                    {
                        //sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongTimeString() + " \n");
                        upLoadFile(strTeachingPlanWebSitePath, strPathCommon, strFileNameFull, strFilePath);
                        // sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongTimeString() + " \n");
                    }
                }
            }
            catch (Exception ex)
            {


                //记录错误日志
                new BLL_clsAuth().AddLogErrorFromBS("习题集-学生答案,同步错误：", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                    , ex.TargetSite.Name.ToString(), ex.Message));
                //FileStream fs = new FileStream(@"d:\test.txt", FileMode.OpenOrCreate, FileAccess.Write);
                //StreamWriter sw = new StreamWriter(fs);
                //sw.BaseStream.Seek(0, SeekOrigin.End);
                //sw.WriteLine("Error:ResSynchroStudentAnswer, " + ex.Message.ToString() + "\n");
                //sw.Flush();
                //sw.Close();
                //fs.Close();
                throw;
            }

        }
        /// <summary>
        /// --习题集-题干(BASE64,HTML内容,选择题的选项（数组对象）,标准答案,解析，强化训练)
        /// </summary>
        /// <param name="strDate"></param>
        public void ResSynchroTest(string strDate)
        {

            try
            {
                //生产环境web站点存放文件的主目录
                string strWebSitePath = ConfigurationManager.AppSettings["WebSitePath"].ToString();
                //习题集存放的地址
                string strTeachingPlanWebSiteUrl = ConfigurationManager.AppSettings["SynTestWebSiteUrl"].ToString();
                //教案存放地址的数据接收页面
                string strTeachingPlanWebSitePath = string.Format("{0}/AuthApi/UploadHandler.ashx", strTeachingPlanWebSiteUrl);

                string strSql = string.Empty;
                // string strDate = DateTime.Now.AddDays(-1).ToShortDateString();
                //教案(SW与class)BASE64内容 SW暂时使用此地址
                strSql = string.Format(@"select  ParticularYear,GradeTerm,Resource_Version,Subject,t1.ResourceToResourceFolder_id
,t2.TestQuestions_ID as FileName ,t3.TestQuestions_Score_ID as fileNameSub,t2.TestQuestions_Type
from ResourceToResourceFolder t1 
inner join TestQuestions t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
left join TestQuestions_Score t3 on t2.TestQuestions_Id=t3.TestQuestions_Id
where 1=1
and Resource_Type in('{0}')
and convert(varchar(10),t2.CreateTime,120)='{1}'
order by t2.Createtime desc", Resource_TypeConst.testPaper类型文件, strDate);
                DataTable dt = new DataTable();

                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strPathCommon = string.Empty;
                    string strPathType = string.Empty;
                    string strFileName = string.Empty;//文件名（没后缀）
                    string strFileNameSub = string.Empty;//文件名称不是以试题标识取名的
                    string strFileNameEx = string.Empty;//文件后缀

                    string strFileNameFull = string.Empty;//文件全名

                    strFileName = dt.Rows[i]["FileName"].ToString();
                    strFileNameSub = dt.Rows[i]["fileNameSub"].ToString();
                    strPathCommon = string.Format("{0}\\{1}\\{2}\\{3}\\"
                        , dt.Rows[i]["ParticularYear"].ToString()
                        , dt.Rows[i]["GradeTerm"].ToString()
                        , dt.Rows[i]["Resource_Version"].ToString()
                        , dt.Rows[i]["Subject"].ToString());
                    string strFilePath = string.Empty;
                    //题干	BASE64内容，存储目录：testQuestionBody/属性层次目录/试题标识.txt
                    strFilePath = strWebSitePath + "testQuestionBody\\" + strPathCommon + strFileName + ".txt";
                    strFileNameFull = strFileName + ".txt";
                    if (File.Exists(strFilePath))
                    {

                        //sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongTimeString() + " \n");
                        upLoadFile(strTeachingPlanWebSitePath, "testQuestionBody\\" + strPathCommon, strFileNameFull, strFilePath);
                        //sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongTimeString() + " \n");
                    }
                    //题干 	HTML内容，存储目录：testQuestionBody/属性层次目录/试题标识.htm
                    strFilePath = strWebSitePath + "testQuestionBody\\" + strPathCommon + strFileName + ".htm";
                    strFileNameFull = strFileName + ".htm";
                    if (File.Exists(strFilePath))
                    {

                        //sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongTimeString() + " \n");
                        upLoadFile(strTeachingPlanWebSitePath, "testQuestionBody\\" + strPathCommon, strFileNameFull, strFilePath);
                        // sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongTimeString() + " \n");
                    }

                    //	选择题的选项（数组对象）存储目录：testQuestionOption/属性层次目录/试题标识.txt（存储JSION结构到文本文件，一个题只有一个文件）
                    strFilePath = strWebSitePath + "testQuestionOption\\" + strPathCommon + strFileNameSub + ".txt";
                    strFileNameFull = strFileNameSub + ".txt";
                    if (File.Exists(strFilePath))
                    {

                        // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongTimeString() + " \n");
                        upLoadFile(strTeachingPlanWebSitePath, "testQuestionOption\\" + strPathCommon, strFileNameFull, strFilePath);
                        //sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongTimeString() + " \n");
                    }
                    //	c)	标准答案（数组）存储目录：testQuestionCurrent/属性层次目录/试题标识.txt（存储JSION结构到文本文件，一个题只有一个文件）
                    strFilePath = strWebSitePath + "testQuestionCurrent\\" + strPathCommon + strFileNameSub + ".txt";
                    strFileNameFull = strFileNameSub + ".txt";
                    if (File.Exists(strFilePath))
                    {

                        //sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongTimeString() + " \n");
                        upLoadFile(strTeachingPlanWebSitePath, "testQuestionCurrent\\" + strPathCommon, strFileNameFull, strFilePath);
                        //sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongTimeString() + " \n");
                    }
                    //	解析 	BASE64内容AnalyzeData/属性层次目录/试题标识.txt
                    strFilePath = strWebSitePath + "AnalyzeData\\" + strPathCommon + strFileNameSub + ".txt";
                    strFileNameFull = strFileNameSub + ".txt";
                    if (File.Exists(strFilePath))
                    {

                        //sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongTimeString() + " \n");
                        upLoadFile(strTeachingPlanWebSitePath, "AnalyzeData\\" + strPathCommon, strFileNameFull, strFilePath);
                        //sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongTimeString() + " \n");
                    }
                    //	解析 	HTML内容AnalyzeHtml/属性层次目录/试题标识.htm
                    strFilePath = strWebSitePath + "AnalyzeHtml\\" + strPathCommon + strFileNameSub + ".htm";
                    strFileNameFull = strFileNameSub + ".htm";
                    if (File.Exists(strFilePath))
                    {

                        // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongTimeString() + " \n");
                        upLoadFile(strTeachingPlanWebSitePath, "AnalyzeHtml\\" + strPathCommon, strFileNameFull, strFilePath);
                        //sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongTimeString() + " \n");
                    }
                    //	e)	强化训练 		BASE64内容TrainData/属性层次目录/试题标识.txt
                    strFilePath = strWebSitePath + "TrainData\\" + strPathCommon + strFileNameSub + ".txt";
                    strFileNameFull = strFileNameSub + ".txt";
                    if (File.Exists(strFilePath))
                    {

                        sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongTimeString() + " \n");
                        upLoadFile(strTeachingPlanWebSitePath, "TrainData\\" + strPathCommon, strFileNameFull, strFilePath);
                        sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongTimeString() + " \n");
                    }
                    //	e)	强化训练 		HTML内容TrainHtml/属性层次目录/试题标识.htm
                    strFilePath = strWebSitePath + "TrainHtml\\" + strPathCommon + strFileNameSub + ".htm";
                    strFileNameFull = strFileNameSub + ".htm";
                    if (File.Exists(strFilePath))
                    {

                        //sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongTimeString() + " \n");
                        upLoadFile(strTeachingPlanWebSitePath, "TrainHtml\\" + strPathCommon, strFileNameFull, strFilePath);
                        //sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongTimeString() + " \n");
                    }
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS("习题集-题干(BASE64,HTML内容,选择题的选项（数组对象）,标准答案,解析，强化训练)，同步错误", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                    , ex.TargetSite.Name.ToString(), ex.Message));
                throw;
            }

        }
        /// <summary>
        /// --教案(SW与class)BASE64图片内容(SW暂时不用) 后缀名为jpg
        /// </summary>
        /// <param name="strDate"></param>
        public void ResSynchroTeachingPlanView(string strDate)
        {

            try
            {
                //生产环境web站点存放文件的主目录
                string strWebSitePath = ConfigurationManager.AppSettings["WebSitePath"].ToString();
                //教案预览图片存放的地址
                string strReveiveWebSiteUrl = ConfigurationManager.AppSettings["SynTeachingPlanViewWebSiteUrl"].ToString();
                //教案存放地址的数据接收页面
                string strTeachingPlanWebSitePath = string.Format("{0}/AuthApi/UploadHandler.ashx", strReveiveWebSiteUrl);

                string strSql = string.Empty;
                // string strDate = DateTime.Now.AddDays(-1).ToShortDateString();
                //教案(SW与class)BASE64内容 SW暂时使用此地址
                strSql = string.Format(@"select  ParticularYear,GradeTerm,Resource_Type,Resource_Version,Subject,t1.ResourceToResourceFolder_id
,t2.ResourceToResourceFolder_img_id  as FileName
from ResourceToResourceFolder t1 
inner join ResourceToResourceFolder_img t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
where 1=1
and Resource_Type in('{0}','{1}')
and convert(varchar(10),t2.CreateTime,120)='{2}'
order by t2.Createtime desc", Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, strDate);
                DataTable dt = new DataTable();

                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strPathCommon = string.Empty;
                    string strPathType = string.Empty;
                    string strFileName = string.Empty;//文件名（没后缀）
                    string strFileNameEx = string.Empty;//文件后缀
                    if (Resource_TypeConst.ScienceWord类型文件 == dt.Rows[i]["Resource_Type"].ToString())
                    {
                        strPathType = "swView";
                    }
                    else if (Resource_TypeConst.class类型文件 == dt.Rows[i]["Resource_Type"].ToString())
                    {
                        strPathType = "classView";
                    }
                    strFileNameEx = "jpg";//预览图片都有jpg
                    string strFileNameFull = string.Empty;//文件全名

                    strFileName = dt.Rows[i]["FileName"].ToString();
                    strPathCommon = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\"
                         , strPathType
                        , dt.Rows[i]["ParticularYear"].ToString()
                        , dt.Rows[i]["GradeTerm"].ToString()
                        , dt.Rows[i]["Resource_Version"].ToString()
                        , dt.Rows[i]["Subject"].ToString());
                    string strFilePath = string.Empty;
                    //同步预览图片文件
                    strFilePath = strWebSitePath + strPathCommon + strFileName + "." + strFileNameEx;
                    strFileNameFull = strFileName + "." + strFileNameEx;
                    //System.IO.FileInfo(Server.MapPath(files));

                    if (File.Exists(strFilePath))
                    {
                        //sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongTimeString() + " \n");
                        upLoadFile(strTeachingPlanWebSitePath, strPathCommon, strFileNameFull, strFilePath);
                        //sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongTimeString() + " \n");
                    }

                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS("教案(SW与class)BASE64图片内容(SW暂时不用) 后缀名为jpg，同步错误：", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                    , ex.TargetSite.Name.ToString(), ex.Message));
                throw;
            }

        }
        /// <summary>
        /// 教案(SW与class)BASE64内容后缀为dsc SW的预览暂时使用此值后缀为htm
        /// </summary>
        /// <param name="strDate"></param>
        public void ResSynchroTeachingPlan(string strDate)
        {


            try
            {
                //生产环境web站点存放文件的主目录
                string strWebSitePath = ConfigurationManager.AppSettings["WebSitePath"].ToString();
                //教案存放的地址
                string strReveiveWebSiteUrl = ConfigurationManager.AppSettings["SynTeachingPlanWebSiteUrl"].ToString();
                //教案存放地址的数据接收页面
                string strTeachingPlanWebSitePath = string.Format("{0}/AuthApi/UploadHandler.ashx", strReveiveWebSiteUrl);

                string strSql = string.Empty;
                // string strDate = DateTime.Now.AddDays(-1).ToShortDateString();
                //教案(SW与class)BASE64内容 SW暂时使用此地址
                strSql = string.Format(@"select  ParticularYear,GradeTerm,Resource_Version,Subject,Resource_Type,ResourceToResourceFolder_id  as FileName 
                from ResourceToResourceFolder t1
                where 1=1
                and Resource_Type in('{0}','{1}')
                and convert(varchar(10),CreateTime,120)='{2}'
                order by Createtime desc", Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, strDate);
                DataTable dt = new DataTable();

                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strPathCommon = string.Empty;
                    string strPathType = string.Empty;
                    string strFileName = string.Empty;//文件名（没后缀）
                    string strFileNameEx = string.Empty;//文件后缀
                    if (Resource_TypeConst.ScienceWord类型文件 == dt.Rows[i]["Resource_Type"].ToString())
                    {
                        strPathType = "swDocument";
                        strFileNameEx = "dsc";
                    }
                    else if (Resource_TypeConst.class类型文件 == dt.Rows[i]["Resource_Type"].ToString())
                    {
                        strPathType = "classDocument";
                        strFileNameEx = "class";
                    }
                    string strFileNameFull = string.Empty;//文件全名

                    strFileName = dt.Rows[i]["FileName"].ToString();
                    strPathCommon = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\"
                         , strPathType
                        , dt.Rows[i]["ParticularYear"].ToString()
                        , dt.Rows[i]["GradeTerm"].ToString()
                        , dt.Rows[i]["Resource_Version"].ToString()
                        , dt.Rows[i]["Subject"].ToString());
                    string strFilePath = string.Empty;
                    //同步dsc文件
                    strFilePath = strWebSitePath + strPathCommon + strFileName + "." + strFileNameEx;
                    strFileNameFull = strFileName + "." + strFileNameEx;
                    if (File.Exists(strFilePath))
                    {
                        upLoadFile(strTeachingPlanWebSitePath, strPathCommon, strFileNameFull, strFilePath);
                    }
                    //同步htm文件（sw的预览图片暂时使用html格式）
                    strFilePath = strWebSitePath + strPathCommon + strFileName + ".htm";
                    strFileNameFull = strFileName + ".htm";
                    if (File.Exists(strFilePath))
                    {

                        // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongTimeString() + " \n");
                        upLoadFile(strTeachingPlanWebSitePath, strPathCommon, strFileNameFull, strFilePath);
                        // sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongTimeString() + " \n");
                    }
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS("教案(SW与class)BASE64内容后缀为dsc SW的预览暂时使用此值后缀为htm，同步错误：", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                     , ex.TargetSite.Name.ToString(), ex.Message));
                throw;
            }

        }
        private string upLoadFile(string url, string strPath, string strFileName, string strFilePath)
        {


            FileInfo f = new FileInfo(strFilePath);
            long ls = f.Length;
            string strTemp = string.Empty;

            Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
            Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();

            if (ls <= 1024 * 1024 * 200)//小于200兆的同步，大于200兆的不同步了
            {
                string strErryType = string.Empty;

                //sw.WriteLine("同步开始【" + ls.ToString() + "】：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString() + " \n");
                Server.ScriptTimeout = 36000;
                System.Net.WebClient myWebClient = new System.Net.WebClient();
                myWebClient.Headers.Add("Content-Type", ContentType);
                myWebClient.QueryString["directory"] = strPath;
                myWebClient.QueryString["fname"] = strFileName;

                try
                {
                    //myWebClient.UploadFileAsync(new Uri(url), "POST", strFilePath);
                    myWebClient.UploadFile(new Uri(url), "POST", strFilePath);
                    strTemp = "1";
                    // model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                    // model.FileSize = ls;
                    // model.SyncUrl = strFilePath;
                    // model.MsgType = "OK";
                    ////model.ErrorMark = ex.Message;
                    // model.CreateTime = DateTime.Now;
                    // bll.Add(model);


                }
                catch (Exception ex)
                {
                    model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                    model.FileSize = ls;
                    model.SyncUrl = strFilePath;
                    model.MsgType = "Error";
                    model.ErrorMark = ex.Message;
                    model.CreateTime = DateTime.Now;
                    bll.Add(model);
                    throw;
                }

                // sw.WriteLine("结束时间：" + DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString() + " \n");


            }
            else
            {
                // sw.WriteLine("太大【" + ls.ToString() + "】没同步：" + strFilePath + "；日期：" + DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString() + " \n");
                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                model.FileSize = ls;
                model.SyncUrl = strFilePath;
                model.MsgType = "ErrorSize";
                model.ErrorMark = "文件太大【" + ls.ToString() + "】没同步：";
                model.CreateTime = DateTime.Now;
                bll.Add(model);

            }
            return strTemp;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string strWebSitePath = ConfigurationManager.AppSettings["WebSitePath"].ToString();
            fs = new FileStream(string.Format(@"{0}SynchroLog.txt", strWebSitePath), FileMode.OpenOrCreate, FileAccess.Write);
            sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            strDate = txtDate.Text.Trim();
            try
            {
                sw.WriteLine("同步开始：开始日期：" + strDate + DateTime.Now.ToLongTimeString() + " \n");
                ResSynchroTeachingPlan(strDate);
                ResSynchroTeachingPlanView(strDate);
                ResSynchroTest(strDate);
                sw.WriteLine("结束时间：" + DateTime.Now.ToLongTimeString() + " \n");
                sw.Flush();
                sw.Close();
                fs.Close();
                //同步学生答案，是多余的，原因是学生不会在生产端做作业
                //ResSynchroStudentAnswer(strDate);
            }
            catch (Exception ex)
            {
                if (sw != null)
                {
                    sw.WriteLine("同步错误：" + ex.Message.ToString() + "；时间：" + DateTime.Now.ToLongDateString() + " \n");
                    sw.Flush();
                    sw.Close();
                    if (fs != null)
                    {
                        fs.Close();
                    }

                }
                throw;
            }

        }

        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSynchronousData_Click(object sender, EventArgs e)
        {
            try
            {
                int result = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql("EXEC P_SynchronousData ");

            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS("激活同步错误：", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                   , ex.TargetSite.Name.ToString(), ex.Message));
            }
        }

        /// <summary>
        /// 统计生产数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStatisticsData_Click(object sender, EventArgs e)
        {
            try
            {
                int result = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql("EXEC P_GenerateStatisticsData");

            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS("统计生产数据错误：", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                   , ex.TargetSite.Name.ToString(), ex.Message));
            }
        }
    }
}