using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.test
{
    public partial class zhuaquweb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            string strUrl = string.Empty;
            strUrl = "http://jszg.i.southteacher.com/yxylogin/checkToken.do?token=aaa";
            Response.Write(GetGeneralContent(strUrl));

        }
        private string GetGeneralContent(string strUrl)
        {

            string strMsg = string.Empty;

            try
            {

                WebRequest request = WebRequest.Create(strUrl);

                WebResponse response = request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gbk"));



                strMsg = reader.ReadToEnd();



                reader.Close();

                reader.Dispose();

                response.Close();

            }

            catch

            { }

            return strMsg;

        }
    }
}