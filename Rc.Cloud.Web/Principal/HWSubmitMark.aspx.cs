using Newtonsoft.Json;
using Rc.BLL.Resources;
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
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.Principal
{
    public partial class HWSubmitMark : Rc.Cloud.Web.Common.FInitData
    {
        protected string UserId = string.Empty;
        public string GradeId = string.Empty;
        public string GradeName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            GradeName = Request.QueryString["GradeName"].Filter();
            UserId = FloginUser.UserId;
            if (!IsPostBack)
            {
                ltlGradeName.Text = Server.UrlDecode(GradeName);
                //GetGrade();
                GetClass();
                GetSubjec();
            }
        }

        /// <summary>
        /// 学科
        /// </summary>
        public void GetSubjec()
        {
            ltlSubject.Text = StatsCommonHandle.GetTeacherSubjectData();

        }
        /// <summary>
        /// 获得当前用户所教班级
        /// </summary>
        protected void GetClass()
        {
            ltlClass.Text = StatsCommonHandle.GetGradeAllClass(GradeId);
        }
        ///// <summary>
        ///// 获得当前用户所教年级
        ///// </summary>
        //protected void GetGrade()
        //{
        //    StringBuilder strGrade = new StringBuilder();
        //    BLL_UserGroup bll_user_group = new BLL_UserGroup();
        //    DataTable dt = bll_user_group.GetList("UserGroup_AttrEnum='Grade'").Tables[0];
        //    strGrade.Append("<li><a href='##' class='active' ajax-value=''>全部</a></li>");
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        strGrade.AppendFormat("<li><a href='##' ajax-value='{0}'>{1}</a></li>",
        //            item["UserGroup_id"],
        //            item["UserGroup_Name"]
        //            );
        //    }
        //    ltlGrade.Text = strGrade.ToString();
        //}


        ///// <summary>
        ///// 获得当前用户所教学科
        ///// </summary>
        //protected void GetSubject()
        //{
        //    StringBuilder subjectStr = new StringBuilder();
        //    BLL_Common_Dict bll_common_dict = new BLL_Common_Dict();
        //    DataTable dt = bll_common_dict.GetList("D_Type = '7' ORDER BY D_CreateTime").Tables[0];
        //    subjectStr.Append("<li><a href='##' class='active' ajax-value=''>全部</a></li>");
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        subjectStr.AppendFormat("<li><a href='##' ajax-value='{0}'>{1}</a></li>",
        //            item["Common_Dict_ID"],
        //            item["D_Name"]
        //            );
        //    }
        //    ltlSubject.Text = subjectStr.ToString();
        //}

        ///// <summary>
        ///// 获得当前用户所教班级
        ///// </summary>
        //protected void GetClass()
        //{
        //    StringBuilder classStr = new StringBuilder();
        //    BLL_UserGroup_Member bll_user_group_member = new BLL_UserGroup_Member();
        //    DataTable dt = bll_user_group_member.GetAllClassList("UGM.MembershipEnum = '" + MembershipEnum.classrc + "' ORDER BY UG.UserGroupOrder").Tables[0];
        //    classStr.Append("<li><a href='##' class='active' ajax-value='' data-parentid=''>全部</a></li>");
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        classStr.AppendFormat("<li><a href='##' ajax-value='{0}' data-parentid='{2}'>{1}</a></li>",
        //            item["UserGroup_Id"],
        //            item["UserGroup_Name"],
        //            item["ParentID"]
        //            );
        //    }
        //    ltlClass.Text = classStr.ToString();
        //}


        /// <summary>
        /// 获得列表
        /// </summary>
        [WebMethod]
        public static string GetList(string GradeID, string SubjectID, string ClassID, string DateType, int PageSize, int PageIndex)
        {
            try
            {
                SubjectID = SubjectID.Filter();
                ClassID = ClassID.Filter();
                DateType = DateType.Filter();
                HttpContext.Current.Session["StatsGradeSubject"] = SubjectID;
                string strWhere = "1=1";
                if (!string.IsNullOrEmpty(GradeID))
                {
                    strWhere += " AND GradeID = '" + GradeID + "'";
                }
                if (!string.IsNullOrEmpty(SubjectID))
                {
                    strWhere += " AND SubjectID = '" + SubjectID + "'";
                }
                if (!string.IsNullOrEmpty(ClassID))
                {
                    strWhere += " AND ClassID = '" + ClassID + "'";
                }
                if (!string.IsNullOrEmpty(DateType))
                {
                    strWhere += " AND DateType = '" + DateType + "'";
                }
                DataTable dt = new DataTable();
                BLL_StatsClassHW_CorrectedInData bll = new BLL_StatsClassHW_CorrectedInData();
                dt = bll.GetListByPage(strWhere, " DateData DESC", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                int intRecordCount = bll.GetRecordCount(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        ClassID = dt.Rows[i]["ClassID"].ToString(),
                        ClassName = dt.Rows[i]["ClassName"].ToString(),
                        SubjectID = dt.Rows[i]["SubjectID"].ToString(),
                        AssignedCount = dt.Rows[i]["AssignedCount"].ToString(),
                        CommittedCount = dt.Rows[i]["CommittedCount"].ToString(),
                        CorrectedCount = dt.Rows[i]["CorrectedCount"].ToString(),
                        CommittedCountRate = dt.Rows[i]["CommittedCountRate"].ToString().clearLastZero() + "%",
                        CorrectedCountRate = dt.Rows[i]["CorrectedCountRate"].ToString().clearLastZero() + "%",
                        DateData = dt.Rows[i]["DateData"].ToString(),
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = intRecordCount,
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