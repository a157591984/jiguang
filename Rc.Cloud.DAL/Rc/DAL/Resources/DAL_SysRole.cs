namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysRole
    {
        public bool Add(Model_SysRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysRole(");
            builder.Append("SysRole_ID,SysRole_Name,SysRole_Order,SysRole_Enable,SysRole_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            builder.Append(" values (");
            builder.Append("@SysRole_ID,@SysRole_Name,@SysRole_Order,@SysRole_Enable,@SysRole_Remark,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysRole_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysRole_Name", SqlDbType.NVarChar, 100), new SqlParameter("@SysRole_Order", SqlDbType.Int, 4), new SqlParameter("@SysRole_Enable", SqlDbType.Bit, 1), new SqlParameter("@SysRole_Remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24) };
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

        public Model_SysRole DataRowToModel(DataRow row)
        {
            Model_SysRole role = new Model_SysRole();
            if (row != null)
            {
                if (row["SysRole_ID"] != null)
                {
                    role.SysRole_ID = row["SysRole_ID"].ToString();
                }
                if (row["SysRole_Name"] != null)
                {
                    role.SysRole_Name = row["SysRole_Name"].ToString();
                }
                if ((row["SysRole_Order"] != null) && (row["SysRole_Order"].ToString() != ""))
                {
                    role.SysRole_Order = new int?(int.Parse(row["SysRole_Order"].ToString()));
                }
                if ((row["SysRole_Enable"] != null) && (row["SysRole_Enable"].ToString() != ""))
                {
                    if ((row["SysRole_Enable"].ToString() == "1") || (row["SysRole_Enable"].ToString().ToLower() == "true"))
                    {
                        role.SysRole_Enable = true;
                    }
                    else
                    {
                        role.SysRole_Enable = false;
                    }
                }
                if (row["SysRole_Remark"] != null)
                {
                    role.SysRole_Remark = row["SysRole_Remark"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    role.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    role.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    role.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
                if (row["UpdateUser"] != null)
                {
                    role.UpdateUser = row["UpdateUser"].ToString();
                }
            }
            return role;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysRole ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SysRole_ID,SysRole_Name,SysRole_Order,SysRole_Enable,SysRole_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" FROM SysRole ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" SysRole_ID,SysRole_Name,SysRole_Order,SysRole_Enable,SysRole_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" FROM SysRole ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.SysRole_ID desc");
            }
            builder.Append(")AS Row, T.*  from SysRole T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysRole GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SysRole_ID,SysRole_Name,SysRole_Order,SysRole_Enable,SysRole_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser from SysRole ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SysRole();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SysRole ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Model_SysRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysRole set ");
            builder.Append("SysRole_ID=@SysRole_ID,");
            builder.Append("SysRole_Name=@SysRole_Name,");
            builder.Append("SysRole_Order=@SysRole_Order,");
            builder.Append("SysRole_Enable=@SysRole_Enable,");
            builder.Append("SysRole_Remark=@SysRole_Remark,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("UpdateTime=@UpdateTime,");
            builder.Append("UpdateUser=@UpdateUser");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysRole_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysRole_Name", SqlDbType.NVarChar, 100), new SqlParameter("@SysRole_Order", SqlDbType.Int, 4), new SqlParameter("@SysRole_Enable", SqlDbType.Bit, 1), new SqlParameter("@SysRole_Remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24) };
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
    }
}

