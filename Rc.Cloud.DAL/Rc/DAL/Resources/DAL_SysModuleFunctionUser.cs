namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysModuleFunctionUser
    {
        public bool Add(Model_SysModuleFunctionUser model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysModuleFunctionUser(");
            builder.Append("User_ID,MODULEID,FUNCTIONID,syscode)");
            builder.Append(" values (");
            builder.Append("@User_ID,@MODULEID,@FUNCTIONID,@syscode)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20), new SqlParameter("@syscode", SqlDbType.NChar, 5) };
            cmdParms[0].Value = model.User_ID;
            cmdParms[1].Value = model.MODULEID;
            cmdParms[2].Value = model.FUNCTIONID;
            cmdParms[3].Value = model.syscode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysModuleFunctionUser DataRowToModel(DataRow row)
        {
            Model_SysModuleFunctionUser user = new Model_SysModuleFunctionUser();
            if (row != null)
            {
                if (row["User_ID"] != null)
                {
                    user.User_ID = row["User_ID"].ToString();
                }
                if (row["MODULEID"] != null)
                {
                    user.MODULEID = row["MODULEID"].ToString();
                }
                if (row["FUNCTIONID"] != null)
                {
                    user.FUNCTIONID = row["FUNCTIONID"].ToString();
                }
                if (row["syscode"] != null)
                {
                    user.syscode = row["syscode"].ToString();
                }
            }
            return user;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysModuleFunctionUser ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select User_ID,MODULEID,FUNCTIONID,syscode ");
            builder.Append(" FROM SysModuleFunctionUser ");
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
            builder.Append(" User_ID,MODULEID,FUNCTIONID,syscode ");
            builder.Append(" FROM SysModuleFunctionUser ");
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
                builder.Append("order by T.User_ID desc");
            }
            builder.Append(")AS Row, T.*  from SysModuleFunctionUser T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysModuleFunctionUser GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 User_ID,MODULEID,FUNCTIONID,syscode from SysModuleFunctionUser ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SysModuleFunctionUser();
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
            builder.Append("select count(1) FROM SysModuleFunctionUser ");
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

        public bool Update(Model_SysModuleFunctionUser model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysModuleFunctionUser set ");
            builder.Append("User_ID=@User_ID,");
            builder.Append("MODULEID=@MODULEID,");
            builder.Append("FUNCTIONID=@FUNCTIONID,");
            builder.Append("syscode=@syscode");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20), new SqlParameter("@syscode", SqlDbType.NChar, 5) };
            cmdParms[0].Value = model.User_ID;
            cmdParms[1].Value = model.MODULEID;
            cmdParms[2].Value = model.FUNCTIONID;
            cmdParms[3].Value = model.syscode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

