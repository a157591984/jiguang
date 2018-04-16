namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_TestQuestions_Option
    {
        public bool Add(Model_TestQuestions_Option model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TestQuestions_Option(");
            builder.Append("TestQuestions_Option_Id,TestQuestions_Id,TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,CreateTime,TestQuestions_Score_ID)");
            builder.Append(" values (");
            builder.Append("@TestQuestions_Option_Id,@TestQuestions_Id,@TestQuestions_OptionParent_OrderNum,@TestQuestions_Option_Content,@TestQuestions_Option_OrderNum,@CreateTime,@TestQuestions_Score_ID)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Option_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_OptionParent_OrderNum", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Option_Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestQuestions_Option_OrderNum", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.TestQuestions_Option_Id;
            cmdParms[1].Value = model.TestQuestions_Id;
            cmdParms[2].Value = model.TestQuestions_OptionParent_OrderNum;
            cmdParms[3].Value = model.TestQuestions_Option_Content;
            cmdParms[4].Value = model.TestQuestions_Option_OrderNum;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.TestQuestions_Score_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_TestQuestions_Option DataRowToModel(DataRow row)
        {
            Model_TestQuestions_Option option = new Model_TestQuestions_Option();
            if (row != null)
            {
                if (row["TestQuestions_Option_Id"] != null)
                {
                    option.TestQuestions_Option_Id = row["TestQuestions_Option_Id"].ToString();
                }
                if (row["TestQuestions_Id"] != null)
                {
                    option.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if ((row["TestQuestions_OptionParent_OrderNum"] != null) && (row["TestQuestions_OptionParent_OrderNum"].ToString() != ""))
                {
                    option.TestQuestions_OptionParent_OrderNum = new int?(int.Parse(row["TestQuestions_OptionParent_OrderNum"].ToString()));
                }
                if (row["TestQuestions_Option_Content"] != null)
                {
                    option.TestQuestions_Option_Content = row["TestQuestions_Option_Content"].ToString();
                }
                if ((row["TestQuestions_Option_OrderNum"] != null) && (row["TestQuestions_Option_OrderNum"].ToString() != ""))
                {
                    option.TestQuestions_Option_OrderNum = new int?(int.Parse(row["TestQuestions_Option_OrderNum"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    option.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["TestQuestions_Score_ID"] != null)
                {
                    option.TestQuestions_Score_ID = row["TestQuestions_Score_ID"].ToString();
                }
            }
            return option;
        }

        public bool Delete(string TestQuestions_Option_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestQuestions_Option ");
            builder.Append(" where TestQuestions_Option_Id=@TestQuestions_Option_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Option_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestQuestions_Option_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TestQuestions_Option_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestQuestions_Option ");
            builder.Append(" where TestQuestions_Option_Id in (" + TestQuestions_Option_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TestQuestions_Option_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from TestQuestions_Option");
            builder.Append(" where TestQuestions_Option_Id=@TestQuestions_Option_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Option_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestQuestions_Option_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TestQuestions_Option_Id,TestQuestions_Id,TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,CreateTime,TestQuestions_Score_ID ");
            builder.Append(" FROM TestQuestions_Option ");
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
            builder.Append(" TestQuestions_Option_Id,TestQuestions_Id,TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,CreateTime,TestQuestions_Score_ID ");
            builder.Append(" FROM TestQuestions_Option ");
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
                builder.Append("order by T.TestQuestions_Option_Id desc");
            }
            builder.Append(")AS Row, T.*  from TestQuestions_Option T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_TestQuestions_Option GetModel(string TestQuestions_Option_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TestQuestions_Option_Id,TestQuestions_Id,TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,CreateTime,TestQuestions_Score_ID from TestQuestions_Option ");
            builder.Append(" where TestQuestions_Option_Id=@TestQuestions_Option_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Option_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestQuestions_Option_Id;
            new Model_TestQuestions_Option();
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
            builder.Append("select count(1) FROM TestQuestions_Option ");
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

        public bool Update(Model_TestQuestions_Option model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TestQuestions_Option set ");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("TestQuestions_OptionParent_OrderNum=@TestQuestions_OptionParent_OrderNum,");
            builder.Append("TestQuestions_Option_Content=@TestQuestions_Option_Content,");
            builder.Append("TestQuestions_Option_OrderNum=@TestQuestions_Option_OrderNum,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("TestQuestions_Score_ID=@TestQuestions_Score_ID");
            builder.Append(" where TestQuestions_Option_Id=@TestQuestions_Option_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_OptionParent_OrderNum", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Option_Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestQuestions_Option_OrderNum", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Option_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.TestQuestions_Id;
            cmdParms[1].Value = model.TestQuestions_OptionParent_OrderNum;
            cmdParms[2].Value = model.TestQuestions_Option_Content;
            cmdParms[3].Value = model.TestQuestions_Option_OrderNum;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.TestQuestions_Score_ID;
            cmdParms[6].Value = model.TestQuestions_Option_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

