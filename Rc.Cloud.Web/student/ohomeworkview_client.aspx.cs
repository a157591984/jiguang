using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Data;
using System.Text;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.student
{
    public partial class ohomeworkview_client : System.Web.UI.Page
    {
        protected string ResourceToResourceFolder_Id = string.Empty;
        protected bool isShowWrong = false;
        protected string HomeWork_Id = string.Empty;
        protected string StudentId = string.Empty;
        DataTable dtTQ_StudentAnswerCount = new DataTable();//学生答题状态数量
        List<Model_StatsClassHW_TQ> listHW_TQ = new List<Model_StatsClassHW_TQ>();//小题统计数据
        protected bool isCorrect = false;
        protected string stuHomeWorkId = string.Empty;
        protected Model_HomeWork modelHW = new Model_HomeWork();
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                StudentId = Request.QueryString["StudentId"].Filter();

                strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");
                if (!string.IsNullOrEmpty(StudentId) && strTestpaperViewWebSiteUrl == Rc.Common.ConfigHelper.GetConfigString("TestWebSiteUrl"))
                {
                    strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl", StudentId);
                }

                stuHomeWorkId = Request.QueryString["Student_HomeWork_Id"].Filter();
                string reqWrong = Request["wrong"].Filter();
                if (reqWrong == "w") isShowWrong = true;

                ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
                HomeWork_Id = Request.QueryString["HomeWork_Id"].Filter();

                modelHW = new BLL_HomeWork().GetModel(HomeWork_Id);
                string tempSql = string.Empty;
                tempSql = string.Format(@"select COUNT(1) as icount from Student_HomeWork shw inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id  where HomeWork_Id='{0}' 
and Student_Id='{1}'
and shwCorrect.Student_HomeWork_CorrectStatus='{2}'", HomeWork_Id, StudentId, "1");
                int i = int.Parse(Rc.Common.DBUtility.DbHelperSQL.GetSingle(tempSql).ToString());
                if (i > 0)
                {
                    //}
                    List<Model_StatsClassStudentHW_Score> listSCSHW = new BLL_StatsClassStudentHW_Score().GetModelList(" HomeWork_ID='" + HomeWork_Id + "' and  StudentId='" + StudentId + "' ");
                    if (listSCSHW.Count > 0)
                    {
                        isCorrect = true;
                        this.Title = listSCSHW[0].Resource_Name.ReplaceForFilter();
                        this.ltlTitle.Text = listSCSHW[0].Resource_Name.ReplaceForFilter() + "<span class='correct_state bg-success'>已完成批改</span>";
                        ltlGradeSubject.Text = string.Format("<span>年级：{0}</span><span>班级：{1}</span><span>学生：{2}</span><span>满分：{3}分</span><span>得分：{4}分</span>"
                        , listSCSHW[0].GradeName
                        , listSCSHW[0].ClassName
                        , listSCSHW[0].StudentName
                        , listSCSHW[0].HWScore.ToString().clearLastZero()
                        , listSCSHW[0].StudentScore.ToString().clearLastZero());
                    }
                }
                else
                {
                    #region 未批改
                    DataTable dtw = new DataTable();
                    dtw = Rc.Common.DBUtility.DbHelperSQL.Query(string.Format(@"select hw.*
,vw.GradeName,vw.GradeId,vw.ClassId,vw.ClassName
,u.UserName,u.TrueName
,HWScore=(select SUM(TestQuestions_Score) from TestQuestions_Score where ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id)
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
                    this.ltlTitle.Text = dtw.Rows[0]["HomeWork_Name"].ToString() + "<span class='correct_state bg-warning'>未完成批改</span>";
                    ltlGradeSubject.Text = string.Format("<span>年级：{0}</span><span>班级：{1}</span><span>学生：{2}</span><span>满分：{3}分</span><span>得分：{4}分</span>"
                    , dtw.Rows[0]["GradeName"]
                    , dtw.Rows[0]["ClassName"]
                    , string.IsNullOrEmpty(dtw.Rows[0]["TrueName"].ToString()) ? dtw.Rows[0]["UserName"] : dtw.Rows[0]["TrueName"]
                    , dtw.Rows[0]["HWScore"].ToString().clearLastZero()
                    , dtw.Rows[0]["StudentScore"].ToString().clearLastZero());
                    if (dtw.Rows[0]["Student_HomeWork_CorrectStatus"].ToString() == "1")
                    {
                        isCorrect = true;
                    }
                    #endregion
                }
                StringBuilder stbLink = new StringBuilder();
                stbLink.AppendFormat("<li {3}><a href=\"ohomeworkview_client.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={4}\">批改详情</a></li>"
                    , ResourceToResourceFolder_Id
                    , HomeWork_Id
                    , StudentId
                    , (isShowWrong ? "" : "class=\"active\"")
                    , stuHomeWorkId);
                if (isCorrect)
                {
                    stbLink.AppendFormat("<li {3}><a href=\"ohomeworkview_client.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&wrong=w&Student_HomeWork_Id={4}\">错题详情</a></li>"
                    , ResourceToResourceFolder_Id
                    , HomeWork_Id
                    , StudentId
                    , (isShowWrong ? "class=\"active\"" : "")
                    , stuHomeWorkId);
                    stbLink.AppendFormat("<li><a href=\"../Evaluation/StudentAnalysisReports.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}\">分析报告</a></li>"
                    , ResourceToResourceFolder_Id
                    , HomeWork_Id
                    , StudentId
                    , stuHomeWorkId);
                }
                ltlLink.Text = stbLink.ToString();
                if (!IsPostBack)
                {

                }
            }
            catch (Exception)
            {
                Response.Write("页面加载失败...");
                Response.End();
            }
        }
    }
}