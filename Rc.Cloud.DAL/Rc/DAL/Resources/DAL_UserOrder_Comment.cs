namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_UserOrder_Comment
    {
        public bool Add(Model_UserOrder_Comment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into UserOrder_Comment(");
            builder.Append("comment_id,order_num,user_id,comment_content,comment_evaluate,create_time,remark)");
            builder.Append(" values (");
            builder.Append("@comment_id,@order_num,@user_id,@comment_content,@comment_evaluate,@create_time,@remark)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@comment_id", SqlDbType.VarChar, 0x24), new SqlParameter("@order_num", SqlDbType.VarChar, 50), new SqlParameter("@user_id", SqlDbType.VarChar, 0x24), new SqlParameter("@comment_content", SqlDbType.VarChar, 0x7d0), new SqlParameter("@comment_evaluate", SqlDbType.Decimal, 5), new SqlParameter("@create_time", SqlDbType.DateTime), new SqlParameter("@remark", SqlDbType.VarChar, 0x7d0) };
            cmdParms[0].Value = model.comment_id;
            cmdParms[1].Value = model.order_num;
            cmdParms[2].Value = model.user_id;
            cmdParms[3].Value = model.comment_content;
            cmdParms[4].Value = model.comment_evaluate;
            cmdParms[5].Value = model.create_time;
            cmdParms[6].Value = model.remark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_UserOrder_Comment DataRowToModel(DataRow row)
        {
            Model_UserOrder_Comment comment = new Model_UserOrder_Comment();
            if (row != null)
            {
                if (row["comment_id"] != null)
                {
                    comment.comment_id = row["comment_id"].ToString();
                }
                if (row["order_num"] != null)
                {
                    comment.order_num = row["order_num"].ToString();
                }
                if (row["user_id"] != null)
                {
                    comment.user_id = row["user_id"].ToString();
                }
                if (row["comment_content"] != null)
                {
                    comment.comment_content = row["comment_content"].ToString();
                }
                if ((row["comment_evaluate"] != null) && (row["comment_evaluate"].ToString() != ""))
                {
                    comment.comment_evaluate = new decimal?(decimal.Parse(row["comment_evaluate"].ToString()));
                }
                if ((row["create_time"] != null) && (row["create_time"].ToString() != ""))
                {
                    comment.create_time = new DateTime?(DateTime.Parse(row["create_time"].ToString()));
                }
                if (row["remark"] != null)
                {
                    comment.remark = row["remark"].ToString();
                }
            }
            return comment;
        }

        public bool Delete(string comment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from UserOrder_Comment ");
            builder.Append(" where comment_id=@comment_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@comment_id", SqlDbType.VarChar, 0x24) };
            cmdParms[0].Value = comment_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string comment_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from UserOrder_Comment ");
            builder.Append(" where comment_id in (" + comment_idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string comment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from UserOrder_Comment");
            builder.Append(" where comment_id=@comment_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@comment_id", SqlDbType.VarChar, 0x24) };
            cmdParms[0].Value = comment_id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select comment_id,order_num,user_id,comment_content,comment_evaluate,create_time,remark ");
            builder.Append(" FROM UserOrder_Comment ");
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
            builder.Append(" comment_id,order_num,user_id,comment_content,comment_evaluate,create_time,remark ");
            builder.Append(" FROM UserOrder_Comment ");
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
                builder.Append("order by T.comment_id desc");
            }
            builder.Append(")AS Row, T.*  from UserOrder_Comment T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_UserOrder_Comment GetModel(string comment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 comment_id,order_num,user_id,comment_content,comment_evaluate,create_time,remark from UserOrder_Comment ");
            builder.Append(" where comment_id=@comment_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@comment_id", SqlDbType.VarChar, 0x24) };
            cmdParms[0].Value = comment_id;
            new Model_UserOrder_Comment();
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
            builder.Append("select count(1) FROM UserOrder_Comment ");
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

        public bool Update(Model_UserOrder_Comment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update UserOrder_Comment set ");
            builder.Append("order_num=@order_num,");
            builder.Append("user_id=@user_id,");
            builder.Append("comment_content=@comment_content,");
            builder.Append("comment_evaluate=@comment_evaluate,");
            builder.Append("create_time=@create_time,");
            builder.Append("remark=@remark");
            builder.Append(" where comment_id=@comment_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@order_num", SqlDbType.VarChar, 50), new SqlParameter("@user_id", SqlDbType.VarChar, 0x24), new SqlParameter("@comment_content", SqlDbType.VarChar, 0x7d0), new SqlParameter("@comment_evaluate", SqlDbType.Decimal, 5), new SqlParameter("@create_time", SqlDbType.DateTime), new SqlParameter("@remark", SqlDbType.VarChar, 0x7d0), new SqlParameter("@comment_id", SqlDbType.VarChar, 0x24) };
            cmdParms[0].Value = model.order_num;
            cmdParms[1].Value = model.user_id;
            cmdParms[2].Value = model.comment_content;
            cmdParms[3].Value = model.comment_evaluate;
            cmdParms[4].Value = model.create_time;
            cmdParms[5].Value = model.remark;
            cmdParms[6].Value = model.comment_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

