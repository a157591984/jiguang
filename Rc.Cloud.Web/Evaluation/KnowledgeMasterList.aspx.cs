using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Common.StrUtility;
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
namespace Rc.Cloud.Web.Evaluation
{
    public partial class KnowledgeMasterList : Rc.Cloud.Web.Common.FInitData
    {
        public string ResourceToResourceFolder_Id = string.Empty;
        public string UserId = string.Empty;
        public string StudentNum = string.Empty;
        public string HomeWork_ID = string.Empty;
        public string ClassID = string.Empty;
        public string TeacherID = string.Empty;
        public string ClassName = string.Empty;
        public string StrTemp = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassID = Request.QueryString["ClassID"].Filter();
            TeacherID = Request.QueryString["TeacherID"].Filter();
            ClassName = Request.QueryString["ClassName"].Filter();
            UserId = FloginUser.UserId;
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            HomeWork_ID = Request.QueryString["HomeWork_ID"].Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(TeacherID))
                {
                    StrTemp = "&ClassID=" + ClassID + "&ClassName=" + ClassName + "&TeacherID=" + TeacherID;
                    UserId = TeacherID;
                }
                StudentNum = "[ {value: 10,name: '优秀'},{value: 20,name: '良好'},{value: 30,name: '中等'},{value: 40,name: '及格'},{value: 50,name: '不及格'}]";
                if (!string.IsNullOrEmpty(ResourceToResourceFolder_Id))
                {
                    ltlClasses.Text = StatsCommonHandle.GetTeacherClass(UserId, ClassID, ClassName,ResourceToResourceFolder_Id);//有效班级
                    if (string.IsNullOrEmpty(ClassID))
                    {
                        ClassID = "-1";
                    }
                }
            }
        }
        /// <summary>
        ///知识点掌握情况
        /// </summary>
        /// <param name="Class"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="UserId"></param>
        /// <param name="KPName"></param>
        /// <param name="KPScoreAvgRate"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStatsClassHW_KP(string Class, string ResourceToResourceFolder_Id, string UserId, string KPName, string KPScoreAvgRate)
        {
            try
            {
                Class = Class.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                UserId = UserId.Filter();
                KPName = KPName.Filter();
                KPScoreAvgRate = KPScoreAvgRate.Filter();
                string TempDate = string.Empty;
                string StrWhere = string.Empty;
                string ClassName = string.Empty;
                if (!string.IsNullOrEmpty(KPName))
                {
                    StrWhere += " and KPName like '%" + KPName.TrimEnd() + "%'";
                }
                if (!string.IsNullOrEmpty(KPScoreAvgRate))
                {
                    StrWhere += " and KPScoreAvgRate<" + KPScoreAvgRate.TrimEnd() + "";
                }
                // string Temp = "<tr><td>数列的极限</td><td>20</td><td>90%</td><td><a href="##">第1题</a><a href="##">第3题</a></td></tr>";
                string Temp = "<tr><td>{0}</td><td>{1}</td><td>{2}%</td><td>{3}</td><td><a href='{4}' target='_blank'>查看</a></td></tr>";
                string KPNames = string.Empty;
                string KPScoreAvgRates = string.Empty;
                //name: '一班',data: [83, 78, 98, 93, 56, 84, 65, 104, 91, 83]
                DataTable dtTQ_S = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num,TestQuestions_OrderNum ").Tables[0];
                
                if (Class == "-1")
                {
                    HttpContext.Current.Session["StatsClassClassId"] = "-1";
                    BLL_StatsTeacherHW_KP bllTeacherHW_KP = new BLL_StatsTeacherHW_KP();
                    List<Model_StatsTeacherHW_KP> TeacherHW_KPList = new List<Model_StatsTeacherHW_KP>();

                    TeacherHW_KPList = bllTeacherHW_KP.GetModelList("TeacherID='" + UserId + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'" + StrWhere + " order by TestQuestionNums  ");
                    foreach (var item in TeacherHW_KPList)
                    {
                        TempDate += string.Format(Temp
                            , item.KPName
                            , item.KPScoreSum.ToString().clearLastZero()
                            , item.KPScoreAvgRate.ToString().clearLastZero()
                            , GetTQNum(dtTQ_S, item.KPName, item.ResourceToResourceFolder_Id, item.TestQuestionNumStrs, item.TestQuestionNums)
                            , "HisKlgAnalysisSource.aspx?SubjectID=" + item.SubjectID + "&ClassID=&KPName=" + Rc.Common.DBUtility.DESEncrypt.Encrypt(item.KPName.ToString()) + "&DateData=&DateType=month&TeacherId=" + item.TeacherID
                            );
                        KPScoreAvgRates += item.KPScoreAvgRate.ToString().clearLastZero() + ",";
                        KPNames += item.KPName + ",";
                    }
                    if (TempDate == "")
                    {
                        return "";
                    }
                    else
                    {
                        return TempDate + "|全部班级|" + KPScoreAvgRates.TrimEnd(',') + "|" + KPNames.TrimEnd(',');
                    }
                }
                else
                {
                    HttpContext.Current.Session["StatsClassClassId"] = Class;
                    BLL_StatsClassHW_KP bllClassHW_KP = new BLL_StatsClassHW_KP();
                    List<Model_StatsClassHW_KP> ClassHW_KPList = new List<Model_StatsClassHW_KP>();
                    ClassHW_KPList = bllClassHW_KP.GetModelList("ClassID='" + Class + "' and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' " + StrWhere + " order by TestQuestionNums ");
                    foreach (var item in ClassHW_KPList)
                    {
                        TempDate += string.Format(Temp
                            , item.KPName
                            , item.KPScoreSum.ToString().clearLastZero()
                            , item.KPScoreAvgRate.ToString().clearLastZero()
                            , GetTQNum(dtTQ_S, item.KPName, item.ResourceToResourceFolder_Id, item.TestQuestionNumStrs, item.TestQuestionNums)
                            , "HisKlgAnalysisSource.aspx?SubjectID=" + item.SubjectID + "&ClassID=" + item.ClassID + "&KPName=" + Rc.Common.DBUtility.DESEncrypt.Encrypt(item.KPName.ToString()) + "&DateData=" + pfunction.ConvertToLongDateTime(item.HomeWorkCreateTime.ToString(), "yyyy-MM") + "&DateType=month&TeacherId=" + item.TeacherID
                            );
                        KPScoreAvgRates += item.KPScoreAvgRate.ToString().clearLastZero() + ",";
                        KPNames += item.KPName + ",";
                        ClassName = item.ClassName;
                    }
                    if (TempDate == "")
                    {
                        return "";
                    }
                    else
                    {
                        return TempDate + "|" + ClassName + "|" + KPScoreAvgRates.TrimEnd(',') + "|" + KPNames.TrimEnd(',');
                    }
                }
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
        public static string GetTQNum(DataTable dtTQ_S, string KPName, string ResourceToResourceFolder_Id, string TQNumArr, string TestQuestionNums)
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
                        Temp += string.Format("<a href='../teacher/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id={0}#{2}' target=\"_blank\">第{1}题", ResourceToResourceFolder_Id
                            , TQNum[i].TrimEnd('.') + testIndex
                            , TQNumS[i].TrimEnd('.'));

                    }
                    return Temp;
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
