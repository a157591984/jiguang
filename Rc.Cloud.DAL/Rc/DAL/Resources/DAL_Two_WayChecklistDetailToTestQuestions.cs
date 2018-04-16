namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Two_WayChecklistDetailToTestQuestions
    {
        public bool Add(Model_Two_WayChecklistDetailToTestQuestions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Two_WayChecklistDetailToTestQuestions(");
            builder.Append("Two_WayChecklistDetailToTestQuestions_Id,Two_WayChecklist_Id,Two_WayChecklistDetail_Id,ResourceToResourceFolder_Id,TestQuestions_Id,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@Two_WayChecklistDetailToTestQuestions_Id,@Two_WayChecklist_Id,@Two_WayChecklistDetail_Id,@ResourceToResourceFolder_Id,@TestQuestions_Id,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistDetailToTestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklistDetail_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Two_WayChecklistDetailToTestQuestions_Id;
            cmdParms[1].Value = model.Two_WayChecklist_Id;
            cmdParms[2].Value = model.Two_WayChecklistDetail_Id;
            cmdParms[3].Value = model.ResourceToResourceFolder_Id;
            cmdParms[4].Value = model.TestQuestions_Id;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Two_WayChecklistDetailToTestQuestions DataRowToModel(DataRow row)
        {
            Model_Two_WayChecklistDetailToTestQuestions questions = new Model_Two_WayChecklistDetailToTestQuestions();
            if (row != null)
            {
                if (row["Two_WayChecklistDetailToTestQuestions_Id"] != null)
                {
                    questions.Two_WayChecklistDetailToTestQuestions_Id = row["Two_WayChecklistDetailToTestQuestions_Id"].ToString();
                }
                if (row["Two_WayChecklist_Id"] != null)
                {
                    questions.Two_WayChecklist_Id = row["Two_WayChecklist_Id"].ToString();
                }
                if (row["Two_WayChecklistDetail_Id"] != null)
                {
                    questions.Two_WayChecklistDetail_Id = row["Two_WayChecklistDetail_Id"].ToString();
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

        public bool Delete(string Two_WayChecklistDetailToTestQuestions_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklistDetailToTestQuestions ");
            builder.Append(" where Two_WayChecklistDetailToTestQuestions_Id=@Two_WayChecklistDetailToTestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistDetailToTestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistDetailToTestQuestions_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Two_WayChecklistDetailToTestQuestions_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklistDetailToTestQuestions ");
            builder.Append(" where Two_WayChecklistDetailToTestQuestions_Id in (" + Two_WayChecklistDetailToTestQuestions_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Two_WayChecklistDetailToTestQuestions_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Two_WayChecklistDetailToTestQuestions");
            builder.Append(" where Two_WayChecklistDetailToTestQuestions_Id=@Two_WayChecklistDetailToTestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistDetailToTestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistDetailToTestQuestions_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Two_WayChecklistDetailToTestQuestions_Id,Two_WayChecklist_Id,Two_WayChecklistDetail_Id,ResourceToResourceFolder_Id,TestQuestions_Id,CreateUser,CreateTime ");
            builder.Append(" FROM Two_WayChecklistDetailToTestQuestions ");
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
            builder.Append(" Two_WayChecklistDetailToTestQuestions_Id,Two_WayChecklist_Id,Two_WayChecklistDetail_Id,ResourceToResourceFolder_Id,TestQuestions_Id,CreateUser,CreateTime ");
            builder.Append(" FROM Two_WayChecklistDetailToTestQuestions ");
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
                builder.Append("order by T.Two_WayChecklistDetailToTestQuestions_Id desc");
            }
            builder.Append(")AS Row, T.*  from Two_WayChecklistDetailToTestQuestions T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Two_WayChecklistDetailToTestQuestions GetModel(string Two_WayChecklistDetailToTestQuestions_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Two_WayChecklistDetailToTestQuestions_Id,Two_WayChecklist_Id,Two_WayChecklistDetail_Id,ResourceToResourceFolder_Id,TestQuestions_Id,CreateUser,CreateTime from Two_WayChecklistDetailToTestQuestions ");
            builder.Append(" where Two_WayChecklistDetailToTestQuestions_Id=@Two_WayChecklistDetailToTestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistDetailToTestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistDetailToTestQuestions_Id;
            new Model_Two_WayChecklistDetailToTestQuestions();
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
            builder.Append("select count(1) FROM Two_WayChecklistDetailToTestQuestions ");
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

        public bool Update(Model_Two_WayChecklistDetailToTestQuestions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Two_WayChecklistDetailToTestQuestions set ");
            builder.Append("Two_WayChecklist_Id=@Two_WayChecklist_Id,");
            builder.Append("Two_WayChecklistDetail_Id=@Two_WayChecklistDetail_Id,");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where Two_WayChecklistDetailToTestQuestions_Id=@Two_WayChecklistDetailToTestQuestions_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklistDetail_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Two_WayChecklistDetailToTestQuestions_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Two_WayChecklist_Id;
            cmdParms[1].Value = model.Two_WayChecklistDetail_Id;
            cmdParms[2].Value = model.ResourceToResourceFolder_Id;
            cmdParms[3].Value = model.TestQuestions_Id;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.Two_WayChecklistDetailToTestQuestions_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

