namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_RelationPaperTemp
    {
        public bool Add(Model_RelationPaperTemp model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into RelationPaperTemp(");
            builder.Append("RelationPaperTemp_id,RelationPaper_Id,TestQuestions_Id,CreateUser,Two_WayChecklistDetail_Id)");
            builder.Append(" values (");
            builder.Append("@RelationPaperTemp_id,@RelationPaper_Id,@TestQuestions_Id,@CreateUser,@Two_WayChecklistDetail_Id)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelationPaperTemp_id", SqlDbType.Char, 0x24), new SqlParameter("@RelationPaper_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklistDetail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.RelationPaperTemp_id;
            cmdParms[1].Value = model.RelationPaper_Id;
            cmdParms[2].Value = model.TestQuestions_Id;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.Two_WayChecklistDetail_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_RelationPaperTemp DataRowToModel(DataRow row)
        {
            Model_RelationPaperTemp temp = new Model_RelationPaperTemp();
            if (row != null)
            {
                if (row["RelationPaperTemp_id"] != null)
                {
                    temp.RelationPaperTemp_id = row["RelationPaperTemp_id"].ToString();
                }
                if (row["RelationPaper_Id"] != null)
                {
                    temp.RelationPaper_Id = row["RelationPaper_Id"].ToString();
                }
                if (row["TestQuestions_Id"] != null)
                {
                    temp.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    temp.CreateUser = row["CreateUser"].ToString();
                }
                if (row["Two_WayChecklistDetail_Id"] != null)
                {
                    temp.Two_WayChecklistDetail_Id = row["Two_WayChecklistDetail_Id"].ToString();
                }
            }
            return temp;
        }

        public bool Delete(string RelationPaperTemp_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from RelationPaperTemp ");
            builder.Append(" where RelationPaperTemp_id=@RelationPaperTemp_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelationPaperTemp_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = RelationPaperTemp_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string RelationPaperTemp_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from RelationPaperTemp ");
            builder.Append(" where RelationPaperTemp_id in (" + RelationPaperTemp_idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string RelationPaperTemp_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from RelationPaperTemp");
            builder.Append(" where RelationPaperTemp_id=@RelationPaperTemp_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelationPaperTemp_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = RelationPaperTemp_id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RelationPaperTemp_id,RelationPaper_Id,TestQuestions_Id,CreateUser,Two_WayChecklistDetail_Id ");
            builder.Append(" FROM RelationPaperTemp ");
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
            builder.Append(" RelationPaperTemp_id,RelationPaper_Id,TestQuestions_Id,CreateUser,Two_WayChecklistDetail_Id ");
            builder.Append(" FROM RelationPaperTemp ");
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
                builder.Append("order by T.RelationPaperTemp_id desc");
            }
            builder.Append(")AS Row, T.*  from RelationPaperTemp T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_RelationPaperTemp GetModel(string RelationPaperTemp_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 RelationPaperTemp_id,RelationPaper_Id,TestQuestions_Id,CreateUser,Two_WayChecklistDetail_Id from RelationPaperTemp ");
            builder.Append(" where RelationPaperTemp_id=@RelationPaperTemp_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelationPaperTemp_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = RelationPaperTemp_id;
            new Model_RelationPaperTemp();
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
            builder.Append("select count(1) FROM RelationPaperTemp ");
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

        public bool Update(Model_RelationPaperTemp model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update RelationPaperTemp set ");
            builder.Append("RelationPaper_Id=@RelationPaper_Id,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("Two_WayChecklistDetail_Id=@Two_WayChecklistDetail_Id");
            builder.Append(" where RelationPaperTemp_id=@RelationPaperTemp_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelationPaper_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklistDetail_Id", SqlDbType.Char, 0x24), new SqlParameter("@RelationPaperTemp_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.RelationPaper_Id;
            cmdParms[1].Value = model.TestQuestions_Id;
            cmdParms[2].Value = model.CreateUser;
            cmdParms[3].Value = model.Two_WayChecklistDetail_Id;
            cmdParms[4].Value = model.RelationPaperTemp_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

