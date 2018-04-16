namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SyncFileToSchoolData
    {
        public bool Add(Model_SyncFileToSchoolData model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SyncFileToSchoolData(");
            builder.Append("SyncFileToSchoolData_Id,SchoolId,SchoolName,SchoolUrl,BookAllCount,BookTobeCount,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@SyncFileToSchoolData_Id,@SchoolId,@SchoolName,@SchoolUrl,@BookAllCount,@BookTobeCount,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchoolData_Id", SqlDbType.Char, 0x24), new SqlParameter("@SchoolId", SqlDbType.VarChar, 50), new SqlParameter("@SchoolName", SqlDbType.VarChar, 500), new SqlParameter("@SchoolUrl", SqlDbType.VarChar, 200), new SqlParameter("@BookAllCount", SqlDbType.Int, 4), new SqlParameter("@BookTobeCount", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.SyncFileToSchoolData_Id;
            cmdParms[1].Value = model.SchoolId;
            cmdParms[2].Value = model.SchoolName;
            cmdParms[3].Value = model.SchoolUrl;
            cmdParms[4].Value = model.BookAllCount;
            cmdParms[5].Value = model.BookTobeCount;
            cmdParms[6].Value = model.Remark;
            cmdParms[7].Value = model.CreateUser;
            cmdParms[8].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SyncFileToSchoolData DataRowToModel(DataRow row)
        {
            Model_SyncFileToSchoolData data = new Model_SyncFileToSchoolData();
            if (row != null)
            {
                if (row["SyncFileToSchoolData_Id"] != null)
                {
                    data.SyncFileToSchoolData_Id = row["SyncFileToSchoolData_Id"].ToString();
                }
                if (row["SchoolId"] != null)
                {
                    data.SchoolId = row["SchoolId"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    data.SchoolName = row["SchoolName"].ToString();
                }
                if (row["SchoolUrl"] != null)
                {
                    data.SchoolUrl = row["SchoolUrl"].ToString();
                }
                if ((row["BookAllCount"] != null) && (row["BookAllCount"].ToString() != ""))
                {
                    data.BookAllCount = new int?(int.Parse(row["BookAllCount"].ToString()));
                }
                if ((row["BookTobeCount"] != null) && (row["BookTobeCount"].ToString() != ""))
                {
                    data.BookTobeCount = new int?(int.Parse(row["BookTobeCount"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    data.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    data.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    data.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return data;
        }

        public bool Delete(string SyncFileToSchoolData_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SyncFileToSchoolData ");
            builder.Append(" where SyncFileToSchoolData_Id=@SyncFileToSchoolData_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchoolData_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncFileToSchoolData_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SyncFileToSchoolData_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SyncFileToSchoolData ");
            builder.Append(" where SyncFileToSchoolData_Id in (" + SyncFileToSchoolData_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string SyncFileToSchoolData_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SyncFileToSchoolData");
            builder.Append(" where SyncFileToSchoolData_Id=@SyncFileToSchoolData_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchoolData_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncFileToSchoolData_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SyncFileToSchoolData_Id,SchoolId,SchoolName,SchoolUrl,BookAllCount,BookTobeCount,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM SyncFileToSchoolData ");
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
            builder.Append(" SyncFileToSchoolData_Id,SchoolId,SchoolName,SchoolUrl,BookAllCount,BookTobeCount,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM SyncFileToSchoolData ");
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
                builder.Append("order by T.SyncFileToSchoolData_Id desc");
            }
            builder.Append(")AS Row, T.*  from SyncFileToSchoolData T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage_Operate(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.SyncFileToSchoolData_Id desc");
            }
            builder.Append(")AS Row, T.*  from SyncFileToSchoolData T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL_Operate.Query(builder.ToString());
        }

        public Model_SyncFileToSchoolData GetModel(string SyncFileToSchoolData_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SyncFileToSchoolData_Id,SchoolId,SchoolName,SchoolUrl,BookAllCount,BookTobeCount,Remark,CreateUser,CreateTime from SyncFileToSchoolData ");
            builder.Append(" where SyncFileToSchoolData_Id=@SyncFileToSchoolData_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchoolData_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncFileToSchoolData_Id;
            new Model_SyncFileToSchoolData();
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
            builder.Append("select count(1) FROM SyncFileToSchoolData ");
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

        public int GetRecordCount_Operate(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SyncFileToSchoolData ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL_Operate.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Model_SyncFileToSchoolData model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SyncFileToSchoolData set ");
            builder.Append("SchoolId=@SchoolId,");
            builder.Append("SchoolName=@SchoolName,");
            builder.Append("SchoolUrl=@SchoolUrl,");
            builder.Append("BookAllCount=@BookAllCount,");
            builder.Append("BookTobeCount=@BookTobeCount,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where SyncFileToSchoolData_Id=@SyncFileToSchoolData_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolId", SqlDbType.VarChar, 50), new SqlParameter("@SchoolName", SqlDbType.VarChar, 500), new SqlParameter("@SchoolUrl", SqlDbType.VarChar, 200), new SqlParameter("@BookAllCount", SqlDbType.Int, 4), new SqlParameter("@BookTobeCount", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@SyncFileToSchoolData_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SchoolId;
            cmdParms[1].Value = model.SchoolName;
            cmdParms[2].Value = model.SchoolUrl;
            cmdParms[3].Value = model.BookAllCount;
            cmdParms[4].Value = model.BookTobeCount;
            cmdParms[5].Value = model.Remark;
            cmdParms[6].Value = model.CreateUser;
            cmdParms[7].Value = model.CreateTime;
            cmdParms[8].Value = model.SyncFileToSchoolData_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

