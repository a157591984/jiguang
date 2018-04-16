using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Rc.WebControls {

	/// <summary>
	/// 数字文本框
	/// </summary>
	public class ButtonRefresh : System.Web.UI.WebControls.Literal {

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public ButtonRefresh() {
			Enabled = true;
		}

		/// <summary>
		/// 设置按钮上面显示的文本
		/// </summary>
		public new string Text { get; set; }

		/// <summary>
		/// 控件是否可用
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Render
		/// </summary>
		protected override void Render(System.Web.UI.HtmlTextWriter writer) {
			if (this.Visible) {
				base.Text = string.Format("<input class='MyButton' type='button' onclick='location.reload(true);' value='{0}' {1} />",
					this.Text, Enabled ? string.Empty : "disabled='disabled'");
			}
		}
	}
}
