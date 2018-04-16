namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_S_TestingPoint
    {
        public bool Add(Model_S_TestingPoint model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into S_TestingPoint(");
            builder.Append("S_TestingPoint_Id,GradeTerm,Subject,Syllabus,Test_Category,TPLevel,Parent_Id,S_TestingPointBasic_Id,TPName,TPCode,Importance,Cognitive_Level,IsLast,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@S_TestingPoint_Id,@GradeTerm,@Subject,@Syllabus,@Test_Category,@TPLevel,@Parent_Id,@S_TestingPointBasic_Id,@TPName,@TPCode,@Importance,@Cognitive_Level,@IsLast,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@S_TestingPoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Syllabus", SqlDbType.Char, 0x24), new SqlParameter("@Test_Category", SqlDbType.Char, 0x24), new SqlParameter("@TPLevel", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_TestingPointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@TPName", SqlDbType.VarChar, 200), new SqlParameter("@TPCode", SqlDbType.VarChar, 200), new SqlParameter("@Importance", SqlDbType.Char, 0x24), new SqlParameter("@Cognitive_Level", SqlDbType.Char, 0x24), new SqlParameter("@IsLast", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), 
                new SqlParameter("@UpdateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.S_TestingPoint_Id;
            cmdParms[1].Value = model.GradeTerm;
            cmdParms[2].Value = model.Subject;
            cmdParms[3].Value = model.Syllabus;
            cmdParms[4].Value = model.Test_Category;
            cmdParms[5].Value = model.TPLevel;
            cmdParms[6].Value = model.Parent_Id;
            cmdParms[7].Value = model.S_TestingPointBasic_Id;
            cmdParms[8].Value = model.TPName;
            cmdParms[9].Value = model.TPCode;
            cmdParms[10].Value = model.Importance;
            cmdParms[11].Value = model.Cognitive_Level;
            cmdParms[12].Value = model.IsLast;
            cmdParms[13].Value = model.CreateUser;
            cmdParms[14].Value = model.CreateTime;
            cmdParms[15].Value = model.UpdateUser;
            cmdParms[0x10].Value = model.UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool AddBasic(Model_S_TestingPoint model, Model_S_TestingPointBasic modelBasic)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into S_TestingPointBasic(");
            builder.Append("S_TestingPointBasic_Id,GradeTerm,Subject,TPNameBasic,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@S_TestingPointBasic_Id,@GradeTerm,@Subject,@TPNameBasic,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@S_TestingPointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@TPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            parameterArray[0].Value = modelBasic.S_TestingPointBasic_Id;
            parameterArray[1].Value = modelBasic.GradeTerm;
            parameterArray[2].Value = modelBasic.Subject;
            parameterArray[3].Value = modelBasic.TPNameBasic;
            parameterArray[4].Value = modelBasic.CreateUser;
            parameterArray[5].Value = modelBasic.CreateTime;
            parameterArray[6].Value = modelBasic.UpdateUser;
            parameterArray[7].Value = modelBasic.UpdateTime;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("insert into S_TestingPoint(");
            builder.Append("S_TestingPoint_Id,GradeTerm,Subject,Syllabus,Test_Category,TPLevel,Parent_Id,S_TestingPointBasic_Id,TPName,TPCode,Importance,Cognitive_Level,IsLast,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@S_TestingPoint_Id,@GradeTerm,@Subject,@Syllabus,@Test_Category,@TPLevel,@Parent_Id,@S_TestingPointBasic_Id,@TPName,@TPCode,@Importance,@Cognitive_Level,@IsLast,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { 
                new SqlParameter("@S_TestingPoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Syllabus", SqlDbType.Char, 0x24), new SqlParameter("@Test_Category", SqlDbType.Char, 0x24), new SqlParameter("@TPLevel", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_TestingPointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@TPName", SqlDbType.VarChar, 200), new SqlParameter("@TPCode", SqlDbType.VarChar, 200), new SqlParameter("@Importance", SqlDbType.Char, 0x24), new SqlParameter("@Cognitive_Level", SqlDbType.Char, 0x24), new SqlParameter("@IsLast", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), 
                new SqlParameter("@UpdateTime", SqlDbType.DateTime)
             };
            parameterArray2[0].Value = model.S_TestingPoint_Id;
            parameterArray2[1].Value = model.GradeTerm;
            parameterArray2[2].Value = model.Subject;
            parameterArray2[3].Value = model.Syllabus;
            parameterArray2[4].Value = model.Test_Category;
            parameterArray2[5].Value = model.TPLevel;
            parameterArray2[6].Value = model.Parent_Id;
            parameterArray2[7].Value = model.S_TestingPointBasic_Id;
            parameterArray2[8].Value = model.TPName;
            parameterArray2[9].Value = model.TPCode;
            parameterArray2[10].Value = model.Importance;
            parameterArray2[11].Value = model.Cognitive_Level;
            parameterArray2[12].Value = model.IsLast;
            parameterArray2[13].Value = model.CreateUser;
            parameterArray2[14].Value = model.CreateTime;
            parameterArray2[15].Value = model.UpdateUser;
            parameterArray2[0x10].Value = model.UpdateTime;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_S_TestingPoint DataRowToModel(DataRow row)
        {
            Model_S_TestingPoint point = new Model_S_TestingPoint();
            if (row != null)
            {
                if (row["S_TestingPoint_Id"] != null)
                {
                    point.S_TestingPoint_Id = row["S_TestingPoint_Id"].ToString();
                }
                if (row["GradeTerm"] != null)
                {
                    point.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Subject"] != null)
                {
                    point.Subject = row["Subject"].ToString();
                }
                if (row["Syllabus"] != null)
                {
                    point.Syllabus = row["Syllabus"].ToString();
                }
                if (row["Test_Category"] != null)
                {
                    point.Test_Category = row["Test_Category"].ToString();
                }
                if (row["TPLevel"] != null)
                {
                    point.TPLevel = row["TPLevel"].ToString();
                }
                if (row["Parent_Id"] != null)
                {
                    point.Parent_Id = row["Parent_Id"].ToString();
                }
                if (row["S_TestingPointBasic_Id"] != null)
                {
                    point.S_TestingPointBasic_Id = row["S_TestingPointBasic_Id"].ToString();
                }
                if (row["TPName"] != null)
                {
                    point.TPName = row["TPName"].ToString();
                }
                if (row["TPCode"] != null)
                {
                    point.TPCode = row["TPCode"].ToString();
                }
                if (row["Importance"] != null)
                {
                    point.Importance = row["Importance"].ToString();
                }
                if (row["Cognitive_Level"] != null)
                {
                    point.Cognitive_Level = row["Cognitive_Level"].ToString();
                }
                if (row["IsLast"] != null)
                {
                    point.IsLast = row["IsLast"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    point.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    point.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["UpdateUser"] != null)
                {
                    point.UpdateUser = row["UpdateUser"].ToString();
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    point.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
            }
            return point;
        }

        public bool Delete(string S_TestingPoint_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_TestingPoint ");
            builder.Append(" where S_TestingPoint_Id=@S_TestingPoint_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_TestingPoint_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_TestingPoint_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string S_TestingPoint_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_TestingPoint ");
            builder.Append(" where S_TestingPoint_Id in (" + S_TestingPoint_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string S_TestingPoint_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from S_TestingPoint");
            builder.Append(" where S_TestingPoint_Id=@S_TestingPoint_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_TestingPoint_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_TestingPoint_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetDataForEdit(string tpId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT T.* from (select t.*,t2.TPNameBasic from S_TestingPoint t\r\nleft join S_TestingPointBasic t2 on t.S_TestingPointBasic_Id=t2.S_TestingPointBasic_Id\r\n) T ");
            builder.AppendFormat(" WHERE  S_TestingPoint_Id='{0}' ", tpId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select S_TestingPoint_Id,GradeTerm,Subject,Syllabus,Test_Category,TPLevel,Parent_Id,S_TestingPointBasic_Id,TPName,TPCode,Importance,Cognitive_Level,IsLast,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM S_TestingPoint ");
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
            builder.Append(" S_TestingPoint_Id,GradeTerm,Subject,Syllabus,Test_Category,TPLevel,Parent_Id,S_TestingPointBasic_Id,TPName,TPCode,Importance,Cognitive_Level,IsLast,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM S_TestingPoint ");
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
                builder.Append("order by T.S_TestingPoint_Id desc");
            }
            builder.Append(")AS Row, T.*  from S_TestingPoint T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageJoinDict(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.S_TestingPoint_Id desc");
            }
            builder.Append(")AS Row, T.*  from (select t.*,t2.D_Name as TPLevelName,t3.TPNameBasic from S_TestingPoint t\r\nleft join Common_Dict t2 on t2.Common_Dict_ID=t.TPLevel\r\nleft join S_TestingPointBasic t3 on t.S_TestingPointBasic_Id=t3.S_TestingPointBasic_Id\r\n) T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListJoinDict(string strWhere, string orderby)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT T.*  from (select t.*,t2.D_Name as TPLevelName,t3.TPNameBasic from S_TestingPoint t\r\nleft join Common_Dict t2 on t2.Common_Dict_ID=t.TPLevel\r\nleft join S_TestingPointBasic t3 on t.S_TestingPointBasic_Id=t3.S_TestingPointBasic_Id) T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append(" order by " + orderby);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_S_TestingPoint GetModel(string S_TestingPoint_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 S_TestingPoint_Id,GradeTerm,Subject,Syllabus,Test_Category,TPLevel,Parent_Id,S_TestingPointBasic_Id,TPName,TPCode,Importance,Cognitive_Level,IsLast,CreateUser,CreateTime,UpdateUser,UpdateTime from S_TestingPoint ");
            builder.Append(" where S_TestingPoint_Id=@S_TestingPoint_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_TestingPoint_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_TestingPoint_Id;
            new Model_S_TestingPoint();
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
            builder.Append("select count(1) FROM S_TestingPoint ");
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

        public bool Update(Model_S_TestingPoint model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update S_TestingPoint set ");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("Syllabus=@Syllabus,");
            builder.Append("Test_Category=@Test_Category,");
            builder.Append("TPLevel=@TPLevel,");
            builder.Append("Parent_Id=@Parent_Id,");
            builder.Append("S_TestingPointBasic_Id=@S_TestingPointBasic_Id,");
            builder.Append("TPName=@TPName,");
            builder.Append("TPCode=@TPCode,");
            builder.Append("Importance=@Importance,");
            builder.Append("Cognitive_Level=@Cognitive_Level,");
            builder.Append("IsLast=@IsLast,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UpdateUser=@UpdateUser,");
            builder.Append("UpdateTime=@UpdateTime");
            builder.Append(" where S_TestingPoint_Id=@S_TestingPoint_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Syllabus", SqlDbType.Char, 0x24), new SqlParameter("@Test_Category", SqlDbType.Char, 0x24), new SqlParameter("@TPLevel", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_TestingPointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@TPName", SqlDbType.VarChar, 200), new SqlParameter("@TPCode", SqlDbType.VarChar, 200), new SqlParameter("@Importance", SqlDbType.Char, 0x24), new SqlParameter("@Cognitive_Level", SqlDbType.Char, 0x24), new SqlParameter("@IsLast", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), 
                new SqlParameter("@S_TestingPoint_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.GradeTerm;
            cmdParms[1].Value = model.Subject;
            cmdParms[2].Value = model.Syllabus;
            cmdParms[3].Value = model.Test_Category;
            cmdParms[4].Value = model.TPLevel;
            cmdParms[5].Value = model.Parent_Id;
            cmdParms[6].Value = model.S_TestingPointBasic_Id;
            cmdParms[7].Value = model.TPName;
            cmdParms[8].Value = model.TPCode;
            cmdParms[9].Value = model.Importance;
            cmdParms[10].Value = model.Cognitive_Level;
            cmdParms[11].Value = model.IsLast;
            cmdParms[12].Value = model.CreateUser;
            cmdParms[13].Value = model.CreateTime;
            cmdParms[14].Value = model.UpdateUser;
            cmdParms[15].Value = model.UpdateTime;
            cmdParms[0x10].Value = model.S_TestingPoint_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateBasic(Model_S_TestingPoint model, Model_S_TestingPointBasic modelBasic)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into S_TestingPointBasic(");
            builder.Append("S_TestingPointBasic_Id,GradeTerm,Subject,TPNameBasic,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@S_TestingPointBasic_Id,@GradeTerm,@Subject,@TPNameBasic,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@S_TestingPointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@TPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            parameterArray[0].Value = modelBasic.S_TestingPointBasic_Id;
            parameterArray[1].Value = modelBasic.GradeTerm;
            parameterArray[2].Value = modelBasic.Subject;
            parameterArray[3].Value = modelBasic.TPNameBasic;
            parameterArray[4].Value = modelBasic.CreateUser;
            parameterArray[5].Value = modelBasic.CreateTime;
            parameterArray[6].Value = modelBasic.UpdateUser;
            parameterArray[7].Value = modelBasic.UpdateTime;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("update S_TestingPoint set ");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("Syllabus=@Syllabus,");
            builder.Append("Test_Category=@Test_Category,");
            builder.Append("TPLevel=@TPLevel,");
            builder.Append("Parent_Id=@Parent_Id,");
            builder.Append("S_TestingPointBasic_Id=@S_TestingPointBasic_Id,");
            builder.Append("TPName=@TPName,");
            builder.Append("TPCode=@TPCode,");
            builder.Append("Importance=@Importance,");
            builder.Append("Cognitive_Level=@Cognitive_Level,");
            builder.Append("IsLast=@IsLast,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UpdateUser=@UpdateUser,");
            builder.Append("UpdateTime=@UpdateTime");
            builder.Append(" where S_TestingPoint_Id=@S_TestingPoint_Id ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { 
                new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Syllabus", SqlDbType.Char, 0x24), new SqlParameter("@Test_Category", SqlDbType.Char, 0x24), new SqlParameter("@TPLevel", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_TestingPointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@TPName", SqlDbType.VarChar, 200), new SqlParameter("@TPCode", SqlDbType.VarChar, 200), new SqlParameter("@Importance", SqlDbType.Char, 0x24), new SqlParameter("@Cognitive_Level", SqlDbType.Char, 0x24), new SqlParameter("@IsLast", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), 
                new SqlParameter("@S_TestingPoint_Id", SqlDbType.Char, 0x24)
             };
            parameterArray2[0].Value = model.GradeTerm;
            parameterArray2[1].Value = model.Subject;
            parameterArray2[2].Value = model.Syllabus;
            parameterArray2[3].Value = model.Test_Category;
            parameterArray2[4].Value = model.TPLevel;
            parameterArray2[5].Value = model.Parent_Id;
            parameterArray2[6].Value = model.S_TestingPointBasic_Id;
            parameterArray2[7].Value = model.TPName;
            parameterArray2[8].Value = model.TPCode;
            parameterArray2[9].Value = model.Importance;
            parameterArray2[10].Value = model.Cognitive_Level;
            parameterArray2[11].Value = model.IsLast;
            parameterArray2[12].Value = model.CreateUser;
            parameterArray2[13].Value = model.CreateTime;
            parameterArray2[14].Value = model.UpdateUser;
            parameterArray2[15].Value = model.UpdateTime;
            parameterArray2[0x10].Value = model.S_TestingPoint_Id;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }
    }
}

