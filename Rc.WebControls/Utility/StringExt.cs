using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace Rc.WebControls.Utility {

	/// <summary>
	/// �û����Ӷ���ַ���
	/// </summary>
	public static class StringExt {

		/// <summary>ƥ�����еĻ��к�Tag�ո�</summary>
		public static Regex RegexNewLine = new Regex(@"(\n)|(\r)|(\t)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
		/// <summary>ƥ�����еĻ��к�Tag�ո�</summary>
		public static Regex RegexScript = new Regex(@"<script([^>]*?)>(.*?)</script>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
		/// <summary>ƥ��Title��ǩ����</summary>
		public static Regex RegexPageTitle = new Regex(@"<title>([\s\S]*?)</title>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
		/// <summary>ƥ��һ����ʼHTML��ǩ</summary>
		public static Regex RegexStartTag = new Regex("<([^/])+>");
		/// <summary>ƥ��һ��������HTML��ǩ</summary>
		public static Regex RegexEndTag = new Regex("</(\\w)+>");
		/// <summary>ƥ��һ����������������</summary>
		public static Regex RegexNumber = new Regex("(\\d)+");
        /// <summary>ƥ��һ����������������</summary>
        public static Regex RegexEmail = new Regex(@"(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)");
		/// <summary>����HTML���</summary>
		public static Regex RegexHtmlTag = new Regex("<.*?>");
		/// <summary>����HTML�����ַ����</summary>
		public static Regex RegexHtmlSpecialChar = new Regex(@"&(\w+)?;");

		/// <summary>encodingGb2312</summary>
		public static Encoding EncodingGb2312 = Encoding.GetEncoding("gb2312");
		/// <summary>EncodingBig5</summary>
		public static Encoding EncodingBig5 = Encoding.GetEncoding("big5");
		/// <summary>EncodingGb18030</summary>
		public static Encoding EncodingGb18030 = Encoding.GetEncoding("gb18030");


		/// <summary>�û����·����URL</summary>
		private static readonly char[] _splitsUrlPath = new char[] { '/', '\\' };
		/// <summary>�߼���Ĺ�ϵ</summary>
		private static readonly char[] SplitOr = new char[] { '|' };
		/// <summary>�߼���Ĺ�ϵ</summary>
		private static readonly char[] SplitAnd = new char[] { '&' };

		#region ���Ӷ���ַ��������Url��ַ

		/// <summary>
		/// �ж�һ���ַ����Ƿ�Ϊ��׼��URL��ַ
		/// </summary>
		/// <param name="strValue"></param>
		/// <returns></returns>
		public static bool IsWebLink(this string strValue) {
			return System.Uri.IsWellFormedUriString(strValue, UriKind.Absolute);
		}

		/// <summary>
		/// �������Ӷ��URL��ַ�����õ��ĵ�ַ���ԡ�/����β��
		/// </summary>
		/// <param name="rootUrl">��Url</param>
		/// <param name="strValues">���ӵ�Url��·������</param>
		/// <returns></returns>
		public static string JoinUrl(this string rootUrl, params string[] strValues) {
			IList<string> strUrls = new List<string>();
			//ȥ����β�Ŀո�
			string newRootUrl = rootUrl.Trim().Trim(_splitsUrlPath).Replace('\\', '/');

			//���ԭ·���Ǿ���·������ô��������·��
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

		#region ���ӹ����ַ��������·��

		/// <summary>
		/// ������ļ����ʵ�ַ�������ӣ����õ��ĵ�ַ���ԡ�\����β
		/// </summary>
		/// <param name="rootPath">��·��</param>
		/// <param name="strValues">���ӵ���·������</param>
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

		#region ��URL���д���

		/// <summary>
		/// ��ȡUrl�г��ļ�������Ĳ���
		/// </summary>
		/// <param name="url">��Ҫ������Url</param>
		/// <returns></returns>
		public static string GetUriPath(this string url) {
			url.Replace('\\', '/');
			int last1 = url.LastIndexOf('.');
			int last2 = url.LastIndexOf('/');
			//������һ������/֮����ô���ǿ�����Ϊ��·���д����ļ�������ô���˵��ļ���
			//������һ��/��λ��С��10����ô��Urlֻ��һ����������ôҲ����Ҫ��ȡ
			if (last1 > last2 && last2 > 10)
				return url.Substring(0, last2);
			else
				return url;
		}

		/// <summary>
		/// ����ָ��URI�е��������֡�
		/// </summary>
		/// <param name="url">url��ַ</param>
		/// <returns></returns>
		public static string GetUriDomain(this string url) {
			url.Replace('\\', '/');

			//���Ȼ�ȡ����Э��λ��
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
		/// ����ָ��URI�е��������֡�
		/// </summary>
		/// <param name="url">url��ַ</param>
		/// <returns></returns>
		public static string GetUriDomain(this Uri url) {
			return string.Format("{0}://{1}", url.Scheme, url.Authority);
		}
		

		#endregion

		#region ���ݸ������ַ��������������������

		/// <summary>
		/// ���ݴ�����ַ�������ַ��ؼ��ϣ����ҷ������߼���Ĺ�ϵ�������߼���Ĺ�ϵ
		/// ���������Ҫ���ڲ�ֶ����Ŀ��ID�����ȡ�Tag������
		/// </summary>
		/// <param name="strValue">������ַ���</param>
		/// <returns></returns>
		public static string[] GetStringArrayFromString(string strValue) {
			bool isAndRelation;
			return GetStringArrayFromString(strValue, out isAndRelation);
		}

		/// <summary>
		/// ���ݴ�����ַ�������ַ��ؼ��ϣ����ҷ������߼���Ĺ�ϵ�������߼���Ĺ�ϵ
		/// ���������Ҫ���ڲ�ֶ����Ŀ��ID�����ȡ�Tag������
		/// </summary>
		/// <param name="strValue">������ַ���</param>
		/// <param name="isAndRelation">������ַ���</param>
		/// <returns></returns>
		public static string[] GetStringArrayFromString(string strValue, out bool isAndRelation) {
			isAndRelation = SplitStringIsAnd(strValue);
			if (isAndRelation)
				return strValue.Split(SplitAnd, StringSplitOptions.RemoveEmptyEntries);
			else
				return strValue.Split(SplitOr, StringSplitOptions.RemoveEmptyEntries);
		}

		/// <summary>
		/// ���ݴ�����ַ�������ַ��ؼ��ϣ����ҷ������߼���Ĺ�ϵ�������߼���Ĺ�ϵ
		/// ���������Ҫ���ڲ�ֶ����Ŀ��ID�����ȡ�Tag������
		/// </summary>
		/// <param name="strValue">������ַ���</param>
		/// <returns></returns>
		public static Guid[] GetGuidArrayFromString(string strValue) {
			bool isAndRelation;
			return GetGuidArrayFromString(strValue, out isAndRelation);
		}

		/// <summary>
		/// ���ݴ�����ַ�������ַ��ؼ��ϣ����ҷ������߼���Ĺ�ϵ�������߼���Ĺ�ϵ
		/// ���������Ҫ���ڲ�ֶ����Ŀ��ID�����ȡ�Tag������
		/// </summary>
		/// <param name="strValue">������ַ���</param>
		/// <param name="isAndRelation">������ַ���</param>
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
		/// ���ݴ�����ַ�������ַ��ؼ��ϣ����ҷ������߼���Ĺ�ϵ�������߼���Ĺ�ϵ
		/// ���������Ҫ���ڲ�ֶ����Ŀ��ID�����ȡ�Tag������
		/// </summary>
		/// <param name="strValue">������ַ���</param>
		/// <returns></returns>
		public static int[] GetIntArrayFromString(string strValue) {
			bool isAndRelation;
			return GetIntArrayFromString(strValue, out isAndRelation);
		}

		/// <summary>
		/// ���ݴ�����ַ�������ַ��ؼ��ϣ����ҷ������߼���Ĺ�ϵ�������߼���Ĺ�ϵ
		/// ���������Ҫ���ڲ�ֶ����Ŀ��ID�����ȡ�Tag������
		/// </summary>
		/// <param name="strValue">������ַ���</param>
		/// <param name="isAndRelation">������ַ���</param>
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

		//�����Ҫ��ֵ��ַ���
		private static bool SplitStringIsAnd(string strValue) {
			int orPlace = strValue.IndexOf('|');
			int andPlace = strValue.IndexOf('&');
			if (orPlace != -1 && andPlace != -1)
				throw new Exception(string.Format("�����ڲ���ַ�����{0}��ͬʱ����&��|���š�", strValue));

			if (orPlace != -1)
				return false;

			return true;
		}

		#endregion

		#region ��һ���ַ�������ת��Ϊһ���ַ���

		/// <summary>
		/// ��һ���ַ�������ת��Ϊһ���ַ���
		/// </summary>
		/// <param name="items">�ַ�������</param>
		/// <param name="splitChar">�ָ���</param>
		/// <returns></returns>
		public static string GetStringFromList(this IList<string> items, string splitChar = "|") {
			StringBuilder sb = new StringBuilder(splitChar);
			foreach (var item in items)
				sb.AppendFormat("{0}{1}", item.Trim(), splitChar);

			return sb.ToString();
		}

		/// <summary>
		/// ��һ��SortedListת��Ϊһ���ַ���
		/// </summary>
		/// <param name="items">�ַ�������</param>
		/// <param name="splitChar">�ָ���</param>
		/// <returns></returns>
		public static string GetStringFromSortList(this SortedList<string,string> items, string splitChar = "|") {
			StringBuilder sb = new StringBuilder(splitChar);
			foreach (var item in items)
				sb.AppendFormat("{0}:{1}{2}", item.Key.Trim(), item.Value.Trim(), splitChar);

			return sb.ToString();
		}

		#endregion

		#region ת��ΪExtendedASCII�����webbrowser��ʾһЩ����ҳ��ʱ����������

		/// <summary>
		/// ת��ΪExtendedASCII
		/// </summary>
		/// <param name="strHtml">Ҫת����html</param>
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

		#region ����һ���������

		/// <summary>
		/// �������һ��ָ�����ȵ�����
		/// </summary>
		/// <param name="passwordLength">����ĳ���</param>
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

		#region ��ָ�����ַ���ת��Ϊ�ʺ�javascript��Alert�ܹ���ȷ���������
		/// <summary>
		/// ��ָ�����ַ���ת��Ϊ�ʺ�javascript��Alert�ܹ���ȷ��������ݡ�
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

		#region �������ַ���ת��Ϊ����

		/// <summary>
		/// �������ַ���ת��Ϊ����
		/// </summary>
		/// <param name="password">��Ҫ���ܵ�����</param>
		/// <returns></returns>
		public static string ConvertToSAH1(this string password) {
			return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
		}

		#endregion
	}
}
