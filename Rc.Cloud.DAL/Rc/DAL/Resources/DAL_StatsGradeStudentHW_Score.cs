namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsGradeStudentHW_Score
    {
        public bool Add(Model_StatsGradeStudentHW_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsGradeStudentHW_Score(");
            builder.Append("StatsGradeStudentHW_ScoreID,ResourceToResourceFolder_ID,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,StudentIP,AnswerStart,AnswerEnd,AnswerQTTimeAvg,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,CreateTime,GradeStudentScoreOrder,GradeScoreImprove,GradeHierarchy)");
            builder.Append(" values (");
            builder.Append("@StatsGradeStudentHW_ScoreID,@ResourceToResourceFolder_ID,@Resource_Name,@SchoolID,@SchoolName,@Gradeid,@GradeName,@ClassID,@ClassName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@StudentID,@StudentName,@StudentIP,@AnswerStart,@AnswerEnd,@AnswerQTTimeAvg,@HWScore,@StudentScore,@StudentScoreOrder,@ScoreImprove,@Hierarchy,@CreateTime,@GradeStudentScoreOrder,@GradeScoreImprove,@GradeHierarchy)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsGradeStudentHW_ScoreID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_ID", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@StudentID", SqlDbType.Char, 0x24), new SqlParameter("@StudentName", SqlDbType.NVarChar, 250), new SqlParameter("@StudentIP", SqlDbType.VarChar, 50), 
                new SqlParameter("@AnswerStart", SqlDbType.DateTime), new SqlParameter("@AnswerEnd", SqlDbType.DateTime), new SqlParameter("@AnswerQTTimeAvg", SqlDbType.Decimal, 5), new SqlParameter("@HWScore", SqlDbType.Decimal, 5), new SqlParameter("@StudentScore", SqlDbType.Decimal, 5), new SqlParameter("@StudentScoreOrder", SqlDbType.Decimal, 5), new SqlParameter("@ScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@Hierarchy", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@GradeStudentScoreOrder", SqlDbType.Decimal, 5), new SqlParameter("@GradeScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@GradeHierarchy", SqlDbType.Decimal, 5)
             };
            cmdParms[0].Value = model.StatsGradeStudentHW_ScoreID;
            cmdParms[1].Value = model.ResourceToResourceFolder_ID;
            cmdParms[2].Value = model.Resource_Name;
            cmdParms[3].Value = model.SchoolID;
            cmdParms[4].Value = model.SchoolName;
            cmdParms[5].Value = model.Gradeid;
            cmdParms[6].Value = model.GradeName;
            cmdParms[7].Value = model.ClassID;
            cmdParms[8].Value = model.ClassName;
            cmdParms[9].Value = model.SubjectID;
            cmdParms[10].Value = model.SubjectName;
            cmdParms[11].Value = model.TeacherID;
            cmdParms[12].Value = model.TeacherName;
            cmdParms[13].Value = model.StudentID;
            cmdParms[14].Value = model.StudentName;
            cmdParms[15].Value = model.StudentIP;
            cmdParms[0x10].Value = model.AnswerStart;
            cmdParms[0x11].Value = model.AnswerEnd;
            cmdParms[0x12].Value = model.AnswerQTTimeAvg;
            cmdParms[0x13].Value = model.HWScore;
            cmdParms[20].Value = model.StudentScore;
            cmdParms[0x15].Value = model.StudentScoreOrder;
            cmdParms[0x16].Value = model.ScoreImprove;
            cmdParms[0x17].Value = model.Hierarchy;
            cmdParms[0x18].Value = model.CreateTime;
            cmdParms[0x19].Value = model.GradeStudentScoreOrder;
            cmdParms[0x1a].Value = model.GradeScoreImprove;
            cmdParms[0x1b].Value = model.GradeHierarchy;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsGradeStudentHW_Score DataRowToModel(DataRow row)
        {
            Model_StatsGradeStudentHW_Score score = new Model_StatsGradeStudentHW_Score();
            if (row != null)
            {
                if (row["StatsGradeStudentHW_ScoreID"] != null)
                {
                    score.StatsGradeStudentHW_ScoreID = row["StatsGradeStudentHW_ScoreID"].ToString();
                }
                if (row["ResourceToResourceFolder_ID"] != null)
                {
                    score.ResourceToResourceFolder_ID = row["ResourceToResourceFolder_ID"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    score.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["SchoolID"] != null)
                {
                    score.SchoolID = row["SchoolID"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    score.SchoolName = row["SchoolName"].ToString();
                }
                if (row["Gradeid"] != null)
                {
                    score.Gradeid = row["Gradeid"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    score.GradeName = row["GradeName"].ToString();
                }
                if (row["ClassID"] != null)
                {
                    score.ClassID = row["ClassID"].ToString();
                }
                if (row["ClassName"] != null)
                {
                    score.ClassName = row["ClassName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    score.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    score.SubjectName = row["SubjectName"].ToString();
                }
                if (row["TeacherID"] != null)
                {
                    score.TeacherID = row["TeacherID"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    score.TeacherName = row["TeacherName"].ToString();
                }
                if (row["StudentID"] != null)
                {
                    score.StudentID = row["StudentID"].ToString();
                }
                if (row["StudentName"] != null)
                {
                    score.StudentName = row["StudentName"].ToString();
                }
                if (row["StudentIP"] != null)
                {
                    score.StudentIP = row["StudentIP"].ToString();
                }
                if ((row["AnswerStart"] != null) && (row["AnswerStart"].ToString() != ""))
                {
                    score.AnswerStart = new DateTime?(DateTime.Parse(row["AnswerStart"].ToString()));
                }
                if ((row["AnswerEnd"] != null) && (row["AnswerEnd"].ToString() != ""))
                {
                    score.AnswerEnd = new DateTime?(DateTime.Parse(row["AnswerEnd"].ToString()));
                }
                if ((row["AnswerQTTimeAvg"] != null) && (row["AnswerQTTimeAvg"].ToString() != ""))
                {
                    score.AnswerQTTimeAvg = new decimal?(decimal.Parse(row["AnswerQTTimeAvg"].ToString()));
                }
                if ((row["HWScore"] != null) && (row["HWScore"].ToString() != ""))
                {
                    score.HWScore = new decimal?(decimal.Parse(row["HWScore"].ToString()));
                }
                if ((row["StudentScore"] != null) && (row["StudentScore"].ToString() != ""))
                {
                    score.StudentScore = new decimal?(decimal.Parse(row["StudentScore"].ToString()));
                }
                if ((row["StudentScoreOrder"] != null) && (row["StudentScoreOrder"].ToString() != ""))
                {
                    score.StudentScoreOrder = new decimal?(decimal.Parse(row["StudentScoreOrder"].ToString()));
                }
                if ((row["ScoreImprove"] != null) && (row["ScoreImprove"].ToString() != ""))
                {
                    score.ScoreImprove = new decimal?(decimal.Parse(row["ScoreImprove"].ToString()));
                }
                if ((row["Hierarchy"] != null) && (row["Hierarchy"].ToString() != ""))
                {
                    score.Hierarchy = new decimal?(decimal.Parse(row["Hierarchy"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    score.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if ((row["GradeStudentScoreOrder"] != null) && (row["GradeStudentScoreOrder"].ToString() != ""))
                {
                    score.GradeStudentScoreOrder = new decimal?(decimal.Parse(row["GradeStudentScoreOrder"].ToString()));
                }
                if ((row["GradeScoreImprove"] != null) && (row["GradeScoreImprove"].ToString() != ""))
                {
                    score.GradeScoreImprove = new decimal?(decimal.Parse(row["GradeScoreImprove"].ToString()));
                }
                if ((row["GradeHierarchy"] != null) && (row["GradeHierarchy"].ToString() != ""))
                {
                    score.GradeHierarchy = new decimal?(decimal.Parse(row["GradeHierarchy"].ToString()));
                }
            }
            return score;
        }

        public bool Delete(string StatsGradeStudentHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeStudentHW_Score ");
            builder.Append(" where StatsGradeStudentHW_ScoreID=@StatsGradeStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeStudentHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeStudentHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsGradeStudentHW_ScoreIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeStudentHW_Score ");
            builder.Append(" where StatsGradeStudentHW_ScoreID in (" + StatsGradeStudentHW_ScoreIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsGradeStudentHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsGradeStudentHW_Score");
            builder.Append(" where StatsGradeStudentHW_ScoreID=@StatsGradeStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeStudentHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeStudentHW_ScoreID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsGradeStudentHW_ScoreID,ResourceToResourceFolder_ID,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,StudentIP,AnswerStart,AnswerEnd,AnswerQTTimeAvg,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,CreateTime,GradeStudentScoreOrder,GradeScoreImprove,GradeHierarchy ");
            builder.Append(" FROM StatsGradeStudentHW_Score ");
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
            builder.Append(" StatsGradeStudentHW_ScoreID,ResourceToResourceFolder_ID,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,StudentIP,AnswerStart,AnswerEnd,AnswerQTTimeAvg,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,CreateTime,GradeStudentScoreOrder,GradeScoreImprove,GradeHierarchy ");
            builder.Append(" FROM StatsGradeStudentHW_Score ");
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
                builder.Append("order by T.StatsGradeStudentHW_ScoreID desc");
            }
            builder.Append(")AS Row, T.*  from StatsGradeStudentHW_Score T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsGradeStudentHW_Score GetModel(string StatsGradeStudentHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsGradeStudentHW_ScoreID,ResourceToResourceFolder_ID,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,StudentIP,AnswerStart,AnswerEnd,AnswerQTTimeAvg,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,CreateTime,GradeStudentScoreOrder,GradeScoreImprove,GradeHierarchy from StatsGradeStudentHW_Score ");
            builder.Append(" where StatsGradeStudentHW_ScoreID=@StatsGradeStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeStudentHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeStudentHW_ScoreID;
            new Model_StatsGradeStudentHW_Score();
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
            builder.Append("select count(1) FROM StatsGradeStudentHW_Score ");
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

        public bool Update(Model_StatsGradeStudentHW_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsGradeStudentHW_Score set ");
            builder.Append("ResourceToResourceFolder_ID=@ResourceToResourceFolder_ID,");
            builder.Append("Resource_Name=@Resource_Name,");
            builder.Append("SchoolID=@SchoolID,");
            builder.Append("SchoolName=@SchoolName,");
            builder.Append("Gradeid=@Gradeid,");
            builder.Append("GradeName=@GradeName,");
            builder.Append("ClassID=@ClassID,");
            builder.Append("ClassName=@ClassName,");
            builder.Append("SubjectID=@SubjectID,");
            builder.Append("SubjectName=@SubjectName,");
            builder.Append("TeacherID=@TeacherID,");
            builder.Append("TeacherName=@TeacherName,");
            builder.Append("StudentID=@StudentID,");
            builder.Append("StudentName=@StudentName,");
            builder.Append("StudentIP=@StudentIP,");
            builder.Append("AnswerStart=@AnswerStart,");
            builder.Append("AnswerEnd=@AnswerEnd,");
            builder.Append("AnswerQTTimeAvg=@AnswerQTTimeAvg,");
            builder.Append("HWScore=@HWScore,");
            builder.Append("StudentScore=@StudentScore,");
            builder.Append("StudentScoreOrder=@StudentScoreOrder,");
            builder.Append("ScoreImprove=@ScoreImprove,");
            builder.Append("Hierarchy=@Hierarchy,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("GradeStudentScoreOrder=@GradeStudentScoreOrder,");
            builder.Append("GradeScoreImprove=@GradeScoreImprove,");
            builder.Append("GradeHierarchy=@GradeHierarchy");
            builder.Append(" where StatsGradeStudentHW_ScoreID=@StatsGradeStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_ID", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@StudentID", SqlDbType.Char, 0x24), new SqlParameter("@StudentName", SqlDbType.NVarChar, 250), new SqlParameter("@StudentIP", SqlDbType.VarChar, 50), new SqlParameter("@AnswerStart", SqlDbType.DateTime), 
                new SqlParameter("@AnswerEnd", SqlDbType.DateTime), new SqlParameter("@AnswerQTTimeAvg", SqlDbType.Decimal, 5), new SqlParameter("@HWScore", SqlDbType.Decimal, 5), new SqlParameter("@StudentScore", SqlDbType.Decimal, 5), new SqlParameter("@StudentScoreOrder", SqlDbType.Decimal, 5), new SqlParameter("@ScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@Hierarchy", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@GradeStudentScoreOrder", SqlDbType.Decimal, 5), new SqlParameter("@GradeScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@GradeHierarchy", SqlDbType.Decimal, 5), new SqlParameter("@StatsGradeStudentHW_ScoreID", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceToResourceFolder_ID;
            cmdParms[1].Value = model.Resource_Name;
            cmdParms[2].Value = model.SchoolID;
            cmdParms[3].Value = model.SchoolName;
            cmdParms[4].Value = model.Gradeid;
            cmdParms[5].Value = model.GradeName;
            cmdParms[6].Value = model.ClassID;
            cmdParms[7].Value = model.ClassName;
            cmdParms[8].Value = model.SubjectID;
            cmdParms[9].Value = model.SubjectName;
            cmdParms[10].Value = model.TeacherID;
            cmdParms[11].Value = model.TeacherName;
            cmdParms[12].Value = model.StudentID;
            cmdParms[13].Value = model.StudentName;
            cmdParms[14].Value = model.StudentIP;
            cmdParms[15].Value = model.AnswerStart;
            cmdParms[0x10].Value = model.AnswerEnd;
            cmdParms[0x11].Value = model.AnswerQTTimeAvg;
            cmdParms[0x12].Value = model.HWScore;
            cmdParms[0x13].Value = model.StudentScore;
            cmdParms[20].Value = model.StudentScoreOrder;
            cmdParms[0x15].Value = model.ScoreImprove;
            cmdParms[0x16].Value = model.Hierarchy;
            cmdParms[0x17].Value = model.CreateTime;
            cmdParms[0x18].Value = model.GradeStudentScoreOrder;
            cmdParms[0x19].Value = model.GradeScoreImprove;
            cmdParms[0x1a].Value = model.GradeHierarchy;
            cmdParms[0x1b].Value = model.StatsGradeStudentHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

