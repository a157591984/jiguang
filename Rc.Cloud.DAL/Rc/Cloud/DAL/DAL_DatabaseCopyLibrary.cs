namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_DatabaseCopyLibrary
    {
        public DAL_DatabaseCopyLibrary()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public DAL_DatabaseCopyLibrary(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(Model_DatabaseCopyLibrary model)
        {
            return this.Add(null, model);
        }

        internal int Add(DbTransaction tran, Model_DatabaseCopyLibrary model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append(" DataBaseCopyLibrary( ");
            builder.Append(" DatabaseCopyLibrary_ID,CustomerInfo_ID,DataBaseName,DataBaseAddress,LoginName,LoginPassword,DatabaseCopyLibrary_Remark,CreateTime,CreateUser");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @DataBaseCopyLibrary_ID,@CustomerInfo_ID,@DataBaseName,@DataBaseAddress,@LoginName,@LoginPassword,@DataBaseCopyLibrary_Remark,@CreateTime,@CreateUser");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.DatabaseCopyLibrary_ID, model.CustomerInfo_ID, model.DataBaseName, model.DataBaseAddress, model.LoginName, model.LoginPassword, model.DatabaseCopyLibrary_Remark, model.CreateTime, model.CreateUser });
        }

        public int DeleteByPK(string customervisit_id)
        {
            return this.DeleteByPK(null, customervisit_id);
        }

        internal int DeleteByPK(DbTransaction tran, string customervisit_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM");
            builder.Append(" DatabaseCopyLibrary ");
            builder.Append(" WHERE ");
            builder.Append(" DatabaseCopyLibrary_ID=@DatabaseCopyLibrary_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { customervisit_id });
        }

        public DataSet GetListByID(string id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT *,CustomerInfo.CustomerInfo_NameCN   \r\n                         from DatabaseCopyLibrary left join CustomerInfo on CustomerInfo.CustomerInfo_ID= DatabaseCopyLibrary.CustomerInfo_ID                    \r\n                          where 1=1  ");
            builder.Append(" and DatabaseCopyLibrary.DatabaseCopyLibrary_ID='" + id + "'");
            return this.CurrDB.ExecuteDataSet(builder.ToString(), null, new object[0]);
        }

        public DataSet GetListPaged(Model_DatabaseCopyLibraryParam model, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT row_number() over(order by DatabaseCopyLibrary.UpdateTime  DESC ) AS r_n, DataBaseName,DataBaseAddress,DatabaseCopyLibrary_ID,DatabaseCopyLibrary_Remark,LoginName,LoginPassword,CustomerInfo.CustomerInfo_NameCN   \r\n                         from DatabaseCopyLibrary inner join CustomerInfo on CustomerInfo.CustomerInfo_ID= DatabaseCopyLibrary.CustomerInfo_ID\r\n                          where 1=1 ");
            if (!string.IsNullOrEmpty(model.CustomerInfo_NameCN))
            {
                builder.Append(" and CustomerInfo.CustomerInfo_NameCN like'" + model.CustomerInfo_NameCN + "%'");
            }
            if (!string.IsNullOrEmpty(model.DatabaseCopyLibrary_ID))
            {
                builder.Append(" and DatabaseCopyLibrary.DatabaseCopyLibrary_ID like'" + model.DatabaseCopyLibrary_ID + "%'");
            }
            if (!string.IsNullOrEmpty(model.DataBaseName))
            {
                builder.Append(" and DatabaseCopyLibrary.DataBaseName like'" + model.DataBaseName + "%'");
            }
            return sys.GetRecordByPage(builder.ToString(), PageIndex, PageSize, out rCount, out pCount);
        }

        public Model_DatabaseCopyLibrary GetModel(string id)
        {
            return this.GetModel(null, id);
        }

        internal Model_DatabaseCopyLibrary GetModel(DbTransaction tran, string id)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT  * FROM DatabaseCopyLibrary where CustomerInfo_ID='{0}'", id);
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { id });
            Model_DatabaseCopyLibrary library = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                library = new Model_DatabaseCopyLibrary();
                if (row["DatabaseCopyLibrary_ID"] != null)
                {
                    library.DatabaseCopyLibrary_ID = row["DatabaseCopyLibrary_ID"].ToString();
                }
                if (row["DatabaseCopyLibrary_Remark"] != null)
                {
                    library.DatabaseCopyLibrary_Remark = row["DatabaseCopyLibrary_Remark"].ToString();
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
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        library.CreateTime = null;
                    }
                    else
                    {
                        library.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    library.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateUser"] != null)
                {
                    library.UpdateUser = row["UpdateUser"].ToString();
                }
                if (row["UpdateTime"] == null)
                {
                    return library;
                }
                if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                {
                    library.UpdateTime = null;
                    return library;
                }
                library.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
            }
            return library;
        }

        public Model_DatabaseCopyLibrary GetModel_DatabaseCopyLibraryByPK(string id)
        {
            return this.GetModel_DatabaseCopyLibraryByPK(null, id);
        }

        internal Model_DatabaseCopyLibrary GetModel_DatabaseCopyLibraryByPK(DbTransaction tran, string customervisit_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" DatabaseCopyLibrary ");
            builder.Append(" WHERE ");
            builder.Append(" DatabaseCopyLibrary_ID=@DatabaseCopyLibrary_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { customervisit_id });
            Model_DatabaseCopyLibrary library = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                library = new Model_DatabaseCopyLibrary();
                if (row["DatabaseCopyLibrary_ID"] != null)
                {
                    library.DatabaseCopyLibrary_ID = row["DatabaseCopyLibrary_ID"].ToString();
                }
                if (row["DatabaseCopyLibrary_Remark"] != null)
                {
                    library.DatabaseCopyLibrary_Remark = row["DatabaseCopyLibrary_Remark"].ToString();
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
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        library.CreateTime = null;
                    }
                    else
                    {
                        library.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    library.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateUser"] != null)
                {
                    library.UpdateUser = row["UpdateUser"].ToString();
                }
                if (row["UpdateTime"] == null)
                {
                    return library;
                }
                if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                {
                    library.UpdateTime = null;
                    return library;
                }
                library.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
            }
            return library;
        }

        public int Update(Model_DatabaseCopyLibrary model)
        {
            return this.Update(null, model);
        }

        internal int Update(DbTransaction tran, Model_DatabaseCopyLibrary model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" DatabaseCopyLibrary ");
            builder.Append(" SET ");
            builder.Append(" DatabaseCopyLibrary_ID=@DatabaseCopyLibrary_ID,CustomerInfo_ID=@CustomerInfo_ID,DataBaseName=@DataBaseName,DataBaseAddress=@DataBaseAddress,LoginName=@LoginName,LoginPassword=@LoginPassword,DatabaseCopyLibrary_Remark=@DatabaseCopyLibrary_Remark,CreateTime=@CreateTime,CreateUser=@CreateUser,UpdateUser=@UpdateUser,UpdateTime=@UpdateTime ");
            builder.Append(" WHERE ");
            builder.Append(" DatabaseCopyLibrary_ID=@DatabaseCopyLibrary_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.DatabaseCopyLibrary_ID, model.CustomerInfo_ID, model.DataBaseName, model.DataBaseAddress, model.LoginName, model.LoginPassword, model.DatabaseCopyLibrary_Remark, model.CreateTime, model.CreateUser, model.UpdateUser, model.UpdateTime });
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.Update(null, strUpdateColumns, strCondition, param);
        }

        internal int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" DatabaseCopyLibrary");
            builder.Append(" SET ");
            builder.Append(strUpdateColumns);
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, param);
        }

        private DatabaseSQLHelper CurrDB { get; set; }
    }
}

