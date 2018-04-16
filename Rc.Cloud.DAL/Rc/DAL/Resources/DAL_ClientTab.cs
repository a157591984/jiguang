namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_ClientTab
    {
        public bool Add(Model_ClientTab model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ClientTab(");
            builder.Append("Tabindex,TabName,TabType,Remark,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@Tabindex,@TabName,@TabType,@Remark,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Tabindex", SqlDbType.VarChar, 100), new SqlParameter("@TabName", SqlDbType.VarChar, 100), new SqlParameter("@TabType", SqlDbType.Char, 1), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Tabindex;
            cmdParms[1].Value = model.TabName;
            cmdParms[2].Value = model.TabType;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.UpdateUser;
            cmdParms[7].Value = model.UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_ClientTab DataRowToModel(DataRow row)
        {
            Model_ClientTab tab = new Model_ClientTab();
            if (row != null)
            {
                if (row["Tabindex"] != null)
                {
                    tab.Tabindex = row["Tabindex"].ToString();
                }
                if (row["TabName"] != null)
                {
                    tab.TabName = row["TabName"].ToString();
                }
                if (row["TabType"] != null)
                {
                    tab.TabType = row["TabType"].ToString();
                }
                if (row["Remark"] != null)
                {
                    tab.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    tab.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    tab.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["UpdateUser"] != null)
                {
                    tab.UpdateUser = row["UpdateUser"].ToString();
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    tab.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
            }
            return tab;
        }

        public bool Delete(string Tabindex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ClientTab ");
            builder.Append(" where Tabindex=@Tabindex ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Tabindex", SqlDbType.VarChar, 100) };
            cmdParms[0].Value = Tabindex;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Tabindexlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ClientTab ");
            builder.Append(" where Tabindex in (" + Tabindexlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Tabindex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ClientTab");
            builder.Append(" where Tabindex=@Tabindex ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Tabindex", SqlDbType.VarChar, 100) };
            cmdParms[0].Value = Tabindex;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Tabindex,TabName,TabType,Remark,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM ClientTab ");
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
            builder.Append(" Tabindex,TabName,TabType,Remark,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM ClientTab ");
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
                builder.Append("order by T.Tabindex desc");
            }
            builder.Append(")AS Row, T.*  from ClientTab T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_ClientTab GetModel(string Tabindex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Tabindex,TabName,TabType,Remark,CreateUser,CreateTime,UpdateUser,UpdateTime from ClientTab ");
            builder.Append(" where Tabindex=@Tabindex ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Tabindex", SqlDbType.VarChar, 100) };
            cmdParms[0].Value = Tabindex;
            new Model_ClientTab();
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
            builder.Append("select count(1) FROM ClientTab ");
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

        public bool Update(Model_ClientTab model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ClientTab set ");
            builder.Append("TabName=@TabName,");
            builder.Append("TabType=@TabType,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UpdateUser=@UpdateUser,");
            builder.Append("UpdateTime=@UpdateTime");
            builder.Append(" where Tabindex=@Tabindex ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TabName", SqlDbType.VarChar, 100), new SqlParameter("@TabType", SqlDbType.Char, 1), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@Tabindex", SqlDbType.VarChar, 100) };
            cmdParms[0].Value = model.TabName;
            cmdParms[1].Value = model.TabType;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.UpdateUser;
            cmdParms[6].Value = model.UpdateTime;
            cmdParms[7].Value = model.Tabindex;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

