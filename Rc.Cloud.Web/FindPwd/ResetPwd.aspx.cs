using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.FindPwd
{
    public partial class ResetPwd : System.Web.UI.Page
    {
        public string key = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            key = Rc.Common.DBUtility.DESEncrypt.Decrypt(Request.QueryString["key"].Filter());
            string[] str = key.Split(',');
            DateTime dt = Convert.ToDateTime(str[1]);
            if (dt < DateTime.Now)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('邮件链接时间超时,请重新发送邮件。', { time: 0, icon: 2 });</script>");
                return;
            }
            else
            {
                btnSave.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txt_newpwd.Text.TrimEnd() != this.txt_confirmpwd.Text.TrimEnd())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('二次密码输入不一致,请重新输入。', { time: 2000, icon: 2 });</script>");
                    return;
                }
                string[] str = key.Split(',');
                DateTime dt = Convert.ToDateTime(str[1]);
                if (dt > DateTime.Now)
                {
                    if (str[0] == this.txt_username.Text.TrimEnd())
                    {
                        BLL_F_User bll = new BLL_F_User();
                        Model_F_User model = new Model_F_User();
                        model = bll.GetModelByUserName(this.txt_username.Text.TrimEnd());
                        if (model == null)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('账号不存在，请重新填写',{icon:2,time:2000});</script>");
                            return;
                        }
                        model.Password = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(this.txt_newpwd.Text.TrimEnd());
                        if (bll.Update(model))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('密码重置成功',{icon:1,time:1000},function(){window.location.href='Complete.aspx';});</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('密码重置失败',{icon:2,time:2000});</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('非法操作,请重新输入。', { time: 2000, icon: 2 });</script>");
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('邮件链接时间超时,请重新发送邮件。', { time: 2000, icon: 2 });</script>");
                    return;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}