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
    public partial class BasicRole : Rc.Cloud.Web.Common.InitPage
    {

        protected string strPath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            strPath = Request.Url.ToString();
            Module_Id = "90100200";
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
                strHtmlData.Append("<table class='table table-hover table-bordered'>");
                strHtmlData.Append("<thead>");
                strHtmlData.Append("<tr>");
                strHtmlData.Append("<th>角色名称</th>");
                strHtmlData.Append("<th>创建时间</th>");
                strHtmlData.Append("<th>操作</th>");
                strHtmlData.Append("</tr>");
                strHtmlData.Append("</thead>");
                strHtmlData.Append("<tbody>");
                foreach (var item in modelList)
                {
                    i++;
                    string css = string.Empty;
                    strHtmlData.Append("<tr>");
                    strHtmlData.Append("<td>" + item.SysRole_Name + "</td>");
                    strHtmlData.Append("<td>" + (item.CreateTime.HasValue ? item.CreateTime.Value.ToShortDateString() : "") + "</td>");
                    strHtmlData.Append("<td class='opera'>");
                    strHtmlData.AppendFormat("<a href='SysRoleModuleFunction.aspx?id={0}&dname={1}&iurl={2}'>设置权限</a>"
                        , item.SysRole_ID
                        , Server.UrlEncode(item.SysRole_Name)
                        , Server.UrlEncode(Request.Url.ToString()));
                    strHtmlData.AppendFormat("<a href='javascript:;' onclick=\"showPop('{0}','{1}');\">编辑</a>"
                        , item.SysRole_ID
                        , Server.UrlEncode(item.SysRole_Name));
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