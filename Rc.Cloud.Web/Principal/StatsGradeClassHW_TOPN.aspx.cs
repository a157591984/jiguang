using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.Principal
{
    public partial class StatsGradeClassHW_TOPN : Rc.Cloud.Web.Common.FInitData
    {

        public string GradeId = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
        }

        [WebMethod]
        public static string GetStatsGradeClassHW_TOPN(string GradeId, string ResourceToResourceFolder_Id, string TOPN)
        {
            try
            {
                GradeId = GradeId.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                string TempClassName = string.Empty;
                string ClassCount = string.Empty;
                string Temp = " <tr><td>{0}</td><td>{1}</td><td>{2}%</td></tr>";//<td class='table_opera'><a href='##'>详情</a></td>
                string TempDate = string.Empty;
                string StrWhere = string.Empty;
                BLL_StatsGradeClassHW_TOPN bllGradeClassHW_TOPN = new BLL_StatsGradeClassHW_TOPN();
                List<Model_StatsGradeClassHW_TOPN> StatsGradeClassHW_TOPNList = new List<Model_StatsGradeClassHW_TOPN>();
                if (!string.IsNullOrEmpty(TOPN))
                {
                    StrWhere = "  TOPN='" + TOPN + "' and ";
                }
                StatsGradeClassHW_TOPNList = bllGradeClassHW_TOPN.GetModelList(StrWhere + "GradeID='" + GradeId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by ClassCount desc");
                int i = 0;
                foreach (var item in StatsGradeClassHW_TOPNList)
                {

                    TempDate += string.Format(Temp, item.ClassName, item.ClassCount.ToString().clearLastZero(), item.ClassCountRate.ToString().clearLastZero());
                    TempClassName += "" + item.ClassName + ",";
                    ClassCount += item.ClassCount + ",";
                    i++;
                }
                if (string.IsNullOrEmpty(TempDate))
                {
                    return "";
                }
                else
                {
                    return TempDate + "|" + TempClassName.TrimEnd(',') + "|" + ClassCount.TrimEnd(',') + "";
                }

            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}