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
    public partial class ComparisonBetweenClasses : Rc.Cloud.Web.Common.FInitData
    {
        protected string HomeWork_ID = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        public string UserId = string.Empty;
        // Model_StatsClassHW_ScoreOverview modelschwso = new Model_StatsClassHW_ScoreOverview();
        BLL_StatsClassHW_Score bllschwso = new BLL_StatsClassHW_Score();
        CommonHandel ch = new CommonHandel();
        public string ClassID = string.Empty;
        public string TeacherID = string.Empty;
        public string ClassName = string.Empty;
        public string StrTemp = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = FloginUser.UserId;
            HomeWork_ID = Request.QueryString["HomeWork_ID"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            ClassID = Request.QueryString["ClassID"].Filter();
            TeacherID = Request.QueryString["TeacherID"].Filter();
            ClassName = Request.QueryString["ClassName"].Filter();
            if (!string.IsNullOrEmpty(TeacherID))
            {
                StrTemp = "&ClassID=" + ClassID + "&ClassName=" + ClassName + "&TeacherID=" + TeacherID;
                UserId = TeacherID;
            }
        }
        /// <summary>
        /// 成绩概况
        /// </summary>
        /// <param name="StatsClassHW_ScoreOverviewID"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStatsClassHW_Score(string ResourceToResourceFolder_Id, string UserId, string ClassID)
        {
            try
            {
                string StrWhere = "TeacherID='" + UserId.Filter() + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id.Filter() + "'";
                //if (!string.IsNullOrEmpty(ClassID))
                //{
                //    StrWhere += "and ClassID='" + ClassID.Filter() + "'";
                //}
                string StrTemp = string.Empty;
                BLL_StatsClassHW_Score bllcswso = new BLL_StatsClassHW_Score();
                List<Model_StatsClassHW_Score> ClassScoreOverviewList = new List<Model_StatsClassHW_Score>();
                ClassScoreOverviewList = bllcswso.GetModelList(StrWhere);
                string Temp = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}/{7}</td><td>{8}/{9}</td></tr>";
                foreach (var item in ClassScoreOverviewList)
                {
                    StrTemp += string.Format(Temp, item.ClassName, item.AVGScore.ToString().clearLastZero(), item.HighestScore.ToString().clearLastZero()
                        , item.LowestScore.ToString().clearLastZero(), item.Median.ToString().clearLastZero(), item.Mode, item.CommittedCount.ToString().clearLastZero(), item.UncommittedCount.ToString().clearLastZero(), item.CorrectedCount.ToString().clearLastZero(), item.UnCorrectedCount.ToString().clearLastZero());
                }
                return StrTemp;
            }

            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 班级作业等级
        /// </summary>
        /// <param name="StatsClassHW_ScoreOverviewID"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string StatsClassHW_ScoreLevel(string ResourceToResourceFolder_Id, string UserId, string ClassID)
        {
            try
            {
                ClassID = "";
                string StrTemp = string.Empty;
                string StrTempAll = string.Empty;
                string StrScore = string.Empty;
                string StrClassname = string.Empty;
                int SumHWScoreLevelCount = 0;
                string DateSour = string.Empty;
                string StrScoreTemp = string.Empty;
                string StrWhere = "ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id.Filter() + "' and TeacherID='" + UserId.Filter() + "'";
                if (!string.IsNullOrEmpty(ClassID.Filter()))
                {
                    StrWhere += " and ClassID=" + ClassID.Filter();

                    BLL_StatsClassHW_ScoreLevel bllStatsClassHW_ScoreLevel = new BLL_StatsClassHW_ScoreLevel();
                    List<Model_StatsClassHW_ScoreLevel> StatsClassHW_ScoreLevelList = new List<Model_StatsClassHW_ScoreLevel>();
                    List<Model_StatsClassHW_ScoreLevel> listAll = new List<Model_StatsClassHW_ScoreLevel>();//所有数据
                    listAll = bllStatsClassHW_ScoreLevel.GetModelList(StrWhere + " order by HWScoreLevelRight desc");
                    List<Model_StatsClassHW_ScoreLevel> listDistict = listAll.Where((x, i) => listAll.FindIndex(z => z.ClassID == x.ClassID) == i).ToList();//去重后数据
                    //{name: '1班',data: [5, 13, 12, 4, 1]},
                    foreach (var item in listDistict)
                    {

                        List<Model_StatsClassHW_ScoreLevel> listSub = listAll.Where(x => x.ClassID == item.ClassID).ToList();//子级数据
                        foreach (var itemSub in listSub)
                        {

                            StrScore += "<td>" + itemSub.HWScoreLevelCount.ToString().clearLastZero() + "</td>";
                            SumHWScoreLevelCount = SumHWScoreLevelCount + Convert.ToInt32(itemSub.HWScoreLevelCount.ToString().clearLastZero());
                            StrScoreTemp += itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }

                        StrTemp += "<tr><td>" + item.ClassName + "</td>" + StrScore + "<td>" + SumHWScoreLevelCount + "</td></tr>";
                        DateSour += "{\"name\":\"" + item.ClassName + "\",\"data\":[" + StrScoreTemp.TrimEnd(',') + "]},";
                        StrScore = "";
                        SumHWScoreLevelCount = 0;
                        StrScoreTemp = "";
                    }
                }
                else
                {
                    BLL_StatsClassHW_ScoreLevel bllStatsClassHW_ScoreLevel = new BLL_StatsClassHW_ScoreLevel();
                    List<Model_StatsClassHW_ScoreLevel> StatsClassHW_ScoreLevelList = new List<Model_StatsClassHW_ScoreLevel>();
                    List<Model_StatsClassHW_ScoreLevel> listAll = new List<Model_StatsClassHW_ScoreLevel>();//所有数据
                    listAll = bllStatsClassHW_ScoreLevel.GetModelList(StrWhere + " order by HWScoreLevelRight desc");
                    List<Model_StatsClassHW_ScoreLevel> listDistict = listAll.Where((x, i) => listAll.FindIndex(z => z.ClassID == x.ClassID) == i).ToList();//去重后数据
                    //{name: '1班',data: [5, 13, 12, 4, 1]},
                    foreach (var item in listDistict)
                    {

                        List<Model_StatsClassHW_ScoreLevel> listSub = listAll.Where(x => x.ClassID == item.ClassID).ToList();//子级数据
                        foreach (var itemSub in listSub)
                        {

                            StrScore += "<td>" + itemSub.HWScoreLevelCount.ToString().clearLastZero() + "</td>";
                            SumHWScoreLevelCount = SumHWScoreLevelCount + Convert.ToInt32(itemSub.HWScoreLevelCount.ToString().clearLastZero());
                            StrScoreTemp += itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }

                        StrTemp += "<tr><td>" + item.ClassName + "</td>" + StrScore + "<td>" + SumHWScoreLevelCount + "</td></tr>";
                        DateSour += "{\"name\":\"" + item.ClassName + "\",\"data\":[" + StrScoreTemp.TrimEnd(',') + "]},";
                        StrScore = "";
                        SumHWScoreLevelCount = 0;
                        StrScoreTemp = "";
                    }
                    string teacherStrScore = string.Empty;
                    BLL_StatsTeacherHW_ScoreLevel bllStatsTeacherStudentHW_Score = new BLL_StatsTeacherHW_ScoreLevel();
                    List<Model_StatsTeacherHW_ScoreLevel> StatsTeacherHW_ScoreLevelList = new List<Model_StatsTeacherHW_ScoreLevel>();
                    StatsTeacherHW_ScoreLevelList = bllStatsTeacherStudentHW_Score.GetModelList("TeacherID='" + UserId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by HWScoreLevelRight desc");
                    SumHWScoreLevelCount = 0;
                    foreach (var item in StatsTeacherHW_ScoreLevelList)
                    {
                        teacherStrScore += "<td>" + item.HWScoreLevelCount.ToString().clearLastZero() + "</td>";
                        SumHWScoreLevelCount = SumHWScoreLevelCount + Convert.ToInt32(item.HWScoreLevelCount.ToString().clearLastZero());
                    }
                    StrTempAll = "<tr><td>全部班级</td>" + teacherStrScore + "<td>" + SumHWScoreLevelCount + "</td></tr>";
                }

                return StrTemp + "|[" + DateSour.TrimEnd(',') + "]|" + StrTempAll;
            }

            catch (Exception)
            {
                return "";
            }
        }


        /// <summary>
        /// 班级层次分布对比
        /// </summary>
        /// <param name="StatsClassHW_ScoreOverviewID"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string StatsTeacherClassHW_Hierarchy(string ResourceToResourceFolder_Id, string UserId, string ClassID)
        {
            try
            {
                //Hierarchy=0为未提交学生数据
                string StrWhere = "Hierarchy>0 and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id.Filter() + "' and TeacherID='" + UserId.Filter() + "'";
                //if (!string.IsNullOrEmpty(ClassID.Filter()))
                //{
                //    StrWhere += " and ClassID=" + ClassID.Filter();
                //}
                string StrTBody = string.Empty; ;
                int intCount = 0;
                string StrTemp = string.Empty;
                string DateSour = string.Empty;
                string StrScoreTemp = string.Empty;
                BLL_StatsTeacherClassHW_Hierarchy bllStatsTeacherClassHW_Hierarchy = new BLL_StatsTeacherClassHW_Hierarchy();
                List<Model_StatsTeacherClassHW_Hierarchy> listAll = new List<Model_StatsTeacherClassHW_Hierarchy>();//所有数据
                listAll = bllStatsTeacherClassHW_Hierarchy.GetModelList(StrWhere + " order by Hierarchy");
                List<Model_StatsTeacherClassHW_Hierarchy> listDistict = listAll.Where((x, i) => listAll.FindIndex(z => z.ClassID == x.ClassID) == i).ToList();//去重后数据
                // <td>1班（40人）</td>{name: '1班',data: [10, 12, 14, 4]}
                foreach (var item in listDistict)
                {
                    StrTemp = string.Empty;
                    DateSour = string.Empty;
                    List<Model_StatsTeacherClassHW_Hierarchy> listSub = listAll.Where(x => x.ClassID == item.ClassID).ToList();//子级数据
                    if (listSub.Count == 4)
                    {
                        foreach (var itemSub in listSub)
                        {
                            if (Convert.ToInt16(itemSub.Hierarchy) > 0) intCount += Convert.ToInt32(itemSub.HierarchyCount.ToString().clearLastZero());
                            StrTemp += "<td>" + itemSub.HierarchyCountRate.ToString().clearLastZero() + "%(" + Convert.ToInt32(itemSub.HierarchyCount.ToString().clearLastZero()) + "人)</td>";
                            DateSour += itemSub.HierarchyCount.ToString().clearLastZero() + ",";
                        }
                    }
                    else
                    {
                        string td1 = "<td>0%(0人)</td>", td2 = "<td>0%(0人)</td>", td3 = "<td>0%(0人)</td>", td4 = "<td>0%(0人)</td>";
                        string data1 = "0,", data2 = "0,", data3 = "0,", data4 = "0,";
                        foreach (var itemSub in listSub)
                        {
                            if (Convert.ToInt16(itemSub.Hierarchy) > 0) intCount += Convert.ToInt32(itemSub.HierarchyCount.ToString().clearLastZero());
                            switch (Convert.ToInt16(itemSub.Hierarchy))
                            {
                                case 1:
                                    td1 = "<td>" + itemSub.HierarchyCountRate.ToString().clearLastZero() + "%(" + Convert.ToInt32(itemSub.HierarchyCount.ToString().clearLastZero()) + "人)</td>";
                                    data1 = itemSub.HierarchyCount.ToString().clearLastZero() + ",";
                                    break;
                                case 2:
                                    td2 = "<td>" + itemSub.HierarchyCountRate.ToString().clearLastZero() + "%(" + Convert.ToInt32(itemSub.HierarchyCount.ToString().clearLastZero()) + "人)</td>";
                                    data2 = itemSub.HierarchyCount.ToString().clearLastZero() + ",";
                                    break;
                                case 3:
                                    td3 = "<td>" + itemSub.HierarchyCountRate.ToString().clearLastZero() + "%(" + Convert.ToInt32(itemSub.HierarchyCount.ToString().clearLastZero()) + "人)</td>";
                                    data3 = itemSub.HierarchyCount.ToString().clearLastZero() + ",";
                                    break;
                                case 4:
                                    td4 = "<td>" + itemSub.HierarchyCountRate.ToString().clearLastZero() + "%(" + Convert.ToInt32(itemSub.HierarchyCount.ToString().clearLastZero()) + "人)</td>";
                                    data4 = itemSub.HierarchyCount.ToString().clearLastZero() + ",";
                                    break;
                            }
                        }

                        StrTemp += td1 + td2 + td3 + td4;
                        DateSour += data1 + data2 + data3 + data4;
                    }
                    StrTBody += "<tr><td>" + item.ClassName + "(" + intCount + "人)</td>" + StrTemp + "</tr>";
                    intCount = 0;
                    StrScoreTemp += "{\"name\":\"" + item.ClassName + "\",\"data\":[" + DateSour.TrimEnd(',') + "]},";
                }
                return StrTBody + "|[" + StrScoreTemp.TrimEnd(',') + "]";
            }

            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 分段分布
        /// </summary>
        /// <param name="StatsClassHW_ScoreOverviewID"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string StatsClassHW_Subsection(string ResourceToResourceFolder_Id, string UserId, string ClassID)
        {
            try
            {
                string StrWhere = "ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and TeacherID='" + UserId + "'";
                //if (!string.IsNullOrEmpty(ClassID.Filter()))
                //{
                //    StrWhere += " and ClassID=" + ClassID.Filter();
                //}
                StringBuilder StrTBody = new StringBuilder();
                StringBuilder StrTHead = new StringBuilder();
                string StrClassname = string.Empty;
                string DateSour = string.Empty;
                string StrScoreTemp = string.Empty;
                string StrTQNum = string.Empty;
                BLL_StatsClassHW_Subsection bllStatsClassHW_Subsection = new BLL_StatsClassHW_Subsection();
                List<Model_StatsClassHW_Subsection> StatsClassHW_TQList = new List<Model_StatsClassHW_Subsection>();
                List<Model_StatsClassHW_Subsection> listAll = new List<Model_StatsClassHW_Subsection>();//所有数据
                listAll = bllStatsClassHW_Subsection.GetModelList(StrWhere + "  order by cast(SUBSTRING(SubsectionName,0,charindex('～',SubsectionName,1)) as int) desc");
                List<Model_StatsClassHW_Subsection> listDistictClass = listAll.Where((x, i) => listAll.FindIndex(z => z.ClassID == x.ClassID) == i).ToList();//去重后数据
                List<Model_StatsClassHW_Subsection> listDistictSubsection = listAll.Where((x, i) => listAll.FindIndex(z => z.SubsectionName == x.SubsectionName) == i).ToList();//去重后数据
                StrTHead.Append("<tr><td>班级</td>");
                foreach (var item in listDistictSubsection)
                {
                    StrTHead.AppendFormat("<td>{0}</td>", item.SubsectionName);
                }
                StrTHead.Append("<td>人数</td><td>最高分</td><td>最低分</td><td>平均分</td></tr>");

                foreach (var item in listDistictClass)
                {
                    StrTBody.AppendFormat("<tr><td>{0}</td>", item.ClassName);
                    foreach (var itemDistictSubsection in listDistictSubsection)
                    {
                        List<Model_StatsClassHW_Subsection> listSub = listAll.Where(x => x.ClassID == item.ClassID && x.SubsectionName == itemDistictSubsection.SubsectionName).ToList();//子级数据
                        foreach (var itemSub in listSub)
                        {
                            StrTBody.AppendFormat("<td>{0}</td>", Convert.ToDecimal(itemSub.SubsectionCount.ToString().clearLastZero()).ToString("0"));
                        }
                    }
                    StrTBody.AppendFormat("<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", item.ClassAllCount.ToString().clearLastZero(), item.HighestScore.ToString().clearLastZero(), item.LowestScore.ToString().clearLastZero(), item.AVGScore.ToString().clearLastZero());
                }
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