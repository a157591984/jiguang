using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.SessionState;
using System.IO;
using System.Text;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Cloud.Model;
using Rc.Common.StrUtility;
using System.Collections.Generic;
using Rc.Common.Config;
namespace Rc.Upload
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UploadHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 得到请求内容
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Server.ScriptTimeout = 36000;
           
            try
            {
             
                string strPath = context.Request.QueryString["directory"].ToString();
                string strFileName = context.Request.QueryString["fname"].ToString();
                strPath = context.Server.MapPath("\\Upload\\Resource\\" + strPath);//.Replace("Upload", "newUpload");
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                model.SyncUrl = strPath + strFileName;
                model.MsgType = "整体-开始接收";
                // model.ErrorMark = ex.Message;
                model.CreateTime = DateTime.Now;
                bll.Add(model);

                foreach (string f in context.Request.Files.AllKeys)
                {
                  
                    HttpPostedFile file = context.Request.Files[f];
                    
                    model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                    model.FileSize = file.ContentLength;
                    model.SyncUrl = strPath + strFileName;
                    model.MsgType = "开始接收";
                   // model.ErrorMark = ex.Message;
                    model.CreateTime = DateTime.Now;
                    bll.Add(model);
                    file.SaveAs(strPath + strFileName);

                    model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                    model.FileSize = file.ContentLength;
                    model.SyncUrl = strPath + strFileName;
                    model.MsgType = "接收完成";
                    // model.ErrorMark = ex.Message;
                    model.CreateTime = DateTime.Now;
                    bll.Add(model);
                }

                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
                model.SyncUrl = strPath + strFileName;
                model.MsgType = "整体-接收完成";
                // model.ErrorMark = ex.Message;
                model.CreateTime = DateTime.Now;
                bll.Add(model);

                context.Response.Write("1");
            }
            catch (Exception ex)
            {
                string strPath = context.Request.QueryString["directory"].ToString();
                string strFileName = context.Request.QueryString["fname"].ToString();
                strPath = context.Server.MapPath("\\Upload\\Resource\\" + strPath);//.Replace("Upload", "newUpload");
              
                Rc.Model.Resources.Model_SystemLogFileSync model = new Rc.Model.Resources.Model_SystemLogFileSync();
                Rc.BLL.Resources.BLL_SystemLogFileSync bll = new Rc.BLL.Resources.BLL_SystemLogFileSync();
                model.SystemLogFileSyncID = Guid.NewGuid().ToString();
               
                model.SyncUrl = strPath + strFileName;
                model.MsgType = "接收失败";
                model.ErrorMark = ex.Message;
                model.CreateTime = DateTime.Now;
                bll.Add(model);
                context.Response.Write(ex.Message.ToString());
            }

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
