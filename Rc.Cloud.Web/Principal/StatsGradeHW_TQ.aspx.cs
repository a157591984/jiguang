using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.Principal
{
    public partial class StatsGradeHW_TQ : Rc.Cloud.Web.Common.FInitData
    {
        public string GradeId = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();

        }

        [WebMethod]
        public static string GetStatsGradeHW_TQ(string GradeId, string ResourceToResourceFolder_Id)
        {
            try
            {
                GradeId = GradeId.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                string Temps = "<tr><td>{0}</td><td>{1}</td><td class='text-left'>{2}</td><td class='text-left'>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}%</td><td class='table_opera'><a style='cursor:pointer;' onclick=\"javascript:PicPreview('../student/questionAttr.aspx?resourceid={10}&questionid={11}&attrType=AnalyzeHtml','解析');\">解析</a></td></tr>";
                string TempDate = string.Empty;
                DataTable dtTQ_S = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num,TestQuestions_OrderNum ").Tables[0];

                BLL_StatsGradeHW_TQ bllStatsGradeHW_TQ = new BLL_StatsGradeHW_TQ();
                List<Model_StatsGradeHW_TQ> StatsGradeHW_TQList = new List<Model_StatsGradeHW_TQ>();
                StatsGradeHW_TQList = bllStatsGradeHW_TQ.GetModelList("TQ_Score!=-1 and GradeID='" + GradeId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num,TestQuestions_OrderNum");
                foreach (var item in StatsGradeHW_TQList)
                {
                    List<Model_StatsGradeHW_TQ> listSub = StatsGradeHW_TQList.Where(m => m.TestQuestions_Num == item.TestQuestions_Num).ToList();
                    DataRow[] drTQ_S = dtTQ_S.Select("TestQuestions_Score_ID='" + item.TestQuestions_Score_ID + "'");
                    string testIndex = string.Empty;
                    if (drTQ_S.Length > 0 && (drTQ_S[0]["TestType"].ToString() == "clozeTest" || drTQ_S[0]["TestType"].ToString() == "fill"))
                    {
                        testIndex = string.IsNullOrEmpty(drTQ_S[0]["testIndex"].ToString()) ? "" : "-" + drTQ_S[0]["testIndex"].ToString();
                    }
                    TempDate += string.Format(Temps
                        , item.topicNumber.TrimEnd('.') + testIndex
                        , item.TestQuestions_Type, item.TargetText, item.ContentText, item.complexityText, item.TQ_Score.ToString().clearLastZero(), item.ScoreAvg.ToString().clearLastZero(), item.StandardDeviation.ToString().clearLastZero(), item.Discrimination.ToString().clearLastZero(), item.ErrorRate.ToString().clearLastZero(), item.ResourceToResourceFolder_Id, item.TestQuestions_Score_ID);

                }
                return TempDate;

            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}