namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsClassHW_KPInData
    {
        public bool Add(Model_StatsClassHW_KPInData model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsClassHW_KPInData(");
            builder.Append("StatsClassHW_KPInDataID,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,DateType,DateData,KPName,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsClassHW_KPInDataID,@SchoolID,@SchoolName,@Gradeid,@GradeName,@ClassID,@ClassName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@DateType,@DateData,@KPName,@KPScoreAvg,@KPScoreAvgRate,@TestQuestionNums,@TestQuestionNumStrs,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsClassHW_KPInDataID", SqlDbType.Char, 0x24), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20), new SqlParameter("@KPName", SqlDbType.NVarChar, 200), new SqlParameter("@KPScoreAvg", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvgRate", SqlDbType.Decimal, 5), 
                new SqlParameter("@TestQuestionNums", SqlDbType.VarChar, 500), new SqlParameter("@TestQuestionNumStrs", SqlDbType.VarChar, 800), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsClassHW_KPInDataID;
            cmdParms[1].Value = model.SchoolID;
            cmdParms[2].Value = model.SchoolName;
            cmdParms[3].Value = model.Gradeid;
            cmdParms[4].Value = model.GradeName;
            cmdParms[5].Value = model.ClassID;
            cmdParms[6].Value = model.ClassName;
            cmdParms[7].Value = model.SubjectID;
            cmdParms[8].Value = model.SubjectName;
            cmdParms[9].Value = model.TeacherID;
            cmdParms[10].Value = model.TeacherName;
            cmdParms[11].Value = model.DateType;
            cmdParms[12].Value = model.DateData;
            cmdParms[13].Value = model.KPName;
            cmdParms[14].Value = model.KPScoreAvg;
            cmdParms[15].Value = model.KPScoreAvgRate;
            cmdParms[0x10].Value = model.TestQuestionNums;
            cmdParms[0x11].Value = model.TestQuestionNumStrs;
            cmdParms[0x12].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsClassHW_KPInData DataRowToModel(DataRow row)
        {
            Model_StatsClassHW_KPInData data = new Model_StatsClassHW_KPInData();
            if (row != null)
            {
                if (row["StatsClassHW_KPInDataID"] != null)
                {
                    data.StatsClassHW_KPInDataID = row["StatsClassHW_KPInDataID"].ToString();
                }
                if (row["SchoolID"] != null)
                {
                    data.SchoolID = row["SchoolID"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    data.SchoolName = row["SchoolName"].ToString();
                }
                if (row["Gradeid"] != null)
                {
                    data.Gradeid = row["Gradeid"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    data.GradeName = row["GradeName"].ToString();
                }
                if (row["ClassID"] != null)
                {
                    data.ClassID = row["ClassID"].ToString();
                }
                if (row["ClassName"] != null)
                {
                    data.ClassName = row["ClassName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    data.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    data.SubjectName = row["SubjectName"].ToString();
                }
                if (row["TeacherID"] != null)
                {
                    data.TeacherID = row["TeacherID"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    data.TeacherName = row["TeacherName"].ToString();
                }
                if (row["DateType"] != null)
                {
                    data.DateType = row["DateType"].ToString();
                }
                if (row["DateData"] != null)
                {
                    data.DateData = row["DateData"].ToString();
                }
                if (row["KPName"] != null)
                {
                    data.KPName = row["KPName"].ToString();
                }
                if ((row["KPScoreAvg"] != null) && (row["KPScoreAvg"].ToString() != ""))
                {
                    data.KPScoreAvg = new decimal?(decimal.Parse(row["KPScoreAvg"].ToString()));
                }
                if ((row["KPScoreAvgRate"] != null) && (row["KPScoreAvgRate"].ToString() != ""))
                {
                    data.KPScoreAvgRate = new decimal?(decimal.Parse(row["KPScoreAvgRate"].ToString()));
                }
                if (row["TestQuestionNums"] != null)
                {
                    data.TestQuestionNums = row["TestQuestionNums"].ToString();
                }
                if (row["TestQuestionNumStrs"] != null)
                {
                    data.TestQuestionNumStrs = row["TestQuestionNumStrs"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    data.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return data;
        }

        public bool Delete(string StatsClassHW_KPInDataID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsClassHW_KPInData ");
            builder.Append(" where StatsClassHW_KPInDataID=@StatsClassHW_KPInDataID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_KPInDataID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_KPInDataID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsClassHW_KPInDataIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsClassHW_KPInData ");
            builder.Append(" where StatsClassHW_KPInDataID in (" + StatsClassHW_KPInDataIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsClassHW_KPInDataID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsClassHW_KPInData");
            builder.Append(" where StatsClassHW_KPInDataID=@StatsClassHW_KPInDataID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_KPInDataID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_KPInDataID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsClassHW_KPInDataID,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,DateType,DateData,KPName,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime ");
            builder.Append(" FROM StatsClassHW_KPInData ");
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
            builder.Append(" StatsClassHW_KPInDataID,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,DateType,DateData,KPName,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime ");
            builder.Append(" FROM StatsClassHW_KPInData ");
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
                builder.Append("order by T.StatsClassHW_KPInDataID desc");
            }
            builder.Append(")AS Row, T.*  from StatsClassHW_KPInData T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsClassHW_KPInData GetModel(string StatsClassHW_KPInDataID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsClassHW_KPInDataID,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,DateType,DateData,KPName,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime from StatsClassHW_KPInData ");
            builder.Append(" where StatsClassHW_KPInDataID=@StatsClassHW_KPInDataID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_KPInDataID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_KPInDataID;
            new Model_StatsClassHW_KPInData();
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
            builder.Append("select count(1) FROM StatsClassHW_KPInData ");
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

        public bool Update(Model_StatsClassHW_KPInData model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsClassHW_KPInData set ");
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
            builder.Append("DateType=@DateType,");
            builder.Append("DateData=@DateData,");
            builder.Append("KPName=@KPName,");
            builder.Append("KPScoreAvg=@KPScoreAvg,");
            builder.Append("KPScoreAvgRate=@KPScoreAvgRate,");
            builder.Append("TestQuestionNums=@TestQuestionNums,");
            builder.Append("TestQuestionNumStrs=@TestQuestionNumStrs,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsClassHW_KPInDataID=@StatsClassHW_KPInDataID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20), new SqlParameter("@KPName", SqlDbType.NVarChar, 200), new SqlParameter("@KPScoreAvg", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvgRate", SqlDbType.Decimal, 5), new SqlParameter("@TestQuestionNums", SqlDbType.VarChar, 500), 
                new SqlParameter("@TestQuestionNumStrs", SqlDbType.VarChar, 800), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsClassHW_KPInDataID", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.SchoolID;
            cmdParms[1].Value = model.SchoolName;
            cmdParms[2].Value = model.Gradeid;
            cmdParms[3].Value = model.GradeName;
            cmdParms[4].Value = model.ClassID;
            cmdParms[5].Value = model.ClassName;
            cmdParms[6].Value = model.SubjectID;
            cmdParms[7].Value = model.SubjectName;
            cmdParms[8].Value = model.TeacherID;
            cmdParms[9].Value = model.TeacherName;
            cmdParms[10].Value = model.DateType;
            cmdParms[11].Value = model.DateData;
            cmdParms[12].Value = model.KPName;
            cmdParms[13].Value = model.KPScoreAvg;
            cmdParms[14].Value = model.KPScoreAvgRate;
            cmdParms[15].Value = model.TestQuestionNums;
            cmdParms[0x10].Value = model.TestQuestionNumStrs;
            cmdParms[0x11].Value = model.CreateTime;
            cmdParms[0x12].Value = model.StatsClassHW_KPInDataID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

