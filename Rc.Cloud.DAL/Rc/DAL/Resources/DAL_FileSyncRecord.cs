namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_FileSyncRecord
    {
        public bool Add(Model_FileSyncRecord model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into FileSyncRecord(");
            builder.Append("FileSyncRecord_Id,SyncLong,Remark,SyncTime,SyncUserId,SyncUserName,SyncType)");
            builder.Append(" values (");
            builder.Append("@FileSyncRecord_Id,@SyncLong,@Remark,@SyncTime,@SyncUserId,@SyncUserName,@SyncType)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncRecord_Id", SqlDbType.Char, 0x24), new SqlParameter("@SyncLong", SqlDbType.VarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@SyncTime", SqlDbType.DateTime), new SqlParameter("@SyncUserId", SqlDbType.Char, 0x24), new SqlParameter("@SyncUserName", SqlDbType.NVarChar, 200), new SqlParameter("@SyncType", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = model.FileSyncRecord_Id;
            cmdParms[1].Value = model.SyncLong;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.SyncTime;
            cmdParms[4].Value = model.SyncUserId;
            cmdParms[5].Value = model.SyncUserName;
            cmdParms[6].Value = model.SyncType;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_FileSyncRecord DataRowToModel(DataRow row)
        {
            Model_FileSyncRecord record = new Model_FileSyncRecord();
            if (row != null)
            {
                if (row["FileSyncRecord_Id"] != null)
                {
                    record.FileSyncRecord_Id = row["FileSyncRecord_Id"].ToString();
                }
                if (row["SyncLong"] != null)
                {
                    record.SyncLong = row["SyncLong"].ToString();
                }
                if (row["Remark"] != null)
                {
                    record.Remark = row["Remark"].ToString();
                }
                if ((row["SyncTime"] != null) && (row["SyncTime"].ToString() != ""))
                {
                    record.SyncTime = new DateTime?(DateTime.Parse(row["SyncTime"].ToString()));
                }
                if (row["SyncUserId"] != null)
                {
                    record.SyncUserId = row["SyncUserId"].ToString();
                }
                if (row["SyncUserName"] != null)
                {
                    record.SyncUserName = row["SyncUserName"].ToString();
                }
                if (row["SyncType"] != null)
                {
                    record.SyncType = row["SyncType"].ToString();
                }
            }
            return record;
        }

        public bool Delete(string FileSyncRecord_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from FileSyncRecord ");
            builder.Append(" where FileSyncRecord_Id=@FileSyncRecord_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncRecord_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncRecord_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string FileSyncRecord_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from FileSyncRecord ");
            builder.Append(" where FileSyncRecord_Id in (" + FileSyncRecord_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string FileSyncRecord_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from FileSyncRecord");
            builder.Append(" where FileSyncRecord_Id=@FileSyncRecord_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncRecord_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncRecord_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FileSyncRecord_Id,SyncLong,Remark,SyncTime,SyncUserId,SyncUserName,SyncType ");
            builder.Append(" FROM FileSyncRecord ");
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
            builder.Append(" FileSyncRecord_Id,SyncLong,Remark,SyncTime,SyncUserId,SyncUserName,SyncType ");
            builder.Append(" FROM FileSyncRecord ");
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
                builder.Append("order by T.FileSyncRecord_Id desc");
            }
            builder.Append(")AS Row, T.*  from FileSyncRecord T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_FileSyncRecord GetModel(string FileSyncRecord_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FileSyncRecord_Id,SyncLong,Remark,SyncTime,SyncUserId,SyncUserName,SyncType from FileSyncRecord ");
            builder.Append(" where FileSyncRecord_Id=@FileSyncRecord_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncRecord_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncRecord_Id;
            new Model_FileSyncRecord();
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
            builder.Append("select count(1) FROM FileSyncRecord ");
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

        public GH_PagerInfo<List<Model_FileSyncRecord>> SearhExeccuteDataAnalysis(string Where, string Sort, int pageIndex, int pageSize)
        {
            new StringBuilder();
            string strWhere = " 1=1 " + Where;
            int recordCount = this.GetRecordCount(strWhere);
            int startIndex = ((pageIndex - 1) * pageSize) + 1;
            int endIndex = pageIndex * pageSize;
            DataTable table = this.GetListByPage(strWhere, Sort, startIndex, endIndex).Tables[0];
            List<Model_FileSyncRecord> list = new List<Model_FileSyncRecord>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(this.DataRowToModel(row));
            }
            return new GH_PagerInfo<List<Model_FileSyncRecord>> { PageSize = pageSize, CurrentPage = pageIndex, RecordCount = recordCount, PageData = list };
        }

        public bool Update(Model_FileSyncRecord model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update FileSyncRecord set ");
            builder.Append("SyncLong=@SyncLong,");
            builder.Append("Remark=@Remark,");
            builder.Append("SyncTime=@SyncTime,");
            builder.Append("SyncUserId=@SyncUserId,");
            builder.Append("SyncUserName=@SyncUserName,");
            builder.Append("SyncType=@SyncType");
            builder.Append(" where FileSyncRecord_Id=@FileSyncRecord_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncLong", SqlDbType.VarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@SyncTime", SqlDbType.DateTime), new SqlParameter("@SyncUserId", SqlDbType.Char, 0x24), new SqlParameter("@SyncUserName", SqlDbType.NVarChar, 200), new SqlParameter("@SyncType", SqlDbType.VarChar, 50), new SqlParameter("@FileSyncRecord_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SyncLong;
            cmdParms[1].Value = model.Remark;
            cmdParms[2].Value = model.SyncTime;
            cmdParms[3].Value = model.SyncUserId;
            cmdParms[4].Value = model.SyncUserName;
            cmdParms[5].Value = model.SyncType;
            cmdParms[6].Value = model.FileSyncRecord_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

