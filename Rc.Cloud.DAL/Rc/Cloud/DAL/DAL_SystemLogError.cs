namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_SystemLogError
    {
        public DAL_SystemLogError()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public DAL_SystemLogError(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(Model_SystemLogError model)
        {
            return this.Add(null, model);
        }

        internal int Add(DbTransaction tran, Model_SystemLogError model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append(" SystemLogError( ");
            builder.Append(" SystemLog_ID,SystemLog_PagePath,SystemLog_SysPath,SystemLog_Desc,SystemLog_LoginID,SystemLog_CreateDate,SystemLog_IP,SystemLog_Source ");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @SystemLog_ID,@SystemLog_PagePath,@SystemLog_SysPath,@SystemLog_Desc,@SystemLog_LoginID,@SystemLog_CreateDate,@SystemLog_IP,@SystemLog_Source ");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SystemLog_ID, model.SystemLog_PagePath, model.SystemLog_SysPath, model.SystemLog_Desc, model.SystemLog_LoginID, model.SystemLog_CreateDate, model.SystemLog_IP, model.SystemLog_Source });
        }

        public Model_SystemLogError DataRowToModel(DataRow row)
        {
            Model_SystemLogError error = new Model_SystemLogError();
            if (row != null)
            {
                if (row["SystemLog_ID"] != null)
                {
                    error.SystemLog_ID = row["SystemLog_ID"].ToString();
                }
                if (row["SystemLog_PagePath"] != null)
                {
                    error.SystemLog_PagePath = row["SystemLog_PagePath"].ToString();
                }
                if (row["SystemLog_SysPath"] != null)
                {
                    error.SystemLog_SysPath = row["SystemLog_SysPath"].ToString();
                }
                if (row["SystemLog_Desc"] != null)
                {
                    error.SystemLog_Desc = row["SystemLog_Desc"].ToString();
                }
                if (row["SystemLog_LoginID"] != null)
                {
                    error.SystemLog_LoginID = row["SystemLog_LoginID"].ToString();
                }
                if (row["SystemLog_CreateDate"] != null)
                {
                    error.SystemLog_CreateDate = new DateTime?(DateTime.Parse(row["SystemLog_CreateDate"].ToString()));
                }
                if (row["SystemLog_IP"] != null)
                {
                    error.SystemLog_IP = row["SystemLog_IP"].ToString();
                }
                if (row["SystemLog_Source"] != null)
                {
                    error.SystemLog_Source = new int?(int.Parse(row["SystemLog_Source"].ToString()));
                }
                if (row["DoctorInfo_Name"] != null)
                {
                    error.DoctorName = row["DoctorInfo_Name"].ToString();
                }
            }
            return error;
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DeleteByCondition(null, strCondition, param);
        }

        internal int DeleteByCondition(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM ");
            builder.Append(" SystemLogError ");
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
            builder.Append(" SystemLogError ");
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
            builder.Append(" SystemLogError ");
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
            builder.Append(" SystemLogError ");
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
            builder.Append(" SystemLogError ");
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

        public Model_SystemLogError GetModel_SystemLogErrorByPK(string systemlog_id)
        {
            return this.GetModel_SystemLogErrorByPK(null, systemlog_id);
        }

        internal Model_SystemLogError GetModel_SystemLogErrorByPK(DbTransaction tran, string systemlog_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" SystemLogError ");
            builder.Append(" WHERE ");
            builder.Append(" SystemLog_ID=@SystemLog_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { systemlog_id });
            Model_SystemLogError error = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                error = new Model_SystemLogError();
                if (row["SystemLog_ID"] != null)
                {
                    error.SystemLog_ID = row["SystemLog_ID"].ToString();
                }
                if (row["SystemLog_PagePath"] != null)
                {
                    error.SystemLog_PagePath = row["SystemLog_PagePath"].ToString();
                }
                if (row["SystemLog_SysPath"] != null)
                {
                    error.SystemLog_SysPath = row["SystemLog_SysPath"].ToString();
                }
                if (row["SystemLog_Desc"] != null)
                {
                    error.SystemLog_Desc = row["SystemLog_Desc"].ToString();
                }
                if (row["SystemLog_LoginID"] != null)
                {
                    error.SystemLog_LoginID = row["SystemLog_LoginID"].ToString();
                }
                if (row["SystemLog_CreateDate"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SystemLog_CreateDate"].ToString()))
                    {
                        error.SystemLog_CreateDate = null;
                    }
                    else
                    {
                        error.SystemLog_CreateDate = new DateTime?(DateTime.Parse(row["SystemLog_CreateDate"].ToString()));
                    }
                }
                if (row["SystemLog_IP"] != null)
                {
                    error.SystemLog_IP = row["SystemLog_IP"].ToString();
                }
                if (row["SystemLog_Source"] == null)
                {
                    return error;
                }
                if (string.IsNullOrWhiteSpace(row["SystemLog_Source"].ToString()))
                {
                    error.SystemLog_Source = null;
                    return error;
                }
                error.SystemLog_Source = new int?(int.Parse(row["SystemLog_Source"].ToString()));
            }
            return error;
        }

        public List<Model_SystemLogError> GetModel_SystemLogErrorList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetModel_SystemLogErrorList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SystemLogError> GetModel_SystemLogErrorList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" SystemLogError ");
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
            List<Model_SystemLogError> list = new List<Model_SystemLogError>();
            Model_SystemLogError item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_SystemLogError();
                if (row["SystemLog_ID"] != null)
                {
                    item.SystemLog_ID = row["SystemLog_ID"].ToString();
                }
                if (row["SystemLog_PagePath"] != null)
                {
                    item.SystemLog_PagePath = row["SystemLog_PagePath"].ToString();
                }
                if (row["SystemLog_SysPath"] != null)
                {
                    item.SystemLog_SysPath = row["SystemLog_SysPath"].ToString();
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
                list.Add(item);
            }
            return list;
        }

        public List<Model_SystemLogError> GetModel_SystemLogErrorListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetModel_SystemLogErrorListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SystemLogError> GetModel_SystemLogErrorListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
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
            builder.Append(string.Format(" SELECT (ROW_NUMBER() OVER(ORDER BY {0} {1})) as rownum,* FROM SystemLogError", orderColumn, orderType));
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
            List<Model_SystemLogError> list = new List<Model_SystemLogError>();
            Model_SystemLogError item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_SystemLogError();
                if (row["SystemLog_ID"] != null)
                {
                    item.SystemLog_ID = row["SystemLog_ID"].ToString();
                }
                if (row["SystemLog_PagePath"] != null)
                {
                    item.SystemLog_PagePath = row["SystemLog_PagePath"].ToString();
                }
                if (row["SystemLog_SysPath"] != null)
                {
                    item.SystemLog_SysPath = row["SystemLog_SysPath"].ToString();
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
                list.Add(item);
            }
            return list;
        }

        public int GetSystemLogErrorCount(string strCondition, params object[] param)
        {
            return this.GetSystemLogErrorCount(null, strCondition, param);
        }

        internal int GetSystemLogErrorCount(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" Count(1) ");
            builder.Append(" FROM ");
            builder.Append(" SystemLogError ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, param));
        }

        public Model_SystemLogError GetSystemLogErrorModelBySystemLog_ID(string SystemLog_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SystemLog_ID,SystemLog_PagePath,SystemLog_SysPath,SystemLog_Desc,SystemLog_LoginID,SystemLog_CreateDate,SystemLog_IP,SystemLog_Source,DoctorInfo_Name ");
            builder.Append(" FROM SystemLogError left join DoctorInfo  on SystemLog_LoginID = DoctorInfo_ID");
            builder.Append(" where SystemLog_ID=@SystemLog_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SystemLog_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SystemLog_ID;
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet selectAllSystemLogErrorModel(string txtEC, string D_Name, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            StringBuilder builder = new StringBuilder();
            new StringBuilder();
            builder.Append("select row_number() over(order by SystemLog_CreateDate desc) AS r_n,*  from (  ");
            builder.Append("select d.SystemLog_ID,d.SystemLog_IP,d.SystemLog_PagePath,d.SystemLog_SysPath,d.SystemLog_Desc\r\n                           ,s.SysUser_Name,d.SystemLog_CreateDate from SystemLogError  d left join dbo.SysUser s on \r\n                           d.SystemLog_LoginID=s.SysUser_ID where 1=1 ");
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

        public int Update(Model_SystemLogError model)
        {
            return this.Update(null, model);
        }

        internal int Update(DbTransaction tran, Model_SystemLogError model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SystemLogError ");
            builder.Append(" SET ");
            builder.Append(" SystemLog_ID=@SystemLog_ID,SystemLog_PagePath=@SystemLog_PagePath,SystemLog_SysPath=@SystemLog_SysPath,SystemLog_Desc=@SystemLog_Desc,SystemLog_LoginID=@SystemLog_LoginID,SystemLog_CreateDate=@SystemLog_CreateDate,SystemLog_IP=@SystemLog_IP,SystemLog_Source=@SystemLog_Source ");
            builder.Append(" WHERE ");
            builder.Append(" SystemLog_ID=@SystemLog_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SystemLog_ID, model.SystemLog_PagePath, model.SystemLog_SysPath, model.SystemLog_Desc, model.SystemLog_LoginID, model.SystemLog_CreateDate, model.SystemLog_IP, model.SystemLog_Source });
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.Update(null, strUpdateColumns, strCondition, param);
        }

        internal int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SystemLogError ");
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

