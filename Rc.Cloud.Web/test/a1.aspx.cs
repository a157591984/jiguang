using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;

namespace Rc.Cloud.Web.test
{
    public partial class a1 : System.Web.UI.Page
    {
        private const String LDAP_Server = "cas.yzlyxx.com";
        private const int LDAP_Port = 389;
        private const String LDAP_Search_DN = "ou=people,dc=yzlyxx,dc=com";

        private void Page_Load(object sender, System.EventArgs e)
        {
            String userNameInput = "90823925";
            String passwordInput = "Aa987654321";

            DirectoryEntry de = new DirectoryEntry();
            de.Path = "LDAP://" + LDAP_Server + ":" + LDAP_Port;
            de.Username = "uid=" + userNameInput + "," + LDAP_Search_DN;
            de.Password = passwordInput;
            de.AuthenticationType = AuthenticationTypes.None;

            try
            {
                object o = de.NativeObject;
            }
            catch (Exception ex)
            {
                Response.Write("Auth failed!" + ex.Message);
                return;
            }
            finally
            {
                de.Close();
                de = null;
            }
            Response.Write("Auth successful!");
        }
    }
}