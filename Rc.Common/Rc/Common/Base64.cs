namespace Rc.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Base64
    {
        public static string DecodeBase64(string code, string code_type = "utf-8")
        {
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                return Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                return code;
            }
        }

        public static string EncodeBase64(string code, string code_type = "utf-8")
        {
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return code;
            }
        }
    }
}

