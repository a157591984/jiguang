
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.FindPwd
{
    public partial class VerIdentity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string CheckCode = Session["AdminValidateCode"].ToString();
                Session.Remove("AdminValidateCode");
                if (TxtVerify.Value.ToUpper() != CheckCode)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('验证码输入不正确,请重新输入。', { time: 2000, icon: 2 });</script>");
                    return;
                }

                BLL_F_User bll = new BLL_F_User();
                Model_F_User user = new Model_F_User();
                user = bll.GetModelByUserName(this.txt_username.Text.TrimEnd());
                if (user != null)
                {
                    if (string.IsNullOrEmpty(user.Email))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('您没有绑定邮箱账号无法重置密码，请与网站管理员联系。', { time: 0, icon: 2 });</script>");
                        return;
                    }
                    else
                    {
                        if (user.Email != this.txt_emial.Text.TrimEnd())
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('请输入此账号绑定的邮箱账号（" + GetEmailSub(user.Email) + "）进行密码找回。', { time: 5000, icon: 2 });</script>");
                            return;
                        }
                        string url = string.Empty;
                        string key = string.Empty;
                        key = Rc.Common.DBUtility.DESEncrypt.Encrypt(this.txt_username.Text.TrimEnd() + "," + DateTime.Now.AddHours(1).ToString());
                        url = pfunction.getHostPath() + "/FindPwd/ResetPwd.aspx?key=" + key;
                        if (pfunction.SendMail(this.txt_emial.Text.TrimEnd(), this.txt_username.Text.TrimEnd(), url))
                        {
                            string temp = string.Empty;
                            temp = "<b> " + GetThrowEmail(this.txt_emial.Text.TrimEnd()) == "" ? "邮箱" : GetThrowEmail(this.txt_emial.Text.TrimEnd()) + " </b>";
                            ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('邮件发送成功，请登录<b> " + temp + " </b>，进行重置密码。', {time:0, icon: 1 });</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('邮件发送失败，请重试。', { time: 2000, icon: 2 });</script>");
                            return;
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('账号不存在,请重新输入。', { time: 2000, icon: 2 });</script>");
                    return;
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('邮件发送异常，请重试。', { time: 2000, icon: 2 });</script>");
                return;
            }
        }

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [WebMethod]
        public static string CheckUserNameIsExist(string userName)
        {
            try
            {
                if (new BLL_F_User().GetRecordCount(" UserName='" + userName + "'") > 0)
                {
                    return "1";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GetEmailSub(string email)
        {
            try
            {
                string temp = email;
                if (email.Substring(2, email.IndexOf('@')).Length > 2)
                {
                    return email.Substring(0, 2) + "****" + temp.Substring(temp.IndexOf('@'));
                }
                else
                {
                    return email.Substring(0, 1) + "****" + temp.Substring(temp.IndexOf('@'));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GetThrowEmail(string email)
        {
            try
            {
                string temp = string.Empty;
                string tempEmial = string.Empty;
                tempEmial = email.Substring(email.IndexOf('@') + 1);
                tempEmial = tempEmial.ToLower();
                switch (tempEmial)
                {
                    case "qq.com":
                        temp = "<a href=\"http://mail.qq.com\"  target=\"_blank\">QQ邮箱</a>";
                        break;
                    case "163.com":
                        temp = "<a href=\"http://email.163.com\"  target=\"_blank\">163邮箱</a>";
                        break;
                    case "126.com":
                        temp = "<a href=\"http://www.126.com\"  target=\"_blank\">126邮箱</a>";
                        break;
                    case "yahoo.com.cn":
                        temp = "<a href=\"http://cn.overview.mail.yahoo.com\"  target=\"_blank\">yahoo邮箱</a>";
                        break;
                    case "yahoo.cn":
                       temp = "<a href=\"http://cn.overview.mail.yahoo.com\"  target=\"_blank\">yahoo邮箱</a>";
                        break;
                    case "yeah.net":
                        temp = "<a href=\"http://www.yeah.net\"  target=\"_blank\">yeah邮箱</a>";
                        break;

                }
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}