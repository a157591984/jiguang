using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.Web.Common;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.AuthApi.visit
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder strSql = new StringBuilder();
            string strAction = string.Empty;
            string strWhere = string.Empty;
            string strJsion = string.Empty;
            string strUserID = string.Empty;
            string strDocumentID = string.Empty;
            string strtoken = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
            if (!String.IsNullOrEmpty(Request["token"]) && !String.IsNullOrEmpty(Request["userId"]) && !String.IsNullOrEmpty(Request["productType"]))
            {
                string LoginUrl = string.Empty;
                LoginUrl = string.Format("{0}/AuthApi/loginApi.aspx?token={1}&userId={2}&productType={3}"
                   , pfunction.getHostPath()
                   , Request["token"].ToString()
                   , Request["userId"].ToString()
                   , Request["productType"].ToString());
                Response.Redirect(LoginUrl);
            }
            else
            {
                //Response.Write("参数错误");
                strJsion = objJson.Serialize(new
                {
                    status = false,
                    errmsg = "参数错误,请求失败"
                });
                Response.Write(strJsion);
            }
        }
    }
}