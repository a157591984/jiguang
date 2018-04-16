using Rc.Cloud.BLL;
using Rc.Cloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.Sys
{
    public partial class changepwd : Rc.Cloud.Web.Common.InitPage
    {
        public string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userid = Request.QueryString["userid"].Filter();
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
                    //string oldPass = txt_oldpassword.Text.Trim();
                    string newPass = txt_newpassword.Text.Trim();
                    
                    //model = bll.GetModel(model.UserName, Rc.Common.StrUtility.DESEncryptLogin.EncryptString(oldPass));
                    //if (model == null)
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('原始密码不正确',{icon:2,time:2000});</script>");
                    //    return;
                    //}
                    model.Password = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(newPass);
                    if (bll.Update(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('密码修改成功',{icon:1,time:1000},function(){parent.layer.close(index);});</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('密码修改失败',{icon:2,time:2000});</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('密码修改失败',{icon:2,time:2000});</script>");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('密码修改失败',{icon:2,time:2000});</script>");
            }
        }
    }
}