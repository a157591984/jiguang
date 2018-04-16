using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Text;

namespace Rc.WebControls {

	/// <summary>
	/// 所有页面的基类
	/// </summary>
	public class BasePage : System.Web.UI.Page {

		#region 输出提示脚本

		/// <summary>
		/// 用户显示一个弹出提示框
		/// </summary>
		/// <param name="message">要显示的信息</param>
		/// <param name="millisec">提示停留的时间</param>
		/// <param name="dialogTop">距离顶部的距离</param>
		protected void ShowAutoCloseAlert(string message, int millisec = 2000, int? dialogTop = null) {
			System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"),
				string.Format("JqueryCustomDialog.AlertAutoClose('{0}', {1}, {2});", message, millisec,
				(dialogTop == null) ? "undefined" : dialogTop.ToString()),
				true);
		}

		/// <summary>
		/// 用户显示一个弹出提示框
		/// </summary>
		/// <param name="message">要显示的信息</param>
		protected void ShowAlert(string message) {
			ShowAlert(message, false);
		}

		/// <summary>
		/// 用户显示一个弹出提示框
		/// </summary>
		/// <param name="message">要显示的信息</param>
		/// <param name="dialogTop">对话框距离顶部的距离</param>
		/// <param name="dialogWidth">对话框的宽度</param>
		protected void ShowAlert(string message, int dialogWidth, int dialogTop) {
			System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"),
			string.Format("JqueryCustomDialog.AlertAtPlace('{0}', {1}, {2});", message, dialogWidth, dialogTop), true);
		}

		/// <summary>
		/// 用户显示一个弹出提示框
		/// </summary>
		/// <param name="message">要显示的信息</param>
		/// <param name="blnCloseWindows">是否显示完信息后关闭窗口</param>
		protected void ShowAlert(string message, bool blnCloseWindows) {
			string strScript;
			if (blnCloseWindows) {
				strScript = "window.close();";
			}
			else {
				strScript = string.Empty;
			}

			message = Utility.StringExt.ConvertToAlterText(message);
			System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"),
				string.Format("JqueryCustomDialog.Alert('{0}');{1};", message, strScript), true);
		}

		/// <summary>
		/// 用户显示一个弹出提示框
		/// </summary>
		/// <param name="message">要显示的信息</param>
		/// <param name="newUrl">显示完提示信息后跳转到那个页面</param>
		protected void ShowAlert(string message, string newUrl) {
			string strNewUrlScript;
			if (!string.IsNullOrEmpty(newUrl)) {
				strNewUrlScript = string.Format("window.location.href='{0}'", newUrl);
			}
			else {
				strNewUrlScript = string.Empty;
			}

			message = Utility.StringExt.ConvertToAlterText(message);
			System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"),
				string.Format("JqueryCustomDialog.Alert('{0}');{1};", message, strNewUrlScript), true);
		}

		/// <summary>
		/// 返回请求的页面
		/// </summary>
		protected void ReturnPrePage() {
			ClientScript.RegisterStartupScript(this.GetType(), "tiaozhuan", "window.history.back(-1)", true);
		}

		#endregion

		#region 定义弹出窗口

		/// <summary>
		/// 获取显示模态对话框的代码
		/// </summary>
		/// <param name="strUrl">需要打开的页面Url</param>
		/// <param name="width">对话框的宽度</param>
		/// <param name="height">对话框的高度</param>
		/// <returns></returns>
		protected string GetShowModalDialogString(string strUrl, int width, int height) {
			return string.Format("javascript:showModalDialog('{0}','_blank','dialogWidth:{1}px;DialogHeight={2}px;help=0;center=1;status:yes;scroll=1');", strUrl, width, height);
		}

		/// <summary>
		/// 获取显示模态对话框的代码
		/// </summary>
		/// <param name="strUrl">需要打开的页面Url</param>
		protected string GetShowModalDialogString(string strUrl) {
			return string.Format("javascript:showModalDialog('{0}','_blank','dialogWidth:'+{1}+'px;DialogHeight='+{2}+'px;help=0;center=1;status:yes;scroll=1');",
				strUrl,
				"(window.screen.availWidth-40)",
				"(window.screen.availHeight-120)");
		}

		/// <summary>
		/// 获取显示非模态对话框的代码
		/// </summary>
		/// <param name="strUrl">需要打开的页面Url</param>
		/// <param name="width">对话框的宽度</param>
		/// <param name="height">对话框的高度</param>
		/// <returns></returns>
		protected string GetShowModelessDialogString(string strUrl, int width, int height) {
			return string.Format("javascript:window.open('{0}','_blank','width='+{1}+',height='+{2}+',toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no');void(null);", strUrl, width, height);
		}

		/// <summary>
		/// 获取显示非模态对话框的代码
		/// </summary>
		/// <param name="strUrl">需要打开的页面Url</param>
		protected string GetShowModelessDialogString(string strUrl) {
			return string.Format("javascript:window.open('{0}','_blank','width='+{1}+',height='+{2}+',toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no');void(null);",
				strUrl,
				"(window.screen.availWidth-40)",
				"(window.screen.availHeight-120)");
		}

		#endregion

		#region 刷新当前页面

		/// <summary>
		/// 重新加载页面
		/// </summary>
		protected void ReLoadPage() {
			Response.Redirect(Request.Url.ToString());
		}

		#endregion

		#region 注册页面头部脚本块

		/// <summary>
		/// 注册页面头部脚本块
		/// </summary>
		/// <param name="strScript"></param>
		protected void RegisterStartupScript(string strScript) {
			System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), strScript, true);
		}

		#endregion
	}
}