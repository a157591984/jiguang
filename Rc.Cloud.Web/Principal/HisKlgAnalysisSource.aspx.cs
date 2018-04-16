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

namespace Rc.Cloud.Web.Principal
{
    public partial class HisKlgAnalysisSource : Rc.Cloud.Web.Common.FInitData
    {
        protected string UserId = string.Empty;//用户id
        protected string GradeId = string.Empty;//年级id
        protected string SubjectID = string.Empty;//科目id
        protected string ClassID = string.Empty;//班级id
        protected string KPName = string.Empty;//知识点名称
        protected string KPNameEncode = string.Empty;
        protected string DateData = string.Empty;//时间段
        protected string DateType = string.Empty;//时间类型
        public string GradeName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = FloginUser.UserId;
            SubjectID = Request.QueryString["SubjectID"].ToString().Filter();
            ClassID = Request.QueryString["ClassID"].ToString().Filter();
            KPNameEncode = Rc.Common.DBUtility.DESEncrypt.Decrypt(Request.QueryString["KPName"].ToString());
            KPName = Request.QueryString["KPName"].ToString();
            DateData = Request.QueryString["DateData"].ToString().Filter();
            DateType = Request.QueryString["DateType"].ToString().Filter();
            GradeName = Server.UrlDecode(Request.QueryString["GradeName"].Filter());
            GradeId = Request.QueryString["GradeId"].Filter();
            if (!IsPostBack)
            {

            }

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
        public static string GetHWList(string GradeId, string SubjectID, string ClassID, string KPName, string DateData, string DateType)
        {
            try
            {
                GradeId = GradeId.Filter();
                SubjectID = SubjectID.Filter();
                ClassID = ClassID.Filter();
                KPName = Rc.Common.DBUtility.DESEncrypt.Decrypt(KPName); ;
                DateData = DateData.Filter();
                DateType = DateType.Filter();
                string strWhere = "1=1";
                if (!string.IsNullOrEmpty(GradeId))
                {
                    strWhere += " AND Gradeid ='" + GradeId + "'";
                }
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
                strWhere += " ORDER BY HomeWorkCreateTime DESC";
                DataTable dt = new DataTable();
                BLL_StatsClassHW_KP bll = new BLL_StatsClassHW_KP();
                dt = bll.GetList(strWhere).Tables[0];
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                string strxAxis = string.Empty;
                string strSeries = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string TQNums = string.Empty;
                    string TQStr = dt.Rows[i]["TestQuestionNumStrs"].ToString().TrimEnd('.');
                    string TQNumsStr = dt.Rows[i]["TestQuestionNums"].ToString();
                    if (!string.IsNullOrEmpty(TQStr))
                    {
                        string[] strNumArr = TQNumsStr.Split(',');
                        string[] strArr = TQStr.Split(',');
                        for (int j = 0; j < strArr.Length; j++)
                        {
                            TQNums += string.Format("<a href='../teacher/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id={0}#{2}' target=\"_blank\">第{1}题</a>"
                                , dt.Rows[i]["ResourceToResourceFolder_Id"].ToString()
                             , strArr[j].TrimEnd('.')
                            , strNumArr[j].TrimEnd('.'));
                        }
                    }
                    strxAxis += ((!string.IsNullOrEmpty(dt.Rows[i]["HomeWork_Name"].ToString())) ? dt.Rows[i]["HomeWork_Name"].ToString() : "-") + ",";
                    strSeries += dt.Rows[i]["KPScoreAvgRate"].ToString() + ",";
                    listReturn.Add(new
                    {
                        HomeWork_Name = dt.Rows[i]["HomeWork_Name"].ToString(),
                        HomeWorkCreateTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["HomeWorkCreateTime"].ToString(), "yyyy-MM-dd HH:mm"),
                        KPScoreSum = dt.Rows[i]["KPScoreSum"].ToString().clearLastZero(),
                        KPScoreAvgRate = (!string.IsNullOrEmpty(dt.Rows[i]["KPScoreAvgRate"].ToString())) ? dt.Rows[i]["KPScoreAvgRate"].ToString().clearLastZero() + "%" : "-",
                        TestQuestionNums = TQNums,
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