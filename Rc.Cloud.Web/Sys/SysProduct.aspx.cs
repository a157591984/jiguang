using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;
using System.Data;
using System.Text;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysProduct : Rc.Cloud.Web.Common.InitPage
    {
        BLL_SysProduct BLL = new BLL_SysProduct();
        protected string ReturnUrl = string.Empty;
         
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90301000";
            SetSearchP();
            ReturnUrl = strPageName + "?" + Rc.Cloud.Web.Common.pfunction.getPageParam();
            //得到用户在此页面的权限
            //UserFun = MS.Authority.clsAuth.GetUserFunc(loginUser.DoctorInfo_ID, PHHC.Share.clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), Module_Id);
            //查询
            if (!IsPostBack)
            {
                SetSearchO();
            }
        }

        //给查询对象附值
        private void SetSearchO()
        {
            if (Request.QueryString["p"] != null && Request.QueryString["p"] != "")
                txtSysCodeName.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["p"]);

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
            strUrlPara += "&p=" + Server.UrlEncode(Request.QueryString["p"]);


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
            if (!string.IsNullOrEmpty(txtSysCodeName.Text.Trim()))
            {
                condition.AppendFormat(" and (SysName like '%{0}%')", txtSysCodeName.Text.Trim());
            }
            dt = BLL.GetCodeList(condition.ToString(), PageIndex, PageSize, out  rCount, out  pCount).Tables[0];
            int i = 0;
            StringBuilder strHtmlData = new StringBuilder();
            strHtmlData.Append("<table class='table_list' cellpadding='0' cellspacing='0' >");
            strHtmlData.Append("<tr class='tr_title'>");
            strHtmlData.Append("<th style='width:20%;'>系统编码</th>");
            strHtmlData.Append("<th>系统名称</th>");
            strHtmlData.Append("<th style='width:15%;'>排    序</th>");
            strHtmlData.Append("<th style='width:20%;'>操    作</th>");
            strHtmlData.Append("</tr>");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                i++;
                string css = string.Empty;
                if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                strHtmlData.Append("<tr class='" + css + "'>");
                strHtmlData.Append("<td>" + dt.Rows[j]["SysCode"].ToString() + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["SysName"].ToString() + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["SysOrder"].ToString() + "</td>");
                strHtmlData.Append("<td>");
                strHtmlData.AppendFormat("&nbsp;&nbsp;<input type=\"button\" title='编辑系统信息' class=\"btn_modify\" onclick=\"showPopSysCode('{0}',2);\" />", Rc.Common.StrUtility.clsUtility.Encrypt(dt.Rows[j]["SysCode"].ToString()));
                strHtmlData.AppendFormat("|");
                strHtmlData.AppendFormat("<input type=\"button\" class=\"btn_delete\" title='删除系统信息' onclick=\"DeleteItemDesc('{0}')\" />", Rc.Common.StrUtility.clsUtility.Encrypt(dt.Rows[j]["SysCode"].ToString()));
                strHtmlData.Append("</td>");
                strHtmlData.Append("</tr>");
            }
            strHtmlData.Append("</table>");
            if (i == 0)
            {
                strHtmlData.Append(" <div class='nodata_div'>暂无数据</div>");
            }
            return strHtmlData.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;

            strUrlPara += "&p=" + Rc.Common.StrUtility.clsUtility.Encrypt(txtSysCodeName.Text.Trim());

            Response.Redirect(strUrlPara);
        }

    }
}