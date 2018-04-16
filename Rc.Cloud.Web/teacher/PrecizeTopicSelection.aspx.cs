using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Data;
using Newtonsoft.Json;
namespace Rc.Cloud.Web.teacher
{
    public partial class PrecizeTopicSelection : Rc.Cloud.Web.Common.FInitData
    {
        public string ChapterAssembly_TQ_Id = string.Empty;
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");
            ChapterAssembly_TQ_Id = Request["ChapterAssembly_TQ_Id"];
        }
        /// <summary>
        /// 更新题
        /// </summary>
        /// <param name="RelationPaperTemp_id"></param>
        /// <param name="TestQuestions_id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string UpdateTQ(string ChapterAssembly_TQ_Id, string TestQuestions_id)
        {
            try
            {
                Model_ChapterAssembly_TQ ChapterTQ = new BLL_ChapterAssembly_TQ().GetModel(ChapterAssembly_TQ_Id);
                Model_TestQuestions TQ = new BLL_TestQuestions().GetModel(TestQuestions_id);
                string sqlUpdate = string.Empty;
                int m = 0;
                int RetrunValue = 0;
                int changeType = 0;//换题类型（1普通题换综合题，2综合题换普通题）
                if (ChapterTQ != null && TQ != null)
                {
                    if (ChapterTQ.type.TrimEnd() == "simple" && TQ.type.TrimEnd() == "complex")//普通题换成综合题
                    {
                        changeType = 1;
                        sqlUpdate = string.Format("update ChapterAssembly_TQ set TestQuestions_id='{0}',type='complex'  where ChapterAssembly_TQ_Id='{1}'", TestQuestions_id, ChapterAssembly_TQ_Id);
                        m = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sqlUpdate);
                        DataTable dtList = new BLL_ChapterAssembly_TQ().GetList(ChapterTQ.Identifier_Id, ChapterAssembly_TQ_Id, "1", out RetrunValue).Tables[0];
                    }
                    if (ChapterTQ.type.TrimEnd() == "complex" && TQ.type.TrimEnd() == "simple")//综合题换成普通题
                    {
                        changeType = 2;
                        sqlUpdate = string.Format("update ChapterAssembly_TQ set TestQuestions_id='{0}', type='simple'  where ChapterAssembly_TQ_Id='{1}'", TestQuestions_id, ChapterAssembly_TQ_Id);
                        m = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sqlUpdate);
                        DataTable dtList = new BLL_ChapterAssembly_TQ().GetList(ChapterTQ.Identifier_Id, ChapterAssembly_TQ_Id, "2", out RetrunValue).Tables[0];
                    }
                    if (ChapterTQ.type.TrimEnd() == TQ.type.TrimEnd())//相同题型互换
                    {
                        sqlUpdate = string.Format("update ChapterAssembly_TQ set TestQuestions_id='{0}' where ChapterAssembly_TQ_Id='{1}'", TestQuestions_id, ChapterAssembly_TQ_Id);
                        m = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sqlUpdate);
                    }
                    if (m > 0)
                    {
                        string strJson = JsonConvert.SerializeObject(new
                        {
                            err = "",
                            RetrunValue = RetrunValue,
                            ChangeType = changeType
                        });
                        return strJson.ToString();
                    }
                    else
                    {
                        string strJson = JsonConvert.SerializeObject(new
                       {
                           err = "null"
                       });
                        return strJson.ToString();
                    }
                }
                else
                {
                    string strJson = JsonConvert.SerializeObject(new
                   {
                       err = "null"
                   });
                    return strJson.ToString();
                }

            }
            catch (Exception ex)
            {
                string strJson = JsonConvert.SerializeObject(new
               {
                   err = "null"
               });
                return strJson.ToString();
            }
        }
    }
}