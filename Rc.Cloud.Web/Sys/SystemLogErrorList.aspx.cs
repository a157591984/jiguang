using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Rc.Cloud.BLL;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.Sys
{
    public partial class SystemLogErrorList : Rc.Cloud.Web.Common.InitPage
    {
        SystemLogErrorBLL BLL = new SystemLogErrorBLL();
        public string name;
        protected string iurl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90207000";
            SetSearchP();
            if (!IsPostBack)
            {
                SetSearchO();
            }

        }
        //给查询对象附值
        private void SetSearchO()
        {
            if (!string.IsNullOrEmpty(Request["n"]) || !string.IsNullOrEmpty(Request["p"]))
            {
                txtRoleName.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["n"]);
                txtErrorContent.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["p"]);

            }
        }
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
            strUrlPara += "&p=" + Server.UrlEncode(Request["p"]);
        }
        protected string GetPageIndex()
        {
            return Rc.Cloud.Web.Common.PageFuntion.GetPageIndex(PageIndex, PageSize, rCount, pCount, strUrlPara);
        }
        protected string selectAllSystemLogErrorModel()
        {
            var items = BLL.selectAllSystemLogErrorModel(this.txtErrorContent.Text.Trim(), this.txtRoleName.Text.Trim(), PageIndex, PageSize, out  rCount, out  pCount).Tables[0];
            return geSystemLogValue(items);
        }

        private static string geSystemLogValue(DataTable items)
        {
            try
            {
                StringBuilder strHtmlData = new StringBuilder();
                strHtmlData.Append("<table class='table_list' cellpadding='0' cellspacing='0' >");
                strHtmlData.Append("<tr class='tr_title'>");
                strHtmlData.Append("<td style='width:120px;'>IP地址</td>");
                strHtmlData.Append("<td style='width:20%;'>操作页面路径</td>");
                strHtmlData.Append("<td style='width:20%;'>操作系统路径</td>");
                strHtmlData.Append("<td>错误内容</td>");
                strHtmlData.Append("<td style='width:50px;'>操作人</td>");
                strHtmlData.Append("<td style='width:15%;'>操作时间</td>");
                strHtmlData.Append("<td style='width:80px;'>查看</td>");
                strHtmlData.Append("</tr>");

                for (int i = 0; i < items.Rows.Count; i++)
                {
                    string css = string.Empty;

                    if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                    strHtmlData.Append("<tr class='" + css + "'>");
                    strHtmlData.Append("<td>" + items.Rows[i]["SystemLog_IP"].ToString() + "</td>");
                    strHtmlData.Append("<td title='" + items.Rows[i]["SystemLog_PagePath"].ToString() + "'>");
                    strHtmlData.Append(Rc.Cloud.Web.Common.pfunction.SplitStr(items.Rows[i]["SystemLog_PagePath"].ToString(), 20));
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("<td title='" + items.Rows[i]["SystemLog_SysPath"].ToString() + "'>");
                    strHtmlData.Append(Rc.Cloud.Web.Common.pfunction.SplitStr(items.Rows[i]["SystemLog_SysPath"].ToString(), 14));
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("<td title='" + items.Rows[i]["SystemLog_Desc"].ToString() + "'>");
                    strHtmlData.Append(Rc.Cloud.Web.Common.pfunction.SplitStr(items.Rows[i]["SystemLog_Desc"].ToString(), 18));
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("<td>" + items.Rows[i]["SysUser_Name"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + items.Rows[i]["SystemLog_CreateDate"].ToString() + "</td>");
                    strHtmlData.Append("<td>");
                    strHtmlData.AppendFormat("&nbsp;&nbsp;&nbsp;<input type=\"button\" title='查看' class=\"btn_modify\" onclick=\"showInfo('{0}',2,'{1}');\" />", clsUtility.Encrypt(items.Rows[i]["SystemLog_ID"].ToString()), items.Rows[i]["SysUser_Name"].ToString());
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("</tr>");
                }
                strHtmlData.Append("</table>");
                if (items.Rows.Count == 0)
                {
                    strHtmlData.Append(" <div class='nodata_div'>暂无数据</div>");
                }
                return strHtmlData.ToString();
            }
            catch (Exception ex)
            {
                ////MS.Authority.clsAuth.AddLogErrorFromBS("Sys/SystemLogErrorList.aspx", string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;
            strUrlPara += "&n=" + Rc.Common.StrUtility.clsUtility.Encrypt(this.txtRoleName.Text.Trim());
            strUrlPara += "&p=" + Rc.Common.StrUtility.clsUtility.Encrypt(this.txtErrorContent.Text.Trim());
            Response.Redirect(strUrlPara);
        }

    }
}