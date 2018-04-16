using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.Principal
{
    public partial class ResultsPreview : Rc.Cloud.Web.Common.FInitData
    {
        public string GradeId = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();

        }

        [WebMethod]
        public static string GetStatsGradeHW_Score(string GradeId, string ResourceToResourceFolder_Id)
        {
            try
            {
                GradeId = GradeId.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                string TempAll = "<tr><td class='text-left'>全部</td><td class='text-left'>{8}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{0}</td><td>{9}</td><td>{6}/{7}</td></tr>";
                string Temps = "<tr><td class='text-left'>{9}</td><td class='text-left'>{8}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{0}</td><td>{10}</td><td>{6}/{7}</td></tr>";
                string TempDate = string.Empty;
                string TempDateAll = string.Empty;
                BLL_StatsGradeHW_Score bllStatsGradeHW_Score = new BLL_StatsGradeHW_Score();
                List<Model_StatsGradeHW_Score> StatsGradeHW_ScoreList = new List<Model_StatsGradeHW_Score>();
                StatsGradeHW_ScoreList = bllStatsGradeHW_Score.GetModelList("GradeID='" + GradeId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'");
                foreach (var item in StatsGradeHW_ScoreList)
                {
                    TempDateAll = string.Format(TempAll, item.ClassAllCount.ToString().clearLastZero(), item.HighestScore.ToString().clearLastZero(), item.LowestScore.ToString().clearLastZero(), item.AVGScore.ToString().clearLastZero(), item.Median.ToString().clearLastZero(), item.Mode, item.CommittedCount.ToString().clearLastZero(), item.UncommittedCount.ToString().clearLastZero(), "-", item.AssignedCount.ToString().clearLastZero() + "/" + (Convert.ToInt32(item.ClassAllCount) - Convert.ToInt32(item.AssignedCount)));
                }
                BLL_StatsClassHW_Score bllStatsClassHW_Score = new BLL_StatsClassHW_Score();
                List<Model_StatsClassHW_Score> StatsClassHW_ScoreList = new List<Model_StatsClassHW_Score>();
                StatsClassHW_ScoreList = bllStatsClassHW_Score.GetModelList("GradeID='" + GradeId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'");
                foreach (var item in StatsClassHW_ScoreList)
                {
                    TempDate += string.Format(Temps, item.ClassAllCount.ToString().clearLastZero(), item.HighestScore.ToString().clearLastZero(), item.LowestScore.ToString().clearLastZero(), item.AVGScore.ToString().clearLastZero(), item.Median.ToString().clearLastZero(), item.Mode, item.CommittedCount.ToString().clearLastZero(), item.UncommittedCount.ToString().clearLastZero(), item.TeacherName, item.ClassName, item.AssignedCount.ToString().clearLastZero() + "/" + (Convert.ToInt32(item.ClassAllCount) - Convert.ToInt32(item.AssignedCount)));
                }
                return TempDateAll + "†" + TempDate;

            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}