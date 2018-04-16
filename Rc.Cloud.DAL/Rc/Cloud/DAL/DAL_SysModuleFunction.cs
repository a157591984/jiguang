namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysModuleFunction
    {
        public bool AddSysModuleFunction(Model_SysModuleFunction model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysModuleFunction(");
            builder.Append("ModuleID,FunctionId,IsDefault)");
            builder.Append(" values (");
            builder.Append("@ModuleID,@FunctionId,@IsDefault)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModuleID", SqlDbType.VarChar, 20), new SqlParameter("@FunctionId", SqlDbType.VarChar, 20), new SqlParameter("@IsDefault", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ModuleID;
            cmdParms[1].Value = model.FunctionId;
            cmdParms[2].Value = model.IsDefault;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteSysModuleFunction(string ModuleID, string FunctionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysModuleFunction ");
            builder.Append(" where ModuleID=@ModuleID and FunctionId=@FunctionId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModuleID", SqlDbType.VarChar, 20), new SqlParameter("@FunctionId", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = ModuleID;
            cmdParms[1].Value = FunctionId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int GetSysModuleFunctionCount(string strWhere)
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

        public DataSet GetSysModuleFunctionList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ModuleID,FunctionId,IsDefault ");
            builder.Append(" FROM SysModuleFunction ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleFunctionList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ModuleID,FunctionId,IsDefault ");
            builder.Append(" FROM SysModuleFunction ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleFunctionListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.FunctionId desc");
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

        public Model_SysModuleFunction GetSysModuleFunctionModel(string ModuleID, string FunctionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ModuleID,FunctionId,IsDefault from SysModuleFunction ");
            builder.Append(" where ModuleID=@ModuleID and FunctionId=@FunctionId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModuleID", SqlDbType.VarChar, 20), new SqlParameter("@FunctionId", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = ModuleID;
            cmdParms[1].Value = FunctionId;
            Model_SysModuleFunction function = new Model_SysModuleFunction();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ModuleID"] != null) && (set.Tables[0].Rows[0]["ModuleID"].ToString() != ""))
            {
                function.ModuleID = set.Tables[0].Rows[0]["ModuleID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["FunctionId"] != null) && (set.Tables[0].Rows[0]["FunctionId"].ToString() != ""))
            {
                function.FunctionId = set.Tables[0].Rows[0]["FunctionId"].ToString();
            }
            if ((set.Tables[0].Rows[0]["IsDefault"] != null) && (set.Tables[0].Rows[0]["IsDefault"].ToString() != ""))
            {
                function.IsDefault = new int?(int.Parse(set.Tables[0].Rows[0]["IsDefault"].ToString()));
            }
            return function;
        }

        public bool SetModuleFunction(string ModuleId, string FunctionIds)
        {
            List<string> sQLStringList = new List<string>();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Delete SysModuleFunction where ModuleId='{0}'", ModuleId);
            sQLStringList.Add(builder.ToString());
            int index = 0;
        Label_0091:;
            if (index < FunctionIds.Split(new char[] { ',' }).Length)
            {
                StringBuilder builder2 = new StringBuilder();
                builder2.Append("insert into SysModuleFunction(ModuleId,FunctionId) values('" + ModuleId + "','" + FunctionIds.Split(new char[] { ',' })[index] + "')");
                sQLStringList.Add(builder2.ToString());
                index++;
                goto Label_0091;
            }
            return (DbHelperSQL.ExecuteSqlTran(sQLStringList) > 0);
        }

        public bool SetModuleFunctionByModuleIdAndSysCode(string ModuleId, string FunctionIds, string sysCode)
        {
            List<string> sQLStringList = new List<string>();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Delete SysModuleFunction where ModuleId='{0}' and SysCode='" + sysCode + "'", ModuleId);
            sQLStringList.Add(builder.ToString());
            int index = 0;
        Label_00AA:;
            if (index < FunctionIds.Split(new char[] { ',' }).Length)
            {
                StringBuilder builder2 = new StringBuilder();
                builder2.Append("insert into SysModuleFunction(ModuleId,FunctionId,SysCode) values('" + ModuleId + "','" + FunctionIds.Split(new char[] { ',' })[index] + "','" + sysCode + "')");
                sQLStringList.Add(builder2.ToString());
                index++;
                goto Label_00AA;
            }
            return (DbHelperSQL.ExecuteSqlTran(sQLStringList) > 0);
        }

        public bool UpdateSysModuleFunction(Model_SysModuleFunction model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysModuleFunction set ");
            builder.Append("IsDefault=@IsDefault");
            builder.Append(" where ModuleID=@ModuleID and FunctionId=@FunctionId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@IsDefault", SqlDbType.Int, 4), new SqlParameter("@ModuleID", SqlDbType.VarChar, 20), new SqlParameter("@FunctionId", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = model.IsDefault;
            cmdParms[1].Value = model.ModuleID;
            cmdParms[2].Value = model.FunctionId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

