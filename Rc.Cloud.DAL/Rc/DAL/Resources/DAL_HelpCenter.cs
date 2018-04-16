namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_HelpCenter
    {
        public bool Add(Model_HelpCenter model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HelpCenter(");
            builder.Append("help_id,help_title,help_content,create_time,create_userid)");
            builder.Append(" values (");
            builder.Append("@help_id,@help_title,@help_content,@create_time,@create_userid)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@help_id", SqlDbType.NChar, 0x24), new SqlParameter("@help_title", SqlDbType.NVarChar, 200), new SqlParameter("@help_content", SqlDbType.Text), new SqlParameter("@create_time", SqlDbType.DateTime), new SqlParameter("@create_userid", SqlDbType.NChar, 0x24) };
            cmdParms[0].Value = model.help_id;
            cmdParms[1].Value = model.help_title;
            cmdParms[2].Value = model.help_content;
            cmdParms[3].Value = model.create_time;
            cmdParms[4].Value = model.create_userid;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_HelpCenter DataRowToModel(DataRow row)
        {
            Model_HelpCenter center = new Model_HelpCenter();
            if (row != null)
            {
                if (row["help_id"] != null)
                {
                    center.help_id = row["help_id"].ToString();
                }
                if (row["help_title"] != null)
                {
                    center.help_title = row["help_title"].ToString();
                }
                if (row["help_content"] != null)
                {
                    center.help_content = row["help_content"].ToString();
                }
                if ((row["create_time"] != null) && (row["create_time"].ToString() != ""))
                {
                    center.create_time = new DateTime?(DateTime.Parse(row["create_time"].ToString()));
                }
                if (row["create_userid"] != null)
                {
                    center.create_userid = row["create_userid"].ToString();
                }
            }
            return center;
        }

        public bool Delete(string help_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from HelpCenter ");
            builder.Append(" where help_id=@help_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@help_id", SqlDbType.NChar, 0x24) };
            cmdParms[0].Value = help_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string help_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from HelpCenter ");
            builder.Append(" where help_id in (" + help_idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string help_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from HelpCenter");
            builder.Append(" where help_id=@help_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@help_id", SqlDbType.NChar, 0x24) };
            cmdParms[0].Value = help_id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select help_id,help_title,help_content,create_time,create_userid ");
            builder.Append(" FROM HelpCenter ");
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
            builder.Append(" help_id,help_title,help_content,create_time,create_userid ");
            builder.Append(" FROM HelpCenter ");
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
                builder.Append("order by T.help_id desc");
            }
            builder.Append(")AS Row, T.*  from HelpCenter T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_HelpCenter GetModel(string help_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 help_id,help_title,help_content,create_time,create_userid from HelpCenter ");
            builder.Append(" where help_id=@help_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@help_id", SqlDbType.NChar, 0x24) };
            cmdParms[0].Value = help_id;
            new Model_HelpCenter();
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
            builder.Append("select count(1) FROM HelpCenter ");
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

        public bool Update(Model_HelpCenter model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HelpCenter set ");
            builder.Append("help_title=@help_title,");
            builder.Append("help_content=@help_content,");
            builder.Append("create_time=@create_time,");
            builder.Append("create_userid=@create_userid");
            builder.Append(" where help_id=@help_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@help_title", SqlDbType.NVarChar, 200), new SqlParameter("@help_content", SqlDbType.Text), new SqlParameter("@create_time", SqlDbType.DateTime), new SqlParameter("@create_userid", SqlDbType.NChar, 0x24), new SqlParameter("@help_id", SqlDbType.NChar, 0x24) };
            cmdParms[0].Value = model.help_title;
            cmdParms[1].Value = model.help_content;
            cmdParms[2].Value = model.create_time;
            cmdParms[3].Value = model.create_userid;
            cmdParms[4].Value = model.help_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

