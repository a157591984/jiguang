using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.RE_Register
{
    public partial class parentRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            BLL_F_User bll_f_user = new BLL_F_User();
            if (txt_password.Text.Trim() != txt_confirmPassword.Text.Trim())
            {
                ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('两次输入的密码不一致,请重新输入。', { time: 2000, icon: 2 })</script>");
                return;
            }
            string CheckCode = Session["AdminValidateCode"].ToString();
            Session.Remove("AdminValidateCode");
            if (TxtVerify.Value.ToUpper() != CheckCode)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('验证码输入不正确,请重新输入。', { time: 2000, icon: 2 })</script>");
                return;
            }
            if (pfunction.FilterKeyWords(this.txt_username.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('用户名存在敏感词汇，请重新填写。',{icon:2,time:2000});</script>");
                return;
            }
            Model_F_User f_user = new Model_F_User();
            f_user.UserId = Guid.NewGuid().ToString();
            f_user.UserName = txt_username.Text.Trim();
            f_user.Password = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(txt_password.Text.Trim());
            f_user.UserIdentity = "P";
            f_user.CreateTime = DateTime.Now;
            if (bll_f_user.Exists(f_user, "1"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('用户名/邮箱/手机号已被注册,请重新输入。', { time: 2000, icon: 2 })</script>");
                return;
            }
            bool result = bll_f_user.Add(f_user);
            if (result)
            {
                Session["FLoginUser"] = f_user;
                if (f_user.UserIdentity == "P")
                {
                    Response.Redirect("/parent/student.aspx");
                }
                else
                {
                    Response.Redirect("/student/basicSetting.aspx");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('注册失败，请重新注册。', { time: 2000, icon: 2 })</script>");
                return;
            }
        }

    }
}