using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Common.Config;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.teacher
{
    public partial class FileView : System.Web.UI.Page
    {
        string ResourceToResourceFolder_Id = string.Empty;
        protected string strTeachingPlanViewWebSiteUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strTeachingPlanViewWebSiteUrl = pfunction.GetResourceHost("TeachingPlanViewWebSiteUrl") + "Upload/Resource/";
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            string Resource_Class = Request.QueryString["Resource_Class"].Filter();

            if (!string.IsNullOrEmpty(ResourceToResourceFolder_Id))
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            DataTable dtSmall = new Rc.BLL.Resources.BLL_ResourceToResourceFolder_img().GetListByPage("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'", "ResourceToResourceFolderImg_Order", 1, 300).Tables[0];
            DataTable dtBig = new Rc.BLL.Resources.BLL_ResourceToResourceFolder_img().GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id
                + "' order by ResourceToResourceFolderImg_Order").Tables[0];
            rptImgSmall.DataSource = dtSmall;
            rptImgSmall.DataBind();
            rptImgBig.DataSource = dtBig;
            rptImgBig.DataBind();
        }

    }
}