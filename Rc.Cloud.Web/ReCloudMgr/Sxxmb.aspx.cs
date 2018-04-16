using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Model;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class Sxxmb : Rc.Cloud.Web.Common.InitPage
    {
        protected string strGradeTerm = string.Empty;//年级学期
        protected string strSubject = string.Empty;//学科
        protected string strResource_Version = string.Empty;//教材版本
        public string TestPaper_Frame_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "30100400";
            TestPaper_Frame_Id = Request["TestPaper_Frame_Id"].Filter();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;

                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "教材版本");
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID", "年级学期");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "学科");

            }
        }

        /// <summary>
        /// 获取书本目录
        /// </summary>
        /// <param name="DocName"></param>
        /// <param name="strResource_Type"></param>
        /// <param name="strResource_Class"></param>
        /// <param name="strGradeTerm"></param>
        /// <param name="strSubject"></param>
        /// <param name="strResource_Version"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetSxxmbList(string TestPaper_Frame_Id, string Two_WayChecklist_Name, string strGradeTerm, string strSubject, string strResource_Version, int PageIndex, int PageSize)
        {
            try
            {
                Two_WayChecklist_Name = Two_WayChecklist_Name.Filter();
                strGradeTerm = strGradeTerm.Filter();
                strSubject = strSubject.Filter();
                strResource_Version = strResource_Version.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                #region 双向细目表
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string StrWhere = " where Two_WayChecklistType='0' and  ParentId='" + TestPaper_Frame_Id.Filter() + "' ";
                if (!string.IsNullOrEmpty(Two_WayChecklist_Name))
                {
                    StrWhere += " and Two_WayChecklist_Name like '%" + Two_WayChecklist_Name.TrimEnd() + "%'";
                }

                if (!string.IsNullOrEmpty(strGradeTerm) && strGradeTerm != "-1")
                {
                    StrWhere += " and GradeTerm='" + strGradeTerm + "'";
                }
                if (!string.IsNullOrEmpty(strSubject) && strSubject != "-1")
                {
                    StrWhere += " and Subject='" + strSubject + "'";
                }
                if (!string.IsNullOrEmpty(strResource_Version) && strResource_Version != "-1")
                {
                    StrWhere += " and Resource_Version='" + strResource_Version + "'";
                }

                strSqlCount = @"select count(*) from Two_WayChecklist" + StrWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.CreateTime DESC) row,A.* ,cdG.D_Name GradeTermName,cdr.D_Name Resource_VersionName,cdS.D_Name SubjectName,u.SysUser_Name,CountQuestions=(select count(*) from Two_WayChecklistDetail where ParentId<>'0' and Two_WayChecklist_Id=A.Two_WayChecklist_Id),CountAuth=(select count(*) from Two_WayChecklistAuth where Two_WayChecklist_Id=A.Two_WayChecklist_Id) from dbo.Two_WayChecklist A
left join dbo.Common_Dict cdG on cdg.Common_Dict_ID=A.GradeTerm
left join dbo.Common_Dict cdR on cdr.Common_Dict_ID=A.Resource_Version
left join dbo.Common_Dict cdS on cds.Common_Dict_ID=A.Subject
left join dbo.SysUser u on u.SysUser_ID=A.CreateUser"
                    + StrWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        Two_WayChecklist_Name = dt.Rows[i]["Two_WayChecklist_Name"].ToString(),
                        CreateTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                        GradeTermName = dt.Rows[i]["GradeTermName"].ToString(),
                        Resource_VersionName = dt.Rows[i]["Resource_VersionName"].ToString(),
                        SubjectName = dt.Rows[i]["SubjectName"].ToString(),
                        Two_WayChecklist_Id = dt.Rows[i]["Two_WayChecklist_Id"].ToString(),
                        SysUser_Name = dt.Rows[i]["SysUser_Name"],
                        Two_WayChecklist_NameUrl = HttpContext.Current.Server.UrlEncode(dt.Rows[i]["Two_WayChecklist_Name"].ToString()),
                        CountQuestions = dt.Rows[i]["CountQuestions"].ToString(),
                        Year = dt.Rows[i]["ParticularYear"].ToString(),
                        GradeTerm = dt.Rows[i]["GradeTerm"].ToString(),
                        Resource_Version = dt.Rows[i]["Resource_Version"].ToString(),
                        Subject = dt.Rows[i]["Subject"].ToString(),
                        CountAuth = dt.Rows[i]["CountAuth"].ToString()
                    });
                }
                #endregion

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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }

        [WebMethod]
        public static string DeleteData(string Two_WayChecklist_Id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Two_WayChecklist_Id.Filter()))
                {
                    string strSql = string.Format(@"delete from Two_WayChecklist where Two_WayChecklist_Id='{0}'; 
delete from Two_WayChecklistDetail where Two_WayChecklist_Id='{0}';
delete from Two_WayChecklistDetailToTestQuestions where Two_WayChecklist_Id='{0}'; 
delete from Two_WayChecklistDetailToAttr where Two_WayChecklist_Id='{0}';", Two_WayChecklist_Id);
                    int i = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql);
                    if (i > 0)
                    {
                        return "1";
                    }
                    else
                    {
                        return "";
                    }

                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}