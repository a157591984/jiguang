using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.parent
{
    public partial class JobTrack : Rc.Cloud.Web.Common.FInitData
    {
        protected string UserID = string.Empty;
        Model_F_User loginUser = new Model_F_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            loginUser = FloginUser;
            if (!IsPostBack)
            {
                LoadMyBaby();
            }
        }
        [WebMethod]
        /// <summary>
        /// 加载学生购买过的课程的学科
        /// </summary>
        public static string GetSubject(string BabyId)
        {
            try
            {
                BabyId = BabyId.Filter();
                string SubjectName = string.Empty;
                string StrSql = @"select distinct(cd.D_Name),cd.Common_Dict_ID from Student_HomeWork  shw
left join HomeWork hw on hw.HomeWork_Id=shw.HomeWork_Id
left join ResourceToResourceFolder re on re.ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id
left join dbo.Common_Dict cd on cd.Common_Dict_ID=re.Subject where Student_Id='" + BabyId + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SubjectName += string.Format("<li><a href='##' SubjectId='{0}'>{1}</a></li>"
                            , dt.Rows[i]["Common_Dict_ID"]
                            , dt.Rows[i]["D_Name"]);
                    }
                }
                return "<li><a href='##' class=\"active\" SubjectId='-1'>全部</a></li>" + SubjectName;
            }
            catch (Exception)
            {

                return "";
            }
        }
        /// <summary>
        /// 加载宝贝
        /// </summary>
        private void LoadMyBaby()
        {
            try
            {
                string BabyName = string.Empty;
                string StrSql = @"select ISNULL(fu.TrueName,fu.UserName) BabyName,fu.UserId,stp.Parent_ID from F_User fu
inner join StudentToParent stp on stp.Student_ID=fu.UserId where Parent_ID='" + loginUser.UserId + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BabyName += string.Format("<li><a href='##' BabyId='{0}'>{1}</a></li>"
                            , dt.Rows[i]["UserId"]
                            , dt.Rows[i]["BabyName"]);
                    }
                }
                ltlBabyName.Text = BabyName;
            }
            catch (Exception)
            {

                throw;
            }
        }


        [WebMethod]
        public static string GetHWList(string StudentID, string SubjectID, string HomeWorkCreateTime, string HomeWorkFinishTime, int PageIndex, int PageSize)
        {
            try
            {
                StudentID = StudentID.Filter();
                SubjectID = SubjectID.Filter();
                HomeWorkCreateTime = HomeWorkCreateTime.Filter();
                HomeWorkCreateTime = HomeWorkCreateTime.Filter();
                string strWhere = "1=1";
                if (!string.IsNullOrEmpty(StudentID))
                {
                    strWhere += " AND  StudentID = '" + StudentID + "'";
                }
                if (!string.IsNullOrEmpty(SubjectID) && SubjectID != "-1")
                {
                    strWhere += " AND  SubjectID = '" + SubjectID + "'";
                }
                if (!string.IsNullOrEmpty(HomeWorkCreateTime))
                {
                    strWhere += " and HomeWorkCreateTime > '" + HomeWorkCreateTime + "'";
                }
                if (!string.IsNullOrEmpty(HomeWorkFinishTime))
                {
                    strWhere += " and HomeWorkCreateTime <= '" + HomeWorkFinishTime + "'";
                }
                DataTable dt = new DataTable();
                BLL_StatsClassStudentHW_Score bll = new BLL_StatsClassStudentHW_Score();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( ");
                strSql.Append(" SELECT ROW_NUMBER() OVER ( order by HomeWorkCreateTime desc ");

                strSql.Append(@")AS Row, T.*,shwCorrect.Student_HomeWork_CorrectStatus  from StatsClassStudentHW_Score T  
                                                 left join Student_HomeWork shw on shw.HomeWork_Id=T.HomeWork_Id and Student_Id='" + StudentID + @"' 
                                                 inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id  ");
                if (!string.IsNullOrEmpty(strWhere.Trim()))
                {
                    strSql.Append(" WHERE " + strWhere);
                }

                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize));
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                string SqlCount = @"select count(1) from StatsClassStudentHW_Score T   where " + strWhere;
                int intRecordCount = (int)Rc.Common.DBUtility.DbHelperSQL.GetSingle(SqlCount);

                string StrSql = @"select shw.Student_HomeWork_Id,shw.HomeWork_Id,shw.Student_Id,shw.CreateTime,shwCorrect.CorrectMode from Student_HomeWork shw
 inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id where Student_Id='" + StudentID + "'";
                DataTable dtSHW = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];

                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                string cProgress = string.Empty;
                string gProgress = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cProgress = dt.Rows[i]["ScoreImprove"].ToString();
                    gProgress = dt.Rows[i]["GradeScoreImprove"].ToString();
                    if (Convert.ToInt16(cProgress) > 0)
                    {
                        cProgress = "<span class='text-success'><i class='fa fa-arrow-up'></i>&nbsp;" + cProgress + "</span>";
                    }
                    else if (Convert.ToInt16(cProgress) < 0)
                    {
                        cProgress = "<span class='text-danger'><i class='fa fa-arrow-down'></i>&nbsp;" + Math.Abs(Convert.ToInt16(cProgress)) + "</span>";
                    }
                    else
                    {
                        cProgress = "-";
                    }
                    if (Convert.ToInt16(gProgress) > 0)
                    {
                        gProgress = "<span class='text-success'><i class='fa fa-arrow-up'></i>&nbsp;" + gProgress + "</span>";
                    }
                    else if (Convert.ToInt16(gProgress) < 0)
                    {
                        gProgress = "<span class='text-danger'><i class='fa fa-arrow-down'></i>&nbsp;" + Math.Abs(Convert.ToInt16(gProgress)) + "</span>";
                    }
                    else
                    {
                        gProgress = "-";
                    }
                    string strCorrectMode = string.Empty;
                    string strStudent_Homework_Id = string.Empty;
                    DataRow[] drSHW = dtSHW.Select("Homework_Id='" + dt.Rows[i]["HomeWork_ID"].ToString() + "' and Student_Id='" + dt.Rows[i]["StudentID"].ToString() + "' ");
                    if (drSHW.Length > 0)
                    {
                        strCorrectMode = drSHW[0]["CorrectMode"].ToString();
                        strStudent_Homework_Id = drSHW[0]["Student_Homework_Id"].ToString();
                    }
                    double sec = 0;
                    try
                    {
                        TimeSpan ts1 = Convert.ToDateTime(dt.Rows[i]["AnswerEnd"].ToString()) - Convert.ToDateTime(dt.Rows[i]["AnswerStart"].ToString());
                        sec = ts1.TotalSeconds;
                    }
                    catch (Exception)
                    {

                    }

                    listReturn.Add(new
                    {
                        HomeWork_Name = dt.Rows[i]["HomeWork_Name"].ToString(),
                        AnswerStart = dt.Rows[i]["AnswerStart"].ToString(),
                        AnswerEnd = dt.Rows[i]["AnswerEnd"].ToString(),
                        StudentIP = dt.Rows[i]["StudentIP"].ToString(),
                        StudentScore = dt.Rows[i]["StudentScore"].ToString().clearLastZero(),
                        StudentScoreOrder = dt.Rows[i]["StudentScoreOrder"].ToString().clearLastZero(),
                        ScoreImprove = "[" + cProgress + "]",
                        Hierarchy = dt.Rows[i]["Hierarchy"].ToString().clearLastZero() == "0" ? "-" : "第" + dt.Rows[i]["Hierarchy"].ToString() + "层次",
                        GradeStudentScoreOrder = dt.Rows[i]["GradeStudentScoreOrder"].ToString().clearLastZero() == "0" ? "-" : dt.Rows[i]["GradeStudentScoreOrder"].ToString().clearLastZero(),
                        GradeScoreImprove = "[" + gProgress + "]",
                        GradeHierarchy = dt.Rows[i]["GradeHierarchy"].ToString().clearLastZero() == "0" ? "-" : "第" + dt.Rows[i]["GradeHierarchy"].ToString() + "层次",
                        HomeWork_ID = dt.Rows[i]["HomeWork_ID"].ToString(),
                        StudentID = dt.Rows[i]["StudentID"].ToString(),
                        ResourceToResourceFolder_Id = dt.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        CorrectMode = strCorrectMode,
                        Student_Homework_Id = strStudent_Homework_Id,
                        DateDiffMin = Rc.Cloud.Web.Common.pfunction.ConvertSecond(sec.ToString()),
                        Student_HomeWork_CorrectStatus = dt.Rows[i]["Student_HomeWork_CorrectStatus"].ToString()
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


    }
}