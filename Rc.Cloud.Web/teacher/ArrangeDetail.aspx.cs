using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.teacher
{
    public partial class ArrangeDetail : Rc.Cloud.Web.Common.FInitData
    {
        string ResourceToResourceFolderId = string.Empty;
        string classId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceToResourceFolderId = Request.QueryString["ResourceToResourceFolderId"].Filter();
            classId = Request.QueryString["classId"].Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(ResourceToResourceFolderId))
                {
                    DataTable dtHW = new BLL_HomeWork().GetListForTeacherView("UserGroup_Id='" + classId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolderId + "'", "").Tables[0];
                    rptHW.DataSource = dtHW;
                    rptHW.DataBind();
                }
            }
        }
    }
}