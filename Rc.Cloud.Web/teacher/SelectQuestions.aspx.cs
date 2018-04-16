using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Text;
using System.Web.Services;
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.teacher
{
    public partial class SelectQuestions : Rc.Cloud.Web.Common.FInitData
    {
        public string RelationPaperTemp_id = string.Empty;
        public string Two_WayChecklistDetail_Id = string.Empty;
        public string TestQuestions_Id = string.Empty;
        DataTable dt = new DataTable();
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        protected string CreateUser = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            RelationPaperTemp_id = Request["RelationPaperTemp_id"].Filter();
            strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");
            CreateUser = FloginUser.UserId;
            if (!IsPostBack)
            {
                Model_RelationPaperTemp modelRPT = new BLL_RelationPaperTemp().GetModel(RelationPaperTemp_id);
                string Two_WayChecklistDetail_Id = string.Empty;
                string TestQuestions_Id = string.Empty;
                if (modelRPT != null)
                {
                    Two_WayChecklistDetail_Id = modelRPT.Two_WayChecklistDetail_Id;
                    TestQuestions_Id = modelRPT.TestQuestions_Id;
                    //除了已存在之外的所有题
                    string sqlTQ = string.Format(@" select ROW_NUMBER() OVER(ORDER BY Two_WayChecklistDetailToTestQuestions_Id) AS R_N 
,twtq.TestQuestions_Id 
from Two_WayChecklistDetailToTestQuestions twtq
inner join [dbo].[TestQuestions] tq on tq.TestQuestions_Id=twtq.TestQuestions_Id
inner join [dbo].[ResourceToResourceFolder] rtrf on rtrf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
 where Two_WayChecklistDetail_Id='{0}' and twtq.TestQuestions_Id<>'{1}'"
                        , Two_WayChecklistDetail_Id
                        , TestQuestions_Id);
                    rptTQ.DataSource = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQ).Tables[0];
                    rptTQ.DataBind();
                }
                
            }
        }

        /// <summary>
        /// 更新题
        /// </summary>
        /// <param name="RelationPaperTemp_id"></param>
        /// <param name="TestQuestions_id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string UpdateTQ(string RelationPaperTemp_id, string TestQuestions_id)
        {
            try
            {
                Model_RelationPaperTemp model = new BLL_RelationPaperTemp().GetModel(RelationPaperTemp_id);
                if (model != null)
                {
                    model.TestQuestions_Id = TestQuestions_id;
                    if (new BLL_RelationPaperTemp().Update(model))
                    {
                        return "1";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                { return ""; }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}