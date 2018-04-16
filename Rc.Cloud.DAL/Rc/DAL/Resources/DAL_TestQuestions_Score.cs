namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_TestQuestions_Score
    {
        public bool Add(Model_TestQuestions_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TestQuestions_Score(");
            builder.Append("TestQuestions_Score_ID,ResourceToResourceFolder_Id,TestQuestions_Id,TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,AnalyzeHyperlinkData,AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,TrainHyperlinkData,TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,CreateTime,AreaHyperlink,AreaText,kaofaText,testIndex)");
            builder.Append(" values (");
            builder.Append("@TestQuestions_Score_ID,@ResourceToResourceFolder_Id,@TestQuestions_Id,@TestQuestions_Num,@TestQuestions_OrderNum,@TestQuestions_Score,@AnalyzeHyperlink,@AnalyzeHyperlinkData,@AnalyzeHyperlinkHtml,@AnalyzeText,@ComplexityHyperlink,@ComplexityText,@ContentHyperlink,@ContentText,@DocBase64,@DocHtml,@ScoreHyperlink,@ScoreText,@TargetHyperlink,@TrainHyperlinkData,@TrainHyperlinkHtml,@TargetText,@TestCorrect,@TestType,@TrainHyperlink,@TrainText,@TypeHyperlink,@TypeText,@CreateTime,@AreaHyperlink,@AreaText,@kaofaText,@testIndex)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_OrderNum", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Score", SqlDbType.Decimal, 5), new SqlParameter("@AnalyzeHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeHyperlinkData", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeHyperlinkHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ComplexityHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ComplexityText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ContentHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ContentText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@DocBase64", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@DocHtml", SqlDbType.NVarChar, 0x3e8), 
                new SqlParameter("@ScoreHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ScoreText", SqlDbType.NVarChar, 10), new SqlParameter("@TargetHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainHyperlinkData", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainHyperlinkHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TargetText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestCorrect", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestType", SqlDbType.VarChar, 50), new SqlParameter("@TrainHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TypeHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TypeText", SqlDbType.NVarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@AreaHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AreaText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@kaofaText", SqlDbType.NVarChar, 0x3e8), 
                new SqlParameter("@testIndex", SqlDbType.VarChar, 50)
             };
            cmdParms[0].Value = model.TestQuestions_Score_ID;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.TestQuestions_Id;
            cmdParms[3].Value = model.TestQuestions_Num;
            cmdParms[4].Value = model.TestQuestions_OrderNum;
            cmdParms[5].Value = model.TestQuestions_Score;
            cmdParms[6].Value = model.AnalyzeHyperlink;
            cmdParms[7].Value = model.AnalyzeHyperlinkData;
            cmdParms[8].Value = model.AnalyzeHyperlinkHtml;
            cmdParms[9].Value = model.AnalyzeText;
            cmdParms[10].Value = model.ComplexityHyperlink;
            cmdParms[11].Value = model.ComplexityText;
            cmdParms[12].Value = model.ContentHyperlink;
            cmdParms[13].Value = model.ContentText;
            cmdParms[14].Value = model.DocBase64;
            cmdParms[15].Value = model.DocHtml;
            cmdParms[0x10].Value = model.ScoreHyperlink;
            cmdParms[0x11].Value = model.ScoreText;
            cmdParms[0x12].Value = model.TargetHyperlink;
            cmdParms[0x13].Value = model.TrainHyperlinkData;
            cmdParms[20].Value = model.TrainHyperlinkHtml;
            cmdParms[0x15].Value = model.TargetText;
            cmdParms[0x16].Value = model.TestCorrect;
            cmdParms[0x17].Value = model.TestType;
            cmdParms[0x18].Value = model.TrainHyperlink;
            cmdParms[0x19].Value = model.TrainText;
            cmdParms[0x1a].Value = model.TypeHyperlink;
            cmdParms[0x1b].Value = model.TypeText;
            cmdParms[0x1c].Value = model.CreateTime;
            cmdParms[0x1d].Value = model.AreaHyperlink;
            cmdParms[30].Value = model.AreaText;
            cmdParms[0x1f].Value = model.kaofaText;
            cmdParms[0x20].Value = model.testIndex;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_TestQuestions_Score DataRowToModel(DataRow row)
        {
            Model_TestQuestions_Score score = new Model_TestQuestions_Score();
            if (row != null)
            {
                if (row["TestQuestions_Score_ID"] != null)
                {
                    score.TestQuestions_Score_ID = row["TestQuestions_Score_ID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    score.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["TestQuestions_Id"] != null)
                {
                    score.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if ((row["TestQuestions_Num"] != null) && (row["TestQuestions_Num"].ToString() != ""))
                {
                    score.TestQuestions_Num = new int?(int.Parse(row["TestQuestions_Num"].ToString()));
                }
                if ((row["TestQuestions_OrderNum"] != null) && (row["TestQuestions_OrderNum"].ToString() != ""))
                {
                    score.TestQuestions_OrderNum = new int?(int.Parse(row["TestQuestions_OrderNum"].ToString()));
                }
                if ((row["TestQuestions_Score"] != null) && (row["TestQuestions_Score"].ToString() != ""))
                {
                    score.TestQuestions_Score = new decimal?(decimal.Parse(row["TestQuestions_Score"].ToString()));
                }
                if (row["AnalyzeHyperlink"] != null)
                {
                    score.AnalyzeHyperlink = row["AnalyzeHyperlink"].ToString();
                }
                if (row["AnalyzeHyperlinkData"] != null)
                {
                    score.AnalyzeHyperlinkData = row["AnalyzeHyperlinkData"].ToString();
                }
                if (row["AnalyzeHyperlinkHtml"] != null)
                {
                    score.AnalyzeHyperlinkHtml = row["AnalyzeHyperlinkHtml"].ToString();
                }
                if (row["AnalyzeText"] != null)
                {
                    score.AnalyzeText = row["AnalyzeText"].ToString();
                }
                if (row["ComplexityHyperlink"] != null)
                {
                    score.ComplexityHyperlink = row["ComplexityHyperlink"].ToString();
                }
                if (row["ComplexityText"] != null)
                {
                    score.ComplexityText = row["ComplexityText"].ToString();
                }
                if (row["ContentHyperlink"] != null)
                {
                    score.ContentHyperlink = row["ContentHyperlink"].ToString();
                }
                if (row["ContentText"] != null)
                {
                    score.ContentText = row["ContentText"].ToString();
                }
                if (row["DocBase64"] != null)
                {
                    score.DocBase64 = row["DocBase64"].ToString();
                }
                if (row["DocHtml"] != null)
                {
                    score.DocHtml = row["DocHtml"].ToString();
                }
                if (row["ScoreHyperlink"] != null)
                {
                    score.ScoreHyperlink = row["ScoreHyperlink"].ToString();
                }
                if (row["ScoreText"] != null)
                {
                    score.ScoreText = row["ScoreText"].ToString();
                }
                if (row["TargetHyperlink"] != null)
                {
                    score.TargetHyperlink = row["TargetHyperlink"].ToString();
                }
                if (row["TrainHyperlinkData"] != null)
                {
                    score.TrainHyperlinkData = row["TrainHyperlinkData"].ToString();
                }
                if (row["TrainHyperlinkHtml"] != null)
                {
                    score.TrainHyperlinkHtml = row["TrainHyperlinkHtml"].ToString();
                }
                if (row["TargetText"] != null)
                {
                    score.TargetText = row["TargetText"].ToString();
                }
                if (row["TestCorrect"] != null)
                {
                    score.TestCorrect = row["TestCorrect"].ToString();
                }
                if (row["TestType"] != null)
                {
                    score.TestType = row["TestType"].ToString();
                }
                if (row["TrainHyperlink"] != null)
                {
                    score.TrainHyperlink = row["TrainHyperlink"].ToString();
                }
                if (row["TrainText"] != null)
                {
                    score.TrainText = row["TrainText"].ToString();
                }
                if (row["TypeHyperlink"] != null)
                {
                    score.TypeHyperlink = row["TypeHyperlink"].ToString();
                }
                if (row["TypeText"] != null)
                {
                    score.TypeText = row["TypeText"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    score.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["AreaHyperlink"] != null)
                {
                    score.AreaHyperlink = row["AreaHyperlink"].ToString();
                }
                if (row["AreaText"] != null)
                {
                    score.AreaText = row["AreaText"].ToString();
                }
                if (row["kaofaText"] != null)
                {
                    score.kaofaText = row["kaofaText"].ToString();
                }
                if (row["testIndex"] != null)
                {
                    score.testIndex = row["testIndex"].ToString();
                }
            }
            return score;
        }

        public bool Delete(string TestQuestions_Score_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestQuestions_Score ");
            builder.Append(" where TestQuestions_Score_ID=@TestQuestions_Score_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestQuestions_Score_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TestQuestions_Score_IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestQuestions_Score ");
            builder.Append(" where TestQuestions_Score_ID in (" + TestQuestions_Score_IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TestQuestions_Score_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from TestQuestions_Score");
            builder.Append(" where TestQuestions_Score_ID=@TestQuestions_Score_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestQuestions_Score_ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TestQuestions_Score_ID,ResourceToResourceFolder_Id,TestQuestions_Id,TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,AnalyzeHyperlinkData,AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,TrainHyperlinkData,TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,CreateTime,AreaHyperlink,AreaText,kaofaText,testIndex ");
            builder.Append(" FROM TestQuestions_Score ");
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
            builder.Append(" TestQuestions_Score_ID,ResourceToResourceFolder_Id,TestQuestions_Id,TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,AnalyzeHyperlinkData,AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,TrainHyperlinkData,TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,CreateTime,AreaHyperlink,AreaText,kaofaText,testIndex ");
            builder.Append(" FROM TestQuestions_Score ");
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
                builder.Append("order by T.TestQuestions_Score_ID desc");
            }
            builder.Append(")AS Row, T.*  from TestQuestions_Score T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_TestQuestions_Score GetModel(string TestQuestions_Score_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TestQuestions_Score_ID,ResourceToResourceFolder_Id,TestQuestions_Id,TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,AnalyzeHyperlinkData,AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,TrainHyperlinkData,TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,CreateTime,AreaHyperlink,AreaText,kaofaText,testIndex from TestQuestions_Score ");
            builder.Append(" where TestQuestions_Score_ID=@TestQuestions_Score_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestQuestions_Score_ID;
            new Model_TestQuestions_Score();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_TestQuestions_Score GetModelByResourceToResourceFolder_IdNum(string ResourceToResourceFolder_Id, int TestQuestions_Num)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from TestQuestions_Score ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id and TestQuestions_Num=@TestQuestions_Num ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            cmdParms[1].Value = TestQuestions_Num;
            new Model_TestQuestions_Score();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_TestQuestions_Score GetModelByResourceToResourceFolder_IdTestID(string ResourceToResourceFolder_Id, string TestQuestions_ID, int TestQuestions_OrderNum)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from TestQuestions_Score ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id and TestQuestions_ID=@TestQuestions_ID and TestQuestions_OrderNum=@TestQuestions_OrderNum ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_OrderNum", SqlDbType.Int) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            cmdParms[1].Value = TestQuestions_ID;
            cmdParms[2].Value = TestQuestions_OrderNum;
            new Model_TestQuestions_Score();
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
            builder.Append("select count(1) FROM TestQuestions_Score ");
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

        public decimal GetSutdentFillScore(string ResourceToResourceFolder_Id, string TestQuestions_Id, int TestQuestions_OrderNum)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TestQuestions_Score from TestQuestions_Score ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id and TestQuestions_Id=@TestQuestions_Id  and TestQuestions_OrderNum=@TestQuestions_OrderNum ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_OrderNum", SqlDbType.Int, 4) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            cmdParms[1].Value = TestQuestions_Id;
            cmdParms[2].Value = TestQuestions_OrderNum;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                return decimal.Parse(single.ToString());
            }
            return 0M;
        }

        public string GetTestAnalyzeData(string ResourceToResourceFolder_Id, string TestQuestions_Id)
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AnalyzeHyperlinkData from TestQuestions_Score ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id and TestQuestions_Id=@TestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            cmdParms[1].Value = TestQuestions_Id;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                str = single.ToString();
            }
            return str;
        }

        public string GetTestAnalyzeHtml(string ResourceToResourceFolder_Id, string TestQuestions_Id)
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AnalyzeHyperlinkHtml from TestQuestions_Score ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id and TestQuestions_Id=@TestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            cmdParms[1].Value = TestQuestions_Id;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                str = single.ToString();
            }
            return str;
        }

        public string GetTestTrainData(string ResourceToResourceFolder_Id, string TestQuestions_Id)
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TrainHyperlinkData from TestQuestions_Score ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id and TestQuestions_Id=@TestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            cmdParms[1].Value = TestQuestions_Id;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                str = single.ToString();
            }
            return str;
        }

        public string GetTestTrainHtml(string ResourceToResourceFolder_Id, string TestQuestions_Id)
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TrainHyperlinkHtml from TestQuestions_Score ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id and TestQuestions_Id=@TestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            cmdParms[1].Value = TestQuestions_Id;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                str = single.ToString();
            }
            return str;
        }

        public bool Update(Model_TestQuestions_Score model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TestQuestions_Score set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("TestQuestions_Num=@TestQuestions_Num,");
            builder.Append("TestQuestions_OrderNum=@TestQuestions_OrderNum,");
            builder.Append("TestQuestions_Score=@TestQuestions_Score,");
            builder.Append("AnalyzeHyperlink=@AnalyzeHyperlink,");
            builder.Append("AnalyzeHyperlinkData=@AnalyzeHyperlinkData,");
            builder.Append("AnalyzeHyperlinkHtml=@AnalyzeHyperlinkHtml,");
            builder.Append("AnalyzeText=@AnalyzeText,");
            builder.Append("ComplexityHyperlink=@ComplexityHyperlink,");
            builder.Append("ComplexityText=@ComplexityText,");
            builder.Append("ContentHyperlink=@ContentHyperlink,");
            builder.Append("ContentText=@ContentText,");
            builder.Append("DocBase64=@DocBase64,");
            builder.Append("DocHtml=@DocHtml,");
            builder.Append("ScoreHyperlink=@ScoreHyperlink,");
            builder.Append("ScoreText=@ScoreText,");
            builder.Append("TargetHyperlink=@TargetHyperlink,");
            builder.Append("TrainHyperlinkData=@TrainHyperlinkData,");
            builder.Append("TrainHyperlinkHtml=@TrainHyperlinkHtml,");
            builder.Append("TargetText=@TargetText,");
            builder.Append("TestCorrect=@TestCorrect,");
            builder.Append("TestType=@TestType,");
            builder.Append("TrainHyperlink=@TrainHyperlink,");
            builder.Append("TrainText=@TrainText,");
            builder.Append("TypeHyperlink=@TypeHyperlink,");
            builder.Append("TypeText=@TypeText,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("AreaHyperlink=@AreaHyperlink,");
            builder.Append("AreaText=@AreaText,");
            builder.Append("kaofaText=@kaofaText,");
            builder.Append("testIndex=@testIndex");
            builder.Append(" where TestQuestions_Score_ID=@TestQuestions_Score_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_OrderNum", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Score", SqlDbType.Decimal, 5), new SqlParameter("@AnalyzeHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeHyperlinkData", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeHyperlinkHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ComplexityHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ComplexityText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ContentHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ContentText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@DocBase64", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@DocHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ScoreHyperlink", SqlDbType.NVarChar, 0x3e8), 
                new SqlParameter("@ScoreText", SqlDbType.NVarChar, 10), new SqlParameter("@TargetHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainHyperlinkData", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainHyperlinkHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TargetText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestCorrect", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestType", SqlDbType.VarChar, 50), new SqlParameter("@TrainHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TypeHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TypeText", SqlDbType.NVarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@AreaHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AreaText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@kaofaText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@testIndex", SqlDbType.VarChar, 50), 
                new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.TestQuestions_Id;
            cmdParms[2].Value = model.TestQuestions_Num;
            cmdParms[3].Value = model.TestQuestions_OrderNum;
            cmdParms[4].Value = model.TestQuestions_Score;
            cmdParms[5].Value = model.AnalyzeHyperlink;
            cmdParms[6].Value = model.AnalyzeHyperlinkData;
            cmdParms[7].Value = model.AnalyzeHyperlinkHtml;
            cmdParms[8].Value = model.AnalyzeText;
            cmdParms[9].Value = model.ComplexityHyperlink;
            cmdParms[10].Value = model.ComplexityText;
            cmdParms[11].Value = model.ContentHyperlink;
            cmdParms[12].Value = model.ContentText;
            cmdParms[13].Value = model.DocBase64;
            cmdParms[14].Value = model.DocHtml;
            cmdParms[15].Value = model.ScoreHyperlink;
            cmdParms[0x10].Value = model.ScoreText;
            cmdParms[0x11].Value = model.TargetHyperlink;
            cmdParms[0x12].Value = model.TrainHyperlinkData;
            cmdParms[0x13].Value = model.TrainHyperlinkHtml;
            cmdParms[20].Value = model.TargetText;
            cmdParms[0x15].Value = model.TestCorrect;
            cmdParms[0x16].Value = model.TestType;
            cmdParms[0x17].Value = model.TrainHyperlink;
            cmdParms[0x18].Value = model.TrainText;
            cmdParms[0x19].Value = model.TypeHyperlink;
            cmdParms[0x1a].Value = model.TypeText;
            cmdParms[0x1b].Value = model.CreateTime;
            cmdParms[0x1c].Value = model.AreaHyperlink;
            cmdParms[0x1d].Value = model.AreaText;
            cmdParms[30].Value = model.kaofaText;
            cmdParms[0x1f].Value = model.testIndex;
            cmdParms[0x20].Value = model.TestQuestions_Score_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

