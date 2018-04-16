namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Student_HomeWork
    {
        public bool Add(Model_Student_HomeWork model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Student_HomeWork(");
            builder.Append("Student_HomeWork_Id,HomeWork_Id,Student_Id,CreateTime)");
            builder.Append(" values (");
            builder.Append("@Student_HomeWork_Id,@HomeWork_Id,@Student_Id,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Student_HomeWork_Id;
            cmdParms[1].Value = model.HomeWork_Id;
            cmdParms[2].Value = model.Student_Id;
            cmdParms[3].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Student_HomeWork DataRowToModel(DataRow row)
        {
            Model_Student_HomeWork work = new Model_Student_HomeWork();
            if (row != null)
            {
                if (row["Student_HomeWork_Id"] != null)
                {
                    work.Student_HomeWork_Id = row["Student_HomeWork_Id"].ToString();
                }
                if (row["HomeWork_Id"] != null)
                {
                    work.HomeWork_Id = row["HomeWork_Id"].ToString();
                }
                if (row["Student_Id"] != null)
                {
                    work.Student_Id = row["Student_Id"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    work.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return work;
        }

        public bool Delete(string Student_HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_HomeWork ");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWork_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Student_HomeWork_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_HomeWork ");
            builder.Append(" where Student_HomeWork_Id in (" + Student_HomeWork_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Student_HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Student_HomeWork");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWork_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetClassHomeWorkList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\nselect  COUNT(*) as NoCorrectCount,shw.HomeWork_Id from Student_HomeWork  shw\r\ninner join HomeWork hw on hw.HomeWork_Id=shw.HomeWork_Id \r\ninner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id \r\ninner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id \r\nwhere  {0} and shwCorrect.Student_HomeWork_CorrectStatus=0 and shwSubmit.Student_HomeWork_Status=1 group by shw.HomeWork_Id ", strWhere);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetClassStudentSubmitedList(string HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select t.Student_HomeWork_Id,t.HomeWork_Id,t.Student_Id,t.CreateTime,shwCorrect.Student_HomeWork_CorrectStatus,\r\n                                  shwSubmit.OpenTime,shwSubmit.Student_Answer_Time\r\n,(case when t2.TrueName='' then t2.UserName when t2.TrueName is null then t2.UserName else t2.UserName end) as StudentName\r\n,(select ISNULL(SUM(Student_Score),0) from Student_HomeWorkAnswer where Student_HomeWork_Id=t.Student_HomeWork_Id) as StudentScore \r\nfrom Student_HomeWork t \r\ninner join F_User t2 on t2.UserId=t.Student_Id \r\ninner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=t.Student_HomeWork_Id \r\ninner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=t.Student_HomeWork_Id \r\nwhere shwSubmit.Student_HomeWork_Status='1' and t.HomeWork_Id='{0}' order by shwSubmit.Student_Answer_Time desc", HomeWork_Id);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetClassStudentUnSubmitedList(string HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select t.* \r\n,(case when t2.TrueName='' then t2.UserName when t2.TrueName is null then t2.UserName else t2.UserName end) as StudentName\r\nfrom Student_HomeWork t\r\ninner join F_User t2 on t2.UserId=t.Student_Id  \r\ninner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=t.Student_HomeWork_Id where shwSubmit.Student_HomeWork_Status='0' \r\nand t.HomeWork_Id='{0}' order by shwSubmit.Student_Answer_Time desc", HomeWork_Id);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetHWStuCount(string HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\nselect yf.HomeWork_Id\r\n,ISNULL(yf.AssignedCount,0) as AssignedCount\r\n,ISNULL(yj.CommittedCount,0) as CommittedCount\r\n,ISNULL(wj.UnCommittedCount,0) as UnCommittedCount  \r\n from \r\n(\r\nSELECT hw.HomeWork_Id,COUNT(*) AssignedCount FROM Homework hw\r\ninner join Student_HomeWork shw on hw.HomeWork_Id=shw.HomeWork_Id\r\nwhere hw.HomeWork_ID='{0}'\r\ngroup by hw.HomeWork_Id\r\n) yf \r\nleft join \r\n(\r\nSELECT hw.HomeWork_Id,COUNT(*) CommittedCount FROM HomeWork hw\r\ninner join Student_HomeWork shw on hw.HomeWork_Id=shw.HomeWork_Id\r\ninner join Student_HomeWork_Submit shws on shws.Student_HomeWork_Id=shw.Student_HomeWork_Id\r\nwhere hw.HomeWork_ID='{0}' \r\nand shws.Student_HomeWork_Status=1\r\ngroup by hw.HomeWork_Id\r\n) yj on yj.HomeWork_Id=yf.HomeWork_Id\r\nleft join \r\n(\r\nSELECT hw.HomeWork_Id,COUNT(*) UnCommittedCount FROM HomeWork hw\r\ninner join Student_HomeWork shw on hw.HomeWork_Id=shw.HomeWork_Id\r\ninner join Student_HomeWork_Submit shws on shws.Student_HomeWork_Id=shw.Student_HomeWork_Id\r\nwhere hw.HomeWork_ID='{0}' \r\nand shws.Student_HomeWork_Status=0\r\ngroup by hw.HomeWork_Id\r\n) wj on wj.HomeWork_Id=yf.HomeWork_Id ", HomeWork_Id);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Student_HomeWork_Id,HomeWork_Id,Student_Id,CreateTime ");
            builder.Append(" FROM Student_HomeWork ");
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
            builder.Append(" Student_HomeWork_Id,HomeWork_Id,Student_Id,CreateTime ");
            builder.Append(" FROM Student_HomeWork ");
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
            builder.Append(")AS Row, T.*  from Student_HomeWork T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Student_HomeWork GetModel(string Student_HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Student_HomeWork_Id,HomeWork_Id,Student_Id,CreateTime from Student_HomeWork ");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWork_Id;
            new Model_Student_HomeWork();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_Student_HomeWork GetModelNew(string HomeWork_Id, string Student_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Student_HomeWork_Id,HomeWork_Id,Student_Id,CreateTime from Student_HomeWork ");
            builder.Append(" where HomeWork_Id=@HomeWork_Id and Student_Id=@Student_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HomeWork_Id;
            cmdParms[1].Value = Student_Id;
            new Model_Student_HomeWork();
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
            builder.Append("select count(1) FROM Student_HomeWork ");
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

        public bool Update(Model_Student_HomeWork model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Student_HomeWork set ");
            builder.Append("HomeWork_Id=@HomeWork_Id,");
            builder.Append("Student_Id=@Student_Id,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.HomeWork_Id;
            cmdParms[1].Value = model.Student_Id;
            cmdParms[2].Value = model.CreateTime;
            cmdParms[3].Value = model.Student_HomeWork_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

