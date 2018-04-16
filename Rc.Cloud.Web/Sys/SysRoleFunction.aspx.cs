using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Rc.Cloud.BLL;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysRoleFunction : Rc.Cloud.Web.Common.InitPage
    {
        //
        protected string strPath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            strPath = Request.Url.ToString();
            Module_Id = "90306000";
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
                txtRoleName.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request["n"].ToString());
            }
        }

        //设置URL参数
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

      

        //查询
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;
            strUrlPara += "&n=" + Rc.Common.StrUtility.clsUtility.Encrypt(txtRoleName.Text.Trim());
            Response.Redirect(strUrlPara);
        }

        //得到角色列表
        protected string GetHtmlData()
        {
            try
            {
                Rc.Cloud.Model.Model_SysRoleParameter parameter = new Rc.Cloud.Model.Model_SysRoleParameter();
                parameter.MODEL_SysRole.SysRole_Name = txtRoleName.Text.Trim();
                var modelList = new Rc.Cloud.BLL.BLL_SysRole().GetSysRoleModelList(parameter, PageIndex, PageSize, out  rCount, out  pCount);
                int i = 0;
                StringBuilder strHtmlData = new StringBuilder();
                strHtmlData.Append("<table class='table_list' cellpadding='0' cellspacing='0' >");
                strHtmlData.Append("<tr class='tr_title'>");
                strHtmlData.Append("<td >角色名称</td>");
                strHtmlData.Append("<td >创建时间</td>");
                strHtmlData.Append("<td style='width:10%;'>操作</td>");
                strHtmlData.Append("</tr>");
                foreach (var item in modelList)
                {
                    i++;
                    string css = string.Empty;
                    if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                    strHtmlData.Append("<tr class='" + css + "'>");
                    strHtmlData.Append("<td>" + item.SysRole_Name + "</td>");
                    strHtmlData.Append("<td>" + (item.CreateTime.HasValue ? item.CreateTime.Value.ToShortDateString() : "") + "</td>");
                    strHtmlData.Append("<td class='cc'>");
                    strHtmlData.Append("<a href='SysRoleModuleFunction.aspx?id=" + item.SysRole_ID + "");
                    strHtmlData.Append("&dname=" + Server.UrlEncode(item.SysRole_Name) + "");
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

        //分页
        protected string GetPageIndex()
        {
            return Rc.Cloud.Web.Common.PageFuntion.GetPageIndex(PageIndex, PageSize, rCount, pCount, strUrlPara);
        }
    }
}