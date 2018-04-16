using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Rc.Cloud.Web.Evaluation
{
    public partial class StudentAchievementTrack : Rc.Cloud.Web.Common.FInitData
    {
        public string StatsClassHW_ScoreOverviewID = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        public string UserId = string.Empty;
        BLL_StatsClassHW_Score bllschwso = new BLL_StatsClassHW_Score();
        public string StrTempClasses = string.Empty;
        public string HomeWorkCreateTime = string.Empty;
        public string StudentNum = string.Empty;
        //public string StatsClassHW_ScoreID = string.Empty;
        public string ClassID = string.Empty;
        public string TeacherID = string.Empty;
        public string ClassName = string.Empty;
        public string StrTemp = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassID = Request.QueryString["ClassID"].Filter();
            TeacherID = Request.QueryString["TeacherID"].Filter();
            ClassName = Request.QueryString["ClassName"].Filter();
            UserId = FloginUser.UserId;
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            //StatsClassHW_ScoreID = Request.QueryString["StatsClassHW_ScoreID"].Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(TeacherID))
                {
                    StrTemp = "&ClassID=" + ClassID + "&ClassName=" + ClassName + "&TeacherID=" + TeacherID;
                    UserId = TeacherID;
                }
                StudentNum = "[ {value: 10,name: '优秀'},{value: 20,name: '良好'},{value: 30,name: '中等'},{value: 40,name: '及格'},{value: 50,name: '不及格'}]";
                if (!string.IsNullOrEmpty(ResourceToResourceFolder_Id))
                {
                    ltlClasses.Text = StatsCommonHandle.GetTeacherClass(UserId, ClassID, ClassName, ResourceToResourceFolder_Id);//有效班级
                    if (string.IsNullOrEmpty(ClassID))
                    {
                        ClassID = "-1";
                    }
                }
            }
        }

        /// <summary>
        /// 学生成绩分析
        /// </summary>
        [WebMethod]
        public static string GetStatsClassStudentHW_Score(string Class, string ResourceToResourceFolder_Id, string UserId)
        {
            try
            {
                Class = Class.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                UserId = UserId.Filter();
                string TempDate = string.Empty;
                //<td class=\"progress font_color_green\"><i class=\"fa fa-arrow-up\"></i>5</td>../student/OHomeWorkViewTT.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}
                // string Temp = "<tr><td>李博</td><td>1班</td><td>80</td><td>3</td><td class=\"progress font_color_green\"><i class=\"fa fa-arrow-up\"></i>5</td><td>第一层次</td><td class=\"table_opera\"><a href=\"##\">作业详情</a><a href=\"成长趋势.html\" target=\"_blank\">成长趋势</a></td></tr>";
                //string Temp = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>{4}<td>{5}</td>{6}<td>第{7}层次</td><td class=\"table_opera\"><a href=\"../student/OHomeWorkViewTT.aspx?ResourceToResourceFolder_Id={8}&HomeWork_Id={9}&StudentId={10}\" target=\"_blank\">批改详情</a><a href=\"javascript:ShowPic('{10}','{11}','{9}');\">成长趋势</a></td></tr>";
                string Temp = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>{4}{5}</tr>";
                string Tempo = "<td><a href=\"javascript:CorrectView('{0}','{1}','{2}','{4}','{8}','{9}');\" class='{3}' title='{5}'>批改详情</a><a href=\"javascript:AnalysisReport('{0}','{1}','{2}','{4}','{6}','{8}','{9}');\" class='{7}' title='{5}'>分析报告</a></td>";
                if (Class == "-1")
                {
                    HttpContext.Current.Session["StatsClassClassId"] = "-1";
                    BLL_StatsTeacherStudentHW_Score bllStatsTeacherStudentHW_Score = new BLL_StatsTeacherStudentHW_Score();
                    List<Model_StatsTeacherStudentHW_Score> TeacherStudentHW_ScoreList = new List<Model_StatsTeacherStudentHW_Score>();
                    TeacherStudentHW_ScoreList = bllStatsTeacherStudentHW_Score.GetModelList("TeacherID='" + UserId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by StudentScore desc,StudentScoreOrder ");
                    //根据资源标识+班级标识，关联出作业标识
                    string strSqlHW = @"select ss.StatsTeacherStudentHW_ScoreId,hw.HomeWork_Id from StatsTeacherStudentHW_Score ss
inner join HomeWork hw on hw.ResourceToResourceFolder_Id=ss.ResourceToResourceFolder_Id and hw.UserGroup_Id=ss.ClassId
where ss.TeacherID='" + UserId + "' and ss.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'";
                    DataTable dtHW = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlHW).Tables[0];

                    string strSqlSHW = @"select shw.Student_HomeWork_Id,shw.HomeWork_Id,shw.Student_Id,shw.CreateTime,shwCorrect.CorrectMode from Student_HomeWork shw 
inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id 
where shw.HomeWork_Id in(select hw.HomeWork_Id from StatsTeacherStudentHW_Score ss
inner join HomeWork hw on hw.ResourceToResourceFolder_Id=ss.ResourceToResourceFolder_Id and hw.UserGroup_Id=ss.ClassId
where ss.TeacherID='" + UserId + "' and ss.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "') ";
                    DataTable dtSHW = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlSHW).Tables[0];

                    foreach (var item in TeacherStudentHW_ScoreList)
                    {
                        DataRow[] drHW = dtHW.Select("StatsTeacherStudentHW_ScoreId='" + item.StatsTeacherStudentHW_ScoreID + "'");
                        string strHomeWork_ID = string.Empty;
                        if (drHW.Length > 0)
                        {
                            strHomeWork_ID = drHW[0]["HomeWork_ID"].ToString();
                        }
                        string strCorrectMode = string.Empty;
                        string strStudent_Homework_Id = string.Empty;
                        DataRow[] drSHW = dtSHW.Select("Student_Id='" + item.StudentID + "' ");
                        if (drSHW.Length > 0)
                        {
                            strCorrectMode = drSHW[0]["CorrectMode"].ToString();
                            strStudent_Homework_Id = drSHW[0]["Student_Homework_Id"].ToString();
                        }
                        string TJ = GetSubmitStudent(strHomeWork_ID, item.StudentID);
                        string PG = GetCorrectStudent(strHomeWork_ID, item.StudentID);
                        TempDate += string.Format(Temp
                            , item.StudentName//0
                            , item.ClassName//1
                            , item.StudentScore.ToString().clearLastZero()//2
                                                                          //, item.GradeStudentScoreOrder.ToString().clearLastZero()//3
                                                                          //, GetScoreImprove(item.GradeScoreImprove.ToString())//4
                            , item.StudentScoreOrder.ToString().clearLastZero()//5
                            , GetScoreImprove(item.ScoreImprove.ToString())//6
                                                                           //, item.Hierarchy.ToString().clearLastZero()//7
                            , string.Format(Tempo
                            , item.ResourceToResourceFolder_Id
                            , strHomeWork_ID
                            , item.StudentID
                            , TJ == "0" ? "disabled" : PG == "0" ? "danger" : ""
                            , TJ == "0" ? "1" : ""
                            , TJ == "0" ? "学生未提交" : PG == "0" ? "老师未批改" : ""
                            , TJ == "0" ? "" : PG == "0" ? "1" : ""
                            , TJ == "0" ? "disabled" : PG == "0" ? "disabled" : ""
                            , strCorrectMode
                            , strStudent_Homework_Id)

                            , item.ResourceToResourceFolder_Id//8
                            , strHomeWork_ID//9
                            , item.StudentID//10
                            );//11
                    }
                    return TempDate;
                }
                else
                {
                    HttpContext.Current.Session["StatsClassClassId"] = Class;
                    BLL_StatsClassStudentHW_Score bllClassStudentHW_Score = new BLL_StatsClassStudentHW_Score();
                    List<Model_StatsClassStudentHW_Score> ClassStudentHW_ScoreList = new List<Model_StatsClassStudentHW_Score>();
                    ClassStudentHW_ScoreList = bllClassStudentHW_Score.GetModelList("ClassID='" + Class + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by StudentScore desc,StudentScoreOrder ");

                    string strSqlSHW = @"select  shw.Student_HomeWork_Id,shw.HomeWork_Id,shw.Student_Id,shw.CreateTime,shwCorrect.CorrectMode from Student_HomeWork shw 
inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id 
                    where shw.HomeWork_Id in(select HomeWork_Id from HomeWork where UserGroup_Id='" + Class
                        + "' and HomeWork_AssignTeacher='" + UserId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "') ";
                    DataTable dtSHW = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlSHW).Tables[0];

                    foreach (var item in ClassStudentHW_ScoreList)
                    {
                        string strCorrectMode = string.Empty;
                        string strStudent_Homework_Id = string.Empty;
                        DataRow[] drSHW = dtSHW.Select("Student_Id='" + item.StudentID + "' ");
                        if (drSHW.Length > 0)
                        {
                            strCorrectMode = drSHW[0]["CorrectMode"].ToString();
                            strStudent_Homework_Id = drSHW[0]["Student_Homework_Id"].ToString();
                        }
                        string TJ = GetSubmitStudent(item.HomeWork_ID, item.StudentID);
                        string PG = GetCorrectStudent(item.HomeWork_ID, item.StudentID);
                        TempDate += string.Format(Temp
                            , item.StudentName
                            , item.ClassName
                            , item.StudentScore.ToString().clearLastZero()
                            //, item.GradeStudentScoreOrder.ToString().clearLastZero()
                            //, GetScoreImprove(item.GradeScoreImprove.ToString())
                            , item.StudentScoreOrder.ToString().clearLastZero()
                            , GetScoreImprove(item.ScoreImprove.ToString())
                            , string.Format(Tempo
                            , item.ResourceToResourceFolder_Id
                            , item.HomeWork_ID
                            , item.StudentID
                            , TJ == "0" ? "disabled" : PG == "0" ? "danger" : ""
                            , TJ == "0" ? "1" : ""
                            , TJ == "0" ? "学生未提交" : PG == "0" ? "老师未批改" : ""
                            , TJ == "0" ? "" : PG == "0" ? "1" : ""
                            , TJ == "0" ? "disabled" : PG == "0" ? "disabled" : ""
                            , strCorrectMode
                            , strStudent_Homework_Id)

                            //, item.Hierarchy.ToString().clearLastZero()
                            , item.ResourceToResourceFolder_Id
                            , item.HomeWork_ID
                            , item.StudentID);
                    }
                    return TempDate;
                }
            }
            catch (Exception)
            {

                return "";
            }
        }
        /// <summary>
        /// 排名
        /// </summary>
        /// <param name="TQNumArr"></param>
        /// <returns></returns>
        public static string GetScoreImprove(string ScoreImprove)
        {
            try
            {
                if (!string.IsNullOrEmpty(ScoreImprove))
                {
                    int Index = Convert.ToInt32(ScoreImprove);
                    string Temp = string.Empty;
                    if (Index < 0)
                    {
                        //Temp = "<td class=\"progress font_color_green\"><i class=\"fa fa-arrow-up\"></i>5</td>";
                        Temp = "<td><span class='text-danger'><i class='material-icons'>&#xE5DB;</i>&nbsp;" + Index + "</span></td>";
                        return Temp;
                    }
                    else if (Index == 0)
                    {
                        Temp = "<td ><i></i>-</td>";
                        return Temp;
                    }
                    else
                    {
                        Temp = "<td><span class='text-success'><i class='material-icons'>&#xE5D8;</i>&nbsp;" + ScoreImprove + "</span></td>";
                        return Temp;
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="HomeWork_Id"></param>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        private static string GetSubmitStudent(string HomeWork_Id, string StudentId)
        {
            string temp = string.Empty;
            try
            {
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(@"select COUNT(1) as ICount,shwSubmit.Student_HomeWork_Status from Student_HomeWork shw 
                inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
                where HomeWork_Id='" + HomeWork_Id + "' and Student_Id='" + StudentId + "' group by shwSubmit.Student_HomeWork_Status").Tables[0];
                DataRow[] drYJ = dt.Select("Student_HomeWork_Status='1'");
                temp = (drYJ.Length > 0 ? drYJ[0]["ICount"].ToString() : "0");
            }
            catch (Exception)
            {
                temp = "0";
            }
            return temp;
        }
        /// <summary>
        /// 批改
        /// </summary>
        /// <param name="HomeWork_Id"></param>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        private static string GetCorrectStudent(string HomeWork_Id, string StudentId)
        {
            string temp = string.Empty;
            try
            {
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(@"select COUNT(1) as ICount,shwCorrect.Student_HomeWork_CorrectStatus from Student_HomeWork shw 
                 inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
                 inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id 
                where HomeWork_Id='" + HomeWork_Id + "' and Student_Id='" + StudentId + "' and shwSubmit.Student_HomeWork_Status=1 group by shwCorrect.Student_HomeWork_CorrectStatus").Tables[0];
                DataRow[] drYP = dt.Select("Student_HomeWork_CorrectStatus='1'");
                DataRow[] drWP = dt.Select("Student_HomeWork_CorrectStatus='0'");
                temp = (drYP.Length > 0 ? drYP[0]["ICount"].ToString() : "0");
            }
            catch (Exception)
            {
                temp = "0";
            }
            return temp;
        }
    }
}
