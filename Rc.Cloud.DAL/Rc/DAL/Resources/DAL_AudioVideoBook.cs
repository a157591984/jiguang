namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_AudioVideoBook
    {
        public bool Add(Model_AudioVideoBook model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into AudioVideoBook(");
            builder.Append("AudioVideoBookId,BookName,ParticularYear,GradeTerm,Resource_Version,Subject,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@AudioVideoBookId,@BookName,@ParticularYear,@GradeTerm,@Resource_Version,@Subject,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AudioVideoBookId", SqlDbType.Char, 0x24), new SqlParameter("@BookName", SqlDbType.NVarChar, 300), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.AudioVideoBookId;
            cmdParms[1].Value = model.BookName;
            cmdParms[2].Value = model.ParticularYear;
            cmdParms[3].Value = model.GradeTerm;
            cmdParms[4].Value = model.Resource_Version;
            cmdParms[5].Value = model.Subject;
            cmdParms[6].Value = model.CreateUser;
            cmdParms[7].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_AudioVideoBook DataRowToModel(DataRow row)
        {
            Model_AudioVideoBook book = new Model_AudioVideoBook();
            if (row != null)
            {
                if (row["AudioVideoBookId"] != null)
                {
                    book.AudioVideoBookId = row["AudioVideoBookId"].ToString();
                }
                if (row["BookName"] != null)
                {
                    book.BookName = row["BookName"].ToString();
                }
                if ((row["ParticularYear"] != null) && (row["ParticularYear"].ToString() != ""))
                {
                    book.ParticularYear = new int?(int.Parse(row["ParticularYear"].ToString()));
                }
                if (row["GradeTerm"] != null)
                {
                    book.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    book.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["Subject"] != null)
                {
                    book.Subject = row["Subject"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    book.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    book.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return book;
        }

        public bool Delete(string AudioVideoBookId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from AudioVideoBook ");
            builder.Append(" where AudioVideoBookId=@AudioVideoBookId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AudioVideoBookId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = AudioVideoBookId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string AudioVideoBookIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from AudioVideoBook ");
            builder.Append(" where AudioVideoBookId in (" + AudioVideoBookIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string AudioVideoBookId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from AudioVideoBook");
            builder.Append(" where AudioVideoBookId=@AudioVideoBookId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AudioVideoBookId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = AudioVideoBookId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select AudioVideoBookId,BookName,ParticularYear,GradeTerm,Resource_Version,Subject,CreateUser,CreateTime ");
            builder.Append(" FROM AudioVideoBook ");
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
            builder.Append(" AudioVideoBookId,BookName,ParticularYear,GradeTerm,Resource_Version,Subject,CreateUser,CreateTime ");
            builder.Append(" FROM AudioVideoBook ");
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
                builder.Append("order by T.AudioVideoBookId desc");
            }
            builder.Append(")AS Row, T.*  from AudioVideoBook T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_AudioVideoBook GetModel(string AudioVideoBookId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AudioVideoBookId,BookName,ParticularYear,GradeTerm,Resource_Version,Subject,CreateUser,CreateTime from AudioVideoBook ");
            builder.Append(" where AudioVideoBookId=@AudioVideoBookId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AudioVideoBookId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = AudioVideoBookId;
            new Model_AudioVideoBook();
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
            builder.Append("select count(1) FROM AudioVideoBook ");
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

        public bool Update(Model_AudioVideoBook model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update AudioVideoBook set ");
            builder.Append("BookName=@BookName,");
            builder.Append("ParticularYear=@ParticularYear,");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("Subject=@Subject,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where AudioVideoBookId=@AudioVideoBookId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookName", SqlDbType.NVarChar, 300), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@AudioVideoBookId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.BookName;
            cmdParms[1].Value = model.ParticularYear;
            cmdParms[2].Value = model.GradeTerm;
            cmdParms[3].Value = model.Resource_Version;
            cmdParms[4].Value = model.Subject;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            cmdParms[7].Value = model.AudioVideoBookId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

