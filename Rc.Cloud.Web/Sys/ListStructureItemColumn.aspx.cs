using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;

namespace Rc.Cloud.Web.Sys
{
    public partial class ListStructureItemColumn : System.Web.UI.Page
    {
        BLL_ListStructure bll = new BLL_ListStructure();
        private string database = string.Empty;
        private string tablenames = string.Empty;
        private string tableColumns = string.Empty;
        private string tablecolumninfos = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            database = Request["database"].ToString();
            tablenames = Request["tablename"].ToString();
            tableColumns = Request["tableColumn"].ToString();
            tablecolumninfos = Request["tablecolumninfo"].ToString();
            if (!IsPostBack)
            {
                this.tableColumn.Text = tableColumns;
                if (!Request["tablecolumninfo"].ToString().Equals("0000"))
                    this.tablecolumninfo.Text = tablecolumninfos;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool tablecolue = false;

            if (string.IsNullOrEmpty(this.tablecolumninfo.Text))
            {
                tablecolue = bll.ExecAddDescriptionTableColumn(database, tablenames, tableColumns, this.tablecolumninfo.Text);
            }
            else
            {
                tablecolue = bll.ExecUpdataDescriptionTableColumn(database, tablenames, tableColumns, tablecolumninfo.Text);
                if (!tablecolue)
                {
                    tablecolue = bll.ExecAddDescriptionTableColumn(database, tablenames, tableColumns, this.tablecolumninfo.Text);
                }
            }
            if (tablecolue)
            { ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.HandelAddUser('1','操作成功！');</script>"); }
            else
            { ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.HandelAddUser('2','操作失败！');</script>"); }
        }
    }
}