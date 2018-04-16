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
using Rc.Model.Resources;
using Rc.BLL.Resources;


namespace Rc.Cloud.Web.teacher
{
    public partial class ClassToResourceList : System.Web.UI.Page
    {
        protected string strResource_Class = string.Empty;//资源类别（云资源、自有资源）
        protected string strGradeTerm = string.Empty;//年级学期
        protected string strResource_Version = string.Empty;//教材版本
        protected string strSubject = string.Empty;//学科

        protected string userId = string.Empty;
        protected string userIdentity = string.Empty;
        protected string folderId = string.Empty;
        string isBack = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = Request["userId"].Filter();
            userIdentity = Request["userIdentity"].Filter();
            folderId = Request["folderId"].Filter();
            isBack = Request["isBack"].Filter();
            hiduserId.Value = userId;
            hiduserIdentity.Value = userIdentity;
            hidfolderId.Value = folderId;

            strResource_Class = Request["Resource_Class"].Filter();
            strGradeTerm = Request["GradeTerm"].Filter();
            strResource_Version = Request["Resource_Version"].Filter();
            strSubject = Request["Subject"].Filter();

            if (string.IsNullOrEmpty(isBack))
            {
                btnBack.Visible = false;
            }

            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //资源类别
                ddlResource_Class.Items.Clear();
                ddlResource_Class.Items.Add(new ListItem("云资源", Rc.Common.Config.Resource_ClassConst.云资源));
                if (userIdentity == "T") ddlResource_Class.Items.Add(new ListItem("自有资源", Rc.Common.Config.Resource_ClassConst.自有资源));
                ddlResource_Class.SelectedValue = strResource_Class;
                if (userIdentity == "A") ddlResource_Class.Style.Add("display", "none");

                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID", "年级学期");
                ddlGradeTerm.SelectedValue = strGradeTerm;

                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "教材版本");
                ddlResource_Version.SelectedValue = strResource_Version;

                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "学科");
                ddlSubject.SelectedValue = strSubject;

            }
        }

        [WebMethod]
        public static string GetCloudBooks(string DocName, string strResource_Class, string strGradeTerm, string strSubject, string strResource_Version, string userId, string userIdentity, string folderId, int PageIndex, int PageSize)
        {
            try
            {
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;

                string strWhere = " Resource_Type='" + Rc.Common.Config.Resource_TypeConst.testPaper类型文件 + "' ";
                if (!string.IsNullOrEmpty(DocName)) strWhere += " and ResourceFolder_Name like '%" + DocName.Filter() + "%' ";
                if (strResource_Class != "-1") strWhere += " and Resource_Class = '" + strResource_Class.Filter() + "' ";
                if (strGradeTerm != "-1") strWhere += " and GradeTerm = '" + strGradeTerm.Filter() + "' ";
                if (strSubject != "-1") strWhere += " and Subject = '" + strSubject.Filter() + "' ";
                if (strResource_Version != "-1") strWhere += " and Resource_Version = '" + strResource_Version.Filter() + "' ";
                if (userIdentity == "A")
                {
                    #region 管理员
                    if (folderId == "0")
                    {
                        strWhere += " and ResourceFolder_Level=5 ";
                    }
                    else
                    {
                        strWhere += " and ResourceFolder_ParentId='" + folderId.Filter() + "' ";
                    }
                    strSql = string.Format(@"select * from (select ROW_NUMBER() over(ORDER BY vw.ResourceFolder_Name) row,vw.*
from VW_ResourceAndResourceFolder vw 
inner join BookAudit bk on bk.ResourceFolder_Id=vw.Book_ID and bk.AuditState='1' 
where {0} ) t where row between {1} and {2} ", strWhere, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize));
                    strSqlCount = string.Format(@"select count(*) from VW_ResourceAndResourceFolder vw 
inner join BookAudit bk on bk.ResourceFolder_Id=vw.Book_ID and bk.AuditState='1' where {0} ", strWhere);
                    #endregion
                }
                else if (userIdentity == "T")
                {
                    #region 老师
                    if (strResource_Class == Rc.Common.Config.Resource_ClassConst.云资源.ToString())
                    {
                        #region 老师云资源
                        if (folderId == "0")
                        {
                            strWhere += " and ResourceFolder_Level=5 ";
                        }
                        else
                        {
                            strWhere += " and ResourceFolder_ParentId='" + folderId.Filter() + "' ";
                        }
                        strSql = string.Format(@"select * from (select ROW_NUMBER() over(ORDER BY vw.ResourceFolder_Name) row,vw.*
from VW_ResourceAndResourceFolder vw 
inner join Bookshelves bk on bk.ResourceFolder_Id=vw.Book_ID and bk.BookShelvesState='1' 
inner join UserBuyResources ub on ub.Book_ID=vw.Book_id AND ub.UserId='{0}' 
where {1} ) t where row between {2} and {3} ", userId, strWhere, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize));
                        strSqlCount = string.Format(@"select count(*) from VW_ResourceAndResourceFolder vw 
inner join Bookshelves bk on bk.ResourceFolder_Id=vw.Book_ID and bk.BookShelvesState='1' 
inner join UserBuyResources ub on ub.Book_ID=vw.Book_id AND ub.UserId='{0}' 
where {1} ", userId, strWhere);
                        #endregion
                    }
                    else
                    {
                        #region 老师自有资源
                        strWhere += " and ResourceFolder_ParentId='" + folderId.Filter() + "' and CreateFUser='" + userId + "' ";
                        strSql = string.Format(@"select * from (select ROW_NUMBER() over(ORDER BY vw.ResourceFolder_Name) row,vw.*
from VW_ResourceAndResourceFolder vw 
where {0} ) t where row between {1} and {2} ", strWhere, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize));
                        strSqlCount = string.Format(@"select count(*) from VW_ResourceAndResourceFolder vw where {0} ", strWhere);
                        #endregion
                    }
                    #endregion
                }

                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        RType = dtRes.Rows[i]["RType"].ToString(), // 0文件夹 1文件
                        docId = dtRes.Rows[i]["ResourceFolder_Id"].ToString(),
                        docName = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(),
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