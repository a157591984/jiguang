using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {
	
	/// <summary>
	/// 整数文本框
	/// </summary>
	public class TextBoxDate : TextBox {

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBoxDate() {
			this.ValidationExpression = @"^(\d{4})(\-)(01|02|03|04|05|06|07|08|09|10|11|12)(\-)([0-3]?\d)$";

			WatermarkText = "日期格式";

			_allowKeyPressCodes = new string[] { 
				"48,57",
				"45"
			};
		}

		#region 定义属性

		/// <summary>
		/// 日历控件日期显示类型枚举
		/// </summary>
		public enum TextBoxDateShowTypes { 
			/// <summary>
			/// 按日选择
			/// </summary>
			Day, 
			/// <summary>
			/// 按月选择
			/// </summary>
			Month,
			/// <summary>
			/// 年
			/// </summary>
			Year
		};

		private TextBoxDateShowTypes _textBoxDateShowTypes = TextBoxDateShowTypes.Day;
		/// <summary>
		/// 日历控件日期显示类型，按日，还是按月
		/// </summary>
		public TextBoxDateShowTypes TextBoxDateShowType {
			get { return _textBoxDateShowTypes; }
			set { _textBoxDateShowTypes = value; }
		}

		private DateTime _minValue = DateTime.MinValue;
		/// <summary>
		/// 最大日期范围
		/// </summary>
		public DateTime MinValue {
			get { return _minValue; }
			set { _minValue = value; }
		}

		private DateTime _maxValue = DateTime.MaxValue;
		/// <summary>
		/// 最小日期范围
		/// </summary>
		public DateTime MaxValue {
			get { return _maxValue; }
			set { _maxValue = value; }
		}

		/// <summary>
		/// 当前控件能够选择最大日期对应控件ID
		/// </summary>
		public string MaxDateControlId { get; set; }

		/// <summary>
		/// 当前控件能够选择最小日期对应控件ID
		/// </summary>
		public string MinDateControlId { get; set; }

		#endregion

		/// <summary>
		/// OnPreRender
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) {
			//日期范围
			StringBuilder sb = new StringBuilder(50);

			if (this.Attributes["onclick"] != null) {
				sb.Append(this.Attributes["onclick"]);
				sb.Append(";");
			}

			switch (TextBoxDateShowType) {
				case TextBoxDateShowTypes.Day:
					this.ValidationExpression = @"^(\d{4})(\-)(01|02|03|04|05|06|07|08|09|10|11|12)(\-)([0-3]?\d)$";
					sb.Append("WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd'");
					break;
				case TextBoxDateShowTypes.Month:
					this.ValidationExpression = @"^(\d{4})(\-)(01|02|03|04|05|06|07|08|09|10|11|12)$";
					sb.Append("WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM'");
					break;
				case TextBoxDateShowTypes.Year:
					this.ValidationExpression = @"^(\d{4})$";
					sb.Append("WdatePicker({skin:'whyGreen',dateFmt:'yyyy'");
					break;
			}

			if (string.IsNullOrWhiteSpace(MinDateControlId)) {
				sb.AppendFormat(",minDate:'{0}'", _minValue.ToString("yyyy-MM-dd"));
			}
			else {
				sb.Append(",minDate:'#F{$dp.$D(\\'");
				sb.Append(MinDateControlId);
				sb.Append("\\',{d:0});}'");
			}
			if (string.IsNullOrWhiteSpace(MaxDateControlId)) {
				sb.AppendFormat(",maxDate:'{0}'", _maxValue.ToString("yyyy-MM-dd"));
			}
			else {
				sb.Append(",maxDate:'#F{$dp.$D(\\'");
				sb.Append(MinDateControlId);
				sb.Append("\\',{d:0});}'");
			}

			sb.Append("})");
			this.Attributes.Add("onclick", sb.ToString());
			base.OnPreRender(e);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(System.Web.UI.HtmlTextWriter writer) {
			writer.WriteLine("<script src='/Scripts/My97DatePicker/WdatePicker.js' type='text/javascript'></script>");
			base.Render(writer);			
		}

		/// <summary>
		/// 设置或获取日期的值
		/// </summary>
		public DateTime? TextDate {
			get {
				if (string.IsNullOrWhiteSpace(this.Text))
					return null;
				else
					return DateTime.Parse(this.Text);
			
			}
			set {
				if (value.HasValue) {
					this.Text = value.Value.ToString("yyyy-MM-dd");
				}
				else {
					this.Text = string.Empty;
				}
			}
		}
	}
}
