namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysModuleFunction
    {
        public bool Add(Model_SysModuleFunction model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysModuleFunction(");
            builder.Append("ModuleID,FunctionId,IsDefault,syscode)");
            builder.Append(" values (");
            builder.Append("@ModuleID,@FunctionId,@IsDefault,@syscode)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModuleID", SqlDbType.VarChar, 20), new SqlParameter("@FunctionId", SqlDbType.VarChar, 20), new SqlParameter("@IsDefault", SqlDbType.Int, 4), new SqlParameter("@syscode", SqlDbType.NChar, 5) };
            cmdParms[0].Value = model.ModuleID;
            cmdParms[1].Value = model.FunctionId;
            cmdParms[2].Value = model.IsDefault;
            cmdParms[3].Value = model.syscode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysModuleFunction DataRowToModel(DataRow row)
        {
            Model_SysModuleFunction function = new Model_SysModuleFunction();
            if (row != null)
            {
                if (row["ModuleID"] != null)
                {
                    function.ModuleID = row["ModuleID"].ToString();
                }
                if (row["FunctionId"] != null)
                {
                    function.FunctionId = row["FunctionId"].ToString();
                }
                if ((row["IsDefault"] != null) && (row["IsDefault"].ToString() != ""))
                {
                    function.IsDefault = new int?(int.Parse(row["IsDefault"].ToString()));
                }
                if (row["syscode"] != null)
                {
                    function.syscode = row["syscode"].ToString();
                }
            }
            return function;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysModuleFunction ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ModuleID,FunctionId,IsDefault,syscode ");
            builder.Append(" FROM SysModuleFunction ");
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
            builder.Append(" ModuleID,FunctionId,IsDefault,syscode ");
            builder.Append(" FROM SysModuleFunction ");
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
                builder.Append("order by T.ModuleID desc");
            }
            builder.Append(")AS Row, T.*  from SysModuleFunction T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysModuleFunction GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ModuleID,FunctionId,IsDefault,syscode from SysModuleFunction ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SysModuleFunction();
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
            builder.Append("select count(1) FROM SysModuleFunction ");
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

        public bool Update(Model_SysModuleFunction model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysModuleFunction set ");
            builder.Append("ModuleID=@ModuleID,");
            builder.Append("FunctionId=@FunctionId,");
            builder.Append("IsDefault=@IsDefault,");
            builder.Append("syscode=@syscode");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModuleID", SqlDbType.VarChar, 20), new SqlParameter("@FunctionId", SqlDbType.VarChar, 20), new SqlParameter("@IsDefault", SqlDbType.Int, 4), new SqlParameter("@syscode", SqlDbType.NChar, 5) };
            cmdParms[0].Value = model.ModuleID;
            cmdParms[1].Value = model.FunctionId;
            cmdParms[2].Value = model.IsDefault;
            cmdParms[3].Value = model.syscode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

