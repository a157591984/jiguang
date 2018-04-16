using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.Principal
{
    public partial class CheckGradeStatsHelper : Rc.Cloud.Web.Common.FInitData
    {
        protected string link = string.Empty;
        protected string ResourceToResourceFolder_Id = string.Empty;
        protected string GradeId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            GradeId = Request.QueryString["GradeId"].Filter();
            link = string.Format("StatsGradeHW_TQ.aspx?ResourceToResourceFolder_Id={0}&GradeId={1}"
                , ResourceToResourceFolder_Id
                , GradeId);
        }
    }
}