namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_BookAudit
    {
        public bool Add(Model_BookAudit model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into BookAudit(");
            builder.Append("ResourceFolder_Id,Book_Name,AuditState,AuditRemark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@ResourceFolder_Id,@Book_Name,@AuditState,@AuditRemark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Book_Name", SqlDbType.NVarChar, 100), new SqlParameter("@AuditState", SqlDbType.Char, 1), new SqlParameter("@AuditRemark", SqlDbType.NVarChar, 500), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.ResourceFolder_Id;
            cmdParms[1].Value = model.Book_Name;
            cmdParms[2].Value = model.AuditState;
            cmdParms[3].Value = model.AuditRemark;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_BookAudit DataRowToModel(DataRow row)
        {
            Model_BookAudit audit = new Model_BookAudit();
            if (row != null)
            {
                if (row["ResourceFolder_Id"] != null)
                {
                    audit.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["Book_Name"] != null)
                {
                    audit.Book_Name = row["Book_Name"].ToString();
                }
                if (row["AuditState"] != null)
                {
                    audit.AuditState = row["AuditState"].ToString();
                }
                if (row["AuditRemark"] != null)
                {
                    audit.AuditRemark = row["AuditRemark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    audit.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    audit.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return audit;
        }

        public bool Delete(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookAudit ");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ResourceFolder_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookAudit ");
            builder.Append(" where ResourceFolder_Id in (" + ResourceFolder_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from BookAudit");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceFolder_Id,Book_Name,AuditState,AuditRemark,CreateUser,CreateTime ");
            builder.Append(" FROM BookAudit ");
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
            builder.Append(" ResourceFolder_Id,Book_Name,AuditState,AuditRemark,CreateUser,CreateTime ");
            builder.Append(" FROM BookAudit ");
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
            builder.Append(")AS Row, T.*  from BookAudit T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_BookAudit GetModel(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ResourceFolder_Id,Book_Name,AuditState,AuditRemark,CreateUser,CreateTime from BookAudit ");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            new Model_BookAudit();
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
            builder.Append("select count(1) FROM BookAudit ");
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

        public bool Update(Model_BookAudit model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update BookAudit set ");
            builder.Append("Book_Name=@Book_Name,");
            builder.Append("AuditState=@AuditState,");
            builder.Append("AuditRemark=@AuditRemark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Book_Name", SqlDbType.NVarChar, 100), new SqlParameter("@AuditState", SqlDbType.Char, 1), new SqlParameter("@AuditRemark", SqlDbType.NVarChar, 500), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Book_Name;
            cmdParms[1].Value = model.AuditState;
            cmdParms[2].Value = model.AuditRemark;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.ResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

