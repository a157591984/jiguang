namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SystemLogFileSync
    {
        public bool Add(Model_SystemLogFileSync model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SystemLogFileSync(");
            builder.Append("SystemLogFileSyncID,SyncUrl,FileSize,ErrorMark,MsgType,CreateTime)");
            builder.Append(" values (");
            builder.Append("@SystemLogFileSyncID,@SyncUrl,@FileSize,@ErrorMark,@MsgType,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SystemLogFileSyncID", SqlDbType.Char, 0x24), new SqlParameter("@SyncUrl", SqlDbType.VarChar, 300), new SqlParameter("@FileSize", SqlDbType.Decimal, 9), new SqlParameter("@ErrorMark", SqlDbType.VarChar, 500), new SqlParameter("@MsgType", SqlDbType.VarChar, 10), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.SystemLogFileSyncID;
            cmdParms[1].Value = model.SyncUrl;
            cmdParms[2].Value = model.FileSize;
            cmdParms[3].Value = model.ErrorMark;
            cmdParms[4].Value = model.MsgType;
            cmdParms[5].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SystemLogFileSync DataRowToModel(DataRow row)
        {
            Model_SystemLogFileSync sync = new Model_SystemLogFileSync();
            if (row != null)
            {
                if (row["SystemLogFileSyncID"] != null)
                {
                    sync.SystemLogFileSyncID = row["SystemLogFileSyncID"].ToString();
                }
                if (row["SyncUrl"] != null)
                {
                    sync.SyncUrl = row["SyncUrl"].ToString();
                }
                if ((row["FileSize"] != null) && (row["FileSize"].ToString() != ""))
                {
                    sync.FileSize = new decimal?(decimal.Parse(row["FileSize"].ToString()));
                }
                if (row["ErrorMark"] != null)
                {
                    sync.ErrorMark = row["ErrorMark"].ToString();
                }
                if (row["MsgType"] != null)
                {
                    sync.MsgType = row["MsgType"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    sync.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return sync;
        }

        public bool Delete(string SystemLogFileSyncID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SystemLogFileSync ");
            builder.Append(" where SystemLogFileSyncID=@SystemLogFileSyncID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SystemLogFileSyncID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SystemLogFileSyncID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SystemLogFileSyncIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SystemLogFileSync ");
            builder.Append(" where SystemLogFileSyncID in (" + SystemLogFileSyncIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string SystemLogFileSyncID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SystemLogFileSync");
            builder.Append(" where SystemLogFileSyncID=@SystemLogFileSyncID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SystemLogFileSyncID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SystemLogFileSyncID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SystemLogFileSyncID,SyncUrl,FileSize,ErrorMark,MsgType,CreateTime ");
            builder.Append(" FROM SystemLogFileSync ");
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
            builder.Append(" SystemLogFileSyncID,SyncUrl,FileSize,ErrorMark,MsgType,CreateTime ");
            builder.Append(" FROM SystemLogFileSync ");
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
                builder.Append("order by T.SystemLogFileSyncID desc");
            }
            builder.Append(")AS Row, T.*  from SystemLogFileSync T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SystemLogFileSync GetModel(string SystemLogFileSyncID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SystemLogFileSyncID,SyncUrl,FileSize,ErrorMark,MsgType,CreateTime from SystemLogFileSync ");
            builder.Append(" where SystemLogFileSyncID=@SystemLogFileSyncID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SystemLogFileSyncID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SystemLogFileSyncID;
            new Model_SystemLogFileSync();
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
            builder.Append("select count(1) FROM SystemLogFileSync ");
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

        public bool Update(Model_SystemLogFileSync model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SystemLogFileSync set ");
            builder.Append("SyncUrl=@SyncUrl,");
            builder.Append("FileSize=@FileSize,");
            builder.Append("ErrorMark=@ErrorMark,");
            builder.Append("MsgType=@MsgType,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where SystemLogFileSyncID=@SystemLogFileSyncID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncUrl", SqlDbType.VarChar, 300), new SqlParameter("@FileSize", SqlDbType.Decimal, 9), new SqlParameter("@ErrorMark", SqlDbType.VarChar, 500), new SqlParameter("@MsgType", SqlDbType.VarChar, 10), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@SystemLogFileSyncID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SyncUrl;
            cmdParms[1].Value = model.FileSize;
            cmdParms[2].Value = model.ErrorMark;
            cmdParms[3].Value = model.MsgType;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.SystemLogFileSyncID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

