namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsStuHW_Wrong_TQ
    {
        public bool Add(Model_StatsStuHW_Wrong_TQ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsStuHW_Wrong_TQ(");
            builder.Append("StatsStuHW_Wrong_TQ_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,TestQuestions_Id,topicNumber,TestQuestions_Num,TestType,ComplexityText,S_KnowledgePoint_Id,KPNameBasic,S_TestingPoint_Id,TPNameBasic,KPImportant,TQScore,Score,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsStuHW_Wrong_TQ_Id,@HomeWork_Id,@HomeWorkCreateTime,@Student_HomeWork_Id,@Student_Id,@TestQuestions_Id,@topicNumber,@TestQuestions_Num,@TestType,@ComplexityText,@S_KnowledgePoint_Id,@KPNameBasic,@S_TestingPoint_Id,@TPNameBasic,@KPImportant,@TQScore,@Score,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsStuHW_Wrong_TQ_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestType", SqlDbType.VarChar, 500), new SqlParameter("@ComplexityText", SqlDbType.VarChar, 500), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.VarChar, 500), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 0x7d0), new SqlParameter("@S_TestingPoint_Id", SqlDbType.VarChar, 500), new SqlParameter("@TPNameBasic", SqlDbType.VarChar, 0x7d0), new SqlParameter("@KPImportant", SqlDbType.VarChar, 500), new SqlParameter("@TQScore", SqlDbType.Decimal, 9), 
                new SqlParameter("@Score", SqlDbType.Decimal, 9), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsStuHW_Wrong_TQ_Id;
            cmdParms[1].Value = model.HomeWork_Id;
            cmdParms[2].Value = model.HomeWorkCreateTime;
            cmdParms[3].Value = model.Student_HomeWork_Id;
            cmdParms[4].Value = model.Student_Id;
            cmdParms[5].Value = model.TestQuestions_Id;
            cmdParms[6].Value = model.topicNumber;
            cmdParms[7].Value = model.TestQuestions_Num;
            cmdParms[8].Value = model.TestType;
            cmdParms[9].Value = model.ComplexityText;
            cmdParms[10].Value = model.S_KnowledgePoint_Id;
            cmdParms[11].Value = model.KPNameBasic;
            cmdParms[12].Value = model.S_TestingPoint_Id;
            cmdParms[13].Value = model.TPNameBasic;
            cmdParms[14].Value = model.KPImportant;
            cmdParms[15].Value = model.TQScore;
            cmdParms[0x10].Value = model.Score;
            cmdParms[0x11].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsStuHW_Wrong_TQ DataRowToModel(DataRow row)
        {
            Model_StatsStuHW_Wrong_TQ g_tq = new Model_StatsStuHW_Wrong_TQ();
            if (row != null)
            {
                if (row["StatsStuHW_Wrong_TQ_Id"] != null)
                {
                    g_tq.StatsStuHW_Wrong_TQ_Id = row["StatsStuHW_Wrong_TQ_Id"].ToString();
                }
                if (row["HomeWork_Id"] != null)
                {
                    g_tq.HomeWork_Id = row["HomeWork_Id"].ToString();
                }
                if ((row["HomeWorkCreateTime"] != null) && (row["HomeWorkCreateTime"].ToString() != ""))
                {
                    g_tq.HomeWorkCreateTime = new DateTime?(DateTime.Parse(row["HomeWorkCreateTime"].ToString()));
                }
                if (row["Student_HomeWork_Id"] != null)
                {
                    g_tq.Student_HomeWork_Id = row["Student_HomeWork_Id"].ToString();
                }
                if (row["Student_Id"] != null)
                {
                    g_tq.Student_Id = row["Student_Id"].ToString();
                }
                if (row["TestQuestions_Id"] != null)
                {
                    g_tq.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if (row["topicNumber"] != null)
                {
                    g_tq.topicNumber = row["topicNumber"].ToString();
                }
                if ((row["TestQuestions_Num"] != null) && (row["TestQuestions_Num"].ToString() != ""))
                {
                    g_tq.TestQuestions_Num = new int?(int.Parse(row["TestQuestions_Num"].ToString()));
                }
                if (row["TestType"] != null)
                {
                    g_tq.TestType = row["TestType"].ToString();
                }
                if (row["ComplexityText"] != null)
                {
                    g_tq.ComplexityText = row["ComplexityText"].ToString();
                }
                if (row["S_KnowledgePoint_Id"] != null)
                {
                    g_tq.S_KnowledgePoint_Id = row["S_KnowledgePoint_Id"].ToString();
                }
                if (row["KPNameBasic"] != null)
                {
                    g_tq.KPNameBasic = row["KPNameBasic"].ToString();
                }
                if (row["S_TestingPoint_Id"] != null)
                {
                    g_tq.S_TestingPoint_Id = row["S_TestingPoint_Id"].ToString();
                }
                if (row["TPNameBasic"] != null)
                {
                    g_tq.TPNameBasic = row["TPNameBasic"].ToString();
                }
                if (row["KPImportant"] != null)
                {
                    g_tq.KPImportant = row["KPImportant"].ToString();
                }
                if ((row["TQScore"] != null) && (row["TQScore"].ToString() != ""))
                {
                    g_tq.TQScore = new decimal?(decimal.Parse(row["TQScore"].ToString()));
                }
                if ((row["Score"] != null) && (row["Score"].ToString() != ""))
                {
                    g_tq.Score = new decimal?(decimal.Parse(row["Score"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    g_tq.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return g_tq;
        }

        public bool Delete(string StatsStuHW_Wrong_TQ_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_Wrong_TQ ");
            builder.Append(" where StatsStuHW_Wrong_TQ_Id=@StatsStuHW_Wrong_TQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Wrong_TQ_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Wrong_TQ_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsStuHW_Wrong_TQ_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_Wrong_TQ ");
            builder.Append(" where StatsStuHW_Wrong_TQ_Id in (" + StatsStuHW_Wrong_TQ_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsStuHW_Wrong_TQ_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsStuHW_Wrong_TQ");
            builder.Append(" where StatsStuHW_Wrong_TQ_Id=@StatsStuHW_Wrong_TQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Wrong_TQ_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Wrong_TQ_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsStuHW_Wrong_TQ_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,TestQuestions_Id,topicNumber,TestQuestions_Num,TestType,ComplexityText,S_KnowledgePoint_Id,KPNameBasic,S_TestingPoint_Id,TPNameBasic,KPImportant,TQScore,Score,CreateTime ");
            builder.Append(" FROM StatsStuHW_Wrong_TQ ");
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
            builder.Append(" StatsStuHW_Wrong_TQ_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,TestQuestions_Id,topicNumber,TestQuestions_Num,TestType,ComplexityText,S_KnowledgePoint_Id,KPNameBasic,S_TestingPoint_Id,TPNameBasic,KPImportant,TQScore,Score,CreateTime ");
            builder.Append(" FROM StatsStuHW_Wrong_TQ ");
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
                builder.Append("order by T.StatsStuHW_Wrong_TQ_Id desc");
            }
            builder.Append(")AS Row, T.*  from StatsStuHW_Wrong_TQ T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsStuHW_Wrong_TQ GetModel(string StatsStuHW_Wrong_TQ_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsStuHW_Wrong_TQ_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,TestQuestions_Id,topicNumber,TestQuestions_Num,TestType,ComplexityText,S_KnowledgePoint_Id,KPNameBasic,S_TestingPoint_Id,TPNameBasic,KPImportant,TQScore,Score,CreateTime from StatsStuHW_Wrong_TQ ");
            builder.Append(" where StatsStuHW_Wrong_TQ_Id=@StatsStuHW_Wrong_TQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Wrong_TQ_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Wrong_TQ_Id;
            new Model_StatsStuHW_Wrong_TQ();
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
            builder.Append("select count(1) FROM StatsStuHW_Wrong_TQ ");
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

        public bool Update(Model_StatsStuHW_Wrong_TQ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsStuHW_Wrong_TQ set ");
            builder.Append("HomeWork_Id=@HomeWork_Id,");
            builder.Append("HomeWorkCreateTime=@HomeWorkCreateTime,");
            builder.Append("Student_HomeWork_Id=@Student_HomeWork_Id,");
            builder.Append("Student_Id=@Student_Id,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("topicNumber=@topicNumber,");
            builder.Append("TestQuestions_Num=@TestQuestions_Num,");
            builder.Append("TestType=@TestType,");
            builder.Append("ComplexityText=@ComplexityText,");
            builder.Append("S_KnowledgePoint_Id=@S_KnowledgePoint_Id,");
            builder.Append("KPNameBasic=@KPNameBasic,");
            builder.Append("S_TestingPoint_Id=@S_TestingPoint_Id,");
            builder.Append("TPNameBasic=@TPNameBasic,");
            builder.Append("KPImportant=@KPImportant,");
            builder.Append("TQScore=@TQScore,");
            builder.Append("Score=@Score,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsStuHW_Wrong_TQ_Id=@StatsStuHW_Wrong_TQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestType", SqlDbType.VarChar, 500), new SqlParameter("@ComplexityText", SqlDbType.VarChar, 500), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.VarChar, 500), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 0x7d0), new SqlParameter("@S_TestingPoint_Id", SqlDbType.VarChar, 500), new SqlParameter("@TPNameBasic", SqlDbType.VarChar, 0x7d0), new SqlParameter("@KPImportant", SqlDbType.VarChar, 500), new SqlParameter("@TQScore", SqlDbType.Decimal, 9), new SqlParameter("@Score", SqlDbType.Decimal, 9), 
                new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsStuHW_Wrong_TQ_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.HomeWork_Id;
            cmdParms[1].Value = model.HomeWorkCreateTime;
            cmdParms[2].Value = model.Student_HomeWork_Id;
            cmdParms[3].Value = model.Student_Id;
            cmdParms[4].Value = model.TestQuestions_Id;
            cmdParms[5].Value = model.topicNumber;
            cmdParms[6].Value = model.TestQuestions_Num;
            cmdParms[7].Value = model.TestType;
            cmdParms[8].Value = model.ComplexityText;
            cmdParms[9].Value = model.S_KnowledgePoint_Id;
            cmdParms[10].Value = model.KPNameBasic;
            cmdParms[11].Value = model.S_TestingPoint_Id;
            cmdParms[12].Value = model.TPNameBasic;
            cmdParms[13].Value = model.KPImportant;
            cmdParms[14].Value = model.TQScore;
            cmdParms[15].Value = model.Score;
            cmdParms[0x10].Value = model.CreateTime;
            cmdParms[0x11].Value = model.StatsStuHW_Wrong_TQ_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

