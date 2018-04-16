namespace Rc.Common
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI.WebControls;

    public class PageValidate
    {
        private static Regex RegCHZN = new Regex("[一-龥]");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$");
        private static Regex RegEmail = new Regex(@"^[\w-]+@[\w-]+\.(com|net|org|edu|mil|tv|biz|info)$");
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegPhone = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$");

        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }

        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }

        public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
        {
            string sqlInput = string.Empty;
            if ((inputKey != null) && (inputKey != string.Empty))
            {
                sqlInput = req.QueryString[inputKey];
                if (sqlInput == null)
                {
                    sqlInput = req.Form[inputKey];
                }
                if (sqlInput != null)
                {
                    sqlInput = SqlText(sqlInput, maxLen);
                    if (!IsNumber(sqlInput))
                    {
                        sqlInput = string.Empty;
                    }
                }
            }
            if (sqlInput == null)
            {
                sqlInput = string.Empty;
            }
            return sqlInput;
        }

        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }

        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder builder = new StringBuilder();
            if ((inputString != null) && (inputString != string.Empty))
            {
                inputString = inputString.Trim();
                if (inputString.Length > maxLength)
                {
                    inputString = inputString.Substring(0, maxLength);
                }
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '<':
                        {
                            builder.Append("&lt;");
                            continue;
                        }
                        case '>':
                        {
                            builder.Append("&gt;");
                            continue;
                        }
                        case '"':
                        {
                            builder.Append("&quot;");
                            continue;
                        }
                    }
                    builder.Append(inputString[i]);
                }
                builder.Replace("'", " ");
            }
            return builder.ToString();
        }

        public static bool isContainSameChar(string strInput)
        {
            string charInput = string.Empty;
            if (!string.IsNullOrEmpty(strInput))
            {
                charInput = strInput.Substring(0, 1);
            }
            return isContainSameChar(strInput, charInput, strInput.Length);
        }

        public static bool isContainSameChar(string strInput, string charInput, int lenInput)
        {
            if (string.IsNullOrEmpty(charInput))
            {
                return false;
            }
            Regex regex = new Regex(string.Format("^([{0}])+$", charInput));
            return regex.Match(strInput).Success;
        }

        public static bool isContainSpecChar(string strInput)
        {
            string[] strArray = new string[] { "123456", "654321" };
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strInput == strArray[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsDateTime(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    DateTime.Parse(str);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDecimal(string inputData)
        {
            return RegDecimal.Match(inputData).Success;
        }

        public static bool IsDecimalSign(string inputData)
        {
            return RegDecimalSign.Match(inputData).Success;
        }

        public static bool IsEmail(string inputData)
        {
            return RegEmail.Match(inputData).Success;
        }

        public static bool IsHasCHZN(string inputData)
        {
            return RegCHZN.Match(inputData).Success;
        }

        public static bool IsNumber(string inputData)
        {
            return RegNumber.Match(inputData).Success;
        }

        public static bool IsNumberSign(string inputData)
        {
            return RegNumberSign.Match(inputData).Success;
        }

        public static bool IsPhone(string inputData)
        {
            return RegPhone.Match(inputData).Success;
        }

        public static string SafeLongFilter(string obj, int i)
        {
            return "";
        }

        public static void SetLabel(Label lbl, object inputObj)
        {
            SetLabel(lbl, inputObj.ToString());
        }

        public static void SetLabel(Label lbl, string txtInput)
        {
            lbl.Text = HtmlEncode(txtInput);
        }

        public static string SqlText(string sqlInput, int maxLength)
        {
            if ((sqlInput != null) && (sqlInput != string.Empty))
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)
                {
                    sqlInput = sqlInput.Substring(0, maxLength);
                }
            }
            return sqlInput;
        }

        public static string SqlTextClear(string sqlText)
        {
            if (sqlText == null)
            {
                return null;
            }
            if (sqlText == "")
            {
                return "";
            }
            sqlText = sqlText.Replace(",", "");
            sqlText = sqlText.Replace("<", "");
            sqlText = sqlText.Replace(">", "");
            sqlText = sqlText.Replace("--", "");
            sqlText = sqlText.Replace("'", "");
            sqlText = sqlText.Replace("\"", "");
            sqlText = sqlText.Replace("=", "");
            sqlText = sqlText.Replace("%", "");
            sqlText = sqlText.Replace(" ", "");
            return sqlText;
        }
    }
}

