using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.WebControls {
	/// <summary>
	/// 校验码类型，因为一个页面中可能存在多个验证码，需要做出区分
	/// </summary>
	public enum VerificationCodeTypes {
		/// <summary>
		/// 登录验证码
		/// </summary>
		Login,
		/// <summary>
		/// 注册验证码
		/// </summary>
		Regist,
		/// <summary>
		/// 其他验证码
		/// </summary>
		Other
	}
}
