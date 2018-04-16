using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rc.Cloud.BLL;
using Rc.Cloud.Model;
using System.Configuration;


namespace Rc.Cloud.Web
{
    public partial class index : System.Web.UI.Page
    {
        protected string strSysName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            strSysName = ConfigurationManager.AppSettings["SysName"].ToString();
        }

        protected void ibtnLogin_Click(object sender, EventArgs e)
        {
            if (Session["AdminValidateCode"] == null)
            {
                Response.Redirect("Index.aspx");
                Response.End();
            }
            string CheckCode = Session["AdminValidateCode"].ToString();
            Session.Remove("AdminValidateCode");
            if (TxtVerify.Value.ToUpper() == CheckCode)
            {
                string loginName = string.Empty;
                string passWord = string.Empty;
                loginName = txtLoginName.Value.Trim();
                passWord = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(psd.Value);

                Model_VSysUserRole loginModel = new Model_VSysUserRole();
                object obj = new BLL_VSysUserRole().GetVDoctorInfoModelByLogin(loginName, passWord);
                if (obj != null)
                {
                    loginModel = obj as Model_VSysUserRole;
                    DataTable dt = new BLL_SysModule().GetOwenModuleListByCacheBySysCode(loginModel.SysUser_ID, Rc.Common.StrUtility.clsUtility.ReDoStr(loginModel.SysRole_IDs, ','));
                    DataRow[] drs = dt.Select("isLast='1' and url<>'#'", "DefaultOrder desc");
                    if (drs.Count() > 0)
                    {
                        Session["LoginUser"] = loginModel;
                        if (Request["iurl"] != null)
                        {
                            Response.Redirect(Request["iurl"].ToString());
                        }
                        else
                        {
                            Response.Redirect("/" + drs[0]["url"].ToString());
                        }

                    }
                    else
                    {
                        Rc.Common.StrUtility.clsUtility.ErrorDispose(this.Page, 6, false);
                    }

                }
                else
                {
                    Session.Clear();
                    ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>$(function(){layer.ready(function(){layer.msg('用户名或密码错误',{icon:2})})})</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>$(function(){layer.ready(function(){layer.msg('验证码输入不正确',{icon:2})})})</script>");
            }
        }


        



    }
}