using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Common.StrUtility;
using System.Data;
namespace Rc.Cloud.Web.Evaluation
{
    public partial class StudentRemedyScheme : System.Web.UI.Page
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
                Model_HomeWork model = new Model_HomeWork();
                model = new BLL_HomeWork().GetModel(HomeWork_Id);
                if (model != null)
                {
                    ResourceToResourceFolder_Id = model.ResourceToResourceFolder_Id;

                }
                #region 读取连接link
                if (string.IsNullOrEmpty(Student_HomeWork_Id))//web端批改
                {
                    link += string.Format("<li><a href='../student/OHomeWorkViewTTNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>批改详情</a></li><li><li><a href='../Evaluation/StudentAnalysisReportsNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>答题分析</a></li><li><a href='../Evaluation/StudentErrorAnalysis.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>错题分析</a></li><li  class='active'><a href='../Evaluation/StudentRemedyScheme.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}'>补救方案</a></li>", ResourceToResourceFolder_Id, HomeWork_Id, StudentId);
                }
                else//客户端批改
                {
                    link += string.Format(@"<li><a href='../student/ohomeworkview_clientNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>批改详情</a></li><li><a href='../Evaluation/StudentAnalysisReportsNew.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>答题分析</a></li><li><a href='../Evaluation/StudentErrorAnalysis.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>错题分析</a></li><li  class='active'><a href='../Evaluation/StudentRemedyScheme.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}'>补救方案</a></li>"
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
                #region 数据分析
                string sql = string.Format(@" select  KPNameBasic=(select KPNameBasic+'，' from [dbo].[StatsStuHW_Wrong_KP] where HomeWork_Id='{0}' and Student_Id='{1}' FOR XML PATH('') )
,CountKPNameBasic=(select count(*) from [dbo].[StatsStuHW_Wrong_KP] where HomeWork_Id='{0}' and Student_Id='{1}' )
,CountTQ=(select count(*) from [dbo].[StatsStuHW_Wrong_TQ] where HomeWork_Id='{0}' and Student_Id='{1}' )
,topicNumber=(select replace(topicNumber,'.','')+'题，' from [dbo].[StatsStuHW_Wrong_TQ]  where HomeWork_Id='{0}' and Student_Id='{1}' group by topicNumber,TestQuestions_Num order by TestQuestions_Num FOR XML PATH('') )"
                    , HomeWork_Id
                    , StudentId);
                DataTable Dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (Dt.Rows.Count > 0)
                {
                    ltlKPName.Text = Dt.Rows[0]["KPNameBasic"].ToString().TrimEnd('，');
                    ltlCountKPName.Text = Dt.Rows[0]["CountKPNameBasic"].ToString();
                    ltlTQNum.Text = string.IsNullOrEmpty(Dt.Rows[0]["topicNumber"].ToString().TrimEnd('，')) ? "" : "请该同学对本次作业中的" + Dt.Rows[0]["topicNumber"].ToString().TrimEnd('，') + "进行再次练习，以便从中总结规律、方法、技巧，巩固对知识点的学习。";
                    //ltlCountTQ.Text = Dt.Rows[0]["CountTQ"].ToString().clearLastZero();
                }
                #endregion
            }

        }
    }
}