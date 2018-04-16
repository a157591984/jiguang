using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using System.Text;
using System.Data;
using Rc.Common.Config;

namespace Rc.Cloud.Web.teacher
{
    public partial class GradeData : Rc.Cloud.Web.Common.FInitData
    {
        BLL_UserGroup bll = new BLL_UserGroup();
        protected string ugroupId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (FloginUser.UserPost == UserPost.备课组长.ToString() || FloginUser.UserPost == UserPost.普通老师.ToString())
            {
                //liCreateGrade.Visible = false;
            }
            ugroupId = Request.QueryString["ugroupId"].Filter();
            txtUserGroup_Id.Attributes.Add("readonly", "true");
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //年级类型
                strWhere = " D_Type='9' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeType, dt, "D_Name", "Common_Dict_ID", "--请选择--");
                
                //入学年份
                int years = DateTime.Now.Year;
                Rc.Cloud.Web.Common.pfunction.SetDdlStartSchoolYear(ddlStartSchoolYear, years - 5, years + 1, false);
                LoadData();
            }
        }

        private void LoadData()
        {
            DataTable dt = new BLL_UserGroup().GetPassGroupByUserIdUserGroupAttrEnum(FloginUser.UserId, UserGroup_AttrEnum.Grade.ToString()).Tables[0];
            if (string.IsNullOrEmpty(ugroupId) && dt.Rows.Count > 0)
            {
                ugroupId = dt.Rows[0]["UserGroup_Id"].ToString();
            }
            rptClass.DataSource = dt;
            rptClass.DataBind();

            Model_UserGroup model = bll.GetModel(ugroupId);
            if (model != null)
            {
                txtUserGroup_Name.Text = model.UserGroup_Name;
                txtUserGroup_Id.Text = model.UserGroup_Id;
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
                txtUserGroup_BriefIntroduction.Text = model.UserGroup_BriefIntroduction;
                txtSort.Text = model.UserGroupOrder.ToString();
                if (model.User_ID != FloginUser.UserId)//判断登录用户是否为群组创建者，不是不让编辑
                {
                    ButtonOK.Visible = false;
                }
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
        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                Model_UserGroup userGroup = bll.GetModel(ugroupId);
                if (pfunction.FilterKeyWords(this.txtUserGroup_Name.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('年级名称存在敏感词汇，请重新填写。',{icon:2,time:2000});</script>");
                    return;
                }
                else
                {
                    userGroup.UserGroup_Name = this.txtUserGroup_Name.Text.Trim();
                }
                if (pfunction.FilterKeyWords(this.txtUserGroup_BriefIntroduction.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('年级简介存在敏感词汇，请重新填写。',{icon:2,time:2000});</script>");
                    return;
                }
                else
                {
                    userGroup.UserGroup_BriefIntroduction = this.txtUserGroup_BriefIntroduction.Text.Trim();
                }
                userGroup.GradeType = this.ddlGradeType.SelectedValue;
                userGroup.Grade = Convert.ToDecimal(this.HidGrade.Value);
                userGroup.StartSchoolYear = Convert.ToDecimal(this.ddlStartSchoolYear.SelectedValue);
                int intOrder = 1;
                int.TryParse(txtSort.Text, out intOrder);
                userGroup.UserGroupOrder = intOrder;

                if (bll.Update(userGroup))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'> Handel('1','修改成功')</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'> Handel('2','修改失败')</script>");
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'> Handel('2','修改失败err')</script>");
            }
        }
    }
}