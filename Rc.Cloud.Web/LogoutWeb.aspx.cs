﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web
{
    public partial class LogoutWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Rc.Common.StrUtility.CookieClass.RemoveCookie("UserPublicUrl_Cookie");
            Session.Clear();
            Response.Redirect(Rc.Common.StrUtility.clsUtility.getHostPath() + "/index.aspx");
        }
    }
}