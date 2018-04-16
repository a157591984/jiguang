using Rc.BLL.Resources;
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
using System.Text;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.Evaluation
{
    public partial class AssessmentProfile : Rc.Cloud.Web.Common.FInitData
    {
        public string StatsClassHW_ScoreOverviewID = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        public string UserId = string.Empty;
        // Model_StatsClassHW_ScoreOverview modelschwso = new Model_StatsClassHW_ScoreOverview();
        BLL_StatsClassHW_Score bllschwso = new BLL_StatsClassHW_Score();
        public string StrTempClasses = string.Empty;
        public string HomeWorkCreateTime = string.Empty;
        public string StudentNum = string.Empty;
        public string ClassID = string.Empty;
        public string TeacherID = string.Empty;
        public string ClassName = string.Empty;
        public string ClassCode = string.Empty;
        CommonHandel ch = new CommonHandel();
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = FloginUser.UserId;
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            ClassID = Request.QueryString["ClassID"].Filter();
            TeacherID = Request.QueryString["TeacherID"].Filter();
            ClassCode = Request.QueryString["ClassCode"].Filter();
            ClassName = HttpContext.Current.Server.UrlDecode(Request.QueryString["ClassName"].Filter());
            if (!string.IsNullOrEmpty(ClassCode)) Session["StatsClassClassId"] = ClassCode;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(TeacherID))
                {
                    UserId = TeacherID;
                }
                StudentNum = "[ {y: 10,name: '优秀'},{y: 20,name: '良好'},{y: 30,name: '中等'},{y: 40,name: '及格'},{y: 50,name: '不及格'}]";

                if (!string.IsNullOrEmpty(ResourceToResourceFolder_Id))
                {

                    ltlClasses.Text = StatsCommonHandle.GetTeacherClass(UserId, ClassID, ClassName,ResourceToResourceFolder_Id);//有效班级
                    if (string.IsNullOrEmpty(ClassID))
                    {
                        ClassID = "-1";
                    }
                }
            }
        }
        /// <summary>
        /// 成绩概况
        /// </summary>
        /// <param name="StatsClassHW_ScoreOverviewID"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStatsClassHW_ScoreOverview(string ClassCode, string ResourceToResourceFolder_Id, string UserId)
        {
            try
            {
                ClassCode = ClassCode.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                UserId = UserId.Filter();
                CommonHandel chbll = new CommonHandel();
                BLL_StatsTeacherHW_Score bllsthwso = new BLL_StatsTeacherHW_Score();
                List<Model_StatsTeacherHW_Score> teacherScoreOverviewList = new List<Model_StatsTeacherHW_Score>();
                DataTable dt = new DataTable();
                string StrTemp = string.Empty;
                if (ClassCode == "-1")
                {
                    HttpContext.Current.Session["StatsClassClassId"] = "-1";
                    teacherScoreOverviewList = bllsthwso.GetModelList(" TeacherID='" + UserId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' ");
                    foreach (var item in teacherScoreOverviewList)
                    {
                        StrTemp += string.Format("<tr><td>全部</td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td></tr>"
                            , item.ClassAllCount
                            , item.HighestScore.ToString().clearLastZero()
                            , item.LowestScore.ToString().clearLastZero()
                            , item.AVGScore.ToString().clearLastZero()
                            , item.Median.ToString().clearLastZero()
                            , item.Mode
                            , item.StandardDeviation.ToString().clearLastZero()
                            , item.AssignedCount.ToString().clearLastZero() + "/" + (Convert.ToInt32(item.ClassAllCount) - Convert.ToInt32(item.AssignedCount.ToString().clearLastZero()))
                            , item.CommittedCount.ToString().clearLastZero() + "/" + item.UncommittedCount.ToString().clearLastZero()
                            , item.CorrectedCount.ToString().clearLastZero() + "/" + item.UnCorrectedCount.ToString().clearLastZero());
                    }
                    return StrTemp;
                }
                else
                {
                    HttpContext.Current.Session["StatsClassClassId"] = ClassCode;
                    BLL_StatsClassHW_Score bllcswso = new BLL_StatsClassHW_Score();
                    List<Rc.Model.Resources.Model_StatsClassHW_Score> ClassScoreOverviewList = new List<Model_StatsClassHW_Score>();
                    ClassScoreOverviewList = bllcswso.GetModelList("ClassID='" + ClassCode + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'");
                    foreach (var item in ClassScoreOverviewList)
                    {
                        StrTemp = string.Format("< ><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td>{10}</td></tr>", item.ClassName, item.ClassAllCount
                            , item.HighestScore.ToString().clearLastZero()
                            , item.LowestScore.ToString().clearLastZero()
                            , item.AVGScore.ToString().clearLastZero()
                            , item.Median.ToString().clearLastZero()
                            , item.Mode
                            , item.StandardDeviation.ToString().clearLastZero()
                            , item.AssignedCount.ToString().clearLastZero() + "/" + (Convert.ToInt32(item.ClassAllCount) - Convert.ToInt32(item.AssignedCount.ToString().clearLastZero()))
                            , item.CommittedCount.ToString().clearLastZero() + "/" + item.UncommittedCount.ToString().clearLastZero()
                            , item.CorrectedCount.ToString().clearLastZero() + "/" + item.UnCorrectedCount.ToString().clearLastZero());
                    }
                    return StrTemp;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 前五后五
        /// </summary>
        /// <param name="ClassCode"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStatsClassHW_ScoreOrder(string ClassCode, string ResourceToResourceFolder_Id, string UserId)
        {
            try
            {
                CommonHandel ch = new CommonHandel();
                DataTable dt = new DataTable();
                string StrTemp = string.Empty;
                string Num = "5";
                string StrSql = string.Empty;
                if (ClassCode == "-1")
                {
                    StrSql = @"select top 5  stsh.* from StatsTeacherStudentHW_Score stsh 
 where  stsh.StudentID in(select Student_Id from Student_HomeWork shw 
 inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwSubmit.Student_HomeWork_Status=1
 where  HomeWork_Id in(select HomeWork_Id from HomeWork 
 where ResourceToResourceFolder_Id=stsh.ResourceToResourceFolder_Id and HomeWork_AssignTeacher=stsh.TeacherID) ) and stsh.TeacherID='" + UserId + "' and stsh.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by stsh.StudentScoreOrder";
                    BLL_StatsTeacherStudentHW_Score bllTeacherStudentHW_Score = new BLL_StatsTeacherStudentHW_Score();
                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                    //dt = bllTeacherStudentHW_Score.GetList(5, "TeacherID='" + UserId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'", "StudentScoreOrder").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count < 5)
                        {
                            Num = dt.Rows.Count.ToString();
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)//dt.Rows[i]["Score"]dt.Rows[i]["StudentName"]
                        {
                            StrTemp += string.Format(" <td class=\"before\"><h2>{0}</h2><p>{1}</p></td>", dt.Rows[i]["StudentScore"].ToString().clearLastZero(), dt.Rows[i]["StudentName"].ToString());
                        }
                    }
                    StrSql = @"select top 5  stsh.* from StatsTeacherStudentHW_Score stsh 
 where  stsh.StudentID in(select Student_Id from Student_HomeWork shw 
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwSubmit.Student_HomeWork_Status=1 where HomeWork_Id in(select HomeWork_Id from HomeWork 
 where ResourceToResourceFolder_Id=stsh.ResourceToResourceFolder_Id and HomeWork_AssignTeacher=stsh.TeacherID) ) and stsh.TeacherID='" + UserId + "' and stsh.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by stsh.StudentScoreOrder desc";
                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                    //dt = bllTeacherStudentHW_Score.GetList(5, "TeacherID='" + UserId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'", "StudentScoreOrder desc").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count < 5)
                        {
                            Num = dt.Rows.Count.ToString();
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)//dt.Rows[i]["Score"]dt.Rows[i]["StudentName"]
                        {
                            StrTemp += string.Format(" <td class=\"after\"><h2>{0}</h2><p>{1}</p></td>", dt.Rows[i]["StudentScore"].ToString().clearLastZero(), (dt.Rows[i]["StudentName"].ToString()));
                        }
                    }
                    return StrTemp + "|" + Num;
                }
                else
                {
                    BLL_StatsClassStudentHW_Score bllClassStudentHW_Score = new BLL_StatsClassStudentHW_Score();
                    StrSql = @"select top 5  stsh.*,shwSubmit.Student_HomeWork_Status from StatsClassStudentHW_Score stsh 
left join dbo.Student_HomeWork shw on shw.HomeWork_ID=stsh.HomeWork_ID and shw.Student_Id=stsh.StudentID 
 inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
 where  stsh.ClassID='" + ClassCode + "' and stsh.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and shwSubmit.Student_HomeWork_Status=1 order by stsh.StudentScoreOrder,AnswerEnd";
                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                    //dt = bllClassStudentHW_Score.GetList(5, "ClassID='" + ClassCode + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'", "StudentScoreOrder,AnswerEnd").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count < 5)
                        {
                            Num = dt.Rows.Count.ToString();
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)//dt.Rows[i]["Score"]dt.Rows[i]["StudentName"]
                        {
                            StrTemp += string.Format(" <td class=\"before\"><h2>{0}</h2><p>{1}</p></td>", dt.Rows[i]["StudentScore"].ToString().clearLastZero(), (dt.Rows[i]["StudentName"].ToString()));
                        }
                    }
                    StrSql = @"select top 5 stsh.*,shwSubmit.Student_HomeWork_Status from StatsClassStudentHW_Score stsh 
left join dbo.Student_HomeWork shw on shw.HomeWork_ID=stsh.HomeWork_ID and shw.Student_Id=stsh.StudentID 
 inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
 where  stsh.ClassID='" + ClassCode + "' and stsh.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and shwSubmit.Student_HomeWork_Status=1 order by stsh.StudentScoreOrder desc ,AnswerEnd desc";
                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                    //dt = bllClassStudentHW_Score.GetList(5, "ClassID='" + ClassCode + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'", "StudentScoreOrder desc,AnswerEnd desc").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count < 5)
                        {
                            Num = dt.Rows.Count.ToString();
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)//dt.Rows[i]["Score"]dt.Rows[i]["StudentName"]
                        {
                            StrTemp += string.Format(" <td class=\"after\"><h2>{0}</h2><p>{1}</p></td>", dt.Rows[i]["StudentScore"].ToString().clearLastZero(), (dt.Rows[i]["StudentName"].ToString()));
                        }
                    }
                    return StrTemp + "|" + Num;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 班级等级排名
        /// </summary>
        /// <param name="ClassCode"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="HomeWorkCreateTime"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStatsClassHW_ScoreOrderDistribution(string ClassCode, string ResourceToResourceFolder_Id, string UserId)
        {
            try
            {
                DataTable dt = new DataTable();
                string StrTemp = string.Empty;
                string StudentsNum = string.Empty;
                if (ClassCode == "-1")
                {
                    BLL_StatsTeacherHW_ScoreLevel bllTeacherHW_ScoreLevel = new BLL_StatsTeacherHW_ScoreLevel();
                    List<Model_StatsTeacherHW_ScoreLevel> TeacherHW_ScoreLevelList = new List<Model_StatsTeacherHW_ScoreLevel>();
                    TeacherHW_ScoreLevelList = bllTeacherHW_ScoreLevel.GetModelList(" TeacherID='" + UserId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by HWScoreLevelRight desc");
                    foreach (var item in TeacherHW_ScoreLevelList)
                    {
                        StrTemp += string.Format("<tr><td>{0}</td><td>{1}-{2}</td><td>{3}</td><td>{4}%</td><td>{5}%-{6}%</td></tr>"
                            , item.HWScoreLevelName
                            , item.HWScoreLevelLeft.ToString().clearLastZero()
                            , item.HWScoreLevelRight.ToString().clearLastZero()
                            , item.HWScoreLevelCount.ToString().clearLastZero()
                            , item.HWScoreLevelCountRate.ToString().clearLastZero()
                            , item.HWScoreLevelRateLeft
                            , item.HWScoreLevelRateRight);
                        StudentsNum += item.HWScoreLevelCountRate.ToString().clearLastZero() + ",";
                    }
                    StudentsNum = StudentsNum.TrimEnd(',');
                    string[] StrArr = StudentsNum.Split(',');
                    string StrNum = "[ {\"y\": " + StrArr[0] + ",\"name\": \"优秀\"},{\"y\": " + StrArr[1] + ",\"name\": \"良好\"},{\"y\": " + StrArr[2] + ",\"name\": \"中等\"},{\"y\": " + StrArr[3]
                        + ",\"name\": \"及格\"},{\"y\": " + StrArr[4] + ",\"name\": \"不及格\"}]";
                    return StrTemp + "|" + StrNum;
                }
                else
                {
                    BLL_StatsClassHW_ScoreLevel bllClassHW_ScoreLevel = new BLL_StatsClassHW_ScoreLevel();
                    List<Model_StatsClassHW_ScoreLevel> ClassHW_ScoreLevelList = new List<Model_StatsClassHW_ScoreLevel>();
                    ClassHW_ScoreLevelList = bllClassHW_ScoreLevel.GetModelList(" ClassID='" + ClassCode + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by HWScoreLevelRight desc");
                    foreach (var item in ClassHW_ScoreLevelList)
                    {
                        StrTemp += string.Format("<tr><td>{0}</td><td>{1}-{2}</td><td>{3}</td><td>{4}%</td><td>{5}%-{6}%</td></tr>"
                            , item.HWScoreLevelName
                            , item.HWScoreLevelLeft.ToString().clearLastZero()
                            , item.HWScoreLevelRight.ToString().clearLastZero()
                            , item.HWScoreLevelCount.ToString().clearLastZero()
                            , item.HWScoreLevelCountRate.ToString().clearLastZero()
                            , item.HWScoreLevelRateLeft
                            , item.HWScoreLevelRateRight);
                        StudentsNum += item.HWScoreLevelCountRate.ToString().clearLastZero() + ",";
                    }
                    StudentsNum = StudentsNum.TrimEnd(',');
                    string[] StrArr = StudentsNum.Split(',');
                    string StrNum = "[ {\"y\": " + StrArr[0] + ",\"name\": \"优秀\"},{\"y\": " + StrArr[1] + ",\"name\": \"良好\"},{\"y\": " + StrArr[2] + ",\"name\": \"中等\"},{\"y\": " + StrArr[3]
                        + ",\"name\": \"及格\"},{\"y\": " + StrArr[4] + ",\"name\": \"不及格\"}]";
                    return StrTemp + "|" + StrNum;
                }
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}