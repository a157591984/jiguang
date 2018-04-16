using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.BLL;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Configuration;
using System.Text;
using Rc.Common.Config;
using System.Data;
using System.IO;
using System.Collections;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncFail : Rc.Cloud.Web.Common.InitPage
    {
        StringBuilder strLog = new StringBuilder();
        public string TimeLenght = string.Empty;
        public string strReveiveWebSiteUrl = ConfigurationManager.AppSettings["SynTeachingPlanWebSiteUrl"].ToString();
        public string SynTestWebSiteUrl = ConfigurationManager.AppSettings["SynTestWebSiteUrl"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10259000";
            if (!IsPostBack)
            {
                // BindList();
            }
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetDataList(int PageIndex, int PageSize)
        {
            try
            {
                string strWhere = string.Empty;
                BLL_FileSyncRecordFail bll = new BLL_FileSyncRecordFail();
                strWhere = "";
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                DataTable dt = bll.GetListByPageForFileSyncRecordFail("", " SyncFailTime", (PageIndex - 1) * PageSize + 1, PageIndex * PageSize).Tables[0];

                List<object> listReturn = new List<object>();
                int RecordCount = bll.GetRecordCount("");
                int inum = 1;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listReturn.Add(new
                        {
                            Num = inum + PageSize * (PageIndex - 1),
                            SyncFailTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["SyncFailTime"].ToString()),
                            Name = dt.Rows[i]["Resource_Name"].ToString(),
                            type = dt.Rows[i]["File_Type"].ToString(),
                            url = dt.Rows[i]["FileUrl"].ToString()
                        });
                        inum++;
                    }
                }

                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = RecordCount,
                        list = listReturn
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "暂无数据"
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
        }

    


        /// <summary>
        /// 同步数据
        /// </summary>
        private void SynchronousData()
        {

            try
            {
                Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                //model.FileSize = ls;
                // model.SyncUrl = strFilePath;
                model.MsgType = "SynDataStart";

                model.CreateTime = DateTime.Now;
                bll.Add(model);
                int result = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("EXEC P_FileSyncRecordFail", 3600);
                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                //model.FileSize = ls;
                // model.SyncUrl = strFilePath;
                model.MsgType = "SynDataEnd";

                model.CreateTime = DateTime.Now;
                bll.Add(model);
                Response.Redirect(Request.Url.ToString());

            }
            catch (Exception ex)
            {
                Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                //model.FileSize = ls;
                // model.SyncUrl = strFilePath;
                model.MsgType = "SynDataError";
                model.ErrorMark = ex.Message.ToString();
                model.CreateTime = DateTime.Now;
                bll.Add(model);
                Response.Redirect(Request.Url.ToString());
            }
        }
        /// <summary>
        /// 同步失败文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUsering_Click(object sender, EventArgs e)
        {

            try
            {
                //string strDate = txtBeginTime.Text.Trim();
                //if (string.IsNullOrEmpty(strDate))
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.closeAll();layer.msg('文件同步失败',{icon: 2, time: 2000 });", true);
                //    return;
                //}

                try
                {
                    Model_FileSyncRecord modelSync = new Model_FileSyncRecord();
                    modelSync.FileSyncRecord_Id = Guid.NewGuid().ToString();
                    modelSync.SyncTime = DateTime.Now;

                    Stopwatch timer = new System.Diagnostics.Stopwatch();
                    timer.Start();

                    #region 同步

                    //把运营平台的失败文件数据同步到生产平台
                    //SynchronousData();
                    Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                    Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                    model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                    model.MsgType = "Start";
                    model.ErrorMark = DateTime.Now.ToString();
                    model.CreateTime = DateTime.Now;
                    bll.Add(model);
                    model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                    model.MsgType = "Start";
                    model.ErrorMark = DateTime.Now.ToString();
                    model.CreateTime = DateTime.Now;
                    bll.Add(model);

                    strLog = new StringBuilder();

                    //同步文件
                    ResSynchroTeachingPlan();
                    ResSynchroTeachingPlanView();
                    ResSynchroTest();

                    model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                    //model.FileSize = ls;
                    // model.SyncUrl = strFilePath;
                    model.MsgType = "End";
                    // model.ErrorMark = ex.Message;
                    model.CreateTime = DateTime.Now;
                    bll.Add(model);
                    //}
                    #endregion

                    timer.Stop();
                    string lenght = timer.Elapsed.ToString();
                    modelSync.SyncUserId = loginUser.SysUser_ID;
                    modelSync.SyncUserName = loginUser.SysUser_Name == "" ? loginUser.SysUser_LoginName : loginUser.SysUser_Name;
                    modelSync.SyncLong = lenght;


                    if (new BLL_FileSyncRecord().Add(modelSync))
                    {
                        string TempSql = "delete from FileSyncRecordFail";
                        Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(TempSql);
                        ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.closeAll();layer.msg('文件同步成功',{icon: 1, time: 1000 },function(){loadData();});", true);
                        return;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.closeAll();layer.msg('文件同步失败',{icon: 2, time: 2000 });", true);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                    Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                    model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                    //model.FileSize = ls;
                    // model.SyncUrl = strFilePath;
                    model.MsgType = "Error";
                    model.ErrorMark = ex.Message;
                    model.CreateTime = DateTime.Now;
                    bll.Add(model);
                    throw;
                }

            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS("文件同步失败：", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                   , ex.TargetSite.Name.ToString(), ex.Message));
                ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.closeAll();layer.msg('文件同步失败，出现异常',{icon: 2, time: 2000 });", true);
            }
        }


        /// <summary>
        /// 教案(SW与class)BASE64内容后缀为dsc SW的预览暂时使用此值后缀为htm
        /// </summary>,ResourceToResourceFolder_id  as FileName 
        /// <param name="strDate"></param>
        public void ResSynchroTeachingPlan()
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
            strSql = string.Format(@"select rtrf.ParticularYear,rtrf.GradeTerm,rtrf.Resource_Version,Subject,rtrf.Resource_Type,rtrf.ResourceToResourceFolder_id  as FileName from dbo.FileSyncRecordFail fsrf inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=fsrf.ResourceToResourceFolder_Id  
                where 1=1
                and fsrf.Resource_Type in('{0}','{1}','{2}')
                order by fsrf.SyncFailTime desc", Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, Resource_TypeConst.class类型微课件);
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
                else if (Resource_TypeConst.class类型微课件 == dt.Rows[i]["Resource_Type"].ToString())
                {
                    strPathType = "microClassDocument";
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

                    // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + " \n");
                    upLoadFile(strTeachingPlanWebSitePath, strPathCommon, strFileNameFull, strFilePath);
                    // sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongDateString() + " \n");
                }
            }


        }
        /// <summary>
        /// --教案(SW与class)BASE64图片内容(SW暂时不用) 后缀名为jpg
        /// </summary>inner join ResourceToResourceFolder_img t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
        /// <param name="strDate"></param>
        public void ResSynchroTeachingPlanView()
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
            strSql = string.Format(@"select rtrf.ParticularYear,rtrf.GradeTerm,rtrf.Resource_Version,Subject,rtrf.Resource_Type,rtrf.ResourceToResourceFolder_id,  t2.ResourceToResourceFolder_img_id as FileName from dbo.FileSyncRecordFail fsrf 
                   inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=fsrf.ResourceToResourceFolder_Id 
                   inner join ResourceToResourceFolder_img t2 on rtrf.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
                where 1=1
                and fsrf.Resource_Type in('{0}','{1}','{2}')
                order by fsrf.SyncFailTime desc", Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, Resource_TypeConst.class类型微课件);
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
                else if (Resource_TypeConst.class类型微课件 == dt.Rows[i]["Resource_Type"].ToString())
                {
                    strPathType = "microClassView";
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
                if (File.Exists(strFilePath))
                {

                    // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + " \n");
                    upLoadFile(strTeachingPlanWebSitePath, strPathCommon, strFileNameFull, strFilePath);
                    // sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongDateString() + " \n");
                }

            }


        }
        /// <summary>
        /// --习题集-题干(BASE64,HTML内容,选择题的选项（数组对象）,标准答案,解析，强化训练)
        /// </summary>
        /// <param name="strDate"></param>
        public void ResSynchroTest()
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
            strSql = string.Format(@"select rtrf.ParticularYear,rtrf.GradeTerm,rtrf.Resource_Version,Subject,rtrf.Resource_Type,rtrf.ResourceToResourceFolder_id
,t2.TestQuestions_ID as FileName ,t3.TestQuestions_Score_ID as fileNameSub,t2.TestQuestions_Type from dbo.FileSyncRecordFail fsrf
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=fsrf.ResourceToResourceFolder_Id  
inner join TestQuestions t2 on rtrf.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
left join TestQuestions_Score t3 on t2.TestQuestions_Id=t3.TestQuestions_Id
where 1=1
and fsrf.Resource_Type in('{0}')
order by t2.Createtime desc", Resource_TypeConst.testPaper类型文件);
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

                    //sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + " \n");
                    upLoadFile(strTeachingPlanWebSitePath, "testQuestionBody\\" + strPathCommon, strFileNameFull, strFilePath);
                    // sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongDateString() + " \n");
                }
                //题干 	HTML内容，存储目录：testQuestionBody/属性层次目录/试题标识.htm
                strFilePath = strWebSitePath + "testQuestionBody\\" + strPathCommon + strFileName + ".htm";
                strFileNameFull = strFileName + ".htm";
                if (File.Exists(strFilePath))
                {

                    // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + " \n");
                    upLoadFile(strTeachingPlanWebSitePath, "testQuestionBody\\" + strPathCommon, strFileNameFull, strFilePath);
                    // sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongDateString() + " \n");
                }

                //	选择题的选项（数组对象）存储目录：testQuestionOption/属性层次目录/试题标识.txt（存储JSION结构到文本文件，一个题只有一个文件）
                strFilePath = strWebSitePath + "testQuestionOption\\" + strPathCommon + strFileNameSub + ".txt";
                strFileNameFull = strFileNameSub + ".txt";
                if (File.Exists(strFilePath))
                {

                    // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + " \n");
                    upLoadFile(strTeachingPlanWebSitePath, "testQuestionOption\\" + strPathCommon, strFileNameFull, strFilePath);
                    //sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongDateString() + " \n");
                }
                //	c)	标准答案（数组）存储目录：testQuestionCurrent/属性层次目录/试题标识.txt（存储JSION结构到文本文件，一个题只有一个文件）
                strFilePath = strWebSitePath + "testQuestionCurrent\\" + strPathCommon + strFileNameSub + ".txt";
                strFileNameFull = strFileNameSub + ".txt";
                if (File.Exists(strFilePath))
                {

                    //sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + " \n");
                    upLoadFile(strTeachingPlanWebSitePath, "testQuestionCurrent\\" + strPathCommon, strFileNameFull, strFilePath);
                    // sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongDateString() + " \n");
                }
                //	解析 	BASE64内容AnalyzeData/属性层次目录/试题标识.txt
                strFilePath = strWebSitePath + "AnalyzeData\\" + strPathCommon + strFileNameSub + ".txt";
                strFileNameFull = strFileNameSub + ".txt";
                if (File.Exists(strFilePath))
                {

                    // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + " \n");
                    upLoadFile(strTeachingPlanWebSitePath, "AnalyzeData\\" + strPathCommon, strFileNameFull, strFilePath);
                    // sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongDateString() + " \n");
                }
                //	解析 	HTML内容AnalyzeHtml/属性层次目录/试题标识.htm
                strFilePath = strWebSitePath + "AnalyzeHtml\\" + strPathCommon + strFileNameSub + ".htm";
                strFileNameFull = strFileNameSub + ".htm";
                if (File.Exists(strFilePath))
                {

                    // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + " \n");
                    upLoadFile(strTeachingPlanWebSitePath, "AnalyzeHtml\\" + strPathCommon, strFileNameFull, strFilePath);
                    // sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongDateString() + " \n");
                }
                //	e)	强化训练 		BASE64内容TrainData/属性层次目录/试题标识.txt
                strFilePath = strWebSitePath + "TrainData\\" + strPathCommon + strFileNameSub + ".txt";
                strFileNameFull = strFileNameSub + ".txt";
                if (File.Exists(strFilePath))
                {

                    // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + " \n");
                    upLoadFile(strTeachingPlanWebSitePath, "TrainData\\" + strPathCommon, strFileNameFull, strFilePath);
                    // sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongDateString() + " \n");
                }
                //	e)	强化训练 		HTML内容TrainHtml/属性层次目录/试题标识.htm
                strFilePath = strWebSitePath + "TrainHtml\\" + strPathCommon + strFileNameSub + ".htm";
                strFileNameFull = strFileNameSub + ".htm";
                if (File.Exists(strFilePath))
                {

                    // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + " \n");
                    upLoadFile(strTeachingPlanWebSitePath, "TrainHtml\\" + strPathCommon, strFileNameFull, strFilePath);
                    //sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongDateString() + " \n");
                }
                ////	作业的原DSC文件 	
                //strFilePath = strWebSitePath + "testPaperFile\\" + strPathCommon + strFileNameSub + ".dsc";
                //strFileNameFull = strFileNameSub + ".dsc";
                //if (File.Exists(strFilePath))
                //{

                //    // sw.WriteLine("同步开始：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + " \n");
                //    upLoadFile(strTeachingPlanWebSitePath, "testPaperFile\\" + strPathCommon, strFileNameFull, strFilePath);
                //    //sw.WriteLine("同步完成：" + strFilePath + "；完成时间：" + DateTime.Now.ToLongDateString() + " \n");
                //}
            }


        }
        private string upLoadFile(string url, string strPath, string strFileName, string strFilePath)
        {
            FileInfo f = new FileInfo(strFilePath);
            long ls = f.Length;
            string strTemp = string.Empty;
            //Rc.Web.Payment.Core.LogHandler.WriteLogForPay("123123123", string.Format("url:{0},strPath:{1},strFileName:{2},strFilePath:{3}", url, strPath, strFileName, strFilePath), "");

            if (ls <= 1024 * 1024 * 2000)//小于2000兆的同步，大于2000兆的不同步了
            //if (1024 * 1024 * 8 <= ls && ls <= 1024 * 1024 * 20)//小于20兆的同步，大于20兆的不同步了
            {
                string strErryType = string.Empty;

                //sw.WriteLine("同步开始【" + ls.ToString() + "】：" + strFilePath + "；开始时间：" + DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString() + " \n");
                // Server.ScriptTimeout = 36000;
                System.Net.WebClient myWebClient = new System.Net.WebClient();
                // myWebClient.Headers.Add("Content-Type", ContentType);
                myWebClient.QueryString["directory"] = strPath;
                myWebClient.QueryString["fname"] = strFileName;

                try
                {
                    Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                    Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                    //myWebClient.UploadFileAsync(new Uri(url), "POST", strFilePath);
                    myWebClient.UploadFile(new Uri(url), "POST", strFilePath);
                    strTemp = "1";
                    model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                    model.FileSize = ls;
                    model.SyncUrl = strFilePath;
                    model.MsgType = "OK";
                    //model.ErrorMark = ex.Message;
                    model.CreateTime = DateTime.Now;
                    bll.Add(model);
                    //if (strPath.IndexOf("microClassDocument") > -1) XHZ.Web.Payment.Core.LogHandler.WriteLogForPay(DateTime.Now.ToString("yyyy-MM-dd"), "同步microClassDocument", strFileName);
                }
                catch (Exception ex)
                {
                    Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                    Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
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
                //if ((ls > 1024 * 1024 * 20))
                //{
                Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                // sw.WriteLine("太大【" + ls.ToString() + "】没同步：" + strFilePath + "；日期：" + DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString() + " \n");
                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                model.FileSize = ls;
                model.SyncUrl = strFilePath;
                model.MsgType = "ErrorSize";
                model.ErrorMark = "文件太大【" + ls.ToString() + "】没同步：";
                model.CreateTime = DateTime.Now;
                bll.Add(model);
                //}

            }
            return strTemp;
        }

        protected void btnDate_Click(object sender, EventArgs e)
        {
            SynchronousData();
        }




        [WebMethod]
        public static string Syn()
        {
            try
            {
                Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                //model.FileSize = ls;
                // model.SyncUrl = strFilePath;
                model.MsgType = "SynDataStart";

                model.CreateTime = DateTime.Now;
                bll.Add(model);
                int result = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("EXEC P_FileSyncRecordFail", 3600);
                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                //model.FileSize = ls;
                // model.SyncUrl = strFilePath;
                model.MsgType = "SynDataEnd";

                model.CreateTime = DateTime.Now;
                bll.Add(model);

                return "1";




            }
            catch (Exception ex)
            {
                Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                //model.FileSize = ls;
                // model.SyncUrl = strFilePath;
                model.MsgType = "SynDataError";
                model.ErrorMark = ex.Message.ToString();
                model.CreateTime = DateTime.Now;
                bll.Add(model);
                return "1";
            }
        }
    }
}