using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {

	/// <summary>
	/// 整数文本框
	/// </summary>
	public class TextBoxEmail : TextBox {

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBoxEmail() {
			this.ValidationExpression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			MaxLength = 50;

			WatermarkText = "电子邮箱";

			_allowKeyPressCodes = new string[] { 
				"48,57",
				"97,122",
				"65,90",
				"45",
				"46",
				"64",
				"95"
			};
		}		
	}
}
