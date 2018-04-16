namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysCode
    {
        public bool Add(Model_SysCode model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysCode(");
            builder.Append("SysCode,SysName,SysOrder,SysURL,SysIcon,Sys_CreateUser,Sys_CreateTime,Sys_UpdateUser,Sys_UpdateTime)");
            builder.Append(" values (");
            builder.Append("@SysCode,@SysName,@SysOrder,@SysURL,@SysIcon,@Sys_CreateUser,@Sys_CreateTime,@Sys_UpdateUser,@Sys_UpdateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysCode", SqlDbType.NChar, 5), new SqlParameter("@SysName", SqlDbType.VarChar, 100), new SqlParameter("@SysOrder", SqlDbType.Int, 4), new SqlParameter("@SysURL", SqlDbType.VarChar, 500), new SqlParameter("@SysIcon", SqlDbType.VarChar, 100), new SqlParameter("@Sys_CreateUser", SqlDbType.NChar, 0x24), new SqlParameter("@Sys_CreateTime", SqlDbType.DateTime), new SqlParameter("@Sys_UpdateUser", SqlDbType.NChar, 0x24), new SqlParameter("@Sys_UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.SysCode;
            cmdParms[1].Value = model.SysName;
            cmdParms[2].Value = model.SysOrder;
            cmdParms[3].Value = model.SysURL;
            cmdParms[4].Value = model.SysIcon;
            cmdParms[5].Value = model.Sys_CreateUser;
            cmdParms[6].Value = model.Sys_CreateTime;
            cmdParms[7].Value = model.Sys_UpdateUser;
            cmdParms[8].Value = model.Sys_UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysCode DataRowToModel(DataRow row)
        {
            Model_SysCode code = new Model_SysCode();
            if (row != null)
            {
                if (row["SysCode"] != null)
                {
                    code.SysCode = row["SysCode"].ToString();
                }
                if (row["SysName"] != null)
                {
                    code.SysName = row["SysName"].ToString();
                }
                if ((row["SysOrder"] != null) && (row["SysOrder"].ToString() != ""))
                {
                    code.SysOrder = new int?(int.Parse(row["SysOrder"].ToString()));
                }
                if (row["SysURL"] != null)
                {
                    code.SysURL = row["SysURL"].ToString();
                }
                if (row["SysIcon"] != null)
                {
                    code.SysIcon = row["SysIcon"].ToString();
                }
                if (row["Sys_CreateUser"] != null)
                {
                    code.Sys_CreateUser = row["Sys_CreateUser"].ToString();
                }
                if ((row["Sys_CreateTime"] != null) && (row["Sys_CreateTime"].ToString() != ""))
                {
                    code.Sys_CreateTime = new DateTime?(DateTime.Parse(row["Sys_CreateTime"].ToString()));
                }
                if (row["Sys_UpdateUser"] != null)
                {
                    code.Sys_UpdateUser = row["Sys_UpdateUser"].ToString();
                }
                if ((row["Sys_UpdateTime"] != null) && (row["Sys_UpdateTime"].ToString() != ""))
                {
                    code.Sys_UpdateTime = new DateTime?(DateTime.Parse(row["Sys_UpdateTime"].ToString()));
                }
            }
            return code;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysCode ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SysCode,SysName,SysOrder,SysURL,SysIcon,Sys_CreateUser,Sys_CreateTime,Sys_UpdateUser,Sys_UpdateTime ");
            builder.Append(" FROM SysCode ");
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
            builder.Append(" SysCode,SysName,SysOrder,SysURL,SysIcon,Sys_CreateUser,Sys_CreateTime,Sys_UpdateUser,Sys_UpdateTime ");
            builder.Append(" FROM SysCode ");
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
                builder.Append("order by T.SysCode desc");
            }
            builder.Append(")AS Row, T.*  from SysCode T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysCode GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SysCode,SysName,SysOrder,SysURL,SysIcon,Sys_CreateUser,Sys_CreateTime,Sys_UpdateUser,Sys_UpdateTime from SysCode ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SysCode();
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
            builder.Append("select count(1) FROM SysCode ");
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

        public bool Update(Model_SysCode model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysCode set ");
            builder.Append("SysCode=@SysCode,");
            builder.Append("SysName=@SysName,");
            builder.Append("SysOrder=@SysOrder,");
            builder.Append("SysURL=@SysURL,");
            builder.Append("SysIcon=@SysIcon,");
            builder.Append("Sys_CreateUser=@Sys_CreateUser,");
            builder.Append("Sys_CreateTime=@Sys_CreateTime,");
            builder.Append("Sys_UpdateUser=@Sys_UpdateUser,");
            builder.Append("Sys_UpdateTime=@Sys_UpdateTime");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysCode", SqlDbType.NChar, 5), new SqlParameter("@SysName", SqlDbType.VarChar, 100), new SqlParameter("@SysOrder", SqlDbType.Int, 4), new SqlParameter("@SysURL", SqlDbType.VarChar, 500), new SqlParameter("@SysIcon", SqlDbType.VarChar, 100), new SqlParameter("@Sys_CreateUser", SqlDbType.NChar, 0x24), new SqlParameter("@Sys_CreateTime", SqlDbType.DateTime), new SqlParameter("@Sys_UpdateUser", SqlDbType.NChar, 0x24), new SqlParameter("@Sys_UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.SysCode;
            cmdParms[1].Value = model.SysName;
            cmdParms[2].Value = model.SysOrder;
            cmdParms[3].Value = model.SysURL;
            cmdParms[4].Value = model.SysIcon;
            cmdParms[5].Value = model.Sys_CreateUser;
            cmdParms[6].Value = model.Sys_CreateTime;
            cmdParms[7].Value = model.Sys_UpdateUser;
            cmdParms[8].Value = model.Sys_UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

