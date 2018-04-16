namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsTeacherHW_KP
    {
        public bool Add(Model_StatsTeacherHW_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsTeacherHW_KP(");
            builder.Append("StatsTeacherHW_KPID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,KPName,KPScoreSum,KPScoreStudentSum,ClassCount,ClassCountAffective,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsTeacherHW_KPID,@ResourceToResourceFolder_Id,@Resource_Name,@SchoolID,@SchoolName,@Gradeid,@GradeName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@KPName,@KPScoreSum,@KPScoreStudentSum,@ClassCount,@ClassCountAffective,@KPScoreAvg,@KPScoreAvgRate,@TestQuestionNums,@TestQuestionNumStrs,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsTeacherHW_KPID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@KPName", SqlDbType.NVarChar, 200), new SqlParameter("@KPScoreSum", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreStudentSum", SqlDbType.Decimal, 5), new SqlParameter("@ClassCount", SqlDbType.Decimal, 5), new SqlParameter("@ClassCountAffective", SqlDbType.Decimal, 5), 
                new SqlParameter("@KPScoreAvg", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvgRate", SqlDbType.Decimal, 5), new SqlParameter("@TestQuestionNums", SqlDbType.VarChar, 500), new SqlParameter("@TestQuestionNumStrs", SqlDbType.VarChar, 800), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsTeacherHW_KPID;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.Resource_Name;
            cmdParms[3].Value = model.SchoolID;
            cmdParms[4].Value = model.SchoolName;
            cmdParms[5].Value = model.Gradeid;
            cmdParms[6].Value = model.GradeName;
            cmdParms[7].Value = model.SubjectID;
            cmdParms[8].Value = model.SubjectName;
            cmdParms[9].Value = model.TeacherID;
            cmdParms[10].Value = model.TeacherName;
            cmdParms[11].Value = model.KPName;
            cmdParms[12].Value = model.KPScoreSum;
            cmdParms[13].Value = model.KPScoreStudentSum;
            cmdParms[14].Value = model.ClassCount;
            cmdParms[15].Value = model.ClassCountAffective;
            cmdParms[0x10].Value = model.KPScoreAvg;
            cmdParms[0x11].Value = model.KPScoreAvgRate;
            cmdParms[0x12].Value = model.TestQuestionNums;
            cmdParms[0x13].Value = model.TestQuestionNumStrs;
            cmdParms[20].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsTeacherHW_KP DataRowToModel(DataRow row)
        {
            Model_StatsTeacherHW_KP rhw_kp = new Model_StatsTeacherHW_KP();
            if (row != null)
            {
                if (row["StatsTeacherHW_KPID"] != null)
                {
                    rhw_kp.StatsTeacherHW_KPID = row["StatsTeacherHW_KPID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    rhw_kp.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    rhw_kp.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["SchoolID"] != null)
                {
                    rhw_kp.SchoolID = row["SchoolID"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    rhw_kp.SchoolName = row["SchoolName"].ToString();
                }
                if (row["Gradeid"] != null)
                {
                    rhw_kp.Gradeid = row["Gradeid"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    rhw_kp.GradeName = row["GradeName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    rhw_kp.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    rhw_kp.SubjectName = row["SubjectName"].ToString();
                }
                if (row["TeacherID"] != null)
                {
                    rhw_kp.TeacherID = row["TeacherID"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    rhw_kp.TeacherName = row["TeacherName"].ToString();
                }
                if (row["KPName"] != null)
                {
                    rhw_kp.KPName = row["KPName"].ToString();
                }
                if ((row["KPScoreSum"] != null) && (row["KPScoreSum"].ToString() != ""))
                {
                    rhw_kp.KPScoreSum = new decimal?(decimal.Parse(row["KPScoreSum"].ToString()));
                }
                if ((row["KPScoreStudentSum"] != null) && (row["KPScoreStudentSum"].ToString() != ""))
                {
                    rhw_kp.KPScoreStudentSum = new decimal?(decimal.Parse(row["KPScoreStudentSum"].ToString()));
                }
                if ((row["ClassCount"] != null) && (row["ClassCount"].ToString() != ""))
                {
                    rhw_kp.ClassCount = new decimal?(decimal.Parse(row["ClassCount"].ToString()));
                }
                if ((row["ClassCountAffective"] != null) && (row["ClassCountAffective"].ToString() != ""))
                {
                    rhw_kp.ClassCountAffective = new decimal?(decimal.Parse(row["ClassCountAffective"].ToString()));
                }
                if ((row["KPScoreAvg"] != null) && (row["KPScoreAvg"].ToString() != ""))
                {
                    rhw_kp.KPScoreAvg = new decimal?(decimal.Parse(row["KPScoreAvg"].ToString()));
                }
                if ((row["KPScoreAvgRate"] != null) && (row["KPScoreAvgRate"].ToString() != ""))
                {
                    rhw_kp.KPScoreAvgRate = new decimal?(decimal.Parse(row["KPScoreAvgRate"].ToString()));
                }
                if (row["TestQuestionNums"] != null)
                {
                    rhw_kp.TestQuestionNums = row["TestQuestionNums"].ToString();
                }
                if (row["TestQuestionNumStrs"] != null)
                {
                    rhw_kp.TestQuestionNumStrs = row["TestQuestionNumStrs"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    rhw_kp.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return rhw_kp;
        }

        public bool Delete(string StatsTeacherHW_KPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsTeacherHW_KP ");
            builder.Append(" where StatsTeacherHW_KPID=@StatsTeacherHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherHW_KPID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherHW_KPID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsTeacherHW_KPIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsTeacherHW_KP ");
            builder.Append(" where StatsTeacherHW_KPID in (" + StatsTeacherHW_KPIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsTeacherHW_KPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsTeacherHW_KP");
            builder.Append(" where StatsTeacherHW_KPID=@StatsTeacherHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherHW_KPID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherHW_KPID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsTeacherHW_KPID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,KPName,KPScoreSum,KPScoreStudentSum,ClassCount,ClassCountAffective,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime ");
            builder.Append(" FROM StatsTeacherHW_KP ");
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
            builder.Append(" StatsTeacherHW_KPID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,KPName,KPScoreSum,KPScoreStudentSum,ClassCount,ClassCountAffective,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime ");
            builder.Append(" FROM StatsTeacherHW_KP ");
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
                builder.Append("order by T.StatsTeacherHW_KPID desc");
            }
            builder.Append(")AS Row, T.*  from StatsTeacherHW_KP T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsTeacherHW_KP GetModel(string StatsTeacherHW_KPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsTeacherHW_KPID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,KPName,KPScoreSum,KPScoreStudentSum,ClassCount,ClassCountAffective,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime from StatsTeacherHW_KP ");
            builder.Append(" where StatsTeacherHW_KPID=@StatsTeacherHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherHW_KPID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherHW_KPID;
            new Model_StatsTeacherHW_KP();
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
            builder.Append("select count(1) FROM StatsTeacherHW_KP ");
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

        public bool Update(Model_StatsTeacherHW_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsTeacherHW_KP set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("Resource_Name=@Resource_Name,");
            builder.Append("SchoolID=@SchoolID,");
            builder.Append("SchoolName=@SchoolName,");
            builder.Append("Gradeid=@Gradeid,");
            builder.Append("GradeName=@GradeName,");
            builder.Append("SubjectID=@SubjectID,");
            builder.Append("SubjectName=@SubjectName,");
            builder.Append("TeacherID=@TeacherID,");
            builder.Append("TeacherName=@TeacherName,");
            builder.Append("KPName=@KPName,");
            builder.Append("KPScoreSum=@KPScoreSum,");
            builder.Append("KPScoreStudentSum=@KPScoreStudentSum,");
            builder.Append("ClassCount=@ClassCount,");
            builder.Append("ClassCountAffective=@ClassCountAffective,");
            builder.Append("KPScoreAvg=@KPScoreAvg,");
            builder.Append("KPScoreAvgRate=@KPScoreAvgRate,");
            builder.Append("TestQuestionNums=@TestQuestionNums,");
            builder.Append("TestQuestionNumStrs=@TestQuestionNumStrs,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsTeacherHW_KPID=@StatsTeacherHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@KPName", SqlDbType.NVarChar, 200), new SqlParameter("@KPScoreSum", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreStudentSum", SqlDbType.Decimal, 5), new SqlParameter("@ClassCount", SqlDbType.Decimal, 5), new SqlParameter("@ClassCountAffective", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvg", SqlDbType.Decimal, 5), 
                new SqlParameter("@KPScoreAvgRate", SqlDbType.Decimal, 5), new SqlParameter("@TestQuestionNums", SqlDbType.VarChar, 500), new SqlParameter("@TestQuestionNumStrs", SqlDbType.VarChar, 800), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsTeacherHW_KPID", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.Resource_Name;
            cmdParms[2].Value = model.SchoolID;
            cmdParms[3].Value = model.SchoolName;
            cmdParms[4].Value = model.Gradeid;
            cmdParms[5].Value = model.GradeName;
            cmdParms[6].Value = model.SubjectID;
            cmdParms[7].Value = model.SubjectName;
            cmdParms[8].Value = model.TeacherID;
            cmdParms[9].Value = model.TeacherName;
            cmdParms[10].Value = model.KPName;
            cmdParms[11].Value = model.KPScoreSum;
            cmdParms[12].Value = model.KPScoreStudentSum;
            cmdParms[13].Value = model.ClassCount;
            cmdParms[14].Value = model.ClassCountAffective;
            cmdParms[15].Value = model.KPScoreAvg;
            cmdParms[0x10].Value = model.KPScoreAvgRate;
            cmdParms[0x11].Value = model.TestQuestionNums;
            cmdParms[0x12].Value = model.TestQuestionNumStrs;
            cmdParms[0x13].Value = model.CreateTime;
            cmdParms[20].Value = model.StatsTeacherHW_KPID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

