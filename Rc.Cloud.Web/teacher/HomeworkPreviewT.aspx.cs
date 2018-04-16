using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using System.Data;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.teacher
{
    public partial class HomeworkPreviewT : Rc.Cloud.Web.Common.FInitData
    {
        protected string ResourceToResourceFolder_Id = string.Empty;
        public string userId = string.Empty;
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = FloginUser.UserId;
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");

            string strResourceForder_IdDefault = Request.QueryString["strResourceForder_IdDefault"].Filter();
            if (Request["isBack"].Filter() == "1")//cHomework_API_Select.aspx
            {
                divBack.Visible = true;
                aBack.HRef = "cHomework_API_Select.aspx?strResourceForder_IdDefault=" + strResourceForder_IdDefault;
            }
            else if (Request["isBack"].Filter() == "2")//mHomework_API_Select.aspx
            {
                divBack.Visible = true;
                aBack.HRef = "mHomework_API_Select.aspx?strResourceForder_IdDefault=" + strResourceForder_IdDefault;
            }
            else if (Request["isBack"].Filter() == "3")//pHomework_API_Select.aspx
            {
                divBack.Visible = true;
                aBack.HRef = "pHomework_API_Select.aspx?strResourceForder_IdDefault=" + strResourceForder_IdDefault;
            }
        }
    }

}