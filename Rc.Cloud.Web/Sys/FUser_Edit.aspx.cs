using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.BLL.Resources;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.Sys
{
    public partial class FUser_Edit : Rc.Cloud.Web.Common.InitPage
    {
        public string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userid = Request.QueryString["UserId"].Filter();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                strWhere = " D_Type='15' order by d_order ";
                Rc.Cloud.Web.Common.pfunction.SetDdlEmpty(ddlUserPost, new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0], "D_Name", "Common_Dict_ID", "--请选择--");

                BLL_F_User bll = new BLL_F_User();
                Model_F_User model = new Model_F_User();
                model = bll.GetModel(userid);
                txtTureName.Text = model.TrueName;
                txtUserName.Text = model.UserName;
                ddlUserPost.SelectedValue = model.UserPost;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BLL_F_User bll = new BLL_F_User();
                Model_F_User model = new Model_F_User();
                model = bll.GetModel(userid);
                if (model != null)
                {
                    string UserName = txtUserName.Text.Trim();
                    if (string.IsNullOrEmpty(UserName))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function () {layer.msg('请输入登录名',{icon:2,time:2000});})</script>");
                        return;
                    }
                    if (bll.GetRecordCount("UserId<>'" + userid + "' and UserName='" + UserName + "'") > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function () {layer.msg('登录名已存在，请重新输入',{icon:2,time:2000});})</script>");
                        return;
                    }
                    model.UserName = UserName;
                    model.TrueName = txtTureName.Text.Trim();
                    model.UserPost = ddlUserPost.SelectedValue;
                    if (bll.Update(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function () {layer.msg('修改成功',{icon:1,time:1000},function(){parent.loadData();parent.layer.close(index);});})</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function () {layer.msg('修改失败',{icon:2,time:2000});})</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改失败',{icon:2,time:2000});</script>");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改失败',{icon:2,time:2000});</script>");
            }
        }
    }
}