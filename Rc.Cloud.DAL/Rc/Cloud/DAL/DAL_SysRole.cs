namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_SysRole
    {
        public bool AddSysRole(Model_SysRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysRole(");
            builder.Append("SysRole_ID,SysRole_Name,SysRole_Order,SysRole_Enable,SysRole_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            builder.Append(" values (");
            builder.Append("@SysRole_ID,@SysRole_Name,@SysRole_Order,@SysRole_Enable,@SysRole_Remark,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysRole_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysRole_Name", SqlDbType.NVarChar), new SqlParameter("@SysRole_Order", SqlDbType.Int), new SqlParameter("@SysRole_Enable", SqlDbType.Bit), new SqlParameter("@SysRole_Remark", SqlDbType.NVarChar), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SysRole_ID;
            cmdParms[1].Value = model.SysRole_Name;
            cmdParms[2].Value = model.SysRole_Order;
            cmdParms[3].Value = model.SysRole_Enable;
            cmdParms[4].Value = model.SysRole_Remark;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.CreateUser;
            cmdParms[7].Value = model.UpdateTime;
            cmdParms[8].Value = model.UpdateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool ExistsSysRole(Model_SysRole mcd, string type)
        {
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            if (type == "1")
            {
                builder.AppendFormat("SELECT COUNT(*) FROM SysRole WHERE SysRole_Name='{0}'", mcd.SysRole_Name);
            }
            else if (type == "2")
            {
                builder.AppendFormat("SELECT COUNT(*) FROM SysRole WHERE SysRole_ID<>'{0}' and  SysRole_Name='{1}'", mcd.SysRole_ID, mcd.SysRole_Name);
            }
            if (int.Parse(DbHelperSQL.GetSingle(builder.ToString()).ToString()) > 0)
            {
                flag = true;
            }
            return flag;
        }

        public DataSet GetSysRoleList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from SysRole");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysRoleList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM SysRole ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysRoleList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" * ");
            builder.Append(" FROM SysRole ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysRoleList(Model_SysRoleParameter parameter, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            StringBuilder builder = new StringBuilder();
            string str = string.Empty;
            builder.Append("select row_number() over(order by SysRole_Name) AS r_n, * from SysRole where 1=1 ");
            if (parameter.MODEL_SysRole.SysRole_Name != "")
            {
                str = str + " and SysRole_Name like '%" + parameter.MODEL_SysRole.SysRole_Name + "%'";
            }
            builder.Append(str);
            return sys.GetRecordByPage(builder.ToString(), PageIndex, PageSize, out rCount, out pCount);
        }

        public Model_SysRole GetSysRoleModel(string SysRole_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from SysRole ");
            builder.Append(" where SysRole_ID=@SysRole_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysRole_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SysRole_ID;
            Model_SysRole role = new Model_SysRole();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["SysRole_ID"] != null) && (set.Tables[0].Rows[0]["SysRole_ID"].ToString() != ""))
            {
                role.SysRole_ID = set.Tables[0].Rows[0]["SysRole_ID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysRole_Name"] != null) && (set.Tables[0].Rows[0]["SysRole_Name"].ToString() != ""))
            {
                role.SysRole_Name = set.Tables[0].Rows[0]["SysRole_Name"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysRole_Order"] != null) && (set.Tables[0].Rows[0]["SysRole_Order"].ToString() != ""))
            {
                role.SysRole_Order = new int?(int.Parse(set.Tables[0].Rows[0]["SysRole_Order"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["SysRole_Enable"] != null) && (set.Tables[0].Rows[0]["SysRole_Enable"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["SysRole_Enable"].ToString() == "1") || (set.Tables[0].Rows[0]["SysRole_Enable"].ToString().ToLower() == "true"))
                {
                    role.SysRole_Enable = true;
                }
                else
                {
                    role.SysRole_Enable = false;
                }
            }
            if ((set.Tables[0].Rows[0]["SysRole_Remark"] != null) && (set.Tables[0].Rows[0]["SysRole_Remark"].ToString() != ""))
            {
                role.SysRole_Remark = set.Tables[0].Rows[0]["SysRole_Remark"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CreateTime"] != null) && (set.Tables[0].Rows[0]["CreateTime"].ToString() != ""))
            {
                role.CreateTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["CreateTime"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreateUser"] != null) && (set.Tables[0].Rows[0]["CreateUser"].ToString() != ""))
            {
                role.CreateUser = set.Tables[0].Rows[0]["CreateUser"].ToString();
            }
            if ((set.Tables[0].Rows[0]["UpdateTime"] != null) && (set.Tables[0].Rows[0]["UpdateTime"].ToString() != ""))
            {
                role.UpdateTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["UpdateTime"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["UpdateUser"] != null) && (set.Tables[0].Rows[0]["UpdateUser"].ToString() != ""))
            {
                role.UpdateUser = set.Tables[0].Rows[0]["UpdateUser"].ToString();
            }
            return role;
        }

        public bool UpdateSysRoleByID(Model_SysRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysRole set ");
            builder.Append("SysRole_Name=@SysRole_Name,");
            builder.Append("SysRole_Order=@SysRole_Order,");
            builder.Append("SysRole_Enable=@SysRole_Enable,");
            builder.Append("SysRole_Remark=@SysRole_Remark,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("UpdateTime=@UpdateTime,");
            builder.Append("UpdateUser=@UpdateUser ");
            builder.Append(" where SysRole_ID=@SysRole_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysRole_Name", SqlDbType.NVarChar), new SqlParameter("@SysRole_Order", SqlDbType.Int, 4), new SqlParameter("@SysRole_Enable", SqlDbType.Bit, 1), new SqlParameter("@SysRole_Remark", SqlDbType.NVarChar), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@SysRole_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SysRole_Name;
            cmdParms[1].Value = model.SysRole_Order;
            cmdParms[2].Value = model.SysRole_Enable;
            cmdParms[3].Value = model.SysRole_Remark;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.UpdateTime;
            cmdParms[7].Value = model.UpdateUser;
            cmdParms[8].Value = model.SysRole_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

