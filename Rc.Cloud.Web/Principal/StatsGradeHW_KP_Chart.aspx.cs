using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.Principal
{
    public partial class StatsGradeHW_KP_Chart : Rc.Cloud.Web.Common.FInitData
    {
        protected string kName = string.Empty;
        protected string cName = string.Empty;
        protected string data = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            kName = Request.QueryString["kName"].ToString().Filter();
            cName = Request.QueryString["cName"].ToString().Filter();
            data = Request.QueryString["data"].ToString().Filter();
        }
    }
}