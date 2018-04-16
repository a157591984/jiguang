using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using Rc.Cloud.Model;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class CloudResUserEmpower : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Module_Id = "10200400";
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "选择学科");
                //职务
                strWhere = " D_Type='15' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlUserTitle, dt, "D_Name", "Common_Dict_ID", "选择职务");
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetUserList(string Subjectid, string UserTitle, string UserName, string ClassId, string GradeId, string School, int PageSize, int PageIndex)
        {
            try
            {
                Subjectid = Subjectid.Filter();
                UserTitle = UserTitle.Filter();
                UserName = UserName.Filter();
                ClassId = ClassId.Trim().Filter();
                GradeId = GradeId.Trim().Filter();
                School = School.Trim().Filter();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                string strWhere1 = string.Empty;
                //                if (!string.IsNullOrEmpty(Schoolid) && Schoolid != "-1")
                //                {
                //                    strWhere += string.Format(@" and t.UserId in
                //(
                //select UserId from VW_UserOnClassGradeSchool vus where vus.schoolid='{0}'
                //) ", Schoolid);
                //                }
                if (!string.IsNullOrEmpty(Subjectid) && Subjectid != "-1")
                {
                    strWhere += " and t.Subject='" + Subjectid + "'";
                }

                if (!string.IsNullOrEmpty(UserTitle) && UserTitle != "-1")
                {
                    strWhere += " and t.UserPost='" + UserTitle + "'";
                }
                if (!string.IsNullOrEmpty(UserName))
                {
                    strWhere += " and ( t.UserName like '%" + UserName + "%' or t.TrueName like '%" + UserName + "%')";
                }
                if (!string.IsNullOrEmpty(ClassId))
                {
                    strWhere1 += string.Format(@" inner join [dbo].[VW_UserOnClassGradeSchool] vwC on vwC.userId=t.userid   and (vwC.ClassId like '%{0}%' or vwC.ClassName like '%{0}%')", ClassId);
                }
                if (!string.IsNullOrEmpty(GradeId))
                {
                    strWhere1 += string.Format(@" inner join [dbo].[VW_UserOnClassGradeSchool] vwG on vwG.userId=t.userid  and (vwG.GradeId like '%{0}%' or vwG.GradeName like '%{0}%')", GradeId);
                }
                if (!string.IsNullOrEmpty(School))
                {
                    strWhere1 += string.Format(@" inner join [dbo].[VW_UserOnClassGradeSchool] vwS on vwS.userId=t.userid  and (vwS.SchoolId like '%{0}%' or vwS.SchoolName like '%{0}%')", School);
                }
                strSqlCount = @"select count(*)  from (
select distinct t.userid,
t.Subject,t.UserPost,t.TrueName,t.UserName,t.Mobile,t.Email,t2.D_Name SchoolName,t3.D_Name SubjectName,t4.D_Name UserTitleName from f_user t
left join Common_Dict t2 on t.School=t2.Common_Dict_ID
left join Common_Dict t3 on t.Subject=t3.Common_Dict_ID
left join  Common_Dict t4 on t.UserPost=t4.Common_Dict_ID " + strWhere1 + " where t.UserIdentity ='T' " + strWhere + ") a ";

                strSql = @"select * from (select *, ROW_NUMBER() over(ORDER BY a.UserName) row from (
select distinct t.userid,
t.Subject,t.UserPost,t.TrueName,t.UserName,t.Mobile,t.Email,t2.D_Name SchoolName,t3.D_Name SubjectName,t4.D_Name UserTitleName from f_user t
left join Common_Dict t2 on t.School=t2.Common_Dict_ID
left join Common_Dict t3 on t.Subject=t3.Common_Dict_ID
left join  Common_Dict t4 on t.UserPost=t4.Common_Dict_ID
 " + strWhere1 + " where t.UserIdentity ='T'" + strWhere + ") a )s where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        UserName = dt.Rows[i]["UserName"].ToString(),
                        UserId = dt.Rows[i]["UserId"].ToString(),
                        docName = dt.Rows[i]["UserName"].ToString(),
                        TrueName = dt.Rows[i]["TrueName"].ToString(),
                        SchoolName = dt.Rows[i]["SchoolName"].ToString(),
                        SubjectName = dt.Rows[i]["SubjectName"].ToString(),
                        UserTitleName = dt.Rows[i]["UserTitleName"].ToString()
                    });
                }

                if (inum > 0)
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
                    err = "error"//ex.Message.ToString()
                });
            }
        }




    }
}