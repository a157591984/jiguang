﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Text;
using Rc.Common.Config;
using System.Data;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Web.Services;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.teacher
{
    public partial class mHomework_API : Rc.Cloud.Web.Common.FInitData
    {
        StringBuilder strHtml = new StringBuilder();

        protected string strResourceForder_IdDefault = string.Empty;
        protected string strUserGroup_IdActivity = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Rc.Common.ConfigHelper.GetConfigBool("IsShowTestAssembly"))
            {
                testPaper2.Visible = true;
            }
            strResourceForder_IdDefault = Request.QueryString["strResourceForder_IdDefault"].Filter();
            if (!IsPostBack)
            {
                string strLessonPlan_Type = string.Empty;


                string strTestForder_IdActivity = string.Empty;
                string strSubject = FloginUser.Subject;
                string strResource_Version = FloginUser.Resource_Version;
                string strResource_Class = Resource_ClassConst.自有资源;
                string strResource_Type = Resource_TypeConst.testPaper类型文件;

                DataTable dtUserGroupList = new DataTable();
                string strWhere = string.Empty;
                strWhere = string.Format("UserGroup_AttrEnum='{1}' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and User_Id='{0}') order by UserGroupOrder "
                    , FloginUser.UserId
                    , UserGroup_AttrEnum.Class);
                dtUserGroupList = new BLL_UserGroup().GetList(strWhere).Tables[0];
                strUserGroup_IdActivity = GetUserGroupOne(dtUserGroupList);
                if (!String.IsNullOrEmpty(Request["ugid"]))
                {
                    strUserGroup_IdActivity = Request["ugid"].ToString().Trim().Filter();
                }
                if (!String.IsNullOrEmpty(Request["tfid"]))
                {
                    strTestForder_IdActivity = Request["tfid"].ToString().Trim().Filter();
                }
                DataTable dtTestForder = new DataTable();
                dtTestForder = GetTestForder(strResource_Class, strResource_Type, strSubject, strResource_Version, strUserGroup_IdActivity);
                strResourceForder_IdDefault = GetTestForderOne(strUserGroup_IdActivity, dtTestForder);
                litContent.Text = GetContentHtml(strUserGroup_IdActivity, strTestForder_IdActivity, dtTestForder, dtUserGroupList).ToString();

            }
        }
        private string GetUrl(string strUserGroup_IdActivity, string strTestForder_IdActivity)
        {
            string strTemp = string.Empty;
            strTemp += string.Format("ugid={0}"
                , strUserGroup_IdActivity.Trim());
            return strTemp;
        }
        private DataTable GetTestForder(string strResource_Class, string strResource_Type, string strSubject, string strResource_Version, string strUserGroup_IdActivity)
        {
            DataTable dtTestForder = new DataTable();
            string strWhere = string.Empty;
            strWhere = string.Format(@"  1=1
            AND Resource_Class='{0}'
            AND Resource_Type='{1}'
            ", strResource_Class, strResource_Type);
            strWhere += string.Format("AND CreateFUser ='{0}'"
               , FloginUser.UserId);
            strWhere += " ORDER BY ResourceFolder_Level,ResourceFolder_Order,ResourceFolder_Name";
            dtTestForder = new Rc.BLL.Resources.BLL_ResourceFolder().GetList(strWhere).Tables[0];
            return dtTestForder;
        }
        private StringBuilder GetContentHtml(string strUserGroup_IdActivity, string strTestForder_IdActivity, DataTable dtTestForder, DataTable dtUserGroupList)
        {
            StringBuilder strTempHtml = new StringBuilder();

            //班列表
            strTempHtml.Append(GetUserGroupHtml(strUserGroup_IdActivity, dtUserGroupList));

            strTempHtml.Append("<div class='iframe-container'>");

            //练习册列表
            strTempHtml.Append(GetTestForderHtml(strUserGroup_IdActivity, strTestForder_IdActivity, dtTestForder));

            strTempHtml.Append("<div class='iframe-main'>");

            strTempHtml.Append("<div class='container-fluid section_container'>");
            strTempHtml.Append("<div class=\"panel panel-default table_list\">");
            strTempHtml.Append("<table class='table table-bordered table-hover'>");
            strTempHtml.Append("<thead>");
            strTempHtml.Append("<tr>");
            strTempHtml.Append("<td>作业内容</td>");
            strTempHtml.Append("<td>时间</td>");
            strTempHtml.Append("<td width='30%' class='text-center'>操作</td>");
            strTempHtml.Append("</tr>");
            strTempHtml.Append("</thead>");
            strTempHtml.Append("<tbody id='tbRes'>");
            strTempHtml.Append("</tbody>");
            strTempHtml.Append("</table>");
            strTempHtml.Append("</div>");
            strTempHtml.Append("<div class='pagination_container'>");
            strTempHtml.Append("<ul class='pagination'>");
            strTempHtml.Append("</ul>");
            strTempHtml.Append("</div>");
            strTempHtml.Append("</div>");
            strTempHtml.Append("</div>");

            return strTempHtml;
        }
        private StringBuilder GetTestForderHtml(string strUserGroup_IdActivity, string strTestForder_IdActivity, DataTable dtTestForder)
        {
            StringBuilder strTempHtml = new StringBuilder();
            strTempHtml.Append("<div class=\"iframe-sidebar\">");

            //树状列表开始
            strTempHtml.Append("<div class='tree sidebar_menu' data-name='exerciseBook' id='list'>");
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
                        strHtml.AppendFormat(" <ul data-level='{0}'>", level - 5);
                    }
                    strHtml.Append("<li>");
                    
                    string tempactive = drv["ResourceFolder_Id"].ToString() == strResourceForder_IdDefault ? "active" : "";
                    strHtml.Append(" <div class='name " + tempactive + "'>");
                    strHtml.AppendFormat(" <i class='fa tree_btn'></i>");
                    strHtml.AppendFormat(" <a href='##' id='{0}' onclick=\"ShowSubDocument('{0}','{1}')\" >{1}</a>"
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
        /// 得到班列表 HTML
        /// </summary>
        /// <param name="strUserGroup_IdActivity"></param>
        /// <returns></returns>
        private StringBuilder GetUserGroupHtml(string strUserGroup_IdActivity, DataTable dtUserGroupList)
        {
            StringBuilder strTempHtml = new StringBuilder();

            strTempHtml.Append("<div class='iframe-subnav'>");
            strTempHtml.Append("<ul class='subnav'>");
            for (int i = 0; i < dtUserGroupList.Rows.Count; i++)
            {
                strTempHtml.Append("<li>");
                strTempHtml.AppendFormat("<a class='{5}' href='##' onclick=\"Show('{0}','{4}')\" val='{2}' title=\"{3}\">{1}<span>({2})</span>"
                    , GetUrl(dtUserGroupList.Rows[i]["UserGroup_Id"].ToString(), "")
                    , Rc.Cloud.Web.Common.pfunction.GetSubstring(dtUserGroupList.Rows[i]["UserGroup_Name"].ToString().Trim(), 10, true)
                    , dtUserGroupList.Rows[i]["UserGroup_Id"].ToString().Trim()
                    , dtUserGroupList.Rows[i]["UserGroup_Name"]
                    , new Random().Next()
                    , strUserGroup_IdActivity.Trim() == dtUserGroupList.Rows[i]["UserGroup_Id"].ToString().Trim() ? "active" : ""
                    );

                strTempHtml.Append("</a>");
                strTempHtml.Append("</li>");
            }
            strTempHtml.Append("</ul>");
            strTempHtml.Append("</div>");
            return strTempHtml;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="ResourceFolder_Id">文件夹标识</param>
        /// <param name="DocName">文档名称的查询条件</param>
        /// <param name="ShowFolderIn">显示某个文件夹中的文件 ：1搜索 0当前目录</param>
        /// <param name="PageIndex">页码</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetcHomework(string ResourceFolder_Id, string DocName, string ShowFolderIn, string UserGroupId, int PageIndex, int PageSize)
        {
            Rc.Model.Resources.Model_F_User FloginUser = Rc.Common.StrUtility.clsUtility.IsFPageFlag() as Rc.Model.Resources.Model_F_User;
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

                strWhere += string.Format(" and Resource_Type='{0}'", Rc.Common.Config.Resource_TypeConst.testPaper类型文件);
                strSqlCount = @"select count(*) from ResourceToResourceFolder A
INNER JOIN Resource B ON A.Resource_Id=B.Resource_Id 
left join HomeWork hw on hw.UserGroup_Id='" + UserGroupId + "' and hw.ResourceToResourceFolder_Id=A.ResourceToResourceFolder_Id where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.ResourceToResourceFolder_Order,A.Resource_Name ) row,A.*,B.Resource_ContentLength,hw.HomeWork_Id
from ResourceToResourceFolder A
INNER JOIN Resource B ON A.Resource_Id=B.Resource_Id
left join HomeWork hw on hw.UserGroup_Id='" + UserGroupId + "' and hw.ResourceToResourceFolder_Id=A.ResourceToResourceFolder_Id where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize);
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string docName = dtRes.Rows[i]["Resource_Name"].ToString();
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        docName = docName,
                        EnHomeWork_Name = HttpContext.Current.Server.UrlEncode(dtRes.Rows[i]["Resource_Name"].ToString()),
                        docTime = Rc.Cloud.Web.Common.pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
                        HomeWork_Id = dtRes.Rows[i]["HomeWork_Id"].ToString()
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