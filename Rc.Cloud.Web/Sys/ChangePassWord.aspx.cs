using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;

namespace Rc.Cloud.Web.Sys
{
    public partial class ChangPassWord : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90104000";
        }
        protected void btnChangePassword_Click(object sender, EventArgs e)
        { 
            string old_ = string.Empty;
            string new_ = string.Empty;
            string login_name = string.Empty;
            old_ = txt_oldpwd.Value;
            new_ = txt_newpwd.Value;
            login_name = loginUser.SysUser_LoginName;
            if (new BLL_Login().UpdateSysUserChangePassword(old_, new_, login_name))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>alert('密码修改成功');</script>");

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>alert('对不起，您输入的原始密码有误，请重新修改。');</script>");
            }
        }
    }
}