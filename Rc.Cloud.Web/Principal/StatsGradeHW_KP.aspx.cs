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
    public partial class StatsGradeHW_KP : Rc.Cloud.Web.Common.FInitData
    {
        public string GradeId = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
        }


        /// <summary>
        /// 知识点得分对比
        /// </summary>
        /// <param name="StatsClassHW_ScoreOverviewID"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStatsGradeHW_KP(string ResourceToResourceFolder_Id, string GradeId)
        {
            try
            {
                StringBuilder StrTBody = new StringBuilder();
                StringBuilder StrTHead = new StringBuilder();
                string StrClassname = string.Empty;
                string DateSour = string.Empty;

                string StrKPNum = string.Empty;
                //班级
                BLL_StatsClassHW_KP bllStatsClassHW_KP = new BLL_StatsClassHW_KP();
                List<Model_StatsClassHW_KP> StatsClassHW_KPList = new List<Model_StatsClassHW_KP>();
                List<Model_StatsClassHW_KP> listAll = new List<Model_StatsClassHW_KP>();//所有数据
                listAll = bllStatsClassHW_KP.GetModelList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and Gradeid='" + GradeId + "' and  KPName<>'' order by TestQuestionNums  ");
                List<Model_StatsClassHW_KP> listDistictClass = listAll.Where((x, i) => listAll.FindIndex(z => z.ClassID == x.ClassID) == i).ToList();//去重后数据
                List<Model_StatsClassHW_KP> listDistictKP = listAll.Where((x, i) => listAll.FindIndex(z => z.KPName == x.KPName) == i).ToList();//去重后数据

                foreach (var item in listDistictClass)
                {
                    StrTHead.AppendFormat("<li title='{0}得分率'>{0}得分率</li>", item.ClassName);

                }
                
                //年级
                BLL_StatsGradeHW_KP bllStatsGradeHW_KP = new BLL_StatsGradeHW_KP();
                List<Model_StatsGradeHW_KP> StatsGradeHW_KPList = new List<Model_StatsGradeHW_KP>();
                List<Model_StatsGradeHW_KP> StatsGradeHW_KPlistAll = new List<Model_StatsGradeHW_KP>();//所有数据
                StatsGradeHW_KPlistAll = bllStatsGradeHW_KP.GetModelList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and Gradeid='" + GradeId + "' and KPName<>'' order by TestQuestionNums  ");
                List<Model_StatsGradeHW_KP> listStatsGradeHW_KP = StatsGradeHW_KPlistAll.Where((x, i) => StatsGradeHW_KPlistAll.FindIndex(z => z.KPName == x.KPName) == i).ToList();//去重后数据
                DataTable dtTQ_S = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num,TestQuestions_OrderNum ").Tables[0];

                string GradeBody = string.Empty;
                GradeBody = "<ul class=\"clearfix\"><li class=\"perform_left\"><ul class=\"clearfix\"><li class=\"l_1\" title='{0}'>{0}</li><li class=\"l_2\">{1}</li><li class=\"l_3\" title='{2}'>{2}</li><li class=\"l_4\">{3}%</li></ul></li>";
                foreach (var item in listStatsGradeHW_KP)
                {
                    string KPClassNameTemp = string.Empty;
                    string KPScoreAvgRateTemp = string.Empty;
                    string strKPName = item.KPName.Replace("'", "").Replace("\"", "");
                    StrTBody.AppendFormat(GradeBody
                        , strKPName
                        , item.KPScoreSum.ToString().clearLastZero()
                        , GetTQNum(dtTQ_S, item.KPName, item.TestQuestionNumStrs.TrimEnd('.'), item.TestQuestionNums)
                        , item.KPScoreAvgRate.ToString().clearLastZero());

                    StrTBody.Append("<li class=\"perform_center\"><div class=\"perform_div\" data-name=\"perform_div\"><ul class=\"clearfix\">");
                    foreach (var itemClass in listDistictClass)
                    {
                        List<Model_StatsClassHW_KP> listDistictBody = listAll.Where(x => x.ClassID == itemClass.ClassID && x.KPName == item.KPName).ToList();
                        foreach (var itemBody in listDistictBody)
                        {
                            StrTBody.AppendFormat("<li>{0}%</li>", itemBody.KPScoreAvgRate.ToString().clearLastZero());
                            KPClassNameTemp += itemClass.ClassName + "†";
                            KPScoreAvgRateTemp += itemBody.KPScoreAvgRate + "†";

                        }
                        if (listDistictBody.Count == 0)
                        {
                            StrTBody.Append("<li>-</li>");
                            KPClassNameTemp += itemClass.ClassName + "†";
                            KPScoreAvgRateTemp += "0" + "†";
                        }
                    }
                    //string Josn = "['" + item.KPName + "','" + KPClassNameTemp.TrimEnd(',') + "','" + KPScoreAvgRateTemp.TrimEnd(',') + "']";
                    //string Josn = "";
                    StrTBody.Append("</ul></div></li><li class=\"perform_right\"><a href='##' onclick=\"javascript:ShowPic('" + strKPName + "','" + KPClassNameTemp.TrimEnd('†') + "','" + KPScoreAvgRateTemp.TrimEnd('†') + "');\">图表</a></li></ul>");

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
        /// <summary>
        /// 取题号
        /// </summary>
        /// <param name="TQNumArr"></param>
        /// <returns></returns>
        public static string GetTQNum(DataTable dtTQ_S, string KPName, string TQNumArr, string TestQuestionNums)
        {
            try
            {

                //<a href='##'>第{3}题</a><a href=javascript:;>第{3}题</a>
                string[] TQNum = TQNumArr.Split(',');
                string[] TQNumS = TestQuestionNums.Split(',');
                string Temp = string.Empty;
                if (TQNum.Length > 0)
                {
                    DataRow[] drTQ_S = dtTQ_S.Select(" ContentText='" + KPName + "' ");
                    for (int i = 0; i < TQNum.Length; i++)
                    {
                        string testIndex = string.Empty;
                        if (drTQ_S.Length > 0 && (drTQ_S[i]["TestType"].ToString() == "clozeTest" || drTQ_S[i]["TestType"].ToString() == "fill"))
                        {
                            testIndex = string.IsNullOrEmpty(drTQ_S[i]["testIndex"].ToString()) ? "" : "-" + drTQ_S[i]["testIndex"].ToString();
                        }
                        Temp += "第" + TQNum[i].TrimEnd('.') + testIndex + "题，";
                    }
                    return Temp.TrimEnd('，');
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}