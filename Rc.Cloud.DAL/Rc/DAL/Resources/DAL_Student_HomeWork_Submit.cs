namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Student_HomeWork_Submit
    {
        public bool Add(Model_Student_HomeWork_Submit model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Student_HomeWork_Submit(");
            builder.Append("Student_HomeWork_Id,Student_HomeWork_Status,OpenTime,StudentIP,Student_Answer_Time)");
            builder.Append(" values (");
            builder.Append("@Student_HomeWork_Id,@Student_HomeWork_Status,@OpenTime,@StudentIP,@Student_Answer_Time)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_HomeWork_Status", SqlDbType.Int, 4), new SqlParameter("@OpenTime", SqlDbType.DateTime), new SqlParameter("@StudentIP", SqlDbType.Char, 0x24), new SqlParameter("@Student_Answer_Time", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Student_HomeWork_Id;
            cmdParms[1].Value = model.Student_HomeWork_Status;
            cmdParms[2].Value = model.OpenTime;
            cmdParms[3].Value = model.StudentIP;
            cmdParms[4].Value = model.Student_Answer_Time;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Student_HomeWork_Submit DataRowToModel(DataRow row)
        {
            Model_Student_HomeWork_Submit submit = new Model_Student_HomeWork_Submit();
            if (row != null)
            {
                if (row["Student_HomeWork_Id"] != null)
                {
                    submit.Student_HomeWork_Id = row["Student_HomeWork_Id"].ToString();
                }
                if ((row["Student_HomeWork_Status"] != null) && (row["Student_HomeWork_Status"].ToString() != ""))
                {
                    submit.Student_HomeWork_Status = new int?(int.Parse(row["Student_HomeWork_Status"].ToString()));
                }
                if ((row["OpenTime"] != null) && (row["OpenTime"].ToString() != ""))
                {
                    submit.OpenTime = new DateTime?(DateTime.Parse(row["OpenTime"].ToString()));
                }
                if (row["StudentIP"] != null)
                {
                    submit.StudentIP = row["StudentIP"].ToString();
                }
                if ((row["Student_Answer_Time"] != null) && (row["Student_Answer_Time"].ToString() != ""))
                {
                    submit.Student_Answer_Time = new DateTime?(DateTime.Parse(row["Student_Answer_Time"].ToString()));
                }
            }
            return submit;
        }

        public bool Delete(string Student_HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_HomeWork_Submit ");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWork_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Student_HomeWork_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_HomeWork_Submit ");
            builder.Append(" where Student_HomeWork_Id in (" + Student_HomeWork_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Student_HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Student_HomeWork_Submit");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWork_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Student_HomeWork_Id,Student_HomeWork_Status,OpenTime,StudentIP,Student_Answer_Time ");
            builder.Append(" FROM Student_HomeWork_Submit ");
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
            builder.Append(" Student_HomeWork_Id,Student_HomeWork_Status,OpenTime,StudentIP,Student_Answer_Time ");
            builder.Append(" FROM Student_HomeWork_Submit ");
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
            builder.Append(")AS Row, T.*  from Student_HomeWork_Submit T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Student_HomeWork_Submit GetModel(string Student_HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Student_HomeWork_Id,Student_HomeWork_Status,OpenTime,StudentIP,Student_Answer_Time from Student_HomeWork_Submit ");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWork_Id;
            new Model_Student_HomeWork_Submit();
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
            builder.Append("select count(1) FROM Student_HomeWork_Submit ");
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

        public bool Update(Model_Student_HomeWork_Submit model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Student_HomeWork_Submit set ");
            builder.Append("Student_HomeWork_Status=@Student_HomeWork_Status,");
            builder.Append("OpenTime=@OpenTime,");
            builder.Append("StudentIP=@StudentIP,");
            builder.Append("Student_Answer_Time=@Student_Answer_Time");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Status", SqlDbType.Int, 4), new SqlParameter("@OpenTime", SqlDbType.DateTime), new SqlParameter("@StudentIP", SqlDbType.Char, 0x24), new SqlParameter("@Student_Answer_Time", SqlDbType.DateTime), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Student_HomeWork_Status;
            cmdParms[1].Value = model.OpenTime;
            cmdParms[2].Value = model.StudentIP;
            cmdParms[3].Value = model.Student_Answer_Time;
            cmdParms[4].Value = model.Student_HomeWork_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

