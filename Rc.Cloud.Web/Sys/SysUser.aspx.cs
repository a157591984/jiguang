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
using Rc.Common.Config;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysUser : Rc.Cloud.Web.Common.InitPage
    {
        SysUserBLL BLL = new SysUserBLL();
        protected string ReturnUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90100300";
            SetSearchP();
            ReturnUrl = strPageName + "?" + Rc.Cloud.Web.Common.pfunction.getPageParam();
            //得到用户在此页面的权限
            //UserFun = MS.Authority.clsAuth.GetUserFunc(sysLoginUser.sysUser_ID, PHHC.Share.StrUtility.clsUtility.ReDoStr(sysLoginUser.SysRole_IDs, ','), Module_Id);
            UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), Module_Id);
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
            if (Consts.AdminID != loginUser.SysUser_ID)
            {
                condition.AppendFormat(" and sysUser_ID<>'{0}'", Consts.AdminID);
            }

            StringBuilder strHtmlData = new StringBuilder();
            try
            {

                dt = BLL.GetUserList(condition.ToString(), PageIndex, PageSize, out  rCount, out  pCount).Tables[0];
                int i = 0;
                strHtmlData.Append("<table class='table table-hover table-bordered'>");
                strHtmlData.Append("<thead>");
                strHtmlData.Append("<tr>");
                strHtmlData.Append("<th>登录名</th>");
                strHtmlData.Append("<th>用户名</th>");
                strHtmlData.Append("<th>角色名称</th>");
                strHtmlData.Append("<th>更新时间</th>");
                strHtmlData.Append("<th>操作</th>");
                strHtmlData.Append("</tr>");
                strHtmlData.Append("</thead>");
                strHtmlData.Append("<tbody>");
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    i++;
                    string css = string.Empty;
                    strHtmlData.Append("<tr>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["SysUser_LoginName"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["SysUser_Name"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + GetSysRole_Name(dt.Rows[j]["SysUser_ID"].ToString()) + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["UpdateTime"].ToString() + "</td>");
                    strHtmlData.Append("<td class='opera'>");
                    if (UserFun.Check)
                    {
                        strHtmlData.AppendFormat("<a href='SysUserModuleFunction.aspx?id={0}&dname={1}&iurl={2}'>设置权限</a>"
                            , dt.Rows[j]["SysUser_ID"].ToString()
                            , Server.UrlEncode(dt.Rows[j]["SysUser_Name"].ToString())
                            , Server.UrlEncode(Request.Url.ToString()));
                    }
                    strHtmlData.AppendFormat("<a href='javascript:;' onclick=\"showPopAddUser('{0}');\" />编辑</a>", clsUtility.Encrypt(dt.Rows[j]["SysUser_ID"].ToString()));
                    strHtmlData.AppendFormat("<a href='javasctipt:;' onclick=\"DeleteItemDesc('{0}')\" />删除</a>", clsUtility.Encrypt(dt.Rows[j]["SysUser_ID"].ToString()));
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("</tr>");
                }
                strHtmlData.Append("</tbody>");
                strHtmlData.Append("</table>");
                if (i == 0)
                {
                    strHtmlData.Append(" <div class='nodata'>暂无数据</div>");
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
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
        public string GetSysRole_Name(string SysUser_ID)
        {
            return BLL.GetSysRole_Name(SysUser_ID);
        }
    }
}