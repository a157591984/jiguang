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
using System.Data;
namespace Rc.Cloud.Web.Evaluation
{
    public partial class TQScoreComparison : Rc.Cloud.Web.Common.FInitData
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
            UserId = FloginUser.UserId;
            ClassID = Request.QueryString["ClassID"].Filter();
            TeacherID = Request.QueryString["TeacherID"].Filter();
            ClassName = Request.QueryString["ClassName"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            HomeWork_ID = Request.QueryString["HomeWork_ID"].Filter();
            if (!string.IsNullOrEmpty(TeacherID))
            {
                StrTemp = "&ClassID=" + ClassID + "&ClassName=" + ClassName + "&TeacherID=" + TeacherID;
                UserId = TeacherID;
            }
        }
        /// <summary>
        /// 小题得分对比
        /// </summary>
        /// <param name="StatsClassHW_ScoreOverviewID"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string StatsClassHW_TQ(string ResourceToResourceFolder_Id, string UserId, string ClassID)
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
                string StrTQNum = string.Empty;
                BLL_StatsClassHW_TQ bllStatsClassHW_TQ = new BLL_StatsClassHW_TQ();
                List<Model_StatsClassHW_TQ> StatsClassHW_TQList = new List<Model_StatsClassHW_TQ>();
                List<Model_StatsClassHW_TQ> listAll = new List<Model_StatsClassHW_TQ>();//所有数据
                listAll = bllStatsClassHW_TQ.GetModelList(StrWhere + " order by TestQuestions_Num,TestQuestions_OrderNum ");
                List<Model_StatsClassHW_TQ> listDistictClass = listAll.Where((x, i) => listAll.FindIndex(z => z.ClassID == x.ClassID) == i).ToList();//去重后数据
                List<Model_StatsClassHW_TQ> listDistictTQ = listAll.Where((x, i) => listAll.FindIndex(z => z.TestQuestions_Score_ID == x.TestQuestions_Score_ID) == i).ToList();//去重后数据
                StrTHead.Append("<tr><td>题号</td><td>小题分数</td>");

                foreach (var item in listDistictClass)
                {
                    StrTHead.AppendFormat("<td>{0}平均得分率</td>", item.ClassName);
                    List<Model_StatsClassHW_TQ> listAllbyClass = listAll.Where(x => x.ClassID == item.ClassID).ToList();
                    foreach (var itemtq in listAllbyClass)
                    {
                        StrScoreTemp += itemtq.ScoreAvgRate.ToString().clearLastZero() + ",";

                    }

                    DateSour += "{\"name\":\"" + item.ClassName + "\",\"data\":[" + StrScoreTemp.TrimEnd(',') + "]},";
                    StrScoreTemp = "";
                }
                StrTHead.Append("</tr>");

                DataTable dtTQ_S = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num,TestQuestions_OrderNum ").Tables[0];

                foreach (var item in listDistictTQ)
                {
                    DataRow[] drTQ_S = dtTQ_S.Select("TestQuestions_Score_ID='" + item.TestQuestions_Score_ID + "'");
                    string testIndex = string.Empty;
                    if (drTQ_S.Length > 0 && (drTQ_S[0]["TestType"].ToString() == "clozeTest" || drTQ_S[0]["TestType"].ToString() == "fill"))
                    {
                        testIndex = string.IsNullOrEmpty(drTQ_S[0]["testIndex"].ToString()) ? "" : "-" + drTQ_S[0]["testIndex"].ToString();
                    }
                    List<Model_StatsClassHW_TQ> listSub = listDistictTQ.Where(m => m.TestQuestions_Num == item.TestQuestions_Num).ToList();
                    StrTBody.AppendFormat("<tr><td>{0}</td><td>{1}</td>"
                        , item.topicNumber.TrimEnd('.') + testIndex
                        , item.TQ_Score.ToString().clearLastZero());
                    foreach (var itemClass in listDistictClass)
                    {
                        List<Model_StatsClassHW_TQ> listDistictBody = listAll.Where(x => x.ClassID == itemClass.ClassID && x.TestQuestions_Score_ID == item.TestQuestions_Score_ID).ToList();
                        foreach (var itemBody in listDistictBody)
                        {
                            StrTBody.AppendFormat("<td>{0}%</td>", itemBody.ScoreAvgRate.ToString().clearLastZero());
                        }
                        if (listDistictBody.Count == 0)
                        {
                            StrTBody.Append("<td>-</td>");
                        }
                    }
                    StrTQNum += item.topicNumber.TrimEnd('.') + "题,";
                    StrTBody.Append("</tr>");
                }
                string oo = DateSour;
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    thead = StrTHead.ToString(),
                    tbody = StrTBody.ToString(),
                    DateSour = "[" + DateSour.TrimEnd(',') + "]",
                    StrTQNum = StrTQNum.TrimEnd(',')
                });
            }

            catch (Exception)
            {
                return "";
            }
        }
    }
}