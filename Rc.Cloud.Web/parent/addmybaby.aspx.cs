using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.parent
{
    public partial class addmybaby : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                BLL_F_User bll = new BLL_F_User();
                Model_F_User model = new Model_F_User();
                string UserName = txtUserName.Text.Trim();
                string PassWord = txtPassWord.Text.Trim();
                model = bll.GetModel(UserName, Rc.Common.StrUtility.DESEncryptLogin.EncryptString(PassWord));
                if (model == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('账号或密码不正确，请重新填写',{icon:4,time:2000,offset:'10px'})})</script>");
                    return;
                }
                else
                {
                    BLL_StudentToParent Sbll = new BLL_StudentToParent();
                    Model_StudentToParent Smodel = new Model_StudentToParent();
                    DataTable dt = Sbll.GetList("Student_ID='" + model.UserId + "' and parent_id='" + FloginUser.UserId + "'").Tables[0];

                    if (model.UserIdentity == "S" && dt.Rows.Count == 0)
                    {
                        Smodel.StudentToParent_ID = Guid.NewGuid().ToString();
                        Smodel.Parent_ID = FloginUser.UserId;
                        Smodel.Student_ID = model.UserId;
                        Smodel.CreateTime = DateTime.Now;
                        if (Sbll.Add(Smodel))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('添加成功',{icon:1,time:2000,offset:'10px'},function(){parent.window.location.reload()})})</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('账号不正确或此账号已绑定',{icon:4,time:2000,offset:'10px'})})</script>");
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('" + ex.Message + "',{icon:4,time:2000,offset:'10px'})})</script>");
            }
        }
    }
}