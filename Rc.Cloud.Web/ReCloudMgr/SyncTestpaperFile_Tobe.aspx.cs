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
using Rc.Cloud.Web.Common;
using System.Web.Services;
using Newtonsoft.Json;
using Rc.Model.Resources;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SyncTestpaperFile_Tobe : System.Web.UI.Page
    {
        string strLocalPhsicalPath = string.Empty;
        protected string SysUser_ID = string.Empty;
        protected string bookId = string.Empty;
        protected string isCanExecing = "1";//是否可执行
        protected void Page_Load(object sender, EventArgs e)
        {
            bookId = Request.QueryString["bookId"].Filter();
            SysUser_ID = Request.QueryString["SysUser_ID"].Filter();
            strLocalPhsicalPath = Request.MapPath("\\Upload\\Resource\\");
            string strSql = string.Empty;
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='同步试卷new' order by FileSyncExecRecord_TimeStart desc";
            string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
            if (str != "")
            {
                isCanExecing = "0";
                //btnTestpaper.Text = "试卷文件正在同步中，同步开始时间为" + str;
                //btnTestpaper.Enabled = false;
                btnConfirm.Value = "试卷文件正在同步中，同步开始时间为" + DateTime.Now.ToShortDateString();
                btnConfirm.Attributes.Add("disabled", "disabled");

                strSql = @"update FileSyncExecRecord set FileSyncExecRecord_Status='2',FileSyncExecRecord_Remark='已执行180分钟，超时，自动更新状态'
where FileSyncExecRecord_Type='同步试卷new' and FileSyncExecRecord_Status='0' and DateDiff(MI,FileSyncExecRecord_TimeStart,getdate())>180 ";
                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql) > 0)
                {
                    Response.Redirect(Request.Url.ToString());
                }
            }
            if (string.IsNullOrEmpty(bookId))
            {
                btnBack.Visible = false;
            }
        }

        protected void btnTestpaper_Click(object sender, EventArgs e)
        {

            //btnTestpaper.Text = "试卷文件正在同步中，同步开始时间为" + DateTime.Now.ToShortDateString();
            //btnTestpaper.Enabled = false;
            btnConfirm.Value = "试卷文件正在同步中，同步开始时间为" + DateTime.Now.ToShortDateString();
            btnConfirm.Attributes.Add("disabled", "disabled");

            Thread thread1 = new Thread(new ThreadStart(ResSynchroTeachingPlan));
            if (!thread1.IsAlive)
            {
                thread1.Start();

            }
            Response.Redirect("SyncTestpaperFile_Tobe.aspx?SysUser_ID=" + SysUser_ID);
        }

        /// <summary>
        /// 同步习题集
        /// </summary>
        public void ResSynchroTeachingPlan()
        {
            //生产环境web站点存放文件的主目录
            string strProductPublicUrl = ConfigurationManager.AppSettings["ProductPublicUrl"].ToString();
            string strFileSyncExecRecord_id = Guid.NewGuid().ToString();
            bool isCover = true;//是否覆盖
            string strCondition = string.Format("日期【】；是覆盖：【{0}】", isCover);
            string strbookId = hid_bookId.Value.Filter();
            string strrtrfId = hid_rtrfId.Value.Filter();
            if (!string.IsNullOrEmpty(strbookId))
            {
                Model_ResourceFolder modelRF = new BLL_ResourceFolder().GetModel(strbookId);
                if (modelRF != null) strCondition += string.Format("；书本【{0}】【{1}】", modelRF.ResourceFolder_Name, modelRF.ResourceFolder_Id);
            }
            if (!string.IsNullOrEmpty(strrtrfId))
            {
                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(strrtrfId);
                if (modelRTRF != null) strCondition += string.Format("；资源【{0}】【{1}】", modelRTRF.Resource_Name, modelRTRF.ResourceToResourceFolder_Id);
            }

            #region 记录同步开始信息

            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "同步试卷new";
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步进行中...";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.FileSyncExecRecord_Condition = strCondition;
            model_FileSyncExecRecord.createUser = SysUser_ID;
            bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);
            #endregion

            try
            {
                //试卷文件相关数据
                DataTable dtFileTestPaper = GetDtFileTestPaperTestQuestion(strbookId, strrtrfId);
                //试题相关数据
                DataTable dtFileTestPaperScore = GetDtFileTestPaperTestQuestions_Score(strbookId, strrtrfId);

                DataView dataView = dtFileTestPaper.DefaultView;
                // 资源数据
                DataTable dtRTRF = new DataTable();
                if (dtFileTestPaper.Rows.Count > 0)
                {
                    dtRTRF = dataView.ToTable(true, "Book_Id", "ResourceToResourceFolder_Id");
                }
                foreach (DataRow item in dtRTRF.Rows)
                {
                    #region 下载文件
                    DataRow[] dvFileTestPaper = dtFileTestPaper.Select(string.Format(" ResourceToResourceFolder_Id='{0}' ", item["ResourceToResourceFolder_Id"]));
                    //题干	BASE64内容，存储目录：testQuestionBody/属性层次目录/试题标识.txt
                    DownLoadFile(dvFileTestPaper, "testQuestionBody", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "题干txt", isCover);
                    //题干 	HTML内容，存储目录：testQuestionBody/属性层次目录/试题标识.htm
                    DownLoadFile(dvFileTestPaper, "testQuestionBody", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "题干htm", isCover);

                    DataRow[] dvFileTestPaperScore = dtFileTestPaperScore.Select(string.Format(" ResourceToResourceFolder_Id='{0}' ", item["ResourceToResourceFolder_Id"]));
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

                    #region 更新SyncData
                    string strSqlSyncData = string.Format(@"update SyncData set SyncStatus='3' where TableName='ResourceFolder' and  SyncStatus<>'3' and DataId='{0}';
                        update SyncData set SyncStatus='3' where TableName='ResourceToResourceFolder' and  SyncStatus<>'3' and DataId='{1}'; "
                        , item["Book_Id"], item["ResourceToResourceFolder_Id"]);
                    Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime(strSqlSyncData, 60);
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
                throw;
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
                    model_FileSyncExecRecordDetail.Detail_Remark = strRemark;
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
                            else //文件不存在，更新同步状态为失败 17-09-11TS
                            {
                                model_FileSyncExecRecordDetail.Detail_Status = "2";
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

        /// <summary>
        /// 试卷题干
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFileTestPaperTestQuestion(string strbookId, string strrtrfId)
        {
            #region 读取需要同步的数据
            string strSql = string.Empty;
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(strbookId)) strWhere += " and t1.Book_Id='" + strbookId + "' ";
            if (!string.IsNullOrEmpty(strrtrfId)) strWhere += " and t1.ResourceToResourceFolder_Id='" + strrtrfId + "' ";

            strSql = string.Format(@"select distinct t1.Book_Id,t1.Resource_Type,t1.ParticularYear,t1.GradeTerm,t1.Resource_Version,t1.Subject,t1.ResourceToResourceFolder_id
,t2.TestQuestions_ID,t2.TestQuestions_ID as FileName ,t2.TestQuestions_Type,t2.Createtime
from ResourceToResourceFolder t1 
inner join TestQuestions t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
left join BookAudit ba on ba.ResourceFolder_Id=t1.Book_Id
inner join SyncData sd on sd.OperateType in('add','modify') and sd.TableName='ResourceToResourceFolder' and sd.SyncStatus=1 and sd.DataId=t1.ResourceToResourceFolder_Id
where t1.Resource_Type='{0}' and t1.Resource_Class='{1}' and ba.AuditState='1' 
{2} 
order by t2.Createtime ", Resource_TypeConst.testPaper类型文件, Resource_ClassConst.云资源, strWhere);

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
            if (!string.IsNullOrEmpty(strbookId)) strWhere += " and t1.Book_Id='" + strbookId + "' ";
            if (!string.IsNullOrEmpty(strrtrfId)) strWhere += " and t1.ResourceToResourceFolder_Id='" + strrtrfId + "' ";

            strSql = string.Format(@"select distinct t1.Book_Id,t1.Resource_Type,t1.ParticularYear,t1.GradeTerm,t1.Resource_Version,t1.Subject,t1.ResourceToResourceFolder_id
,t2.TestQuestions_ID,t3.TestQuestions_Score_ID as FileName,t2.TestQuestions_Type,t2.Createtime
from ResourceToResourceFolder t1 
inner join TestQuestions t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
left join TestQuestions_Score t3 on t2.TestQuestions_Id=t3.TestQuestions_Id
left join BookAudit ba on ba.ResourceFolder_Id=t1.Book_Id
inner join SyncData sd on sd.OperateType in('add','modify') and sd.TableName='ResourceToResourceFolder' and sd.SyncStatus=1 and sd.DataId=t1.ResourceToResourceFolder_Id
where t1.Resource_Type='{0}' and t1.Resource_Class='{1}' 
and t2.TestQuestions_Type!='title' and t2.TestQuestions_Type!='' 
and ba.AuditState='1' 
{2} 
order by t2.Createtime ", Resource_TypeConst.testPaper类型文件, Resource_ClassConst.云资源, strWhere);

            DataTable dtFileTestPaper = new DataTable();

            dtFileTestPaper = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
            return dtFileTestPaper;
            #endregion
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string isCanExecing, string bookId, int PageSize, int PageIndex)
        {
            try
            {
                bookId = bookId.Filter();

                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                string isShowDetail = "0";
                if (string.IsNullOrEmpty(bookId))
                {
                    isShowDetail = "1";
                    #region 图书列表
                    strSql = string.Format(@"select * from (select ROW_NUMBER() over(ORDER BY CreateTime desc) row,* from (
select rf2.Book_Id as bookId,rf2.ResourceFolder_Name as bookName,min(t.CreateTime) as CreateTime
,null as rtrfId,null as rtrfName 
from SyncData t
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.DataId
inner join ResourceFolder rf2 on rf2.Book_Id=rtrf.Book_Id and rf2.ResourceFolder_Level=5
left join BookAudit ba on ba.ResourceFolder_Id=rtrf.Book_Id
where t.TableName='ResourceToResourceFolder' and t.OperateType in('add','modify') and t.SyncStatus=1 {0}
and rtrf.Resource_Type='{1}' and rtrf.Resource_Class='{2}' 
and ba.AuditState='1' 
group by rf2.Book_Id,rf2.ResourceFolder_Name
) t ) t where row between {3} and {4} "
                        , strWhere
                        , Resource_TypeConst.testPaper类型文件
                        , Resource_ClassConst.云资源
                        , ((PageIndex - 1) * PageSize + 1)
                        , (PageIndex * PageSize));

                    strSqlCount = string.Format(@"select count(1) as icount from (select rf2.Book_Id from SyncData t
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.DataId
inner join ResourceFolder rf2 on rf2.Book_Id=rtrf.Book_Id and rf2.ResourceFolder_Level=5
left join BookAudit ba on ba.ResourceFolder_Id=rtrf.Book_Id
where t.TableName='ResourceToResourceFolder' and t.OperateType in('add','modify') and t.SyncStatus=1 {0}
and rtrf.Resource_Type='{1}' and rtrf.Resource_Class='{2}' 
and ba.AuditState='1' group by rf2.Book_Id,rf2.ResourceFolder_Name) t "
                        , strWhere
                        , Resource_TypeConst.testPaper类型文件
                        , Resource_ClassConst.云资源);
                    #endregion
                }
                else
                {
                    #region 资源列表
                    strWhere += " and rf2.Book_Id='" + bookId + "' ";
                    strSql = string.Format(@"select * from (select ROW_NUMBER() over(ORDER BY CreateTime desc) row,* from (
select rf2.Book_Id as bookId,rf2.ResourceFolder_Name as bookName,min(t.CreateTime) as CreateTime
,rtrf.ResourceToResourceFolder_Id as rtrfId,rtrf.Resource_Name as rtrfName 
from SyncData t
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.DataId 
inner join ResourceFolder rf2 on rf2.Book_Id=rtrf.Book_Id and rf2.ResourceFolder_Level=5
left join BookAudit ba on ba.ResourceFolder_Id=rtrf.Book_Id
where t.TableName='ResourceToResourceFolder' and t.OperateType in('add','modify') and t.SyncStatus=1 {0}
and rtrf.Resource_Type='{1}' and rtrf.Resource_Class='{2}' 
and ba.AuditState='1' 
group by rf2.Book_Id,rf2.ResourceFolder_Name,rtrf.ResourceToResourceFolder_Id,rtrf.Resource_Name
) t ) t where row between {3} and {4} "
                        , strWhere
                        , Resource_TypeConst.testPaper类型文件
                        , Resource_ClassConst.云资源
                        , ((PageIndex - 1) * PageSize + 1)
                        , (PageIndex * PageSize));

                    strSqlCount = string.Format(@"select count(1) as icount from ( 
select rtrf.ResourceToResourceFolder_Id from SyncData t
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.DataId 
inner join ResourceFolder rf2 on rf2.Book_Id=rtrf.Book_Id and rf2.ResourceFolder_Level=5
left join BookAudit ba on ba.ResourceFolder_Id=rtrf.Book_Id
where t.TableName='ResourceToResourceFolder' and t.OperateType in('add','modify') and t.SyncStatus=1 {0}
and rtrf.Resource_Type='{1}' and rtrf.Resource_Class='{2}' 
and ba.AuditState='1' group by rf2.Book_Id,rf2.ResourceFolder_Name,rtrf.ResourceToResourceFolder_Id,rtrf.Resource_Name ) t "
                        , strWhere
                        , Resource_TypeConst.testPaper类型文件
                        , Resource_ClassConst.云资源);
                    #endregion
                }

                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];

                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount, 300).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        bookId = dtRes.Rows[i]["bookId"].ToString(),
                        rtrfId = dtRes.Rows[i]["rtrfId"].ToString(),
                        bookName = dtRes.Rows[i]["bookName"].ToString(),
                        rtrfName = dtRes.Rows[i]["rtrfName"].ToString(),
                        CreateTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
                        IsShowDetail = isShowDetail,
                        IsCanExecing = isCanExecing
                    });
                }

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