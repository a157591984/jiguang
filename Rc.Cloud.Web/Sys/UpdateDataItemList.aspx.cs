using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.Model;
using Rc.Cloud.BLL;
using System.Data;
using System.Text;

namespace Rc.Cloud.Web.Sys
{
    public partial class UpdateDataItemList : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "30301000";
            UserFun = new BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, Rc.Common.StrUtility.clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), Module_Id);
            SetSearchP();
            if (!IsPostBack)
            {
                SetSearchO();
            }
        }

        //给查询对象附值
        private void SetSearchO()
        {
            txtCName.Text = Request.QueryString["cN"];
        }

        private void SetSearchP()
        {
            if (!string.IsNullOrEmpty(Request["PageIndex"]))
            {
                if (!int.TryParse(Request["PageIndex"].ToString(), out PageIndex)) { PageIndex = 1; }
                if (PageIndex <= 0) { PageIndex = 1; }
                //if (PageIndex >= pCount) { PageIndex = pCount; }
            }
            if (!string.IsNullOrEmpty(Request["PageSize"]))
            {
                if (!int.TryParse(Request["PageSize"].ToString(), out PageSize)) { PageSize = 10; }
                //if (PageSize <= 0) { PageSize = 10; }
            }

            strUrlPara = strPageName + "?PageIndex={0}&PageSize={1}";
            strUrlPara += "&cN=" + Server.UrlEncode(Request.QueryString["cN"]);

        }

        protected string GetPageIndex()
        {
            return Rc.Cloud.Web.Common.PageFuntion.GetPageIndex(PageIndex, PageSize, rCount, pCount, strUrlPara);
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;
            strUrlPara += "&cN=" + txtCName.Text.Trim();
            Response.Redirect(strUrlPara);
        }

        protected string GetHtmlData()
        {
            try
            {

                Common_DictBLL bll = new Common_DictBLL();
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                if (!string.IsNullOrEmpty(txtCName.Text.Trim()))
                {
                    strSql = " and D_Remark like '" + txtCName.Text.Trim() + "%'";
                }
                strSql += " and d_type=43 ";
                dt = bll.GetCommon_DictList(strSql, PageIndex, PageSize, out  rCount, out  pCount).Tables[0];

                int i = 0;
                StringBuilder strHtmlData = new StringBuilder();
                strHtmlData.Append("<table class='table_list' cellpadding='0' cellspacing='0' >");
                strHtmlData.Append("<tr class='tr_title'>");
                strHtmlData.Append("<td style='width:30%;'>数据项名称</td>");
                strHtmlData.Append("<td style='width:30%;'>表名称</td>");
                strHtmlData.Append("<td style='width:30%;'>唯一ID</td>");
                strHtmlData.Append("<td style='width:10%;'>操作</td>");
                strHtmlData.Append("</tr>");

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    i++;
                    string css = string.Empty;
                    if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                    strHtmlData.Append("<tr class='" + css + "'>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["D_Remark"] + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["d_name"] + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["d_code"] + "</td>");

                    strHtmlData.Append("<td><input type=\"button\" title='编辑数据项信息' class=\"btn_modify\" onclick=\"showUpdateDataItemEdit('" + dt.Rows[j]["common_dict_id"] + "')\"/>|");
                    strHtmlData.Append("<input type=\"button\" title='删除数据项信息' class=\"btn_delete\" onclick=\"DeleteUpdateDataItem('" + dt.Rows[j]["common_dict_id"] + "')\"/>");
                    strHtmlData.Append("</td>  ");

                    strHtmlData.Append("</tr>");
                }

                strHtmlData.Append("</table>");
                if (i == 0)
                {
                    strHtmlData.Append(" <div class='nodata_div'>暂无数据</div>");
                }
                return strHtmlData.ToString();
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }
    }
}