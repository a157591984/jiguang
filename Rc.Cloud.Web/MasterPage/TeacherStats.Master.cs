using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.MasterPage
{
    public partial class TeacherStats : System.Web.UI.MasterPage
    {
        public string ResourceToResourceFolder_Id = string.Empty;
        public string UserId = string.Empty;
        public string ClassID = string.Empty;
        public string ClassName = string.Empty;
        public string TeacherID = string.Empty;
        public string StrTemp = string.Empty;
        public bool Judge = false;
        public string LeftNAV = string.Empty;
        string HomeWork_ID = string.Empty;
        BLL_StatsClassHW_Score bllschwso = new BLL_StatsClassHW_Score();
        protected void Page_Load(object sender, EventArgs e)
        {
            Model_F_User modelFUser = (Model_F_User)Session["FLoginUser"];
            UserId = modelFUser.UserId;
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            HomeWork_ID = Request.QueryString["HomeWork_ID"].Filter();

            ClassID = Request.QueryString["ClassID"].Filter();
            TeacherID = Request.QueryString["TeacherID"].Filter();
            ClassName = Request.QueryString["ClassName"].Filter();
            if (!string.IsNullOrEmpty(HomeWork_ID))
            {

                if (!string.IsNullOrEmpty(ClassID))//判断查看的统计是否为此老师的
                {
                    StrTemp = "&ClassID=" + ClassID + "&ClassName=" + ClassName + "&TeacherID=" + TeacherID;
                    DataTable dt = new CommonHandel().GetTeacherAllClassByRTRFolder_Id(TeacherID, ResourceToResourceFolder_Id).Tables[0];
                    DataRow[] dr = dt.Select("AnalysisDataCount=1");
                    if (dr.Length < 2)
                    {
                        Judge = true;
                    }
                }
                else
                {
                    DataTable dt = new CommonHandel().GetTeacherAllClassByRTRFolder_Id(UserId, ResourceToResourceFolder_Id).Tables[0];
                    DataRow[] dr = dt.Select("AnalysisDataCount=1");
                    if (dr.Length < 2)
                    {
                        Judge = true;
                    }
                }

                LoadDate();
                GetLeftNAV();
            }

        }
        protected void LoadDate()
        {
            DataTable dt = bllschwso.GetList("HomeWork_ID='" + HomeWork_ID + "' ").Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.ltlHomeWork_Name.Text = dt.Rows[0]["Resource_Name"].ToString().ReplaceForFilter();
                this.ltlGrade.Text = dt.Rows[0]["GradeName"].ToString();
                this.ltlSubjectName.Text = dt.Rows[0]["SubjectName"].ToString();
                this.ltlSumSore.Text = Convert.ToInt32(dt.Rows[0]["HW_Score"]).ToString();
                this.ltlTeacherName.Text = dt.Rows[0]["TeacherName"].ToString();
                ResourceToResourceFolder_Id = dt.Rows[0]["ResourceToResourceFolder_Id"].ToString();
            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'> Handel('2','数据已更新,请重新打开页面.')</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.msg('暂无数据.',{icon:2,time:0, shade: [0.1, '#fff'] });</script>");
                return;
            }
        }
        public void GetLeftNAV()
        {
            try
            {
                if (Judge == false)
                {
                    LeftNAV = string.Format("<li><a href='AssessmentProfile.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}{2}'>测评概况</a></li>", ResourceToResourceFolder_Id, HomeWork_ID, StrTemp);
                    LeftNAV += string.Format("<li><a href='KnowledgeMasterList.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}{2}'>知识点掌握情况</a></li>", ResourceToResourceFolder_Id, HomeWork_ID, StrTemp);
                    LeftNAV += string.Format("<li><a href='ComparisonBetweenClasses.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}{2}'>班级间成绩对比</a></li>", ResourceToResourceFolder_Id, HomeWork_ID, StrTemp);
                    LeftNAV += string.Format("<li><a href='StudentAchievementTrack.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}{2}'>学生成绩分析</a></li>", ResourceToResourceFolder_Id, HomeWork_ID, StrTemp);
                }
                else
                {
                    LeftNAV = string.Format("<li><a href='AssessmentProfile.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}{2}'>测评概况</a></li>", ResourceToResourceFolder_Id, HomeWork_ID, StrTemp);
                    LeftNAV += string.Format("<li><a href='KnowledgeMasterList.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}{2}'>知识点掌握情况</a></li>", ResourceToResourceFolder_Id, HomeWork_ID, StrTemp);
                    //LeftNAV += string.Format("<li><a href='ComparisonBetweenClasses.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}{2}'>班级间成绩对比</a></li>", ResourceToResourceFolder_Id, HomeWork_ID, StrTemp);
                    LeftNAV += string.Format("<li><a href='StudentAchievementTrack.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}{2}'>学生成绩分析</a></li>", ResourceToResourceFolder_Id, HomeWork_ID, StrTemp);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}