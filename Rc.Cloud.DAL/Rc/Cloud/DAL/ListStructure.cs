namespace Rc.Cloud.DAL
{
    using Rc.Common.DBUtility;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ListStructure
    {
        public bool ExecAddDescriptionTable(string database, string tablename, string tableinfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" EXEC  sys.sp_addextendedproperty \r\n                                            @name=N'MS_Description', \r\n                                            @value=N'{0}' ,\r\n                                            @level0type=N'SCHEMA',\r\n                                            @level0name=N'dbo', \r\n                                            @level1type=N'TABLE',\r\n                                            @level1name=N'{1}'\r\n                            ", tableinfo, tablename);
            try
            {
                DbHelperSQL.ExecuteSql(builder.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExecAddDescriptionTableColumn(string database, string tablename, string tableColumn, string tablecolumninfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("EXEC sys.sp_addextendedproperty @name=N'MS_Description', \r\n                                        @value=N'{0}' , \r\n                                        @level0type=N'SCHEMA',\r\n                                        @level0name=N'dbo',\r\n                                        @level1type=N'TABLE',\r\n                                        @level1name=N'{1}',\r\n                                        @level2type=N'COLUMN',\r\n                                        @level2name=N'{2}'\r\n                            ", tablecolumninfo, tablename, tableColumn);
            try
            {
                DbHelperSQL.ExecuteSql(builder.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExecUpdataDescriptionTable(string database, string tablename, string tableinfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("EXEC sys.sp_updateextendedproperty @name = N'MS_Description', \r\n                                @value = N'{0}', \r\n                                @level0type = N'SCHEMA', \r\n                                @level0name = N'dbo', \r\n                                @level1type=N'TABLE', \r\n                                @level1name = N'{1}' \r\n                            ", tableinfo, tablename);
            try
            {
                DbHelperSQL.ExecuteSql(builder.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExecUpdataDescriptionTableColumn(string database, string tablename, string tableColumn, string tablecolumninfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" EXEC sys.sp_updateextendedproperty @name = N'MS_Description',\r\n                                        @value = '{0}', \r\n                                        @level0type = N'SCHEMA',\r\n                                        @level0name = N'dbo', \r\n                                        @level1type = N'TABLE', \r\n                                        @level1name = '{1}', \r\n                                        @level2type=N'COLUMN',\r\n                                        @level2name=N'{2}'\r\n                            ", tablecolumninfo, tablename, tableColumn);
            try
            {
                DbHelperSQL.ExecuteSql(builder.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataSet GetListStructure(string database, string Content, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 100 PERCENT  \r\n                          row_number() over(order BY d.name, a.colorder ) AS r_n,\r\n                          d.name AS 表名, \r\n                          isnull(f.value, '') AS 表说明, \r\n                          a.colorder AS 字段序号, a.name AS 字段名, CASE WHEN COLUMNPROPERTY(a.id, \r\n                          a.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识, \r\n                          CASE WHEN EXISTS\r\n                              (SELECT 1\r\n                             FROM  dbo.sysindexes si INNER JOIN\r\n                                   dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN\r\n                                   dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN\r\n                                   dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'\r\n                             WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键, \r\n                          b.name AS 类型, a.length AS 长度, COLUMNPROPERTY(a.id, a.name, 'PRECISION') \r\n                          AS 精度, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数, \r\n                          CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, ISNULL(e.text, '') \r\n                          AS 默认值, ISNULL(g.[value], '') AS 字段说明, d.crdate AS 创建时间, \r\n                          CASE WHEN a.colorder = 1 THEN d.refdate ELSE NULL END AS 更改时间\r\n                    FROM  dbo.syscolumns a LEFT OUTER JOIN\r\n                          dbo.systypes b ON a.xtype = b.xusertype INNER JOIN\r\n                          dbo.sysobjects d ON a.id = d.id AND d.xtype = 'U' AND \r\n                          d.status >= 0 LEFT OUTER JOIN\r\n                          dbo.syscomments e ON a.cdefault = e.id LEFT OUTER JOIN\r\n                          sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id AND \r\n                          g.name = 'MS_Description' LEFT OUTER JOIN\r\n                          sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0 AND \r\n                          f.name = 'MS_Description'\r\n                ");
            builder.Append("where 1=1 ");
            if (Content != " ")
            {
                builder.Append(Content);
            }
            builder.Append(" ORDER BY d.name, 字段序号 ");
            return DbHelperSQL.GetRecordByPage(builder.ToString(), PageIndex, PageSize, out rCount, out pCount);
        }
    }
}

