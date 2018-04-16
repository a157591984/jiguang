namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_FileSyncRecordFail
    {
        public bool Add(Model_FileSyncRecordFail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into FileSyncRecordFail(");
            builder.Append("FileSyncRecordFail_Id,Book_Id,ResourceToResourceFolder_Id,FileUrl,SyncFailTime,Resource_Type,File_Type)");
            builder.Append(" values (");
            builder.Append("@FileSyncRecordFail_Id,@Book_Id,@ResourceToResourceFolder_Id,@FileUrl,@SyncFailTime,@Resource_Type,@File_Type)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncRecordFail_Id", SqlDbType.Char, 0x24), new SqlParameter("@Book_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@FileUrl", SqlDbType.NVarChar, 500), new SqlParameter("@SyncFailTime", SqlDbType.DateTime), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@File_Type", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.FileSyncRecordFail_Id;
            cmdParms[1].Value = model.Book_Id;
            cmdParms[2].Value = model.ResourceToResourceFolder_Id;
            cmdParms[3].Value = model.FileUrl;
            cmdParms[4].Value = model.SyncFailTime;
            cmdParms[5].Value = model.Resource_Type;
            cmdParms[6].Value = model.File_Type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_FileSyncRecordFail DataRowToModel(DataRow row)
        {
            Model_FileSyncRecordFail fail = new Model_FileSyncRecordFail();
            if (row != null)
            {
                if (row["FileSyncRecordFail_Id"] != null)
                {
                    fail.FileSyncRecordFail_Id = row["FileSyncRecordFail_Id"].ToString();
                }
                if (row["Book_Id"] != null)
                {
                    fail.Book_Id = row["Book_Id"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    fail.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["FileUrl"] != null)
                {
                    fail.FileUrl = row["FileUrl"].ToString();
                }
                if ((row["SyncFailTime"] != null) && (row["SyncFailTime"].ToString() != ""))
                {
                    fail.SyncFailTime = new DateTime?(DateTime.Parse(row["SyncFailTime"].ToString()));
                }
                if (row["Resource_Type"] != null)
                {
                    fail.Resource_Type = row["Resource_Type"].ToString();
                }
                if (row["File_Type"] != null)
                {
                    fail.File_Type = row["File_Type"].ToString();
                }
            }
            return fail;
        }

        public bool Delete(string FileSyncRecordFail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from FileSyncRecordFail ");
            builder.Append(" where FileSyncRecordFail_Id=@FileSyncRecordFail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncRecordFail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncRecordFail_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteClass(string StrWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from FileSyncRecordFail ");
            builder.Append(" where " + StrWhere);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteList(string FileSyncRecordFail_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from FileSyncRecordFail ");
            builder.Append(" where FileSyncRecordFail_Id in (" + FileSyncRecordFail_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string FileSyncRecordFail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from FileSyncRecordFail");
            builder.Append(" where FileSyncRecordFail_Id=@FileSyncRecordFail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncRecordFail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncRecordFail_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FileSyncRecordFail_Id,Book_Id,ResourceToResourceFolder_Id,FileUrl,SyncFailTime,Resource_Type,File_Type ");
            builder.Append(" FROM FileSyncRecordFail ");
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
            builder.Append(" FileSyncRecordFail_Id,Book_Id,ResourceToResourceFolder_Id,FileUrl,SyncFailTime,Resource_Type,File_Type ");
            builder.Append(" FROM FileSyncRecordFail ");
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
                builder.Append("order by T.FileSyncRecordFail_Id desc");
            }
            builder.Append(")AS Row, T.*  from FileSyncRecordFail T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageForFileSyncRecordFail(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by " + orderby);
            }
            else
            {
                builder.Append("order by CreateTime desc");
            }
            builder.Append(")AS Row, T.*  from (select ff.SyncFailTime,rf.Resource_Name,ff.File_Type,ff.FileUrl from dbo.FileSyncRecordFail ff \r\nleft join ResourceToResourceFolder rf on  rf.ResourceToResourceFolder_Id=ff.ResourceToResourceFolder_Id) T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_FileSyncRecordFail GetModel(string FileSyncRecordFail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FileSyncRecordFail_Id,Book_Id,ResourceToResourceFolder_Id,FileUrl,SyncFailTime,Resource_Type,File_Type from FileSyncRecordFail ");
            builder.Append(" where FileSyncRecordFail_Id=@FileSyncRecordFail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncRecordFail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncRecordFail_Id;
            new Model_FileSyncRecordFail();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM FileSyncRecordFail ");
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

        public bool Update(Model_FileSyncRecordFail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update FileSyncRecordFail set ");
            builder.Append("Book_Id=@Book_Id,");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("FileUrl=@FileUrl,");
            builder.Append("SyncFailTime=@SyncFailTime,");
            builder.Append("Resource_Type=@Resource_Type,");
            builder.Append("File_Type=@File_Type");
            builder.Append(" where FileSyncRecordFail_Id=@FileSyncRecordFail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Book_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@FileUrl", SqlDbType.NVarChar, 500), new SqlParameter("@SyncFailTime", SqlDbType.DateTime), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@File_Type", SqlDbType.NVarChar, 200), new SqlParameter("@FileSyncRecordFail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Book_Id;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.FileUrl;
            cmdParms[3].Value = model.SyncFailTime;
            cmdParms[4].Value = model.Resource_Type;
            cmdParms[5].Value = model.File_Type;
            cmdParms[6].Value = model.FileSyncRecordFail_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

