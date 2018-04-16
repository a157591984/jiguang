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

    public class DAL_DictionarySQlMaintenance
    {
        public DAL_DictionarySQlMaintenance()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public DAL_DictionarySQlMaintenance(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(Model_DictionarySQlMaintenance model)
        {
            return this.Add(null, model);
        }

        private int Add(DbTransaction tran, Model_DictionarySQlMaintenance model)
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

        public int AddNew(Model_DictionarySQlMaintenance model)
        {
            return this.AddNew(null, model);
        }

        private int AddNew(DbTransaction tran, Model_DictionarySQlMaintenance model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append("  DictionarySQlMaintenance( ");
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

        private int DeleteByCondition(DbTransaction tran, string strCondition, params object[] param)
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

        private int DeleteByPK(DbTransaction tran, string dictionarysqlmaintenance_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM");
            builder.Append(" DictionarySQlMaintenance ");
            builder.Append(" WHERE ");
            builder.Append(" DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { dictionarysqlmaintenance_id });
        }

        public int DeleteByPKNew(string dictionarysqlmaintenance_id)
        {
            return this.DeleteByPKNew(null, dictionarysqlmaintenance_id);
        }

        private int DeleteByPKNew(DbTransaction tran, string dictionarysqlmaintenance_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM");
            builder.Append("  DictionarySQlMaintenance ");
            builder.Append(" WHERE ");
            builder.Append(" DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { dictionarysqlmaintenance_id });
        }

        public DataSet DictionarySQlMaintenanceList(string Content)
        {
            StringBuilder builder = new StringBuilder();
            new StringBuilder();
            builder.Append("select *  from  DictionarySQlMaintenance");
            builder.Append(" where 1=1 ");
            if (Content != "")
            {
                builder.Append(Content);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet DictionarySQlMaintenanceList(string Content, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            StringBuilder builder = new StringBuilder();
            new StringBuilder();
            builder.Append("select row_number() over(order by DictionarySQlMaintenance_Mark) AS r_n,*  from  DictionarySQlMaintenance");
            builder.Append(" where 1=1 ");
            if (Content != "")
            {
                builder.Append(Content);
            }
            return DbHelperSQL.GetRecordByPage(builder.ToString(), PageIndex, PageSize, out rCount, out pCount);
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.ExistsByCondition(null, conditionStr, paramValues);
        }

        private bool ExistsByCondition(DbTransaction tran, string conditionStr, params object[] paramValues)
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

        private bool ExistsByPK(DbTransaction tran, string dictionarysqlmaintenance_id)
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

        private DataSet GetDataSet(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
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

        private int GetDictionarySQlMaintenanceCount(DbTransaction tran, string strCondition, params object[] param)
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

        public Model_DictionarySQlMaintenance GetDictionarySQlMaintenanceModelByPK(string dictionarysqlmaintenance_id)
        {
            return this.GetDictionarySQlMaintenanceModelByPK(null, dictionarysqlmaintenance_id);
        }

        private Model_DictionarySQlMaintenance GetDictionarySQlMaintenanceModelByPK(DbTransaction tran, string dictionarysqlmaintenance_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" DictionarySQlMaintenance ");
            builder.Append(" WHERE ");
            builder.Append(" DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { dictionarysqlmaintenance_id });
            Model_DictionarySQlMaintenance maintenance = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                maintenance = new Model_DictionarySQlMaintenance();
                if (row["DictionarySQlMaintenance_ID"] != null)
                {
                    maintenance.DictionarySQlMaintenance_ID = row["DictionarySQlMaintenance_ID"].ToString();
                }
                if (row["DictionarySQlMaintenance_Mark"] != null)
                {
                    maintenance.DictionarySQlMaintenance_Mark = row["DictionarySQlMaintenance_Mark"].ToString();
                }
                if (row["DictionarySQlMaintenance_Name"] != null)
                {
                    maintenance.DictionarySQlMaintenance_Name = row["DictionarySQlMaintenance_Name"].ToString();
                }
                if (row["DictionarySQlMaintenance_Explanation"] != null)
                {
                    maintenance.DictionarySQlMaintenance_Explanation = row["DictionarySQlMaintenance_Explanation"].ToString();
                }
                if (row["DictionarySQlMaintenance_SQL"] != null)
                {
                    maintenance.DictionarySQlMaintenance_SQL = row["DictionarySQlMaintenance_SQL"].ToString();
                }
                if (row["DictionarySQlMaintenance_CretateUser"] != null)
                {
                    maintenance.DictionarySQlMaintenance_CretateUser = row["DictionarySQlMaintenance_CretateUser"].ToString();
                }
                if (row["DictionarySQlMaintenance_CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["DictionarySQlMaintenance_CreateTime"].ToString()))
                    {
                        maintenance.DictionarySQlMaintenance_CreateTime = null;
                    }
                    else
                    {
                        maintenance.DictionarySQlMaintenance_CreateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_CreateTime"].ToString()));
                    }
                }
                if (row["DictionarySQlMaintenance_UpdateUser"] != null)
                {
                    maintenance.DictionarySQlMaintenance_UpdateUser = row["DictionarySQlMaintenance_UpdateUser"].ToString();
                }
                if (row["DictionarySQlMaintenance_UpdateTime"] == null)
                {
                    return maintenance;
                }
                if (string.IsNullOrWhiteSpace(row["DictionarySQlMaintenance_UpdateTime"].ToString()))
                {
                    maintenance.DictionarySQlMaintenance_UpdateTime = null;
                    return maintenance;
                }
                maintenance.DictionarySQlMaintenance_UpdateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_UpdateTime"].ToString()));
            }
            return maintenance;
        }

        public Model_DictionarySQlMaintenance GetDictionarySQlMaintenanceModelByPKNew(string dictionarysqlmaintenance_id)
        {
            return this.GetDictionarySQlMaintenanceModelByPKNew(null, dictionarysqlmaintenance_id);
        }

        private Model_DictionarySQlMaintenance GetDictionarySQlMaintenanceModelByPKNew(DbTransaction tran, string dictionarysqlmaintenance_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append("  DictionarySQlMaintenance ");
            builder.Append(" WHERE ");
            builder.Append(" DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { dictionarysqlmaintenance_id });
            Model_DictionarySQlMaintenance maintenance = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                maintenance = new Model_DictionarySQlMaintenance();
                if (row["DictionarySQlMaintenance_ID"] != null)
                {
                    maintenance.DictionarySQlMaintenance_ID = row["DictionarySQlMaintenance_ID"].ToString();
                }
                if (row["DictionarySQlMaintenance_Mark"] != null)
                {
                    maintenance.DictionarySQlMaintenance_Mark = row["DictionarySQlMaintenance_Mark"].ToString();
                }
                if (row["DictionarySQlMaintenance_Name"] != null)
                {
                    maintenance.DictionarySQlMaintenance_Name = row["DictionarySQlMaintenance_Name"].ToString();
                }
                if (row["DictionarySQlMaintenance_Explanation"] != null)
                {
                    maintenance.DictionarySQlMaintenance_Explanation = row["DictionarySQlMaintenance_Explanation"].ToString();
                }
                if (row["DictionarySQlMaintenance_SQL"] != null)
                {
                    maintenance.DictionarySQlMaintenance_SQL = row["DictionarySQlMaintenance_SQL"].ToString();
                }
                if (row["DictionarySQlMaintenance_CretateUser"] != null)
                {
                    maintenance.DictionarySQlMaintenance_CretateUser = row["DictionarySQlMaintenance_CretateUser"].ToString();
                }
                if (row["DictionarySQlMaintenance_CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["DictionarySQlMaintenance_CreateTime"].ToString()))
                    {
                        maintenance.DictionarySQlMaintenance_CreateTime = null;
                    }
                    else
                    {
                        maintenance.DictionarySQlMaintenance_CreateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_CreateTime"].ToString()));
                    }
                }
                if (row["DictionarySQlMaintenance_UpdateUser"] != null)
                {
                    maintenance.DictionarySQlMaintenance_UpdateUser = row["DictionarySQlMaintenance_UpdateUser"].ToString();
                }
                if (row["DictionarySQlMaintenance_UpdateTime"] == null)
                {
                    return maintenance;
                }
                if (string.IsNullOrWhiteSpace(row["DictionarySQlMaintenance_UpdateTime"].ToString()))
                {
                    maintenance.DictionarySQlMaintenance_UpdateTime = null;
                    return maintenance;
                }
                maintenance.DictionarySQlMaintenance_UpdateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_UpdateTime"].ToString()));
            }
            return maintenance;
        }

        public List<Model_DictionarySQlMaintenance> GetDictionarySQlMaintenanceModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetDictionarySQlMaintenanceModelList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        private List<Model_DictionarySQlMaintenance> GetDictionarySQlMaintenanceModelList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
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
            List<Model_DictionarySQlMaintenance> list = new List<Model_DictionarySQlMaintenance>();
            Model_DictionarySQlMaintenance item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_DictionarySQlMaintenance();
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

        public List<Model_DictionarySQlMaintenance> GetDictionarySQlMaintenanceModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetDictionarySQlMaintenanceModelListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        private List<Model_DictionarySQlMaintenance> GetDictionarySQlMaintenanceModelListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
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
            List<Model_DictionarySQlMaintenance> list = new List<Model_DictionarySQlMaintenance>();
            Model_DictionarySQlMaintenance item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_DictionarySQlMaintenance();
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

        public bool IsOrNotExists(Model_DictionarySQlMaintenance model, int i)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from  DictionarySQlMaintenance");
            if (i == 0)
            {
                builder.AppendFormat(" where 1=1 and DictionarySQlMaintenance_ID<>'{0}' AND DictionarySQlMaintenance_Mark='{1}' ", model.DictionarySQlMaintenance_ID, model.DictionarySQlMaintenance_Mark);
            }
            else
            {
                builder.AppendFormat(" where 1=1 and DictionarySQlMaintenance_Mark='{0}'", model.DictionarySQlMaintenance_Mark);
            }
            return (int.Parse(DbHelperSQL.GetSingle(builder.ToString()).ToString()) > 0);
        }

        public int Update(Model_DictionarySQlMaintenance model)
        {
            return this.Update(null, model);
        }

        private int Update(DbTransaction tran, Model_DictionarySQlMaintenance model)
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

        private int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
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

        public int UpdateNew(Model_DictionarySQlMaintenance model)
        {
            return this.UpdateNew(null, model);
        }

        private int UpdateNew(DbTransaction tran, Model_DictionarySQlMaintenance model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append("  DictionarySQlMaintenance ");
            builder.Append(" SET ");
            builder.Append(" DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID,DictionarySQlMaintenance_Mark=@DictionarySQlMaintenance_Mark,DictionarySQlMaintenance_Name=@DictionarySQlMaintenance_Name,DictionarySQlMaintenance_Explanation=@DictionarySQlMaintenance_Explanation,DictionarySQlMaintenance_SQL=@DictionarySQlMaintenance_SQL,DictionarySQlMaintenance_CretateUser=@DictionarySQlMaintenance_CretateUser,DictionarySQlMaintenance_CreateTime=@DictionarySQlMaintenance_CreateTime,DictionarySQlMaintenance_UpdateUser=@DictionarySQlMaintenance_UpdateUser,DictionarySQlMaintenance_UpdateTime=@DictionarySQlMaintenance_UpdateTime ");
            builder.Append(" WHERE ");
            builder.Append(" DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.DictionarySQlMaintenance_ID, model.DictionarySQlMaintenance_Mark, model.DictionarySQlMaintenance_Name, model.DictionarySQlMaintenance_Explanation, model.DictionarySQlMaintenance_SQL, model.DictionarySQlMaintenance_CretateUser, model.DictionarySQlMaintenance_CreateTime, model.DictionarySQlMaintenance_UpdateUser, model.DictionarySQlMaintenance_UpdateTime });
        }

        private DatabaseSQLHelper CurrDB { get; set; }
    }
}

