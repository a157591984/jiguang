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
using System.Web.Services;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
namespace Rc.Cloud.Web.Evaluation
{
    //public partial class StudentAnalysisReports : Rc.Cloud.Web.Common.FInitData
    //注意此处不能集成基类FInitData，原因是 可以多端可以直接调用此页面
    public partial class StudentAnalysisReportsNew2 : System.Web.UI.Page
    {
        protected string HomeWork_Id = string.Empty;
        protected string StudentId = string.Empty;
        protected string IsTrue = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        public string TempHw_Score = "<tr><td>{0}</td><td>{1}</td><td>{2}</td>";
        public string Temp_HwStudent_Score = "<td>{0}</td><td>第{1}名</td><td>{3}</td><td>第{2}层次</td><td>{4}</td></tr>";
        public string TempHW_KP = "<tr><td class=\"text-left\">{0}</td> <td class='text-left'>{1}</td><td class='text-left'>{2}</td><td>{3}%</td></tr>";
        public string TempHW_TQ = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}%</td></tr>";
        DataTable dt_KPScore = new DataTable();
        DataTable dtTQ_S = new DataTable();
        protected string link = string.Empty;

        protected string strKPName = string.Empty;
        protected string strKPScoreRate = string.Empty;
        protected string SumTrueTQScore = string.Empty;//正确空数量
        protected string SumFlaseTQScore = string.Empty;//错误空数量
        protected decimal TQFalseAvg = 0;//失分点比例
        protected decimal TQTrueAvg = 0;//得分点比例
        protected string strTQNum = string.Empty;//题号
        protected string strTQScore = string.Empty;//得分
        protected string strComplexityText = string.Empty;//难易度名称难易度占比
        BLL_StatsClassHW_Score bll_StatsClassHW_Score = new BLL_StatsClassHW_Score();

