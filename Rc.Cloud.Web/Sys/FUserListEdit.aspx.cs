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
    public partial class FUserListEdit : System.Web.UI.Page
    {
        protected string UserId = string.Empty;
        BLL_F_User bll_f_user = new BLL_F_User();
        Model_F_User model_f_user = new Model_F_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = Request.QueryString["UserId"].ToString().Filter();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "--请选择--");
                //职务
                strWhere = " D_Type='15' order by d_order ";
                Rc.Cloud.Web.Common.pfunction.SetDdlEmpty(ddlUserPost, new BLL_Common_Dict().GetList(strWhere).Tables[0], "D_Name", "Common_Dict_ID", "");

                if (!string.IsNullOrEmpty(UserId))
                {
                    loadData();
                }
            }
        }

        /// <summary>
        /// 修改时的默认值
        /// </summary>
        protected void loadData()
        {
            model_f_user = bll_f_user.GetModel(UserId);
            if (model_f_user == null)
            {
                return;
            }
            else
            {
                ddlUserPost.SelectedValue = model_f_user.UserPost;
                txtUserName.Text = model_f_user.UserName;
                txtTrueName.Text = model_f_user.TrueName;
                txtAge.Text = (model_f_user.Birthday != null ? DateTime.Parse(model_f_user.Birthday.ToString()).ToString("yyyy-MM-dd") : "");
                ddlSex.SelectedValue = model_f_user.Sex;
                txtEmail.Text = model_f_user.Email;
                txtMobile.Text = model_f_user.Mobile;
                ddlSubject.SelectedValue = model_f_user.Subject;
                txtExpirationDate.Text = (model_f_user.ExpirationDate != null ? DateTime.Parse(model_f_user.ExpirationDate.ToString()).ToString("yyyy-MM-dd") : "");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    #region 添加
                    string strUserId = Guid.NewGuid().ToString();
                    string Password = txtPassword.Text.Trim();
                    string VerPassword = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(txtVerPassword.Text.Trim());
                    model_f_user.UserId = strUserId;
                    model_f_user.UserPost = ddlUserPost.SelectedValue;
                    model_f_user.UserName = txtUserName.Text.Trim();
                    model_f_user.Password = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(Password);
                    model_f_user.TrueName = txtTrueName.Text.Trim();
                    if (txtAge.Text != "")
                    {
                        model_f_user.Birthday = Convert.ToDateTime(txtAge.Text.Trim());
                    }
                    else
                    {
                        model_f_user.Birthday = null;
                    }
                    model_f_user.Sex = ddlSex.SelectedValue;
                    model_f_user.Email = txtEmail.Text.Trim();
                    model_f_user.Mobile = txtMobile.Text.Trim();
                    model_f_user.Subject = ddlSubject.SelectedValue;
                    model_f_user.CreateTime = DateTime.Now;
                    if (txtExpirationDate.Text != "")
                    {
                        model_f_user.ExpirationDate = Convert.ToDateTime(txtExpirationDate.Text.Trim());
                    }
                    else
                    {
                        model_f_user.ExpirationDate = null;
                    }
                    //验证是否选择学科
                    if (ddlSubject.SelectedValue == "-1")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('请选择学科', {icon: 4})})})</script>");
                        return;
                    }
                    //验证密码是否为空
                    if (string.IsNullOrEmpty(Password))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('密码不能为空', {icon: 4})})})</script>");
                        return;
                    }
                    //验证密码和确认密码是否一致
                    if (model_f_user.Password != VerPassword)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('两次输入的密码不一致,请重新输入。', {icon: 4})})})</script>");
                        return;
                    }
                    //验证用户名是否已存在
                    if (bll_f_user.Exists(model_f_user, "1"))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('用户名/邮箱/手机号已被注册,请重新输入。', {icon: 4})})})</script>");
                        return;
                    }
                    bool boolAddRes = bll_f_user.Add(model_f_user);
                    if (boolAddRes)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('新增成功!',{ time: 1000,icon:1},function(){parent.loadData();parent.layer.close(index)});})})</script>");
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region 修改
                    model_f_user = bll_f_user.GetModel(UserId);
                    string Password = txtPassword.Text.Trim();
                    string VerPassword = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(txtVerPassword.Text.Trim());
                    model_f_user.UserId = UserId;
                    model_f_user.UserPost = ddlUserPost.SelectedValue;
                    model_f_user.UserName = txtUserName.Text.Trim();
                    if (!string.IsNullOrEmpty(Password))
                    {
                        model_f_user.Password = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(Password);
                    }
                    model_f_user.TrueName = txtTrueName.Text.Trim();
                    if (txtAge.Text != "")
                    {
                        model_f_user.Birthday = Convert.ToDateTime(txtAge.Text.Trim());
                    }
                    else
                    {
                        model_f_user.Birthday = null;
                    }
                    model_f_user.Sex = ddlSex.SelectedValue;
                    model_f_user.Email = txtEmail.Text.Trim();
                    model_f_user.Mobile = txtMobile.Text.Trim();
                    model_f_user.Subject = ddlSubject.SelectedValue;
                    if (txtExpirationDate.Text != "")
                    {
                        model_f_user.ExpirationDate = Convert.ToDateTime(txtExpirationDate.Text.Trim());
                    }
                    else
                    {
                        model_f_user.ExpirationDate = null;
                    }
                    //验证是否选择学科
                    if (ddlSubject.SelectedValue == "-1")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('请选择学科', {icon: 4})})})</script>");
                        return;
                    }
                    //验证密码和确认密码是否一致
                    if (model_f_user.Password != VerPassword && !string.IsNullOrEmpty(Password))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script>$(function(){layer.ready(function(){layer.msg('两次输入的密码不一致,请重新输入。', {icon: 4})})})</script>");
                    }
                    //验证用户名是否已存在
                    if (bll_f_user.Exists(model_f_user, "2"))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script>$(function(){layer.ready(function(){layer.msg('用户名/邮箱/手机号已被注册,请重新输入。', {icon: 4})})})</script>");
                        return;
                    }
                    bool boolUpdateRes = bll_f_user.Update(model_f_user);
                    if (boolUpdateRes)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('修改成功!',{ time: 1000,icon:1},function(){parent.loadData();parent.layer.close(index);});})})</script>");
                    }
                    #endregion
                }

            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('操作失败!',{icon:2},function(){parent.loadData();parent.layer.close(index);});})})</script>");
            }
        }


    }
}