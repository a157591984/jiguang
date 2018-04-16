namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SyncData
    {
        public bool Add(Model_SyncData model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SyncData(");
            builder.Append("SyncDataId,TableName,DataId,OperateType,CreateTime,SyncStatus)");
            builder.Append(" values (");
            builder.Append("@SyncDataId,@TableName,@DataId,@OperateType,@CreateTime,@SyncStatus)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncDataId", SqlDbType.Char, 0x24), new SqlParameter("@TableName", SqlDbType.VarChar, 100), new SqlParameter("@DataId", SqlDbType.Char, 0x24), new SqlParameter("@OperateType", SqlDbType.VarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@SyncStatus", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = model.SyncDataId;
            cmdParms[1].Value = model.TableName;
            cmdParms[2].Value = model.DataId;
            cmdParms[3].Value = model.OperateType;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.SyncStatus;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SyncData DataRowToModel(DataRow row)
        {
            Model_SyncData data = new Model_SyncData();
            if (row != null)
            {
                if (row["SyncDataId"] != null)
                {
                    data.SyncDataId = row["SyncDataId"].ToString();
                }
                if (row["TableName"] != null)
                {
                    data.TableName = row["TableName"].ToString();
                }
                if (row["DataId"] != null)
                {
                    data.DataId = row["DataId"].ToString();
                }
                if (row["OperateType"] != null)
                {
                    data.OperateType = row["OperateType"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    data.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["SyncStatus"] != null)
                {
                    data.SyncStatus = row["SyncStatus"].ToString();
                }
            }
            return data;
        }

        public bool Delete(string SyncDataId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SyncData ");
            builder.Append(" where SyncDataId=@SyncDataId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncDataId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncDataId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SyncDataIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SyncData ");
            builder.Append(" where SyncDataId in (" + SyncDataIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string SyncDataId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SyncData");
            builder.Append(" where SyncDataId=@SyncDataId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncDataId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncDataId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SyncDataId,TableName,DataId,OperateType,CreateTime,SyncStatus ");
            builder.Append(" FROM SyncData ");
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
            builder.Append(" SyncDataId,TableName,DataId,OperateType,CreateTime,SyncStatus ");
            builder.Append(" FROM SyncData ");
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
                builder.Append("order by T.SyncDataId desc");
            }
            builder.Append(")AS Row, T.*  from SyncData T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SyncData GetModel(string SyncDataId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SyncDataId,TableName,DataId,OperateType,CreateTime,SyncStatus from SyncData ");
            builder.Append(" where SyncDataId=@SyncDataId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncDataId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncDataId;
            new Model_SyncData();
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
            builder.Append("select count(1) FROM SyncData ");
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

        public bool Update(Model_SyncData model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SyncData set ");
            builder.Append("TableName=@TableName,");
            builder.Append("DataId=@DataId,");
            builder.Append("OperateType=@OperateType,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("SyncStatus=@SyncStatus");
            builder.Append(" where SyncDataId=@SyncDataId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TableName", SqlDbType.VarChar, 100), new SqlParameter("@DataId", SqlDbType.Char, 0x24), new SqlParameter("@OperateType", SqlDbType.VarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@SyncStatus", SqlDbType.VarChar, 50), new SqlParameter("@SyncDataId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.TableName;
            cmdParms[1].Value = model.DataId;
            cmdParms[2].Value = model.OperateType;
            cmdParms[3].Value = model.CreateTime;
            cmdParms[4].Value = model.SyncStatus;
            cmdParms[5].Value = model.SyncDataId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

