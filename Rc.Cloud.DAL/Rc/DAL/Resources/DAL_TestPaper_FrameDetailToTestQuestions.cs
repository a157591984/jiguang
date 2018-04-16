namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_TestPaper_FrameDetailToTestQuestions
    {
        public bool Add(Model_TestPaper_FrameDetailToTestQuestions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TestPaper_FrameDetailToTestQuestions(");
            builder.Append("TestPaper_FrameDetailToTestQuestions_Id,TestPaper_Frame_Id,TestPaper_FrameDetail_Id,ResourceToResourceFolder_Id,TestQuestions_Id,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@TestPaper_FrameDetailToTestQuestions_Id,@TestPaper_Frame_Id,@TestPaper_FrameDetail_Id,@ResourceToResourceFolder_Id,@TestQuestions_Id,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetailToTestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_FrameDetail_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.TestPaper_FrameDetailToTestQuestions_Id;
            cmdParms[1].Value = model.TestPaper_Frame_Id;
            cmdParms[2].Value = model.TestPaper_FrameDetail_Id;
            cmdParms[3].Value = model.ResourceToResourceFolder_Id;
            cmdParms[4].Value = model.TestQuestions_Id;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_TestPaper_FrameDetailToTestQuestions DataRowToModel(DataRow row)
        {
            Model_TestPaper_FrameDetailToTestQuestions questions = new Model_TestPaper_FrameDetailToTestQuestions();
            if (row != null)
            {
                if (row["TestPaper_FrameDetailToTestQuestions_Id"] != null)
                {
                    questions.TestPaper_FrameDetailToTestQuestions_Id = row["TestPaper_FrameDetailToTestQuestions_Id"].ToString();
                }
                if (row["TestPaper_Frame_Id"] != null)
                {
                    questions.TestPaper_Frame_Id = row["TestPaper_Frame_Id"].ToString();
                }
                if (row["TestPaper_FrameDetail_Id"] != null)
                {
                    questions.TestPaper_FrameDetail_Id = row["TestPaper_FrameDetail_Id"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    questions.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["TestQuestions_Id"] != null)
                {
                    questions.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    questions.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    questions.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return questions;
        }

        public bool Delete(string TestPaper_FrameDetailToTestQuestions_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestPaper_FrameDetailToTestQuestions ");
            builder.Append(" where TestPaper_FrameDetailToTestQuestions_Id=@TestPaper_FrameDetailToTestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetailToTestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameDetailToTestQuestions_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TestPaper_FrameDetailToTestQuestions_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestPaper_FrameDetailToTestQuestions ");
            builder.Append(" where TestPaper_FrameDetailToTestQuestions_Id in (" + TestPaper_FrameDetailToTestQuestions_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TestPaper_FrameDetailToTestQuestions_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from TestPaper_FrameDetailToTestQuestions");
            builder.Append(" where TestPaper_FrameDetailToTestQuestions_Id=@TestPaper_FrameDetailToTestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetailToTestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameDetailToTestQuestions_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TestPaper_FrameDetailToTestQuestions_Id,TestPaper_Frame_Id,TestPaper_FrameDetail_Id,ResourceToResourceFolder_Id,TestQuestions_Id,CreateUser,CreateTime ");
            builder.Append(" FROM TestPaper_FrameDetailToTestQuestions ");
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
            builder.Append(" TestPaper_FrameDetailToTestQuestions_Id,TestPaper_Frame_Id,TestPaper_FrameDetail_Id,ResourceToResourceFolder_Id,TestQuestions_Id,CreateUser,CreateTime ");
            builder.Append(" FROM TestPaper_FrameDetailToTestQuestions ");
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
                builder.Append("order by T.TestPaper_FrameDetailToTestQuestions_Id desc");
            }
            builder.Append(")AS Row, T.*  from TestPaper_FrameDetailToTestQuestions T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_TestPaper_FrameDetailToTestQuestions GetModel(string TestPaper_FrameDetailToTestQuestions_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TestPaper_FrameDetailToTestQuestions_Id,TestPaper_Frame_Id,TestPaper_FrameDetail_Id,ResourceToResourceFolder_Id,TestQuestions_Id,CreateUser,CreateTime from TestPaper_FrameDetailToTestQuestions ");
            builder.Append(" where TestPaper_FrameDetailToTestQuestions_Id=@TestPaper_FrameDetailToTestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetailToTestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameDetailToTestQuestions_Id;
            new Model_TestPaper_FrameDetailToTestQuestions();
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
            builder.Append("select count(1) FROM TestPaper_FrameDetailToTestQuestions ");
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

        public bool Update(Model_TestPaper_FrameDetailToTestQuestions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TestPaper_FrameDetailToTestQuestions set ");
            builder.Append("TestPaper_Frame_Id=@TestPaper_Frame_Id,");
            builder.Append("TestPaper_FrameDetail_Id=@TestPaper_FrameDetail_Id,");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where TestPaper_FrameDetailToTestQuestions_Id=@TestPaper_FrameDetailToTestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_FrameDetail_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestPaper_FrameDetailToTestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.TestPaper_Frame_Id;
            cmdParms[1].Value = model.TestPaper_FrameDetail_Id;
            cmdParms[2].Value = model.ResourceToResourceFolder_Id;
            cmdParms[3].Value = model.TestQuestions_Id;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.TestPaper_FrameDetailToTestQuestions_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

