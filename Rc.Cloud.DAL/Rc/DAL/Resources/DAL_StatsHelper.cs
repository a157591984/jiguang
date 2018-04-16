namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsHelper
    {
        public bool Add(Model_StatsHelper model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsHelper(");
            builder.Append("StatsHelper_Id,ResourceToResourceFolder_Id,Homework_Id,Correct_Time,Exec_Status,SType,CreateUser,Exec_Time,SchoolId,GradeId,ClassId,TeacherId,HW_CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsHelper_Id,@ResourceToResourceFolder_Id,@Homework_Id,@Correct_Time,@Exec_Status,@SType,@CreateUser,@Exec_Time,@SchoolId,@GradeId,@ClassId,@TeacherId,@HW_CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsHelper_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Homework_Id", SqlDbType.Char, 0x24), new SqlParameter("@Correct_Time", SqlDbType.DateTime), new SqlParameter("@Exec_Status", SqlDbType.Char, 1), new SqlParameter("@SType", SqlDbType.VarChar, 50), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@Exec_Time", SqlDbType.DateTime), new SqlParameter("@SchoolId", SqlDbType.Char, 0x24), new SqlParameter("@GradeId", SqlDbType.Char, 0x24), new SqlParameter("@ClassId", SqlDbType.Char, 0x24), new SqlParameter("@TeacherId", SqlDbType.Char, 0x24), new SqlParameter("@HW_CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.StatsHelper_Id;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.Homework_Id;
            cmdParms[3].Value = model.Correct_Time;
            cmdParms[4].Value = model.Exec_Status;
            cmdParms[5].Value = model.SType;
            cmdParms[6].Value = model.CreateUser;
            cmdParms[7].Value = model.Exec_Time;
            cmdParms[8].Value = model.SchoolId;
            cmdParms[9].Value = model.GradeId;
            cmdParms[10].Value = model.ClassId;
            cmdParms[11].Value = model.TeacherId;
            cmdParms[12].Value = model.HW_CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsHelper DataRowToModel(DataRow row)
        {
            Model_StatsHelper helper = new Model_StatsHelper();
            if (row != null)
            {
                if (row["StatsHelper_Id"] != null)
                {
                    helper.StatsHelper_Id = row["StatsHelper_Id"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    helper.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Homework_Id"] != null)
                {
                    helper.Homework_Id = row["Homework_Id"].ToString();
                }
                if ((row["Correct_Time"] != null) && (row["Correct_Time"].ToString() != ""))
                {
                    helper.Correct_Time = new DateTime?(DateTime.Parse(row["Correct_Time"].ToString()));
                }
                if (row["Exec_Status"] != null)
                {
                    helper.Exec_Status = row["Exec_Status"].ToString();
                }
                if (row["SType"] != null)
                {
                    helper.SType = row["SType"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    helper.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["Exec_Time"] != null) && (row["Exec_Time"].ToString() != ""))
                {
                    helper.Exec_Time = new DateTime?(DateTime.Parse(row["Exec_Time"].ToString()));
                }
                if (row["SchoolId"] != null)
                {
                    helper.SchoolId = row["SchoolId"].ToString();
                }
                if (row["GradeId"] != null)
                {
                    helper.GradeId = row["GradeId"].ToString();
                }
                if (row["ClassId"] != null)
                {
                    helper.ClassId = row["ClassId"].ToString();
                }
                if (row["TeacherId"] != null)
                {
                    helper.TeacherId = row["TeacherId"].ToString();
                }
                if ((row["HW_CreateTime"] != null) && (row["HW_CreateTime"].ToString() != ""))
                {
                    helper.HW_CreateTime = new DateTime?(DateTime.Parse(row["HW_CreateTime"].ToString()));
                }
            }
            return helper;
        }

        public bool Delete(string StatsHelper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsHelper ");
            builder.Append(" where StatsHelper_Id=@StatsHelper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsHelper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsHelper_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsHelper_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsHelper ");
            builder.Append(" where StatsHelper_Id in (" + StatsHelper_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsHelper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsHelper");
            builder.Append(" where StatsHelper_Id=@StatsHelper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsHelper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsHelper_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsHelper_Id,ResourceToResourceFolder_Id,Homework_Id,Correct_Time,Exec_Status,SType,CreateUser,Exec_Time,SchoolId,GradeId,ClassId,TeacherId,HW_CreateTime ");
            builder.Append(" FROM StatsHelper ");
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
            builder.Append(" StatsHelper_Id,ResourceToResourceFolder_Id,Homework_Id,Correct_Time,Exec_Status,SType,CreateUser,Exec_Time,SchoolId,GradeId,ClassId,TeacherId,HW_CreateTime ");
            builder.Append(" FROM StatsHelper ");
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
                builder.Append("order by T.StatsHelper_Id desc");
            }
            builder.Append(")AS Row, T.*  from StatsHelper T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsHelper GetModel(string StatsHelper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsHelper_Id,ResourceToResourceFolder_Id,Homework_Id,Correct_Time,Exec_Status,SType,CreateUser,Exec_Time,SchoolId,GradeId,ClassId,TeacherId,HW_CreateTime from StatsHelper ");
            builder.Append(" where StatsHelper_Id=@StatsHelper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsHelper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsHelper_Id;
            new Model_StatsHelper();
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
            builder.Append("select count(1) FROM StatsHelper ");
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

        public bool Update(Model_StatsHelper model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsHelper set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("Homework_Id=@Homework_Id,");
            builder.Append("Correct_Time=@Correct_Time,");
            builder.Append("Exec_Status=@Exec_Status,");
            builder.Append("SType=@SType,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("Exec_Time=@Exec_Time,");
            builder.Append("SchoolId=@SchoolId,");
            builder.Append("GradeId=@GradeId,");
            builder.Append("ClassId=@ClassId,");
            builder.Append("TeacherId=@TeacherId,");
            builder.Append("HW_CreateTime=@HW_CreateTime");
            builder.Append(" where StatsHelper_Id=@StatsHelper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Homework_Id", SqlDbType.Char, 0x24), new SqlParameter("@Correct_Time", SqlDbType.DateTime), new SqlParameter("@Exec_Status", SqlDbType.Char, 1), new SqlParameter("@SType", SqlDbType.VarChar, 50), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@Exec_Time", SqlDbType.DateTime), new SqlParameter("@SchoolId", SqlDbType.Char, 0x24), new SqlParameter("@GradeId", SqlDbType.Char, 0x24), new SqlParameter("@ClassId", SqlDbType.Char, 0x24), new SqlParameter("@TeacherId", SqlDbType.Char, 0x24), new SqlParameter("@HW_CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsHelper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.Homework_Id;
            cmdParms[2].Value = model.Correct_Time;
            cmdParms[3].Value = model.Exec_Status;
            cmdParms[4].Value = model.SType;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.Exec_Time;
            cmdParms[7].Value = model.SchoolId;
            cmdParms[8].Value = model.GradeId;
            cmdParms[9].Value = model.ClassId;
            cmdParms[10].Value = model.TeacherId;
            cmdParms[11].Value = model.HW_CreateTime;
            cmdParms[12].Value = model.StatsHelper_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

