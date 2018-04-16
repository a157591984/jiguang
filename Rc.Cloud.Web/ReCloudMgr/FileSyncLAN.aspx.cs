using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using System.Diagnostics;
using Rc.Cloud.BLL;
using System.Configuration;
using Rc.Common.Config;
using System.Data;
using System.IO;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncLAN : Rc.Cloud.Web.Common.InitPage
    {
        StringBuilder strLog = new StringBuilder();
        public string TimeLenght = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10258500";
            if (!IsPostBack)
            {

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
                BLL_FileSyncRecord bll = new BLL_FileSyncRecord();
                strWhere = " and SyncType='" + FileSyncType.SyncSchool.ToString() + "' ";
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                GH_PagerInfo<List<Model_FileSyncRecord>> pageInfo = bll.SearhExeccuteDataAnalysis(strWhere, "SyncTime desc", PageIndex, PageSize);
                List<Model_FileSyncRecord> list = pageInfo.PageData;
                List<object> listReturn = new List<object>();
                int inum = 1;
                foreach (var item in list)
                {
                    listReturn.Add(new
                    {
                        Num = inum + PageSize * (PageIndex - 1),
                        SyncTime = pfunction.ConvertToLongDateTime(item.SyncTime.ToString()),
                        SyncLong = item.SyncLong.ToString(),
                        SyncUserName = item.SyncUserName,
                        SysType = item.SyncType
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = pageInfo.CurrentPage,
                        PageSize = pageInfo.PageSize,
                        TotalCount = pageInfo.RecordCount,
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
        /// 文件同步
        /// </summary>
        protected void btnDataAnalysis_Click(object sender, EventArgs e)
        {
            try
            {
                string strDate = txtBeginTime.Text.Trim();
                string strSchool = hidtxtSchool.Value.Trim();
                string strSchoolExtranet = string.Empty;
                if (string.IsNullOrEmpty(strDate))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.closeAll();layer.msg('请选择日期',{icon: 2, time: 2000 });", true);
                    return;
                }
                if (string.IsNullOrEmpty(strSchool))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.closeAll();layer.msg('请选择学校',{icon: 2, time: 2000 });", true);
                    return;
                }
                Model_ConfigSchool modelCS = new Model_ConfigSchool();
                BLL_ConfigSchool bllCS = new BLL_ConfigSchool();
                modelCS = bllCS.GetModelBySchoolId(hidtxtSchool.Value);
                if (modelCS == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.closeAll();layer.msg('学校配置信息不存在',{icon: 2, time: 2000 });", true);
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(modelCS.D_PublicValue))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.closeAll();layer.msg('请配置学校外网IP',{icon: 2, time: 2000 });", true);
                        return;
                    }
                    else
                    {
                        strSchoolExtranet = modelCS.D_PublicValue;
                    }
                }

                try
                {
                    Model_FileSyncRecord modelSync = new Model_FileSyncRecord();
                    modelSync.FileSyncRecord_Id = Guid.NewGuid().ToString();
                    modelSync.SyncTime = DateTime.Now;

                    Stopwatch timer = new System.Diagnostics.Stopwatch();
                    timer.Start();

                    #region 同步

                    Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                    Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                    model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                    //model.FileSize = ls;
                    // model.SyncUrl = strFilePath;
                    model.MsgType = "Start";
                    model.ErrorMark = DateTime.Now.ToString();
                    model.CreateTime = DateTime.Now;
                    bll.Add(model);

                    //for (int i = 0; i < 30; i++)
                    //{
                    //string strDate = DateTime.Now.ToString("yyyy-MM-dd");

                    // Date = DateTime.Now.ToString("yyyy-MM-dd");
                    //Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                    // Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                    model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                    //model.FileSize = ls;
                    // model.SyncUrl = strFilePath;
                    model.MsgType = "Start";
                    model.ErrorMark = strDate;
                    model.CreateTime = DateTime.Now;
                    bll.Add(model);

                    strLog = new StringBuilder();

                    //同步文件
                    if (ddlType.SelectedValue == "1")//教案
                    {
                        ResSynchroTeachingPlan(strSchoolExtranet, strDate, strSchool);
                        ResSynchroTeachingPlanView(strSchoolExtranet, strDate, strSchool);
                    }
                    else if (ddlType.SelectedValue=="2")//习题集
                    {
                        ResSynchroTest(strSchoolExtranet, strDate, strSchool);
                    }
                    else
                    {
                        ResSynchroTeachingPlan(strSchoolExtranet, strDate, strSchool);
                        ResSynchroTeachingPlanView(strSchoolExtranet, strDate, strSchool);
                        ResSynchroTest(strSchoolExtranet, strDate, strSchool);
                    }

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
                    modelSync.SyncType = FileSyncType.SyncSchool.ToString();


                    if (new BLL_FileSyncRecord().Add(modelSync))
                    {
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
                int result = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("EXEC P_SynchronousData", 3600);
                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                //model.FileSize = ls;
                // model.SyncUrl = strFilePath;
                model.MsgType = "SynDataEnd";

                model.CreateTime = DateTime.Now;
                bll.Add(model);

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
            }
        }
        /// <summary>
        /// 教案(SW与class)BASE64内容后缀为dsc SW的预览暂时使用此值后缀为htm
        /// </summary>
        /// <param name="strDate"></param>
        public void ResSynchroTeachingPlan(string strSchoolExtranet, string strDate, string strSchool)
        {



            //生产环境web站点存放文件的主目录
            string strWebSitePath = ConfigurationManager.AppSettings["WebSitePath"].ToString();
            //教案存放的地址
            string strReveiveWebSiteUrl = strSchoolExtranet;
            //教案存放地址的数据接收页面
            string strTeachingPlanWebSitePath = string.Format("{0}/AuthApi/UploadHandler.ashx", strReveiveWebSiteUrl);

            string strSql = string.Empty;
            // string strDate = DateTime.Now.AddDays(-1).ToShortDateString();
            //教案(SW与class)BASE64内容 SW暂时使用此地址
            strSql = string.Format(@"select  ParticularYear,GradeTerm,Resource_Version,Subject,Resource_Type,ResourceToResourceFolder_id  as FileName 
                from ResourceToResourceFolder t1
                left join BookAudit ba on ba.ResourceFolder_Id=t1.Book_Id
                where 1=1
                and t1.Resource_Type in('{0}','{1}','{2}')
                and convert(varchar(10),ba.CreateTime,120)='{3}'
                and ba.AuditState='1'
                and t1.Book_ID in(select distinct Book_id from [103.6.222.206].TestRe.dbo.UserBuyResources where UserId in(select distinct UserId from [103.6.222.206].TestRe.dbo.VW_UserOnClassGradeSchool where SchoolId='{4}'))
                order by t1.Createtime desc"
                , Resource_TypeConst.ScienceWord类型文件
                , Resource_TypeConst.class类型文件
                , Resource_TypeConst.class类型微课件
                , strDate
                , strSchool);
            Rc.Web.Payment.Core.LogHandler.WriteLogForPay("1111", strSql, "");
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
        /// </summary>
        /// <param name="strDate"></param>
        public void ResSynchroTeachingPlanView(string strSchoolExtranet, string strDate, string strSchool)
        {


            //生产环境web站点存放文件的主目录
            string strWebSitePath = ConfigurationManager.AppSettings["WebSitePath"].ToString();
            //教案预览图片存放的地址
            string strReveiveWebSiteUrl = strSchoolExtranet;
            //教案存放地址的数据接收页面
            string strTeachingPlanWebSitePath = string.Format("{0}/AuthApi/UploadHandler.ashx", strReveiveWebSiteUrl);

            string strSql = string.Empty;
            // string strDate = DateTime.Now.AddDays(-1).ToShortDateString();
            //教案(SW与class)BASE64内容 SW暂时使用此地址
            strSql = string.Format(@"select  ParticularYear,GradeTerm,Resource_Type,Resource_Version,Subject,t1.ResourceToResourceFolder_id
,t2.ResourceToResourceFolder_img_id  as FileName
from ResourceToResourceFolder t1 
inner join ResourceToResourceFolder_img t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
left join BookAudit ba on ba.ResourceFolder_Id=t1.Book_Id
where 1=1
and t1.Resource_Type in('{0}','{1}','{2}')
and convert(varchar(10),ba.CreateTime,120)='{3}'
and ba.AuditState='1'
and t1.Book_ID in(select distinct Book_id from [103.6.222.206].TestRe.dbo.UserBuyResources where UserId in(select distinct UserId from [103.6.222.206].TestRe.dbo.VW_UserOnClassGradeSchool where SchoolId='{4}'))
order by t2.Createtime desc", Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, Resource_TypeConst.class类型微课件, strDate, strSchool);
            Rc.Web.Payment.Core.LogHandler.WriteLogForPay("1111", strSql, "");
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
        public void ResSynchroTest(string strSchoolExtranet, string strDate, string strSchool)
        {

            //生产环境web站点存放文件的主目录
            string strWebSitePath = ConfigurationManager.AppSettings["WebSitePath"].ToString();
            //习题集存放的地址
            string strTeachingPlanWebSiteUrl = strSchoolExtranet;
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
left join BookAudit ba on ba.ResourceFolder_Id=t1.Book_Id
where 1=1
and t1.Resource_Type in('{0}')
and convert(varchar(10),ba.CreateTime,120)='{1}'
and ba.AuditState='1'
and t1.Book_ID in(select distinct Book_id from [103.6.222.206].TestRe.dbo.UserBuyResources where UserId in(select distinct UserId from [103.6.222.206].TestRe.dbo.VW_UserOnClassGradeSchool where SchoolId='{2}'))
order by t2.Createtime desc", Resource_TypeConst.testPaper类型文件, strDate, strSchool);
            Rc.Web.Payment.Core.LogHandler.WriteLogForPay("1111", strSql, "");
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

    }
}