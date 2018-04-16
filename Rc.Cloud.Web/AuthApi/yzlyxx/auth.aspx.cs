using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.AuthApi.yzlyxx
{
    public partial class auth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String LDAP_Server = "cas.yzlyxx.com";
                int LDAP_Port = 389;
                String LDAP_Search_DN = "ou=people,dc=yzlyxx,dc=com";

                String userNameInput = Request["UserName"].Filter();
                String passwordInput = Request["Password"].Filter();
                DirectoryEntry de = new DirectoryEntry();
                de.Path = "LDAP://" + LDAP_Server + ":" + LDAP_Port;
                de.Username = "uid=" + userNameInput + "," + LDAP_Search_DN;
                de.Password = passwordInput;
                de.AuthenticationType = AuthenticationTypes.None;
                try
                {
                    object o = de.NativeObject;
                }
                catch (Exception)
                {
                    Response.Write("failed");
                    return;
                }
                finally
                {
                    de.Close();
                    de = null;
                }
                Response.Write("successful");
            }
            catch (Exception)
            {
                Response.Write("failed");
            }
        }
    }
}