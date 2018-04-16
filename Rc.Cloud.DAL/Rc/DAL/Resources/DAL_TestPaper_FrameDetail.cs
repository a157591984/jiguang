namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_TestPaper_FrameDetail
    {
        public bool Add(Model_TestPaper_FrameDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TestPaper_FrameDetail(");
            builder.Append("TestPaper_FrameDetail_Id,TestPaper_Frame_Id,ParentId,TestQuestions_Num,TestQuestions_NumStr,TestQuestions_Type,TargetText,ComplexityText,KnowledgePoint,Score,Remark,TestPaper_FrameType,CreateUser,CreateTime,TestQuestionType_Web)");
            builder.Append(" values (");
            builder.Append("@TestPaper_FrameDetail_Id,@TestPaper_Frame_Id,@ParentId,@TestQuestions_Num,@TestQuestions_NumStr,@TestQuestions_Type,@TargetText,@ComplexityText,@KnowledgePoint,@Score,@Remark,@TestPaper_FrameType,@CreateUser,@CreateTime,@TestQuestionType_Web)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetail_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24), new SqlParameter("@ParentId", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_NumStr", SqlDbType.VarChar, 500), new SqlParameter("@TestQuestions_Type", SqlDbType.NVarChar, 50), new SqlParameter("@TargetText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ComplexityText", SqlDbType.NVarChar, 50), new SqlParameter("@KnowledgePoint", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Score", SqlDbType.Decimal, 5), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@TestPaper_FrameType", SqlDbType.NVarChar, 50), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestQuestionType_Web", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.TestPaper_FrameDetail_Id;
            cmdParms[1].Value = model.TestPaper_Frame_Id;
            cmdParms[2].Value = model.ParentId;
            cmdParms[3].Value = model.TestQuestions_Num;
            cmdParms[4].Value = model.TestQuestions_NumStr;
            cmdParms[5].Value = model.TestQuestions_Type;
            cmdParms[6].Value = model.TargetText;
            cmdParms[7].Value = model.ComplexityText;
            cmdParms[8].Value = model.KnowledgePoint;
            cmdParms[9].Value = model.Score;
            cmdParms[10].Value = model.Remark;
            cmdParms[11].Value = model.TestPaper_FrameType;
            cmdParms[12].Value = model.CreateUser;
            cmdParms[13].Value = model.CreateTime;
            cmdParms[14].Value = model.TestQuestionType_Web;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_TestPaper_FrameDetail DataRowToModel(DataRow row)
        {
            Model_TestPaper_FrameDetail detail = new Model_TestPaper_FrameDetail();
            if (row != null)
            {
                if (row["TestPaper_FrameDetail_Id"] != null)
                {
                    detail.TestPaper_FrameDetail_Id = row["TestPaper_FrameDetail_Id"].ToString();
                }
                if (row["TestPaper_Frame_Id"] != null)
                {
                    detail.TestPaper_Frame_Id = row["TestPaper_Frame_Id"].ToString();
                }
                if (row["ParentId"] != null)
                {
                    detail.ParentId = row["ParentId"].ToString();
                }
                if ((row["TestQuestions_Num"] != null) && (row["TestQuestions_Num"].ToString() != ""))
                {
                    detail.TestQuestions_Num = new int?(int.Parse(row["TestQuestions_Num"].ToString()));
                }
                if (row["TestQuestions_NumStr"] != null)
                {
                    detail.TestQuestions_NumStr = row["TestQuestions_NumStr"].ToString();
                }
                if (row["TestQuestions_Type"] != null)
                {
                    detail.TestQuestions_Type = row["TestQuestions_Type"].ToString();
                }
                if (row["TargetText"] != null)
                {
                    detail.TargetText = row["TargetText"].ToString();
                }
                if (row["ComplexityText"] != null)
                {
                    detail.ComplexityText = row["ComplexityText"].ToString();
                }
                if (row["KnowledgePoint"] != null)
                {
                    detail.KnowledgePoint = row["KnowledgePoint"].ToString();
                }
                if ((row["Score"] != null) && (row["Score"].ToString() != ""))
                {
                    detail.Score = new decimal?(decimal.Parse(row["Score"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    detail.Remark = row["Remark"].ToString();
                }
                if (row["TestPaper_FrameType"] != null)
                {
                    detail.TestPaper_FrameType = row["TestPaper_FrameType"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    detail.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    detail.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["TestQuestionType_Web"] != null)
                {
                    detail.TestQuestionType_Web = row["TestQuestionType_Web"].ToString();
                }
            }
            return detail;
        }

        public bool Delete(string TestPaper_FrameDetail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestPaper_FrameDetail ");
            builder.Append(" where TestPaper_FrameDetail_Id=@TestPaper_FrameDetail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameDetail_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TestPaper_FrameDetail_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestPaper_FrameDetail ");
            builder.Append(" where TestPaper_FrameDetail_Id in (" + TestPaper_FrameDetail_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TestPaper_FrameDetail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from TestPaper_FrameDetail");
            builder.Append(" where TestPaper_FrameDetail_Id=@TestPaper_FrameDetail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameDetail_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TestPaper_FrameDetail_Id,TestPaper_Frame_Id,ParentId,TestQuestions_Num,TestQuestions_NumStr,TestQuestions_Type,TargetText,ComplexityText,KnowledgePoint,Score,Remark,TestPaper_FrameType,CreateUser,CreateTime,TestQuestionType_Web ");
            builder.Append(" FROM TestPaper_FrameDetail ");
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
            builder.Append(" TestPaper_FrameDetail_Id,TestPaper_Frame_Id,ParentId,TestQuestions_Num,TestQuestions_NumStr,TestQuestions_Type,TargetText,ComplexityText,KnowledgePoint,Score,Remark,TestPaper_FrameType,CreateUser,CreateTime,TestQuestionType_Web ");
            builder.Append(" FROM TestPaper_FrameDetail ");
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
                builder.Append("order by T.TestPaper_FrameDetail_Id desc");
            }
            builder.Append(")AS Row, T.*  from TestPaper_FrameDetail T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_TestPaper_FrameDetail GetModel(string TestPaper_FrameDetail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TestPaper_FrameDetail_Id,TestPaper_Frame_Id,ParentId,TestQuestions_Num,TestQuestions_NumStr,TestQuestions_Type,TargetText,ComplexityText,KnowledgePoint,Score,Remark,TestPaper_FrameType,CreateUser,CreateTime,TestQuestionType_Web from TestPaper_FrameDetail ");
            builder.Append(" where TestPaper_FrameDetail_Id=@TestPaper_FrameDetail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_FrameDetail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_FrameDetail_Id;
            new Model_TestPaper_FrameDetail();
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
            builder.Append("select count(1) FROM TestPaper_FrameDetail ");
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

        public bool Update(Model_TestPaper_FrameDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TestPaper_FrameDetail set ");
            builder.Append("TestPaper_Frame_Id=@TestPaper_Frame_Id,");
            builder.Append("ParentId=@ParentId,");
            builder.Append("TestQuestions_Num=@TestQuestions_Num,");
            builder.Append("TestQuestions_NumStr=@TestQuestions_NumStr,");
            builder.Append("TestQuestions_Type=@TestQuestions_Type,");
            builder.Append("TargetText=@TargetText,");
            builder.Append("ComplexityText=@ComplexityText,");
            builder.Append("KnowledgePoint=@KnowledgePoint,");
            builder.Append("Score=@Score,");
            builder.Append("Remark=@Remark,");
            builder.Append("TestPaper_FrameType=@TestPaper_FrameType,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("TestQuestionType_Web=@TestQuestionType_Web");
            builder.Append(" where TestPaper_FrameDetail_Id=@TestPaper_FrameDetail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24), new SqlParameter("@ParentId", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_NumStr", SqlDbType.VarChar, 500), new SqlParameter("@TestQuestions_Type", SqlDbType.NVarChar, 50), new SqlParameter("@TargetText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ComplexityText", SqlDbType.NVarChar, 50), new SqlParameter("@KnowledgePoint", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Score", SqlDbType.Decimal, 5), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@TestPaper_FrameType", SqlDbType.NVarChar, 50), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestQuestionType_Web", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_FrameDetail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.TestPaper_Frame_Id;
            cmdParms[1].Value = model.ParentId;
            cmdParms[2].Value = model.TestQuestions_Num;
            cmdParms[3].Value = model.TestQuestions_NumStr;
            cmdParms[4].Value = model.TestQuestions_Type;
            cmdParms[5].Value = model.TargetText;
            cmdParms[6].Value = model.ComplexityText;
            cmdParms[7].Value = model.KnowledgePoint;
            cmdParms[8].Value = model.Score;
            cmdParms[9].Value = model.Remark;
            cmdParms[10].Value = model.TestPaper_FrameType;
            cmdParms[11].Value = model.CreateUser;
            cmdParms[12].Value = model.CreateTime;
            cmdParms[13].Value = model.TestQuestionType_Web;
            cmdParms[14].Value = model.TestPaper_FrameDetail_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

