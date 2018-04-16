namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_BookProductionLog
    {
        public bool Add(Model_BookProductionLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into BookProductionLog(");
            builder.Append("BookProductionLog_Id,BookId,ResourceToResourceFolder_Id,ParticularYear,Resource_Type,LogTypeEnum,CreateUser,CreateTime,LogRemark)");
            builder.Append(" values (");
            builder.Append("@BookProductionLog_Id,@BookId,@ResourceToResourceFolder_Id,@ParticularYear,@Resource_Type,@LogTypeEnum,@CreateUser,@CreateTime,@LogRemark)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookProductionLog_Id", SqlDbType.Char, 0x24), new SqlParameter("@BookId", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@LogTypeEnum", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@LogRemark", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.BookProductionLog_Id;
            cmdParms[1].Value = model.BookId;
            cmdParms[2].Value = model.ResourceToResourceFolder_Id;
            cmdParms[3].Value = model.ParticularYear;
            cmdParms[4].Value = model.Resource_Type;
            cmdParms[5].Value = model.LogTypeEnum;
            cmdParms[6].Value = model.CreateUser;
            cmdParms[7].Value = model.CreateTime;
            cmdParms[8].Value = model.LogRemark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_BookProductionLog DataRowToModel(DataRow row)
        {
            Model_BookProductionLog log = new Model_BookProductionLog();
            if (row != null)
            {
                if (row["BookProductionLog_Id"] != null)
                {
                    log.BookProductionLog_Id = row["BookProductionLog_Id"].ToString();
                }
                if (row["BookId"] != null)
                {
                    log.BookId = row["BookId"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    log.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if ((row["ParticularYear"] != null) && (row["ParticularYear"].ToString() != ""))
                {
                    log.ParticularYear = int.Parse(row["ParticularYear"].ToString());
                }
                if (row["Resource_Type"] != null)
                {
                    log.Resource_Type = row["Resource_Type"].ToString();
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
                    log.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["LogRemark"] != null)
                {
                    log.LogRemark = row["LogRemark"].ToString();
                }
            }
            return log;
        }

        public bool Delete(string BookProductionLog_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookProductionLog ");
            builder.Append(" where BookProductionLog_Id=@BookProductionLog_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookProductionLog_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookProductionLog_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string BookProductionLog_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookProductionLog ");
            builder.Append(" where BookProductionLog_Id in (" + BookProductionLog_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string BookProductionLog_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from BookProductionLog");
            builder.Append(" where BookProductionLog_Id=@BookProductionLog_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookProductionLog_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookProductionLog_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select BookProductionLog_Id,BookId,ResourceToResourceFolder_Id,ParticularYear,Resource_Type,LogTypeEnum,CreateUser,CreateTime,LogRemark ");
            builder.Append(" FROM BookProductionLog ");
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
            builder.Append(" BookProductionLog_Id,BookId,ResourceToResourceFolder_Id,ParticularYear,Resource_Type,LogTypeEnum,CreateUser,CreateTime,LogRemark ");
            builder.Append(" FROM BookProductionLog ");
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
                builder.Append("order by T.BookProductionLog_Id desc");
            }
            builder.Append(")AS Row, T.*  from BookProductionLog T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_BookProductionLog GetModel(string BookProductionLog_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 BookProductionLog_Id,BookId,ResourceToResourceFolder_Id,ParticularYear,Resource_Type,LogTypeEnum,CreateUser,CreateTime,LogRemark from BookProductionLog ");
            builder.Append(" where BookProductionLog_Id=@BookProductionLog_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookProductionLog_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookProductionLog_Id;
            new Model_BookProductionLog();
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
            builder.Append("select count(1) FROM BookProductionLog ");
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

        public bool Update(Model_BookProductionLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update BookProductionLog set ");
            builder.Append("BookId=@BookId,");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("ParticularYear=@ParticularYear,");
            builder.Append("Resource_Type=@Resource_Type,");
            builder.Append("LogTypeEnum=@LogTypeEnum,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("LogRemark=@LogRemark");
            builder.Append(" where BookProductionLog_Id=@BookProductionLog_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookId", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@LogTypeEnum", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@LogRemark", SqlDbType.NVarChar, 200), new SqlParameter("@BookProductionLog_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.BookId;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.ParticularYear;
            cmdParms[3].Value = model.Resource_Type;
            cmdParms[4].Value = model.LogTypeEnum;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            cmdParms[7].Value = model.LogRemark;
            cmdParms[8].Value = model.BookProductionLog_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

