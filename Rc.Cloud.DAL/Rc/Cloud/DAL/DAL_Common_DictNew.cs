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

    public class DAL_Common_DictNew
    {
        public DAL_Common_DictNew()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public DAL_Common_DictNew(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(Model_Common_Dict model)
        {
            return this.Add(null, model);
        }

        private int Add(DbTransaction tran, Model_Common_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append("  Common_Dict( ");
            builder.Append(" Common_Dict_ID,D_Name,D_PartentID,D_Value,D_Code,D_Level,D_Order,D_Type,D_Remark,D_CreateUser,D_CreateTime,D_UpdateUser,D_UpdateTime ");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @Common_Dict_ID,@D_Name,@D_PartentID,@D_Value,@D_Code,@D_Level,@D_Order,@D_Type,@D_Remark,@D_CreateUser,@D_CreateTime,@D_UpdateUser,@D_UpdateTime ");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.Common_Dict_ID, model.D_Name, model.D_PartentID, model.D_Value, model.D_Code, model.D_Level, model.D_Order, model.D_Type, model.D_Remark, model.D_CreateUser, model.D_CreateTime, model.D_UpdateUser, model.D_UpdateTime });
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DeleteByCondition(null, strCondition, param);
        }

        private int DeleteByCondition(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM ");
            builder.Append("  Common_Dict ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, param);
        }

        public int DeleteByPK(string common_dict_id)
        {
            return this.DeleteByPK(null, common_dict_id);
        }

        private int DeleteByPK(DbTransaction tran, string common_dict_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM");
            builder.Append("  Common_Dict ");
            builder.Append(" WHERE ");
            builder.Append(" Common_Dict_ID=@Common_Dict_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { common_dict_id });
        }

        public bool Exists(Model_Common_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from  Common_Dict");
            builder.AppendFormat(" where Common_Dict_ID<>'{0}'and D_Name='{1}' and D_Type='{2}' ", model.Common_Dict_ID, model.D_Name, model.D_Type);
            return (int.Parse(DbHelperSQL.GetSingle(builder.ToString()).ToString()) > 0);
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
            builder.Append("  Common_Dict ");
            if (!string.IsNullOrEmpty(conditionStr))
            {
                builder.Append(" WHERE ");
                builder.Append(conditionStr);
            }
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, paramValues)) > 0);
        }

        public bool ExistsByPK(string common_dict_id)
        {
            return this.ExistsByPK(null, common_dict_id);
        }

        private bool ExistsByPK(DbTransaction tran, string common_dict_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append("  Common_Dict ");
            builder.Append(" WHERE ");
            builder.Append(" Common_Dict_ID=@Common_Dict_ID ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { common_dict_id })) > 0);
        }

        public int GetCommon_DictCount(string strCondition, params object[] param)
        {
            return this.GetCommon_DictCount(null, strCondition, param);
        }

        private int GetCommon_DictCount(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" Count(1) ");
            builder.Append(" FROM ");
            builder.Append("  Common_Dict ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, param));
        }

        public DataSet GetCommon_DictList(string condition, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            string pSql = string.Empty;
            pSql = "select row_number() over(order by D_Type,D_Order desc,D_Name) AS r_n,* from (\r\n                  select * from  Common_Dict\r\n                       ) as t where 1=1 and D_Type!=0";
            if (condition != "")
            {
                pSql = pSql + condition;
            }
            return sys.GetRecordByPage(pSql, PageIndex, PageSize, out rCount, out pCount);
        }

        public Model_Common_Dict GetCommon_DictModelByPK(string common_dict_id)
        {
            return this.GetCommon_DictModelByPK(null, common_dict_id);
        }

        private Model_Common_Dict GetCommon_DictModelByPK(DbTransaction tran, string common_dict_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append("  Common_Dict ");
            builder.Append(" WHERE ");
            builder.Append(" Common_Dict_ID=@Common_Dict_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { common_dict_id });
            Model_Common_Dict dict = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                dict = new Model_Common_Dict();
                if (row["Common_Dict_ID"] != null)
                {
                    dict.Common_Dict_ID = row["Common_Dict_ID"].ToString();
                }
                if (row["D_Name"] != null)
                {
                    dict.D_Name = row["D_Name"].ToString();
                }
                if (row["D_PartentID"] != null)
                {
                    dict.D_PartentID = row["D_PartentID"].ToString();
                }
                if (row["D_Value"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Value"].ToString()))
                    {
                        dict.D_Value = null;
                    }
                    else
                    {
                        dict.D_Value = new int?(int.Parse(row["D_Value"].ToString()));
                    }
                }
                if (row["D_Code"] != null)
                {
                    dict.D_Code = row["D_Code"].ToString();
                }
                if (row["D_Level"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Level"].ToString()))
                    {
                        dict.D_Level = null;
                    }
                    else
                    {
                        dict.D_Level = new int?(int.Parse(row["D_Level"].ToString()));
                    }
                }
                if (row["D_Order"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Order"].ToString()))
                    {
                        dict.D_Order = null;
                    }
                    else
                    {
                        dict.D_Order = new int?(int.Parse(row["D_Order"].ToString()));
                    }
                }
                if (row["D_Type"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Type"].ToString()))
                    {
                        dict.D_Type = null;
                    }
                    else
                    {
                        dict.D_Type = new int?(int.Parse(row["D_Type"].ToString()));
                    }
                }
                if (row["D_Remark"] != null)
                {
                    dict.D_Remark = row["D_Remark"].ToString();
                }
                if (row["D_CreateUser"] != null)
                {
                    dict.D_CreateUser = row["D_CreateUser"].ToString();
                }
                if (row["D_CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_CreateTime"].ToString()))
                    {
                        dict.D_CreateTime = null;
                    }
                    else
                    {
                        dict.D_CreateTime = new DateTime?(DateTime.Parse(row["D_CreateTime"].ToString()));
                    }
                }
                if (row["D_UpdateUser"] != null)
                {
                    dict.D_UpdateUser = row["D_UpdateUser"].ToString();
                }
                if (row["D_UpdateTime"] == null)
                {
                    return dict;
                }
                if (string.IsNullOrWhiteSpace(row["D_UpdateTime"].ToString()))
                {
                    dict.D_UpdateTime = null;
                    return dict;
                }
                dict.D_UpdateTime = new DateTime?(DateTime.Parse(row["D_UpdateTime"].ToString()));
            }
            return dict;
        }

        public List<Model_Common_Dict> GetCommon_DictModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetCommon_DictModelList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        private List<Model_Common_Dict> GetCommon_DictModelList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append("  Common_Dict ");
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
            List<Model_Common_Dict> list = new List<Model_Common_Dict>();
            Model_Common_Dict item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_Common_Dict();
                if (row["Common_Dict_ID"] != null)
                {
                    item.Common_Dict_ID = row["Common_Dict_ID"].ToString();
                }
                if (row["D_Name"] != null)
                {
                    item.D_Name = row["D_Name"].ToString();
                }
                if (row["D_PartentID"] != null)
                {
                    item.D_PartentID = row["D_PartentID"].ToString();
                }
                if (row["D_Value"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Value"].ToString()))
                    {
                        item.D_Value = null;
                    }
                    else
                    {
                        item.D_Value = new int?(int.Parse(row["D_Value"].ToString()));
                    }
                }
                if (row["D_Code"] != null)
                {
                    item.D_Code = row["D_Code"].ToString();
                }
                if (row["D_Level"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Level"].ToString()))
                    {
                        item.D_Level = null;
                    }
                    else
                    {
                        item.D_Level = new int?(int.Parse(row["D_Level"].ToString()));
                    }
                }
                if (row["D_Order"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Order"].ToString()))
                    {
                        item.D_Order = null;
                    }
                    else
                    {
                        item.D_Order = new int?(int.Parse(row["D_Order"].ToString()));
                    }
                }
                if (row["D_Type"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Type"].ToString()))
                    {
                        item.D_Type = null;
                    }
                    else
                    {
                        item.D_Type = new int?(int.Parse(row["D_Type"].ToString()));
                    }
                }
                if (row["D_Remark"] != null)
                {
                    item.D_Remark = row["D_Remark"].ToString();
                }
                if (row["D_CreateUser"] != null)
                {
                    item.D_CreateUser = row["D_CreateUser"].ToString();
                }
                if (row["D_CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_CreateTime"].ToString()))
                    {
                        item.D_CreateTime = null;
                    }
                    else
                    {
                        item.D_CreateTime = new DateTime?(DateTime.Parse(row["D_CreateTime"].ToString()));
                    }
                }
                if (row["D_UpdateUser"] != null)
                {
                    item.D_UpdateUser = row["D_UpdateUser"].ToString();
                }
                if (row["D_UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_UpdateTime"].ToString()))
                    {
                        item.D_UpdateTime = null;
                    }
                    else
                    {
                        item.D_UpdateTime = new DateTime?(DateTime.Parse(row["D_UpdateTime"].ToString()));
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public List<Model_Common_Dict> GetCommon_DictModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetCommon_DictModelListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        private List<Model_Common_Dict> GetCommon_DictModelListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
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
            builder.Append(string.Format(" SELECT (ROW_NUMBER() OVER(ORDER BY {0} {1})) as rownum,* FROM  Common_Dict", orderColumn, orderType));
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
            List<Model_Common_Dict> list = new List<Model_Common_Dict>();
            Model_Common_Dict item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_Common_Dict();
                if (row["Common_Dict_ID"] != null)
                {
                    item.Common_Dict_ID = row["Common_Dict_ID"].ToString();
                }
                if (row["D_Name"] != null)
                {
                    item.D_Name = row["D_Name"].ToString();
                }
                if (row["D_PartentID"] != null)
                {
                    item.D_PartentID = row["D_PartentID"].ToString();
                }
                if (row["D_Value"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Value"].ToString()))
                    {
                        item.D_Value = null;
                    }
                    else
                    {
                        item.D_Value = new int?(int.Parse(row["D_Value"].ToString()));
                    }
                }
                if (row["D_Code"] != null)
                {
                    item.D_Code = row["D_Code"].ToString();
                }
                if (row["D_Level"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Level"].ToString()))
                    {
                        item.D_Level = null;
                    }
                    else
                    {
                        item.D_Level = new int?(int.Parse(row["D_Level"].ToString()));
                    }
                }
                if (row["D_Order"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Order"].ToString()))
                    {
                        item.D_Order = null;
                    }
                    else
                    {
                        item.D_Order = new int?(int.Parse(row["D_Order"].ToString()));
                    }
                }
                if (row["D_Type"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_Type"].ToString()))
                    {
                        item.D_Type = null;
                    }
                    else
                    {
                        item.D_Type = new int?(int.Parse(row["D_Type"].ToString()));
                    }
                }
                if (row["D_Remark"] != null)
                {
                    item.D_Remark = row["D_Remark"].ToString();
                }
                if (row["D_CreateUser"] != null)
                {
                    item.D_CreateUser = row["D_CreateUser"].ToString();
                }
                if (row["D_CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_CreateTime"].ToString()))
                    {
                        item.D_CreateTime = null;
                    }
                    else
                    {
                        item.D_CreateTime = new DateTime?(DateTime.Parse(row["D_CreateTime"].ToString()));
                    }
                }
                if (row["D_UpdateUser"] != null)
                {
                    item.D_UpdateUser = row["D_UpdateUser"].ToString();
                }
                if (row["D_UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_UpdateTime"].ToString()))
                    {
                        item.D_UpdateTime = null;
                    }
                    else
                    {
                        item.D_UpdateTime = new DateTime?(DateTime.Parse(row["D_UpdateTime"].ToString()));
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public string GetD_Name(string D_Type)
        {
            StringBuilder builder = new StringBuilder();
            string str = string.Empty;
            builder.Append("select D_Name from  Common_Dict where D_Value='" + D_Type + "'");
            DataTable table = DbHelperSQL.Query(builder.ToString()).Tables[0];
            if (table.Rows.Count > 0)
            {
                str = str + table.Rows[0]["D_Name"].ToString();
            }
            return str;
        }

        public DataSet GetD_Remark(int D_Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select DISTINCT D_Remark from  Common_Dict ");
            builder.AppendFormat("where D_Type={0}", D_Type);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetD_Type()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select DISTINCT D_type,D_Remark from  Common_Dict ");
            return DbHelperSQL.Query(builder.ToString());
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
            builder.Append("  Common_Dict ");
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

        public DataSet GetTheTypeMaxOrder(int D_Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT MAX(D_Order)+1  FROM  Common_Dict");
            builder.AppendFormat(" WHERE D_Type='{0}'", D_Type);
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool SysCommon_DictExists(Model_Common_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from  Common_Dict");
            builder.AppendFormat(" where D_Name='{0}' and D_Type='{1}'", model.D_Name, model.D_Type);
            return (int.Parse(DbHelperSQL.GetSingle(builder.ToString()).ToString()) > 0);
        }

        public int Update(Model_Common_Dict model)
        {
            return this.Update(null, model);
        }

        private int Update(DbTransaction tran, Model_Common_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append("  Common_Dict ");
            builder.Append(" SET ");
            builder.Append(" Common_Dict_ID=@Common_Dict_ID,D_Name=@D_Name,D_PartentID=@D_PartentID,D_Value=@D_Value,D_Code=@D_Code,D_Level=@D_Level,D_Order=@D_Order,D_Type=@D_Type,D_Remark=@D_Remark,D_CreateUser=@D_CreateUser,D_CreateTime=@D_CreateTime,D_UpdateUser=@D_UpdateUser,D_UpdateTime=@D_UpdateTime ");
            builder.Append(" WHERE ");
            builder.Append(" Common_Dict_ID=@Common_Dict_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.Common_Dict_ID, model.D_Name, model.D_PartentID, model.D_Value, model.D_Code, model.D_Level, model.D_Order, model.D_Type, model.D_Remark, model.D_CreateUser, model.D_CreateTime, model.D_UpdateUser, model.D_UpdateTime });
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.Update(null, strUpdateColumns, strCondition, param);
        }

        private int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append("  Common_Dict ");
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

