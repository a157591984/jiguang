namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_F_User_GradeTerm
    {
        public bool Add(Model_F_User_GradeTerm model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into F_User_GradeTerm(");
            builder.Append("UserId,GradeTerm_ID)");
            builder.Append(" values (");
            builder.Append("@UserId,@GradeTerm_ID)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.GradeTerm_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_F_User_GradeTerm DataRowToModel(DataRow row)
        {
            Model_F_User_GradeTerm term = new Model_F_User_GradeTerm();
            if (row != null)
            {
                if (row["UserId"] != null)
                {
                    term.UserId = row["UserId"].ToString();
                }
                if (row["GradeTerm_ID"] != null)
                {
                    term.GradeTerm_ID = row["GradeTerm_ID"].ToString();
                }
            }
            return term;
        }

        public bool Delete(string UserId, string GradeTerm_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from F_User_GradeTerm ");
            builder.Append(" where UserId=@UserId and GradeTerm_ID=@GradeTerm_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserId;
            cmdParms[1].Value = GradeTerm_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(string UserId, string GradeTerm_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from F_User_GradeTerm");
            builder.Append(" where UserId=@UserId and GradeTerm_ID=@GradeTerm_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserId;
            cmdParms[1].Value = GradeTerm_ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserId,GradeTerm_ID ");
            builder.Append(" FROM F_User_GradeTerm ");
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
            builder.Append(" UserId,GradeTerm_ID ");
            builder.Append(" FROM F_User_GradeTerm ");
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
                builder.Append("order by T.GradeTerm_ID desc");
            }
            builder.Append(")AS Row, T.*  from F_User_GradeTerm T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_F_User_GradeTerm GetModel(string UserId, string GradeTerm_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserId,GradeTerm_ID from F_User_GradeTerm ");
            builder.Append(" where UserId=@UserId and GradeTerm_ID=@GradeTerm_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserId;
            cmdParms[1].Value = GradeTerm_ID;
            new Model_F_User_GradeTerm();
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
            builder.Append("select count(1) FROM F_User_GradeTerm ");
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

        public bool Update(Model_F_User_GradeTerm model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update F_User_GradeTerm set ");
            builder.Append("UserId=@UserId,");
            builder.Append("GradeTerm_ID=@GradeTerm_ID");
            builder.Append(" where UserId=@UserId and GradeTerm_ID=@GradeTerm_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.GradeTerm_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

