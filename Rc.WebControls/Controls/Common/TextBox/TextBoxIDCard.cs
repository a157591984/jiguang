using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {
	
	/// <summary>
	/// 整数文本框
	/// </summary>
	public class TextBoxIDCard : TextBox {

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBoxIDCard() {
			this.ValidationExpression = @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)";
			MaxLength = 18;
			MinLength = 18;

			WatermarkText = "身份证号码";

			_allowKeyPressCodes = new string[] { 
				"48,57",
				"88",
				"120"
			};
		}
	}
}
