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
    public partial class StatsGradeHW_ScoreLevel : Rc.Cloud.Web.Common.FInitData
    {

        public string GradeId = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();

        }

        [WebMethod]
        public static string GetStatsGradeHW_ScoreLevel(string GradeId, string ResourceToResourceFolder_Id)
        {
            try
            {
                StringBuilder StrBody = new StringBuilder();
                StringBuilder StrBodyGrade = new StringBuilder();
                GradeId = GradeId.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                string TempClassName = string.Empty;
                string ClassHWScoreLevelCount = string.Empty;
                string TempDate = string.Empty;
                string Ydate = string.Empty;
                string Ldate = string.Empty;
                string Zdate = string.Empty;
                string Jdate = string.Empty;
                string Bdate = string.Empty;
                ///年级
                BLL_StatsGradeHW_ScoreLevel bllStatsGradeHW_ScoreLevel = new BLL_StatsGradeHW_ScoreLevel();
                List<Model_StatsGradeHW_ScoreLevel> StatsGradeHW_ScoreLevelList = new List<Model_StatsGradeHW_ScoreLevel>();
                StatsGradeHW_ScoreLevelList = bllStatsGradeHW_ScoreLevel.GetModelList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and Gradeid='" + GradeId + "' order by HWScoreLevelRight desc");
                List<Model_StatsGradeHW_ScoreLevel> listGradeDistict = StatsGradeHW_ScoreLevelList.Where((x, i) => StatsGradeHW_ScoreLevelList.FindIndex(z => z.Gradeid == x.Gradeid) == i).ToList();//去重后数据
                string sqlGrade = "select ResourceToResourceFolder_Id,Gradeid,CommittedCount ,UncommittedCount ,AssignedCount,ClassAllCount  from StatsGradeHW_Score";
                DataTable dtGrade = Rc.Common.DBUtility.DbHelperSQL.Query(sqlGrade).Tables[0];

                foreach (var item in listGradeDistict)
                {
                    DataRow[] drGrade = dtGrade.Select("ResourceToResourceFolder_Id='" + item.ResourceToResourceFolder_Id + "' and Gradeid='" + item.Gradeid + "'");
                    StrBodyGrade.Append("<tr><td>年级</td>");
                    List<Model_StatsGradeHW_ScoreLevel> listSub = StatsGradeHW_ScoreLevelList.Where(x => x.Gradeid == item.Gradeid).ToList();//子级数据
                    foreach (var itemSub in listSub)
                    {
                        StrBodyGrade.AppendFormat("<td>{0}</td>", itemSub.HWScoreLevelCount.ToString().clearLastZero());
                        if (itemSub.HWScoreLevelName == "优秀")
                        {
                            Ydate = itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }
                        if (itemSub.HWScoreLevelName == "良好")
                        {
                            Ldate = itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }
                        if (itemSub.HWScoreLevelName == "中等")
                        {
                            Zdate = itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }
                        if (itemSub.HWScoreLevelName == "及格")
                        {
                            Jdate = itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }
                        if (itemSub.HWScoreLevelName == "不及格")
                        {
                            Bdate = itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }

                    }
                    StrBodyGrade.AppendFormat("<td>{0}</td><td>{1}</td><td>{2}</td>"
                        , item.GradeAllCount
                        , drGrade[0]["AssignedCount"].ToString().clearLastZero() + "/" + (Convert.ToInt32(drGrade[0]["ClassAllCount"]) - Convert.ToInt32(drGrade[0]["AssignedCount"]))
                        , drGrade[0]["CommittedCount"].ToString().clearLastZero() + "/" + drGrade[0]["UncommittedCount"].ToString().clearLastZero());
                }
                //1,2,3
                ///班级
                BLL_StatsClassHW_ScoreLevel bllStatsClassHW_ScoreLevel = new BLL_StatsClassHW_ScoreLevel();
                List<Model_StatsClassHW_ScoreLevel> StatsClassHW_ScoreLevelList = new List<Model_StatsClassHW_ScoreLevel>();
                List<Model_StatsClassHW_ScoreLevel> listAll = new List<Model_StatsClassHW_ScoreLevel>();//所有数据
                listAll = bllStatsClassHW_ScoreLevel.GetModelList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and Gradeid='" + GradeId + "' order by HWScoreLevelRight desc");
                List<Model_StatsClassHW_ScoreLevel> listDistict = listAll.Where((x, i) => listAll.FindIndex(z => z.ClassID == x.ClassID) == i).ToList();//去重后数据
                string sqlClass = "select ResourceToResourceFolder_Id,Gradeid,ClassID,CommittedCount ,UncommittedCount ,AssignedCount,ClassAllCount  from StatsClassHW_Score";
                DataTable dtClass = Rc.Common.DBUtility.DbHelperSQL.Query(sqlClass).Tables[0];
                foreach (var item in listDistict)
                {
                    DataRow[] drClass = dtClass.Select("ResourceToResourceFolder_Id='" + item.ResourceToResourceFolder_Id + "' and Gradeid='" + item.Gradeid + "' and ClassID='" + item.ClassID + "'");
                    StrBody.AppendFormat("<tr><td>{0}</td>", item.ClassName);
                    List<Model_StatsClassHW_ScoreLevel> listSub = listAll.Where(x => x.ClassID == item.ClassID).ToList();//子级数据
                    foreach (var itemSub in listSub)
                    {
                        StrBody.AppendFormat("<td>{0}</td>", itemSub.HWScoreLevelCount.ToString().clearLastZero());
                        if (itemSub.HWScoreLevelName == "优秀")
                        {
                            Ydate += itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }
                        if (itemSub.HWScoreLevelName == "良好")
                        {
                            Ldate += itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }
                        if (itemSub.HWScoreLevelName == "中等")
                        {
                            Zdate += itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }
                        if (itemSub.HWScoreLevelName == "及格")
                        {
                            Jdate += itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }
                        if (itemSub.HWScoreLevelName == "不及格")
                        {
                            Bdate += itemSub.HWScoreLevelCount.ToString().clearLastZero() + ",";
                        }

                    }
                    StrBody.AppendFormat("<td>{0}</td><td>{1}</td><td>{2}</td>"
                        , Convert.ToInt32(item.ClassAllCount)
                        , drClass[0]["AssignedCount"].ToString().clearLastZero() + "/" + (Convert.ToInt32(drClass[0]["ClassAllCount"]) - Convert.ToInt32(drClass[0]["AssignedCount"]))
                        , drClass[0]["CommittedCount"].ToString().clearLastZero() + "/" + drClass[0]["UncommittedCount"].ToString().clearLastZero());
                    TempClassName += item.ClassName.TrimEnd() + "|";
                }
                StrBody.AppendFormat("</tr>");
                TempDate = "[{\"name\":\"优秀\",\"data\":[" + Ydate.TrimEnd(',') + "]},"
                    + "{\"name\":\"良好\",\"data\":[" + Ldate.TrimEnd(',') + "]},"
                    + "{\"name\":\"中等\",\"data\":[" + Zdate.TrimEnd(',') + "]},"
                    + "{\"name\":\"及格\",\"data\":[" + Jdate.TrimEnd(',') + "]}," + "{\"name\":\"不及格\",\"data\":[" + Bdate.TrimEnd(',') + "]}]";
                if (string.IsNullOrEmpty(StrBody.ToString()))
                {
                    return "";
                }
                //    [
                //    {
                //        name: '优秀',
                //        data: [49, 71, 76, 99, 100, 76, 35, 48, 62, 94, 95, 54]
                //    },
                //]
                else
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        TempClassName = "全年级|" + TempClassName.TrimEnd('|'),
                        TempDate = TempDate,
                        tb = StrBody.ToString(),
                        tbGrade = StrBodyGrade.ToString(),
                    });
                }

            }
            catch (Exception)
            {

                return "";
            }
        }

    }
}