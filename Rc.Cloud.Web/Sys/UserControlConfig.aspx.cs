using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.Web.Common;
using System.Data;
using System.Text;
using Rc.Common.StrUtility;
using Rc.Cloud.BLL;

namespace Rc.Cloud.Web.Sys
{
    public partial class UserControlConfig : Rc.Cloud.Web.Common.InitPage
    {
        BLL_DictionarySQlMaintenance BLL = new BLL_DictionarySQlMaintenance();

        protected string ReturnUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90300200";
            SetSearchP();
            ReturnUrl = strPageName + "?" + pfunction.getPageParam();

            if (!IsPostBack)
            {
                SetSearchO();
            }
        }

        //给查询对象附值
        private void SetSearchO()
        {
            if (!string.IsNullOrEmpty(Request["n"]))
            {
                txtName.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request["n"].ToString());

            }
            if (!string.IsNullOrEmpty(Request["n"]))
            {
                txtMark.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request["r"].ToString());

            }

        }

        //URL加密
        private void SetSearchP()
        {
            if (!string.IsNullOrEmpty(Request["PageIndex"]))
            {
                if (!int.TryParse(Request["PageIndex"].ToString(), out PageIndex)) { PageIndex = 1; }
                if (PageIndex <= 0) { PageIndex = 1; }
            }
            if (!string.IsNullOrEmpty(Request["PageSize"]))
            {
                if (!int.TryParse(Request["PageSize"].ToString(), out PageSize)) { PageSize = 10; }
            }

            strUrlPara = strPageName + "?PageIndex={0}&PageSize={1}";
            strUrlPara += "&n=" + Server.UrlEncode(Request["n"]);
            strUrlPara += "&r=" + Server.UrlEncode(Request["r"]);

        }

        //分页
        protected string GetPageIndex()
        {
            return Rc.Cloud.Web.Common.PageFuntion.GetPageIndex(PageIndex, PageSize, rCount, pCount, strUrlPara);
        }

        //获得数据
        protected string GetHtmlData()
        {
            DataTable dt = new DataTable();
            StringBuilder condition = new StringBuilder();
            if (txtMark.Text != "")
            {
                condition.Append(" and  DictionarySQlMaintenance_Mark like '%" + txtMark.Text.Trim() + "%'");
            }
            if (txtName.Text != "")
            {
                condition.Append(" and  DictionarySQlMaintenance_Name like '%" + txtName.Text.Trim() + "%'");
            }
            dt = BLL.DictionarySQlMaintenanceList(condition.ToString(), PageIndex, PageSize, out  rCount, out  pCount).Tables[0];
            int i = 0;
            StringBuilder strHtmlData = new StringBuilder();
            strHtmlData.Append("<table class='table table-hover table-bordered'>");
            strHtmlData.Append("<thead>");
            strHtmlData.Append("<tr>");
            strHtmlData.Append("<td>标识</td>");
            strHtmlData.Append("<td>名称</td>");
            strHtmlData.Append("<td>说明</td>");
            strHtmlData.Append("<td>SQL语句</td>");
            strHtmlData.Append("<td>操作</td>");
            strHtmlData.Append("</tr>");
            strHtmlData.Append("</thead>");
            strHtmlData.Append("<tbody>");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                i++;
                strHtmlData.Append("<tr>");
                strHtmlData.Append("<td>" + dt.Rows[j]["DictionarySQlMaintenance_Mark"] + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["DictionarySQlMaintenance_Name"] + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["DictionarySQlMaintenance_Explanation"] + "</td>");
                strHtmlData.Append("<td>" + Rc.Cloud.Web.Common.pfunction.SplitStr(Rc.Common.StrUtility.clsUtility.Decrypt(dt.Rows[j]["DictionarySQlMaintenance_SQL"].ToString()), 60) + "</td>");
                strHtmlData.Append("<td class='opera'>");
                strHtmlData.AppendFormat("<a href='javascript:void(0)' onclick=\"showPopAddOrEdit('{0}');\" />编辑</a>", Rc.Common.StrUtility.clsUtility.Encrypt(dt.Rows[j]["DictionarySQlMaintenance_ID"].ToString()));
                strHtmlData.AppendFormat("<a href='javascript:void(0)' onclick=\"DeleteItemDesc('{0}');\" />删除</a>", dt.Rows[j]["DictionarySQlMaintenance_ID"]);
                strHtmlData.Append("</td>");
                strHtmlData.Append("</tr>");
            }
            strHtmlData.Append("</tbody>");
            strHtmlData.Append("</table>");
            if (i == 0)
            {
                strHtmlData.Append("<div class='nodata'>暂无数据</div>");
            }
            return strHtmlData.ToString();
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;
            strUrlPara += "&n=" + Rc.Common.StrUtility.clsUtility.Encrypt(this.txtName.Text.Trim());
            strUrlPara += "&r=" + Rc.Common.StrUtility.clsUtility.Encrypt(this.txtMark.Text.Trim());
            Response.Redirect(strUrlPara);
        }

        protected void tooutline(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            StringBuilder condition = new StringBuilder();
            if (txtMark.Text != "")
            {
                condition.Append(" and  DictionarySQlMaintenance_Mark like '%" + txtMark.Text.Trim() + "%'");
            }
            if (txtName.Text != "")
            {
                condition.Append(" and  DictionarySQlMaintenance_Name like '%" + txtName.Text.Trim() + "%'");
            }
            dt = BLL.DictionarySQlMaintenanceList(condition.ToString()).Tables[0];

            StringBuilder sb = new StringBuilder();

            foreach (DataRow inrow in dt.Rows)
            {
                sb.AppendFormat(@"INSERT INTO [DictionarySQlMaintenance]
                                 VALUES ('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}' ,'{7}' ,'{8}')
                            ", inrow[0], inrow[1], inrow[2], inrow[3], inrow[4], inrow[5], inrow[6], inrow[7], inrow[8]);
                sb.Append("\r\n");
            }

            foreach (DataRow inrow in dt.Rows)
            {
                sb.AppendFormat(@"update dbo.DictionarySQlMaintenance set DictionarySQlMaintenance_Mark='{1}',
                                                                          DictionarySQlMaintenance_Name='{2}',
                                                                          DictionarySQlMaintenance_Explanation='{3}',
                                                                          DictionarySQlMaintenance_SQL='{4}',
                                                                          DictionarySQlMaintenance_CretateUser='{5}',
                                                                          DictionarySQlMaintenance_CreateTime='{6}',
                                                                          DictionarySQlMaintenance_UpdateUser='{7}',
                                                                          DictionarySQlMaintenance_UpdateTime='{8}'
                                                                          where  DictionarySQlMaintenance_ID='{0}'",
                                inrow[0], inrow[1], inrow[2], inrow[3], inrow[4], inrow[5], inrow[6], inrow[7], inrow[8]);
                sb.Append("\r\n");
            }

            Response.AddHeader("Content-Disposition", "attachment;filename= 智能控件.sql");
            //Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            //Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");

            Response.Write("\r\n" + sb);
            //Response.Flush();
            Response.End();
        }
    }
}