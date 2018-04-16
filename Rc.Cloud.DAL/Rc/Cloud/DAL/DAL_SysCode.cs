namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_SysCode
    {
        public bool AddSysModule(Model_SysModule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into  SysModule(");
            builder.Append("MODULEID,SYSCODE,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,isLast,DefaultOrder)");
            builder.Append(" values (");
            builder.Append("@MODULEID,@SYSCODE,@MODULENAME,@PARENTID,@SLEVEL,@URL,@QUERYFORM,@OTHKEY,@REMARK,@IMGICON,@ISINTREE,@MODULETYPE,@ATTACH_SQL,@ISINTAB,@Depth,@isLast,@DefaultOrder)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@SYSCODE", SqlDbType.NChar, 5), new SqlParameter("@MODULENAME", SqlDbType.VarChar, 200), new SqlParameter("@PARENTID", SqlDbType.VarChar, 20), new SqlParameter("@SLEVEL", SqlDbType.VarChar, 0xff), new SqlParameter("@URL", SqlDbType.VarChar, 0xff), new SqlParameter("@QUERYFORM", SqlDbType.VarChar, 0xff), new SqlParameter("@OTHKEY", SqlDbType.VarChar, 0xff), new SqlParameter("@REMARK", SqlDbType.VarChar, 0xff), new SqlParameter("@IMGICON", SqlDbType.VarChar, 50), new SqlParameter("@ISINTREE", SqlDbType.Char, 1), new SqlParameter("@MODULETYPE", SqlDbType.VarChar, 50), new SqlParameter("@ATTACH_SQL", SqlDbType.VarChar, 0xff), new SqlParameter("@ISINTAB", SqlDbType.Char, 1), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@isLast", SqlDbType.Int, 4), 
                new SqlParameter("@DefaultOrder", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.MODULEID;
            cmdParms[1].Value = model.SYSCODE;
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

        public bool DeleteBaseModule(string moduleID, string sysCode)
        {
            return (DbHelperSQL.ExecuteSql(" delete  SysModule where MODULEID='" + moduleID + "' and SYSCODE ='" + sysCode + "'") > 0);
        }

        public bool ExistsSysModule(Model_SysModule model, string type)
        {
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            if (type == "1")
            {
                builder.AppendFormat("SELECT COUNT(*) FROM  SysModule WHERE SysCode='{0}' and  MODULEID='{1}'", model.SYSCODE, model.MODULEID);
            }
            else if (type == "2")
            {
                builder.AppendFormat("SELECT COUNT(*) FROM  SysModule WHERE SysCode='{0}' and  MODULEID!='{1}'", model.SYSCODE, model.MODULEID);
            }
            if (int.Parse(DbHelperSQL.GetSingle(builder.ToString()).ToString()) > 0)
            {
                flag = true;
            }
            return flag;
        }

        public DataSet GetComparisonDataList(string id, string syscode, string dataBase)
        {
            return DbHelperSQL.Query(string.Format(" select row_number() over(order by MODULEID) AS r_n,* from (\r\n                                   select * from {0}.dbo.SysModule f1 where MODULEID='{1}' and Syscode='{2}'\r\n\r\n                ) as t", dataBase, id, syscode));
        }

        public DataSet GetDataList(string dataBase, string condition)
        {
            string str = string.Empty;
            str = string.Format(" select row_number() over(order by MODULEID) AS r_n,* from (select * from  SysModule S1 where \r\n                                    NOT EXISTS( SELECT * from {0}.dbo.SysModule S2 where\r\n                                    S1.MODULEID=S2.MODULEID AND S1.Syscode=S2.Syscode)", dataBase);
            if (condition != "")
            {
                str = str + condition;
            }
            return DbHelperSQL.Query(str + "  ) as t");
        }

        public DataSet GetModuleListBySysCode(string condition)
        {
            string str = string.Empty;
            str = "select * from (\r\n                     select sm.*\r\n                     from  SysModule sm left join  SysCode sc on sm.SYSCODE=sc.SysCode \r\n                     where 1=1";
            if (condition != "")
            {
                str = str + condition;
            }
            return DbHelperSQL.Query(str + "  ) as t");
        }

        public DataSet GetModuleListBySysCode(string condition, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            string str = string.Empty;
            str = "select row_number() over(order by SLEVEL ) AS r_n,* from (\r\n                     select sm.MODULEID,sm.MODULENAME,sm.PARENTID,sm.SLEVEL,sm.URL,sc.SysName,sm.SYSCODE,sm.ISINTREE,sm.isLast,sm.DefaultOrder,sm.Depth\r\n                     from  SysModule sm left join  SysCode sc on sm.SYSCODE=sc.SysCode \r\n                     where 1=1";
            if (condition != "")
            {
                str = str + condition;
            }
            return sys.GetRecordByPage(str + "  ) as t", PageIndex, PageSize, out rCount, out pCount);
        }

        public DataSet GetSysModuleColumnName()
        {
            return DbHelperSQL.Query(string.Format(" select top 1 * from  SysModule", new object[0]));
        }

        public Model_SysModule GetSysModuleModelBySyscodeAndModuleID(string syscode, string moduleid)
        {
            return this.GetSysModuleModelBySyscodeAndModuleID(null, syscode, moduleid);
        }

        internal Model_SysModule GetSysModuleModelBySyscodeAndModuleID(DbTransaction tran, string syscode, string moduleid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append("  SysModule ");
            builder.Append(" WHERE ");
            builder.Append(" SYSCODE=@SYSCODE and MODULEID=@MODULEID  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SYSCODE", SqlDbType.NChar, 5), new SqlParameter("@MODULEID", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = syscode;
            cmdParms[1].Value = moduleid;
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            Model_SysModule module = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                module = new Model_SysModule();
                if (row["SYSCODE"] != null)
                {
                    module.SYSCODE = row["SYSCODE"].ToString();
                }
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

        public DataSet GetSysName()
        {
            string sQLString = string.Empty;
            sQLString = " select * from  SysCode ";
            return DbHelperSQL.Query(sQLString);
        }

        public DataSet GetUpdateDataList(string where, string dataBase, string condition)
        {
            string sQLString = string.Empty;
            sQLString = string.Format(" select row_number() over(order by MODULEID) AS r_n,* from (\r\n                                   select * from  SysModule f1 where EXISTS (select * from {0}.dbo.SysModule f2 where {1})\r\n                ) as t", dataBase, where);
            if (!string.IsNullOrEmpty(condition))
            {
                sQLString = sQLString + "  where 1=1 " + condition;
            }
            return DbHelperSQL.Query(sQLString);
        }

        public bool UpdateSysModuleBySyscodeAndModuleID(Model_SysModule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update  SysModule set ");
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
            cmdParms[0].Value = model.SYSCODE;
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

