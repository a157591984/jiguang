using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Data;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.Evaluation
{
    public partial class CommentReportStudentAnswer : System.Web.UI.Page
    {
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        protected string strResourceToResourceFolder_Id = string.Empty;
        protected string strTestQuestions_Id = string.Empty;
        protected string strStudent_Id = string.Empty;
        protected string strHomework_Id = string.Empty;
        protected string strAttrType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");
                        
            strResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            strTestQuestions_Id = Request.QueryString["TestQuestions_Id"].Filter();
            strStudent_Id = Request.QueryString["Student_Id"].Filter();
            strHomework_Id = Request.QueryString["Homework_Id"].Filter();
            strAttrType = "studentAnswer";
            

        }
    }
}