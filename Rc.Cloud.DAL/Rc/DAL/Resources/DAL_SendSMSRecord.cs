namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SendSMSRecord
    {
        public bool Add(Model_SendSMSRecord model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SendSMSRecord(");
            builder.Append("SendSMSRecordId,Mobile,Content,Status,SType,CTime,ReturnContent,ReturnStatus,SchoolId)");
            builder.Append(" values (");
            builder.Append("@SendSMSRecordId,@Mobile,@Content,@Status,@SType,@CTime,@ReturnContent,@ReturnStatus,@SchoolId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SendSMSRecordId", SqlDbType.Char, 0x24), new SqlParameter("@Mobile", SqlDbType.VarChar, 50), new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Status", SqlDbType.NVarChar, 100), new SqlParameter("@SType", SqlDbType.VarChar, 50), new SqlParameter("@CTime", SqlDbType.DateTime), new SqlParameter("@ReturnContent", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ReturnStatus", SqlDbType.VarChar, 100), new SqlParameter("@SchoolId", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = model.SendSMSRecordId;
            cmdParms[1].Value = model.Mobile;
            cmdParms[2].Value = model.Content;
            cmdParms[3].Value = model.Status;
            cmdParms[4].Value = model.SType;
            cmdParms[5].Value = model.CTime;
            cmdParms[6].Value = model.ReturnContent;
            cmdParms[7].Value = model.ReturnStatus;
            cmdParms[8].Value = model.SchoolId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool AddMultiSMS(List<Model_SendSMSRecord> listModel)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (Model_SendSMSRecord record in listModel)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into SendSMSRecord(");
                builder.Append("SendSMSRecordId,Mobile,Content,Status,SType,CTime,ReturnContent,ReturnStatus,SchoolId)");
                builder.Append(" values (");
                builder.Append("@SendSMSRecordId,@Mobile,@Content,@Status,@SType,@CTime,@ReturnContent,@ReturnStatus,@SchoolId)");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@SendSMSRecordId", SqlDbType.Char, 0x24), new SqlParameter("@Mobile", SqlDbType.VarChar, 50), new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Status", SqlDbType.NVarChar, 100), new SqlParameter("@SType", SqlDbType.VarChar, 50), new SqlParameter("@CTime", SqlDbType.DateTime), new SqlParameter("@ReturnContent", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ReturnStatus", SqlDbType.VarChar, 100), new SqlParameter("@SchoolId", SqlDbType.VarChar, 50) };
                parameterArray[0].Value = record.SendSMSRecordId;
                parameterArray[1].Value = record.Mobile;
                parameterArray[2].Value = record.Content;
                parameterArray[3].Value = record.Status;
                parameterArray[4].Value = record.SType;
                parameterArray[5].Value = record.CTime;
                parameterArray[6].Value = record.ReturnContent;
                parameterArray[7].Value = record.ReturnStatus;
                parameterArray[8].Value = record.SchoolId;
                dictionary.Add(builder.ToString(), parameterArray);
            }
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_SendSMSRecord DataRowToModel(DataRow row)
        {
            Model_SendSMSRecord record = new Model_SendSMSRecord();
            if (row != null)
            {
                if (row["SendSMSRecordId"] != null)
                {
                    record.SendSMSRecordId = row["SendSMSRecordId"].ToString();
                }
                if (row["Mobile"] != null)
                {
                    record.Mobile = row["Mobile"].ToString();
                }
                if (row["Content"] != null)
                {
                    record.Content = row["Content"].ToString();
                }
                if (row["Status"] != null)
                {
                    record.Status = row["Status"].ToString();
                }
                if (row["SType"] != null)
                {
                    record.SType = row["SType"].ToString();
                }
                if ((row["CTime"] != null) && (row["CTime"].ToString() != ""))
                {
                    record.CTime = DateTime.Parse(row["CTime"].ToString());
                }
                if (row["ReturnContent"] != null)
                {
                    record.ReturnContent = row["ReturnContent"].ToString();
                }
                if (row["ReturnStatus"] != null)
                {
                    record.ReturnStatus = row["ReturnStatus"].ToString();
                }
                if (row["SchoolId"] != null)
                {
                    record.SchoolId = row["SchoolId"].ToString();
                }
            }
            return record;
        }

        public bool Delete(string SendSMSRecordId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SendSMSRecord ");
            builder.Append(" where SendSMSRecordId=@SendSMSRecordId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SendSMSRecordId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SendSMSRecordId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SendSMSRecordIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SendSMSRecord ");
            builder.Append(" where SendSMSRecordId in (" + SendSMSRecordIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string SendSMSRecordId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SendSMSRecord");
            builder.Append(" where SendSMSRecordId=@SendSMSRecordId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SendSMSRecordId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SendSMSRecordId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SendSMSRecordId,Mobile,Content,Status,SType,CTime,ReturnContent,ReturnStatus,SchoolId ");
            builder.Append(" FROM SendSMSRecord ");
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
            builder.Append(" SendSMSRecordId,Mobile,Content,Status,SType,CTime,ReturnContent,ReturnStatus,SchoolId ");
            builder.Append(" FROM SendSMSRecord ");
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
                builder.Append("order by T.SendSMSRecordId desc");
            }
            builder.Append(")AS Row, T.*  from SendSMSRecord T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SendSMSRecord GetModel(string SendSMSRecordId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SendSMSRecordId,Mobile,Content,Status,SType,CTime,ReturnContent,ReturnStatus,SchoolId from SendSMSRecord ");
            builder.Append(" where SendSMSRecordId=@SendSMSRecordId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SendSMSRecordId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SendSMSRecordId;
            new Model_SendSMSRecord();
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
            builder.Append("select count(1) FROM SendSMSRecord ");
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

        public bool Update(Model_SendSMSRecord model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SendSMSRecord set ");
            builder.Append("Mobile=@Mobile,");
            builder.Append("Content=@Content,");
            builder.Append("Status=@Status,");
            builder.Append("SType=@SType,");
            builder.Append("CTime=@CTime,");
            builder.Append("ReturnContent=@ReturnContent,");
            builder.Append("ReturnStatus=@ReturnStatus,");
            builder.Append("SchoolId=@SchoolId");
            builder.Append(" where SendSMSRecordId=@SendSMSRecordId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Mobile", SqlDbType.VarChar, 50), new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Status", SqlDbType.NVarChar, 100), new SqlParameter("@SType", SqlDbType.VarChar, 50), new SqlParameter("@CTime", SqlDbType.DateTime), new SqlParameter("@ReturnContent", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ReturnStatus", SqlDbType.VarChar, 100), new SqlParameter("@SchoolId", SqlDbType.VarChar, 50), new SqlParameter("@SendSMSRecordId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Mobile;
            cmdParms[1].Value = model.Content;
            cmdParms[2].Value = model.Status;
            cmdParms[3].Value = model.SType;
            cmdParms[4].Value = model.CTime;
            cmdParms[5].Value = model.ReturnContent;
            cmdParms[6].Value = model.ReturnStatus;
            cmdParms[7].Value = model.SchoolId;
            cmdParms[8].Value = model.SendSMSRecordId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

