using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Common.StrUtility;
using Rc.Model.Resources;

namespace Homework.student
{
    public partial class classInfo : Rc.Cloud.Web.Common.FInitData
    {
        protected string ugroupId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ugroupId = Request.QueryString["ugroupId"].Filter();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //年级类型
                strWhere = " D_Type='9' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeType, dt, "D_Name", "Common_Dict_ID", "-无-");
                //入学年份
                int years = DateTime.Now.Year;
                Rc.Cloud.Web.Common.pfunction.SetDdlYear(ddlStartSchoolYear, years - 5, years + 1, true);
                LoadData();
            }
        }

        private void LoadData()
        {
            DataTable dt = new BLL_UserGroup().GetList(string.Format("UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and User_Id='{0}') order by CreateTime desc ", FloginUser.UserId)).Tables[0];
            if (string.IsNullOrEmpty(ugroupId) && dt.Rows.Count > 0)
            {
                ugroupId = dt.Rows[0]["UserGroup_Id"].ToString();
            }
            rptClass.DataSource = dt;
            rptClass.DataBind();

            Model_UserGroup model = new BLL_UserGroup().GetModel(ugroupId);
            if (model != null)
            {
                txtUserGroup_Name.Text = model.UserGroup_Name;
                txtUserGroup_Id.Text = model.UserGroup_Id;
                this.txtClass.Text = model.Class.ToString();
                this.ddlGradeType.SelectedValue = model.GradeType;
                this.ddlStartSchoolYear.SelectedValue = model.StartSchoolYear.ToString();
                if (string.IsNullOrEmpty(model.Grade.ToString()))
                {
                    this.HidGrade.Value = "-1";

                }
                else
                {
                    this.HidGrade.Value = model.Grade.ToString();
                }
                txtUserName.Text = pfunction.GetUserTrueNameByUserId(model.User_ID);
                txtUserGroup_BriefIntroduction.Text = model.UserGroup_BriefIntroduction;
            }
        }
        protected string GetStyle(string strGradeTerm_ID)
        {
            string strTemp = string.Empty;
            if (ugroupId.Trim() == strGradeTerm_ID.Trim())
            {
                strTemp = " active";
            }
            return strTemp;
        }
    }
}