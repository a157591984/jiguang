namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysModuleFunctionRole
    {
        public bool AddSysModuleFunctionRole(Model_SysModuleFunctionRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysModuleFunctionRole(");
            builder.Append("SysRole_ID,MODULEID,FUNCTIONID)");
            builder.Append(" values (");
            builder.Append("@SysRole_ID,@MODULEID,@FUNCTIONID)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysRole_ID", SqlDbType.Char, 0x24), new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = model.SysRole_ID;
            cmdParms[1].Value = model.MODULEID;
            cmdParms[2].Value = model.FUNCTIONID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetRoleModuleFunctionBySysCode(string SysRole_ID, string ModuleFirst, string sysCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select isnull(uf.moduleid, -1) as mChecked, ");
            builder.Append(" isnull(uf.functionid, -1) as fChecked, ");
            builder.Append(" isnull(mf.moduleid, uf.moduleid) as moduleid, ");
            builder.Append(" convert(int,isnull(mf.functionid, uf.functionid)) as functionid, ");
            builder.Append(" f.FUNCTIONName ,uf.SysCode ");
            builder.AppendFormat(" from ((Select * from SysModuleFunction where SysCode='{0}')) mf ", sysCode);
            builder.Append(" left join Sys_Function f on mf.FunctionId=f.FUNCTIONID ");
            builder.AppendFormat(" full join (select * from SysModuleFunctionRole where SysRole_ID = '{0}' and MODULEID like '{1}%' and SysCode='{2}') uf ", SysRole_ID, ModuleFirst, sysCode);
            builder.Append(" on mf.moduleid = uf.moduleid ");
            builder.Append(" and mf.functionid = uf.functionid ");
            builder.AppendFormat(" where isnull(mf.moduleid, uf.moduleid) like '{0}%' ", ModuleFirst);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleFunctionList(string SysRole_ID, string ModuleFirst)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select isnull(uf.moduleid, -1) as mChecked, ");
            builder.Append(" isnull(uf.functionid, -1) as fChecked, ");
            builder.Append(" isnull(mf.moduleid, uf.moduleid) as moduleid, ");
            builder.Append(" isnull(mf.functionid, uf.functionid) as functionid, ");
            builder.Append(" f.FUNCTIONName ");
            builder.Append(" from SysModuleFunction mf ");
            builder.Append(" left join Sys_Function f on mf.FunctionId=f.FUNCTIONID ");
            builder.AppendFormat(" full join (select * from SysModuleFunctionRole where SysRole_ID = '{0}' and MODULEID like '{1}%') uf ", SysRole_ID, ModuleFirst);
            builder.Append(" on mf.moduleid = uf.moduleid ");
            builder.Append(" and mf.functionid = uf.functionid ");
            builder.AppendFormat(" where isnull(mf.moduleid, uf.moduleid) like '{0}%' ", ModuleFirst);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetSysModuleFunctionRoleCount(string strWhere)
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

        public DataSet GetSysModuleFunctionRoleList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SysRole_ID,MODULEID,FUNCTIONID ");
            builder.Append(" FROM SysModuleFunctionRole ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleFunctionRoleList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" SysRole_ID,MODULEID,FUNCTIONID ");
            builder.Append(" FROM SysModuleFunctionRole ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleFunctionRoleListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
            builder.Append(")AS Row, T.*  from SysModuleFunctionRole T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysModuleFunctionRole GetSysModuleFunctionRoleModel(string SysRole_ID, string MODULEID, string FUNCTIONID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SysRole_ID,MODULEID,FUNCTIONID from SysModuleFunctionRole ");
            builder.Append(" where SysRole_ID=@SysRole_ID and MODULEID=@MODULEID and FUNCTIONID=@FUNCTIONID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysRole_ID", SqlDbType.Char, 0x24), new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = SysRole_ID;
            cmdParms[1].Value = MODULEID;
            cmdParms[2].Value = FUNCTIONID;
            Model_SysModuleFunctionRole role = new Model_SysModuleFunctionRole();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["SysRole_ID"] != null) && (set.Tables[0].Rows[0]["SysRole_ID"].ToString() != ""))
            {
                role.SysRole_ID = set.Tables[0].Rows[0]["SysRole_ID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MODULEID"] != null) && (set.Tables[0].Rows[0]["MODULEID"].ToString() != ""))
            {
                role.MODULEID = set.Tables[0].Rows[0]["MODULEID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["FUNCTIONID"] != null) && (set.Tables[0].Rows[0]["FUNCTIONID"].ToString() != ""))
            {
                role.FUNCTIONID = set.Tables[0].Rows[0]["FUNCTIONID"].ToString();
            }
            return role;
        }

        public int SetRoleModuleFunction(string checkeds, string strSysRole_ID, string strModuleIdLike)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            StringBuilder builder = new StringBuilder();
            if (!(checkeds != string.Empty))
            {
                goto Label_00E3;
            }
            int index = 0;
        Label_0095:;
            if (index < checkeds.Split(new char[] { ',' }).Length)
            {
                builder.AppendFormat("select '{0}' as SysRole_ID,'{1}' as moduleid,'{2}' as functionid union ", strSysRole_ID, checkeds.Split(new char[] { ',' })[index].Split(new char[] { '|' })[0], checkeds.Split(new char[] { ',' })[index].Split(new char[] { '|' })[1]);
                index++;
                goto Label_0095;
            }
            if (builder.ToString() != string.Empty)
            {
                str2 = "insert into SysModuleFunctionRole(SysRole_ID,moduleid,functionid)" + builder.ToString().Remove(builder.Length - 7);
            }
        Label_00E3:
            str = string.Format("delete SysModuleFunctionRole where SysRole_ID='{0}' AND moduleid like '{1}%'", strSysRole_ID, strModuleIdLike);
            return DbHelperSQL.ExecuteSqlTran(new string[] { str, str2 });
        }

        public int SetRoleModuleFunctionBySysCode(string checkeds, string strSysRole_ID, string strModuleIdLike, string sysCode)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            StringBuilder builder = new StringBuilder();
            if (!(checkeds != string.Empty))
            {
                goto Label_0102;
            }
            int index = 0;
        Label_00B4:;
            if (index < checkeds.Split(new char[] { ',' }).Length)
            {
                builder.AppendFormat("select '{0}' as SysRole_ID,'{1}' as moduleid,'{2}' as functionid ,'{3}' as SysCode union ", new object[] { strSysRole_ID, checkeds.Split(new char[] { ',' })[index].Split(new char[] { '|' })[0], checkeds.Split(new char[] { ',' })[index].Split(new char[] { '|' })[1], sysCode });
                index++;
                goto Label_00B4;
            }
            if (builder.ToString() != string.Empty)
            {
                str2 = "insert into SysModuleFunctionRole(SysRole_ID,moduleid,functionid,SysCode)" + builder.ToString().Remove(builder.Length - 7);
            }
        Label_0102:
            str = string.Format("delete SysModuleFunctionRole where SysRole_ID='{0}' AND moduleid like '{1}%' and SysCode='{2}'", strSysRole_ID, strModuleIdLike, sysCode);
            return DbHelperSQL.ExecuteSqlTran(new string[] { str, str2 });
        }
    }
}

