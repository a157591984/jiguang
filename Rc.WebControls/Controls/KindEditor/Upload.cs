using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;

namespace PHHC.Web.Controls.KindEditor {

	/// <summary>
	/// KindEditor上传文件方法
	/// </summary>
	public class Upload : IHttpHandler {

		//定义允许上传的文件扩展名
		private String fileTypes = "gif,jpg,jpeg,png,doc,docx,xls,xlsx,ppt,pptx";

		private HttpContext context;

		/// <summary>
		/// 处理请求
		/// </summary>
		/// <param name="context"></param>
		public void ProcessRequest(HttpContext context) {
			this.context = context;

			HttpPostedFile imgFile = context.Request.Files["imgFile"];
			if (imgFile == null) {
				showError("请选择文件。");
			}

			if (imgFile.InputStream == null || imgFile.InputStream.Length > 200 * 1024 * 1024) {
				showError(string.Format("被上传文件不能大于 {0} K，现在大小为 {1} K。", 200, imgFile.InputStream.Length));
			}

			string fileExt = Path.GetExtension(imgFile.FileName).ToLower().Substring(1);
			if (String.IsNullOrEmpty(fileExt) || !fileTypes.Contains(fileExt)) {
				showError("上传文件扩展名是不允许的扩展名。");
			}

			string filePath;
			string fileUrl;
			if (fileExt == "doc" || fileExt == "docx" || fileExt == "xls" || fileExt == "xlsx" || fileExt == "ppt" || fileExt == "pptx") {
				Utility.FileUploadManage.GetSaveFullPath(UploadFileTypes.File, imgFile.FileName, out filePath, out fileUrl);
			}
			else {
				Utility.FileUploadManage.GetSaveFullPath(UploadFileTypes.Image, imgFile.FileName, out filePath, out fileUrl);
			}
			imgFile.SaveAs(filePath);

			Hashtable hash = new Hashtable();
			hash["error"] = 0;
			hash["url"] = fileUrl;
			context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			context.Response.Write(JsonMapper.ToJson(hash));
			context.Response.End();
		}

		private void showError(string message) {
			Hashtable hash = new Hashtable();
			hash["error"] = 1;
			hash["message"] = message;
			context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			context.Response.Write(JsonMapper.ToJson(hash));
			context.Response.End();
		}

		/// <summary>
		/// 是否可重复使用
		/// </summary>
		public bool IsReusable {
			get {
				return true;
			}
		}
	}
}