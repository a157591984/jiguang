namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Sys_Function
    {
        public bool Add(Model_Sys_Function model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sys_Function(");
            builder.Append("FUNCTIONID,FUNCTIONName)");
            builder.Append(" values (");
            builder.Append("@FUNCTIONID,@FUNCTIONName)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONName", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = model.FUNCTIONID;
            cmdParms[1].Value = model.FUNCTIONName;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Sys_Function DataRowToModel(DataRow row)
        {
            Model_Sys_Function function = new Model_Sys_Function();
            if (row != null)
            {
                if (row["FUNCTIONID"] != null)
                {
                    function.FUNCTIONID = row["FUNCTIONID"].ToString();
                }
                if (row["FUNCTIONName"] != null)
                {
                    function.FUNCTIONName = row["FUNCTIONName"].ToString();
                }
            }
            return function;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sys_Function ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FUNCTIONID,FUNCTIONName ");
            builder.Append(" FROM Sys_Function ");
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
            builder.Append(" FUNCTIONID,FUNCTIONName ");
            builder.Append(" FROM Sys_Function ");
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
                builder.Append("order by T.FUNCTIONID desc");
            }
            builder.Append(")AS Row, T.*  from Sys_Function T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Sys_Function GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FUNCTIONID,FUNCTIONName from Sys_Function ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_Sys_Function();
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
            builder.Append("select count(1) FROM Sys_Function ");
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

        public bool Update(Model_Sys_Function model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sys_Function set ");
            builder.Append("FUNCTIONID=@FUNCTIONID,");
            builder.Append("FUNCTIONName=@FUNCTIONName");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FUNCTIONID", SqlDbType.VarChar, 20), new SqlParameter("@FUNCTIONName", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = model.FUNCTIONID;
            cmdParms[1].Value = model.FUNCTIONName;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

