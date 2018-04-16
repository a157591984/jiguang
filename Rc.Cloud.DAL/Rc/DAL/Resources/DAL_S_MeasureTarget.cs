namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_S_MeasureTarget
    {
        public bool Add(Model_S_MeasureTarget model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into S_MeasureTarget(");
            builder.Append("S_MeasureTarget_Id,GradeTerm,Subject,Resource_Version,Parent_Id,MTName,MTCode,MTLevel,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@S_MeasureTarget_Id,@GradeTerm,@Subject,@Resource_Version,@Parent_Id,@MTName,@MTCode,@MTLevel,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_MeasureTarget_Id", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@MTName", SqlDbType.VarChar, 200), new SqlParameter("@MTCode", SqlDbType.VarChar, 50), new SqlParameter("@MTLevel", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.S_MeasureTarget_Id;
            cmdParms[1].Value = model.GradeTerm;
            cmdParms[2].Value = model.Subject;
            cmdParms[3].Value = model.Resource_Version;
            cmdParms[4].Value = model.Parent_Id;
            cmdParms[5].Value = model.MTName;
            cmdParms[6].Value = model.MTCode;
            cmdParms[7].Value = model.MTLevel;
            cmdParms[8].Value = model.CreateUser;
            cmdParms[9].Value = model.CreateTime;
            cmdParms[10].Value = model.UpdateUser;
            cmdParms[11].Value = model.UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_S_MeasureTarget DataRowToModel(DataRow row)
        {
            Model_S_MeasureTarget target = new Model_S_MeasureTarget();
            if (row != null)
            {
                if (row["S_MeasureTarget_Id"] != null)
                {
                    target.S_MeasureTarget_Id = row["S_MeasureTarget_Id"].ToString();
                }
                if (row["GradeTerm"] != null)
                {
                    target.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Subject"] != null)
                {
                    target.Subject = row["Subject"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    target.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["Parent_Id"] != null)
                {
                    target.Parent_Id = row["Parent_Id"].ToString();
                }
                if (row["MTName"] != null)
                {
                    target.MTName = row["MTName"].ToString();
                }
                if (row["MTCode"] != null)
                {
                    target.MTCode = row["MTCode"].ToString();
                }
                if (row["MTLevel"] != null)
                {
                    target.MTLevel = row["MTLevel"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    target.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    target.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["UpdateUser"] != null)
                {
                    target.UpdateUser = row["UpdateUser"].ToString();
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    target.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
            }
            return target;
        }

        public bool Delete(string S_MeasureTarget_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_MeasureTarget ");
            builder.Append(" where S_MeasureTarget_Id=@S_MeasureTarget_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_MeasureTarget_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_MeasureTarget_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string S_MeasureTarget_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_MeasureTarget ");
            builder.Append(" where S_MeasureTarget_Id in (" + S_MeasureTarget_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string S_MeasureTarget_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from S_MeasureTarget");
            builder.Append(" where S_MeasureTarget_Id=@S_MeasureTarget_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_MeasureTarget_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_MeasureTarget_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select S_MeasureTarget_Id,GradeTerm,Subject,Resource_Version,Parent_Id,MTName,MTCode,MTLevel,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM S_MeasureTarget ");
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
            builder.Append(" S_MeasureTarget_Id,GradeTerm,Subject,Resource_Version,Parent_Id,MTName,MTCode,MTLevel,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM S_MeasureTarget ");
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
                builder.Append("order by T.S_MeasureTarget_Id desc");
            }
            builder.Append(")AS Row, T.*  from S_MeasureTarget T ");
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
                builder.Append("order by T.S_MeasureTarget_Id desc");
            }
            builder.Append(")AS Row, T.*  from (select t.*,t2.D_Name as MTLevelName from S_MeasureTarget t\r\nleft join Common_Dict t2 on t2.Common_Dict_ID=t.MTLevel) T ");
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
            builder.Append("SELECT T.*  from (select t.*,t2.D_Name as MTLevelName from S_MeasureTarget t\r\nleft join Common_Dict t2 on t2.Common_Dict_ID=t.MTLevel) T ");
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

        public Model_S_MeasureTarget GetModel(string S_MeasureTarget_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 S_MeasureTarget_Id,GradeTerm,Subject,Resource_Version,Parent_Id,MTName,MTCode,MTLevel,CreateUser,CreateTime,UpdateUser,UpdateTime from S_MeasureTarget ");
            builder.Append(" where S_MeasureTarget_Id=@S_MeasureTarget_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_MeasureTarget_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_MeasureTarget_Id;
            new Model_S_MeasureTarget();
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
            builder.Append("select count(1) FROM S_MeasureTarget ");
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

        public bool Update(Model_S_MeasureTarget model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update S_MeasureTarget set ");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("Parent_Id=@Parent_Id,");
            builder.Append("MTName=@MTName,");
            builder.Append("MTCode=@MTCode,");
            builder.Append("MTLevel=@MTLevel,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UpdateUser=@UpdateUser,");
            builder.Append("UpdateTime=@UpdateTime");
            builder.Append(" where S_MeasureTarget_Id=@S_MeasureTarget_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@MTName", SqlDbType.VarChar, 200), new SqlParameter("@MTCode", SqlDbType.VarChar, 50), new SqlParameter("@MTLevel", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@S_MeasureTarget_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.GradeTerm;
            cmdParms[1].Value = model.Subject;
            cmdParms[2].Value = model.Resource_Version;
            cmdParms[3].Value = model.Parent_Id;
            cmdParms[4].Value = model.MTName;
            cmdParms[5].Value = model.MTCode;
            cmdParms[6].Value = model.MTLevel;
            cmdParms[7].Value = model.CreateUser;
            cmdParms[8].Value = model.CreateTime;
            cmdParms[9].Value = model.UpdateUser;
            cmdParms[10].Value = model.UpdateTime;
            cmdParms[11].Value = model.S_MeasureTarget_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

