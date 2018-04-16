using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {
	
	/// <summary>
	/// 整数文本框
	/// </summary>
	public class TextBoxPostCode : TextBox {

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBoxPostCode() {
			MaxLength = 6;
			MinLength = 6;
			this.ValidationExpression = @"^\d{6}$";

			WatermarkText = "邮政编码";

			_allowKeyPressCodes = new string[] { 
				"48,57"
			};
		}
	}
}
