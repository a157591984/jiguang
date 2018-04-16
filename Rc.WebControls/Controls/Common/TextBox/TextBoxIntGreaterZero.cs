using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {

	/// <summary>
	/// 正整数文本框
	/// </summary>
	public class TextBoxIntGreaterZero : TextBoxInt {

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBoxIntGreaterZero() {
			MinValue = 0;
			MaxValue = 1000000;
			this.ValidationExpression = @"^\d+$";

			WatermarkText = "非负整数";

			_allowKeyPressCodes = new string[] { 
				"48,57"
			};
		}

	}
}