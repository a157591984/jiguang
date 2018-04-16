namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SystemLogError
    {
        public bool Add(Model_SystemLogError model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SystemLogError(");
            builder.Append("SystemLog_ID,SystemLog_PagePath,SystemLog_SysPath,SystemLog_Desc,SystemLog_LoginID,SystemLog_CreateDate,SystemLog_IP,SystemLog_Source)");
            builder.Append(" values (");
            builder.Append("@SystemLog_ID,@SystemLog_PagePath,@SystemLog_SysPath,@SystemLog_Desc,@SystemLog_LoginID,@SystemLog_CreateDate,@SystemLog_IP,@SystemLog_Source)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SystemLog_ID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_PagePath", SqlDbType.NVarChar, 500), new SqlParameter("@SystemLog_SysPath", SqlDbType.NVarChar, 500), new SqlParameter("@SystemLog_Desc", SqlDbType.NVarChar, -1), new SqlParameter("@SystemLog_LoginID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_CreateDate", SqlDbType.DateTime), new SqlParameter("@SystemLog_IP", SqlDbType.VarChar, 50), new SqlParameter("@SystemLog_Source", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.SystemLog_ID;
            cmdParms[1].Value = model.SystemLog_PagePath;
            cmdParms[2].Value = model.SystemLog_SysPath;
            cmdParms[3].Value = model.SystemLog_Desc;
            cmdParms[4].Value = model.SystemLog_LoginID;
            cmdParms[5].Value = model.SystemLog_CreateDate;
            cmdParms[6].Value = model.SystemLog_IP;
            cmdParms[7].Value = model.SystemLog_Source;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SystemLogError DataRowToModel(DataRow row)
        {
            Model_SystemLogError error = new Model_SystemLogError();
            if (row != null)
            {
                if (row["SystemLog_ID"] != null)
                {
                    error.SystemLog_ID = row["SystemLog_ID"].ToString();
                }
                if (row["SystemLog_PagePath"] != null)
                {
                    error.SystemLog_PagePath = row["SystemLog_PagePath"].ToString();
                }
                if (row["SystemLog_SysPath"] != null)
                {
                    error.SystemLog_SysPath = row["SystemLog_SysPath"].ToString();
                }
                if (row["SystemLog_Desc"] != null)
                {
                    error.SystemLog_Desc = row["SystemLog_Desc"].ToString();
                }
                if (row["SystemLog_LoginID"] != null)
                {
                    error.SystemLog_LoginID = row["SystemLog_LoginID"].ToString();
                }
                if ((row["SystemLog_CreateDate"] != null) && (row["SystemLog_CreateDate"].ToString() != ""))
                {
                    error.SystemLog_CreateDate = new DateTime?(DateTime.Parse(row["SystemLog_CreateDate"].ToString()));
                }
                if (row["SystemLog_IP"] != null)
                {
                    error.SystemLog_IP = row["SystemLog_IP"].ToString();
                }
                if ((row["SystemLog_Source"] != null) && (row["SystemLog_Source"].ToString() != ""))
                {
                    error.SystemLog_Source = new int?(int.Parse(row["SystemLog_Source"].ToString()));
                }
            }
            return error;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SystemLogError ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SystemLog_ID,SystemLog_PagePath,SystemLog_SysPath,SystemLog_Desc,SystemLog_LoginID,SystemLog_CreateDate,SystemLog_IP,SystemLog_Source ");
            builder.Append(" FROM SystemLogError ");
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
            builder.Append(" SystemLog_ID,SystemLog_PagePath,SystemLog_SysPath,SystemLog_Desc,SystemLog_LoginID,SystemLog_CreateDate,SystemLog_IP,SystemLog_Source ");
            builder.Append(" FROM SystemLogError ");
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
            builder.Append(")AS Row, T.*  from SystemLogError T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SystemLogError GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SystemLog_ID,SystemLog_PagePath,SystemLog_SysPath,SystemLog_Desc,SystemLog_LoginID,SystemLog_CreateDate,SystemLog_IP,SystemLog_Source from SystemLogError ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SystemLogError();
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
            builder.Append("select count(1) FROM SystemLogError ");
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

        public bool Update(Model_SystemLogError model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SystemLogError set ");
            builder.Append("SystemLog_ID=@SystemLog_ID,");
            builder.Append("SystemLog_PagePath=@SystemLog_PagePath,");
            builder.Append("SystemLog_SysPath=@SystemLog_SysPath,");
            builder.Append("SystemLog_Desc=@SystemLog_Desc,");
            builder.Append("SystemLog_LoginID=@SystemLog_LoginID,");
            builder.Append("SystemLog_CreateDate=@SystemLog_CreateDate,");
            builder.Append("SystemLog_IP=@SystemLog_IP,");
            builder.Append("SystemLog_Source=@SystemLog_Source");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SystemLog_ID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_PagePath", SqlDbType.NVarChar, 500), new SqlParameter("@SystemLog_SysPath", SqlDbType.NVarChar, 500), new SqlParameter("@SystemLog_Desc", SqlDbType.NVarChar, -1), new SqlParameter("@SystemLog_LoginID", SqlDbType.Char, 0x24), new SqlParameter("@SystemLog_CreateDate", SqlDbType.DateTime), new SqlParameter("@SystemLog_IP", SqlDbType.VarChar, 50), new SqlParameter("@SystemLog_Source", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.SystemLog_ID;
            cmdParms[1].Value = model.SystemLog_PagePath;
            cmdParms[2].Value = model.SystemLog_SysPath;
            cmdParms[3].Value = model.SystemLog_Desc;
            cmdParms[4].Value = model.SystemLog_LoginID;
            cmdParms[5].Value = model.SystemLog_CreateDate;
            cmdParms[6].Value = model.SystemLog_IP;
            cmdParms[7].Value = model.SystemLog_Source;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

