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
    public partial class RemedialPlan : Rc.Cloud.Web.Common.FInitData
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

                //dt = bll.GetListByPage(strWhere, "KPNameBasic", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                dt = bll.GetList(strWhere + " order by KPNameBasic ").Tables[0];
                int rCount = bll.GetRecordCount(strWhere);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        Student_Id = StuId,
                        S_KnowledgePoint_Id = dt.Rows[i]["S_KnowledgePoint_Id"].ToString(),
                        KPNameBasic = dt.Rows[i]["KPNameBasic"].ToString(),
                        KPNameBasic_En = HttpContext.Current.Server.UrlEncode(dt.Rows[i]["KPNameBasic"].ToString()),
                        TQCount_Wrong = dt.Rows[i]["TQCount_Wrong"].ToString()
                    });
                }

                string strInfo = string.Empty;
                DataTable dtInfo = new BLL_StatsStuHW_Analysis_KP_Info().GetList("Student_Id='" + StuId.Filter() + "' and Parent_Id='" + ParentId.Filter() + "'").Tables[0];
                if (dtInfo.Rows.Count == 1)
                {
                    strInfo = string.Format(@"截止到{0}， 同学共完成{1}道作业题目，其中错误题目为{2}道，"
                        , pfunction.ConvertToLongDateTime(dtInfo.Rows[0]["EndDate"].ToString(), "yyyy年MM月dd日")
                        , dtInfo.Rows[0]["TQCount"].ToString()
                        , dtInfo.Rows[0]["TQCount_Wrong"].ToString()
                        , dtInfo.Rows[0]["TQMastery"].ToString().clearLastZero());
                    if (dtInfo.Rows[0]["KPCount_Wrong"].ToString() != "0" || dtInfo.Rows[0]["KP_Wrong_Text"].ToString() != "0")
                    {
                        strInfo += string.Format(@"共涉及到知识点{0}个：{1}。请认知将以上知识点进行补救学习或请老师指导学习。"
                        , dtInfo.Rows[0]["KPCount_Wrong"].ToString()
                        , dtInfo.Rows[0]["KP_Wrong_Text"].ToString());
                    }

                }

                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
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