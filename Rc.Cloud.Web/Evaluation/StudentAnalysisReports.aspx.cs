using Rc.BLL.Resources;
using Rc.Common.DBUtility;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.Evaluation
{
    //public partial class StudentAnalysisReports : Rc.Cloud.Web.Common.FInitData
    //注意此处不能集成基类FInitData，原因是 可以端可以直接调用此页面
    public partial class StudentAnalysisReports : System.Web.UI.Page
    {
        protected string HomeWork_Id = string.Empty;
        protected string StudentId = string.Empty;
        protected string IsTrue = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        BLL_StatsClassHW_Score bll_StatsClassHW_Score = new BLL_StatsClassHW_Score();
        public string TempHw_Score = "<tr><td>{0}</td><td>{1}</td><td>{2}</td>";
        public string Temp_HwStudent_Score = "<td>{0}</td><td>第{1}名</td><td>{3}</td><td>第{2}层次</td><td>{4}</td></tr>";
        public string TempHW_TQ = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}%</td></tr>";
        public string TempHW_TQ_Target = "<tr><td class=\"text-left\">{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}%</td></tr>";
        public string TempHW_KP = "<tr><td class=\"text-left\">{0}</td> <td class='text-left'>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}%</td></tr>";
        //static Model_F_User loginUser = new Model_F_User();
        DataTable dtStu_Score = new DataTable();
        DataTable dt_KPScore = new DataTable();
        DataTable dtTQ_S = new DataTable();
        protected string link = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //loginUser = FloginUser;
            string Student_HomeWork_Id = Request.QueryString["Student_HomeWork_Id"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            HomeWork_Id = Request.QueryString["HomeWork_Id"].Filter();
            StudentId = Request.QueryString["StudentId"].Filter();
            if (string.IsNullOrEmpty(Student_HomeWork_Id))//web端批改
            {
                link += string.Format("<li><a href='../student/OHomeWorkViewTT.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>批改详情</a></li><li><a href='../student/OHomeWorkViewTT.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&wrong=w'>错题详情</a></li><li class='active'><a href='../Evaluation/StudentAnalysisReports.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>分析报告</a></li>", ResourceToResourceFolder_Id, HomeWork_Id, StudentId);
            }
            else//客户端批改
            {
                link += string.Format(@"<li><a href='../student/ohomeworkview_client.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>批改详情</a></li><li><a href='../student/ohomeworkview_client.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&wrong=w&Student_HomeWork_Id={3}'>错题详情</a></li><li class='active'><a href='../Evaluation/StudentAnalysisReports.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>分析报告</a></li>"
                    , ResourceToResourceFolder_Id
                    , HomeWork_Id
                    , StudentId
                    , Student_HomeWork_Id);
            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HomeWork_Id))
                {
                    LoadData();

                }
            }
        }
        protected void LoadData()
        {
            dtTQ_S = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num,TestQuestions_OrderNum ").Tables[0];

            DataTable dt = new BLL_StatsClassStudentHW_Score().GetList("HomeWork_ID='" + HomeWork_Id + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and StudentId='" + StudentId + "' ").Tables[0];
            if (dt.Rows.Count > 0)
            {
                string tempSql = string.Empty;
                tempSql = string.Format(@"select COUNT(1) as icount from Student_HomeWork shw inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id  where HomeWork_Id='{0}' 
and Student_Id='{1}'
and shwCorrect.Student_HomeWork_CorrectStatus='{2}'", HomeWork_Id, StudentId, "1");
                int i = int.Parse(Rc.Common.DBUtility.DbHelperSQL.GetSingle(tempSql).ToString());
                this.ltlHomeWork_Name.Text = dt.Rows[0]["Resource_Name"].ToString().ReplaceForFilter()
                    + (i > 0 ? "<span class='panel_state bg-success'>已完成批改</span>" : "<span class='panel_state bg-warning'>未完成批改</span>");
                this.ltlGradeName.Text = dt.Rows[0]["GradeName"].ToString();
                this.ltlClassName.Text = dt.Rows[0]["ClassName"].ToString();
                this.ltlStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                this.ltlHWScore.Text = dt.Rows[0]["HWScore"].ToString().clearLastZero();
                this.ltlStudentScore.Text = dt.Rows[0]["StudentScore"].ToString().clearLastZero();

                #region 获取小题得分列表
                string strSql = @"select HomeWork_Id,TestQuestions_Score_ID,Student_Id,Student_Score from Student_HomeWorkAnswer where  HomeWork_Id='" + HomeWork_Id + "' and Student_Id='" + StudentId + "'";
                dtStu_Score = DbHelperSQL.Query(strSql).Tables[0];
                #endregion
                #region 获取知识点得分列表
                //,KPScoreStudentSum=(select ISNULL(SUM(Student_Score),0) from Student_HomeWorkAnswer where Student_Id=stuHW.Student_Id and HomeWork_Id=hw.HomeWork_Id and TestQuestions_Score_ID in(select TestQuestions_Score_ID from TestQuestions_Score where ContentText=tqs.ContentText))
                strSql = @"	select
	tqs.ResourceToResourceFolder_Id
	,hw.HomeWork_Id
	,stuHW.Student_Id as StudentId
	,left(tqs.ContentText,100) as KPName
    ,ISNULL(SUM(TestQuestions_Score),0) as KPScoreSum	
    ,KPScoreStudentSum=(select ISNULL(SUM(Student_Score),0) from Student_HomeWorkAnswer t 
	inner join TestQuestions_Score t2 on t2.TestQuestions_Score_Id=t.TestQuestions_Score_Id and t2.ContentText=tqs.ContentText
	 where Student_Id=stuHW.Student_Id and HomeWork_Id=hw.HomeWork_Id )	
    from TestQuestions_Score tqs
	left join HomeWork hw on hw.ResourceToResourceFolder_Id=tqs.ResourceToResourceFolder_Id
	left join Student_HomeWork stuHW on stuHW.HomeWork_Id=hw.HomeWork_Id	
	where  stuHW.Student_Id='" + StudentId + "' and hw.HomeWork_Id='" + HomeWork_Id + "' group by tqs.ResourceToResourceFolder_Id,hw.HomeWork_Id,stuHW.Student_Id,tqs.ContentText";
                dt_KPScore = DbHelperSQL.Query(strSql).Tables[0];
                #endregion

                LoadDateHw_score();
                LoadStatsClassHW_TQ();
                LoadStatsClassHW_KP();
                LoadStatsClassHW_TQ_Target();
            }
            else
            {
                IsTrue = "1";
            }
        }

        public void LoadDateHw_score()
        {
            string strClassCount = "0";
            string StrTemp = string.Empty;
            List<Model_StatsClassHW_Score> StatsClassHW_Score_list = new List<Model_StatsClassHW_Score>();
            StatsClassHW_Score_list = bll_StatsClassHW_Score.GetModelList("HomeWork_ID='" + HomeWork_Id + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'");
            foreach (var item in StatsClassHW_Score_list)//
            {
                StrTemp = string.Format(TempHw_Score, item.AVGScore.ToString().clearLastZero(), item.LowestScore.ToString().clearLastZero(), item.HighestScore.ToString().clearLastZero());
                strClassCount = item.ClassAllCount.ToString().clearLastZero();
            }
            BLL_StatsClassStudentHW_Score bll_StatsClassStudentHW_Score = new BLL_StatsClassStudentHW_Score();
            List<Model_StatsClassStudentHW_Score> StatsClassStudentHW_Score_list = new List<Model_StatsClassStudentHW_Score>();
            StatsClassStudentHW_Score_list = bll_StatsClassStudentHW_Score.GetModelList("HomeWork_ID='" + HomeWork_Id + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and  StudentID='" + StudentId + "'");
            foreach (var item in StatsClassStudentHW_Score_list)
            {
                StrTemp += string.Format(Temp_HwStudent_Score
                    , item.StudentScore.ToString().clearLastZero()
                    , Convert.ToInt32(item.StudentScoreOrder).ToString()
                    , item.Hierarchy.ToString().clearLastZero()
                    , item.HWScoreLevelName
                    , strClassCount);
            }
            this.ltlHw_Score.Text = StrTemp;
        }

        public void LoadStatsClassHW_TQ()//小题得分
        {
            string temp = "<a href='/teacher/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id={0}#{1}' class='btn-link' target=\"_blank\">{2}</a>";
            string StrTemp = string.Empty;
            List<Model_StatsClassHW_TQ> StatsClassHW_TQ_list = new List<Model_StatsClassHW_TQ>();
            BLL_StatsClassHW_TQ bll_StatsClassHW_TQ = new BLL_StatsClassHW_TQ();
            StatsClassHW_TQ_list = bll_StatsClassHW_TQ.GetModelList("TQ_Score!=-1 and HomeWork_ID='" + HomeWork_Id + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num,TestQuestions_OrderNum");

            foreach (var item in StatsClassHW_TQ_list)
            {
                DataRow[] drTQ_S = dtTQ_S.Select("TestQuestions_Score_ID='" + item.TestQuestions_Score_ID + "'");
                string testIndex = string.Empty;
                if (drTQ_S.Length > 0 && (drTQ_S[0]["TestType"].ToString() == "clozeTest" || drTQ_S[0]["TestType"].ToString() == "fill"))
                {
                    testIndex = string.IsNullOrEmpty(drTQ_S[0]["testIndex"].ToString()) ? "" : "-" + drTQ_S[0]["testIndex"].ToString();
                }
                StrTemp += string.Format(TempHW_TQ
                    , string.Format(temp, ResourceToResourceFolder_Id, item.TestQuestions_Num, item.topicNumber.TrimEnd('.') + testIndex)
                    , item.TQ_Score.ToString().clearLastZero()
                    , item.complexityText
                    , GetTQScore(item.TestQuestions_Score_ID)
                    , item.ScoreAvg.ToString().clearLastZero()
                    , item.ScoreAvgRate.ToString().clearLastZero());
            }
            this.ltlHW_TQ.Text = StrTemp;
        }

        public void LoadStatsClassHW_KP()//知识点
        {

            string StrTemp = string.Empty;
            List<Model_StatsClassHW_KP> StatsClassHW_KP_list = new List<Model_StatsClassHW_KP>();
            BLL_StatsClassHW_KP bll_StatsClassHW_KP = new BLL_StatsClassHW_KP();
            StatsClassHW_KP_list = bll_StatsClassHW_KP.GetModelList("HomeWork_ID='" + HomeWork_Id + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestionNums");
            foreach (var item in StatsClassHW_KP_list)
            {
                StrTemp += string.Format(TempHW_KP
                    , item.KPName
                    , GetTQNum(dtTQ_S, item.KPName, item.TestQuestionNumStrs.ToString(), item.TestQuestionNums)
                    , GetKPScore(item.KPName, 1)
                    , GetKPScore(item.KPName, 2)
                    , item.KPScoreAvg.ToString().clearLastZero()
                    , item.KPScoreAvgRate.ToString().clearLastZero());
            }
            this.ltlHW_KP.Text = StrTemp;
        }

        public void LoadStatsClassHW_TQ_Target()//测量目标
        {
            string temp = "<a href='/teacher/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id={0}#{1}' class='btn-link' target=\"_blank\">第{2}题</a>";
            string StrTemp = string.Empty;
            List<Model_StatsClassHW_TQ> StatsClassHW_TQ_list = new List<Model_StatsClassHW_TQ>();
            BLL_StatsClassHW_TQ bll_StatsClassHW_TQ = new BLL_StatsClassHW_TQ();
            StatsClassHW_TQ_list = bll_StatsClassHW_TQ.GetModelList("HomeWork_ID='" + HomeWork_Id + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and TargetText<>'' order by TestQuestions_Num,TestQuestions_OrderNum");
            foreach (var item in StatsClassHW_TQ_list)
            {
                DataRow[] drTQ_S = dtTQ_S.Select("TestQuestions_Score_ID='" + item.TestQuestions_Score_ID + "'");
                string testIndex = string.Empty;
                if (drTQ_S.Length > 0 && (drTQ_S[0]["TestType"].ToString() == "clozeTest" || drTQ_S[0]["TestType"].ToString() == "fill"))
                {
                    testIndex = string.IsNullOrEmpty(drTQ_S[0]["testIndex"].ToString()) ? "" : "-" + drTQ_S[0]["testIndex"].ToString();
                }
                StrTemp += string.Format(TempHW_TQ_Target
                    , item.TargetText == "" ? "-" : item.TargetText
                    , string.Format(temp, ResourceToResourceFolder_Id, item.TestQuestions_Num, item.topicNumber.TrimEnd('.') + testIndex)
                    , item.TQ_Score.ToString().clearLastZero()
                    , GetTQScore(item.TestQuestions_Score_ID)
                    , item.ScoreAvg.ToString().clearLastZero()
                    , item.ScoreAvgRate.ToString().clearLastZero());
            }
            this.ltlHW_TQ_Target.Text = StrTemp;
        }
        public string GetTQNum(DataTable dtTQ_S, string KPName, string TQNumArr, string TestQuestionNums)//第2题 第3题 第4(1)题 第4(2)题
        {
            try
            {
                string StrTempTQNum = string.Empty;
                string temp = "<a href='/teacher/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id={0}#{1}' class='btn-link' target=\"_blank\">{2}</a>";
                if (!string.IsNullOrEmpty(TestQuestionNums))
                {
                    string[] arr = TQNumArr.Split(',');
                    string[] TQNumS = TestQuestionNums.Split(',');
                    DataRow[] drTQ_S = dtTQ_S.Select(" ContentText='" + KPName + "' ");
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string testIndex = string.Empty;
                        if (drTQ_S.Length > 0 && (drTQ_S[i]["TestType"].ToString() == "clozeTest" || drTQ_S[i]["TestType"].ToString() == "fill"))
                        {
                            testIndex = string.IsNullOrEmpty(drTQ_S[i]["testIndex"].ToString()) ? "" : "-" + drTQ_S[i]["testIndex"].ToString();
                        }
                        StrTempTQNum += string.Format(temp, ResourceToResourceFolder_Id, TQNumS[i].ToString(), "第" + arr[i].TrimEnd('.') + testIndex + "题&nbsp;&nbsp;");
                    }
                    return StrTempTQNum;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 学生小题得分
        /// </summary>
        /// <param name="TestQuestions_Score_ID"></param>
        /// <returns></returns>
        public string GetTQScore(string TestQuestions_Score_ID)
        {
            DataRow[] dr = dtStu_Score.Select("TestQuestions_Score_ID='" + TestQuestions_Score_ID + "'");
            if (dr.Length == 0)
            {
                return "0";
            }
            else
            {
                return dr[0]["Student_Score"].ToString().clearLastZero();
            }
        }
        /// <summary>
        /// 知识点得分
        /// </summary>
        /// <param name="ContentText"></param>
        /// <param name="type">1知识点分数，2知识点得分</param>
        /// <returns></returns>
        public string GetKPScore(string ContentText, int type)
        {
            //            string Sql = @"	select
            //	tqs.ResourceToResourceFolder_Id
            //	,hw.HomeWork_Id
            //	,stuHW.Student_Id as StudentId
            //	,left(tqs.ContentText,100) as KPName
            //	,KPScoreSum=(select ISNULL(SUM(TestQuestions_Score),0) from TestQuestions_Score where ResourceToResourceFolder_Id=tqs.ResourceToResourceFolder_Id and ContentText=tqs.ContentText)
            //	,KPScoreStudentSum=(select ISNULL(SUM(Student_Score),0) from Student_HomeWorkAnswer where Student_Id=stuHW.Student_Id and HomeWork_Id=hw.HomeWork_Id and TestQuestions_Score_ID in(select TestQuestions_Score_ID from TestQuestions_Score where ContentText=tqs.ContentText))
            //	from TestQuestions_Score tqs
            //	left join HomeWork hw on hw.ResourceToResourceFolder_Id=tqs.ResourceToResourceFolder_Id
            //	left join Student_HomeWork stuHW on stuHW.HomeWork_Id=hw.HomeWork_Id	
            //	where  stuHW.Student_Id='" + StudentId + "' and tqs.ContentText='" + ContentText + "' and hw.HomeWork_Id='" + HomeWork_Id + "' group by tqs.ResourceToResourceFolder_Id,hw.HomeWork_Id,stuHW.Student_Id,tqs.ContentText";
            //            DataTable dt = DbHelperSQL.Query(Sql.ToString()).Tables[0];
            DataRow[] dr = dt_KPScore.Select("KPName='" + ContentText + "'");
            if (dr.Length > 0)
            {
                if (type == 1)
                {
                    return dr[0]["KPScoreSum"].ToString().clearLastZero();
                }
                else
                {
                    return dr[0]["KPScoreStudentSum"].ToString().clearLastZero();
                }
            }
            else
            {
                return "0";
            }
        }
    }
}