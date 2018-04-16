using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using System.Data;

namespace Rc.Cloud.Web.student
{
    public partial class HomeworkPreviewT : Rc.Cloud.Web.Common.FInitData
    {
        protected string groupId = string.Empty;
        protected string ResourceToResourceFolder_Id = string.Empty;
        protected string HomeWork_Id = string.Empty;
        public string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userid = FloginUser.UserId;
            groupId = Request.QueryString["groupId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();

            //BLL_HomeWork hwbll = new BLL_HomeWork();
            //DataTable dt = hwbll.GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and UserGroup_Id='" + groupId + "'").Tables[0];
            //if (dt.Rows.Count > 0)
            //{
            //    HomeWork_Id = dt.Rows[0]["HomeWork_Id"].ToString();
            //}
            //if (!IsPostBack)
            //{
            //    if (string.IsNullOrEmpty(HomeWork_Id))
            //    {
            //        ltlBtn.Text = "<a href='##' class=\"layoutIframe preview_arrange_btn\">布置作业</a>";
            //    }
            //    else
            //    {
            //        ltlBtn.Text = "<a href='##' class=\"AddlayoutIframe preview_arrange_btn\">增加学生</a><a href='##' class=\"DellayoutIframe preview_arrange_btn\" style='top:230px'>重新布置</a>";
            //    }
            //}
        }
    }

}