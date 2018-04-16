namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysModule
    {
        public bool Add(Model_SysModule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysModule(");
            builder.Append("MODULEID,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,isLast,DefaultOrder,syscode)");
            builder.Append(" values (");
            builder.Append("@MODULEID,@MODULENAME,@PARENTID,@SLEVEL,@URL,@QUERYFORM,@OTHKEY,@REMARK,@IMGICON,@ISINTREE,@MODULETYPE,@ATTACH_SQL,@ISINTAB,@Depth,@isLast,@DefaultOrder,@syscode)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@MODULENAME", SqlDbType.VarChar, 200), new SqlParameter("@PARENTID", SqlDbType.VarChar, 20), new SqlParameter("@SLEVEL", SqlDbType.VarChar, 0xff), new SqlParameter("@URL", SqlDbType.VarChar, 0xff), new SqlParameter("@QUERYFORM", SqlDbType.VarChar, 0xff), new SqlParameter("@OTHKEY", SqlDbType.VarChar, 0xff), new SqlParameter("@REMARK", SqlDbType.VarChar, 0xff), new SqlParameter("@IMGICON", SqlDbType.VarChar, 50), new SqlParameter("@ISINTREE", SqlDbType.Char, 1), new SqlParameter("@MODULETYPE", SqlDbType.VarChar, 50), new SqlParameter("@ATTACH_SQL", SqlDbType.VarChar, 0xff), new SqlParameter("@ISINTAB", SqlDbType.Char, 1), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@isLast", SqlDbType.Int, 4), new SqlParameter("@DefaultOrder", SqlDbType.Int, 4), 
                new SqlParameter("@syscode", SqlDbType.NChar, 5)
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
            cmdParms[14].Value = model.isLast;
            cmdParms[15].Value = model.DefaultOrder;
            cmdParms[0x10].Value = model.syscode;
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
                if (row["syscode"] != null)
                {
                    module.syscode = row["syscode"].ToString();
                }
            }
            return module;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysModule ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MODULEID,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,isLast,DefaultOrder,syscode ");
            builder.Append(" FROM SysModule ");
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
            builder.Append(" MODULEID,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,isLast,DefaultOrder,syscode ");
            builder.Append(" FROM SysModule ");
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
            builder.Append(")AS Row, T.*  from SysModule T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysModule GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 MODULEID,MODULENAME,PARENTID,SLEVEL,URL,QUERYFORM,OTHKEY,REMARK,IMGICON,ISINTREE,MODULETYPE,ATTACH_SQL,ISINTAB,Depth,isLast,DefaultOrder,syscode from SysModule ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SysModule();
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

        public bool Update(Model_SysModule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysModule set ");
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
            builder.Append("isLast=@isLast,");
            builder.Append("DefaultOrder=@DefaultOrder,");
            builder.Append("syscode=@syscode");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@MODULEID", SqlDbType.VarChar, 20), new SqlParameter("@MODULENAME", SqlDbType.VarChar, 200), new SqlParameter("@PARENTID", SqlDbType.VarChar, 20), new SqlParameter("@SLEVEL", SqlDbType.VarChar, 0xff), new SqlParameter("@URL", SqlDbType.VarChar, 0xff), new SqlParameter("@QUERYFORM", SqlDbType.VarChar, 0xff), new SqlParameter("@OTHKEY", SqlDbType.VarChar, 0xff), new SqlParameter("@REMARK", SqlDbType.VarChar, 0xff), new SqlParameter("@IMGICON", SqlDbType.VarChar, 50), new SqlParameter("@ISINTREE", SqlDbType.Char, 1), new SqlParameter("@MODULETYPE", SqlDbType.VarChar, 50), new SqlParameter("@ATTACH_SQL", SqlDbType.VarChar, 0xff), new SqlParameter("@ISINTAB", SqlDbType.Char, 1), new SqlParameter("@Depth", SqlDbType.Int, 4), new SqlParameter("@isLast", SqlDbType.Int, 4), new SqlParameter("@DefaultOrder", SqlDbType.Int, 4), 
                new SqlParameter("@syscode", SqlDbType.NChar, 5)
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
            cmdParms[14].Value = model.isLast;
            cmdParms[15].Value = model.DefaultOrder;
            cmdParms[0x10].Value = model.syscode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

