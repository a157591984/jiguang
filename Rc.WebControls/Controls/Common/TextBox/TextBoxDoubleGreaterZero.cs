using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {

	/// <summary>
	/// 整数文本框
	/// </summary>
	public class TextBoxDoubleGreaterZero : TextBoxDouble {

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBoxDoubleGreaterZero() {
			MinValue = 0;
			MaxValue = 1000000;
			MaxLength = 15;

			WatermarkText = "非负小数";

			_allowKeyPressCodes = new string[] { 
				"48,57",
				"46"
			};
		}

	}
}