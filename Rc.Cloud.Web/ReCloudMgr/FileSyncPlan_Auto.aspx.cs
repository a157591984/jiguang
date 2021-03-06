﻿using Rc.Cloud.Web.Common;
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

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncPlan_Auto : System.Web.UI.Page
    {
        public string strLocalPhsicalPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strLocalPhsicalPath = Request.MapPath("\\Upload\\Resource\\");
            string strSql = string.Empty;
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='自动同步教案' order by FileSyncExecRecord_TimeStart desc";
            string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
            if (str == "")
            {
                Thread thread1 = new Thread(new ThreadStart(ResSynResource));
                if (!thread1.IsAlive)
                {
                    thread1.Start();

                }
            }
            else
            {
                strSql = @"update FileSyncExecRecord set FileSyncExecRecord_Status='2',FileSyncExecRecord_Remark='已执行600分钟，超时，自动更新状态'
where FileSyncExecRecord_Type='自动同步教案' and FileSyncExecRecord_Status='0' and DateDiff(MI,FileSyncExecRecord_TimeStart,getdate())>600 ";
                Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql);
            }
        }
        /// <summary>
        /// 自动同步教案
        /// </summary>
        public void ResSynResource()
        {
            //生产环境web站点存放文件的主目录
            string strProductPublicUrl = ConfigurationManager.AppSettings["ProductPublicUrl"].ToString();
            string strCondition = string.Empty;
            string strFileSyncExecRecord_id = Guid.NewGuid().ToString();
            bool isCover = false;
            #region 记录同步开始信息

            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "自动同步教案";
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步进行中...";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.FileSyncExecRecord_Condition = strCondition;
            model_FileSyncExecRecord.createUser = "";

            #endregion
            try
            {
                bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);
                string strSql = string.Empty;
                strSql = string.Format(@"select ResourceToResourceFolder_Id,Resource_Type,Resource_Class,Resource_Version,GradeTerm,Subject,ParticularYear,Book_Id 
from ResourceToResourceFolder where  ResourceToResourceFolder_Id in(
select DataId from SyncData where OperateType in('add','modify') and TableName='ResourceToResourceFolder' and SyncStatus=1 
)
and Resource_Type in ('{0}','{1}','{2}') and Resource_Class='{3}' "
                    , Resource_TypeConst.ScienceWord类型文件
                    , Resource_TypeConst.class类型文件
                    , Resource_TypeConst.class类型微课件
                    , Resource_ClassConst.云资源);//and CONVERT(varchar(10),CreateTime,23)=CONVERT(varchar(10),DATEADD(day,-1,getDate()),23)) 
                DataTable dtResource = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
                foreach (DataRow item in dtResource.Rows)
                {
                    #region class类型文件下载
                    strSql = string.Format(@"select ResourceToResourceFolder_Id,ResourceToResourceFolder_Id as FileName,Resource_Type,Resource_Class,Resource_Version,GradeTerm,Subject,ParticularYear from ResourceToResourceFolder where ResourceToResourceFolder_Id='" + item["ResourceToResourceFolder_Id"].ToString() + "'");
                    DataTable dtFilePlan = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                    //教案SW+thm
                    DataRow[] drSWdsc = dtFilePlan.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.ScienceWord类型文件));
                    DownLoadFile(drSWdsc, "swDocument", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "dsc", strProductPublicUrl, strFileSyncExecRecord_id, "教案SWdsc", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    DownLoadFile(drSWdsc, "swDocument", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "htm", strProductPublicUrl, strFileSyncExecRecord_id, "教案SWdsc", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    //教案class+thm
                    DataRow[] drClass = dtFilePlan.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型文件));
                    DownLoadFile(drClass, "classDocument", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "class", strProductPublicUrl, strFileSyncExecRecord_id, "教案class", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    DownLoadFile(drClass, "classDocument", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "htm", strProductPublicUrl, strFileSyncExecRecord_id, "教案class", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    //微课class+thm
                    DataRow[] drMicroClass = dtFilePlan.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型微课件));
                    DownLoadFile(drMicroClass, "microClassDocument", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "class", strProductPublicUrl, strFileSyncExecRecord_id, "微课class", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    DownLoadFile(drMicroClass, "microClassDocument", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "htm", strProductPublicUrl, strFileSyncExecRecord_id, "微课class", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);

                    //教案预览图片文件相关数据
                    //教案SW+预览图片
                    strSql = string.Format(@" select  t.ResourceToResourceFolder_img_id as FileName,t.ResourceToResourceFolder_Id 
,rtrf.Resource_Type
from ResourceToResourceFolder_img t
inner join ResourceToResourceFolder rtrf on t.ResourceToResourceFolder_id=rtrf.ResourceToResourceFolder_id
where t.ResourceToResourceFolder_id='{0}'", item["ResourceToResourceFolder_id"].ToString());
                    DataTable dtFilePlanView = new DataTable();

                    dtFilePlanView = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                    DataRow[] drSWdscView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.ScienceWord类型文件));
                    DownLoadFile(drSWdscView, "swView", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "教案SW预览图片", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    //教案Class+预览图片
                    DataRow[] drClassView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型文件));
                    DownLoadFile(drClassView, "classView", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "教案Class预览图片", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    //微课Class+预览图片
                    DataRow[] drMicroClassView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型微课件));
                    DownLoadFile(drMicroClassView, "microClassView", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "微课Class预览图片", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    #endregion
                    #region 成功更新SycnData 状态
                    string sql = " update SyncData set SyncStatus='3' where OperateType in('add','modify') and DataId='" + item["ResourceToResourceFolder_Id"] + "'";
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
        private void DownLoadFile(DataRow[] dvFile, string strPathType, string ParticularYear, string GradeTerm, string Resource_Version, string Subject, string strFileNameEx, string strProductPublicUrl, string strFileSyncExecRecord_id, string strRemark, string Book_Id, string Resource_Type, bool isCoverDownload)
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
                  , ParticularYear
                  , GradeTerm
                  , Resource_Version
                  , Subject);
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
                    model_FileSyncExecRecordDetail.Book_Id = Book_Id;
                    model_FileSyncExecRecordDetail.ResourceToResourceFolder_Id = item["ResourceToResourceFolder_Id"].ToString();
                    model_FileSyncExecRecordDetail.Resource_Type = Resource_Type;
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