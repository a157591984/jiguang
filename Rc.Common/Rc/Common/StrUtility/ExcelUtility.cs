namespace Rc.Common.StrUtility
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;
    using System.Xml;

    public sealed class ExcelUtility
    {
        public static void CreateExcel2(DataSet ds, string FileName)
        {
            HttpContext.Current.Response.Clear();
            HttpResponse response = HttpContext.Current.Response;
            response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
            response.ContentEncoding = Encoding.GetEncoding("GB2312");
            response.ContentType = "application/vnd.ms-excel";
            string s = "";
            string str2 = "";
            DataTable table = ds.Tables[0];
            DataRow[] rowArray = table.Select();
            int num = 0;
            int count = table.Columns.Count;
            num = 0;
            while (num < count)
            {
                if (num == (count - 1))
                {
                    s = s + table.Columns[num].Caption.ToString() + "\n";
                }
                else
                {
                    s = s + table.Columns[num].Caption.ToString() + "\t";
                }
                num++;
            }
            response.Write(s);
            foreach (DataRow row in rowArray)
            {
                for (num = 0; num < count; num++)
                {
                    if (num == (count - 1))
                    {
                        str2 = str2 + row[num].ToString().Replace("\r\n", "<br />") + "\n";
                    }
                    else
                    {
                        string str3 = row[num].ToString().Replace("\r", "").Replace("\n", "").Replace("\r\n", "").Replace("\n\r", "");
                        str2 = str2 + str3 + "\t";
                    }
                }
                response.Write(str2);
                str2 = "";
            }
            response.End();
        }

        public static DataSet GetExcelData(string file, string tableName, string excelVersion = "8.0")
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file + ";Extended Properties='Excel " + excelVersion + ";HDR=Yes;IMEX=1;'";
            OleDbCommand selectCommand = new OleDbCommand("SELECT * FROM [" + tableName + "$]", new OleDbConnection(connectionString));
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }

        public static DataTable GetExeclToDT(string filePath)
        {
            DataTable dataTable = new DataTable();
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HER=YES,IMEX=1'"))
                    {
                        connection.Open();
                        string selectCommandText = "select * from [sheet1$]";
                        new OleDbDataAdapter(selectCommandText, connection).Fill(dataTable);
                    }
                    XmlDocument document = new XmlDocument();
                    XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", null);
                    document.AppendChild(newChild);
                    XmlElement element = document.CreateElement("root");
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        XmlElement element2 = document.CreateElement("line");
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            XmlElement element3 = document.CreateElement("xe");
                            element3.InnerText = dataTable.Rows[i][j].ToString();
                            element2.AppendChild(element3);
                        }
                        element.AppendChild(element2);
                    }
                    document.AppendChild(element);
                    string text1 = filePath.Substring(0, filePath.LastIndexOf('.')) + ".xml";
                    return dataTable;
                }
                catch
                {
                }
            }
            return null;
        }
    }
}

