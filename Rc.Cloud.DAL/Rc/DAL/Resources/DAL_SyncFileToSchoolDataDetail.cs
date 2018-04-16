﻿namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SyncFileToSchoolDataDetail
    {
        public bool Add(Model_SyncFileToSchoolDataDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SyncFileToSchoolDataDetail(");
            builder.Append("SyncFileToSchoolDataDetail_Id,SchoolId,SchoolName,BookId,BookName,Resource_Type,Resource_TypeName,SyncStatus,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@SyncFileToSchoolDataDetail_Id,@SchoolId,@SchoolName,@BookId,@BookName,@Resource_Type,@Resource_TypeName,@SyncStatus,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchoolDataDetail_Id", SqlDbType.Char, 0x24), new SqlParameter("@SchoolId", SqlDbType.VarChar, 50), new SqlParameter("@SchoolName", SqlDbType.VarChar, 500), new SqlParameter("@BookId", SqlDbType.Char, 0x24), new SqlParameter("@BookName", SqlDbType.VarChar, 500), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_TypeName", SqlDbType.VarChar, 500), new SqlParameter("@SyncStatus", SqlDbType.Char, 1), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.SyncFileToSchoolDataDetail_Id;
            cmdParms[1].Value = model.SchoolId;
            cmdParms[2].Value = model.SchoolName;
            cmdParms[3].Value = model.BookId;
            cmdParms[4].Value = model.BookName;
            cmdParms[5].Value = model.Resource_Type;
            cmdParms[6].Value = model.Resource_TypeName;
            cmdParms[7].Value = model.SyncStatus;
            cmdParms[8].Value = model.Remark;
            cmdParms[9].Value = model.CreateUser;
            cmdParms[10].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SyncFileToSchoolDataDetail DataRowToModel(DataRow row)
        {
            Model_SyncFileToSchoolDataDetail detail = new Model_SyncFileToSchoolDataDetail();
            if (row != null)
            {
                if (row["SyncFileToSchoolDataDetail_Id"] != null)
                {
                    detail.SyncFileToSchoolDataDetail_Id = row["SyncFileToSchoolDataDetail_Id"].ToString();
                }
                if (row["SchoolId"] != null)
                {
                    detail.SchoolId = row["SchoolId"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    detail.SchoolName = row["SchoolName"].ToString();
                }
                if (row["BookId"] != null)
                {
                    detail.BookId = row["BookId"].ToString();
                }
                if (row["BookName"] != null)
                {
                    detail.BookName = row["BookName"].ToString();
                }
                if (row["Resource_Type"] != null)
                {
                    detail.Resource_Type = row["Resource_Type"].ToString();
                }
                if (row["Resource_TypeName"] != null)
                {
                    detail.Resource_TypeName = row["Resource_TypeName"].ToString();
                }
                if (row["SyncStatus"] != null)
                {
                    detail.SyncStatus = row["SyncStatus"].ToString();
                }
                if (row["Remark"] != null)
                {
                    detail.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    detail.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    detail.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return detail;
        }

        public bool Delete(string SyncFileToSchoolDataDetail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SyncFileToSchoolDataDetail ");
            builder.Append(" where SyncFileToSchoolDataDetail_Id=@SyncFileToSchoolDataDetail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchoolDataDetail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncFileToSchoolDataDetail_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SyncFileToSchoolDataDetail_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SyncFileToSchoolDataDetail ");
            builder.Append(" where SyncFileToSchoolDataDetail_Id in (" + SyncFileToSchoolDataDetail_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string SyncFileToSchoolDataDetail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SyncFileToSchoolDataDetail");
            builder.Append(" where SyncFileToSchoolDataDetail_Id=@SyncFileToSchoolDataDetail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchoolDataDetail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncFileToSchoolDataDetail_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SyncFileToSchoolDataDetail_Id,SchoolId,SchoolName,BookId,BookName,Resource_Type,Resource_TypeName,SyncStatus,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM SyncFileToSchoolDataDetail ");
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
            builder.Append(" SyncFileToSchoolDataDetail_Id,SchoolId,SchoolName,BookId,BookName,Resource_Type,Resource_TypeName,SyncStatus,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM SyncFileToSchoolDataDetail ");
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
                builder.Append("order by T.SyncFileToSchoolDataDetail_Id desc");
            }
            builder.Append(")AS Row, T.*  from SyncFileToSchoolDataDetail T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SyncFileToSchoolDataDetail GetModel(string SyncFileToSchoolDataDetail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SyncFileToSchoolDataDetail_Id,SchoolId,SchoolName,BookId,BookName,Resource_Type,Resource_TypeName,SyncStatus,Remark,CreateUser,CreateTime from SyncFileToSchoolDataDetail ");
            builder.Append(" where SyncFileToSchoolDataDetail_Id=@SyncFileToSchoolDataDetail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchoolDataDetail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncFileToSchoolDataDetail_Id;
            new Model_SyncFileToSchoolDataDetail();
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
            builder.Append("select count(1) FROM SyncFileToSchoolDataDetail ");
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

        public bool Update(Model_SyncFileToSchoolDataDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SyncFileToSchoolDataDetail set ");
            builder.Append("SchoolId=@SchoolId,");
            builder.Append("SchoolName=@SchoolName,");
            builder.Append("BookId=@BookId,");
            builder.Append("BookName=@BookName,");
            builder.Append("Resource_Type=@Resource_Type,");
            builder.Append("Resource_TypeName=@Resource_TypeName,");
            builder.Append("SyncStatus=@SyncStatus,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where SyncFileToSchoolDataDetail_Id=@SyncFileToSchoolDataDetail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolId", SqlDbType.VarChar, 50), new SqlParameter("@SchoolName", SqlDbType.VarChar, 500), new SqlParameter("@BookId", SqlDbType.Char, 0x24), new SqlParameter("@BookName", SqlDbType.VarChar, 500), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_TypeName", SqlDbType.VarChar, 500), new SqlParameter("@SyncStatus", SqlDbType.Char, 1), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@SyncFileToSchoolDataDetail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SchoolId;
            cmdParms[1].Value = model.SchoolName;
            cmdParms[2].Value = model.BookId;
            cmdParms[3].Value = model.BookName;
            cmdParms[4].Value = model.Resource_Type;
            cmdParms[5].Value = model.Resource_TypeName;
            cmdParms[6].Value = model.SyncStatus;
            cmdParms[7].Value = model.Remark;
            cmdParms[8].Value = model.CreateUser;
            cmdParms[9].Value = model.CreateTime;
            cmdParms[10].Value = model.SyncFileToSchoolDataDetail_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

