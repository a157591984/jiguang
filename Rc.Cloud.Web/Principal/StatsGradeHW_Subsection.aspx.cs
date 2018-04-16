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
using System.Text;
namespace Rc.Cloud.Web.Principal
{
    public partial class StatsGradeHW_Subsection : Rc.Cloud.Web.Common.FInitData
    {

        public string GradeId = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
        }

        [WebMethod]
        public static string GetStatsGradeHW_Subsection(string GradeId, string ResourceToResourceFolder_Id)
        {
            try
            {
                StringBuilder StrTBody = new StringBuilder();
                StringBuilder StrTHead = new StringBuilder();
                string StrClassname = string.Empty;
                string DateSour = string.Empty;
                string StrScoreTemp = string.Empty;
                string StrTQNum = string.Empty;
                //年级
                BLL_StatsGradeHW_Subsection bllStatsGradeHW_Subsection = new BLL_StatsGradeHW_Subsection();
                List<Model_StatsGradeHW_Subsection> StatsClassHW_TQList = new List<Model_StatsGradeHW_Subsection>();
                List<Model_StatsGradeHW_Subsection> listAll = new List<Model_StatsGradeHW_Subsection>();//所有数据
                listAll = bllStatsGradeHW_Subsection.GetModelList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and Gradeid='" + GradeId + "' order by cast(SUBSTRING(SubsectionName,0,charindex('～',SubsectionName,1)) as int) desc");
                List<Model_StatsGradeHW_Subsection> listDistictGrade = listAll.Where((x, i) => listAll.FindIndex(z => z.Gradeid == x.Gradeid) == i).ToList();//去重后数据
                List<Model_StatsGradeHW_Subsection> listDistictSubsection = listAll.Where((x, i) => listAll.FindIndex(z => z.SubsectionName == x.SubsectionName) == i).ToList();//去重后数据
                StrTHead.Append("<tr><th>班级</th>");
                foreach (var item in listDistictSubsection)
                {
                    StrTHead.AppendFormat("<th>{0}</th>", item.SubsectionName);
                }
                StrTHead.Append("</tr>");

                foreach (var item in listDistictGrade)
                {
                    StrTBody.AppendFormat("<tr><td>{0}</td>", "年级");
                    foreach (var itemDistictSubsection in listDistictSubsection)
                    {
                        List<Model_StatsGradeHW_Subsection> listSub = listAll.Where(x => x.Gradeid == item.Gradeid && x.SubsectionName == itemDistictSubsection.SubsectionName).ToList();//子级数据
                        foreach (var itemSub in listSub)
                        {
                            StrTBody.AppendFormat("<td>{0}</td>", Convert.ToDecimal(itemSub.SubsectionCount.ToString().clearLastZero()).ToString("0"));
                        }
                    }
                    StrTBody.AppendFormat("</tr>");
                }
                //班级
                BLL_StatsClassHW_Subsection bllStatsClassHW_Subsection = new BLL_StatsClassHW_Subsection();//年级
                List<Model_StatsClassHW_Subsection> StatsClassHW_SubsectionList = new List<Model_StatsClassHW_Subsection>();
                List<Model_StatsClassHW_Subsection> listStatsClassHW_SubsectionAll = new List<Model_StatsClassHW_Subsection>();//所有数据
                listStatsClassHW_SubsectionAll = bllStatsClassHW_Subsection.GetModelList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and Gradeid='" + GradeId + "' order by cast(SUBSTRING(SubsectionName,0,charindex('～',SubsectionName,1)) as int) desc");
                List<Model_StatsClassHW_Subsection> listDistictClass = listStatsClassHW_SubsectionAll.Where((x, i) => listStatsClassHW_SubsectionAll.FindIndex(z => z.ClassID == x.ClassID) == i).ToList();//去重后数据
                List<Model_StatsClassHW_Subsection> listDistictSubsectionClass = listStatsClassHW_SubsectionAll.Where((x, i) => listStatsClassHW_SubsectionAll.FindIndex(z => z.SubsectionName == x.SubsectionName) == i).ToList();//去重后数据

                foreach (var item in listDistictClass)
                {
                    StrTBody.AppendFormat("<tr><td>{0}</td>", item.ClassName);
                    foreach (var itemDistictSubsection in listDistictSubsectionClass)
                    {
                        List<Model_StatsClassHW_Subsection> listSub = listStatsClassHW_SubsectionAll.Where(x => x.ClassID == item.ClassID && x.SubsectionName == itemDistictSubsection.SubsectionName).ToList();//子级数据
                        foreach (var itemSub in listSub)
                        {
                            StrTBody.AppendFormat("<td>{0}</td>", Convert.ToDecimal(itemSub.SubsectionCount.ToString().clearLastZero()).ToString("0"));
                        }
                    }
                    StrTBody.AppendFormat("</tr>");
                }

                string ss = StrTBody.ToString() + StrTHead.ToString();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    thead = StrTHead.ToString(),
                    tbody = StrTBody.ToString(),
                });

            }
            catch (Exception)
            {

                return "";
            }
        }

    }
}