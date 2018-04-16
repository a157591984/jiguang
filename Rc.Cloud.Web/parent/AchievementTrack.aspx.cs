using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using System.Data;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
using System.Text;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.parent
{
    public partial class AchievementTrack : Rc.Cloud.Web.Common.FInitData
    {
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
        public static string GetStatsClassStudentHW_Score(string BabyId, string SubjectId, string HomeWorkCreateTime, string HomeWorkFinishTime, int PageSize, int PageIndex)
        {
            try
            {
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                BabyId = BabyId.Filter();
                SubjectId = SubjectId.Filter();
                HomeWorkCreateTime = HomeWorkCreateTime.Filter();
                HomeWorkCreateTime = HomeWorkCreateTime.Filter();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                BLL_StatsClassStudentHW_Score bllStatsClassStudentHW_Score = new BLL_StatsClassStudentHW_Score();

                string strSqlCount = string.Empty;
                string strWhere = " StudentID='" + BabyId + "'";
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

                strSql.Append(@")AS Row, T.*  from (select scss.StudentID,scss.HomeWorkCreateTime,scss.SubjectID,scss.SubjectName,scss.ClassName,scss.HomeWork_Name,scss.StudentScore,scss.GradeStudentScoreOrder,scss.StudentScoreOrder,scs.AVGScore classavg,sgs.AVGScore as gradeavg from StatsClassStudentHW_Score scss
inner join HomeWork hw on hw.HomeWork_ID=scss.HomeWork_ID
left join dbo.StatsClassHW_Score scs on scs.ClassID=scss.ClassID and scss.HomeWork_ID=scs.HomeWork_ID and scs.ResourceToResourceFolder_Id=scss.ResourceToResourceFolder_Id
left join dbo.StatsGradeHW_Score sgs on sgs.Gradeid=scss.Gradeid and sgs.ResourceToResourceFolder_Id=scss.ResourceToResourceFolder_Id) T ");
                if (!string.IsNullOrEmpty(strWhere.Trim()))
                {
                    strSql.Append(" WHERE " + strWhere);
                }

                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize));
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                string SqlCount = @"select count(1) from (select scss.HomeWorkCreateTime,StudentID, scss.SubjectID from  StatsClassStudentHW_Score scss
inner join HomeWork hw on hw.HomeWork_ID=scss.HomeWork_ID
left join dbo.StatsClassHW_Score scs on scs.ClassID=scss.ClassID and scss.HomeWork_ID=scs.HomeWork_ID and scs.ResourceToResourceFolder_Id=scss.ResourceToResourceFolder_Id
left join dbo.StatsGradeHW_Score sgs on sgs.Gradeid=scss.Gradeid and sgs.ResourceToResourceFolder_Id=scss.ResourceToResourceFolder_Id) t where" + strWhere;
                int rCount = (int)Rc.Common.DBUtility.DbHelperSQL.GetSingle(SqlCount);
                int inum = 0;
                string HomeWork_NameArray = string.Empty;
                string StudentScoreArray = string.Empty;
                string classavgArray = string.Empty;
                //string gradeavgArray = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HomeWork_NameArray += dt.Rows[i]["HomeWork_Name"] + ",";
                    StudentScoreArray += dt.Rows[i]["StudentScore"].ToString().clearLastZero() + ",";
                    classavgArray += dt.Rows[i]["classavg"].ToString().clearLastZero() + ",";
                    //gradeavgArray += dt.Rows[i]["gradeavg"] + ",";
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        ClassName = dt.Rows[i]["ClassName"].ToString(),
                        HomeWork_Name = dt.Rows[i]["HomeWork_Name"].ToString(),
                        HomeWorkCreateTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["HomeWorkCreateTime"].ToString(), "yyyy-MM-dd HH:mm:ss"),
                        StudentScore = dt.Rows[i]["StudentScore"].ToString().clearLastZero(),
                        StudentScoreOrder = dt.Rows[i]["StudentScoreOrder"].ToString().clearLastZero(),
                        GradeStudentScoreOrder = dt.Rows[i]["GradeStudentScoreOrder"].ToString().clearLastZero() == "0" ? "-" : dt.Rows[i]["GradeStudentScoreOrder"].ToString().clearLastZero(),
                        SubjectName = dt.Rows[i]["SubjectName"].ToString(),
                        classavg = dt.Rows[i]["classavg"].ToString().clearLastZero() == "0" ? "-" : dt.Rows[i]["classavg"].ToString().clearLastZero(),
                        gradeavg = dt.Rows[i]["gradeavg"].ToString().clearLastZero() == "0" ? "-" : dt.Rows[i]["gradeavg"].ToString().clearLastZero()
                    });
                }
                string[] scoreArr = StudentScoreArray.TrimEnd(',').Split(',');
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
                        jsonStr = "[{\"name\": \"分数\",\"data\": [" + string.Join(",", scoreArr.Reverse()) + "]},{\"name\": \"班级平均分\",\"data\": [" + string.Join(",", classArr.Reverse()) + "]}]",
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