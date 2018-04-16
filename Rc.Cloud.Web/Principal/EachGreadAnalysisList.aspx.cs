using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Web.Common;
using System.Web.Services;
using System.Data;
using Rc.BLL.Resources;
using Newtonsoft.Json;
using Rc.Model.Resources;
using System.Text;

namespace Rc.Cloud.Web.Principal
{
    public partial class EachGreadAnalysisList : Rc.Cloud.Web.Common.FInitData
    {
        public string UserId = string.Empty;
        public string GradeId = string.Empty;
        public string GradeName = string.Empty;
        Model_F_User user = new Model_F_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            GradeName = Request.QueryString["GradeName"].Filter();
            UserId = FloginUser.UserId;
            if (!IsPostBack)
            {
                ltlGradeName.Text = Server.UrlDecode(GradeName);
                GetSubjec();
            }
        }

        /// <summary>
        /// 学科
        /// </summary>
        public void GetSubjec()
        {
            ltlSubject.Text = StatsCommonHandle.GetTeacherSubjectData();

        }
        /// <summary>
        /// 分页得到班级作业成绩概况列表
        /// </summary>
        /// <param name="HWName">家庭作业名称</param>
        /// <param name="HomeWorkCreateTime">开始时间</param>
        /// <param name="HomeWorkFinishTime">结束时间</param>
        /// <param name="TeacherID">老师Id</param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]//string HomeWorkCreateTime, string HomeWorkFinishTime,
        public static string GetEachHWAnalysis(string HWName, string GradeID, string GradeName, string SubjectID, int PageIndex, int PageSize)
        {
            try
            {
                DataTable dt = new DataTable();
                HttpContext.Current.Session["StatsGradeSubject"] = SubjectID;
                HWName = HWName.Filter();
                GradeID = GradeID.Filter();
                GradeName = GradeName.Filter();
                SubjectID = SubjectID.Filter();

                string strWhere = "1=1 ";
                if (!string.IsNullOrEmpty(HWName))
                {
                    strWhere += " and  Resource_Name like '%" + HWName + "%'";
                }
                string strSqlMain = string.Format(@"select hw.ResourceToResourceFolder_Id,MAX(hw.CreateTime) as CreateTime,rtrf.Resource_Name
,cd.D_Name as SubjectName
from HomeWork hw
inner join ResourceToResourceFolder rtrf on hw.ResourceToResourceFolder_Id=rtrf.ResourceToResourceFolder_Id
left join Common_Dict cd on cd.Common_Dict_Id=hw.SubjectId
where hw.SubjectId='{0}' and hw.UserGroup_Id in(select ClassId from VW_ClassGradeSchool where GradeId='{1}') 
group by hw.ResourceToResourceFolder_Id,rtrf.Resource_Name,cd.D_Name ", SubjectID, GradeID);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( ");
                strSql.Append(" SELECT ROW_NUMBER() OVER (");
                strSql.Append("order by T.CreateTime desc");
                strSql.AppendFormat(")AS Row, T.*  from ({0}) T ", strSqlMain);
                strSql.Append(" WHERE " + strWhere);
                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize);

                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                StringBuilder strSqlCount = new StringBuilder();
                strSqlCount.AppendFormat("SELECT count(1)  from ( {0} ) T ", strSqlMain);
                strSqlCount.Append(" WHERE " + strWhere);

                int intRecordCount = int.Parse(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount.ToString()).ToString());

