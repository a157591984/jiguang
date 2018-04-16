namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysModuleHistory
    {
        public bool Add(Model_SysModuleHistory model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysModuleHistory(");
            builder.Append("MODULEID,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,IsLast,DefaultOrder,Syscode,UpdateTime,ModuleSource)");
            builder.Append(" values (");
            builder.Append("@MODULEID,@MODULENAME,@PARENTID,@SLEVEL,@URL,@QUERYFORM,@OTHKEY,@REMARK,@IMGICON,@ISINTREE,@MODULETYPE,@ATTACH_SQL,@ISINTAB,@Depth,@IsLast,@DefaultOrder,@Syscode,@UpdateTime,@ModuleSource)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@MODULENAME", SqlDbType.VarChar, 200), new SqlParameter("@PARENTID", SqlDbType.VarChar, 20), new SqlParameter("@SLEVEL", SqlDbType.VarChar, 0xff), new SqlParameter("@URL", SqlDbType.VarChar, 0xff), new SqlParameter("@QUERYFORM", SqlDbType.VarChar, 0xff), new SqlParameter("@OTHKEY", SqlDbType.VarChar, 0xff), new SqlParameter("@REMARK", SqlDbType.VarChar, 0xff), new SqlParameter("@IMGICON", SqlDbType.VarChar, 50), new SqlParameter("@ISINTREE", SqlDbType.Char, 1), new SqlParameter("@MODULETYPE", SqlDbType.VarChar, 50), new SqlParameter("@ATTACH_SQL", SqlDbType.VarChar, 0xff), new SqlParameter("@ISINTAB", SqlDbType.Char, 1), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@IsLast", SqlDbType.Int, 4), new SqlParameter("@DefaultOrder", SqlDbType.Int, 4), 
                new SqlParameter("@Syscode", SqlDbType.NChar, 5), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@ModuleSource", SqlDbType.NChar, 50)
             };
            cmdParms[0].Value = model.MODULEID;
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
            cmdParms[14].Value = model.IsLast;
            cmdParms[15].Value = model.DefaultOrder;
            cmdParms[0x10].Value = model.Syscode;
            cmdParms[0x11].Value = model.UpdateTime;
            cmdParms[0x12].Value = model.ModuleSource;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysModuleHistory DataRowToModel(DataRow row)
        {
            Model_SysModuleHistory history = new Model_SysModuleHistory();
            if (row != null)
            {
                if (row["MODULEID"] != null)
                {
                    history.MODULEID = row["MODULEID"].ToString();
                }
                if (row["MODULENAME"] != null)
                {
                    history.MODULENAME = row["MODULENAME"].ToString();
                }
                if (row["PARENTID"] != null)
                {
                    history.PARENTID = row["PARENTID"].ToString();
                }
                if (row["SLEVEL"] != null)
                {
                    history.SLEVEL = row["SLEVEL"].ToString();
                }
                if (row["URL"] != null)
                {
                    history.URL = row["URL"].ToString();
                }
                if (row["QUERYFORM"] != null)
                {
                    history.QUERYFORM = row["QUERYFORM"].ToString();
                }
                if (row["OTHKEY"] != null)
                {
                    history.OTHKEY = row["OTHKEY"].ToString();
                }
                if (row["REMARK"] != null)
                {
                    history.REMARK = row["REMARK"].ToString();
                }
                if (row["IMGICON"] != null)
                {
                    history.IMGICON = row["IMGICON"].ToString();
                }
                if (row["ISINTREE"] != null)
                {
                    history.ISINTREE = row["ISINTREE"].ToString();
                }
                if (row["MODULETYPE"] != null)
                {
                    history.MODULETYPE = row["MODULETYPE"].ToString();
                }
                if (row["ATTACH_SQL"] != null)
                {
                    history.ATTACH_SQL = row["ATTACH_SQL"].ToString();
                }
                if (row["ISINTAB"] != null)
                {
                    history.ISINTAB = row["ISINTAB"].ToString();
                }
                if ((row["Depth"] != null) && (row["Depth"].ToString() != ""))
                {
                    history.Depth = new int?(int.Parse(row["Depth"].ToString()));
                }
                if ((row["IsLast"] != null) && (row["IsLast"].ToString() != ""))
                {
                    history.IsLast = new int?(int.Parse(row["IsLast"].ToString()));
                }
                if ((row["DefaultOrder"] != null) && (row["DefaultOrder"].ToString() != ""))
                {
                    history.DefaultOrder = new int?(int.Parse(row["DefaultOrder"].ToString()));
                }
                if (row["Syscode"] != null)
                {
                    history.Syscode = row["Syscode"].ToString();
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    history.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
                if (row["ModuleSource"] != null)
                {
                    history.ModuleSource = row["ModuleSource"].ToString();
                }
            }
            return history;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysModuleHistory ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MODULEID,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,IsLast,DefaultOrder,Syscode,UpdateTime,ModuleSource ");
            builder.Append(" FROM SysModuleHistory ");
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
            builder.Append(" MODULEID,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,IsLast,DefaultOrder,Syscode,UpdateTime,ModuleSource ");
            builder.Append(" FROM SysModuleHistory ");
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
                builder.Append("order by T.MODULEID desc");
            }
            builder.Append(")AS Row, T.*  from SysModuleHistory T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysModuleHistory GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 MODULEID,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,IsLast,DefaultOrder,Syscode,UpdateTime,ModuleSource from SysModuleHistory ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SysModuleHistory();
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
            builder.Append("select count(1) FROM SysModuleHistory ");
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

        public bool Update(Model_SysModuleHistory model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysModuleHistory set ");
            builder.Append("MODULEID=@MODULEID,");
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
            builder.Append("IsLast=@IsLast,");
            builder.Append("DefaultOrder=@DefaultOrder,");
            builder.Append("Syscode=@Syscode,");
            builder.Append("UpdateTime=@UpdateTime,");
            builder.Append("ModuleSource=@ModuleSource");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@MODULENAME", SqlDbType.VarChar, 200), new SqlParameter("@PARENTID", SqlDbType.VarChar, 20), new SqlParameter("@SLEVEL", SqlDbType.VarChar, 0xff), new SqlParameter("@URL", SqlDbType.VarChar, 0xff), new SqlParameter("@QUERYFORM", SqlDbType.VarChar, 0xff), new SqlParameter("@OTHKEY", SqlDbType.VarChar, 0xff), new SqlParameter("@REMARK", SqlDbType.VarChar, 0xff), new SqlParameter("@IMGICON", SqlDbType.VarChar, 50), new SqlParameter("@ISINTREE", SqlDbType.Char, 1), new SqlParameter("@MODULETYPE", SqlDbType.VarChar, 50), new SqlParameter("@ATTACH_SQL", SqlDbType.VarChar, 0xff), new SqlParameter("@ISINTAB", SqlDbType.Char, 1), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@IsLast", SqlDbType.Int, 4), new SqlParameter("@DefaultOrder", SqlDbType.Int, 4), 
                new SqlParameter("@Syscode", SqlDbType.NChar, 5), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@ModuleSource", SqlDbType.NChar, 50)
             };
            cmdParms[0].Value = model.MODULEID;
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
            cmdParms[14].Value = model.IsLast;
            cmdParms[15].Value = model.DefaultOrder;
            cmdParms[0x10].Value = model.Syscode;
            cmdParms[0x11].Value = model.UpdateTime;
            cmdParms[0x12].Value = model.ModuleSource;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

