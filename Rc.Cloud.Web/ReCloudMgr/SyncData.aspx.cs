using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Common.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SyncData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSql = string.Empty;
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='自动同步数据' order by FileSyncExecRecord_TimeStart desc";
            string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
            if (str != "")
            {
                btnData.Text = "数据正在同步中，同步开始时间为" + str;
                btnData.Enabled = false;
            }
            else
            {
                Thread thread1 = new Thread(new ThreadStart(SynchronousData));
                if (!thread1.IsAlive)
                {
                    thread1.Start();

                }
            }
        }

        protected void btnData_Click(object sender, EventArgs e)
        {

            btnData.Text = "数据正在同步中，同步开始时间为" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            btnData.Enabled = false;

            Thread thread1 = new Thread(new ThreadStart(SynchronousData));
            if (!thread1.IsAlive)
            {
                thread1.Start();

            }
        }

        /// <summary>
        /// 自动同步数据
        /// </summary>
        private void SynchronousData()
        {
            string strFileSyncExecRecord_id = Guid.NewGuid().ToString();
            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "自动同步数据";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步进行中...";
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.createUser = "";
            try
            {

                //记录同步之前的信息
                bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);

                ExecSyncData(strFileSyncExecRecord_id);

                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "1";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步已完成";
                //记录同步完成后的信息
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);

            }
            catch (Exception ex)
            {
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "2";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步失败：" + ex.Message.ToString();
                //记录同步完成后的信息
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
            }
        }

        /// <summary>
        /// 执行数据同步
        /// </summary>
        private void ExecSyncData(string strFileSyncExecRecord_id)
        {
            //所有需要同步数据
            DataTable dtAll = new DataTable();
            dtAll = new BLL_SyncData().GetList("SyncStatus='0' or SyncStatus='2' ").Tables[0];
            if (dtAll.Rows.Count > 0)
            {
                DataView dataView = dtAll.DefaultView;
                dtAll = dataView.ToTable(true, "TableName", "DataId", "OperateType");
            }

            //ResourceFolder数据
            DataTable dtRF = new DataTable();
            DataRow[] drRF = null;
            if (dtAll.Rows.Count > 0) drRF = dtAll.Select("TableName='ResourceFolder'");
            if (drRF != null && drRF.Length > 0) dtRF = drRF.CopyToDataTable();

            //ResourceToResourceFolder数据（delete）
            DataTable dtRTRF_Delete = new DataTable();
            DataRow[] drRTRF_Delete = null;
            if (dtAll.Rows.Count > 0) drRTRF_Delete = dtAll.Select("TableName='ResourceToResourceFolder' and OperateType='delete'");
            if (drRTRF_Delete != null && drRTRF_Delete.Length > 0) dtRTRF_Delete = drRTRF_Delete.CopyToDataTable();

            //ResourceToResourceFolder数据（move，rename）
            DataTable dtRTRF_MoveRename = new DataTable();
            DataRow[] drRTRF_MoveRename = null;
            if (dtAll.Rows.Count > 0) drRTRF_MoveRename = dtAll.Select("TableName='ResourceToResourceFolder' and (OperateType='move' or OperateType='rename') ");
            if (drRTRF_MoveRename != null && drRTRF_MoveRename.Length > 0) dtRTRF_MoveRename = drRTRF_MoveRename.CopyToDataTable();

            //ResourceToResourceFolder数据（add，modify）
            DataTable dtRTRF_AddModify = new DataTable();
            DataRow[] drRTRF_AddModify = null;
            if (dtAll.Rows.Count > 0) drRTRF_AddModify = dtAll.Select("TableName='ResourceToResourceFolder' and (OperateType='modify' or OperateType='add') ");
            if (drRTRF_AddModify != null && drRTRF_AddModify.Length > 0) dtRTRF_AddModify = drRTRF_AddModify.CopyToDataTable();
            if (dtRTRF_AddModify.Rows.Count > 0)
            {
                DataView dataView = dtRTRF_AddModify.DefaultView;
                dtRTRF_AddModify = dataView.ToTable(true, "DataId");
            }

            StringBuilder stbRF = new StringBuilder();
            StringBuilder stbRTRF_Delete = new StringBuilder();
            StringBuilder stbRTRF_MoveRename = new StringBuilder();
            StringBuilder stbRTRF_AddModify = new StringBuilder();

            #region ①删除ResourceToResourceFolder数据（delete）
            Rc.Model.Resources.Model_FileSyncExecRecordDetail model_FileSyncExecRecordDetail = new Rc.Model.Resources.Model_FileSyncExecRecordDetail();
            Rc.BLL.Resources.BLL_FileSyncExecRecordDetail bll_FileSyncExecRecordDetail = new Rc.BLL.Resources.BLL_FileSyncExecRecordDetail();
            model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id = Guid.NewGuid().ToString();
            model_FileSyncExecRecordDetail.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecordDetail.Detail_Remark = "①删除ResourceToResourceFolder数据（delete）";
            model_FileSyncExecRecordDetail.Detail_Status = "0";
            model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
            bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

            foreach (DataRow item in dtRTRF_Delete.Rows)
            {
                stbRTRF_Delete = new StringBuilder();
                stbRTRF_Delete.Append("declare @resourceId char(36);");
                stbRTRF_Delete.AppendFormat(@"delete from TestQuestions_Option where TestQuestions_Id in(select TestQuestions_Id from TestQuestions where ResourceToResourceFolder_Id='{0}');
delete from TestQuestions_Score where ResourceToResourceFolder_Id='{0}';
delete from TestQuestions where ResourceToResourceFolder_Id='{0}';
delete from ResourceToResourceFolder_Property where ResourceToResourceFolder_Id='{0}';
delete from ResourceToResourceFolder_img where ResourceToResourceFolder_Id='{0}';
select @resourceId=Resource_Id from ResourceToResourceFolder where ResourceToResourceFolder_Id='{0}';
delete from ResourceToResourceFolder where ResourceToResourceFolder_Id='{0}';
delete from [Resource] where Resource_Id=@resourceId;", item["DataId"]);
                DbHelperSQL_Operate.ExecuteSqlByTime(stbRTRF_Delete.ToString(), 7200);
                DbHelperSQL.ExecuteSqlByTime(string.Format("update SyncData set SyncStatus='1' where OperateType='delete' and DataId='{0}'; ", item["DataId"]), 60);
            }

            model_FileSyncExecRecordDetail.Detail_Status = "1";
            model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
            bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
            #endregion

            #region ②同步ResourceFolder数据
            model_FileSyncExecRecordDetail = new Rc.Model.Resources.Model_FileSyncExecRecordDetail();
            model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id = Guid.NewGuid().ToString();
            model_FileSyncExecRecordDetail.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecordDetail.Detail_Remark = "②同步ResourceFolder数据";
            model_FileSyncExecRecordDetail.Detail_Status = "0";
            model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
            bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

            foreach (DataRow item in dtRF.Rows)
            {
                stbRF = new StringBuilder();
                #region delete
                if (item["OperateType"].ToString() == "delete") stbRF.AppendFormat("delete from ResourceFolder where ResourceFolder_Id='{0}'; ", item["DataId"]);
                #endregion
                DataTable dtSub = new BLL_ResourceFolder().GetList("ResourceFolder_Id='" + item["DataId"] + "'").Tables[0];
                if (dtSub.Rows.Count == 1)
                {
                    #region move，rename
                    if (item["OperateType"].ToString() == "move" || item["OperateType"].ToString() == "rename")
                    {
                        stbRF.Append("update ResourceFolder set ");
                        stbRF.AppendFormat("ResourceFolder_ParentId='{0}',", dtSub.Rows[0]["ResourceFolder_ParentId"]);
                        stbRF.AppendFormat("ResourceFolder_Name='{0}',", dtSub.Rows[0]["ResourceFolder_Name"]);
                        stbRF.AppendFormat("ResourceFolder_Level='{0}',", dtSub.Rows[0]["ResourceFolder_Level"]);
                        stbRF.AppendFormat("Resource_Type='{0}',", dtSub.Rows[0]["Resource_Type"]);
                        stbRF.AppendFormat("Resource_Class='{0}',", dtSub.Rows[0]["Resource_Class"]);
                        stbRF.AppendFormat("Resource_Version='{0}',", dtSub.Rows[0]["Resource_Version"]);
                        stbRF.AppendFormat("ResourceFolder_Remark='{0}',", dtSub.Rows[0]["ResourceFolder_Remark"]);
                        stbRF.AppendFormat("ResourceFolder_Order='{0}',", dtSub.Rows[0]["ResourceFolder_Order"]);
                        stbRF.AppendFormat("ResourceFolder_Owner='{0}',", dtSub.Rows[0]["ResourceFolder_Owner"]);
                        stbRF.AppendFormat("CreateFUser='{0}',", dtSub.Rows[0]["CreateFUser"]);
                        stbRF.AppendFormat("CreateTime='{0}',", dtSub.Rows[0]["CreateTime"]);
                        stbRF.AppendFormat("ResourceFolder_isLast='{0}',", dtSub.Rows[0]["ResourceFolder_isLast"]);
                        stbRF.AppendFormat("LessonPlan_Type='{0}',", dtSub.Rows[0]["LessonPlan_Type"]);
                        stbRF.AppendFormat("GradeTerm='{0}',", dtSub.Rows[0]["GradeTerm"]);
                        stbRF.AppendFormat("Subject='{0}',", dtSub.Rows[0]["Subject"]);
                        stbRF.AppendFormat("Book_ID='{0}',", dtSub.Rows[0]["Book_ID"]);
                        stbRF.AppendFormat("ParticularYear='{0}'", dtSub.Rows[0]["ParticularYear"]);
                        stbRF.AppendFormat(" where ResourceFolder_Id='{0}' ", dtSub.Rows[0]["ResourceFolder_Id"]);
                    }
                    #endregion
                    #region add
                    if (item["OperateType"].ToString() == "add")
                    {
                        stbRF.AppendFormat("if(select COUNT(1) from ResourceFolder where ResourceFolder_Id='{0}')=0 insert into ResourceFolder(ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear)", dtSub.Rows[0]["ResourceFolder_Id"]);
                        stbRF.AppendFormat(" values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}');"
                            , dtSub.Rows[0]["ResourceFolder_Id"]
                            , dtSub.Rows[0]["ResourceFolder_ParentId"]
                            , dtSub.Rows[0]["ResourceFolder_Name"]
                            , dtSub.Rows[0]["ResourceFolder_Level"]
                            , dtSub.Rows[0]["Resource_Type"]
                            , dtSub.Rows[0]["Resource_Class"]
                            , dtSub.Rows[0]["Resource_Version"]
                            , dtSub.Rows[0]["ResourceFolder_Remark"]
                            , dtSub.Rows[0]["ResourceFolder_Order"]
                            , dtSub.Rows[0]["ResourceFolder_Owner"]
                            , dtSub.Rows[0]["CreateFUser"]
                            , dtSub.Rows[0]["CreateTime"]
                            , dtSub.Rows[0]["ResourceFolder_isLast"]
                            , dtSub.Rows[0]["LessonPlan_Type"]
                            , dtSub.Rows[0]["GradeTerm"]
                            , dtSub.Rows[0]["Subject"]
                            , dtSub.Rows[0]["Book_ID"]
                            , dtSub.Rows[0]["ParticularYear"]);
                    }
                    #endregion
                }
                if (!string.IsNullOrEmpty(stbRF.ToString())) DbHelperSQL_Operate.ExecuteSqlByTime(stbRF.ToString(), 7200);
                DbHelperSQL.ExecuteSqlByTime(string.Format("update SyncData set SyncStatus='1' where DataId='{0}'; ", item["DataId"]), 60);
            }

            model_FileSyncExecRecordDetail.Detail_Status = "1";
            model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
            bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
            #endregion

            #region ③更新ResourceToResourceFolder数据（move，rename）
            model_FileSyncExecRecordDetail = new Rc.Model.Resources.Model_FileSyncExecRecordDetail();
            model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id = Guid.NewGuid().ToString();
            model_FileSyncExecRecordDetail.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecordDetail.Detail_Remark = "③更新ResourceToResourceFolder数据（move，rename）";
            model_FileSyncExecRecordDetail.Detail_Status = "0";
            model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
            bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

            foreach (DataRow item in dtRTRF_MoveRename.Rows)
            {
                stbRTRF_MoveRename = new StringBuilder();
                DataTable dtSub = new BLL_ResourceToResourceFolder().GetList("ResourceToResourceFolder_Id='" + item["DataId"] + "'").Tables[0];
                if (dtSub.Rows.Count == 1)
                {
                    stbRTRF_MoveRename.Append("update ResourceToResourceFolder set ");
                    stbRTRF_MoveRename.AppendFormat("ResourceFolder_Id='{0}',", dtSub.Rows[0]["ResourceFolder_Id"]);
                    stbRTRF_MoveRename.AppendFormat("Resource_Id='{0}',", dtSub.Rows[0]["Resource_Id"]);
                    stbRTRF_MoveRename.AppendFormat("File_Name='{0}',", dtSub.Rows[0]["File_Name"]);
                    stbRTRF_MoveRename.AppendFormat("Resource_Type='{0}',", dtSub.Rows[0]["Resource_Type"]);
                    stbRTRF_MoveRename.AppendFormat("Resource_Name='{0}',", dtSub.Rows[0]["Resource_Name"]);
                    stbRTRF_MoveRename.AppendFormat("Resource_Class='{0}',", dtSub.Rows[0]["Resource_Class"]);
                    stbRTRF_MoveRename.AppendFormat("Resource_Version='{0}',", dtSub.Rows[0]["Resource_Version"]);
                    stbRTRF_MoveRename.AppendFormat("File_Owner='{0}',", dtSub.Rows[0]["File_Owner"]);
                    stbRTRF_MoveRename.AppendFormat("CreateFUser='{0}',", dtSub.Rows[0]["CreateFUser"]);
                    stbRTRF_MoveRename.AppendFormat("CreateTime='{0}',", dtSub.Rows[0]["CreateTime"]);
                    stbRTRF_MoveRename.AppendFormat("UpdateTime='{0}',", dtSub.Rows[0]["UpdateTime"]);
                    stbRTRF_MoveRename.AppendFormat("File_Suffix='{0}',", dtSub.Rows[0]["File_Suffix"]);
                    stbRTRF_MoveRename.AppendFormat("LessonPlan_Type='{0}',", dtSub.Rows[0]["LessonPlan_Type"]);
                    stbRTRF_MoveRename.AppendFormat("GradeTerm='{0}',", dtSub.Rows[0]["GradeTerm"]);
                    stbRTRF_MoveRename.AppendFormat("Subject='{0}',", dtSub.Rows[0]["Subject"]);
                    stbRTRF_MoveRename.AppendFormat("Resource_Domain='{0}',", dtSub.Rows[0]["Resource_Domain"]);
                    stbRTRF_MoveRename.AppendFormat("Resource_Url='{0}',", dtSub.Rows[0]["Resource_Url"]);
                    stbRTRF_MoveRename.AppendFormat("Resource_shared='{0}',", dtSub.Rows[0]["Resource_shared"]);
                    stbRTRF_MoveRename.AppendFormat("Book_ID='{0}',", dtSub.Rows[0]["Book_ID"]);
                    stbRTRF_MoveRename.AppendFormat("ParticularYear='{0}',", dtSub.Rows[0]["ParticularYear"]);
                    stbRTRF_MoveRename.AppendFormat("ResourceToResourceFolder_Order='{0}'", dtSub.Rows[0]["ResourceToResourceFolder_Order"]);
                    stbRTRF_MoveRename.AppendFormat(" where ResourceToResourceFolder_Id='{0}'; ", dtSub.Rows[0]["ResourceToResourceFolder_Id"]);
                }
                if (!string.IsNullOrEmpty(stbRTRF_MoveRename.ToString())) DbHelperSQL_Operate.ExecuteSqlByTime(stbRTRF_MoveRename.ToString(), 7200);
                DbHelperSQL.ExecuteSqlByTime(string.Format("update SyncData set SyncStatus='1' where (OperateType='move' or OperateType='rename') and DataId='{0}'; ", item["DataId"]), 60);
            }

            model_FileSyncExecRecordDetail.Detail_Status = "1";
            model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
            bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
            #endregion

            #region ④同步ResourceToResourceFolder数据 先删除数据 再增加数据（add，modify）
            model_FileSyncExecRecordDetail = new Rc.Model.Resources.Model_FileSyncExecRecordDetail();
            model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id = Guid.NewGuid().ToString();
            model_FileSyncExecRecordDetail.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecordDetail.Detail_Remark = "④同步ResourceToResourceFolder数据 先删除数据 再增加数据（add，modify）";
            model_FileSyncExecRecordDetail.Detail_Status = "0";
            model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
            bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

            foreach (DataRow item in dtRTRF_AddModify.Rows)
            {
                string resourceId = DbHelperSQL.GetSingle("select Resource_Id from ResourceToResourceFolder where ResourceToResourceFolder_Id='" + item["DataId"] + "'").ToString();
                if (!string.IsNullOrEmpty(resourceId))
                {
                    //删除数据
                    stbRTRF_AddModify = new StringBuilder();
                    stbRTRF_AddModify.AppendFormat(@"delete from TestQuestions_Option where TestQuestions_Id in(select TestQuestions_Id from TestQuestions where ResourceToResourceFolder_Id='{0}');
delete from TestQuestions_Score where ResourceToResourceFolder_Id='{0}';
delete from TestQuestions where ResourceToResourceFolder_Id='{0}';
delete from ResourceToResourceFolder_Property where ResourceToResourceFolder_Id='{0}';
delete from ResourceToResourceFolder_img where ResourceToResourceFolder_Id='{0}';
delete from ResourceToResourceFolder where ResourceToResourceFolder_Id='{0}';
delete from [Resource] where Resource_Id='{1}';"
                        , item["DataId"]
                        , resourceId);
                    AddLogInfo(string.Format("{0}--结束--", stbRTRF_AddModify.ToString()), true);
                    if (!string.IsNullOrEmpty(stbRTRF_AddModify.ToString())) DbHelperSQL_Operate.ExecuteSqlByTime(stbRTRF_AddModify.ToString(), 7200);

                    //新增数据
                    #region Resource
                    stbRTRF_AddModify = new StringBuilder();
                    DataTable dtRes = new BLL_Resource().GetList("Resource_Id='" + resourceId + "'").Tables[0];
                    stbRTRF_AddModify.Append("insert into Resource(Resource_Id,Resource_MD5,Resource_DataStrem,Resource_ContentHtml,CreateTime,Resource_ContentLength)");
                    stbRTRF_AddModify.AppendFormat(" values ('{0}','{1}','{2}','{3}','{4}','{5}');"
                        , dtRes.Rows[0]["Resource_Id"]
                        , dtRes.Rows[0]["Resource_MD5"]
                        , dtRes.Rows[0]["Resource_DataStrem"]
                        , dtRes.Rows[0]["Resource_ContentHtml"]
                        , dtRes.Rows[0]["CreateTime"]
                        , dtRes.Rows[0]["Resource_ContentLength"]);
                    AddLogInfo(string.Format("{0}--结束--", stbRTRF_AddModify.ToString()), true);
                    if (!string.IsNullOrEmpty(stbRTRF_AddModify.ToString())) DbHelperSQL_Operate.ExecuteSqlByTime(stbRTRF_AddModify.ToString(), 7200);

                    #endregion
                    #region ResourceToResourceFolder
                    stbRTRF_AddModify = new StringBuilder();
                    DataTable dtRTRF = new BLL_ResourceToResourceFolder().GetList("ResourceToResourceFolder_Id='" + item["DataId"] + "'").Tables[0];
                    stbRTRF_AddModify.Append("insert into ResourceToResourceFolder(ResourceToResourceFolder_Id,ResourceFolder_Id,Resource_Id,File_Name,Resource_Type,Resource_Name,Resource_Class,Resource_Version,File_Owner,CreateFUser,CreateTime,UpdateTime,File_Suffix,LessonPlan_Type,GradeTerm,Subject,Resource_Domain,Resource_Url,Resource_shared,Book_ID,ParticularYear,ResourceToResourceFolder_Order)");
                    stbRTRF_AddModify.AppendFormat(" values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}');"
                        , dtRTRF.Rows[0]["ResourceToResourceFolder_Id"]
                        , dtRTRF.Rows[0]["ResourceFolder_Id"]
                        , dtRTRF.Rows[0]["Resource_Id"]
                        , dtRTRF.Rows[0]["File_Name"]
                        , dtRTRF.Rows[0]["Resource_Type"]
                        , dtRTRF.Rows[0]["Resource_Name"]
                        , dtRTRF.Rows[0]["Resource_Class"]
                        , dtRTRF.Rows[0]["Resource_Version"]
                        , dtRTRF.Rows[0]["File_Owner"]
                        , dtRTRF.Rows[0]["CreateFUser"]
                        , dtRTRF.Rows[0]["CreateTime"]
                        , dtRTRF.Rows[0]["UpdateTime"]
                        , dtRTRF.Rows[0]["File_Suffix"]
                        , dtRTRF.Rows[0]["LessonPlan_Type"]
                        , dtRTRF.Rows[0]["GradeTerm"]
                        , dtRTRF.Rows[0]["Subject"]
                        , dtRTRF.Rows[0]["Resource_Domain"]
                        , dtRTRF.Rows[0]["Resource_Url"]
                        , dtRTRF.Rows[0]["Resource_shared"]
                        , dtRTRF.Rows[0]["Book_ID"]
                        , dtRTRF.Rows[0]["ParticularYear"]
                        , dtRTRF.Rows[0]["ResourceToResourceFolder_Order"]);
                    AddLogInfo(string.Format("{0}--结束--", stbRTRF_AddModify.ToString()), true);
                    if (!string.IsNullOrEmpty(stbRTRF_AddModify.ToString())) DbHelperSQL_Operate.ExecuteSqlByTime(stbRTRF_AddModify.ToString(), 7200);

                    #endregion
                    #region ResourceToResourceFolder_img
                    stbRTRF_AddModify = new StringBuilder();
                    DataTable dtRTRF_Img = new BLL_ResourceToResourceFolder_img().GetList("ResourceToResourceFolder_Id='" + item["DataId"] + "'").Tables[0];
                    foreach (DataRow item_Img in dtRTRF_Img.Rows)
                    {
                        stbRTRF_AddModify.Append("insert into ResourceToResourceFolder_img(ResourceToResourceFolder_img_id,ResourceToResourceFolder_id,ResourceToResourceFolderImg_Url,ResourceToResourceFolderImg_Order,CreateTime)");
                        stbRTRF_AddModify.AppendFormat(" values ('{0}','{1}','{2}','{3}','{4}');"
                            , item_Img["ResourceToResourceFolder_img_id"]
                            , item_Img["ResourceToResourceFolder_id"]
                            , item_Img["ResourceToResourceFolderImg_Url"]
                            , item_Img["ResourceToResourceFolderImg_Order"]
                            , item_Img["CreateTime"]);
                    }
                    AddLogInfo(string.Format("{0}--结束--", stbRTRF_AddModify.ToString()), true);
                    if (!string.IsNullOrEmpty(stbRTRF_AddModify.ToString())) DbHelperSQL_Operate.ExecuteSqlByTime(stbRTRF_AddModify.ToString(), 7200);

                    #endregion
                    #region ResourceToResourceFolder_Property
                    stbRTRF_AddModify = new StringBuilder();
                    DataTable dtRTRF_Property = new BLL_ResourceToResourceFolder_Property().GetList("ResourceToResourceFolder_Id='" + item["DataId"] + "'").Tables[0];
                    foreach (DataRow item_Property in dtRTRF_Property.Rows)
                    {
                        stbRTRF_AddModify.Append("insert into ResourceToResourceFolder_Property(ResourceToResourceFolder_Id,BooksCode,BooksUnitCode,GuidDoc,TestPaperName,CreateTime,paperHeaderDoc,paperHeaderHtml)");
                        stbRTRF_AddModify.AppendFormat(" values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');"
                            , item_Property["ResourceToResourceFolder_Id"]
                            , item_Property["BooksCode"]
                            , item_Property["BooksUnitCode"]
                            , item_Property["GuidDoc"]
                            , item_Property["TestPaperName"]
                            , item_Property["CreateTime"]
                            , item_Property["paperHeaderDoc"]
                            , item_Property["paperHeaderHtml"]);
                    }
                    AddLogInfo(string.Format("{0}--结束--", stbRTRF_AddModify.ToString()), true);
                    if (!string.IsNullOrEmpty(stbRTRF_AddModify.ToString())) DbHelperSQL_Operate.ExecuteSqlByTime(stbRTRF_AddModify.ToString(), 7200);

                    #endregion
                    #region TestQuestions，TestQuestions_Option
                    stbRTRF_AddModify = new StringBuilder();
                    DataTable dtTQ = new BLL_TestQuestions().GetList("ResourceToResourceFolder_Id='" + item["DataId"] + "'").Tables[0];
                    foreach (DataRow item_TQ in dtTQ.Rows)
                    {
                        stbRTRF_AddModify.Append("insert into TestQuestions(TestQuestions_Id,ResourceToResourceFolder_Id,TestQuestions_Num,TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,CreateTime,topicNumber,Parent_Id,[type])");
                        stbRTRF_AddModify.AppendFormat(" values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');"
                            , item_TQ["TestQuestions_Id"]
                            , item_TQ["ResourceToResourceFolder_Id"]
                            , item_TQ["TestQuestions_Num"]
                            , item_TQ["TestQuestions_Type"]
                            , item_TQ["TestQuestions_SumScore"]
                            , item_TQ["TestQuestions_Content"]
                            , item_TQ["TestQuestions_Answer"]
                            , item_TQ["CreateTime"]
                            , item_TQ["topicNumber"].ToString().Filter()
                            , item_TQ["Parent_Id"]
                            , item_TQ["type"]);
                        #region TestQuestions_Option
                        DataTable dtTQ_Option = new BLL_TestQuestions_Option().GetList("TestQuestions_Id='" + item_TQ["TestQuestions_Id"] + "'").Tables[0];
                        foreach (DataRow item_TQ_Option in dtTQ_Option.Rows)
                        {
                            stbRTRF_AddModify.Append("insert into TestQuestions_Option(TestQuestions_Option_Id,TestQuestions_Id,TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,CreateTime,TestQuestions_Score_ID)");
                            stbRTRF_AddModify.AppendFormat(" values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}');"
                                , item_TQ_Option["TestQuestions_Option_Id"]
                                , item_TQ_Option["TestQuestions_Id"]
                                , item_TQ_Option["TestQuestions_OptionParent_OrderNum"]
                                , item_TQ_Option["TestQuestions_Option_Content"].ToString().Filter()
                                , item_TQ_Option["TestQuestions_Option_OrderNum"]
                                , item_TQ_Option["CreateTime"]
                                , item_TQ_Option["TestQuestions_Score_ID"]);
                        }
                        #endregion
                    }
                    AddLogInfo(string.Format("{0}--结束--", stbRTRF_AddModify.ToString()), true);
                    if (!string.IsNullOrEmpty(stbRTRF_AddModify.ToString())) DbHelperSQL_Operate.ExecuteSqlByTime(stbRTRF_AddModify.ToString(), 7200);

                    #endregion
                    #region TestQuestions_Score
                    stbRTRF_AddModify = new StringBuilder();
                    DataTable dtTQ_Score = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + item["DataId"] + "'").Tables[0];
                    foreach (DataRow item_TQ_Score in dtTQ_Score.Rows)
                    {
                        stbRTRF_AddModify.Append("insert into TestQuestions_Score(TestQuestions_Score_ID,ResourceToResourceFolder_Id,TestQuestions_Id,TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,AnalyzeHyperlinkData,AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,TrainHyperlinkData,TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,CreateTime,AreaHyperlink,AreaText,kaofaText,testIndex)");
                        stbRTRF_AddModify.AppendFormat(" values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}');"
                            , item_TQ_Score["TestQuestions_Score_ID"]
                            , item_TQ_Score["ResourceToResourceFolder_Id"]
                            , item_TQ_Score["TestQuestions_Id"]
                            , item_TQ_Score["TestQuestions_Num"]
                            , item_TQ_Score["TestQuestions_OrderNum"]
                            , item_TQ_Score["TestQuestions_Score"]
                            , item_TQ_Score["AnalyzeHyperlink"]
                            , item_TQ_Score["AnalyzeHyperlinkData"]
                            , item_TQ_Score["AnalyzeHyperlinkHtml"]
                            , item_TQ_Score["AnalyzeText"]
                            , item_TQ_Score["ComplexityHyperlink"]
                            , item_TQ_Score["ComplexityText"]
                            , item_TQ_Score["ContentHyperlink"]
                            , item_TQ_Score["ContentText"]
                            , item_TQ_Score["DocBase64"]
                            , item_TQ_Score["DocHtml"]
                            , item_TQ_Score["ScoreHyperlink"]
                            , item_TQ_Score["ScoreText"]
                            , item_TQ_Score["TargetHyperlink"]
                            , item_TQ_Score["TrainHyperlinkData"]
                            , item_TQ_Score["TrainHyperlinkHtml"]
                            , item_TQ_Score["TargetText"]
                            , item_TQ_Score["TestCorrect"]
                            , item_TQ_Score["TestType"]
                            , item_TQ_Score["TrainHyperlink"]
                            , item_TQ_Score["TrainText"]
                            , item_TQ_Score["TypeHyperlink"]
                            , item_TQ_Score["TypeText"]
                            , item_TQ_Score["CreateTime"]
                            , item_TQ_Score["AreaHyperlink"]
                            , item_TQ_Score["AreaText"]
                            , item_TQ_Score["kaofaText"]
                            , item_TQ_Score["testIndex"]);
                    }
                    AddLogInfo(string.Format("{0}--结束--", stbRTRF_AddModify.ToString()), true);
                    if (!string.IsNullOrEmpty(stbRTRF_AddModify.ToString())) DbHelperSQL_Operate.ExecuteSqlByTime(stbRTRF_AddModify.ToString(), 7200);

                    #endregion
                }

                DbHelperSQL.ExecuteSqlByTime(string.Format("update SyncData set SyncStatus='1' where (OperateType='add' or OperateType='modify') and DataId='{0}'; ", item["DataId"]), 60);
            }

            model_FileSyncExecRecordDetail.Detail_Status = "1";
            model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
            bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
            #endregion

            #region ⑤同步SyncData表数据
            model_FileSyncExecRecordDetail = new Rc.Model.Resources.Model_FileSyncExecRecordDetail();
            model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id = Guid.NewGuid().ToString();
            model_FileSyncExecRecordDetail.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecordDetail.Detail_Remark = "⑤同步SyncData表数据";
            model_FileSyncExecRecordDetail.Detail_Status = "0";
            model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
            bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

            StringBuilder stbSyncData = new StringBuilder();
            DataTable dtSyncData = new BLL_SyncData().GetList("").Tables[0];
            foreach (DataRow item in dtSyncData.Rows)
            {
                stbSyncData.AppendFormat("if(select COUNT(1) from SyncData where SyncDataId='{0}')=0 insert into SyncData(SyncDataId,TableName,DataId,OperateType,CreateTime,SyncStatus)", item["SyncDataId"]);
                stbSyncData.AppendFormat(" values ('{0}','{1}','{2}','{3}','{4}','{5}');"
                    , item["SyncDataId"]
                    , item["TableName"]
                    , item["DataId"]
                    , item["OperateType"]
                    , item["CreateTime"]
                    , item["SyncStatus"]);
            }
            AddLogInfo(string.Format("{0}--结束--", stbRTRF_AddModify.ToString()), true);
            if (!string.IsNullOrEmpty(stbSyncData.ToString())) DbHelperSQL_Operate.ExecuteSqlByTime(stbSyncData.ToString(), 7200);

            model_FileSyncExecRecordDetail.Detail_Status = "1";
            model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
            bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
            #endregion

            #region ⑥同步BookAudit表数据
            model_FileSyncExecRecordDetail = new Rc.Model.Resources.Model_FileSyncExecRecordDetail();
            model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id = Guid.NewGuid().ToString();
            model_FileSyncExecRecordDetail.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecordDetail.Detail_Remark = "⑥同步BookAudit表数据";
            model_FileSyncExecRecordDetail.Detail_Status = "0";
            model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
            bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

            StringBuilder stbBookAudit = new StringBuilder();
            DataTable dtBookAudit = new BLL_BookAudit().GetList("").Tables[0];
            foreach (DataRow item in dtBookAudit.Rows)
            {
                stbBookAudit.AppendFormat(@"if(select COUNT(1) from BookAudit where ResourceFolder_Id='{0}')=0 
                begin 
                insert into BookAudit(ResourceFolder_Id,Book_Name,AuditState,AuditRemark,CreateUser,CreateTime) values ('{0}','{1}','{2}','{3}','{4}','{5}');"
                    , item["ResourceFolder_Id"]
                    , item["Book_Name"]
                    , item["AuditState"]
                    , item["AuditRemark"]
                    , item["CreateUser"]
                    , item["CreateTime"]);
                stbBookAudit.Append(" end ");
                stbBookAudit.Append(" else ");
                stbBookAudit.Append(" begin ");
                stbBookAudit.AppendFormat(@"if(select COUNT(1) from BookAudit where ResourceFolder_Id='{0}' and (AuditState='{1}' or CreateTime='{2}') )=0 
                begin 
                    update BookAudit set AuditState='{1}',CreateTime='{2}' where ResourceFolder_Id='{0}';
                end ", item["ResourceFolder_Id"], item["AuditState"], item["CreateTime"]);
                stbBookAudit.Append(" end ");
            }
            AddLogInfo(string.Format("{0}--结束--", stbBookAudit.ToString()), true);
            if (!string.IsNullOrEmpty(stbBookAudit.ToString())) DbHelperSQL_Operate.ExecuteSqlByTime(stbBookAudit.ToString(), 7200);

            model_FileSyncExecRecordDetail.Detail_Status = "1";
            model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
            bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
            #endregion

        }

        private void AddLogInfo(string txt, bool isAppend)
        {
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "\\Upload\\SyncDataAutoLog\\";
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

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string strFileSyncExecRecord_Status, int PageSize, int PageIndex)
        {
            try
            {

                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;

                if (strFileSyncExecRecord_Status == "0") strWhere += " and f.Detail_Status='0' ";
                if (strFileSyncExecRecord_Status == "1") strWhere += " and f.Detail_Status='1' ";
                if (strFileSyncExecRecord_Status == "2") strWhere += " and f.Detail_Status='2' ";

                strWhere += " and f.FileSyncExecRecord_Type='自动同步数据' ";
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
                    string strIType = string.Empty;
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
                        SysUser_Name = dtRes.Rows[i]["SysUser_Name"].ToString()
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
                    err = "error"// ex.Message.ToString()
                });
            }
        }

        protected void btnDataReExec_Click(object sender, EventArgs e)
        {
            btnData.Text = "数据正在同步中，同步开始时间为" + DateTime.Now.ToShortDateString();
            btnData.Enabled = false;

            Thread threadRe = new Thread(new ThreadStart(SynchronousDataReExec));
            if (!threadRe.IsAlive)
            {
                threadRe.Start();

            }
        }
        private void SynchronousDataReExec()
        {
            string strFileSyncExecRecord_id = hidId.Value;
            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord = bll_FileSyncExecRecord.GetModel(strFileSyncExecRecord_id);
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "自动同步数据";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步进行中...";
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.createUser = "";
            try
            {

                //记录同步之前的信息
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);

                ExecSyncData(strFileSyncExecRecord_id);

                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "1";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步已完成";
                //记录同步完成后的信息
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);

            }
            catch (Exception ex)
            {
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "2";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步失败：" + ex.Message.ToString();
                //记录同步完成后的信息
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
                //Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                //Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                //model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                ////model.FileSize = ls;
                //// model.SyncUrl = strFilePath;
                //model.MsgType = "SynDataError";
                //model.ErrorMark = ex.Message.ToString();
                //model.CreateTime = DateTime.Now;
                //bll.Add(model);
            }
        }
    }
}