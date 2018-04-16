namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsStuHW_Analysis_KP
    {
        public bool Add(Model_StatsStuHW_Analysis_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsStuHW_Analysis_KP(");
            builder.Append("StatsStuHW_Analysis_KP_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,HWCount,TQCount_Right,TQCount_Wrong,TQMastery_No,ComplexityText,TotalScore,Score,KPMastery,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsStuHW_Analysis_KP_Id,@Student_Id,@Subject,@Resource_Version,@Book_Type,@Parent_Id,@S_KnowledgePoint_Id,@KPNameBasic,@KPImportant,@GKScore,@HWCount,@TQCount_Right,@TQCount_Wrong,@TQMastery_No,@ComplexityText,@TotalScore,@Score,@KPMastery,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsStuHW_Analysis_KP_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Book_Type", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@KPImportant", SqlDbType.Int, 4), new SqlParameter("@GKScore", SqlDbType.Decimal, 9), new SqlParameter("@HWCount", SqlDbType.Int, 4), new SqlParameter("@TQCount_Right", SqlDbType.Int, 4), new SqlParameter("@TQCount_Wrong", SqlDbType.Int, 4), new SqlParameter("@TQMastery_No", SqlDbType.Decimal, 9), new SqlParameter("@ComplexityText", SqlDbType.VarChar, 500), new SqlParameter("@TotalScore", SqlDbType.Decimal, 9), 
                new SqlParameter("@Score", SqlDbType.Decimal, 9), new SqlParameter("@KPMastery", SqlDbType.Decimal, 9), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsStuHW_Analysis_KP_Id;
            cmdParms[1].Value = model.Student_Id;
            cmdParms[2].Value = model.Subject;
            cmdParms[3].Value = model.Resource_Version;
            cmdParms[4].Value = model.Book_Type;
            cmdParms[5].Value = model.Parent_Id;
            cmdParms[6].Value = model.S_KnowledgePoint_Id;
            cmdParms[7].Value = model.KPNameBasic;
            cmdParms[8].Value = model.KPImportant;
            cmdParms[9].Value = model.GKScore;
            cmdParms[10].Value = model.HWCount;
            cmdParms[11].Value = model.TQCount_Right;
            cmdParms[12].Value = model.TQCount_Wrong;
            cmdParms[13].Value = model.TQMastery_No;
            cmdParms[14].Value = model.ComplexityText;
            cmdParms[15].Value = model.TotalScore;
            cmdParms[0x10].Value = model.Score;
            cmdParms[0x11].Value = model.KPMastery;
            cmdParms[0x12].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsStuHW_Analysis_KP DataRowToModel(DataRow row)
        {
            Model_StatsStuHW_Analysis_KP s_kp = new Model_StatsStuHW_Analysis_KP();
            if (row != null)
            {
                if (row["StatsStuHW_Analysis_KP_Id"] != null)
                {
                    s_kp.StatsStuHW_Analysis_KP_Id = row["StatsStuHW_Analysis_KP_Id"].ToString();
                }
                if (row["Student_Id"] != null)
                {
                    s_kp.Student_Id = row["Student_Id"].ToString();
                }
                if (row["Subject"] != null)
                {
                    s_kp.Subject = row["Subject"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    s_kp.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["Book_Type"] != null)
                {
                    s_kp.Book_Type = row["Book_Type"].ToString();
                }
                if (row["Parent_Id"] != null)
                {
                    s_kp.Parent_Id = row["Parent_Id"].ToString();
                }
                if (row["S_KnowledgePoint_Id"] != null)
                {
                    s_kp.S_KnowledgePoint_Id = row["S_KnowledgePoint_Id"].ToString();
                }
                if (row["KPNameBasic"] != null)
                {
                    s_kp.KPNameBasic = row["KPNameBasic"].ToString();
                }
                if ((row["KPImportant"] != null) && (row["KPImportant"].ToString() != ""))
                {
                    s_kp.KPImportant = new int?(int.Parse(row["KPImportant"].ToString()));
                }
                if ((row["GKScore"] != null) && (row["GKScore"].ToString() != ""))
                {
                    s_kp.GKScore = new decimal?(decimal.Parse(row["GKScore"].ToString()));
                }
                if ((row["HWCount"] != null) && (row["HWCount"].ToString() != ""))
                {
                    s_kp.HWCount = new int?(int.Parse(row["HWCount"].ToString()));
                }
                if ((row["TQCount_Right"] != null) && (row["TQCount_Right"].ToString() != ""))
                {
                    s_kp.TQCount_Right = new int?(int.Parse(row["TQCount_Right"].ToString()));
                }
                if ((row["TQCount_Wrong"] != null) && (row["TQCount_Wrong"].ToString() != ""))
                {
                    s_kp.TQCount_Wrong = new int?(int.Parse(row["TQCount_Wrong"].ToString()));
                }
                if ((row["TQMastery_No"] != null) && (row["TQMastery_No"].ToString() != ""))
                {
                    s_kp.TQMastery_No = new decimal?(decimal.Parse(row["TQMastery_No"].ToString()));
                }
                if (row["ComplexityText"] != null)
                {
                    s_kp.ComplexityText = row["ComplexityText"].ToString();
                }
                if ((row["TotalScore"] != null) && (row["TotalScore"].ToString() != ""))
                {
                    s_kp.TotalScore = new decimal?(decimal.Parse(row["TotalScore"].ToString()));
                }
                if ((row["Score"] != null) && (row["Score"].ToString() != ""))
                {
                    s_kp.Score = new decimal?(decimal.Parse(row["Score"].ToString()));
                }
                if ((row["KPMastery"] != null) && (row["KPMastery"].ToString() != ""))
                {
                    s_kp.KPMastery = new decimal?(decimal.Parse(row["KPMastery"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    s_kp.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return s_kp;
        }

        public bool Delete(string StatsStuHW_Analysis_KP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_Analysis_KP ");
            builder.Append(" where StatsStuHW_Analysis_KP_Id=@StatsStuHW_Analysis_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Analysis_KP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Analysis_KP_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsStuHW_Analysis_KP_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_Analysis_KP ");
            builder.Append(" where StatsStuHW_Analysis_KP_Id in (" + StatsStuHW_Analysis_KP_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsStuHW_Analysis_KP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsStuHW_Analysis_KP");
            builder.Append(" where StatsStuHW_Analysis_KP_Id=@StatsStuHW_Analysis_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Analysis_KP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Analysis_KP_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsStuHW_Analysis_KP_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,HWCount,TQCount_Right,TQCount_Wrong,TQMastery_No,ComplexityText,TotalScore,Score,KPMastery,CreateTime ");
            builder.Append(" FROM StatsStuHW_Analysis_KP ");
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
            builder.Append(" StatsStuHW_Analysis_KP_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,HWCount,TQCount_Right,TQCount_Wrong,TQMastery_No,ComplexityText,TotalScore,Score,KPMastery,CreateTime ");
            builder.Append(" FROM StatsStuHW_Analysis_KP ");
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
                builder.Append("order by T.StatsStuHW_Analysis_KP_Id desc");
            }
            builder.Append(")AS Row, T.*  from StatsStuHW_Analysis_KP T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsStuHW_Analysis_KP GetModel(string StatsStuHW_Analysis_KP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsStuHW_Analysis_KP_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,HWCount,TQCount_Right,TQCount_Wrong,TQMastery_No,ComplexityText,TotalScore,Score,KPMastery,CreateTime from StatsStuHW_Analysis_KP ");
            builder.Append(" where StatsStuHW_Analysis_KP_Id=@StatsStuHW_Analysis_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Analysis_KP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Analysis_KP_Id;
            new Model_StatsStuHW_Analysis_KP();
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
            builder.Append("select count(1) FROM StatsStuHW_Analysis_KP ");
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

        public bool Update(Model_StatsStuHW_Analysis_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsStuHW_Analysis_KP set ");
            builder.Append("Student_Id=@Student_Id,");
            builder.Append("Subject=@Subject,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("Book_Type=@Book_Type,");
            builder.Append("Parent_Id=@Parent_Id,");
            builder.Append("S_KnowledgePoint_Id=@S_KnowledgePoint_Id,");
            builder.Append("KPNameBasic=@KPNameBasic,");
            builder.Append("KPImportant=@KPImportant,");
            builder.Append("GKScore=@GKScore,");
            builder.Append("HWCount=@HWCount,");
            builder.Append("TQCount_Right=@TQCount_Right,");
            builder.Append("TQCount_Wrong=@TQCount_Wrong,");
            builder.Append("TQMastery_No=@TQMastery_No,");
            builder.Append("ComplexityText=@ComplexityText,");
            builder.Append("TotalScore=@TotalScore,");
            builder.Append("Score=@Score,");
            builder.Append("KPMastery=@KPMastery,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsStuHW_Analysis_KP_Id=@StatsStuHW_Analysis_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Book_Type", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@KPImportant", SqlDbType.Int, 4), new SqlParameter("@GKScore", SqlDbType.Decimal, 9), new SqlParameter("@HWCount", SqlDbType.Int, 4), new SqlParameter("@TQCount_Right", SqlDbType.Int, 4), new SqlParameter("@TQCount_Wrong", SqlDbType.Int, 4), new SqlParameter("@TQMastery_No", SqlDbType.Decimal, 9), new SqlParameter("@ComplexityText", SqlDbType.VarChar, 500), new SqlParameter("@TotalScore", SqlDbType.Decimal, 9), new SqlParameter("@Score", SqlDbType.Decimal, 9), 
                new SqlParameter("@KPMastery", SqlDbType.Decimal, 9), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsStuHW_Analysis_KP_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.Student_Id;
            cmdParms[1].Value = model.Subject;
            cmdParms[2].Value = model.Resource_Version;
            cmdParms[3].Value = model.Book_Type;
            cmdParms[4].Value = model.Parent_Id;
            cmdParms[5].Value = model.S_KnowledgePoint_Id;
            cmdParms[6].Value = model.KPNameBasic;
            cmdParms[7].Value = model.KPImportant;
            cmdParms[8].Value = model.GKScore;
            cmdParms[9].Value = model.HWCount;
            cmdParms[10].Value = model.TQCount_Right;
            cmdParms[11].Value = model.TQCount_Wrong;
            cmdParms[12].Value = model.TQMastery_No;
            cmdParms[13].Value = model.ComplexityText;
            cmdParms[14].Value = model.TotalScore;
            cmdParms[15].Value = model.Score;
            cmdParms[0x10].Value = model.KPMastery;
            cmdParms[0x11].Value = model.CreateTime;
            cmdParms[0x12].Value = model.StatsStuHW_Analysis_KP_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

