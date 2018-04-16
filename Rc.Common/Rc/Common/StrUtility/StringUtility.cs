namespace Rc.Common.StrUtility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI.WebControls;

    public static class StringUtility
    {
        public static void AddEmptyItem(this DropDownList ddlList, string defValue = "=请选择=")
        {
            ListItem item = new ListItem {
                Text = defValue,
                Value = ""
            };
            ddlList.Items.Insert(0, item);
        }

        public static string checkID(string ID)
        {
            if ((ID == null) && (ID == string.Empty))
            {
                throw new Exception("参数传递错误!<li>参数不能为空</li>");
            }
            ID = ID.Filter();
            return ID;
        }

        public static bool CheckStr(this string str)
        {
            char[] chArray = str.ToArray<char>();
            for (int i = 0; i < str.Length; i++)
            {
                if ((chArray[i] > 0xff00) && (chArray[i] < 0xff5f))
                {
                    break;
                }
                if (i == (str.Length - 1))
                {
                    if ((chArray[i] <= 0xff00) || (chArray[i] >= 0xff5f))
                    {
                        return false;
                    }
                    break;
                }
            }
            return true;
        }

        public static string ClearHTML(this string Str)
        {
            string str = "";
            if ((Str != null) && (Str != string.Empty))
            {
                string pattern = @"<\/*[^<>]*>";
                str = Regex.Replace(Str, pattern, "");
            }
            return str.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
        }

        public static string ClearKeyOfSQL(this string input)
        {
            if ((input == null) || (input.Length == 0))
            {
                return input;
            }
            string pattern = "delete|insert|update|drop|alter|exec";
            return Regex.Replace(input, pattern, "");
        }

        public static string clearLastZero(this string x)
        {
            double result = 0.0;
            double.TryParse(x, out result);
            return result.ToString("G");
        }

        public static string ClearScript(this string Input)
        {
            if ((Input != string.Empty) && (Input != null))
            {
                StringBuilder builder = new StringBuilder(Input);
                builder.Replace("<", "&lt;");
                builder.Replace(">", "&gt;");
                return builder.ToString();
            }
            return Input;
        }

        public static string CutLastChar(this string Input)
        {
            if (string.IsNullOrEmpty(Input))
            {
                return string.Empty;
            }
            return Input.Substring(Input.Length - 1, 1);
        }

        public static string CutLastChar(this string Input, string indexStr)
        {
            if (Input.IndexOf(indexStr) >= 0)
            {
                return Input.Remove(Input.LastIndexOf(indexStr));
            }
            return Input;
        }

        public static string DataChar(this string source)
        {
            source = Regex.Replace(source, "select ", "selecｔ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "insert ", "inserｔ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "delete ", "deletｅ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "count'' ", "counｔ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "table ", "tablｅ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "truncate ", "truncatｅ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "asc ", "asＣ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "mid ", "miｄ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "char ", "chaｒ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "xp_", "xp＿　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "exec master ", "exeｃ masteｒ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "net localgroup administrators ", "neｔ localgrouｐ administratorｓ ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "and ", "anｄ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "net user ", "neｔ useｒ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "or ", "oｒ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "net ", "neｔ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "--", "＿　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "delete ", "deletｅ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "drop ", "droｐ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "Exec ", "Exeｃ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "Execute ", "Executｅ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "sp_", "sp＿", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "script", "scripｔ　", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "from ", "froｍ　", RegexOptions.IgnoreCase);
            return source;
        }

        public static string Filter(this string sInput)
        {
            if ((sInput == null) || (sInput == ""))
            {
                return null;
            }
            string input = sInput.ToLower();
            string str2 = sInput;
            string str = "*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(input, Regex.Escape(str), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                throw new Exception("字符串中含有非法字符!");
            }
            return str2.Replace("'", "''");
        }

        public static string FilterAll(this string old)
        {
            old = old.ScriptFilter();
            old = old.DataChar();
            old = old.SpeChar();
            return old;
        }

        public static string FilterHTML(string html)
        {
            if (html == null)
            {
                return "";
            }
            Regex regex = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(@" href *= *[\s\S]*script *:", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@" on[\s\S]*=", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            Regex regex6 = new Regex(@"\<img[^\>]+\>", RegexOptions.IgnoreCase);
            Regex regex7 = new Regex("</p>", RegexOptions.IgnoreCase);
            Regex regex8 = new Regex("<p>", RegexOptions.IgnoreCase);
            Regex regex9 = new Regex("<[^>]*>", RegexOptions.IgnoreCase);
            html = regex.Replace(html, "");
            html = regex2.Replace(html, "");
            html = regex3.Replace(html, " _disibledevent=");
            html = regex4.Replace(html, "");
            html = regex5.Replace(html, "");
            html = regex6.Replace(html, "");
            html = regex7.Replace(html, "");
            html = regex8.Replace(html, "");
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }

        public static string FilterScriptData(this string old)
        {
            old = old.ScriptFilter();
            old = old.DataChar();
            return old;
        }

        public static string FilterSpec(this string old)
        {
            old = old.SpeChar();
            return old;
        }

        public static string[] GetBir(this string nubmer)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = nubmer.Substring(6, 8);
            str = str4.Substring(0, 4);
            str2 = str4.Substring(4, 2);
            str3 = str4.Substring(6, 2);
            return new string[] { str, str2, str3 };
        }

        public static string GetCut(object obj)
        {
            string str = obj.ToString();
            if (str.Length > 8)
            {
                return (str.Substring(0, 8) + "...");
            }
            return str;
        }

        public static string GetMonthStr(string month)
        {
            if (month == null)
            {
                return null;
            }
            if (int.Parse(month) < 10)
            {
                return string.Format("0{0}", month);
            }
            return month;
        }

        public static string GetRealNumberValue(this string number)
        {
            if (!string.IsNullOrWhiteSpace(number) && (number.IndexOf(".") > -1))
            {
                Regex regex = new Regex("0*$");
                return regex.Replace(number, "").TrimEnd(new char[] { '.' });
            }
            return number;
        }

        public static string getSpell(this string cnChar)
        {
            byte[] bytes = Encoding.Default.GetBytes(cnChar);
            if (bytes.Length <= 1)
            {
                return cnChar;
            }
            int num = bytes[0];
            int num2 = bytes[1];
            int num3 = (num << 8) + num2;
            int[] numArray = new int[] { 
                0xb0a1, 0xb0c5, 0xb2c1, 0xb4ee, 0xb6ea, 0xb7a2, 0xb8c1, 0xb9fe, 0xbbf7, 0xbbf7, 0xbfa6, 0xc0ac, 0xc2e8, 0xc4c3, 0xc5b6, 0xc5be, 
                0xc6da, 0xc8bb, 0xc8f6, 0xcbfa, 0xcdda, 0xcdda, 0xcdda, 0xcef4, 0xd1b9, 0xd4d1
             };
            for (int i = 0; i < 0x1a; i++)
            {
                int num5 = 0xd7fa;
                if (i != 0x19)
                {
                    num5 = numArray[i + 1];
                }
                if ((numArray[i] <= num3) && (num3 < num5))
                {
                    return Encoding.Default.GetString(new byte[] { (byte) (0x41 + i) });
                }
            }
            return "*";
        }

        public static int GetStrLengthCN(this string Input)
        {
            byte[] bytes = new ASCIIEncoding().GetBytes(Input);
            int num = 0;
            for (int i = 0; i <= (bytes.Length - 1); i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num++;
                }
                num++;
            }
            return num;
        }

        public static string GetSubString(this string Str, int Num)
        {
            if (string.IsNullOrEmpty(Str))
            {
                return "";
            }
            string str = string.Empty;
            int num = 0;
            foreach (char ch in Str)
            {
                num += Encoding.Default.GetByteCount(ch.ToString());
                if (num > Num)
                {
                    return (str + "...");
                }
                str = str + ch;
            }
            return str;
        }

        public static string GetSubString(this string Str, int Num, string LastStr)
        {
            if (string.IsNullOrEmpty(Str))
            {
                return "";
            }
            if (Str.Length <= Num)
            {
                return Str;
            }
            return (Str.Substring(0, Num) + LastStr);
        }

        public static string HtmlDecode(this string Input)
        {
            if (string.IsNullOrEmpty(Input))
            {
                return Input;
            }
            return HttpUtility.HtmlDecode(Input);
        }

        public static string HtmlEncode(this string Input)
        {
            if (string.IsNullOrEmpty(Input))
            {
                return Input;
            }
            return HttpUtility.HtmlEncode(Input);
        }

        public static string HtmlToTxt(this string Input)
        {
            StringBuilder builder = new StringBuilder(Input);
            builder.Replace("&nbsp;", " ");
            builder.Replace("<br>", "\r\n");
            builder.Replace("<br>", "\n");
            builder.Replace("<br />", "\n");
            builder.Replace("<br />", "\r\n");
            builder.Replace("&lt;", "<");
            builder.Replace("&gt;", ">");
            builder.Replace("&amp;", "&");
            return builder.ToString();
        }

        public static bool IsDate(this string s)
        {
            try
            {
                DateTime.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDateTime(this string Input)
        {
            try
            {
                DateTime.Parse(Input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDecimal(this string Input)
        {
            if (Input == null)
            {
                return false;
            }
            try
            {
                decimal.Parse(Input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsEmail(this string strInput)
        {
            if (strInput.IsNullOrEmpty())
            {
                return false;
            }
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return regex.IsMatch(strInput);
        }

        public static bool IsFloat(this string strInupt)
        {
            if (strInupt.IsNullOrEmpty())
            {
                return false;
            }
            Regex regex = new Regex(@"^-?\d{1,10}(.\d+)?$");
            return regex.IsMatch(strInupt);
        }

        public static bool isIdCard(this string input)
        {
            int num;
            string pattern = "^d{8}((0[1-9])|(1[012]))(0[1-9]|[12][0-9]|3[01])d{3}$";
            string str2 = "^d{6}(19)|(20)d{2}((0[1-9])|(1[012]))(0[1-9]|[12][0-9]|3[01])d{3}[0-9]|X$";
            bool flag = Regex.IsMatch(input, pattern) || Regex.IsMatch(input, str2, RegexOptions.IgnoreCase);
            if (!flag)
            {
                return flag;
            }
            string str3 = string.Empty;
            if (input.Length == 15)
            {
                num = 0;
                str3 = "19";
            }
            else
            {
                num = 2;
            }
            str3 = str3 + input.Substring(6, 6 + num);
            string str4 = str3.Substring(0, 4);
            string str5 = str3.Substring(4, 2);
            string str6 = str3.Substring(6, 2);
            return (str4 + "-" + str5 + "-" + str6).IsDate();
        }

        public static bool IsImgString(this string Input)
        {
            return Input.IsImgString("/{@dirfile}/");
        }

        public static bool IsImgString(this string Input, string checkStr)
        {
            bool flag = false;
            if (!(Input != string.Empty))
            {
                return flag;
            }
            string str = Input.ToLower();
            if ((str.IndexOf(checkStr.ToLower()) == -1) || (str.IndexOf(".") == -1))
            {
                return flag;
            }
            string str2 = str.Substring(str.LastIndexOf(".") + 1).ToString().ToLower();
            if ((!(str2 == "jpg") && !(str2 == "gif")) && (!(str2 == "bmp") && !(str2 == "png")))
            {
                return flag;
            }
            return true;
        }

        public static bool IsInt(this string Input)
        {
            if (Input == null)
            {
                return false;
            }
            return Input.IsInteger(false);
        }

        public static bool IsIntBetween(this string strInput, int? minValue, int? maxValue)
        {
            if (!strInput.IsInteger())
            {
                return false;
            }
            int num = Convert.ToInt32(strInput);
            if (minValue.HasValue)
            {
                if (num < minValue)
                {
                    return false;
                }
            }
            if (maxValue.HasValue)
            {
                if (num > maxValue)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsInteger(this string Input)
        {
            if (Input == null)
            {
                return false;
            }
            return Input.IsInteger(true);
        }

        public static bool IsInteger(this string Input, bool Plus)
        {
            if (Input == null)
            {
                return false;
            }
            string pattern = "^-?[0-9]+$";
            if (Plus)
            {
                pattern = "^[0-9]+$";
            }
            return Regex.Match(Input, pattern, RegexOptions.Compiled).Success;
        }

        public static bool IsLength(this string strInput, int? minLength, int? maxLength)
        {
            int strLengthCN = strInput.GetStrLengthCN();
            if (minLength.HasValue)
            {
                if (strLengthCN < minLength)
                {
                    return false;
                }
            }
            if (maxLength.HasValue)
            {
                if (strLengthCN > maxLength)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsMatch(this string strInput, string parttern)
        {
            Regex regex = new Regex(parttern);
            return regex.IsMatch(strInput);
        }

        public static bool IsMobile(this string strInput)
        {
            if (strInput.IsNullOrEmpty())
            {
                return false;
            }
            Regex regex = new Regex(@"^\d{8,20}$");
            return regex.IsMatch(strInput);
        }

        public static bool IsNullOrEmpty(this string input)
        {
            if ((input != null) && (input.Trim().Length != 0))
            {
                return false;
            }
            return true;
        }

        public static bool IsTelphone(this string strInput)
        {
            if (strInput.IsNullOrEmpty())
            {
                return false;
            }
            Regex regex = new Regex(@"^(\d{2,4}-)?(\d{2,6}-)?\d{7,12}(-\d{1,6})?$");
            return regex.IsMatch(strInput);
        }

        public static bool IsZipCode(this string strInput)
        {
            if (strInput.IsNullOrEmpty())
            {
                return false;
            }
            Regex regex = new Regex(@"^\d{6}$");
            return regex.IsMatch(strInput);
        }

        public static string MD5(this string Input)
        {
            return Input.MD5(true);
        }

        public static string MD5(this string Input, bool Half)
        {
            string str = FormsAuthentication.HashPasswordForStoringInConfigFile(Input, "MD5").ToLower();
            if (Half)
            {
                str = str.Substring(8, 0x10);
            }
            return str;
        }

        public static Stack<Match> NotMatchHtmls(this string content)
        {
            Stack<Match> stack = new Stack<Match>();
            if (!string.IsNullOrEmpty(content))
            {
                MatchCollection matchs = Regex.Matches(content, "<(/\\s*)?((\\w+:)?\\w+)(\\w+(\\s*=\\s*(([\"'])(\\\\[\"'tbnr]|[^\\7])*?\\7|\\w+)|.{0})|\\s)*?(/\\s*)?>");
                for (int i = 0; i < matchs.Count; i++)
                {
                    if (((!matchs[i].Value.EndsWith("/>") && (matchs[i].Groups[2].Value != "br")) && ((matchs[i].Groups[2].Value != "input") && (matchs[i].Groups[2].Value != "hr"))) && (matchs[i].Groups[2].Value != "img"))
                    {
                        if (((stack.Count > 0) && (matchs[i].Groups[1].Value == "/")) && (stack.Peek().Groups[2].Value == matchs[i].Groups[2].Value))
                        {
                            stack.Pop();
                        }
                        else
                        {
                            stack.Push(matchs[i]);
                        }
                    }
                }
            }
            return stack;
        }

        public static string ReDateTime()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static string ReplaceForFilter(this string sInput)
        {
            if ((sInput == null) || (sInput == ""))
            {
                return null;
            }
            string str = sInput;
            return str.Replace("''", "'");
        }

        public static string ReplaceStrForJson(this string Str)
        {
            string str = "";
            if (!string.IsNullOrWhiteSpace(Str))
            {
                str = Regex.Replace(Str, "\"", "\\\"");
            }
            return str;
        }

        public static string ScriptFilter(this string source)
        {
            source = Regex.Replace(source, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "-->", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<!--.*", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "&(iexcl|#161);", "\x00a1", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "&(cent|#162);", "\x00a2", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "&(pound|#163);", "\x00a3", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "&(copy|#169);", "\x00a9", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            return source;
        }

        public static string SpeChar(this string str)
        {
            if (str == string.Empty)
            {
                return string.Empty;
            }
            str = str.Replace("'", "‘");
            str = str.Replace("(", "（");
            str = str.Replace(")", "）");
            str = str.Replace(";", "；");
            str = str.Replace(",", ",");
            str = str.Replace("?", "?");
            str = str.Replace("<", "＜");
            str = str.Replace(">", "＞");
            str = str.Replace("(", "(");
            str = str.Replace(")", ")");
            str = str.Replace("@", "＠");
            str = str.Replace("=", "＝");
            str = str.Replace("+", "＋");
            str = str.Replace("*", "＊");
            str = str.Replace("&", "＆");
            str = str.Replace("#", "＃");
            str = str.Replace("%", "％");
            str = str.Replace("$", "＄");
            str = str.Replace("{", "｛");
            str = str.Replace("}", "｝");
            str = str.Replace("[", "［");
            str = str.Replace("]", "］");
            str = str.Replace("_", "＿");
            return str;
        }

        public static string ToSBC(string input)
        {
            char[] chArray = input.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == ' ')
                {
                    chArray[i] = '　';
                }
                else if (chArray[i] < '\x007f')
                {
                    chArray[i] = (char) (chArray[i] + 0xfee0);
                }
            }
            return new string(chArray);
        }

        public static string URLDecode(this string Input)
        {
            if (string.IsNullOrEmpty(Input))
            {
                return string.Empty;
            }
            return HttpUtility.UrlDecode(Input);
        }

        public static string URLEncode(this string Input)
        {
            if (string.IsNullOrEmpty(Input))
            {
                return string.Empty;
            }
            return HttpUtility.UrlEncode(Input);
        }
    }
}

