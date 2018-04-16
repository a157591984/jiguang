namespace YunHuaTong.Lib
{
    using Rc.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Encrypt
    {
        public static string MakeKey(Dictionary<string, string> para, string key)
        {
            para["key"] = MakeKeyStr(para, key);
            return para["key"];
        }

        private static string MakeKeyStr(Dictionary<string, string> dic, string key)
        {
            string str = key;
            IOrderedEnumerable<KeyValuePair<string, string>> enumerable = from objDic in dic
                orderby objDic.Key
                select objDic;
            string str2 = "";
            int num = 0;
            foreach (KeyValuePair<string, string> pair in enumerable)
            {
                num++;
                if (num > 1)
                {
                    str2 = str2 + "&";
                }
                str2 = str2 + pair.Key + "=" + Base64.EncodeBase64(pair.Value, "utf-8");
            }
            return Md5(str2 + str);
        }

        public static string Md5(string Source)
        {
            MD5 md = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md.ComputeHash(Encoding.UTF8.GetBytes(Source))).Replace("-", "").ToLower();
        }

        public static string MD5ForPHP(string stringToHash)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(stringToHash.ToLower());
            byte[] buffer2 = provider.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            foreach (byte num in buffer2)
            {
                builder.Append(num.ToString("x2").ToLower());
            }
            return builder.ToString();
        }

        public static string UrlEncode(string strCode)
        {
            StringBuilder builder = new StringBuilder();
            byte[] bytes = Encoding.UTF8.GetBytes(strCode);
            Regex regex = new Regex("^[A-Za-z0-9-_.]+$");
            for (int i = 0; i < bytes.Length; i++)
            {
                string input = Convert.ToChar(bytes[i]).ToString();
                if (regex.IsMatch(input))
                {
                    builder.Append(input);
                }
                else
                {
                    builder.Append("%" + Convert.ToString(bytes[i], 0x10).ToUpper());
                }
            }
            return builder.ToString();
        }
    }
}

