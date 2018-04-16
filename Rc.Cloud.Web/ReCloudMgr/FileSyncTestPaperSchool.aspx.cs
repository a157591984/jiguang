﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data;
using System.IO;
using Rc.Cloud.Web.Common;
using Rc.Common.Config;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncTestPaperSchool : System.Web.UI.Page
    {
        protected string strFileSyncExecRecord_id = string.Empty;
        string strLocalPhsicalPath = string.Empty;
        protected string SchoolId = string.Empty;
        string SysUser_ID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            SchoolId = Request.QueryString["SchoolId"].Filter();
            SysUser_ID = Request.QueryString["SysUser_ID"].Filter();
            strLocalPhsicalPath = Request.MapPath("\\Upload\\Resource\\");
            string strSql = string.Empty;
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='同步试卷" + SchoolId + "' order by FileSyncExecRecord_TimeStart desc";
            string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
            if (str != "")
            {
                btn.Text = "试卷文件正在同步中，同步开始时间为" + str;
                btn.Enabled = false;

                strSql = @"update FileSyncExecRecord set FileSyncExecRecord_Status='2',FileSyncExecRecord_Remark='已执行120分钟，超时，自动更新状态'
where FileSyncExecRecord_Type='同步试卷" + SchoolId + "' and FileSyncExecRecord_Status='0' and DateDiff(MI,FileSyncExecRecord_TimeStart,getdate())>120 ";
                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql) > 0)
                {
                    Response.Redirect(Request.Url.ToString());
                }
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {

            btn.Text = "试卷文件正在同步中，同步开始时间为" + DateTime.Now.ToShortDateString();
            btn.Enabled = false;

            Thread thread1 = new Thread(new ThreadStart(ResSynchroTeachingPlan));
            if (!thread1.IsAlive)
            {
                thread1.Start();

            }
        }

        /// <summary>
        /// 同步习题集
        /// </summary>
        public void ResSynchroTeachingPlan()
        {
            //生产环境web站点存放文件的主目录
            string strProductPublicUrl = ConfigurationManager.AppSettings["ProductPublicUrl"].ToString();
            string strCondition = string.Empty;
            strCondition = string.Format("日期【{0}】；书本【{1}】【{2}】；是覆盖：【{3}】", txtSDate.Value, txtBOOK.Value, hidtxtBOOK.Value, chbCover.Checked);
            string strFileSyncExecRecord_id = Guid.NewGuid().ToString();
            bool isCover = false;
            isCover = chbCover.Checked;
            #region 记录同步开始信息

            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "同步试卷" + SchoolId;
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步进行中...";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.FileSyncExecRecord_Condition = strCondition;
            model_FileSyncExecRecord.createUser = SysUser_ID;

            #endregion
            try
            {

                bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);
                //试卷文件相关数据
                DataTable dtFileTestPaper = GetDtFileTestPaperTestQuestion();
                DataRow[] dvFileTestPaper = dtFileTestPaper.Select();

                #region 下载文件
                //题干	BASE64内容，存储目录：testQuestionBody/属性层次目录/试题标识.txt
                DownLoadFile(dvFileTestPaper, "testQuestionBody", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "题干txt", isCover);
                //题干 	HTML内容，存储目录：testQuestionBody/属性层次目录/试题标识.htm
                DownLoadFile(dvFileTestPaper, "testQuestionBody", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "题干htm", isCover);

                DataTable dtFileTestPaperScore = GetDtFileTestPaperTestQuestions_Score();
                DataRow[] dvFileTestPaperScore = dtFileTestPaperScore.Select();
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
        private DataTable GetDtFileTestPaperTestQuestion()
        {
            #region 读取需要同步的数据
            string strSql = string.Empty;
            string strSqlTemp = string.Empty;
            string strDate = txtSDate.Value.Trim().Filter();
            if (hidtxtBOOK.Value != "")
            {
                strSqlTemp += string.Format(" and t1.Book_id='{0}'", hidtxtBOOK.Value.Trim().Filter());
            }
            if (!string.IsNullOrEmpty(strDate))
            {
                strSqlTemp += string.Format(" and convert(varchar(10),ba.CreateTime,120)='{0}' ", strDate);
            }
            //strSqlTemp += string.Format(" and ub.UserId in(select UserId from dbo.VW_UserOnClassGradeSchool where SchoolId='{0}') ", SchoolId);

            strSql = string.Format(@"select distinct t1.Book_Id,Resource_Type, ParticularYear,GradeTerm,Resource_Version,Subject,t1.ResourceToResourceFolder_id
,t2.TestQuestions_ID,t2.TestQuestions_ID as FileName ,t2.TestQuestions_Type,t2.Createtime
from ResourceToResourceFolder t1 
inner join TestQuestions t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
left join BookAudit ba on ba.ResourceFolder_Id=t1.Book_Id
left join UserBuyResources ub on ub.Book_id=t1.Book_ID
inner join VW_UserOnClassGradeSchool vcgs on ub.UserId =vcgs.UserId  and  vcgs.SchoolId='{1}'
where 1=1
and t1.Resource_Type='{0}'
and ba.AuditState='1'
{2}
order by t2.Createtime desc", Resource_TypeConst.testPaper类型文件, SchoolId, strSqlTemp);

            DataTable dtFileTestPaper = new DataTable();

            dtFileTestPaper = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
            return dtFileTestPaper;
            #endregion
        }
        /// <summary>
        /// 试卷试题选项，标准答案，解析，强化训练
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFileTestPaperTestQuestions_Score()
        {
            #region 读取需要同步的数据
            string strSql = string.Empty;
            string strSqlTemp = string.Empty;
            string strDate = txtSDate.Value.Trim().Filter();
            if (hidtxtBOOK.Value != "")
            {
                strSqlTemp += string.Format(" and t1.Book_id='{0}'", hidtxtBOOK.Value.Trim().Filter());
            }
            if (!string.IsNullOrEmpty(strDate))
            {
                strSqlTemp += string.Format(" and convert(varchar(10),ba.CreateTime,120)='{0}' ", strDate);
            }
            //strSqlTemp += string.Format(" and ub.UserId in(select UserId from dbo.VW_UserOnClassGradeSchool where SchoolId='{0}') ", SchoolId);

            strSql = string.Format(@"select distinct t1.Book_Id,Resource_Type, ParticularYear,GradeTerm,Resource_Version,Subject,t1.ResourceToResourceFolder_id
,t2.TestQuestions_ID,t3.TestQuestions_Score_ID as FileName,t2.TestQuestions_Type,t2.Createtime
from ResourceToResourceFolder t1 
inner join TestQuestions t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
left join TestQuestions_Score t3 on t2.TestQuestions_Id=t3.TestQuestions_Id
left join BookAudit ba on ba.ResourceFolder_Id=t1.Book_Id
left join UserBuyResources ub on ub.Book_id=t1.Book_ID
inner join VW_UserOnClassGradeSchool vcgs on ub.UserId =vcgs.UserId  and  vcgs.SchoolId='{1}'
where 1=1
and t1.Resource_Type='{0}'
and t2.TestQuestions_Type!='title' and t2.TestQuestions_Type!=''
and ba.AuditState='1'
{2}
order by t2.Createtime desc", Resource_TypeConst.testPaper类型文件, SchoolId, strSqlTemp);

            DataTable dtFileTestPaper = new DataTable();

            dtFileTestPaper = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
            return dtFileTestPaper;
            #endregion
        }
        /// <summary>
        /// 加载数据
        /// </summary>
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


                strWhere += " and f.FileSyncExecRecord_Type='同步试卷" + SchoolId + "' ";
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

        /// <summary>
        /// 重新同步失败习题集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDataReExec_Click(object sender, EventArgs e)
        {
            btn.Text = "试卷文件正在同步中，同步开始时间为" + DateTime.Now.ToShortDateString();
            btn.Enabled = false;

            Thread threadRe = new Thread(new ThreadStart(SynchronousDataReExec));
            if (!threadRe.IsAlive)
            {
                threadRe.Start();

            }
        }
        public void SynchronousDataReExec()
        {
            //生产环境web站点存放文件的主目录
            string strProductPublicUrl = ConfigurationManager.AppSettings["ProductPublicUrl"].ToString();
            string strFileSyncExecRecord_id = hidId.Value;
            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord = bll_FileSyncExecRecord.GetModel(strFileSyncExecRecord_id);
            bool isCover = false;
            string strCondition = model_FileSyncExecRecord.FileSyncExecRecord_Condition;
            txtSDate.Value = strCondition.Substring(strCondition.IndexOf("【") + 1, strCondition.IndexOf("】") - strCondition.IndexOf("【") - 1);
            strCondition = strCondition.Substring(strCondition.IndexOf("】") + 1);
            txtBOOK.Value = strCondition.Substring(strCondition.IndexOf("【") + 1, strCondition.IndexOf("】") - strCondition.IndexOf("【") - 1);
            strCondition = strCondition.Substring(strCondition.IndexOf("】") + 1);
            hidtxtBOOK.Value = strCondition.Substring(strCondition.IndexOf("【") + 1, strCondition.IndexOf("】") - strCondition.IndexOf("【") - 1);
            strCondition = strCondition.Substring(strCondition.IndexOf("】") + 1);
            string strCover = strCondition.Substring(strCondition.IndexOf("【") + 1, strCondition.IndexOf("】") - strCondition.IndexOf("【") - 1);
            bool.TryParse(strCover, out isCover);
            chbCover.Checked = isCover;

            strCondition = string.Format("日期【{0}】；书本【{1}】【{2}】；是覆盖：【{3}】", txtSDate.Value, txtBOOK.Value, hidtxtBOOK.Value, chbCover.Checked);
            #region 记录同步开始信息

            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "同步试卷" + SchoolId;
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步进行中...";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = null;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.FileSyncExecRecord_Condition = strCondition;
            model_FileSyncExecRecord.createUser = SysUser_ID;

            #endregion
            try
            {
                Rc.Common.DBUtility.DbHelperSQL.ExecuteSql("delete from FileSyncExecRecordDetail where FileSyncExecRecord_id='" + hidId.Value + "' ");
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
                //试卷文件相关数据
                DataTable dtFileTestPaper = GetDtFileTestPaperTestQuestion();
                DataRow[] dvFileTestPaper = dtFileTestPaper.Select();

                #region 下载文件
                //题干	BASE64内容，存储目录：testQuestionBody/属性层次目录/试题标识.txt
                DownLoadFile(dvFileTestPaper, "testQuestionBody", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "题干txt", isCover);
                //题干 	HTML内容，存储目录：testQuestionBody/属性层次目录/试题标识.htm
                DownLoadFile(dvFileTestPaper, "testQuestionBody", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "题干htm", isCover);

                DataTable dtFileTestPaperScore = GetDtFileTestPaperTestQuestions_Score();
                DataRow[] dvFileTestPaperScore = dtFileTestPaperScore.Select();
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

    }
}