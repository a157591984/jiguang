namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using Rc.Common.StrUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysModuleFunctionUser
    {
        public bool AddSysModuleFunctionUser(Model_SysModuleFunctionUser model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysModuleFunctionUser(");
            builder.Append("User_ID,MODULEID,FUNCTIONID)");
            builder.Append(" values (");
            builder.Append("@User_ID,@MODULEID,@FUNCTIONID)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = model.User_ID;
            cmdParms[1].Value = model.MODULEID;
            cmdParms[2].Value = model.FUNCTIONID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetSysModuleFunctionList(string User_ID, string ModuleFirst)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select isnull(uf.moduleid, -1) as mChecked, ");
            builder.Append(" isnull(uf.functionid, -1) as fChecked, ");
            builder.Append(" isnull(mf.moduleid, uf.moduleid) as moduleid, ");
            builder.Append(" isnull(mf.functionid, uf.functionid) as functionid, ");
            builder.Append(" f.FUNCTIONName ");
            builder.Append(" from SysModuleFunction mf ");
            builder.Append(" left join Sys_Function f on mf.FunctionId=f.FUNCTIONID ");
            builder.AppendFormat(" full join (select * from SysModuleFunctionUser where User_ID = '{0}' and MODULEID like '{1}%') uf ", User_ID, ModuleFirst);
            builder.Append(" on mf.moduleid = uf.moduleid ");
            builder.Append(" and mf.functionid = uf.functionid ");
            builder.AppendFormat(" where isnull(mf.moduleid, uf.moduleid) like '{0}%' ", ModuleFirst);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetSysModuleFunctionUserCount(string strWhere)
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

        public DataSet GetSysModuleFunctionUserList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select User_ID,MODULEID,FUNCTIONID ");
            builder.Append(" FROM SysModuleFunctionUser ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleFunctionUserList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" User_ID,MODULEID,FUNCTIONID ");
            builder.Append(" FROM SysModuleFunctionUser ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleFunctionUserListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
            builder.Append(")AS Row, T.*  from SysModuleFunctionUser T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysModuleFunctionUser GetSysModuleFunctionUserModel(string User_ID, string MODULEID, string FUNCTIONID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 User_ID,MODULEID,FUNCTIONID from SysModuleFunctionUser ");
            builder.Append(" where User_ID=@User_ID and MODULEID=@MODULEID and FUNCTIONID=@FUNCTIONID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = User_ID;
            cmdParms[1].Value = MODULEID;
            cmdParms[2].Value = FUNCTIONID;
            Model_SysModuleFunctionUser user = new Model_SysModuleFunctionUser();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["User_ID"] != null) && (set.Tables[0].Rows[0]["User_ID"].ToString() != ""))
            {
                user.User_ID = set.Tables[0].Rows[0]["User_ID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MODULEID"] != null) && (set.Tables[0].Rows[0]["MODULEID"].ToString() != ""))
            {
                user.MODULEID = set.Tables[0].Rows[0]["MODULEID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["FUNCTIONID"] != null) && (set.Tables[0].Rows[0]["FUNCTIONID"].ToString() != ""))
            {
                user.FUNCTIONID = set.Tables[0].Rows[0]["FUNCTIONID"].ToString();
            }
            return user;
        }

        public DataSet GetUserModuleFunctionBySysCode(string User_ID, string ModuleFirst, string sysCode)
        {
            Model_VSysUserRole role = new Model_VSysUserRole();
            role = clsUtility.IsPageFlag() as Model_VSysUserRole;
            StringBuilder builder = new StringBuilder();
            builder.Append(" select isnull(uf.moduleid, -1) as mChecked, ");
            builder.Append(" isnull(uf.functionid, -1) as fChecked, ");
            builder.Append(" isnull(mf.moduleid, uf.moduleid) as moduleid, ");
            builder.Append(" convert(int,isnull(mf.functionid, uf.functionid)) as functionid, ");
            builder.Append(" f.FUNCTIONName ,uf.SysCode");
            if (role.SysUser_ID == "1ebb1705-c073-41e8-b9ab-1ea594abd433")
            {
                builder.AppendFormat(" from (Select * from SysModuleFunction where SysCode='{0}') mf ", sysCode);
            }
            else
            {
                builder.AppendFormat(" from (Select MODULEID,FUNCTIONID,syscode from SysModuleFunctionUser where SysCode='{0}' and User_ID='{1}'\r\nUNION\r\n                    SELECT  MODULEID,FUNCTIONID,syscode\r\n                    FROM    dbo.SysModuleFunctionRole\r\n                    WHERE   SysRole_ID IN (\r\n                            SELECT  SysRole_ID\r\n                            FROM    dbo.SysUserRoleRelation\r\n                            WHERE   syscode = '{0}'\r\n                                    AND SysUser_ID = '{1}') \r\n) mf ", sysCode, role.SysUser_ID);
            }
            builder.Append(" left join Sys_Function f on mf.FunctionId=f.FUNCTIONID ");
            builder.AppendFormat(" full join (select * from SysModuleFunctionUser where User_ID = '{0}' and MODULEID like '{1}%' and SysCode='{2}') uf ", User_ID, ModuleFirst, sysCode);
            builder.Append(" on mf.moduleid = uf.moduleid ");
            builder.Append(" and mf.functionid = uf.functionid ");
            builder.AppendFormat(" where isnull(mf.moduleid, uf.moduleid) like '{0}%' ", ModuleFirst);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int SetUserModuleFunction(string checkeds, string strUserId, string strModuleIdLike)
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
                builder.AppendFormat("select '{0}' as strUserId,'{1}' as moduleid,'{2}' as functionid union ", strUserId, checkeds.Split(new char[] { ',' })[index].Split(new char[] { '|' })[0], checkeds.Split(new char[] { ',' })[index].Split(new char[] { '|' })[1]);
                index++;
                goto Label_0095;
            }
            if (builder.ToString() != string.Empty)
            {
                str2 = "insert into SysModuleFunctionUser(User_ID,moduleid,functionid)" + builder.ToString().Remove(builder.Length - 7);
            }
        Label_00E3:
            str = string.Format("delete SysModuleFunctionUser where User_ID='{0}' AND moduleid like '{1}%'", strUserId, strModuleIdLike);
            List<string> sQLStringList = new List<string> {
                str
            };
            if (!string.IsNullOrEmpty(str2))
            {
                sQLStringList.Add(str2);
            }
            return DbHelperSQL.ExecuteSqlTran(sQLStringList);
        }

        public int SetUserModuleFunctionBySysCode(string checkeds, string strUser_ID, string strModuleIdLike, string sysCode)
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
                builder.AppendFormat("select '{0}' as User_ID,'{1}' as moduleid,'{2}' as functionid ,'{3}' as SysCode union ", new object[] { strUser_ID, checkeds.Split(new char[] { ',' })[index].Split(new char[] { '|' })[0], checkeds.Split(new char[] { ',' })[index].Split(new char[] { '|' })[1], sysCode });
                index++;
                goto Label_00B4;
            }
            if (builder.ToString() != string.Empty)
            {
                str2 = "insert into SysModuleFunctionUser(User_ID,moduleid,functionid,SysCode)" + builder.ToString().Remove(builder.Length - 7);
            }
        Label_0102:
            str = string.Format("delete SysModuleFunctionUser where User_ID='{0}' AND moduleid like '{1}%' and SysCode='{2}'", strUser_ID, strModuleIdLike, sysCode);
            List<string> sQLStringList = new List<string> {
                str
            };
            if (!string.IsNullOrEmpty(str2))
            {
                sQLStringList.Add(str2);
            }
            return DbHelperSQL.ExecuteSqlTran(sQLStringList);
        }
    }
}

