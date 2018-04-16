namespace Rc.Common.StrUtility
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Linq;
    public static class StringExtenstion
    {
        public static string GetLowOrderASCIICharacters(this string input)
        {
            int num;
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            int startIndex = 0;
            int length = input.Length;
            if (length <= 4)
            {
                return input;
            }
            StringBuilder builder = new StringBuilder();
            while ((num = input.IndexOf("&#x", startIndex)) >= 0)
            {
                bool flag = false;
                string oldValue = string.Empty;
                string newValue = string.Empty;
                int count = ((length - num) < 6) ? (length - num) : 6;
                int num5 = input.IndexOf(";", num, count);
                if (num5 >= 0)
                {
                    short num6;
                    oldValue = input.Substring(num, (num5 - num) + 1);
                    if (short.TryParse(oldValue.Substring(3, (num5 - num) - 3), NumberStyles.AllowHexSpecifier, (IFormatProvider) null, out num6) && ((((num6 >= 0) && (num6 <= 8)) || ((num6 >= 11) && (num6 <= 12))) || ((num6 >= 14) && (num6 <= 0x20))))
                    {
                        flag = true;
                        newValue = Convert.ToChar(num6).ToString();
                    }
                    num = num5 + 1;
                }
                else
                {
                    num += count;
                }
                string str3 = input.Substring(startIndex, num - startIndex);
                if (flag)
                {
                    builder.Append(str3.Replace(oldValue, newValue));
                }
                else
                {
                    builder.Append(str3);
                }
                startIndex = num;
            }
            builder.Append(input.Substring(startIndex));
            return builder.ToString();
        }

        public static string ReplaceLowOrderASCIICharacters(this string tmp)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in tmp)
            {
                int num = ch;
                if ((((num >= 0) && (num <= 8)) || ((num >= 11) && (num <= 12))) || ((num >= 14) && (num <= 0x20)))
                {
                    builder.AppendFormat("&#x{0:X};", num);
                }
                else
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        public static void SetWSDecoding<T>(T t)
        {
            PropertyInfo[] properties = t.GetType().GetProperties();
            if (properties != null)
            {
                foreach (PropertyInfo info in from p in properties
                    where p.PropertyType.ToString() == typeof(string).ToString()
                    select p)
                {
                    if (info.PropertyType.ToString() == typeof(string).ToString())
                    {
                        object obj2 = info.GetValue(t, null);
                        if (obj2 != null)
                        {
                            info.SetValue(t, obj2.ToString().GetLowOrderASCIICharacters(), null);
                        }
                    }
                }
            }
        }

        public static void SetWSEncoding<T>(T t)
        {
            PropertyInfo[] properties = t.GetType().GetProperties();
            if (properties != null)
            {
                foreach (PropertyInfo info in from p in properties
                    where p.PropertyType.ToString() == typeof(string).ToString()
                    select p)
                {
                    if (info.PropertyType.ToString() == typeof(string).ToString())
                    {
                        object obj2 = info.GetValue(t, null);
                        if (obj2 != null)
                        {
                            info.SetValue(t, obj2.ToString().ReplaceLowOrderASCIICharacters(), null);
                        }
                    }
                }
            }
        }
    }
}

