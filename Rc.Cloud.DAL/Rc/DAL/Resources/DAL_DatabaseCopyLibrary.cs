namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_DatabaseCopyLibrary
    {
        public bool Add(Model_DatabaseCopyLibrary model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into DatabaseCopyLibrary(");
            builder.Append("DatabaseCopyLibrary_ID,CustomerInfo_ID,DataBaseName,DataBaseAddress,LoginName,LoginPassword,DatabaseCopyLibrary_Remark,CreateTime,CreateUser,UpdateUser,UpdateTime)");
            builder.Append(" values (");
            builder.Append("@DatabaseCopyLibrary_ID,@CustomerInfo_ID,@DataBaseName,@DataBaseAddress,@LoginName,@LoginPassword,@DatabaseCopyLibrary_Remark,@CreateTime,@CreateUser,@UpdateUser,@UpdateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DatabaseCopyLibrary_ID", SqlDbType.Char, 0x24), new SqlParameter("@CustomerInfo_ID", SqlDbType.Char, 0x24), new SqlParameter("@DataBaseName", SqlDbType.NVarChar, 0x80), new SqlParameter("@DataBaseAddress", SqlDbType.NVarChar, 500), new SqlParameter("@LoginName", SqlDbType.NVarChar, 0x80), new SqlParameter("@LoginPassword", SqlDbType.NVarChar, 0x80), new SqlParameter("@DatabaseCopyLibrary_Remark", SqlDbType.NVarChar, 0x800), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.DatabaseCopyLibrary_ID;
            cmdParms[1].Value = model.CustomerInfo_ID;
            cmdParms[2].Value = model.DataBaseName;
            cmdParms[3].Value = model.DataBaseAddress;
            cmdParms[4].Value = model.LoginName;
            cmdParms[5].Value = model.LoginPassword;
            cmdParms[6].Value = model.DatabaseCopyLibrary_Remark;
            cmdParms[7].Value = model.CreateTime;
            cmdParms[8].Value = model.CreateUser;
            cmdParms[9].Value = model.UpdateUser;
            cmdParms[10].Value = model.UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_DatabaseCopyLibrary DataRowToModel(DataRow row)
        {
            Model_DatabaseCopyLibrary library = new Model_DatabaseCopyLibrary();
            if (row != null)
            {
                if (row["DatabaseCopyLibrary_ID"] != null)
                {
                    library.DatabaseCopyLibrary_ID = row["DatabaseCopyLibrary_ID"].ToString();
                }
                if (row["CustomerInfo_ID"] != null)
                {
                    library.CustomerInfo_ID = row["CustomerInfo_ID"].ToString();
                }
                if (row["DataBaseName"] != null)
                {
                    library.DataBaseName = row["DataBaseName"].ToString();
                }
                if (row["DataBaseAddress"] != null)
                {
                    library.DataBaseAddress = row["DataBaseAddress"].ToString();
                }
                if (row["LoginName"] != null)
                {
                    library.LoginName = row["LoginName"].ToString();
                }
                if (row["LoginPassword"] != null)
                {
                    library.LoginPassword = row["LoginPassword"].ToString();
                }
                if (row["DatabaseCopyLibrary_Remark"] != null)
                {
                    library.DatabaseCopyLibrary_Remark = row["DatabaseCopyLibrary_Remark"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    library.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    library.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateUser"] != null)
                {
                    library.UpdateUser = row["UpdateUser"].ToString();
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    library.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
            }
            return library;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from DatabaseCopyLibrary ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select DatabaseCopyLibrary_ID,CustomerInfo_ID,DataBaseName,DataBaseAddress,LoginName,LoginPassword,DatabaseCopyLibrary_Remark,CreateTime,CreateUser,UpdateUser,UpdateTime ");
            builder.Append(" FROM DatabaseCopyLibrary ");
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
            builder.Append(" DatabaseCopyLibrary_ID,CustomerInfo_ID,DataBaseName,DataBaseAddress,LoginName,LoginPassword,DatabaseCopyLibrary_Remark,CreateTime,CreateUser,UpdateUser,UpdateTime ");
            builder.Append(" FROM DatabaseCopyLibrary ");
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
                builder.Append("order by T.UserGroup_Member_Id desc");
            }
            builder.Append(")AS Row, T.*  from DatabaseCopyLibrary T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_DatabaseCopyLibrary GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 DatabaseCopyLibrary_ID,CustomerInfo_ID,DataBaseName,DataBaseAddress,LoginName,LoginPassword,DatabaseCopyLibrary_Remark,CreateTime,CreateUser,UpdateUser,UpdateTime from DatabaseCopyLibrary ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_DatabaseCopyLibrary();
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
            builder.Append("select count(1) FROM DatabaseCopyLibrary ");
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

        public bool Update(Model_DatabaseCopyLibrary model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update DatabaseCopyLibrary set ");
            builder.Append("DatabaseCopyLibrary_ID=@DatabaseCopyLibrary_ID,");
            builder.Append("CustomerInfo_ID=@CustomerInfo_ID,");
            builder.Append("DataBaseName=@DataBaseName,");
            builder.Append("DataBaseAddress=@DataBaseAddress,");
            builder.Append("LoginName=@LoginName,");
            builder.Append("LoginPassword=@LoginPassword,");
            builder.Append("DatabaseCopyLibrary_Remark=@DatabaseCopyLibrary_Remark,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("UpdateUser=@UpdateUser,");
            builder.Append("UpdateTime=@UpdateTime");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DatabaseCopyLibrary_ID", SqlDbType.Char, 0x24), new SqlParameter("@CustomerInfo_ID", SqlDbType.Char, 0x24), new SqlParameter("@DataBaseName", SqlDbType.NVarChar, 0x80), new SqlParameter("@DataBaseAddress", SqlDbType.NVarChar, 500), new SqlParameter("@LoginName", SqlDbType.NVarChar, 0x80), new SqlParameter("@LoginPassword", SqlDbType.NVarChar, 0x80), new SqlParameter("@DatabaseCopyLibrary_Remark", SqlDbType.NVarChar, 0x800), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.DatabaseCopyLibrary_ID;
            cmdParms[1].Value = model.CustomerInfo_ID;
            cmdParms[2].Value = model.DataBaseName;
            cmdParms[3].Value = model.DataBaseAddress;
            cmdParms[4].Value = model.LoginName;
            cmdParms[5].Value = model.LoginPassword;
            cmdParms[6].Value = model.DatabaseCopyLibrary_Remark;
            cmdParms[7].Value = model.CreateTime;
            cmdParms[8].Value = model.CreateUser;
            cmdParms[9].Value = model.UpdateUser;
            cmdParms[10].Value = model.UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

