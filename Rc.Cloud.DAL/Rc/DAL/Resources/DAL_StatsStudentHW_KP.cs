namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsStudentHW_KP
    {
        public bool Add(Model_StatsStudentHW_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsStudentHW_KP(");
            builder.Append("StatsStudentHW_KPID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,GradeID,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,KPName,KPScoreSum,KPScoreStudentSum,ClassCount,ClassCountAffective,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime,KPScoreSumAffective)");
            builder.Append(" values (");
            builder.Append("@StatsStudentHW_KPID,@ResourceToResourceFolder_Id,@Resource_Name,@HomeWork_ID,@HomeWork_Name,@HomeWorkCreateTime,@SchoolID,@SchoolName,@GradeID,@GradeName,@ClassID,@ClassName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@StudentID,@StudentName,@KPName,@KPScoreSum,@KPScoreStudentSum,@ClassCount,@ClassCountAffective,@KPScoreAvg,@KPScoreAvgRate,@TestQuestionNums,@TestQuestionNumStrs,@CreateTime,@KPScoreSumAffective)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsStudentHW_KPID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_ID", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@GradeID", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), 
                new SqlParameter("@StudentID", SqlDbType.Char, 0x24), new SqlParameter("@StudentName", SqlDbType.NVarChar, 250), new SqlParameter("@KPName", SqlDbType.NVarChar, 250), new SqlParameter("@KPScoreSum", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreStudentSum", SqlDbType.Decimal, 5), new SqlParameter("@ClassCount", SqlDbType.Decimal, 5), new SqlParameter("@ClassCountAffective", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvg", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvgRate", SqlDbType.Decimal, 5), new SqlParameter("@TestQuestionNums", SqlDbType.VarChar, 500), new SqlParameter("@TestQuestionNumStrs", SqlDbType.VarChar, 800), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@KPScoreSumAffective", SqlDbType.Decimal, 5)
             };
            cmdParms[0].Value = model.StatsStudentHW_KPID;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.Resource_Name;
            cmdParms[3].Value = model.HomeWork_ID;
            cmdParms[4].Value = model.HomeWork_Name;
            cmdParms[5].Value = model.HomeWorkCreateTime;
            cmdParms[6].Value = model.SchoolID;
            cmdParms[7].Value = model.SchoolName;
            cmdParms[8].Value = model.GradeID;
            cmdParms[9].Value = model.GradeName;
            cmdParms[10].Value = model.ClassID;
            cmdParms[11].Value = model.ClassName;
            cmdParms[12].Value = model.SubjectID;
            cmdParms[13].Value = model.SubjectName;
            cmdParms[14].Value = model.TeacherID;
            cmdParms[15].Value = model.TeacherName;
            cmdParms[0x10].Value = model.StudentID;
            cmdParms[0x11].Value = model.StudentName;
            cmdParms[0x12].Value = model.KPName;
            cmdParms[0x13].Value = model.KPScoreSum;
            cmdParms[20].Value = model.KPScoreStudentSum;
            cmdParms[0x15].Value = model.ClassCount;
            cmdParms[0x16].Value = model.ClassCountAffective;
            cmdParms[0x17].Value = model.KPScoreAvg;
            cmdParms[0x18].Value = model.KPScoreAvgRate;
            cmdParms[0x19].Value = model.TestQuestionNums;
            cmdParms[0x1a].Value = model.TestQuestionNumStrs;
            cmdParms[0x1b].Value = model.CreateTime;
            cmdParms[0x1c].Value = model.KPScoreSumAffective;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsStudentHW_KP DataRowToModel(DataRow row)
        {
            Model_StatsStudentHW_KP thw_kp = new Model_StatsStudentHW_KP();
            if (row != null)
            {
                if (row["StatsStudentHW_KPID"] != null)
                {
                    thw_kp.StatsStudentHW_KPID = row["StatsStudentHW_KPID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    thw_kp.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    thw_kp.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["HomeWork_ID"] != null)
                {
                    thw_kp.HomeWork_ID = row["HomeWork_ID"].ToString();
                }
                if (row["HomeWork_Name"] != null)
                {
                    thw_kp.HomeWork_Name = row["HomeWork_Name"].ToString();
                }
                if ((row["HomeWorkCreateTime"] != null) && (row["HomeWorkCreateTime"].ToString() != ""))
                {
                    thw_kp.HomeWorkCreateTime = new DateTime?(DateTime.Parse(row["HomeWorkCreateTime"].ToString()));
                }
                if (row["SchoolID"] != null)
                {
                    thw_kp.SchoolID = row["SchoolID"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    thw_kp.SchoolName = row["SchoolName"].ToString();
                }
                if (row["GradeID"] != null)
                {
                    thw_kp.GradeID = row["GradeID"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    thw_kp.GradeName = row["GradeName"].ToString();
                }
                if (row["ClassID"] != null)
                {
                    thw_kp.ClassID = row["ClassID"].ToString();
                }
                if (row["ClassName"] != null)
                {
                    thw_kp.ClassName = row["ClassName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    thw_kp.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    thw_kp.SubjectName = row["SubjectName"].ToString();
                }
                if (row["TeacherID"] != null)
                {
                    thw_kp.TeacherID = row["TeacherID"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    thw_kp.TeacherName = row["TeacherName"].ToString();
                }
                if (row["StudentID"] != null)
                {
                    thw_kp.StudentID = row["StudentID"].ToString();
                }
                if (row["StudentName"] != null)
                {
                    thw_kp.StudentName = row["StudentName"].ToString();
                }
                if (row["KPName"] != null)
                {
                    thw_kp.KPName = row["KPName"].ToString();
                }
                if ((row["KPScoreSum"] != null) && (row["KPScoreSum"].ToString() != ""))
                {
                    thw_kp.KPScoreSum = new decimal?(decimal.Parse(row["KPScoreSum"].ToString()));
                }
                if ((row["KPScoreStudentSum"] != null) && (row["KPScoreStudentSum"].ToString() != ""))
                {
                    thw_kp.KPScoreStudentSum = new decimal?(decimal.Parse(row["KPScoreStudentSum"].ToString()));
                }
                if ((row["ClassCount"] != null) && (row["ClassCount"].ToString() != ""))
                {
                    thw_kp.ClassCount = new decimal?(decimal.Parse(row["ClassCount"].ToString()));
                }
                if ((row["ClassCountAffective"] != null) && (row["ClassCountAffective"].ToString() != ""))
                {
                    thw_kp.ClassCountAffective = new decimal?(decimal.Parse(row["ClassCountAffective"].ToString()));
                }
                if ((row["KPScoreAvg"] != null) && (row["KPScoreAvg"].ToString() != ""))
                {
                    thw_kp.KPScoreAvg = new decimal?(decimal.Parse(row["KPScoreAvg"].ToString()));
                }
                if ((row["KPScoreAvgRate"] != null) && (row["KPScoreAvgRate"].ToString() != ""))
                {
                    thw_kp.KPScoreAvgRate = new decimal?(decimal.Parse(row["KPScoreAvgRate"].ToString()));
                }
                if (row["TestQuestionNums"] != null)
                {
                    thw_kp.TestQuestionNums = row["TestQuestionNums"].ToString();
                }
                if (row["TestQuestionNumStrs"] != null)
                {
                    thw_kp.TestQuestionNumStrs = row["TestQuestionNumStrs"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    thw_kp.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if ((row["KPScoreSumAffective"] != null) && (row["KPScoreSumAffective"].ToString() != ""))
                {
                    thw_kp.KPScoreSumAffective = new decimal?(decimal.Parse(row["KPScoreSumAffective"].ToString()));
                }
            }
            return thw_kp;
        }

        public bool Delete(string StatsStudentHW_KPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStudentHW_KP ");
            builder.Append(" where StatsStudentHW_KPID=@StatsStudentHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStudentHW_KPID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStudentHW_KPID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsStudentHW_KPIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStudentHW_KP ");
            builder.Append(" where StatsStudentHW_KPID in (" + StatsStudentHW_KPIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsStudentHW_KPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsStudentHW_KP");
            builder.Append(" where StatsStudentHW_KPID=@StatsStudentHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStudentHW_KPID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStudentHW_KPID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsStudentHW_KPID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,GradeID,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,KPName,KPScoreSum,KPScoreStudentSum,ClassCount,ClassCountAffective,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime,KPScoreSumAffective ");
            builder.Append(" FROM StatsStudentHW_KP ");
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
            builder.Append(" StatsStudentHW_KPID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,GradeID,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,KPName,KPScoreSum,KPScoreStudentSum,ClassCount,ClassCountAffective,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime,KPScoreSumAffective ");
            builder.Append(" FROM StatsStudentHW_KP ");
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
                builder.Append("order by T.StatsStudentHW_KPID desc");
            }
            builder.Append(")AS Row, T.*  from StatsStudentHW_KP T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsStudentHW_KP GetModel(string StatsStudentHW_KPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsStudentHW_KPID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,GradeID,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,KPName,KPScoreSum,KPScoreStudentSum,ClassCount,ClassCountAffective,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime,KPScoreSumAffective from StatsStudentHW_KP ");
            builder.Append(" where StatsStudentHW_KPID=@StatsStudentHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStudentHW_KPID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStudentHW_KPID;
            new Model_StatsStudentHW_KP();
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
            builder.Append("select count(1) FROM StatsStudentHW_KP ");
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

        public bool Update(Model_StatsStudentHW_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsStudentHW_KP set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("Resource_Name=@Resource_Name,");
            builder.Append("HomeWork_ID=@HomeWork_ID,");
            builder.Append("HomeWork_Name=@HomeWork_Name,");
            builder.Append("HomeWorkCreateTime=@HomeWorkCreateTime,");
            builder.Append("SchoolID=@SchoolID,");
            builder.Append("SchoolName=@SchoolName,");
            builder.Append("GradeID=@GradeID,");
            builder.Append("GradeName=@GradeName,");
            builder.Append("ClassID=@ClassID,");
            builder.Append("ClassName=@ClassName,");
            builder.Append("SubjectID=@SubjectID,");
            builder.Append("SubjectName=@SubjectName,");
            builder.Append("TeacherID=@TeacherID,");
            builder.Append("TeacherName=@TeacherName,");
            builder.Append("StudentID=@StudentID,");
            builder.Append("StudentName=@StudentName,");
            builder.Append("KPName=@KPName,");
            builder.Append("KPScoreSum=@KPScoreSum,");
            builder.Append("KPScoreStudentSum=@KPScoreStudentSum,");
            builder.Append("ClassCount=@ClassCount,");
            builder.Append("ClassCountAffective=@ClassCountAffective,");
            builder.Append("KPScoreAvg=@KPScoreAvg,");
            builder.Append("KPScoreAvgRate=@KPScoreAvgRate,");
            builder.Append("TestQuestionNums=@TestQuestionNums,");
            builder.Append("TestQuestionNumStrs=@TestQuestionNumStrs,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("KPScoreSumAffective=@KPScoreSumAffective");
            builder.Append(" where StatsStudentHW_KPID=@StatsStudentHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_ID", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@GradeID", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@StudentID", SqlDbType.Char, 0x24), 
                new SqlParameter("@StudentName", SqlDbType.NVarChar, 250), new SqlParameter("@KPName", SqlDbType.NVarChar, 250), new SqlParameter("@KPScoreSum", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreStudentSum", SqlDbType.Decimal, 5), new SqlParameter("@ClassCount", SqlDbType.Decimal, 5), new SqlParameter("@ClassCountAffective", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvg", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvgRate", SqlDbType.Decimal, 5), new SqlParameter("@TestQuestionNums", SqlDbType.VarChar, 500), new SqlParameter("@TestQuestionNumStrs", SqlDbType.VarChar, 800), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@KPScoreSumAffective", SqlDbType.Decimal, 5), new SqlParameter("@StatsStudentHW_KPID", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.Resource_Name;
            cmdParms[2].Value = model.HomeWork_ID;
            cmdParms[3].Value = model.HomeWork_Name;
            cmdParms[4].Value = model.HomeWorkCreateTime;
            cmdParms[5].Value = model.SchoolID;
            cmdParms[6].Value = model.SchoolName;
            cmdParms[7].Value = model.GradeID;
            cmdParms[8].Value = model.GradeName;
            cmdParms[9].Value = model.ClassID;
            cmdParms[10].Value = model.ClassName;
            cmdParms[11].Value = model.SubjectID;
            cmdParms[12].Value = model.SubjectName;
            cmdParms[13].Value = model.TeacherID;
            cmdParms[14].Value = model.TeacherName;
            cmdParms[15].Value = model.StudentID;
            cmdParms[0x10].Value = model.StudentName;
            cmdParms[0x11].Value = model.KPName;
            cmdParms[0x12].Value = model.KPScoreSum;
            cmdParms[0x13].Value = model.KPScoreStudentSum;
            cmdParms[20].Value = model.ClassCount;
            cmdParms[0x15].Value = model.ClassCountAffective;
            cmdParms[0x16].Value = model.KPScoreAvg;
            cmdParms[0x17].Value = model.KPScoreAvgRate;
            cmdParms[0x18].Value = model.TestQuestionNums;
            cmdParms[0x19].Value = model.TestQuestionNumStrs;
            cmdParms[0x1a].Value = model.CreateTime;
            cmdParms[0x1b].Value = model.KPScoreSumAffective;
            cmdParms[0x1c].Value = model.StatsStudentHW_KPID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

