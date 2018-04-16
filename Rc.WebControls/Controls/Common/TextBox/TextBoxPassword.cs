using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {
	
	/// <summary>
	/// 整数文本框
	/// </summary>
	public class TextBoxPassword : TextBox {	

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e) {
			MaxLength = 20;
			MinLength = 6;
			TextMode = System.Web.UI.WebControls.TextBoxMode.Password;
		}		

		/// <summary>
		/// 获取经过SHA密码后的密码文本
		/// </summary>
		public string PasswordText {
			get { return Utility.StringExt.ConvertToSAH1(base.Text); }
		}
	}
}
