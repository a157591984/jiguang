namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class SysUser_For_CustomerInfoDAL
    {
        public SysUser_For_CustomerInfoDAL()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public SysUser_For_CustomerInfoDAL(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(SysUser_For_CustomerInfoModel model)
        {
            return this.Add(null, model);
        }

        internal int Add(DbTransaction tran, SysUser_For_CustomerInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append(" SysUser_For_CustomerInfo( ");
            builder.Append(" SysUser_ID,CustomerInfo_ID ");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @SysUser_ID,@CustomerInfo_ID ");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SysUser_ID, model.CustomerInfo_ID });
        }

        public bool AddSysUser_For_CustomerInfo(string sysUser_ID, string customerInfo_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO SysUser_For_CustomerInfo (SysUser_ID,CustomerInfo_ID) VALUES ('" + sysUser_ID + "','" + customerInfo_ID + "')");
            return (DbHelperSQL.ExecuteSqlTran(new List<string> { builder.ToString() }) > 0);
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DeleteByCondition(null, strCondition, param);
        }

        internal int DeleteByCondition(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM ");
            builder.Append(" SysUser_For_CustomerInfo ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, param);
        }

        public int DeleteByPK(string sysuser_id)
        {
            return this.DeleteByPK(null, sysuser_id);
        }

        internal int DeleteByPK(DbTransaction tran, string sysuser_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM");
            builder.Append(" SysUser_For_CustomerInfo ");
            builder.Append(" WHERE ");
            builder.Append(" SysUser_ID=@SysUser_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { sysuser_id });
        }

        public bool EditSysUser_For_CustomerInfo(string sysUser_ID, string customerInfo_ID)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            new StringBuilder();
            if ((customerInfo_ID != "") && (customerInfo_ID != null))
            {
                builder.AppendFormat("delete SysUser_For_CustomerInfo where customerInfo_ID='{0}'", customerInfo_ID);
            }
            int index = 0;
        Label_0089:;
            if (index < sysUser_ID.Split(new char[] { ',' }).Count<string>())
            {
                builder2.Append("INSERT INTO SysUser_For_CustomerInfo (SysUser_ID,CustomerInfo_ID) VALUES ('" + sysUser_ID.Split(new char[] { ',' })[index] + "','" + customerInfo_ID + "')");
                index++;
                goto Label_0089;
            }
            return (DbHelperSQL.ExecuteSqlTran(new List<string> { builder.ToString(), builder2.ToString() }) > 0);
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
            builder.Append(" SysUser_For_CustomerInfo ");
            if (!string.IsNullOrEmpty(conditionStr))
            {
                builder.Append(" WHERE ");
                builder.Append(conditionStr);
            }
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, paramValues)) > 0);
        }

        public bool ExistsByPK(string sysuser_id)
        {
            return this.ExistsByPK(null, sysuser_id);
        }

        internal bool ExistsByPK(DbTransaction tran, string sysuser_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" SysUser_For_CustomerInfo ");
            builder.Append(" WHERE ");
            builder.Append(" SysUser_ID=@SysUser_ID ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { sysuser_id })) > 0);
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
            builder.Append(" SysUser_For_CustomerInfo ");
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

        public int GetSysUser_For_CustomerInfoCount(string strCondition, params object[] param)
        {
            return this.GetSysUser_For_CustomerInfoCount(null, strCondition, param);
        }

        internal int GetSysUser_For_CustomerInfoCount(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" Count(1) ");
            builder.Append(" FROM ");
            builder.Append(" SysUser_For_CustomerInfo ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, param));
        }

        public SysUser_For_CustomerInfoModel GetSysUser_For_CustomerInfoModelByPK(string sysuser_id)
        {
            return this.GetSysUser_For_CustomerInfoModelByPK(null, sysuser_id);
        }

        internal SysUser_For_CustomerInfoModel GetSysUser_For_CustomerInfoModelByPK(DbTransaction tran, string sysuser_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" SysUser_For_CustomerInfo ");
            builder.Append(" WHERE ");
            builder.Append(" SysUser_ID=@SysUser_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { sysuser_id });
            SysUser_For_CustomerInfoModel model = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                model = new SysUser_For_CustomerInfoModel();
                if (row["SysUser_ID"] != null)
                {
                    model.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["CustomerInfo_ID"] != null)
                {
                    model.CustomerInfo_ID = row["CustomerInfo_ID"].ToString();
                }
            }
            return model;
        }

        public List<SysUser_For_CustomerInfoModel> GetSysUser_For_CustomerInfoModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetSysUser_For_CustomerInfoModelList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal List<SysUser_For_CustomerInfoModel> GetSysUser_For_CustomerInfoModelList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" SysUser_For_CustomerInfo ");
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
            List<SysUser_For_CustomerInfoModel> list = new List<SysUser_For_CustomerInfoModel>();
            SysUser_For_CustomerInfoModel item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new SysUser_For_CustomerInfoModel();
                if (row["SysUser_ID"] != null)
                {
                    item.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["CustomerInfo_ID"] != null)
                {
                    item.CustomerInfo_ID = row["CustomerInfo_ID"].ToString();
                }
                list.Add(item);
            }
            return list;
        }

        public List<SysUser_For_CustomerInfoModel> GetSysUser_For_CustomerInfoModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetSysUser_For_CustomerInfoModelListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        internal List<SysUser_For_CustomerInfoModel> GetSysUser_For_CustomerInfoModelListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
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
            builder.Append(string.Format(" SELECT (ROW_NUMBER() OVER(ORDER BY {0} {1})) as rownum,* FROM SysUser_For_CustomerInfo", orderColumn, orderType));
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
            List<SysUser_For_CustomerInfoModel> list = new List<SysUser_For_CustomerInfoModel>();
            SysUser_For_CustomerInfoModel item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new SysUser_For_CustomerInfoModel();
                if (row["SysUser_ID"] != null)
                {
                    item.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["CustomerInfo_ID"] != null)
                {
                    item.CustomerInfo_ID = row["CustomerInfo_ID"].ToString();
                }
                list.Add(item);
            }
            return list;
        }

        public int Update(SysUser_For_CustomerInfoModel model)
        {
            return this.Update(null, model);
        }

        internal int Update(DbTransaction tran, SysUser_For_CustomerInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SysUser_For_CustomerInfo ");
            builder.Append(" SET ");
            builder.Append(" SysUser_ID=@SysUser_ID,CustomerInfo_ID=@CustomerInfo_ID ");
            builder.Append(" WHERE ");
            builder.Append(" SysUser_ID=@SysUser_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SysUser_ID, model.CustomerInfo_ID });
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.Update(null, strUpdateColumns, strCondition, param);
        }

        internal int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SysUser_For_CustomerInfo ");
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

