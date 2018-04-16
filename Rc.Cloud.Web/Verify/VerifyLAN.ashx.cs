using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.Verify
{
    /// <summary>
    /// VerifyLAN1 的摘要说明
    /// </summary>
    public class VerifyLAN1 : IHttpHandler
    {
        string key = string.Empty;
        string get_info_key = string.Empty;
        string fixedPara = string.Empty;
        string para = string.Empty;
        string strSql = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            string callbackFunName = context.Request["callbackparam"];
            #region switch key
            key = context.Request.QueryString["key"].Filter().ToUpper();
            switch (key)
            {
                case "VERIFYLAN":
                    string cardNO = string.Empty;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(callbackFunName + "({value:\"2\"})");

                    break;
            }
            #endregion
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}