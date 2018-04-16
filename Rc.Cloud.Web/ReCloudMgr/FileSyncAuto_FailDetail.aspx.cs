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
using Rc.BLL.Resources;
using System.Text;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncAuto_FailDetail : Rc.Cloud.Web.Common.InitPage
    {
        StringBuilder strHtml = new StringBuilder();
        public string FileSyncExecRecord_id = string.Empty;
        public string Book_id = string.Empty;
        public string StrTemp = string.Empty;
        public string ResourceFolder_NameE = string.Empty;
        DataTable dtre = new DataTable();
        DataView dvw = new DataView();
        protected void Page_Load(object sender, EventArgs e)
        {
            FileSyncExecRecord_id = Request.QueryString["FileSyncExecRecord_id"].Filter();
            Book_id = Request.QueryString["Book_id"].Filter();
            ResourceFolder_NameE = Request.QueryString["FileName"];
            ResourceFolder_NameE = Server.UrlDecode(ResourceFolder_NameE);
            Module_Id = "10300200";
            if (!IsPostBack)
            {
                BLL_ResourceFolder bll = new BLL_ResourceFolder();
                dtre = bll.GetList("Book_id='" + Book_id + "'").Tables[0];
                if (dtre.Rows.Count > 0)
                {
                    dvw.Table = dtre;
                }
                if (dvw.Count > 0)
                {
                    ltlTest.Text = GetBody();
                }
            }
        }
        public string GetBody()
        {
            try
            {
                string strsql = @"select fd.Book_Id,fd.FileSyncExecRecord_id,rf.File_Name,fd.FileUrl,r.ResourceFolder_id,fd.FileSyncExecRecordDetail_id,rf.Resource_Name,tq.topicNumber from FileSyncExecRecordDetail fd
left join ResourceToResourceFolder rf on rf.ResourceToResourceFolder_Id=fd.ResourceToResourceFolder_Id
left join ResourceFolder r on r.ResourceFolder_Id=rf.ResourceFolder_Id
left join TestQuestions tq on tq.TestQuestions_Id=fd.TestQuestions_Id where fd.FileSyncExecRecord_id='" + FileSyncExecRecord_id + "' and fd.Book_id='" + Book_id + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strsql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        StrTemp = "";
                        strHtml.Append("<tr class='tr_con_001'>");
                        strHtml.Append("<td>" + InitNavigationTD(dvw, item["ResourceFolder_id"].ToString(), 0) + "</td>");
                        strHtml.Append("<td>" + item["File_Name"].ToString().ReplaceForFilter() + "</td>");
                        strHtml.Append("<td title='" + item["FileUrl"].ToString() + "'>" + item["FileUrl"].ToString() + "</td>");
                        strHtml.Append("<td>" + item["topicNumber"].ToString() + "</td>");
                        strHtml.Append("</tr>");
                    }
                    return strHtml.ToString();
                }
                else
                {
                    return "<tr class='tr_con_001'><td colspan='100' align='center'>暂无数据</td></tr>";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string InitNavigationTD(DataView dvw, string ResourceFolder_id, int i)
        {
            try
            {

                if (i == 0)
                {
                    dvw.RowFilter = (" ResourceFolder_id ='" + ResourceFolder_id + "'");
                }
                else
                {
                    //DataRow[] dr = dtre.Select("ResourceFolder_id='" + ResourceFolder_id + "'");
                    dvw.RowFilter = string.Format(" ResourceFolder_id = '{0}' ", ResourceFolder_id);
                }
                if (dvw.Count > 0)
                {
                    int subMax = dvw.Count;
                    foreach (DataRowView drv in dvw)
                    {
                        StrTemp += drv["ResourceFolder_Name"].ToString().ReplaceForFilter() + ",";
                        InitNavigationTD(dvw, drv["ResourceFolder_ParentId"].ToString(), 2);

                    }
                }
                string[] strarr = StrTemp.TrimEnd(',').Split(',');
                Array.Reverse(strarr);
                string str = string.Empty;
                if (strarr.Length > 1)
                {
                    for (int j = 0; j < strarr.Length; j++)
                    {
                        str += strarr[j] + "/";
                    } return str.TrimEnd('/');
                }
                else
                {
                    return strarr[0].ToString();
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}