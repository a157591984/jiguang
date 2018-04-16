namespace Rc.DAL
{
    using Rc.Common.DBUtility;
    using Rc.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_NoticeBoard
    {
        public bool Add(Model_NoticeBoard model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into NoticeBoard(");
            builder.Append("notice_id,notice_title,notice_content,create_time,create_userid,start_time,end_time)");
            builder.Append(" values (");
            builder.Append("@notice_id,@notice_title,@notice_content,@create_time,@create_userid,@start_time,@end_time)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@notice_id", SqlDbType.NChar, 0x24), new SqlParameter("@notice_title", SqlDbType.NVarChar, 200), new SqlParameter("@notice_content", SqlDbType.Text), new SqlParameter("@create_time", SqlDbType.DateTime), new SqlParameter("@create_userid", SqlDbType.NChar, 0x24), new SqlParameter("@start_time", SqlDbType.DateTime), new SqlParameter("@end_time", SqlDbType.DateTime) };
            cmdParms[0].Value = model.notice_id;
            cmdParms[1].Value = model.notice_title;
            cmdParms[2].Value = model.notice_content;
            cmdParms[3].Value = model.create_time;
            cmdParms[4].Value = model.create_userid;
            cmdParms[5].Value = model.start_time;
            cmdParms[6].Value = model.end_time;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_NoticeBoard DataRowToModel(DataRow row)
        {
            Model_NoticeBoard board = new Model_NoticeBoard();
            if (row != null)
            {
                if (row["notice_id"] != null)
                {
                    board.notice_id = row["notice_id"].ToString();
                }
                if (row["notice_title"] != null)
                {
                    board.notice_title = row["notice_title"].ToString();
                }
                if (row["notice_content"] != null)
                {
                    board.notice_content = row["notice_content"].ToString();
                }
                if ((row["create_time"] != null) && (row["create_time"].ToString() != ""))
                {
                    board.create_time = new DateTime?(DateTime.Parse(row["create_time"].ToString()));
                }
                if (row["create_userid"] != null)
                {
                    board.create_userid = row["create_userid"].ToString();
                }
                if ((row["start_time"] != null) && (row["start_time"].ToString() != ""))
                {
                    board.start_time = new DateTime?(DateTime.Parse(row["start_time"].ToString()));
                }
                if ((row["end_time"] != null) && (row["end_time"].ToString() != ""))
                {
                    board.end_time = new DateTime?(DateTime.Parse(row["end_time"].ToString()));
                }
            }
            return board;
        }

        public bool Delete(string notice_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from NoticeBoard ");
            builder.Append(" where notice_id=@notice_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@notice_id", SqlDbType.NChar, 0x24) };
            cmdParms[0].Value = notice_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string notice_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from NoticeBoard ");
            builder.Append(" where notice_id in (" + notice_idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string notice_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from NoticeBoard");
            builder.Append(" where notice_id=@notice_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@notice_id", SqlDbType.NChar, 0x24) };
            cmdParms[0].Value = notice_id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select notice_id,notice_title,notice_content,create_time,create_userid,start_time,end_time ");
            builder.Append(" FROM NoticeBoard ");
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
            builder.Append(" notice_id,notice_title,notice_content,create_time,create_userid,start_time,end_time ");
            builder.Append(" FROM NoticeBoard ");
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
                builder.Append("order by T.notice_id desc");
            }
            builder.Append(")AS Row, T.*  from NoticeBoard T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_NoticeBoard GetModel(string notice_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 notice_id,notice_title,notice_content,create_time,create_userid,start_time,end_time from NoticeBoard ");
            builder.Append(" where notice_id=@notice_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@notice_id", SqlDbType.NChar, 0x24) };
            cmdParms[0].Value = notice_id;
            new Model_NoticeBoard();
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
            builder.Append("select count(1) FROM NoticeBoard ");
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

        public bool Update(Model_NoticeBoard model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update NoticeBoard set ");
            builder.Append("notice_title=@notice_title,");
            builder.Append("notice_content=@notice_content,");
            builder.Append("create_time=@create_time,");
            builder.Append("create_userid=@create_userid,");
            builder.Append("start_time=@start_time,");
            builder.Append("end_time=@end_time");
            builder.Append(" where notice_id=@notice_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@notice_title", SqlDbType.NVarChar, 200), new SqlParameter("@notice_content", SqlDbType.Text), new SqlParameter("@create_time", SqlDbType.DateTime), new SqlParameter("@create_userid", SqlDbType.NChar, 0x24), new SqlParameter("@start_time", SqlDbType.DateTime), new SqlParameter("@end_time", SqlDbType.DateTime), new SqlParameter("@notice_id", SqlDbType.NChar, 0x24) };
            cmdParms[0].Value = model.notice_title;
            cmdParms[1].Value = model.notice_content;
            cmdParms[2].Value = model.create_time;
            cmdParms[3].Value = model.create_userid;
            cmdParms[4].Value = model.start_time;
            cmdParms[5].Value = model.end_time;
            cmdParms[6].Value = model.notice_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

