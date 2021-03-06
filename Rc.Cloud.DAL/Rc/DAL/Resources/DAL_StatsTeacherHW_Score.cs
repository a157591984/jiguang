﻿namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsTeacherHW_Score
    {
        public bool Add(Model_StatsTeacherHW_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsTeacherHW_Score(");
            builder.Append("StatsTeacherHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,CommittedCount,UncommittedCount,CorrectedCount,UnCorrectedCount,ClassAllCount,HW_Score,StandardDeviation,HighestScore,LowestScore,AVGScore,AVGScoreRate,Median,Mode,CreateTime,AssignedCount)");
            builder.Append(" values (");
            builder.Append("@StatsTeacherHW_ScoreID,@ResourceToResourceFolder_Id,@Resource_Name,@SchoolID,@SchoolName,@Gradeid,@GradeName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@CommittedCount,@UncommittedCount,@CorrectedCount,@UnCorrectedCount,@ClassAllCount,@HW_Score,@StandardDeviation,@HighestScore,@LowestScore,@AVGScore,@AVGScoreRate,@Median,@Mode,@CreateTime,AssignedCount)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsTeacherHW_ScoreID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@CommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@UncommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@UnCorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@ClassAllCount", SqlDbType.Decimal, 5), 
                new SqlParameter("@HW_Score", SqlDbType.Decimal, 5), new SqlParameter("@StandardDeviation", SqlDbType.Decimal, 5), new SqlParameter("@HighestScore", SqlDbType.Decimal, 5), new SqlParameter("@LowestScore", SqlDbType.Decimal, 5), new SqlParameter("@AVGScore", SqlDbType.Decimal, 5), new SqlParameter("@AVGScoreRate", SqlDbType.Decimal, 5), new SqlParameter("@Median", SqlDbType.Decimal, 5), new SqlParameter("@Mode", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@AssignedCount", SqlDbType.Decimal, 5)
             };
            cmdParms[0].Value = model.StatsTeacherHW_ScoreID;
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
            cmdParms[11].Value = model.CommittedCount;
            cmdParms[12].Value = model.UncommittedCount;
            cmdParms[13].Value = model.CorrectedCount;
            cmdParms[14].Value = model.UnCorrectedCount;
            cmdParms[15].Value = model.ClassAllCount;
            cmdParms[0x10].Value = model.HW_Score;
            cmdParms[0x11].Value = model.StandardDeviation;
            cmdParms[0x12].Value = model.HighestScore;
            cmdParms[0x13].Value = model.LowestScore;
            cmdParms[20].Value = model.AVGScore;
            cmdParms[0x15].Value = model.AVGScoreRate;
            cmdParms[0x16].Value = model.Median;
            cmdParms[0x17].Value = model.Mode;
            cmdParms[0x18].Value = model.CreateTime;
            cmdParms[0x19].Value = model.AssignedCount;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsTeacherHW_Score DataRowToModel(DataRow row)
        {
            Model_StatsTeacherHW_Score score = new Model_StatsTeacherHW_Score();
            if (row != null)
            {
                if (row["StatsTeacherHW_ScoreID"] != null)
                {
                    score.StatsTeacherHW_ScoreID = row["StatsTeacherHW_ScoreID"].ToString();
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
                if ((row["CommittedCount"] != null) && (row["CommittedCount"].ToString() != ""))
                {
                    score.CommittedCount = new decimal?(decimal.Parse(row["CommittedCount"].ToString()));
                }
                if ((row["UncommittedCount"] != null) && (row["UncommittedCount"].ToString() != ""))
                {
                    score.UncommittedCount = new decimal?(decimal.Parse(row["UncommittedCount"].ToString()));
                }
                if ((row["CorrectedCount"] != null) && (row["CorrectedCount"].ToString() != ""))
                {
                    score.CorrectedCount = new decimal?(decimal.Parse(row["CorrectedCount"].ToString()));
                }
                if ((row["UnCorrectedCount"] != null) && (row["UnCorrectedCount"].ToString() != ""))
                {
                    score.UnCorrectedCount = new decimal?(decimal.Parse(row["UnCorrectedCount"].ToString()));
                }
                if ((row["ClassAllCount"] != null) && (row["ClassAllCount"].ToString() != ""))
                {
                    score.ClassAllCount = new decimal?(decimal.Parse(row["ClassAllCount"].ToString()));
                }
                if ((row["HW_Score"] != null) && (row["HW_Score"].ToString() != ""))
                {
                    score.HW_Score = new decimal?(decimal.Parse(row["HW_Score"].ToString()));
                }
                if ((row["StandardDeviation"] != null) && (row["StandardDeviation"].ToString() != ""))
                {
                    score.StandardDeviation = new decimal?(decimal.Parse(row["StandardDeviation"].ToString()));
                }
                if ((row["HighestScore"] != null) && (row["HighestScore"].ToString() != ""))
                {
                    score.HighestScore = new decimal?(decimal.Parse(row["HighestScore"].ToString()));
                }
                if ((row["LowestScore"] != null) && (row["LowestScore"].ToString() != ""))
                {
                    score.LowestScore = new decimal?(decimal.Parse(row["LowestScore"].ToString()));
                }
                if ((row["AVGScore"] != null) && (row["AVGScore"].ToString() != ""))
                {
                    score.AVGScore = new decimal?(decimal.Parse(row["AVGScore"].ToString()));
                }
                if ((row["AVGScoreRate"] != null) && (row["AVGScoreRate"].ToString() != ""))
                {
                    score.AVGScoreRate = new decimal?(decimal.Parse(row["AVGScoreRate"].ToString()));
                }
                if ((row["Median"] != null) && (row["Median"].ToString() != ""))
                {
                    score.Median = new decimal?(decimal.Parse(row["Median"].ToString()));
                }
                if ((row["Mode"] != null) && (row["Mode"].ToString() != ""))
                {
                    score.Mode = row["Mode"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    score.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if ((row["AssignedCount"] != null) && (row["AssignedCount"].ToString() != ""))
                {
                    score.AssignedCount = new decimal?(decimal.Parse(row["AssignedCount"].ToString()));
                }
            }
            return score;
        }

        public bool Delete(string StatsTeacherHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsTeacherHW_Score ");
            builder.Append(" where StatsTeacherHW_ScoreID=@StatsTeacherHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsTeacherHW_ScoreIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsTeacherHW_Score ");
            builder.Append(" where StatsTeacherHW_ScoreID in (" + StatsTeacherHW_ScoreIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsTeacherHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsTeacherHW_Score");
            builder.Append(" where StatsTeacherHW_ScoreID=@StatsTeacherHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherHW_ScoreID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsTeacherHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,CommittedCount,UncommittedCount,CorrectedCount,UnCorrectedCount,ClassAllCount,HW_Score,StandardDeviation,HighestScore,LowestScore,AVGScore,AVGScoreRate,Median,Mode,CreateTime,AssignedCount ");
            builder.Append(" FROM StatsTeacherHW_Score ");
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
            builder.Append(" StatsTeacherHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,CommittedCount,UncommittedCount,CorrectedCount,UnCorrectedCount,ClassAllCount,HW_Score,StandardDeviation,HighestScore,LowestScore,AVGScore,AVGScoreRate,Median,Mode,CreateTime,AssignedCount ");
            builder.Append(" FROM StatsTeacherHW_Score ");
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
                builder.Append("order by T.StatsTeacherHW_ScoreID desc");
            }
            builder.Append(")AS Row, T.*  from StatsTeacherHW_Score T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsTeacherHW_Score GetModel(string StatsTeacherHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsTeacherHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,CommittedCount,UncommittedCount,CorrectedCount,UnCorrectedCount,ClassAllCount,HW_Score,StandardDeviation,HighestScore,LowestScore,AVGScore,AVGScoreRate,Median,Mode,CreateTime,AssignedCount from StatsTeacherHW_Score ");
            builder.Append(" where StatsTeacherHW_ScoreID=@StatsTeacherHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherHW_ScoreID;
            new Model_StatsTeacherHW_Score();
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
            builder.Append("select count(1) FROM StatsTeacherHW_Score ");
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

        public bool Update(Model_StatsTeacherHW_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsTeacherHW_Score set ");
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
            builder.Append("CommittedCount=@CommittedCount,");
            builder.Append("UncommittedCount=@UncommittedCount,");
            builder.Append("CorrectedCount=@CorrectedCount,");
            builder.Append("UnCorrectedCount=@UnCorrectedCount,");
            builder.Append("ClassAllCount=@ClassAllCount,");
            builder.Append("HW_Score=@HW_Score,");
            builder.Append("StandardDeviation=@StandardDeviation,");
            builder.Append("HighestScore=@HighestScore,");
            builder.Append("LowestScore=@LowestScore,");
            builder.Append("AVGScore=@AVGScore,");
            builder.Append("AVGScoreRate=@AVGScoreRate,");
            builder.Append("Median=@Median,");
            builder.Append("Mode=@Mode,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append("AssignedCount=@AssignedCount");
            builder.Append(" where StatsTeacherHW_ScoreID=@StatsTeacherHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@CommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@UncommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@UnCorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@ClassAllCount", SqlDbType.Decimal, 5), new SqlParameter("@HW_Score", SqlDbType.Decimal, 5), 
                new SqlParameter("@StandardDeviation", SqlDbType.Decimal, 5), new SqlParameter("@HighestScore", SqlDbType.Decimal, 5), new SqlParameter("@LowestScore", SqlDbType.Decimal, 5), new SqlParameter("@AVGScore", SqlDbType.Decimal, 5), new SqlParameter("@AVGScoreRate", SqlDbType.Decimal, 5), new SqlParameter("@Median", SqlDbType.Decimal, 5), new SqlParameter("@Mode", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@AssignedCount", SqlDbType.Decimal, 5), new SqlParameter("@StatsTeacherHW_ScoreID", SqlDbType.Char, 0x24)
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
            cmdParms[10].Value = model.CommittedCount;
            cmdParms[11].Value = model.UncommittedCount;
            cmdParms[12].Value = model.CorrectedCount;
            cmdParms[13].Value = model.UnCorrectedCount;
            cmdParms[14].Value = model.ClassAllCount;
            cmdParms[15].Value = model.HW_Score;
            cmdParms[0x10].Value = model.StandardDeviation;
            cmdParms[0x11].Value = model.HighestScore;
            cmdParms[0x12].Value = model.LowestScore;
            cmdParms[0x13].Value = model.AVGScore;
            cmdParms[20].Value = model.AVGScoreRate;
            cmdParms[0x15].Value = model.Median;
            cmdParms[0x16].Value = model.Mode;
            cmdParms[0x17].Value = model.CreateTime;
            cmdParms[0x18].Value = model.AssignedCount;
            cmdParms[0x19].Value = model.StatsTeacherHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

