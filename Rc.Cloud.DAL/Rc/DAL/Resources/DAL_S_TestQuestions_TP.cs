namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_S_TestQuestions_TP
    {
        public bool Add(Model_S_TestQuestions_TP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into S_TestQuestions_TP(");
            builder.Append("S_TestQuestions_TP_Id,TestQuestions_Score_ID,TestQuestions_Id,ResourceToResourceFolder_Id,S_TestingPoint_Id,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@S_TestQuestions_TP_Id,@TestQuestions_Score_ID,@TestQuestions_Id,@ResourceToResourceFolder_Id,@S_TestingPoint_Id,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_TestQuestions_TP_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_TestingPoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.S_TestQuestions_TP_Id;
            cmdParms[1].Value = model.TestQuestions_Score_ID;
            cmdParms[2].Value = model.TestQuestions_Id;
            cmdParms[3].Value = model.ResourceToResourceFolder_Id;
            cmdParms[4].Value = model.S_TestingPoint_Id;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_S_TestQuestions_TP DataRowToModel(DataRow row)
        {
            Model_S_TestQuestions_TP s_tp = new Model_S_TestQuestions_TP();
            if (row != null)
            {
                if (row["S_TestQuestions_TP_Id"] != null)
                {
                    s_tp.S_TestQuestions_TP_Id = row["S_TestQuestions_TP_Id"].ToString();
                }
                if (row["TestQuestions_Score_ID"] != null)
                {
                    s_tp.TestQuestions_Score_ID = row["TestQuestions_Score_ID"].ToString();
                }
                if (row["TestQuestions_Id"] != null)
                {
                    s_tp.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    s_tp.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["S_TestingPoint_Id"] != null)
                {
                    s_tp.S_TestingPoint_Id = row["S_TestingPoint_Id"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    s_tp.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    s_tp.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return s_tp;
        }

        public bool Delete(string S_TestQuestions_TP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_TestQuestions_TP ");
            builder.Append(" where S_TestQuestions_TP_Id=@S_TestQuestions_TP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_TestQuestions_TP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_TestQuestions_TP_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string S_TestQuestions_TP_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_TestQuestions_TP ");
            builder.Append(" where S_TestQuestions_TP_Id in (" + S_TestQuestions_TP_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string S_TestQuestions_TP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from S_TestQuestions_TP");
            builder.Append(" where S_TestQuestions_TP_Id=@S_TestQuestions_TP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_TestQuestions_TP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_TestQuestions_TP_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select S_TestQuestions_TP_Id,TestQuestions_Score_ID,TestQuestions_Id,ResourceToResourceFolder_Id,S_TestingPoint_Id,CreateUser,CreateTime ");
            builder.Append(" FROM S_TestQuestions_TP ");
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
            builder.Append(" S_TestQuestions_TP_Id,TestQuestions_Score_ID,TestQuestions_Id,ResourceToResourceFolder_Id,S_TestingPoint_Id,CreateUser,CreateTime ");
            builder.Append(" FROM S_TestQuestions_TP ");
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
                builder.Append("order by T.S_TestQuestions_TP_Id desc");
            }
            builder.Append(")AS Row, T.*  from S_TestQuestions_TP T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_S_TestQuestions_TP GetModel(string S_TestQuestions_TP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 S_TestQuestions_TP_Id,TestQuestions_Score_ID,TestQuestions_Id,ResourceToResourceFolder_Id,S_TestingPoint_Id,CreateUser,CreateTime from S_TestQuestions_TP ");
            builder.Append(" where S_TestQuestions_TP_Id=@S_TestQuestions_TP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_TestQuestions_TP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_TestQuestions_TP_Id;
            new Model_S_TestQuestions_TP();
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
            builder.Append("select count(1) FROM S_TestQuestions_TP ");
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

        public bool Update(Model_S_TestQuestions_TP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update S_TestQuestions_TP set ");
            builder.Append("TestQuestions_Score_ID=@TestQuestions_Score_ID,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("S_TestingPoint_Id=@S_TestingPoint_Id,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where S_TestQuestions_TP_Id=@S_TestQuestions_TP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_TestingPoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@S_TestQuestions_TP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.TestQuestions_Score_ID;
            cmdParms[1].Value = model.TestQuestions_Id;
            cmdParms[2].Value = model.ResourceToResourceFolder_Id;
            cmdParms[3].Value = model.S_TestingPoint_Id;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.S_TestQuestions_TP_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

