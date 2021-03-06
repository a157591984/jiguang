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
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncSchool_Auto : System.Web.UI.Page
    {
        public string strLocalPhsicalPath = string.Empty;
        public string SchoolId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            SchoolId = Request.QueryString["SchoolId"].Filter();
            strLocalPhsicalPath = Request.MapPath("\\Upload\\Resource\\");
            string strSql = string.Empty;
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='自动同步资源" + SchoolId + "' order by FileSyncExecRecord_TimeStart desc";
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
where FileSyncExecRecord_Type='自动同步资源" + SchoolId + "' and FileSyncExecRecord_Status='0' and DateDiff(MI,FileSyncExecRecord_TimeStart,getdate())>600 ";
                Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql);
            }
        }
        public void ResSynResource()
        {
            ResSynResourceTestPaper();
            ResSynResourcePlan();
        }
        /// <summary>
        /// 同步习题集
        /// </summary>
        public void ResSynResourceTestPaper()
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
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "自动同步资源" + SchoolId;
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步进行中...";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.FileSyncExecRecord_Condition = strCondition;
            model_FileSyncExecRecord.createUser = "";

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
                //bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);
                //string strWhere = string.Format(" and ub.UserId in(select UserId from dbo.VW_UserOnClassGradeSchool where SchoolId='{0}') ", SchoolId);
                string strSql = string.Empty;
                strSql = string.Format(@"select distinct rtrf.ResourceToResourceFolder_Id,rtrf.Resource_Type,rtrf.Resource_Class,rtrf.Resource_Version,rtrf.GradeTerm,rtrf.Subject,rtrf.ParticularYear,rtrf.Book_Id 
from ResourceToResourceFolder rtrf
inner join SyncData t on t.DataId=rtrf.ResourceToResourceFolder_Id and t.OperateType in('add','modify') and t.TableName='ResourceToResourceFolder' and t.SyncStatus=3
left join UserBuyResources ub on ub.Book_id=rtrf.Book_ID
inner join VW_UserOnClassGradeSchool vcgs on ub.UserId =vcgs.UserId  and  vcgs.SchoolId='{2}'
where rtrf.Resource_Type='{0}' and rtrf.Resource_Class='{1}' "
                    , Resource_TypeConst.testPaper类型文件
                    , Resource_ClassConst.云资源
                    , SchoolId);//and CONVERT(varchar(10),CreateTime,23)=CONVERT(varchar(10),DATEADD(day,-1,getDate()),23)) 
                DataTable dtResource = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
                foreach (DataRow item in dtResource.Rows)
                {
                    #region 下载文件 试卷类型
                    strSql = string.Format(@"select  TestQuestions_Id as FileName,ResourceToResourceFolder_Id from TestQuestions where  ResourceToResourceFolder_Id in('{0}')", item["ResourceToResourceFolder_Id"]);

                    DataTable dtFileTestPaper = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                    //试卷文件相关数据
                    DataRow[] dvFileTestPaper = dtFileTestPaper.Select();
                    //题干	BASE64内容，存储目录：testQuestionBody/属性层次目录/试题标识.txt
                    DownLoadFile(dvFileTestPaper, "testQuestionBody", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "txt", strProductPublicUrl, strFileSyncExecRecord_id, "题干txt", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    //题干 	HTML内容，存储目录：testQuestionBody/属性层次目录/试题标识.htm
                    DownLoadFile(dvFileTestPaper, "testQuestionBody", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "htm", strProductPublicUrl, strFileSyncExecRecord_id, "题干htm", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);

                    //题的选项等
                    strSql = string.Format(@"select t3.TestQuestions_Score_ID as FileName,t2.ResourceToResourceFolder_Id from TestQuestions t2
inner join TestQuestions_Score t3 on t2.TestQuestions_Id=t3.TestQuestions_Id
where t2.ResourceToResourceFolder_Id='{0}' and TestQuestions_Type!='title'", item["ResourceToResourceFolder_Id"]);
                    DataTable dtFileTestPaperScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                    DataRow[] dvFileTestPaperScore = dtFileTestPaperScore.Select();
                    //	选择题的选项（数组对象）存储目录：testQuestionOption/属性层次目录/试题标识.txt（存储JSION结构到文本文件，一个题只有一个文件）
                    DownLoadFile(dvFileTestPaperScore, "testQuestionOption", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "txt", strProductPublicUrl, strFileSyncExecRecord_id, "选择题选项txt", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    //	c)	标准答案（数组）存储目录：testQuestionCurrent/属性层次目录/试题标识.txt（存储JSION结构到文本文件，一个题只有一个文件）
                    DownLoadFile(dvFileTestPaperScore, "testQuestionCurrent", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "txt", strProductPublicUrl, strFileSyncExecRecord_id, "标准答案txt", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    //	解析 	BASE64内容AnalyzeData/属性层次目录/试题标识.txt
                    DownLoadFile(dvFileTestPaperScore, "AnalyzeData", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "txt", strProductPublicUrl, strFileSyncExecRecord_id, "解析txt", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    //	解析 	HTML内容AnalyzeHtml/属性层次目录/试题标识.htm
                    DownLoadFile(dvFileTestPaperScore, "AnalyzeHtml", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "htm", strProductPublicUrl, strFileSyncExecRecord_id, "解析htm", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    //	e)	强化训练 		BASE64内容TrainData/属性层次目录/试题标识.txt
                    DownLoadFile(dvFileTestPaperScore, "TrainData", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "txt", strProductPublicUrl, strFileSyncExecRecord_id, "强化训练txt", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);
                    //	e)	强化训练 		HTML内容TrainHtml/属性层次目录/试题标识.htm
                    DownLoadFile(dvFileTestPaperScore, "TrainHtml", item["ParticularYear"].ToString(), item["GradeTerm"].ToString(), item["Resource_Version"].ToString(), item["Subject"].ToString(), "htm", strProductPublicUrl, strFileSyncExecRecord_id, "强化训练htm", item["Book_Id"].ToString(), item["Resource_Type"].ToString(), isCover);

                    #endregion

                    #region 成功更新SycnData 状态
                    string sql = " update SyncData set SyncStatus='4' where OperateType in('add','modify') and DataId='" + item["ResourceToResourceFolder_Id"] + "' ";
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
        /// 同步教案
        /// </summary>
        public void ResSynResourcePlan()
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
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "自动同步资源" + SchoolId;
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
                //string strWhere = string.Format(" and ub.UserId in(select distinct UserId from dbo.VW_UserOnClassGradeSchool where SchoolId='{0}') ", SchoolId);
                strSql = string.Format(@"select distinct rtrf.ResourceToResourceFolder_Id,rtrf.Resource_Type,rtrf.Resource_Class,rtrf.Resource_Version,rtrf.GradeTerm,rtrf.Subject,rtrf.ParticularYear,rtrf.Book_Id 
from ResourceToResourceFolder rtrf
inner join SyncData t on t.DataId=rtrf.ResourceToResourceFolder_Id and t.OperateType in('add','modify') and t.TableName='ResourceToResourceFolder' and t.SyncStatus=3
left join UserBuyResources ub on ub.Book_id=rtrf.Book_ID
inner join VW_UserOnClassGradeSchool vcgs on ub.UserId =vcgs.UserId  and  vcgs.SchoolId='{4}'
where rtrf.Resource_Type in ('{0}','{1}','{2}') and rtrf.Resource_Class='{3}' "
                    , Resource_TypeConst.ScienceWord类型文件
                    , Resource_TypeConst.class类型文件
                    , Resource_TypeConst.class类型微课件
                    , Resource_ClassConst.云资源
                    , SchoolId);//and CONVERT(varchar(10),CreateTime,23)=CONVERT(varchar(10),DATEADD(day,-1,getDate()),23)) 
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
                    string sql = " update SyncData set SyncStatus='4' where OperateType in('add','modify') and DataId='" + item["ResourceToResourceFolder_Id"] + "'";
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