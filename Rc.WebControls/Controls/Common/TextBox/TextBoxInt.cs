using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {
	
	/// <summary>
	/// 整数文本框
	/// </summary>
	public class TextBoxInt : TextBox{

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBoxInt() {
			MinValue = 0;
			MaxValue = 1000000;
			this.ValidationExpression = @"^-?\d+$";

			WatermarkText = "整数";

			_allowKeyPressCodes = new string[] { 
				"48,57",
				"45"
			};
		}

		/// <summary>
		/// 可以录入的最大值
		/// </summary>
		public int MaxValue { get; set; }

		/// <summary>
		/// 可以录入的最小值
		/// </summary>
		public int MinValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e) {
			if (MaxLength == 0) {
				MaxLength = 10;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) {
			if (this.Attributes["MaxValue"] == null)
				this.Attributes.Add("MaxValue", MaxValue.ToString());
			else
				this.Attributes["MaxValue"] = MaxValue.ToString();

			if (this.Attributes["MinValue"] == null)
				this.Attributes.Add("MinValue", MinValue.ToString());
			else
				this.Attributes["MinValue"] = MinValue.ToString();

			if (MaxValue < MinValue) {
				throw new ArgumentOutOfRangeException(string.Format("控件的最大值不能小于最小值，当前为：{0} - {1}", MinValue, MaxValue));
			}

			base.OnPreRender(e);
		}

		/// <summary>
		/// 当前控件的整数值
		/// </summary>
		public int TextInt {
			get {
				if (string.IsNullOrWhiteSpace(this.Text)) {
					return 0;
				}
				else {
					return int.Parse(this.Text);
				}
			}
			set { this.Text = value.ToString(); }
		}
	}
}
