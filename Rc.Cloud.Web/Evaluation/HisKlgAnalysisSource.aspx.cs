using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using System.Data;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.Evaluation
{
    public partial class HisKlgAnalysisSource : Rc.Cloud.Web.Common.FInitData
    {
        protected string UserId = string.Empty;//用户id
        protected string SubjectID = string.Empty;//科目id
        protected string ClassID = string.Empty;//班级id
        protected string KPName = string.Empty;//知识点名称
        protected string KPNameEncode = string.Empty;//知识点名称
        protected string DateData = string.Empty;//时间段
        protected string DateType = string.Empty;//时间类型
        protected void Page_Load(object sender, EventArgs e)
        {
            SubjectID = Request.QueryString["SubjectID"].ToString().Filter();
            ClassID = Request.QueryString["ClassID"].ToString().Filter();
            KPNameEncode = Request.QueryString["KPName"].ToString().Filter();
            KPName = Rc.Common.DBUtility.DESEncrypt.Decrypt(Request.QueryString["KPName"].ToString());
            DateData = Request.QueryString["DateData"].ToString().Filter();
            DateType = Request.QueryString["DateType"].ToString().Filter();
            UserId = Request["TeacherId"].Filter();
            if (string.IsNullOrEmpty(UserId)) UserId = FloginUser.UserId;
            if (string.IsNullOrEmpty(ClassID)) tdHWTime.Visible = false;
        }

        /// <summary>
        /// 获得作业列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="SubjectID"></param>
        /// <param name="ClassID"></param>
        /// <param name="KPName"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetHWList(string TeacherID, string SubjectID, string ClassID, string KPName, string DateData, string DateType)
        {
            try
            {
                TeacherID = TeacherID.Filter();
                SubjectID = SubjectID.Filter();
                ClassID = ClassID.Filter();
                KPName = KPName = Rc.Common.DBUtility.DESEncrypt.Decrypt(KPName);
                DateData = DateData.Filter();
                DateType = DateType.Filter();
                string strWhere = "TeacherID ='" + TeacherID + "'";
                if (!string.IsNullOrEmpty(SubjectID))
                {
                    strWhere += " AND SubjectID ='" + SubjectID + "'";
                }
                if (!string.IsNullOrEmpty(ClassID))
                {
                    strWhere += " AND ClassID ='" + ClassID + "'";
                }
                if (!string.IsNullOrEmpty(KPName))
                {
                    strWhere += " AND KPName ='" + KPName + "'";
                }
                if (!string.IsNullOrEmpty(DateData))
                {
                    switch (DateType)
                    {
                        case "month":
                            strWhere += " AND CONVERT(varchar(7),HomeWorkCreateTime,120)='" + DateData + "'";
                            break;
                        case "halfyear":
                            strWhere += " AND dbo.[f_GetDatePartHalfYear](CONVERT(varchar(10),HomeWorkCreateTime,120))='" + DateData + "'";
                            break;
                        case "quarter":
                            strWhere += " AND dbo.[f_GetDatePartQuarter](CONVERT(varchar(10),HomeWorkCreateTime,120))='" + DateData + "'";
                            break;
                    }

                }
                DataTable dt = new DataTable();
                if (string.IsNullOrEmpty(ClassID))//从老师表读取数据
                {
                    dt = new BLL_StatsTeacherHW_KP().GetList(strWhere + " ORDER BY Resource_Name DESC").Tables[0];
                }
                else
                {
                    dt = new BLL_StatsClassHW_KP().GetList(strWhere + " ORDER BY HomeWorkCreateTime DESC").Tables[0];
                }
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                string strxAxis = string.Empty;
                string strSeries = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dtTQ_S = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + dt.Rows[i]["ResourceToResourceFolder_Id"] + "' and ContentText='" + KPName + "' order by TestQuestions_Num,TestQuestions_OrderNum ").Tables[0];

                    string TQNums = string.Empty;
                    string TQStr = dt.Rows[i]["TestQuestionNumStrs"].ToString();
                    string TQNumsStr = dt.Rows[i]["TestQuestionNums"].ToString();
                    if (!string.IsNullOrEmpty(TQStr))
                    {
                        string[] strArr = TQStr.Split(',');
                        string[] strNumArr = TQNumsStr.Split(',');
                        for (int j = 0; j < strArr.Length; j++)
                        {
                            string testIndex = string.Empty;
                            if (dtTQ_S.Rows.Count > 0 && (dtTQ_S.Rows[j]["TestType"].ToString() == "clozeTest" || dtTQ_S.Rows[j]["TestType"].ToString() == "fill"))
                            {
                                testIndex = string.IsNullOrEmpty(dtTQ_S.Rows[j]["testIndex"].ToString()) ? "" : "-" + dtTQ_S.Rows[j]["testIndex"].ToString();
                            }
                            TQNums += string.Format("<a href='../teacher/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id={0}#{2}' target=\"_blank\">第{1}题", dt.Rows[i]["ResourceToResourceFolder_Id"].ToString()
                            , strArr[j].TrimEnd('.') + testIndex
                            , strNumArr[j].TrimEnd('.'));
                        }
                    }
                    strxAxis += ((!string.IsNullOrEmpty(dt.Rows[i]["Resource_Name"].ToString())) ? dt.Rows[i]["Resource_Name"].ToString().ReplaceForFilter() : "-") + ",";
                    strSeries += dt.Rows[i]["KPScoreAvgRate"].ToString() + ",";
                    listReturn.Add(new
                    {
                        HomeWork_Name = dt.Rows[i]["Resource_Name"].ToString().ReplaceForFilter(),
                        HomeWorkCreateTime = string.IsNullOrEmpty(ClassID) ? pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString(), "yyyy-MM-dd HH:mm") : pfunction.ConvertToLongDateTime(dt.Rows[i]["HomeWorkCreateTime"].ToString(), "yyyy-MM-dd HH:mm"),
                        KPScoreSum = dt.Rows[i]["KPScoreSum"].ToString().clearLastZero(),
                        KPScoreAvgRate = (!string.IsNullOrEmpty(dt.Rows[i]["KPScoreAvgRate"].ToString())) ? dt.Rows[i]["KPScoreAvgRate"].ToString().clearLastZero() + "%" : "-",
                        TestQuestionNums = TQNums,
                        ClassID = string.IsNullOrEmpty(ClassID) ? "" : ClassID
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        list = listReturn,
                        strxAxisCategories = strxAxis.TrimEnd(','),
                        strSeriesData = strSeries.TrimEnd(','),
                        DataCount = dt.Rows.Count
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "暂无数据"
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
        }
    }
}