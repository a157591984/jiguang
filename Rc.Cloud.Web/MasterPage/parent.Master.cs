using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Homework.MasterPage
{
    public partial class parent : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request["psdTips"].Filter() == "1")
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.confirm('密码过于简单，请修改密码！', {}, function () { window.location.href = '/parent/safeSetting.aspx'; });});</script>");
            //}

            if (pfunction.GetWebMdlIsShow("FAQ")) ahelp.Visible = true;
            Model_F_User modelFUser = (Model_F_User)Session["FLoginUser"];
            ltlUserPost.Text = string.IsNullOrEmpty(modelFUser.TrueName) ? modelFUser.UserName : modelFUser.TrueName;
        }
        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(Rc.Common.StrUtility.clsUtility.getHostPath() + "/index.aspx");
        }
    }
}