using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {

	/// <summary>
	/// 整数文本框
	/// </summary>
	public class TextBoxUrl : TextBox {

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBoxUrl() {
			this.ValidationExpression = @"^((\w+):\/\/)?((\w+):?(\w+)?@)?([^\/\?:]+):?(\d+)?(\/?[^\?#]+)?\??([^#]+)?#?(\w*)";
			MaxLength = 255;

			WatermarkText = "网址";
		}

		private UriKind _uriKind = UriKind.Absolute;
		/// <summary>
		/// URL的模式
		/// </summary>
		public UriKind UriKind {
			get {return _uriKind; }
			set { 
				_uriKind = value;
				switch (_uriKind) { 
					case System.UriKind.Relative:
					case System.UriKind.RelativeOrAbsolute:
						this.ValidationExpression = @"^/[\w-]+(/[\w- ./?%&=]*)?";
						break;
				}
			}
		}
	}
}
