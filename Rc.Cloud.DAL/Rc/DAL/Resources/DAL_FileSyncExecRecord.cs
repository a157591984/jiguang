namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_FileSyncExecRecord
    {
        public bool Add(Model_FileSyncExecRecord model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into FileSyncExecRecord(");
            builder.Append("FileSyncExecRecord_id,FileSyncExecRecord_Type,FileSyncExecRecord_TimeStart,FileSyncExecRecord_TimeEnd,FileSyncExecRecord_Status,FileSyncExecRecord_Condition,createUser,FileSyncExecRecord_Remark)");
            builder.Append(" values (");
            builder.Append("@FileSyncExecRecord_id,@FileSyncExecRecord_Type,@FileSyncExecRecord_TimeStart,@FileSyncExecRecord_TimeEnd,@FileSyncExecRecord_Status,@FileSyncExecRecord_Condition,@createUser,@FileSyncExecRecord_Remark)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecord_id", SqlDbType.Char, 0x24), new SqlParameter("@FileSyncExecRecord_Type", SqlDbType.VarChar, 30), new SqlParameter("@FileSyncExecRecord_TimeStart", SqlDbType.DateTime), new SqlParameter("@FileSyncExecRecord_TimeEnd", SqlDbType.DateTime), new SqlParameter("@FileSyncExecRecord_Status", SqlDbType.VarChar, 10), new SqlParameter("@FileSyncExecRecord_Condition", SqlDbType.VarChar, 100), new SqlParameter("@createUser", SqlDbType.Char, 0x24), new SqlParameter("@FileSyncExecRecord_Remark", SqlDbType.VarChar, 0x3e8) };
            cmdParms[0].Value = model.FileSyncExecRecord_id;
            cmdParms[1].Value = model.FileSyncExecRecord_Type;
            cmdParms[2].Value = model.FileSyncExecRecord_TimeStart;
            cmdParms[3].Value = model.FileSyncExecRecord_TimeEnd;
            cmdParms[4].Value = model.FileSyncExecRecord_Status;
            cmdParms[5].Value = model.FileSyncExecRecord_Condition;
            cmdParms[6].Value = model.createUser;
            cmdParms[7].Value = model.FileSyncExecRecord_Remark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Add_Operate(Model_FileSyncExecRecord model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into FileSyncExecRecord(");
            builder.Append("FileSyncExecRecord_id,FileSyncExecRecord_Type,FileSyncExecRecord_TimeStart,FileSyncExecRecord_TimeEnd,FileSyncExecRecord_Status,FileSyncExecRecord_Condition,createUser,FileSyncExecRecord_Remark)");
            builder.Append(" values (");
            builder.Append("@FileSyncExecRecord_id,@FileSyncExecRecord_Type,@FileSyncExecRecord_TimeStart,@FileSyncExecRecord_TimeEnd,@FileSyncExecRecord_Status,@FileSyncExecRecord_Condition,@createUser,@FileSyncExecRecord_Remark)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecord_id", SqlDbType.Char, 0x24), new SqlParameter("@FileSyncExecRecord_Type", SqlDbType.VarChar, 30), new SqlParameter("@FileSyncExecRecord_TimeStart", SqlDbType.DateTime), new SqlParameter("@FileSyncExecRecord_TimeEnd", SqlDbType.DateTime), new SqlParameter("@FileSyncExecRecord_Status", SqlDbType.VarChar, 10), new SqlParameter("@FileSyncExecRecord_Condition", SqlDbType.VarChar, 100), new SqlParameter("@createUser", SqlDbType.Char, 0x24), new SqlParameter("@FileSyncExecRecord_Remark", SqlDbType.VarChar, 0x3e8) };
            cmdParms[0].Value = model.FileSyncExecRecord_id;
            cmdParms[1].Value = model.FileSyncExecRecord_Type;
            cmdParms[2].Value = model.FileSyncExecRecord_TimeStart;
            cmdParms[3].Value = model.FileSyncExecRecord_TimeEnd;
            cmdParms[4].Value = model.FileSyncExecRecord_Status;
            cmdParms[5].Value = model.FileSyncExecRecord_Condition;
            cmdParms[6].Value = model.createUser;
            cmdParms[7].Value = model.FileSyncExecRecord_Remark;
            return (DbHelperSQL_Operate.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_FileSyncExecRecord DataRowToModel(DataRow row)
        {
            Model_FileSyncExecRecord record = new Model_FileSyncExecRecord();
            if (row != null)
            {
                if (row["FileSyncExecRecord_id"] != null)
                {
                    record.FileSyncExecRecord_id = row["FileSyncExecRecord_id"].ToString();
                }
                if (row["FileSyncExecRecord_Type"] != null)
                {
                    record.FileSyncExecRecord_Type = row["FileSyncExecRecord_Type"].ToString();
                }
                if ((row["FileSyncExecRecord_TimeStart"] != null) && (row["FileSyncExecRecord_TimeStart"].ToString() != ""))
                {
                    record.FileSyncExecRecord_TimeStart = new DateTime?(DateTime.Parse(row["FileSyncExecRecord_TimeStart"].ToString()));
                }
                if ((row["FileSyncExecRecord_TimeEnd"] != null) && (row["FileSyncExecRecord_TimeEnd"].ToString() != ""))
                {
                    record.FileSyncExecRecord_TimeEnd = new DateTime?(DateTime.Parse(row["FileSyncExecRecord_TimeEnd"].ToString()));
                }
                if (row["FileSyncExecRecord_Status"] != null)
                {
                    record.FileSyncExecRecord_Status = row["FileSyncExecRecord_Status"].ToString();
                }
                if (row["FileSyncExecRecord_Condition"] != null)
                {
                    record.FileSyncExecRecord_Condition = row["FileSyncExecRecord_Condition"].ToString();
                }
                if (row["createUser"] != null)
                {
                    record.createUser = row["createUser"].ToString();
                }
                if (row["FileSyncExecRecord_Remark"] != null)
                {
                    record.FileSyncExecRecord_Remark = row["FileSyncExecRecord_Remark"].ToString();
                }
            }
            return record;
        }

        public bool Delete(string FileSyncExecRecord_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from FileSyncExecRecord ");
            builder.Append(" where FileSyncExecRecord_id=@FileSyncExecRecord_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecord_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncExecRecord_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string FileSyncExecRecord_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from FileSyncExecRecord ");
            builder.Append(" where FileSyncExecRecord_id in (" + FileSyncExecRecord_idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string FileSyncExecRecord_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from FileSyncExecRecord");
            builder.Append(" where FileSyncExecRecord_id=@FileSyncExecRecord_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecord_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncExecRecord_id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FileSyncExecRecord_id,FileSyncExecRecord_Type,FileSyncExecRecord_TimeStart,FileSyncExecRecord_TimeEnd,FileSyncExecRecord_Status,FileSyncExecRecord_Condition,createUser,FileSyncExecRecord_Remark ");
            builder.Append(" FROM FileSyncExecRecord ");
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
            builder.Append(" FileSyncExecRecord_id,FileSyncExecRecord_Type,FileSyncExecRecord_TimeStart,FileSyncExecRecord_TimeEnd,FileSyncExecRecord_Status,FileSyncExecRecord_Condition,createUser,FileSyncExecRecord_Remark ");
            builder.Append(" FROM FileSyncExecRecord ");
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
                builder.Append("order by T.FileSyncExecRecord_id desc");
            }
            builder.Append(")AS Row, T.*  from FileSyncExecRecord T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_FileSyncExecRecord GetModel(string FileSyncExecRecord_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FileSyncExecRecord_id,FileSyncExecRecord_Type,FileSyncExecRecord_TimeStart,FileSyncExecRecord_TimeEnd,FileSyncExecRecord_Status,FileSyncExecRecord_Condition,createUser,FileSyncExecRecord_Remark from FileSyncExecRecord ");
            builder.Append(" where FileSyncExecRecord_id=@FileSyncExecRecord_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecord_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncExecRecord_id;
            new Model_FileSyncExecRecord();
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
            builder.Append("select count(1) FROM FileSyncExecRecord ");
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

        public bool Update(Model_FileSyncExecRecord model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update FileSyncExecRecord set ");
            builder.Append("FileSyncExecRecord_Type=@FileSyncExecRecord_Type,");
            builder.Append("FileSyncExecRecord_TimeStart=@FileSyncExecRecord_TimeStart,");
            builder.Append("FileSyncExecRecord_TimeEnd=@FileSyncExecRecord_TimeEnd,");
            builder.Append("FileSyncExecRecord_Status=@FileSyncExecRecord_Status,");
            builder.Append("FileSyncExecRecord_Condition=@FileSyncExecRecord_Condition,");
            builder.Append("createUser=@createUser,");
            builder.Append("FileSyncExecRecord_Remark=@FileSyncExecRecord_Remark");
            builder.Append(" where FileSyncExecRecord_id=@FileSyncExecRecord_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecord_Type", SqlDbType.VarChar, 30), new SqlParameter("@FileSyncExecRecord_TimeStart", SqlDbType.DateTime), new SqlParameter("@FileSyncExecRecord_TimeEnd", SqlDbType.DateTime), new SqlParameter("@FileSyncExecRecord_Status", SqlDbType.VarChar, 10), new SqlParameter("@FileSyncExecRecord_Condition", SqlDbType.VarChar, 100), new SqlParameter("@createUser", SqlDbType.Char, 0x24), new SqlParameter("@FileSyncExecRecord_Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@FileSyncExecRecord_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.FileSyncExecRecord_Type;
            cmdParms[1].Value = model.FileSyncExecRecord_TimeStart;
            cmdParms[2].Value = model.FileSyncExecRecord_TimeEnd;
            cmdParms[3].Value = model.FileSyncExecRecord_Status;
            cmdParms[4].Value = model.FileSyncExecRecord_Condition;
            cmdParms[5].Value = model.createUser;
            cmdParms[6].Value = model.FileSyncExecRecord_Remark;
            cmdParms[7].Value = model.FileSyncExecRecord_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update_Operate(Model_FileSyncExecRecord model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update FileSyncExecRecord set ");
            builder.Append("FileSyncExecRecord_Type=@FileSyncExecRecord_Type,");
            builder.Append("FileSyncExecRecord_TimeStart=@FileSyncExecRecord_TimeStart,");
            builder.Append("FileSyncExecRecord_TimeEnd=@FileSyncExecRecord_TimeEnd,");
            builder.Append("FileSyncExecRecord_Status=@FileSyncExecRecord_Status,");
            builder.Append("FileSyncExecRecord_Condition=@FileSyncExecRecord_Condition,");
            builder.Append("createUser=@createUser,");
            builder.Append("FileSyncExecRecord_Remark=@FileSyncExecRecord_Remark");
            builder.Append(" where FileSyncExecRecord_id=@FileSyncExecRecord_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecord_Type", SqlDbType.VarChar, 30), new SqlParameter("@FileSyncExecRecord_TimeStart", SqlDbType.DateTime), new SqlParameter("@FileSyncExecRecord_TimeEnd", SqlDbType.DateTime), new SqlParameter("@FileSyncExecRecord_Status", SqlDbType.VarChar, 10), new SqlParameter("@FileSyncExecRecord_Condition", SqlDbType.VarChar, 100), new SqlParameter("@createUser", SqlDbType.Char, 0x24), new SqlParameter("@FileSyncExecRecord_Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@FileSyncExecRecord_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.FileSyncExecRecord_Type;
            cmdParms[1].Value = model.FileSyncExecRecord_TimeStart;
            cmdParms[2].Value = model.FileSyncExecRecord_TimeEnd;
            cmdParms[3].Value = model.FileSyncExecRecord_Status;
            cmdParms[4].Value = model.FileSyncExecRecord_Condition;
            cmdParms[5].Value = model.createUser;
            cmdParms[6].Value = model.FileSyncExecRecord_Remark;
            cmdParms[7].Value = model.FileSyncExecRecord_id;
            return (DbHelperSQL_Operate.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

