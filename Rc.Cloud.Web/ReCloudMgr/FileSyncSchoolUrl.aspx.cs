using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncSchoolUrl : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            try
            {
                DataTable dt = new BLL_ConfigSchool().GetList("D_PublicValue<>''").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string url = item["D_PublicValue"].ToString() + "ReCloudMgr/FileSyncSchool_Auto.aspx?SchoolId=" + item["School_ID"];
                        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                    }
                }

            }
            catch (Exception ex)
            {


            }
        }
    }
}