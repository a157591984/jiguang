using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.teacher
{
    public partial class CheckChapterStatsHelper : System.Web.UI.Page
    {
        protected string Identifier_Id = string.Empty;
        protected string strUserGroup_IdActivity = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Identifier_Id = Request["Identifier_Id"].Filter();
            strUserGroup_IdActivity = Request["ugid"].Filter();
        }
    }
}