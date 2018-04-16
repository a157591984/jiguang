namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_TestPaper_FrameDetailToTestQuestions_Attr
    {
        public bool Add(Model_TestPaper_FrameDetailToTestQuestions_Attr model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TestPaper_FrameDetailToTestQuestions_Attr(");
            builder.Append("TestPaper_FrameDetailToTestQuestions_Attr_Id,TestPaper_Frame_Id,TestPaper_FrameDetail_Id,Attr_Type,Attr_Value,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@TestPaper_FrameDetailToTestQuestions_Attr_Id,@TestPaper_Frame_Id,@TestPaper_FrameDetail_Id,@Attr_Type,@Attr_Value,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetailToTestQuestions_Attr_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_FrameDetail_Id", SqlDbType.Char, 0x24), new SqlParameter("@Attr_Type", SqlDbType.Char, 1), new SqlParameter("@Attr_Value", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.TestPaper_FrameDetailToTestQuestions_Attr_Id;
            cmdParms[1].Value = model.TestPaper_Frame_Id;
            cmdParms[2].Value = model.TestPaper_FrameDetail_Id;
            cmdParms[3].Value = model.Attr_Type;
            cmdParms[4].Value = model.Attr_Value;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_TestPaper_FrameDetailToTestQuestions_Attr DataRowToModel(DataRow row)
        {
            Model_TestPaper_FrameDetailToTestQuestions_Attr attr = new Model_TestPaper_FrameDetailToTestQuestions_Attr();
            if (row != null)
            {
                if (row["TestPaper_FrameDetailToTestQuestions_Attr_Id"] != null)
                {
                    attr.TestPaper_FrameDetailToTestQuestions_Attr_Id = row["TestPaper_FrameDetailToTestQuestions_Attr_Id"].ToString();
                }
                if (row["TestPaper_Frame_Id"] != null)
                {
                    attr.TestPaper_Frame_Id = row["TestPaper_Frame_Id"].ToString();
                }
                if (row["TestPaper_FrameDetail_Id"] != null)
                {
                    attr.TestPaper_FrameDetail_Id = row["TestPaper_FrameDetail_Id"].ToString();
                }
                if (row["Attr_Type"] != null)
                {
                    attr.Attr_Type = row["Attr_Type"].ToString();
                }
                if (row["Attr_Value"] != null)
                {
                    attr.Attr_Value = row["Attr_Value"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    attr.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    attr.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return attr;
        }

        public bool Delete(string TestPaper_FrameDetailToTestQuestions_Attr_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestPaper_FrameDetailToTestQuestions_Attr ");
            builder.Append(" where TestPaper_FrameDetailToTestQuestions_Attr_Id=@TestPaper_FrameDetailToTestQuestions_Attr_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetailToTestQuestions_Attr_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameDetailToTestQuestions_Attr_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TestPaper_FrameDetailToTestQuestions_Attr_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestPaper_FrameDetailToTestQuestions_Attr ");
            builder.Append(" where TestPaper_FrameDetailToTestQuestions_Attr_Id in (" + TestPaper_FrameDetailToTestQuestions_Attr_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TestPaper_FrameDetailToTestQuestions_Attr_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from TestPaper_FrameDetailToTestQuestions_Attr");
            builder.Append(" where TestPaper_FrameDetailToTestQuestions_Attr_Id=@TestPaper_FrameDetailToTestQuestions_Attr_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetailToTestQuestions_Attr_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameDetailToTestQuestions_Attr_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TestPaper_FrameDetailToTestQuestions_Attr_Id,TestPaper_Frame_Id,TestPaper_FrameDetail_Id,Attr_Type,Attr_Value,CreateUser,CreateTime ");
            builder.Append(" FROM TestPaper_FrameDetailToTestQuestions_Attr ");
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
            builder.Append(" TestPaper_FrameDetailToTestQuestions_Attr_Id,TestPaper_Frame_Id,TestPaper_FrameDetail_Id,Attr_Type,Attr_Value,CreateUser,CreateTime ");
            builder.Append(" FROM TestPaper_FrameDetailToTestQuestions_Attr ");
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
                builder.Append("order by T.TestPaper_FrameDetailToTestQuestions_Attr_Id desc");
            }
            builder.Append(")AS Row, T.*  from TestPaper_FrameDetailToTestQuestions_Attr T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_TestPaper_FrameDetailToTestQuestions_Attr GetModel(string TestPaper_FrameDetailToTestQuestions_Attr_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TestPaper_FrameDetailToTestQuestions_Attr_Id,TestPaper_Frame_Id,TestPaper_FrameDetail_Id,Attr_Type,Attr_Value,CreateUser,CreateTime from TestPaper_FrameDetailToTestQuestions_Attr ");
            builder.Append(" where TestPaper_FrameDetailToTestQuestions_Attr_Id=@TestPaper_FrameDetailToTestQuestions_Attr_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetailToTestQuestions_Attr_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameDetailToTestQuestions_Attr_Id;
            new Model_TestPaper_FrameDetailToTestQuestions_Attr();
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
            builder.Append("select count(1) FROM TestPaper_FrameDetailToTestQuestions_Attr ");
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

        public bool Update(Model_TestPaper_FrameDetailToTestQuestions_Attr model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TestPaper_FrameDetailToTestQuestions_Attr set ");
            builder.Append("TestPaper_Frame_Id=@TestPaper_Frame_Id,");
            builder.Append("TestPaper_FrameDetail_Id=@TestPaper_FrameDetail_Id,");
            builder.Append("Attr_Type=@Attr_Type,");
            builder.Append("Attr_Value=@Attr_Value,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where TestPaper_FrameDetailToTestQuestions_Attr_Id=@TestPaper_FrameDetailToTestQuestions_Attr_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_FrameDetail_Id", SqlDbType.Char, 0x24), new SqlParameter("@Attr_Type", SqlDbType.Char, 1), new SqlParameter("@Attr_Value", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestPaper_FrameDetailToTestQuestions_Attr_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.TestPaper_Frame_Id;
            cmdParms[1].Value = model.TestPaper_FrameDetail_Id;
            cmdParms[2].Value = model.Attr_Type;
            cmdParms[3].Value = model.Attr_Value;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.TestPaper_FrameDetailToTestQuestions_Attr_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

