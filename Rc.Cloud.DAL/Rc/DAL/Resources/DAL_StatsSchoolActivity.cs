namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsSchoolActivity
    {
        public bool Add(Model_StatsSchoolActivity model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsSchoolActivity(");
            builder.Append("StatsSchoolActivity_Id,DateType,DateData,SchoolId,SchoolName,ResourceClass,ProductType,Activity,CreateTime,NActivity)");
            builder.Append(" values (");
            builder.Append("@StatsSchoolActivity_Id,@DateType,@DateData,@SchoolId,@SchoolName,@ResourceClass,@ProductType,@Activity,@CreateTime,@NActivity)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsSchoolActivity_Id", SqlDbType.Char, 0x24), new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20), new SqlParameter("@SchoolId", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 200), new SqlParameter("@ResourceClass", SqlDbType.VarChar, 50), new SqlParameter("@ProductType", SqlDbType.VarChar, 50), new SqlParameter("@Activity", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@NActivity", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.StatsSchoolActivity_Id;
            cmdParms[1].Value = model.DateType;
            cmdParms[2].Value = model.DateData;
            cmdParms[3].Value = model.SchoolId;
            cmdParms[4].Value = model.SchoolName;
            cmdParms[5].Value = model.ResourceClass;
            cmdParms[6].Value = model.ProductType;
            cmdParms[7].Value = model.Activity;
            cmdParms[8].Value = model.CreateTime;
            cmdParms[9].Value = model.NActivity;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsSchoolActivity DataRowToModel(DataRow row)
        {
            Model_StatsSchoolActivity activity = new Model_StatsSchoolActivity();
            if (row != null)
            {
                if (row["StatsSchoolActivity_Id"] != null)
                {
                    activity.StatsSchoolActivity_Id = row["StatsSchoolActivity_Id"].ToString();
                }
                if (row["DateType"] != null)
                {
                    activity.DateType = row["DateType"].ToString();
                }
                if (row["DateData"] != null)
                {
                    activity.DateData = row["DateData"].ToString();
                }
                if (row["SchoolId"] != null)
                {
                    activity.SchoolId = row["SchoolId"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    activity.SchoolName = row["SchoolName"].ToString();
                }
                if (row["ResourceClass"] != null)
                {
                    activity.ResourceClass = row["ResourceClass"].ToString();
                }
                if (row["ProductType"] != null)
                {
                    activity.ProductType = row["ProductType"].ToString();
                }
                if ((row["Activity"] != null) && (row["Activity"].ToString() != ""))
                {
                    activity.Activity = new int?(int.Parse(row["Activity"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    activity.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if ((row["NActivity"] != null) && (row["NActivity"].ToString() != ""))
                {
                    activity.NActivity = new int?(int.Parse(row["NActivity"].ToString()));
                }
            }
            return activity;
        }

        public bool Delete(string StatsSchoolActivity_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsSchoolActivity ");
            builder.Append(" where StatsSchoolActivity_Id=@StatsSchoolActivity_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsSchoolActivity_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsSchoolActivity_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsSchoolActivity_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsSchoolActivity ");
            builder.Append(" where StatsSchoolActivity_Id in (" + StatsSchoolActivity_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsSchoolActivity_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsSchoolActivity");
            builder.Append(" where StatsSchoolActivity_Id=@StatsSchoolActivity_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsSchoolActivity_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsSchoolActivity_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsSchoolActivity_Id,DateType,DateData,SchoolId,SchoolName,ResourceClass,ProductType,Activity,CreateTime,NActivity ");
            builder.Append(" FROM StatsSchoolActivity ");
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
            builder.Append(" StatsSchoolActivity_Id,DateType,DateData,SchoolId,SchoolName,ResourceClass,ProductType,Activity,CreateTime,NActivity ");
            builder.Append(" FROM StatsSchoolActivity ");
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
                builder.Append("order by T.StatsSchoolActivity_Id desc");
            }
            builder.Append(")AS Row, T.*  from StatsSchoolActivity T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsSchoolActivity GetModel(string StatsSchoolActivity_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsSchoolActivity_Id,DateType,DateData,SchoolId,SchoolName,ResourceClass,ProductType,Activity,CreateTime,NActivity from StatsSchoolActivity ");
            builder.Append(" where StatsSchoolActivity_Id=@StatsSchoolActivity_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsSchoolActivity_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsSchoolActivity_Id;
            new Model_StatsSchoolActivity();
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
            builder.Append("select count(1) FROM StatsSchoolActivity ");
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

        public bool Update(Model_StatsSchoolActivity model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsSchoolActivity set ");
            builder.Append("DateType=@DateType,");
            builder.Append("DateData=@DateData,");
            builder.Append("SchoolId=@SchoolId,");
            builder.Append("SchoolName=@SchoolName,");
            builder.Append("ResourceClass=@ResourceClass,");
            builder.Append("ProductType=@ProductType,");
            builder.Append("Activity=@Activity,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("NActivity=@NActivity");
            builder.Append(" where StatsSchoolActivity_Id=@StatsSchoolActivity_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20), new SqlParameter("@SchoolId", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 200), new SqlParameter("@ResourceClass", SqlDbType.VarChar, 50), new SqlParameter("@ProductType", SqlDbType.VarChar, 50), new SqlParameter("@Activity", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@NActivity", SqlDbType.Int, 4), new SqlParameter("@StatsSchoolActivity_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.DateType;
            cmdParms[1].Value = model.DateData;
            cmdParms[2].Value = model.SchoolId;
            cmdParms[3].Value = model.SchoolName;
            cmdParms[4].Value = model.ResourceClass;
            cmdParms[5].Value = model.ProductType;
            cmdParms[6].Value = model.Activity;
            cmdParms[7].Value = model.CreateTime;
            cmdParms[8].Value = model.NActivity;
            cmdParms[9].Value = model.StatsSchoolActivity_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

