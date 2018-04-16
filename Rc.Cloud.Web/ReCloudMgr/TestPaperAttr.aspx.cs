using Rc.Cloud.Model;
using Rc.Cloud.Web.Common;
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
using Newtonsoft.Json;
using Rc.Cloud.BLL;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Common.Config;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class TestPaperAttr : Rc.Cloud.Web.Common.InitPage
    {
        protected string strResource_Type = string.Empty;//资源类型（class类型文件、testPaper类型文件、ScienceWord类型文件）
        protected string strResource_Class = string.Empty;//资源类别（云资源、自有资源）
        protected string strGradeTerm = string.Empty;//年级学期
        protected string strSubject = string.Empty;//学科
        protected string strResource_Version = string.Empty;//教材版本
        public string _t = string.Empty;
        protected string s = string.Empty;
        StringBuilder strHtml = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "30400100";

            strResource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
            //strResource_Type = ddlResource_Type.SelectedValue;
            strGradeTerm = ddlGradeTerm.SelectedValue;
            strSubject = ddlSubject.SelectedValue;
            strResource_Version = ddlResource_Version.SelectedValue;
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "请选择");
                ////文档类型
                //strWhere = string.Format(" D_Type='1' AND Common_Dict_ID!='{0}' order by d_order", Rc.Common.Config.Resource_TypeConst.按属性生成的目录);
                //dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                //Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Type, dt, "D_Name", "Common_Dict_ID", "请选择");
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID", "请选择");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "请选择");

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
        public static string GetCloudBooks(string DocName, string strResource_Class, string strGradeTerm, string strSubject, string strResource_Version, int PageIndex, int PageSize)
        {
            try
            {
                DocName = DocName.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(DocName)) strWhere += " and ResourceFolder_Name like '%" + DocName.Filter() + "%' ";
                if (strResource_Class != "-1") strWhere += " and Resource_Class = '" + strResource_Class.Filter() + "' ";
                strWhere += " and Resource_Type = '" + Resource_TypeConst.testPaper类型文件 + "' ";
                if (strGradeTerm != "-1") strWhere += " and GradeTerm = '" + strGradeTerm.Filter() + "' ";
                if (strSubject != "-1") strWhere += " and Subject = '" + strSubject.Filter() + "' ";
                if (strResource_Version != "-1") strWhere += " and Resource_Version = '" + strResource_Version.Filter() + "' ";

                strWhere += " AND ResourceFolder_Level=5";//管理员维护的书籍目录
                strSqlCount = @"select count(1) from ResourceFolder A
left join BookAudit bka on bka.ResourceFolder_Id=A.ResourceFolder_Id   
where 1=1 and  bka.AuditState='1' " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.CreateTime DESC) row,A.*,bka.AuditState
,t1.D_Name as GradeTermName,t2.D_Name as SubjectName,t3.D_Name as Resource_VersionName,t4.D_Name as Resource_TypeName
from ResourceFolder A
left join BookAudit bka on bka.ResourceFolder_Id=A.ResourceFolder_Id     
left join Common_Dict t1 on t1.Common_Dict_Id=A.GradeTerm
left join Common_Dict t2 on t2.Common_Dict_Id=A.Subject
left join Common_Dict t3 on t3.Common_Dict_Id=A.Resource_Version
left join Common_Dict t4 on t4.Common_Dict_Id=A.Resource_Type
where 1=1 and bka.AuditState='1'" + strWhere + " ) z where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string docName = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter();

                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceFolder_Id"].ToString(),
                        GradeTerm = dtRes.Rows[i]["GradeTermName"].ToString(),
                        Subject = dtRes.Rows[i]["SubjectName"].ToString(),
                        Resource_Version = dtRes.Rows[i]["Resource_VersionName"].ToString(),
                        Resource_Type = dtRes.Rows[i]["Resource_TypeName"].ToString(),
                        docName = docName,
                        docNameSub = pfunction.GetSubstring(docName, 30, true),
                        docTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString())
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
    }
}