namespace Rc.Common.StrUtility
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class EncryptUtility
    {
        private static string Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";
        private static SymmetricAlgorithm mobjCryptoService = new RijndaelManaged();

        public static string DecryptString(this string Source)
        {
            byte[] buffer = Convert.FromBase64String(Source);
            MemoryStream stream = new MemoryStream(buffer, 0, buffer.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform transform = mobjCryptoService.CreateDecryptor();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(stream2);
            return reader.ReadToEnd();
        }

        public static string EncryptString(this string Source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Source);
            MemoryStream stream = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform transform = mobjCryptoService.CreateEncryptor();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            stream.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
        public static string MyEncryptString( string Source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Source);
            MemoryStream stream = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform transform = mobjCryptoService.CreateEncryptor();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            stream.Close();
            return Convert.ToBase64String(stream.ToArray());
        }

        private static byte[] GetLegalIV()
        {
            string s = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
            mobjCryptoService.GenerateIV();
            int length = mobjCryptoService.IV.Length;
            if (s.Length > length)
            {
                s = s.Substring(0, length);
            }
            else if (s.Length < length)
            {
                s = s.PadRight(length, ' ');
            }
            return Encoding.ASCII.GetBytes(s);
        }

        private static byte[] GetLegalKey()
        {
            string key = Key;
            mobjCryptoService.GenerateKey();
            int length = mobjCryptoService.Key.Length;
            if (key.Length > length)
            {
                key = key.Substring(0, length);
            }
            else if (key.Length < length)
            {
                key = key.PadRight(length, ' ');
            }
            return Encoding.ASCII.GetBytes(key);
        }
    }
}

