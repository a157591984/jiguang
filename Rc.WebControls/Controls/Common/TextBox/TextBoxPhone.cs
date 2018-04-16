using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {
	
	/// <summary>
	/// 整数文本框
	/// </summary>
	public class TextBoxPhone : TextBox {

		/// <summary>
		/// 电话类型枚举
		/// </summary>
		public enum PhoneTypes {
			/// <summary>
			/// 固定电话
			/// </summary>
			FixedPhone, 
			/// <summary>
			/// 移动电话
			/// </summary>
			MobilePhone,
			/// <summary>
			/// 所有电话格式
			/// </summary>
			All }

		private PhoneTypes _phoneType = PhoneTypes.All;
		/// <summary>
		/// 电话类型
		/// </summary>
		public PhoneTypes PhoneType {
			set { _phoneType = value; }
			get { return _phoneType; }
		}

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBoxPhone() {
			MinLength = 11;

			_allowKeyPressCodes = new string[] { 
				"48,57",
				"45"
			};
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) {

			if (string.IsNullOrWhiteSpace(ValidationExpression)) {
				switch (PhoneType) {
					case PhoneTypes.All:
						if (string.IsNullOrWhiteSpace(WatermarkText))
							WatermarkText = "请输入电话号码";
						this.ValidationExpression = @"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)";
						break;
					case PhoneTypes.FixedPhone:
						if (string.IsNullOrWhiteSpace(WatermarkText))
							WatermarkText = "区号-固定电话-分机";
						this.ValidationExpression = @"^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,5})?$";
						break;
					case PhoneTypes.MobilePhone:
						if (string.IsNullOrWhiteSpace(WatermarkText))
							WatermarkText = "11位手机号码";
						this.ValidationExpression = @"^((\(\d{3}\))|(\d{3}\-))?13\d{9}|15\d{9}|18\d{9}";
						break;
				}
			}
			if (MaxLength == 0) {
				switch (PhoneType) {
					case PhoneTypes.All:
						MaxLength = 18;
						break;
					case PhoneTypes.FixedPhone:
						MaxLength = 18;
						break;
					case PhoneTypes.MobilePhone:
						MaxLength = 11;
						break;
				}
			}

			if (this.Attributes["PhoneType"] == null)
				this.Attributes.Add("PhoneType", PhoneType.ToString());
			else
				this.Attributes["PhoneType"] = PhoneType.ToString();
			
			base.OnPreRender(e);
		}
	}
}
