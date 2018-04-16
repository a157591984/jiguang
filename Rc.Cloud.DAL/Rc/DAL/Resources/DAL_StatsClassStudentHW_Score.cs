namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsClassStudentHW_Score
    {
        public bool Add(Model_StatsClassStudentHW_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsClassStudentHW_Score(");
            builder.Append("StatsClassStudentHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,StudentIP,AnswerStart,AnswerEnd,AnswerQTTimeAvg,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,GradeStudentScoreOrder,GradeScoreImprove,GradeHierarchy,HWScoreLevelName,CreateTime,TeacherStudentScoreOrder,TeacherScoreImprove,TeacherHierarchy)");
            builder.Append(" values (");
            builder.Append("@StatsClassStudentHW_ScoreID,@ResourceToResourceFolder_Id,@Resource_Name,@HomeWork_ID,@HomeWork_Name,@HomeWorkCreateTime,@SchoolID,@SchoolName,@Gradeid,@GradeName,@ClassID,@ClassName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@StudentID,@StudentName,@StudentIP,@AnswerStart,@AnswerEnd,@AnswerQTTimeAvg,@HWScore,@StudentScore,@StudentScoreOrder,@ScoreImprove,@Hierarchy,@GradeStudentScoreOrder,@GradeScoreImprove,@GradeHierarchy,@HWScoreLevelName,@CreateTime,@TeacherStudentScoreOrder,@TeacherScoreImprove,@TeacherHierarchy)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsClassStudentHW_ScoreID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_ID", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), 
                new SqlParameter("@StudentID", SqlDbType.Char, 0x24), new SqlParameter("@StudentName", SqlDbType.NVarChar, 250), new SqlParameter("@StudentIP", SqlDbType.VarChar, 50), new SqlParameter("@AnswerStart", SqlDbType.DateTime), new SqlParameter("@AnswerEnd", SqlDbType.DateTime), new SqlParameter("@AnswerQTTimeAvg", SqlDbType.Decimal, 5), new SqlParameter("@HWScore", SqlDbType.Decimal, 5), new SqlParameter("@StudentScore", SqlDbType.Decimal, 5), new SqlParameter("@StudentScoreOrder", SqlDbType.Decimal, 5), new SqlParameter("@ScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@Hierarchy", SqlDbType.Decimal, 5), new SqlParameter("@GradeStudentScoreOrder", SqlDbType.Decimal, 5), new SqlParameter("@GradeScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@GradeHierarchy", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelName", SqlDbType.NVarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime), 
                new SqlParameter("@TeacherStudentScoreOrder", SqlDbType.Decimal, 5), new SqlParameter("@TeacherScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@TeacherHierarchy", SqlDbType.Decimal, 5)
             };
            cmdParms[0].Value = model.StatsClassStudentHW_ScoreID;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.Resource_Name;
            cmdParms[3].Value = model.HomeWork_ID;
            cmdParms[4].Value = model.HomeWork_Name;
            cmdParms[5].Value = model.HomeWorkCreateTime;
            cmdParms[6].Value = model.SchoolID;
            cmdParms[7].Value = model.SchoolName;
            cmdParms[8].Value = model.Gradeid;
            cmdParms[9].Value = model.GradeName;
            cmdParms[10].Value = model.ClassID;
            cmdParms[11].Value = model.ClassName;
            cmdParms[12].Value = model.SubjectID;
            cmdParms[13].Value = model.SubjectName;
            cmdParms[14].Value = model.TeacherID;
            cmdParms[15].Value = model.TeacherName;
            cmdParms[0x10].Value = model.StudentID;
            cmdParms[0x11].Value = model.StudentName;
            cmdParms[0x12].Value = model.StudentIP;
            cmdParms[0x13].Value = model.AnswerStart;
            cmdParms[20].Value = model.AnswerEnd;
            cmdParms[0x15].Value = model.AnswerQTTimeAvg;
            cmdParms[0x16].Value = model.HWScore;
            cmdParms[0x17].Value = model.StudentScore;
            cmdParms[0x18].Value = model.StudentScoreOrder;
            cmdParms[0x19].Value = model.ScoreImprove;
            cmdParms[0x1a].Value = model.Hierarchy;
            cmdParms[0x1b].Value = model.GradeStudentScoreOrder;
            cmdParms[0x1c].Value = model.GradeScoreImprove;
            cmdParms[0x1d].Value = model.GradeHierarchy;
            cmdParms[30].Value = model.HWScoreLevelName;
            cmdParms[0x1f].Value = model.CreateTime;
            cmdParms[0x20].Value = model.TeacherStudentScoreOrder;
            cmdParms[0x21].Value = model.TeacherScoreImprove;
            cmdParms[0x22].Value = model.TeacherHierarchy;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsClassStudentHW_Score DataRowToModel(DataRow row)
        {
            Model_StatsClassStudentHW_Score score = new Model_StatsClassStudentHW_Score();
            if (row != null)
            {
                if (row["StatsClassStudentHW_ScoreID"] != null)
                {
                    score.StatsClassStudentHW_ScoreID = row["StatsClassStudentHW_ScoreID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    score.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    score.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["HomeWork_ID"] != null)
                {
                    score.HomeWork_ID = row["HomeWork_ID"].ToString();
                }
                if (row["HomeWork_Name"] != null)
                {
                    score.HomeWork_Name = row["HomeWork_Name"].ToString();
                }
                if ((row["HomeWorkCreateTime"] != null) && (row["HomeWorkCreateTime"].ToString() != ""))
                {
                    score.HomeWorkCreateTime = new DateTime?(DateTime.Parse(row["HomeWorkCreateTime"].ToString()));
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
                if (row["HWScoreLevelName"] != null)
                {
                    score.HWScoreLevelName = row["HWScoreLevelName"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    score.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if ((row["TeacherStudentScoreOrder"] != null) && (row["TeacherStudentScoreOrder"].ToString() != ""))
                {
                    score.TeacherStudentScoreOrder = new decimal?(decimal.Parse(row["TeacherStudentScoreOrder"].ToString()));
                }
                if ((row["TeacherScoreImprove"] != null) && (row["TeacherScoreImprove"].ToString() != ""))
                {
                    score.TeacherScoreImprove = new decimal?(decimal.Parse(row["TeacherScoreImprove"].ToString()));
                }
                if ((row["TeacherHierarchy"] != null) && (row["TeacherHierarchy"].ToString() != ""))
                {
                    score.TeacherHierarchy = new decimal?(decimal.Parse(row["TeacherHierarchy"].ToString()));
                }
            }
            return score;
        }

        public bool Delete(string StatsClassStudentHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsClassStudentHW_Score ");
            builder.Append(" where StatsClassStudentHW_ScoreID=@StatsClassStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassStudentHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassStudentHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsClassStudentHW_ScoreIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsClassStudentHW_Score ");
            builder.Append(" where StatsClassStudentHW_ScoreID in (" + StatsClassStudentHW_ScoreIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsClassStudentHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsClassStudentHW_Score");
            builder.Append(" where StatsClassStudentHW_ScoreID=@StatsClassStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassStudentHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassStudentHW_ScoreID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsClassStudentHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,StudentIP,AnswerStart,AnswerEnd,AnswerQTTimeAvg,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,GradeStudentScoreOrder,GradeScoreImprove,GradeHierarchy,HWScoreLevelName,CreateTime,TeacherStudentScoreOrder,TeacherScoreImprove,TeacherHierarchy ");
            builder.Append(" FROM StatsClassStudentHW_Score ");
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
            builder.Append(" StatsClassStudentHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,StudentIP,AnswerStart,AnswerEnd,AnswerQTTimeAvg,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,GradeStudentScoreOrder,GradeScoreImprove,GradeHierarchy,HWScoreLevelName,CreateTime,TeacherStudentScoreOrder,TeacherScoreImprove,TeacherHierarchy ");
            builder.Append(" FROM StatsClassStudentHW_Score ");
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
                builder.Append("order by T.StatsClassStudentHW_ScoreID desc");
            }
            builder.Append(")AS Row, T.*  from StatsClassStudentHW_Score T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsClassStudentHW_Score GetModel(string StatsClassStudentHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsClassStudentHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,StudentIP,AnswerStart,AnswerEnd,AnswerQTTimeAvg,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,GradeStudentScoreOrder,GradeScoreImprove,GradeHierarchy,HWScoreLevelName,CreateTime,TeacherStudentScoreOrder,TeacherScoreImprove,TeacherHierarchy from StatsClassStudentHW_Score ");
            builder.Append(" where StatsClassStudentHW_ScoreID=@StatsClassStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassStudentHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassStudentHW_ScoreID;
            new Model_StatsClassStudentHW_Score();
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
            builder.Append("select count(1) FROM StatsClassStudentHW_Score ");
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

        public bool Update(Model_StatsClassStudentHW_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsClassStudentHW_Score set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("Resource_Name=@Resource_Name,");
            builder.Append("HomeWork_ID=@HomeWork_ID,");
            builder.Append("HomeWork_Name=@HomeWork_Name,");
            builder.Append("HomeWorkCreateTime=@HomeWorkCreateTime,");
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
            builder.Append("GradeStudentScoreOrder=@GradeStudentScoreOrder,");
            builder.Append("GradeScoreImprove=@GradeScoreImprove,");
            builder.Append("GradeHierarchy=@GradeHierarchy,");
            builder.Append("HWScoreLevelName=@HWScoreLevelName,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("TeacherStudentScoreOrder=@TeacherStudentScoreOrder,");
            builder.Append("TeacherScoreImprove=@TeacherScoreImprove,");
            builder.Append("TeacherHierarchy=@TeacherHierarchy");
            builder.Append(" where StatsClassStudentHW_ScoreID=@StatsClassStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_ID", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@StudentID", SqlDbType.Char, 0x24), 
                new SqlParameter("@StudentName", SqlDbType.NVarChar, 250), new SqlParameter("@StudentIP", SqlDbType.VarChar, 50), new SqlParameter("@AnswerStart", SqlDbType.DateTime), new SqlParameter("@AnswerEnd", SqlDbType.DateTime), new SqlParameter("@AnswerQTTimeAvg", SqlDbType.Decimal, 5), new SqlParameter("@HWScore", SqlDbType.Decimal, 5), new SqlParameter("@StudentScore", SqlDbType.Decimal, 5), new SqlParameter("@StudentScoreOrder", SqlDbType.Decimal, 5), new SqlParameter("@ScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@Hierarchy", SqlDbType.Decimal, 5), new SqlParameter("@GradeStudentScoreOrder", SqlDbType.Decimal, 5), new SqlParameter("@GradeScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@GradeHierarchy", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelName", SqlDbType.NVarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TeacherStudentScoreOrder", SqlDbType.Decimal, 5), 
                new SqlParameter("@TeacherScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@TeacherHierarchy", SqlDbType.Decimal, 5), new SqlParameter("@StatsClassStudentHW_ScoreID", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.Resource_Name;
            cmdParms[2].Value = model.HomeWork_ID;
            cmdParms[3].Value = model.HomeWork_Name;
            cmdParms[4].Value = model.HomeWorkCreateTime;
            cmdParms[5].Value = model.SchoolID;
            cmdParms[6].Value = model.SchoolName;
            cmdParms[7].Value = model.Gradeid;
            cmdParms[8].Value = model.GradeName;
            cmdParms[9].Value = model.ClassID;
            cmdParms[10].Value = model.ClassName;
            cmdParms[11].Value = model.SubjectID;
            cmdParms[12].Value = model.SubjectName;
            cmdParms[13].Value = model.TeacherID;
            cmdParms[14].Value = model.TeacherName;
            cmdParms[15].Value = model.StudentID;
            cmdParms[0x10].Value = model.StudentName;
            cmdParms[0x11].Value = model.StudentIP;
            cmdParms[0x12].Value = model.AnswerStart;
            cmdParms[0x13].Value = model.AnswerEnd;
            cmdParms[20].Value = model.AnswerQTTimeAvg;
            cmdParms[0x15].Value = model.HWScore;
            cmdParms[0x16].Value = model.StudentScore;
            cmdParms[0x17].Value = model.StudentScoreOrder;
            cmdParms[0x18].Value = model.ScoreImprove;
            cmdParms[0x19].Value = model.Hierarchy;
            cmdParms[0x1a].Value = model.GradeStudentScoreOrder;
            cmdParms[0x1b].Value = model.GradeScoreImprove;
            cmdParms[0x1c].Value = model.GradeHierarchy;
            cmdParms[0x1d].Value = model.HWScoreLevelName;
            cmdParms[30].Value = model.CreateTime;
            cmdParms[0x1f].Value = model.TeacherStudentScoreOrder;
            cmdParms[0x20].Value = model.TeacherScoreImprove;
            cmdParms[0x21].Value = model.TeacherHierarchy;
            cmdParms[0x22].Value = model.StatsClassStudentHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

