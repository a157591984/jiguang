namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_S_KnowledgePointBasic
    {
        public bool Add(Model_S_KnowledgePointBasic model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into S_KnowledgePointBasic(");
            builder.Append("S_KnowledgePointBasic_Id,GradeTerm,Subject,KPNameBasic,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@S_KnowledgePointBasic_Id,@GradeTerm,@Subject,@KPNameBasic,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.S_KnowledgePointBasic_Id;
            cmdParms[1].Value = model.GradeTerm;
            cmdParms[2].Value = model.Subject;
            cmdParms[3].Value = model.KPNameBasic;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.UpdateUser;
            cmdParms[7].Value = model.UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_S_KnowledgePointBasic DataRowToModel(DataRow row)
        {
            Model_S_KnowledgePointBasic basic = new Model_S_KnowledgePointBasic();
            if (row != null)
            {
                if (row["S_KnowledgePointBasic_Id"] != null)
                {
                    basic.S_KnowledgePointBasic_Id = row["S_KnowledgePointBasic_Id"].ToString();
                }
                if (row["GradeTerm"] != null)
                {
                    basic.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Subject"] != null)
                {
                    basic.Subject = row["Subject"].ToString();
                }
                if (row["KPNameBasic"] != null)
                {
                    basic.KPNameBasic = row["KPNameBasic"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    basic.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    basic.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["UpdateUser"] != null)
                {
                    basic.UpdateUser = row["UpdateUser"].ToString();
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    basic.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
            }
            return basic;
        }

        public bool Delete(string S_KnowledgePointBasic_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_KnowledgePointBasic ");
            builder.Append(" where S_KnowledgePointBasic_Id=@S_KnowledgePointBasic_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_KnowledgePointBasic_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string S_KnowledgePointBasic_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_KnowledgePointBasic ");
            builder.Append(" where S_KnowledgePointBasic_Id in (" + S_KnowledgePointBasic_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string S_KnowledgePointBasic_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from S_KnowledgePointBasic");
            builder.Append(" where S_KnowledgePointBasic_Id=@S_KnowledgePointBasic_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_KnowledgePointBasic_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select S_KnowledgePointBasic_Id,GradeTerm,Subject,KPNameBasic,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM S_KnowledgePointBasic ");
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
            builder.Append(" S_KnowledgePointBasic_Id,GradeTerm,Subject,KPNameBasic,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM S_KnowledgePointBasic ");
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
                builder.Append("order by T.S_KnowledgePointBasic_Id desc");
            }
            builder.Append(")AS Row, T.*  from S_KnowledgePointBasic T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_S_KnowledgePointBasic GetModel(string S_KnowledgePointBasic_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 S_KnowledgePointBasic_Id,GradeTerm,Subject,KPNameBasic,CreateUser,CreateTime,UpdateUser,UpdateTime from S_KnowledgePointBasic ");
            builder.Append(" where S_KnowledgePointBasic_Id=@S_KnowledgePointBasic_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_KnowledgePointBasic_Id;
            new Model_S_KnowledgePointBasic();
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
            builder.Append("select count(1) FROM S_KnowledgePointBasic ");
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

        public bool Update(Model_S_KnowledgePointBasic model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update S_KnowledgePointBasic set ");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("KPNameBasic=@KPNameBasic,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UpdateUser=@UpdateUser,");
            builder.Append("UpdateTime=@UpdateTime");
            builder.Append(" where S_KnowledgePointBasic_Id=@S_KnowledgePointBasic_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@KPNameBasic", SqlDbType.VarChar, 200), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.GradeTerm;
            cmdParms[1].Value = model.Subject;
            cmdParms[2].Value = model.KPNameBasic;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.UpdateUser;
            cmdParms[6].Value = model.UpdateTime;
            cmdParms[7].Value = model.S_KnowledgePointBasic_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

