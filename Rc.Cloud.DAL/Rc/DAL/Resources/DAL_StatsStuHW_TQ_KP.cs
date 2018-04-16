namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsStuHW_TQ_KP
    {
        public bool Add(Model_StatsStuHW_TQ_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsStuHW_TQ_KP(");
            builder.Append("StatsStuHW_TQ_KP_Id,HomeWork_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,TestQuestions_Id,TestQuestions_Score_Id,ComplexityText,TQScore,Score,Student_Answer_Status,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsStuHW_TQ_KP_Id,@HomeWork_Id,@Student_Id,@Subject,@Resource_Version,@Book_Type,@Parent_Id,@S_KnowledgePoint_Id,@KPNameBasic,@KPImportant,@GKScore,@TestQuestions_Id,@TestQuestions_Score_Id,@ComplexityText,@TQScore,@Score,@Student_Answer_Status,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsStuHW_TQ_KP_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Book_Type", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@KPImportant", SqlDbType.Int, 4), new SqlParameter("@GKScore", SqlDbType.Decimal, 9), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Score_Id", SqlDbType.Char, 0x24), new SqlParameter("@ComplexityText", SqlDbType.VarChar, 500), new SqlParameter("@TQScore", SqlDbType.Decimal, 9), new SqlParameter("@Score", SqlDbType.Decimal, 9), 
                new SqlParameter("@Student_Answer_Status", SqlDbType.VarChar, 20), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsStuHW_TQ_KP_Id;
            cmdParms[1].Value = model.HomeWork_Id;
            cmdParms[2].Value = model.Student_Id;
            cmdParms[3].Value = model.Subject;
            cmdParms[4].Value = model.Resource_Version;
            cmdParms[5].Value = model.Book_Type;
            cmdParms[6].Value = model.Parent_Id;
            cmdParms[7].Value = model.S_KnowledgePoint_Id;
            cmdParms[8].Value = model.KPNameBasic;
            cmdParms[9].Value = model.KPImportant;
            cmdParms[10].Value = model.GKScore;
            cmdParms[11].Value = model.TestQuestions_Id;
            cmdParms[12].Value = model.TestQuestions_Score_Id;
            cmdParms[13].Value = model.ComplexityText;
            cmdParms[14].Value = model.TQScore;
            cmdParms[15].Value = model.Score;
            cmdParms[0x10].Value = model.Student_Answer_Status;
            cmdParms[0x11].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsStuHW_TQ_KP DataRowToModel(DataRow row)
        {
            Model_StatsStuHW_TQ_KP uhw_tq_kp = new Model_StatsStuHW_TQ_KP();
            if (row != null)
            {
                if (row["StatsStuHW_TQ_KP_Id"] != null)
                {
                    uhw_tq_kp.StatsStuHW_TQ_KP_Id = row["StatsStuHW_TQ_KP_Id"].ToString();
                }
                if (row["HomeWork_Id"] != null)
                {
                    uhw_tq_kp.HomeWork_Id = row["HomeWork_Id"].ToString();
                }
                if (row["Student_Id"] != null)
                {
                    uhw_tq_kp.Student_Id = row["Student_Id"].ToString();
                }
                if (row["Subject"] != null)
                {
                    uhw_tq_kp.Subject = row["Subject"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    uhw_tq_kp.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["Book_Type"] != null)
                {
                    uhw_tq_kp.Book_Type = row["Book_Type"].ToString();
                }
                if (row["Parent_Id"] != null)
                {
                    uhw_tq_kp.Parent_Id = row["Parent_Id"].ToString();
                }
                if (row["S_KnowledgePoint_Id"] != null)
                {
                    uhw_tq_kp.S_KnowledgePoint_Id = row["S_KnowledgePoint_Id"].ToString();
                }
                if (row["KPNameBasic"] != null)
                {
                    uhw_tq_kp.KPNameBasic = row["KPNameBasic"].ToString();
                }
                if ((row["KPImportant"] != null) && (row["KPImportant"].ToString() != ""))
                {
                    uhw_tq_kp.KPImportant = new int?(int.Parse(row["KPImportant"].ToString()));
                }
                if ((row["GKScore"] != null) && (row["GKScore"].ToString() != ""))
                {
                    uhw_tq_kp.GKScore = new decimal?(decimal.Parse(row["GKScore"].ToString()));
                }
                if (row["TestQuestions_Id"] != null)
                {
                    uhw_tq_kp.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if (row["TestQuestions_Score_Id"] != null)
                {
                    uhw_tq_kp.TestQuestions_Score_Id = row["TestQuestions_Score_Id"].ToString();
                }
                if (row["ComplexityText"] != null)
                {
                    uhw_tq_kp.ComplexityText = row["ComplexityText"].ToString();
                }
                if ((row["TQScore"] != null) && (row["TQScore"].ToString() != ""))
                {
                    uhw_tq_kp.TQScore = new decimal?(decimal.Parse(row["TQScore"].ToString()));
                }
                if ((row["Score"] != null) && (row["Score"].ToString() != ""))
                {
                    uhw_tq_kp.Score = new decimal?(decimal.Parse(row["Score"].ToString()));
                }
                if (row["Student_Answer_Status"] != null)
                {
                    uhw_tq_kp.Student_Answer_Status = row["Student_Answer_Status"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    uhw_tq_kp.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return uhw_tq_kp;
        }

        public bool Delete(string StatsStuHW_TQ_KP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_TQ_KP ");
            builder.Append(" where StatsStuHW_TQ_KP_Id=@StatsStuHW_TQ_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_TQ_KP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_TQ_KP_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsStuHW_TQ_KP_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_TQ_KP ");
            builder.Append(" where StatsStuHW_TQ_KP_Id in (" + StatsStuHW_TQ_KP_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsStuHW_TQ_KP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsStuHW_TQ_KP");
            builder.Append(" where StatsStuHW_TQ_KP_Id=@StatsStuHW_TQ_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_TQ_KP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_TQ_KP_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsStuHW_TQ_KP_Id,HomeWork_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,TestQuestions_Id,TestQuestions_Score_Id,ComplexityText,TQScore,Score,Student_Answer_Status,CreateTime ");
            builder.Append(" FROM StatsStuHW_TQ_KP ");
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
            builder.Append(" StatsStuHW_TQ_KP_Id,HomeWork_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,TestQuestions_Id,TestQuestions_Score_Id,ComplexityText,TQScore,Score,Student_Answer_Status,CreateTime ");
            builder.Append(" FROM StatsStuHW_TQ_KP ");
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
                builder.Append("order by T.StatsStuHW_TQ_KP_Id desc");
            }
            builder.Append(")AS Row, T.*  from StatsStuHW_TQ_KP T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsStuHW_TQ_KP GetModel(string StatsStuHW_TQ_KP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsStuHW_TQ_KP_Id,HomeWork_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,TestQuestions_Id,TestQuestions_Score_Id,ComplexityText,TQScore,Score,Student_Answer_Status,CreateTime from StatsStuHW_TQ_KP ");
            builder.Append(" where StatsStuHW_TQ_KP_Id=@StatsStuHW_TQ_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_TQ_KP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_TQ_KP_Id;
            new Model_StatsStuHW_TQ_KP();
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
            builder.Append("select count(1) FROM StatsStuHW_TQ_KP ");
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

        public bool Update(Model_StatsStuHW_TQ_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsStuHW_TQ_KP set ");
            builder.Append("HomeWork_Id=@HomeWork_Id,");
            builder.Append("Student_Id=@Student_Id,");
            builder.Append("Subject=@Subject,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("Book_Type=@Book_Type,");
            builder.Append("Parent_Id=@Parent_Id,");
            builder.Append("S_KnowledgePoint_Id=@S_KnowledgePoint_Id,");
            builder.Append("KPNameBasic=@KPNameBasic,");
            builder.Append("KPImportant=@KPImportant,");
            builder.Append("GKScore=@GKScore,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("TestQuestions_Score_Id=@TestQuestions_Score_Id,");
            builder.Append("ComplexityText=@ComplexityText,");
            builder.Append("TQScore=@TQScore,");
            builder.Append("Score=@Score,");
            builder.Append("Student_Answer_Status=@Student_Answer_Status,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsStuHW_TQ_KP_Id=@StatsStuHW_TQ_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Book_Type", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@KPImportant", SqlDbType.Int, 4), new SqlParameter("@GKScore", SqlDbType.Decimal, 9), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Score_Id", SqlDbType.Char, 0x24), new SqlParameter("@ComplexityText", SqlDbType.VarChar, 500), new SqlParameter("@TQScore", SqlDbType.Decimal, 9), new SqlParameter("@Score", SqlDbType.Decimal, 9), new SqlParameter("@Student_Answer_Status", SqlDbType.VarChar, 20), 
                new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsStuHW_TQ_KP_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.HomeWork_Id;
            cmdParms[1].Value = model.Student_Id;
            cmdParms[2].Value = model.Subject;
            cmdParms[3].Value = model.Resource_Version;
            cmdParms[4].Value = model.Book_Type;
            cmdParms[5].Value = model.Parent_Id;
            cmdParms[6].Value = model.S_KnowledgePoint_Id;
            cmdParms[7].Value = model.KPNameBasic;
            cmdParms[8].Value = model.KPImportant;
            cmdParms[9].Value = model.GKScore;
            cmdParms[10].Value = model.TestQuestions_Id;
            cmdParms[11].Value = model.TestQuestions_Score_Id;
            cmdParms[12].Value = model.ComplexityText;
            cmdParms[13].Value = model.TQScore;
            cmdParms[14].Value = model.Score;
            cmdParms[15].Value = model.Student_Answer_Status;
            cmdParms[0x10].Value = model.CreateTime;
            cmdParms[0x11].Value = model.StatsStuHW_TQ_KP_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

