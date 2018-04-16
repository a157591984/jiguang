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
using System.Web.Services;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.Evaluation
{
    public partial class CommentReport : Rc.Cloud.Web.Common.FInitData
    {
        public string HomeWork_Id = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected DataTable dtTQ = new DataTable();//试题数据
        
        
        
        
        protected string strHomeWorkId = string.Empty;
        protected string visit_web_id = string.Empty;
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");
            try
            {
                HomeWork_Id = Request.QueryString["HomeWork_Id"];
                ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(HomeWork_Id))
                    {
                        BLL_StatsClassHW_Score bllschwso = new BLL_StatsClassHW_Score();
                        DataTable dt = bllschwso.GetList("HomeWork_ID='" + HomeWork_Id + "' ").Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            string strSql = string.Format(@"select t.*,t2.Resource_Name
,t3.GradeName,t3.ClassName
,t4.D_Name as SubjectName
,(case when t5.TrueName is null then t5.UserName when t5.TrueName='' then t5.UserName else t5.UserName end) as TeacherName
,HW_Score=(select ISNULL(SUM(TestQuestions_Score),0) from TestQuestions_Score where ResourceToResourceFolder_Id=t.ResourceToResourceFolder_Id)
,t.HomeWork_AssignTeacher as TeacherID,t.UserGroup_Id as ClassId
from HomeWork t 
inner join ResourceToResourceFolder t2 on t2.ResourceToResourceFolder_Id=t.ResourceToResourceFolder_Id
inner join VW_ClassGradeSchool t3 on t3.ClassId=t.UserGroup_Id
inner join Common_Dict t4 on t4.Common_Dict_ID=t.SubjectId
inner join F_User t5 on t5.UserId=t.HomeWork_AssignTeacher
where t.HomeWork_Id='{0}'", HomeWork_Id);
                            dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        }
                        if (dt.Rows.Count > 0)
                        {
                            ltlHomeWork_Name.Text = dt.Rows[0]["Resource_Name"].ToString().ReplaceForFilter();
                            ltlGrade.Text = dt.Rows[0]["GradeName"].ToString();
                            ltlClass.Text = dt.Rows[0]["ClassName"].ToString();
                            ltlSubjectName.Text = dt.Rows[0]["SubjectName"].ToString();
                            ltlTeacherName.Text = dt.Rows[0]["TeacherName"].ToString();
                            ltlSumSore.Text = dt.Rows[0]["HW_Score"].ToString().clearLastZero();
                            strHomeWorkId = " and HomeWork_Id='" + HomeWork_Id + "' ";

                            RecordVisit_Web(dt.Rows[0]["TeacherID"].ToString(), dt.Rows[0]["ClassID"].ToString());
                        }
                    }
                    LoadData();
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 讲评报告访问情况
        /// </summary>
        private void RecordVisit_Web(string tch_id, string class_id)
        {
            visit_web_id = Guid.NewGuid().ToString();
            Model_visit_web modelVW = new Model_visit_web();
            modelVW.visit_web_id = visit_web_id;
            modelVW.user_id = tch_id;
            modelVW.resource_data_id = HomeWork_Id;
            modelVW.class_id = class_id;
            modelVW.open_time = DateTime.Now;
            modelVW.close_time = DateTime.Now.AddMinutes(1);
            new BLL_visit_web().Add(modelVW);

        }

        [WebMethod]
        public static string UpdateRecordVisitCloseTime(string visit_web_id)
        {
            try
            {
                visit_web_id = visit_web_id.Filter();
                BLL_visit_web bll = new BLL_visit_web();
                Model_visit_web modelVW = bll.GetModel(visit_web_id);
                modelVW.close_time = DateTime.Now;
                bll.Update(modelVW);
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }

        }

        private void LoadData()
        {
            string strSql = string.Format(@"select *
,StudentAvgScoreRateStyle=(case when StudentAvgScoreRate<60 then 'danger' when StudentAvgScoreRate<70 then 'warning'  when StudentAvgScoreRate<85 then 'info' else 'success' end)
from (select *
,StudentAvgScoreRate=(case when TestQuestions_SumScore=0 or StudentCount=0 then 0 else CONVERT(numeric(8,2),StudentSumScore/StudentCount/TestQuestions_SumScore*100) end) 
from (
select tq.TestQuestions_Id,tq.ResourceToResourceFolder_Id,tq.TestQuestions_Num,tq.TestQuestions_Type,tq.TestQuestions_SumScore,tq.topicNumber 
,StudentSumScore=(select ISNULL(SUM(Student_Score),0) from Student_HomeWorkAnswer where TestQuestions_Id=tq.TestQuestions_Id {0})
,StudentCount=(select Count(1) from ((select distinct Student_Id from Student_HomeWorkAnswer where TestQuestions_Id=tq.TestQuestions_Id {0}))t) 
from TestQuestions tq
where tq.ResourceToResourceFolder_Id='{1}'
) t ) temp order by TestQuestions_Num"
                , strHomeWorkId
                , ResourceToResourceFolder_Id.Filter());
            dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            DataTable dtNew = dtTQ;
            DataRow[] drTQ = null;
            if (dtNew.Rows.Count > 0)
            {
                drTQ = dtNew.Select("TestQuestions_Type<>'title' and TestQuestions_Type<>''", "TestQuestions_Num");
                if (drTQ != null && drTQ.Length > 0)
                {
                    dtNew = drTQ.CopyToDataTable();
                    rptTQ.DataSource = dtNew;
                    rptTQ.DataBind();
                }
            }

           

            
            
        }


    }
}