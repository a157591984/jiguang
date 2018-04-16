namespace Rc.Common.AppUtility
{
    using System;
    using System.Data;
    using System.Xml;

    public class XMLClass
    {
        public DataTable ReadExcel_SQL(string path, string tableKey)
        {
            try
            {
                DataTable table = null;
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNode node = document.SelectSingleNode("//Table[@key='" + tableKey + "']");
                if ((node != null) && (node.ChildNodes.Count > 0))
                {
                    table = new DataTable();
                    for (int i = 0; i < node.ChildNodes[0].Attributes.Count; i++)
                    {
                        table.Columns.Add(new DataColumn(node.ChildNodes[0].Attributes[i].Name));
                    }
                    for (int j = 0; j < node.ChildNodes.Count; j++)
                    {
                        DataRow row = table.NewRow();
                        for (int k = 0; k < node.ChildNodes[j].Attributes.Count; k++)
                        {
                            row[node.ChildNodes[0].Attributes[k].Name] = node.ChildNodes[j].Attributes[k].Value;
                        }
                        table.Rows.Add(row);
                    }
                }
                return table;
            }
            catch
            {
                return null;
            }
        }
    }
}

