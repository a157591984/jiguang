using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using System.Text;
using System.Data;
using Rc.Common.Config;
using Rc.Cloud.Web.Common;
using Rc.Cloud.BLL;
using Rc.Common.DBUtility;
using Rc.Common;
using Rc.Model.Resources;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.teacher
{
    public partial class CombinationTestPaperToChapter : Rc.Cloud.Web.Common.FInitData
    {
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        public string Identifier_Id = string.Empty;
        public string guid = string.Empty;
        public string ugid = string.Empty;//班级id
        protected void Page_Load(object sender, EventArgs e)
        {
            ugid = Request["ugid"].Filter();
            strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");
            Identifier_Id = Request["Identifier_Id"].Filter();
            if (!IsPostBack)
            {
                //guid = Guid.NewGuid().ToString();

            }
        }
        ///// <summary>
        ///// 初始化题
        ///// </summary>
        ///// <param name="Two_WayChecklist_Id"></param>
        ///// <param name="RelationPaper_Id"></param>
        ///// <param name="CreateUser"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public static string InitializationTestQuestions(string Identifier_Id)
        //{
        //    try
        //    {
        //        int row = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec dbo.P_RelationPaper '" + Two_WayChecklist_Id + "','" + RelationPaper_Id + "','" + CreateUser + "' ", 7200);
        //        if (row > 0)
        //        {
        //            return "1";
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return "";
        //    }
        //}

    }
}