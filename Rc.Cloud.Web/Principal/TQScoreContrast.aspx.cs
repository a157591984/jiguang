using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.Principal
{
    public partial class TQScoreContrast : Rc.Cloud.Web.Common.FInitData
    {
        public string GradeId = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
        }
        /// <summary>
        /// 小题得分对比
        /// </summary>
        /// <param name="StatsClassHW_ScoreOverviewID"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStatsGradeHW_TQ(string ResourceToResourceFolder_Id, string GradeId)
        {
            try
            {
                StringBuilder StrTBody = new StringBuilder();
                StringBuilder StrTHead = new StringBuilder();
                string StrClassname = string.Empty;
                string DateSour = string.Empty;
                string KPScoreAvgRateTemp = string.Empty;
                string StrKPNum = string.Empty;
                //班级
                BLL_StatsClassHW_TQ bllStatsClassHW_TQ = new BLL_StatsClassHW_TQ();
                List<Model_StatsClassHW_TQ> StatsClassHW_TQList = new List<Model_StatsClassHW_TQ>();
                List<Model_StatsClassHW_TQ> listAll = new List<Model_StatsClassHW_TQ>();//所有数据
                listAll = bllStatsClassHW_TQ.GetModelList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and Gradeid='" + GradeId + "' order by TestQuestions_Num ");
                List<Model_StatsClassHW_TQ> listDistictClass = listAll.Where((x, i) => listAll.FindIndex(z => z.ClassID == x.ClassID) == i).ToList();//去重后数据
                List<Model_StatsClassHW_TQ> listDistictKP = listAll.Where((x, i) => listAll.FindIndex(z => z.TestQuestions_Score_ID == x.TestQuestions_Score_ID) == i).ToList();//去重后数据

                foreach (var item in listDistictClass)
                {
                    StrTHead.AppendFormat("<li title='{0}得分率'>{0}得分率</li>", item.ClassName);

                }

                //年级
                DataTable dtTQ_S = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num,TestQuestions_OrderNum ").Tables[0];
                BLL_StatsGradeHW_TQ bllStatsGradeHW_TQ = new BLL_StatsGradeHW_TQ();
                List<Model_StatsGradeHW_TQ> StatsGradeHW_TQlistAll = new List<Model_StatsGradeHW_TQ>();//所有数据
                StatsGradeHW_TQlistAll = bllStatsGradeHW_TQ.GetModelList("TQ_Score!=-1 and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and Gradeid='" + GradeId + "' order by TestQuestions_Num,TestQuestions_OrderNum ");
                List<Model_StatsGradeHW_TQ> listStatsGradeHW_TQ = StatsGradeHW_TQlistAll.Where((x, i) => StatsGradeHW_TQlistAll.FindIndex(z => z.TestQuestions_Score_ID == x.TestQuestions_Score_ID) == i).ToList();//去重后数据
                string GradeBody = string.Empty;
                GradeBody = "<ul class=\"clearfix\"><li class=\"perform_left\"><ul class=\"clearfix\"><li class=\"l_1\">{0}</li><li class=\"l_2\">{1}</li><li class=\"l_3\">{2}%</li></ul></li>";
                foreach (var item in listStatsGradeHW_TQ)
                {
                    List<Model_StatsGradeHW_TQ> listSub = StatsGradeHW_TQlistAll.Where(m => m.TestQuestions_Num == item.TestQuestions_Num).ToList();
                    DataRow[] drTQ_S = dtTQ_S.Select("TestQuestions_Score_ID='" + item.TestQuestions_Score_ID + "'");
                    string testIndex = string.Empty;
                    if (drTQ_S.Length > 0 && (drTQ_S[0]["TestType"].ToString() == "clozeTest" || drTQ_S[0]["TestType"].ToString() == "fill"))
                    {
                        testIndex = string.IsNullOrEmpty(drTQ_S[0]["testIndex"].ToString()) ? "" : "-" + drTQ_S[0]["testIndex"].ToString();
                    }
                    StrTBody.AppendFormat(GradeBody
                        , item.topicNumber.TrimEnd('.') + testIndex
                        , item.TQ_Score.ToString().clearLastZero()
                        , item.ScoreAvgRate.ToString().clearLastZero());
                    StrTBody.Append("<li class=\"perform_center\"><div class=\"perform_div\" data-name=\"perform_div\"><ul class=\"clearfix\">");
                    foreach (var itemClass in listDistictClass)
                    {
                        List<Model_StatsClassHW_TQ> listDistictBody = listAll.Where(x => x.ClassID == itemClass.ClassID && x.TestQuestions_Score_ID == item.TestQuestions_Score_ID).ToList();
                        foreach (var itemBody in listDistictBody)
                        {
                            StrTBody.AppendFormat("<li>{0}%</li>", itemBody.ScoreAvgRate.ToString().clearLastZero());
                        }
                        if (listDistictBody.Count == 0)
                        {
                            StrTBody.Append("<li>-</li>");
                        }
                    }
                    StrTBody.Append("</ul></div></li></ul>");

                }
                string oo = StrTBody.ToString();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    err = "",
                    thead = StrTHead.ToString(),
                    tbody = StrTBody.ToString()
                });
            }

            catch (Exception)
            {
                return "";
            }
        }

    }
}