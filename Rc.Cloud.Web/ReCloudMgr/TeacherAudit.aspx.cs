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

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class TeacherAudit : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10301000";

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
                    strSql.Append(" and (UserName like '" + Request["name"].ToString() + "%')");
                }

                BLL_F_User bll = new BLL_F_User();
                DataTable dt = bll.GetListPaged(strSql.ToString(), PageIndex, PageSize, out rCount, out pCount).Tables[0];

                StringBuilder strHtmlData = new StringBuilder();
                strHtmlData.Append("<table class='table_list' cellpadding='0' cellspacing='0' >");
                strHtmlData.Append("<tr class='tr_title'>");
                strHtmlData.Append("<td style='width:8%;'>账号</td>");
                strHtmlData.Append("<td style='width:8%;'>真实姓名</td>");
                strHtmlData.Append("<td style='width:8%;'>邮箱</td>");
                strHtmlData.Append("<td style='width:8%;'>手机</td>");
                //if (UserFun.Edit)
                //{
                strHtmlData.Append("<td style='width:8%;'>操作</td>");
                //}
                strHtmlData.Append("</tr>");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string css = string.Empty;
                    if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                    strHtmlData.Append("<tr class='" + css + "'>");
                    strHtmlData.Append("<td>" + dt.Rows[i]["UserName"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[i]["TrueName"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[i]["Email"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[i]["Mobile"].ToString() + "</td>");
                    strHtmlData.Append("<td>");
                    if (UserFun.Edit)
                    {

                        strHtmlData.AppendFormat("&nbsp;&nbsp;<input type=\"button\" title='编辑用户信息' class=\"btn_modify\" onclick=\"showPopAddDepartment('{0}',1);\" />", dt.Rows[i]["UserId"].ToString());

                    }

                    else
                    {

                        strHtmlData.Append("&nbsp;&nbsp;<input type=\"button\" title='没有权限编辑用户信息' class=\"btn_modify2\" />");
                    }
                    strHtmlData.AppendFormat("|");
                    if (UserFun.Delete)
                    {
                        strHtmlData.AppendFormat("<input type=\"button\" class=\"btn_delete\" title='删除用户信息' onclick=\"Delete('{0}')\" />", dt.Rows[i]["UserId"].ToString());

                    }
                    else
                    {
                        strHtmlData.Append("&nbsp;&nbsp;<input type=\"button\" title='没有权限删除用户信息' class=\"btn_delete2\" />");
                    }
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("</tr>");
                }

                if (dt.Rows.Count <= 0)
                {
                    strHtmlData.Append(" <tr><td colspan=\"8\"><div class='nodata_div'>暂无数据!</td></td></tr>");
                }

                strHtmlData.Append("</table>");

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