namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_PrpeLesson
    {
        public bool Add(Model_PrpeLesson model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PrpeLesson(");
            builder.Append("ResourceFolder_Id,Grade,Subject,Stage,StartTime,EndTime,Require,NameRule,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@ResourceFolder_Id,@Grade,@Subject,@Stage,@StartTime,@EndTime,@Require,@NameRule,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Grade", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Stage", SqlDbType.VarChar, 100), new SqlParameter("@StartTime", SqlDbType.DateTime), new SqlParameter("@EndTime", SqlDbType.DateTime), new SqlParameter("@Require", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@NameRule", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Remark", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.ResourceFolder_Id;
            cmdParms[1].Value = model.Grade;
            cmdParms[2].Value = model.Subject;
            cmdParms[3].Value = model.Stage;
            cmdParms[4].Value = model.StartTime;
            cmdParms[5].Value = model.EndTime;
            cmdParms[6].Value = model.Require;
            cmdParms[7].Value = model.NameRule;
            cmdParms[8].Value = model.Remark;
            cmdParms[9].Value = model.CreateUser;
            cmdParms[10].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Add(Model_PrpeLesson model, Model_PrpeLesson_Person prpeLesson_Person, Model_ResourceFolder rfmodel)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into PrpeLesson(");
            builder.Append("ResourceFolder_Id,Grade,Subject,Stage,StartTime,EndTime,Require,NameRule,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@ResourceFolder_Id,@Grade,@Subject,@Stage,@StartTime,@EndTime,@Require,@NameRule,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Grade", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Stage", SqlDbType.VarChar, 100), new SqlParameter("@StartTime", SqlDbType.DateTime), new SqlParameter("@EndTime", SqlDbType.DateTime), new SqlParameter("@Require", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@NameRule", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Remark", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            parameterArray[0].Value = model.ResourceFolder_Id;
            parameterArray[1].Value = model.Grade;
            parameterArray[2].Value = model.Subject;
            parameterArray[3].Value = model.Stage;
            parameterArray[4].Value = model.StartTime;
            parameterArray[5].Value = model.EndTime;
            parameterArray[6].Value = model.Require;
            parameterArray[7].Value = model.NameRule;
            parameterArray[8].Value = model.Remark;
            parameterArray[9].Value = model.CreateUser;
            parameterArray[10].Value = model.CreateTime;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("insert into PrpeLesson_Person(");
            builder.Append("PrpeLesson_Person_Id,ResourceFolder_Id,ChargePerson,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@PrpeLesson_Person_Id,@ResourceFolder_Id,@ChargePerson,@CreateTime,@CreateUser)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@PrpeLesson_Person_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ChargePerson", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            parameterArray2[0].Value = prpeLesson_Person.PrpeLesson_Person_Id;
            parameterArray2[1].Value = prpeLesson_Person.ResourceFolder_Id;
            parameterArray2[2].Value = prpeLesson_Person.ChargePerson;
            parameterArray2[3].Value = prpeLesson_Person.CreateTime;
            parameterArray2[4].Value = prpeLesson_Person.CreateUser;
            dictionary.Add(builder.ToString(), parameterArray2);
            builder = new StringBuilder();
            builder.Append("insert into ResourceFolder(");
            builder.Append("ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear)");
            builder.Append(" values (");
            builder.Append("@ResourceFolder_Id,@ResourceFolder_ParentId,@ResourceFolder_Name,@ResourceFolder_Level,@Resource_Type,@Resource_Class,@Resource_Version,@ResourceFolder_Remark,@ResourceFolder_Order,@ResourceFolder_Owner,@CreateFUser,@CreateTime,@ResourceFolder_isLast,@LessonPlan_Type,@GradeTerm,@Subject,@Book_ID,@ParticularYear)");
            SqlParameter[] parameterArray3 = new SqlParameter[] { 
                new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceFolder_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ResourceFolder_Level", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@ResourceFolder_Order", SqlDbType.Int, 4), new SqlParameter("@ResourceFolder_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ResourceFolder_isLast", SqlDbType.Char, 1), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), 
                new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4)
             };
            parameterArray3[0].Value = rfmodel.ResourceFolder_Id;
            parameterArray3[1].Value = rfmodel.ResourceFolder_ParentId;
            parameterArray3[2].Value = rfmodel.ResourceFolder_Name;
            parameterArray3[3].Value = rfmodel.ResourceFolder_Level;
            parameterArray3[4].Value = rfmodel.Resource_Type;
            parameterArray3[5].Value = rfmodel.Resource_Class;
            parameterArray3[6].Value = rfmodel.Resource_Version;
            parameterArray3[7].Value = rfmodel.ResourceFolder_Remark;
            parameterArray3[8].Value = rfmodel.ResourceFolder_Order;
            parameterArray3[9].Value = rfmodel.ResourceFolder_Owner;
            parameterArray3[10].Value = rfmodel.CreateFUser;
            parameterArray3[11].Value = rfmodel.CreateTime;
            parameterArray3[12].Value = rfmodel.ResourceFolder_isLast;
            parameterArray3[13].Value = rfmodel.LessonPlan_Type;
            parameterArray3[14].Value = rfmodel.GradeTerm;
            parameterArray3[15].Value = rfmodel.Subject;
            parameterArray3[0x10].Value = rfmodel.Book_ID;
            parameterArray3[0x11].Value = rfmodel.ParticularYear;
            dictionary.Add(builder.ToString(), parameterArray3);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_PrpeLesson DataRowToModel(DataRow row)
        {
            Model_PrpeLesson lesson = new Model_PrpeLesson();
            if (row != null)
            {
                if (row["ResourceFolder_Id"] != null)
                {
                    lesson.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["Grade"] != null)
                {
                    lesson.Grade = row["Grade"].ToString();
                }
                if (row["Subject"] != null)
                {
                    lesson.Subject = row["Subject"].ToString();
                }
                if (row["Stage"] != null)
                {
                    lesson.Stage = row["Stage"].ToString();
                }
                if ((row["StartTime"] != null) && (row["StartTime"].ToString() != ""))
                {
                    lesson.StartTime = new DateTime?(DateTime.Parse(row["StartTime"].ToString()));
                }
                if ((row["EndTime"] != null) && (row["EndTime"].ToString() != ""))
                {
                    lesson.EndTime = new DateTime?(DateTime.Parse(row["EndTime"].ToString()));
                }
                if (row["Require"] != null)
                {
                    lesson.Require = row["Require"].ToString();
                }
                if (row["NameRule"] != null)
                {
                    lesson.NameRule = row["NameRule"].ToString();
                }
                if (row["Remark"] != null)
                {
                    lesson.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    lesson.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    lesson.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return lesson;
        }

        public bool Delete(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PrpeLesson ");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ResourceFolder_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PrpeLesson ");
            builder.Append(" where ResourceFolder_Id in (" + ResourceFolder_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from PrpeLesson");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceFolder_Id,Grade,Subject,Stage,StartTime,EndTime,Require,NameRule,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM PrpeLesson ");
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
            builder.Append(" ResourceFolder_Id,Grade,Subject,Stage,StartTime,EndTime,Require,NameRule,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM PrpeLesson ");
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
                builder.Append("order by T.ResourceFolder_Id desc");
            }
            builder.Append(")AS Row, T.*  from PrpeLesson T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_PrpeLesson GetModel(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ResourceFolder_Id,Grade,Subject,Stage,StartTime,EndTime,Require,NameRule,Remark,CreateUser,CreateTime from PrpeLesson ");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            new Model_PrpeLesson();
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
            builder.Append("select count(1) FROM PrpeLesson ");
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

        public bool Update(Model_PrpeLesson model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PrpeLesson set ");
            builder.Append("Grade=@Grade,");
            builder.Append("Subject=@Subject,");
            builder.Append("Stage=@Stage,");
            builder.Append("StartTime=@StartTime,");
            builder.Append("EndTime=@EndTime,");
            builder.Append("Require=@Require,");
            builder.Append("NameRule=@NameRule,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Grade", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Stage", SqlDbType.VarChar, 100), new SqlParameter("@StartTime", SqlDbType.DateTime), new SqlParameter("@EndTime", SqlDbType.DateTime), new SqlParameter("@Require", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@NameRule", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Remark", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Grade;
            cmdParms[1].Value = model.Subject;
            cmdParms[2].Value = model.Stage;
            cmdParms[3].Value = model.StartTime;
            cmdParms[4].Value = model.EndTime;
            cmdParms[5].Value = model.Require;
            cmdParms[6].Value = model.NameRule;
            cmdParms[7].Value = model.Remark;
            cmdParms[8].Value = model.CreateUser;
            cmdParms[9].Value = model.CreateTime;
            cmdParms[10].Value = model.ResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(Model_PrpeLesson model, Model_ResourceFolder rfmodel)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("update PrpeLesson set ");
            builder.Append("Grade=@Grade,");
            builder.Append("Subject=@Subject,");
            builder.Append("Stage=@Stage,");
            builder.Append("StartTime=@StartTime,");
            builder.Append("EndTime=@EndTime,");
            builder.Append("Require=@Require,");
            builder.Append("NameRule=@NameRule,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@Grade", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Stage", SqlDbType.VarChar, 100), new SqlParameter("@StartTime", SqlDbType.DateTime), new SqlParameter("@EndTime", SqlDbType.DateTime), new SqlParameter("@Require", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@NameRule", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Remark", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            parameterArray[0].Value = model.Grade;
            parameterArray[1].Value = model.Subject;
            parameterArray[2].Value = model.Stage;
            parameterArray[3].Value = model.StartTime;
            parameterArray[4].Value = model.EndTime;
            parameterArray[5].Value = model.Require;
            parameterArray[6].Value = model.NameRule;
            parameterArray[7].Value = model.Remark;
            parameterArray[8].Value = model.CreateUser;
            parameterArray[9].Value = model.CreateTime;
            parameterArray[10].Value = model.ResourceFolder_Id;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("update ResourceFolder set ");
            builder.Append("ResourceFolder_ParentId=@ResourceFolder_ParentId,");
            builder.Append("ResourceFolder_Name=@ResourceFolder_Name,");
            builder.Append("ResourceFolder_Level=@ResourceFolder_Level,");
            builder.Append("Resource_Type=@Resource_Type,");
            builder.Append("Resource_Class=@Resource_Class,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("ResourceFolder_Remark=@ResourceFolder_Remark,");
            builder.Append("ResourceFolder_Order=@ResourceFolder_Order,");
            builder.Append("ResourceFolder_Owner=@ResourceFolder_Owner,");
            builder.Append("CreateFUser=@CreateFUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("ResourceFolder_isLast=@ResourceFolder_isLast,");
            builder.Append("LessonPlan_Type=@LessonPlan_Type,");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("Book_ID=@Book_ID,");
            builder.Append("ParticularYear=@ParticularYear");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { 
                new SqlParameter("@ResourceFolder_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceFolder_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ResourceFolder_Level", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@ResourceFolder_Order", SqlDbType.Int, 4), new SqlParameter("@ResourceFolder_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ResourceFolder_isLast", SqlDbType.Char, 1), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), 
                new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24)
             };
            parameterArray2[0].Value = rfmodel.ResourceFolder_ParentId;
            parameterArray2[1].Value = rfmodel.ResourceFolder_Name;
            parameterArray2[2].Value = rfmodel.ResourceFolder_Level;
            parameterArray2[3].Value = rfmodel.Resource_Type;
            parameterArray2[4].Value = rfmodel.Resource_Class;
            parameterArray2[5].Value = rfmodel.Resource_Version;
            parameterArray2[6].Value = rfmodel.ResourceFolder_Remark;
            parameterArray2[7].Value = rfmodel.ResourceFolder_Order;
            parameterArray2[8].Value = rfmodel.ResourceFolder_Owner;
            parameterArray2[9].Value = rfmodel.CreateFUser;
            parameterArray2[10].Value = rfmodel.CreateTime;
            parameterArray2[11].Value = rfmodel.ResourceFolder_isLast;
            parameterArray2[12].Value = rfmodel.LessonPlan_Type;
            parameterArray2[13].Value = rfmodel.GradeTerm;
            parameterArray2[14].Value = rfmodel.Subject;
            parameterArray2[15].Value = rfmodel.Book_ID;
            parameterArray2[0x10].Value = rfmodel.ParticularYear;
            parameterArray2[0x11].Value = rfmodel.ResourceFolder_Id;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }
    }
}

