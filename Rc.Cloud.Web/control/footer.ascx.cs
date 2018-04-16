using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.control
{
    public partial class footer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltlFooter.Text = new Rc.BLL.Resources.BLL_CustomizeHtml().GetHtmlContentByHtmlType(Rc.Model.Resources.CustomizeHtmlTypeEnum.footer.ToString()).ToString();
        }
    }
}