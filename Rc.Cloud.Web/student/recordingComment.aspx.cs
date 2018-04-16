using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.student
{
    public partial class recordingComment : System.Web.UI.Page
    {
        protected string hwCTime = string.Empty;
        protected string stuHwId = string.Empty;
        protected string tqId = string.Empty;
        protected string recordingPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hwCTime = pfunction.ToShortDate(Request["hwCTime"].Filter());
                stuHwId = Request["stuHwId"].Filter();
                tqId = Request["tqId"].Filter();
                string filePath = string.Format("/Upload/Resource/teacherRecording/{0}/{1}/{2}.wav"
                    , pfunction.ToShortDate(hwCTime), stuHwId, tqId);
                if (File.Exists(Server.MapPath(filePath)))
                {
                    recordingPath = filePath + "?" + DateTime.Now.ToString();
                }
            }
            catch (Exception)
            {
                
            }
           
        }

    }
}