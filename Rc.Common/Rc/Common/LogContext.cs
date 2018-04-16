namespace Rc.Common
{
    using System;
    using System.IO;
    using System.Text;

    public class LogContext
    {
        public static void AddLogInfo(string strPath, string txt, bool isAppend)
        {
            StreamWriter writer;
            string path = strPath.Substring(0, strPath.LastIndexOf('\\'));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            if (isAppend)
            {
                writer = File.AppendText(strPath);
            }
            else
            {
                writer = File.CreateText(strPath);
            }
            writer.WriteLine(txt);
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }

        public static string ReadDataLog(string strPath)
        {
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            return File.ReadAllText(strPath, Encoding.UTF8);
        }
    }
}

