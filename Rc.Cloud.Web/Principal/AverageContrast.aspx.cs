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
    public partial class AverageContrast : Rc.Cloud.Web.Common.FInitData
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
                string TempGrade = string.Empty;
                string avgScore = string.Empty;
                string Temp = " <tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>";
                string TempDate = string.Empty;
                BLL_StatsClassHW_Score bllStatsClassHW_Score = new BLL_StatsClassHW_Score();
                List<Model_StatsClassHW_Score> StatsClassHW_ScoreList = new List<Model_StatsClassHW_Score>();
                StatsClassHW_ScoreList = bllStatsClassHW_Score.GetModelList("GradeID='" + GradeId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by AVGScore desc");
                int i = 0;
                foreach (var item in StatsClassHW_ScoreList)
                {

                    TempDate += string.Format(Temp, i + 1, item.ClassName, item.TeacherName, item.AVGScore.ToString().clearLastZero());
                    TempGrade += "" + item.ClassName + ",";
                    avgScore += item.AVGScore + ",";
                    i++;
                }
                if (string.IsNullOrEmpty(TempDate))
                {
                    return "";
                }
                else
                {
                    return TempDate + "|" + TempGrade.TrimEnd(',') + "|" + avgScore.TrimEnd(',') + "";
                }

            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}