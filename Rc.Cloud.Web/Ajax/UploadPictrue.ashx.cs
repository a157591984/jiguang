using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Rc.Cloud.Web.Ajax
{
    /// <summary>
    /// UploadPictrue 的摘要说明
    /// </summary>
    public class UploadPictrue : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            object result = null;
            try
            {
                // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                string filepath = context.Request["filepath"];
                if (string.IsNullOrEmpty(filepath)) filepath = "/Upload/Sxxmb/";
                string fileExt = context.Request["fileExt"];
                if (string.IsNullOrEmpty(fileExt)) fileExt = "jpg,jpeg,png";
                double maxSize = 1;
                double.TryParse(context.Request["maxSize"], out maxSize);
                if (maxSize == 0) maxSize = 1;

                // do something with the file in this space
                // {....}
                // end of file doing

                string[] strArrExt = fileExt.Split(',');
                string strFolder = filepath;
                string strGuid = context.Request["hidFileID"]; // Guid.NewGuid().ToString();
                if (string.IsNullOrEmpty(strGuid)) strGuid = Guid.NewGuid().ToString();
                string strFileExt = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1).ToLower();
                string Name = file.FileName.Substring(file.FileName.LastIndexOf("/") + 1);
                string fileName = strGuid + "." + strFileExt;
                if (strArrExt.Contains(strFileExt))
                {
                    if (file.ContentLength > 1024 * 1024 * maxSize)
                    {
                        result = new
                        {
                            sta = false,
                            msg = string.Format("上传文件大小不能超过{0}M,请重新选择上传文件", maxSize)
                        };
                    }
                    else
                    {
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(strFolder)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(strFolder));
                        }
                        this.SaveAs(HttpContext.Current.Server.MapPath(strFolder) + fileName, file);

                        // Now we need to wire up a response so that the calling script understands what happened
                        HttpContext.Current.Response.ContentType = "text/plain";

                        result = new
                        {
                            sta = true,
                            msg = "上传成功",
                            previewSrc = strFolder + fileName,
                            FileName = Name
                        };
                    }
                }
                else
                {
                    result = new
                    {
                        sta = false,
                        msg = "上传文件格式不支持,请重新选择上传文件"
                    };
                }
            }
            catch (Exception)
            {
                result = new
                {
                    sta = false,
                    msg = "上传失败"
                };
            }


            HttpContext.Current.Response.Write(serializer.Serialize(result));
        }



        private void SaveAs(string saveFilePath, HttpPostedFile file)
        {
            try
            {
                long lStartPos = 0;
                int startPosition = 0;
                int endPosition = 0;
                var contentRange = HttpContext.Current.Request.Headers["Content-Range"];
                //bytes 10000-19999/1157632
                if (!string.IsNullOrEmpty(contentRange))
                {
                    contentRange = contentRange.Replace("bytes", "").Trim();
                    contentRange = contentRange.Substring(0, contentRange.IndexOf("/"));
                    string[] ranges = contentRange.Split('-');
                    startPosition = int.Parse(ranges[0]);
                    endPosition = int.Parse(ranges[1]);
                }
                System.IO.FileStream fs;
                if (System.IO.File.Exists(saveFilePath))
                {
                    fs = System.IO.File.OpenWrite(saveFilePath);
                    lStartPos = fs.Length;

                }
                else
                {
                    fs = new System.IO.FileStream(saveFilePath, System.IO.FileMode.Create);
                    lStartPos = 0;
                }
                if (lStartPos > endPosition)
                {
                    fs.Close();
                    return;
                }
                else if (lStartPos < startPosition)
                {
                    lStartPos = startPosition;
                }
                else if (lStartPos > startPosition && lStartPos < endPosition)
                {
                    lStartPos = startPosition;
                }
                fs.Seek(lStartPos, System.IO.SeekOrigin.Current);
                byte[] nbytes = new byte[512];
                int nReadSize = 0;
                nReadSize = file.InputStream.Read(nbytes, 0, 512);
                while (nReadSize > 0)
                {
                    fs.Write(nbytes, 0, nReadSize);
                    nReadSize = file.InputStream.Read(nbytes, 0, 512);
                }
                fs.Close();
            }
            catch (Exception)
            {

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