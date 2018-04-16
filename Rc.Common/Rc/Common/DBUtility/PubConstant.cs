namespace Rc.Common.DBUtility
{
    using System;
    using System.Configuration;

    public class PubConstant
    {
        public static string GetConnectionString(string connName)
        {
            string str = ConfigurationManager.ConnectionStrings[connName].ToString();
            string text1 = ConfigurationManager.AppSettings["ConStringEncrypt"];
            return str;
        }

        public static string ConnectionString
        {
            get
            {
                string text = ConfigurationManager.ConnectionStrings["MedicalService"].ToString();
                string str2 = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (str2 == "true")
                {
                    text = DESEncrypt.DecryptConfig(text);
                }
                return text;
            }
        }

        public static string ConnectionString_Operate
        {
            get
            {
                string text = ConfigurationManager.ConnectionStrings["OperateService"].ToString();
                string str2 = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (str2 == "true")
                {
                    text = DESEncrypt.DecryptConfig(text);
                }
                return text;
            }
        }
    }
}