                //BLL_StatsGradeHW_Score bll = new BLL_StatsGradeHW_Score();
                //dt = bll.GetListByPage(strWhere, "HW_CreateTime desc", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                //int intRecordCount = bll.GetRecordCount(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strPG = GetCorrectClass(dt.Rows[i]["ResourceToResourceFolder_Id"].ToString(), GradeID);
                    listReturn.Add(new
                    {
                        HomeWork_Name = dt.Rows[i]["Resource_Name"].ToString().ReplaceForFilter(),
                        HW_CreatTime = pfunction.ToShortDate(dt.Rows[i]["CreateTime"].ToString()),
                        ResourceToResourceFolder_Id = dt.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        GradeName = GradeName,
                        SubjectName = dt.Rows[i]["SubjectName"].ToString(),
                        GradeId = GradeID,
                        PG = strPG,
                        SubjectId = SubjectID,
                        SubmitStudent = GetSubmitStudent(dt.Rows[i]["ResourceToResourceFolder_Id"].ToString(), GradeID)
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = intRecordCount,
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

        private static string GetCorrectClass(string ResourceToResourceFolder_Id, string GradeId)
        {
            string temp = string.Empty;
            try
            {
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query("select COUNT(1) as ICount,HomeWork_Status from HomeWork where ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and UserGroup_Id in(select ClassId from VW_ClassGradeSchool where GradeId='" + GradeId + "')  group by HomeWork_Status").Tables[0];
                DataRow[] drYP = dt.Select("HomeWork_Status='1'");
                DataRow[] drWP = dt.Select("HomeWork_Status='0'");
                temp = (drYP.Length > 0 ? drYP[0]["ICount"].ToString() : "0") + "/";
                temp += (drWP.Length > 0 ? drWP[0]["ICount"].ToString() : "0");
            }
            catch (Exception)
            {
                temp = "0/0";
            }
            return temp;
        }

        private static string GetSubmitStudent(string ResourceToResourceFolder_Id, string GradeId)
        {
            string temp = string.Empty;
            try
            {
                string strSql = string.Format(@"select count(1) from HomeWork hw
left join VW_ClassGradeSchool t1 on t1.ClassId=hw.UserGroup_Id
inner join Student_HomeWork shw on shw.HomeWork_Id=hw.HomeWork_Id 
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
where hw.ResourceToResourceFolder_Id='{0}' and t1.GradeId='{1}'
and shwSubmit.Student_HomeWork_Status='1' ", ResourceToResourceFolder_Id, GradeId);
                temp = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
            }
            catch (Exception)
            {
                temp = "0";
            }
            return temp;
        }

        /// <summary>
        /// 重新计算
        /// </summary>
        [WebMethod]
        public static string ReCalculation(string rtrfId, string gradeId)
        {
            try
            {
                rtrfId = rtrfId.Filter();
                gradeId = gradeId.Filter();
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                #region 按试卷 执行数据分析，记录日志
                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                Model_StatsLog modelLog = new Model_StatsLog();
                modelLog.StatsLogId = Guid.NewGuid().ToString();
                modelLog.DataId = modelRTRF.ResourceToResourceFolder_Id;
                modelLog.DataName = modelRTRF.File_Name.ReplaceForFilter();
                modelLog.DataType = "2";
                modelLog.LogStatus = "2";
                modelLog.CTime = DateTime.Now;
                modelLog.CUser = loginUser.UserId;
                modelLog.GradeId = gradeId;

                bool flag = new BLL_StatsLog().ReExecuteStatsAddLog(modelLog);
                #endregion
                if (flag)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("", "重新计算失败：" + ex.Message.ToString());
                return "0";
            }
        }

        /// <summary>
        /// 验证计算
        /// </summary>
        [WebMethod]
        public static string CheckCalculation(string rtrfId, string gradeId)
        {
            try
            {
                rtrfId = rtrfId.Filter();
                gradeId = gradeId.Filter();
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                #region 按试卷 执行数据分析，记录日志
                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                Model_StatsLog modelLog = new Model_StatsLog();
                modelLog.StatsLogId = Guid.NewGuid().ToString();
                modelLog.DataId = modelRTRF.ResourceToResourceFolder_Id;
                modelLog.DataName = modelRTRF.File_Name.ReplaceForFilter();
                modelLog.DataType = "2";
                modelLog.LogStatus = "2";
                modelLog.CTime = DateTime.Now;
                modelLog.CUser = loginUser.UserId;
                modelLog.GradeId = gradeId;

                bool flag = new BLL_StatsLog().ExecuteStatsAddLog(modelLog);
                #endregion
                if (flag)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("", "计算失败：" + ex.Message.ToString());
                return "0";
            }
        }


        [WebMethod]
        public static string CheckStatsHelper(string rtrfId)
        {
            try
            {
                rtrfId = rtrfId.Filter();
                int inum = new BLL_StatsHelper().GetRecordCount("Exec_Status='0' and SType='2' and ResourceToResourceFolder_Id='" + rtrfId + "' ");
                if (inum != 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("", "检查资源是否要重新统计失败：" + ex.Message.ToString());
                return "2";
            }
        }

    }
}