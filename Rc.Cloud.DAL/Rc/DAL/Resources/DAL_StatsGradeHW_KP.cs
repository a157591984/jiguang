namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsGradeHW_KP
    {
        public bool Add(Model_StatsGradeHW_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsGradeHW_KP(");
            builder.Append("StatsGradeHW_KPID,ResourceToResourceFolder_Id,Resource_Name,Gradeid,GradeName,SubjectID,SubjectName,KPName,KPScoreSum,KPScoreStudentSum,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsGradeHW_KPID,@ResourceToResourceFolder_Id,@Resource_Name,@Gradeid,@GradeName,@SubjectID,@SubjectName,@KPName,@KPScoreSum,@KPScoreStudentSum,@KPScoreAvg,@KPScoreAvgRate,@TestQuestionNums,@TestQuestionNumStrs,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeHW_KPID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@KPName", SqlDbType.NVarChar, 250), new SqlParameter("@KPScoreSum", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreStudentSum", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvg", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvgRate", SqlDbType.Decimal, 5), new SqlParameter("@TestQuestionNums", SqlDbType.VarChar, 500), new SqlParameter("@TestQuestionNumStrs", SqlDbType.VarChar, 800), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.StatsGradeHW_KPID;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.Resource_Name;
            cmdParms[3].Value = model.Gradeid;
            cmdParms[4].Value = model.GradeName;
            cmdParms[5].Value = model.SubjectID;
            cmdParms[6].Value = model.SubjectName;
            cmdParms[7].Value = model.KPName;
            cmdParms[8].Value = model.KPScoreSum;
            cmdParms[9].Value = model.KPScoreStudentSum;
            cmdParms[10].Value = model.KPScoreAvg;
            cmdParms[11].Value = model.KPScoreAvgRate;
            cmdParms[12].Value = model.TestQuestionNums;
            cmdParms[13].Value = model.TestQuestionNumStrs;
            cmdParms[14].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsGradeHW_KP DataRowToModel(DataRow row)
        {
            Model_StatsGradeHW_KP ehw_kp = new Model_StatsGradeHW_KP();
            if (row != null)
            {
                if (row["StatsGradeHW_KPID"] != null)
                {
                    ehw_kp.StatsGradeHW_KPID = row["StatsGradeHW_KPID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    ehw_kp.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    ehw_kp.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["Gradeid"] != null)
                {
                    ehw_kp.Gradeid = row["Gradeid"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    ehw_kp.GradeName = row["GradeName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    ehw_kp.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    ehw_kp.SubjectName = row["SubjectName"].ToString();
                }
                if (row["KPName"] != null)
                {
                    ehw_kp.KPName = row["KPName"].ToString();
                }
                if ((row["KPScoreSum"] != null) && (row["KPScoreSum"].ToString() != ""))
                {
                    ehw_kp.KPScoreSum = new decimal?(decimal.Parse(row["KPScoreSum"].ToString()));
                }
                if ((row["KPScoreStudentSum"] != null) && (row["KPScoreStudentSum"].ToString() != ""))
                {
                    ehw_kp.KPScoreStudentSum = new decimal?(decimal.Parse(row["KPScoreStudentSum"].ToString()));
                }
                if ((row["KPScoreAvg"] != null) && (row["KPScoreAvg"].ToString() != ""))
                {
                    ehw_kp.KPScoreAvg = new decimal?(decimal.Parse(row["KPScoreAvg"].ToString()));
                }
                if ((row["KPScoreAvgRate"] != null) && (row["KPScoreAvgRate"].ToString() != ""))
                {
                    ehw_kp.KPScoreAvgRate = new decimal?(decimal.Parse(row["KPScoreAvgRate"].ToString()));
                }
                if (row["TestQuestionNums"] != null)
                {
                    ehw_kp.TestQuestionNums = row["TestQuestionNums"].ToString();
                }
                if (row["TestQuestionNumStrs"] != null)
                {
                    ehw_kp.TestQuestionNumStrs = row["TestQuestionNumStrs"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    ehw_kp.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return ehw_kp;
        }

        public bool Delete(string StatsGradeHW_KPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeHW_KP ");
            builder.Append(" where StatsGradeHW_KPID=@StatsGradeHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeHW_KPID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeHW_KPID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsGradeHW_KPIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeHW_KP ");
            builder.Append(" where StatsGradeHW_KPID in (" + StatsGradeHW_KPIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsGradeHW_KPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsGradeHW_KP");
            builder.Append(" where StatsGradeHW_KPID=@StatsGradeHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeHW_KPID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeHW_KPID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsGradeHW_KPID,ResourceToResourceFolder_Id,Resource_Name,Gradeid,GradeName,SubjectID,SubjectName,KPName,KPScoreSum,KPScoreStudentSum,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime ");
            builder.Append(" FROM StatsGradeHW_KP ");
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
            builder.Append(" StatsGradeHW_KPID,ResourceToResourceFolder_Id,Resource_Name,Gradeid,GradeName,SubjectID,SubjectName,KPName,KPScoreSum,KPScoreStudentSum,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime ");
            builder.Append(" FROM StatsGradeHW_KP ");
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
                builder.Append("order by T.StatsGradeHW_KPID desc");
            }
            builder.Append(")AS Row, T.*  from StatsGradeHW_KP T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsGradeHW_KP GetModel(string StatsGradeHW_KPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsGradeHW_KPID,ResourceToResourceFolder_Id,Resource_Name,Gradeid,GradeName,SubjectID,SubjectName,KPName,KPScoreSum,KPScoreStudentSum,KPScoreAvg,KPScoreAvgRate,TestQuestionNums,TestQuestionNumStrs,CreateTime from StatsGradeHW_KP ");
            builder.Append(" where StatsGradeHW_KPID=@StatsGradeHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeHW_KPID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeHW_KPID;
            new Model_StatsGradeHW_KP();
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
            builder.Append("select count(1) FROM StatsGradeHW_KP ");
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

        public bool Update(Model_StatsGradeHW_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsGradeHW_KP set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("Resource_Name=@Resource_Name,");
            builder.Append("Gradeid=@Gradeid,");
            builder.Append("GradeName=@GradeName,");
            builder.Append("SubjectID=@SubjectID,");
            builder.Append("SubjectName=@SubjectName,");
            builder.Append("KPName=@KPName,");
            builder.Append("KPScoreSum=@KPScoreSum,");
            builder.Append("KPScoreStudentSum=@KPScoreStudentSum,");
            builder.Append("KPScoreAvg=@KPScoreAvg,");
            builder.Append("KPScoreAvgRate=@KPScoreAvgRate,");
            builder.Append("TestQuestionNums=@TestQuestionNums,");
            builder.Append("TestQuestionNumStrs=@TestQuestionNumStrs,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsGradeHW_KPID=@StatsGradeHW_KPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@KPName", SqlDbType.NVarChar, 250), new SqlParameter("@KPScoreSum", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreStudentSum", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvg", SqlDbType.Decimal, 5), new SqlParameter("@KPScoreAvgRate", SqlDbType.Decimal, 5), new SqlParameter("@TestQuestionNums", SqlDbType.VarChar, 500), new SqlParameter("@TestQuestionNumStrs", SqlDbType.VarChar, 800), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsGradeHW_KPID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.Resource_Name;
            cmdParms[2].Value = model.Gradeid;
            cmdParms[3].Value = model.GradeName;
            cmdParms[4].Value = model.SubjectID;
            cmdParms[5].Value = model.SubjectName;
            cmdParms[6].Value = model.KPName;
            cmdParms[7].Value = model.KPScoreSum;
            cmdParms[8].Value = model.KPScoreStudentSum;
            cmdParms[9].Value = model.KPScoreAvg;
            cmdParms[10].Value = model.KPScoreAvgRate;
            cmdParms[11].Value = model.TestQuestionNums;
            cmdParms[12].Value = model.TestQuestionNumStrs;
            cmdParms[13].Value = model.CreateTime;
            cmdParms[14].Value = model.StatsGradeHW_KPID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

