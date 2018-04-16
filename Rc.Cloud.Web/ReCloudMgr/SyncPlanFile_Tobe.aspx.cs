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
    public partial class SyncPlanFile_Tobe : System.Web.UI.Page
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
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='同步教案new' order by FileSyncExecRecord_TimeStart desc";
            string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
            if (str != "")
            {
                isCanExecing = "0";
                //btnPlan.Text = "教案文件正在同步中，同步开始时间为" + str;
                //btnPlan.Enabled = false;
                btnConfirm.Value = "教案文件正在同步中，同步开始时间为" + DateTime.Now.ToShortDateString();
                btnConfirm.Attributes.Add("disabled", "disabled");

                strSql = @"update FileSyncExecRecord set FileSyncExecRecord_Status='2',FileSyncExecRecord_Remark='已执行180分钟，超时，自动更新状态'
where FileSyncExecRecord_Type='同步教案new' and FileSyncExecRecord_Status='0' and DateDiff(MI,FileSyncExecRecord_TimeStart,getdate())>180 ";
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


        protected void btnPlan_Click(object sender, EventArgs e)
        {

            //btnPlan.Text = "教案文件正在同步中，同步开始时间为" + DateTime.Now.ToShortDateString();
            //btnPlan.Enabled = false;
            btnConfirm.Value = "教案文件正在同步中，同步开始时间为" + DateTime.Now.ToShortDateString();
            btnConfirm.Attributes.Add("disabled", "disabled");

            Thread thread1 = new Thread(new ThreadStart(ResSynchroTeachingPlan));
            if (!thread1.IsAlive)
            {
                thread1.Start();
            }
            Response.Redirect("SyncPlanFile_Tobe.aspx?SysUser_ID=" + SysUser_ID);
        }

        /// <summary>
        /// 同步教案new
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
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "同步教案new";
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "正在同步...";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.createUser = SysUser_ID;
            model_FileSyncExecRecord.FileSyncExecRecord_Condition = strCondition;
            bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);
            #endregion

            try
            {
                //教案文件，htm文件相关数据
                DataTable dtFilePlan = GetDtFilePlan(strbookId, strrtrfId);
                //教案预览图片文件相关数据
                DataTable dtFilePlanView = GetDtFilePlanView(strbookId, strrtrfId);

                DataView dataView = dtFilePlan.DefaultView;
                // 资源数据
                DataTable dtRTRF = new DataTable();
                if (dtFilePlan.Rows.Count > 0)
                {
                    dtRTRF = dataView.ToTable(true, "Book_Id", "ResourceToResourceFolder_Id");
                }

                foreach (DataRow item in dtRTRF.Rows)
                {
                    #region 下载文件
                    //教案SW+thm
                    DataRow[] drSWdsc = dtFilePlan.Select(string.Format(" Resource_Type='{0}' and ResourceToResourceFolder_Id='{1}' "
                        , Resource_TypeConst.ScienceWord类型文件, item["ResourceToResourceFolder_Id"]));
                    DownLoadFile(drSWdsc, "swDocument", "dsc", strProductPublicUrl, strFileSyncExecRecord_id, "教案SWdsc", isCover);
                    DownLoadFile(drSWdsc, "swDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "教案SWdsc", isCover);
                    //教案class+thm
                    DataRow[] drClass = dtFilePlan.Select(string.Format(" Resource_Type='{0}' and ResourceToResourceFolder_Id='{1}' "
                        , Resource_TypeConst.class类型文件, item["ResourceToResourceFolder_Id"]));
                    DownLoadFile(drClass, "classDocument", "class", strProductPublicUrl, strFileSyncExecRecord_id, "教案class", isCover);
                    DownLoadFile(drClass, "classDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "教案class", isCover);
                    //微课class+thm
                    DataRow[] drMicroClass = dtFilePlan.Select(string.Format(" Resource_Type='{0}' and ResourceToResourceFolder_Id='{1}' "
                        , Resource_TypeConst.class类型微课件, item["ResourceToResourceFolder_Id"]));
                    DownLoadFile(drMicroClass, "microClassDocument", "class", strProductPublicUrl, strFileSyncExecRecord_id, "微课class", isCover);
                    DownLoadFile(drMicroClass, "microClassDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "微课class", isCover);


                    //教案SW+预览图片
                    DataRow[] drSWdscView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}' and ResourceToResourceFolder_Id='{1}' "
                        , Resource_TypeConst.ScienceWord类型文件, item["ResourceToResourceFolder_Id"]));
                    DownLoadFile(drSWdscView, "swView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "教案SW预览图片", isCover);
                    //教案Class+预览图片
                    DataRow[] drClassView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}' and ResourceToResourceFolder_Id='{1}' "
                        , Resource_TypeConst.class类型文件, item["ResourceToResourceFolder_Id"]));
                    DownLoadFile(drClassView, "classView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "教案Class预览图片", isCover);
                    //微课Class+预览图片
                    DataRow[] drMicroClassView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}' and ResourceToResourceFolder_Id='{1}' "
                        , Resource_TypeConst.class类型微课件, item["ResourceToResourceFolder_Id"]));
                    DownLoadFile(drMicroClassView, "microClassView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "微课Class预览图片", isCover);
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
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步成功完成";
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
        private DataTable GetDtFilePlan(string strbookId, string strrtrfId)
        {
            #region 读取需要同步的数据
            string strSql = string.Empty;
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(strbookId)) strWhere += " and t1.Book_Id='" + strbookId + "' ";
            if (!string.IsNullOrEmpty(strrtrfId)) strWhere += " and t1.ResourceToResourceFolder_Id='" + strrtrfId + "' ";

            //教案(SW与class)BASE64内容 SW暂时使用此地址
            strSql = string.Format(@"select distinct t1.Book_Id,t1.ParticularYear,t1.GradeTerm,t1.Resource_Version,t1.Subject,t1.Resource_Type,t1.ResourceToResourceFolder_Id,t1.ResourceToResourceFolder_Id  as FileName 
,t1.Createtime
from ResourceToResourceFolder t1
left join BookAudit ba on ba.ResourceFolder_Id=t1.Book_Id
inner join SyncData sd on sd.OperateType in('add','modify') and sd.TableName='ResourceToResourceFolder' and sd.SyncStatus=1 and sd.DataId=t1.ResourceToResourceFolder_Id
where t1.Resource_Type in('{0}','{1}','{2}') and t1.Resource_Class='{3}' 
and ba.AuditState='1' 
{4} 
order by t1.Createtime ", Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, Resource_TypeConst.class类型微课件
                            , Resource_ClassConst.云资源
                            , strWhere);
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
            if (!string.IsNullOrEmpty(strbookId)) strWhere += " and t1.Book_Id='" + strbookId + "' ";
            if (!string.IsNullOrEmpty(strrtrfId)) strWhere += " and t1.ResourceToResourceFolder_Id='" + strrtrfId + "' ";

            //教案(SW与class)BASE64内容 SW暂时使用此地址
            strSql = string.Format(@"select distinct t1.Book_Id,t1.ParticularYear,t1.GradeTerm,t1.Resource_Type,t1.Resource_Version,t1.Subject,t1.ResourceToResourceFolder_id
,t2.ResourceToResourceFolder_img_id  as FileName,t2.Createtime 
from ResourceToResourceFolder t1 
inner join ResourceToResourceFolder_img t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
left join BookAudit ba on ba.ResourceFolder_Id=t1.Book_Id
inner join SyncData sd on sd.OperateType in('add','modify') and sd.TableName='ResourceToResourceFolder' and sd.SyncStatus=1 and sd.DataId=t1.ResourceToResourceFolder_Id
where t1.Resource_Type in('{0}','{1}','{2}') and t1.Resource_Class='{3}' 
and ba.AuditState='1' 
{4} 
order by t2.Createtime ", Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, Resource_TypeConst.class类型微课件
                            , Resource_ClassConst.云资源
                            , strWhere);
            DataTable dtFilePlanView = new DataTable();

            dtFilePlanView = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
            return dtFilePlanView;
            #endregion
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
and rtrf.Resource_Type in('{1}','{2}','{3}') and rtrf.Resource_Class='{4}' 
and ba.AuditState='1' 
group by rf2.Book_Id,rf2.ResourceFolder_Name
) t ) t where row between {5} and {6} "
                        , strWhere
                        , Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, Resource_TypeConst.class类型微课件
                        , Resource_ClassConst.云资源
                        , ((PageIndex - 1) * PageSize + 1)
                        , (PageIndex * PageSize));

                    strSqlCount = string.Format(@"select count(1) as icount from (select rf2.Book_Id from SyncData t
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.DataId 
inner join ResourceFolder rf2 on rf2.Book_Id=rtrf.Book_Id and rf2.ResourceFolder_Level=5
left join BookAudit ba on ba.ResourceFolder_Id=rtrf.Book_Id
where t.TableName='ResourceToResourceFolder' and t.OperateType in('add','modify') and t.SyncStatus=1 {0}
and rtrf.Resource_Type in('{1}','{2}','{3}') and rtrf.Resource_Class='{4}' 
and ba.AuditState='1' group by rf2.Book_Id,rf2.ResourceFolder_Name ) t "
                        , strWhere
                        , Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, Resource_TypeConst.class类型微课件
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
and rtrf.Resource_Type in('{1}','{2}','{3}') and rtrf.Resource_Class='{4}' 
and ba.AuditState='1' 
group by rf2.Book_Id,rf2.ResourceFolder_Name,rtrf.ResourceToResourceFolder_Id,rtrf.Resource_Name
) t ) t where row between {5} and {6} "
                        , strWhere
                        , Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, Resource_TypeConst.class类型微课件
                        , Resource_ClassConst.云资源
                        , ((PageIndex - 1) * PageSize + 1)
                        , (PageIndex * PageSize));

                    strSqlCount = string.Format(@"select count(1) as icount from ( 
select rtrf.ResourceToResourceFolder_Id from SyncData t
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.DataId 
inner join ResourceFolder rf2 on rf2.Book_Id=rtrf.Book_Id and rf2.ResourceFolder_Level=5
left join BookAudit ba on ba.ResourceFolder_Id=rtrf.Book_Id
where t.TableName='ResourceToResourceFolder' and t.OperateType in('add','modify') and t.SyncStatus=1 {0}
and rtrf.Resource_Type in('{1}','{2}','{3}') and rtrf.Resource_Class='{4}' 
and ba.AuditState='1' group by rf2.Book_Id,rf2.ResourceFolder_Name,rtrf.ResourceToResourceFolder_Id,rtrf.Resource_Name ) t "
                        , strWhere
                        , Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, Resource_TypeConst.class类型微课件
                        , Resource_ClassConst.云资源);
                    #endregion
                }

                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
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
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
        }

    }
}