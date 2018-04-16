namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Student_WrongHomeWork
    {
        public bool Add(Model_Student_WrongHomeWork model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Student_WrongHomeWork(");
            builder.Append("Student_WrongHomeWork_Id,Student_HomeWorkAnswer_Id,CreateTime)");
            builder.Append(" values (");
            builder.Append("@Student_WrongHomeWork_Id,@Student_HomeWorkAnswer_Id,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_WrongHomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_HomeWorkAnswer_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Student_WrongHomeWork_Id;
            cmdParms[1].Value = model.Student_HomeWorkAnswer_Id;
            cmdParms[2].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Student_WrongHomeWork DataRowToModel(DataRow row)
        {
            Model_Student_WrongHomeWork work = new Model_Student_WrongHomeWork();
            if (row != null)
            {
                if (row["Student_WrongHomeWork_Id"] != null)
                {
                    work.Student_WrongHomeWork_Id = row["Student_WrongHomeWork_Id"].ToString();
                }
                if (row["Student_HomeWorkAnswer_Id"] != null)
                {
                    work.Student_HomeWorkAnswer_Id = row["Student_HomeWorkAnswer_Id"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    work.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return work;
        }

        public bool Delete(string Student_WrongHomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_WrongHomeWork ");
            builder.Append(" where Student_WrongHomeWork_Id=@Student_WrongHomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_WrongHomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_WrongHomeWork_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Student_WrongHomeWork_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_WrongHomeWork ");
            builder.Append(" where Student_WrongHomeWork_Id in (" + Student_WrongHomeWork_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Student_WrongHomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Student_WrongHomeWork");
            builder.Append(" where Student_WrongHomeWork_Id=@Student_WrongHomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_WrongHomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_WrongHomeWork_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Student_WrongHomeWork_Id,Student_HomeWorkAnswer_Id,CreateTime ");
            builder.Append(" FROM Student_WrongHomeWork ");
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
            builder.Append(" Student_WrongHomeWork_Id,Student_HomeWorkAnswer_Id,CreateTime ");
            builder.Append(" FROM Student_WrongHomeWork ");
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
                builder.Append("order by T.Student_WrongHomeWork_Id desc");
            }
            builder.Append(")AS Row, T.*  from Student_WrongHomeWork T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Student_WrongHomeWork GetModel(string Student_WrongHomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Student_WrongHomeWork_Id,Student_HomeWorkAnswer_Id,CreateTime from Student_WrongHomeWork ");
            builder.Append(" where Student_WrongHomeWork_Id=@Student_WrongHomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_WrongHomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_WrongHomeWork_Id;
            new Model_Student_WrongHomeWork();
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
            builder.Append("select count(1) FROM Student_WrongHomeWork ");
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

        public bool Update(Model_Student_WrongHomeWork model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Student_WrongHomeWork set ");
            builder.Append("Student_HomeWorkAnswer_Id=@Student_HomeWorkAnswer_Id,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where Student_WrongHomeWork_Id=@Student_WrongHomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWorkAnswer_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Student_WrongHomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Student_HomeWorkAnswer_Id;
            cmdParms[1].Value = model.CreateTime;
            cmdParms[2].Value = model.Student_WrongHomeWork_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

