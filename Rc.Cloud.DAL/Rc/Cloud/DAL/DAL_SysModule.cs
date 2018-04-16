namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using Rc.Common.StrUtility;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysModule
    {
        public bool AddSysModule(Model_SysModule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysModule(");
            builder.Append("MODULEID,SYSCODE,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,isLast,DefaultOrder)");
            builder.Append(" values (");
            builder.Append("@MODULEID,@SYSCODE,@MODULENAME,@PARENTID,@SLEVEL,@URL,@QUERYFORM,@OTHKEY,@REMARK,@IMGICON,@ISINTREE,@MODULETYPE,@ATTACH_SQL,@ISINTAB,@Depth,@isLast,@DefaultOrder)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@SYSCODE", SqlDbType.NChar, 5), new SqlParameter("@MODULENAME", SqlDbType.VarChar, 200), new SqlParameter("@PARENTID", SqlDbType.VarChar, 20), new SqlParameter("@SLEVEL", SqlDbType.VarChar, 0xff), new SqlParameter("@URL", SqlDbType.VarChar, 0xff), new SqlParameter("@QUERYFORM", SqlDbType.VarChar, 0xff), new SqlParameter("@OTHKEY", SqlDbType.VarChar, 0xff), new SqlParameter("@REMARK", SqlDbType.VarChar, 0xff), new SqlParameter("@IMGICON", SqlDbType.VarChar, 50), new SqlParameter("@ISINTREE", SqlDbType.Char, 1), new SqlParameter("@MODULETYPE", SqlDbType.VarChar, 50), new SqlParameter("@ATTACH_SQL", SqlDbType.VarChar, 0xff), new SqlParameter("@ISINTAB", SqlDbType.Char, 1), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@isLast", SqlDbType.Int, 4), 
                new SqlParameter("@DefaultOrder", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.MODULEID;
            cmdParms[1].Value = "00001";
            cmdParms[2].Value = model.MODULENAME;
            cmdParms[3].Value = model.PARENTID;
            cmdParms[4].Value = model.SLEVEL;
            cmdParms[5].Value = model.URL;
            cmdParms[6].Value = model.QUERYFORM;
            cmdParms[7].Value = model.OTHKEY;
            cmdParms[8].Value = model.REMARK;
            cmdParms[9].Value = model.IMGICON;
            cmdParms[10].Value = model.ISINTREE;
            cmdParms[11].Value = model.MODULETYPE;
            cmdParms[12].Value = model.ATTACH_SQL;
            cmdParms[13].Value = model.ISINTAB;
            cmdParms[14].Value = model.Depth;
            cmdParms[15].Value = model.isLast;
            cmdParms[0x10].Value = model.DefaultOrder;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysModule DataRowToModel(DataRow row)
        {
            Model_SysModule module = new Model_SysModule();
            if (row != null)
            {
                if (row["MODULEID"] != null)
                {
                    module.MODULEID = row["MODULEID"].ToString();
                }
                if (row["SYSCODE"] != null)
                {
                    module.SYSCODE = row["SYSCODE"].ToString();
                }
                if (row["MODULENAME"] != null)
                {
                    module.MODULENAME = row["MODULENAME"].ToString();
                }
                if (row["PARENTID"] != null)
                {
                    module.PARENTID = row["PARENTID"].ToString();
                }
                if (row["SLEVEL"] != null)
                {
                    module.SLEVEL = row["SLEVEL"].ToString();
                }
                if (row["URL"] != null)
                {
                    module.URL = row["URL"].ToString();
                }
                if (row["QUERYFORM"] != null)
                {
                    module.QUERYFORM = row["QUERYFORM"].ToString();
                }
                if (row["OTHKEY"] != null)
                {
                    module.OTHKEY = row["OTHKEY"].ToString();
                }
                if (row["REMARK"] != null)
                {
                    module.REMARK = row["REMARK"].ToString();
                }
                if (row["IMGICON"] != null)
                {
                    module.IMGICON = row["IMGICON"].ToString();
                }
                if (row["ISINTREE"] != null)
                {
                    module.ISINTREE = row["ISINTREE"].ToString();
                }
                if (row["MODULETYPE"] != null)
                {
                    module.MODULETYPE = row["MODULETYPE"].ToString();
                }
                if (row["ATTACH_SQL"] != null)
                {
                    module.ATTACH_SQL = row["ATTACH_SQL"].ToString();
                }
                if (row["ISINTAB"] != null)
                {
                    module.ISINTAB = row["ISINTAB"].ToString();
                }
                if ((row["Depth"] != null) && (row["Depth"].ToString() != ""))
                {
                    module.Depth = new int?(int.Parse(row["Depth"].ToString()));
                }
                if ((row["isLast"] != null) && (row["isLast"].ToString() != ""))
                {
                    module.isLast = new int?(int.Parse(row["isLast"].ToString()));
                }
                if ((row["DefaultOrder"] != null) && (row["DefaultOrder"].ToString() != ""))
                {
                    module.DefaultOrder = new int?(int.Parse(row["DefaultOrder"].ToString()));
                }
            }
            return module;
        }

        public bool DeleteSysModuleByID(string module_ID)
        {
            StringBuilder builder = new StringBuilder();
            this.DeleteSysModuleFunctionRole(module_ID);
            this.DeleteSysModuleFunctionUser(module_ID);
            builder.AppendFormat("delete from SysModule where MODULEID='{0}'", module_ID);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteSysModuleBySyscodeAndModuleID(string MODULEID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysModule ");
            builder.Append(" where MODULEID=@MODULEID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MODULEID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = MODULEID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public void DeleteSysModuleFunctionRole(string module_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("delete from SysModuleFunctionRole where MODULEID='{0}'", module_ID);
            DbHelperSQL.Query(builder.ToString());
        }

        public void DeleteSysModuleFunctionUser(string module_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("delete from SysModuleFunctionUser where MODULEID='{0}'", module_ID);
            DbHelperSQL.Query(builder.ToString());
        }

        public bool DeleteSysModuleListByModuleID(string MODULEIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysModule ");
            builder.Append(" where MODULEID in (" + MODULEIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool ExistsSysModule(Model_SysModule model, string type)
        {
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            if (type == "1")
            {
                builder.AppendFormat("SELECT COUNT(*) FROM SysModule WHERE SysCode='{0}' and  MODULEID='{1}'", model.SYSCODE, model.MODULEID);
            }
            else if (type == "2")
            {
                builder.AppendFormat("SELECT COUNT(*) FROM SysModule WHERE SysCode='{0}' and  MODULEID!='{1}'", model.SYSCODE, model.MODULEID);
            }
            if (int.Parse(DbHelperSQL.GetSingle(builder.ToString()).ToString()) > 0)
            {
                flag = true;
            }
            return flag;
        }

        public bool ExistsSysModuleByID(string MODULEID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SysModule");
            builder.Append(" where MODULEID=@MODULEID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MODULEID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = MODULEID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetModuleListBySysCode(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sm.MODULEID,sm.MODULENAME,sm.PARENTID,sm.SLEVEL,sm.URL,sc.SysName,sm.SYSCODE,sm.ISINTREE,sm.isLast,sm.DefaultOrder,sm.Depth  ");
            builder.Append(" from SysModule sm left join SysCode sc on sm.SYSCODE=sc.SysCode ");
            if (strWhere.Trim() != "")
            {
                builder.AppendFormat(" where 1=1 {0}", strWhere);
            }
            builder.Append(" order by SLEVEL");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetModuleListBySysCode_Power(string User_ID, string SysRole_IDS)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MODULEID,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,isLast,DefaultOrder ");
            builder.Append(" FROM SysModule  WHERE  ISINTREE ='Y' ");
            builder.Append(" AND SysCode='" + clsUtility.GetSysCode() + "' ");
            if (User_ID != "1ebb1705-c073-41e8-b9ab-1ea594abd433")
            {
                builder.Append(" and MODULEID IN (");
                builder.Append(" select MODULEID from SysModuleFunctionUser ");
                builder.Append(" where User_ID='" + User_ID + "' AND SysCode='" + clsUtility.GetSysCode() + "' ");
                if (string.IsNullOrEmpty(SysRole_IDS))
                {
                    builder.Append(" )");
                }
                else
                {
                    builder.Append(" union");
                    builder.Append(" select MODULEID from SysModuleFunctionRole");
                    builder.Append("  where SysRole_ID IN (" + SysRole_IDS + ") AND SysCode='" + clsUtility.GetSysCode() + "' )");
                }
            }
            builder.Append(" order by SLEVEL");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetPM_Detect_ConfigList()
        {
            return DbHelperSQL.Query("select * from PM_Detect_Config ");
        }

        public string GetSetMap(string ModuleId, string type)
        {
            object single = DbHelperSQL.GetSingle("SELECT * from GetModuleLine('" + ModuleId + "'," + type + ")");
            if (single == null)
            {
                return "";
            }
            return single.ToString();
        }

        public string GetSetMapBySysCode(string ModuleId, string type)
        {
            object single = DbHelperSQL.GetSingle("SELECT * from GetModuleLineBySysCode('" + ModuleId + "'," + type + ",'" + clsUtility.GetSysCode() + "')");
            if (single == null)
            {
                return "";
            }
            return single.ToString();
        }

        public DataSet GetSysCodeList()
        {
            return DbHelperSQL.Query("select SysCode,SysName from SysCode order by SysOrder");
        }

        public int GetSysModuleCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SysModule ");
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

        public DataSet GetSysModuleForFirstBySysCode(string sysCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MODULEID,MODULENAME");
            builder.Append(" FROM SysModule ");
            builder.Append(" WHERE PARENTID='0' AND  ISINTREE='Y' AND SysCode='" + sysCode + "' ");
            builder.Append(" order by SLEVEL");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleForFirstLevel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MODULEID,MODULENAME");
            builder.Append(" FROM SysModule ");
            builder.Append(" WHERE PARENTID='0' AND  ISINTREE='Y'  ");
            builder.Append(" order by SLEVEL");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM SysModule ");
            if (strWhere.Trim() != "")
            {
                builder.AppendFormat(" where 1=1 {0}", strWhere);
            }
            builder.Append(" order by SysCode,MODULEID,DefaultOrder");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" MODULEID,SYSCODE,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,isLast,DefaultOrder ");
            builder.Append(" FROM SysModule ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleList_Power(string User_ID, string SysRole_IDS)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MODULEID,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,isLast,DefaultOrder ");
            builder.Append(" FROM SysModule  WHERE  ISINTREE ='Y' ");
            if (User_ID != "1ebb1705-c073-41e8-b9ab-1ea594abd433")
            {
                builder.Append(" and MODULEID IN (");
                builder.Append(" select MODULEID from SysModuleFunctionUser ");
                builder.Append(" where User_ID='" + User_ID + "'");
                builder.Append(" union");
                builder.Append(" select MODULEID from SysModuleFunctionRole");
                if (string.IsNullOrEmpty(SysRole_IDS))
                {
                    builder.Append("  )");
                }
                else
                {
                    builder.Append("  where SysRole_ID IN (" + SysRole_IDS + "))");
                }
            }
            builder.Append(" order by SLEVEL");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.MODULEID desc");
            }
            builder.Append(")AS Row, T.*  from SysModule T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSysModuleListJoinSysCode(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sm.MODULEID,sm.MODULENAME,sm.PARENTID,sm.SLEVEL,sm.URL,sc.SysName,sm.SYSCODE,sm.ISINTREE,sm.isLast,sm.DefaultOrder,sm.Depth  ");
            builder.Append(" from SysModule sm left join SysCode sc on sm.SYSCODE=sc.SysCode ");
            if (strWhere.Trim() != "")
            {
                builder.AppendFormat(" where 1=1 {0}", strWhere);
            }
            builder.Append(" order by SLEVEL");
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysModule GetSysModuleModelByID(string MODULEID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 MODULEID,SYSCODE,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,isLast,DefaultOrder from SysModule ");
            builder.Append(" where MODULEID=@MODULEID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MODULEID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = MODULEID;
            new Model_SysModule();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_SysModule GetSysModuleModelBySyscodeAndModuleID(string moduleid)
        {
            return this.GetSysModuleModelBySyscodeAndModuleID(null, moduleid);
        }

        internal Model_SysModule GetSysModuleModelBySyscodeAndModuleID(DbTransaction tran, string moduleid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" SysModule ");
            builder.Append(" WHERE ");
            builder.Append(" MODULEID=@MODULEID  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MODULEID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = moduleid;
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            Model_SysModule module = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                module = new Model_SysModule();
                if (row["MODULEID"] != null)
                {
                    module.MODULEID = row["MODULEID"].ToString();
                }
                if (row["MODULENAME"] != null)
                {
                    module.MODULENAME = row["MODULENAME"].ToString();
                }
                if (row["PARENTID"] != null)
                {
                    module.PARENTID = row["PARENTID"].ToString();
                }
                if (row["SLEVEL"] != null)
                {
                    module.SLEVEL = row["SLEVEL"].ToString();
                }
                if (row["URL"] != null)
                {
                    module.URL = row["URL"].ToString();
                }
                if (row["QUERYFORM"] != null)
                {
                    module.QUERYFORM = row["QUERYFORM"].ToString();
                }
                if (row["OTHKEY"] != null)
                {
                    module.OTHKEY = row["OTHKEY"].ToString();
                }
                if (row["REMARK"] != null)
                {
                    module.REMARK = row["REMARK"].ToString();
                }
                if (row["IMGICON"] != null)
                {
                    module.IMGICON = row["IMGICON"].ToString();
                }
                if (row["ISINTREE"] != null)
                {
                    module.ISINTREE = row["ISINTREE"].ToString();
                }
                if (row["MODULETYPE"] != null)
                {
                    module.MODULETYPE = row["MODULETYPE"].ToString();
                }
                if (row["ATTACH_SQL"] != null)
                {
                    module.ATTACH_SQL = row["ATTACH_SQL"].ToString();
                }
                if (row["ISINTAB"] != null)
                {
                    module.ISINTAB = row["ISINTAB"].ToString();
                }
                if (row["Depth"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["Depth"].ToString()))
                    {
                        module.Depth = null;
                    }
                    else
                    {
                        module.Depth = new int?(int.Parse(row["Depth"].ToString()));
                    }
                }
                if (row["isLast"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["isLast"].ToString()))
                    {
                        module.isLast = null;
                    }
                    else
                    {
                        module.isLast = new int?(int.Parse(row["isLast"].ToString()));
                    }
                }
                if (row["DefaultOrder"] == null)
                {
                    return module;
                }
                if (string.IsNullOrWhiteSpace(row["DefaultOrder"].ToString()))
                {
                    module.DefaultOrder = null;
                    return module;
                }
                module.DefaultOrder = new int?(int.Parse(row["DefaultOrder"].ToString()));
            }
            return module;
        }

        public string GetURL(string code, string module)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select URL from SysModule ");
            builder.Append(" where  SYSCODE=@SYSCODE AND MODULEID=@MODULEID;");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SYSCODE", SqlDbType.NVarChar, 5), new SqlParameter("@MODULEID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = code;
            cmdParms[1].Value = module;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                return single.ToString();
            }
            return "";
        }

        public bool UpdateSysModuleBySyscodeAndModuleID(Model_SysModule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysModule set ");
            builder.Append("SYSCODE=@SYSCODE,");
            builder.Append("MODULENAME=@MODULENAME,");
            builder.Append("PARENTID=@PARENTID,");
            builder.Append("SLEVEL=@SLEVEL,");
            builder.Append("URL=@URL,");
            builder.Append("QUERYFORM=@QUERYFORM,");
            builder.Append("OTHKEY=@OTHKEY,");
            builder.Append("REMARK=@REMARK,");
            builder.Append("IMGICON=@IMGICON,");
            builder.Append("ISINTREE=@ISINTREE,");
            builder.Append("MODULETYPE=@MODULETYPE,");
            builder.Append("ATTACH_SQL=@ATTACH_SQL,");
            builder.Append("ISINTAB=@ISINTAB,");
            builder.Append("Depth=@Depth,");
            builder.Append("isLast=@isLast,");
            builder.Append("DefaultOrder=@DefaultOrder");
            builder.Append(" where SYSCODE=@SYSCODE and MODULEID=@MODULEID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@SYSCODE", SqlDbType.NChar, 5), new SqlParameter("@MODULENAME", SqlDbType.VarChar, 200), new SqlParameter("@PARENTID", SqlDbType.VarChar, 20), new SqlParameter("@SLEVEL", SqlDbType.VarChar, 0xff), new SqlParameter("@URL", SqlDbType.VarChar, 0xff), new SqlParameter("@QUERYFORM", SqlDbType.VarChar, 0xff), new SqlParameter("@OTHKEY", SqlDbType.VarChar, 0xff), new SqlParameter("@REMARK", SqlDbType.VarChar, 0xff), new SqlParameter("@IMGICON", SqlDbType.VarChar, 50), new SqlParameter("@ISINTREE", SqlDbType.Char, 1), new SqlParameter("@MODULETYPE", SqlDbType.VarChar, 50), new SqlParameter("@ATTACH_SQL", SqlDbType.VarChar, 0xff), new SqlParameter("@ISINTAB", SqlDbType.Char, 1), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@isLast", SqlDbType.Int, 4), new SqlParameter("@DefaultOrder", SqlDbType.Int, 4), 
                new SqlParameter("@MODULEID", SqlDbType.VarChar, 20)
             };
            cmdParms[0].Value = "00001";
            cmdParms[1].Value = model.MODULENAME;
            cmdParms[2].Value = model.PARENTID;
            cmdParms[3].Value = model.SLEVEL;
            cmdParms[4].Value = model.URL;
            cmdParms[5].Value = model.QUERYFORM;
            cmdParms[6].Value = model.OTHKEY;
            cmdParms[7].Value = model.REMARK;
            cmdParms[8].Value = model.IMGICON;
            cmdParms[9].Value = model.ISINTREE;
            cmdParms[10].Value = model.MODULETYPE;
            cmdParms[11].Value = model.ATTACH_SQL;
            cmdParms[12].Value = model.ISINTAB;
            cmdParms[13].Value = model.Depth;
            cmdParms[14].Value = model.isLast;
            cmdParms[15].Value = model.DefaultOrder;
            cmdParms[0x10].Value = model.MODULEID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

