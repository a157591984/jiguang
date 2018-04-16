using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {

	/// <summary>
	/// 整数文本框
	/// </summary>
	public class TextBoxNoSpace : TextBox {

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBoxNoSpace() {
			_prohibitionKeyPressCodes = new string[] { 
				"32"
			};

			WatermarkText = "不含空格的内容";
		}
	}
}
