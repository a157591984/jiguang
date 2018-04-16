namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysModuleFunctionRole
    {
        public bool Add(Model_SysModuleFunctionRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysModuleFunctionRole(");
            builder.Append("SysRole_ID,MODULEID,FUNCTIONID,syscode)");
            builder.Append(" values (");
            builder.Append("@SysRole_ID,@MODULEID,@FUNCTIONID,@syscode)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysRole_ID", SqlDbType.Char, 0x24), new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20), new SqlParameter("@syscode", SqlDbType.NChar, 5) };
            cmdParms[0].Value = model.SysRole_ID;
            cmdParms[1].Value = model.MODULEID;
            cmdParms[2].Value = model.FUNCTIONID;
            cmdParms[3].Value = model.syscode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysModuleFunctionRole DataRowToModel(DataRow row)
        {
            Model_SysModuleFunctionRole role = new Model_SysModuleFunctionRole();
            if (row != null)
            {
                if (row["SysRole_ID"] != null)
                {
                    role.SysRole_ID = row["SysRole_ID"].ToString();
                }
                if (row["MODULEID"] != null)
                {
                    role.MODULEID = row["MODULEID"].ToString();
                }
                if (row["FUNCTIONID"] != null)
                {
                    role.FUNCTIONID = row["FUNCTIONID"].ToString();
                }
                if (row["syscode"] != null)
                {
                    role.syscode = row["syscode"].ToString();
                }
            }
            return role;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysModuleFunctionRole ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SysRole_ID,MODULEID,FUNCTIONID,syscode ");
            builder.Append(" FROM SysModuleFunctionRole ");
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
            builder.Append(" SysRole_ID,MODULEID,FUNCTIONID,syscode ");
            builder.Append(" FROM SysModuleFunctionRole ");
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
            builder.Append(")AS Row, T.*  from SysModuleFunctionRole T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysModuleFunctionRole GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SysRole_ID,MODULEID,FUNCTIONID,syscode from SysModuleFunctionRole ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SysModuleFunctionRole();
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
            builder.Append("select count(1) FROM SysModuleFunctionRole ");
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

        public bool Update(Model_SysModuleFunctionRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysModuleFunctionRole set ");
            builder.Append("SysRole_ID=@SysRole_ID,");
            builder.Append("MODULEID=@MODULEID,");
            builder.Append("FUNCTIONID=@FUNCTIONID,");
            builder.Append("syscode=@syscode");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysRole_ID", SqlDbType.Char, 0x24), new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20), new SqlParameter("@syscode", SqlDbType.NChar, 5) };
            cmdParms[0].Value = model.SysRole_ID;
            cmdParms[1].Value = model.MODULEID;
            cmdParms[2].Value = model.FUNCTIONID;
            cmdParms[3].Value = model.syscode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

