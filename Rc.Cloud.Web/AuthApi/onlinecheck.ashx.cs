using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.AuthApi
{
    /// <summary>
    /// onlinecheck 的摘要说明
    /// </summary>
    public class onlinecheck : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.Write("ok");
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