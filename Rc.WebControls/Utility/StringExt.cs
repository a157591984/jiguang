using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace Rc.WebControls.Utility {

	/// <summary>
	/// 用户连接多个字符串
	/// </summary>
	public static class StringExt {

		/// <summary>匹配所有的换行和Tag空格</summary>
		public static Regex RegexNewLine = new Regex(@"(\n)|(\r)|(\t)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
		/// <summary>匹配所有的换行和Tag空格</summary>
		public static Regex RegexScript = new Regex(@"<script([^>]*?)>(.*?)</script>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
		/// <summary>匹配Title标签内容</summary>
		public static Regex RegexPageTitle = new Regex(@"<title>([\s\S]*?)</title>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
		/// <summary>匹配一个开始HTML标签</summary>
		public static Regex RegexStartTag = new Regex("<([^/])+>");
		/// <summary>匹配一个结束的HTML标签</summary>
		public static Regex RegexEndTag = new Regex("</(\\w)+>");
		/// <summary>匹配一个或多个连续的数字</summary>
		public static Regex RegexNumber = new Regex("(\\d)+");
        /// <summary>匹配一个或多个连续的数字</summary>
        public static Regex RegexEmail = new Regex(@"(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)");
		/// <summary>所有HTML标记</summary>
		public static Regex RegexHtmlTag = new Regex("<.*?>");
		/// <summary>所有HTML特殊字符标记</summary>
		public static Regex RegexHtmlSpecialChar = new Regex(@"&(\w+)?;");

		/// <summary>encodingGb2312</summary>
		public static Encoding EncodingGb2312 = Encoding.GetEncoding("gb2312");
		/// <summary>EncodingBig5</summary>
		public static Encoding EncodingBig5 = Encoding.GetEncoding("big5");
		/// <summary>EncodingGb18030</summary>
		public static Encoding EncodingGb18030 = Encoding.GetEncoding("gb18030");


		/// <summary>用户拆分路径和URL</summary>
		private static readonly char[] _splitsUrlPath = new char[] { '/', '\\' };
		/// <summary>逻辑或的关系</summary>
		private static readonly char[] SplitOr = new char[] { '|' };
		/// <summary>逻辑与的关系</summary>
		private static readonly char[] SplitAnd = new char[] { '&' };

		#region 连接多个字符串，组成Url地址

		/// <summary>
		/// 判断一个字符串是否为标准的URL地址
		/// </summary>
		/// <param name="strValue"></param>
		/// <returns></returns>
		public static bool IsWebLink(this string strValue) {
			return System.Uri.IsWellFormedUriString(strValue, UriKind.Absolute);
		}

		/// <summary>
		/// 用于连接多个URL地址，最后得到的地址不以“/”结尾。
		/// </summary>
		/// <param name="rootUrl">根Url</param>
		/// <param name="strValues">附加的Url子路径集合</param>
		/// <returns></returns>
		public static string JoinUrl(this string rootUrl, params string[] strValues) {
			IList<string> strUrls = new List<string>();
			//去掉首尾的空格
			string newRootUrl = rootUrl.Trim().Trim(_splitsUrlPath).Replace('\\', '/');

			//如果原路径是绝对路径，那么保留绝对路径
			if (rootUrl.StartsWith("/")) {
				if (string.IsNullOrEmpty(newRootUrl))
					strUrls.Add(string.Empty);
				else
					strUrls.Add("/" + newRootUrl);
			}
			else {
				strUrls.Add(newRootUrl);
			}

			for (int i = 0, j = strValues.Length; i < j; i++) {
				var strValue = strValues[i].Trim().Trim(_splitsUrlPath).Replace('\\', '/');
				if (!string.IsNullOrEmpty(strValue))
					strUrls.Add(strValue);
			}

			return string.Join("/", strUrls);
		}
		#endregion

		#region 连接过个字符串，组成路径

		/// <summary>
		/// 将多个文件访问地址进行连接，最后得到的地址不以“\”结尾
		/// </summary>
		/// <param name="rootPath">根路径</param>
		/// <param name="strValues">附加的子路径集合</param>
		/// <returns></returns>
		public static string JoinPath(this string rootPath, params string[] strValues) {
			IList<string> strPaths = new List<string>();
			strPaths.Add(rootPath.Trim().Trim(_splitsUrlPath).Replace('/', '\\'));

			for (int i = 0, j = strValues.Length; i < j; i++) {
				var strValue = strValues[i].Trim().Trim(_splitsUrlPath).Replace('/', '\\');
				if (!string.IsNullOrEmpty(strValue))
					strPaths.Add(strValue);
			}

			return string.Join("\\", strPaths);
		}

		#endregion

		#region 对URL进行处理

		/// <summary>
		/// 获取Url中除文件名以外的部分
		/// </summary>
		/// <param name="url">需要分析的Url</param>
		/// <returns></returns>
		public static string GetUriPath(this string url) {
			url.Replace('\\', '/');
			int last1 = url.LastIndexOf('.');
			int last2 = url.LastIndexOf('/');
			//如果最后一个点在/之后，那么我们可以认为该路径中存在文件名，那么过滤掉文件名
			//如果最后一个/的位置小于10，那么该Url只是一个域名，那么也不需要截取
			if (last1 > last2 && last2 > 10)
				return url.Substring(0, last2);
			else
				return url;
		}

		/// <summary>
		/// 返回指定URI中的域名部分。
		/// </summary>
		/// <param name="url">url地址</param>
		/// <returns></returns>
		public static string GetUriDomain(this string url) {
			url.Replace('\\', '/');

			//首先获取连接协议位置
			int place = url.IndexOf("://");
			if (place == -1)
				return url;

			place = url.IndexOf('/', place + 4);

			if (place == -1)
				return url;
			else
				return url.Substring(0, place);
		}

		/// <summary>
		/// 返回指定URI中的域名部分。
		/// </summary>
		/// <param name="url">url地址</param>
		/// <returns></returns>
		public static string GetUriDomain(this Uri url) {
			return string.Format("{0}://{1}", url.Scheme, url.Authority);
		}
		

		#endregion

		#region 根据给定的字符串，拆分输出并输出集合

		/// <summary>
		/// 根据传入的字符串，拆分返回集合，并且返回是逻辑与的关系，还是逻辑或的关系
		/// 这个功能主要用于拆分多个栏目、ID、长度、Tag等数据
		/// </summary>
		/// <param name="strValue">输入的字符串</param>
		/// <returns></returns>
		public static string[] GetStringArrayFromString(string strValue) {
			bool isAndRelation;
			return GetStringArrayFromString(strValue, out isAndRelation);
		}

		/// <summary>
		/// 根据传入的字符串，拆分返回集合，并且返回是逻辑与的关系，还是逻辑或的关系
		/// 这个功能主要用于拆分多个栏目、ID、长度、Tag等数据
		/// </summary>
		/// <param name="strValue">输入的字符串</param>
		/// <param name="isAndRelation">输出的字符串</param>
		/// <returns></returns>
		public static string[] GetStringArrayFromString(string strValue, out bool isAndRelation) {
			isAndRelation = SplitStringIsAnd(strValue);
			if (isAndRelation)
				return strValue.Split(SplitAnd, StringSplitOptions.RemoveEmptyEntries);
			else
				return strValue.Split(SplitOr, StringSplitOptions.RemoveEmptyEntries);
		}

		/// <summary>
		/// 根据传入的字符串，拆分返回集合，并且返回是逻辑与的关系，还是逻辑或的关系
		/// 这个功能主要用于拆分多个栏目、ID、长度、Tag等数据
		/// </summary>
		/// <param name="strValue">输入的字符串</param>
		/// <returns></returns>
		public static Guid[] GetGuidArrayFromString(string strValue) {
			bool isAndRelation;
			return GetGuidArrayFromString(strValue, out isAndRelation);
		}

		/// <summary>
		/// 根据传入的字符串，拆分返回集合，并且返回是逻辑与的关系，还是逻辑或的关系
		/// 这个功能主要用于拆分多个栏目、ID、长度、Tag等数据
		/// </summary>
		/// <param name="strValue">输入的字符串</param>
		/// <param name="isAndRelation">输出的字符串</param>
		/// <returns></returns>
		public static Guid[] GetGuidArrayFromString(string strValue, out bool isAndRelation) {
			isAndRelation = SplitStringIsAnd(strValue);
			string[] strValues;
			if (isAndRelation)
				strValues = strValue.Split(SplitAnd, StringSplitOptions.RemoveEmptyEntries);
			else
				strValues = strValue.Split(SplitOr, StringSplitOptions.RemoveEmptyEntries);

			Guid[] newValues = new Guid[strValues.Length];
			for (int i = 0; i < newValues.Length; i++)
				newValues[i] = new Guid(strValues[i]);
			return newValues;
		}

		/// <summary>
		/// 根据传入的字符串，拆分返回集合，并且返回是逻辑与的关系，还是逻辑或的关系
		/// 这个功能主要用于拆分多个栏目、ID、长度、Tag等数据
		/// </summary>
		/// <param name="strValue">输入的字符串</param>
		/// <returns></returns>
		public static int[] GetIntArrayFromString(string strValue) {
			bool isAndRelation;
			return GetIntArrayFromString(strValue, out isAndRelation);
		}

		/// <summary>
		/// 根据传入的字符串，拆分返回集合，并且返回是逻辑与的关系，还是逻辑或的关系
		/// 这个功能主要用于拆分多个栏目、ID、长度、Tag等数据
		/// </summary>
		/// <param name="strValue">输入的字符串</param>
		/// <param name="isAndRelation">输出的字符串</param>
		/// <returns></returns>
		public static int[] GetIntArrayFromString(string strValue, out bool isAndRelation) {
			isAndRelation = SplitStringIsAnd(strValue);
			string[] strValues;
			if (isAndRelation)
				strValues = strValue.Split(SplitAnd, StringSplitOptions.RemoveEmptyEntries);
			else
				strValues = strValue.Split(SplitOr, StringSplitOptions.RemoveEmptyEntries);

			int[] newValues = new int[strValues.Length];
			for (int i = 0; i < newValues.Length; i++)
				newValues[i] = int.Parse(strValues[i]);
			return newValues;
		}

		//检查需要拆分的字符串
		private static bool SplitStringIsAnd(string strValue) {
			int orPlace = strValue.IndexOf('|');
			int andPlace = strValue.IndexOf('&');
			if (orPlace != -1 && andPlace != -1)
				throw new Exception(string.Format("不能在拆分字符串：{0}中同时包含&和|符号。", strValue));

			if (orPlace != -1)
				return false;

			return true;
		}

		#endregion

		#region 将一个字符串集合转换为一个字符串

		/// <summary>
		/// 将一个字符串集合转换为一个字符串
		/// </summary>
		/// <param name="items">字符串集合</param>
		/// <param name="splitChar">分隔符</param>
		/// <returns></returns>
		public static string GetStringFromList(this IList<string> items, string splitChar = "|") {
			StringBuilder sb = new StringBuilder(splitChar);
			foreach (var item in items)
				sb.AppendFormat("{0}{1}", item.Trim(), splitChar);

			return sb.ToString();
		}

		/// <summary>
		/// 将一个SortedList转换为一个字符串
		/// </summary>
		/// <param name="items">字符串集合</param>
		/// <param name="splitChar">分隔符</param>
		/// <returns></returns>
		public static string GetStringFromSortList(this SortedList<string,string> items, string splitChar = "|") {
			StringBuilder sb = new StringBuilder(splitChar);
			foreach (var item in items)
				sb.AppendFormat("{0}:{1}{2}", item.Key.Trim(), item.Value.Trim(), splitChar);

			return sb.ToString();
		}

		#endregion

		#region 转换为ExtendedASCII，解决webbrowser显示一些中文页面时的乱码问题

		/// <summary>
		/// 转换为ExtendedASCII
		/// </summary>
		/// <param name="strHtml">要转换的html</param>
		/// <returns></returns>
		public static string ConvertExtendedASCII(string strHtml) {
			StringBuilder str = new StringBuilder();
			char c;
			for (int i = 0; i < strHtml.Length; i++) {
				c = strHtml[i];
				if (Convert.ToInt32(c) > 127) {
					str.Append("&#" + Convert.ToInt32(c) + ";");
				}
				else {
					str.Append(c);
				}
			}
			return str.ToString();
		}

		#endregion

		#region 生成一个随机密码

		/// <summary>
		/// 随机生成一个指定长度的密码
		/// </summary>
		/// <param name="passwordLength">密码的长度</param>
		/// <returns></returns>
		public static string BuildNewPassword(int passwordLength) {
			char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
			char[] letters = new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 
				'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

			StringBuilder sb = new StringBuilder(passwordLength);
			for (int i = 0; i < passwordLength; i++) {
				Random random = new Random(DateTime.Now.Millisecond * i);
				if (i % 2 == 0) {
					sb.Append(numbers[random.Next(numbers.Length)]);
				}
				else {
					sb.Append(letters[random.Next(letters.Length)]);
				}
			}

			return sb.ToString();
		}

		#endregion

		#region 将指定的字符串转换为适合javascript中Alert能够正确输出的内容
		/// <summary>
		/// 将指定的字符串转换为适合javascript中Alert能够正确输出的内容。
		/// </summary>
		/// <param name="strValue"></param>
		/// <returns></returns>
		public static string ConvertToAlterText(string strValue) {
			if (!string.IsNullOrEmpty(strValue)) {
				strValue = strValue.Replace("\'", "\"");
				strValue = strValue.Replace("\n", "");
				strValue = strValue.Replace("\r", "");
				strValue = strValue.Replace("\t", "");
				strValue = strValue.Trim();
			}
			return strValue;
		}
		#endregion

		#region 将密码字符串转换为密码

		/// <summary>
		/// 将密码字符串转换为密码
		/// </summary>
		/// <param name="password">需要加密的密码</param>
		/// <returns></returns>
		public static string ConvertToSAH1(this string password) {
			return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
		}

		#endregion
	}
}
