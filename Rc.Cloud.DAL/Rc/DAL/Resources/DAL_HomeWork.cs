namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_HomeWork
    {
        public bool Add(Model_HomeWork model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HomeWork(");
            builder.Append("HomeWork_Id,ResourceToResourceFolder_Id,HomeWork_Name,HomeWork_AssignTeacher,BeginTime,StopTime,IsHide,HomeWork_Status,HomeWork_FinishTime,CreateTime,UserGroup_Id,CorrectMode,isTimeLimt,isTimeLength,SubjectId,IsShowAnswer,IsCountdown,rtrfId_Old)");
            builder.Append(" values (");
            builder.Append("@HomeWork_Id,@ResourceToResourceFolder_Id,@HomeWork_Name,@HomeWork_AssignTeacher,@BeginTime,@StopTime,@IsHide,@HomeWork_Status,@HomeWork_FinishTime,@CreateTime,@UserGroup_Id,@CorrectMode,@isTimeLimt,@isTimeLength,@SubjectId,@IsShowAnswer,@IsCountdown,@rtrfId_Old)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_AssignTeacher", SqlDbType.Char, 0x24), new SqlParameter("@BeginTime", SqlDbType.DateTime), new SqlParameter("@StopTime", SqlDbType.DateTime), new SqlParameter("@IsHide", SqlDbType.Int, 4), new SqlParameter("@HomeWork_Status", SqlDbType.Int, 4), new SqlParameter("@HomeWork_FinishTime", SqlDbType.DateTime), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@CorrectMode", SqlDbType.Char, 1), new SqlParameter("@isTimeLimt", SqlDbType.Int, 4), new SqlParameter("@isTimeLength", SqlDbType.Int, 4), new SqlParameter("@SubjectId", SqlDbType.Char, 0x24), new SqlParameter("@IsShowAnswer", SqlDbType.Int, 4), 
                new SqlParameter("@IsCountdown", SqlDbType.Char, 1), new SqlParameter("@rtrfId_Old", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.HomeWork_Id;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.HomeWork_Name;
            cmdParms[3].Value = model.HomeWork_AssignTeacher;
            cmdParms[4].Value = model.BeginTime;
            cmdParms[5].Value = model.StopTime;
            cmdParms[6].Value = model.IsHide;
            cmdParms[7].Value = model.HomeWork_Status;
            cmdParms[8].Value = model.HomeWork_FinishTime;
            cmdParms[9].Value = model.CreateTime;
            cmdParms[10].Value = model.UserGroup_Id;
            cmdParms[11].Value = model.CorrectMode;
            cmdParms[12].Value = model.isTimeLimt;
            cmdParms[13].Value = model.isTimeLength;
            cmdParms[14].Value = model.SubjectId;
            cmdParms[15].Value = model.IsShowAnswer;
            cmdParms[0x10].Value = model.IsCountdown;
            cmdParms[0x11].Value = model.rtrfId_Old;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool AddHomework(Model_HomeWork model, List<Model_Student_HomeWork> list, List<Model_Student_HomeWork_Submit> listSubmit, List<Model_Student_HomeWork_Correct> listCorrect, Model_StatsHelper modelSH_HW)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HomeWork(");
            builder.Append("HomeWork_Id,ResourceToResourceFolder_Id,HomeWork_Name,HomeWork_AssignTeacher,BeginTime,StopTime,IsHide,HomeWork_Status,HomeWork_FinishTime,CreateTime,UserGroup_Id,CorrectMode,isTimeLimt,isTimeLength,SubjectId,IsShowAnswer,IsCountdown,rtrfId_Old)");
            builder.Append(" values (");
            builder.Append("@HomeWork_Id,@ResourceToResourceFolder_Id,@HomeWork_Name,@HomeWork_AssignTeacher,@BeginTime,@StopTime,@IsHide,@HomeWork_Status,@HomeWork_FinishTime,@CreateTime,@UserGroup_Id,@CorrectMode,@isTimeLimt,@isTimeLength,@SubjectId,@IsShowAnswer,@IsCountdown,@rtrfId_Old)");
            SqlParameter[] parameterArray = new SqlParameter[] { 
                new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_AssignTeacher", SqlDbType.Char, 0x24), new SqlParameter("@BeginTime", SqlDbType.DateTime), new SqlParameter("@StopTime", SqlDbType.DateTime), new SqlParameter("@IsHide", SqlDbType.Int, 4), new SqlParameter("@HomeWork_Status", SqlDbType.Int, 4), new SqlParameter("@HomeWork_FinishTime", SqlDbType.DateTime), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@CorrectMode", SqlDbType.Char, 1), new SqlParameter("@isTimeLimt", SqlDbType.Int, 4), new SqlParameter("@isTimeLength", SqlDbType.Int, 4), new SqlParameter("@SubjectId", SqlDbType.Char, 0x24), new SqlParameter("@IsShowAnswer", SqlDbType.Int, 4), 
                new SqlParameter("@IsCountdown", SqlDbType.Char, 1), new SqlParameter("@rtrfId_Old", SqlDbType.Char, 0x24)
             };
            parameterArray[0].Value = model.HomeWork_Id;
            parameterArray[1].Value = model.ResourceToResourceFolder_Id;
            parameterArray[2].Value = model.HomeWork_Name;
            parameterArray[3].Value = model.HomeWork_AssignTeacher;
            parameterArray[4].Value = model.BeginTime;
            parameterArray[5].Value = model.StopTime;
            parameterArray[6].Value = model.IsHide;
            parameterArray[7].Value = model.HomeWork_Status;
            parameterArray[8].Value = model.HomeWork_FinishTime;
            parameterArray[9].Value = model.CreateTime;
            parameterArray[10].Value = model.UserGroup_Id;
            parameterArray[11].Value = model.CorrectMode;
            parameterArray[12].Value = model.isTimeLimt;
            parameterArray[13].Value = model.isTimeLength;
            parameterArray[14].Value = model.SubjectId;
            parameterArray[15].Value = model.IsShowAnswer;
            parameterArray[0x10].Value = model.IsCountdown;
            parameterArray[0x11].Value = model.rtrfId_Old;
            dictionary.Add(builder.ToString(), parameterArray);
            int num = 0;
            foreach (Model_Student_HomeWork work in list)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into Student_HomeWork(");
                builder.Append("Student_HomeWork_Id,HomeWork_Id,Student_Id,CreateTime)");
                builder.Append(" values (");
                builder.Append("@Student_HomeWork_Id,@HomeWork_Id,@Student_Id,@CreateTime)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
                parameterArray2[0].Value = work.Student_HomeWork_Id;
                parameterArray2[1].Value = work.HomeWork_Id;
                parameterArray2[2].Value = work.Student_Id;
                parameterArray2[3].Value = work.CreateTime;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            foreach (Model_Student_HomeWork_Submit submit in listSubmit)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into Student_HomeWork_Submit(");
                builder.Append("Student_HomeWork_Id,Student_HomeWork_Status,OpenTime,StudentIP,Student_Answer_Time)");
                builder.Append(" values (");
                builder.Append("@Student_HomeWork_Id,@Student_HomeWork_Status,@OpenTime,@StudentIP,@Student_Answer_Time)");
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("Student_HomeWork_Status", SqlDbType.Int, 4), new SqlParameter("OpenTime", SqlDbType.DateTime), new SqlParameter("StudentIP", SqlDbType.Char, 0x24), new SqlParameter("Student_Answer_Time", SqlDbType.DateTime) };
                parameterArray3[0].Value = submit.Student_HomeWork_Id;
                parameterArray3[1].Value = submit.Student_HomeWork_Status;
                parameterArray3[2].Value = submit.OpenTime;
                parameterArray3[3].Value = submit.StudentIP;
                parameterArray3[4].Value = submit.Student_Answer_Time;
                dictionary.Add(builder.ToString(), parameterArray3);
            }
            foreach (Model_Student_HomeWork_Correct correct in listCorrect)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into Student_HomeWork_Correct(");
                builder.Append("Student_HomeWork_Id,Student_HomeWork_CorrectStatus,CorrectTime,CorrectMode)");
                builder.Append(" values (");
                builder.Append(" @Student_HomeWork_Id, @Student_HomeWork_CorrectStatus, @CorrectTime, @CorrectMode)");
                SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("Student_HomeWork_CorrectStatus", SqlDbType.Int, 4), new SqlParameter("CorrectTime", SqlDbType.DateTime), new SqlParameter("CorrectMode", SqlDbType.Char, 1) };
                parameterArray4[0].Value = correct.Student_HomeWork_Id;
                parameterArray4[1].Value = correct.Student_HomeWork_CorrectStatus;
                parameterArray4[2].Value = correct.CorrectTime;
                parameterArray4[3].Value = correct.CorrectMode;
                dictionary.Add(builder.ToString(), parameterArray4);
            }
            builder = new StringBuilder();
            builder.Append("insert into StatsHelper(");
            builder.Append("StatsHelper_Id,ResourceToResourceFolder_Id,Homework_Id,Correct_Time,Exec_Status,SType,CreateUser,Exec_Time,SchoolId,GradeId,ClassId,TeacherId,HW_CreateTime)");
            builder.AppendFormat(" values ('{0}','{1}','{2}',null,'{3}','{4}','{5}',null,'{6}','{7}','{8}','{9}','{10}');", new object[] { Guid.NewGuid().ToString(), modelSH_HW.ResourceToResourceFolder_Id, modelSH_HW.Homework_Id, modelSH_HW.Exec_Status, modelSH_HW.SType, modelSH_HW.CreateUser, modelSH_HW.SchoolId, modelSH_HW.GradeId, modelSH_HW.ClassId, modelSH_HW.TeacherId, modelSH_HW.HW_CreateTime });
            builder.AppendFormat("\r\n            merge into StatsHelper sl\r\n            using (select '{1}' as DataId,'2' as DataType,'{5}' as GradeId) tt\r\n            on sl.ResourceToResourceFolder_Id=tt.DataId and sl.SType=tt.DataType and  sl.GradeId=tt.GradeId \r\n            when matched then update set sl.HW_CreateTime='{6}',sl.CreateUser='{3}'\r\n            when not matched then\r\n            insert values('{0}','{1}',null,null,'{2}','2','{3}',null,'{4}','{5}',null,null,'{6}');", new object[] { Guid.NewGuid().ToString(), modelSH_HW.ResourceToResourceFolder_Id, modelSH_HW.Exec_Status, modelSH_HW.CreateUser, modelSH_HW.SchoolId, modelSH_HW.GradeId, modelSH_HW.HW_CreateTime });
            builder.Append("insert into StatsHelper(");
            builder.Append("StatsHelper_Id,ResourceToResourceFolder_Id,Homework_Id,Correct_Time,Exec_Status,SType,CreateUser,Exec_Time,SchoolId,GradeId,ClassId,TeacherId,HW_CreateTime)");
            builder.AppendFormat(" values ('{0}','{1}','{2}',null,'{3}','3','{4}',null,'{5}','{6}','{7}','{8}','{9}');", new object[] { Guid.NewGuid().ToString(), modelSH_HW.ResourceToResourceFolder_Id, modelSH_HW.Homework_Id, modelSH_HW.Exec_Status, modelSH_HW.CreateUser, modelSH_HW.SchoolId, modelSH_HW.GradeId, modelSH_HW.ClassId, modelSH_HW.TeacherId, modelSH_HW.HW_CreateTime });
            dictionary.Add(builder.ToString(), null);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_HomeWork DataRowToModel(DataRow row)
        {
            Model_HomeWork work = new Model_HomeWork();
            if (row != null)
            {
                if (row["HomeWork_Id"] != null)
                {
                    work.HomeWork_Id = row["HomeWork_Id"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    work.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["HomeWork_Name"] != null)
                {
                    work.HomeWork_Name = row["HomeWork_Name"].ToString();
                }
                if (row["HomeWork_AssignTeacher"] != null)
                {
                    work.HomeWork_AssignTeacher = row["HomeWork_AssignTeacher"].ToString();
                }
                if ((row["BeginTime"] != null) && (row["BeginTime"].ToString() != ""))
                {
                    work.BeginTime = new DateTime?(DateTime.Parse(row["BeginTime"].ToString()));
                }
                if ((row["StopTime"] != null) && (row["StopTime"].ToString() != ""))
                {
                    work.StopTime = new DateTime?(DateTime.Parse(row["StopTime"].ToString()));
                }
                if ((row["IsHide"] != null) && (row["IsHide"].ToString() != ""))
                {
                    work.IsHide = new int?(int.Parse(row["IsHide"].ToString()));
                }
                if ((row["HomeWork_Status"] != null) && (row["HomeWork_Status"].ToString() != ""))
                {
                    work.HomeWork_Status = new int?(int.Parse(row["HomeWork_Status"].ToString()));
                }
                if ((row["HomeWork_FinishTime"] != null) && (row["HomeWork_FinishTime"].ToString() != ""))
                {
                    work.HomeWork_FinishTime = new DateTime?(DateTime.Parse(row["HomeWork_FinishTime"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    work.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["UserGroup_Id"] != null)
                {
                    work.UserGroup_Id = row["UserGroup_Id"].ToString();
                }
                if (row["CorrectMode"] != null)
                {
                    work.CorrectMode = row["CorrectMode"].ToString();
                }
                if ((row["isTimeLimt"] != null) && (row["isTimeLimt"].ToString() != ""))
                {
                    work.isTimeLimt = new int?(int.Parse(row["isTimeLimt"].ToString()));
                }
                if ((row["isTimeLength"] != null) && (row["isTimeLength"].ToString() != ""))
                {
                    work.isTimeLength = new int?(int.Parse(row["isTimeLength"].ToString()));
                }
                if (row["SubjectId"] != null)
                {
                    work.SubjectId = row["SubjectId"].ToString();
                }
                if ((row["IsShowAnswer"] != null) && (row["IsShowAnswer"].ToString() != ""))
                {
                    work.IsShowAnswer = new int?(int.Parse(row["IsShowAnswer"].ToString()));
                }
                if (row["IsCountdown"] != null)
                {
                    work.IsCountdown = row["IsCountdown"].ToString();
                }
                if (row["rtrfId_Old"] != null)
                {
                    work.rtrfId_Old = row["rtrfId_Old"].ToString();
                }
            }
            return work;
        }

        public bool Delete(string HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from HomeWork ");
            builder.Append(" where HomeWork_Id=@HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HomeWork_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string HomeWork_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from HomeWork ");
            builder.Append(" where HomeWork_Id in (" + HomeWork_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from HomeWork");
            builder.Append(" where HomeWork_Id=@HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HomeWork_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetHWDetail(string HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select hw.ResourceToResourceFolder_Id,hw.HomeWork_AssignTeacher as TeacherId,hw.UserGroup_Id as ClassId,t.GradeId,t.SchoolId,hw.HomeWork_Name from HomeWork hw\r\ninner join VW_ClassGradeSchool t on t.ClassId=hw.UserGroup_Id and t.GradeId is not null and t.SchoolId is not null ");
            builder.AppendFormat(" WHERE hw.HomeWork_Id='{0}' ", HomeWork_Id);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetHWInofBySHWId(string shwId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT hw.ResourceToResourceFolder_Id,SHW.Student_Id,SHW.HomeWork_Id,SHW.Student_HomeWork_Id,shwCorrect.CorrectMode FROM HomeWork HW\r\nINNER JOIN Student_HomeWork SHW ON HW.HomeWork_Id=SHW.HomeWork_Id\r\ninner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=SHW.Student_HomeWork_Id \r\nWHERE SHW.Student_HomeWork_Id='{0}';", shwId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select HomeWork_Id,ResourceToResourceFolder_Id,HomeWork_Name,HomeWork_AssignTeacher,BeginTime,StopTime,IsHide,HomeWork_Status,HomeWork_FinishTime,CreateTime,UserGroup_Id,CorrectMode,isTimeLimt,isTimeLength,SubjectId,IsShowAnswer,IsCountdown,rtrfId_Old ");
            builder.Append(" FROM HomeWork ");
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
            builder.Append(" HomeWork_Id,ResourceToResourceFolder_Id,HomeWork_Name,HomeWork_AssignTeacher,BeginTime,StopTime,IsHide,HomeWork_Status,HomeWork_FinishTime,CreateTime,UserGroup_Id,CorrectMode,isTimeLimt,isTimeLength,SubjectId,IsShowAnswer,IsCountdown,rtrfId_Old ");
            builder.Append(" FROM HomeWork ");
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
                builder.Append("order by T.HomeWork_Id desc");
            }
            builder.Append(")AS Row, T.*  from HomeWork T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListForStatisticsByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by " + orderby);
            }
            else
            {
                builder.Append("order by HomeWork_Id desc");
            }
            builder.Append(")AS Row, T.*  from (select hw.HomeWork_Id,hw.ResourceToResourceFolder_Id,hw.HomeWork_Name,hw.HomeWork_AssignTeacher,hw.HomeWork_Status,hw.CreateTime,hw.UserGroup_Id\r\n,HomeworkCount=0\r\n,StudentCount=(select COUNT(1) from Student_HomeWork shw where shw.HomeWork_Id=hw.HomeWork_Id )\r\n,SubmitCount=(select COUNT(1) from Student_HomeWork shw inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwSubmit.Student_HomeWork_Status=1 where shw.HomeWork_Id=hw.HomeWork_Id   )\r\nfrom dbo.HomeWork hw left join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id ) T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetListForStatisticsRecordcount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT count(1) FROM ( ");
            builder.Append(" select hw.HomeWork_Id,hw.ResourceToResourceFolder_Id,hw.HomeWork_Name,hw.HomeWork_AssignTeacher,hw.HomeWork_Status,hw.CreateTime,hw.UserGroup_Id\r\nfrom dbo.HomeWork hw left join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id ) T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetListForTeacherView(string strWhere, string orderby)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT *,StudentCount=(select COUNT(1) from Student_HomeWork where HomeWork_Id=HomeWork.HomeWork_Id),dbo.f_GetStudentNamesByHomeworkId(HomeWork_Id) as StudentNames from HomeWork ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by " + orderby);
            }
            else
            {
                builder.Append("order by CreateTime desc");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_HomeWork GetModel(string HomeWork_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 HomeWork_Id,ResourceToResourceFolder_Id,HomeWork_Name,HomeWork_AssignTeacher,BeginTime,StopTime,IsHide,HomeWork_Status,HomeWork_FinishTime,CreateTime,UserGroup_Id,CorrectMode,isTimeLimt,isTimeLength,SubjectId,IsShowAnswer,IsCountdown,rtrfId_Old from HomeWork ");
            builder.Append(" where HomeWork_Id=@HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HomeWork_Id;
            new Model_HomeWork();
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
            builder.Append("select count(1) FROM HomeWork ");
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

        public int GetRecordCount_Operate(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM HomeWork ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL_Operate.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetTchHWListByPage_APP(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by " + orderby);
            }
            else
            {
                builder.Append("order by HomeWork_Id desc");
            }
            builder.Append(")AS Row, T.*  from (select hw.*,t2.UserGroup_Name as ClassName from HomeWork hw\r\ninner join UserGroup t2 on t2.UserGroup_Id=hw.UserGroup_Id ) T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool RevokeHW(string HomeWorkId)
        {
            try
            {
                return (DbHelperSQL.ExecuteSqlByTime(string.Format("\r\nupdate StatsHelper set Exec_Time=null where ResourceToResourceFolder_Id in(select ResourceToResourceFolder_Id from HomeWork where HomeWork_Id='{0}' );\r\ndelete from Student_HomeWork_Correct where Student_HomeWork_Id in(select Student_HomeWork_Id from dbo.Student_HomeWork where HomeWork_Id='{0}');\r\ndelete from Student_HomeWork_Submit where Student_HomeWork_Id in(select Student_HomeWork_Id from dbo.Student_HomeWork where HomeWork_Id='{0}');\r\ndelete from Student_WrongHomeWork where Student_HomeWorkAnswer_Id in(select Student_HomeWorkAnswer_Id from dbo.Student_HomeWorkAnswer where HomeWork_Id='{0}');\r\ndelete from Student_HomeWorkAnswer where HomeWork_Id='{0}';\r\ndelete from Student_HomeWork_Submit where Student_HomeWork_Id in(select Student_HomeWork_Id from Student_HomeWork where HomeWork_Id='{0}' );\r\ndelete from Student_HomeWork_Correct where Student_HomeWork_Id in(select Student_HomeWork_Id from Student_HomeWork where HomeWork_Id='{0}' );\r\ndelete from Student_HomeWork where HomeWork_Id='{0}';\r\ndelete from HomeWork where  HomeWork_Id='{0}';\r\ndelete from StatsClassHW_KP where HomeWork_Id='{0}';\r\ndelete from StatsClassHW_Score where HomeWork_Id='{0}';\r\ndelete from StatsClassHW_ScoreLevel where HomeWork_Id='{0}';\r\ndelete from StatsClassHW_Subsection where HomeWork_Id='{0}';\r\ndelete from StatsClassHW_TQ where HomeWork_Id='{0}';\r\ndelete from StatsClassStudentHW_Score where HomeWork_Id='{0}';\r\ndelete from StatsHelper where HomeWork_Id='{0}';\r\ndelete from HomeworkToTQ where HomeWork_Id='{0}';\r\ndelete from Student_Mutual_CorrectSub where HomeWork_Id='{0}';\r\ndelete from Student_Mutual_Correct where HomeWork_Id='{0}';\r\ndelete from StatsStuHW_KP where HomeWork_Id='{0}';\r\ndelete from StatsStuHW_TQ_KP where HomeWork_Id='{0}';\r\ndelete from StatsStuHW_Wrong_KP where HomeWork_Id='{0}';\r\ndelete from StatsStuHW_Wrong_TQ where HomeWork_Id='{0}';\r\n", HomeWorkId).ToString(), 0x1c20) > 0);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Model_PagerInfo<List<Model_HomeWork>> SearhList(string Where, string Sort, int pageIndex, int pageSize)
        {
            new StringBuilder();
            string strWhere = " 1=1 " + Where;
            int recordCount = this.GetRecordCount(strWhere);
            int startIndex = ((pageIndex - 1) * pageSize) + 1;
            int endIndex = pageIndex * pageSize;
            DataTable table = this.GetListByPage(strWhere, Sort, startIndex, endIndex).Tables[0];
            List<Model_HomeWork> list = new List<Model_HomeWork>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(this.DataRowToModel(row));
            }
            return new Model_PagerInfo<List<Model_HomeWork>> { PageSize = pageSize, CurrentPage = pageIndex, RecordCount = recordCount, PageData = list };
        }

        public bool TchAssignHW_APP(List<Model_HomeWork> listHW, List<Model_Student_HomeWork> listStuHW, List<Model_Student_HomeWork_Submit> listSubmit, List<Model_Student_HomeWork_Correct> listCorrect, List<Model_StatsHelper> listSH)
        {
            StringBuilder builder = new StringBuilder();
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            int num = 0;
            foreach (Model_HomeWork work in listHW)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into HomeWork(");
                builder.Append("HomeWork_Id,ResourceToResourceFolder_Id,HomeWork_Name,HomeWork_AssignTeacher,BeginTime,StopTime,IsHide,HomeWork_Status,HomeWork_FinishTime,CreateTime,UserGroup_Id,CorrectMode,isTimeLimt,isTimeLength,SubjectId,IsShowAnswer,IsCountdown,rtrfId_Old)");
                builder.Append(" values (");
                builder.Append("@HomeWork_Id,@ResourceToResourceFolder_Id,@HomeWork_Name,@HomeWork_AssignTeacher,@BeginTime,@StopTime,@IsHide,@HomeWork_Status,@HomeWork_FinishTime,@CreateTime,@UserGroup_Id,@CorrectMode,@isTimeLimt,@isTimeLength,@SubjectId,@IsShowAnswer,@IsCountdown,@rtrfId_Old)");
                SqlParameter[] parameterArray = new SqlParameter[] { 
                    new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_AssignTeacher", SqlDbType.Char, 0x24), new SqlParameter("@BeginTime", SqlDbType.DateTime), new SqlParameter("@StopTime", SqlDbType.DateTime), new SqlParameter("@IsHide", SqlDbType.Int, 4), new SqlParameter("@HomeWork_Status", SqlDbType.Int, 4), new SqlParameter("@HomeWork_FinishTime", SqlDbType.DateTime), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@CorrectMode", SqlDbType.Char, 1), new SqlParameter("@isTimeLimt", SqlDbType.Int, 4), new SqlParameter("@isTimeLength", SqlDbType.Int, 4), new SqlParameter("@SubjectId", SqlDbType.Char, 0x24), new SqlParameter("@IsShowAnswer", SqlDbType.Int, 4), 
                    new SqlParameter("@IsCountdown", SqlDbType.Char, 1), new SqlParameter("@rtrfId_Old", SqlDbType.Char, 0x24)
                 };
                parameterArray[0].Value = work.HomeWork_Id;
                parameterArray[1].Value = work.ResourceToResourceFolder_Id;
                parameterArray[2].Value = work.HomeWork_Name;
                parameterArray[3].Value = work.HomeWork_AssignTeacher;
                parameterArray[4].Value = work.BeginTime;
                parameterArray[5].Value = work.StopTime;
                parameterArray[6].Value = work.IsHide;
                parameterArray[7].Value = work.HomeWork_Status;
                parameterArray[8].Value = work.HomeWork_FinishTime;
                parameterArray[9].Value = work.CreateTime;
                parameterArray[10].Value = work.UserGroup_Id;
                parameterArray[11].Value = work.CorrectMode;
                parameterArray[12].Value = work.isTimeLimt;
                parameterArray[13].Value = work.isTimeLength;
                parameterArray[14].Value = work.SubjectId;
                parameterArray[15].Value = work.IsShowAnswer;
                parameterArray[0x10].Value = work.IsCountdown;
                parameterArray[0x11].Value = work.rtrfId_Old;
                dictionary.Add(builder.ToString(), parameterArray);
            }
            foreach (Model_Student_HomeWork work2 in listStuHW)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into Student_HomeWork(");
                builder.Append("Student_HomeWork_Id,HomeWork_Id,Student_Id,CreateTime)");
                builder.Append(" values (");
                builder.Append("@Student_HomeWork_Id,@HomeWork_Id,@Student_Id,@CreateTime)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
                parameterArray2[0].Value = work2.Student_HomeWork_Id;
                parameterArray2[1].Value = work2.HomeWork_Id;
                parameterArray2[2].Value = work2.Student_Id;
                parameterArray2[3].Value = work2.CreateTime;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            foreach (Model_Student_HomeWork_Submit submit in listSubmit)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into Student_HomeWork_Submit(");
                builder.Append("Student_HomeWork_Id,Student_HomeWork_Status,OpenTime,StudentIP,Student_Answer_Time)");
                builder.Append(" values (");
                builder.Append("@Student_HomeWork_Id,@Student_HomeWork_Status,@OpenTime,@StudentIP,@Student_Answer_Time)");
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("Student_HomeWork_Status", SqlDbType.Int, 4), new SqlParameter("OpenTime", SqlDbType.DateTime), new SqlParameter("StudentIP", SqlDbType.Char, 0x24), new SqlParameter("Student_Answer_Time", SqlDbType.DateTime) };
                parameterArray3[0].Value = submit.Student_HomeWork_Id;
                parameterArray3[1].Value = submit.Student_HomeWork_Status;
                parameterArray3[2].Value = submit.OpenTime;
                parameterArray3[3].Value = submit.StudentIP;
                parameterArray3[4].Value = submit.Student_Answer_Time;
                dictionary.Add(builder.ToString(), parameterArray3);
            }
            foreach (Model_Student_HomeWork_Correct correct in listCorrect)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into Student_HomeWork_Correct(");
                builder.Append("Student_HomeWork_Id,Student_HomeWork_CorrectStatus,CorrectTime,CorrectMode)");
                builder.Append(" values (");
                builder.Append(" @Student_HomeWork_Id, @Student_HomeWork_CorrectStatus, @CorrectTime, @CorrectMode)");
                SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("Student_HomeWork_CorrectStatus", SqlDbType.Int, 4), new SqlParameter("CorrectTime", SqlDbType.DateTime), new SqlParameter("CorrectMode", SqlDbType.Char, 1) };
                parameterArray4[0].Value = correct.Student_HomeWork_Id;
                parameterArray4[1].Value = correct.Student_HomeWork_CorrectStatus;
                parameterArray4[2].Value = correct.CorrectTime;
                parameterArray4[3].Value = correct.CorrectMode;
                dictionary.Add(builder.ToString(), parameterArray4);
            }
            foreach (Model_StatsHelper helper in listSH)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into StatsHelper(");
                builder.Append("StatsHelper_Id,ResourceToResourceFolder_Id,Homework_Id,Correct_Time,Exec_Status,SType,CreateUser,Exec_Time,SchoolId,GradeId,ClassId,TeacherId,HW_CreateTime)");
                builder.AppendFormat(" values ('{0}','{1}','{2}',null,'{3}','{4}','{5}',null,'{6}','{7}','{8}','{9}','{10}');", new object[] { Guid.NewGuid().ToString(), helper.ResourceToResourceFolder_Id, helper.Homework_Id, helper.Exec_Status, helper.SType, helper.CreateUser, helper.SchoolId, helper.GradeId, helper.ClassId, helper.TeacherId, helper.HW_CreateTime });
                builder.AppendFormat("\r\n            merge into StatsHelper sl\r\n            using (select '{1}' as DataId,'2' as DataType,'{5}' as GradeId) tt\r\n            on sl.ResourceToResourceFolder_Id=tt.DataId and sl.SType=tt.DataType and  sl.GradeId=tt.GradeId \r\n            when matched then update set sl.HW_CreateTime='{6}',sl.CreateUser='{3}'\r\n            when not matched then\r\n            insert values('{0}','{1}',null,null,'{2}','2','{3}',null,'{4}','{5}',null,null,'{6}');", new object[] { Guid.NewGuid().ToString(), helper.ResourceToResourceFolder_Id, helper.Exec_Status, helper.CreateUser, helper.SchoolId, helper.GradeId, helper.HW_CreateTime });
                builder.Append("insert into StatsHelper(");
                builder.Append("StatsHelper_Id,ResourceToResourceFolder_Id,Homework_Id,Correct_Time,Exec_Status,SType,CreateUser,Exec_Time,SchoolId,GradeId,ClassId,TeacherId,HW_CreateTime)");
                builder.AppendFormat(" values ('{0}','{1}','{2}',null,'{3}','3','{4}',null,'{5}','{6}','{7}','{8}','{9}');", new object[] { Guid.NewGuid().ToString(), helper.ResourceToResourceFolder_Id, helper.Homework_Id, helper.Exec_Status, helper.CreateUser, helper.SchoolId, helper.GradeId, helper.ClassId, helper.TeacherId, helper.HW_CreateTime });
                dictionary.Add(builder.ToString(), null);
            }
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public bool Update(Model_HomeWork model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HomeWork set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("HomeWork_Name=@HomeWork_Name,");
            builder.Append("HomeWork_AssignTeacher=@HomeWork_AssignTeacher,");
            builder.Append("BeginTime=@BeginTime,");
            builder.Append("StopTime=@StopTime,");
            builder.Append("IsHide=@IsHide,");
            builder.Append("HomeWork_Status=@HomeWork_Status,");
            builder.Append("HomeWork_FinishTime=@HomeWork_FinishTime,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UserGroup_Id=@UserGroup_Id,");
            builder.Append("CorrectMode=@CorrectMode,");
            builder.Append("isTimeLimt=@isTimeLimt,");
            builder.Append("isTimeLength=@isTimeLength,");
            builder.Append("SubjectId=@SubjectId,");
            builder.Append("IsShowAnswer=@IsShowAnswer,");
            builder.Append("IsCountdown=@IsCountdown,");
            builder.Append("rtrfId_Old=@rtrfId_Old");
            builder.Append(" where HomeWork_Id=@HomeWork_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_AssignTeacher", SqlDbType.Char, 0x24), new SqlParameter("@BeginTime", SqlDbType.DateTime), new SqlParameter("@StopTime", SqlDbType.DateTime), new SqlParameter("@IsHide", SqlDbType.Int, 4), new SqlParameter("@HomeWork_Status", SqlDbType.Int, 4), new SqlParameter("@HomeWork_FinishTime", SqlDbType.DateTime), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@CorrectMode", SqlDbType.Char, 1), new SqlParameter("@isTimeLimt", SqlDbType.Int, 4), new SqlParameter("@isTimeLength", SqlDbType.Int, 4), new SqlParameter("@SubjectId", SqlDbType.Char, 0x24), new SqlParameter("@IsShowAnswer", SqlDbType.Int, 4), new SqlParameter("@IsCountdown", SqlDbType.Char, 1), 
                new SqlParameter("@rtrfId_Old", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.HomeWork_Name;
            cmdParms[2].Value = model.HomeWork_AssignTeacher;
            cmdParms[3].Value = model.BeginTime;
            cmdParms[4].Value = model.StopTime;
            cmdParms[5].Value = model.IsHide;
            cmdParms[6].Value = model.HomeWork_Status;
            cmdParms[7].Value = model.HomeWork_FinishTime;
            cmdParms[8].Value = model.CreateTime;
            cmdParms[9].Value = model.UserGroup_Id;
            cmdParms[10].Value = model.CorrectMode;
            cmdParms[11].Value = model.isTimeLimt;
            cmdParms[12].Value = model.isTimeLength;
            cmdParms[13].Value = model.SubjectId;
            cmdParms[14].Value = model.IsShowAnswer;
            cmdParms[15].Value = model.IsCountdown;
            cmdParms[0x10].Value = model.rtrfId_Old;
            cmdParms[0x11].Value = model.HomeWork_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateHomework(Model_HomeWork model, List<Model_Student_HomeWork> list, List<Model_Student_HomeWork_Submit> listSubmit, List<Model_Student_HomeWork_Correct> listCorrect, Model_StatsHelper modelSH_HW)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder.Append("update HomeWork set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("HomeWork_Name=@HomeWork_Name,");
            builder.Append("HomeWork_AssignTeacher=@HomeWork_AssignTeacher,");
            builder.Append("BeginTime=@BeginTime,");
            builder.Append("StopTime=@StopTime,");
            builder.Append("IsHide=@IsHide,");
            builder.Append("HomeWork_Status=@HomeWork_Status,");
            builder.Append("HomeWork_FinishTime=@HomeWork_FinishTime,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UserGroup_Id=@UserGroup_Id,");
            builder.Append("CorrectMode=@CorrectMode,");
            builder.Append("isTimeLimt=@isTimeLimt,");
            builder.Append("isTimeLength=@isTimeLength,");
            builder.Append("SubjectId=@SubjectId,");
            builder.Append("IsShowAnswer=@IsShowAnswer,");
            builder.Append("IsCountdown=@IsCountdown,");
            builder.Append("rtrfId_Old=@rtrfId_Old");
            builder.Append(" where HomeWork_Id=@HomeWork_Id ");
            SqlParameter[] parameterArray = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Name", SqlDbType.NVarChar, 250), new SqlParameter("@HomeWork_AssignTeacher", SqlDbType.Char, 0x24), new SqlParameter("@BeginTime", SqlDbType.DateTime), new SqlParameter("@StopTime", SqlDbType.DateTime), new SqlParameter("@IsHide", SqlDbType.Int, 4), new SqlParameter("@HomeWork_Status", SqlDbType.Int, 4), new SqlParameter("@HomeWork_FinishTime", SqlDbType.DateTime), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@CorrectMode", SqlDbType.Char, 1), new SqlParameter("@isTimeLimt", SqlDbType.Int, 4), new SqlParameter("@isTimeLength", SqlDbType.Int, 4), new SqlParameter("@SubjectId", SqlDbType.Char, 0x24), new SqlParameter("@IsShowAnswer", SqlDbType.Int, 4), new SqlParameter("@IsCountdown", SqlDbType.Char, 1), 
                new SqlParameter("@rtrfId_Old", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24)
             };
            parameterArray[0].Value = model.ResourceToResourceFolder_Id;
            parameterArray[1].Value = model.HomeWork_Name;
            parameterArray[2].Value = model.HomeWork_AssignTeacher;
            parameterArray[3].Value = model.BeginTime;
            parameterArray[4].Value = model.StopTime;
            parameterArray[5].Value = model.IsHide;
            parameterArray[6].Value = model.HomeWork_Status;
            parameterArray[7].Value = model.HomeWork_FinishTime;
            parameterArray[8].Value = model.CreateTime;
            parameterArray[9].Value = model.UserGroup_Id;
            parameterArray[10].Value = model.CorrectMode;
            parameterArray[11].Value = model.isTimeLimt;
            parameterArray[12].Value = model.isTimeLength;
            parameterArray[13].Value = model.SubjectId;
            parameterArray[14].Value = model.IsShowAnswer;
            parameterArray[15].Value = model.IsCountdown;
            parameterArray[0x10].Value = model.rtrfId_Old;
            parameterArray[0x11].Value = model.HomeWork_Id;
            dictionary.Add(builder.ToString(), parameterArray);
            int num = 0;
            foreach (Model_Student_HomeWork work in list)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into Student_HomeWork(");
                builder.Append("Student_HomeWork_Id,HomeWork_Id,Student_Id,CreateTime)");
                builder.Append(" values (");
                builder.Append("@Student_HomeWork_Id,@HomeWork_Id,@Student_Id,@CreateTime)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
                parameterArray2[0].Value = work.Student_HomeWork_Id;
                parameterArray2[1].Value = work.HomeWork_Id;
                parameterArray2[2].Value = work.Student_Id;
                parameterArray2[3].Value = work.CreateTime;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            foreach (Model_Student_HomeWork_Submit submit in listSubmit)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into Student_HomeWork_Submit(");
                builder.Append("Student_HomeWork_Id,Student_HomeWork_Status,OpenTime,StudentIP,Student_Answer_Time)");
                builder.Append(" values (");
                builder.Append("@Student_HomeWork_Id,@Student_HomeWork_Status,@OpenTime,@StudentIP,@Student_Answer_Time)");
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("Student_HomeWork_Status", SqlDbType.Int, 4), new SqlParameter("OpenTime", SqlDbType.DateTime), new SqlParameter("StudentIP", SqlDbType.Char, 0x24), new SqlParameter("Student_Answer_Time", SqlDbType.DateTime) };
                parameterArray3[0].Value = submit.Student_HomeWork_Id;
                parameterArray3[1].Value = submit.Student_HomeWork_Status;
                parameterArray3[2].Value = submit.OpenTime;
                parameterArray3[3].Value = submit.StudentIP;
                parameterArray3[4].Value = submit.Student_Answer_Time;
                dictionary.Add(builder.ToString(), parameterArray3);
            }
            foreach (Model_Student_HomeWork_Correct correct in listCorrect)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into Student_HomeWork_Correct(");
                builder.Append("Student_HomeWork_Id,Student_HomeWork_CorrectStatus,CorrectTime,CorrectMode)");
                builder.Append(" values (");
                builder.Append(" @Student_HomeWork_Id, @Student_HomeWork_CorrectStatus, @CorrectTime, @CorrectMode)");
                SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("Student_HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("Student_HomeWork_CorrectStatus", SqlDbType.Int, 4), new SqlParameter("CorrectTime", SqlDbType.DateTime), new SqlParameter("CorrectMode", SqlDbType.Char, 1) };
                parameterArray4[0].Value = correct.Student_HomeWork_Id;
                parameterArray4[1].Value = correct.Student_HomeWork_CorrectStatus;
                parameterArray4[2].Value = correct.CorrectTime;
                parameterArray4[3].Value = correct.CorrectMode;
                dictionary.Add(builder.ToString(), parameterArray4);
            }
            builder = new StringBuilder();
            builder.AppendFormat("update StatsHelper set Exec_Time=(GETDATE()-1000) where (SType='1' or SType='3') and HomeWork_Id='{0}'; ", modelSH_HW.Homework_Id);
            builder.AppendFormat("update StatsHelper set Exec_Time=(GETDATE()-1000) where SType='2' and ResourceToResourceFolder_Id='{0}' and GradeId='{1}' ; ", modelSH_HW.ResourceToResourceFolder_Id, modelSH_HW.GradeId);
            dictionary.Add(builder.ToString(), null);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }
    }
}

