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

    public class DictionarySQlMaintenanceDAL
    {
        public DictionarySQlMaintenanceDAL()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public DictionarySQlMaintenanceDAL(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(DictionarySQlMaintenanceModel model)
        {
            return this.Add(null, model);
        }

        internal int Add(DbTransaction tran, DictionarySQlMaintenanceModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append(" DictionarySQlMaintenance( ");
            builder.Append(" DictionarySQlMaintenance_ID,DictionarySQlMaintenance_Mark,DictionarySQlMaintenance_Name,DictionarySQlMaintenance_Explanation,DictionarySQlMaintenance_SQL,DictionarySQlMaintenance_CretateUser,DictionarySQlMaintenance_CreateTime,DictionarySQlMaintenance_UpdateUser,DictionarySQlMaintenance_UpdateTime ");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @DictionarySQlMaintenance_ID,@DictionarySQlMaintenance_Mark,@DictionarySQlMaintenance_Name,@DictionarySQlMaintenance_Explanation,@DictionarySQlMaintenance_SQL,@DictionarySQlMaintenance_CretateUser,@DictionarySQlMaintenance_CreateTime,@DictionarySQlMaintenance_UpdateUser,@DictionarySQlMaintenance_UpdateTime ");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.DictionarySQlMaintenance_ID, model.DictionarySQlMaintenance_Mark, model.DictionarySQlMaintenance_Name, model.DictionarySQlMaintenance_Explanation, model.DictionarySQlMaintenance_SQL, model.DictionarySQlMaintenance_CretateUser, model.DictionarySQlMaintenance_CreateTime, model.DictionarySQlMaintenance_UpdateUser, model.DictionarySQlMaintenance_UpdateTime });
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DeleteByCondition(null, strCondition, param);
        }

        internal int DeleteByCondition(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM ");
            builder.Append(" DictionarySQlMaintenance ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, param);
        }

        public int DeleteByPK(string dictionarysqlmaintenance_id)
        {
            return this.DeleteByPK(null, dictionarysqlmaintenance_id);
        }

        internal int DeleteByPK(DbTransaction tran, string dictionarysqlmaintenance_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM");
            builder.Append(" DictionarySQlMaintenance ");
            builder.Append(" WHERE ");
            builder.Append(" DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { dictionarysqlmaintenance_id });
        }

        public DataSet DictionarySQlMaintenanceList(string Content, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            StringBuilder builder = new StringBuilder();
            new StringBuilder();
            builder.Append("select row_number() over(order by DictionarySQlMaintenance_CreateTime desc) AS r_n,*  from   DictionarySQlMaintenance");
            builder.Append(" where 1=1 ");
            if (Content != "")
            {
                builder.Append(Content);
            }
            return sys.GetRecordByPage(builder.ToString(), PageIndex, PageSize, out rCount, out pCount);
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
            builder.Append(" DictionarySQlMaintenance ");
            if (!string.IsNullOrEmpty(conditionStr))
            {
                builder.Append(" WHERE ");
                builder.Append(conditionStr);
            }
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, paramValues)) > 0);
        }

        public bool ExistsByPK(string dictionarysqlmaintenance_id)
        {
            return this.ExistsByPK(null, dictionarysqlmaintenance_id);
        }

        internal bool ExistsByPK(DbTransaction tran, string dictionarysqlmaintenance_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" DictionarySQlMaintenance ");
            builder.Append(" WHERE ");
            builder.Append(" DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { dictionarysqlmaintenance_id })) > 0);
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
            builder.Append(" DictionarySQlMaintenance ");
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

        public int GetDictionarySQlMaintenanceCount(string strCondition, params object[] param)
        {
            return this.GetDictionarySQlMaintenanceCount(null, strCondition, param);
        }

        internal int GetDictionarySQlMaintenanceCount(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" Count(1) ");
            builder.Append(" FROM ");
            builder.Append(" DictionarySQlMaintenance ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, param));
        }

        public DictionarySQlMaintenanceModel GetDictionarySQlMaintenanceModelByPK(string dictionarysqlmaintenance_id)
        {
            return this.GetDictionarySQlMaintenanceModelByPK(null, dictionarysqlmaintenance_id);
        }

        internal DictionarySQlMaintenanceModel GetDictionarySQlMaintenanceModelByPK(DbTransaction tran, string dictionarysqlmaintenance_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" DictionarySQlMaintenance ");
            builder.Append(" WHERE ");
            builder.Append(" DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { dictionarysqlmaintenance_id });
            DictionarySQlMaintenanceModel model = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                model = new DictionarySQlMaintenanceModel();
                if (row["DictionarySQlMaintenance_ID"] != null)
                {
                    model.DictionarySQlMaintenance_ID = row["DictionarySQlMaintenance_ID"].ToString();
                }
                if (row["DictionarySQlMaintenance_Mark"] != null)
                {
                    model.DictionarySQlMaintenance_Mark = row["DictionarySQlMaintenance_Mark"].ToString();
                }
                if (row["DictionarySQlMaintenance_Name"] != null)
                {
                    model.DictionarySQlMaintenance_Name = row["DictionarySQlMaintenance_Name"].ToString();
                }
                if (row["DictionarySQlMaintenance_Explanation"] != null)
                {
                    model.DictionarySQlMaintenance_Explanation = row["DictionarySQlMaintenance_Explanation"].ToString();
                }
                if (row["DictionarySQlMaintenance_SQL"] != null)
                {
                    model.DictionarySQlMaintenance_SQL = row["DictionarySQlMaintenance_SQL"].ToString();
                }
                if (row["DictionarySQlMaintenance_CretateUser"] != null)
                {
                    model.DictionarySQlMaintenance_CretateUser = row["DictionarySQlMaintenance_CretateUser"].ToString();
                }
                if (row["DictionarySQlMaintenance_CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["DictionarySQlMaintenance_CreateTime"].ToString()))
                    {
                        model.DictionarySQlMaintenance_CreateTime = null;
                    }
                    else
                    {
                        model.DictionarySQlMaintenance_CreateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_CreateTime"].ToString()));
                    }
                }
                if (row["DictionarySQlMaintenance_UpdateUser"] != null)
                {
                    model.DictionarySQlMaintenance_UpdateUser = row["DictionarySQlMaintenance_UpdateUser"].ToString();
                }
                if (row["DictionarySQlMaintenance_UpdateTime"] == null)
                {
                    return model;
                }
                if (string.IsNullOrWhiteSpace(row["DictionarySQlMaintenance_UpdateTime"].ToString()))
                {
                    model.DictionarySQlMaintenance_UpdateTime = null;
                    return model;
                }
                model.DictionarySQlMaintenance_UpdateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_UpdateTime"].ToString()));
            }
            return model;
        }

        public List<DictionarySQlMaintenanceModel> GetDictionarySQlMaintenanceModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetDictionarySQlMaintenanceModelList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal List<DictionarySQlMaintenanceModel> GetDictionarySQlMaintenanceModelList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" DictionarySQlMaintenance ");
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
            List<DictionarySQlMaintenanceModel> list = new List<DictionarySQlMaintenanceModel>();
            DictionarySQlMaintenanceModel item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new DictionarySQlMaintenanceModel();
                if (row["DictionarySQlMaintenance_ID"] != null)
                {
                    item.DictionarySQlMaintenance_ID = row["DictionarySQlMaintenance_ID"].ToString();
                }
                if (row["DictionarySQlMaintenance_Mark"] != null)
                {
                    item.DictionarySQlMaintenance_Mark = row["DictionarySQlMaintenance_Mark"].ToString();
                }
                if (row["DictionarySQlMaintenance_Name"] != null)
                {
                    item.DictionarySQlMaintenance_Name = row["DictionarySQlMaintenance_Name"].ToString();
                }
                if (row["DictionarySQlMaintenance_Explanation"] != null)
                {
                    item.DictionarySQlMaintenance_Explanation = row["DictionarySQlMaintenance_Explanation"].ToString();
                }
                if (row["DictionarySQlMaintenance_SQL"] != null)
                {
                    item.DictionarySQlMaintenance_SQL = row["DictionarySQlMaintenance_SQL"].ToString();
                }
                if (row["DictionarySQlMaintenance_CretateUser"] != null)
                {
                    item.DictionarySQlMaintenance_CretateUser = row["DictionarySQlMaintenance_CretateUser"].ToString();
                }
                if (row["DictionarySQlMaintenance_CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["DictionarySQlMaintenance_CreateTime"].ToString()))
                    {
                        item.DictionarySQlMaintenance_CreateTime = null;
                    }
                    else
                    {
                        item.DictionarySQlMaintenance_CreateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_CreateTime"].ToString()));
                    }
                }
                if (row["DictionarySQlMaintenance_UpdateUser"] != null)
                {
                    item.DictionarySQlMaintenance_UpdateUser = row["DictionarySQlMaintenance_UpdateUser"].ToString();
                }
                if (row["DictionarySQlMaintenance_UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["DictionarySQlMaintenance_UpdateTime"].ToString()))
                    {
                        item.DictionarySQlMaintenance_UpdateTime = null;
                    }
                    else
                    {
                        item.DictionarySQlMaintenance_UpdateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_UpdateTime"].ToString()));
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public List<DictionarySQlMaintenanceModel> GetDictionarySQlMaintenanceModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetDictionarySQlMaintenanceModelListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        internal List<DictionarySQlMaintenanceModel> GetDictionarySQlMaintenanceModelListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
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
            builder.Append(string.Format(" SELECT (ROW_NUMBER() OVER(ORDER BY {0} {1})) as rownum,* FROM DictionarySQlMaintenance", orderColumn, orderType));
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
            List<DictionarySQlMaintenanceModel> list = new List<DictionarySQlMaintenanceModel>();
            DictionarySQlMaintenanceModel item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new DictionarySQlMaintenanceModel();
                if (row["DictionarySQlMaintenance_ID"] != null)
                {
                    item.DictionarySQlMaintenance_ID = row["DictionarySQlMaintenance_ID"].ToString();
                }
                if (row["DictionarySQlMaintenance_Mark"] != null)
                {
                    item.DictionarySQlMaintenance_Mark = row["DictionarySQlMaintenance_Mark"].ToString();
                }
                if (row["DictionarySQlMaintenance_Name"] != null)
                {
                    item.DictionarySQlMaintenance_Name = row["DictionarySQlMaintenance_Name"].ToString();
                }
                if (row["DictionarySQlMaintenance_Explanation"] != null)
                {
                    item.DictionarySQlMaintenance_Explanation = row["DictionarySQlMaintenance_Explanation"].ToString();
                }
                if (row["DictionarySQlMaintenance_SQL"] != null)
                {
                    item.DictionarySQlMaintenance_SQL = row["DictionarySQlMaintenance_SQL"].ToString();
                }
                if (row["DictionarySQlMaintenance_CretateUser"] != null)
                {
                    item.DictionarySQlMaintenance_CretateUser = row["DictionarySQlMaintenance_CretateUser"].ToString();
                }
                if (row["DictionarySQlMaintenance_CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["DictionarySQlMaintenance_CreateTime"].ToString()))
                    {
                        item.DictionarySQlMaintenance_CreateTime = null;
                    }
                    else
                    {
                        item.DictionarySQlMaintenance_CreateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_CreateTime"].ToString()));
                    }
                }
                if (row["DictionarySQlMaintenance_UpdateUser"] != null)
                {
                    item.DictionarySQlMaintenance_UpdateUser = row["DictionarySQlMaintenance_UpdateUser"].ToString();
                }
                if (row["DictionarySQlMaintenance_UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["DictionarySQlMaintenance_UpdateTime"].ToString()))
                    {
                        item.DictionarySQlMaintenance_UpdateTime = null;
                    }
                    else
                    {
                        item.DictionarySQlMaintenance_UpdateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_UpdateTime"].ToString()));
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public int Update(DictionarySQlMaintenanceModel model)
        {
            return this.Update(null, model);
        }

        internal int Update(DbTransaction tran, DictionarySQlMaintenanceModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" DictionarySQlMaintenance ");
            builder.Append(" SET ");
            builder.Append(" DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID,DictionarySQlMaintenance_Mark=@DictionarySQlMaintenance_Mark,DictionarySQlMaintenance_Name=@DictionarySQlMaintenance_Name,DictionarySQlMaintenance_Explanation=@DictionarySQlMaintenance_Explanation,DictionarySQlMaintenance_SQL=@DictionarySQlMaintenance_SQL,DictionarySQlMaintenance_CretateUser=@DictionarySQlMaintenance_CretateUser,DictionarySQlMaintenance_CreateTime=@DictionarySQlMaintenance_CreateTime,DictionarySQlMaintenance_UpdateUser=@DictionarySQlMaintenance_UpdateUser,DictionarySQlMaintenance_UpdateTime=@DictionarySQlMaintenance_UpdateTime ");
            builder.Append(" WHERE ");
            builder.Append(" DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.DictionarySQlMaintenance_ID, model.DictionarySQlMaintenance_Mark, model.DictionarySQlMaintenance_Name, model.DictionarySQlMaintenance_Explanation, model.DictionarySQlMaintenance_SQL, model.DictionarySQlMaintenance_CretateUser, model.DictionarySQlMaintenance_CreateTime, model.DictionarySQlMaintenance_UpdateUser, model.DictionarySQlMaintenance_UpdateTime });
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.Update(null, strUpdateColumns, strCondition, param);
        }

        internal int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" DictionarySQlMaintenance ");
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

