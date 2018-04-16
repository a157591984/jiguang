using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;

namespace Rc.Cloud.Web.Sys
{
    public partial class ListStructureItem : System.Web.UI.Page
    {
        BLL_ListStructure bll = new BLL_ListStructure();
        private string tablenames = string.Empty;
        private string database = string.Empty;
        private string tableinfos = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            database = Request["database"].ToString();
            tablenames = Request["tablename"].ToString();
            tableinfos = Request["tableinfo"].ToString();
            if (!IsPostBack)
            {
                this.tablename.Text = tablenames;
                if (!Request["tableinfo"].ToString().Equals("000"))
                    this.tableinfo.Text = tableinfos;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool table = false;

            if (string.IsNullOrEmpty(this.tableinfo.Text))
            {
                table = bll.ExecAddDescriptionTable(database, tablenames, this.tableinfo.Text);
            }
            else
            {
                table = bll.ExecUpdataDescriptionTable(database, tablenames, this.tableinfo.Text);
                if (!table)
                {
                    table = bll.ExecAddDescriptionTable(database, tablenames, this.tableinfo.Text);
                }
            }
            if (table)
            { ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.HandelAddUser('1','操作成功！');</script>"); }
            else
            { ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.HandelAddUser('2','操作失败！');</script>"); }
        }
    }
}