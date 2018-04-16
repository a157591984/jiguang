using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Data;
using System.Web.Services;
using Rc.Common.Config;
using Rc.Cloud.Web.Common;

namespace Homework
{
    public partial class teacherRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "--请选择--");

                strWhere = " D_Type='15' order by d_order ";
                Rc.Cloud.Web.Common.pfunction.SetDdlEmpty(ddlUserPost, new BLL_Common_Dict().GetList(strWhere).Tables[0], "D_Name", "Common_Dict_ID", "");
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            BLL_F_User bll_f_user = new BLL_F_User();
            string strUserId = Guid.NewGuid().ToString();
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
            f_user.UserId = strUserId;
            f_user.UserName = txt_username.Text.Trim();
            f_user.Password = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(txt_password.Text.Trim());
            f_user.UserIdentity = "T";
            f_user.Subject = ddlSubject.SelectedValue;
            f_user.UserPost = ddlUserPost.SelectedValue.Trim();
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
                string redirectUrl = "/teacher/classList.aspx";

                switch (f_user.UserPost)
                {
                    case UserPost.校长:
                    case UserPost.副校长:
                    case UserPost.教务主任:
                        redirectUrl = "/teacher/SchoolList.aspx";
                        break;
                    case UserPost.年级组长:
                    case UserPost.备课组长:
                        redirectUrl = "/teacher/GradeList.aspx";
                        break;
                    case UserPost.普通老师:
                        redirectUrl = "/teacher/classList.aspx";
                        break;
                }
                Response.Redirect(redirectUrl);
                //if (f_user.UserIdentity == "T")
                //{
                //Response.Redirect("/teacher/classList.aspx");
                //}
                //else
                //{
                //    Response.Redirect("/student/basicSetting.aspx");
                //}
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.msg('注册失败，请重新注册。', { time: 2000, icon: 2 })</script>");
                return;
            }
        }

        [WebMethod]
        public static string CheckUserNameIsExist(string userName)
        {
            string temp = "1";
            try
            {
                if (new BLL_F_User().GetRecordCount("UserName='" + userName + "'") == 0)
                {
                    temp = "0";
                }
            }
            catch (Exception)
            {

            }
            return temp;
        }

    }
}