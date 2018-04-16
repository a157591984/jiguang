namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_SysProduct
    {
        public bool Add(Model_SysProduct model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysProduct(");
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

        public bool AddExists(Model_SysProduct model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SysProduct");
            builder.AppendFormat(" where SysCode='{0}'or SysName='{1}'", model.SysCode, model.SysName);
            return (int.Parse(DbHelperSQL.GetSingle(builder.ToString()).ToString()) > 0);
        }

        public Model_SysProduct DataRowToModel(DataRow row)
        {
            Model_SysProduct product = new Model_SysProduct();
            if (row != null)
            {
                if (row["SysCode"] != null)
                {
                    product.SysCode = row["SysCode"].ToString();
                }
                if (row["SysName"] != null)
                {
                    product.SysName = row["SysName"].ToString();
                }
                if ((row["SysOrder"] != null) && (row["SysOrder"].ToString() != ""))
                {
                    product.SysOrder = new int?(int.Parse(row["SysOrder"].ToString()));
                }
                if (row["SysURL"] != null)
                {
                    product.SysURL = row["SysURL"].ToString();
                }
                if (row["SysIcon"] != null)
                {
                    product.SysIcon = row["SysIcon"].ToString();
                }
                if (row["Sys_CreateUser"] != null)
                {
                    product.Sys_CreateUser = row["Sys_CreateUser"].ToString();
                }
                if ((row["Sys_CreateTime"] != null) && (row["Sys_CreateTime"].ToString() != ""))
                {
                    product.Sys_CreateTime = new DateTime?(DateTime.Parse(row["Sys_CreateTime"].ToString()));
                }
                if (row["Sys_UpdateUser"] != null)
                {
                    product.Sys_UpdateUser = row["Sys_UpdateUser"].ToString();
                }
                if ((row["Sys_UpdateTime"] != null) && (row["Sys_UpdateTime"].ToString() != ""))
                {
                    product.Sys_UpdateTime = new DateTime?(DateTime.Parse(row["Sys_UpdateTime"].ToString()));
                }
            }
            return product;
        }

        public bool Delete(string SysCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysProduct ");
            builder.Append(" where SysCode=@SysCode ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysCode", SqlDbType.NChar, 5) };
            cmdParms[0].Value = SysCode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SysCodelist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysProduct ");
            builder.Append(" where SysCode in (" + SysCodelist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool EideExists(Model_SysProduct model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SysProduct");
            builder.AppendFormat(" where SysCode<>'{0}'and SysName='{1}'", model.SysCode, model.SysName);
            return (int.Parse(DbHelperSQL.GetSingle(builder.ToString()).ToString()) > 0);
        }

        public bool Exists(string SysCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SysProduct");
            builder.Append(" where SysCode=@SysCode ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysCode", SqlDbType.NChar, 5) };
            cmdParms[0].Value = SysCode;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SysCode,SysName,SysOrder,SysURL,SysIcon,Sys_CreateUser,Sys_CreateTime,Sys_UpdateUser,Sys_UpdateTime ");
            builder.Append(" FROM SysProduct ");
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
            builder.Append(" FROM SysProduct ");
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
            builder.Append(")AS Row, T.*  from SysProduct T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysProduct GetModel(string SysCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SysCode,SysName,SysOrder,SysURL,SysIcon,Sys_CreateUser,Sys_CreateTime,Sys_UpdateUser,Sys_UpdateTime from SysProduct ");
            builder.Append(" where SysCode=@SysCode ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysCode", SqlDbType.NChar, 5) };
            cmdParms[0].Value = SysCode;
            new Model_SysProduct();
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
            builder.Append("select count(1) FROM SysProduct ");
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

        public DataSet GetSysCodeList(string condition, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            string pSql = string.Empty;
            pSql = "select row_number() over(order by SysOrder ) AS r_n,* from (\r\n                  select SysCode,SysName,SysIcon,SysOrder,SysURL from SysProduct\r\n                       ) as t where 1=1";
            if (condition != "")
            {
                pSql = pSql + condition;
            }
            return sys.GetRecordByPage(pSql, PageIndex, PageSize, out rCount, out pCount);
        }

        public bool Update(Model_SysProduct model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysProduct set ");
            builder.Append("SysName=@SysName,");
            builder.Append("SysOrder=@SysOrder,");
            builder.Append("SysURL=@SysURL,");
            builder.Append("SysIcon=@SysIcon,");
            builder.Append("Sys_CreateUser=@Sys_CreateUser,");
            builder.Append("Sys_CreateTime=@Sys_CreateTime,");
            builder.Append("Sys_UpdateUser=@Sys_UpdateUser,");
            builder.Append("Sys_UpdateTime=@Sys_UpdateTime");
            builder.Append(" where SysCode=@SysCode ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysName", SqlDbType.VarChar, 100), new SqlParameter("@SysOrder", SqlDbType.Int, 4), new SqlParameter("@SysURL", SqlDbType.VarChar, 500), new SqlParameter("@SysIcon", SqlDbType.VarChar, 100), new SqlParameter("@Sys_CreateUser", SqlDbType.NChar, 0x24), new SqlParameter("@Sys_CreateTime", SqlDbType.DateTime), new SqlParameter("@Sys_UpdateUser", SqlDbType.NChar, 0x24), new SqlParameter("@Sys_UpdateTime", SqlDbType.DateTime), new SqlParameter("@SysCode", SqlDbType.NChar, 5) };
            cmdParms[0].Value = model.SysName;
            cmdParms[1].Value = model.SysOrder;
            cmdParms[2].Value = model.SysURL;
            cmdParms[3].Value = model.SysIcon;
            cmdParms[4].Value = model.Sys_CreateUser;
            cmdParms[5].Value = model.Sys_CreateTime;
            cmdParms[6].Value = model.Sys_UpdateUser;
            cmdParms[7].Value = model.Sys_UpdateTime;
            cmdParms[8].Value = model.SysCode;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

