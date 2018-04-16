namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_ClientTabToTab
    {
        public bool Add(Model_ClientTabToTab model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ClientTabToTab(");
            builder.Append("ClientTabToTab_Id,Tabindex,ToTabindex,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@ClientTabToTab_Id,@Tabindex,@ToTabindex,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClientTabToTab_Id", SqlDbType.Char, 0x24), new SqlParameter("@Tabindex", SqlDbType.VarChar, 100), new SqlParameter("@ToTabindex", SqlDbType.VarChar, 100), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.ClientTabToTab_Id;
            cmdParms[1].Value = model.Tabindex;
            cmdParms[2].Value = model.ToTabindex;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_ClientTabToTab DataRowToModel(DataRow row)
        {
            Model_ClientTabToTab tab = new Model_ClientTabToTab();
            if (row != null)
            {
                if (row["ClientTabToTab_Id"] != null)
                {
                    tab.ClientTabToTab_Id = row["ClientTabToTab_Id"].ToString();
                }
                if (row["Tabindex"] != null)
                {
                    tab.Tabindex = row["Tabindex"].ToString();
                }
                if (row["ToTabindex"] != null)
                {
                    tab.ToTabindex = row["ToTabindex"].ToString();
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
            }
            return tab;
        }

        public bool Delete(string Tabindex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ClientTabToTab ");
            builder.Append(" where Tabindex=@Tabindex ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Tabindex", SqlDbType.VarChar, 100) };
            cmdParms[0].Value = Tabindex;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Tabindexlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ClientTabToTab ");
            builder.Append(" where Tabindex in (" + Tabindexlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Tabindex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ClientTabToTab");
            builder.Append(" where Tabindex=@Tabindex ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Tabindex", SqlDbType.VarChar, 100) };
            cmdParms[0].Value = Tabindex;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ClientTabToTab_Id,Tabindex,ToTabindex,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM ClientTabToTab ");
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
            builder.Append(" ClientTabToTab_Id,Tabindex,ToTabindex,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM ClientTabToTab ");
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
            builder.Append(")AS Row, T.*  from ClientTabToTab T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_ClientTabToTab GetModel(string Tabindex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ClientTabToTab_Id,Tabindex,ToTabindex,Remark,CreateUser,CreateTime from ClientTabToTab ");
            builder.Append(" where Tabindex=@Tabindex ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Tabindex", SqlDbType.VarChar, 100) };
            cmdParms[0].Value = Tabindex;
            new Model_ClientTabToTab();
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
            builder.Append("select count(1) FROM ClientTabToTab ");
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

        public bool Update(Model_ClientTabToTab model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ClientTabToTab set ");
            builder.Append("ClientTabToTab_Id=@ClientTabToTab_Id,");
            builder.Append("ToTabindex=@ToTabindex,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where Tabindex=@Tabindex ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClientTabToTab_Id", SqlDbType.Char, 0x24), new SqlParameter("@ToTabindex", SqlDbType.VarChar, 100), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Tabindex", SqlDbType.VarChar, 100) };
            cmdParms[0].Value = model.ClientTabToTab_Id;
            cmdParms[1].Value = model.ToTabindex;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.Tabindex;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

