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
    public partial class DownloadHWFiles : System.Web.UI.Page
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
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status ='0' and FileSyncExecRecord_Type='同步作业相关文件" + SchoolId + "' order by FileSyncExecRecord_TimeStart desc";
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
        /// 同步作业相关文件
        /// </summary>
        public void ResSynchroFile()
        {
            // web站点存放文件的主目录
            string strProductPublicUrl = sourceServerUrl;
            string strFileSyncExecRecord_id = Guid.NewGuid().ToString();
            string strCondition = string.Format("学校标识：{0}，来源服务器：{1}，目标服务器：{2}", SchoolId, sourceServerUrl, targetServerUrl);
            #region 记录同步开始信息
            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "同步作业相关文件" + SchoolId;
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.createUser = SysUser_ID;
            model_FileSyncExecRecord.FileSyncExecRecord_Condition = strCondition;
            bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);
            #endregion

            try
            {
                #region 下载作业文件
                // 作业数据
                DataTable dtHW = GetDtHW();
                DataRow[] dvHW = dtHW.Select();
                // 学生作业文件    JSON结构内容，存储目录：studentPaper/作业时间/作业标识.txt
                DownLoadFile(dvHW, "studentPaper", "hwTime", "hwId", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "学生作业txt");
                // 老师作业文件    JSON结构内容，存储目录：studentPaper/作业时间/作业标识.htm
                DownLoadFile(dvHW, "studentPaper", "hwTime", "hwId", "tch.txt", strProductPublicUrl, strFileSyncExecRecord_id, "老师作业txt");

                // 学生作业数据
                DataTable dtStuHW = GetDtStuHW();
                DataRow[] dvStuHW = dtStuHW.Select();
                // 学生答题文件   JSON结构内容，存储目录：studentAnswerForSubmit/作业时间/作业标识/学生作业标识.txt
                DownLoadFile(dvStuHW, "studentAnswerForSubmit", "hwTime,hwId", "shwId", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "学生答题txt");
                // 学生答题批改文件   JSON结构内容，存储目录：studentAnswerForMarking/作业时间/学生作业标识/学生作业标识.txt
                DownLoadFile(dvStuHW, "studentAnswerForMarking", "hwTime,shwId", "shwId", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "学生答题For批改txt");
                // 老师批改笔记   BASE64内容，存储目录：teacherMarking/作业时间/学生作业标识/学生作业标识.txt
                DownLoadFile(dvStuHW, "teacherMarking", "hwTime,shwId", "shwId", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "老师批改笔记txt");

                // 学生作业答题数据
                DataTable dtStuHWAnswer = GetDtStuHWAnswer();
                DataRow[] dvStuHWAnswer = dtStuHWAnswer.Select();
                // 学生答题答案文件   html内容，存储目录：studentAnswer/作业时间/云资源属性层次目录/学生作业答题标识.txt（一个答题空有一个文件）
                DownLoadFile(dvStuHWAnswer, "studentAnswer", "hwTime,ParticularYear,GradeTerm,Resource_Version,Subject", "shwaId", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "学生答题-答案文件txt");
                // 老师批改批注   BASE64内容，存储目录：teacherMarking/作业时间/学生作业标识/试题标识.txt（一个试题有一个文件）
                DownLoadFile(dvStuHW, "teacherMarking", "hwTime,shwId", "tqId", "txt", strProductPublicUrl, strFileSyncExecRecord_id, "老师批改批注txt");

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
        /// 下载
        /// </summary>
        /// <param name="dvFile"></param>
        /// <param name="strPathType"></param>
        /// <param name="strFileNameEx"></param>
        /// <param name="strProductPublicUrl"></param>
        /// <param name="strFileSyncExecRecord_id"></param>
        /// <param name="strRemark"></param>
        /// <param name="pathColumn">存储路径数据字段</param>
        private void DownLoadFile(DataRow[] dvFile, string strPathType, string pathColumn, string strFileNameColumn, string strFileNameEx, string strProductPublicUrl, string strFileSyncExecRecord_id, string strRemark)
        {
            foreach (var item in dvFile)
            {
                #region 组织文件源的URL与本地存储的路径
                string strPathCommon = string.Empty;
                string strFileName = string.Empty;//文件名（没后缀）
                strFileName = item[strFileNameColumn].ToString();
                strPathCommon = string.Format("{0}/", strPathType);
                string[] arrPathColumn = pathColumn.Split(',');
                if (strPathType == "studentAnswer" && item["Resource_Class"].ToString() == Rc.Common.Config.Resource_ClassConst.自有资源)
                {
                    strPathCommon += string.Format("{0}/", pfunction.ToShortDate(item["hwTime"].ToString()));
                }
                else
                {
                    foreach (var strColumn in arrPathColumn)
                    {
                        if (strColumn == "hwTime")
                        {
                            strPathCommon += string.Format("{0}/", pfunction.ToShortDate(item[strColumn].ToString()));
                        }
                        else
                        {
                            strPathCommon += string.Format("{0}/", item[strColumn].ToString());
                        }
                    }
                }

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
                model_FileSyncExecRecordDetail.TestQuestions_Id = item["tqId"].ToString();
                model_FileSyncExecRecordDetail.Resource_Type = item["Resource_Type"].ToString();
                model_FileSyncExecRecordDetail.FileUrl = strPathCommon + strFileNameFullDocument;
                model_FileSyncExecRecordDetail.Detail_Remark = strRemark;
                model_FileSyncExecRecordDetail.Detail_Status = "0";
                model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
                bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

                if (File.Exists(localFilePath + strFileNameFullDocument))
                {
                    model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
                    model_FileSyncExecRecordDetail.Detail_Remark = "文件已存在";
                    model_FileSyncExecRecordDetail.Detail_Status = "1";
                    bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
                }
                else
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
            }
        }

        /// <summary>
        /// 作业数据
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtHW()
        {
            #region 读取作业数据
            string strSql = string.Empty;
            string strWhere = " 1=1 ";
            if (!string.IsNullOrEmpty(SchoolId))
            {
                strWhere += string.Format(" and hw.HomeWork_AssignTeacher in(select UserId from dbo.VW_UserOnClassGradeSchool where ClassMemberShipEnum<>'student' and SchoolId='{0}') ", SchoolId);
            }
            else
            {
                strWhere += " and hw.HomeWork_AssignTeacher in(select UserId from dbo.VW_UserOnClassGradeSchool vw left join ConfigSchool t on t.School_Id=vw.SchoolId where vw.ClassMemberShipEnum<>'student' and t.School_Id is null ) ";
            }
            strSql = string.Format(@"select distinct hw.HomeWork_Id as hwId,hw.Createtime as hwTime 
,'' as shwId,'' as tqId 
,rtrf.ParticularYear,rtrf.GradeTerm,rtrf.Resource_Version,rtrf.Subject,rtrf.ResourceToResourceFolder_Id,rtrf.Resource_Class,rtrf.Book_Id,rtrf.Resource_Type 
from HomeWork hw 
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id 
where {0} order by hw.Createtime desc ", strWhere);

            DataTable dtHW = new DataTable();

            dtHW = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            return dtHW;
            #endregion
        }
        /// <summary>
        /// 学生作业数据
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtStuHW()
        {
            #region 读取学生作业数据
            string strSql = string.Empty;
            string strWhere = " 1=1 ";
            if (!string.IsNullOrEmpty(SchoolId))
            {
                strWhere += string.Format(" and hw.HomeWork_AssignTeacher in(select UserId from dbo.VW_UserOnClassGradeSchool where ClassMemberShipEnum<>'student' and SchoolId='{0}') ", SchoolId);
            }
            else
            {
                strWhere += " and hw.HomeWork_AssignTeacher in(select UserId from dbo.VW_UserOnClassGradeSchool vw left join ConfigSchool t on t.School_Id=vw.SchoolId where vw.ClassMemberShipEnum<>'student' and t.School_Id is null ) ";
            }
            strSql = string.Format(@"select distinct hw.HomeWork_Id as hwId,hw.Createtime as hwTime 
,shw.Student_HomeWork_Id as shwId,'' as tqId 
,rtrf.ParticularYear,rtrf.GradeTerm,rtrf.Resource_Version,rtrf.Subject,rtrf.ResourceToResourceFolder_Id,rtrf.Resource_Class,rtrf.Book_Id,rtrf.Resource_Type 
from HomeWork hw 
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id 
inner join Student_HomeWork shw on shw.HomeWork_Id=hw.HomeWork_Id 
where {0} order by hw.Createtime desc ", strWhere);

            DataTable dtStuHW = new DataTable();

            dtStuHW = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            return dtStuHW;
            #endregion
        }
        /// <summary>
        /// 学生作业答题数据
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtStuHWAnswer()
        {
            #region 读取学生作业答题数据
            string strSql = string.Empty;
            string strWhere = " 1=1 ";
            if (!string.IsNullOrEmpty(SchoolId))
            {
                strWhere += string.Format(" and hw.HomeWork_AssignTeacher in(select UserId from dbo.VW_UserOnClassGradeSchool where ClassMemberShipEnum<>'student' and SchoolId='{0}') ", SchoolId);
            }
            else
            {
                strWhere += " and hw.HomeWork_AssignTeacher in(select UserId from dbo.VW_UserOnClassGradeSchool vw left join ConfigSchool t on t.School_Id=vw.SchoolId where vw.ClassMemberShipEnum<>'student' and t.School_Id is null ) ";
            }
            strSql = string.Format(@"select distinct hw.HomeWork_Id as hwId,hw.Createtime as hwTime 
,shw.Student_HomeWork_Id as shwId,shwa.TestQuestions_Id as tqId,shwa.Student_HomeWorkAnswer_Id as shwaId 
,rtrf.ParticularYear,rtrf.GradeTerm,rtrf.Resource_Version,rtrf.Subject,rtrf.ResourceToResourceFolder_Id,rtrf.Resource_Class,rtrf.Book_Id,rtrf.Resource_Type 
from HomeWork hw 
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id 
inner join Student_HomeWork shw on shw.HomeWork_Id=hw.HomeWork_Id 
inner join Student_HomeWorkAnswer shwa on shwa.Student_HomeWork_Id=shw.Student_HomeWork_Id 
where {0} order by hw.Createtime desc ", strWhere);

            DataTable dtStuHWAnswer = new DataTable();

            dtStuHWAnswer = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            return dtStuHWAnswer;
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


                strWhere += " and f.FileSyncExecRecord_Type='同步作业相关文件" + SchoolId + "' ";
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