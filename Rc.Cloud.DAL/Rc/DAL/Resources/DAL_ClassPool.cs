namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_ClassPool
    {
        public bool Add(Model_ClassPool model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ClassPool(");
            builder.Append("ClassPool_Id,Class_Id,IsEnabled,IsUsed,CreateTime)");
            builder.Append(" values (");
            builder.Append("@ClassPool_Id,@Class_Id,@IsEnabled,@IsUsed,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassPool_Id", SqlDbType.Char, 0x24), new SqlParameter("@Class_Id", SqlDbType.VarChar, 10), new SqlParameter("@IsEnabled", SqlDbType.Int, 4), new SqlParameter("@IsUsed", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.ClassPool_Id;
            cmdParms[1].Value = model.Class_Id;
            cmdParms[2].Value = model.IsEnabled;
            cmdParms[3].Value = model.IsUsed;
            cmdParms[4].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_ClassPool DataRowToModel(DataRow row)
        {
            Model_ClassPool pool = new Model_ClassPool();
            if (row != null)
            {
                if (row["ClassPool_Id"] != null)
                {
                    pool.ClassPool_Id = row["ClassPool_Id"].ToString();
                }
                if (row["Class_Id"] != null)
                {
                    pool.Class_Id = row["Class_Id"].ToString();
                }
                if ((row["IsEnabled"] != null) && (row["IsEnabled"].ToString() != ""))
                {
                    pool.IsEnabled = new int?(int.Parse(row["IsEnabled"].ToString()));
                }
                if ((row["IsUsed"] != null) && (row["IsUsed"].ToString() != ""))
                {
                    pool.IsUsed = new int?(int.Parse(row["IsUsed"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    pool.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return pool;
        }

        public bool Delete(string ClassPool_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ClassPool ");
            builder.Append(" where ClassPool_Id=@ClassPool_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassPool_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ClassPool_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ClassPool_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ClassPool ");
            builder.Append(" where ClassPool_Id in (" + ClassPool_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ClassPool_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ClassPool");
            builder.Append(" where ClassPool_Id=@ClassPool_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassPool_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ClassPool_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ClassPool_Id,Class_Id,IsEnabled,IsUsed,CreateTime ");
            builder.Append(" FROM ClassPool ");
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
            builder.Append(" ClassPool_Id,Class_Id,IsEnabled,IsUsed,CreateTime ");
            builder.Append(" FROM ClassPool ");
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
                builder.Append("order by T.ClassPool_Id desc");
            }
            builder.Append(")AS Row, T.*  from ClassPool T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_ClassPool GetModel(string ClassPool_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ClassPool_Id,Class_Id,IsEnabled,IsUsed,CreateTime from ClassPool ");
            builder.Append(" where ClassPool_Id=@ClassPool_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassPool_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ClassPool_Id;
            new Model_ClassPool();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_ClassPool GetModelByClass_Id(string Class_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ClassPool_Id,Class_Id,IsEnabled,IsUsed,CreateTime from ClassPool ");
            builder.Append(" where Class_Id=@Class_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Class_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Class_Id;
            new Model_ClassPool();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public string GetNewClassID()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ISNULL(max(Class_id),'') from (select top 1 Class_id from dbo.ClassPool where IsEnabled='1' and IsUsed='0' order by ClassPool_Id) t;");
            builder.Append("update ClassPool set IsUsed='1' where Class_id in(select ISNULL(max(Class_id),'') from (select top 1 Class_id from dbo.ClassPool where IsEnabled='1' and IsUsed='0' order by ClassPool_Id) t);");
            return DbHelperSQL.GetSingle(builder.ToString()).ToString();
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM ClassPool ");
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

        public bool Update(Model_ClassPool model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ClassPool set ");
            builder.Append("Class_Id=@Class_Id,");
            builder.Append("IsEnabled=@IsEnabled,");
            builder.Append("IsUsed=@IsUsed,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where ClassPool_Id=@ClassPool_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Class_Id", SqlDbType.VarChar, 10), new SqlParameter("@IsEnabled", SqlDbType.Int, 4), new SqlParameter("@IsUsed", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ClassPool_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Class_Id;
            cmdParms[1].Value = model.IsEnabled;
            cmdParms[2].Value = model.IsUsed;
            cmdParms[3].Value = model.CreateTime;
            cmdParms[4].Value = model.ClassPool_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

