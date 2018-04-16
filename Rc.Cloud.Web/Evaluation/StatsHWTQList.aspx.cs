using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Text;
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.Evaluation
{
    public partial class StatsHWTQList : Rc.Cloud.Web.Common.FInitData
    {
        public string UserId = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        public string HomeWork_ID = string.Empty;
        public string ClassID = string.Empty;
        public string TeacherID = string.Empty;
        public string ClassName = string.Empty;
        public string StrTemp = string.Empty;
        //Model_StatsClassHW_ScoreOverview modelschwso = new Model_StatsClassHW_ScoreOverview();
        BLL_StatsClassHW_Score bllschwso = new BLL_StatsClassHW_Score();
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassID = Request.QueryString["ClassID"].Filter();
            TeacherID = Request.QueryString["TeacherID"].Filter();
            ClassName = Request.QueryString["ClassName"].Filter();
            HomeWork_ID = Request.QueryString["HomeWork_ID"].Filter();
            UserId = FloginUser.UserId;
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(TeacherID))
                {
                    StrTemp = "&ClassID=" + ClassID + "&ClassName=" + ClassName + "&TeacherID=" + TeacherID;
                    UserId = TeacherID;
                }
                if (!string.IsNullOrEmpty(ResourceToResourceFolder_Id))
                {
                    ltlClasses.Text = StatsCommonHandle.GetTeacherClass(UserId, ClassID, ClassName, ResourceToResourceFolder_Id);//有效班级
                    if (string.IsNullOrEmpty(ClassID))
                    {
                        ClassID = "-1";
                    }
                }
            }
        }

        /// <summary>
        ///试题掌握情况（双向细目表）
        /// </summary>
        /// <param name="Class"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="UserId"></param>
        /// <param name="KPName"></param>
        /// <param name="KPScoreAvgRate"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStatsClassHW_TQ(string Class, string ResourceToResourceFolder_Id, string UserId)
        {
            try
            {
                Class = Class.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                UserId = UserId.Filter();
                string TempDate = string.Empty;
                string StrWhere = string.Empty;
                string Temp = "<tr><td>{0}</td><td>{1}</td><td class='text-left'>{2}</td><td class='text-left'>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}%</td><td><a style='cursor:pointer;' onclick=\"javascript:PicPreview('../student/questionAttr.aspx?resourceid={10}&questionid={11}&attrType=AnalyzeHtml','解析');\">解析</a></td></tr>";
                DataTable dtTQ_S = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num,TestQuestions_OrderNum ").Tables[0];

                if (Class == "-1")
                {
                    HttpContext.Current.Session["StatsClassClassId"] = "-1";
                    BLL_StatsTeacherHW_TQ bllStatsTeacherHW_TQ = new BLL_StatsTeacherHW_TQ();
                    List<Model_StatsTeacherHW_TQ> StatsTeacherHW_TQList = new List<Model_StatsTeacherHW_TQ>();

                    StatsTeacherHW_TQList = bllStatsTeacherHW_TQ.GetModelList("TQ_Score!=-1 and TeacherID='" + UserId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'" + StrWhere + " order by TestQuestions_Num,TestQuestions_OrderNum");
                    foreach (var item in StatsTeacherHW_TQList)
                    {
                        List<Model_StatsTeacherHW_TQ> listSub = StatsTeacherHW_TQList.Where(m => m.TestQuestions_Num == item.TestQuestions_Num).ToList();
                        DataRow[] drTQ_S = dtTQ_S.Select("TestQuestions_Score_ID='" + item.TestQuestions_Score_ID + "'");
                        string testIndex = string.Empty;
                        if (drTQ_S.Length > 0 && (drTQ_S[0]["TestType"].ToString() == "clozeTest" || drTQ_S[0]["TestType"].ToString() == "fill"))
                        {
                            testIndex = string.IsNullOrEmpty(drTQ_S[0]["testIndex"].ToString()) ? "" : "-" + drTQ_S[0]["testIndex"].ToString() ;
                        }
                        TempDate += string.Format(Temp
                            , item.topicNumber.TrimEnd('.') + testIndex
                            , item.TestQuestions_Type, item.TargetText, item.ContentText
                            , item.complexityText, item.TQ_Score.ToString().clearLastZero()
                            , item.ScoreAvg.ToString().clearLastZero() == null ? "0.00" : item.ScoreAvg.ToString().clearLastZero()
                            , item.StandardDeviation.ToString().clearLastZero() == null ? "0.00" : item.StandardDeviation.ToString().clearLastZero()
                            , item.Discrimination.ToString().clearLastZero() == null ? "0.00" : item.Discrimination.ToString().clearLastZero()
                            , item.ErrorRate.ToString().clearLastZero() == null ? "0.00" : item.ErrorRate.ToString().clearLastZero()
                            , item.ResourceToResourceFolder_Id, item.TestQuestions_Score_ID);
                    }
                    return TempDate;
                }
                else
                {
                    HttpContext.Current.Session["StatsClassClassId"] = Class;
                    BLL_StatsClassHW_TQ bllStatsClassHW_TQ = new BLL_StatsClassHW_TQ();
                    List<Model_StatsClassHW_TQ> ClassHW_TQList = new List<Model_StatsClassHW_TQ>();
                    ClassHW_TQList = bllStatsClassHW_TQ.GetModelList("TQ_Score!=-1 and ClassID='" + Class + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' " + StrWhere + " order by TestQuestions_Num,TestQuestions_OrderNum ");
                    foreach (var item in ClassHW_TQList)
                    {
                        List<Model_StatsClassHW_TQ> listSub = ClassHW_TQList.Where(m => m.TestQuestions_Num == item.TestQuestions_Num).ToList();
                        DataRow[] drTQ_S = dtTQ_S.Select("TestQuestions_Score_ID='" + item.TestQuestions_Score_ID + "'");
                        string testIndex = string.Empty;
                        if (drTQ_S.Length > 0 && (drTQ_S[0]["TestType"].ToString() == "clozeTest" || drTQ_S[0]["TestType"].ToString() == "fill"))
                        {
                            testIndex = string.IsNullOrEmpty(drTQ_S[0]["testIndex"].ToString()) ? "" : "-" + drTQ_S[0]["testIndex"].ToString();
                        }
                        TempDate += string.Format(Temp
                            , item.topicNumber.TrimEnd('.') + testIndex
                            , item.TestQuestions_Type, item.TargetText, item.ContentText
                            , item.complexityText, item.TQ_Score.ToString().clearLastZero()
                            , item.ScoreAvg.ToString().clearLastZero() == null ? "0.00" : item.ScoreAvg.ToString().clearLastZero()
                            , item.StandardDeviation.ToString().clearLastZero() == null ? "0.00" : item.StandardDeviation.ToString().clearLastZero()
                            , item.Discrimination.ToString().clearLastZero() == null ? "0.00" : item.Discrimination.ToString().clearLastZero()
                            , item.ErrorRate.ToString().clearLastZero() == null ? "0.00" : item.ErrorRate.ToString().clearLastZero()
                            , item.ResourceToResourceFolder_Id, item.TestQuestions_Score_ID);
                    }
                    return TempDate;
                }
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}