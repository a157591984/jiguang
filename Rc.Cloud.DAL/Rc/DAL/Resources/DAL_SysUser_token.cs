namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysUser_token
    {
        public bool Add(Model_SysUser_token model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysUser_token(");
            builder.Append("user_id,product_type,token,token_time,login_time,login_ip)");
            builder.Append(" values (");
            builder.Append("@user_id,@product_type,@token,@token_time,@login_time,@login_ip)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@user_id", SqlDbType.Char, 0x24), new SqlParameter("@product_type", SqlDbType.VarChar, 50), new SqlParameter("@token", SqlDbType.VarChar, 50), new SqlParameter("@token_time", SqlDbType.DateTime), new SqlParameter("@login_time", SqlDbType.DateTime), new SqlParameter("@login_ip", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = model.user_id;
            cmdParms[1].Value = model.product_type;
            cmdParms[2].Value = model.token;
            cmdParms[3].Value = model.token_time;
            cmdParms[4].Value = model.login_time;
            cmdParms[5].Value = model.login_ip;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysUser_token DataRowToModel(DataRow row)
        {
            Model_SysUser_token _token = new Model_SysUser_token();
            if (row != null)
            {
                if (row["user_id"] != null)
                {
                    _token.user_id = row["user_id"].ToString();
                }
                if (row["product_type"] != null)
                {
                    _token.product_type = row["product_type"].ToString();
                }
                if (row["token"] != null)
                {
                    _token.token = row["token"].ToString();
                }
                if ((row["token_time"] != null) && (row["token_time"].ToString() != ""))
                {
                    _token.token_time = new DateTime?(DateTime.Parse(row["token_time"].ToString()));
                }
                if ((row["login_time"] != null) && (row["login_time"].ToString() != ""))
                {
                    _token.login_time = new DateTime?(DateTime.Parse(row["login_time"].ToString()));
                }
                if (row["login_ip"] != null)
                {
                    _token.login_ip = row["login_ip"].ToString();
                }
            }
            return _token;
        }

        public bool Delete(string user_id, string product_type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysUser_token ");
            builder.Append(" where user_id=@user_id and product_type=@product_type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@user_id", SqlDbType.Char, 0x24), new SqlParameter("@product_type", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = user_id;
            cmdParms[1].Value = product_type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(string user_id, string product_type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SysUser_token");
            builder.Append(" where user_id=@user_id and product_type=@product_type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@user_id", SqlDbType.Char, 0x24), new SqlParameter("@product_type", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = user_id;
            cmdParms[1].Value = product_type;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select user_id,product_type,token,token_time,login_time,login_ip ");
            builder.Append(" FROM SysUser_token ");
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
            builder.Append(" user_id,product_type,token,token_time,login_time,login_ip ");
            builder.Append(" FROM SysUser_token ");
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
                builder.Append("order by T.product_type desc");
            }
            builder.Append(")AS Row, T.*  from SysUser_token T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysUser_token GetModel(string user_id, string product_type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 user_id,product_type,token,token_time,login_time,login_ip from SysUser_token ");
            builder.Append(" where user_id=@user_id and product_type=@product_type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@user_id", SqlDbType.Char, 0x24), new SqlParameter("@product_type", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = user_id;
            cmdParms[1].Value = product_type;
            new Model_SysUser_token();
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
            builder.Append("select count(1) FROM SysUser_token ");
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

        public bool Update(Model_SysUser_token model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysUser_token set ");
            builder.Append("token=@token,");
            builder.Append("token_time=@token_time,");
            builder.Append("login_time=@login_time,");
            builder.Append("login_ip=@login_ip");
            builder.Append(" where user_id=@user_id and product_type=@product_type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@token", SqlDbType.VarChar, 50), new SqlParameter("@token_time", SqlDbType.DateTime), new SqlParameter("@login_time", SqlDbType.DateTime), new SqlParameter("@login_ip", SqlDbType.VarChar, 50), new SqlParameter("@user_id", SqlDbType.Char, 0x24), new SqlParameter("@product_type", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = model.token;
            cmdParms[1].Value = model.token_time;
            cmdParms[2].Value = model.login_time;
            cmdParms[3].Value = model.login_ip;
            cmdParms[4].Value = model.user_id;
            cmdParms[5].Value = model.product_type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

