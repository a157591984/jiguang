namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsGradeHW_Subsection
    {
        public bool Add(Model_StatsGradeHW_Subsection model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsGradeHW_Subsection(");
            builder.Append("StatsGradeHW_SubsectionID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,SubsectionName,SubsectionCount,HighestScore,LowestScore,AVGScore,HWScore,GradeAllCount,CorrectedCount,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsGradeHW_SubsectionID,@ResourceToResourceFolder_Id,@Resource_Name,@SchoolID,@SchoolName,@Gradeid,@GradeName,@SubjectID,@SubjectName,@SubsectionName,@SubsectionCount,@HighestScore,@LowestScore,@AVGScore,@HWScore,@GradeAllCount,@CorrectedCount,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsGradeHW_SubsectionID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@SubsectionName", SqlDbType.NVarChar, 20), new SqlParameter("@SubsectionCount", SqlDbType.Decimal, 5), new SqlParameter("@HighestScore", SqlDbType.Decimal, 5), new SqlParameter("@LowestScore", SqlDbType.Decimal, 5), new SqlParameter("@AVGScore", SqlDbType.Decimal, 5), new SqlParameter("@HWScore", SqlDbType.Decimal, 5), new SqlParameter("@GradeAllCount", SqlDbType.Decimal, 5), 
                new SqlParameter("@CorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsGradeHW_SubsectionID;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.Resource_Name;
            cmdParms[3].Value = model.SchoolID;
            cmdParms[4].Value = model.SchoolName;
            cmdParms[5].Value = model.Gradeid;
            cmdParms[6].Value = model.GradeName;
            cmdParms[7].Value = model.SubjectID;
            cmdParms[8].Value = model.SubjectName;
            cmdParms[9].Value = model.SubsectionName;
            cmdParms[10].Value = model.SubsectionCount;
            cmdParms[11].Value = model.HighestScore;
            cmdParms[12].Value = model.LowestScore;
            cmdParms[13].Value = model.AVGScore;
            cmdParms[14].Value = model.HWScore;
            cmdParms[15].Value = model.GradeAllCount;
            cmdParms[0x10].Value = model.CorrectedCount;
            cmdParms[0x11].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsGradeHW_Subsection DataRowToModel(DataRow row)
        {
            Model_StatsGradeHW_Subsection subsection = new Model_StatsGradeHW_Subsection();
            if (row != null)
            {
                if (row["StatsGradeHW_SubsectionID"] != null)
                {
                    subsection.StatsGradeHW_SubsectionID = row["StatsGradeHW_SubsectionID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    subsection.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    subsection.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["SchoolID"] != null)
                {
                    subsection.SchoolID = row["SchoolID"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    subsection.SchoolName = row["SchoolName"].ToString();
                }
                if (row["Gradeid"] != null)
                {
                    subsection.Gradeid = row["Gradeid"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    subsection.GradeName = row["GradeName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    subsection.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    subsection.SubjectName = row["SubjectName"].ToString();
                }
                if (row["SubsectionName"] != null)
                {
                    subsection.SubsectionName = row["SubsectionName"].ToString();
                }
                if ((row["SubsectionCount"] != null) && (row["SubsectionCount"].ToString() != ""))
                {
                    subsection.SubsectionCount = new decimal?(decimal.Parse(row["SubsectionCount"].ToString()));
                }
                if ((row["HighestScore"] != null) && (row["HighestScore"].ToString() != ""))
                {
                    subsection.HighestScore = new decimal?(decimal.Parse(row["HighestScore"].ToString()));
                }
                if ((row["LowestScore"] != null) && (row["LowestScore"].ToString() != ""))
                {
                    subsection.LowestScore = new decimal?(decimal.Parse(row["LowestScore"].ToString()));
                }
                if ((row["AVGScore"] != null) && (row["AVGScore"].ToString() != ""))
                {
                    subsection.AVGScore = new decimal?(decimal.Parse(row["AVGScore"].ToString()));
                }
                if ((row["HWScore"] != null) && (row["HWScore"].ToString() != ""))
                {
                    subsection.HWScore = new decimal?(decimal.Parse(row["HWScore"].ToString()));
                }
                if ((row["GradeAllCount"] != null) && (row["GradeAllCount"].ToString() != ""))
                {
                    subsection.GradeAllCount = new decimal?(decimal.Parse(row["GradeAllCount"].ToString()));
                }
                if ((row["CorrectedCount"] != null) && (row["CorrectedCount"].ToString() != ""))
                {
                    subsection.CorrectedCount = new decimal?(decimal.Parse(row["CorrectedCount"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    subsection.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return subsection;
        }

        public bool Delete(string StatsGradeHW_SubsectionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeHW_Subsection ");
            builder.Append(" where StatsGradeHW_SubsectionID=@StatsGradeHW_SubsectionID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeHW_SubsectionID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeHW_SubsectionID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsGradeHW_SubsectionIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeHW_Subsection ");
            builder.Append(" where StatsGradeHW_SubsectionID in (" + StatsGradeHW_SubsectionIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsGradeHW_SubsectionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsGradeHW_Subsection");
            builder.Append(" where StatsGradeHW_SubsectionID=@StatsGradeHW_SubsectionID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeHW_SubsectionID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeHW_SubsectionID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsGradeHW_SubsectionID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,SubsectionName,SubsectionCount,HighestScore,LowestScore,AVGScore,HWScore,GradeAllCount,CorrectedCount,CreateTime ");
            builder.Append(" FROM StatsGradeHW_Subsection ");
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
            builder.Append(" StatsGradeHW_SubsectionID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,SubsectionName,SubsectionCount,HighestScore,LowestScore,AVGScore,HWScore,GradeAllCount,CorrectedCount,CreateTime ");
            builder.Append(" FROM StatsGradeHW_Subsection ");
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
                builder.Append("order by T.StatsGradeHW_SubsectionID desc");
            }
            builder.Append(")AS Row, T.*  from StatsGradeHW_Subsection T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsGradeHW_Subsection GetModel(string StatsGradeHW_SubsectionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsGradeHW_SubsectionID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,SubsectionName,SubsectionCount,HighestScore,LowestScore,AVGScore,HWScore,GradeAllCount,CorrectedCount,CreateTime from StatsGradeHW_Subsection ");
            builder.Append(" where StatsGradeHW_SubsectionID=@StatsGradeHW_SubsectionID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeHW_SubsectionID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeHW_SubsectionID;
            new Model_StatsGradeHW_Subsection();
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
            builder.Append("select count(1) FROM StatsGradeHW_Subsection ");
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

        public bool Update(Model_StatsGradeHW_Subsection model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsGradeHW_Subsection set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("Resource_Name=@Resource_Name,");
            builder.Append("SchoolID=@SchoolID,");
            builder.Append("SchoolName=@SchoolName,");
            builder.Append("Gradeid=@Gradeid,");
            builder.Append("GradeName=@GradeName,");
            builder.Append("SubjectID=@SubjectID,");
            builder.Append("SubjectName=@SubjectName,");
            builder.Append("SubsectionName=@SubsectionName,");
            builder.Append("SubsectionCount=@SubsectionCount,");
            builder.Append("HighestScore=@HighestScore,");
            builder.Append("LowestScore=@LowestScore,");
            builder.Append("AVGScore=@AVGScore,");
            builder.Append("HWScore=@HWScore,");
            builder.Append("GradeAllCount=@GradeAllCount,");
            builder.Append("CorrectedCount=@CorrectedCount,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsGradeHW_SubsectionID=@StatsGradeHW_SubsectionID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@SubsectionName", SqlDbType.NVarChar, 20), new SqlParameter("@SubsectionCount", SqlDbType.Decimal, 5), new SqlParameter("@HighestScore", SqlDbType.Decimal, 5), new SqlParameter("@LowestScore", SqlDbType.Decimal, 5), new SqlParameter("@AVGScore", SqlDbType.Decimal, 5), new SqlParameter("@HWScore", SqlDbType.Decimal, 5), new SqlParameter("@GradeAllCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCount", SqlDbType.Decimal, 5), 
                new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsGradeHW_SubsectionID", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.Resource_Name;
            cmdParms[2].Value = model.SchoolID;
            cmdParms[3].Value = model.SchoolName;
            cmdParms[4].Value = model.Gradeid;
            cmdParms[5].Value = model.GradeName;
            cmdParms[6].Value = model.SubjectID;
            cmdParms[7].Value = model.SubjectName;
            cmdParms[8].Value = model.SubsectionName;
            cmdParms[9].Value = model.SubsectionCount;
            cmdParms[10].Value = model.HighestScore;
            cmdParms[11].Value = model.LowestScore;
            cmdParms[12].Value = model.AVGScore;
            cmdParms[13].Value = model.HWScore;
            cmdParms[14].Value = model.GradeAllCount;
            cmdParms[15].Value = model.CorrectedCount;
            cmdParms[0x10].Value = model.CreateTime;
            cmdParms[0x11].Value = model.StatsGradeHW_SubsectionID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

