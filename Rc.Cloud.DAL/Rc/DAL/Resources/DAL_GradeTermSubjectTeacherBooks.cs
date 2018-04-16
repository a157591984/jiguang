namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_GradeTermSubjectTeacherBooks
    {
        public bool Add(Model_GradeTermSubjectTeacherBooks model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GradeTermSubjectTeacherBooks(");
            builder.Append("ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear)");
            builder.Append(" values (");
            builder.Append("@ResourceFolder_Id,@ResourceFolder_ParentId,@ResourceFolder_Name,@ResourceFolder_Level,@Resource_Type,@Resource_Class,@Resource_Version,@ResourceFolder_Remark,@ResourceFolder_Order,@ResourceFolder_Owner,@CreateFUser,@CreateTime,@ResourceFolder_isLast,@LessonPlan_Type,@GradeTerm,@Subject,@Book_ID,@ParticularYear)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceFolder_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ResourceFolder_Level", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@ResourceFolder_Order", SqlDbType.Int, 4), new SqlParameter("@ResourceFolder_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ResourceFolder_isLast", SqlDbType.Char, 1), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), 
                new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.ResourceFolder_Id;
            cmdParms[1].Value = model.ResourceFolder_ParentId;
            cmdParms[2].Value = model.ResourceFolder_Name;
            cmdParms[3].Value = model.ResourceFolder_Level;
            cmdParms[4].Value = model.Resource_Type;
            cmdParms[5].Value = model.Resource_Class;
            cmdParms[6].Value = model.Resource_Version;
            cmdParms[7].Value = model.ResourceFolder_Remark;
            cmdParms[8].Value = model.ResourceFolder_Order;
            cmdParms[9].Value = model.ResourceFolder_Owner;
            cmdParms[10].Value = model.CreateFUser;
            cmdParms[11].Value = model.CreateTime;
            cmdParms[12].Value = model.ResourceFolder_isLast;
            cmdParms[13].Value = model.LessonPlan_Type;
            cmdParms[14].Value = model.GradeTerm;
            cmdParms[15].Value = model.Subject;
            cmdParms[0x10].Value = model.Book_ID;
            cmdParms[0x11].Value = model.ParticularYear;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int AddandUpdateMulti(List<Model_GradeTermSubjectTeacherBooks> listAdd, List<Model_GradeTermSubjectTeacherBooks> listUpdate)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (Model_GradeTermSubjectTeacherBooks books in listAdd)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0} ;", num);
                builder.Append("insert into GradeTermSubjectTeacherBooks(");
                builder.Append("ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear)");
                builder.Append(" values (");
                builder.Append("@ResourceFolder_Id,@ResourceFolder_ParentId,@ResourceFolder_Name,@ResourceFolder_Level,@Resource_Type,@Resource_Class,@Resource_Version,@ResourceFolder_Remark,@ResourceFolder_Order,@ResourceFolder_Owner,@CreateFUser,@CreateTime,@ResourceFolder_isLast,@LessonPlan_Type,@GradeTerm,@Subject,@Book_ID,@ParticularYear)");
                SqlParameter[] parameterArray = new SqlParameter[] { 
                    new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceFolder_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ResourceFolder_Level", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@ResourceFolder_Order", SqlDbType.Int, 4), new SqlParameter("@ResourceFolder_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ResourceFolder_isLast", SqlDbType.Char, 1), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), 
                    new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4)
                 };
                parameterArray[0].Value = books.ResourceFolder_Id;
                parameterArray[1].Value = books.ResourceFolder_ParentId;
                parameterArray[2].Value = books.ResourceFolder_Name;
                parameterArray[3].Value = books.ResourceFolder_Level;
                parameterArray[4].Value = books.Resource_Type;
                parameterArray[5].Value = books.Resource_Class;
                parameterArray[6].Value = books.Resource_Version;
                parameterArray[7].Value = books.ResourceFolder_Remark;
                parameterArray[8].Value = books.ResourceFolder_Order;
                parameterArray[9].Value = books.ResourceFolder_Owner;
                parameterArray[10].Value = books.CreateFUser;
                parameterArray[11].Value = books.CreateTime;
                parameterArray[12].Value = books.ResourceFolder_isLast;
                parameterArray[13].Value = books.LessonPlan_Type;
                parameterArray[14].Value = books.GradeTerm;
                parameterArray[15].Value = books.Subject;
                parameterArray[0x10].Value = books.Book_ID;
                parameterArray[0x11].Value = books.ParticularYear;
                dictionary.Add(builder.ToString(), parameterArray);
            }
            foreach (Model_GradeTermSubjectTeacherBooks books2 in listUpdate)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0} ;", num);
                builder.Append("update GradeTermSubjectTeacherBooks set ");
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
                parameterArray2[0].Value = books2.ResourceFolder_ParentId;
                parameterArray2[1].Value = books2.ResourceFolder_Name;
                parameterArray2[2].Value = books2.ResourceFolder_Level;
                parameterArray2[3].Value = books2.Resource_Type;
                parameterArray2[4].Value = books2.Resource_Class;
                parameterArray2[5].Value = books2.Resource_Version;
                parameterArray2[6].Value = books2.ResourceFolder_Remark;
                parameterArray2[7].Value = books2.ResourceFolder_Order;
                parameterArray2[8].Value = books2.ResourceFolder_Owner;
                parameterArray2[9].Value = books2.CreateFUser;
                parameterArray2[10].Value = books2.CreateTime;
                parameterArray2[11].Value = books2.ResourceFolder_isLast;
                parameterArray2[12].Value = books2.LessonPlan_Type;
                parameterArray2[13].Value = books2.GradeTerm;
                parameterArray2[14].Value = books2.Subject;
                parameterArray2[15].Value = books2.Book_ID;
                parameterArray2[0x10].Value = books2.ParticularYear;
                parameterArray2[0x11].Value = books2.ResourceFolder_Id;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            int num2 = DbHelperSQL.ExecuteSqlTran(dictionary);
            if (num2 > 0)
            {
                return num2;
            }
            return 0;
        }

        public Model_GradeTermSubjectTeacherBooks DataRowToModel(DataRow row)
        {
            Model_GradeTermSubjectTeacherBooks books = new Model_GradeTermSubjectTeacherBooks();
            if (row != null)
            {
                if (row["ResourceFolder_Id"] != null)
                {
                    books.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["ResourceFolder_ParentId"] != null)
                {
                    books.ResourceFolder_ParentId = row["ResourceFolder_ParentId"].ToString();
                }
                if (row["ResourceFolder_Name"] != null)
                {
                    books.ResourceFolder_Name = row["ResourceFolder_Name"].ToString();
                }
                if ((row["ResourceFolder_Level"] != null) && (row["ResourceFolder_Level"].ToString() != ""))
                {
                    books.ResourceFolder_Level = new int?(int.Parse(row["ResourceFolder_Level"].ToString()));
                }
                if (row["Resource_Type"] != null)
                {
                    books.Resource_Type = row["Resource_Type"].ToString();
                }
                if (row["Resource_Class"] != null)
                {
                    books.Resource_Class = row["Resource_Class"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    books.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["ResourceFolder_Remark"] != null)
                {
                    books.ResourceFolder_Remark = row["ResourceFolder_Remark"].ToString();
                }
                if ((row["ResourceFolder_Order"] != null) && (row["ResourceFolder_Order"].ToString() != ""))
                {
                    books.ResourceFolder_Order = new int?(int.Parse(row["ResourceFolder_Order"].ToString()));
                }
                if (row["ResourceFolder_Owner"] != null)
                {
                    books.ResourceFolder_Owner = row["ResourceFolder_Owner"].ToString();
                }
                if (row["CreateFUser"] != null)
                {
                    books.CreateFUser = row["CreateFUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    books.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["ResourceFolder_isLast"] != null)
                {
                    books.ResourceFolder_isLast = row["ResourceFolder_isLast"].ToString();
                }
                if (row["LessonPlan_Type"] != null)
                {
                    books.LessonPlan_Type = row["LessonPlan_Type"].ToString();
                }
                if (row["GradeTerm"] != null)
                {
                    books.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Subject"] != null)
                {
                    books.Subject = row["Subject"].ToString();
                }
                if (row["Book_ID"] != null)
                {
                    books.Book_ID = row["Book_ID"].ToString();
                }
                if ((row["ParticularYear"] != null) && (row["ParticularYear"].ToString() != ""))
                {
                    books.ParticularYear = new int?(int.Parse(row["ParticularYear"].ToString()));
                }
            }
            return books;
        }

        public bool Delete(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from GradeTermSubjectTeacherBooks ");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ResourceFolder_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from GradeTermSubjectTeacherBooks ");
            builder.Append(" where ResourceFolder_Id in (" + ResourceFolder_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from GradeTermSubjectTeacherBooks");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear ");
            builder.Append(" FROM GradeTermSubjectTeacherBooks ");
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
            builder.Append(" ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear ");
            builder.Append(" FROM GradeTermSubjectTeacherBooks ");
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
            builder.Append(")AS Row, T.*  from GradeTermSubjectTeacherBooks T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_GradeTermSubjectTeacherBooks GetModel(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear from GradeTermSubjectTeacherBooks ");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            new Model_GradeTermSubjectTeacherBooks();
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
            builder.Append("select count(1) FROM GradeTermSubjectTeacherBooks ");
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

        public bool Update(Model_GradeTermSubjectTeacherBooks model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GradeTermSubjectTeacherBooks set ");
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
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceFolder_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceFolder_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ResourceFolder_Level", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@ResourceFolder_Order", SqlDbType.Int, 4), new SqlParameter("@ResourceFolder_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ResourceFolder_isLast", SqlDbType.Char, 1), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), 
                new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceFolder_ParentId;
            cmdParms[1].Value = model.ResourceFolder_Name;
            cmdParms[2].Value = model.ResourceFolder_Level;
            cmdParms[3].Value = model.Resource_Type;
            cmdParms[4].Value = model.Resource_Class;
            cmdParms[5].Value = model.Resource_Version;
            cmdParms[6].Value = model.ResourceFolder_Remark;
            cmdParms[7].Value = model.ResourceFolder_Order;
            cmdParms[8].Value = model.ResourceFolder_Owner;
            cmdParms[9].Value = model.CreateFUser;
            cmdParms[10].Value = model.CreateTime;
            cmdParms[11].Value = model.ResourceFolder_isLast;
            cmdParms[12].Value = model.LessonPlan_Type;
            cmdParms[13].Value = model.GradeTerm;
            cmdParms[14].Value = model.Subject;
            cmdParms[15].Value = model.Book_ID;
            cmdParms[0x10].Value = model.ParticularYear;
            cmdParms[0x11].Value = model.ResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

