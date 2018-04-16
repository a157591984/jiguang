using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Rc.Cloud.BLL;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.Sys
{
    public partial class SystemLogList : Rc.Cloud.Web.Common.InitPage
    {
        private readonly BLL_SystemLog BLL = new BLL_SystemLog();
        public string name;
        protected string iurl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90400100";
            SetSearchP();
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
                txtRoleName.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["n"]);
            }
            if (!string.IsNullOrEmpty(Request["p"]))
            {
                txtContent.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["p"]);
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
        protected string selectAllSystemLogModel()
        {
            //var items = BLL.SelectAllSystemLogModel(this.txtContent.Text.Trim(), this.txtRoleName.Text.Trim(), PageIndex, PageSize, out  rCount, out  pCount,null).Tables[0];
            var items = BLL.SelectAllSystemLogModel(this.txtContent.Text.Trim(), this.txtRoleName.Text.Trim(), PageIndex, PageSize, out  rCount, out  pCount).Tables[0];
            return geSystemLogValue(items);
        }

        private static string geSystemLogValue(DataTable items)
        {
            try
            {
                StringBuilder strHtmlData = new StringBuilder();
                strHtmlData.Append("<table class='table table-hover table-bordered'>");
                strHtmlData.Append("<thead>");
                strHtmlData.Append("<tr>");
                strHtmlData.Append("<th>IP地址</th>");
                strHtmlData.Append("<th>页面路径</th>");
                strHtmlData.Append("<th>系统路径</th>");
                strHtmlData.Append("<th width='50%'>内容</th>");
                strHtmlData.Append("<th>操作人</th>");
                strHtmlData.Append("<th>时间</th>");
                strHtmlData.Append("<th>操作</th>");
                strHtmlData.Append("</tr>");
                strHtmlData.Append("</thead>");
                strHtmlData.Append("<tbody>");

                for (int i = 0; i < items.Rows.Count; i++)
                {
                    strHtmlData.Append("<tr>");
                    strHtmlData.Append("<td>" + items.Rows[i]["SystemLog_IP"].ToString() + "</td>");

                    strHtmlData.Append("<td title='" + items.Rows[i]["SystemLog_Level"].ToString() + "'>");
                    strHtmlData.Append(Rc.Cloud.Web.Common.pfunction.SplitStr(items.Rows[i]["SystemLog_Level"].ToString(), 20));
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("<td title='" + items.Rows[i]["SystemLog_Model"].ToString() + "'>");
                    strHtmlData.Append(Rc.Cloud.Web.Common.pfunction.SplitStr(items.Rows[i]["SystemLog_Model"].ToString(), 15));
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("<td title='" + items.Rows[i]["SystemLog_Desc"].ToString() + "'>");
                    //strHtmlData.Append(Rc.Cloud.Web.Common.pfunction.SplitStr(items.Rows[i]["SystemLog_Desc"].ToString(), 20));
                    strHtmlData.Append(items.Rows[i]["SystemLog_Desc"].ToString());
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("<td");
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("<td>" + items.Rows[i]["SysUser_Name"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + items.Rows[i]["SystemLog_CreateDate"].ToString() + "</td>");
                    strHtmlData.Append("<td class='opera'>");
                    strHtmlData.AppendFormat("<a href='javascript:;' onclick=\"showInfo('{0}',2,'{1}');\">查看</a>", clsUtility.Encrypt(items.Rows[i]["SystemLog_ID"].ToString()), items.Rows[i]["SysUser_Name"].ToString());
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("</tr>");
                }
                strHtmlData.Append("</tbody>");
                strHtmlData.Append("</table>");
                if (items.Rows.Count == 0)
                {
                    strHtmlData.Append("<div class='nodata'>暂无数据</div>");
                }
                return strHtmlData.ToString();
            }
            catch (Exception ex)
            {
                ////MS.Authority.clsAuth.AddLogErrorFromBS("Sys/SystemLogList.aspx", string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;
            strUrlPara += "&n=" + Rc.Common.StrUtility.clsUtility.Encrypt(this.txtRoleName.Text.Trim()) + "&p=" + Rc.Common.StrUtility.clsUtility.Encrypt(txtContent.Text.Trim());
            Response.Redirect(strUrlPara);
        }

    }
}