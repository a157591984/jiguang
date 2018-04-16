using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.Evaluation
{
    public partial class KPScoreComparison : Rc.Cloud.Web.Common.FInitData
    {
        protected string HomeWork_ID = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        public string UserId = string.Empty;
        public string ClassID = string.Empty;
        public string TeacherID = string.Empty;
        public string ClassName = string.Empty;
        public string StrTemp = string.Empty;
        // Model_StatsClassHW_ScoreOverview modelschwso = new Model_StatsClassHW_ScoreOverview();
        BLL_StatsClassHW_Score bllschwso = new BLL_StatsClassHW_Score();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClassID = Request.QueryString["ClassID"].Filter();
            TeacherID = Request.QueryString["TeacherID"].Filter();
            ClassName = Request.QueryString["ClassName"].Filter();
            UserId = FloginUser.UserId;
            HomeWork_ID = Request.QueryString["HomeWork_ID"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            if (!string.IsNullOrEmpty(TeacherID))
            {
                StrTemp = "&ClassID=" + ClassID + "&ClassName=" + ClassName + "&TeacherID=" + TeacherID;
                UserId = TeacherID;
            }
        }
        /// <summary>
        /// 知识点得分对比
        /// </summary>
        /// <param name="StatsClassHW_ScoreOverviewID"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string StatsClassHW_KP(string ResourceToResourceFolder_Id, string UserId, string ClassID)
        {
            try
            {
                string StrWhere = "ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id.Filter() + "' and TeacherID='" + UserId.Filter() + "'";
                //if (!string.IsNullOrEmpty(ClassID.Filter()))
                //{
                //    StrWhere += " and ClassID='" + ClassID.Filter() + "'";
                //}
                StringBuilder StrTBody = new StringBuilder();
                StringBuilder StrTHead = new StringBuilder();
                string StrClassname = string.Empty;
                string DateSour = string.Empty;
                string StrScoreTemp = string.Empty;
                string StrKPNum = string.Empty;
                BLL_StatsClassHW_KP bllStatsClassHW_KP = new BLL_StatsClassHW_KP();
                List<Model_StatsClassHW_KP> StatsClassHW_KPList = new List<Model_StatsClassHW_KP>();
                List<Model_StatsClassHW_KP> listAll = new List<Model_StatsClassHW_KP>();//所有数据
                listAll = bllStatsClassHW_KP.GetModelList(StrWhere + " order by KPName ");
                List<Model_StatsClassHW_KP> listDistictClass = listAll.Where((x, i) => listAll.FindIndex(z => z.ClassID == x.ClassID) == i).ToList();//去重后数据
                List<Model_StatsClassHW_KP> listDistictKP = listAll.Where((x, i) => listAll.FindIndex(z => z.KPName == x.KPName) == i).ToList();//去重后数据
                StrTHead.Append("<tr><td class='align_left'>知识点名称</td><td>知识点分数</td>");
                foreach (var item in listDistictClass)
                {
                    StrTHead.AppendFormat("<td>{0}平均得分率</td>", item.ClassName);
                    List<Model_StatsClassHW_KP> listAllbyClass = listAll.Where(x => x.ClassID == item.ClassID).ToList();
                    foreach (var itemtq in listAllbyClass)
                    {
                        StrScoreTemp += itemtq.KPScoreAvgRate + ",";

                    }

                    DateSour += "{\"name\":\"" + item.ClassName + "\",\"data\":[" + StrScoreTemp.TrimEnd(',') + "]},";
                    StrScoreTemp = "";
                }
                StrTHead.Append("</tr>");

                foreach (var item in listDistictKP)
                {
                    StrTBody.AppendFormat("<tr><td class='align_left'>{0}</td><td>{1}</td>", item.KPName, item.KPScoreSum.ToString().clearLastZero());
                    foreach (var itemClass in listDistictClass)
                    {
                        List<Model_StatsClassHW_KP> listDistictBody = listAll.Where(x => x.ClassID == itemClass.ClassID && x.KPName == item.KPName).ToList();
                        foreach (var itemBody in listDistictBody)
                        {
                            StrTBody.AppendFormat("<td>{0}%</td>", itemBody.KPScoreAvgRate.ToString().clearLastZero());
                        }
                        if (listDistictBody.Count == 0)
                        {
                            StrTBody.Append("<td>-</td>");
                        }
                    }
                    StrKPNum += item.KPName + ",";
                    StrTBody.Append("</tr>");
                }
                string oo = DateSour;
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    thead = StrTHead.ToString(),
                    tbody = StrTBody.ToString(),
                    DateSour = "[" + DateSour.TrimEnd(',') + "]",
                    StrKPNum = StrKPNum.TrimEnd(',')
                });
            }

            catch (Exception)
            {
                return "";
            }
        }
    }
}