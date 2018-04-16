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
    public partial class SysUser_For_CustomerInfo : Rc.Cloud.Web.Common.InitPage
    {
        SysUserBLL BLL = new SysUserBLL();
        protected string ReturnUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10103000";
            SetSearchP();
            ReturnUrl = strPageName + "?" + Rc.Cloud.Web.Common.pfunction.getPageParam();
            //得到用户在此页面的权限
            //UserFun = MS.Authority.clsAuth.GetUserFunc(sysLoginUser.sysUser_ID, PHHC.Share.StrUtility.clsUtility.ReDoStr(sysLoginUser.SysRole_IDs, ','), Module_Id);

            if (!IsPostBack)
            {
                SetSearchO();
            }
        }

        //给查询对象附值
        private void SetSearchO()
        {
            if (Request.QueryString["p"] != null && Request.QueryString["p"] != "")
                txtsysUserName.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["p"]);

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
            if (txtsysUserName.Text.Trim() != "")
            {
                condition.AppendFormat(" and (sysUser_Name like '%{0}%')", txtsysUserName.Text.Trim());
            }
            dt = BLL.GetUserList(condition.ToString(), PageIndex, PageSize, out  rCount, out  pCount).Tables[0];
            int i = 0;
            StringBuilder strHtmlData = new StringBuilder();
            strHtmlData.Append("<table class='table_list' cellpadding='0' cellspacing='0' >");
            strHtmlData.Append("<tr class='tr_title'>");
            strHtmlData.Append("<th style='width:20%;'>实施专员</th>");
            strHtmlData.Append("<th style='width:20%;'>联系方式</th>");
            strHtmlData.Append("<th>客户</th>");
            strHtmlData.Append("<th style='width:15%;'>设置客户</th>");
            strHtmlData.Append("</tr>");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                i++;
                string css = string.Empty;
                if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                strHtmlData.Append("<tr class='" + css + "'>");
                strHtmlData.Append("<td>" + dt.Rows[j]["SysUser_Name"].ToString() + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["SysUser_Tel"].ToString() + "</td>");
                strHtmlData.Append("<td>" + GetCustomerInfo_NameCN(dt.Rows[j]["SysUser_ID"].ToString()) + "</td>");
                //strHtmlData.Append("<td>" + dt.Rows[j]["SysUser_ID"].ToString() + "</td>");
                strHtmlData.Append("<td style='text-align:center'>");
                strHtmlData.AppendFormat("&nbsp;&nbsp;<input type=\"button\" title='设置客户' class=\"btn_modify\" onclick=\"SetCustomerInfo('{0}','{1}');\" />", clsUtility.Encrypt(dt.Rows[j]["SysUser_ID"].ToString()), dt.Rows[j]["SysUser_Name"].ToString());
                //strHtmlData.AppendFormat("|");                                                                  
//                strHtmlData.AppendFormat(@"<input type='text' id='txtV_Common_Dict_DoctorInfo' readonly='readonly'   
//                    clientidmode='Static'  class='txt' style='width:80%;'  runat='server' />
//                    <input type='hidden' id='hidV_Common_Dict_DoctorInfo' clientidmode='Static' runat='server'  />
//                    <input type='button' title='选择人员' onclick='showCommonDict('txtV_Common_Dict_DoctorInfo','hidV_Common_Dict_DoctorInfo','V001','0|0|0','');'/>");
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

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;

            strUrlPara += "&p=" + Rc.Common.StrUtility.clsUtility.Encrypt(txtsysUserName.Text.Trim());

            Response.Redirect(strUrlPara);
        }

        protected string converTime(string dt)
        {
            if (dt != "" && dt != null)
            {
                string Time = dt.Substring(0, dt.LastIndexOf(' '));

                return Time.Replace("/", "-");
            }
            return null;
        }

        /// <summary>
        /// 获得角色名称
        /// </summary>
        /// <param name="SysUser_ID"></param>
        /// <returns></returns>
        public string GetCustomerInfo_NameCN(string SysUser_ID)
        {
            return BLL.GetCustomerInfo_NameCN(SysUser_ID);           
        }
    }
}