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
    public partial class CombinationTestPaper : Rc.Cloud.Web.Common.FInitData
    {
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        public string Two_WayChecklist_Id = string.Empty;
        public string Two_WayChecklist_Name = string.Empty;
        public string guid = string.Empty;
        public string CreateUser = string.Empty;
        public string ugid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");
            Two_WayChecklist_Id = Request["Two_WayChecklist_Id"].Filter();
            ugid = Request["ugid"].Filter();//班级ID
            //Server.UrlDecode(Request["Two_WayChecklist_Name"].Filter());
            if (!IsPostBack)
            {
                Rc.Model.Resources.Model_Two_WayChecklist model = new Rc.BLL.Resources.BLL_Two_WayChecklist().GetModel(Two_WayChecklist_Id);
                if (model != null)
                {
                    Two_WayChecklist_Name = model.Two_WayChecklist_Name;
                }
                guid = Guid.NewGuid().ToString();
                CreateUser = FloginUser.UserId;

            }
        }
        /// <summary>
        /// 初始化题
        /// </summary>
        /// <param name="Two_WayChecklist_Id"></param>
        /// <param name="RelationPaper_Id"></param>
        /// <param name="CreateUser"></param>
        /// <returns></returns>
        [WebMethod]
        public static string InitializationTestQuestions(string Two_WayChecklist_Id, string RelationPaper_Id, string CreateUser)
        {
            try
            {
                int row = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec dbo.P_RelationPaper '" + Two_WayChecklist_Id + "','" + RelationPaper_Id + "','" + CreateUser + "' ", 7200);
                if (row > 0)
                {
                    return "1";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {

                return "";
            }
        }

    }
}