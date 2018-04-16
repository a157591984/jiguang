namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_visit_web
    {
        public bool Add(Model_visit_web model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into visit_web(");
            builder.Append("visit_web_id,user_id,resource_data_id,class_id,open_time,close_time)");
            builder.Append(" values (");
            builder.Append("@visit_web_id,@user_id,@resource_data_id,@class_id,@open_time,@close_time)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@visit_web_id", SqlDbType.Char, 0x24), new SqlParameter("@user_id", SqlDbType.Char, 0x24), new SqlParameter("@resource_data_id", SqlDbType.Char, 0x24), new SqlParameter("@class_id", SqlDbType.VarChar, 50), new SqlParameter("@open_time", SqlDbType.DateTime), new SqlParameter("@close_time", SqlDbType.DateTime) };
            cmdParms[0].Value = model.visit_web_id;
            cmdParms[1].Value = model.user_id;
            cmdParms[2].Value = model.resource_data_id;
            cmdParms[3].Value = model.class_id;
            cmdParms[4].Value = model.open_time;
            cmdParms[5].Value = model.close_time;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_visit_web DataRowToModel(DataRow row)
        {
            Model_visit_web _web = new Model_visit_web();
            if (row != null)
            {
                if (row["visit_web_id"] != null)
                {
                    _web.visit_web_id = row["visit_web_id"].ToString();
                }
                if (row["user_id"] != null)
                {
                    _web.user_id = row["user_id"].ToString();
                }
                if (row["resource_data_id"] != null)
                {
                    _web.resource_data_id = row["resource_data_id"].ToString();
                }
                if (row["class_id"] != null)
                {
                    _web.class_id = row["class_id"].ToString();
                }
                if ((row["open_time"] != null) && (row["open_time"].ToString() != ""))
                {
                    _web.open_time = new DateTime?(DateTime.Parse(row["open_time"].ToString()));
                }
                if ((row["close_time"] != null) && (row["close_time"].ToString() != ""))
                {
                    _web.close_time = new DateTime?(DateTime.Parse(row["close_time"].ToString()));
                }
            }
            return _web;
        }

        public bool Delete(string visit_web_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from visit_web ");
            builder.Append(" where visit_web_id=@visit_web_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@visit_web_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = visit_web_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string visit_web_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from visit_web ");
            builder.Append(" where visit_web_id in (" + visit_web_idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string visit_web_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from visit_web");
            builder.Append(" where visit_web_id=@visit_web_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@visit_web_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = visit_web_id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select visit_web_id,user_id,resource_data_id,class_id,open_time,close_time ");
            builder.Append(" FROM visit_web ");
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
            builder.Append(" visit_web_id,user_id,resource_data_id,class_id,open_time,close_time ");
            builder.Append(" FROM visit_web ");
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
                builder.Append("order by T.visit_web_id desc");
            }
            builder.Append(")AS Row, T.*  from visit_web T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_visit_web GetModel(string visit_web_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 visit_web_id,user_id,resource_data_id,class_id,open_time,close_time from visit_web ");
            builder.Append(" where visit_web_id=@visit_web_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@visit_web_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = visit_web_id;
            new Model_visit_web();
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
            builder.Append("select count(1) FROM visit_web ");
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

        public bool Update(Model_visit_web model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update visit_web set ");
            builder.Append("user_id=@user_id,");
            builder.Append("resource_data_id=@resource_data_id,");
            builder.Append("class_id=@class_id,");
            builder.Append("open_time=@open_time,");
            builder.Append("close_time=@close_time");
            builder.Append(" where visit_web_id=@visit_web_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@user_id", SqlDbType.Char, 0x24), new SqlParameter("@resource_data_id", SqlDbType.Char, 0x24), new SqlParameter("@class_id", SqlDbType.VarChar, 50), new SqlParameter("@open_time", SqlDbType.DateTime), new SqlParameter("@close_time", SqlDbType.DateTime), new SqlParameter("@visit_web_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.user_id;
            cmdParms[1].Value = model.resource_data_id;
            cmdParms[2].Value = model.class_id;
            cmdParms[3].Value = model.open_time;
            cmdParms[4].Value = model.close_time;
            cmdParms[5].Value = model.visit_web_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

