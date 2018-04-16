namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_TestPaper_FrameToTestpaper
    {
        public bool Add(Model_TestPaper_FrameToTestpaper model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TestPaper_FrameToTestpaper(");
            builder.Append("TestPaper_FrameToTestpaper_Id,TestPaper_Frame_Id,ResourceToResourceFolder_Id,CreateUser,CreateTime,TestPaper_FrameToTestpaper_Type)");
            builder.Append(" values (");
            builder.Append("@TestPaper_FrameToTestpaper_Id,@TestPaper_Frame_Id,@ResourceToResourceFolder_Id,@CreateUser,@CreateTime,@TestPaper_FrameToTestpaper_Type)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameToTestpaper_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestPaper_FrameToTestpaper_Type", SqlDbType.Char, 1) };
            cmdParms[0].Value = model.TestPaper_FrameToTestpaper_Id;
            cmdParms[1].Value = model.TestPaper_Frame_Id;
            cmdParms[2].Value = model.ResourceToResourceFolder_Id;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.TestPaper_FrameToTestpaper_Type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int AddRelationPaper(Model_TestPaper_FrameToTestpaper model, List<Model_TestPaper_FrameDetailToTestQuestions> listModel)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into TestPaper_FrameToTestpaper(");
            builder.Append("TestPaper_FrameToTestpaper_Id,TestPaper_Frame_Id,ResourceToResourceFolder_Id,CreateUser,CreateTime,TestPaper_FrameToTestpaper_Type)");
            builder.Append(" values (");
            builder.Append("@TestPaper_FrameToTestpaper_Id,@TestPaper_Frame_Id,@ResourceToResourceFolder_Id,@CreateUser,@CreateTime,@TestPaper_FrameToTestpaper_Type)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@TestPaper_FrameToTestpaper_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestPaper_FrameToTestpaper_Type", SqlDbType.Char, 1) };
            parameterArray[0].Value = model.TestPaper_FrameToTestpaper_Id;
            parameterArray[1].Value = model.TestPaper_Frame_Id;
            parameterArray[2].Value = model.ResourceToResourceFolder_Id;
            parameterArray[3].Value = model.CreateUser;
            parameterArray[4].Value = model.CreateTime;
            parameterArray[5].Value = model.TestPaper_FrameToTestpaper_Type;
            dictionary.Add(builder.ToString(), parameterArray);
            int num = 0;
            foreach (Model_TestPaper_FrameDetailToTestQuestions questions in listModel)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into TestPaper_FrameDetailToTestQuestions(");
                builder.Append("TestPaper_FrameDetailToTestQuestions_Id,TestPaper_Frame_Id,TestPaper_FrameDetail_Id,ResourceToResourceFolder_Id,TestQuestions_Id,CreateUser,CreateTime)");
                builder.Append(" values (");
                builder.Append("@TestPaper_FrameDetailToTestQuestions_Id,@TestPaper_Frame_Id,@TestPaper_FrameDetail_Id,@ResourceToResourceFolder_Id,@TestQuestions_Id,@CreateUser,@CreateTime)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetailToTestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_FrameDetail_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
                parameterArray2[0].Value = questions.TestPaper_FrameDetailToTestQuestions_Id;
                parameterArray2[1].Value = questions.TestPaper_Frame_Id;
                parameterArray2[2].Value = questions.TestPaper_FrameDetail_Id;
                parameterArray2[3].Value = questions.ResourceToResourceFolder_Id;
                parameterArray2[4].Value = questions.TestQuestions_Id;
                parameterArray2[5].Value = questions.CreateUser;
                parameterArray2[6].Value = questions.CreateTime;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            int num2 = DbHelperSQL.ExecuteSqlTran(dictionary);
            if (num2 > 0)
            {
                return num2;
            }
            return 0;
        }

        public Model_TestPaper_FrameToTestpaper DataRowToModel(DataRow row)
        {
            Model_TestPaper_FrameToTestpaper testpaper = new Model_TestPaper_FrameToTestpaper();
            if (row != null)
            {
                if (row["TestPaper_FrameToTestpaper_Id"] != null)
                {
                    testpaper.TestPaper_FrameToTestpaper_Id = row["TestPaper_FrameToTestpaper_Id"].ToString();
                }
                if (row["TestPaper_Frame_Id"] != null)
                {
                    testpaper.TestPaper_Frame_Id = row["TestPaper_Frame_Id"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    testpaper.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    testpaper.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    testpaper.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["TestPaper_FrameToTestpaper_Type"] != null)
                {
                    testpaper.TestPaper_FrameToTestpaper_Type = row["TestPaper_FrameToTestpaper_Type"].ToString();
                }
            }
            return testpaper;
        }

        public bool Delete(string TestPaper_FrameToTestpaper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestPaper_FrameToTestpaper ");
            builder.Append(" where TestPaper_FrameToTestpaper_Id=@TestPaper_FrameToTestpaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameToTestpaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameToTestpaper_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TestPaper_FrameToTestpaper_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestPaper_FrameToTestpaper ");
            builder.Append(" where TestPaper_FrameToTestpaper_Id in (" + TestPaper_FrameToTestpaper_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TestPaper_FrameToTestpaper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from TestPaper_FrameToTestpaper");
            builder.Append(" where TestPaper_FrameToTestpaper_Id=@TestPaper_FrameToTestpaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameToTestpaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameToTestpaper_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TestPaper_FrameToTestpaper_Id,TestPaper_Frame_Id,ResourceToResourceFolder_Id,CreateUser,CreateTime,TestPaper_FrameToTestpaper_Type ");
            builder.Append(" FROM TestPaper_FrameToTestpaper ");
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
            builder.Append(" TestPaper_FrameToTestpaper_Id,TestPaper_Frame_Id,ResourceToResourceFolder_Id,CreateUser,CreateTime,TestPaper_FrameToTestpaper_Type ");
            builder.Append(" FROM TestPaper_FrameToTestpaper ");
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
                builder.Append("order by T.TestPaper_FrameToTestpaper_Id desc");
            }
            builder.Append(")AS Row, T.*  from TestPaper_FrameToTestpaper T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_TestPaper_FrameToTestpaper GetModel(string TestPaper_FrameToTestpaper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TestPaper_FrameToTestpaper_Id,TestPaper_Frame_Id,ResourceToResourceFolder_Id,CreateUser,CreateTime,TestPaper_FrameToTestpaper_Type from TestPaper_FrameToTestpaper ");
            builder.Append(" where TestPaper_FrameToTestpaper_Id=@TestPaper_FrameToTestpaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameToTestpaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameToTestpaper_Id;
            new Model_TestPaper_FrameToTestpaper();
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
            builder.Append("select count(1) FROM TestPaper_FrameToTestpaper ");
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

        public int GetRecordCount_Operate(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM TestPaper_FrameToTestpaper ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL_Operate.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Model_TestPaper_FrameToTestpaper model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TestPaper_FrameToTestpaper set ");
            builder.Append("TestPaper_Frame_Id=@TestPaper_Frame_Id,");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("TestPaper_FrameToTestpaper_Type=@TestPaper_FrameToTestpaper_Type");
            builder.Append(" where TestPaper_FrameToTestpaper_Id=@TestPaper_FrameToTestpaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestPaper_FrameToTestpaper_Type", SqlDbType.Char, 1), new SqlParameter("@TestPaper_FrameToTestpaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.TestPaper_Frame_Id;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.CreateUser;
            cmdParms[3].Value = model.CreateTime;
            cmdParms[4].Value = model.TestPaper_FrameToTestpaper_Type;
            cmdParms[5].Value = model.TestPaper_FrameToTestpaper_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

