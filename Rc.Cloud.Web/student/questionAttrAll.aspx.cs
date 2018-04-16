using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using System.Text;
using System.Data;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.student
{
    public partial class questionAttrAll : System.Web.UI.Page
    {
        protected string strStuId = string.Empty;
        protected string strResourceToResourceFolder_Id = string.Empty;
        protected string strTestQuestions_Id = string.Empty;
        protected string strAttrType = string.Empty;
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strStuId = Request.QueryString["stuId"].Filter();

            strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");
            if (!string.IsNullOrEmpty(strStuId) && strTestpaperViewWebSiteUrl == Rc.Common.ConfigHelper.GetConfigString("TestWebSiteUrl"))
            {
                strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl", strStuId);
            }

            strResourceToResourceFolder_Id = Request.QueryString["resourceid"].Filter();
            strTestQuestions_Id = Request.QueryString["questionid"].Filter();
            strAttrType = Request.QueryString["attrType"].Filter();

        }
    }
}