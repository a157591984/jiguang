using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;
using System.Data;
using System.Text;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysSpecial_Dict : Rc.Cloud.Web.Common.InitPage
    {
        protected string ReturnUrl = string.Empty;
        BLL_Common_DictNew BLL = new BLL_Common_DictNew();

        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "30204000";
            SetSearchP();
            ReturnUrl = strPageName + "?" + Rc.Cloud.Web.Common.pfunction.getPageParam();

            if (!IsPostBack)
            {
                DataTable ddlDType = new DataTable();
                ddlDType = BLL.GetD_Type().Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlD_Type, ddlDType, "D_Remark", "D_type");
                SetSearchO();
            }
        }

        //给查询对象附值
        private void SetSearchO()
        {
            if (Request.QueryString["p"] != null && Request.QueryString["p"] != "")
                txtD_Name.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["p"]);
            if (Request.QueryString["a"] != "-1" && Request.QueryString["a"] != null && Request.QueryString["a"] != "")
                ddlD_Type.SelectedValue = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["a"]);

        }

        //url加密
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
            strUrlPara += "&a=" + Server.UrlEncode(Request.QueryString["a"]);
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
            StringBuilder strHtmlData = new StringBuilder();
            try
            {
                if (txtD_Name.Text.Trim() != "")
                {
                    condition.AppendFormat(" and (D_Name like '%{0}%')", txtD_Name.Text.Trim());
                }
                if (ddlD_Type.SelectedValue != "-1")
                {
                    condition.AppendFormat(" and (D_Type='{0}')", ddlD_Type.SelectedValue);
                }
                dt = BLL.GetCommon_DictList(condition.ToString(), PageIndex, PageSize, out  rCount, out  pCount).Tables[0];
                int i = 0;

                strHtmlData.Append("<table class='table_list' cellpadding='0' cellspacing='0' >");
                strHtmlData.Append("<tr class='tr_title'>");
                strHtmlData.Append("<th style='width:18%;'>名称</th>");
                strHtmlData.Append("<th style='width:15%;'>类型</th>");
                strHtmlData.Append("<th>备注</th>");
                strHtmlData.Append("<th style='width:18%;'>修改时间</th>");
                strHtmlData.Append("<th style='width:18%;'>操作</th>");
                strHtmlData.Append("</tr>");
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string date = exchdatatoZh(dt.Rows[j]["D_UpdateTime"].ToString());
                    i++;
                    string css = string.Empty;
                    if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                    strHtmlData.Append("<tr class='" + css + "'>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["D_Name"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + GetD_Name(dt.Rows[j]["D_Type"].ToString()) + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["D_Remark"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + date + "</td>");
                    strHtmlData.Append("<td>");
                    strHtmlData.AppendFormat("&nbsp;&nbsp;<input type=\"button\" title='修改字典信息' class=\"btn_modify\" onclick=\"showPopCommon_Dict('{0}',2);\" />", clsUtility.Encrypt(dt.Rows[j]["Common_Dict_ID"].ToString()));
                    strHtmlData.AppendFormat("|");
                    strHtmlData.AppendFormat("<input type=\"button\" class=\"btn_delete\" title='删除字典信息' onclick=\"DeleteItemDesc('{0}')\" />", clsUtility.Encrypt(dt.Rows[j]["Common_Dict_ID"].ToString()));
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("</tr>");
                }
                strHtmlData.Append("</table>");
                if (i == 0)
                {
                    strHtmlData.Append(" <div class='nodata_div'>暂无数据</div>");
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
            return strHtmlData.ToString();
        }

        private string exchdatatoZh(string p)
        {string resultdata = string.Empty;
            if (p.Length != 0)
            {
                
                p = p.GetSubString(10);
                var databefor = p.Split('/');
                resultdata += databefor[0];
                resultdata += "年";
                resultdata += databefor[1];
                resultdata += "月";
                resultdata += databefor[2];

                var a = resultdata.Split('.');
                resultdata = a[0] + "日";
                return resultdata;
            }
            return resultdata;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;

            strUrlPara += "&p=" + Rc.Common.StrUtility.clsUtility.Encrypt(txtD_Name.Text.Trim());
            strUrlPara += "&a=" + Rc.Common.StrUtility.clsUtility.Encrypt(ddlD_Type.SelectedValue);

            Response.Redirect(strUrlPara);
        }


        public string GetD_Name(string D_Type)
        {
            return BLL.GetD_Name(D_Type) + D_Type;

        }
    }
}