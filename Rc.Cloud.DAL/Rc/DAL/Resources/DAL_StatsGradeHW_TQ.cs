namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsGradeHW_TQ
    {
        public bool Add(Model_StatsGradeHW_TQ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsGradeHW_TQ(");
            builder.Append("StatsGradeHW_TQID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TestQuestions_ID,TestQuestions_Score_ID,TestQuestions_Num,TestQuestions_OrderNum,topicNumber,TestQuestions_Type,TargetText,ContentText,complexityText,TQ_Score,ScoreAvg,ScoreAvgRate,StandardDeviation,Discrimination,ErrorRate,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsGradeHW_TQID,@ResourceToResourceFolder_Id,@Resource_Name,@SchoolID,@SchoolName,@Gradeid,@GradeName,@SubjectID,@SubjectName,@TestQuestions_ID,@TestQuestions_Score_ID,@TestQuestions_Num,@TestQuestions_OrderNum,@topicNumber,@TestQuestions_Type,@TargetText,@ContentText,@complexityText,@TQ_Score,@ScoreAvg,@ScoreAvgRate,@StandardDeviation,@Discrimination,@ErrorRate,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsGradeHW_TQID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TestQuestions_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_OrderNum", SqlDbType.Int, 4), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@TestQuestions_Type", SqlDbType.VarChar, 50), new SqlParameter("@TargetText", SqlDbType.VarChar, 200), 
                new SqlParameter("@ContentText", SqlDbType.NVarChar, 200), new SqlParameter("@complexityText", SqlDbType.VarChar, 200), new SqlParameter("@TQ_Score", SqlDbType.Decimal, 5), new SqlParameter("@ScoreAvg", SqlDbType.Decimal, 5), new SqlParameter("@ScoreAvgRate", SqlDbType.Decimal, 5), new SqlParameter("@StandardDeviation", SqlDbType.Decimal, 5), new SqlParameter("@Discrimination", SqlDbType.Decimal, 5), new SqlParameter("@ErrorRate", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsGradeHW_TQID;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.Resource_Name;
            cmdParms[3].Value = model.SchoolID;
            cmdParms[4].Value = model.SchoolName;
            cmdParms[5].Value = model.Gradeid;
            cmdParms[6].Value = model.GradeName;
            cmdParms[7].Value = model.SubjectID;
            cmdParms[8].Value = model.SubjectName;
            cmdParms[9].Value = model.TestQuestions_ID;
            cmdParms[10].Value = model.TestQuestions_Score_ID;
            cmdParms[11].Value = model.TestQuestions_Num;
            cmdParms[12].Value = model.TestQuestions_OrderNum;
            cmdParms[13].Value = model.topicNumber;
            cmdParms[14].Value = model.TestQuestions_Type;
            cmdParms[15].Value = model.TargetText;
            cmdParms[0x10].Value = model.ContentText;
            cmdParms[0x11].Value = model.complexityText;
            cmdParms[0x12].Value = model.TQ_Score;
            cmdParms[0x13].Value = model.ScoreAvg;
            cmdParms[20].Value = model.ScoreAvgRate;
            cmdParms[0x15].Value = model.StandardDeviation;
            cmdParms[0x16].Value = model.Discrimination;
            cmdParms[0x17].Value = model.ErrorRate;
            cmdParms[0x18].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsGradeHW_TQ DataRowToModel(DataRow row)
        {
            Model_StatsGradeHW_TQ ehw_tq = new Model_StatsGradeHW_TQ();
            if (row != null)
            {
                if (row["StatsGradeHW_TQID"] != null)
                {
                    ehw_tq.StatsGradeHW_TQID = row["StatsGradeHW_TQID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    ehw_tq.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    ehw_tq.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["SchoolID"] != null)
                {
                    ehw_tq.SchoolID = row["SchoolID"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    ehw_tq.SchoolName = row["SchoolName"].ToString();
                }
                if (row["Gradeid"] != null)
                {
                    ehw_tq.Gradeid = row["Gradeid"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    ehw_tq.GradeName = row["GradeName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    ehw_tq.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    ehw_tq.SubjectName = row["SubjectName"].ToString();
                }
                if (row["TestQuestions_ID"] != null)
                {
                    ehw_tq.TestQuestions_ID = row["TestQuestions_ID"].ToString();
                }
                if (row["TestQuestions_Score_ID"] != null)
                {
                    ehw_tq.TestQuestions_Score_ID = row["TestQuestions_Score_ID"].ToString();
                }
                if ((row["TestQuestions_Num"] != null) && (row["TestQuestions_Num"].ToString() != ""))
                {
                    ehw_tq.TestQuestions_Num = new int?(int.Parse(row["TestQuestions_Num"].ToString()));
                }
                if ((row["TestQuestions_OrderNum"] != null) && (row["TestQuestions_OrderNum"].ToString() != ""))
                {
                    ehw_tq.TestQuestions_OrderNum = new int?(int.Parse(row["TestQuestions_OrderNum"].ToString()));
                }
                if (row["topicNumber"] != null)
                {
                    ehw_tq.topicNumber = row["topicNumber"].ToString();
                }
                if (row["TestQuestions_Type"] != null)
                {
                    ehw_tq.TestQuestions_Type = row["TestQuestions_Type"].ToString();
                }
                if (row["TargetText"] != null)
                {
                    ehw_tq.TargetText = row["TargetText"].ToString();
                }
                if (row["ContentText"] != null)
                {
                    ehw_tq.ContentText = row["ContentText"].ToString();
                }
                if (row["complexityText"] != null)
                {
                    ehw_tq.complexityText = row["complexityText"].ToString();
                }
                if ((row["TQ_Score"] != null) && (row["TQ_Score"].ToString() != ""))
                {
                    ehw_tq.TQ_Score = new decimal?(decimal.Parse(row["TQ_Score"].ToString()));
                }
                if ((row["ScoreAvg"] != null) && (row["ScoreAvg"].ToString() != ""))
                {
                    ehw_tq.ScoreAvg = new decimal?(decimal.Parse(row["ScoreAvg"].ToString()));
                }
                if ((row["ScoreAvgRate"] != null) && (row["ScoreAvgRate"].ToString() != ""))
                {
                    ehw_tq.ScoreAvgRate = new decimal?(decimal.Parse(row["ScoreAvgRate"].ToString()));
                }
                if ((row["StandardDeviation"] != null) && (row["StandardDeviation"].ToString() != ""))
                {
                    ehw_tq.StandardDeviation = new decimal?(decimal.Parse(row["StandardDeviation"].ToString()));
                }
                if ((row["Discrimination"] != null) && (row["Discrimination"].ToString() != ""))
                {
                    ehw_tq.Discrimination = new decimal?(decimal.Parse(row["Discrimination"].ToString()));
                }
                if ((row["ErrorRate"] != null) && (row["ErrorRate"].ToString() != ""))
                {
                    ehw_tq.ErrorRate = new decimal?(decimal.Parse(row["ErrorRate"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    ehw_tq.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return ehw_tq;
        }

        public bool Delete(string StatsGradeHW_TQID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeHW_TQ ");
            builder.Append(" where StatsGradeHW_TQID=@StatsGradeHW_TQID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeHW_TQID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeHW_TQID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsGradeHW_TQIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeHW_TQ ");
            builder.Append(" where StatsGradeHW_TQID in (" + StatsGradeHW_TQIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsGradeHW_TQID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsGradeHW_TQ");
            builder.Append(" where StatsGradeHW_TQID=@StatsGradeHW_TQID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeHW_TQID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeHW_TQID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsGradeHW_TQID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TestQuestions_ID,TestQuestions_Score_ID,TestQuestions_Num,TestQuestions_OrderNum,topicNumber,TestQuestions_Type,TargetText,ContentText,complexityText,TQ_Score,ScoreAvg,ScoreAvgRate,StandardDeviation,Discrimination,ErrorRate,CreateTime ");
            builder.Append(" FROM StatsGradeHW_TQ ");
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
            builder.Append(" StatsGradeHW_TQID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TestQuestions_ID,TestQuestions_Score_ID,TestQuestions_Num,TestQuestions_OrderNum,topicNumber,TestQuestions_Type,TargetText,ContentText,complexityText,TQ_Score,ScoreAvg,ScoreAvgRate,StandardDeviation,Discrimination,ErrorRate,CreateTime ");
            builder.Append(" FROM StatsGradeHW_TQ ");
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
                builder.Append("order by T.StatsGradeHW_TQID desc");
            }
            builder.Append(")AS Row, T.*  from StatsGradeHW_TQ T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsGradeHW_TQ GetModel(string StatsGradeHW_TQID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsGradeHW_TQID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TestQuestions_ID,TestQuestions_Score_ID,TestQuestions_Num,TestQuestions_OrderNum,topicNumber,TestQuestions_Type,TargetText,ContentText,complexityText,TQ_Score,ScoreAvg,ScoreAvgRate,StandardDeviation,Discrimination,ErrorRate,CreateTime from StatsGradeHW_TQ ");
            builder.Append(" where StatsGradeHW_TQID=@StatsGradeHW_TQID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeHW_TQID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeHW_TQID;
            new Model_StatsGradeHW_TQ();
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
            builder.Append("select count(1) FROM StatsGradeHW_TQ ");
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

        public bool Update(Model_StatsGradeHW_TQ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsGradeHW_TQ set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("Resource_Name=@Resource_Name,");
            builder.Append("SchoolID=@SchoolID,");
            builder.Append("SchoolName=@SchoolName,");
            builder.Append("Gradeid=@Gradeid,");
            builder.Append("GradeName=@GradeName,");
            builder.Append("SubjectID=@SubjectID,");
            builder.Append("SubjectName=@SubjectName,");
            builder.Append("TestQuestions_ID=@TestQuestions_ID,");
            builder.Append("TestQuestions_Score_ID=@TestQuestions_Score_ID,");
            builder.Append("TestQuestions_Num=@TestQuestions_Num,");
            builder.Append("TestQuestions_OrderNum=@TestQuestions_OrderNum,");
            builder.Append("topicNumber=@topicNumber,");
            builder.Append("TestQuestions_Type=@TestQuestions_Type,");
            builder.Append("TargetText=@TargetText,");
            builder.Append("ContentText=@ContentText,");
            builder.Append("complexityText=@complexityText,");
            builder.Append("TQ_Score=@TQ_Score,");
            builder.Append("ScoreAvg=@ScoreAvg,");
            builder.Append("ScoreAvgRate=@ScoreAvgRate,");
            builder.Append("StandardDeviation=@StandardDeviation,");
            builder.Append("Discrimination=@Discrimination,");
            builder.Append("ErrorRate=@ErrorRate,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsGradeHW_TQID=@StatsGradeHW_TQID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TestQuestions_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_OrderNum", SqlDbType.Int, 4), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@TestQuestions_Type", SqlDbType.VarChar, 50), new SqlParameter("@TargetText", SqlDbType.VarChar, 200), new SqlParameter("@ContentText", SqlDbType.NVarChar, 200), 
                new SqlParameter("@complexityText", SqlDbType.VarChar, 200), new SqlParameter("@TQ_Score", SqlDbType.Decimal, 5), new SqlParameter("@ScoreAvg", SqlDbType.Decimal, 5), new SqlParameter("@ScoreAvgRate", SqlDbType.Decimal, 5), new SqlParameter("@StandardDeviation", SqlDbType.Decimal, 5), new SqlParameter("@Discrimination", SqlDbType.Decimal, 5), new SqlParameter("@ErrorRate", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsGradeHW_TQID", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.Resource_Name;
            cmdParms[2].Value = model.SchoolID;
            cmdParms[3].Value = model.SchoolName;
            cmdParms[4].Value = model.Gradeid;
            cmdParms[5].Value = model.GradeName;
            cmdParms[6].Value = model.SubjectID;
            cmdParms[7].Value = model.SubjectName;
            cmdParms[8].Value = model.TestQuestions_ID;
            cmdParms[9].Value = model.TestQuestions_Score_ID;
            cmdParms[10].Value = model.TestQuestions_Num;
            cmdParms[11].Value = model.TestQuestions_OrderNum;
            cmdParms[12].Value = model.topicNumber;
            cmdParms[13].Value = model.TestQuestions_Type;
            cmdParms[14].Value = model.TargetText;
            cmdParms[15].Value = model.ContentText;
            cmdParms[0x10].Value = model.complexityText;
            cmdParms[0x11].Value = model.TQ_Score;
            cmdParms[0x12].Value = model.ScoreAvg;
            cmdParms[0x13].Value = model.ScoreAvgRate;
            cmdParms[20].Value = model.StandardDeviation;
            cmdParms[0x15].Value = model.Discrimination;
            cmdParms[0x16].Value = model.ErrorRate;
            cmdParms[0x17].Value = model.CreateTime;
            cmdParms[0x18].Value = model.StatsGradeHW_TQID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

