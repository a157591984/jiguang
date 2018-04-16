using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string strUrl = "~/index.aspx";
            //if (Request["t"] != null)
            //{
            //    if (Request["t"].ToString() == "1")
            //    {
                   
            //        strUrl = "~/index.aspx";
                   
            //    }
            //}
           // Response.Redirect(strUrl);
            Session.Clear();
            Response.Redirect(Rc.Common.StrUtility.clsUtility.getHostPath() + "/RcAdmin/index.aspx");
        }
    }
}