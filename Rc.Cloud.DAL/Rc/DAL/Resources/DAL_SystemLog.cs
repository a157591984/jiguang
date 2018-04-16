namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SystemLog
    {
        public bool Add(Model_SystemLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SystemLog(");
            builder.Append("SystemLog_ID,SystemLog_Level,SystemLog_Model,SystemLog_DrugID,SystemLog_TableName,SystemLog_TableDataID,SystemLog_Desc,SystemLog_LoginID,SystemLog_CreateDate,SystemLog_IP,SystemLog_Source,SystemLog_Type,SystemLog_Remark)");
            builder.Append(" values (");
            builder.Append("@SystemLog_ID,@SystemLog_Level,@SystemLog_Model,@SystemLog_DrugID,@SystemLog_TableName,@SystemLog_TableDataID,@SystemLog_Desc,@SystemLog_LoginID,@SystemLog_CreateDate,@SystemLog_IP,@SystemLog_Source,@SystemLog_Type,@SystemLog_Remark)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SystemLog_ID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_Level", SqlDbType.VarChar, -1), new SqlParameter("@SystemLog_Model", SqlDbType.NVarChar, 500), new SqlParameter("@SystemLog_DrugID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_TableName", SqlDbType.VarChar, 0xff), new SqlParameter("@SystemLog_TableDataID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_Desc", SqlDbType.NVarChar, -1), new SqlParameter("@SystemLog_LoginID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_CreateDate", SqlDbType.DateTime), new SqlParameter("@SystemLog_IP", SqlDbType.VarChar, 50), new SqlParameter("@SystemLog_Source", SqlDbType.Int, 4), new SqlParameter("@SystemLog_Type", SqlDbType.Int, 4), new SqlParameter("@SystemLog_Remark", SqlDbType.NVarChar, 500) };
            cmdParms[0].Value = model.SystemLog_ID;
            cmdParms[1].Value = model.SystemLog_Level;
            cmdParms[2].Value = model.SystemLog_Model;
            cmdParms[3].Value = model.SystemLog_DrugID;
            cmdParms[4].Value = model.SystemLog_TableName;
            cmdParms[5].Value = model.SystemLog_TableDataID;
            cmdParms[6].Value = model.SystemLog_Desc;
            cmdParms[7].Value = model.SystemLog_LoginID;
            cmdParms[8].Value = model.SystemLog_CreateDate;
            cmdParms[9].Value = model.SystemLog_IP;
            cmdParms[10].Value = model.SystemLog_Source;
            cmdParms[11].Value = model.SystemLog_Type;
            cmdParms[12].Value = model.SystemLog_Remark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SystemLog DataRowToModel(DataRow row)
        {
            Model_SystemLog log = new Model_SystemLog();
            if (row != null)
            {
                if (row["SystemLog_ID"] != null)
                {
                    log.SystemLog_ID = row["SystemLog_ID"].ToString();
                }
                if (row["SystemLog_Level"] != null)
                {
                    log.SystemLog_Level = row["SystemLog_Level"].ToString();
                }
                if (row["SystemLog_Model"] != null)
                {
                    log.SystemLog_Model = row["SystemLog_Model"].ToString();
                }
                if (row["SystemLog_DrugID"] != null)
                {
                    log.SystemLog_DrugID = row["SystemLog_DrugID"].ToString();
                }
                if (row["SystemLog_TableName"] != null)
                {
                    log.SystemLog_TableName = row["SystemLog_TableName"].ToString();
                }
                if (row["SystemLog_TableDataID"] != null)
                {
                    log.SystemLog_TableDataID = row["SystemLog_TableDataID"].ToString();
                }
                if (row["SystemLog_Desc"] != null)
                {
                    log.SystemLog_Desc = row["SystemLog_Desc"].ToString();
                }
                if (row["SystemLog_LoginID"] != null)
                {
                    log.SystemLog_LoginID = row["SystemLog_LoginID"].ToString();
                }
                if ((row["SystemLog_CreateDate"] != null) && (row["SystemLog_CreateDate"].ToString() != ""))
                {
                    log.SystemLog_CreateDate = new DateTime?(DateTime.Parse(row["SystemLog_CreateDate"].ToString()));
                }
                if (row["SystemLog_IP"] != null)
                {
                    log.SystemLog_IP = row["SystemLog_IP"].ToString();
                }
                if ((row["SystemLog_Source"] != null) && (row["SystemLog_Source"].ToString() != ""))
                {
                    log.SystemLog_Source = new int?(int.Parse(row["SystemLog_Source"].ToString()));
                }
                if ((row["SystemLog_Type"] != null) && (row["SystemLog_Type"].ToString() != ""))
                {
                    log.SystemLog_Type = new int?(int.Parse(row["SystemLog_Type"].ToString()));
                }
                if (row["SystemLog_Remark"] != null)
                {
                    log.SystemLog_Remark = row["SystemLog_Remark"].ToString();
                }
            }
            return log;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SystemLog ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SystemLog_ID,SystemLog_Level,SystemLog_Model,SystemLog_DrugID,SystemLog_TableName,SystemLog_TableDataID,SystemLog_Desc,SystemLog_LoginID,SystemLog_CreateDate,SystemLog_IP,SystemLog_Source,SystemLog_Type,SystemLog_Remark ");
            builder.Append(" FROM SystemLog ");
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
            builder.Append(" SystemLog_ID,SystemLog_Level,SystemLog_Model,SystemLog_DrugID,SystemLog_TableName,SystemLog_TableDataID,SystemLog_Desc,SystemLog_LoginID,SystemLog_CreateDate,SystemLog_IP,SystemLog_Source,SystemLog_Type,SystemLog_Remark ");
            builder.Append(" FROM SystemLog ");
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
                builder.Append("order by T.SystemLog_ID desc");
            }
            builder.Append(")AS Row, T.*  from SystemLog T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SystemLog GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SystemLog_ID,SystemLog_Level,SystemLog_Model,SystemLog_DrugID,SystemLog_TableName,SystemLog_TableDataID,SystemLog_Desc,SystemLog_LoginID,SystemLog_CreateDate,SystemLog_IP,SystemLog_Source,SystemLog_Type,SystemLog_Remark from SystemLog ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SystemLog();
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
            builder.Append("select count(1) FROM SystemLog ");
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

        public bool Update(Model_SystemLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SystemLog set ");
            builder.Append("SystemLog_ID=@SystemLog_ID,");
            builder.Append("SystemLog_Level=@SystemLog_Level,");
            builder.Append("SystemLog_Model=@SystemLog_Model,");
            builder.Append("SystemLog_DrugID=@SystemLog_DrugID,");
            builder.Append("SystemLog_TableName=@SystemLog_TableName,");
            builder.Append("SystemLog_TableDataID=@SystemLog_TableDataID,");
            builder.Append("SystemLog_Desc=@SystemLog_Desc,");
            builder.Append("SystemLog_LoginID=@SystemLog_LoginID,");
            builder.Append("SystemLog_CreateDate=@SystemLog_CreateDate,");
            builder.Append("SystemLog_IP=@SystemLog_IP,");
            builder.Append("SystemLog_Source=@SystemLog_Source,");
            builder.Append("SystemLog_Type=@SystemLog_Type,");
            builder.Append("SystemLog_Remark=@SystemLog_Remark");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SystemLog_ID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_Level", SqlDbType.VarChar, -1), new SqlParameter("@SystemLog_Model", SqlDbType.NVarChar, 500), new SqlParameter("@SystemLog_DrugID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_TableName", SqlDbType.VarChar, 0xff), new SqlParameter("@SystemLog_TableDataID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_Desc", SqlDbType.NVarChar, -1), new SqlParameter("@SystemLog_LoginID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_CreateDate", SqlDbType.DateTime), new SqlParameter("@SystemLog_IP", SqlDbType.VarChar, 50), new SqlParameter("@SystemLog_Source", SqlDbType.Int, 4), new SqlParameter("@SystemLog_Type", SqlDbType.Int, 4), new SqlParameter("@SystemLog_Remark", SqlDbType.NVarChar, 500) };
            cmdParms[0].Value = model.SystemLog_ID;
            cmdParms[1].Value = model.SystemLog_Level;
            cmdParms[2].Value = model.SystemLog_Model;
            cmdParms[3].Value = model.SystemLog_DrugID;
            cmdParms[4].Value = model.SystemLog_TableName;
            cmdParms[5].Value = model.SystemLog_TableDataID;
            cmdParms[6].Value = model.SystemLog_Desc;
            cmdParms[7].Value = model.SystemLog_LoginID;
            cmdParms[8].Value = model.SystemLog_CreateDate;
            cmdParms[9].Value = model.SystemLog_IP;
            cmdParms[10].Value = model.SystemLog_Source;
            cmdParms[11].Value = model.SystemLog_Type;
            cmdParms[12].Value = model.SystemLog_Remark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

