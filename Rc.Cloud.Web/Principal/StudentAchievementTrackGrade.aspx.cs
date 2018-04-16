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
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.Principal
{
    public partial class StudentAchievementTrackGrade : Rc.Cloud.Web.Common.FInitData
    {
        public string GradeId = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            if (!string.IsNullOrEmpty(GradeId))
            {
                LoadClasses();
            }
        }

        /// <summary>
        /// 有效班级
        /// </summary>
        protected void LoadClasses()
        {
            ltlClasses.Text = StatsCommonHandle.GetGradeAllClass(GradeId);

        }
        /// <summary>
        ///学生成绩分析
        /// </summary>
        [WebMethod]
        public static string GetStatsClassStudentHW_Score(string Class, string ResourceToResourceFolder_Id, string GradeId)
        {
            try
            {
                Class = Class.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                GradeId = GradeId.Filter();
                string TempDate = string.Empty;//../Evaluation/StudentAnalysisReports.aspx?ResourceToResourceFolder_Id={8}&HomeWork_Id={9}&StudentId={7}
                //<td class=\"progress font_color_green\"><i class=\"fa fa-arrow-up\"></i>5</td>../student/OHomeWorkViewTT.aspx?ResourceToResourceFolder_Id={8}&HomeWork_Id={9}&StudentId={7}
                // string Temp = "<tr><td>李博</td><td>1班</td><td>80</td><td>3</td><td class=\"progress font_color_green\"><i class=\"fa fa-arrow-up\"></i>5</td><td>第一层次</td><td class=\"table_opera\"><a href=\"##\">作业详情</a><a href=\"成长趋势.html\" target=\"_blank\">成长趋势</a></td></tr>";<a href=\"javascript:ShowPic('{8}','{9}','{11}');\">成长趋势</a>
                string Temp = "<tr><td class='text-left'>{0}</td><td class='text-left'>{1}</td><td>{2}</td><td>{3}</td>{4}<td>{5}</td>{6}{7}</tr>";
                string Tempo = "<td class=\"table_opera\"><a href=\"javascript:CorrectView('{0}','{1}','{2}','{4}','{8}','{9}');\" class='{3}' title='{5}'>批改详情</a><a href=\"javascript:AnalysisReport('{0}','{1}','{2}','{4}','{6}','{8}','{9}');\" class='{7}' title='{5}'>分析报告</a></td>";
                //学生作业提交情况
                string SqlSHW = @"select  
shw.Student_HomeWork_Id,shw.HomeWork_Id,shw.Student_Id,shw.CreateTime,shwCorrect.CorrectTime,shwCorrect.Student_HomeWork_CorrectStatus
,shwCorrect.CorrectMode,shwSubmit.Student_HomeWork_Status,shwSubmit.OpenTime,shwSubmit.StudentIP,shwSubmit.Student_Answer_Time from  StatsClassStudentHW_Score ss
inner join dbo.Student_HomeWork shw on shw.HomeWork_Id=ss.HomeWork_ID and shw.Student_Id=ss.StudentID 
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id 
where ss.Gradeid='" + GradeId + "' and ss.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' ";
                DataTable dtSHW = Rc.Common.DBUtility.DbHelperSQL.Query(SqlSHW).Tables[0];
                if (string.IsNullOrEmpty(Class))
                {
                    List<Model_StatsGradeStudentHW_Score> ClassStudentHW_ScoreList = new List<Model_StatsGradeStudentHW_Score>();
                    ClassStudentHW_ScoreList = new BLL_StatsGradeStudentHW_Score().GetModelList("Gradeid='" + GradeId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by GradeStudentScoreOrder,StudentScoreOrder");
                    //根据资源标识+班级标识，关联出作业标识
                    string strHW = @"select ss.StatsGradeStudentHW_ScoreId,hw.HomeWork_Id from StatsGradeStudentHW_Score ss
inner join dbo.HomeWork hw on hw.ResourceToResourceFolder_Id=ss.ResourceToResourceFolder_Id and hw.UserGroup_Id=ss.ClassId
where ss.Gradeid='" + GradeId + "' and ss.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' ";
                    DataTable dtHW = Rc.Common.DBUtility.DbHelperSQL.Query(strHW).Tables[0];
                    foreach (var item in ClassStudentHW_ScoreList)
                    {
                        DataRow[] drHW = dtHW.Select("StatsGradeStudentHW_ScoreId='" + item.StatsGradeStudentHW_ScoreID + "'");
                        string strHomeWork_ID = string.Empty;
                        if (drHW.Length > 0)
                        {
                            strHomeWork_ID = drHW[0]["HomeWork_ID"].ToString();
                        }
                        DataRow[] drPG = dtSHW.Select("Student_Id='" + item.StudentID + "' and HomeWork_Id='" + strHomeWork_ID + "' and Student_HomeWork_CorrectStatus=1 and Student_HomeWork_Status=1");
                        string PG = (drPG.Length > 0 ? "1" : "0");
                        DataRow[] drTJ = dtSHW.Select("Student_Id='" + item.StudentID + "' and HomeWork_Id='" + strHomeWork_ID + "' and  Student_HomeWork_Status=1");
                        string TJ = (drTJ.Length > 0 ? "1" : "0");

                        string strCorrectMode = string.Empty;
                        string strStudent_Homework_Id = string.Empty;
                        DataRow[] drSHW = dtSHW.Select("Student_Id='" + item.StudentID + "' ");
                        if (drSHW.Length > 0)
                        {
                            strCorrectMode = drSHW[0]["CorrectMode"].ToString();
                            strStudent_Homework_Id = drSHW[0]["Student_Homework_Id"].ToString();
                        }

                        TempDate += string.Format(Temp
                            , item.StudentName
                            , item.ClassName
                            , item.StudentScore.ToString().clearLastZero()
                            , item.GradeStudentScoreOrder.ToString().clearLastZero()
                            , GetScoreImprove(item.GradeScoreImprove.ToString())
                            , item.StudentScoreOrder
                            , GetScoreImprove(item.ScoreImprove.ToString())
                            //, item.Hierarchy.ToString().clearLastZero()
                            , string.Format(Tempo
                            , item.ResourceToResourceFolder_ID
                            , strHomeWork_ID
                            , item.StudentID
                            , TJ == "0" ? "disabled" : PG == "0" ? "danger" : ""
                            , TJ == "0" ? "1" : ""
                            , TJ == "0" ? "学生未提交" : PG == "0" ? "老师未批改" : ""
                            , TJ == "0" ? "" : PG == "0" ? "1" : ""
                            , TJ == "0" ? "disabled" : PG == "0" ? "disabled" : ""
                            , strCorrectMode
                            , strStudent_Homework_Id)

                            , item.StudentID
                            , item.ResourceToResourceFolder_ID
                            , strHomeWork_ID);
                    }
                    return TempDate;
                }
                else
                {
                    BLL_StatsClassStudentHW_Score bllClassStudentHW_Score = new BLL_StatsClassStudentHW_Score();
                    List<Model_StatsClassStudentHW_Score> ClassStudentHW_ScoreList = new List<Model_StatsClassStudentHW_Score>();
                    ClassStudentHW_ScoreList = bllClassStudentHW_Score.GetModelList("ClassID='" + Class + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by GradeStudentScoreOrder,StudentScoreOrder");
                    foreach (var item in ClassStudentHW_ScoreList)
                    {
                        DataRow[] drPG = dtSHW.Select("Student_Id='" + item.StudentID + "' and HomeWork_Id='" + item.HomeWork_ID + "' and Student_HomeWork_CorrectStatus=1 and Student_HomeWork_Status=1");
                        string PG = (drPG.Length > 0 ? "1" : "0");
                        DataRow[] drTJ = dtSHW.Select("Student_Id='" + item.StudentID + "' and HomeWork_Id='" + item.HomeWork_ID + "' and  Student_HomeWork_Status=1");
                        string TJ = (drTJ.Length > 0 ? "1" : "0");

                        string strCorrectMode = string.Empty;
                        string strStudent_Homework_Id = string.Empty;
                        DataRow[] drSHW = dtSHW.Select("Student_Id='" + item.StudentID + "' ");
                        if (drSHW.Length > 0)
                        {
                            strCorrectMode = drSHW[0]["CorrectMode"].ToString();
                            strStudent_Homework_Id = drSHW[0]["Student_Homework_Id"].ToString();
                        }

                        TempDate += string.Format(Temp
                            , item.StudentName
                            , item.ClassName
                            , item.StudentScore.ToString().clearLastZero()
                            , item.GradeStudentScoreOrder.ToString().clearLastZero()
                            , GetScoreImprove(item.GradeScoreImprove.ToString())
                            , item.StudentScoreOrder.ToString().clearLastZero()
                            , GetScoreImprove(item.ScoreImprove.ToString())
                            //, item.Hierarchy.ToString().clearLastZero()
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

                            , item.StudentID
                            , item.ResourceToResourceFolder_Id
                            , item.HomeWork_ID);
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
                        Temp = "<td><i></i>-</td>";
                        return Temp;
                    }
                    else
                    {
                        Temp = "<td><span class='text-success'><i class='material-icons'>&#xE5D8;</i>&nbsp;" + ScoreImprove.ToString().clearLastZero() + "</span></td>";
                        return Temp;
                    }
                }
                else
                {
                    return "<td>-</td>";
                }
            }
            catch (Exception)
            {
                return "<td>-</td>";
            }
        }
    }
}