namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using Rc.Common.StrUtility;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Sys_Function
    {
        public bool AddSys_Function(Model_Sys_Function model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sys_Function(");
            builder.Append("FUNCTIONID,FUNCTIONName)");
            builder.Append(" values (");
            builder.Append("@FUNCTIONID,@FUNCTIONName)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONName", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = model.FUNCTIONID;
            cmdParms[1].Value = model.FUNCTIONName;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteSys_FunctionByID(string FUNCTIONID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sys_Function ");
            builder.Append(" where FUNCTIONID=@FUNCTIONID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = FUNCTIONID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteSys_FunctionListByID(string FUNCTIONIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sys_Function ");
            builder.Append(" where FUNCTIONID in (" + FUNCTIONIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public int GetSys_FunctionCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Sys_Function ");
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

        public DataSet GetSys_FunctionList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FUNCTIONID,FUNCTIONName ");
            builder.Append(" FROM Sys_Function ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSys_FunctionList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" FUNCTIONID,FUNCTIONName ");
            builder.Append(" FROM Sys_Function ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSys_FunctionListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.FUNCTIONID desc");
            }
            builder.Append(")AS Row, T.*  from Sys_Function T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Sys_Function GetSys_FunctionModel(string FUNCTIONID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FUNCTIONID,FUNCTIONName from Sys_Function ");
            builder.Append(" where FUNCTIONID=@FUNCTIONID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = FUNCTIONID;
            Model_Sys_Function function = new Model_Sys_Function();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["FUNCTIONID"] != null) && (set.Tables[0].Rows[0]["FUNCTIONID"].ToString() != ""))
            {
                function.FUNCTIONID = set.Tables[0].Rows[0]["FUNCTIONID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["FUNCTIONName"] != null) && (set.Tables[0].Rows[0]["FUNCTIONName"].ToString() != ""))
            {
                function.FUNCTIONName = set.Tables[0].Rows[0]["FUNCTIONName"].ToString();
            }
            return function;
        }

        public DataSet GetSysModuleFunctionByModuleID(string ModuleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select mf.*,f.FUNCTIONName from SysModuleFunction mf");
            builder.Append(" inner join Sys_Function f on mf.FunctionId=f.FUNCTIONID AND MF.SYSCODE='" + clsUtility.GetSysCode() + "'");
            builder.AppendFormat(" where ModuleID='{0}'", ModuleId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool UpdateSys_FunctionByID(Model_Sys_Function model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sys_Function set ");
            builder.Append("FUNCTIONName=@FUNCTIONName");
            builder.Append(" where FUNCTIONID=@FUNCTIONID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FUNCTIONName", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = model.FUNCTIONName;
            cmdParms[1].Value = model.FUNCTIONID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

