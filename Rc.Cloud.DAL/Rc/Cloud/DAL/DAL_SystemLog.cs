namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_SystemLog
    {
        public DAL_SystemLog()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public DAL_SystemLog(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(Model_SystemLog model)
        {
            return this.Add(null, model);
        }

        internal int Add(DbTransaction tran, Model_SystemLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append(" SystemLog( ");
            builder.Append(" SystemLog_ID,SystemLog_Level,SystemLog_Model,SystemLog_TableName,SystemLog_TableDataID,SystemLog_Desc,SystemLog_LoginID,SystemLog_CreateDate,SystemLog_IP,SystemLog_Source,SystemLog_Type,SystemLog_Remark ");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @SystemLog_ID,@SystemLog_Level,@SystemLog_Model,@SystemLog_TableName,@SystemLog_TableDataID,@SystemLog_Desc,@SystemLog_LoginID,@SystemLog_CreateDate,@SystemLog_IP,@SystemLog_Source,@SystemLog_Type,@SystemLog_Remark ");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SystemLog_ID, model.SystemLog_Level, model.SystemLog_Model, model.SystemLog_TableName, model.SystemLog_TableDataID, model.SystemLog_Desc, model.SystemLog_LoginID, model.SystemLog_CreateDate, model.SystemLog_IP, model.SystemLog_Source, model.SystemLog_Type, model.SystemLog_Remark });
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DeleteByCondition(null, strCondition, param);
        }

        internal int DeleteByCondition(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM ");
            builder.Append(" SystemLog ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, param);
        }

        public int DeleteByPK(string systemlog_id)
        {
            return this.DeleteByPK(null, systemlog_id);
        }

        internal int DeleteByPK(DbTransaction tran, string systemlog_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM");
            builder.Append(" SystemLog ");
            builder.Append(" WHERE ");
            builder.Append(" SystemLog_ID=@SystemLog_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { systemlog_id });
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.ExistsByCondition(null, conditionStr, paramValues);
        }

        internal bool ExistsByCondition(DbTransaction tran, string conditionStr, params object[] paramValues)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" SystemLog ");
            if (!string.IsNullOrEmpty(conditionStr))
            {
                builder.Append(" WHERE ");
                builder.Append(conditionStr);
            }
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, paramValues)) > 0);
        }

        public bool ExistsByPK(string systemlog_id)
        {
            return this.ExistsByPK(null, systemlog_id);
        }

        internal bool ExistsByPK(DbTransaction tran, string systemlog_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" SystemLog ");
            builder.Append(" WHERE ");
            builder.Append(" SystemLog_ID=@SystemLog_ID ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { systemlog_id })) > 0);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetDataSet(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal DataSet GetDataSet(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" SystemLog ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            if (!string.IsNullOrEmpty(orderColumn))
            {
                builder.Append(" ORDER BY ");
                builder.Append(orderColumn);
                if (!string.IsNullOrEmpty(orderType))
                {
                    builder.Append(" " + orderType);
                }
            }
            return this.CurrDB.ExecuteDataSet(builder.ToString(), tran, param);
        }

        public Model_SystemLog GetModel_SystemLogByPK(string systemlog_id)
        {
            return this.GetModel_SystemLogByPK(null, systemlog_id);
        }

        internal Model_SystemLog GetModel_SystemLogByPK(DbTransaction tran, string systemlog_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" SystemLog ");
            builder.Append(" WHERE ");
            builder.Append(" SystemLog_ID=@SystemLog_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { systemlog_id });
            Model_SystemLog log = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                log = new Model_SystemLog();
                if (row["SystemLog_ID"] != null)
                {
                    log.SystemLog_ID = row["SystemLog_ID"].ToString();
                }
                if (row["SystemLog_Level"] != null)
                {
                    log.SystemLog_Level = row["SystemLog_Level"].ToString();
                }
                if (row["SystemLog_Model"] != null)
                {
                    log.SystemLog_Model = row["SystemLog_Model"].ToString();
                }
                if (row["SystemLog_TableName"] != null)
                {
                    log.SystemLog_TableName = row["SystemLog_TableName"].ToString();
                }
                if (row["SystemLog_TableDataID"] != null)
                {
                    log.SystemLog_TableDataID = row["SystemLog_TableDataID"].ToString();
                }
                if (row["SystemLog_Desc"] != null)
                {
                    log.SystemLog_Desc = row["SystemLog_Desc"].ToString();
                }
                if (row["SystemLog_LoginID"] != null)
                {
                    log.SystemLog_LoginID = row["SystemLog_LoginID"].ToString();
                }
                if (row["SystemLog_CreateDate"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SystemLog_CreateDate"].ToString()))
                    {
                        log.SystemLog_CreateDate = null;
                    }
                    else
                    {
                        log.SystemLog_CreateDate = new DateTime?(DateTime.Parse(row["SystemLog_CreateDate"].ToString()));
                    }
                }
                if (row["SystemLog_IP"] != null)
                {
                    log.SystemLog_IP = row["SystemLog_IP"].ToString();
                }
                if (row["SystemLog_Source"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SystemLog_Source"].ToString()))
                    {
                        log.SystemLog_Source = null;
                    }
                    else
                    {
                        log.SystemLog_Source = new int?(int.Parse(row["SystemLog_Source"].ToString()));
                    }
                }
                if (row["SystemLog_Type"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SystemLog_Type"].ToString()))
                    {
                        log.SystemLog_Type = null;
                    }
                    else
                    {
                        log.SystemLog_Type = new int?(int.Parse(row["SystemLog_Type"].ToString()));
                    }
                }
                if (row["SystemLog_Remark"] != null)
                {
                    log.SystemLog_Remark = row["SystemLog_Remark"].ToString();
                }
            }
            return log;
        }

        public List<Model_SystemLog> GetModel_SystemLogList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetModel_SystemLogList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SystemLog> GetModel_SystemLogList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" SystemLog ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            if (!string.IsNullOrEmpty(orderColumn))
            {
                builder.Append(" ORDER BY ");
                builder.Append(orderColumn);
                if (!string.IsNullOrEmpty(orderType))
                {
                    builder.Append(" " + orderType);
                }
            }
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, param);
            List<Model_SystemLog> list = new List<Model_SystemLog>();
            Model_SystemLog item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_SystemLog();
                if (row["SystemLog_ID"] != null)
                {
                    item.SystemLog_ID = row["SystemLog_ID"].ToString();
                }
                if (row["SystemLog_Level"] != null)
                {
                    item.SystemLog_Level = row["SystemLog_Level"].ToString();
                }
                if (row["SystemLog_Model"] != null)
                {
                    item.SystemLog_Model = row["SystemLog_Model"].ToString();
                }
                if (row["SystemLog_TableName"] != null)
                {
                    item.SystemLog_TableName = row["SystemLog_TableName"].ToString();
                }
                if (row["SystemLog_TableDataID"] != null)
                {
                    item.SystemLog_TableDataID = row["SystemLog_TableDataID"].ToString();
                }
                if (row["SystemLog_Desc"] != null)
                {
                    item.SystemLog_Desc = row["SystemLog_Desc"].ToString();
                }
                if (row["SystemLog_LoginID"] != null)
                {
                    item.SystemLog_LoginID = row["SystemLog_LoginID"].ToString();
                }
                if (row["SystemLog_CreateDate"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SystemLog_CreateDate"].ToString()))
                    {
                        item.SystemLog_CreateDate = null;
                    }
                    else
                    {
                        item.SystemLog_CreateDate = new DateTime?(DateTime.Parse(row["SystemLog_CreateDate"].ToString()));
                    }
                }
                if (row["SystemLog_IP"] != null)
                {
                    item.SystemLog_IP = row["SystemLog_IP"].ToString();
                }
                if (row["SystemLog_Source"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SystemLog_Source"].ToString()))
                    {
                        item.SystemLog_Source = null;
                    }
                    else
                    {
                        item.SystemLog_Source = new int?(int.Parse(row["SystemLog_Source"].ToString()));
                    }
                }
                if (row["SystemLog_Type"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SystemLog_Type"].ToString()))
                    {
                        item.SystemLog_Type = null;
                    }
                    else
                    {
                        item.SystemLog_Type = new int?(int.Parse(row["SystemLog_Type"].ToString()));
                    }
                }
                if (row["SystemLog_Remark"] != null)
                {
                    item.SystemLog_Remark = row["SystemLog_Remark"].ToString();
                }
                list.Add(item);
            }
            return list;
        }

        public List<Model_SystemLog> GetModel_SystemLogListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetModel_SystemLogListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SystemLog> GetModel_SystemLogListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            if ((pageSize <= 0) || (pageIndex <= 0))
            {
                throw new Exception("分页参数错误，必须大于零");
            }
            if (string.IsNullOrEmpty(orderColumn))
            {
                throw new Exception("排序字段必须填写");
            }
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageIndex * pageSize;
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM (");
            builder.Append(string.Format(" SELECT (ROW_NUMBER() OVER(ORDER BY {0} {1})) as rownum,* FROM SystemLog", orderColumn, orderType));
            if (!string.IsNullOrWhiteSpace(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            builder.Append(" ) t ");
            builder.Append(" WHERE rownum between ");
            builder.Append(string.Format(" {0} ", num));
            builder.Append(" AND ");
            builder.Append(string.Format(" {0} ", num2));
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, param);
            List<Model_SystemLog> list = new List<Model_SystemLog>();
            Model_SystemLog item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_SystemLog();
                if (row["SystemLog_ID"] != null)
                {
                    item.SystemLog_ID = row["SystemLog_ID"].ToString();
                }
                if (row["SystemLog_Level"] != null)
                {
                    item.SystemLog_Level = row["SystemLog_Level"].ToString();
                }
                if (row["SystemLog_Model"] != null)
                {
                    item.SystemLog_Model = row["SystemLog_Model"].ToString();
                }
                if (row["SystemLog_TableName"] != null)
                {
                    item.SystemLog_TableName = row["SystemLog_TableName"].ToString();
                }
                if (row["SystemLog_TableDataID"] != null)
                {
                    item.SystemLog_TableDataID = row["SystemLog_TableDataID"].ToString();
                }
                if (row["SystemLog_Desc"] != null)
                {
                    item.SystemLog_Desc = row["SystemLog_Desc"].ToString();
                }
                if (row["SystemLog_LoginID"] != null)
                {
                    item.SystemLog_LoginID = row["SystemLog_LoginID"].ToString();
                }
                if (row["SystemLog_CreateDate"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SystemLog_CreateDate"].ToString()))
                    {
                        item.SystemLog_CreateDate = null;
                    }
                    else
                    {
                        item.SystemLog_CreateDate = new DateTime?(DateTime.Parse(row["SystemLog_CreateDate"].ToString()));
                    }
                }
                if (row["SystemLog_IP"] != null)
                {
                    item.SystemLog_IP = row["SystemLog_IP"].ToString();
                }
                if (row["SystemLog_Source"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SystemLog_Source"].ToString()))
                    {
                        item.SystemLog_Source = null;
                    }
                    else
                    {
                        item.SystemLog_Source = new int?(int.Parse(row["SystemLog_Source"].ToString()));
                    }
                }
                if (row["SystemLog_Type"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SystemLog_Type"].ToString()))
                    {
                        item.SystemLog_Type = null;
                    }
                    else
                    {
                        item.SystemLog_Type = new int?(int.Parse(row["SystemLog_Type"].ToString()));
                    }
                }
                if (row["SystemLog_Remark"] != null)
                {
                    item.SystemLog_Remark = row["SystemLog_Remark"].ToString();
                }
                list.Add(item);
            }
            return list;
        }

        public int GetSystemLogCount(string strCondition, params object[] param)
        {
            return this.GetSystemLogCount(null, strCondition, param);
        }

        internal int GetSystemLogCount(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" Count(1) ");
            builder.Append(" FROM ");
            builder.Append(" SystemLog ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, param));
        }

        public DataSet SelectAllSystemLogModel(string txtEC, string D_Name, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            StringBuilder builder = new StringBuilder();
            new StringBuilder();
            builder.Append("select row_number() over(order by SystemLog_CreateDate desc) AS r_n,*  from (  ");
            builder.Append("select d.SystemLog_ID,d.SystemLog_IP,d.SystemLog_Level,d.SystemLog_Model,d.SystemLog_Desc\r\n                           ,s.SysUser_Name,d.SystemLog_CreateDate from SystemLog d left join dbo.SysUser s on \r\n                           d.SystemLog_LoginID=s.SysUser_ID where 1=1 ");
            if (D_Name != "")
            {
                builder.Append(" and s.SysUser_Name like '%" + D_Name + "%'");
            }
            if (txtEC != "")
            {
                builder.Append(" and d.SystemLog_Desc like '%" + txtEC + "%'");
            }
            builder.Append(") a");
            return sys.GetRecordByPage(builder.ToString(), PageIndex, PageSize, out rCount, out pCount);
        }

        public int Update(Model_SystemLog model)
        {
            return this.Update(null, model);
        }

        internal int Update(DbTransaction tran, Model_SystemLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SystemLog ");
            builder.Append(" SET ");
            builder.Append(" SystemLog_ID=@SystemLog_ID,SystemLog_Level=@SystemLog_Level,SystemLog_Model=@SystemLog_Model,SystemLog_TableName=@SystemLog_TableName,SystemLog_TableDataID=@SystemLog_TableDataID,SystemLog_Desc=@SystemLog_Desc,SystemLog_LoginID=@SystemLog_LoginID,SystemLog_CreateDate=@SystemLog_CreateDate,SystemLog_IP=@SystemLog_IP,SystemLog_Source=@SystemLog_Source,SystemLog_Type=@SystemLog_Type,SystemLog_Remark=@SystemLog_Remark ");
            builder.Append(" WHERE ");
            builder.Append(" SystemLog_ID=@SystemLog_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SystemLog_ID, model.SystemLog_Level, model.SystemLog_Model, model.SystemLog_TableName, model.SystemLog_TableDataID, model.SystemLog_Desc, model.SystemLog_LoginID, model.SystemLog_CreateDate, model.SystemLog_IP, model.SystemLog_Source, model.SystemLog_Type, model.SystemLog_Remark });
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.Update(null, strUpdateColumns, strCondition, param);
        }

        internal int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SystemLog ");
            builder.Append(" SET ");
            builder.Append(strUpdateColumns);
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, param);
        }

        private DatabaseSQLHelper CurrDB { get; set; }
    }
}

