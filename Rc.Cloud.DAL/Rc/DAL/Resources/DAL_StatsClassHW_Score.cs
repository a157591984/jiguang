namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsClassHW_Score
    {
        public bool Add(Model_StatsClassHW_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsClassHW_Score(");
            builder.Append("StatsClassHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,AssignedCount,CommittedCount,CommittedCountRate,UncommittedCount,CorrectedCount,UnCorrectedCount,CorrectedCountRate,ClassAllCount,HW_Score,StandardDeviation,HighestScore,LowestScore,AVGScore,AVGScoreRate,Median,Mode,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsClassHW_ScoreID,@ResourceToResourceFolder_Id,@Resource_Name,@HomeWork_ID,@HomeWork_Name,@HomeWorkCreateTime,@SchoolID,@SchoolName,@Gradeid,@GradeName,@ClassID,@ClassName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@AssignedCount,@CommittedCount,@CommittedCountRate,@UncommittedCount,@CorrectedCount,@UnCorrectedCount,@CorrectedCountRate,@ClassAllCount,@HW_Score,@StandardDeviation,@HighestScore,@LowestScore,@AVGScore,@AVGScoreRate,@Median,@Mode,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsClassHW_ScoreID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_ID", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), 
                new SqlParameter("@AssignedCount", SqlDbType.Decimal, 5), new SqlParameter("@CommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@CommittedCountRate", SqlDbType.Decimal, 5), new SqlParameter("@UncommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@UnCorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCountRate", SqlDbType.Decimal, 5), new SqlParameter("@ClassAllCount", SqlDbType.Decimal, 5), new SqlParameter("@HW_Score", SqlDbType.Decimal, 5), new SqlParameter("@StandardDeviation", SqlDbType.Decimal, 5), new SqlParameter("@HighestScore", SqlDbType.Decimal, 5), new SqlParameter("@LowestScore", SqlDbType.Decimal, 5), new SqlParameter("@AVGScore", SqlDbType.Decimal, 5), new SqlParameter("@AVGScoreRate", SqlDbType.Decimal, 5), new SqlParameter("@Median", SqlDbType.Decimal, 5), new SqlParameter("@Mode", SqlDbType.VarChar, 0x3e8), 
                new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsClassHW_ScoreID;
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
            cmdParms[0x10].Value = model.AssignedCount;
            cmdParms[0x11].Value = model.CommittedCount;
            cmdParms[0x12].Value = model.CommittedCountRate;
            cmdParms[0x13].Value = model.UncommittedCount;
            cmdParms[20].Value = model.CorrectedCount;
            cmdParms[0x15].Value = model.UnCorrectedCount;
            cmdParms[0x16].Value = model.CorrectedCountRate;
            cmdParms[0x17].Value = model.ClassAllCount;
            cmdParms[0x18].Value = model.HW_Score;
            cmdParms[0x19].Value = model.StandardDeviation;
            cmdParms[0x1a].Value = model.HighestScore;
            cmdParms[0x1b].Value = model.LowestScore;
            cmdParms[0x1c].Value = model.AVGScore;
            cmdParms[0x1d].Value = model.AVGScoreRate;
            cmdParms[30].Value = model.Median;
            cmdParms[0x1f].Value = model.Mode;
            cmdParms[0x20].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsClassHW_Score DataRowToModel(DataRow row)
        {
            Model_StatsClassHW_Score score = new Model_StatsClassHW_Score();
            if (row != null)
            {
                if (row["StatsClassHW_ScoreID"] != null)
                {
                    score.StatsClassHW_ScoreID = row["StatsClassHW_ScoreID"].ToString();
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
                if ((row["AssignedCount"] != null) && (row["AssignedCount"].ToString() != ""))
                {
                    score.AssignedCount = new decimal?(decimal.Parse(row["AssignedCount"].ToString()));
                }
                if ((row["CommittedCount"] != null) && (row["CommittedCount"].ToString() != ""))
                {
                    score.CommittedCount = new decimal?(decimal.Parse(row["CommittedCount"].ToString()));
                }
                if ((row["CommittedCountRate"] != null) && (row["CommittedCountRate"].ToString() != ""))
                {
                    score.CommittedCountRate = new decimal?(decimal.Parse(row["CommittedCountRate"].ToString()));
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
                if ((row["CorrectedCountRate"] != null) && (row["CorrectedCountRate"].ToString() != ""))
                {
                    score.CorrectedCountRate = new decimal?(decimal.Parse(row["CorrectedCountRate"].ToString()));
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
            }
            return score;
        }

        public bool Delete(string StatsClassHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsClassHW_Score ");
            builder.Append(" where StatsClassHW_ScoreID=@StatsClassHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsClassHW_ScoreIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsClassHW_Score ");
            builder.Append(" where StatsClassHW_ScoreID in (" + StatsClassHW_ScoreIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsClassHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsClassHW_Score");
            builder.Append(" where StatsClassHW_ScoreID=@StatsClassHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_ScoreID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetAllClass(string ResourceToResourceFolder_Id, string UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select * from (select hw.ResourceToResourceFolder_Id,u.UserGroupOrder,u.UserGroup_Name,ug.User_ID,ug.UserStatus,ug.MembershipEnum,ug.UserGroup_Id,hw.StatsClassHW_ScoreID from UserGroup_Member ug left join dbo.UserGroup u on u.UserGroup_Id=ug.UserGroup_Id left join dbo.StatsClassHW_Score hw on hw.ClassID=ug.UserGroup_Id and ResourceToResourceFolder_Id='{0}' and TeacherId=ug.User_ID) s where User_ID='{1}' and MembershipEnum in('{2}','{3}') and UserStatus=0 order by UserGroupOrder", new object[] { ResourceToResourceFolder_Id, UserId, MembershipEnum.headmaster, MembershipEnum.teacher });
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetGradeAllClass(string ResourceToResourceFolder_Id, string UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select hw.ResourceToResourceFolder_Id,ug.UserGroup_Name, ug.UserGroup_Id,hw.StatsClassHW_ScoreID from UserGroup ug \r\nleft join dbo.StatsClassHW_Score hw on hw.ClassID=ug.UserGroup_Id and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' where User_ID='" + UserId + "'  ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsClassHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,AssignedCount,CommittedCount,CommittedCountRate,UncommittedCount,CorrectedCount,UnCorrectedCount,CorrectedCountRate,ClassAllCount,HW_Score,StandardDeviation,HighestScore,LowestScore,AVGScore,AVGScoreRate,Median,Mode,CreateTime ");
            builder.Append(" FROM StatsClassHW_Score ");
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
            builder.Append(" StatsClassHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,AssignedCount,CommittedCount,CommittedCountRate,UncommittedCount,CorrectedCount,UnCorrectedCount,CorrectedCountRate,ClassAllCount,HW_Score,StandardDeviation,HighestScore,LowestScore,AVGScore,AVGScoreRate,Median,Mode,CreateTime ");
            builder.Append(" FROM StatsClassHW_Score ");
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
                builder.Append("order by T.StatsClassHW_ScoreID desc");
            }
            builder.Append(")AS Row, T.*  from StatsClassHW_Score T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsClassHW_Score GetModel(string StatsClassHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsClassHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,AssignedCount,CommittedCount,CommittedCountRate,UncommittedCount,CorrectedCount,UnCorrectedCount,CorrectedCountRate,ClassAllCount,HW_Score,StandardDeviation,HighestScore,LowestScore,AVGScore,AVGScoreRate,Median,Mode,CreateTime from StatsClassHW_Score ");
            builder.Append(" where StatsClassHW_ScoreID=@StatsClassHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_ScoreID;
            new Model_StatsClassHW_Score();
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
            builder.Append("select count(1) FROM StatsClassHW_Score ");
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

        public bool Update(Model_StatsClassHW_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsClassHW_Score set ");
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
            builder.Append("AssignedCount=@AssignedCount,");
            builder.Append("CommittedCount=@CommittedCount,");
            builder.Append("CommittedCountRate=@CommittedCountRate,");
            builder.Append("UncommittedCount=@UncommittedCount,");
            builder.Append("CorrectedCount=@CorrectedCount,");
            builder.Append("UnCorrectedCount=@UnCorrectedCount,");
            builder.Append("CorrectedCountRate=@CorrectedCountRate,");
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
            builder.Append(" where StatsClassHW_ScoreID=@StatsClassHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_ID", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@AssignedCount", SqlDbType.Decimal, 5), 
                new SqlParameter("@CommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@CommittedCountRate", SqlDbType.Decimal, 5), new SqlParameter("@UncommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@UnCorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCountRate", SqlDbType.Decimal, 5), new SqlParameter("@ClassAllCount", SqlDbType.Decimal, 5), new SqlParameter("@HW_Score", SqlDbType.Decimal, 5), new SqlParameter("@StandardDeviation", SqlDbType.Decimal, 5), new SqlParameter("@HighestScore", SqlDbType.Decimal, 5), new SqlParameter("@LowestScore", SqlDbType.Decimal, 5), new SqlParameter("@AVGScore", SqlDbType.Decimal, 5), new SqlParameter("@AVGScoreRate", SqlDbType.Decimal, 5), new SqlParameter("@Median", SqlDbType.Decimal, 5), new SqlParameter("@Mode", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), 
                new SqlParameter("@StatsClassHW_ScoreID", SqlDbType.Char, 0x24)
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
            cmdParms[15].Value = model.AssignedCount;
            cmdParms[0x10].Value = model.CommittedCount;
            cmdParms[0x11].Value = model.CommittedCountRate;
            cmdParms[0x12].Value = model.UncommittedCount;
            cmdParms[0x13].Value = model.CorrectedCount;
            cmdParms[20].Value = model.UnCorrectedCount;
            cmdParms[0x15].Value = model.CorrectedCountRate;
            cmdParms[0x16].Value = model.ClassAllCount;
            cmdParms[0x17].Value = model.HW_Score;
            cmdParms[0x18].Value = model.StandardDeviation;
            cmdParms[0x19].Value = model.HighestScore;
            cmdParms[0x1a].Value = model.LowestScore;
            cmdParms[0x1b].Value = model.AVGScore;
            cmdParms[0x1c].Value = model.AVGScoreRate;
            cmdParms[0x1d].Value = model.Median;
            cmdParms[30].Value = model.Mode;
            cmdParms[0x1f].Value = model.CreateTime;
            cmdParms[0x20].Value = model.StatsClassHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

