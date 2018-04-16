namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using Rc.Common.StrUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class SysUserDAL
    {
        public SysUserDAL()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public SysUserDAL(DatabaseSQLHelper db)
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
            builder.Append(" SysUser_ID,SysUser_Name,SysUser_LoginName,SysUser_PassWord,SysUser_Tel,SysDepartment_ID,SysUser_Enable,SysUser_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @SysUser_ID,@SysUser_Name,@SysUser_LoginName,@SysUser_PassWord,@SysUser_Tel,@SysDepartment_ID,@SysUser_Enable,@SysUser_Remark,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser ");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SysUser_ID, model.SysUser_Name, model.SysUser_LoginName, model.SysUser_PassWord, model.SysUser_Tel, model.SysDepartment_ID, model.SysUser_Enable, model.SysUser_Remark, model.CreateTime, model.CreateUser, model.UpdateTime, model.UpdateUser });
        }

        public int AddSysUser(Model_SysUser Model_SysUser, SysRoleModel SysRoleModel, string sType)
        {
            List<string> sQLStringList = new List<string>();
            string item = string.Empty;
            if (sType == "1")
            {
                string str2 = string.Empty;
                str2 = string.Format("insert into SysUser(SysUser_ID,SysUser_Name,SysUser_LoginName,SysUser_PassWord,SysUser_Tel,SysDepartment_ID,SysUser_Enable,SysUser_Remark,CreateTime,CreateUser) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}') ;", new object[] { Model_SysUser.SysUser_ID, Model_SysUser.SysUser_Name, Model_SysUser.SysUser_LoginName, Model_SysUser.SysUser_PassWord, Model_SysUser.SysUser_Tel, Model_SysUser.SysDepartment_ID, Model_SysUser.SysUser_Enable, Model_SysUser.SysUser_Remark, Model_SysUser.CreateTime, Model_SysUser.CreateUser });
                sQLStringList.Add(str2);
            }
            else
            {
                string str3 = string.Empty;
                string str4 = string.Empty;
                string str5 = string.Empty;
                str3 = string.Format("update SysUser set SysUser_Name='{0}',SysUser_LoginName='{1}',SysUser_PassWord='{2}',SysUser_Tel='{3}',SysUser_Enable='{4}',[UpdateTime]='{5}',[UpdateUser]='{6}' where sysUser_ID='{7}'", new object[] { Model_SysUser.SysUser_Name, Model_SysUser.SysUser_LoginName, Model_SysUser.SysUser_PassWord, Model_SysUser.SysUser_Tel, Model_SysUser.SysUser_Enable, Model_SysUser.UpdateTime, Model_SysUser.UpdateUser, Model_SysUser.SysUser_ID });
                str5 = string.Format(" delete from SysUserRoleRelation where SysUser_ID='{0}'", Model_SysUser.SysUser_ID);
                sQLStringList.Add(str3);
                sQLStringList.Add(str4);
                sQLStringList.Add(str5);
            }
            foreach (SysUserRoleRelationModel model in SysRoleModel.SysUserRoleList)
            {
                item = string.Format("insert into SysUserRoleRelation(SysUser_ID,SysRole_ID,CreateTime,CreateUser) values('{0}','{1}','{2}','{3}');", new object[] { model.SysUser_ID, model.SysRole_ID, model.CreateTime, model.CreateUser });
                sQLStringList.Add(item);
            }
            return DbHelperSQL.ExecuteSqlTran(sQLStringList);
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

        public bool DeleteSysUserRoleRelationByModuleID(string sysUser_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysUserRoleRelation ");
            builder.AppendFormat(" where sysUser_ID ='{0}'", sysUser_ID);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool EditSysUser_For_CustomerInfo(string sysUser_ID, string customerInfo_ID)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            new StringBuilder();
            if ((sysUser_ID != "") && (sysUser_ID != null))
            {
                builder.AppendFormat("delete SysUser_For_CustomerInfo where SysUser_ID='{0}'", sysUser_ID.Decrypt());
            }
            int index = 0;
        Label_0093:;
            if (index < customerInfo_ID.Split(new char[] { ',' }).Count<string>())
            {
                builder2.Append("INSERT INTO SysUser_For_CustomerInfo (SysUser_ID,CustomerInfo_ID) VALUES ('" + sysUser_ID.Decrypt() + "','" + customerInfo_ID.Split(new char[] { ',' })[index] + "')");
                index++;
                goto Label_0093;
            }
            return (DbHelperSQL.ExecuteSqlTran(new List<string> { builder.ToString(), builder2.ToString() }) > 0);
        }

        public bool EditSysUser_For_CustomerInfoTwo(string sysUser_ID, string customerInfo_ID)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            new StringBuilder();
            if ((customerInfo_ID != "") && (customerInfo_ID != null))
            {
                builder.AppendFormat("delete SysUser_For_CustomerInfo where customerInfo_ID='{0}'", customerInfo_ID.Decrypt());
            }
            int index = 0;
        Label_008E:;
            if (index < sysUser_ID.Split(new char[] { ',' }).Count<string>())
            {
                builder2.Append("INSERT INTO SysUser_For_CustomerInfo (SysUser_ID,CustomerInfo_ID) VALUES ('" + sysUser_ID.Split(new char[] { ',' })[index] + "','" + customerInfo_ID + "')");
                index++;
                goto Label_008E;
            }
            return (DbHelperSQL.ExecuteSqlTran(new List<string> { builder.ToString(), builder2.ToString() }) > 0);
        }

        public bool Exists(Model_SysUser model, int i)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SysUser");
            if (i == 0)
            {
                builder.AppendFormat(" where 1=1 and sysUser_ID<>'{0}' AND SysUser_LoginName='{1}'", model.SysUser_ID, model.SysUser_LoginName);
            }
            else
            {
                builder.AppendFormat(" where 1=1 and SysUser_LoginName='{0}'", model.SysUser_LoginName);
            }
            return (int.Parse(DbHelperSQL.GetSingle(builder.ToString()).ToString()) > 0);
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

        public DataSet GetCustomerInfo()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CustomerInfo_ID,CustomerInfo_NameCN from CustomerInfo");
            return DbHelperSQL.Query(builder.ToString());
        }

        public string GetCustomerInfo_NameCN(string SysUser_ID)
        {
            DataTable table = DbHelperSQL.Query(string.Format("SELECT CustomerInfo_NameCN FROM dbo.SysUser_For_CustomerInfo s JOIN CustomerInfo c ON s.CustomerInfo_ID=c.CustomerInfo_ID WHERE SysUser_ID='{0}'", SysUser_ID)).Tables[0];
            int count = table.Rows.Count;
            string str2 = null;
            for (int i = 0; i < count; i++)
            {
                str2 = str2 + table.Rows[i]["CustomerInfo_NameCN"] + ",";
            }
            if (!string.IsNullOrEmpty(str2))
            {
                str2 = str2.TrimEnd(new char[] { ',' });
            }
            return str2;
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

        public DataSet GetRoleList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from SysRole");
            return DbHelperSQL.Query(builder.ToString());
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

        public DataSet GetSysUser_For_CustomerInfo(string sysUser_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select s.CustomerInfo_ID,SysUser_ID,ct.CustomerInfo_NameCN from dbo.SysUser_For_CustomerInfo s JOIN dbo.CustomerInfo ct ON s.CustomerInfo_ID =ct.CustomerInfo_ID \r\n where SysUser_ID='" + sysUser_ID + "'");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysUser_For_CustomerInfoTwo(string CustomerInfo_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select s.CustomerInfo_ID,s.SysUser_ID, ct.SysUser_Name\r\n                            from dbo.SysUser_For_CustomerInfo s \r\n                            JOIN dbo.SysUser ct ON s.SysUser_ID =ct.SysUser_ID \r\n                            WHERE CustomerInfo_ID='" + CustomerInfo_ID + "'");
            return DbHelperSQL.Query(builder.ToString());
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

        public DataSet GetSysUserInfo(string sysUser_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT s.*,sd.SysDepartment_Name FROM dbo.SysUser s JOIN dbo.SysDepartment sd ON s.SysDepartment_ID=sd.SysDepartment_ID where 1=1 ");
            builder.Append(" and s.SysUser_ID=@SysUser_ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = sysUser_ID;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetSysUserList(string condition, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            string pSql = string.Empty;
            pSql = "select row_number() over(order by CreateTime ) AS r_n,* from (\r\n                   SELECT *  FROM SysUser\r\n                       ) as t where SysUser_Enable=1 and 1=1";
            if (condition != "")
            {
                pSql = pSql + condition;
            }
            return sys.GetRecordByPage(pSql, PageIndex, PageSize, out rCount, out pCount);
        }

        public Model_SysUser GetSysUserModelByPK(string sysuser_id)
        {
            return this.GetSysUserModelByPK(null, sysuser_id);
        }

        internal Model_SysUser GetSysUserModelByPK(DbTransaction tran, string sysuser_id)
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
                    if (string.IsNullOrWhiteSpace(row["SysUser_Enable"].ToString()))
                    {
                        user.SysUser_Enable = null;
                    }
                    else if ((row["SysUser_Enable"].ToString() == "0") || (row["SysUser_Enable"].ToString().ToLower() == "false"))
                    {
                        user.SysUser_Enable = false;
                    }
                    else if ((row["SysUser_Enable"].ToString() == "1") || (row["SysUser_Enable"].ToString().ToLower() == "true"))
                    {
                        user.SysUser_Enable = true;
                    }
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
                if (row["UpdateUser"] != null)
                {
                    user.UpdateUser = row["UpdateUser"].ToString();
                }
            }
            return user;
        }

        public List<Model_SysUser> GetSysUserModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetSysUserModelList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SysUser> GetSysUserModelList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
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
                if (row["SysDepartment_ID"] != null)
                {
                    item.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if (row["SysUser_Enable"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SysUser_Enable"].ToString()))
                    {
                        item.SysUser_Enable = null;
                    }
                    else if ((row["SysUser_Enable"].ToString() == "0") || (row["SysUser_Enable"].ToString().ToLower() == "false"))
                    {
                        item.SysUser_Enable = false;
                    }
                    else if ((row["SysUser_Enable"].ToString() == "1") || (row["SysUser_Enable"].ToString().ToLower() == "true"))
                    {
                        item.SysUser_Enable = true;
                    }
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
                    item.UpdateUser = row["UpdateUser"].ToString();
                }
                list.Add(item);
            }
            return list;
        }

        public List<Model_SysUser> GetSysUserModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetSysUserModelListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_SysUser> GetSysUserModelListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
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
                if (row["SysDepartment_ID"] != null)
                {
                    item.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if (row["SysUser_Enable"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["SysUser_Enable"].ToString()))
                    {
                        item.SysUser_Enable = null;
                    }
                    else if ((row["SysUser_Enable"].ToString() == "0") || (row["SysUser_Enable"].ToString().ToLower() == "false"))
                    {
                        item.SysUser_Enable = false;
                    }
                    else if ((row["SysUser_Enable"].ToString() == "1") || (row["SysUser_Enable"].ToString().ToLower() == "true"))
                    {
                        item.SysUser_Enable = true;
                    }
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
                    item.UpdateUser = row["UpdateUser"].ToString();
                }
                list.Add(item);
            }
            return list;
        }

        public DataSet GetUserRoleInfo(string sysUser_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select SysRole_ID,SysUser_ID FROM SysUserRoleRelation ");
            builder.Append("where SysUser_ID =@sysUser_ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@sysUser_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = sysUser_ID;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public bool MyInfoUpdate(Model_SysUser model)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE SysUser SET [SysUser_Name]='{0}',[SysUser_LoginName]='{1}',[SysUser_Tel]='{2}',[SysUser_Remark]='{3}',[UpdateTime]='{4}',[UpdateUser]='{5}' where [SysUser_ID]='{6}';", new object[] { model.SysUser_Name, model.SysUser_LoginName, model.SysUser_Tel, model.SysUser_Remark, model.UpdateTime, model.UpdateUser, model.SysUser_ID });
            if (DbHelperSQL.ExecuteSql(builder.ToString()) <= 0)
            {
                return false;
            }
            return true;
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
            builder.Append(" SysUser_ID=@SysUser_ID,SysUser_Name=@SysUser_Name,SysUser_LoginName=@SysUser_LoginName,SysUser_PassWord=@SysUser_PassWord,SysUser_Tel=@SysUser_Tel,SysDepartment_ID=@SysDepartment_ID,SysUser_Enable=@SysUser_Enable,SysUser_Remark=@SysUser_Remark,CreateTime=@CreateTime,CreateUser=@CreateUser,UpdateTime=@UpdateTime,UpdateUser=@UpdateUser ");
            builder.Append(" WHERE ");
            builder.Append(" SysUser_ID=@SysUser_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.SysUser_ID, model.SysUser_Name, model.SysUser_LoginName, model.SysUser_PassWord, model.SysUser_Tel, model.SysDepartment_ID, model.SysUser_Enable, model.SysUser_Remark, model.CreateTime, model.CreateUser, model.UpdateTime, model.UpdateUser });
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

