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
using Newtonsoft.Json;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncViewList : System.Web.UI.Page
    {
        protected string strResource_Type = string.Empty;//资源类型（class类型文件、testPaper类型文件、ScienceWord类型文件）
        protected string strResource_Class = string.Empty;//资源类别（云资源、自有资源）
        protected string strGradeTerm = string.Empty;//年级学期
        protected string strSubject = string.Empty;//学科
        protected string strResource_Version = string.Empty;//教材版本
        public string _t = string.Empty;
        protected string s = string.Empty;
        protected string userId = string.Empty;
        protected string FileSyncExecRecord_Type = string.Empty;//同步类型
        StringBuilder strHtml = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            strResource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
            strResource_Type = ddlResource_Type.SelectedValue;
            strGradeTerm = ddlGradeTerm.SelectedValue;
            strSubject = ddlSubject.SelectedValue;
            strResource_Version = ddlResource_Version.SelectedValue;
            FileSyncExecRecord_Type = Request.QueryString["FileSyncExecRecord_Type"].Filter();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "请选择");
                //文档类型
                strWhere = string.Format(" D_Type='1' AND Common_Dict_ID!='{0}' order by d_order", Rc.Common.Config.Resource_TypeConst.按属性生成的目录);
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Type, dt, "D_Name", "Common_Dict_ID", "请选择");
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

        [WebMethod]
        public static string GetDataList(string FileSyncExecRecord_Type, string DocName, string strResource_Type, string strResource_Class, string strGradeTerm, string strSubject, string strResource_Version, int PageIndex, int PageSize)
        {
            try
            {
                FileSyncExecRecord_Type = FileSyncExecRecord_Type.Filter();
                DocName = DocName.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                BLL_ResourceFolder bll = new BLL_ResourceFolder();
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;

                string strWhere = " 1=1 ";
                //                strWhere += string.Format(@" Book_Id in(select distinct t2.Book_Id
                //from FileSyncExecRecord t 
                //inner join FileSyncExecRecordDetail t2 on t2.FileSyncExecRecord_id=t.FileSyncExecRecord_id
                //where t.FileSyncExecRecord_Type='{0}' and t.FileSyncExecRecord_Status='1') ", FileSyncExecRecord_Type);//同步过的书籍

                if (!string.IsNullOrEmpty(DocName)) strWhere += " and ResourceFolder_Name like '%" + DocName.Filter() + "%' ";
                if (strResource_Class != "-1") strWhere += " and Resource_Class = '" + strResource_Class.Filter() + "' ";
                if (strResource_Type != "-1") strWhere += " and Resource_Type = '" + strResource_Type.Filter() + "' ";
                if (strGradeTerm != "-1") strWhere += " and GradeTerm = '" + strGradeTerm.Filter() + "' ";
                if (strSubject != "-1") strWhere += " and Subject = '" + strSubject.Filter() + "' ";
                if (strResource_Version != "-1") strWhere += " and Resource_Version = '" + strResource_Version.Filter() + "' ";

                strWhere += " and ResourceFolder_Level=5 ";//管理员维护的书籍目录

                strSql = string.Format(@"select * from (select row_number() over(order by ResourceFolder_Name) as r_n,* from (select distinct rf.ResourceFolder_Id,rf.GradeTerm,rf.Subject,rf.Resource_Version,rf.Resource_Type,rf.Resource_Class,rf.ResourceFolder_Level,rf.ResourceFolder_Name from ResourceFolder rf
inner join FileSyncExecRecordDetail t on t.Book_Id=rf.Book_ID
inner join FileSyncExecRecord t2 on t2.FileSyncExecRecord_id=t.FileSyncExecRecord_id
where rf.ResourceFolder_Level=5 and t2.FileSyncExecRecord_Type='{0}') t where {1} ) tt where r_n between {2} and {3} "
                    , FileSyncExecRecord_Type, strWhere, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize));

                strSqlCount = string.Format(@"select count(1) from (select distinct rf.ResourceFolder_Id,rf.GradeTerm,rf.Subject,rf.Resource_Version,rf.Resource_Type,rf.Resource_Class,rf.ResourceFolder_Level,rf.ResourceFolder_Name from ResourceFolder rf
inner join FileSyncExecRecordDetail t on t.Book_Id=rf.Book_ID
inner join FileSyncExecRecord t2 on t2.FileSyncExecRecord_id=t.FileSyncExecRecord_id
where rf.ResourceFolder_Level=5 and t2.FileSyncExecRecord_Type='{0}') t where {1} "
                    , FileSyncExecRecord_Type, strWhere);

                //dtRes = bll.GetListByPage(strWhere, "ResourceFolder_Name", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                //int rCount = bll.GetRecordCount(strWhere);

                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
                int rCount = int.Parse(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount, 300).ToString());

                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        docId = dtRes.Rows[i]["ResourceFolder_Id"].ToString(),
                        GradeTerm = new BLL.Common_DictBLL().GetD_NameByID(dtRes.Rows[i]["GradeTerm"].ToString()),
                        Subject = new BLL.Common_DictBLL().GetD_NameByID(dtRes.Rows[i]["Subject"].ToString()),
                        Resource_Version = new BLL.Common_DictBLL().GetD_NameByID(dtRes.Rows[i]["Resource_Version"].ToString()),
                        Resource_Type = new BLL.Common_DictBLL().GetD_NameByID(dtRes.Rows[i]["Resource_Type"].ToString()),
                        docName = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter()
                    });
                }
                #endregion

                if (dtRes.Rows.Count > 0)
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