namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Student_HomeWorkAnswer
    {
        public bool Add(Model_Student_HomeWorkAnswer model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Student_HomeWorkAnswer(");
            builder.Append("Student_HomeWorkAnswer_Id,Student_HomeWork_Id,TestQuestions_Score_ID,TestQuestions_Id,Student_Id,HomeWork_Id,TestQuestions_Num,TestQuestions_Detail_OrderNum,Student_Answer,Student_Score,Student_Answer_Status,CreateTime,TestQuestions_NumStr,Comment,isRead)");
            builder.Append(" values (");
            builder.Append("@Student_HomeWorkAnswer_Id,@Student_HomeWork_Id,@TestQuestions_Score_ID,@TestQuestions_Id,@Student_Id,@HomeWork_Id,@TestQuestions_Num,@TestQuestions_Detail_OrderNum,@Student_Answer,@Student_Score,@Student_Answer_Status,@CreateTime,@TestQuestions_NumStr,@Comment,@isRead)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWorkAnswer_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Detail_OrderNum", SqlDbType.Int, 4), new SqlParameter("@Student_Answer", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Student_Score", SqlDbType.Decimal, 5), new SqlParameter("@Student_Answer_Status", SqlDbType.VarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestQuestions_NumStr", SqlDbType.VarChar, 100), new SqlParameter("@Comment", SqlDbType.NVarChar, 500), new SqlParameter("@isRead", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Student_HomeWorkAnswer_Id;
            cmdParms[1].Value = model.Student_HomeWork_Id;
            cmdParms[2].Value = model.TestQuestions_Score_ID;
            cmdParms[3].Value = model.TestQuestions_Id;
            cmdParms[4].Value = model.Student_Id;
            cmdParms[5].Value = model.HomeWork_Id;
            cmdParms[6].Value = model.TestQuestions_Num;
            cmdParms[7].Value = model.TestQuestions_Detail_OrderNum;
            cmdParms[8].Value = model.Student_Answer;
            cmdParms[9].Value = model.Student_Score;
            cmdParms[10].Value = model.Student_Answer_Status;
            cmdParms[11].Value = model.CreateTime;
            cmdParms[12].Value = model.TestQuestions_NumStr;
            cmdParms[13].Value = model.Comment;
            cmdParms[14].Value = model.isRead;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Student_HomeWorkAnswer DataRowToModel(DataRow row)
        {
            Model_Student_HomeWorkAnswer answer = new Model_Student_HomeWorkAnswer();
            if (row != null)
            {
                if (row["Student_HomeWorkAnswer_Id"] != null)
                {
                    answer.Student_HomeWorkAnswer_Id = row["Student_HomeWorkAnswer_Id"].ToString();
                }
                if (row["Student_HomeWork_Id"] != null)
                {
                    answer.Student_HomeWork_Id = row["Student_HomeWork_Id"].ToString();
                }
                if (row["TestQuestions_Score_ID"] != null)
                {
                    answer.TestQuestions_Score_ID = row["TestQuestions_Score_ID"].ToString();
                }
                if (row["TestQuestions_Id"] != null)
                {
                    answer.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if (row["Student_Id"] != null)
                {
                    answer.Student_Id = row["Student_Id"].ToString();
                }
                if (row["HomeWork_Id"] != null)
                {
                    answer.HomeWork_Id = row["HomeWork_Id"].ToString();
                }
                if ((row["TestQuestions_Num"] != null) && (row["TestQuestions_Num"].ToString() != ""))
                {
                    answer.TestQuestions_Num = new int?(int.Parse(row["TestQuestions_Num"].ToString()));
                }
                if ((row["TestQuestions_Detail_OrderNum"] != null) && (row["TestQuestions_Detail_OrderNum"].ToString() != ""))
                {
                    answer.TestQuestions_Detail_OrderNum = new int?(int.Parse(row["TestQuestions_Detail_OrderNum"].ToString()));
                }
                if (row["Student_Answer"] != null)
                {
                    answer.Student_Answer = row["Student_Answer"].ToString();
                }
                if ((row["Student_Score"] != null) && (row["Student_Score"].ToString() != ""))
                {
                    answer.Student_Score = new decimal?(decimal.Parse(row["Student_Score"].ToString()));
                }
                if (row["Student_Answer_Status"] != null)
                {
                    answer.Student_Answer_Status = row["Student_Answer_Status"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    answer.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["TestQuestions_NumStr"] != null)
                {
                    answer.TestQuestions_NumStr = row["TestQuestions_NumStr"].ToString();
                }
                if (row["Comment"] != null)
                {
                    answer.Comment = row["Comment"].ToString();
                }
                if ((row["isRead"] != null) && (row["isRead"].ToString() != ""))
                {
                    answer.isRead = new int?(int.Parse(row["isRead"].ToString()));
                }
            }
            return answer;
        }

        public bool Delete(string Student_HomeWorkAnswer_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_HomeWorkAnswer ");
            builder.Append(" where Student_HomeWorkAnswer_Id=@Student_HomeWorkAnswer_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWorkAnswer_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWorkAnswer_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Student_HomeWorkAnswer_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Student_HomeWorkAnswer ");
            builder.Append(" where Student_HomeWorkAnswer_Id in (" + Student_HomeWorkAnswer_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Student_HomeWorkAnswer_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Student_HomeWorkAnswer");
            builder.Append(" where Student_HomeWorkAnswer_Id=@Student_HomeWorkAnswer_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWorkAnswer_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWorkAnswer_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Student_HomeWorkAnswer_Id,Student_HomeWork_Id,TestQuestions_Score_ID,TestQuestions_Id,Student_Id,HomeWork_Id,TestQuestions_Num,TestQuestions_Detail_OrderNum,Student_Answer,Student_Score,Student_Answer_Status,CreateTime,TestQuestions_NumStr,Comment,isRead ");
            builder.Append(" FROM Student_HomeWorkAnswer ");
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
            builder.Append(" Student_HomeWorkAnswer_Id,Student_HomeWork_Id,TestQuestions_Score_ID,TestQuestions_Id,Student_Id,HomeWork_Id,TestQuestions_Num,TestQuestions_Detail_OrderNum,Student_Answer,Student_Score,Student_Answer_Status,CreateTime,TestQuestions_NumStr,Comment,isRead ");
            builder.Append(" FROM Student_HomeWorkAnswer ");
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
                builder.Append("order by T.Student_HomeWorkAnswer_Id desc");
            }
            builder.Append(")AS Row, T.*  from Student_HomeWorkAnswer T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Student_HomeWorkAnswer GetModel(string Student_HomeWorkAnswer_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Student_HomeWorkAnswer_Id,Student_HomeWork_Id,TestQuestions_Score_ID,TestQuestions_Id,Student_Id,HomeWork_Id,TestQuestions_Num,TestQuestions_Detail_OrderNum,Student_Answer,Student_Score,Student_Answer_Status,CreateTime,TestQuestions_NumStr,Comment,isRead from Student_HomeWorkAnswer ");
            builder.Append(" where Student_HomeWorkAnswer_Id=@Student_HomeWorkAnswer_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWorkAnswer_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Student_HomeWorkAnswer_Id;
            new Model_Student_HomeWorkAnswer();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_Student_HomeWorkAnswer GetModelBySHWIdAndNum(string Student_HomeWork_Id, string TestQuestions_Id, string TestQuestions_Detail_OrderNum)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Student_HomeWorkAnswer_Id,TestQuestions_Id,Student_HomeWork_Id,Student_Id,HomeWork_Id,TestQuestions_Num,TestQuestions_Detail_OrderNum,Student_Answer,Student_Score,Student_Answer_Status,CreateTime,TestQuestions_NumStr,Comment,TestQuestions_Score_ID from Student_HomeWorkAnswer ");
            builder.Append(" where Student_HomeWork_Id=@Student_HomeWork_Id and TestQuestions_Id=@TestQuestions_Id and TestQuestions_Detail_OrderNum=@TestQuestions_Detail_OrderNum ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Detail_OrderNum", SqlDbType.Int, 4) };
            cmdParms[0].Value = Student_HomeWork_Id;
            cmdParms[1].Value = TestQuestions_Id;
            cmdParms[2].Value = TestQuestions_Detail_OrderNum;
            new Model_Student_HomeWorkAnswer();
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
            builder.Append("select count(1) FROM Student_HomeWorkAnswer ");
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

        public int StudentAnswerSubmit(Model_Student_HomeWork_Submit modelSHWSubmit, List<Model_Student_HomeWorkAnswer> listModel)
        {
            new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder.Append("update Student_HomeWork_Submit set ");
            builder.AppendFormat("Student_HomeWork_Status='{0}'", modelSHWSubmit.Student_HomeWork_Status);
            builder.AppendFormat(" where Student_HomeWork_Id='{0}'; ", modelSHWSubmit.Student_HomeWork_Id);
            foreach (Model_Student_HomeWorkAnswer answer in listModel)
            {
                builder.Append("insert into Student_HomeWorkAnswer(");
                builder.Append("Student_HomeWorkAnswer_Id,Student_HomeWork_Id,TestQuestions_Score_ID,TestQuestions_Id,Student_Id,HomeWork_Id,TestQuestions_Num,TestQuestions_Detail_OrderNum,Student_Answer,Student_Score,Student_Answer_Status,CreateTime,TestQuestions_NumStr,Comment,isRead)");
                builder.AppendFormat(" values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}');", new object[] { answer.Student_HomeWorkAnswer_Id, answer.Student_HomeWork_Id, answer.TestQuestions_Score_ID, answer.TestQuestions_Id, answer.Student_Id, answer.HomeWork_Id, answer.TestQuestions_Num, answer.TestQuestions_Detail_OrderNum, answer.Student_Answer, answer.Student_Score, answer.Student_Answer_Status, answer.CreateTime, answer.TestQuestions_NumStr, answer.Comment, answer.isRead });
            }
            builder.AppendFormat("update t set t.TestQuestions_NumStr=tq.topicNumber\r\nfrom Student_HomeWorkAnswer t,TestQuestions tq \r\nwhere t.TestQuestions_NumStr='' and t.Student_HomeWork_Id='{0}' and t.TestQuestions_Id=tq.TestQuestions_Id;", modelSHWSubmit.Student_HomeWork_Id);
            builder.AppendFormat("if(\r\nselect count(*) from Student_HomeWorkAnswer where Student_HomeWork_Id='{0}' and isRead=0)=0 begin update Student_HomeWork_Correct set Student_HomeWork_CorrectStatus='1',CorrectTime=getdate() where Student_HomeWork_Id='{0}';insert into Student_WrongHomeWork\r\nselect NEWID(),t.Student_HomeWorkAnswer_Id,getdate() from Student_HomeWorkAnswer t inner join TestQuestions_Score tq on tq.TestQuestions_Score_ID=t.TestQuestions_Score_ID\r\nwhere tq.TestQuestions_Score!=-1 and t.Student_Answer_Status<>'right' and t.Student_HomeWork_Id='{0}'; end; ", modelSHWSubmit.Student_HomeWork_Id);
            int num = DbHelperSQL.ExecuteSql(builder.ToString());
            if (num > 0)
            {
                return num;
            }
            return 0;
        }

        public int TeacherCorrectStuHomeWork(Model_Student_HomeWork_Correct modelSHWCorrect, List<Model_Student_HomeWorkAnswer> listModel, Model_StatsHelper modelSH_HW)
        {
            new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("update Student_HomeWork_Correct set ");
            builder.AppendFormat("Student_HomeWork_CorrectStatus='{0}'", modelSHWCorrect.Student_HomeWork_CorrectStatus);
            builder.AppendFormat(",CorrectTime='{0}'", modelSHWCorrect.CorrectTime.ToString());
            builder.AppendFormat(",CorrectMode='{0}'", modelSHWCorrect.CorrectMode.ToString());
            builder.AppendFormat(",CorrectUser='{0}'", modelSHWCorrect.CorrectUser.ToString());
            builder.AppendFormat(" where Student_HomeWork_Id='{0}';", modelSHWCorrect.Student_HomeWork_Id);
            foreach (Model_Student_HomeWorkAnswer answer in listModel)
            {
                builder.Append("update Student_HomeWorkAnswer set ");
                builder.AppendFormat("Student_Score='{0}',", answer.Student_Score);
                builder.AppendFormat("Student_Answer_Status='{0}',", answer.Student_Answer_Status);
                builder.AppendFormat("Comment='{0}',", answer.Comment);
                builder.AppendFormat("isRead='{0}'", answer.isRead);
                builder.AppendFormat(" where Student_HomeWorkAnswer_Id='{0}'; ", answer.Student_HomeWorkAnswer_Id);
            }
            builder.AppendFormat("delete from Student_WrongHomeWork where Student_HomeWorkAnswer_Id in(select Student_HomeWorkAnswer_Id from Student_HomeWorkAnswer where Student_HomeWork_Id='{0}'); ", modelSHWCorrect.Student_HomeWork_Id);
            builder.AppendFormat("insert into Student_WrongHomeWork\r\nselect NEWID(),t.Student_HomeWorkAnswer_Id,getdate() from Student_HomeWorkAnswer t inner join TestQuestions_Score tq on tq.TestQuestions_Score_ID=t.TestQuestions_Score_ID\r\nwhere tq.TestQuestions_Score!=-1 and t.Student_Answer_Status<>'right' and t.Student_HomeWork_Id='{0}'; ", modelSHWCorrect.Student_HomeWork_Id);
            builder.AppendFormat("update StatsHelper set Correct_Time='{0}',Exec_Status='0' where (SType='1' or SType='3') and Homework_Id='{1}' ;", modelSH_HW.Correct_Time, modelSH_HW.Homework_Id);
            builder.AppendFormat("update StatsHelper set Correct_Time='{0}',Exec_Status='0' where SType='2' and ResourceToResourceFolder_Id='{1}' and GradeId='{2}' ;", modelSH_HW.Correct_Time, modelSH_HW.ResourceToResourceFolder_Id, modelSH_HW.GradeId);
            int num = DbHelperSQL.ExecuteSql(builder.ToString());
            if (num > 0)
            {
                return num;
            }
            return 0;
        }

        public bool Update(Model_Student_HomeWorkAnswer model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Student_HomeWorkAnswer set ");
            builder.Append("Student_HomeWork_Id=@Student_HomeWork_Id,");
            builder.Append("TestQuestions_Score_ID=@TestQuestions_Score_ID,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("Student_Id=@Student_Id,");
            builder.Append("HomeWork_Id=@HomeWork_Id,");
            builder.Append("TestQuestions_Num=@TestQuestions_Num,");
            builder.Append("TestQuestions_Detail_OrderNum=@TestQuestions_Detail_OrderNum,");
            builder.Append("Student_Answer=@Student_Answer,");
            builder.Append("Student_Score=@Student_Score,");
            builder.Append("Student_Answer_Status=@Student_Answer_Status,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("TestQuestions_NumStr=@TestQuestions_NumStr,");
            builder.Append("Comment=@Comment,");
            builder.Append("isRead=@isRead");
            builder.Append(" where Student_HomeWorkAnswer_Id=@Student_HomeWorkAnswer_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Detail_OrderNum", SqlDbType.Int, 4), new SqlParameter("@Student_Answer", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Student_Score", SqlDbType.Decimal, 5), new SqlParameter("@Student_Answer_Status", SqlDbType.VarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestQuestions_NumStr", SqlDbType.VarChar, 100), new SqlParameter("@Comment", SqlDbType.NVarChar, 500), new SqlParameter("@isRead", SqlDbType.Int, 4), new SqlParameter("@Student_HomeWorkAnswer_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Student_HomeWork_Id;
            cmdParms[1].Value = model.TestQuestions_Score_ID;
            cmdParms[2].Value = model.TestQuestions_Id;
            cmdParms[3].Value = model.Student_Id;
            cmdParms[4].Value = model.HomeWork_Id;
            cmdParms[5].Value = model.TestQuestions_Num;
            cmdParms[6].Value = model.TestQuestions_Detail_OrderNum;
            cmdParms[7].Value = model.Student_Answer;
            cmdParms[8].Value = model.Student_Score;
            cmdParms[9].Value = model.Student_Answer_Status;
            cmdParms[10].Value = model.CreateTime;
            cmdParms[11].Value = model.TestQuestions_NumStr;
            cmdParms[12].Value = model.Comment;
            cmdParms[13].Value = model.isRead;
            cmdParms[14].Value = model.Student_HomeWorkAnswer_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

