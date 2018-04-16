using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.BLL.Resources;
using Newtonsoft.Json;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.Evaluation
{
    public partial class StudentErrorAnalysis : System.Web.UI.Page
    {
        public string HomeWork_Id = string.Empty;
        public string Student_HomeWork_Id = string.Empty;
        public string StudentId = string.Empty;
        public string link = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            HomeWork_Id = Request["HomeWork_Id"].Filter();
            Student_HomeWork_Id = Request["Student_HomeWork_Id"].Filter();
            StudentId = Request["StudentId"].Filter();
            if (!IsPostBack)
            {
                try
                {
                    Model_HomeWork model = new Model_HomeWork();
                    model = new BLL_HomeWork().GetModel(HomeWork_Id);
                    if (model != null)
                    {
                        ResourceToResourceFolder_Id = model.ResourceToResourceFolder_Id;

                    }
                    #region 读取连接link
                    if (string.IsNullOrEmpty(Student_HomeWork_Id))//web端批改
                    {
                        link += string.Format("<li><a href='../student/OHomeWorkViewTTNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>批改详情</a></li><li><li><a href='../Evaluation/StudentAnalysisReportsNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>答题分析</a></li><li  class='active'><a href='../Evaluation/StudentErrorAnalysis.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>错题分析</a></li><li><a href='../Evaluation/StudentRemedyScheme.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>补救方案</a></li>", ResourceToResourceFolder_Id, HomeWork_Id, StudentId);
                    }
                    else//客户端批改
                    {
                        link += string.Format(@"<li><a href='../student/ohomeworkview_clientNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>批改详情</a></li><li><a href='../Evaluation/StudentAnalysisReportsNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>答题分析</a></li><li  class='active'><a href='../Evaluation/StudentErrorAnalysis.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>错题分析</a></li><li><a href='../Evaluation/StudentRemedyScheme.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>补救方案</a></li>"
                            , ResourceToResourceFolder_Id
                            , HomeWork_Id
                            , StudentId
                            , Student_HomeWork_Id);
                    }
                    #endregion
                    #region 读取作业基本信息
                    DataTable dtw = new DataTable();
                    dtw = Rc.Common.DBUtility.DbHelperSQL.Query(string.Format(@"select hw.*
,vw.GradeName,vw.GradeId,vw.ClassId,vw.ClassName
,u.UserName,u.TrueName
,HWScore=(select SUM(TestQuestions_Score) from TestQuestions_Score where TestQuestions_Score!=-1 and ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id)
,StudentScore=(select SUM(Student_Score) from Student_HomeWorkAnswer where Student_Id='{0}' and HomeWork_Id=hw.HomeWork_Id )
,shwCorrect.Student_HomeWork_CorrectStatus
from HomeWork hw 
inner join Student_HomeWork shw on shw.Student_Id='{0}' and shw.HomeWork_Id=hw.HomeWork_Id 
inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id 
inner join F_User u on u.UserId=shw.Student_Id
left join VW_ClassGradeSchool vw on vw.ClassId=hw.UserGroup_Id and vw.GradeId!=''
where hw.HomeWork_Id='{1}'"
                        , StudentId, HomeWork_Id)).Tables[0];
                    this.Title = dtw.Rows[0]["HomeWork_Name"].ToString();
                    this.ltlHwName.Text = dtw.Rows[0]["HomeWork_Name"].ToString();
                    this.ltlClassName.Text = dtw.Rows[0]["ClassName"].ToString();
                    this.ltlGradeName.Text = dtw.Rows[0]["GradeName"].ToString();
                    this.ltlSundentName.Text = string.IsNullOrEmpty(dtw.Rows[0]["TrueName"].ToString()) ? dtw.Rows[0]["UserName"].ToString() : dtw.Rows[0]["TrueName"].ToString();
                    this.ltlHwSorce.Text = dtw.Rows[0]["HWScore"].ToString().clearLastZero();
                    this.ltlStuScorce.Text = dtw.Rows[0]["StudentScore"].ToString().clearLastZero();
                    #endregion
                }
                catch (Exception)
                {
                    Response.Write("页面加载失败...");
                    Response.End();
                }

            }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="Two_WayChecklist_Id"></param>
        /// <param name="RelationPaper_Id"></param>
        /// <param name="CreateUser"></param>
        /// <returns></returns>
        [WebMethod]
        public static string InitializationData(string HomeWork_Id)
        {
            try
            {
                int row = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec dbo.[P_StatsStuHW_Wrong_KP] '" + HomeWork_Id + "'", 7200);
                if (row > 0)
                {
                    return "1";
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
        /// 获取错题分析综述
        /// </summary>
        /// <param name="HomeWork_Id"></param>
        /// <param name="Student_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetGeneralData(string HomeWork_Id, string Student_Id)
        {
            try
            {
                HomeWork_Id = HomeWork_Id.Filter();
                Student_Id = Student_Id.Filter();
                //string temp = "本次作业一共{0}道题目，错误题目为{1}道，错误率为{2}%，其中涉及的知识点为：{3}，本次错误知识点在中考中最低{4}分，最高{5}分，如果不解决本次作业中的问题将在中考中将可能丢失约占{6}分。";
                string sql = @"select TQCount,TQCountFalse,TQFalseAvg,STUFF(KPCountFalse,len(KPCountFalse),1,'') as KPCountFalse,GKMinScore ,GKMaxScore ,GKSumScore  from (
select 
TQCount=(select count(*) from TestQuestions where TestQuestions_Type<>'' and TestQuestions_Type<>'title' and ResourceToResourceFolder_Id=(select ResourceToResourceFolder_Id from HomeWork where HomeWork_Id= shw.HomeWork_Id))
,TQCountFalse=(select count(*) from StatsStuHW_Wrong_TQ where HomeWork_Id=shw.HomeWork_Id and Student_HomeWork_Id=shw.Student_HomeWork_Id and Student_Id=shw.Student_Id)
,TQFalseAvg=convert(decimal(5,2),(convert(decimal(8,2),(select count(*) from StatsStuHW_Wrong_TQ where HomeWork_Id=shw.HomeWork_Id and Student_HomeWork_Id=shw.Student_HomeWork_Id and Student_Id=shw.Student_Id))/(select count(*) from TestQuestions where TestQuestions_Type<>'' and TestQuestions_Type<>'title' and ResourceToResourceFolder_Id=(select ResourceToResourceFolder_Id from HomeWork where HomeWork_Id= shw.HomeWork_Id))*100))
,KPCountFalse=(select KPNameBasic+'、' FROM StatsStuHW_TQ_KP where HomeWork_Id=shw.HomeWork_Id and  Student_Id=shw.Student_Id and Student_Answer_Status<>'right' group by KPNameBasic FOR XML PATH('') )
,GKMinScore=(select min(S_KnowledgePointAttrValue) from S_KnowledgePointAttrExtend where S_KnowledgePointAttrEnum='attrKnowledgeScore' and S_KnowledgePointBasic_Id in(select skp.S_KnowledgePointBasic_Id from StatsStuHW_TQ_KP ss left join S_KnowledgePoint skp on skp.S_KnowledgePoint_Id=ss.S_KnowledgePoint_Id  where ss.HomeWork_Id=shw.HomeWork_Id and ss.Student_Id=shw.Student_Id and  Student_Answer_Status<>'right'))
,GKMaxScore=(select max(S_KnowledgePointAttrValue) from S_KnowledgePointAttrExtend where S_KnowledgePointAttrEnum='attrKnowledgeScore' and S_KnowledgePointBasic_Id in(select skp.S_KnowledgePointBasic_Id from StatsStuHW_TQ_KP ss left join S_KnowledgePoint skp on skp.S_KnowledgePoint_Id=ss.S_KnowledgePoint_Id where ss.HomeWork_Id=shw.HomeWork_Id and ss.Student_Id=shw.Student_Id and Student_Answer_Status<>'right'))
,GKSumScore=(select sum(S_KnowledgePointAttrValue) from S_KnowledgePointAttrExtend where S_KnowledgePointAttrEnum='attrKnowledgeScore' and S_KnowledgePointBasic_Id in(select skp.S_KnowledgePointBasic_Id from StatsStuHW_TQ_KP ss left join S_KnowledgePoint skp on skp.S_KnowledgePoint_Id=ss.S_KnowledgePoint_Id where ss.HomeWork_Id=shw.HomeWork_Id and ss.Student_Id=shw.Student_Id and Student_Answer_Status<>'right'))
 from Student_HomeWork shw
where shw.HomeWork_Id='" + HomeWork_Id + "' and Student_Id='" + Student_Id + "') a";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //string str = string.Format(temp, dt.Rows[0]["TQCount"].ToString().clearLastZero()
                    //                             , dt.Rows[0]["TQCountFalse"].ToString().clearLastZero()
                    //                             , dt.Rows[0]["TQFalseAvg"].ToString().clearLastZero()
                    //                             , dt.Rows[0]["KPCountFalse"].ToString()
                    //                             , dt.Rows[0]["GKMinScore"].ToString().clearLastZero()
                    //                             , dt.Rows[0]["GKMaxScore"].ToString().clearLastZero()
                    //                             , dt.Rows[0]["GKSumScore"].ToString().clearLastZero());
                    string result = string.Empty;
                    if (dt.Rows[0]["TQCount"].ToString().clearLastZero() != "0")
                    {
                        result += string.Format("本次作业一共{0}道题目", dt.Rows[0]["TQCount"].ToString().clearLastZero());

                        if (dt.Rows[0]["TQCountFalse"].ToString().clearLastZero() != "0")
                        {
                            result += string.Format("，错误题目为{0}道，错误率为{1}%，其中涉及的知识点为：{2}。"
                                , dt.Rows[0]["TQCountFalse"].ToString().clearLastZero()
                                , dt.Rows[0]["TQFalseAvg"].ToString().clearLastZero()
                                , dt.Rows[0]["KPCountFalse"].ToString());
                        }
                    }
                    
                    return result;
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
                        topicNumber = dt.Rows[i]["topicNumber"].ToString(),
                        TPNameBasic = dt.Rows[i]["TPNameBasic"].ToString().TrimEnd('.'),
                        TQScore = dt.Rows[i]["TQScore"].ToString().clearLastZero(),
                        Score = dt.Rows[i]["Score"].ToString().clearLastZero(),
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
    }
}