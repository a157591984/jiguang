namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsTeacherStudentHW_Score
    {
        public bool Add(Model_StatsTeacherStudentHW_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsTeacherStudentHW_Score(");
            builder.Append("StatsTeacherStudentHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsTeacherStudentHW_ScoreID,@ResourceToResourceFolder_Id,@Resource_Name,@SchoolID,@SchoolName,@Gradeid,@GradeName,@ClassID,@ClassName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@StudentID,@StudentName,@HWScore,@StudentScore,@StudentScoreOrder,@ScoreImprove,@Hierarchy,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsTeacherStudentHW_ScoreID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@StudentID", SqlDbType.Char, 0x24), new SqlParameter("@StudentName", SqlDbType.NVarChar, 250), new SqlParameter("@HWScore", SqlDbType.Decimal, 5), 
                new SqlParameter("@StudentScore", SqlDbType.Decimal, 5), new SqlParameter("@StudentScoreOrder", SqlDbType.Decimal, 5), new SqlParameter("@ScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@Hierarchy", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsTeacherStudentHW_ScoreID;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
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
            cmdParms[15].Value = model.HWScore;
            cmdParms[0x10].Value = model.StudentScore;
            cmdParms[0x11].Value = model.StudentScoreOrder;
            cmdParms[0x12].Value = model.ScoreImprove;
            cmdParms[0x13].Value = model.Hierarchy;
            cmdParms[20].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsTeacherStudentHW_Score DataRowToModel(DataRow row)
        {
            Model_StatsTeacherStudentHW_Score score = new Model_StatsTeacherStudentHW_Score();
            if (row != null)
            {
                if (row["StatsTeacherStudentHW_ScoreID"] != null)
                {
                    score.StatsTeacherStudentHW_ScoreID = row["StatsTeacherStudentHW_ScoreID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    score.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
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
            }
            return score;
        }

        public bool Delete(string StatsTeacherStudentHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsTeacherStudentHW_Score ");
            builder.Append(" where StatsTeacherStudentHW_ScoreID=@StatsTeacherStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherStudentHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherStudentHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsTeacherStudentHW_ScoreIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsTeacherStudentHW_Score ");
            builder.Append(" where StatsTeacherStudentHW_ScoreID in (" + StatsTeacherStudentHW_ScoreIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsTeacherStudentHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsTeacherStudentHW_Score");
            builder.Append(" where StatsTeacherStudentHW_ScoreID=@StatsTeacherStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherStudentHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherStudentHW_ScoreID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsTeacherStudentHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,CreateTime ");
            builder.Append(" FROM StatsTeacherStudentHW_Score ");
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
            builder.Append(" StatsTeacherStudentHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,CreateTime ");
            builder.Append(" FROM StatsTeacherStudentHW_Score ");
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
                builder.Append("order by T.StatsTeacherStudentHW_ScoreID desc");
            }
            builder.Append(")AS Row, T.*  from StatsTeacherStudentHW_Score T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsTeacherStudentHW_Score GetModel(string StatsTeacherStudentHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsTeacherStudentHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,StudentID,StudentName,HWScore,StudentScore,StudentScoreOrder,ScoreImprove,Hierarchy,CreateTime from StatsTeacherStudentHW_Score ");
            builder.Append(" where StatsTeacherStudentHW_ScoreID=@StatsTeacherStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherStudentHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherStudentHW_ScoreID;
            new Model_StatsTeacherStudentHW_Score();
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
            builder.Append("select count(1) FROM StatsTeacherStudentHW_Score ");
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

        public bool Update(Model_StatsTeacherStudentHW_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsTeacherStudentHW_Score set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
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
            builder.Append("HWScore=@HWScore,");
            builder.Append("StudentScore=@StudentScore,");
            builder.Append("StudentScoreOrder=@StudentScoreOrder,");
            builder.Append("ScoreImprove=@ScoreImprove,");
            builder.Append("Hierarchy=@Hierarchy,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsTeacherStudentHW_ScoreID=@StatsTeacherStudentHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@StudentID", SqlDbType.Char, 0x24), new SqlParameter("@StudentName", SqlDbType.NVarChar, 250), new SqlParameter("@HWScore", SqlDbType.Decimal, 5), new SqlParameter("@StudentScore", SqlDbType.Decimal, 5), 
                new SqlParameter("@StudentScoreOrder", SqlDbType.Decimal, 5), new SqlParameter("@ScoreImprove", SqlDbType.Decimal, 5), new SqlParameter("@Hierarchy", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsTeacherStudentHW_ScoreID", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
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
            cmdParms[14].Value = model.HWScore;
            cmdParms[15].Value = model.StudentScore;
            cmdParms[0x10].Value = model.StudentScoreOrder;
            cmdParms[0x11].Value = model.ScoreImprove;
            cmdParms[0x12].Value = model.Hierarchy;
            cmdParms[0x13].Value = model.CreateTime;
            cmdParms[20].Value = model.StatsTeacherStudentHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

