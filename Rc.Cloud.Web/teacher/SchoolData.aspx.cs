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
    public partial class SchoolData : Rc.Cloud.Web.Common.FInitData
    {
        BLL_UserGroup bll = new BLL_UserGroup();
        protected string ugroupId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (FloginUser.UserPost != UserPost.校长.ToString())
            {
                //liCreateSchool.Visible = false;
            }
            ugroupId = Request.QueryString["ugroupId"].Filter();
            txtUserGroup_Id.Attributes.Add("readonly", "true");
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            DataTable dt = new BLL_UserGroup().GetList("UserGroup_AttrEnum='" + UserGroup_AttrEnum.School.ToString() + "' and User_Id='" + FloginUser.UserId + "' order by CreateTime desc").Tables[0];
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
                txtUserGroup_BriefIntroduction.Text = model.UserGroup_BriefIntroduction;
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
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('学校名称存在敏感词汇，请重新填写。',{icon:2,time:2000});</script>");
                    return;
                }
                else
                {
                    userGroup.UserGroup_Name = this.txtUserGroup_Name.Text.Trim();
                }
                if (pfunction.FilterKeyWords(this.txtUserGroup_BriefIntroduction.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('学校简介存在敏感词汇，请重新填写。',{icon:2,time:2000});</script>");
                    return;
                }
                else
                {
                    userGroup.UserGroup_BriefIntroduction = this.txtUserGroup_BriefIntroduction.Text.Trim();
                }
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