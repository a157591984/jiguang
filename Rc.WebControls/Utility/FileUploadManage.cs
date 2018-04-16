using System;
using System.Collections.Generic;
using System.IO;

namespace Rc.WebControls.Utility {

	/// <summary>
	/// 用户管理上传文件的类
	/// </summary>
	public static class FileUploadManage {

		private const string FILE_UPLOAD_PATH = "/FileUpload";

		/// <summary>
		/// 根据上传的文件名，生成包含文件名的文件存储的绝对URl
		/// </summary>
		/// <param name="fileName">需要上传的文件名</param>
		/// <param name="absoluteUrl">上传后访问该文件的绝对URl地址</param>
		/// <param name="saveFullPath">该文件的磁盘保存路径</param>
		/// <param name="uploadFileType">上传文件类型</param>
		/// <returns></returns>
		public static void GetSaveFullPath(UploadFileTypes uploadFileType, string fileName, out string saveFullPath, out string absoluteUrl) {

			//生成用于保存文件的完整路径
			DateTime now = DateTime.Now;
			//资源访问的相对url目录			
			string newUrl = FILE_UPLOAD_PATH.JoinUrl(uploadFileType.ToString()).JoinUrl(now.ToString("yyyyMM"));
			string newDir = System.Web.HttpContext.Current.Server.MapPath(newUrl);

			//如果目录不存在，那么就创建目录
			if (!Directory.Exists(newDir))
				Directory.CreateDirectory(newDir);

			//生成上传后的文件名
			string newFileName = null;
			int no = 0;
			do {
				newFileName = string.Format("{0}{1}", now.Ticks + no, System.IO.Path.GetExtension(fileName));
				saveFullPath = newDir.JoinPath(newFileName);
				no++;
			} while (File.Exists(saveFullPath));

			//获取资源的绝对URL
			absoluteUrl = newUrl.JoinUrl(newFileName);
		}
	}
}
