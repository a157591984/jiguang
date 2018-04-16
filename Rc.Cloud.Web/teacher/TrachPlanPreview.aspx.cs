using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.teacher
{
    public partial class TrachPlanPreview : System.Web.UI.Page
    {
        protected string strViewUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            strViewUrl = "../Upload/Resource/";
            if (!string.IsNullOrEmpty(Request["iview"]))
            {
                strViewUrl += string.Format("{0}.htm", Request["iview"].ToString());
            }
            else
            {
                strViewUrl = "../NoResource.html";
            }
        }
    }
}