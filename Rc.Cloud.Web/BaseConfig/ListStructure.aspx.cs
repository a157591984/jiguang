using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.Web.Common;
using System.Data;
using System.Text;
using Rc.Common.StrUtility;
using Rc.Cloud.BLL;



namespace Rc.Cloud.Web.BaseConfig
{
    public partial class ListStructure : Rc.Cloud.Web.Common.InitPage
    {
        protected string ReturnUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "30206000";
            SetSearchP();
            if (!IsPostBack)
            {
                SetSearchO();
            }
        }
        /// <summary>
        /// 从url获得东西并赋值
        /// </summary>
        private void SetSearchO()
        {
            //表明
            if (!string.IsNullOrEmpty(Request["n"]))
            {
                txtMark.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request["n"].ToString());
            }
            //表说明
            if (!string.IsNullOrEmpty(Request["td"]))
            {
                TableDescription.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request["td"].ToString());
            }
            //表说明是否为空
            if (!string.IsNullOrEmpty(Request["tdyon"]))
            {
                tdyon.SelectedValue = Rc.Common.StrUtility.clsUtility.Decrypt(Request["tdyon"].ToString());
            }
            //字段名
            if (!string.IsNullOrEmpty(Request["file"]))
            {
                Field.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request["file"].ToString());
            }
            //字段是否为空
            if (!string.IsNullOrEmpty(Request["yon"]))
            {
                yesorno.SelectedValue = Rc.Common.StrUtility.clsUtility.Decrypt(Request["yon"].ToString());
            }
            //字段说明是否为空
            if (!string.IsNullOrEmpty(Request["FieldDescribe"]))
            {
                FieldDescribe.Text = Rc.Common.StrUtility.clsUtility.Decrypt(Request["FieldDescribe"].ToString());
            }
            DataTable dataBaseCopyLibrary = new DataTable();
            dataBaseCopyLibrary = Rc.Common.DBUtility.DbHelperSQL.Query(@"SELECT * FROM dbo.DatabaseCopyLibrary where DataBaseName like '%xyfw%'").Tables[0];
            //Rc.Cloud.Web.Common.pfunction.SetDdl(ddlDataName, dataBaseCopyLibrary, "DataBaseName", "DatabaseCopyLibrary_ID", "");
            //if (!string.IsNullOrEmpty(Request["database"]))
            //{
            //    ddlDataName.SelectedItem.Text = PHHC.Share.StrUtility.clsUtility.Decrypt(Request["database"].ToString());
            //}
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
            strUrlPara += "&n=" + Server.UrlEncode(Request["n"]);
            strUrlPara += "&yon=" + Server.UrlEncode(Request["yon"]);
            strUrlPara += "&td=" + Server.UrlEncode(Request["td"]);
            strUrlPara += "&tdyon=" + Server.UrlEncode(Request["tdyon"]);
            strUrlPara += "&file=" + Server.UrlEncode(Request["file"]);
            strUrlPara += "&FieldDescribe" + Server.UrlEncode(Request["FieldDescribe"]);
        }

        /// <summary>
        /// 添加主数据
        /// </summary>
        /// <returns></returns>
        protected string GetHtmlData()
        {
            StringBuilder condition = new StringBuilder();
            if (!txtMark.Text.IsNullOrEmpty())
            {
                condition.Append(" and d.name LIKE'%" + txtMark.Text.Trim() + "%' ");
            }
            if (TableDescription.Text.Trim() != "")
            {
                condition.Append(" AND cast(f.value AS VARCHAR)  LIKE '%" + TableDescription.Text.Trim() + "%' ");
            }
            #region 下拉列表
            switch (tdyon.SelectedValue)
            {
                case "00":
                    condition.Append(" and isnull(f.value, '')!=''");
                    break;
                case "11":
                    condition.Append("  and ISNULL( f.[value], '')='' ");
                    break;
                case "01":
                    condition.Append(" ");
                    break;
                default:
                    condition.Append("  ");
                    break;
            }
            switch (yesorno.SelectedValue)
            {
                case "00":
                    condition.Append(" and isnull(g.value, '')!=' '");
                    break;
                case "11":
                    condition.Append("  and ISNULL( g.[value], '')='' ");
                    break;
                case "01":
                    condition.Append(" ");
                    break;
                default:
                    condition.Append("  ");
                    break;
            }
            #endregion
            if (Field.Text.Trim() != "")
            {
                condition.Append(" AND a.name  LIKE '%" + Field.Text.Trim() + "%' ");
            }
            if (!FieldDescribe.Text.Trim().IsNullOrEmpty())
            {
                condition.Append(" AND CAST(g.value AS VARCHAR) LIKE '"+FieldDescribe.Text.Trim()+"' ");
            }
            StringBuilder strHtmlData = new StringBuilder();
            BLL_ListStructure bll = new BLL_ListStructure();
            string databases = "[YXFW_DATA_0.9]";
            //databases = "temp_yxfw_forZXDP";
            DataSet ds = bll.GetListStructure(databases,condition.ToString(), PageIndex, PageSize, out  rCount, out  pCount);
            DataTable dt = ds.Tables[0];
            int i = 0;

            strHtmlData.Append("<table class='table_list' cellpadding='0' cellspacing='0' >");
            strHtmlData.Append("<tr class='tr_title'>");
            strHtmlData.Append("<td style='width:15%'>表名</td>");
            strHtmlData.Append("<td style='width:18%'>表说明</td>");
            strHtmlData.Append("<td style='width:15%'>字段名</td>");
            strHtmlData.Append("<td style='width:25%'>字段说明</td>");
            strHtmlData.Append("<td style='width:8%'>字段类型</td>");
            strHtmlData.Append("<td style='width:8%'>字段长度</td>");
            strHtmlData.Append("</tr>");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string tablename = dt.Rows[j]["表名"].ToString();
                string tableinfo = dt.Rows[j]["表说明"].ToString();
                string tableColumn = dt.Rows[j]["字段名"].ToString();
                string tablecolumninfo = dt.Rows[j]["字段说明"].ToString();
                
                i++;
                string css = string.Empty;
                if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                strHtmlData.Append("<tr class='" + css + "'>");
                strHtmlData.Append("<td>" + tablename + "</td>");
                strHtmlData.Append("<td>");
                strHtmlData.Append("<input type='button' class='btn_modify'onclick=\"AddModule('" + databases + "','" + tablename + "','" + tableinfo + "');\">	");
                strHtmlData.Append(tableinfo);
                strHtmlData.Append("</td>");
                strHtmlData.Append("<td>" + tableColumn + "</td>");
                strHtmlData.Append("<td>");
                strHtmlData.Append("<input type='button' class='btn_modify'onclick=\"AddModuleColumns('"+databases+"','" + tablename + "','" + tableColumn + "','" + tablecolumninfo + "');\">	");
                strHtmlData.Append(tablecolumninfo);
                strHtmlData.Append("</td>");
                
                strHtmlData.Append("<td>" + dt.Rows[j]["类型"] + "</td>");
                strHtmlData.Append("<td>" + dt.Rows[j]["长度"] + "</td>");
              
                if (tableinfo.Equals(string.Empty))
                    tableinfo = "000";
                if (tablecolumninfo.Equals(string.Empty))
                    tablecolumninfo = "0000";
                //strHtmlData.Append("<td><a href='javascript:void(0)' onclick=\"AddModule('" + tablename + "','" + tableinfo + "','" + tableColumn + "','" + tablecolumninfo + "');\">修改</a></td>");
                strHtmlData.Append("</tr>");
            }


            return strHtmlData.ToString();
        }
        private string Cutdatetime(object date)
        {
            string time = string.Empty;
            if (!date.ToString().IsNullOrEmpty())
            {
                DateTime mid = Convert.ToDateTime(date);
                time = mid.ToString("yyyy-MM-dd");
            }
            return time;
        }

        /// <summary>
        /// 添加目录
        /// </summary>
        /// <returns></returns>
        protected string GetPageIndex()
        {
            return Rc.Cloud.Web.Common.PageFuntion.GetPageIndex(PageIndex, PageSize, rCount, pCount, strUrlPara);
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;
            strUrlPara += "&n=" + Rc.Common.StrUtility.clsUtility.Encrypt(this.txtMark.Text.Trim());
            strUrlPara += "&yon=" + Rc.Common.StrUtility.clsUtility.Encrypt(this.yesorno.SelectedValue);
            strUrlPara += "&td=" + Rc.Common.StrUtility.clsUtility.Encrypt(this.TableDescription.Text.Trim());
            strUrlPara += "&tdyon=" + Rc.Common.StrUtility.clsUtility.Encrypt(this.tdyon.SelectedValue);
            strUrlPara += "&file=" + Rc.Common.StrUtility.clsUtility.Encrypt(this.Field.Text.Trim());
            strUrlPara += "&FieldDescribe=" + Rc.Common.StrUtility.clsUtility.Encrypt(this.FieldDescribe.Text.Trim());
            //strUrlPara += "&database=" + PHHC.Share.StrUtility.clsUtility.Encrypt(this.ddlDataName.SelectedItem.Text.Trim());
            Response.Redirect(strUrlPara);
        }

    }
}