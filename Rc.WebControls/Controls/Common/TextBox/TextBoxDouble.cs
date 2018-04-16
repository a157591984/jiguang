using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {

	/// <summary>
	/// 整数文本框
	/// </summary>
	public class TextBoxDouble : TextBox {

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBoxDouble() {
			MinValue = 0;
			MaxValue = 1000000;
			MaxLength = 15;
			WatermarkText = "小数";
			this.ValidationExpression = @"^\d+(\.\d{0,2})?$";

			_allowKeyPressCodes = new string[] { 
				"48,57",
				"45",
				"46"
			};
		}

		#region 属性定义

		/// <summary>
		/// 可以录入的最大值
		/// </summary>
		public double MaxValue { get; set; }

		/// <summary>
		/// 可以录入的最小值
		/// </summary>
		public double MinValue { get; set; }

		private int _decimalDigits = 2;
		/// <summary>
		/// 最少小数位数
		/// </summary>
		public int DecimalDigits {
			get { return _decimalDigits; }
			set {
				if (value < 0) {
					throw new Exception(string.Format("小数输入框小数最多位数不能小于零，当前为：{0}。", value));
				}
				if (value > 6) {
					throw new Exception(string.Format("小数输入框小数最多位数为6，当前为：{0}。", value));
				}
				_decimalDigits = value;
				this.ValidationExpression = string.Format(@"^\d+(\.\d{{0,{0}}})?$", value);
			}
		}

		#endregion

		/// <summary>
		/// OnPreRender
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
		public double TextDouble {
			get { return double.Parse(this.Text); }
			set { this.Text = value.ToString(); }
		}
	}
}
