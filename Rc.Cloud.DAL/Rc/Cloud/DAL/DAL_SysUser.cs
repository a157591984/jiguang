namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using Rc.Common.StrUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_SysUser
    {
        public DAL_SysUser()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public DAL_SysUser(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(Model_SysUser model)
        {
            return this.Add(null, model);
        }

        internal int Add(DbTransaction tran, Model_SysUser model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append(" SysUser( ");
            builder.Append(" SysUser_ID,SysUser_Name,SysUser_LoginName,SysUser_PassWord,SysUser_Tel,SysRole_ID,SysDepartment_ID,SysUser_Enable,SysUser_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @SysUser_ID,@SysUser_Name,@SysUser_LoginName,@SysUser_PassWord,@SysUser_Tel,@SysRole_ID,@SysDepartment_ID,@SysUser_Enable,@SysUser_Remark,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser ");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SysUser_ID, model.SysUser_Name, model.SysUser_LoginName, model.SysUser_PassWord, model.SysUser_Tel, model.SysRole_ID, model.SysDepartment_ID, model.SysUser_Enable, model.SysUser_Remark, model.CreateTime, model.CreateUser, model.UpdateTime, model.UpdateUser });
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DeleteByCondition(null, strCondition, param);
        }

        internal int DeleteByCondition(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM ");
            builder.Append(" SysUser ");
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
            builder.Append(" SysUser ");
            builder.Append(" WHERE ");
            builder.Append(" SysUser_ID=@SysUser_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { sysuser_id });
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
            builder.Append(" SysUser ");
            if (!string.IsNullOrEmpty(conditionStr))
            {
                builder.Append(" WHERE ");
                builder.Append(conditionStr);
            }
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, paramValues)) > 0);
        }

        public bool ExistsByLogic(string sysuser_id)
        {
            return this.ExistsByLogic(null, sysuser_id);
        }

        internal bool ExistsByLogic(DbTransaction tran, string sysuser_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" SysUser ");
            builder.Append(" WHERE ");
            builder.Append(" SysUser_ID=@SysUser_ID  ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { sysuser_id })) > 0);
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
            builder.Append(" SysUser ");
            builder.Append(" WHERE ");
            builder.Append(" SysUser_ID=@SysUser_ID ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { sysuser_id })) > 0);
        }

        public DataSet GetDataList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from SysUser");
            return DbHelperSQL.Query(builder.ToString());
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
            builder.Append(" SysUser ");
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

        public Model_SysUser GetModel_SysUserByLogic(string sysuser_id)
        {
            return this.GetModel_SysUserByLogic(null, sysuser_id);
        }

        internal Model_SysUser GetModel_SysUserByLogic(DbTransaction tran, string sysuser_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" SysUser ");
            builder.Append(" WHERE ");
            builder.Append(" SysUser_ID=@SysUser_ID  ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { sysuser_id });
            Model_SysUser user = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                user = new Model_SysUser();
                if (row["SysUser_ID"] != null)
                {
                    user.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["SysUser_Name"] != null)
                {
                    user.SysUser_Name = row["SysUser_Name"].ToString();
                }
                if (row["SysUser_LoginName"] != null)
                {
                    user.SysUser_LoginName = row["SysUser_LoginName"].ToString();
                }
                if (row["SysUser_PassWord"] != null)
                {
                    user.SysUser_PassWord = row["SysUser_PassWord"].ToString();
                }
                if (row["SysUser_Tel"] != null)
                {
                    user.SysUser_Tel = row["SysUser_Tel"].ToString();
                }
                if (row["SysDepartment_ID"] != null)
                {
                    user.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if (row["SysUser_Enable"] != null)
                {
                    user.SysUser_Enable = new bool?(Convert.ToBoolean(Convert.ToBoolean(row["SysUser_Enable"].ToString())));
                }
                if (row["SysUser_Remark"] != null)
                {
                    user.SysUser_Remark = row["SysUser_Remark"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        user.CreateTime = null;
                    }
                    else
                    {
                        user.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    user.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                    {
                        user.UpdateTime = null;
                    }
                    else
                    {
                        user.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                    }
                }
                if (row["UpdateUser"] == null)
                {
                    return user;
                }
                if (string.IsNullOrWhiteSpace(row["UpdateUser"].ToString()))
                {
                    user.UpdateUser = null;
                    return user;
                }
                user.UpdateUser = row["UpdateUser"].ToString();
            }
            return user;
        }

        public Model_SysUser GetModel_SysUserByPK(string sysuser_id)
        {
            return this.GetModel_SysUserByPK(null, sysuser_id);
        }

        internal Model_SysUser GetModel_SysUserByPK(DbTransaction tran, string sysuser_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" SysUser ");
            builder.Append(" WHERE ");
            builder.Append(" SysUser_ID=@SysUser_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { sysuser_id });
            Model_SysUser user = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                user = new Model_SysUser();
                if (row["SysUser_ID"] != null)
                {
                    user.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["SysUser_Name"] != null)
                {
                    user.SysUser_Name = row["SysUser_Name"].ToString();
                }
                if (row["SysUser_LoginName"] != null)
                {
                    user.SysUser_LoginName = row["SysUser_LoginName"].ToString();
                }
                if (row["SysUser_PassWord"] != null)
                {
                    user.SysUser_PassWord = row["SysUser_PassWord"].ToString();
                }
                if (row["SysUser_Tel"] != null)
                {
                    user.SysUser_Tel = row["SysUser_Tel"].ToString();
                }
                if (row["SysDepartment_ID"] != null)
                {
                    user.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if (row["SysUser_Enable"] != null)
                {
                    user.SysUser_Enable = new bool?(Convert.ToBoolean(row["SysUser_Enable"].ToString()));
                }
                if (row["SysUser_Remark"] != null)
                {
                    user.SysUser_Remark = row["SysUser_Remark"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        user.CreateTime = null;
                    }
                    else
                    {
                        user.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    user.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                    {
                        user.UpdateTime = null;
                    }
                    else
                    {
                        user.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                    }
                }
                if (row["UpdateUser"] == null)
                {
                    return user;
                }
                if (string.IsNullOrWhiteSpace(row["UpdateUser"].ToString()))
                {
                    user.UpdateUser = null;
                    return user;
                }
                user.UpdateUser = row["UpdateUser"].ToString();
            }
            return user;
        }

        public List<Model_SysUser> GetModel_SysUserList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetModel_SysUserList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SysUser> GetModel_SysUserList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" SysUser ");
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
            List<Model_SysUser> list = new List<Model_SysUser>();
            Model_SysUser item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_SysUser();
                if (row["SysUser_ID"] != null)
                {
                    item.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["SysUser_Name"] != null)
                {
                    item.SysUser_Name = row["SysUser_Name"].ToString();
                }
                if (row["SysUser_LoginName"] != null)
                {
                    item.SysUser_LoginName = row["SysUser_LoginName"].ToString();
                }
                if (row["SysUser_PassWord"] != null)
                {
                    item.SysUser_PassWord = row["SysUser_PassWord"].ToString();
                }
                if (row["SysUser_Tel"] != null)
                {
                    item.SysUser_Tel = row["SysUser_Tel"].ToString();
                }
                if (row["SysRole_ID"] != null)
                {
                    item.SysRole_ID = row["SysRole_ID"].ToString();
                }
                if (row["SysDepartment_ID"] != null)
                {
                    item.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if (row["SysUser_Enable"] != null)
                {
                    item.SysUser_Enable = new bool?(Convert.ToBoolean(row["SysUser_Enable"].ToString()));
                }
                if (row["SysUser_Remark"] != null)
                {
                    item.SysUser_Remark = row["SysUser_Remark"].ToString();
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
                    if (string.IsNullOrWhiteSpace(row["UpdateUser"].ToString()))
                    {
                        item.UpdateUser = null;
                    }
                    else
                    {
                        item.UpdateUser = row["UpdateUser"].ToString();
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public List<Model_SysUser> GetModel_SysUserListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetModel_SysUserListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SysUser> GetModel_SysUserListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
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
            builder.Append(string.Format(" SELECT (ROW_NUMBER() OVER(ORDER BY {0} {1})) as rownum,* FROM SysUser", orderColumn, orderType));
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
            List<Model_SysUser> list = new List<Model_SysUser>();
            Model_SysUser item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_SysUser();
                if (row["SysUser_ID"] != null)
                {
                    item.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["SysUser_Name"] != null)
                {
                    item.SysUser_Name = row["SysUser_Name"].ToString();
                }
                if (row["SysUser_LoginName"] != null)
                {
                    item.SysUser_LoginName = row["SysUser_LoginName"].ToString();
                }
                if (row["SysUser_PassWord"] != null)
                {
                    item.SysUser_PassWord = row["SysUser_PassWord"].ToString();
                }
                if (row["SysUser_Tel"] != null)
                {
                    item.SysUser_Tel = row["SysUser_Tel"].ToString();
                }
                if (row["SysRole_ID"] != null)
                {
                    item.SysRole_ID = row["SysRole_ID"].ToString();
                }
                if (row["SysDepartment_ID"] != null)
                {
                    item.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if (row["SysUser_Enable"] != null)
                {
                    item.SysUser_Enable = new bool?(Convert.ToBoolean(row["SysUser_Enable"].ToString()));
                }
                if (row["SysUser_Remark"] != null)
                {
                    item.SysUser_Remark = row["SysUser_Remark"].ToString();
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
                    if (string.IsNullOrWhiteSpace(row["UpdateUser"].ToString()))
                    {
                        item.UpdateUser = null;
                    }
                    else
                    {
                        item.UpdateUser = row["UpdateUser"].ToString();
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public string GetSysRole_Name(string SysUser_ID)
        {
            DataTable table = DbHelperSQL.Query(string.Format("SELECT SysRole_Name FROM SysUserRoleRelation sur JOIN SysRole s ON sur.SysRole_ID=s.SysRole_ID WHERE SysUser_ID='{0}'", SysUser_ID)).Tables[0];
            int count = table.Rows.Count;
            string str2 = null;
            for (int i = 0; i < count; i++)
            {
                str2 = str2 + table.Rows[i]["SysRole_Name"] + "；";
            }
            if (!string.IsNullOrEmpty(str2))
            {
                str2 = str2.TrimEnd();//new char[] { 0xff1b });
            }
            return str2;
        }

        public string GetSysUser_ID(string customerInfo_ID)
        {
            DataTable table = DbHelperSQL.Query(string.Format("SELECT SysUser_ID FROM dbo.SysUser_For_CustomerInfo WHERE CustomerInfo_ID='{0}'", customerInfo_ID)).Tables[0];
            int count = table.Rows.Count;
            string str2 = null;
            if (count > 0)
            {
                str2 = table.Rows[0]["SysUser_ID"].ToString();
            }
            return str2;
        }

        public int GetSysUserCount(string strCondition, params object[] param)
        {
            return this.GetSysUserCount(null, strCondition, param);
        }

        internal int GetSysUserCount(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" Count(1) ");
            builder.Append(" FROM ");
            builder.Append(" SysUser ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, param));
        }

        public DataSet GetSysUserListByName(string name, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            Model_VSysUserRole role = new Model_VSysUserRole();
            role = clsUtility.IsPageFlag() as Model_VSysUserRole;
            string pSql = string.Empty;
            pSql = "select row_number() over(order by CreateTime ) AS r_n,* from (\r\n                  select dbo.f_GetUserRoleNames(SysUser_ID) as SysRole_Names,* from SysUser\r\n                       ) as t where 1=1";
            if (name != "")
            {
                string str2 = pSql;
                pSql = str2 + " and (SysUser_Name like '" + name + "%' or SysUser_LoginName like '" + name + "%') ";
            }
            if ("1ebb1705-c073-41e8-b9ab-1ea594abd433" != role.SysUser_ID)
            {
                pSql = pSql + string.Format(" and sysUser_ID<>'{0}'", "1ebb1705-c073-41e8-b9ab-1ea594abd433") + string.Format(" and sysUser_ID<>'{0}'", "1961b4b3-bbc8-4658-b80c-c38b68054449");
            }
            return sys.GetRecordByPage(pSql, PageIndex, PageSize, out rCount, out pCount);
        }

        public int Update(Model_SysUser model)
        {
            return this.Update(null, model);
        }

        internal int Update(DbTransaction tran, Model_SysUser model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SysUser ");
            builder.Append(" SET ");
            builder.Append(" SysUser_ID=@SysUser_ID,SysUser_Name=@SysUser_Name,SysUser_LoginName=@SysUser_LoginName,SysUser_PassWord=@SysUser_PassWord,SysUser_Tel=@SysUser_Tel,SysRole_ID=@SysRole_ID,SysDepartment_ID=@SysDepartment_ID,SysUser_Enable=@SysUser_Enable,SysUser_Remark=@SysUser_Remark,CreateTime=@CreateTime,CreateUser=@CreateUser,UpdateTime=@UpdateTime,UpdateUser=@UpdateUser ");
            builder.Append(" WHERE ");
            builder.Append(" SysUser_ID=@SysUser_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SysUser_ID, model.SysUser_Name, model.SysUser_LoginName, model.SysUser_PassWord, model.SysUser_Tel, model.SysRole_ID, model.SysDepartment_ID, model.SysUser_Enable, model.SysUser_Remark, model.CreateTime, model.CreateUser, model.UpdateTime, model.UpdateUser });
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.Update(null, strUpdateColumns, strCondition, param);
        }

        internal int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" SysUser ");
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

