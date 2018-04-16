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

    public class DAL_SysDepartment
    {
        public DAL_SysDepartment()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public DAL_SysDepartment(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(Model_SysDepartment model)
        {
            return this.Add(null, model);
        }

        internal int Add(DbTransaction tran, Model_SysDepartment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append(" SysDepartment( ");
            builder.Append(" SysDepartment_ID,SysDepartment_Name,SysDepartment_ParentID,SysDepartment_Tel,SysUser_ID,SysDepartment_Enable,SysDepartment_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @SysDepartment_ID,@SysDepartment_Name,@SysDepartment_ParentID,@SysDepartment_Tel,@SysUser_ID,@SysDepartment_Enable,@SysDepartment_Remark,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser ");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SysDepartment_ID, model.SysDepartment_Name, model.SysDepartment_ParentID, model.SysDepartment_Tel, model.SysUser_ID, model.SysDepartment_Enable, model.SysDepartment_Remark, model.CreateTime, model.CreateUser, model.UpdateTime, model.UpdateUser });
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DeleteByCondition(null, strCondition, param);
        }

        internal int DeleteByCondition(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM ");
            builder.Append(" SysDepartment ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, param);
        }

        public int DeleteByPK(string sysdepartment_id)
        {
            return this.DeleteByPK(null, sysdepartment_id);
        }

        internal int DeleteByPK(DbTransaction tran, string sysdepartment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM");
            builder.Append(" SysDepartment ");
            builder.Append(" WHERE ");
            builder.Append(" SysDepartment_ID=@SysDepartment_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { sysdepartment_id });
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
            builder.Append(" SysDepartment ");
            if (!string.IsNullOrEmpty(conditionStr))
            {
                builder.Append(" WHERE ");
                builder.Append(conditionStr);
            }
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, paramValues)) > 0);
        }

        public bool ExistsByLogic(string sysdepartment_id)
        {
            return this.ExistsByLogic(null, sysdepartment_id);
        }

        internal bool ExistsByLogic(DbTransaction tran, string sysdepartment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" SysDepartment ");
            builder.Append(" WHERE ");
            builder.Append(" SysDepartment_ID=@SysDepartment_ID  ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { sysdepartment_id })) > 0);
        }

        public bool ExistsByPK(string sysdepartment_id)
        {
            return this.ExistsByPK(null, sysdepartment_id);
        }

        internal bool ExistsByPK(DbTransaction tran, string sysdepartment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" SysDepartment ");
            builder.Append(" WHERE ");
            builder.Append(" SysDepartment_ID=@SysDepartment_ID ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { sysdepartment_id })) > 0);
        }

        public DataSet GetDataList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM SysDepartment");
            return this.CurrDB.ExecuteDataSet(builder.ToString(), null, new object[0]);
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
            builder.Append(" SysDepartment ");
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

        public DataSet GetListPaged(string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT row_number() over(order by SysDepartment.CreateTime) AS r_n,\r\n                     SysDepartment.SysDepartment_ID,SysDepartment_Name,SysDepartment_Enable,SysDepartment_Remark,SysUser.SysUser_Name \r\n                 FROM SysDepartment inner join SysUser on SysDepartment.SysUser_ID=SysUser.SysUser_ID\r\n                                        where 1=1 ");
            builder.Append(strWhere);
            return sys.GetRecordByPage(builder.ToString(), PageIndex, PageSize, out rCount, out pCount);
        }

        public Model_SysDepartment GetModel_SysDepartmentByLogic(string sysdepartment_id)
        {
            return this.GetModel_SysDepartmentByLogic(null, sysdepartment_id);
        }

        internal Model_SysDepartment GetModel_SysDepartmentByLogic(DbTransaction tran, string sysdepartment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" SysDepartment ");
            builder.Append(" WHERE ");
            builder.Append(" SysDepartment_ID=@SysDepartment_ID  ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { sysdepartment_id });
            Model_SysDepartment department = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                department = new Model_SysDepartment();
                if (row["SysDepartment_ID"] != null)
                {
                    department.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if (row["SysDepartment_Name"] != null)
                {
                    department.SysDepartment_Name = row["SysDepartment_Name"].ToString();
                }
                if (row["SysDepartment_ParentID"] != null)
                {
                    department.SysDepartment_ParentID = row["SysDepartment_ParentID"].ToString();
                }
                if (row["SysDepartment_Tel"] != null)
                {
                    department.SysDepartment_Tel = row["SysDepartment_Tel"].ToString();
                }
                if (row["SysUser_ID"] != null)
                {
                    department.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["SysDepartment_Enable"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SysDepartment_Enable"].ToString()))
                    {
                        department.SysDepartment_Enable = null;
                    }
                    else if ((row["SysDepartment_Enable"].ToString() == "0") || (row["SysDepartment_Enable"].ToString().ToLower() == "false"))
                    {
                        department.SysDepartment_Enable = false;
                    }
                    else if ((row["SysDepartment_Enable"].ToString() == "1") || (row["SysDepartment_Enable"].ToString().ToLower() == "true"))
                    {
                        department.SysDepartment_Enable = true;
                    }
                }
                if (row["SysDepartment_Remark"] != null)
                {
                    department.SysDepartment_Remark = row["SysDepartment_Remark"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        department.CreateTime = null;
                    }
                    else
                    {
                        department.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    department.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                    {
                        department.UpdateTime = null;
                    }
                    else
                    {
                        department.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                    }
                }
                if (row["UpdateUser"] != null)
                {
                    department.UpdateUser = row["UpdateUser"].ToString();
                }
            }
            return department;
        }

        public Model_SysDepartment GetModel_SysDepartmentByPK(string sysdepartment_id)
        {
            return this.GetModel_SysDepartmentByPK(null, sysdepartment_id);
        }

        internal Model_SysDepartment GetModel_SysDepartmentByPK(DbTransaction tran, string sysdepartment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" SysDepartment ");
            builder.Append(" WHERE ");
            builder.Append(" SysDepartment_ID=@SysDepartment_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { sysdepartment_id });
            Model_SysDepartment department = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                department = new Model_SysDepartment();
                if (row["SysDepartment_ID"] != null)
                {
                    department.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if (row["SysDepartment_Name"] != null)
                {
                    department.SysDepartment_Name = row["SysDepartment_Name"].ToString();
                }
                if (row["SysDepartment_ParentID"] != null)
                {
                    department.SysDepartment_ParentID = row["SysDepartment_ParentID"].ToString();
                }
                if (row["SysDepartment_Tel"] != null)
                {
                    department.SysDepartment_Tel = row["SysDepartment_Tel"].ToString();
                }
                if (row["SysUser_ID"] != null)
                {
                    department.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["SysDepartment_Enable"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SysDepartment_Enable"].ToString()))
                    {
                        department.SysDepartment_Enable = null;
                    }
                    else if ((row["SysDepartment_Enable"].ToString() == "0") || (row["SysDepartment_Enable"].ToString().ToLower() == "false"))
                    {
                        department.SysDepartment_Enable = false;
                    }
                    else if ((row["SysDepartment_Enable"].ToString() == "1") || (row["SysDepartment_Enable"].ToString().ToLower() == "true"))
                    {
                        department.SysDepartment_Enable = true;
                    }
                }
                if (row["SysDepartment_Remark"] != null)
                {
                    department.SysDepartment_Remark = row["SysDepartment_Remark"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        department.CreateTime = null;
                    }
                    else
                    {
                        department.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    department.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                    {
                        department.UpdateTime = null;
                    }
                    else
                    {
                        department.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                    }
                }
                if (row["UpdateUser"] != null)
                {
                    department.UpdateUser = row["UpdateUser"].ToString();
                }
            }
            return department;
        }

        public List<Model_SysDepartment> GetModel_SysDepartmentList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetModel_SysDepartmentList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SysDepartment> GetModel_SysDepartmentList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" SysDepartment ");
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
            List<Model_SysDepartment> list = new List<Model_SysDepartment>();
            Model_SysDepartment item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_SysDepartment();
                if (row["SysDepartment_ID"] != null)
                {
                    item.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if (row["SysDepartment_Name"] != null)
                {
                    item.SysDepartment_Name = row["SysDepartment_Name"].ToString();
                }
                if (row["SysDepartment_ParentID"] != null)
                {
                    item.SysDepartment_ParentID = row["SysDepartment_ParentID"].ToString();
                }
                if (row["SysDepartment_Tel"] != null)
                {
                    item.SysDepartment_Tel = row["SysDepartment_Tel"].ToString();
                }
                if (row["SysUser_ID"] != null)
                {
                    item.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["SysDepartment_Enable"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SysDepartment_Enable"].ToString()))
                    {
                        item.SysDepartment_Enable = null;
                    }
                    else if ((row["SysDepartment_Enable"].ToString() == "0") || (row["SysDepartment_Enable"].ToString().ToLower() == "false"))
                    {
                        item.SysDepartment_Enable = false;
                    }
                    else if ((row["SysDepartment_Enable"].ToString() == "1") || (row["SysDepartment_Enable"].ToString().ToLower() == "true"))
                    {
                        item.SysDepartment_Enable = true;
                    }
                }
                if (row["SysDepartment_Remark"] != null)
                {
                    item.SysDepartment_Remark = row["SysDepartment_Remark"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        item.CreateTime = null;
                    }
                    else
                    {
                        item.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    item.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                    {
                        item.UpdateTime = null;
                    }
                    else
                    {
                        item.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                    }
                }
                if (row["UpdateUser"] != null)
                {
                    item.UpdateUser = row["UpdateUser"].ToString();
                }
                list.Add(item);
            }
            return list;
        }

        public List<Model_SysDepartment> GetModel_SysDepartmentListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetModel_SysDepartmentListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SysDepartment> GetModel_SysDepartmentListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
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
            builder.Append(string.Format(" SELECT (ROW_NUMBER() OVER(ORDER BY {0} {1})) as rownum,* FROM SysDepartment", orderColumn, orderType));
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
            List<Model_SysDepartment> list = new List<Model_SysDepartment>();
            Model_SysDepartment item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_SysDepartment();
                if (row["SysDepartment_ID"] != null)
                {
                    item.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if (row["SysDepartment_Name"] != null)
                {
                    item.SysDepartment_Name = row["SysDepartment_Name"].ToString();
                }
                if (row["SysDepartment_ParentID"] != null)
                {
                    item.SysDepartment_ParentID = row["SysDepartment_ParentID"].ToString();
                }
                if (row["SysDepartment_Tel"] != null)
                {
                    item.SysDepartment_Tel = row["SysDepartment_Tel"].ToString();
                }
                if (row["SysUser_ID"] != null)
                {
                    item.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["SysDepartment_Enable"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SysDepartment_Enable"].ToString()))
                    {
                        item.SysDepartment_Enable = null;
                    }
                    else if ((row["SysDepartment_Enable"].ToString() == "0") || (row["SysDepartment_Enable"].ToString().ToLower() == "false"))
                    {
                        item.SysDepartment_Enable = false;
                    }
                    else if ((row["SysDepartment_Enable"].ToString() == "1") || (row["SysDepartment_Enable"].ToString().ToLower() == "true"))
                    {
                        item.SysDepartment_Enable = true;
                    }
                }
                if (row["SysDepartment_Remark"] != null)
                {
                    item.SysDepartment_Remark = row["SysDepartment_Remark"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        item.CreateTime = null;
                    }
                    else
                    {
                        item.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    item.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                    {
                        item.UpdateTime = null;
                    }
                    else
                    {
                        item.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                    }
                }
                if (row["UpdateUser"] != null)
                {
                    item.UpdateUser = row["UpdateUser"].ToString();
                }
                list.Add(item);
            }
            return list;
        }

        public int GetSysDepartmentCount(string strCondition, params object[] param)
        {
            return this.GetSysDepartmentCount(null, strCondition, param);
        }

        internal int GetSysDepartmentCount(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" Count(1) ");
            builder.Append(" FROM ");
            builder.Append(" SysDepartment ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, param));
        }

        public bool IsOrNotExists(Model_SysDepartment model, int i)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SysDepartment");
            if (i == 0)
            {
                builder.AppendFormat(" where 1=1 and SysDepartment_ID<>'{0}' AND SysDepartment_Name='{1}'", model.SysDepartment_ID, model.SysDepartment_Name);
            }
            else
            {
                builder.AppendFormat(" where 1=1 and SysDepartment_Name='{0}'", model.SysDepartment_Name);
            }
            return (int.Parse(DbHelperSQL.GetSingle(builder.ToString()).ToString()) > 0);
        }

        public int Update(Model_SysDepartment model)
        {
            return this.Update(null, model);
        }

        internal int Update(DbTransaction tran, Model_SysDepartment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SysDepartment ");
            builder.Append(" SET ");
            builder.Append(" SysDepartment_ID=@SysDepartment_ID,SysDepartment_Name=@SysDepartment_Name,SysDepartment_ParentID=@SysDepartment_ParentID,SysDepartment_Tel=@SysDepartment_Tel,SysUser_ID=@SysUser_ID,SysDepartment_Enable=@SysDepartment_Enable,SysDepartment_Remark=@SysDepartment_Remark,CreateTime=@CreateTime,CreateUser=@CreateUser,UpdateTime=@UpdateTime,UpdateUser=@UpdateUser ");
            builder.Append(" WHERE ");
            builder.Append(" SysDepartment_ID=@SysDepartment_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SysDepartment_ID, model.SysDepartment_Name, model.SysDepartment_ParentID, model.SysDepartment_Tel, model.SysUser_ID, model.SysDepartment_Enable, model.SysDepartment_Remark, model.CreateTime, model.CreateUser, model.UpdateTime, model.UpdateUser });
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.Update(null, strUpdateColumns, strCondition, param);
        }

        internal int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SysDepartment ");
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

