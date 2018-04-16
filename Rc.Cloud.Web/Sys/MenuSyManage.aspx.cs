using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Rc.Cloud.Model;
using Rc.Cloud.BLL;

namespace Rc.Cloud.Web.Sys
{
    public partial class MenuSyManage : Rc.Cloud.Web.Common.InitPage
    {
        BLL_SysCode bll = new BLL_SysCode();
        protected string ReturnUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "30203000";
            SetSearchP();
            ReturnUrl = strPageName + "?" + Rc.Cloud.Web.Common.pfunction.getPageParam();
            //得到用户在此页面的权限
            //UserFun = new BLL_clsAuth().GetUserFunc(loginUser.DoctorInfo_ID, PHHC.Share.StrUtility.clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), Module_Id);
            if (!IsPostBack)
            {
               DataTable dt =new DataTable();
               dt = new BLL_SysCode().GetSysName().Tables[0];
               if (dt.Rows.Count>0)
               {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ddlSysCode.Items.Add(new ListItem(dt.Rows[i]["SysName"].ToString(), dt.Rows[i]["SysCode"].ToString()));
                    }
                }
                SetSearchO();
            }
        }

        //给查询对象附值
        private void SetSearchO()
        {
            if (!string.IsNullOrEmpty(ddlSysCode.SelectedValue))
                ddlSysCode.SelectedValue = Request.QueryString["a"];
            if (Request.QueryString["p"] != null && Request.QueryString["p"] != "")
                moduleName.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["p"]);

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
            strUrlPara += "&a=" + Server.UrlEncode(Request.QueryString["a"]);
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
            if (moduleName.Text.ToString() != "")
            {
                condition.Append(" and (MODULENAME like '%" + moduleName.Text.Trim() + "%' or MODULEID like '" + moduleName.Text.ToString() + "%')");
            }
            if (ddlSysCode.SelectedValue.Trim() != null && ddlSysCode.SelectedValue.Trim() != "" && ddlSysCode.SelectedValue.Trim() != "-1")
            {
                condition.AppendFormat(" and sm.SysCode='{0}'", ddlSysCode.SelectedValue.Trim());
            }

            dt = bll.GetModuleListBySysCode(condition.ToString(), PageIndex, PageSize, out  rCount, out  pCount).Tables[0];
            int i = 0;
            StringBuilder strHtmlData = new StringBuilder();
            strHtmlData.Append("<table class='table_list' cellpadding='0' cellspacing='0' >");
            strHtmlData.Append("<tr class='tr_title'>");
            strHtmlData.Append("<th style='width:9%;'>系统名称</th>");
            strHtmlData.Append("<th style='width:7%;'>模块编码</th>");
            strHtmlData.Append("<th style='width:8%;'>模块名称</th>");
            strHtmlData.Append("<th style='width:7%;'>父模块编码</th>");
            strHtmlData.Append("<th style='width:7%;'>SLEVER</th>");
            strHtmlData.Append("<th>链接地址</th>");
            strHtmlData.Append("<th style='width:6%;'>是否显示</th>");
            strHtmlData.Append("<th style='width:6%;'>菜单级别</th>");
            strHtmlData.Append("<th style='width:10%;'>是否最后一级菜单</th>");
            strHtmlData.Append("<th style='width:6%;'>默认页</th>");
            strHtmlData.Append("<th style='width:12%;'>操作</th>");
            strHtmlData.Append("</tr>");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                i++;
                string css = string.Empty;
                if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                strHtmlData.Append("<tr class='" + css + "'>");
                strHtmlData.Append("<td>" + dt.Rows[j]["SysName"].ToString() + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["MODULEID"].ToString() + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["MODULENAME"].ToString() + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["PARENTID"].ToString() + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["SLEVEL"].ToString() + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["URL"].ToString() + "</td>");
                strHtmlData.Append("<td>" + (dt.Rows[j]["ISINTREE"].ToString() == "Y" ? "是" : "否") + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["Depth"].ToString() + "</td>");
                strHtmlData.Append("<td>" + (dt.Rows[j]["isLast"].ToString() == "1" ? "是" : "否") + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["DefaultOrder"].ToString() + "</td>");
                strHtmlData.Append("<td>");
                strHtmlData.AppendFormat("<a href='javascript:void(0)' onclick=\"AddModule(1,'{0}','{1}');\">类似新增</a>", dt.Rows[j]["MODULEID"].ToString(), dt.Rows[j]["SYSCODE"].ToString());
                strHtmlData.AppendFormat("&nbsp;&nbsp;");
                strHtmlData.AppendFormat("<a href='javascript:void(0)' onclick=\"AddModule(2,'{0}','{1}');\">修改</a>", dt.Rows[j]["MODULEID"].ToString(), dt.Rows[j]["SYSCODE"].ToString());
                strHtmlData.AppendFormat("&nbsp;&nbsp;");
                strHtmlData.AppendFormat("<a href='javascript:void(0)' onclick=\"DeleteSysModuleByID('{0}','{1}')\">删除</a>", dt.Rows[j]["MODULEID"].ToString(), dt.Rows[j]["SYSCODE"].ToString());
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
            strUrlPara += "&a=" + ddlSysCode.SelectedValue;
            strUrlPara += "&p=" + Rc.Common.StrUtility.clsUtility.Encrypt(moduleName.Text.Trim());
            Response.Redirect(strUrlPara);
        }

        protected void btnTongBu_Click(object sender, EventArgs e)
        {
            Response.Redirect("SysMenuSynchronous.aspx");
        }

        protected void btnout_Click(object sender, EventArgs e)
        {
            string moduleNames = moduleName.Text;
            string syscode = ddlSysCode.SelectedValue;

            DataTable dt = new DataTable();
            StringBuilder condition = new StringBuilder();
            if (moduleName.Text.ToString() != "")
            {
                condition.Append(" and (MODULENAME like '%" + moduleName.Text.Trim() + "%' or MODULEID like '" + moduleName.Text.ToString() + "%')");
            }
            if (ddlSysCode.SelectedValue.Trim() != null && ddlSysCode.SelectedValue.Trim() != "" && ddlSysCode.SelectedValue.Trim() != "-1")
            {
                condition.AppendFormat(" and sm.SysCode='{0}'", ddlSysCode.SelectedValue.Trim());

            }
            dt = bll.GetModuleListBySysCode(condition.ToString()).Tables[0];

            StringBuilder sb = new StringBuilder();

            foreach (DataRow inrow in dt.Rows)
            {
                sb.AppendFormat(@"INSERT INTO [SysModule]
                                 VALUES ('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}' ,'{7}' ,'{8}' ,'{9}' ,'{10}' ,'{11}' ,'{12}' ,'{13}' ,'{14}' ,'{15}' ,'{16}')
                            ", inrow[0], inrow[1], inrow[2], inrow[3], inrow[4], inrow[5], inrow[6], inrow[7], inrow[8], inrow[9], inrow[10], inrow[11], inrow[12], inrow[13], inrow[14], inrow[15], inrow[16]);
                sb.Append("\r\n");
            }

            foreach (DataRow inrow in dt.Rows)
            {
                sb.AppendFormat(@"update dbo.SysModule set MODULENAME='{1}',PARENTID='{2}',SLEVEL='{3}'
                                ,URL='{4}',QUERYFORM='{5}',OTHKEY='{6}',REMARK='{7}',IMGICON='{8}',ISINTREE='{9}',MODULETYPE='{10}',ATTACH_SQL='{11}',ISINTAB='{12}',Depth={13}
                                ,IsLast={14},DefaultOrder={15} where  MODULEID='{0}' and Syscode='{16}'",
                                inrow[0], inrow[1], inrow[2], inrow[3], inrow[4], inrow[5], inrow[6], inrow[7], inrow[8], inrow[9], inrow[10], inrow[11], inrow[12], inrow[13], inrow[14], inrow[15], inrow[16]);
                sb.Append("\r\n");
            }

            Response.AddHeader("Content-Disposition", "attachment;filename= 要插入的sql.sql");
            //Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            //Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");

            Response.Write("\r\n" + sb);
            //Response.Flush();
            Response.End();
        }
    }
}