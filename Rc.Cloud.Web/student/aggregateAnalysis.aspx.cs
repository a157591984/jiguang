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

namespace Rc.Cloud.Web.student
{
    public partial class aggregateAnalysis : Rc.Cloud.Web.Common.FInitData
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
        public static string GetDictList(string Type, string ParentId, string CheckedDicId, string StuId)
        {
            StringBuilder stbHtml = new StringBuilder();
            try
            {
                string strSql = string.Empty;
                if (Type == "1")
                {
                    strSql = string.Format(@"select t.[Subject] as value,t2.D_Name as text from StatsStuHW_Analysis_KP t
inner join Common_Dict t2 on t2.Common_Dict_Id=t.[Subject]
where t.Student_Id='{0}' group by t.[Subject],t2.D_Name ", StuId);
                }
                else
                {
                    strSql = string.Format(@"select t.Book_Type as value,t2.D_Name as text from StatsStuHW_Analysis_KP t
inner join Common_Dict t2 on t2.Common_Dict_Id=t.Book_Type
where t.Student_Id='{0}' and t.[Subject]='{1}' group by t.Book_Type,t2.D_Name ", StuId, ParentId);
                }
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int num = 0;
                foreach (DataRow item in dt.Rows)
                {
                    num++;
                    string strClass = string.Empty;
                    if (string.IsNullOrEmpty(CheckedDicId))
                    {
                        if (num == 1) strClass = "active";
                    }
                    else
                    {
                        if (item["value"].ToString() == CheckedDicId)
                        {
                            strClass = "active";
                        }
                    }
                    stbHtml.AppendFormat("<li><a href=\"##\" ajax-value=\"{0}\" class='{2}'>{1}</a></li>"
                        , item["value"].ToString()
                        , item["text"].ToString()
                        , strClass);
                }

                return stbHtml.ToString();

            }
            catch (Exception ex)
            {
                return stbHtml.ToString();
            }
        }
        [WebMethod]
        public static string GetKPList(string Subject, string BookType, string CheckedKPId, string StuId)
        {
            StringBuilder stbHtml = new StringBuilder();
            try
            {
                string strSql = string.Format(@"select t2.KPCode,t2.Parent_Id,t2.KPName,t2.S_KnowledgePoint_Id from StatsStuHW_Analysis_KP t
inner join S_KnowledgePoint t2 on t2.S_KnowledgePoint_Id=t.Parent_Id and t2.Subject='{0}' and t2.Book_Type='{1}'
where t.Student_Id='{2}' group by t2.KPCode,t2.Parent_Id,t2.KPName,t2.S_KnowledgePoint_Id order by t2.KPCode "
                    , Subject
                    , BookType
                    , StuId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                stbHtml.Append(InitKPTree(CheckedKPId, "0", 1, dt));
            }
            catch (Exception)
            {

            }
            return stbHtml.ToString();
        }

        protected static StringBuilder InitKPTree(string CheckedKPId, string ParentId, int level, DataTable dt)
        {
            StringBuilder strHtml = new StringBuilder();
            DataRow[] dr = dt.Select("Parent_Id='" + ParentId + "'");
            int num = 0;
            foreach (DataRow item in dr)
            {
                num++;
                string strClass = string.Empty;
                if (string.IsNullOrEmpty(CheckedKPId))
                {
                    if (num == 1 && level == 1) strClass = "active";
                }
                else
                {
                    if (item["S_KnowledgePoint_Id"].ToString() == CheckedKPId)
                    {
                        strClass = "active";
                    }
                }
                strHtml.AppendFormat("<ul>", level);
                strHtml.Append("<li>");
                strHtml.AppendFormat("<div class='mtree_link mtree-link-hook {0}' ajax-value='{1}'><div class='mtree_indent mtree-indent-hook'></div><div class='mtree_btn mtree-btn-hook'></div><div class='mtree_name mtree-name-hook'>{2}</div></div>"
                    , strClass
                    , item["S_KnowledgePoint_Id"]
                    , item["KPName"]);
                strHtml.Append(InitKPTree(CheckedKPId, item["S_KnowledgePoint_Id"].ToString(), level + 1, dt));
                strHtml.Append("</li>");
                strHtml.Append("</ul>");

            }
            return strHtml;
        }

        [WebMethod]
        public static string GetDataList(string StuId, string Subject, string BookType, string ParentId, int PageIndex, int PageSize)
        {
            try
            {
                BLL_StatsStuHW_Analysis_KP bll = new BLL_StatsStuHW_Analysis_KP();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " Student_Id='" + StuId.Filter() + "' ";
                if (!string.IsNullOrEmpty(Subject)) strWhere += " and Subject='" + Subject.Filter() + "' ";
                if (!string.IsNullOrEmpty(BookType)) strWhere += " and Book_Type='" + BookType.Filter() + "' ";
                if (!string.IsNullOrEmpty(ParentId)) strWhere += " and Parent_Id='" + ParentId.Filter() + "' ";

                dt = bll.GetListByPage(strWhere, "KPNameBasic", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount(strWhere);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        Student_Id = dt.Rows[i]["Student_Id"].ToString(),
                        S_KnowledgePoint_Id = dt.Rows[i]["S_KnowledgePoint_Id"].ToString(),
                        KPNameBasic = dt.Rows[i]["KPNameBasic"].ToString(),
                        KPNameBasic_En = HttpContext.Current.Server.UrlEncode(dt.Rows[i]["KPNameBasic"].ToString()),
                        HWCount = dt.Rows[i]["HWCount"].ToString(),
                        TQCount_Right = dt.Rows[i]["TQCount_Right"].ToString(),
                        TQCount_Wrong = dt.Rows[i]["TQCount_Wrong"].ToString(),
                        KPMastery = dt.Rows[i]["KPMastery"].ToString().clearLastZero()
                    });
                }

                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = rCount,
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