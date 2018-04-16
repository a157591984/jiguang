namespace Rc.Common.AppUtility
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;

    public class HttpFile
    {
        private string _errorStr = "";
        private int _errorType = -1;
        private List<string> mimelist = new List<string>();

        public void AddMime(string mime)
        {
            this.mimelist.Add(mime.ToLower());
        }

        public bool DownFile_HTTP(string mime, string filePath)
        {
            return this.DownFile_HTTP(mime, filePath, Encoding.UTF8);
        }

        public bool DownFile_HTTP(string mime, string filePath, Encoding code)
        {
            try
            {
                string str = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                FileInfo info = new FileInfo(filePath);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + str);
                HttpContext.Current.Response.AddHeader("Content-Length", info.Length.ToString());
                HttpContext.Current.Response.ContentType = mime;
                HttpContext.Current.Response.ContentEncoding = code;
                HttpContext.Current.Response.WriteFile(info.FullName);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DownFile_Stream(string mime, string filePath)
        {
            return this.DownFile_Stream(mime, filePath, Encoding.UTF8);
        }

        public bool DownFile_Stream(string mime, string filePath, Encoding code)
        {
            try
            {
                string str = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                FileStream stream = new FileStream(filePath, FileMode.Open);
                byte[] buffer = new byte[(int) stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(str, code));
                HttpContext.Current.Response.BinaryWrite(buffer);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UploadFile(HttpPostedFile file, int maxLength, string savePath)
        {
            try
            {
                string item = file.ContentType.Trim().ToLower();
                if (file.ContentLength > maxLength)
                {
                    this._errorStr = "上传文件超过最大限制";
                    this._errorType = 1;
                    return false;
                }
                if (this.mimelist.IndexOf(item) < 0)
                {
                    this._errorType = 2;
                    this._errorStr = "上传文件格式类型不正确";
                    return false;
                }
                file.SaveAs(savePath);
                return true;
            }
            catch
            {
                this._errorType = 0;
                this._errorStr = "意外错误";
                return false;
            }
        }

        public string ErrorStr
        {
            get
            {
                return this._errorStr;
            }
        }

        public int ErrorType
        {
            get
            {
                return this._errorType;
            }
        }
    }
}

