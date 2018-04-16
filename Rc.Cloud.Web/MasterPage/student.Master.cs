using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Common.StrUtility;

namespace Homework.MasterPage
{
    public partial class student : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request["psdTips"].Filter() == "1")
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.confirm('密码过于简单，请修改密码！', {}, function () { window.location.href = '/student/safeSetting.aspx'; });});</script>");
            //}

            if (pfunction.GetWebMdlIsShow("FAQ")) ahelp.Visible = true;
            Model_F_User modelFUser = (Model_F_User)Session["FLoginUser"];
            int MsgCount = new BLL_Msg().GetRecordCount("MsgAccepter='" + modelFUser.UserId + "' and MsgStatus='Unread' ");
            ltlUserPost.Text = string.IsNullOrEmpty(modelFUser.TrueName) ? modelFUser.UserName : modelFUser.TrueName;
            ltlMsgCount.Text = string.Format("{0}"
                , (MsgCount > 0) ? "<span class='badge'>" + MsgCount + "</span>" : "");
        }

    }
}