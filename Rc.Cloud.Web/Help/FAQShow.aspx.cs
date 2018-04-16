using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.Help
{
    public partial class FAQShow : System.Web.UI.Page
    {
        public string help_id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            help_id = Request.QueryString["help_id"].Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(help_id))
                {
                    LoadData();
                }
            }
        }
        protected void LoadData()
        {
            try
            {
                BLL_HelpCenter bll = new BLL_HelpCenter();
                Model_HelpCenter model = new Model_HelpCenter();
                model = bll.GetModel(help_id);
                if (model != null)
                {
                    this.ltl_content.Text = model.help_content;
                    this.ltl_title.Text = model.help_title;
                    this.ltl_titles.Text = model.help_title;
                    ltlHtltel.Text = model.help_title;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}