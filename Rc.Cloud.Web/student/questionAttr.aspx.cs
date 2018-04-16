using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.student
{
    public partial class questionAttr : System.Web.UI.Page
    {
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        protected string strResourceToResourceFolder_Id = string.Empty;
        protected string strTestQuestions_Id = string.Empty;
        protected string strAttrType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");

            strResourceToResourceFolder_Id = Request.QueryString["resourceid"].Filter();
            strTestQuestions_Id = Request.QueryString["questionid"].Filter();
            strAttrType = Request.QueryString["attrType"].Filter();

        }
    }
}