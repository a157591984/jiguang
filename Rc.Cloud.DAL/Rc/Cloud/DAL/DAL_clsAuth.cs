namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using Rc.Common.StrUtility;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;

    public class DAL_clsAuth
    {
        private int AddLog(int source, string strModulePath, string strContent, int SystemLog_Type = -1, string SystemLog_Remark = "")
        {
            Model_VSysUserRole role = new Model_VSysUserRole();
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;
            string str2 = string.Empty;
            if (HttpContext.Current.Session["LoginUser"] != null)
            {
                role = (Model_VSysUserRole) HttpContext.Current.Session["LoginUser"];
                str2 = role.SysUser_ID.ToString();
            }
            else
            {
                str2 = "-1";
            }
            string str3 = HttpContext.Current.Request.Url.ToString().Filter();
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO [SystemLog]");
            builder.Append(" ([SystemLog_ID]");
            builder.Append(" ,[SystemLog_Level]");
            builder.Append(" ,[SystemLog_Model]");
            builder.Append(" ,[SystemLog_Desc]");
            builder.Append(" ,[SystemLog_LoginID]");
            builder.Append(" ,[SystemLog_CreateDate]");
            builder.Append(" ,[SystemLog_IP]");
            builder.Append(" ,[SystemLog_Type]");
            builder.Append(" ,[SystemLog_Remark]");
            builder.Append(" ,[SystemLog_Source])");
            builder.Append(" VALUES ");
            builder.Append(" (");
            builder.Append(" newid(), ");
            builder.Append(" '" + str3 + "',");
            builder.Append(" '" + this.GetSetMap(strModulePath, "1") + "',");
            builder.Append(" '" + strContent + "',");
            builder.Append(" '" + str2 + "',");
            builder.Append(" getdate(),");
            builder.Append(" '" + userHostAddress + "',");
            builder.Append(" " + SystemLog_Type + ",");
            builder.Append(" '" + SystemLog_Remark + "',");
            builder.Append(" " + source);
            builder.Append(") ");
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public int AddLogError(int source, string strModulePath, string strContent)
        {
            Model_VSysUserRole role = new Model_VSysUserRole();
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;
            string str2 = string.Empty;
            if (HttpContext.Current.Session["LoginUser"] != null)
            {
                role = (Model_VSysUserRole) HttpContext.Current.Session["LoginUser"];
                str2 = role.SysUser_ID.ToString();
            }
            else
            {
                str2 = "-1";
            }
            string str3 = HttpContext.Current.Request.Url.ToString().Filter();
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO [SystemLogError] ");
            builder.Append("([SystemLog_ID],[SystemLog_PagePath],[SystemLog_SysPath],[SystemLog_Desc],[SystemLog_LoginID],[SystemLog_CreateDate],[SystemLog_IP],[SystemLog_Source])");
            builder.Append(" values (");
            builder.Append("newid(),@SystemLog_PagePath,@SystemLog_SysPath,@SystemLog_Desc,@SystemLog_LoginID,getdate(),@SystemLog_IP,@SystemLog_Source)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SystemLog_PagePath", SqlDbType.NVarChar, 500), new SqlParameter("@SystemLog_SysPath", SqlDbType.NVarChar, 500), new SqlParameter("@SystemLog_Desc", SqlDbType.VarChar, 0xfa0), new SqlParameter("@SystemLog_LoginID", SqlDbType.NVarChar, 0x24), new SqlParameter("@SystemLog_IP", SqlDbType.NVarChar, 50), new SqlParameter("@SystemLog_Source", SqlDbType.Int, 1) };
            cmdParms[0].Value = str3;
            cmdParms[1].Value = this.GetSetMap(strModulePath, "1");
            cmdParms[2].Value = strContent;
            cmdParms[3].Value = str2;
            cmdParms[4].Value = userHostAddress;
            cmdParms[5].Value = 1;
            return DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public int AddLogErrorFromBS(string strModulePath, string strContent)
        {
            return this.AddLogError(1, strModulePath, strContent);
        }

        public int AddLogErrorFromCS(string strModulePath, string strContent)
        {
            return this.AddLogError(2, strModulePath, strContent);
        }

        public int AddLogFromBS(string strModulePath, string strContent)
        {
            return this.AddLog(1, strModulePath, strContent, -1, "");
        }

        public int AddLogFromCS(string strModulePath, string eventContent)
        {
            return this.AddLog(2, strModulePath, eventContent, -1, "");
        }

        public int AddLogFromPrescriptionQuery(string strModulePath, string strContent, string SystemLog_Remark)
        {
            return this.AddLog(1, strModulePath, strContent, 1, SystemLog_Remark);
        }

        public DataTable GetOwenTree(string User_ID, string SysRole_IDS, string ModuleIDLike)
        {
            if (string.IsNullOrEmpty(SysRole_IDS))
            {
                SysRole_IDS = "''";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM SysModule  WHERE  ISINTREE ='Y'");
            builder.Append(" and (slevel LIKE '" + ModuleIDLike + "%') ");
            if (User_ID != "1ebb1705-c073-41e8-b9ab-1ea594abd433")
            {
                builder.Append(" and MODULEID IN (");
                builder.Append(" select MODULEID from SysModuleFunctionUser ");
                builder.Append(" where User_ID='" + User_ID + "'");
                builder.Append(" union");
                builder.Append(" select MODULEID from SysModuleFunctionRole");
                builder.Append("  where SysRole_ID IN (" + SysRole_IDS + ")");
                builder.Append(" )");
            }
            builder.Append(" ORDER BY SLEVEL");
            return DbHelperSQL.Query(builder.ToString()).Tables[0];
        }

        public DataTable GetOwenTreeByCatch(string User_ID, string SysRole_IDS, string ModuleIDLike)
        {
            string str = ConfigurationManager.AppSettings["IsEnableDataCache"];
            bool flag = false;
            if (!string.IsNullOrEmpty(str) && (str.ToLower().Equals("true") || str.ToLower().Equals("false")))
            {
                flag = bool.Parse(str);
            }
            if (flag)
            {
                string cacheName = "Owen_LeftTree_" + User_ID + "_" + ModuleIDLike;
                object cache = CacheClass.GetCache(cacheName);
                if (cache == null)
                {
                    try
                    {
                        cache = this.GetOwenTree(User_ID, SysRole_IDS, ModuleIDLike);
                        if (cache != null)
                        {
                            int num = 1;
                            CacheClass.AddCache(cacheName, DateTime.Now.AddMinutes((double) num), cache);
                        }
                    }
                    catch
                    {
                    }
                }
                return (DataTable) cache;
            }
            object obj3 = this.GetOwenTree(User_ID, SysRole_IDS, ModuleIDLike);
            CacheClass.DeleteCache("Owen_LeftTree_" + User_ID + "_" + ModuleIDLike);
            return (DataTable) obj3;
        }

        public DataTable GetOwenTreeByCatch(string User_ID, string SysRole_IDS, string ModuleIDLike, bool IsEnableDataCache)
        {
            DataTable table = new DataTable();
            if (IsEnableDataCache)
            {
                return this.GetOwenTreeByCatch(User_ID, SysRole_IDS, ModuleIDLike);
            }
            table = this.GetOwenTree(User_ID, SysRole_IDS, ModuleIDLike);
            CacheClass.DeleteCache("Owen_LeftTree_" + User_ID + "_" + ModuleIDLike);
            return table;
        }

        public DataTable GetOwenTreeByCatchBySysCode(string User_ID, string SysRole_IDS, string ModuleIDLike)
        {
            string str = ConfigurationManager.AppSettings["IsEnableDataCache"];
            bool flag = false;
            if (!string.IsNullOrEmpty(str) && (str.ToLower().Equals("true") || str.ToLower().Equals("false")))
            {
                flag = bool.Parse(str);
            }
            if (flag)
            {
                string cacheName = "Owen_LeftTree_" + clsUtility.GetSysCode() + "_" + User_ID + "_" + ModuleIDLike;
                object cache = CacheClass.GetCache(cacheName);
                if (cache == null)
                {
                    try
                    {
                        cache = this.GetOwenTreeBySysCode(User_ID, SysRole_IDS, ModuleIDLike);
                        if (cache != null)
                        {
                            int num = 1;
                            CacheClass.AddCache(cacheName, DateTime.Now.AddMinutes((double) num), cache);
                        }
                    }
                    catch
                    {
                    }
                }
                return (DataTable) cache;
            }
            object obj3 = this.GetOwenTreeBySysCode(User_ID, SysRole_IDS, ModuleIDLike);
            CacheClass.DeleteCache("Owen_LeftTree_" + clsUtility.GetSysCode() + "_" + User_ID + "_" + ModuleIDLike);
            return (DataTable) obj3;
        }

        public DataTable GetOwenTreeByCatchBySysCode(string User_ID, string SysRole_IDS, string ModuleIDLike, bool IsEnableDataCache)
        {
            DataTable table = new DataTable();
            if (IsEnableDataCache)
            {
                return this.GetOwenTreeByCatchBySysCode(User_ID, SysRole_IDS, ModuleIDLike);
            }
            table = this.GetOwenTreeBySysCode(User_ID, SysRole_IDS, ModuleIDLike);
            CacheClass.DeleteCache("Owen_LeftTree_" + clsUtility.GetSysCode() + "_" + User_ID + "_" + ModuleIDLike);
            return table;
        }

        public DataTable GetOwenTreeBySysCode(string User_ID, string SysRole_IDS, string ModuleIDLike)
        {
            if (string.IsNullOrEmpty(SysRole_IDS))
            {
                SysRole_IDS = "''";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM SysModule  WHERE  ISINTREE ='Y' and SysCode ='" + clsUtility.GetSysCode() + "'");
            builder.Append(" and (slevel LIKE '" + ModuleIDLike + "%') ");
            if (User_ID != "1ebb1705-c073-41e8-b9ab-1ea594abd433")
            {
                builder.Append(" and MODULEID IN (");
                builder.Append(" select MODULEID from SysModuleFunctionUser  ");
                builder.Append(" where User_ID='" + User_ID + "'  and SysCode ='" + clsUtility.GetSysCode() + "'");
                builder.Append(" union");
                builder.Append(" select MODULEID from SysModuleFunctionRole");
                builder.Append("  where SysRole_ID IN (" + SysRole_IDS + ")  and SysCode ='" + clsUtility.GetSysCode() + "'");
                builder.Append(")");
            }
            builder.Append(" ORDER BY SLEVEL");
            return DbHelperSQL.Query(builder.ToString()).Tables[0];
        }

        public string GetSetMap(string ModuleId, string type)
        {
            object single = DbHelperSQL.GetSingle("SELECT * from GetModuleLineBySysCode('" + ModuleId + "'," + type + ",'" + clsUtility.GetSysCode() + "')");
            if ((single != null) && !(single.ToString() == ""))
            {
                return single.ToString();
            }
            return ModuleId;
        }

        public Model_Struct_Func GetUserFunc(string User_ID, string SysRole_IDs, string ModuleID)
        {
            Model_Struct_Func func = new Model_Struct_Func();
            if (User_ID == "1ebb1705-c073-41e8-b9ab-1ea594abd433")
            {
                func.page = true;
                func.Add = true;
                func.Edit = true;
                func.Delete = true;
                func.Select = true;
                func.Input = true;
                func.Output = true;
                func.Check = true;
                func.Synchronization = true;
                return func;
            }
            DataTable table = this.GetUserFunction(User_ID, SysRole_IDs, ModuleID).Tables[0];
            foreach (DataRow row in table.Rows)
            {
                switch (int.Parse(row["FUNCTIONID"].ToString()))
                {
                    case 0:
                        func.page = true;
                        break;

                    case 1:
                        func.Add = true;
                        break;

                    case 2:
                        func.Edit = true;
                        break;

                    case 3:
                        func.Delete = true;
                        break;

                    case 4:
                        func.Select = true;
                        break;

                    case 5:
                        func.Check = true;
                        break;

                    case 6:
                        func.Input = true;
                        break;

                    case 7:
                        func.Output = true;
                        break;

                    case 8:
                        func.Synchronization = true;
                        break;

                    case 9:
                        func.Move = true;
                        break;

                    case 10:
                        func.Copy = true;
                        break;
                }
            }
            return func;
        }

        public DataSet GetUserFunction(string User_ID, string SysRole_IDs, string ModuleID)
        {
            if (string.IsNullOrEmpty(SysRole_IDs))
            {
                SysRole_IDs = "''";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("select mfu.FUNCTIONID from SysModuleFunctionUser mfu");
            builder.Append(" where  mfu.User_ID='" + User_ID + "' and mfu.MODULEID='" + ModuleID + "' ");
            builder.Append(" union ");
            builder.Append(" select mfr.FUNCTIONID  from SysModuleFunctionRole mfr");
            builder.Append(" where mfr.SysRole_ID in (" + SysRole_IDs + ") ");
            builder.Append(" and MODULEID='" + ModuleID + "'");
            return DbHelperSQL.Query(builder.ToString());
        }

        public static string GetUserOfCoustmSql(string tableAs)
        {
            string str = string.Empty;
            Model_VSysUserRole role = new Model_VSysUserRole();
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;
            string str2 = string.Empty;
            DataTable table = new DataTable();
            string str3 = string.Empty;
            if (HttpContext.Current.Session["LoginUser"] != null)
            {
                role = (Model_VSysUserRole) HttpContext.Current.Session["LoginUser"];
                str2 = role.SysUser_ID.ToString();
                if (!(str2 != "1ebb1705-c073-41e8-b9ab-1ea594abd433"))
                {
                    return str;
                }
                table = DbHelperSQL.Query("SELECT * FROM SysUser_For_CustomerInfo where dbo.SysUser_For_CustomerInfo.SysUser_ID='" + str2 + "'").Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    if (row != null)
                    {
                        str3 = str3 + "'" + row["CustomerInfo_ID"].ToString() + "',";
                    }
                }
                if (!string.IsNullOrEmpty(str3))
                {
                    return (" and " + tableAs + ".CustomerInfo_ID in(" + str3.TrimEnd(new char[] { ',' }) + ")");
                }
                return " and 1<>1 ";
            }
            return " and 1<>1 ";
        }
    }
}

