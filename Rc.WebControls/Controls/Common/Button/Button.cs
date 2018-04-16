using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {

	/// <summary>
	/// 提交按钮
	/// </summary>
	public class Button : System.Web.UI.WebControls.Button {

		/// <summary>
		/// 出发数据验证功能
		/// </summary>
		public Button() {
			this.CausesValidation = true;
			TargetControlID = string.Empty;
		}

		#region 定义属性

		/// <summary>
		/// 点击按钮时的提示文本
		/// </summary>
		public string PromptMessage { get; set; }

		/// <summary>
		/// 在点击按钮时，如果显示方式为alert，那么是否只显示第一条错误
		/// </summary>
		public bool IsOnlyShowFirstError { get; set; }

		/// <summary>
		/// 本按钮出发的验证控件的范围，如果指定，只检查控件的内部控件
		/// </summary>
		public string TargetControlID { get; set; }

		#endregion		

		/// <summary>
		/// 输出之前
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) {

			if (string.IsNullOrWhiteSpace(this.CssClass)) {
				this.CssClass = "MyButton";
			}
			else {
				this.CssClass += " MyButton";
			}

			if (!string.IsNullOrEmpty(PromptMessage)) {
				PromptMessage = PromptMessage.Replace("\'", "\"");
				PromptMessage = PromptMessage.Replace("\n", "");
				PromptMessage = PromptMessage.Replace("\r", "");
				PromptMessage = PromptMessage.Replace("\t", "");
				PromptMessage = PromptMessage.Trim();
			}

			string validaJs;
			if (this.CausesValidation) {		//如果启动了验证
				//生成脚本
				if (!string.IsNullOrWhiteSpace(PromptMessage)) {
					validaJs = string.Format("javascript:return Phhc_data_check_for_page_on_submit({0}, '{1}') && Show_My_Confirm('{1}')",
						(IsOnlyShowFirstError) ? "true" : "false", PromptMessage, TargetControlID.Trim());
				}
				else {
					validaJs = string.Format("javascript:return Phhc_data_check_for_page_on_submit({0}, '{1}')",
						(IsOnlyShowFirstError) ? "true" : "false", TargetControlID.Trim());
				}
				//添加脚本
				if (string.IsNullOrWhiteSpace(this.OnClientClick)) {
					this.OnClientClick = string.Format("{0};", validaJs);
				}
				else if (this.OnClientClick.IndexOf("return Phhc_data_check_for_page_on_submit") == -1) {
					if (this.OnClientClick.ToUpper().IndexOf("RETURN ") != -1) {
						throw new Exception("您自定义的OnClientClick中不能包含return。");
					}
					this.OnClientClick = string.Format("{0} && {1}", validaJs, this.OnClientClick);
				}
			}
			else if (!string.IsNullOrWhiteSpace(PromptMessage)) {		//如果没有启用验证并且有确认信息，那么	
				//生成脚本
				validaJs = string.Format("javascript:return Show_My_Confirm('{0}')", PromptMessage);
				//添加脚本
				if (string.IsNullOrWhiteSpace(this.OnClientClick)) {
					this.OnClientClick = string.Format("{0};", validaJs);
				}
				else if (this.OnClientClick.IndexOf("return Show_My_Confirm") == -1) {
					if (this.OnClientClick.ToUpper().IndexOf("RETURN ") != -1) {
						throw new Exception("您自定义的OnClientClick中不能包含return。");
					}
					this.OnClientClick = string.Format("{0} && {1}", validaJs, this.OnClientClick);
				}
			}

			base.OnPreRender(e);
		}
	}
}
