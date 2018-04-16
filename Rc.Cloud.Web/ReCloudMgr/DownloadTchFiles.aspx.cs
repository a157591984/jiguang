using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Common.Config;
using System.IO;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using System.Web.Services;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class DownloadTchFiles : System.Web.UI.Page
    {
        protected string sourceServerUrl = string.Empty;
        protected string targetServerUrl = string.Empty;
        protected string SchoolId = string.Empty;
        string strLocalPhsicalPath = string.Empty;
        string SysUser_ID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            sourceServerUrl = Uri.UnescapeDataString(Request.QueryString["sourceServerUrl"].Filter());
            targetServerUrl = Uri.UnescapeDataString(Request.QueryString["targetServerUrl"].Filter());
            SchoolId = Request.QueryString["SchoolId"].Filter();
            SysUser_ID = Request.QueryString["SysUser_ID"].Filter();
            strLocalPhsicalPath = Request.MapPath("\\Upload\\Resource\\");
            string strSql = string.Empty;
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status ='0' and FileSyncExecRecord_Type='同步老师自有文件" + SchoolId + "' order by FileSyncExecRecord_TimeStart desc";
            string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
            if (!string.IsNullOrEmpty(str))
            {
                btn.Text = "文件正在同步中，同步开始时间为" + str;
                btn.Enabled = false;
            }

        }

        protected void btn_Click(object sender, EventArgs e)
        {

            btn.Text = "文件正在同步中，同步开始时间为" + DateTime.Now.ToShortDateString();
            btn.Enabled = false;

            Thread thread1 = new Thread(new ThreadStart(ResSynchroFile));
            if (!thread1.IsAlive)
            {
                thread1.Start();

            }
            Response.Redirect(Request.Url.ToString());
        }

        /// <summary>
        /// 同步老师自有文件
        /// </summary>
        public void ResSynchroFile()
        {
            //运营平台web站点存放文件的主目录
            string strProductPublicUrl = sourceServerUrl;
            string strFileSyncExecRecord_id = Guid.NewGuid().ToString();
            string strSchoolName = string.Empty;
            if (!string.IsNullOrEmpty(SchoolId))
            {
                Rc.Model.Resources.Model_UserGroup modelUG = new Rc.BLL.Resources.BLL_UserGroup().GetModel(SchoolId);
                if (modelUG != null)
                {
                    strSchoolName = string.Format("，学校名称：{0}", modelUG.UserGroup_Name);
                }
            }
            string strCondition = string.Format("学校标识：{0}{1}，来源服务器：{2}，目标服务器：{3}", SchoolId, strSchoolName, sourceServerUrl, targetServerUrl);
            #region 记录同步开始信息
            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "同步老师自有文件" + SchoolId;
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.createUser = SysUser_ID;
            model_FileSyncExecRecord.FileSyncExecRecord_Condition = strCondition;
            bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);
            #endregion

            try
            {
                #region 下载文件
                //教案文件，htm文件相关数据
                DataTable dtFilePlan = GetDtFilePlan();
                //教案SW+thm
                DataRow[] drSWdsc = dtFilePlan.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.ScienceWord类型文件));
                DownLoadFileTeachingPlan(drSWdsc, "swDocument", "dsc", strProductPublicUrl, strFileSyncExecRecord_id, "教案SWdsc");
                DownLoadFileTeachingPlan(drSWdsc, "swDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "教案SWdsc");
                //教案class+thm
                DataRow[] drClass = dtFilePlan.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型文件));
                DownLoadFileTeachingPlan(drClass, "classDocument", "class", strProductPublicUrl, strFileSyncExecRecord_id, "教案class");
                DownLoadFileTeachingPlan(drClass, "classDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "教案class");
                //微课class+thm
                DataRow[] drMicroClass = dtFilePlan.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型微课件));
                DownLoadFileTeachingPlan(drMicroClass, "microClassDocument", "class", strProductPublicUrl, strFileSyncExecRecord_id, "微课class");
                DownLoadFileTeachingPlan(drMicroClass, "microClassDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "微课class");

                //教案预览图片文件相关数据
                DataTable dtFilePlanView = GetDtFilePlanView();
                //教案SW+预览图片
                DataRow[] drSWdscView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.ScienceWord类型文件));
                DownLoadFileTeachingPlan(drSWdscView, "swView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "教案SW预览图片");
                //教案Class+预览图片
                DataRow[] drClassView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型文件));
                DownLoadFileTeachingPlan(drClassView, "classView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "教案Class预览图片");
                //微课Class+预览图片
                DataRow[] drMicroClassView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型微课件));
                DownLoadFileTeachingPlan(drMicroClassView, "microClassView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "微课Class预览图片");
                #endregion

                #region 下载文件
                //试卷文件相关数据
                DataTable dtFileTestPaper = GetDtFileTestPaperTestQuestion();
                DataRow[] dvFileTestPaper = dtFileTestPaper.Select();
                //题干	BASE64内容，存储目录：testQuestionBody/属性层次目录/试题标识.txt
                DownLoadFileTestpaper(dvFileTestPaper, "testQuestionBody", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "题干txt");
                //题干 	HTML内容，存储目录：testQuestionBody/属性层次目录/试题标识.htm
                DownLoadFileTestpaper(dvFileTestPaper, "testQuestionBody", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "题干htm");

                DataTable dtFileTestPaperScore = GetDtFileTestPaperTestQuestions_Score();
                DataRow[] dvFileTestPaperScore = dtFileTestPaperScore.Select();
                //	选择题的选项（数组对象）存储目录：testQuestionOption/属性层次目录/试题标识.txt（存储JSION结构到文本文件，一个题只有一个文件）
                DownLoadFileTestpaper(dvFileTestPaperScore, "testQuestionOption", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "选择题选项txt");
                //	c)	标准答案（数组）存储目录：testQuestionCurrent/属性层次目录/试题标识.txt（存储JSION结构到文本文件，一个题只有一个文件）
                DownLoadFileTestpaper(dvFileTestPaperScore, "testQuestionCurrent", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "标准答案txt");
                //	解析 	BASE64内容AnalyzeData/属性层次目录/试题标识.txt
                DownLoadFileTestpaper(dvFileTestPaperScore, "AnalyzeData", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "解析txt");
                //	解析 	HTML内容AnalyzeHtml/属性层次目录/试题标识.htm
                DownLoadFileTestpaper(dvFileTestPaperScore, "AnalyzeHtml", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "解析htm");
                //	e)	强化训练 		BASE64内容TrainData/属性层次目录/试题标识.txt
                DownLoadFileTestpaper(dvFileTestPaperScore, "TrainData", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "强化训练txt");
                //	e)	强化训练 		HTML内容TrainHtml/属性层次目录/试题标识.htm
                DownLoadFileTestpaper(dvFileTestPaperScore, "TrainHtml", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "强化训练htm");
                #endregion

                #region 记录同步结束信息并保存数据
                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "1";

                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
                #endregion
            }
            catch (Exception ex)
            {
                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "2";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步失败：" + ex.Message.ToString();
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);

            }
        }
        /// <summary>
        /// 得到教案,htm文件的相关数据信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFilePlan()
        {
            #region 读取数据
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(SchoolId))
            {
                strWhere += string.Format(" and t1.CreateFUser in(select UserId from dbo.VW_UserOnClassGradeSchool where ClassMemberShipEnum<>'student' and SchoolId='{0}') ", SchoolId);
            }
            else
            {
                strWhere += " and t1.CreateFUser in(select UserId from dbo.VW_UserOnClassGradeSchool vw left join ConfigSchool t on t.School_Id=vw.SchoolId where  vw.ClassMemberShipEnum<>'student' and t.School_Id is null ) ";
            }
            string strSql = string.Empty;
            //教案(SW与class)BASE64内容 SW暂时使用此地址
            strSql = string.Format(@"select distinct t1.Book_Id, ParticularYear,GradeTerm,Resource_Version,Subject,Resource_Type,ResourceToResourceFolder_Id,ResourceToResourceFolder_Id  as FileName,t1.Createtime
,t1.Resource_Url as url 
from ResourceToResourceFolder t1 
where t1.Resource_Type in('{0}','{1}','{2}') and t1.Resource_Class='{3}' 
{4} order by t1.Createtime desc"
                , Resource_TypeConst.ScienceWord类型文件
                , Resource_TypeConst.class类型文件
                , Resource_TypeConst.class类型微课件
                , Resource_ClassConst.自有资源
                , strWhere);
            DataTable dtFilePlan = new DataTable();

            dtFilePlan = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            return dtFilePlan;
            #endregion
        }
        /// <summary>
        /// 得到教案预览图片文件的相关数据信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFilePlanView()
        {
            #region 读取数据
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(SchoolId))
            {
                strWhere += string.Format(" and t1.CreateFUser in(select UserId from dbo.VW_UserOnClassGradeSchool where ClassMemberShipEnum<>'student' and SchoolId='{0}') ", SchoolId);
            }
            else
            {
                strWhere += " and t1.CreateFUser in(select UserId from dbo.VW_UserOnClassGradeSchool vw left join ConfigSchool t on t.School_Id=vw.SchoolId where  vw.ClassMemberShipEnum<>'student' and t.School_Id is null ) ";
            }
            string strSql = string.Empty;
            //教案(SW与class)BASE64内容 SW暂时使用此地址
            strSql = string.Format(@"select distinct t1.Book_Id,ParticularYear,GradeTerm,Resource_Type,Resource_Version,Subject,t1.ResourceToResourceFolder_id
,t2.ResourceToResourceFolder_img_id  as FileName,t1.Createtime,t2.ResourceToResourceFolderImg_Url as url  
from ResourceToResourceFolder t1 
inner join ResourceToResourceFolder_img t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id 
where t1.Resource_Type in('{0}','{1}','{2}') and t1.Resource_Class='{3}' 
{4} order by t1.Createtime desc"
                , Resource_TypeConst.ScienceWord类型文件
                , Resource_TypeConst.class类型文件
                , Resource_TypeConst.class类型微课件
                , Resource_ClassConst.自有资源
                , strWhere);
            DataTable dtFilePlanView = new DataTable();

            dtFilePlanView = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            return dtFilePlanView;
            #endregion
        }

        /// <summary>
        /// 下载
        /// </summary>
        private void DownLoadFileTeachingPlan(DataRow[] dvFile, string strPathType, string strFileNameEx, string strProductPublicUrl, string strFileSyncExecRecord_id, string strRemark)
        {
            foreach (var item in dvFile)
            {
                #region 组织文件源的URL与本地存储的路径
                string strPathCommon = string.Empty;
                string strFileName = string.Empty;//文件名（没后缀）
                strFileName = item["FileName"].ToString();
                strPathCommon = item["url"].ToString();// string.Format("{0}/", strPathType);
                if (strPathCommon.Length > 1) strPathCommon = strPathCommon.Substring(0, strPathCommon.LastIndexOf("\\") + 1);
                string strFilePath = string.Empty;
                string strFileNameFullDocument = strFileName + "." + strFileNameEx;
                string remotingFileUrl = string.Empty;
                string localFilePath = string.Empty;
                remotingFileUrl = strProductPublicUrl + "Upload/Resource/" + strPathCommon;
                localFilePath = strLocalPhsicalPath + strPathCommon;
                string remotingViewFileUrl = string.Empty;
                string localViewFilePath = string.Empty;
                #endregion

                Rc.Model.Resources.Model_FileSyncExecRecordDetail model_FileSyncExecRecordDetail = new Rc.Model.Resources.Model_FileSyncExecRecordDetail();
                Rc.BLL.Resources.BLL_FileSyncExecRecordDetail bll_FileSyncExecRecordDetail = new Rc.BLL.Resources.BLL_FileSyncExecRecordDetail();
                model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id = Guid.NewGuid().ToString();
                model_FileSyncExecRecordDetail.FileSyncExecRecord_id = strFileSyncExecRecord_id;
                model_FileSyncExecRecordDetail.Book_Id = item["Book_Id"].ToString();
                model_FileSyncExecRecordDetail.ResourceToResourceFolder_Id = item["ResourceToResourceFolder_Id"].ToString();
                model_FileSyncExecRecordDetail.Resource_Type = item["Resource_Type"].ToString();
                model_FileSyncExecRecordDetail.FileUrl = strPathCommon + strFileNameFullDocument;
                model_FileSyncExecRecordDetail.Detail_Remark = strRemark;
                model_FileSyncExecRecordDetail.Detail_Status = "0";
                model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
                bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

                string downloadFileMsg = pfunction.DownLoadFileByWebClient(remotingFileUrl, localFilePath, strFileNameFullDocument);

                model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
                if (downloadFileMsg != "true")
                {
                    model_FileSyncExecRecordDetail.Detail_Status = "2";
                    if (downloadFileMsg.Contains("404"))
                    {
                        model_FileSyncExecRecordDetail.Detail_Status = "1";//如果是远程服务器未找到文件，设置为状态为成功
                        // 17-08-21如果是远程服务器未找到文件，删除同步记录
                        bll_FileSyncExecRecordDetail.Delete(model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id);
                    }
                    else
                    {
                        model_FileSyncExecRecordDetail.Detail_Remark = strRemark + ":" + downloadFileMsg;
                        bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
                    }
                }
                else
                {
                    model_FileSyncExecRecordDetail.Detail_Remark = "同步成功：" + strRemark;
                    if (File.Exists(localFilePath + strFileNameFullDocument))
                    {
                        model_FileSyncExecRecordDetail.Detail_Status = "1";
                    }
                    bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
                }

            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        private void DownLoadFileTestpaper(DataRow[] dvFile, string strPathType, string strFileNameEx, string strProductPublicUrl, string strFileSyncExecRecord_id, string strRemark)
        {
            foreach (var item in dvFile)
            {
                #region 组织文件源的URL与本地存储的路径
                string strPathCommon = string.Empty;
                string strFileName = string.Empty;//文件名（没后缀）
                strFileName = item["FileName"].ToString();

                strPathCommon = string.Format("{0}/{1}/"
                   , pfunction.ToShortDate(item["Createtime"].ToString())
                   , strPathType);
                string strFilePath = string.Empty;
                string strFileNameFullDocument = strFileName + "." + strFileNameEx;
                string remotingFileUrl = string.Empty;
                string localFilePath = string.Empty;
                remotingFileUrl = strProductPublicUrl + "Upload/Resource/" + strPathCommon;
                localFilePath = strLocalPhsicalPath + strPathCommon;
                string remotingViewFileUrl = string.Empty;
                string localViewFilePath = string.Empty;
                #endregion
                Rc.Model.Resources.Model_FileSyncExecRecordDetail model_FileSyncExecRecordDetail = new Rc.Model.Resources.Model_FileSyncExecRecordDetail();
                Rc.BLL.Resources.BLL_FileSyncExecRecordDetail bll_FileSyncExecRecordDetail = new Rc.BLL.Resources.BLL_FileSyncExecRecordDetail();
                model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id = Guid.NewGuid().ToString();
                model_FileSyncExecRecordDetail.FileSyncExecRecord_id = strFileSyncExecRecord_id;
                model_FileSyncExecRecordDetail.Book_Id = item["Book_Id"].ToString();
                model_FileSyncExecRecordDetail.ResourceToResourceFolder_Id = item["ResourceToResourceFolder_Id"].ToString();
                model_FileSyncExecRecordDetail.TestQuestions_Id = item["TestQuestions_Id"].ToString();
                model_FileSyncExecRecordDetail.Resource_Type = item["Resource_Type"].ToString();
                model_FileSyncExecRecordDetail.FileUrl = strPathCommon + strFileNameFullDocument;
                model_FileSyncExecRecordDetail.Detail_Remark = strRemark;
                model_FileSyncExecRecordDetail.Detail_Status = "0";
                model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
                bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

                string downloadFileMsg = pfunction.DownLoadFileByWebClient(remotingFileUrl, localFilePath, strFileNameFullDocument);

                model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
                if (downloadFileMsg != "true")
                {
                    model_FileSyncExecRecordDetail.Detail_Status = "2";
                    if (downloadFileMsg.Contains("404"))
                    {
                        model_FileSyncExecRecordDetail.Detail_Status = "1";//如果是远程服务器未找到文件，设置为状态为成功
                        // 17-08-21如果是远程服务器未找到文件，删除同步记录
                        bll_FileSyncExecRecordDetail.Delete(model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id);
                    }
                    else
                    {
                        model_FileSyncExecRecordDetail.Detail_Remark = strRemark + ":" + downloadFileMsg;
                        bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
                    }
                }
                else
                {
                    model_FileSyncExecRecordDetail.Detail_Remark = "同步成功：" + strRemark;
                    if (File.Exists(localFilePath + strFileNameFullDocument))
                    {
                        model_FileSyncExecRecordDetail.Detail_Status = "1";
                    }
                    bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
                }
            }
        }

        /// <summary>
        /// 试卷题干
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFileTestPaperTestQuestion()
        {
            #region 读取数据
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(SchoolId))
            {
                strWhere += string.Format(" and t1.CreateFUser in(select UserId from dbo.VW_UserOnClassGradeSchool where ClassMemberShipEnum<>'student' and SchoolId='{0}') ", SchoolId);
            }
            else
            {
                strWhere += " and t1.CreateFUser in(select UserId from dbo.VW_UserOnClassGradeSchool vw left join ConfigSchool t on t.School_Id=vw.SchoolId where  vw.ClassMemberShipEnum<>'student' and t.School_Id is null ) ";
            }
            string strSql = string.Empty;
            strSql = string.Format(@"select distinct t1.Book_Id,Resource_Type, ParticularYear,GradeTerm,Resource_Version,Subject,t1.ResourceToResourceFolder_id,t1.Createtime
,t2.TestQuestions_ID,t2.TestQuestions_ID as FileName ,t2.TestQuestions_Type
from ResourceToResourceFolder t1 
inner join TestQuestions t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
where t1.Resource_Type='{0}' and t1.Resource_Class='{1}' 
{2} order by t1.Createtime desc ", Resource_TypeConst.testPaper类型文件, Resource_ClassConst.自有资源, strWhere);

            DataTable dtFileTestPaper = new DataTable();

            dtFileTestPaper = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            return dtFileTestPaper;
            #endregion
        }
        /// <summary>
        /// 试卷试题选项，标准答案，解析，强化训练
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFileTestPaperTestQuestions_Score()
        {
            #region 读取数据
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(SchoolId))
            {
                strWhere += string.Format(" and t1.CreateFUser in(select UserId from dbo.VW_UserOnClassGradeSchool where ClassMemberShipEnum<>'student' and SchoolId='{0}') ", SchoolId);
            }
            else
            {
                strWhere += " and t1.CreateFUser in(select UserId from dbo.VW_UserOnClassGradeSchool vw left join ConfigSchool t on t.School_Id=vw.SchoolId where  vw.ClassMemberShipEnum<>'student' and t.School_Id is null ) ";
            }
            string strSql = string.Empty;
            strSql = string.Format(@"select distinct t1.Book_Id,Resource_Type, ParticularYear,GradeTerm,Resource_Version,Subject,t1.ResourceToResourceFolder_id,t1.Createtime
,t2.TestQuestions_ID,t3.TestQuestions_Score_ID as FileName,t2.TestQuestions_Type
from ResourceToResourceFolder t1 
inner join TestQuestions t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
left join TestQuestions_Score t3 on t2.TestQuestions_Id=t3.TestQuestions_Id
where t1.Resource_Type='{0}' and t1.Resource_Class='{1}' 
{2} order by t1.Createtime desc", Resource_TypeConst.testPaper类型文件, Resource_ClassConst.自有资源, strWhere);

            DataTable dtFileTestPaper = new DataTable();

            dtFileTestPaper = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            return dtFileTestPaper;
            #endregion
        }


        [WebMethod]
        public static string GetDataList(string SchoolId, string strFileSyncExecRecord_Status, int PageSize, int PageIndex)
        {
            try
            {
                strFileSyncExecRecord_Status = strFileSyncExecRecord_Status.Filter();

                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;

                if (strFileSyncExecRecord_Status == "0") strWhere += " and f.Detail_Status='0' ";
                if (strFileSyncExecRecord_Status == "1") strWhere += " and f.Detail_Status='1' ";
                if (strFileSyncExecRecord_Status == "2") strWhere += " and f.Detail_Status='2' ";


                strWhere += " and f.FileSyncExecRecord_Type='同步老师自有文件" + SchoolId + "' ";
                strSqlCount = @"select count(*) from FileSyncExecRecord f where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY f.FileSyncExecRecord_TimeStart desc) row,f.*,u.SysUser_Name from FileSyncExecRecord f
 left join SysUser u on f.createUser=u.SysUser_ID where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string strIType = string.Empty;
                    string strDetail_StatusTemp = string.Empty;
                    if (dtRes.Rows[i]["FileSyncExecRecord_Status"].ToString() == "1")
                    {
                        strDetail_StatusTemp = "成功";
                    }
                    else if (dtRes.Rows[i]["FileSyncExecRecord_Status"].ToString() == "2")
                    {
                        strDetail_StatusTemp = "失败";
                    }
                    else
                    {
                        strDetail_StatusTemp = "进行中";
                    }
                    double execHours = 0;
                    DateTime d1 = new DateTime();
                    if (!string.IsNullOrEmpty(dtRes.Rows[i]["FileSyncExecRecord_TimeStart"].ToString()))
                    {
                        d1 = Convert.ToDateTime(dtRes.Rows[i]["FileSyncExecRecord_TimeStart"].ToString());
                    }
                    DateTime d2 = DateTime.Now;
                    TimeSpan d3 = d2 - d1;
                    execHours = d3.TotalHours;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        FileSyncExecRecord_id = dtRes.Rows[i]["FileSyncExecRecord_id"].ToString(),
                        FileSyncExecRecord_Type = dtRes.Rows[i]["FileSyncExecRecord_Type"].ToString(),
                        FileSyncExecRecord_TimeStart = dtRes.Rows[i]["FileSyncExecRecord_TimeStart"].ToString(),
                        FileSyncExecRecord_TimeEnd = dtRes.Rows[i]["FileSyncExecRecord_TimeEnd"].ToString(),
                        FileSyncExecRecord_Long = pfunction.ExecDateDiff(dtRes.Rows[i]["FileSyncExecRecord_TimeStart"].ToString(), dtRes.Rows[i]["FileSyncExecRecord_TimeEnd"].ToString()),
                        FileSyncExecRecord_Status = strDetail_StatusTemp,
                        FileSyncExecRecord_Condition = dtRes.Rows[i]["FileSyncExecRecord_Condition"].ToString(),
                        createUser = dtRes.Rows[i]["createUser"].ToString(),
                        FileSyncExecRecord_Remark = dtRes.Rows[i]["FileSyncExecRecord_Remark"].ToString(),
                        SysUser_Name = dtRes.Rows[i]["SysUser_Name"].ToString(),
                        ExecHours = execHours
                    });
                }
                #endregion

                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = rCount,
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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }

    }
}