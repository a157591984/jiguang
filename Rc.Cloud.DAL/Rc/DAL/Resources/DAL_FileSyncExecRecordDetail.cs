namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_FileSyncExecRecordDetail
    {
        public bool Add(Model_FileSyncExecRecordDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into FileSyncExecRecordDetail(");
            builder.Append("FileSyncExecRecordDetail_id,FileSyncExecRecord_id,Book_Id,ResourceToResourceFolder_Id,TestQuestions_Id,Resource_Type,FileUrl,Detail_TimeStart,Detail_TimeEnd,Detail_Status,Detail_Remark)");
            builder.Append(" values (");
            builder.Append("@FileSyncExecRecordDetail_id,@FileSyncExecRecord_id,@Book_Id,@ResourceToResourceFolder_Id,@TestQuestions_Id,@Resource_Type,@FileUrl,@Detail_TimeStart,@Detail_TimeEnd,@Detail_Status,@Detail_Remark)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecordDetail_id", SqlDbType.Char, 0x24), new SqlParameter("@FileSyncExecRecord_id", SqlDbType.Char, 0x24), new SqlParameter("@Book_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@FileUrl", SqlDbType.VarChar, 0x3e8), new SqlParameter("@Detail_TimeStart", SqlDbType.DateTime), new SqlParameter("@Detail_TimeEnd", SqlDbType.DateTime), new SqlParameter("@Detail_Status", SqlDbType.VarChar, 10), new SqlParameter("@Detail_Remark", SqlDbType.VarChar, 500) };
            cmdParms[0].Value = model.FileSyncExecRecordDetail_id;
            cmdParms[1].Value = model.FileSyncExecRecord_id;
            cmdParms[2].Value = model.Book_Id;
            cmdParms[3].Value = model.ResourceToResourceFolder_Id;
            cmdParms[4].Value = model.TestQuestions_Id;
            cmdParms[5].Value = model.Resource_Type;
            cmdParms[6].Value = model.FileUrl;
            cmdParms[7].Value = model.Detail_TimeStart;
            cmdParms[8].Value = model.Detail_TimeEnd;
            cmdParms[9].Value = model.Detail_Status;
            cmdParms[10].Value = model.Detail_Remark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_FileSyncExecRecordDetail DataRowToModel(DataRow row)
        {
            Model_FileSyncExecRecordDetail detail = new Model_FileSyncExecRecordDetail();
            if (row != null)
            {
                if (row["FileSyncExecRecordDetail_id"] != null)
                {
                    detail.FileSyncExecRecordDetail_id = row["FileSyncExecRecordDetail_id"].ToString();
                }
                if (row["FileSyncExecRecord_id"] != null)
                {
                    detail.FileSyncExecRecord_id = row["FileSyncExecRecord_id"].ToString();
                }
                if (row["Book_Id"] != null)
                {
                    detail.Book_Id = row["Book_Id"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    detail.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["TestQuestions_Id"] != null)
                {
                    detail.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if (row["Resource_Type"] != null)
                {
                    detail.Resource_Type = row["Resource_Type"].ToString();
                }
                if (row["FileUrl"] != null)
                {
                    detail.FileUrl = row["FileUrl"].ToString();
                }
                if ((row["Detail_TimeStart"] != null) && (row["Detail_TimeStart"].ToString() != ""))
                {
                    detail.Detail_TimeStart = new DateTime?(DateTime.Parse(row["Detail_TimeStart"].ToString()));
                }
                if ((row["Detail_TimeEnd"] != null) && (row["Detail_TimeEnd"].ToString() != ""))
                {
                    detail.Detail_TimeEnd = new DateTime?(DateTime.Parse(row["Detail_TimeEnd"].ToString()));
                }
                if (row["Detail_Status"] != null)
                {
                    detail.Detail_Status = row["Detail_Status"].ToString();
                }
                if (row["Detail_Remark"] != null)
                {
                    detail.Detail_Remark = row["Detail_Remark"].ToString();
                }
            }
            return detail;
        }

        public bool Delete(string FileSyncExecRecordDetail_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from FileSyncExecRecordDetail ");
            builder.Append(" where FileSyncExecRecordDetail_id=@FileSyncExecRecordDetail_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecordDetail_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncExecRecordDetail_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string FileSyncExecRecordDetail_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from FileSyncExecRecordDetail ");
            builder.Append(" where FileSyncExecRecordDetail_id in (" + FileSyncExecRecordDetail_idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string FileSyncExecRecordDetail_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from FileSyncExecRecordDetail");
            builder.Append(" where FileSyncExecRecordDetail_id=@FileSyncExecRecordDetail_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecordDetail_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncExecRecordDetail_id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FileSyncExecRecordDetail_id,FileSyncExecRecord_id,Book_Id,ResourceToResourceFolder_Id,TestQuestions_Id,Resource_Type,FileUrl,Detail_TimeStart,Detail_TimeEnd,Detail_Status,Detail_Remark ");
            builder.Append(" FROM FileSyncExecRecordDetail ");
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
            builder.Append(" FileSyncExecRecordDetail_id,FileSyncExecRecord_id,Book_Id,ResourceToResourceFolder_Id,TestQuestions_Id,Resource_Type,FileUrl,Detail_TimeStart,Detail_TimeEnd,Detail_Status,Detail_Remark ");
            builder.Append(" FROM FileSyncExecRecordDetail ");
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
                builder.Append("order by T.FileSyncExecRecordDetail_id desc");
            }
            builder.Append(")AS Row, T.*  from FileSyncExecRecordDetail T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_FileSyncExecRecordDetail GetModel(string FileSyncExecRecordDetail_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FileSyncExecRecordDetail_id,FileSyncExecRecord_id,Book_Id,ResourceToResourceFolder_Id,TestQuestions_Id,Resource_Type,FileUrl,Detail_TimeStart,Detail_TimeEnd,Detail_Status,Detail_Remark from FileSyncExecRecordDetail ");
            builder.Append(" where FileSyncExecRecordDetail_id=@FileSyncExecRecordDetail_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecordDetail_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = FileSyncExecRecordDetail_id;
            new Model_FileSyncExecRecordDetail();
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
            builder.Append("select count(1) FROM FileSyncExecRecordDetail ");
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

        public bool Update(Model_FileSyncExecRecordDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update FileSyncExecRecordDetail set ");
            builder.Append("FileSyncExecRecord_id=@FileSyncExecRecord_id,");
            builder.Append("Book_Id=@Book_Id,");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("Resource_Type=@Resource_Type,");
            builder.Append("FileUrl=@FileUrl,");
            builder.Append("Detail_TimeStart=@Detail_TimeStart,");
            builder.Append("Detail_TimeEnd=@Detail_TimeEnd,");
            builder.Append("Detail_Status=@Detail_Status,");
            builder.Append("Detail_Remark=@Detail_Remark");
            builder.Append(" where FileSyncExecRecordDetail_id=@FileSyncExecRecordDetail_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FileSyncExecRecord_id", SqlDbType.Char, 0x24), new SqlParameter("@Book_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@FileUrl", SqlDbType.VarChar, 0x3e8), new SqlParameter("@Detail_TimeStart", SqlDbType.DateTime), new SqlParameter("@Detail_TimeEnd", SqlDbType.DateTime), new SqlParameter("@Detail_Status", SqlDbType.VarChar, 10), new SqlParameter("@Detail_Remark", SqlDbType.VarChar, 500), new SqlParameter("@FileSyncExecRecordDetail_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.FileSyncExecRecord_id;
            cmdParms[1].Value = model.Book_Id;
            cmdParms[2].Value = model.ResourceToResourceFolder_Id;
            cmdParms[3].Value = model.TestQuestions_Id;
            cmdParms[4].Value = model.Resource_Type;
            cmdParms[5].Value = model.FileUrl;
            cmdParms[6].Value = model.Detail_TimeStart;
            cmdParms[7].Value = model.Detail_TimeEnd;
            cmdParms[8].Value = model.Detail_Status;
            cmdParms[9].Value = model.Detail_Remark;
            cmdParms[10].Value = model.FileSyncExecRecordDetail_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

