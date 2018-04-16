namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_BookshelvesLog
    {
        public bool Add(Model_BookshelvesLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into BookshelvesLog(");
            builder.Append("BookshelvesLog_Id,BookId,LogTypeEnum,CreateUser,CreateTime,LogRemark)");
            builder.Append(" values (");
            builder.Append("@BookshelvesLog_Id,@BookId,@LogTypeEnum,@CreateUser,@CreateTime,@LogRemark)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookshelvesLog_Id", SqlDbType.Char, 0x24), new SqlParameter("@BookId", SqlDbType.Char, 0x24), new SqlParameter("@LogTypeEnum", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@LogRemark", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.BookshelvesLog_Id;
            cmdParms[1].Value = model.BookId;
            cmdParms[2].Value = model.LogTypeEnum;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.LogRemark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_BookshelvesLog DataRowToModel(DataRow row)
        {
            Model_BookshelvesLog log = new Model_BookshelvesLog();
            if (row != null)
            {
                if (row["BookshelvesLog_Id"] != null)
                {
                    log.BookshelvesLog_Id = row["BookshelvesLog_Id"].ToString();
                }
                if (row["BookId"] != null)
                {
                    log.BookId = row["BookId"].ToString();
                }
                if (row["LogTypeEnum"] != null)
                {
                    log.LogTypeEnum = row["LogTypeEnum"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    log.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    log.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["LogRemark"] != null)
                {
                    log.LogRemark = row["LogRemark"].ToString();
                }
            }
            return log;
        }

        public bool Delete(string BookshelvesLog_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookshelvesLog ");
            builder.Append(" where BookshelvesLog_Id=@BookshelvesLog_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookshelvesLog_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookshelvesLog_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string BookshelvesLog_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookshelvesLog ");
            builder.Append(" where BookshelvesLog_Id in (" + BookshelvesLog_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string BookshelvesLog_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from BookshelvesLog");
            builder.Append(" where BookshelvesLog_Id=@BookshelvesLog_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookshelvesLog_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookshelvesLog_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select BookshelvesLog_Id,BookId,LogTypeEnum,CreateUser,CreateTime,LogRemark ");
            builder.Append(" FROM BookshelvesLog ");
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
            builder.Append(" BookshelvesLog_Id,BookId,LogTypeEnum,CreateUser,CreateTime,LogRemark ");
            builder.Append(" FROM BookshelvesLog ");
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
                builder.Append("order by T.BookshelvesLog_Id desc");
            }
            builder.Append(")AS Row, T.*  from BookshelvesLog T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_BookshelvesLog GetModel(string BookshelvesLog_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 BookshelvesLog_Id,BookId,LogTypeEnum,CreateUser,CreateTime,LogRemark from BookshelvesLog ");
            builder.Append(" where BookshelvesLog_Id=@BookshelvesLog_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookshelvesLog_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookshelvesLog_Id;
            new Model_BookshelvesLog();
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
            builder.Append("select count(1) FROM BookshelvesLog ");
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

        public bool Update(Model_BookshelvesLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update BookshelvesLog set ");
            builder.Append("BookId=@BookId,");
            builder.Append("LogTypeEnum=@LogTypeEnum,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("LogRemark=@LogRemark");
            builder.Append(" where BookshelvesLog_Id=@BookshelvesLog_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookId", SqlDbType.Char, 0x24), new SqlParameter("@LogTypeEnum", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@LogRemark", SqlDbType.NVarChar, 200), new SqlParameter("@BookshelvesLog_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.BookId;
            cmdParms[1].Value = model.LogTypeEnum;
            cmdParms[2].Value = model.CreateUser;
            cmdParms[3].Value = model.CreateTime;
            cmdParms[4].Value = model.LogRemark;
            cmdParms[5].Value = model.BookshelvesLog_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

