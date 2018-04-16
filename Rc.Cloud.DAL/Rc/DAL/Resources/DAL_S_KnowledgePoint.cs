namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_S_KnowledgePoint
    {
        public bool Add(Model_S_KnowledgePoint model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into S_KnowledgePoint(");
            builder.Append("S_KnowledgePoint_Id,GradeTerm,Subject,Resource_Version,Book_Type,KPLevel,Parent_Id,S_KnowledgePointBasic_Id,KPName,KPCode,Cognitive_Level,IsLast,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@S_KnowledgePoint_Id,@GradeTerm,@Subject,@Resource_Version,@Book_Type,@KPLevel,@Parent_Id,@S_KnowledgePointBasic_Id,@KPName,@KPCode,@Cognitive_Level,@IsLast,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Book_Type", SqlDbType.Char, 0x24), new SqlParameter("@KPLevel", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@KPName", SqlDbType.VarChar, 200), new SqlParameter("@KPCode", SqlDbType.VarChar, 200), new SqlParameter("@Cognitive_Level", SqlDbType.Char, 0x24), new SqlParameter("@IsLast", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.S_KnowledgePoint_Id;
            cmdParms[1].Value = model.GradeTerm;
            cmdParms[2].Value = model.Subject;
            cmdParms[3].Value = model.Resource_Version;
            cmdParms[4].Value = model.Book_Type;
            cmdParms[5].Value = model.KPLevel;
            cmdParms[6].Value = model.Parent_Id;
            cmdParms[7].Value = model.S_KnowledgePointBasic_Id;
            cmdParms[8].Value = model.KPName;
            cmdParms[9].Value = model.KPCode;
            cmdParms[10].Value = model.Cognitive_Level;
            cmdParms[11].Value = model.IsLast;
            cmdParms[12].Value = model.CreateUser;
            cmdParms[13].Value = model.CreateTime;
            cmdParms[14].Value = model.UpdateUser;
            cmdParms[15].Value = model.UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool AddBasic(Model_S_KnowledgePoint model, Model_S_KnowledgePointBasic modelBasic)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into S_KnowledgePointBasic(");
            builder.Append("S_KnowledgePointBasic_Id,GradeTerm,Subject,KPNameBasic,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@S_KnowledgePointBasic_Id,@GradeTerm,@Subject,@KPNameBasic,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            parameterArray[0].Value = modelBasic.S_KnowledgePointBasic_Id;
            parameterArray[1].Value = modelBasic.GradeTerm;
            parameterArray[2].Value = modelBasic.Subject;
            parameterArray[3].Value = modelBasic.KPNameBasic;
            parameterArray[4].Value = modelBasic.CreateUser;
            parameterArray[5].Value = modelBasic.CreateTime;
            parameterArray[6].Value = modelBasic.UpdateUser;
            parameterArray[7].Value = modelBasic.UpdateTime;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("insert into S_KnowledgePoint(");
            builder.Append("S_KnowledgePoint_Id,GradeTerm,Subject,Resource_Version,Book_Type,KPLevel,Parent_Id,S_KnowledgePointBasic_Id,KPName,KPCode,Cognitive_Level,IsLast,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@S_KnowledgePoint_Id,@GradeTerm,@Subject,@Resource_Version,@Book_Type,@KPLevel,@Parent_Id,@S_KnowledgePointBasic_Id,@KPName,@KPCode,@Cognitive_Level,@IsLast,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Book_Type", SqlDbType.Char, 0x24), new SqlParameter("@KPLevel", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@KPName", SqlDbType.VarChar, 200), new SqlParameter("@KPCode", SqlDbType.VarChar, 200), new SqlParameter("@Cognitive_Level", SqlDbType.Char, 0x24), new SqlParameter("@IsLast", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            parameterArray2[0].Value = model.S_KnowledgePoint_Id;
            parameterArray2[1].Value = model.GradeTerm;
            parameterArray2[2].Value = model.Subject;
            parameterArray2[3].Value = model.Resource_Version;
            parameterArray2[4].Value = model.Book_Type;
            parameterArray2[5].Value = model.KPLevel;
            parameterArray2[6].Value = model.Parent_Id;
            parameterArray2[7].Value = model.S_KnowledgePointBasic_Id;
            parameterArray2[8].Value = model.KPName;
            parameterArray2[9].Value = model.KPCode;
            parameterArray2[10].Value = model.Cognitive_Level;
            parameterArray2[11].Value = model.IsLast;
            parameterArray2[12].Value = model.CreateUser;
            parameterArray2[13].Value = model.CreateTime;
            parameterArray2[14].Value = model.UpdateUser;
            parameterArray2[15].Value = model.UpdateTime;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_S_KnowledgePoint DataRowToModel(DataRow row)
        {
            Model_S_KnowledgePoint point = new Model_S_KnowledgePoint();
            if (row != null)
            {
                if (row["S_KnowledgePoint_Id"] != null)
                {
                    point.S_KnowledgePoint_Id = row["S_KnowledgePoint_Id"].ToString();
                }
                if (row["GradeTerm"] != null)
                {
                    point.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Subject"] != null)
                {
                    point.Subject = row["Subject"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    point.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["Book_Type"] != null)
                {
                    point.Book_Type = row["Book_Type"].ToString();
                }
                if (row["KPLevel"] != null)
                {
                    point.KPLevel = row["KPLevel"].ToString();
                }
                if (row["Parent_Id"] != null)
                {
                    point.Parent_Id = row["Parent_Id"].ToString();
                }
                if (row["S_KnowledgePointBasic_Id"] != null)
                {
                    point.S_KnowledgePointBasic_Id = row["S_KnowledgePointBasic_Id"].ToString();
                }
                if (row["KPName"] != null)
                {
                    point.KPName = row["KPName"].ToString();
                }
                if (row["KPCode"] != null)
                {
                    point.KPCode = row["KPCode"].ToString();
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

        public bool Delete(string S_KnowledgePoint_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_KnowledgePoint ");
            builder.Append(" where S_KnowledgePoint_Id=@S_KnowledgePoint_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_KnowledgePoint_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string S_KnowledgePoint_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_KnowledgePoint ");
            builder.Append(" where S_KnowledgePoint_Id in (" + S_KnowledgePoint_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string S_KnowledgePoint_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from S_KnowledgePoint");
            builder.Append(" where S_KnowledgePoint_Id=@S_KnowledgePoint_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_KnowledgePoint_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetDataForEdit(string kpId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT T.* from (select t.*,t2.KPNameBasic from S_KnowledgePoint t\r\nleft join S_KnowledgePointBasic t2 on t.S_KnowledgePointBasic_Id=t2.S_KnowledgePointBasic_Id\r\n) T ");
            builder.AppendFormat(" WHERE  S_KnowledgePoint_Id='{0}' ", kpId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select S_KnowledgePoint_Id,GradeTerm,Subject,Resource_Version,Book_Type,KPLevel,Parent_Id,S_KnowledgePointBasic_Id,KPName,KPCode,Cognitive_Level,IsLast,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM S_KnowledgePoint ");
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
            builder.Append(" S_KnowledgePoint_Id,GradeTerm,Subject,Resource_Version,Book_Type,KPLevel,Parent_Id,S_KnowledgePointBasic_Id,KPName,KPCode,Cognitive_Level,IsLast,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM S_KnowledgePoint ");
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
                builder.Append("order by T.S_KnowledgePoint_Id desc");
            }
            builder.Append(")AS Row, T.*  from S_KnowledgePoint T ");
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
                builder.Append("order by T.S_KnowledgePoint_Id desc");
            }
            builder.Append(")AS Row, T.*  from (select t.*,t2.D_Name as KPLevelName,t3.KPNameBasic from S_KnowledgePoint t\r\nleft join Common_Dict t2 on t2.Common_Dict_ID=t.KPLevel\r\nleft join S_KnowledgePointBasic t3 on t.S_KnowledgePointBasic_Id=t3.S_KnowledgePointBasic_Id\r\n) T ");
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
            builder.Append("SELECT T.*  from (select t.*,t3.KPNameBasic,t2.D_Name as KPLevelName from S_KnowledgePoint t\r\nleft join S_KnowledgePointBasic t3 on t.S_KnowledgePointBasic_Id=t3.S_KnowledgePointBasic_Id\r\nleft join Common_Dict t2 on t2.Common_Dict_ID=t.KPLevel) T ");
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

        public Model_S_KnowledgePoint GetModel(string S_KnowledgePoint_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 S_KnowledgePoint_Id,GradeTerm,Subject,Resource_Version,Book_Type,KPLevel,Parent_Id,S_KnowledgePointBasic_Id,KPName,KPCode,Cognitive_Level,IsLast,CreateUser,CreateTime,UpdateUser,UpdateTime from S_KnowledgePoint ");
            builder.Append(" where S_KnowledgePoint_Id=@S_KnowledgePoint_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_KnowledgePoint_Id;
            new Model_S_KnowledgePoint();
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
            builder.Append("select count(1) FROM S_KnowledgePoint ");
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

        public bool Update(Model_S_KnowledgePoint model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update S_KnowledgePoint set ");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("Book_Type=@Book_Type,");
            builder.Append("KPLevel=@KPLevel,");
            builder.Append("Parent_Id=@Parent_Id,");
            builder.Append("S_KnowledgePointBasic_Id=@S_KnowledgePointBasic_Id,");
            builder.Append("KPName=@KPName,");
            builder.Append("KPCode=@KPCode,");
            builder.Append("Cognitive_Level=@Cognitive_Level,");
            builder.Append("IsLast=@IsLast,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UpdateUser=@UpdateUser,");
            builder.Append("UpdateTime=@UpdateTime");
            builder.Append(" where S_KnowledgePoint_Id=@S_KnowledgePoint_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Book_Type", SqlDbType.Char, 0x24), new SqlParameter("@KPLevel", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@KPName", SqlDbType.VarChar, 200), new SqlParameter("@KPCode", SqlDbType.VarChar, 200), new SqlParameter("@Cognitive_Level", SqlDbType.Char, 0x24), new SqlParameter("@IsLast", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.GradeTerm;
            cmdParms[1].Value = model.Subject;
            cmdParms[2].Value = model.Resource_Version;
            cmdParms[3].Value = model.Book_Type;
            cmdParms[4].Value = model.KPLevel;
            cmdParms[5].Value = model.Parent_Id;
            cmdParms[6].Value = model.S_KnowledgePointBasic_Id;
            cmdParms[7].Value = model.KPName;
            cmdParms[8].Value = model.KPCode;
            cmdParms[9].Value = model.Cognitive_Level;
            cmdParms[10].Value = model.IsLast;
            cmdParms[11].Value = model.CreateUser;
            cmdParms[12].Value = model.CreateTime;
            cmdParms[13].Value = model.UpdateUser;
            cmdParms[14].Value = model.UpdateTime;
            cmdParms[15].Value = model.S_KnowledgePoint_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateBasic(Model_S_KnowledgePoint model, Model_S_KnowledgePointBasic modelBasic)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into S_KnowledgePointBasic(");
            builder.Append("S_KnowledgePointBasic_Id,GradeTerm,Subject,KPNameBasic,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@S_KnowledgePointBasic_Id,@GradeTerm,@Subject,@KPNameBasic,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            parameterArray[0].Value = modelBasic.S_KnowledgePointBasic_Id;
            parameterArray[1].Value = modelBasic.GradeTerm;
            parameterArray[2].Value = modelBasic.Subject;
            parameterArray[3].Value = modelBasic.KPNameBasic;
            parameterArray[4].Value = modelBasic.CreateUser;
            parameterArray[5].Value = modelBasic.CreateTime;
            parameterArray[6].Value = modelBasic.UpdateUser;
            parameterArray[7].Value = modelBasic.UpdateTime;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("update S_KnowledgePoint set ");
            builder.Append("KPLevel=@KPLevel,");
            builder.Append("S_KnowledgePointBasic_Id=@S_KnowledgePointBasic_Id,");
            builder.Append("KPName=@KPName,");
            builder.Append("KPCode=@KPCode,");
            builder.Append("Cognitive_Level=@Cognitive_Level,");
            builder.Append("IsLast=@IsLast,");
            builder.Append("UpdateUser=@UpdateUser,");
            builder.Append("UpdateTime=@UpdateTime");
            builder.Append(" where S_KnowledgePoint_Id=@S_KnowledgePoint_Id ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@KPLevel", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@KPName", SqlDbType.VarChar, 200), new SqlParameter("@KPCode", SqlDbType.VarChar, 200), new SqlParameter("@Cognitive_Level", SqlDbType.Char, 0x24), new SqlParameter("@IsLast", SqlDbType.Char, 1), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@S_KnowledgePoint_Id", SqlDbType.Char, 0x24) };
            parameterArray2[0].Value = model.KPLevel;
            parameterArray2[1].Value = model.S_KnowledgePointBasic_Id;
            parameterArray2[2].Value = model.KPName;
            parameterArray2[3].Value = model.KPCode;
            parameterArray2[4].Value = model.Cognitive_Level;
            parameterArray2[5].Value = model.IsLast;
            parameterArray2[6].Value = model.UpdateUser;
            parameterArray2[7].Value = model.UpdateTime;
            parameterArray2[8].Value = model.S_KnowledgePoint_Id;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }
    }
}

