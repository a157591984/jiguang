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

namespace Rc.Cloud.Web.parent
{
    public partial class oHomeWorkViewT : Rc.Cloud.Web.Common.FInitData
    {
        protected string ResourceToResourceFolder_Id = string.Empty;
        protected bool isShowWrong = false;
        protected string HomeWork_Name = string.Empty;
        protected string HomeWork_Id = string.Empty;
        protected string StudentId = string.Empty;
        DataTable dtTQ_StudentAnswerCount = new DataTable();//学生答题状态数量
        List<Model_StatsClassHW_TQ> listHW_TQ = new List<Model_StatsClassHW_TQ>();//小题统计数据
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["wrong"] == "w")
                {
                    isShowWrong = true;
                }
                HomeWork_Name = Server.UrlDecode(Request.QueryString["HomeWork_Name"].Filter());
                ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
                HomeWork_Id = Request.QueryString["HomeWork_Id"].Filter();
                StudentId = Request.QueryString["StudentId"].Filter();
                List<Model_StatsClassStudentHW_Score> listSCSHW = new BLL_StatsClassStudentHW_Score().GetModelList(" HomeWork_ID='" + HomeWork_Id + "' and  StudentId='" + StudentId + "' ");
                if (listSCSHW.Count > 0)
                {
                    this.Title = this.ltlTitle.Text = listSCSHW[0].Resource_Name;
                    ltlGradeSubject.Text = string.Format("<span>年级：{0}</span><span>班级：{1}</span><span>学生：{2}</span><span>满分：{3}分</span><span>得分：{4}分</span>"
                    , listSCSHW[0].GradeName
                    , listSCSHW[0].ClassName
                    , listSCSHW[0].StudentName
                    , listSCSHW[0].HWScore.ToString().clearLastZero()
                    , listSCSHW[0].StudentScore.ToString().clearLastZero());
                }
                listHW_TQ = new BLL_StatsClassHW_TQ().GetModelList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and HomeWork_Id='" + HomeWork_Id + "' ");

                string strSql = string.Format(@"select TestQuestions_Id,TestQuestions_Score_ID,Student_Answer_Status,COUNT(1) as ICOUNT from Student_HomeWorkAnswer
where HomeWork_ID='{0}' group by TestQuestions_Id,TestQuestions_Score_ID,Student_Answer_Status", HomeWork_Id);
                dtTQ_StudentAnswerCount = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                if (!IsPostBack)
                {

                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message.ToString());
                Response.Write("页面加载失败...");
                Response.End();
            }
        }

        protected string GetAvgScore(string TestQuestions_Score_ID)
        {
            string temp = string.Empty;
            try
            {
                TestQuestions_Score_ID = TestQuestions_Score_ID.Filter();
                temp = "{0} 分 (答对{1}人/答错{2}人/不全对{3}人/班级得分率{4}%）";
                List<Model_StatsClassHW_TQ> list = listHW_TQ.Where(w => w.TestQuestions_Score_ID == TestQuestions_Score_ID).ToList();
                int rightCount = 0;
                int wrongCount = 0;
                int partrightCount = 0;
                DataRow[] drRight = dtTQ_StudentAnswerCount.Select("Student_Answer_Status='right' and TestQuestions_Score_ID='" + TestQuestions_Score_ID + "' ");
                if (drRight.Length != 0) int.TryParse(drRight[0]["ICOUNT"].ToString(), out rightCount);

                DataRow[] drWrong = dtTQ_StudentAnswerCount.Select("Student_Answer_Status='wrong' and TestQuestions_Score_ID='" + TestQuestions_Score_ID + "' ");
                if (drWrong.Length != 0) int.TryParse(drWrong[0]["ICOUNT"].ToString(), out wrongCount);

                DataRow[] drpartRight = dtTQ_StudentAnswerCount.Select("Student_Answer_Status='partright' and TestQuestions_Score_ID='" + TestQuestions_Score_ID + "' ");
                if (drpartRight.Length != 0) int.TryParse(drpartRight[0]["ICOUNT"].ToString(), out partrightCount);

                temp = string.Format(temp
                    , list[0].ScoreAvg.ToString().clearLastZero()
                    , rightCount
                    , wrongCount
                    , partrightCount
                    , list[0].ScoreAvgRate.ToString().clearLastZero());

            }
            catch (Exception)
            {
                temp = "";
            }
            return temp;
        }

    }
}