namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysUserRoleRelation
    {
        public bool Add(Model_SysUserRoleRelation model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysUserRoleRelation(");
            builder.Append("SysUser_ID,SysRole_ID,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            builder.Append(" values (");
            builder.Append("@SysUser_ID,@SysRole_ID,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysRole_ID", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SysUser_ID;
            cmdParms[1].Value = model.SysRole_ID;
            cmdParms[2].Value = model.CreateTime;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.UpdateTime;
            cmdParms[5].Value = model.UpdateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysUserRoleRelation DataRowToModel(DataRow row)
        {
            Model_SysUserRoleRelation relation = new Model_SysUserRoleRelation();
            if (row != null)
            {
                if (row["SysUser_ID"] != null)
                {
                    relation.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["SysRole_ID"] != null)
                {
                    relation.SysRole_ID = row["SysRole_ID"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    relation.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    relation.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    relation.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
                if (row["UpdateUser"] != null)
                {
                    relation.UpdateUser = row["UpdateUser"].ToString();
                }
            }
            return relation;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysUserRoleRelation ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SysUser_ID,SysRole_ID,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" FROM SysUserRoleRelation ");
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
            builder.Append(" SysUser_ID,SysRole_ID,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" FROM SysUserRoleRelation ");
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
                builder.Append("order by T.SysUser_ID desc");
            }
            builder.Append(")AS Row, T.*  from SysUserRoleRelation T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysUserRoleRelation GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SysUser_ID,SysRole_ID,CreateTime,CreateUser,UpdateTime,UpdateUser from SysUserRoleRelation ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SysUserRoleRelation();
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
            builder.Append("select count(1) FROM SysUserRoleRelation ");
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

        public bool Update(Model_SysUserRoleRelation model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysUserRoleRelation set ");
            builder.Append("SysUser_ID=@SysUser_ID,");
            builder.Append("SysRole_ID=@SysRole_ID,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("UpdateTime=@UpdateTime,");
            builder.Append("UpdateUser=@UpdateUser");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysRole_ID", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SysUser_ID;
            cmdParms[1].Value = model.SysRole_ID;
            cmdParms[2].Value = model.CreateTime;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.UpdateTime;
            cmdParms[5].Value = model.UpdateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

