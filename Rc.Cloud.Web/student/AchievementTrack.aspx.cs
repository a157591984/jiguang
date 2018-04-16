using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
using System.Text;
namespace Rc.Cloud.Web.student
{
    public partial class AchievementTrack : Rc.Cloud.Web.Common.FInitData
    {
        Model_F_User loginUser = new Model_F_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            loginUser = FloginUser;
            if (!IsPostBack)
            {
                LoadSubject();
            }
        }
        /// <summary>
        /// 存在的学科
        /// </summary>
        private void LoadSubject()
        {
            try
            {
                string SubjectName = string.Empty;
                string StrSql = @"select distinct SubjectID,SubjectName from StatsClassStudentHW_Score where StudentID='" + loginUser.UserId + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SubjectName += string.Format("<li><a href='##' SubjectId='{0}'>{1}</a></li>"
                            , dt.Rows[i]["SubjectID"]
                            , dt.Rows[i]["SubjectName"]);
                    }
                }
                ltlSubjectName.Text = "<li><a href='##' class=\"active\" SubjectId='-1'>全部</a></li>" + SubjectName;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        public static string GetStatsClassStudentHW_Score(string SubjectId, string HomeWorkCreateTime, string HomeWorkFinishTime, int PageIndex, int PageSize)
        {
            try
            {
                Model_F_User loginUser = (Model_F_User)HttpContext.Current.Session["FloginUser"];
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                SubjectId = SubjectId.Filter();
                HomeWorkCreateTime = HomeWorkCreateTime.Filter();
                HomeWorkCreateTime = HomeWorkCreateTime.Filter();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                BLL_StatsClassStudentHW_Score bllStatsClassStudentHW_Score = new BLL_StatsClassStudentHW_Score();

                string strSqlCount = string.Empty;
                string strWhere = " StudentID='" + loginUser.UserId + "'";
                if (!string.IsNullOrEmpty(SubjectId) && SubjectId != "-1")
                {
                    strWhere += "and SubjectID='" + SubjectId + "'"; ;
                }
                if (!string.IsNullOrEmpty(HomeWorkCreateTime))
                {
                    strWhere += " and HomeWorkCreateTime > '" + HomeWorkCreateTime + "'";
                }
                if (!string.IsNullOrEmpty(HomeWorkFinishTime))
                {
                    strWhere += " and HomeWorkCreateTime <= '" + HomeWorkFinishTime + "'";
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( ");
                strSql.Append(" SELECT ROW_NUMBER() OVER ( order by HomeWorkCreateTime desc ");

                strSql.Append(@")AS Row, T.*  from (select scss.StudentID,scss.HomeWorkCreateTime,scss.SubjectID,scss.SubjectName,scss.ClassName,scss.HomeWork_Name
,scss.HWScore,scss.StudentScore,scss.GradeStudentScoreOrder,scss.StudentScoreOrder,scs.AVGScore classavg,scs.AVGScoreRate as classavgrate,sgs.AVGScore as gradeavg 
from StatsClassStudentHW_Score scss
left join dbo.StatsClassHW_Score scs on scs.ClassID=scss.ClassID and scs.HomeWork_ID=scs.HomeWork_ID and scs.ResourceToResourceFolder_Id=scss.ResourceToResourceFolder_Id
left join dbo.StatsGradeHW_Score sgs on sgs.Gradeid=scss.Gradeid and sgs.ResourceToResourceFolder_Id=scss.ResourceToResourceFolder_Id) T ");
                if (!string.IsNullOrEmpty(strWhere.Trim()))
                {
                    strSql.Append(" WHERE " + strWhere);
                }
                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize));
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                int rCount = bllStatsClassStudentHW_Score.GetRecordCount(strWhere);
                int inum = 0;
                string HomeWork_NameArray = string.Empty;
                string StudentScoreArray = string.Empty;
                string StudentScoreRateArray = string.Empty;
                string classavgArray = string.Empty;
                //string gradeavgArray = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HomeWork_NameArray += dt.Rows[i]["HomeWork_Name"] + ",";
                    StudentScoreArray += dt.Rows[i]["StudentScore"].ToString().clearLastZero() + ",";
                    double HWScore = 0;
                    double StudentScore = 0;
                    double.TryParse(dt.Rows[i]["HWScore"].ToString(), out HWScore);
                    double.TryParse(dt.Rows[i]["StudentScore"].ToString(), out StudentScore);
                    StudentScoreRateArray += (HWScore == 0 || StudentScore == 0 ? "0" : (100 * StudentScore / HWScore).ToString().clearLastZero()) + ",";
                    classavgArray += dt.Rows[i]["classavgrate"].ToString().clearLastZero() + ",";
                    //gradeavgArray += dt.Rows[i]["gradeavg"] + ",";
                    inum++;
                    string strGradeOrder = dt.Rows[i]["GradeStudentScoreOrder"].ToString().clearLastZero();
                    strGradeOrder = strGradeOrder == "0" ? "-" : strGradeOrder;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        ClassName = dt.Rows[i]["ClassName"].ToString(),
                        HomeWork_Name = dt.Rows[i]["HomeWork_Name"].ToString(),
                        HomeWorkCreateTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["HomeWorkCreateTime"].ToString(), "yyyy-MM-dd HH:mm:ss"),
                        StudentScore = dt.Rows[i]["StudentScore"].ToString().clearLastZero(),
                        StudentScoreOrder = dt.Rows[i]["StudentScoreOrder"].ToString().clearLastZero(),
                        GradeStudentScoreOrder = strGradeOrder,
                        classavg = dt.Rows[i]["classavg"].ToString().clearLastZero(),
                        gradeavg = dt.Rows[i]["gradeavg"].ToString().clearLastZero()
                    });
                }
                string[] scoreArr = StudentScoreArray.TrimEnd(',').Split(',');
                string[] scoreRateArr = StudentScoreRateArray.TrimEnd(',').Split(',');
                string[] classArr = classavgArray.TrimEnd(',').Split(',');
                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = rCount,
                        HomeWork_NameArray = HomeWork_NameArray.TrimEnd(','),
                        jsonStr = "[{\"name\": \"得分率\",\"data\": [" + string.Join(",", scoreRateArr.Reverse()) + "]},{\"name\": \"班级平均得分率\",\"data\": [" + string.Join(",", classArr.Reverse()) + "]}]",
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