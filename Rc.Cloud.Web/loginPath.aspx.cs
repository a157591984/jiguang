using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;

namespace Rc.Cloud.Web
{
    public partial class loginPath : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL_loginPath bll = new BLL_loginPath();
            bll.LoginRedirect(this.Page,Request.QueryString["id"],Request.QueryString["url"]);
        }
    }
}