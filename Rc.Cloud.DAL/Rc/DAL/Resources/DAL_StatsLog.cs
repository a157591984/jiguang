namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsLog
    {
        public bool Add(Model_StatsLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsLog(");
            builder.Append("StatsLogId,DataId,DataName,DataType,LogStatus,CTime,CUser,GradeId)");
            builder.Append(" values (");
            builder.Append("@StatsLogId,@DataId,@DataName,@DataType,@LogStatus,@CTime,@CUser,@GradeId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsLogId", SqlDbType.Char, 0x24), new SqlParameter("@DataId", SqlDbType.Char, 0x24), new SqlParameter("@DataName", SqlDbType.NVarChar, 200), new SqlParameter("@DataType", SqlDbType.VarChar, 50), new SqlParameter("@LogStatus", SqlDbType.Char, 1), new SqlParameter("@CTime", SqlDbType.DateTime), new SqlParameter("@CUser", SqlDbType.Char, 0x24), new SqlParameter("@GradeId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.StatsLogId;
            cmdParms[1].Value = model.DataId;
            cmdParms[2].Value = model.DataName;
            cmdParms[3].Value = model.DataType;
            cmdParms[4].Value = model.LogStatus;
            cmdParms[5].Value = model.CTime;
            cmdParms[6].Value = model.CUser;
            cmdParms[7].Value = model.GradeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsLog DataRowToModel(DataRow row)
        {
            Model_StatsLog log = new Model_StatsLog();
            if (row != null)
            {
                if (row["StatsLogId"] != null)
                {
                    log.StatsLogId = row["StatsLogId"].ToString();
                }
                if (row["DataId"] != null)
                {
                    log.DataId = row["DataId"].ToString();
                }
                if (row["DataName"] != null)
                {
                    log.DataName = row["DataName"].ToString();
                }
                if (row["DataType"] != null)
                {
                    log.DataType = row["DataType"].ToString();
                }
                if (row["LogStatus"] != null)
                {
                    log.LogStatus = row["LogStatus"].ToString();
                }
                if ((row["CTime"] != null) && (row["CTime"].ToString() != ""))
                {
                    log.CTime = new DateTime?(DateTime.Parse(row["CTime"].ToString()));
                }
                if (row["CUser"] != null)
                {
                    log.CUser = row["CUser"].ToString();
                }
                if (row["GradeId"] != null)
                {
                    log.GradeId = row["GradeId"].ToString();
                }
            }
            return log;
        }

        public bool Delete(string StatsLogId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsLog ");
            builder.Append(" where StatsLogId=@StatsLogId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsLogId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsLogId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsLogIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsLog ");
            builder.Append(" where StatsLogId in (" + StatsLogIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool ExecuteStatsAddLog(Model_StatsLog modelLog)
        {
            try
            {
                modelLog.LogStatus = "2";
                if (modelLog.DataType == "1")
                {
                    DbHelperSQL.ExecuteSqlByTime("exec dbo.P_StatsClassIsExec '" + modelLog.DataId + "' ", 0x1c20);
                    modelLog.LogStatus = "1";
                }
                else if (modelLog.DataType == "2")
                {
                    DbHelperSQL.ExecuteSqlByTime("exec dbo.P_StatsGradeIsExec '" + modelLog.DataId + "','" + modelLog.GradeId + "' ", 0x1c20);
                    modelLog.LogStatus = "1";
                }
                else if (modelLog.DataType == "3")
                {
                    DbHelperSQL.ExecuteSqlByTime("exec dbo.P_StatsCommentIsExec '" + modelLog.DataId + "' ", 0x1c20);
                    modelLog.LogStatus = "1";
                }
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("\r\nmerge into StatsLog sl\r\nusing (select '{1}' as DataId,'{3}' as DataType,'{7}' as GradeId) tt\r\non sl.DataId=tt.DataId and sl.DataType=tt.DataType and  sl.GradeId=tt.GradeId \r\nwhen matched then update set sl.LogStatus='{4}',sl.CTime='{5}',sl.CUser='{6}'\r\nwhen not matched then\r\ninsert values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');", new object[] { modelLog.StatsLogId, modelLog.DataId, modelLog.DataName, modelLog.DataType, modelLog.LogStatus, modelLog.CTime, modelLog.CUser, modelLog.GradeId });
                return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Exists(string StatsLogId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsLog");
            builder.Append(" where StatsLogId=@StatsLogId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsLogId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsLogId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsLogId,DataId,DataName,DataType,LogStatus,CTime,CUser,GradeId ");
            builder.Append(" FROM StatsLog ");
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
            builder.Append(" StatsLogId,DataId,DataName,DataType,LogStatus,CTime,CUser,GradeId ");
            builder.Append(" FROM StatsLog ");
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
                builder.Append("order by T.StatsLogId desc");
            }
            builder.Append(")AS Row, T.*  from StatsLog T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsLog GetModel(string StatsLogId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsLogId,DataId,DataName,DataType,LogStatus,CTime,CUser,GradeId from StatsLog ");
            builder.Append(" where StatsLogId=@StatsLogId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsLogId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsLogId;
            new Model_StatsLog();
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
            builder.Append("select count(1) FROM StatsLog ");
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

        public bool ReExecuteStatsAddLog(Model_StatsLog modelLog)
        {
            try
            {
                modelLog.LogStatus = "1";
                if (modelLog.DataType == "1")
                {
                    DbHelperSQL.ExecuteSql("update StatsHelper set Correct_Time=getdate() where Homework_Id='" + modelLog.DataId + "' ");
                    DbHelperSQL.ExecuteSqlByTime("exec dbo.P_StatsClassIsExec '" + modelLog.DataId + "' ", 0x1c20);
                }
                else if (modelLog.DataType == "2")
                {
                    DbHelperSQL.ExecuteSql("update StatsHelper set Correct_Time=getdate() where ResourceToResourceFolder_Id='" + modelLog.DataId + "' and GradeId='" + modelLog.GradeId + "' ");
                    DbHelperSQL.ExecuteSqlByTime("exec dbo.P_StatsGradeIsExec '" + modelLog.DataId + "','" + modelLog.GradeId + "' ", 0x1c20);
                }
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("\r\nmerge into StatsLog sl\r\nusing (select '{1}' as DataId,'{3}' as DataType,'{7}' as GradeId) tt\r\non sl.DataId=tt.DataId and sl.DataType=tt.DataType and  sl.GradeId=tt.GradeId \r\nwhen matched then update set sl.LogStatus='{4}',sl.CTime='{5}',sl.CUser='{6}'\r\nwhen not matched then\r\ninsert values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');", new object[] { modelLog.StatsLogId, modelLog.DataId, modelLog.DataName, modelLog.DataType, modelLog.LogStatus, modelLog.CTime, modelLog.CUser, modelLog.GradeId });
                return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Model_StatsLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsLog set ");
            builder.Append("DataId=@DataId,");
            builder.Append("DataName=@DataName,");
            builder.Append("DataType=@DataType,");
            builder.Append("LogStatus=@LogStatus,");
            builder.Append("CTime=@CTime,");
            builder.Append("CUser=@CUser,");
            builder.Append("GradeId=@GradeId");
            builder.Append(" where StatsLogId=@StatsLogId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DataId", SqlDbType.Char, 0x24), new SqlParameter("@DataName", SqlDbType.NVarChar, 200), new SqlParameter("@DataType", SqlDbType.VarChar, 50), new SqlParameter("@LogStatus", SqlDbType.Char, 1), new SqlParameter("@CTime", SqlDbType.DateTime), new SqlParameter("@CUser", SqlDbType.Char, 0x24), new SqlParameter("@GradeId", SqlDbType.Char, 0x24), new SqlParameter("@StatsLogId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.DataId;
            cmdParms[1].Value = model.DataName;
            cmdParms[2].Value = model.DataType;
            cmdParms[3].Value = model.LogStatus;
            cmdParms[4].Value = model.CTime;
            cmdParms[5].Value = model.CUser;
            cmdParms[6].Value = model.GradeId;
            cmdParms[7].Value = model.StatsLogId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

