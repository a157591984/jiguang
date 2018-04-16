using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.parent
{
    public partial class safeSetting : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                BLL_F_User bll = new BLL_F_User();
                Model_F_User model = new Model_F_User();
                string oldPass = txtOldPass.Text.Trim();
                string newPass = txtNewPass.Text.Trim();
                model = bll.GetModel(FloginUser.UserName, Rc.Common.StrUtility.DESEncryptLogin.EncryptString(oldPass));
                if (model == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('原始密码不正确',{icon:2,time:2000});</script>");
                    return;
                }
                model.Password = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(newPass);
                if (bll.Update(model))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('密码修改成功',{icon:1,time:1000},function(){window.location.href='../index.aspx';});</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('密码修改失败',{icon:2,time:2000});</script>");
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('保存失败',{icon:2,time:2000});</script>");
            }
        }

    }
}