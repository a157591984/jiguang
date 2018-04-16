namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SchoolSMS
    {
        public bool Add(Model_SchoolSMS model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SchoolSMS(");
            builder.Append("School_Id,School_Name,SMSCount,Remark,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@School_Id,@School_Name,@SMSCount,@Remark,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@School_Id", SqlDbType.VarChar, 50), new SqlParameter("@School_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SMSCount", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.School_Id;
            cmdParms[1].Value = model.School_Name;
            cmdParms[2].Value = model.SMSCount;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.UpdateUser;
            cmdParms[7].Value = model.UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SchoolSMS DataRowToModel(DataRow row)
        {
            Model_SchoolSMS lsms = new Model_SchoolSMS();
            if (row != null)
            {
                if (row["School_Id"] != null)
                {
                    lsms.School_Id = row["School_Id"].ToString();
                }
                if (row["School_Name"] != null)
                {
                    lsms.School_Name = row["School_Name"].ToString();
                }
                if ((row["SMSCount"] != null) && (row["SMSCount"].ToString() != ""))
                {
                    lsms.SMSCount = new int?(int.Parse(row["SMSCount"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    lsms.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    lsms.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    lsms.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["UpdateUser"] != null)
                {
                    lsms.UpdateUser = row["UpdateUser"].ToString();
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    lsms.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
            }
            return lsms;
        }

        public bool Delete(string School_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SchoolSMS ");
            builder.Append(" where School_Id=@School_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@School_Id", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = School_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string School_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SchoolSMS ");
            builder.Append(" where School_Id in (" + School_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string School_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SchoolSMS");
            builder.Append(" where School_Id=@School_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@School_Id", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = School_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select School_Id,School_Name,SMSCount,Remark,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM SchoolSMS ");
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
            builder.Append(" School_Id,School_Name,SMSCount,Remark,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            builder.Append(" FROM SchoolSMS ");
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
                builder.Append("order by T.School_Id desc");
            }
            builder.Append(")AS Row, T.*  from SchoolSMS T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SchoolSMS GetModel(string School_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 School_Id,School_Name,SMSCount,Remark,CreateUser,CreateTime,UpdateUser,UpdateTime from SchoolSMS ");
            builder.Append(" where School_Id=@School_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@School_Id", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = School_Id;
            new Model_SchoolSMS();
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
            builder.Append("select count(1) FROM SchoolSMS ");
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

        public bool Update(Model_SchoolSMS model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SchoolSMS set ");
            builder.Append("School_Name=@School_Name,");
            builder.Append("SMSCount=@SMSCount,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UpdateUser=@UpdateUser,");
            builder.Append("UpdateTime=@UpdateTime");
            builder.Append(" where School_Id=@School_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@School_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SMSCount", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@School_Id", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = model.School_Name;
            cmdParms[1].Value = model.SMSCount;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.UpdateUser;
            cmdParms[6].Value = model.UpdateTime;
            cmdParms[7].Value = model.School_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

