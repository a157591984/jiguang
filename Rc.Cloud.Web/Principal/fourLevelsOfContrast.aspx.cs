using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.Principal
{
    public partial class fourLevelsOfContrast : Rc.Cloud.Web.Common.FInitData
    {
        public string Grade = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        BLL_StatsGradeHW_Score bllStatsGradeHW_Score = new BLL_StatsGradeHW_Score();
        protected void Page_Load(object sender, EventArgs e)
        {
            Grade = Request.QueryString["Grade"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            if (!string.IsNullOrEmpty(ResourceToResourceFolder_Id))
            {
                LoadDate();
            }
        }
        protected void LoadDate()
        {
            DataTable dt = bllStatsGradeHW_Score.GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' ").Tables[0];
            this.ltlHomeWork_Name.Text = dt.Rows[0]["Resource_Name"].ToString().ReplaceForFilter();
            this.ltlGrade.Text = dt.Rows[0]["GradeName"].ToString();
            this.ltlSubjectName.Text = dt.Rows[0]["SubjectName"].ToString();
            this.ltlSumSore.Text = Convert.ToInt32(dt.Rows[0]["HW_Score"]).ToString();
        }
    }
}