using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Common.Config;
using System.Data;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Web.Services;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.teacher
{
    public partial class cHomework_API2 : System.Web.UI.Page
    {
        StringBuilder strHtml = new StringBuilder();

        protected string strResourceForder_IdDefault = string.Empty;
        protected string strUserGroup_IdActivity = string.Empty;
        protected void Page_Load(object sender, EventArgs e) 
        {
            strResourceForder_IdDefault = Request.QueryString["strResourceForder_IdDefault"].Filter();
            if (!IsPostBack)
            {
                string strLessonPlan_Type = string.Empty;


                string strTestForder_IdActivity = string.Empty;
                string strResource_Class = Resource_ClassConst.云资源;
                string strResource_Type = Resource_TypeConst.testPaper类型文件;

                if (!String.IsNullOrEmpty(Request["tfid"]))
                {
                    strTestForder_IdActivity = Request["tfid"].ToString().Trim().Filter();
                }
                DataTable dtTestForder = new DataTable();
                dtTestForder = GetTestForder(strResource_Class, strResource_Type, strUserGroup_IdActivity);
                strResourceForder_IdDefault = GetTestForderOne(strUserGroup_IdActivity, dtTestForder);
                litContent.Text = GetContentHtml(strUserGroup_IdActivity, strTestForder_IdActivity, dtTestForder).ToString();

            }
        }
        private string GetUrl(string strUserGroup_IdActivity, string strTestForder_IdActivity)
        {
            string strTemp = string.Empty;
            strTemp += string.Format("ugid={0}"
                , strUserGroup_IdActivity.Trim());
            return strTemp;
        }
        private DataTable GetTestForder(string strResource_Class, string strResource_Type, string strUserGroup_IdActivity)
        {
            DataTable dtTestForder = new DataTable();
            string strWhere = string.Empty;
            strWhere = string.Format(@"  1=1
            AND Resource_Class='{0}'
            AND Resource_Type='{1}'
            ", strResource_Class, strResource_Type);
            strWhere += string.Format(" AND ResourceFolder_Level>={0}", 4);//练习册只取第一级目录（云习题集第一级目录从4开始的）

            strWhere += " ORDER BY ResourceFolder_Level,ResourceFolder_Order,ResourceFolder_Name";
            dtTestForder = new Rc.BLL.Resources.BLL_ResourceFolder().GetList(strWhere).Tables[0];
            return dtTestForder;
        }
        private StringBuilder GetContentHtml(string strUserGroup_IdActivity, string strTestForder_IdActivity, DataTable dtTestForder)
        {
            StringBuilder strTempHtml = new StringBuilder();
            //班列表
            //strTempHtml.Append(GetUserGroupHtml(strUserGroup_IdActivity, dtUserGroupList));

            strTempHtml.Append("<div class='iframe-container'>");

            //练习册列表
            strTempHtml.Append(GetTestForderHtml(strUserGroup_IdActivity, strTestForder_IdActivity, dtTestForder));
            strTempHtml.Append("<div class='iframe-main pa'>");

            //内容
            strTempHtml.Append("<table class='table table-bordered'>");
            strTempHtml.Append("<thead>");
            strTempHtml.Append("<tr>");
            strTempHtml.Append("<td>作业内容</td>");
            strTempHtml.Append("<td width='30%' class='text-center'>操作</td>");
            strTempHtml.Append("</tr>");
            strTempHtml.Append("</thead>");
            strTempHtml.Append("<tbody id='tbRes'>");
            strTempHtml.Append("</tbody>");
            strTempHtml.Append("</table>");
            strTempHtml.Append("<div class='pagination_container'>");
            strTempHtml.Append("<ul class='pagination'>");
            strTempHtml.Append("</ul>");
            strTempHtml.Append("</div>");
            strTempHtml.Append("</div>");

            return strTempHtml;
        }
        private StringBuilder GetTestForderHtml(string strUserGroup_IdActivity, string strTestForder_IdActivity, DataTable dtTestForder)
        {
            StringBuilder strTempHtml = new StringBuilder();
            strTempHtml.Append("<div class='iframe-sidebar'>");

            //树状列表开始
            strTempHtml.Append("<div class='tree sidebar_menu' id='list'>");
            DataView dvw = new DataView();
            dvw.Table = dtTestForder;
            strTempHtml.Append(InitNavigationTree("0", dvw));

            strTempHtml.Append("</div>");
            //树状列表结束

            strTempHtml.Append("</div>");
            return strTempHtml;
        }
        protected StringBuilder InitNavigationTree(string TreeIDCurrent, DataView dvw)
        {
            string strWhere = string.Empty;
            strWhere = " 1=1 ";
            if (TreeIDCurrent == "0")
            {
                dvw.RowFilter = string.Format("  {0} and ResourceFolder_Level ='5'", strWhere);
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
                        strHtml.AppendFormat(" <ul data-level='{0}'>", level - 5);

                    }
                    strHtml.Append("<li>");
                    string tempactive = drv["ResourceFolder_Id"].ToString() == strResourceForder_IdDefault ? "active" : "";
                    strHtml.Append(" <div class='name " + tempactive + "'>");
                    strHtml.AppendFormat(" <i class='fa tree_btn'></i>");
                    strHtml.AppendFormat(" <a href='##' id='{0}' onclick=\"ShowSubDocument('{0}','{1}')\">{1}</a>"
                        , drv["ResourceFolder_Id"].ToString()
                        , drv["ResourceFolder_Name"].ToString().ReplaceForFilter());
                    strHtml.Append(" </div>");
                    InitNavigationTree(drv["ResourceFolder_Id"].ToString(), dvw);
                    strHtml.Append("</li>");
                    if (subProcess == subMax)
                    {
                        strHtml.Append("</ul>");
                    }
                }
            }
            return strHtml;
        }

        private string GetUserGroupOne(DataTable dtUserGroupList)
        {
            string strTemp = string.Empty;
            if (dtUserGroupList.Rows.Count > 0)
            {
                strTemp = dtUserGroupList.Rows[0]["UserGroup_Id"].ToString();
            }
            return strTemp;
        }
        private string GetTestForderOne(string strUserGroup_IdActivity, DataTable dtTestForder)
        {
            string strTemp = string.Empty;
            if (!string.IsNullOrEmpty(strResourceForder_IdDefault))
            {
                strTemp = strResourceForder_IdDefault;
            }
            else
            {
                if (dtTestForder.Rows.Count > 0)
                {
                    DataRow[] dv = dtTestForder.Select(" 1=1");
                    strTemp = dv[0]["ResourceFolder_Id"].ToString();
                }
            }
            return strTemp;
        }


        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetcHomework(string ResourceFolder_Id, string DocName, string ShowFolderIn, int PageIndex, int PageSize)
        {
            try
            {
                DocName = DocName.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                string strOrder = string.Empty;
                if (!string.IsNullOrEmpty(DocName)) strWhere += " and File_Name like '%" + DocName.Filter() + "%' ";

                strWhere += " and ResourceFolder_Id='" + ResourceFolder_Id.Filter() + "' ";
                //strWhere += string.Format(" AND Book_ID IN(SELECT Book_ID FROM UserBuyResources WHERE A.Book_ID=UserBuyResources.Book_id AND UserBuyResources.UserId='{0}' )", FloginUser.UserId);

                strWhere += string.Format(" and Resource_Type='{0}'", Rc.Common.Config.Resource_TypeConst.testPaper类型文件);
                strSqlCount = @"select count(*) from ResourceToResourceFolder A
INNER JOIN Resource B ON A.Resource_Id=B.Resource_Id 
 where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.ResourceToResourceFolder_Order,A.Resource_Name ) row,A.*,B.Resource_ContentLength
from ResourceToResourceFolder A
INNER JOIN Resource B ON A.Resource_Id=B.Resource_Id
 where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize);
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string docName = dtRes.Rows[i]["Resource_Name"].ToString().ReplaceForFilter();
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        docName = docName,
                        EnHomeWork_Name = HttpContext.Current.Server.UrlEncode(dtRes.Rows[i]["Resource_Name"].ToString().ReplaceForFilter()),
                        docTime = Rc.Cloud.Web.Common.pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString())
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