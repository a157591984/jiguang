namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsTeacherHW_TQ
    {
        public bool Add(Model_StatsTeacherHW_TQ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsTeacherHW_TQ(");
            builder.Append("StatsTeacherHW_TQID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,TestQuestions_ID,TestQuestions_Score_ID,TestQuestions_Num,TestQuestions_OrderNum,topicNumber,TestQuestions_Type,TargetText,ContentText,complexityText,TQ_Score,ScoreAvg,ScoreAvgRate,StandardDeviation,Discrimination,ErrorRate,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsTeacherHW_TQID,@ResourceToResourceFolder_Id,@Resource_Name,@SchoolID,@SchoolName,@Gradeid,@GradeName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@TestQuestions_ID,@TestQuestions_Score_ID,@TestQuestions_Num,@TestQuestions_OrderNum,@topicNumber,@TestQuestions_Type,@TargetText,@ContentText,@complexityText,@TQ_Score,@ScoreAvg,@ScoreAvgRate,@StandardDeviation,@Discrimination,@ErrorRate,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsTeacherHW_TQID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@TestQuestions_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_OrderNum", SqlDbType.Int, 4), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), 
                new SqlParameter("@TestQuestions_Type", SqlDbType.VarChar, 50), new SqlParameter("@TargetText", SqlDbType.VarChar, 200), new SqlParameter("@ContentText", SqlDbType.NVarChar, 200), new SqlParameter("@complexityText", SqlDbType.VarChar, 200), new SqlParameter("@TQ_Score", SqlDbType.Decimal, 5), new SqlParameter("@ScoreAvg", SqlDbType.Decimal, 5), new SqlParameter("@ScoreAvgRate", SqlDbType.Decimal, 5), new SqlParameter("@StandardDeviation", SqlDbType.Decimal, 5), new SqlParameter("@Discrimination", SqlDbType.Decimal, 5), new SqlParameter("@ErrorRate", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsTeacherHW_TQID;
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
            cmdParms[11].Value = model.TestQuestions_ID;
            cmdParms[12].Value = model.TestQuestions_Score_ID;
            cmdParms[13].Value = model.TestQuestions_Num;
            cmdParms[14].Value = model.TestQuestions_OrderNum;
            cmdParms[15].Value = model.topicNumber;
            cmdParms[0x10].Value = model.TestQuestions_Type;
            cmdParms[0x11].Value = model.TargetText;
            cmdParms[0x12].Value = model.ContentText;
            cmdParms[0x13].Value = model.complexityText;
            cmdParms[20].Value = model.TQ_Score;
            cmdParms[0x15].Value = model.ScoreAvg;
            cmdParms[0x16].Value = model.ScoreAvgRate;
            cmdParms[0x17].Value = model.StandardDeviation;
            cmdParms[0x18].Value = model.Discrimination;
            cmdParms[0x19].Value = model.ErrorRate;
            cmdParms[0x1a].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsTeacherHW_TQ DataRowToModel(DataRow row)
        {
            Model_StatsTeacherHW_TQ rhw_tq = new Model_StatsTeacherHW_TQ();
            if (row != null)
            {
                if (row["StatsTeacherHW_TQID"] != null)
                {
                    rhw_tq.StatsTeacherHW_TQID = row["StatsTeacherHW_TQID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    rhw_tq.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    rhw_tq.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["SchoolID"] != null)
                {
                    rhw_tq.SchoolID = row["SchoolID"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    rhw_tq.SchoolName = row["SchoolName"].ToString();
                }
                if (row["Gradeid"] != null)
                {
                    rhw_tq.Gradeid = row["Gradeid"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    rhw_tq.GradeName = row["GradeName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    rhw_tq.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    rhw_tq.SubjectName = row["SubjectName"].ToString();
                }
                if (row["TeacherID"] != null)
                {
                    rhw_tq.TeacherID = row["TeacherID"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    rhw_tq.TeacherName = row["TeacherName"].ToString();
                }
                if (row["TestQuestions_ID"] != null)
                {
                    rhw_tq.TestQuestions_ID = row["TestQuestions_ID"].ToString();
                }
                if (row["TestQuestions_Score_ID"] != null)
                {
                    rhw_tq.TestQuestions_Score_ID = row["TestQuestions_Score_ID"].ToString();
                }
                if ((row["TestQuestions_Num"] != null) && (row["TestQuestions_Num"].ToString() != ""))
                {
                    rhw_tq.TestQuestions_Num = new int?(int.Parse(row["TestQuestions_Num"].ToString()));
                }
                if ((row["TestQuestions_OrderNum"] != null) && (row["TestQuestions_OrderNum"].ToString() != ""))
                {
                    rhw_tq.TestQuestions_OrderNum = new int?(int.Parse(row["TestQuestions_OrderNum"].ToString()));
                }
                if (row["topicNumber"] != null)
                {
                    rhw_tq.topicNumber = row["topicNumber"].ToString();
                }
                if (row["TestQuestions_Type"] != null)
                {
                    rhw_tq.TestQuestions_Type = row["TestQuestions_Type"].ToString();
                }
                if (row["TargetText"] != null)
                {
                    rhw_tq.TargetText = row["TargetText"].ToString();
                }
                if (row["ContentText"] != null)
                {
                    rhw_tq.ContentText = row["ContentText"].ToString();
                }
                if (row["complexityText"] != null)
                {
                    rhw_tq.complexityText = row["complexityText"].ToString();
                }
                if ((row["TQ_Score"] != null) && (row["TQ_Score"].ToString() != ""))
                {
                    rhw_tq.TQ_Score = new decimal?(decimal.Parse(row["TQ_Score"].ToString()));
                }
                if ((row["ScoreAvg"] != null) && (row["ScoreAvg"].ToString() != ""))
                {
                    rhw_tq.ScoreAvg = new decimal?(decimal.Parse(row["ScoreAvg"].ToString()));
                }
                if ((row["ScoreAvgRate"] != null) && (row["ScoreAvgRate"].ToString() != ""))
                {
                    rhw_tq.ScoreAvgRate = new decimal?(decimal.Parse(row["ScoreAvgRate"].ToString()));
                }
                if ((row["StandardDeviation"] != null) && (row["StandardDeviation"].ToString() != ""))
                {
                    rhw_tq.StandardDeviation = new decimal?(decimal.Parse(row["StandardDeviation"].ToString()));
                }
                if ((row["Discrimination"] != null) && (row["Discrimination"].ToString() != ""))
                {
                    rhw_tq.Discrimination = new decimal?(decimal.Parse(row["Discrimination"].ToString()));
                }
                if ((row["ErrorRate"] != null) && (row["ErrorRate"].ToString() != ""))
                {
                    rhw_tq.ErrorRate = new decimal?(decimal.Parse(row["ErrorRate"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    rhw_tq.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return rhw_tq;
        }

        public bool Delete(string StatsTeacherHW_TQID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsTeacherHW_TQ ");
            builder.Append(" where StatsTeacherHW_TQID=@StatsTeacherHW_TQID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherHW_TQID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherHW_TQID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsTeacherHW_TQIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsTeacherHW_TQ ");
            builder.Append(" where StatsTeacherHW_TQID in (" + StatsTeacherHW_TQIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsTeacherHW_TQID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsTeacherHW_TQ");
            builder.Append(" where StatsTeacherHW_TQID=@StatsTeacherHW_TQID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherHW_TQID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherHW_TQID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsTeacherHW_TQID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,TestQuestions_ID,TestQuestions_Score_ID,TestQuestions_Num,TestQuestions_OrderNum,topicNumber,TestQuestions_Type,TargetText,ContentText,complexityText,TQ_Score,ScoreAvg,ScoreAvgRate,StandardDeviation,Discrimination,ErrorRate,CreateTime ");
            builder.Append(" FROM StatsTeacherHW_TQ ");
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
            builder.Append(" StatsTeacherHW_TQID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,TestQuestions_ID,TestQuestions_Score_ID,TestQuestions_Num,TestQuestions_OrderNum,topicNumber,TestQuestions_Type,TargetText,ContentText,complexityText,TQ_Score,ScoreAvg,ScoreAvgRate,StandardDeviation,Discrimination,ErrorRate,CreateTime ");
            builder.Append(" FROM StatsTeacherHW_TQ ");
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
                builder.Append("order by T.StatsTeacherHW_TQID desc");
            }
            builder.Append(")AS Row, T.*  from StatsTeacherHW_TQ T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsTeacherHW_TQ GetModel(string StatsTeacherHW_TQID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsTeacherHW_TQID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,SubjectID,SubjectName,TeacherID,TeacherName,TestQuestions_ID,TestQuestions_Score_ID,TestQuestions_Num,TestQuestions_OrderNum,topicNumber,TestQuestions_Type,TargetText,ContentText,complexityText,TQ_Score,ScoreAvg,ScoreAvgRate,StandardDeviation,Discrimination,ErrorRate,CreateTime from StatsTeacherHW_TQ ");
            builder.Append(" where StatsTeacherHW_TQID=@StatsTeacherHW_TQID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsTeacherHW_TQID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsTeacherHW_TQID;
            new Model_StatsTeacherHW_TQ();
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
            builder.Append("select count(1) FROM StatsTeacherHW_TQ ");
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

        public bool Update(Model_StatsTeacherHW_TQ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsTeacherHW_TQ set ");
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
            builder.Append(" where StatsTeacherHW_TQID=@StatsTeacherHW_TQID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@TestQuestions_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_OrderNum", SqlDbType.Int, 4), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@TestQuestions_Type", SqlDbType.VarChar, 50), 
                new SqlParameter("@TargetText", SqlDbType.VarChar, 200), new SqlParameter("@ContentText", SqlDbType.NVarChar, 200), new SqlParameter("@complexityText", SqlDbType.VarChar, 200), new SqlParameter("@TQ_Score", SqlDbType.Decimal, 5), new SqlParameter("@ScoreAvg", SqlDbType.Decimal, 5), new SqlParameter("@ScoreAvgRate", SqlDbType.Decimal, 5), new SqlParameter("@StandardDeviation", SqlDbType.Decimal, 5), new SqlParameter("@Discrimination", SqlDbType.Decimal, 5), new SqlParameter("@ErrorRate", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsTeacherHW_TQID", SqlDbType.Char, 0x24)
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
            cmdParms[10].Value = model.TestQuestions_ID;
            cmdParms[11].Value = model.TestQuestions_Score_ID;
            cmdParms[12].Value = model.TestQuestions_Num;
            cmdParms[13].Value = model.TestQuestions_OrderNum;
            cmdParms[14].Value = model.topicNumber;
            cmdParms[15].Value = model.TestQuestions_Type;
            cmdParms[0x10].Value = model.TargetText;
            cmdParms[0x11].Value = model.ContentText;
            cmdParms[0x12].Value = model.complexityText;
            cmdParms[0x13].Value = model.TQ_Score;
            cmdParms[20].Value = model.ScoreAvg;
            cmdParms[0x15].Value = model.ScoreAvgRate;
            cmdParms[0x16].Value = model.StandardDeviation;
            cmdParms[0x17].Value = model.Discrimination;
            cmdParms[0x18].Value = model.ErrorRate;
            cmdParms[0x19].Value = model.CreateTime;
            cmdParms[0x1a].Value = model.StatsTeacherHW_TQID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

