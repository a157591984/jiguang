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

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class ReCloudMgr : Rc.Cloud.Web.Common.InitPage
    {
        protected string strResource_Type = string.Empty;
        protected string strResource_Class = string.Empty;
        protected string t = string.Empty;
        protected string s = string.Empty;
        StringBuilder strHtml = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10101000";
            //
            if (!String.IsNullOrEmpty(Request["t"]))
            {
                t = Request["t"].ToString();
                if (Request["t"].ToString() == "1")
                {
                    Module_Id = "10101000";
                    strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                }
                else if (Request["t"].ToString() == "2")
                {
                    Module_Id = "10102000";
                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                }
                else if (Request["t"].ToString() == "3")
                {
                    Module_Id = "10103000";
                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                }
            }
            if (!String.IsNullOrEmpty(Request["s"]))
            {
                s = Request["s"].ToString();
                if (Request["s"].ToString() == "1")
                {
                    strResource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
                }
                else if (Request["s"].ToString() == "2")
                {
                    strResource_Class = Rc.Common.Config.Resource_ClassConst.自有资源;
                }
            }
            if (strResource_Class == string.Empty || strResource_Type == string.Empty)
            {
                Rc.Common.StrUtility.clsUtility.ErrorDispose(3, false);
            }
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID");
                //教案类型
                strWhere = " D_Type='5' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlLessonPlan_Type, dt, "D_Name", "Common_Dict_ID");
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID");
               // litTree.Text = GetTreeHtml();
            }
        }
        /// <summary>
        /// 资源属性
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetResourceAttribute()
        {
            StringBuilder sb = new StringBuilder();
            string strWhere = string.Empty;
            strWhere = string.Empty;
            if (ddlResource_Version.SelectedValue != "-1" ||
                ddlLessonPlan_Type.SelectedValue != "-1" ||
                ddlGradeTerm.SelectedValue != "-1" ||
                ddlSubject.SelectedValue != "-1")
            {
                strWhere += string.Format(" 1=1 and Resource_Version='{0}' and LessonPlan_Type='{1}' and GradeTerm='{2}' and Subject='{3}' "
                    , ddlResource_Version.SelectedValue.Filter()
                    , ddlLessonPlan_Type.SelectedValue.Filter()
                    , ddlGradeTerm.SelectedValue.Filter()
                    , ddlSubject.SelectedValue.Filter()
                    );
                strWhere += string.Format(" and Resource_Class ='{0}'", Rc.Common.Config.Resource_ClassConst.云资源);
                strWhere += "  order by ResourceFolder_Name";
                DataTable dt = new Rc.BLL.Resources.BLL_ResourceFolder().GetList(strWhere).Tables[0];

                sb.Append(GetFolderHtml(dt));

                strHtml = new StringBuilder();
            }

            return sb;
        }
        /// <summary>
        /// 资源目录
        /// </summary>
        /// <param name="ResourceVersion_ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private StringBuilder GetFolderHtml(DataTable dt)
        {
            strHtml = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            DataView dvw = new DataView();
            dvw.Table = dt;
            sb.Append("<div data-type='tree'>");
            sb.Append(InitNavigationTree("0", dvw));
            sb.Append("</div>");
            return sb;
        }
        /// <summary>
        /// 递归加载树菜单
        /// </summary>
        /// <param name="TreeIDCurrent"></param>
        /// <param name="TreeLevelCurrent"></param>
        /// <param name="dtAll"></param>
        /// <returns></returns>
        protected StringBuilder InitNavigationTree(string TreeIDCurrent, DataView dvw)
        {
            string strWhere = string.Empty;
            strWhere = " 1=1 ";

            if (TreeIDCurrent == "0")
            {
                dvw.RowFilter = string.Format("  {0} and ResourceFolder_ParentId ='0'", strWhere);
            }
            else
            {
                dvw.RowFilter = string.Format(" {0} and  ResourceFolder_ParentId = '{1}' ", strWhere, TreeIDCurrent);
            }
            if (dvw.Count > 0)
            {
                int subProcess = 0;
                int subMax = dvw.Count;
                string liClass = string.Empty;
                foreach (DataRowView drv in dvw)
                {
                    subProcess++;
                    if (subProcess == 1)
                    {
                        int level = 0;
                        int.TryParse(drv["ResourceFolder_Level"].ToString(), out  level);

                        strHtml.AppendFormat(" <ul class='left_tree_list' data-level='{0}'>", level + 1);
                        strHtml.Append("<li>");
                    }
                    if (drv["ResourceFolder_isLast"].ToString() != "1")
                    {
                        liClass = "fa fa-minus-square-o plus";
                    }
                    else
                    {
                        liClass = "fa plus";
                    }
                    strHtml.Append(" <div>");
                    strHtml.AppendFormat(" <i class='{0}'></i>", liClass);
                    strHtml.AppendFormat(" <a href='##'  onclick=\"ShowSubDocument('{0}','{1}','{2}')\" >{2}</a>"
                        , drv["ResourceFolder_isLast"].ToString()
                        , drv["ResourceFolder_Id"].ToString()
                        , drv["ResourceFolder_Name"].ToString());
                    //strHtml.AppendFormat(" <a href='##' >{0}</a>", drv["ResourceFolder_Name"].ToString());
                    strHtml.Append(" </div>");

                    InitNavigationTree(drv["ResourceFolder_Id"].ToString(), dvw);
                    if (subProcess == subMax)
                    {
                        strHtml.Append("</li>");
                        strHtml.Append("</ul>");
                    }
                }
            }
            return strHtml;
        }
      

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="Catalog_PersonalID">目录ID</param>
        /// <param name="DocName">搜索关键字</param>
        /// <param name="tp">区分：1搜索 0当前目录</param>
        /// <param name="PageIndex">页码</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetCloudResource(string ResourceFolder_Id, string DocName, string tp, string strResource_Type, string strResource_Class, int PageIndex, int PageSize)
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
                if (!string.IsNullOrEmpty(DocName)) strWhere += " and File_Name like '%" + DocName.Filter() + "%' ";
                if (tp == "0") strWhere += " and ResourceFolder_Id='" + ResourceFolder_Id.Filter() + "' ";
                strWhere += string.Format(" and Resource_Class='{0}' and Resource_Type='{1}'", strResource_Class, strResource_Type);
                strSqlCount = @"select count(*) from ResourceToResourceFolder A
