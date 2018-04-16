namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsStuHW_Wrong_KP
    {
        public bool Add(Model_StatsStuHW_Wrong_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsStuHW_Wrong_KP(");
            builder.Append("StatsStuHW_Wrong_KP_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,ComplexityText,TestType,topicNumber,Chapter,KPMastery,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsStuHW_Wrong_KP_Id,@HomeWork_Id,@HomeWorkCreateTime,@Student_HomeWork_Id,@Student_Id,@S_KnowledgePoint_Id,@KPNameBasic,@KPImportant,@GKScore,@ComplexityText,@TestType,@topicNumber,@Chapter,@KPMastery,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Wrong_KP_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@KPImportant", SqlDbType.Int, 4), new SqlParameter("@GKScore", SqlDbType.Decimal, 9), new SqlParameter("@ComplexityText", SqlDbType.VarChar, 500), new SqlParameter("@TestType", SqlDbType.VarChar, 500), new SqlParameter("@topicNumber", SqlDbType.VarChar, 500), new SqlParameter("@Chapter", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@KPMastery", SqlDbType.Decimal, 9), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.StatsStuHW_Wrong_KP_Id;
            cmdParms[1].Value = model.HomeWork_Id;
            cmdParms[2].Value = model.HomeWorkCreateTime;
            cmdParms[3].Value = model.Student_HomeWork_Id;
            cmdParms[4].Value = model.Student_Id;
            cmdParms[5].Value = model.S_KnowledgePoint_Id;
            cmdParms[6].Value = model.KPNameBasic;
            cmdParms[7].Value = model.KPImportant;
            cmdParms[8].Value = model.GKScore;
            cmdParms[9].Value = model.ComplexityText;
            cmdParms[10].Value = model.TestType;
            cmdParms[11].Value = model.topicNumber;
            cmdParms[12].Value = model.Chapter;
            cmdParms[13].Value = model.KPMastery;
            cmdParms[14].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsStuHW_Wrong_KP DataRowToModel(DataRow row)
        {
            Model_StatsStuHW_Wrong_KP g_kp = new Model_StatsStuHW_Wrong_KP();
            if (row != null)
            {
                if (row["StatsStuHW_Wrong_KP_Id"] != null)
                {
                    g_kp.StatsStuHW_Wrong_KP_Id = row["StatsStuHW_Wrong_KP_Id"].ToString();
                }
                if (row["HomeWork_Id"] != null)
                {
                    g_kp.HomeWork_Id = row["HomeWork_Id"].ToString();
                }
                if ((row["HomeWorkCreateTime"] != null) && (row["HomeWorkCreateTime"].ToString() != ""))
                {
                    g_kp.HomeWorkCreateTime = new DateTime?(DateTime.Parse(row["HomeWorkCreateTime"].ToString()));
                }
                if (row["Student_HomeWork_Id"] != null)
                {
                    g_kp.Student_HomeWork_Id = row["Student_HomeWork_Id"].ToString();
                }
                if (row["Student_Id"] != null)
                {
                    g_kp.Student_Id = row["Student_Id"].ToString();
                }
                if (row["S_KnowledgePoint_Id"] != null)
                {
                    g_kp.S_KnowledgePoint_Id = row["S_KnowledgePoint_Id"].ToString();
                }
                if (row["KPNameBasic"] != null)
                {
                    g_kp.KPNameBasic = row["KPNameBasic"].ToString();
                }
                if ((row["KPImportant"] != null) && (row["KPImportant"].ToString() != ""))
                {
                    g_kp.KPImportant = new int?(int.Parse(row["KPImportant"].ToString()));
                }
                if ((row["GKScore"] != null) && (row["GKScore"].ToString() != ""))
                {
                    g_kp.GKScore = new decimal?(decimal.Parse(row["GKScore"].ToString()));
                }
                if (row["ComplexityText"] != null)
                {
                    g_kp.ComplexityText = row["ComplexityText"].ToString();
                }
                if (row["TestType"] != null)
                {
                    g_kp.TestType = row["TestType"].ToString();
                }
                if (row["topicNumber"] != null)
                {
                    g_kp.topicNumber = row["topicNumber"].ToString();
                }
                if (row["Chapter"] != null)
                {
                    g_kp.Chapter = row["Chapter"].ToString();
                }
                if ((row["KPMastery"] != null) && (row["KPMastery"].ToString() != ""))
                {
                    g_kp.KPMastery = new decimal?(decimal.Parse(row["KPMastery"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    g_kp.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return g_kp;
        }

        public bool Delete(string StatsStuHW_Wrong_KP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_Wrong_KP ");
            builder.Append(" where StatsStuHW_Wrong_KP_Id=@StatsStuHW_Wrong_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Wrong_KP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Wrong_KP_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsStuHW_Wrong_KP_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_Wrong_KP ");
            builder.Append(" where StatsStuHW_Wrong_KP_Id in (" + StatsStuHW_Wrong_KP_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsStuHW_Wrong_KP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsStuHW_Wrong_KP");
            builder.Append(" where StatsStuHW_Wrong_KP_Id=@StatsStuHW_Wrong_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Wrong_KP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Wrong_KP_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsStuHW_Wrong_KP_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,ComplexityText,TestType,topicNumber,Chapter,KPMastery,CreateTime ");
            builder.Append(" FROM StatsStuHW_Wrong_KP ");
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
            builder.Append(" StatsStuHW_Wrong_KP_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,ComplexityText,TestType,topicNumber,Chapter,KPMastery,CreateTime ");
            builder.Append(" FROM StatsStuHW_Wrong_KP ");
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
                builder.Append("order by T.StatsStuHW_Wrong_KP_Id desc");
            }
            builder.Append(")AS Row, T.*  from StatsStuHW_Wrong_KP T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsStuHW_Wrong_KP GetModel(string StatsStuHW_Wrong_KP_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsStuHW_Wrong_KP_Id,HomeWork_Id,HomeWorkCreateTime,Student_HomeWork_Id,Student_Id,S_KnowledgePoint_Id,KPNameBasic,KPImportant,GKScore,ComplexityText,TestType,topicNumber,Chapter,KPMastery,CreateTime from StatsStuHW_Wrong_KP ");
            builder.Append(" where StatsStuHW_Wrong_KP_Id=@StatsStuHW_Wrong_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Wrong_KP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Wrong_KP_Id;
            new Model_StatsStuHW_Wrong_KP();
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
            builder.Append("select count(1) FROM StatsStuHW_Wrong_KP ");
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

        public bool Update(Model_StatsStuHW_Wrong_KP model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsStuHW_Wrong_KP set ");
            builder.Append("HomeWork_Id=@HomeWork_Id,");
            builder.Append("HomeWorkCreateTime=@HomeWorkCreateTime,");
            builder.Append("Student_HomeWork_Id=@Student_HomeWork_Id,");
            builder.Append("Student_Id=@Student_Id,");
            builder.Append("S_KnowledgePoint_Id=@S_KnowledgePoint_Id,");
            builder.Append("KPNameBasic=@KPNameBasic,");
            builder.Append("KPImportant=@KPImportant,");
            builder.Append("GKScore=@GKScore,");
            builder.Append("ComplexityText=@ComplexityText,");
            builder.Append("TestType=@TestType,");
            builder.Append("topicNumber=@topicNumber,");
            builder.Append("Chapter=@Chapter,");
            builder.Append("KPMastery=@KPMastery,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsStuHW_Wrong_KP_Id=@StatsStuHW_Wrong_KP_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWorkCreateTime", SqlDbType.DateTime), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@KPImportant", SqlDbType.Int, 4), new SqlParameter("@GKScore", SqlDbType.Decimal, 9), new SqlParameter("@ComplexityText", SqlDbType.VarChar, 500), new SqlParameter("@TestType", SqlDbType.VarChar, 500), new SqlParameter("@topicNumber", SqlDbType.VarChar, 500), new SqlParameter("@Chapter", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@KPMastery", SqlDbType.Decimal, 9), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsStuHW_Wrong_KP_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.HomeWork_Id;
            cmdParms[1].Value = model.HomeWorkCreateTime;
            cmdParms[2].Value = model.Student_HomeWork_Id;
            cmdParms[3].Value = model.Student_Id;
            cmdParms[4].Value = model.S_KnowledgePoint_Id;
            cmdParms[5].Value = model.KPNameBasic;
            cmdParms[6].Value = model.KPImportant;
            cmdParms[7].Value = model.GKScore;
            cmdParms[8].Value = model.ComplexityText;
            cmdParms[9].Value = model.TestType;
            cmdParms[10].Value = model.topicNumber;
            cmdParms[11].Value = model.Chapter;
            cmdParms[12].Value = model.KPMastery;
            cmdParms[13].Value = model.CreateTime;
            cmdParms[14].Value = model.StatsStuHW_Wrong_KP_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

