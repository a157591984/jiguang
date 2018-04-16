using System;
using System.Web;

namespace Rc.WebControls {

	/// <summary>
	/// 生成验证码图片
	/// </summary>
	public class CheckCodeHandler : IHttpHandler {
		
		private const int CODE_LENGTH = 4;			//验证码长度
		private static char[] _chars = new char[] {'a','b','c','d','e','f','g','h','i','j','k','n',
			'm','p','q','r','s','t','u','v','w','x','y','z','2','3','4','5','6','7','8','9','A','B','C','D','E',
			'F','G','H','K','L','M','N','P','Q','R','S','T','U','V','W','X','Y','Z'};		

		/// <summary>
		/// 根据请求返回需要的验证码图片
		/// </summary>
		/// <param name="context">httpContext对象</param>
		public void ProcessRequest(HttpContext context) {
			string checkCode = GetCheckCode();

			var buildImageByText = new Rc.WebControls.HttpHandler.BuildImageByText();
			buildImageByText.FontSize = 16;
			buildImageByText.Build(checkCode, ref context);

			var verificationCodeType = (VerificationCodeTypes)int.Parse(context.Request.Params["type"]);
			//根据不同的请求，写入不同的COOKIE
			switch (verificationCodeType) {
				case VerificationCodeTypes.Login:
					context.Response.Cookies.Add(new HttpCookie(ImageVerificationCode.LOGIN_COOKIE_NAME, checkCode)); break;
				case VerificationCodeTypes.Regist:
					context.Response.Cookies.Add(new HttpCookie(ImageVerificationCode.REGISTER_COOKIE_NAME, checkCode)); break;
				case VerificationCodeTypes.Other:
					context.Response.Cookies.Add(new HttpCookie(ImageVerificationCode.OTHER_COOKIE_NAME, checkCode)); break;
				default:
					throw new Exception(string.Format("{0}是未知的验证码COOKIE类型(login,regist,common)。", context.Request.Params["type"]));
			}
		}

		/// <summary>
		/// 指示其他请求是否可以使用 IHttpHandler 实例
		/// </summary>
		public bool IsReusable {
			get { return true; }
		}

		/// <summary>
		/// 生成随机的字母
		/// </summary>
		/// <returns>验证码</returns>
		private string GetCheckCode() {
			char[] checkCode = new char[CODE_LENGTH];
			Random rand = new Random(DateTime.Now.Millisecond);
			for (int i = 0; i < CODE_LENGTH; i++) {
				checkCode[i] = _chars[rand.Next(_chars.Length)];
			}
			string strCheckCode = new string(checkCode);
			return strCheckCode;
		}
	}
}
