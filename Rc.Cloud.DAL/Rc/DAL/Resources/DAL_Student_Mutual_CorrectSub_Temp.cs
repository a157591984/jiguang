namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Student_Mutual_CorrectSub_Temp
    {
        public bool Add(Model_Student_Mutual_CorrectSub_Temp model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Student_Mutual_CorrectSub_Temp(");
            builder.Append("Student_Mutual_CorrectSub_Temp_Id,Student_Mutual_Correct_Temp_Id,Correct_Guid,HomeWork_Id,Student_HomeWork_Id,Student_Id,CreateTime,Remark)");
            builder.Append(" values (");
            builder.Append("@Student_Mutual_CorrectSub_Temp_Id,@Student_Mutual_Correct_Temp_Id,@Correct_Guid,@HomeWork_Id,@Student_HomeWork_Id,@Student_Id,@CreateTime,@Remark)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_Mutual_CorrectSub_Temp_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Mutual_Correct_Temp_Id", SqlDbType.Char, 0x24), new SqlParameter("@Correct_Guid", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0) };
            cmdParms[0].Value = model.Student_Mutual_CorrectSub_Temp_Id;
            cmdParms[1].Value = model.Student_Mutual_Correct_Temp_Id;
            cmdParms[2].Value = model.Correct_Guid;
            cmdParms[3].Value = model.HomeWork_Id;
            cmdParms[4].Value = model.Student_HomeWork_Id;
            cmdParms[5].Value = model.Student_Id;
            cmdParms[6].Value = model.CreateTime;
            cmdParms[7].Value = model.Remark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Student_Mutual_CorrectSub_Temp DataRowToModel(DataRow row)
        {
            Model_Student_Mutual_CorrectSub_Temp temp = new Model_Student_Mutual_CorrectSub_Temp();
            if (row != null)
            {
                if (row["Student_Mutual_CorrectSub_Temp_Id"] != null)
                {
                    temp.Student_Mutual_CorrectSub_Temp_Id = row["Student_Mutual_CorrectSub_Temp_Id"].ToString();
                }
                if (row["Student_Mutual_Correct_Temp_Id"] != null)
                {
                    temp.Student_Mutual_Correct_Temp_Id = row["Student_Mutual_Correct_Temp_Id"].ToString();
                }
                if (row["Correct_Guid"] != null)
                {
                    temp.Correct_Guid = row["Correct_Guid"].ToString();
                }
                if (row["HomeWork_Id"] != null)
                {
                    temp.HomeWork_Id = row["HomeWork_Id"].ToString();
                }
                if (row["Student_HomeWork_Id"] != null)
                {
                    temp.Student_HomeWork_Id = row["Student_HomeWork_Id"].ToString();
                }
                if (row["Student_Id"] != null)
                {
                    temp.Student_Id = row["Student_Id"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    temp.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    temp.Remark = row["Remark"].ToString();
                }
            }
            return temp;
        }

        public bool Delete(string Student_Mutual_CorrectSub_Temp_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_Mutual_CorrectSub_Temp ");
            builder.Append(" where Student_Mutual_CorrectSub_Temp_Id=@Student_Mutual_CorrectSub_Temp_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_Mutual_CorrectSub_Temp_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_Mutual_CorrectSub_Temp_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Student_Mutual_CorrectSub_Temp_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_Mutual_CorrectSub_Temp ");
            builder.Append(" where Student_Mutual_CorrectSub_Temp_Id in (" + Student_Mutual_CorrectSub_Temp_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Student_Mutual_CorrectSub_Temp_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Student_Mutual_CorrectSub_Temp");
            builder.Append(" where Student_Mutual_CorrectSub_Temp_Id=@Student_Mutual_CorrectSub_Temp_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_Mutual_CorrectSub_Temp_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_Mutual_CorrectSub_Temp_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Student_Mutual_CorrectSub_Temp_Id,Student_Mutual_Correct_Temp_Id,Correct_Guid,HomeWork_Id,Student_HomeWork_Id,Student_Id,CreateTime,Remark ");
            builder.Append(" FROM Student_Mutual_CorrectSub_Temp ");
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
            builder.Append(" Student_Mutual_CorrectSub_Temp_Id,Student_Mutual_Correct_Temp_Id,Correct_Guid,HomeWork_Id,Student_HomeWork_Id,Student_Id,CreateTime,Remark ");
            builder.Append(" FROM Student_Mutual_CorrectSub_Temp ");
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
                builder.Append("order by T.Student_Mutual_CorrectSub_Temp_Id desc");
            }
            builder.Append(")AS Row, T.*  from Student_Mutual_CorrectSub_Temp T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Student_Mutual_CorrectSub_Temp GetModel(string Student_Mutual_CorrectSub_Temp_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Student_Mutual_CorrectSub_Temp_Id,Student_Mutual_Correct_Temp_Id,Correct_Guid,HomeWork_Id,Student_HomeWork_Id,Student_Id,CreateTime,Remark from Student_Mutual_CorrectSub_Temp ");
            builder.Append(" where Student_Mutual_CorrectSub_Temp_Id=@Student_Mutual_CorrectSub_Temp_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_Mutual_CorrectSub_Temp_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_Mutual_CorrectSub_Temp_Id;
            new Model_Student_Mutual_CorrectSub_Temp();
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
            builder.Append("select count(1) FROM Student_Mutual_CorrectSub_Temp ");
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

        public bool Update(Model_Student_Mutual_CorrectSub_Temp model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Student_Mutual_CorrectSub_Temp set ");
            builder.Append("Student_Mutual_Correct_Temp_Id=@Student_Mutual_Correct_Temp_Id,");
            builder.Append("Correct_Guid=@Correct_Guid,");
            builder.Append("HomeWork_Id=@HomeWork_Id,");
            builder.Append("Student_HomeWork_Id=@Student_HomeWork_Id,");
            builder.Append("Student_Id=@Student_Id,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("Remark=@Remark");
            builder.Append(" where Student_Mutual_CorrectSub_Temp_Id=@Student_Mutual_CorrectSub_Temp_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_Mutual_Correct_Temp_Id", SqlDbType.Char, 0x24), new SqlParameter("@Correct_Guid", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@Student_Mutual_CorrectSub_Temp_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Student_Mutual_Correct_Temp_Id;
            cmdParms[1].Value = model.Correct_Guid;
            cmdParms[2].Value = model.HomeWork_Id;
            cmdParms[3].Value = model.Student_HomeWork_Id;
            cmdParms[4].Value = model.Student_Id;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.Remark;
            cmdParms[7].Value = model.Student_Mutual_CorrectSub_Temp_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

