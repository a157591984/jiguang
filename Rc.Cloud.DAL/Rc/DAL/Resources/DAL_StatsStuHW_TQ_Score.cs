namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsStuHW_TQ_Score
    {
        public bool Add(Model_StatsStuHW_TQ_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsStuHW_TQ_Score(");
            builder.Append("StatsStuHW_TQ_Score_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,TestQuestions_Id,TestQuestions_Score_Id,topicNumber,TestQuestions_Num,testIndex,TestType,ComplexityText,S_KnowledgePoint_Id,KPNameBasic,S_TestingPoint_Id,TPNameBasic,KPImportant,TQScore,Score,TQScoreAvg,TQScoreAvgRate,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsStuHW_TQ_Score_Id,@HomeWork_Id,@HomeWorkCreateTime,@Student_HomeWork_Id,@Student_Id,@TestQuestions_Id,@TestQuestions_Score_Id,@topicNumber,@TestQuestions_Num,@testIndex,@TestType,@ComplexityText,@S_KnowledgePoint_Id,@KPNameBasic,@S_TestingPoint_Id,@TPNameBasic,@KPImportant,@TQScore,@Score,@TQScoreAvg,@TQScoreAvgRate,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsStuHW_TQ_Score_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Score_Id", SqlDbType.Char, 0x24), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@testIndex", SqlDbType.Int, 4), new SqlParameter("@TestType", SqlDbType.VarChar, 500), new SqlParameter("@ComplexityText", SqlDbType.VarChar, 500), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.VarChar, 500), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 0x7d0), new SqlParameter("@S_TestingPoint_Id", SqlDbType.VarChar, 500), new SqlParameter("@TPNameBasic", SqlDbType.VarChar, 0x7d0), 
                new SqlParameter("@KPImportant", SqlDbType.VarChar, 500), new SqlParameter("@TQScore", SqlDbType.Decimal, 9), new SqlParameter("@Score", SqlDbType.Decimal, 9), new SqlParameter("@TQScoreAvg", SqlDbType.Decimal, 9), new SqlParameter("@TQScoreAvgRate", SqlDbType.Decimal, 9), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsStuHW_TQ_Score_Id;
            cmdParms[1].Value = model.HomeWork_Id;
            cmdParms[2].Value = model.HomeWorkCreateTime;
            cmdParms[3].Value = model.Student_HomeWork_Id;
            cmdParms[4].Value = model.Student_Id;
            cmdParms[5].Value = model.TestQuestions_Id;
            cmdParms[6].Value = model.TestQuestions_Score_Id;
            cmdParms[7].Value = model.topicNumber;
            cmdParms[8].Value = model.TestQuestions_Num;
            cmdParms[9].Value = model.testIndex;
            cmdParms[10].Value = model.TestType;
            cmdParms[11].Value = model.ComplexityText;
            cmdParms[12].Value = model.S_KnowledgePoint_Id;
            cmdParms[13].Value = model.KPNameBasic;
            cmdParms[14].Value = model.S_TestingPoint_Id;
            cmdParms[15].Value = model.TPNameBasic;
            cmdParms[0x10].Value = model.KPImportant;
            cmdParms[0x11].Value = model.TQScore;
            cmdParms[0x12].Value = model.Score;
            cmdParms[0x13].Value = model.TQScoreAvg;
            cmdParms[20].Value = model.TQScoreAvgRate;
            cmdParms[0x15].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsStuHW_TQ_Score DataRowToModel(DataRow row)
        {
            Model_StatsStuHW_TQ_Score score = new Model_StatsStuHW_TQ_Score();
            if (row != null)
            {
                if (row["StatsStuHW_TQ_Score_Id"] != null)
                {
                    score.StatsStuHW_TQ_Score_Id = row["StatsStuHW_TQ_Score_Id"].ToString();
                }
                if (row["HomeWork_Id"] != null)
                {
                    score.HomeWork_Id = row["HomeWork_Id"].ToString();
                }
                if ((row["HomeWorkCreateTime"] != null) && (row["HomeWorkCreateTime"].ToString() != ""))
                {
                    score.HomeWorkCreateTime = new DateTime?(DateTime.Parse(row["HomeWorkCreateTime"].ToString()));
                }
                if (row["Student_HomeWork_Id"] != null)
                {
                    score.Student_HomeWork_Id = row["Student_HomeWork_Id"].ToString();
                }
                if (row["Student_Id"] != null)
                {
                    score.Student_Id = row["Student_Id"].ToString();
                }
                if (row["TestQuestions_Id"] != null)
                {
                    score.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if (row["TestQuestions_Score_Id"] != null)
                {
                    score.TestQuestions_Score_Id = row["TestQuestions_Score_Id"].ToString();
                }
                if (row["topicNumber"] != null)
                {
                    score.topicNumber = row["topicNumber"].ToString();
                }
                if ((row["TestQuestions_Num"] != null) && (row["TestQuestions_Num"].ToString() != ""))
                {
                    score.TestQuestions_Num = new int?(int.Parse(row["TestQuestions_Num"].ToString()));
                }
                if ((row["testIndex"] != null) && (row["testIndex"].ToString() != ""))
                {
                    score.testIndex = new int?(int.Parse(row["testIndex"].ToString()));
                }
                if (row["TestType"] != null)
                {
                    score.TestType = row["TestType"].ToString();
                }
                if (row["ComplexityText"] != null)
                {
                    score.ComplexityText = row["ComplexityText"].ToString();
                }
                if (row["S_KnowledgePoint_Id"] != null)
                {
                    score.S_KnowledgePoint_Id = row["S_KnowledgePoint_Id"].ToString();
                }
                if (row["KPNameBasic"] != null)
                {
                    score.KPNameBasic = row["KPNameBasic"].ToString();
                }
                if (row["S_TestingPoint_Id"] != null)
                {
                    score.S_TestingPoint_Id = row["S_TestingPoint_Id"].ToString();
                }
                if (row["TPNameBasic"] != null)
                {
                    score.TPNameBasic = row["TPNameBasic"].ToString();
                }
                if (row["KPImportant"] != null)
                {
                    score.KPImportant = row["KPImportant"].ToString();
                }
                if ((row["TQScore"] != null) && (row["TQScore"].ToString() != ""))
                {
                    score.TQScore = new decimal?(decimal.Parse(row["TQScore"].ToString()));
                }
                if ((row["Score"] != null) && (row["Score"].ToString() != ""))
                {
                    score.Score = new decimal?(decimal.Parse(row["Score"].ToString()));
                }
                if ((row["TQScoreAvg"] != null) && (row["TQScoreAvg"].ToString() != ""))
                {
                    score.TQScoreAvg = new decimal?(decimal.Parse(row["TQScoreAvg"].ToString()));
                }
                if ((row["TQScoreAvgRate"] != null) && (row["TQScoreAvgRate"].ToString() != ""))
                {
                    score.TQScoreAvgRate = new decimal?(decimal.Parse(row["TQScoreAvgRate"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    score.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return score;
        }

        public bool Delete(string StatsStuHW_TQ_Score_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_TQ_Score ");
            builder.Append(" where StatsStuHW_TQ_Score_Id=@StatsStuHW_TQ_Score_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_TQ_Score_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_TQ_Score_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsStuHW_TQ_Score_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_TQ_Score ");
            builder.Append(" where StatsStuHW_TQ_Score_Id in (" + StatsStuHW_TQ_Score_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsStuHW_TQ_Score_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsStuHW_TQ_Score");
            builder.Append(" where StatsStuHW_TQ_Score_Id=@StatsStuHW_TQ_Score_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_TQ_Score_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_TQ_Score_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsStuHW_TQ_Score_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,TestQuestions_Id,TestQuestions_Score_Id,topicNumber,TestQuestions_Num,testIndex,TestType,ComplexityText,S_KnowledgePoint_Id,KPNameBasic,S_TestingPoint_Id,TPNameBasic,KPImportant,TQScore,Score,TQScoreAvg,TQScoreAvgRate,CreateTime ");
            builder.Append(" FROM StatsStuHW_TQ_Score ");
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
            builder.Append(" StatsStuHW_TQ_Score_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,TestQuestions_Id,TestQuestions_Score_Id,topicNumber,TestQuestions_Num,testIndex,TestType,ComplexityText,S_KnowledgePoint_Id,KPNameBasic,S_TestingPoint_Id,TPNameBasic,KPImportant,TQScore,Score,TQScoreAvg,TQScoreAvgRate,CreateTime ");
            builder.Append(" FROM StatsStuHW_TQ_Score ");
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
                builder.Append("order by T.StatsStuHW_TQ_Score_Id desc");
            }
            builder.Append(")AS Row, T.*  from StatsStuHW_TQ_Score T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsStuHW_TQ_Score GetModel(string StatsStuHW_TQ_Score_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsStuHW_TQ_Score_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,TestQuestions_Id,TestQuestions_Score_Id,topicNumber,TestQuestions_Num,testIndex,TestType,ComplexityText,S_KnowledgePoint_Id,KPNameBasic,S_TestingPoint_Id,TPNameBasic,KPImportant,TQScore,Score,TQScoreAvg,TQScoreAvgRate,CreateTime from StatsStuHW_TQ_Score ");
            builder.Append(" where StatsStuHW_TQ_Score_Id=@StatsStuHW_TQ_Score_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_TQ_Score_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_TQ_Score_Id;
            new Model_StatsStuHW_TQ_Score();
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
            builder.Append("select count(1) FROM StatsStuHW_TQ_Score ");
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

        public bool Update(Model_StatsStuHW_TQ_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsStuHW_TQ_Score set ");
            builder.Append("HomeWork_Id=@HomeWork_Id,");
            builder.Append("HomeWorkCreateTime=@HomeWorkCreateTime,");
            builder.Append("Student_HomeWork_Id=@Student_HomeWork_Id,");
            builder.Append("Student_Id=@Student_Id,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("TestQuestions_Score_Id=@TestQuestions_Score_Id,");
            builder.Append("topicNumber=@topicNumber,");
            builder.Append("TestQuestions_Num=@TestQuestions_Num,");
            builder.Append("testIndex=@testIndex,");
            builder.Append("TestType=@TestType,");
            builder.Append("ComplexityText=@ComplexityText,");
            builder.Append("S_KnowledgePoint_Id=@S_KnowledgePoint_Id,");
            builder.Append("KPNameBasic=@KPNameBasic,");
            builder.Append("S_TestingPoint_Id=@S_TestingPoint_Id,");
            builder.Append("TPNameBasic=@TPNameBasic,");
            builder.Append("KPImportant=@KPImportant,");
            builder.Append("TQScore=@TQScore,");
            builder.Append("Score=@Score,");
            builder.Append("TQScoreAvg=@TQScoreAvg,");
            builder.Append("TQScoreAvgRate=@TQScoreAvgRate,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsStuHW_TQ_Score_Id=@StatsStuHW_TQ_Score_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Score_Id", SqlDbType.Char, 0x24), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@testIndex", SqlDbType.Int, 4), new SqlParameter("@TestType", SqlDbType.VarChar, 500), new SqlParameter("@ComplexityText", SqlDbType.VarChar, 500), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.VarChar, 500), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 0x7d0), new SqlParameter("@S_TestingPoint_Id", SqlDbType.VarChar, 500), new SqlParameter("@TPNameBasic", SqlDbType.VarChar, 0x7d0), new SqlParameter("@KPImportant", SqlDbType.VarChar, 500), 
                new SqlParameter("@TQScore", SqlDbType.Decimal, 9), new SqlParameter("@Score", SqlDbType.Decimal, 9), new SqlParameter("@TQScoreAvg", SqlDbType.Decimal, 9), new SqlParameter("@TQScoreAvgRate", SqlDbType.Decimal, 9), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsStuHW_TQ_Score_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.HomeWork_Id;
            cmdParms[1].Value = model.HomeWorkCreateTime;
            cmdParms[2].Value = model.Student_HomeWork_Id;
            cmdParms[3].Value = model.Student_Id;
            cmdParms[4].Value = model.TestQuestions_Id;
            cmdParms[5].Value = model.TestQuestions_Score_Id;
            cmdParms[6].Value = model.topicNumber;
            cmdParms[7].Value = model.TestQuestions_Num;
            cmdParms[8].Value = model.testIndex;
            cmdParms[9].Value = model.TestType;
            cmdParms[10].Value = model.ComplexityText;
            cmdParms[11].Value = model.S_KnowledgePoint_Id;
            cmdParms[12].Value = model.KPNameBasic;
            cmdParms[13].Value = model.S_TestingPoint_Id;
            cmdParms[14].Value = model.TPNameBasic;
            cmdParms[15].Value = model.KPImportant;
            cmdParms[0x10].Value = model.TQScore;
            cmdParms[0x11].Value = model.Score;
            cmdParms[0x12].Value = model.TQScoreAvg;
            cmdParms[0x13].Value = model.TQScoreAvgRate;
            cmdParms[20].Value = model.CreateTime;
            cmdParms[0x15].Value = model.StatsStuHW_TQ_Score_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

