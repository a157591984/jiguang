namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class DAL_SysConfigurationItems_Dict
    {
        public DAL_SysConfigurationItems_Dict()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public DAL_SysConfigurationItems_Dict(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(Model_SysConfigurationItems_Dict model)
        {
            return this.Add(null, model);
        }

        internal int Add(DbTransaction tran, Model_SysConfigurationItems_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append(" SysConfigurationItems_Dict( ");
            builder.Append(" SysConfigurationItems_Dict_ID,D_Name,D_ParentID,D_Value,D_DisplayMode,D_LinkType,D_SysModel,D_Code,D_Order,D_Type,D_Remark,D_CreateUser,D_CreateTime,D_ModifyUser,D_ModifyTime ");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @SysConfigurationItems_Dict_ID,@D_Name,@D_ParentID,@D_Value,@D_DisplayMode,@D_LinkType,@D_SysModel,@D_Code,@D_Order,@D_Type,@D_Remark,@D_CreateUser,@D_CreateTime,@D_ModifyUser,@D_ModifyTime ");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SysConfigurationItems_Dict_ID, model.D_Name, model.D_ParentID, model.D_Value, model.D_DisplayMode, model.D_LinkType, model.D_SysModel, model.D_Code, model.D_Order, model.D_Type, model.D_Remark, model.D_CreateUser, model.D_CreateTime, model.D_ModifyUser, model.D_ModifyTime });
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DeleteByCondition(null, strCondition, param);
        }

        internal int DeleteByCondition(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM ");
            builder.Append(" SysConfigurationItems_Dict ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, param);
        }

        public int DeleteByPK(string sysconfigurationitems_dict_id)
        {
            return this.DeleteByPK(null, sysconfigurationitems_dict_id);
        }

        internal int DeleteByPK(DbTransaction tran, string sysconfigurationitems_dict_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM");
            builder.Append(" SysConfigurationItems_Dict ");
            builder.Append(" WHERE ");
            builder.Append(" SysConfigurationItems_Dict_ID=@SysConfigurationItems_Dict_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { sysconfigurationitems_dict_id });
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
            builder.Append(" SysConfigurationItems_Dict ");
            if (!string.IsNullOrEmpty(conditionStr))
            {
                builder.Append(" WHERE ");
                builder.Append(conditionStr);
            }
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, paramValues)) > 0);
        }

        public bool ExistsByPK(string sysconfigurationitems_dict_id)
        {
            return this.ExistsByPK(null, sysconfigurationitems_dict_id);
        }

        internal bool ExistsByPK(DbTransaction tran, string sysconfigurationitems_dict_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" SysConfigurationItems_Dict ");
            builder.Append(" WHERE ");
            builder.Append(" SysConfigurationItems_Dict_ID=@SysConfigurationItems_Dict_ID ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { sysconfigurationitems_dict_id })) > 0);
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
            builder.Append(" SysConfigurationItems_Dict ");
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

        public DataSet GetSysConfigurationItems(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" (");
            builder.Append(" select SysCode AS SysConfigurationItems_Dict_ID ,SysName AS D_Name,'' as D_ParentID ,SysCode AS D_Code ,'' AS D_Remark,'' AS D_Order,'' AS D_Value");
            builder.Append(" from SysProduct");
            builder.Append(" UNION ALL");
            builder.Append(" SELECT SysConfigurationItems_Dict_ID,D_Name,D_ParentID,D_Code,D_Remark,D_Order,D_Value ");
            builder.Append(" FROM SysConfigurationItems_Dict");
            builder.Append(") SysConfigurationItems_Dict");
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
            return this.CurrDB.ExecuteDataSet(builder.ToString(), null, param);
        }

        public int GetSysConfigurationItems_DictCount(string strCondition, params object[] param)
        {
            return this.GetSysConfigurationItems_DictCount(null, strCondition, param);
        }

        internal int GetSysConfigurationItems_DictCount(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" Count(1) ");
            builder.Append(" FROM ");
            builder.Append(" SysConfigurationItems_Dict ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, param));
        }

        public Model_SysConfigurationItems_Dict GetSysConfigurationItems_DictModelByPK(string sysconfigurationitems_dict_id)
        {
            return this.GetSysConfigurationItems_DictModelByPK(null, sysconfigurationitems_dict_id);
        }

        internal Model_SysConfigurationItems_Dict GetSysConfigurationItems_DictModelByPK(DbTransaction tran, string sysconfigurationitems_dict_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" SysConfigurationItems_Dict ");
            builder.Append(" WHERE ");
            builder.Append(" SysConfigurationItems_Dict_ID=@SysConfigurationItems_Dict_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { sysconfigurationitems_dict_id });
            Model_SysConfigurationItems_Dict dict = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                dict = new Model_SysConfigurationItems_Dict();
                if (row["SysConfigurationItems_Dict_ID"] != null)
                {
                    dict.SysConfigurationItems_Dict_ID = row["SysConfigurationItems_Dict_ID"].ToString();
                }
                if (row["D_Name"] != null)
                {
                    dict.D_Name = row["D_Name"].ToString();
                }
                if (row["D_ParentID"] != null)
                {
                    dict.D_ParentID = row["D_ParentID"].ToString();
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
                if (row["D_DisplayMode"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_DisplayMode"].ToString()))
                    {
                        dict.D_DisplayMode = null;
                    }
                    else
                    {
                        dict.D_DisplayMode = new int?(int.Parse(row["D_DisplayMode"].ToString()));
                    }
                }
                if (row["D_LinkType"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_LinkType"].ToString()))
                    {
                        dict.D_LinkType = null;
                    }
                    else
                    {
                        dict.D_LinkType = new int?(int.Parse(row["D_LinkType"].ToString()));
                    }
                }
                if (row["D_SysModel"] != null)
                {
                    dict.D_SysModel = row["D_SysModel"].ToString();
                }
                if (row["D_Code"] != null)
                {
                    dict.D_Code = row["D_Code"].ToString();
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
                if (row["D_ModifyUser"] != null)
                {
                    dict.D_ModifyUser = row["D_ModifyUser"].ToString();
                }
                if (row["D_ModifyTime"] == null)
                {
                    return dict;
                }
                if (string.IsNullOrWhiteSpace(row["D_ModifyTime"].ToString()))
                {
                    dict.D_ModifyTime = null;
                    return dict;
                }
                dict.D_ModifyTime = new DateTime?(DateTime.Parse(row["D_ModifyTime"].ToString()));
            }
            return dict;
        }

        public List<Model_SysConfigurationItems_Dict> GetSysConfigurationItems_DictModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetSysConfigurationItems_DictModelList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SysConfigurationItems_Dict> GetSysConfigurationItems_DictModelList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" SysConfigurationItems_Dict ");
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
            List<Model_SysConfigurationItems_Dict> list = new List<Model_SysConfigurationItems_Dict>();
            Model_SysConfigurationItems_Dict item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_SysConfigurationItems_Dict();
                if (row["SysConfigurationItems_Dict_ID"] != null)
                {
                    item.SysConfigurationItems_Dict_ID = row["SysConfigurationItems_Dict_ID"].ToString();
                }
                if (row["D_Name"] != null)
                {
                    item.D_Name = row["D_Name"].ToString();
                }
                if (row["D_ParentID"] != null)
                {
                    item.D_ParentID = row["D_ParentID"].ToString();
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
                if (row["D_DisplayMode"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_DisplayMode"].ToString()))
                    {
                        item.D_DisplayMode = null;
                    }
                    else
                    {
                        item.D_DisplayMode = new int?(int.Parse(row["D_DisplayMode"].ToString()));
                    }
                }
                if (row["D_LinkType"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_LinkType"].ToString()))
                    {
                        item.D_LinkType = null;
                    }
                    else
                    {
                        item.D_LinkType = new int?(int.Parse(row["D_LinkType"].ToString()));
                    }
                }
                if (row["D_SysModel"] != null)
                {
                    item.D_SysModel = row["D_SysModel"].ToString();
                }
                if (row["D_Code"] != null)
                {
                    item.D_Code = row["D_Code"].ToString();
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
                if (row["D_ModifyUser"] != null)
                {
                    item.D_ModifyUser = row["D_ModifyUser"].ToString();
                }
                if (row["D_ModifyTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_ModifyTime"].ToString()))
                    {
                        item.D_ModifyTime = null;
                    }
                    else
                    {
                        item.D_ModifyTime = new DateTime?(DateTime.Parse(row["D_ModifyTime"].ToString()));
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public List<Model_SysConfigurationItems_Dict> GetSysConfigurationItems_DictModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetSysConfigurationItems_DictModelListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SysConfigurationItems_Dict> GetSysConfigurationItems_DictModelListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
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
            builder.Append(string.Format(" SELECT (ROW_NUMBER() OVER(ORDER BY {0} {1})) as rownum,* FROM SysConfigurationItems_Dict", orderColumn, orderType));
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
            List<Model_SysConfigurationItems_Dict> list = new List<Model_SysConfigurationItems_Dict>();
            Model_SysConfigurationItems_Dict item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_SysConfigurationItems_Dict();
                if (row["SysConfigurationItems_Dict_ID"] != null)
                {
                    item.SysConfigurationItems_Dict_ID = row["SysConfigurationItems_Dict_ID"].ToString();
                }
                if (row["D_Name"] != null)
                {
                    item.D_Name = row["D_Name"].ToString();
                }
                if (row["D_ParentID"] != null)
                {
                    item.D_ParentID = row["D_ParentID"].ToString();
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
                if (row["D_DisplayMode"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_DisplayMode"].ToString()))
                    {
                        item.D_DisplayMode = null;
                    }
                    else
                    {
                        item.D_DisplayMode = new int?(int.Parse(row["D_DisplayMode"].ToString()));
                    }
                }
                if (row["D_LinkType"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_LinkType"].ToString()))
                    {
                        item.D_LinkType = null;
                    }
                    else
                    {
                        item.D_LinkType = new int?(int.Parse(row["D_LinkType"].ToString()));
                    }
                }
                if (row["D_SysModel"] != null)
                {
                    item.D_SysModel = row["D_SysModel"].ToString();
                }
                if (row["D_Code"] != null)
                {
                    item.D_Code = row["D_Code"].ToString();
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
                if (row["D_ModifyUser"] != null)
                {
                    item.D_ModifyUser = row["D_ModifyUser"].ToString();
                }
                if (row["D_ModifyTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["D_ModifyTime"].ToString()))
                    {
                        item.D_ModifyTime = null;
                    }
                    else
                    {
                        item.D_ModifyTime = new DateTime?(DateTime.Parse(row["D_ModifyTime"].ToString()));
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public int Update(Model_SysConfigurationItems_Dict model)
        {
            return this.Update(null, model);
        }

        internal int Update(DbTransaction tran, Model_SysConfigurationItems_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SysConfigurationItems_Dict ");
            builder.Append(" SET ");
            builder.Append(" SysConfigurationItems_Dict_ID=@SysConfigurationItems_Dict_ID,D_Name=@D_Name,D_ParentID=@D_ParentID,D_Value=@D_Value,D_DisplayMode=@D_DisplayMode,D_LinkType=@D_LinkType,D_SysModel=@D_SysModel,D_Code=@D_Code,D_Order=@D_Order,D_Type=@D_Type,D_Remark=@D_Remark,D_CreateUser=@D_CreateUser,D_CreateTime=@D_CreateTime,D_ModifyUser=@D_ModifyUser,D_ModifyTime=@D_ModifyTime ");
            builder.Append(" WHERE ");
            builder.Append(" SysConfigurationItems_Dict_ID=@SysConfigurationItems_Dict_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SysConfigurationItems_Dict_ID, model.D_Name, model.D_ParentID, model.D_Value, model.D_DisplayMode, model.D_LinkType, model.D_SysModel, model.D_Code, model.D_Order, model.D_Type, model.D_Remark, model.D_CreateUser, model.D_CreateTime, model.D_ModifyUser, model.D_ModifyTime });
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.Update(null, strUpdateColumns, strCondition, param);
        }

        internal int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SysConfigurationItems_Dict ");
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

