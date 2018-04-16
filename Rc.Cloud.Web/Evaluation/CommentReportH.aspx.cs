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
using System.Diagnostics;

namespace Rc.Cloud.Web.Evaluation
{
    public partial class CommentReportH : Rc.Cloud.Web.Common.FInitData
    {
        public string StatsClassHW_ScoreID = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        public string HomeWorkId = string.Empty;
        protected DataTable dtTQ = new DataTable();//试题数据
        protected DataTable dtTQ_StudentAnswer = new DataTable();//学生答题情况
        protected DataTable dtTQ_StudentAnswerNew = new DataTable();//学生答题情况单选或者判断
        List<Model_StatsClassHW_TQ> listHW_TQ = new List<Model_StatsClassHW_TQ>();//小题统计数据
        protected string strHomeWorkId = string.Empty;
        protected Stopwatch timer = new System.Diagnostics.Stopwatch();
        protected void Page_Load(object sender, EventArgs e)
        {
            timer.Start();
            try
            {
                ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
                StatsClassHW_ScoreID = Request.QueryString["StatsClassHW_ScoreID"].Filter();
                HomeWorkId = Request.QueryString["HomeWorkId"].Filter();
                if (!IsPostBack)
                {
                    CommonHandel bllc = new CommonHandel();
                    DataTable dt = bllc.GetHomeWorkInfo("HomeWork_Id='" + HomeWorkId + "'").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ltlHomeWork_Name.Text = dt.Rows[0]["HomeWork_Name"].ToString();
                        ltlGrade.Text = dt.Rows[0]["GradeName"].ToString();
                        ltlClass.Text = dt.Rows[0]["ClassName"].ToString();
                        ltlSubjectName.Text = dt.Rows[0]["SubjectName"].ToString();
                        ltlTeacherName.Text = dt.Rows[0]["TeacherName"].ToString();
                        ltlSumSore.Text = dt.Rows[0]["HW_Score"].ToString().clearLastZero();
                        strHomeWorkId = " and HomeWork_Id='" + HomeWorkId + "' ";

                    }

                    LoadData();
                }
            }
            catch (Exception)
            {

            }
        }

        private void LoadData()
        {
            string strSql = string.Format(@"select * 
from (
select tq.TestQuestions_Id,tq.ResourceToResourceFolder_Id,tq.TestQuestions_Num,tq.TestQuestions_Type,tq.TestQuestions_SumScore,tq.topicNumber 
from TestQuestions tq
where tq.TestQuestions_Type!='title' and tq.ResourceToResourceFolder_Id='{1}'
) temp order by TestQuestions_Num"
                , strHomeWorkId
                , ResourceToResourceFolder_Id.Filter());
            dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            rptTQ.DataSource = dtTQ;
            rptTQ.DataBind();
            listHW_TQ = new BLL_StatsClassHW_TQ().GetModelList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id.Filter() + "' " + strHomeWorkId);

        }
        
    }
}