        protected void Page_Load(object sender, EventArgs e)
        {
            string Student_HomeWork_Id = Request.QueryString["Student_HomeWork_Id"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            HomeWork_Id = Request.QueryString["HomeWork_Id"].Filter();
            StudentId = Request.QueryString["StudentId"].Filter();
            if (string.IsNullOrEmpty(Student_HomeWork_Id))//web端批改
            {
                link += string.Format("<li><a href='../student/OHomeWorkViewTTNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>批改详情</a></li><li><li class='active'><a href='../Evaluation/StudentAnalysisReportsNew2.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>分析报告</a></li>", ResourceToResourceFolder_Id, HomeWork_Id, StudentId);
            }
            else//客户端批改
            {
                link += string.Format(@"<li><a href='../student/ohomeworkview_clientNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>批改详情</a></li><li class='active'><a href='../Evaluation/StudentAnalysisReportsNew2.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>分析报告</a></li>"
                    , ResourceToResourceFolder_Id
                    , HomeWork_Id
                    , StudentId
                    , Student_HomeWork_Id);
            }
            if (!IsPostBack)
            {
                #region 分值表
                dtTQ_S = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num,TestQuestions_OrderNum ").Tables[0];
                #endregion
                GetComplexityTextData();
                LoadDateHw_score();
                LoadStatsClassHW_KP();
                LoadStatsClassHW_TQ();
                GetTQFalseAnalysis();
                GetImprovingSuggestions();
                GetWroghtTQ();
                if (!string.IsNullOrEmpty(HomeWork_Id) && !string.IsNullOrEmpty(StudentId))
                {
                    string sql = string.Format(@"select  Resource_Name,HomeWork_ID,GradeName,ClassName,StudentScore,HWScore,SubjectName,StudentName,CountTQ=(select count(*) from StatsStuHW_TQ_Type where HomeWork_Id=t.HomeWork_ID and Student_Id=t.StudentID) from 
[dbo].[StatsClassStudentHW_Score] t
where  HomeWork_Id='{0}'  and StudentID='{1}' ", HomeWork_Id, StudentId);
                    DataTable dtHw = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                    if (dtHw.Rows.Count > 0)
                    {

                        ltlHwName.Text = dtHw.Rows[0]["Resource_Name"].ToString();
                        ltlGradeName.Text = dtHw.Rows[0]["GradeName"].ToString();
                        ltlClassName.Text = dtHw.Rows[0]["ClassName"].ToString();
                        ltlSubjectName.Text = dtHw.Rows[0]["SubjectName"].ToString();
                        ltlCountTQ.Text = dtHw.Rows[0]["CountTQ"].ToString().clearLastZero();
                        ltlHwScore.Text = dtHw.Rows[0]["HWScore"].ToString().clearLastZero();
                        ltlStuScore.Text = dtHw.Rows[0]["StudentScore"].ToString().clearLastZero();
                    }
                    else
                    { IsTrue = "1"; }

                }
                else
                {
                    IsTrue = "1";
                }

            }
        }
        /// <summary>
        /// 获取空的难易度占比
        /// </summary>
        public void GetComplexityTextData()
        {
            try
            {
                //['容易', 45.0]
                string sql = string.Format(@"select count(*) countData,ComplexityText from StatsStuHW_TQ_Score t 
inner join [dbo].[Common_Dict] cd on cd.D_Name=t.ComplexityText where HomeWork_Id='{0}' and Student_Id='{1}' group by ComplexityText,cd.D_Order order by D_Order", HomeWork_Id, StudentId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                decimal CountScore = new BLL_StatsStuHW_TQ_Score().GetRecordCount("HomeWork_Id='" + HomeWork_Id + "' and Student_Id='" + StudentId + "'");//总共多少空
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        strComplexityText += item["ComplexityText"] + "," + decimal.Round(Convert.ToDecimal(item["countData"]) / CountScore * 100, 2) + "|";
                    }
                    strComplexityText = strComplexityText.TrimEnd('|');

                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// 学生作业-试题题型表
        /// </summary>
        [WebMethod]
        public static string GetTQ_Type(string HomeWork_Id, string StudentId)
        {
            try
            {
                HomeWork_Id = HomeWork_Id.Filter();
                StudentId = StudentId.Filter();
                string CountTQ_Type = string.Empty;
                string TQ_Type = string.Empty;
                string sql = string.Format(@"select count(TQ_Type) as CountTQ_Type,TQ_Type from StatsStuHW_TQ_Type where  HomeWork_Id='{0}' and Student_Id='{1}'  group by TQ_Type order by TQ_Type", HomeWork_Id, StudentId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        CountTQ_Type += item["CountTQ_Type"] + ",";
                        TQ_Type += item["TQ_Type"] + ",";
                    }
                    return CountTQ_Type.TrimEnd(',') + "|" + TQ_Type.TrimEnd(',');
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 学生作业答题析报告-知识点分析表(所有)
        /// </summary>
        /// <param name="HomeWork_Id"></param>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStatsStuHW_KPAll(string HomeWork_Id, string StudentId)
        {
            try
            {
                HomeWork_Id = HomeWork_Id.Filter();
                StudentId = StudentId.Filter();
                int inum = 1;
                List<object> listReturn = new List<object>();
                BLL_StatsStuHW_KP bll_StatsClassHW_KP = new BLL_StatsStuHW_KP();
                DataTable dt = bll_StatsClassHW_KP.GetList("HomeWork_Id='" + HomeWork_Id + "' and Student_Id='" + StudentId + "' order by KPNameBasic").Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        KPNameBasic = dt.Rows[i]["KPNameBasic"].ToString(),
                        topicNumber = dt.Rows[i]["topicNumber"].ToString(),
                        TestType = dt.Rows[i]["TestType"].ToString(),
                        SumScore = dt.Rows[i]["SumScore"].ToString().clearLastZero()
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
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
                    err = "暂无数据"
                });
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
        /// <summary>
        /// 按知识点分析
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetListKP(string HomeWork_Id, string Student_Id)
        {
            try
            {
                HomeWork_Id = HomeWork_Id.Filter();
                Student_Id = Student_Id.Filter();
                string strWhere = "1=1 ";
                if (!string.IsNullOrEmpty(HomeWork_Id))
                {
                    strWhere += " and  HomeWork_Id = '" + HomeWork_Id + "'";
                }
                if (!string.IsNullOrEmpty(Student_Id))
                {
                    strWhere += " and  Student_Id = '" + Student_Id + "'";
                }
                DataTable dt = new DataTable();
                int intRecordCount = 0;
                List<object> listReturn = new List<object>();
                int inum = 1;
                BLL_StatsStuHW_Wrong_KP bll = new BLL_StatsStuHW_Wrong_KP();
                dt = bll.GetList(strWhere + " order by KPNameBasic ").Tables[0];
                intRecordCount = bll.GetRecordCount(strWhere);
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        KPImportant = GetKPImportant(string.IsNullOrEmpty(dt.Rows[i]["KPImportant"].ToString()) ? "0" : dt.Rows[i]["KPImportant"].ToString()),
                        KPNameBasic = dt.Rows[i]["KPNameBasic"].ToString(),
                        GKScore = dt.Rows[i]["GKScore"].ToString().clearLastZero(),
                        ComplexityText = dt.Rows[i]["ComplexityText"].ToString(),
                        TestType = dt.Rows[i]["TestType"].ToString(),
                        topicNumber = dt.Rows[i]["topicNumber"].ToString(),
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
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
        /// <summary>
        /// 按题分析
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetListTQ(string HomeWork_Id, string Student_Id)
        {
            try
            {
                HomeWork_Id = HomeWork_Id.Filter();
                Student_Id = Student_Id.Filter();
                string strWhere = "1=1 ";
                if (!string.IsNullOrEmpty(HomeWork_Id))
                {
                    strWhere += " and  HomeWork_Id = '" + HomeWork_Id + "'";
                }
                if (!string.IsNullOrEmpty(Student_Id))
                {
                    strWhere += " and  Student_Id = '" + Student_Id + "'";
                }
                DataTable dt = new DataTable();
                int intRecordCount = 0;
                List<object> listReturn = new List<object>();
                int inum = 1;
                BLL_StatsStuHW_Wrong_TQ bll = new BLL_StatsStuHW_Wrong_TQ();
                dt = bll.GetList(strWhere + " order by TestQuestions_Num").Tables[0];
                intRecordCount = bll.GetRecordCount(strWhere);
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        KPImportant = GetKPImportant1(dt.Rows[i]["KPImportant"].ToString()),
                        KPNameBasic = dt.Rows[i]["KPNameBasic"].ToString(),
                        topicNumber = dt.Rows[i]["topicNumber"].ToString().TrimEnd('.'),
                        TPNameBasic = dt.Rows[i]["TPNameBasic"].ToString().TrimEnd('.'),
                        TQScore = dt.Rows[i]["TQScore"].ToString().clearLastZero(),
                        Score = dt.Rows[i]["Score"].ToString().clearLastZero(),
                        TestType = dt.Rows[i]["TestType"].ToString(),
                        ComplexityText = dt.Rows[i]["ComplexityText"].ToString()
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
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
        /// <summary>
        /// 把重要程度转换成★
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetKPImportant(string value)
        {
            try
            {
                string StrStar = string.Empty;
                for (int i = 0; i < Convert.ToInt32(value); i++)
                {
                    StrStar += "★";
                }
                return StrStar;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GetKPImportant1(string value)
        {
            try
            {
                string StrStar = string.Empty;
                string ArrStar = string.Empty;
                if (string.IsNullOrEmpty(value))
                {
                    return "";
                }
                else
                {
                    string[] arr = value.Split('，');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        for (int j = 0; j < Convert.ToInt32(arr[i].ToString().clearLastZero()); j++)
                        {
                            StrStar += "★";
                        }
                        ArrStar += StrStar + "；";
                    }
                    return ArrStar.TrimEnd('；');
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// 小题得分
        /// </summary>
        public void LoadStatsClassHW_TQ()//小题得分
        {
            string StrTemp = string.Empty;
            List<Model_StatsStuHW_TQ_Score> StatsStuHW_TQ_Score_list = new List<Model_StatsStuHW_TQ_Score>();
            BLL_StatsStuHW_TQ_Score bll_StatsStuHW_TQ_Score = new BLL_StatsStuHW_TQ_Score();
            StatsStuHW_TQ_Score_list = bll_StatsStuHW_TQ_Score.GetModelList("HomeWork_ID='" + HomeWork_Id + "' and Student_Id='" + StudentId + "' order by TestQuestions_Num,testIndex");

            foreach (var item in StatsStuHW_TQ_Score_list)
            {
                DataRow[] drTQ_S = dtTQ_S.Select("TestQuestions_Score_ID='" + item.StatsStuHW_TQ_Score_Id + "'");

                StrTemp += string.Format(TempHW_TQ
                    , !string.IsNullOrEmpty(item.testIndex.ToString()) ? item.topicNumber.ToString() + item.testIndex.ToString() : item.topicNumber.ToString().TrimEnd('.')
                    , item.TestType
                    , item.KPNameBasic
                    , item.ComplexityText
                    , item.TQScore.ToString().clearLastZero()
                    , item.Score.ToString().clearLastZero()
                    , item.TQScoreAvg.ToString().clearLastZero()
                    , item.TQScoreAvgRate.ToString().clearLastZero());
                strTQNum += !string.IsNullOrEmpty(item.testIndex.ToString()) ? item.topicNumber.ToString() + item.testIndex.ToString() + "," : item.topicNumber.ToString().TrimEnd('.') + ",";
                strTQScore += item.Score.ToString().clearLastZero() + ",";
            }
            strTQNum = strTQNum.TrimEnd(',');
            strTQScore = strTQScore.TrimEnd(',');
            this.ltlHW_TQScore.Text = StrTemp;
        }
        /// <summary>
        /// 错题分析
        /// </summary>
        public void GetTQFalseAnalysis()
        {
            try
            {
                string temp = "<a href=\"##\" class=\"btn {0} mb\">{1}</a> ";
                string temp1 = "本次作业共 {0} 道题目，{1}个给分点，其中有{2}个点失分，失分率为 {3}%；";
                string StrTQS = string.Empty;
                string sql = string.Format(@"select TestQuestions_Score_Id,topicNumber,TestQuestions_Num,testIndex,Student_Answer_Status from StatsStuHW_TQ_KP where HomeWork_Id='{0}' and Student_Id='{1}'
 group by TestQuestions_Score_Id,topicNumber,TestQuestions_Num,testIndex,Student_Answer_Status order by TestQuestions_Num,testIndex", HomeWork_Id, StudentId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        StrTQS += string.Format(temp
                            , item["Student_Answer_Status"].ToString() != "right" ? "btn-danger" : "btn-success"
                            , !string.IsNullOrEmpty(item["testIndex"].ToString()) ? item["topicNumber"].ToString() + item["testIndex"].ToString() : item["topicNumber"].ToString().TrimEnd('.'));
                    }
                    ltlTQS.Text = StrTQS;
                }
                sql = string.Format(@" select * from (
select count(*) CountData,1 sort from StatsStuHW_TQ_Type  where HomeWork_Id='{0}' and Student_Id='{1}' 
union
select count(*),2 sort from TestQuestions_Score where ResourceToResourceFolder_Id='{2}'
union
select  count(*),3 sort   from  [Student_HomeWorkAnswer] 
where HomeWork_Id='{0}' and Student_Id='{1}' and Student_Answer_Status<>'right'
)t order by sort", HomeWork_Id, StudentId, ResourceToResourceFolder_Id);//1题目数2空数量3错的空数
                DataTable dt1 = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    ltlTQSView.Text = string.Format(temp1, dt1.Rows[0]["CountData"].ToString().clearLastZero()
                                                       , dt1.Rows[1]["CountData"].ToString().clearLastZero()
                                                       , dt1.Rows[2]["CountData"].ToString().clearLastZero()
                                                       , decimal.Round(Convert.ToDecimal(dt1.Rows[2]["CountData"].ToString().clearLastZero()) / Convert.ToDecimal(dt1.Rows[1]["CountData"].ToString().clearLastZero()) * 100, 1));
                    SumTrueTQScore = (Convert.ToInt32(dt1.Rows[1]["CountData"].ToString().clearLastZero()) - Convert.ToInt32(dt1.Rows[2]["CountData"].ToString().clearLastZero())).ToString().clearLastZero();//正确空数量
                    SumFlaseTQScore = dt1.Rows[2]["CountData"].ToString().clearLastZero();//错误空数量
                    TQFalseAvg = decimal.Round(Convert.ToDecimal(dt1.Rows[2]["CountData"].ToString().clearLastZero()) / Convert.ToDecimal(dt1.Rows[1]["CountData"].ToString().clearLastZero()) * 100, 2);//失分点比例
                    TQTrueAvg = 100 - decimal.Round(Convert.ToDecimal(dt1.Rows[2]["CountData"].ToString().clearLastZero()) / Convert.ToDecimal(dt1.Rows[1]["CountData"].ToString().clearLastZero()) * 100, 2);//得分点比例
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 补救提高建议
        /// </summary>
        public void GetImprovingSuggestions()
        {
            try
            {
                string temp = "<tr><td rowspan=\"{0}\">{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}%</td></tr>";
                string temp1 = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}%</td></tr>";
                string Str = string.Empty;
                string sql = string.Empty;
                DataTable dtP = new DataTable();
                DataTable dtA = new DataTable();
                sql = string.Format(@"select count(Chapter) countRow,Chapter from StatsStuHW_Wrong_KP where HomeWork_Id='{0}' and Student_Id='{1}'  group by Chapter", HomeWork_Id, StudentId);
                dtP = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                sql = string.Format(@"select * from StatsStuHW_Wrong_KP where HomeWork_Id='{0}' and Student_Id='{1}'", HomeWork_Id, StudentId);
                dtA = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dtP.Rows.Count > 0 && dtA.Rows.Count > 0)
                {
                    for (int i = 0; i < dtP.Rows.Count; i++)
                    {
                        DataRow[] dr = dtA.Select("Chapter='" + dtP.Rows[i]["Chapter"] + "'");
                        if (dr.Length > 0)
                        {
                            for (int j = 0; j < dr.Length; j++)
                            {
                                if (j == 0)
                                {
                                    Str += string.Format(temp, dtP.Rows[i]["countRow"].ToString()
                                                              , dtP.Rows[i]["Chapter"].ToString()
                                                              , dr[j]["KPNameBasic"].ToString()
                                                              , GetKPImportant(dr[j]["KPImportant"].ToString())
                                                              , dr[j]["GKScore"].ToString().clearLastZero()
                                                              , decimal.Round(100 - Convert.ToDecimal(dr[j]["KPMastery"].ToString()), 2));
                                }
                                else
                                {
                                    Str += string.Format(temp1
                                        , dr[j]["KPNameBasic"].ToString()
                                        , GetKPImportant(dr[j]["KPImportant"].ToString())
                                        , dr[j]["GKScore"].ToString().clearLastZero()
                                        , decimal.Round(100 - Convert.ToDecimal(dr[j]["KPMastery"].ToString()), 2));
                                }

                            }
                        }
                    }
                }
                ltlImprovingSuggestions.Text = Str;

            }
            catch (Exception ez)
            {

                throw;
            }
        }

        /// <summary>
        /// 错题重练
        /// </summary>
        public void GetWroghtTQ()
        {
            try
            {
                string temp = "请同学对本次作业中的{0}进行再次练习，以便从中总结规律、方法、技巧，加强知识的巩固和学习";
                string str = string.Empty;
                string sql = string.Format(@" select * from StatsStuHW_Wrong_TQ where HomeWork_Id='{0}' and Student_Id='{1}' order by TestQuestions_Num", HomeWork_Id, StudentId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        str += "第" + item["topicNumber"].ToString().TrimEnd('.') + "题， ";
                    }
                    ltlWroghtTQ.Text = string.Format(temp, str.TrimEnd('，'));
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}