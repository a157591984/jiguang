using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;
using Rc.Cloud.Web.Common;
using System.Text;
using System.Data;

namespace Rc.Cloud.Web.Sys
{
    public partial class TeacherAudit : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90100100";

            UserFun = new BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, Rc.Common.StrUtility.clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), Module_Id);

            if (!IsPostBack)
            {
                SetSearchO();
            }
            SetSearchP();
        }

        //给查询对象附值
        private void SetSearchO()
        {


            if (!string.IsNullOrEmpty(Request["name"]))
            {
                txtName.Text = Request["name"].ToString();
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
            string name = string.IsNullOrEmpty(Request["name"]) ? "" : Request["name"].ToString();

            strUrlPara += "&name=" + Server.UrlEncode(name);



        }
        protected string GetPageIndex()
        {
            return PageFuntion.GetPageIndex(PageIndex, PageSize, rCount, pCount, strUrlPara);
        }

        protected string GetHtmlData()
        {
            try
            {
                StringBuilder strSql = new StringBuilder();

                if (!string.IsNullOrEmpty(Request["name"]))
                {
                    strSql.Append(" and (SysDepartment_Name like '" + Request["name"].ToString() + "%')");
                }

                BLL_SysDepartment bll = new BLL_SysDepartment();
                DataTable dt = bll.GetListPaged(strSql.ToString(), PageIndex, PageSize, out rCount, out pCount).Tables[0];

                StringBuilder strHtmlData = new StringBuilder();
                strHtmlData.Append("<table class='table table-hover table-bordered'>");
                strHtmlData.Append("<thead>");
                strHtmlData.Append("<tr>");
                strHtmlData.Append("<th>部门名称</th>");
                strHtmlData.Append("<th>部门负责人</th>");
                strHtmlData.Append("<th>备注</th>");
                //if (UserFun.Edit)
                //{
                strHtmlData.Append("<th>操作</th>");
                //}
                strHtmlData.Append("</tr>");
                strHtmlData.Append("</thead>");
                strHtmlData.Append("<tbody>");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string css = string.Empty;
                    strHtmlData.Append("<tr>");
                    strHtmlData.Append("<td>" + dt.Rows[i]["SysDepartment_Name"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[i]["SysUser_Name"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[i]["SysDepartment_Remark"].ToString() + "</td>");
                    strHtmlData.Append("<td class='opera'>");
                    if (UserFun.Edit)
                    {
                        strHtmlData.AppendFormat("<a href='javascript:;' onclick=\"showPopAddDepartment('1','{0}');\" />编辑</a>", dt.Rows[i]["SysDepartment_ID"].ToString());
                    }
                    else
                    {
                        strHtmlData.Append("<a href='javascript:;' class='disabled'>编辑</a>");
                    }
                    if (UserFun.Delete)
                    {
                        strHtmlData.AppendFormat("<a href='javascript:;' onclick=\"Delete('{0}')\" />删除</a>", dt.Rows[i]["SysDepartment_ID"].ToString());
                    }
                    else
                    {
                        strHtmlData.Append("<a href='javascript:;' class='disabled'>删除</a>");
                    }
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("</tr>");
                }
                strHtmlData.Append("</tbody>");
                strHtmlData.Append("</table>");
                if (dt.Rows.Count <= 0)
                {
                    strHtmlData.Append("<div class='nodata'>暂无数据");
                }
                return strHtmlData.ToString();
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }
        protected void btn_Search_Click(object sender, EventArgs e)
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
            strUrlPara += "&name=" + Server.UrlEncode(txtName.Text);
            Response.Redirect(strUrlPara);
        }
    }
}