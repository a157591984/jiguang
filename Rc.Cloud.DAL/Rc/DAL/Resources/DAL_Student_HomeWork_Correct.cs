namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Student_HomeWork_Correct
    {
        public bool Add(Model_Student_HomeWork_Correct model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Student_HomeWork_Correct(");
            builder.Append("Student_HomeWork_Id,Student_HomeWork_CorrectStatus,CorrectTime,CorrectMode,CorrectUser)");
            builder.Append(" values (");
            builder.Append("@Student_HomeWork_Id,@Student_HomeWork_CorrectStatus,@CorrectTime,@CorrectMode,@CorrectUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_HomeWork_CorrectStatus", SqlDbType.Int, 4), new SqlParameter("@CorrectTime", SqlDbType.DateTime), new SqlParameter("@CorrectMode", SqlDbType.Char, 1), new SqlParameter("@CorrectUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Student_HomeWork_Id;
            cmdParms[1].Value = model.Student_HomeWork_CorrectStatus;
            cmdParms[2].Value = model.CorrectTime;
            cmdParms[3].Value = model.CorrectMode;
            cmdParms[4].Value = model.CorrectUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Student_HomeWork_Correct DataRowToModel(DataRow row)
        {
            Model_Student_HomeWork_Correct correct = new Model_Student_HomeWork_Correct();
            if (row != null)
            {
                if (row["Student_HomeWork_Id"] != null)
                {
                    correct.Student_HomeWork_Id = row["Student_HomeWork_Id"].ToString();
                }
                if ((row["Student_HomeWork_CorrectStatus"] != null) && (row["Student_HomeWork_CorrectStatus"].ToString() != ""))
                {
                    correct.Student_HomeWork_CorrectStatus = new int?(int.Parse(row["Student_HomeWork_CorrectStatus"].ToString()));
                }
                if ((row["CorrectTime"] != null) && (row["CorrectTime"].ToString() != ""))
                {
                    correct.CorrectTime = new DateTime?(DateTime.Parse(row["CorrectTime"].ToString()));
                }
                if (row["CorrectMode"] != null)
                {
                    correct.CorrectMode = row["CorrectMode"].ToString();
                }
                if (row["CorrectUser"] != null)
                {
                    correct.CorrectUser = row["CorrectUser"].ToString();
                }
            }
            return correct;
        }

        public bool Delete(string Student_HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_HomeWork_Correct ");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWork_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Student_HomeWork_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_HomeWork_Correct ");
            builder.Append(" where Student_HomeWork_Id in (" + Student_HomeWork_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Student_HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Student_HomeWork_Correct");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWork_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Student_HomeWork_Id,Student_HomeWork_CorrectStatus,CorrectTime,CorrectMode,CorrectUser ");
            builder.Append(" FROM Student_HomeWork_Correct ");
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
            builder.Append(" Student_HomeWork_Id,Student_HomeWork_CorrectStatus,CorrectTime,CorrectMode,CorrectUser ");
            builder.Append(" FROM Student_HomeWork_Correct ");
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
                builder.Append("order by T.Student_HomeWork_Id desc");
            }
            builder.Append(")AS Row, T.*  from Student_HomeWork_Correct T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Student_HomeWork_Correct GetModel(string Student_HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Student_HomeWork_Id,Student_HomeWork_CorrectStatus,CorrectTime,CorrectMode,CorrectUser from Student_HomeWork_Correct ");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWork_Id;
            new Model_Student_HomeWork_Correct();
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
            builder.Append("select count(1) FROM Student_HomeWork_Correct ");
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

        public bool Update(Model_Student_HomeWork_Correct model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Student_HomeWork_Correct set ");
            builder.Append("Student_HomeWork_CorrectStatus=@Student_HomeWork_CorrectStatus,");
            builder.Append("CorrectTime=@CorrectTime,");
            builder.Append("CorrectMode=@CorrectMode,");
            builder.Append("CorrectUser=@CorrectUser");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_CorrectStatus", SqlDbType.Int, 4), new SqlParameter("@CorrectTime", SqlDbType.DateTime), new SqlParameter("@CorrectMode", SqlDbType.Char, 1), new SqlParameter("@CorrectUser", SqlDbType.Char, 0x24), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Student_HomeWork_CorrectStatus;
            cmdParms[1].Value = model.CorrectTime;
            cmdParms[2].Value = model.CorrectMode;
            cmdParms[3].Value = model.CorrectUser;
            cmdParms[4].Value = model.Student_HomeWork_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

