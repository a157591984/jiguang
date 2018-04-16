namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_TestQuestions
    {
        public bool Add(Model_TestQuestions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TestQuestions(");
            builder.Append("TestQuestions_Id,ResourceToResourceFolder_Id,TestQuestions_Num,TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,CreateTime,topicNumber,Parent_Id,type)");
            builder.Append(" values (");
            builder.Append("@TestQuestions_Id,@ResourceToResourceFolder_Id,@TestQuestions_Num,@TestQuestions_Type,@TestQuestions_SumScore,@TestQuestions_Content,@TestQuestions_Answer,@CreateTime,@topicNumber,@Parent_Id,@type)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Type", SqlDbType.VarChar, 50), new SqlParameter("@TestQuestions_SumScore", SqlDbType.Decimal, 5), new SqlParameter("@TestQuestions_Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestQuestions_Answer", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@type", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = model.TestQuestions_Id;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.TestQuestions_Num;
            cmdParms[3].Value = model.TestQuestions_Type;
            cmdParms[4].Value = model.TestQuestions_SumScore;
            cmdParms[5].Value = model.TestQuestions_Content;
            cmdParms[6].Value = model.TestQuestions_Answer;
            cmdParms[7].Value = model.CreateTime;
            cmdParms[8].Value = model.topicNumber;
            cmdParms[9].Value = model.Parent_Id;
            cmdParms[10].Value = model.type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_TestQuestions DataRowToModel(DataRow row)
        {
            Model_TestQuestions questions = new Model_TestQuestions();
            if (row != null)
            {
                if (row["TestQuestions_Id"] != null)
                {
                    questions.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    questions.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if ((row["TestQuestions_Num"] != null) && (row["TestQuestions_Num"].ToString() != ""))
                {
                    questions.TestQuestions_Num = new int?(int.Parse(row["TestQuestions_Num"].ToString()));
                }
                if (row["TestQuestions_Type"] != null)
                {
                    questions.TestQuestions_Type = row["TestQuestions_Type"].ToString();
                }
                if ((row["TestQuestions_SumScore"] != null) && (row["TestQuestions_SumScore"].ToString() != ""))
                {
                    questions.TestQuestions_SumScore = new decimal?(decimal.Parse(row["TestQuestions_SumScore"].ToString()));
                }
                if (row["TestQuestions_Content"] != null)
                {
                    questions.TestQuestions_Content = row["TestQuestions_Content"].ToString();
                }
                if (row["TestQuestions_Answer"] != null)
                {
                    questions.TestQuestions_Answer = row["TestQuestions_Answer"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    questions.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["topicNumber"] != null)
                {
                    questions.topicNumber = row["topicNumber"].ToString();
                }
                if (row["Parent_Id"] != null)
                {
                    questions.Parent_Id = row["Parent_Id"].ToString();
                }
                if (row["type"] != null)
                {
                    questions.type = row["type"].ToString();
                }
            }
            return questions;
        }

        public bool Delete(string TestQuestions_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestQuestions ");
            builder.Append(" where TestQuestions_Id=@TestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestQuestions_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TestQuestions_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestQuestions ");
            builder.Append(" where TestQuestions_Id in (" + TestQuestions_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TestQuestions_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from TestQuestions");
            builder.Append(" where TestQuestions_Id=@TestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestQuestions_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TestQuestions_Id,ResourceToResourceFolder_Id,TestQuestions_Num,TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,CreateTime,topicNumber,Parent_Id,type ");
            builder.Append(" FROM TestQuestions ");
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
            builder.Append(" TestQuestions_Id,ResourceToResourceFolder_Id,TestQuestions_Num,TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,CreateTime,topicNumber,Parent_Id,type ");
            builder.Append(" FROM TestQuestions ");
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
                builder.Append("order by T.TestQuestions_Id desc");
            }
            builder.Append(")AS Row, T.*  from TestQuestions T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_TestQuestions GetModel(string TestQuestions_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TestQuestions_Id,ResourceToResourceFolder_Id,TestQuestions_Num,TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,CreateTime,topicNumber,Parent_Id,type from TestQuestions ");
            builder.Append(" where TestQuestions_Id=@TestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestQuestions_Id;
            new Model_TestQuestions();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_TestQuestions GetModelByResourceToResourceFolder_IdNum(string ResourceToResourceFolder_Id, int TestQuestions_Num)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from TestQuestions ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id and TestQuestions_Num=@TestQuestions_Num ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            cmdParms[1].Value = TestQuestions_Num;
            new Model_TestQuestions();
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
            builder.Append("select count(1) FROM TestQuestions ");
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

        public bool Update(Model_TestQuestions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TestQuestions set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("TestQuestions_Num=@TestQuestions_Num,");
            builder.Append("TestQuestions_Type=@TestQuestions_Type,");
            builder.Append("TestQuestions_SumScore=@TestQuestions_SumScore,");
            builder.Append("TestQuestions_Content=@TestQuestions_Content,");
            builder.Append("TestQuestions_Answer=@TestQuestions_Answer,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("topicNumber=@topicNumber,");
            builder.Append("Parent_Id=@Parent_Id,");
            builder.Append("type=@type");
            builder.Append(" where TestQuestions_Id=@TestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Type", SqlDbType.VarChar, 50), new SqlParameter("@TestQuestions_SumScore", SqlDbType.Decimal, 5), new SqlParameter("@TestQuestions_Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestQuestions_Answer", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@type", SqlDbType.VarChar, 50), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.TestQuestions_Num;
            cmdParms[2].Value = model.TestQuestions_Type;
            cmdParms[3].Value = model.TestQuestions_SumScore;
            cmdParms[4].Value = model.TestQuestions_Content;
            cmdParms[5].Value = model.TestQuestions_Answer;
            cmdParms[6].Value = model.CreateTime;
            cmdParms[7].Value = model.topicNumber;
            cmdParms[8].Value = model.Parent_Id;
            cmdParms[9].Value = model.type;
            cmdParms[10].Value = model.TestQuestions_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

