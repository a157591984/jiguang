namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsClassHW_ScoreLevel
    {
        public bool Add(Model_StatsClassHW_ScoreLevel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsClassHW_ScoreLevel(");
            builder.Append("StatsClassHW_ScoreLevelID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,HWScoreLevelLeft,HWScoreLevelRight,HWScoreLevelCount,HWScoreLevelCountRate,HWScore,ClassAllCount,CorrectedCount,HWScoreLevelDictID,HWScoreLevelName,HWScoreLevelRateLeft,HWScoreLevelRateRight,AddedValue,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsClassHW_ScoreLevelID,@ResourceToResourceFolder_Id,@Resource_Name,@HomeWork_ID,@HomeWork_Name,@HomeWorkCreateTime,@SchoolID,@SchoolName,@Gradeid,@GradeName,@ClassID,@ClassName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@HWScoreLevelLeft,@HWScoreLevelRight,@HWScoreLevelCount,@HWScoreLevelCountRate,@HWScore,@ClassAllCount,@CorrectedCount,@HWScoreLevelDictID,@HWScoreLevelName,@HWScoreLevelRateLeft,@HWScoreLevelRateRight,@AddedValue,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsClassHW_ScoreLevelID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_ID", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), 
                new SqlParameter("@HWScoreLevelLeft", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelRight", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelCount", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelCountRate", SqlDbType.Decimal, 5), new SqlParameter("@HWScore", SqlDbType.Decimal, 5), new SqlParameter("@ClassAllCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelDictID", SqlDbType.Char, 0x24), new SqlParameter("@HWScoreLevelName", SqlDbType.NVarChar, 50), new SqlParameter("@HWScoreLevelRateLeft", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelRateRight", SqlDbType.Decimal, 5), new SqlParameter("@AddedValue", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsClassHW_ScoreLevelID;
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
            cmdParms[0x10].Value = model.HWScoreLevelLeft;
            cmdParms[0x11].Value = model.HWScoreLevelRight;
            cmdParms[0x12].Value = model.HWScoreLevelCount;
            cmdParms[0x13].Value = model.HWScoreLevelCountRate;
            cmdParms[20].Value = model.HWScore;
            cmdParms[0x15].Value = model.ClassAllCount;
            cmdParms[0x16].Value = model.CorrectedCount;
            cmdParms[0x17].Value = model.HWScoreLevelDictID;
            cmdParms[0x18].Value = model.HWScoreLevelName;
            cmdParms[0x19].Value = model.HWScoreLevelRateLeft;
            cmdParms[0x1a].Value = model.HWScoreLevelRateRight;
            cmdParms[0x1b].Value = model.AddedValue;
            cmdParms[0x1c].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsClassHW_ScoreLevel DataRowToModel(DataRow row)
        {
            Model_StatsClassHW_ScoreLevel level = new Model_StatsClassHW_ScoreLevel();
            if (row != null)
            {
                if (row["StatsClassHW_ScoreLevelID"] != null)
                {
                    level.StatsClassHW_ScoreLevelID = row["StatsClassHW_ScoreLevelID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    level.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    level.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["HomeWork_ID"] != null)
                {
                    level.HomeWork_ID = row["HomeWork_ID"].ToString();
                }
                if (row["HomeWork_Name"] != null)
                {
                    level.HomeWork_Name = row["HomeWork_Name"].ToString();
                }
                if ((row["HomeWorkCreateTime"] != null) && (row["HomeWorkCreateTime"].ToString() != ""))
                {
                    level.HomeWorkCreateTime = new DateTime?(DateTime.Parse(row["HomeWorkCreateTime"].ToString()));
                }
                if (row["SchoolID"] != null)
                {
                    level.SchoolID = row["SchoolID"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    level.SchoolName = row["SchoolName"].ToString();
                }
                if (row["Gradeid"] != null)
                {
                    level.Gradeid = row["Gradeid"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    level.GradeName = row["GradeName"].ToString();
                }
                if (row["ClassID"] != null)
                {
                    level.ClassID = row["ClassID"].ToString();
                }
                if (row["ClassName"] != null)
                {
                    level.ClassName = row["ClassName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    level.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    level.SubjectName = row["SubjectName"].ToString();
                }
                if (row["TeacherID"] != null)
                {
                    level.TeacherID = row["TeacherID"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    level.TeacherName = row["TeacherName"].ToString();
                }
                if ((row["HWScoreLevelLeft"] != null) && (row["HWScoreLevelLeft"].ToString() != ""))
                {
                    level.HWScoreLevelLeft = new decimal?(decimal.Parse(row["HWScoreLevelLeft"].ToString()));
                }
                if ((row["HWScoreLevelRight"] != null) && (row["HWScoreLevelRight"].ToString() != ""))
                {
                    level.HWScoreLevelRight = new decimal?(decimal.Parse(row["HWScoreLevelRight"].ToString()));
                }
                if ((row["HWScoreLevelCount"] != null) && (row["HWScoreLevelCount"].ToString() != ""))
                {
                    level.HWScoreLevelCount = new decimal?(decimal.Parse(row["HWScoreLevelCount"].ToString()));
                }
                if ((row["HWScoreLevelCountRate"] != null) && (row["HWScoreLevelCountRate"].ToString() != ""))
                {
                    level.HWScoreLevelCountRate = new decimal?(decimal.Parse(row["HWScoreLevelCountRate"].ToString()));
                }
                if ((row["HWScore"] != null) && (row["HWScore"].ToString() != ""))
                {
                    level.HWScore = new decimal?(decimal.Parse(row["HWScore"].ToString()));
                }
                if ((row["ClassAllCount"] != null) && (row["ClassAllCount"].ToString() != ""))
                {
                    level.ClassAllCount = new decimal?(decimal.Parse(row["ClassAllCount"].ToString()));
                }
                if ((row["CorrectedCount"] != null) && (row["CorrectedCount"].ToString() != ""))
                {
                    level.CorrectedCount = new decimal?(decimal.Parse(row["CorrectedCount"].ToString()));
                }
                if (row["HWScoreLevelDictID"] != null)
                {
                    level.HWScoreLevelDictID = row["HWScoreLevelDictID"].ToString();
                }
                if (row["HWScoreLevelName"] != null)
                {
                    level.HWScoreLevelName = row["HWScoreLevelName"].ToString();
                }
                if ((row["HWScoreLevelRateLeft"] != null) && (row["HWScoreLevelRateLeft"].ToString() != ""))
                {
                    level.HWScoreLevelRateLeft = new decimal?(decimal.Parse(row["HWScoreLevelRateLeft"].ToString()));
                }
                if ((row["HWScoreLevelRateRight"] != null) && (row["HWScoreLevelRateRight"].ToString() != ""))
                {
                    level.HWScoreLevelRateRight = new decimal?(decimal.Parse(row["HWScoreLevelRateRight"].ToString()));
                }
                if ((row["AddedValue"] != null) && (row["AddedValue"].ToString() != ""))
                {
                    level.AddedValue = new decimal?(decimal.Parse(row["AddedValue"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    level.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return level;
        }

        public bool Delete(string StatsClassHW_ScoreLevelID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsClassHW_ScoreLevel ");
            builder.Append(" where StatsClassHW_ScoreLevelID=@StatsClassHW_ScoreLevelID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_ScoreLevelID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_ScoreLevelID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsClassHW_ScoreLevelIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsClassHW_ScoreLevel ");
            builder.Append(" where StatsClassHW_ScoreLevelID in (" + StatsClassHW_ScoreLevelIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsClassHW_ScoreLevelID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsClassHW_ScoreLevel");
            builder.Append(" where StatsClassHW_ScoreLevelID=@StatsClassHW_ScoreLevelID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_ScoreLevelID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_ScoreLevelID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsClassHW_ScoreLevelID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,HWScoreLevelLeft,HWScoreLevelRight,HWScoreLevelCount,HWScoreLevelCountRate,HWScore,ClassAllCount,CorrectedCount,HWScoreLevelDictID,HWScoreLevelName,HWScoreLevelRateLeft,HWScoreLevelRateRight,AddedValue,CreateTime ");
            builder.Append(" FROM StatsClassHW_ScoreLevel ");
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
            builder.Append(" StatsClassHW_ScoreLevelID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,HWScoreLevelLeft,HWScoreLevelRight,HWScoreLevelCount,HWScoreLevelCountRate,HWScore,ClassAllCount,CorrectedCount,HWScoreLevelDictID,HWScoreLevelName,HWScoreLevelRateLeft,HWScoreLevelRateRight,AddedValue,CreateTime ");
            builder.Append(" FROM StatsClassHW_ScoreLevel ");
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
                builder.Append("order by T.StatsClassHW_ScoreLevelID desc");
            }
            builder.Append(")AS Row, T.*  from StatsClassHW_ScoreLevel T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsClassHW_ScoreLevel GetModel(string StatsClassHW_ScoreLevelID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsClassHW_ScoreLevelID,ResourceToResourceFolder_Id,Resource_Name,HomeWork_ID,HomeWork_Name,HomeWorkCreateTime,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,HWScoreLevelLeft,HWScoreLevelRight,HWScoreLevelCount,HWScoreLevelCountRate,HWScore,ClassAllCount,CorrectedCount,HWScoreLevelDictID,HWScoreLevelName,HWScoreLevelRateLeft,HWScoreLevelRateRight,AddedValue,CreateTime from StatsClassHW_ScoreLevel ");
            builder.Append(" where StatsClassHW_ScoreLevelID=@StatsClassHW_ScoreLevelID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_ScoreLevelID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_ScoreLevelID;
            new Model_StatsClassHW_ScoreLevel();
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
            builder.Append("select count(1) FROM StatsClassHW_ScoreLevel ");
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

        public bool Update(Model_StatsClassHW_ScoreLevel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsClassHW_ScoreLevel set ");
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
            builder.Append("HWScoreLevelLeft=@HWScoreLevelLeft,");
            builder.Append("HWScoreLevelRight=@HWScoreLevelRight,");
            builder.Append("HWScoreLevelCount=@HWScoreLevelCount,");
            builder.Append("HWScoreLevelCountRate=@HWScoreLevelCountRate,");
            builder.Append("HWScore=@HWScore,");
            builder.Append("ClassAllCount=@ClassAllCount,");
            builder.Append("CorrectedCount=@CorrectedCount,");
            builder.Append("HWScoreLevelDictID=@HWScoreLevelDictID,");
            builder.Append("HWScoreLevelName=@HWScoreLevelName,");
            builder.Append("HWScoreLevelRateLeft=@HWScoreLevelRateLeft,");
            builder.Append("HWScoreLevelRateRight=@HWScoreLevelRateRight,");
            builder.Append("AddedValue=@AddedValue,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsClassHW_ScoreLevelID=@StatsClassHW_ScoreLevelID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_ID", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@HWScoreLevelLeft", SqlDbType.Decimal, 5), 
                new SqlParameter("@HWScoreLevelRight", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelCount", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelCountRate", SqlDbType.Decimal, 5), new SqlParameter("@HWScore", SqlDbType.Decimal, 5), new SqlParameter("@ClassAllCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelDictID", SqlDbType.Char, 0x24), new SqlParameter("@HWScoreLevelName", SqlDbType.NVarChar, 50), new SqlParameter("@HWScoreLevelRateLeft", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelRateRight", SqlDbType.Decimal, 5), new SqlParameter("@AddedValue", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsClassHW_ScoreLevelID", SqlDbType.Char, 0x24)
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
            cmdParms[15].Value = model.HWScoreLevelLeft;
            cmdParms[0x10].Value = model.HWScoreLevelRight;
            cmdParms[0x11].Value = model.HWScoreLevelCount;
            cmdParms[0x12].Value = model.HWScoreLevelCountRate;
            cmdParms[0x13].Value = model.HWScore;
            cmdParms[20].Value = model.ClassAllCount;
            cmdParms[0x15].Value = model.CorrectedCount;
            cmdParms[0x16].Value = model.HWScoreLevelDictID;
            cmdParms[0x17].Value = model.HWScoreLevelName;
            cmdParms[0x18].Value = model.HWScoreLevelRateLeft;
            cmdParms[0x19].Value = model.HWScoreLevelRateRight;
            cmdParms[0x1a].Value = model.AddedValue;
            cmdParms[0x1b].Value = model.CreateTime;
            cmdParms[0x1c].Value = model.StatsClassHW_ScoreLevelID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

