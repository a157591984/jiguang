using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using System.Web.Services;
using System.Text;
using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.student
{
    public partial class DataAnalysisReportMultipleJobError : Rc.Cloud.Web.Common.FInitData
    {
        protected string StudentId = string.Empty;
        protected string Subject = string.Empty;
        protected string BookType = string.Empty;
        protected string ParentId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            StudentId = FloginUser.UserId;
            Subject = Request["Subject"].Filter();
            BookType = Request["BookType"].Filter();
            ParentId = Request["ParentId"].Filter();
            if (!IsPostBack)
            {

            }
        }

        [WebMethod]
        public static string GetDataList(string StuId, string Subject, string BookType, string ParentId, int PageIndex, int PageSize)
        {
            try
            {
                BLL_StatsStuHW_Analysis_KP bll = new BLL_StatsStuHW_Analysis_KP();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " Student_Id='" + StuId.Filter() + "' and TQCount_Wrong>0 ";
                if (!string.IsNullOrEmpty(Subject)) strWhere += " and Subject='" + Subject.Filter() + "' ";
                if (!string.IsNullOrEmpty(BookType)) strWhere += " and Book_Type='" + BookType.Filter() + "' ";
                if (!string.IsNullOrEmpty(ParentId)) strWhere += " and Parent_Id='" + ParentId.Filter() + "' ";

                dt = bll.GetListByPage(strWhere, "KPNameBasic", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount(strWhere);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strKPImportant = string.Empty;
                    int intKPImportant = 0;
                    int.TryParse(dt.Rows[i]["KPImportant"].ToString(), out intKPImportant);
                    for (int ii = 0; ii < intKPImportant; ii++)
                    {
                        strKPImportant += "★";
                    }
                    string strTQMastery_No = string.Empty;
                    if (Convert.ToDecimal(dt.Rows[i]["TQCount_Wrong"]) == 0)
                    {
                        strTQMastery_No = "100";
                    }
                    else if ((Convert.ToDecimal(dt.Rows[i]["TQCount_Right"]) + Convert.ToDecimal(dt.Rows[i]["TQCount_Wrong"])) == 0)
                    {
                        strTQMastery_No = "0";
                    }
                    else
                    {
                        strTQMastery_No = (100 * Convert.ToDecimal(dt.Rows[i]["TQCount_Wrong"]) / (Convert.ToDecimal(dt.Rows[i]["TQCount_Right"]) + Convert.ToDecimal(dt.Rows[i]["TQCount_Wrong"]))).ToString().clearLastZero();
                    }
                    listReturn.Add(new
                    {
                        Student_Id = StuId,
                        S_KnowledgePoint_Id = dt.Rows[i]["S_KnowledgePoint_Id"].ToString(),
                        KPNameBasic = dt.Rows[i]["KPNameBasic"].ToString(),
                        KPNameBasic_En = HttpContext.Current.Server.UrlEncode(dt.Rows[i]["KPNameBasic"].ToString()),
                        KPImportant = strKPImportant,
                        GKScore = dt.Rows[i]["GKScore"].ToString().clearLastZero(),
                        ComplexityText = dt.Rows[i]["ComplexityText"].ToString(),
                        HWCount = dt.Rows[i]["HWCount"].ToString(),
                        TQCount_Wrong = dt.Rows[i]["TQCount_Wrong"].ToString(),
                        TQMastery_No = dt.Rows[i]["TQMastery_No"].ToString().clearLastZero()
                    });
                }

                string strInfo = string.Empty;
                DataTable dtInfo = new BLL_StatsStuHW_Analysis_KP_Info().GetList("Student_Id='" + StuId.Filter() + "' and Parent_Id='" + ParentId.Filter() + "'").Tables[0];
                if (dtInfo.Rows.Count == 1)
                {
                    strInfo = string.Format("从{0}至{1}，同学完成作业为{2}道，其中答对{3}道，答错{4}道，整体掌握情况为{5}%。"
                        , pfunction.ConvertToLongDateTime(dtInfo.Rows[0]["StartDate"].ToString(), "yyyy年MM月dd日")
                        , pfunction.ConvertToLongDateTime(dtInfo.Rows[0]["EndDate"].ToString(), "yyyy年MM月dd日")
                        , dtInfo.Rows[0]["TQCount"].ToString()
                        , dtInfo.Rows[0]["TQCount_Right"].ToString()
                        , dtInfo.Rows[0]["TQCount_Wrong"].ToString()
                        , dtInfo.Rows[0]["TQMastery"].ToString().clearLastZero());
                    if (dtInfo.Rows[0]["PCT70"].ToString() != "0" || dtInfo.Rows[0]["PCT90"].ToString() != "0")
                    {
                        if (dtInfo.Rows[0]["PCT70"].ToString() != "0" || dtInfo.Rows[0]["PCT90"].ToString() != "0")
                        {
                            strInfo += string.Format("从作业反馈数据来看：掌握程度在70%以下的有{0}个知识点，掌握程度在70-90%的有{1}个知识点。"
                            , dtInfo.Rows[0]["PCT70"].ToString()
                            , dtInfo.Rows[0]["PCT90"].ToString());

                        }

                    }
                    //if (dtInfo.Rows[0]["GKCount"].ToString() != "0" || dtInfo.Rows[0]["GKScore"].ToString() != "0")
                    //{
                    //    strInfo += string.Format("从没掌握的知识点来看：这些知识点在中考中必考点为{0}个，如不全部掌握将在中考中丢失约{1}分，建议全部掌握。"
                    //        , dtInfo.Rows[0]["GKCount"].ToString()
                    //        , dtInfo.Rows[0]["GKScore"].ToString().clearLastZero());
                    //}
                }

                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = rCount,
                        Info = strInfo,
                        list = listReturn
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