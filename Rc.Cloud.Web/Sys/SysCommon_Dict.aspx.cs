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
    public partial class SysCommon_Dict : Rc.Cloud.Web.Common.InitPage
    {
        Common_DictBLL BLL = new Common_DictBLL();
        protected string ReturnUrl = string.Empty;
        protected string SysName = Rc.Common.ConfigHelper.GetConfigString("SysName");
        Rc.Cloud.Model.Model_Struct_Func UserFun;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90300100";
            UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), Module_Id);
            SetSearchP();
            ReturnUrl = strPageName + "?" + Rc.Cloud.Web.Common.pfunction.getPageParam();
            //if (Rc.Common.ConfigHelper.GetConfigString("SysName") != "内容生产管理平台")
            //{
            //    btnAdd.Visible = false;
            //}
            btnAdd.Visible = UserFun.Add;
            if (!IsPostBack)
            {
                DataTable ddlDType = new DataTable();

                StringBuilder condition = new StringBuilder();

                if (loginUser.SysUser_ID != Rc.Common.Config.Consts.AdminID)
                {
                    //非级管理员按类型排除加载数据（教材版本，年级学期，学科，前端题型）
                    condition.AppendFormat("  Common_Dict_ID in ('{0}','{1}','{2}','{3}')"
                       , "74958B74-D2A4-4ACD-BB4E-F48C59329F40"
                       , "722CE025-A876-4880-AAC1-5E416F3BDB1E"
                       , "934A3541-116E-438C-B9BA-4176368FCD9B"
                       , "3EF9506E-4C4B-407E-AA5D-451E0B20F0DI");
                }
                else
                {
                    condition.AppendFormat(" D_Type='{0}'", 0);
                }
                ddlDType = new Rc.BLL.Resources.BLL_Common_Dict().GetList(condition.ToString()).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlD_Type, ddlDType, "D_Name", "D_Value", false);
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
                if (loginUser.SysUser_ID != Rc.Common.Config.Consts.AdminID)
                {
                    //非级管理员按类型排除加载数据（0：字典类型，1资源类型，2资源类别，4试题类型，5教案类型）
                    condition.AppendFormat(" and D_Type not in ('{0}','{1}','{2}','{3}','{4}')"
                       , "0", "1", "2", "4", "5");
                    //非级管理员按标识排除加载数据（资源类型中的：按目录属性生成）
                    condition.AppendFormat(" and Common_Dict_ID not in ('{0}')"
                      , "f22bd0fd-73b6-4c73-9df0-8b3cb8489816");
                }

                dt = BLL.GetCommon_DictList(condition.ToString(), PageIndex, PageSize, out  rCount, out  pCount).Tables[0];
                int i = 0;

                strHtmlData.Append("<table class='table table-hover table-bordered'>");
                strHtmlData.Append("<thead>");
                strHtmlData.Append("<tr>");
                strHtmlData.Append("<th>名称</th>");
                strHtmlData.Append("<th>类型</th>");
                strHtmlData.Append("<th>排序</th>");
                //strHtmlData.Append("<th style='width:10%;'>值</th>");
                strHtmlData.Append("<th>备注</th>");
                strHtmlData.Append("<th>操作</th>");
                strHtmlData.Append("</tr>");
                strHtmlData.Append("</thead>");
                strHtmlData.Append("<tbody>");
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    i++;
                    strHtmlData.Append("<tr>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["D_Name"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + GetD_Name(dt.Rows[j]["D_Type"].ToString()) + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["D_Order"].ToString() + "</td>");
                    //strHtmlData.Append("<td>" + dt.Rows[j]["D_Value"].ToString() + "</td>");
                    strHtmlData.Append("<td>" + dt.Rows[j]["D_Remark"].ToString() + "</td>");
                    strHtmlData.Append("<td class='opera'>");
                    if (UserFun.Edit)
                    {
                        strHtmlData.AppendFormat("<a href=\"javascript:;\" onclick=\"showPopCommon_Dict('{0}');\">编辑</a>", clsUtility.Encrypt(dt.Rows[j]["Common_Dict_ID"].ToString()));
                    }
                    if (UserFun.Delete)
                    {
                        strHtmlData.AppendFormat("<a href=\"javascript:;\" onclick=\"DeleteItemDesc('{0}')\">删除</a>", clsUtility.Encrypt(dt.Rows[j]["Common_Dict_ID"].ToString()));
                    }

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;

            strUrlPara += "&p=" + Rc.Common.StrUtility.clsUtility.Encrypt(txtD_Name.Text.Trim());
            strUrlPara += "&a=" + Rc.Common.StrUtility.clsUtility.Encrypt(ddlD_Type.SelectedValue);

            Response.Redirect(strUrlPara);
        }

        public string GetD_Name(string D_Type)
        {
            return BLL.GetD_Name(D_Type);// +":" + D_Type;

        }
    }
}