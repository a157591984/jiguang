using Rc.Cloud.Web.Common;
using Rc.Common.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Newtonsoft.Json;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SyncFileSchool_TobeNew : System.Web.UI.Page
    {
        public string strLocalPhsicalPath = string.Empty;
        protected string SchoolId = string.Empty;
        protected string SysUser_ID = string.Empty;
        protected string BookId = string.Empty;
        protected string BookName = string.Empty;
        protected string Resource_TypeName = string.Empty;
        DateTime startTime = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            SchoolId = Request.QueryString["SchoolId"].Filter();
            BookId = Request.QueryString["BookId"].Filter();
            BookName = Request.QueryString["BookName"].Filter();
            Resource_TypeName = Request.QueryString["Resource_TypeName"].Filter();
            SysUser_ID = Request.QueryString["SysUser_ID"].Filter();
            strLocalPhsicalPath = Request.MapPath("\\Upload\\Resource\\");
            string strSql = string.Empty;
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='同步学校资源" + SchoolId + "' order by FileSyncExecRecord_TimeStart desc";
            string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
            if (str != "")
            {
                btnConfirm.Value = "文件正在同步中，同步开始时间为" + str;
                btnConfirm.Attributes.Add("disabled", "disabled");

                strSql = @"update FileSyncExecRecord set FileSyncExecRecord_Status='2',FileSyncExecRecord_Remark='已执行300分钟，超时，自动更新状态'
where FileSyncExecRecord_Type='同步学校资源" + SchoolId + "' and FileSyncExecRecord_Status='0' and DateDiff(MI,FileSyncExecRecord_TimeStart,getdate())>300 ";
                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql) > 0)
                {
                    Response.Redirect(Request.Url.ToString());
                }
            }
            #region 验证是否有可同步数据
            if (string.IsNullOrEmpty(BookId))
            {
                if (new BLL_SyncFileToSchoolDataDetail2().GetRecordCount("SchoolId='" + SchoolId + "' ") == 0)
                {
                    btnConfirm.Attributes.Add("disabled", "disabled");
                    btnConfirm.Attributes.Add("title", "暂无可同步图书");
                }
            }
            else
            {
                if (new BLL_SyncFileToSchoolDataDetail2().GetRecordCount("SchoolId='" + SchoolId + "' and BookId='" + BookId + "' ") == 0)
                {
                    btnConfirm.Attributes.Add("disabled", "disabled");
                    btnConfirm.Attributes.Add("title", "暂无可同步资源");
                }
            }
            #endregion
            if (string.IsNullOrEmpty(BookId))
            {
                btnBack.Visible = false;
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string SchoolId, string BookId, string BookName, string Resource_TypeName, string IsNeed, int PageSize, int PageIndex)
        {
            try
            {
                bool isBook = true;//是否图书
                bool isCanExecing = true;//是否可执行
                string strSql = string.Empty;
                strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='同步学校资源" + SchoolId + "' order by FileSyncExecRecord_TimeStart desc";
                string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
                if (str != "")
                {
                    isCanExecing = false;
                }

                int rCount;
                SchoolId = SchoolId.Filter();
                BookId = BookId.Filter();

                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                strSql = string.Empty;
                string strWhere = string.Format(" SchoolId='{0}' ", SchoolId);
                if (string.IsNullOrEmpty(BookId))
                {
                    //dtRes = new BLL_SyncFileToSchool().GetListByPage(1, SchoolId, BookId, PageIndex, PageSize, out rCount).Tables[0];
                    #region 图书列表
                    BLL_SyncFileToSchoolDataDetail bll = new BLL_SyncFileToSchoolDataDetail();
                    if (IsNeed == "1")
                    {
                        strWhere += " and SyncStatus='0' ";
                    }
                    dtRes = bll.GetListByPage(strWhere, "SyncStatus,Resource_Type,BookName", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                    rCount = bll.GetRecordCount(strWhere);
                    foreach (DataRow item in dtRes.Rows)
                    {
                        listReturn.Add(new
                        {
                            Resource_Type = item["Resource_Type"].ToString(),
                            Resource_TypeName = item["Resource_TypeName"].ToString(),
                            BookId = item["BookId"].ToString(),
                            BookName = item["BookName"].ToString(),
                            ParentName = "",
                            rtrfId = "",
                            rtrfName = "",
                            CreateTime = pfunction.ConvertToLongDateTime(item["CreateTime"].ToString()),
                            SyncStatus = item["SyncStatus"].ToString(),
                            SyncStatusName = item["SyncStatus"].ToString() == "1" ? "已同步" : "待同步",
                            IsCanExecing = isCanExecing,
                            IsBook = isBook
                        });
                    }

                    if (dtRes.Rows.Count > 0)
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            err = "null",
                            PageIndex = PageIndex,
                            PageSize = PageSize,
                            TotalCount = rCount,
                            list = listReturn,
                            IsBook = isBook
                        });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            err = "暂无数据"
                        });
                    }

                    #endregion
                }
                else
                {
                    //dtRes = new BLL_SyncFileToSchool().GetListByPage(2, SchoolId, BookId, PageIndex, PageSize, out rCount).Tables[0];
                    #region 资源列表
                    isBook = false;
                    BLL_SyncFileToSchool bll = new BLL_SyncFileToSchool();
                    dtRes = bll.GetListByPage(SchoolId, BookId, IsNeed, PageIndex, PageSize, out rCount).Tables[0];
                    foreach (DataRow item in dtRes.Rows)
                    {
                        listReturn.Add(new
                        {
                            Resource_Type = item["Resource_Type"].ToString(),
                            Resource_TypeName = Resource_TypeName,
                            BookId = BookId,
                            BookName = BookName,
                            ParentName = ">>" + item["ResourceFolder_Name"].ToString(),
                            rtrfId = item["ResourceToResourceFolder_Id"].ToString(),
                            rtrfName = item["Resource_Name"].ToString(),
                            CreateTime = pfunction.ConvertToLongDateTime(item["CreateTime"].ToString()),
                            SyncStatus = string.IsNullOrEmpty(item["SyncFileToSchoolDataDetail2_Id"].ToString()) ? "1" : "0",
                            SyncStatusName = string.IsNullOrEmpty(item["SyncFileToSchoolDataDetail2_Id"].ToString()) ? "已同步" : "待同步",
                            IsCanExecing = isCanExecing,
                            IsBook = isBook
                        });
                    }

                    if (dtRes.Rows.Count > 0)
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            err = "null",
                            PageIndex = PageIndex,
                            PageSize = PageSize,
                            TotalCount = rCount,
                            list = listReturn,
                            IsBook = isBook
                        });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            err = "暂无数据"
                        });
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error" //ex.Message.ToString()
                });
            }
        }

        protected void btnExec_Click(object sender, EventArgs e)
        {

            btnConfirm.Value = "文件正在同步中，同步开始时间为" + DateTime.Now.ToShortDateString();
            btnConfirm.Attributes.Add("disabled", "disabled");

            Thread thread1 = new Thread(new ThreadStart(ResSynResource));
            if (!thread1.IsAlive)
            {
                thread1.Start();

            }
            Response.Redirect("SyncFileSchool_TobeNew.aspx?SchoolId=" + SchoolId + "&SysUser_ID=" + SysUser_ID);
        }

        public void ResSynResource()
        {
            string strFileSyncExecRecord_id = Guid.NewGuid().ToString();
            startTime = DateTime.Now;
            string strbookId = hid_bookId.Value.Filter();
            string strrtrfId = hid_rtrfId.Value.Filter();
            string strrType = hid_rType.Value.Filter();
            if (string.IsNullOrEmpty(strrType)) // 同步所有
            {
                ResSynResourceTestPaper(strFileSyncExecRecord_id, strbookId, strrtrfId);
                ResSynResourcePlan(strFileSyncExecRecord_id, strbookId, strrtrfId);
            }
            else if (strrType == Resource_TypeConst.testPaper类型文件)// 只同步习题集
            {
                ResSynResourceTestPaper(strFileSyncExecRecord_id, strbookId, strrtrfId);
            }
            else // 只同步教案
            {
                ResSynResourcePlan(strFileSyncExecRecord_id, strbookId, strrtrfId);
            }

            #region 执行完同步后系统自动更新一下待同步图书数量
            string strFileSyncExecRecord_id_exec = Guid.NewGuid().ToString();
            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id_exec;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "更新待同步图书数量";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "执行进行中...";
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.createUser = SysUser_ID;
            try
            {
                //记录同步之前的信息
                bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);

                Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec P_GenerateSyncFileToSchoolData '" + SysUser_ID + "' ", 3600);

                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "1";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "执行已完成";
                //记录同步完成后的信息
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);

            }
            catch (Exception ex)
            {
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "2";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "执行失败：" + ex.Message.ToString();
                //记录同步完成后的信息
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
            }
            #endregion

        }

        /// <summary>
        /// 试卷题干
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFileTestPaperTestQuestion(string strbookId, string strrtrfId)
        {
            #region 读取需要同步的数据
            string strSql = string.Empty;
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(strbookId)) strWhere += " and t.BookId='" + strbookId + "' ";
            if (!string.IsNullOrEmpty(strrtrfId)) strWhere += " and t.ResourceToResourceFolder_Id='" + strrtrfId + "' ";

            strSql = string.Format(@"select distinct t1.Book_Id,t1.Resource_Type,t1.ParticularYear,t1.GradeTerm,t1.Resource_Version,t1.Subject,t1.ResourceToResourceFolder_id
,t2.TestQuestions_ID,t2.TestQuestions_ID as FileName ,t2.TestQuestions_Type,t2.Createtime
from SyncFileToSchoolDataDetail2 t 
inner join ResourceToResourceFolder t1 on t1.ResourceToResourceFolder_Id=t.ResourceToResourceFolder_Id 
inner join TestQuestions t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
where t.SchoolId='{0}' {1} order by t2.Createtime ", SchoolId, strWhere);

            DataTable dtFileTestPaper = new DataTable();

            dtFileTestPaper = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
            return dtFileTestPaper;
            #endregion
        }
        /// <summary>
        /// 试卷试题选项，标准答案，解析，强化训练
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFileTestPaperTestQuestions_Score(string strbookId, string strrtrfId)
        {
            #region 读取需要同步的数据
            string strSql = string.Empty;
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(strbookId)) strWhere += " and t.BookId='" + strbookId + "' ";
            if (!string.IsNullOrEmpty(strrtrfId)) strWhere += " and t.ResourceToResourceFolder_Id='" + strrtrfId + "' ";

            strSql = string.Format(@"select distinct t1.Book_Id,t1.Resource_Type,t1.ParticularYear,t1.GradeTerm,t1.Resource_Version,t1.Subject,t1.ResourceToResourceFolder_id
,t2.TestQuestions_ID,t3.TestQuestions_Score_ID as FileName,t2.TestQuestions_Type,t2.Createtime
from SyncFileToSchoolDataDetail2 t 
inner join ResourceToResourceFolder t1 on t1.ResourceToResourceFolder_Id=t.ResourceToResourceFolder_Id 
inner join TestQuestions t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
left join TestQuestions_Score t3 on t2.TestQuestions_Id=t3.TestQuestions_Id
where t2.TestQuestions_Type!='title' and t2.TestQuestions_Type!='' and t.SchoolId='{0}' {1} order by t2.Createtime ", SchoolId, strWhere);

            DataTable dtFileTestPaper = new DataTable();

            dtFileTestPaper = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
            return dtFileTestPaper;
            #endregion
        }

        /// <summary>
        /// 同步习题集
        /// </summary>
        public void ResSynResourceTestPaper(string strFileSyncExecRecord_id, string strbookId, string strrtrfId)
        {
            //生产环境web站点存放文件的主目录
            string strProductPublicUrl = ConfigurationManager.AppSettings["ProductPublicUrl"].ToString();
            string strCondition = string.Empty;
            bool isCover = true;//是否覆盖
            #region 记录同步开始信息

            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "同步学校资源" + SchoolId;
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步进行中...";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = startTime;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.FileSyncExecRecord_Condition = strCondition;
            model_FileSyncExecRecord.createUser = SysUser_ID;

            #endregion
            try
            {
                bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);

                //试卷文件相关数据
                DataTable dtFileTestPaper = GetDtFileTestPaperTestQuestion(strbookId, strrtrfId);
                //试题相关数据
                DataTable dtFileTestPaperScore = GetDtFileTestPaperTestQuestions_Score(strbookId, strrtrfId);

                DataView dataView = dtFileTestPaper.DefaultView;
                // 资源数据
                DataTable dtRTRF = new DataTable();
                if (dtFileTestPaper.Rows.Count > 0)
                {
                    dtRTRF = dataView.ToTable(true, "ResourceToResourceFolder_Id");
                }

                foreach (DataRow item in dtRTRF.Rows)
                {
                    #region 下载文件 试卷类型

                    //试卷文件相关数据
                    DataRow[] dvFileTestPaper = dtFileTestPaper.Select("ResourceToResourceFolder_id='" + item["ResourceToResourceFolder_id"] + "' ");
                    //题干	BASE64内容，存储目录：testQuestionBody/属性层次目录/试题标识.txt
                    DownLoadFile(dvFileTestPaper, "testQuestionBody", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "题干txt", isCover);
                    //题干 	HTML内容，存储目录：testQuestionBody/属性层次目录/试题标识.htm
                    DownLoadFile(dvFileTestPaper, "testQuestionBody", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "题干htm", isCover);

                    //题的选项等                    
                    DataRow[] dvFileTestPaperScore = dtFileTestPaperScore.Select("ResourceToResourceFolder_id='" + item["ResourceToResourceFolder_id"] + "' ");
                    //	选择题的选项（数组对象）存储目录：testQuestionOption/属性层次目录/试题标识.txt（存储JSION结构到文本文件，一个题只有一个文件）
                    DownLoadFile(dvFileTestPaperScore, "testQuestionOption", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "选择题选项txt", isCover);
                    //	c)	标准答案（数组）存储目录：testQuestionCurrent/属性层次目录/试题标识.txt（存储JSION结构到文本文件，一个题只有一个文件）
                    DownLoadFile(dvFileTestPaperScore, "testQuestionCurrent", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "标准答案txt", isCover);
                    //	解析 	BASE64内容AnalyzeData/属性层次目录/试题标识.txt
                    DownLoadFile(dvFileTestPaperScore, "AnalyzeData", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "解析txt", isCover);
                    //	解析 	HTML内容AnalyzeHtml/属性层次目录/试题标识.htm
                    DownLoadFile(dvFileTestPaperScore, "AnalyzeHtml", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "解析htm", isCover);
                    //	e)	强化训练 		BASE64内容TrainData/属性层次目录/试题标识.txt
                    DownLoadFile(dvFileTestPaperScore, "TrainData", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "强化训练txt", isCover);
                    //	e)	强化训练 		HTML内容TrainHtml/属性层次目录/试题标识.htm
                    DownLoadFile(dvFileTestPaperScore, "TrainHtml", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "强化训练htm", isCover);

                    #endregion

                    #region 成功 写入同步文件到学校记录表数据
                    string strSyncFileToSchool_Id = Guid.NewGuid().ToString();
                    string sql = string.Format(@" insert into SyncFileToSchool select '{0}','{1}','{2}','{3}','{4}',getdate() ; "
                        , strSyncFileToSchool_Id
                        , SchoolId
                        , item["ResourceToResourceFolder_id"].ToString()
                        , ""
                        , SysUser_ID);
                    int i = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql);
                    #endregion
                }
                #region 记录同步结束信息并保存数据
                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "1";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步已完成";
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
                #endregion
            }
            catch (Exception ex)
            {
                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "2";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步失败：" + ex.Message.ToString();
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = ex.Message.ToString();
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);

            }
        }


        /// <summary>
        /// 得到教案,htm文件的相关数据信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFilePlan(string strbookId, string strrtrfId)
        {
            #region 读取需要同步的数据
            string strSql = string.Empty;
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(strbookId)) strWhere += " and t.BookId='" + strbookId + "' ";
            if (!string.IsNullOrEmpty(strrtrfId)) strWhere += " and t.ResourceToResourceFolder_Id='" + strrtrfId + "' ";

            //教案(SW与class)BASE64内容 SW暂时使用此地址
            strSql = string.Format(@"select distinct t1.Book_Id,t1.ParticularYear,t1.GradeTerm,t1.Resource_Version,t1.Subject,t1.Resource_Type,t1.ResourceToResourceFolder_Id,t1.ResourceToResourceFolder_Id  as FileName
,t1.Createtime,null as TestQuestions_Id 
from SyncFileToSchoolDataDetail2 t 
inner join ResourceToResourceFolder t1 on t1.ResourceToResourceFolder_Id=t.ResourceToResourceFolder_Id 
where t.SchoolId='{0}' {1} order by t1.Createtime ", SchoolId, strWhere);
            DataTable dtFilePlan = new DataTable();

            dtFilePlan = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
            return dtFilePlan;
            #endregion
        }
        /// <summary>
        /// 得到教案预览图片文件的相关数据信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFilePlanView(string strbookId, string strrtrfId)
        {
            #region 读取需要同步的数据
            string strSql = string.Empty;
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(strbookId)) strWhere += " and t.BookId='" + strbookId + "' ";
            if (!string.IsNullOrEmpty(strrtrfId)) strWhere += " and t.ResourceToResourceFolder_Id='" + strrtrfId + "' ";

            //教案(SW与class)BASE64内容 SW暂时使用此地址
            strSql = string.Format(@"select distinct t1.Book_Id,t1.ParticularYear,t1.GradeTerm,t1.Resource_Type,t1.Resource_Version,t1.Subject,t1.ResourceToResourceFolder_id
,t2.ResourceToResourceFolder_img_id as FileName,t2.Createtime,t2.ResourceToResourceFolder_img_id as TestQuestions_Id
from SyncFileToSchoolDataDetail2 t 
inner join ResourceToResourceFolder t1 on t1.ResourceToResourceFolder_Id=t.ResourceToResourceFolder_Id 
inner join ResourceToResourceFolder_img t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
where t.SchoolId='{0}' {1} order by t2.Createtime ", SchoolId, strWhere);
            DataTable dtFilePlanView = new DataTable();

            dtFilePlanView = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
            return dtFilePlanView;
            #endregion
        }

        /// <summary>
        /// 同步教案
        /// </summary>
        public void ResSynResourcePlan(string strFileSyncExecRecord_id, string strbookId, string strrtrfId)
        {
            //生产环境web站点存放文件的主目录
            string strProductPublicUrl = ConfigurationManager.AppSettings["ProductPublicUrl"].ToString();
            string strCondition = string.Empty;
            bool isCover = true;//是否覆盖
            #region 记录同步开始信息

            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "同步学校资源" + SchoolId;
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步进行中...";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = startTime;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.FileSyncExecRecord_Condition = strCondition;
            model_FileSyncExecRecord.createUser = SysUser_ID;

            #endregion
            try
            {
                if (bll_FileSyncExecRecord.GetRecordCount("FileSyncExecRecord_id='" + strFileSyncExecRecord_id + "'") > 0)
                {
                    bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
                }
                else
                {
                    bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);
                }

                //教案文件，htm文件相关数据
                DataTable dtFilePlan = GetDtFilePlan(strbookId, strrtrfId);
                //教案预览图片文件相关数据
                DataTable dtFilePlanView = GetDtFilePlanView(strbookId, strrtrfId);

                DataView dataView = dtFilePlan.DefaultView;
                // 资源数据
                DataTable dtRTRF = new DataTable();
                if (dtFilePlan.Rows.Count > 0)
                {
                    dtRTRF = dataView.ToTable(true, "ResourceToResourceFolder_Id");
                }

                foreach (DataRow item in dtRTRF.Rows)
                {
                    #region 教案件下载

                    //教案SW+thm
                    DataRow[] drSWdsc = dtFilePlan.Select(string.Format("ResourceToResourceFolder_id='{0}' and Resource_Type='{1}'"
                        , item["ResourceToResourceFolder_id"]
                        , Resource_TypeConst.ScienceWord类型文件));
                    DownLoadFile(drSWdsc, "swDocument", "dsc", strProductPublicUrl, strFileSyncExecRecord_id, "教案SWdsc", isCover);
                    DownLoadFile(drSWdsc, "swDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "教案SWdsc", isCover);
                    //教案class+thm
                    DataRow[] drClass = dtFilePlan.Select(string.Format("ResourceToResourceFolder_id='{0}' and Resource_Type='{1}'"
                        , item["ResourceToResourceFolder_id"]
                        , Resource_TypeConst.class类型文件));
                    DownLoadFile(drClass, "classDocument", "class", strProductPublicUrl, strFileSyncExecRecord_id, "教案class", isCover);
                    DownLoadFile(drClass, "classDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "教案class", isCover);
                    //微课class+thm
                    DataRow[] drMicroClass = dtFilePlan.Select(string.Format("ResourceToResourceFolder_id='{0}' and Resource_Type='{1}'"
                        , item["ResourceToResourceFolder_id"]
                        , Resource_TypeConst.class类型微课件));
                    DownLoadFile(drMicroClass, "microClassDocument", "class", strProductPublicUrl, strFileSyncExecRecord_id, "微课class", isCover);
                    DownLoadFile(drMicroClass, "microClassDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "微课class", isCover);

                    DataRow[] drSWdscView = dtFilePlanView.Select(string.Format("ResourceToResourceFolder_id='{0}' and Resource_Type='{1}'"
                        , item["ResourceToResourceFolder_id"]
                        , Resource_TypeConst.ScienceWord类型文件));
                    DownLoadFile(drSWdscView, "swView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "教案SW预览图片", isCover);
                    //教案Class+预览图片
                    DataRow[] drClassView = dtFilePlanView.Select(string.Format("ResourceToResourceFolder_id='{0}' and Resource_Type='{1}'"
                        , item["ResourceToResourceFolder_id"]
                        , Resource_TypeConst.class类型文件));
                    DownLoadFile(drClassView, "classView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "教案Class预览图片", isCover);
                    //微课Class+预览图片
                    DataRow[] drMicroClassView = dtFilePlanView.Select(string.Format("ResourceToResourceFolder_id='{0}' and Resource_Type='{1}'"
                        , item["ResourceToResourceFolder_id"]
                        , Resource_TypeConst.class类型微课件));
                    DownLoadFile(drMicroClassView, "microClassView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "微课Class预览图片", isCover);
                    #endregion

                    #region 成功 写入同步文件到学校记录表数据
                    string strSyncFileToSchool_Id = Guid.NewGuid().ToString();
                    string sql = string.Format(@" insert into SyncFileToSchool select '{0}','{1}','{2}','{3}','{4}',getdate() ; "
                        , strSyncFileToSchool_Id
                        , SchoolId
                        , item["ResourceToResourceFolder_id"].ToString()
                        , ""
                        , SysUser_ID);
                    int i = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql);
                    #endregion
                }
                #region 记录同步结束信息并保存数据
                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "1";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步已完成";
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
                #endregion
            }
            catch (Exception ex)
            {
                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "2";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步失败：" + ex.Message.ToString();
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = ex.Message.ToString();
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);

            }
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="dvFile"></param>
        /// <param name="strPathType"></param>
        /// <param name="strFileNameEx"></param>
        /// <param name="strProductPublicUrl"></param>
        /// <param name="strFileSyncExecRecord_id"></param>
        /// <param name="strRemark"></param>
        /// <param name="isCoverDownload">true: 覆盖下载</param>
        private void DownLoadFile(DataRow[] dvFile, string strPathType, string strFileNameEx, string strProductPublicUrl, string strFileSyncExecRecord_id, string strRemark, bool isCoverDownload)
        {
            bool isDownload = false;//是否要下载文件
            foreach (var item in dvFile)
            {
                #region 组织文件源的URL与本地存储的路径
                string strPathCommon = string.Empty;
                string strFileName = string.Empty;//文件名（没后缀）
                strFileName = item["FileName"].ToString();
                strPathCommon = string.Format("{0}/{1}/{2}/{3}/{4}/"
                    , strPathType
                    , item["ParticularYear"].ToString()
                    , item["GradeTerm"].ToString()
                    , item["Resource_Version"].ToString()
                    , item["Subject"].ToString());
                string strFilePath = string.Empty;
                string strFileNameFullDocument = strFileName + "." + strFileNameEx;
                string remotingFileUrl = string.Empty;
                string localFilePath = string.Empty;
                remotingFileUrl = strProductPublicUrl + "Upload/Resource/" + strPathCommon;
                localFilePath = strLocalPhsicalPath + strPathCommon;
                string remotingViewFileUrl = string.Empty;
                string localViewFilePath = string.Empty;
                #endregion
                isDownload = false;
                if (!File.Exists(localFilePath + strFileNameFullDocument))//文件不存在
                {
                    isDownload = true;
                }
                else
                {
                    if (isCoverDownload)//是覆盖下载
                    {
                        isDownload = true;
                    }
                }
                if (isDownload)
                {
                    Rc.Model.Resources.Model_FileSyncExecRecordDetail model_FileSyncExecRecordDetail = new Rc.Model.Resources.Model_FileSyncExecRecordDetail();
                    Rc.BLL.Resources.BLL_FileSyncExecRecordDetail bll_FileSyncExecRecordDetail = new Rc.BLL.Resources.BLL_FileSyncExecRecordDetail();
                    model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id = Guid.NewGuid().ToString();
                    model_FileSyncExecRecordDetail.FileSyncExecRecord_id = strFileSyncExecRecord_id;
                    model_FileSyncExecRecordDetail.Book_Id = item["Book_Id"].ToString();
                    model_FileSyncExecRecordDetail.ResourceToResourceFolder_Id = item["ResourceToResourceFolder_Id"].ToString();
                    model_FileSyncExecRecordDetail.TestQuestions_Id = item["TestQuestions_Id"].ToString();
                    model_FileSyncExecRecordDetail.Resource_Type = item["Resource_Type"].ToString();
                    model_FileSyncExecRecordDetail.FileUrl = strPathCommon + strFileNameFullDocument;
                    model_FileSyncExecRecordDetail.Detail_Remark = "开始同步：" + strRemark;
                    model_FileSyncExecRecordDetail.Detail_Status = "0";
                    model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
                    bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

                    try
                    {
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
                    catch (Exception ex)
                    {
                        //同步异常 删除已创建文件 17-09-11TS
                        if (File.Exists(localFilePath + strFileNameFullDocument)) File.Delete(localFilePath + strFileNameFullDocument);

                        model_FileSyncExecRecordDetail.Detail_Remark = "同步失败，异常：" + ex.Message.ToString();
                        model_FileSyncExecRecordDetail.Detail_Status = "2";
                        bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);

                        throw;
                    }

                }
            }
        }

    }
}