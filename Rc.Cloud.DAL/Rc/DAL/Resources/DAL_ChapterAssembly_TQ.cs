namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_ChapterAssembly_TQ
    {
        public bool Add(Model_ChapterAssembly_TQ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ChapterAssembly_TQ(");
            builder.Append("ChapterAssembly_TQ_Id,Identifier_Id,TQ_Order,TQ_Type,type,ComplexityText,TestQuestions_Id,ResourceToResourceFolder_Id,Parent_Id)");
            builder.Append(" values (");
            builder.Append("@ChapterAssembly_TQ_Id,@Identifier_Id,@TQ_Order,@TQ_Type,@type,@ComplexityText,@TestQuestions_Id,@ResourceToResourceFolder_Id,@Parent_Id)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ChapterAssembly_TQ_Id", SqlDbType.Char, 0x24), new SqlParameter("@Identifier_Id", SqlDbType.Char, 0x24), new SqlParameter("@TQ_Order", SqlDbType.Int, 4), new SqlParameter("@TQ_Type", SqlDbType.Char, 0x24), new SqlParameter("@type", SqlDbType.Char, 0x24), new SqlParameter("@ComplexityText", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.ChapterAssembly_TQ_Id;
            cmdParms[1].Value = model.Identifier_Id;
            cmdParms[2].Value = model.TQ_Order;
            cmdParms[3].Value = model.TQ_Type;
            cmdParms[4].Value = model.type;
            cmdParms[5].Value = model.ComplexityText;
            cmdParms[6].Value = model.TestQuestions_Id;
            cmdParms[7].Value = model.ResourceToResourceFolder_Id;
            cmdParms[8].Value = model.Parent_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_ChapterAssembly_TQ DataRowToModel(DataRow row)
        {
            Model_ChapterAssembly_TQ y_tq = new Model_ChapterAssembly_TQ();
            if (row != null)
            {
                if (row["ChapterAssembly_TQ_Id"] != null)
                {
                    y_tq.ChapterAssembly_TQ_Id = row["ChapterAssembly_TQ_Id"].ToString();
                }
                if (row["Identifier_Id"] != null)
                {
                    y_tq.Identifier_Id = row["Identifier_Id"].ToString();
                }
                if ((row["TQ_Order"] != null) && (row["TQ_Order"].ToString() != ""))
                {
                    y_tq.TQ_Order = new int?(int.Parse(row["TQ_Order"].ToString()));
                }
                if (row["TQ_Type"] != null)
                {
                    y_tq.TQ_Type = row["TQ_Type"].ToString();
                }
                if (row["type"] != null)
                {
                    y_tq.type = row["type"].ToString();
                }
                if (row["ComplexityText"] != null)
                {
                    y_tq.ComplexityText = row["ComplexityText"].ToString();
                }
                if (row["TestQuestions_Id"] != null)
                {
                    y_tq.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    y_tq.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Parent_Id"] != null)
                {
                    y_tq.Parent_Id = row["Parent_Id"].ToString();
                }
            }
            return y_tq;
        }

        public bool Delete(string ChapterAssembly_TQ_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ChapterAssembly_TQ ");
            builder.Append(" where ChapterAssembly_TQ_Id=@ChapterAssembly_TQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ChapterAssembly_TQ_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ChapterAssembly_TQ_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ChapterAssembly_TQ_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ChapterAssembly_TQ ");
            builder.Append(" where ChapterAssembly_TQ_Id in (" + ChapterAssembly_TQ_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ChapterAssembly_TQ_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ChapterAssembly_TQ");
            builder.Append(" where ChapterAssembly_TQ_Id=@ChapterAssembly_TQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ChapterAssembly_TQ_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ChapterAssembly_TQ_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ChapterAssembly_TQ_Id,Identifier_Id,TQ_Order,TQ_Type,type,ComplexityText,TestQuestions_Id,ResourceToResourceFolder_Id,Parent_Id ");
            builder.Append(" FROM ChapterAssembly_TQ ");
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
            builder.Append(" ChapterAssembly_TQ_Id,Identifier_Id,TQ_Order,TQ_Type,type,ComplexityText,TestQuestions_Id,ResourceToResourceFolder_Id,Parent_Id ");
            builder.Append(" FROM ChapterAssembly_TQ ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string identifier, string ChapterAssembly_TQ_Id, string ChangeType, out int rCount)
        {
            rCount = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@identifier", SqlDbType.NVarChar, 200), new SqlParameter("@ChapterAssembly_TQ_Id", SqlDbType.NVarChar, 200), new SqlParameter("@ChangeType", SqlDbType.NVarChar, 50), new SqlParameter("@rCount", SqlDbType.Int) };
            parameters[0].Value = identifier;
            parameters[1].Value = ChapterAssembly_TQ_Id;
            parameters[2].Value = ChangeType;
            parameters[3].Direction = ParameterDirection.Output;
            return DbHelperSQL.RunProcedure("P_ChapterChangeQuestions", parameters, "ds", out rCount);
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
                builder.Append("order by T.ChapterAssembly_TQ_Id desc");
            }
            builder.Append(")AS Row, T.*  from ChapterAssembly_TQ T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_ChapterAssembly_TQ GetModel(string ChapterAssembly_TQ_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ChapterAssembly_TQ_Id,Identifier_Id,TQ_Order,TQ_Type,type,ComplexityText,TestQuestions_Id,ResourceToResourceFolder_Id,Parent_Id from ChapterAssembly_TQ ");
            builder.Append(" where ChapterAssembly_TQ_Id=@ChapterAssembly_TQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ChapterAssembly_TQ_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ChapterAssembly_TQ_Id;
            new Model_ChapterAssembly_TQ();
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
            builder.Append("select count(1) FROM ChapterAssembly_TQ ");
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

        public bool Update(Model_ChapterAssembly_TQ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ChapterAssembly_TQ set ");
            builder.Append("Identifier_Id=@Identifier_Id,");
            builder.Append("TQ_Order=@TQ_Order,");
            builder.Append("TQ_Type=@TQ_Type,");
            builder.Append("type=@type,");
            builder.Append("ComplexityText=@ComplexityText,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("Parent_Id=@Parent_Id");
            builder.Append(" where ChapterAssembly_TQ_Id=@ChapterAssembly_TQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Identifier_Id", SqlDbType.Char, 0x24), new SqlParameter("@TQ_Order", SqlDbType.Int, 4), new SqlParameter("@TQ_Type", SqlDbType.Char, 0x24), new SqlParameter("@type", SqlDbType.Char, 0x24), new SqlParameter("@ComplexityText", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@ChapterAssembly_TQ_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Identifier_Id;
            cmdParms[1].Value = model.TQ_Order;
            cmdParms[2].Value = model.TQ_Type;
            cmdParms[3].Value = model.type;
            cmdParms[4].Value = model.ComplexityText;
            cmdParms[5].Value = model.TestQuestions_Id;
            cmdParms[6].Value = model.ResourceToResourceFolder_Id;
            cmdParms[7].Value = model.Parent_Id;
            cmdParms[8].Value = model.ChapterAssembly_TQ_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

