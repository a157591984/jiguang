using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Rc.Common;

using System.Data;
using Rc.Model;
using Rc.Common.StrUtility;
using Rc.Cloud.Model;
namespace CTM.Web.Ajax
{
    public partial class UserAjax : System.Web.UI.Page
    {
        protected Model_VSysUserRole loginUser = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder strSql = new StringBuilder();
            string key = string.Empty;
            string strWhere = string.Empty;
            string txtName = string.Empty;
            string strHtml = string.Empty;
            string iid = string.Empty;
           // loginUser = Rc.Common.clsSessionManager.IsPageFlag(this.Page);
            DataTable dt = new DataTable();

            if (Request["key"] != null)
            {
                key = Request["key"];
            }
            else
            {
                Response.Write("Error");
            }
            switch (key)
            {
                #region 用户上传文件
                case "AjaxTreeDocument_Personal":
                    iid = Request["iid"].ToString().Filter();
                    // string strTitle = Request["title"].ToString().Filter();
                    string strFileName = Request["filename"].ToString().Filter();
                    string strFileNameSys = Request["filenamesys"].ToString().Filter();
                    string strFileSize = Request["filesize"].ToString().Filter();
                    string strDocument_PersonalID = Guid.NewGuid().ToString();


                    Response.Write("0");
                    Response.End();
                    break;
                #endregion
                #region 用户文件列表
                case "AjaxDocument_PersonalList":
                    iid = Request["iid"].ToString().Filter();
                    // string strTitle = Request["title"].ToString().Filter();
                    strHtml = string.Empty;
                    strHtml += @"<table width='100%' border='0' cellpadding='0' cellspacing='0' class='tab_list'>
                    <thead>
                        <tr>
                            <td class='tab_title'>文件名</td>
                            <td class='tab_size'>大小</td>
                            <td class='tab_date'>修改日期</td>
                            <td class='tab_opera'>操作</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class='tab_list_title'><i class='icon32 txt32'></i>标题标题</td>
                            <td>4.8M</td>
                            <td>2013-07-02 08:56</td>
                            <td class='tab_list_opera'><a href='#'>下载</a><a href='#'><i class='icon16 edit16'></i>修改</a><a href='#'><i class='icon16 del16'></i>删除</a></td>
                        </tr>
                        <tr>
                            <td class='tab_list_title'><i class='icon32 doc32'></i>标题标题</td>
                            <td>4.8M</td>
                            <td>2013-07-02 08:56</td>
                            <td class='tab_list_opera'><a href='#'>下载</a><a href='#'><i class='icon16 edit16'></i>修改</a><a href='#'><i class='icon16 del16'></i>删除</a></td>
                        </tr>
                        <tr>
                            <td class='tab_list_title'><i class='icon32 xls32'></i>标题标题</td>
                            <td>4.8M</td>
                            <td>2013-07-02 08:56</td>
                            <td class='tab_list_opera'><a href='#'>下载</a><a href='#'><i class='icon16 edit16'></i>修改</a><a href='#'><i class='icon16 del16'></i>删除</a></td>
                        </tr>
                        <tr>
                            <td class='tab_list_title'><i class='icon32 pdf32'></i>标题标题</td>
                            <td>4.8M</td>
                            <td>2013-07-02 08:56</td>
                            <td class='tab_list_opera'><a href='#'>下载</a><a href='#'><i class='icon16 edit16'></i>修改</a><a href='#'><i class='icon16 del16'></i>删除</a></td>
                        </tr>
                    </tbody>
                </table>";
                    Response.Write(strHtml);
                    //if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlTran(strList) > 0)
                    //{
                    //    Response.Write("1");
                    //}
                    //else
                    //{
                    //    Response.Write("0");
                    //}
                    Response.End();
                    break;
                #endregion

                #region 后台上传文件
                case "AjaxTreeDocument":

                    
                    Response.End();
                    break;
                #endregion
                #region 后台文件列表
                case "AjaxDocumentList":
                    iid = Request["iid"].ToString().Filter();
                    // string strTitle = Request["title"].ToString().Filter();
                    strHtml = string.Empty;
                    strHtml += @"<table width='100%' border='0' cellpadding='0' cellspacing='0' class='tab_list'>
                    <thead>
                        <tr>
                            <td class='tab_title'>文件名</td>
                            <td class='tab_size'>大小</td>
                            <td class='tab_date'>修改日期</td>
                            <td class='tab_opera'>操作</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class='tab_list_title'><i class='icon32 txt32'></i>标题标题</td>
                            <td>4.8M</td>
                            <td>2013-07-02 08:56</td>
                            <td class='tab_list_opera'><a href='#'>下载</a><a href='#'><i class='icon16 edit16'></i>修改</a><a href='#'><i class='icon16 del16'></i>删除</a></td>
                        </tr>
                        <tr>
                            <td class='tab_list_title'><i class='icon32 doc32'></i>标题标题</td>
                            <td>4.8M</td>
                            <td>2013-07-02 08:56</td>
                            <td class='tab_list_opera'><a href='#'>下载</a><a href='#'><i class='icon16 edit16'></i>修改</a><a href='#'><i class='icon16 del16'></i>删除</a></td>
                        </tr>
                        <tr>
                            <td class='tab_list_title'><i class='icon32 xls32'></i>标题标题</td>
                            <td>4.8M</td>
                            <td>2013-07-02 08:56</td>
                            <td class='tab_list_opera'><a href='#'>下载</a><a href='#'><i class='icon16 edit16'></i>修改</a><a href='#'><i class='icon16 del16'></i>删除</a></td>
                        </tr>
                        <tr>
                            <td class='tab_list_title'><i class='icon32 pdf32'></i>标题标题</td>
                            <td>4.8M</td>
                            <td>2013-07-02 08:56</td>
                            <td class='tab_list_opera'><a href='#'>下载</a><a href='#'><i class='icon16 edit16'></i>修改</a><a href='#'><i class='icon16 del16'></i>删除</a></td>
                        </tr>
                    </tbody>
                </table>";
                    Response.Write(strHtml);
                    //if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlTran(strList) > 0)
                    //{
                    //    Response.Write("1");
                    //}
                    //else
                    //{
                    //    Response.Write("0");
                    //}
                    Response.End();
                    break;
                #endregion

                #region 后台共享资源
                case "AjaxShareDocument":
                    try
                    {
                       
                        
                        Response.Write("1");
                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }
                    Response.End();
                    break;
                #endregion
                #region 后台 返回用户分享文件数据
                case "AjaxGetUserShareDocument":
                    try
                    {
                        strHtml = string.Empty;
                        string userId = Request["userId"].ToString().Filter();
                        strSql = new StringBuilder();
                        strSql.Append("select dbo.[GetCatalogParentLine](cdoc.CatalogID) catName,doc.DocumentName from YW_Document doc ");
                        strSql.Append(" left join YW_Catalog_Document cdoc on doc.DocumentID=cdoc.DocumentID ");
                        strSql.Append(" where doc.DocumentID in( select DocumentID from YW_DocumentShare where UserId='" + userId.Filter() + "') ");
                        dt = new DataTable();
                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                        if (dt.Rows.Count != 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                strHtml += "<tr><td>" + dt.Rows[i]["catName"] + "</td>";
                                strHtml += "<td style=\"border-right:1px solid #ccc;\">" + Rc.Cloud.Web.Common.pfunction.GetDocFileName(dt.Rows[i]["DocumentName"].ToString()) + "</td></tr>";
                            }
                        }
                        else
                        {
                            Response.Write("<tr><td colspan='2' text='align'>暂无文件</td></tr>");
                        }
                        Response.Write(strHtml);
                    }
                    catch (Exception)
                    {
                        Response.Write("<tr><td colspan='2' text='align'>暂无文件</td></tr>");
                    }
                    Response.End();
                    break;
                #endregion

            }

        }

        /// <summary>
        /// 获取后台共享文件 目录及父级目录
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        private string GetCatalogIdandParentId(string docId)
        {
            string result = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                dt = Rc.Common.DBUtility.DbHelperSQL.Query("select CatalogID from dbo.YW_Catalog_Document where DocumentId='" + docId.Filter() + "' ").Tables[0];
                result = dt.Rows[0]["CatalogID"].ToString();
                dt = Rc.Common.DBUtility.DbHelperSQL.Query("select D_ParentId from dbo.YW_Catalog where CatalogID='" + result + "' ").Tables[0];
                if (dt.Rows[0]["D_ParentId"].ToString() != "0") result = dt.Rows[0]["D_ParentId"].ToString() + "," + result;
            }
            catch (Exception)
            {

            }
            return result;
        }

    }
}