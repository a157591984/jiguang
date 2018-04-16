namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysFilter
    {
        public bool Add(Model_SysFilter model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysFilter(");
            builder.Append("SysFilter_id,KeyWord,Create_Time,Create_UserId)");
            builder.Append(" values (");
            builder.Append("@SysFilter_id,@KeyWord,@Create_Time,@Create_UserId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysFilter_id", SqlDbType.Char, 0x24), new SqlParameter("@KeyWord", SqlDbType.VarChar, 500), new SqlParameter("@Create_Time", SqlDbType.DateTime), new SqlParameter("@Create_UserId", SqlDbType.Char, 30) };
            cmdParms[0].Value = model.SysFilter_id;
            cmdParms[1].Value = model.KeyWord;
            cmdParms[2].Value = model.Create_Time;
            cmdParms[3].Value = model.Create_UserId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysFilter DataRowToModel(DataRow row)
        {
            Model_SysFilter filter = new Model_SysFilter();
            if (row != null)
            {
                if (row["SysFilter_id"] != null)
                {
                    filter.SysFilter_id = row["SysFilter_id"].ToString();
                }
                if (row["KeyWord"] != null)
                {
                    filter.KeyWord = row["KeyWord"].ToString();
                }
                if ((row["Create_Time"] != null) && (row["Create_Time"].ToString() != ""))
                {
                    filter.Create_Time = new DateTime?(DateTime.Parse(row["Create_Time"].ToString()));
                }
                if (row["Create_UserId"] != null)
                {
                    filter.Create_UserId = row["Create_UserId"].ToString();
                }
            }
            return filter;
        }

        public bool Delete(string SysFilter_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysFilter ");
            builder.Append(" where SysFilter_id=@SysFilter_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysFilter_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SysFilter_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SysFilter_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysFilter ");
            builder.Append(" where SysFilter_id in (" + SysFilter_idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string SysFilter_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SysFilter");
            builder.Append(" where SysFilter_id=@SysFilter_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysFilter_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SysFilter_id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SysFilter_id,KeyWord,Create_Time,Create_UserId ");
            builder.Append(" FROM SysFilter ");
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
            builder.Append(" SysFilter_id,KeyWord,Create_Time,Create_UserId ");
            builder.Append(" FROM SysFilter ");
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
                builder.Append("order by T.SysFilter_id desc");
            }
            builder.Append(")AS Row, T.*  from SysFilter T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysFilter GetModel(string SysFilter_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SysFilter_id,KeyWord,Create_Time,Create_UserId from SysFilter ");
            builder.Append(" where SysFilter_id=@SysFilter_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysFilter_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SysFilter_id;
            new Model_SysFilter();
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
            builder.Append("select count(1) FROM SysFilter ");
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

        public bool Update(Model_SysFilter model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysFilter set ");
            builder.Append("KeyWord=@KeyWord,");
            builder.Append("Create_Time=@Create_Time,");
            builder.Append("Create_UserId=@Create_UserId");
            builder.Append(" where SysFilter_id=@SysFilter_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyWord", SqlDbType.VarChar, 500), new SqlParameter("@Create_Time", SqlDbType.DateTime), new SqlParameter("@Create_UserId", SqlDbType.Char, 30), new SqlParameter("@SysFilter_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.KeyWord;
            cmdParms[1].Value = model.Create_Time;
            cmdParms[2].Value = model.Create_UserId;
            cmdParms[3].Value = model.SysFilter_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

