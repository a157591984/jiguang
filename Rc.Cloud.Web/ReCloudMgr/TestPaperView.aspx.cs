using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class TestPaperView : System.Web.UI.Page
    {
        protected string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            if (Request["isBack"].Filter() == "1")// /ReCloudMgr/TestPaperView.aspx
            {
                divBack.Visible = true;
            }
        }
    }
}