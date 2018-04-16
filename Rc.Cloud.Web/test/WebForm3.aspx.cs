using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.test
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            download();
        }

        private void download()
        {


            string remoteUri = "http://plan.tuiti.cn/Upload/Resource/classDocument/2016/3b6838b9-4f30-4a31-aff1-9f3966f49cd0/90d87c22-4c1c-43be-8504-e25586fad9e0/d5c974d3-10f6-4a93-b5fb-def0154a587b/"
;
            string fileName = "5a5b8611-004e-49fb-98aa-f0d087b4446e.htm", myStringWebResource = null;
            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            myStringWebResource = remoteUri + fileName;

            string strPath = System.Web.HttpContext.Current.Server.MapPath("\\Upload\\Resource\\" );//.Replace("Upload", "newUpload");
           // pfunction.DownLoadFileByWebClient(remoteUri, strPath, fileName);
           

        }
    }
}