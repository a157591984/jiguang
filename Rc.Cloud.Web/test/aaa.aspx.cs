using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.DirectoryServices;

namespace Rc.Cloud.Web.test
{
    public partial class aaa : System.Web.UI.Page
    {
        private const String LDAP_Server = "cas.yzlyxx.com";
        private const int LDAP_Port = 389;
        private const String LDAP_Search_DN = "ou=people,dc=yzlyxx,dc=com";
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            String userNameInput =  txtUserName.Text.Trim().Filter();
            String passwordInput = txtPassword.Text;
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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }
}