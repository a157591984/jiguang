using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Rc.Cloud.BLL;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysUserFunction : Rc.Cloud.Web.Common.InitPage
    {

        BLL_SysUser BLL = new BLL_SysUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90305000";
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
                txtDoctorInfoName.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request["n"].ToString());
                
            }
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
            strUrlPara += "&n=" + Server.UrlEncode(Request["n"]);
        }

        protected string GetPageIndex()
        {
            return Rc.Cloud.Web.Common.PageFuntion.GetPageIndex(PageIndex, PageSize, rCount, pCount, strUrlPara);
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;
            strUrlPara += "&n=" + Rc.Common.StrUtility.clsUtility.Encrypt(txtDoctorInfoName.Text.Trim());
            Response.Redirect(strUrlPara);
          
        }

        protected string GetHtmlData()
        {
            try
            {
                //var modelList = new  MS.Authority.BLL.BLL_VDoctorInfo().GetModelList(txtDoctorInfoName.Text.Trim(), PageIndex, PageSize, out  rCount, out  pCount);
                DataTable dt = new DataTable();
                dt = BLL.GetSysUserListByName(txtDoctorInfoName.Text.Trim(), PageIndex, PageSize, out  rCount, out  pCount).Tables[0];
                int i = 0;

                StringBuilder strHtmlData = new StringBuilder();
                strHtmlData.Append("<table class='table_list' cellpadding='0' cellspacing='0' >");
                strHtmlData.Append("<tr class='tr_title'>");
                strHtmlData.Append("<td class='pa'>登录名</td>");
                strHtmlData.Append("<td style='width:15%;'>用户姓名</td>");
                strHtmlData.Append("<td >角色</td>");
                strHtmlData.Append("<td style='width:15%;'>电话</td>");
                strHtmlData.Append("<td style='width:15%;'>描述</td>");
                strHtmlData.Append("<td style='width:10%;'>操作</td>");
                strHtmlData.Append("</tr>");
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    i++;
                    string css = string.Empty;
                    if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                    strHtmlData.Append("<tr class='" + css + "'>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["SysUser_LoginName"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["SysUser_Name"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + GetSysRole_Name(dt.Rows[j]["SysUser_ID"].ToString()) + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["SysUser_Tel"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["SysUser_Remark"].ToString() + "</td>");
                    strHtmlData.Append("<td  class='cc'>");
                    strHtmlData.Append("<a href='SysUserModuleFunction.aspx?id=" + dt.Rows[j]["SysUser_ID"].ToString() + "");
                    strHtmlData.Append("&dname=" + Server.UrlEncode(dt.Rows[j]["SysUser_Name"].ToString()) + "");
                    strHtmlData.Append("&iurl=" + Server.UrlEncode(Request.Url.ToString()) + "");
                    strHtmlData.Append("'");
                    strHtmlData.Append(">设置权限</a></td>");
                    strHtmlData.Append("  ");
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

        protected void btnClearCatch_Click(object sender, EventArgs e)
        {
            Rc.Common.StrUtility.clsUtility.DelCacheKeyByPrefix("Owen_");
        }

         /// <summary>
        /// 获得角色名称
        /// </summary>
        /// <param name="SysUser_ID"></param>
        /// <returns></returns>
        public string GetSysRole_Name(string SysUser_ID)
        {
            return BLL.GetSysRole_Name(SysUser_ID);
        }

    }
}