using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.MasterPage
{
    public partial class PrincipalAnalysis : System.Web.UI.MasterPage
    {
        public string GradeId = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            if (!string.IsNullOrEmpty(ResourceToResourceFolder_Id))
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            BLL_StatsGradeHW_Score bllStatsGradeHW_Score = new BLL_StatsGradeHW_Score();
            DataTable dt = bllStatsGradeHW_Score.GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and GradeId='" + GradeId + "' ").Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.ltlHomeWork_Name.Text = dt.Rows[0]["Resource_Name"].ToString().ReplaceForFilter();
                this.ltlGrade.Text = dt.Rows[0]["GradeName"].ToString();
                this.ltlSubjectName.Text = dt.Rows[0]["SubjectName"].ToString();
                this.ltlSumSore.Text = Convert.ToInt32(dt.Rows[0]["HW_Score"]).ToString();
            }
        }
    }
}