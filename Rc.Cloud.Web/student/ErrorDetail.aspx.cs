using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.student
{
    public partial class ErrorDetail : System.Web.UI.Page
    {
        public string HomeWork_Id = string.Empty;
        public string Student_Id = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        public string KPNameBasic = string.Empty;
        public string strTestpaperViewWebSiteUrl = string.Empty;
        public string S_KnowledgePoint_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                S_KnowledgePoint_Id = Request["S_KnowledgePoint_Id"].Filter();
                //HomeWork_Id = "e20fe518-e229-48ba-b86f-c29b6e6894e5";//Request["HomeWork_Id"].Filter();
                Student_Id = Request["Student_Id"].Filter();
                KPNameBasic = Request["KPNameBasic"].Filter();
                this.Title = KPNameBasic;
                strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");
                if (!string.IsNullOrEmpty(Student_Id) && strTestpaperViewWebSiteUrl == Rc.Common.ConfigHelper.GetConfigString("TestWebSiteUrl"))
                {
                    strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl", Student_Id);
                }
                if (!IsPostBack)
                {
                    this.ltlReName.Text = KPNameBasic;
                }
            }
            catch (Exception ex)
            {
                Response.Write("页面加载失败...");
                Response.End();
            }

        }
    }
}