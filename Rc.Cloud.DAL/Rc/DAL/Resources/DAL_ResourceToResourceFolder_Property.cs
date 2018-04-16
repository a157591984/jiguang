namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_ResourceToResourceFolder_Property
    {
        public bool Add(Model_ResourceToResourceFolder_Property model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ResourceToResourceFolder_Property(");
            builder.Append("ResourceToResourceFolder_Id,BooksCode,BooksUnitCode,GuidDoc,TestPaperName,CreateTime,paperHeaderDoc,paperHeaderHtml)");
            builder.Append(" values (");
            builder.Append("@ResourceToResourceFolder_Id,@BooksCode,@BooksUnitCode,@GuidDoc,@TestPaperName,@CreateTime,@paperHeaderDoc,@paperHeaderHtml)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@BooksCode", SqlDbType.VarChar, 50), new SqlParameter("@BooksUnitCode", SqlDbType.VarChar, 50), new SqlParameter("@GuidDoc", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestPaperName", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@paperHeaderDoc", SqlDbType.VarChar, 0x3e8), new SqlParameter("@paperHeaderHtml", SqlDbType.VarChar, 0x3e8) };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.BooksCode;
            cmdParms[2].Value = model.BooksUnitCode;
            cmdParms[3].Value = model.GuidDoc;
            cmdParms[4].Value = model.TestPaperName;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.paperHeaderDoc;
            cmdParms[7].Value = model.paperHeaderHtml;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_ResourceToResourceFolder_Property DataRowToModel(DataRow row)
        {
            Model_ResourceToResourceFolder_Property property = new Model_ResourceToResourceFolder_Property();
            if (row != null)
            {
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    property.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["BooksCode"] != null)
                {
                    property.BooksCode = row["BooksCode"].ToString();
                }
                if (row["BooksUnitCode"] != null)
                {
                    property.BooksUnitCode = row["BooksUnitCode"].ToString();
                }
                if (row["GuidDoc"] != null)
                {
                    property.GuidDoc = row["GuidDoc"].ToString();
                }
                if (row["TestPaperName"] != null)
                {
                    property.TestPaperName = row["TestPaperName"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    property.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["paperHeaderDoc"] != null)
                {
                    property.paperHeaderDoc = row["paperHeaderDoc"].ToString();
                }
                if (row["paperHeaderHtml"] != null)
                {
                    property.paperHeaderHtml = row["paperHeaderHtml"].ToString();
                }
            }
            return property;
        }

        public bool Delete(string ResourceToResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ResourceToResourceFolder_Property ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ResourceToResourceFolder_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ResourceToResourceFolder_Property ");
            builder.Append(" where ResourceToResourceFolder_Id in (" + ResourceToResourceFolder_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ResourceToResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ResourceToResourceFolder_Property");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceToResourceFolder_Id,BooksCode,BooksUnitCode,GuidDoc,TestPaperName,CreateTime,paperHeaderDoc,paperHeaderHtml ");
            builder.Append(" FROM ResourceToResourceFolder_Property ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ResourceToResourceFolder_Id,BooksCode,BooksUnitCode,GuidDoc,TestPaperName,CreateTime,paperHeaderDoc,paperHeaderHtml ");
            builder.Append(" FROM ResourceToResourceFolder_Property ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.ResourceToResourceFolder_Id desc");
            }
            builder.Append(")AS Row, T.*  from ResourceToResourceFolder_Property T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_ResourceToResourceFolder_Property GetModel(string ResourceToResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ResourceToResourceFolder_Id,BooksCode,BooksUnitCode,GuidDoc,TestPaperName,CreateTime,paperHeaderDoc,paperHeaderHtml from ResourceToResourceFolder_Property ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            new Model_ResourceToResourceFolder_Property();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public object GetpaperHeaderDoc(string ResourceToResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select paperHeaderDoc from ResourceToResourceFolder_Property where ResourceToResourceFolder_Id ='{0}';", ResourceToResourceFolder_Id);
            return DbHelperSQL.GetSingle(builder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM ResourceToResourceFolder_Property ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Model_ResourceToResourceFolder_Property model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ResourceToResourceFolder_Property set ");
            builder.Append("BooksCode=@BooksCode,");
            builder.Append("BooksUnitCode=@BooksUnitCode,");
            builder.Append("GuidDoc=@GuidDoc,");
            builder.Append("TestPaperName=@TestPaperName,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("paperHeaderDoc=@paperHeaderDoc,");
            builder.Append("paperHeaderHtml=@paperHeaderHtml");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BooksCode", SqlDbType.VarChar, 50), new SqlParameter("@BooksUnitCode", SqlDbType.VarChar, 50), new SqlParameter("@GuidDoc", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestPaperName", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@paperHeaderDoc", SqlDbType.VarChar, 0x3e8), new SqlParameter("@paperHeaderHtml", SqlDbType.VarChar, 0x3e8), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.BooksCode;
            cmdParms[1].Value = model.BooksUnitCode;
            cmdParms[2].Value = model.GuidDoc;
            cmdParms[3].Value = model.TestPaperName;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.paperHeaderDoc;
            cmdParms[6].Value = model.paperHeaderHtml;
            cmdParms[7].Value = model.ResourceToResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