INNER JOIN Resource B ON A.Resource_Id=B.Resource_Id  where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.CreateTime DESC) row,A.*,B.Resource_ContentLength from ResourceToResourceFolder A
INNER JOIN Resource B ON A.Resource_Id=B.Resource_Id  where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string docName = dtRes.Rows[i]["File_Name"].ToString();
                    docName = pfunction.GetDocFileName(docName);
                    string docType = dtRes.Rows[i]["ResourceToResourceFolder_Id"].ToString();
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        docName = docName,
                        docType = dtRes.Rows[i]["File_Suffix"].ToString(),
                        docSize = pfunction.ConvertDocSizeUnit(dtRes.Rows[i]["Resource_ContentLength"].ToString()),
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
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string DeleteCloudResource(string docId)
        {
            string result = "0";
            try
            {

                #region 删除数据
                StringBuilder strSqlU = new StringBuilder();

                strSqlU.Append("delete from ResourceToResourceFolder ");
                strSqlU.AppendFormat(" where [ResourceToResourceFolder_Id]='{0}'; ", docId.Filter());

                Model_VSysUserRole loginUser = HttpContext.Current.Session["LoginUser"] as Model_VSysUserRole;
                StringBuilder strLog = new StringBuilder();
                strLog.AppendFormat("删除文件：登录名【{0}】用户ID：【{1}】"
                    , loginUser.SysUser_Name, loginUser.SysUser_ID);
                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSqlU.ToString()) > 0)
                {
                    new BLL_clsAuth().AddLogFromBS("Document.aspx", strLog.ToString());
                    result = "1";
                }
                else
                {
                    new BLL_clsAuth().AddLogErrorFromBS("Document.aspx", strLog.ToString());
                }
                #endregion
            }
            catch (Exception)
            {

            }
            return result;
        }
        /// <summary>
        /// 修改文件名
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="docName"></param>
        /// <param name="docType"></param>
        /// <returns></returns>
        [WebMethod]
        public static string UpdateCloudResource(string docId, string docName)
        {
            string result = string.Empty;
            Model_VSysUserRole loginUser = HttpContext.Current.Session["LoginUser"] as Model_VSysUserRole;
            StringBuilder strLog = new StringBuilder();
            strLog.AppendFormat("编辑文件名：登录名【{0}】用户ID：【{1}】", loginUser.SysUser_Name, loginUser.SysUser_ID);

            try
            {

                StringBuilder strSql = new StringBuilder();

                strSql.Append("update ResourceToResourceFolder set ");
                strSql.AppendFormat(" [File_Name]='{0}'", docName.Trim().Filter().Replace("\n", "").Replace("\r", ""));
                strSql.AppendFormat(" ,[CreateFUser]='{0}'", loginUser.SysUser_ID);
                strSql.AppendFormat(" ,[CreateTime]=getdate() ");
                strSql.AppendFormat(" where [ResourceToResourceFolder_Id]='{0}' ", docId.Trim().Filter());


                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql.ToString()) > 0)
                {
                    new BLL_clsAuth().AddLogFromBS("Document.aspx", strLog.ToString());
                    result = "1";
                }
                else
                {
                    new BLL_clsAuth().AddLogFromBS("Document.aspx", strLog.ToString());
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS("Document.aspx", strLog.ToString() + ex.Message.ToString());
            }
            return result;
        }

        protected void btsSearch_Click(object sender, EventArgs e)
        {
            litTree.Text = GetResourceAttribute().ToString();
        }

    }
}