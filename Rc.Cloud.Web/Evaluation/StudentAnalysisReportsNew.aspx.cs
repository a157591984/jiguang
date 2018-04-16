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
    public partial class StudentAnalysisReportsNew : System.Web.UI.Page
    {
        protected string HomeWork_Id = string.Empty;
        protected string StudentId = string.Empty;
        protected string IsTrue = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        BLL_StatsClassHW_Score bll_StatsClassHW_Score = new BLL_StatsClassHW_Score();
        public string TempHw_Score = "<tr><td>{0}</td><td>{1}</td><td>{2}</td>";
        public string Temp_HwStudent_Score = "<td>{0}</td><td>第{1}名</td><td>{3}</td><td>第{2}层次</td><td>{4}</td></tr>";
        public string TempHW_KP = "<tr><td class=\"text-left\">{0}</td> <td class='text-left'>{1}</td><td class='text-left'>{2}</td><td>{3}%</td></tr>";

        DataTable dt_KPScore = new DataTable();
        DataTable dtTQ_S = new DataTable();
        protected string link = string.Empty;

        protected string strKPName = string.Empty;
        protected string strKPScoreRate = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //loginUser = FloginUser;
            string Student_HomeWork_Id = Request.QueryString["Student_HomeWork_Id"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            HomeWork_Id = Request.QueryString["HomeWork_Id"].Filter();
            StudentId = Request.QueryString["StudentId"].Filter();
            if (string.IsNullOrEmpty(Student_HomeWork_Id))//web端批改
            {
                link += string.Format("<li><a href='../student/OHomeWorkViewTTNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>批改详情</a></li><li><li class='active'><a href='../Evaluation/StudentAnalysisReportsNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>答题分析</a></li><li><a href='../Evaluation/StudentErrorAnalysis.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>错题分析</a></li><li><a href='../Evaluation/StudentRemedyScheme.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>补救方案</a></li>", ResourceToResourceFolder_Id, HomeWork_Id, StudentId);
            }
            else//客户端批改
            {
                link += string.Format(@"<li><a href='../student/ohomeworkview_clientNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>批改详情</a></li><li class='active'><a href='../Evaluation/StudentAnalysisReportsNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>答题分析</a></li><li><a href='../Evaluation/StudentErrorAnalysis.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>错题分析</a></li><li><a href='../Evaluation/StudentRemedyScheme.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>补救方案</a></li>"
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
                this.ltlHomeWork_Name.Text = dt.Rows[0]["Resource_Name"].ToString().ReplaceForFilter();
                this.ltlGradeName.Text = dt.Rows[0]["GradeName"].ToString();
                this.ltlClassName.Text = dt.Rows[0]["ClassName"].ToString();
                this.ltlStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                this.ltlHWScore.Text = dt.Rows[0]["HWScore"].ToString().clearLastZero();
                this.ltlStudentScore.Text = dt.Rows[0]["StudentScore"].ToString().clearLastZero();

                #region 获取知识点得分列表
                string strSql = @"	select
	tqs.ResourceToResourceFolder_Id
	,hw.HomeWork_Id
	,stuHW.Student_Id as StudentId
	,left(tqs.ContentText,100) as KPName
    ,ISNULL(SUM(TestQuestions_Score),0) as KPScoreSum
	
	,KPScoreStudentSum=(select ISNULL(SUM(Student_Score),0) from Student_HomeWorkAnswer where Student_Id=stuHW.Student_Id and HomeWork_Id=hw.HomeWork_Id and TestQuestions_Score_ID in(select TestQuestions_Score_ID from TestQuestions_Score where ContentText=tqs.ContentText))
	from TestQuestions_Score tqs
	left join HomeWork hw on hw.ResourceToResourceFolder_Id=tqs.ResourceToResourceFolder_Id
	left join Student_HomeWork stuHW on stuHW.HomeWork_Id=hw.HomeWork_Id	
	where  stuHW.Student_Id='" + StudentId + "' and hw.HomeWork_Id='" + HomeWork_Id + "' group by tqs.ResourceToResourceFolder_Id,hw.HomeWork_Id,stuHW.Student_Id,tqs.ContentText";
                dt_KPScore = DbHelperSQL.Query(strSql).Tables[0];
                #endregion

                LoadDateHw_score();
                LoadStatsClassHW_KP();
            }
            else
            {
                IsTrue = "1";
            }
        }

        //得分概况
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

        //本次作业知识点掌握情况分析
        public void LoadStatsClassHW_KP()
        {

            string StrTemp = string.Empty;
            List<Model_StatsStuHW_KP> StatsClassHW_KP_list = new List<Model_StatsStuHW_KP>();
            BLL_StatsStuHW_KP bll_StatsClassHW_KP = new BLL_StatsStuHW_KP();
            StatsClassHW_KP_list = bll_StatsClassHW_KP.GetModelList("HomeWork_Id='" + HomeWork_Id + "' and Student_Id='" + StudentId + "' order by KPNameBasic");
            foreach (var item in StatsClassHW_KP_list)
            {
                //double kpScore = GetKPScore(item.KPName, 1);
                //double kpScoreStu = GetKPScore(item.KPName, 2);
                //double kpScoreRate = kpScore == 0 ? 0 : kpScoreStu / kpScore * 100;

                StrTemp += string.Format(TempHW_KP
                    , item.KPNameBasic
                    , string.IsNullOrEmpty(item.topicNumber_Right.ToString()) ? "-" : item.topicNumber_Right.ToString()
                    , string.IsNullOrEmpty(item.topicNumber_Wrong.ToString()) ? "-" : item.topicNumber_Wrong.ToString()
                    , item.KPMastery.ToString().clearLastZero());

                strKPName += string.Format("'{0}',", item.KPNameBasic);
                strKPScoreRate += string.Format("{0},", item.KPMastery.ToString().clearLastZero());

            }
            strKPName = strKPName.TrimEnd(',');
            strKPScoreRate = strKPScoreRate.TrimEnd(',');
            this.ltlHW_KP.Text = StrTemp;
        }

        public string GetTQNum(DataTable dtTQ_S, string KPName, string TQNumArr, string TestQuestionNums)//第2题 第3题 第4(1)题 第4(2)题
        {
            try
            {
                string StrTempTQNum = string.Empty;
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
                        StrTempTQNum += "第" + arr[i].TrimEnd('.') + testIndex + "题&nbsp;&nbsp;";
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
        /// 知识点得分
        /// </summary>
        /// <param name="ContentText"></param>
        /// <param name="type">1知识点分数，2知识点得分</param>
        /// <returns></returns>
        public double GetKPScore(string ContentText, int type)
        {
            try
            {
                DataRow[] dr = dt_KPScore.Select("KPName='" + ContentText + "'");
                if (dr.Length > 0)
                {
                    if (type == 1)
                    {
                        return Convert.ToDouble(dr[0]["KPScoreSum"].ToString().clearLastZero());
                    }
                    else
                    {
                        return Convert.ToDouble(dr[0]["KPScoreStudentSum"].ToString().clearLastZero());
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}