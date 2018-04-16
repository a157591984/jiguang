using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {

	/// <summary>
	/// 普通文本框控件
	/// </summary>
	public class TextBox : System.Web.UI.WebControls.TextBox {

		#region 构造函数

		/// <summary>
		/// 默认构造函数
		/// </summary>
		public TextBox() {
			this.MaxLength = 50;
		}

		#endregion

		#region 定义属性

		/// <summary>
		/// 允许输入按键值
		/// </summary>
		protected string[] _allowKeyPressCodes = null;

		/// <summary>
		/// 禁止按键的值
		/// </summary>
		protected string[] _prohibitionKeyPressCodes = null;

		/// <summary>
		/// 是否为必填控件
		/// </summary>
		public bool IsRequired { get; set; }

		private int _minLength = 0;
		/// <summary>
		/// 控件需要填入的最少字符数
		/// </summary>		
		public int MinLength {
			get { return _minLength; }
			set {
				if (value < 0) {
					throw new Exception(string.Format("控件需要输入的最小字符数不能小于0，当前为：", value));
				}
				_minLength = value;
			}
		}

		private ShowErrorTypes _showErrorType = ShowErrorTypes.Inline;
		/// <summary>
		/// 数据错误验证错误信息提示方式
		/// </summary>
		public ShowErrorTypes ShowErrorType {
			get { return _showErrorType; }
			set { _showErrorType = value; }
		}

		/// <summary>
		/// 在用户没有在文本框中输入内容时的提示文本
		/// </summary>
		public string WatermarkText { get; set; }

		/// <summary>
		/// 自定义的验证正则表达式
		/// </summary>
		public string ValidationExpression { get; set; }

		private bool _isFilterSqlChars = true;
		/// <summary>
		/// 是否过滤SQL特殊字符
		/// </summary>
		public bool IsFilterSqlChars {
			get { return _isFilterSqlChars; }
			set { _isFilterSqlChars = value; }
		}

		private bool _isFilterSpecialChars = true;
		/// <summary>
		/// 是否过滤指定的特殊字符
		/// </summary>
		public bool IsFilterSpecialChars {
			get { return _isFilterSpecialChars; }
			set { _isFilterSpecialChars = value; }
		}

		private string _requiredErrorMessage = "不能为空";
		/// <summary>
		/// 必填错误提示信息
		/// </summary>
		public string RequiredErrorMessage {
			get { return _requiredErrorMessage; }
			set { _requiredErrorMessage = value; }
		}

		private string _formatErrorMessage = "内容不符要求";
		/// <summary>
		/// 格式错误提示信息
		/// </summary>
		public string FormatErrorMessage {
			get { return _formatErrorMessage; }
			set { _formatErrorMessage = value; }
		}

		#endregion

		#region 输出控件内容

		/// <summary>
		/// 改写OnPreRender
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) {
			//添加默认样式表
			if (string.IsNullOrWhiteSpace(this.CssClass)) {
				this.CssClass = "myTextBox";
			}
			else if (this.CssClass.IndexOf("myTextBox") == -1) {
				this.CssClass += " myTextBox";
			}

			if (IsRequired) {
				if (this.Attributes["IsRequired"]==null)
					this.Attributes.Add("IsRequired", "True");
				else 
					this.Attributes["IsRequired"] = "true";
			}
			if (MinLength > 0) {
				if (this.Attributes["MinLength"] == null)
					this.Attributes.Add("MinLength", MinLength.ToString());
				else 
					this.Attributes["MinLength"] = MinLength.ToString();
			}
			if (!string.IsNullOrWhiteSpace(WatermarkText)) {
				if (this.Attributes["placeholder"] == null)
					this.Attributes.Add("placeholder", WatermarkText);
				else
					this.Attributes["placeholder"] = WatermarkText;
			}
			if (!string.IsNullOrWhiteSpace(ValidationExpression)) {
				if (this.Attributes["ValidationExpression"] == null)
					this.Attributes.Add("ValidationExpression", ValidationExpression);
				else
					this.Attributes["ValidationExpression"] = ValidationExpression;
			}
			if (IsFilterSqlChars) {
				if (this.Attributes["IsFilterSqlChars"] == null)
					this.Attributes.Add("IsFilterSqlChars", "True");
				else
					this.Attributes["IsFilterSqlChars"] = "True";
			}
			if (IsFilterSpecialChars) {
				if (this.Attributes["IsFilterSpecialChars"] == null)
					this.Attributes.Add("IsFilterSpecialChars", "True");
				else
					this.Attributes["IsFilterSpecialChars"] = "True";
			}

			if (this.Attributes["MyInputType"] == null)
				this.Attributes.Add("MyInputType", this.GetType().Name);
			else
				this.Attributes["MyInputType"] = this.GetType().Name;

			if (this.Attributes["ShowErrorType"] == null)
				this.Attributes.Add("ShowErrorType", ShowErrorType.ToString());
			else
				this.Attributes["ShowErrorType"] = ShowErrorType.ToString();

			if (this.TextMode == System.Web.UI.WebControls.TextBoxMode.MultiLine) {
				this.Attributes.Add("TextAreaMaxLength", MaxLength.ToString());
			}

			base.OnPreRender(e);
		}

		/// <summary>
		/// 重写Render
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(System.Web.UI.HtmlTextWriter writer) {			
			base.Render(writer);
			if (this.IsRequired == true) {
				writer.Write("<span class='span_required_check' style='display:none;' id='span_required_check_for_{0}'>{1}</span>", this.ClientID, RequiredErrorMessage);
			}
			writer.WriteLine("<span class='span_format_check' style='display:none;' id='span_format_check_for_{0}'>{1}</span>", this.ClientID, FormatErrorMessage);

			StringBuilder sb = new StringBuilder(300);			
			if (_allowKeyPressCodes != null) {
				sb.Append("(");
				foreach (var strValue in _allowKeyPressCodes) {
					var strValues = strValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					switch (strValues.Length) {
						case 1:
							sb.AppendFormat("event.which=={0} || ", strValues[0]);
							break;
						case 2:
							sb.AppendFormat("(event.which>={0} && event.which<={1}) || ", strValues[0], strValues[1]);
							break;
						default:
							throw new Exception(string.Format("错误的过滤按钮设置值：{0}", strValue));
					}
				}
				sb.Append("event.which==8 || event.which==0");		//加上退格和删除键				
				sb.Append(")");
			}			

			if (_prohibitionKeyPressCodes != null) {
				if (sb.Length > 0) {
					sb.Append(" && (");
				}
				else {
					sb.Append("(");
				}

				foreach (var strValue in _prohibitionKeyPressCodes) {
					var strValues = strValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					switch (strValues.Length) {
						case 1:
							sb.AppendFormat("event.which!={0} && ", strValues[0]);
							break;						
						default:
							throw new Exception(string.Format("错误的过滤按钮设置值：{0}", strValue));
					}
				}
				sb.Length -= 4;
				sb.Append(")");
			}

			if (sb.Length > 0) {
				writer.WriteLine("<script type='text/javascript'>");
				writer.WriteLine("$('#{0}').keypress(function(event) {{", this.ClientID);
				writer.WriteLine("var checkResult = {0};", sb);
				writer.WriteLine("if(!checkResult) {{ $('#{0}').css('border-color', '#900'); }} ", this.ClientID);
				writer.WriteLine("else {{ $('#{0}').css('border-color', ''); }} ", this.ClientID);
				writer.WriteLine("return checkResult;");
				writer.WriteLine("});");
				writer.WriteLine("</script>");
			}
		}

		#endregion

		#region 数据验证部分

		/// <summary>
		/// 控件数据是否通过验证
		/// </summary>
		public bool IsValid {
			get {
				return DataValid();
			}
		}

		/// <summary>
		/// 验证数据
		/// </summary>
		/// <returns></returns>
		protected virtual bool DataValid() {


			return true;
		}

		#endregion
	}
}
