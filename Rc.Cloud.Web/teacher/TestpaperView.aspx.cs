using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.teacher
{
    public partial class TestpaperView : System.Web.UI.Page
    {
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        protected string ResourceToResourceFolder_Id = string.Empty;
        protected string tchId = string.Empty;
        protected string UserIdentity = string.Empty;
        protected string Resource_Name = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceToResourceFolder_Id = Request.QueryString["rtrfId"].Filter();
            tchId = Request.QueryString["userId"].Filter();
            UserIdentity = Request.QueryString["UserIdentity"].Filter();
            divAssign.Visible = false;
            if (UserIdentity == "T") // 老师，显示布置按钮
            {
                divAssign.Visible = true;
            }
            Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(ResourceToResourceFolder_Id);
            if (modelRTRF == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('资源不存在或已删除。',{icon:2,time:0});</script>");
                return;
            }
            else
            {
                Resource_Name = Server.UrlEncode(modelRTRF.Resource_Name);
            }

            strTestpaperViewWebSiteUrl = Rc.Cloud.Web.Common.pfunction.GetResourceHost("TestWebSiteUrl");
        }
    }
}