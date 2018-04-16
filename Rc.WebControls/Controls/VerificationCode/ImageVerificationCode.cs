using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {
	
	/// <summary>
	/// 验证码控件
	/// </summary>
	public class ImageVerificationCode : System.Web.UI.Control {

		/// <summary>验证登陆的COOKIE名称</summary>
		public const string LOGIN_COOKIE_NAME = "anylen_login_cookie_name";
		/// <summary>验证注册的COOKIE名称</summary>
		public const string REGISTER_COOKIE_NAME = "anylen_register_cookie_name";
		/// <summary>通用的验证COOKIE名称</summary>
		public const string OTHER_COOKIE_NAME = "anylen_other_cookie_name";

		/// <summary>
		/// 设置或获取验证码的类型
		/// </summary>
		public VerificationCodeTypes VerificationCodeType { get; set; }

		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public ImageVerificationCode() {
			VerificationCodeType = VerificationCodeTypes.Other;
		}

		/// <summary>
		/// 重写render
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(System.Web.UI.HtmlTextWriter writer) {

			writer.WriteLine("<img src='/PublicComponent/BuildCheckCodeImage.ashx?type={0}' alt='{1}验证码'/>",
				(int)VerificationCodeType, VerificationCodeType);

			base.Render(writer);
		}

		/// <summary>
		/// 检查用户登陆输入的验证码是否有效
		/// </summary>
		/// <param name="checkCode">用户输入的验证码</param>
		/// <returns></returns>
		public bool IsValid(string checkCode) {
			checkCode = checkCode.Trim().ToUpper();
			switch (VerificationCodeType) { 
				case VerificationCodeTypes.Login:
					return this.Page.Request.Cookies[ImageVerificationCode.LOGIN_COOKIE_NAME].Value.ToUpper() == checkCode;
				case VerificationCodeTypes.Regist:
					return this.Page.Request.Cookies[ImageVerificationCode.REGISTER_COOKIE_NAME].Value.ToUpper() == checkCode;
				case VerificationCodeTypes.Other:
					return this.Page.Request.Cookies[ImageVerificationCode.OTHER_COOKIE_NAME].Value.ToUpper() == checkCode;
				default:
					return false;
			}			
		}

		/// <summary>
		/// 检查用户登陆输入的验证码是否有效
		/// </summary>
		/// <param name="page">控件所属页面的实例</param>
		/// <param name="checkCode">用户输入的验证码</param>
		/// <returns></returns>
		public static bool IsLoginValid(System.Web.UI.Page page, string checkCode) {
			return page.Request.Cookies[ImageVerificationCode.LOGIN_COOKIE_NAME].Value.ToLower() == checkCode.Trim().ToLower();
		}

		/// <summary>
		/// 检查用户注册输入的验证码是否有效
		/// </summary>
		/// <param name="page">控件所属页面的实例</param>
		/// <param name="checkCode">用户输入的验证码</param>
		/// <returns></returns>
		public static bool IsRegisterValid(System.Web.UI.Page page, string checkCode) {
			return page.Request.Cookies[ImageVerificationCode.REGISTER_COOKIE_NAME].Value.ToLower() == checkCode.Trim().ToLower();
		}
	}
}
