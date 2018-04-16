namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsVisitClient
    {
        public bool Add(Model_StatsVisitClient model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsVisitClient(");
            builder.Append("StatsVisitClient_Id,DateType,DateData,TeacherId,TeacherName,VisitCount_All,VisitCount_Cloud,VisitCount_Own,VisitFile_All,VisitFile_Cloud,VisitFile_Own,CreateOwnCount_All,CreateOwnCount_Plan,CreateOwnCount_TestPaper,CreateTime,IsUsed)");
            builder.Append(" values (");
            builder.Append("@StatsVisitClient_Id,@DateType,@DateData,@TeacherId,@TeacherName,@VisitCount_All,@VisitCount_Cloud,@VisitCount_Own,@VisitFile_All,@VisitFile_Cloud,@VisitFile_Own,@CreateOwnCount_All,@CreateOwnCount_Plan,@CreateOwnCount_TestPaper,@CreateTime,@IsUsed)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsVisitClient_Id", SqlDbType.Char, 0x24), new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20), new SqlParameter("@TeacherId", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.VarChar, 200), new SqlParameter("@VisitCount_All", SqlDbType.Int, 4), new SqlParameter("@VisitCount_Cloud", SqlDbType.Int, 4), new SqlParameter("@VisitCount_Own", SqlDbType.Int, 4), new SqlParameter("@VisitFile_All", SqlDbType.Int, 4), new SqlParameter("@VisitFile_Cloud", SqlDbType.Int, 4), new SqlParameter("@VisitFile_Own", SqlDbType.Int, 4), new SqlParameter("@CreateOwnCount_All", SqlDbType.Int, 4), new SqlParameter("@CreateOwnCount_Plan", SqlDbType.Int, 4), new SqlParameter("@CreateOwnCount_TestPaper", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@IsUsed", SqlDbType.Char, 1) };
            cmdParms[0].Value = model.StatsVisitClient_Id;
            cmdParms[1].Value = model.DateType;
            cmdParms[2].Value = model.DateData;
            cmdParms[3].Value = model.TeacherId;
            cmdParms[4].Value = model.TeacherName;
            cmdParms[5].Value = model.VisitCount_All;
            cmdParms[6].Value = model.VisitCount_Cloud;
            cmdParms[7].Value = model.VisitCount_Own;
            cmdParms[8].Value = model.VisitFile_All;
            cmdParms[9].Value = model.VisitFile_Cloud;
            cmdParms[10].Value = model.VisitFile_Own;
            cmdParms[11].Value = model.CreateOwnCount_All;
            cmdParms[12].Value = model.CreateOwnCount_Plan;
            cmdParms[13].Value = model.CreateOwnCount_TestPaper;
            cmdParms[14].Value = model.CreateTime;
            cmdParms[15].Value = model.IsUsed;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsVisitClient DataRowToModel(DataRow row)
        {
            Model_StatsVisitClient client = new Model_StatsVisitClient();
            if (row != null)
            {
                if (row["StatsVisitClient_Id"] != null)
                {
                    client.StatsVisitClient_Id = row["StatsVisitClient_Id"].ToString();
                }
                if (row["DateType"] != null)
                {
                    client.DateType = row["DateType"].ToString();
                }
                if (row["DateData"] != null)
                {
                    client.DateData = row["DateData"].ToString();
                }
                if (row["TeacherId"] != null)
                {
                    client.TeacherId = row["TeacherId"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    client.TeacherName = row["TeacherName"].ToString();
                }
                if ((row["VisitCount_All"] != null) && (row["VisitCount_All"].ToString() != ""))
                {
                    client.VisitCount_All = new int?(int.Parse(row["VisitCount_All"].ToString()));
                }
                if ((row["VisitCount_Cloud"] != null) && (row["VisitCount_Cloud"].ToString() != ""))
                {
                    client.VisitCount_Cloud = new int?(int.Parse(row["VisitCount_Cloud"].ToString()));
                }
                if ((row["VisitCount_Own"] != null) && (row["VisitCount_Own"].ToString() != ""))
                {
                    client.VisitCount_Own = new int?(int.Parse(row["VisitCount_Own"].ToString()));
                }
                if ((row["VisitFile_All"] != null) && (row["VisitFile_All"].ToString() != ""))
                {
                    client.VisitFile_All = new int?(int.Parse(row["VisitFile_All"].ToString()));
                }
                if ((row["VisitFile_Cloud"] != null) && (row["VisitFile_Cloud"].ToString() != ""))
                {
                    client.VisitFile_Cloud = new int?(int.Parse(row["VisitFile_Cloud"].ToString()));
                }
                if ((row["VisitFile_Own"] != null) && (row["VisitFile_Own"].ToString() != ""))
                {
                    client.VisitFile_Own = new int?(int.Parse(row["VisitFile_Own"].ToString()));
                }
                if ((row["CreateOwnCount_All"] != null) && (row["CreateOwnCount_All"].ToString() != ""))
                {
                    client.CreateOwnCount_All = new int?(int.Parse(row["CreateOwnCount_All"].ToString()));
                }
                if ((row["CreateOwnCount_Plan"] != null) && (row["CreateOwnCount_Plan"].ToString() != ""))
                {
                    client.CreateOwnCount_Plan = new int?(int.Parse(row["CreateOwnCount_Plan"].ToString()));
                }
                if ((row["CreateOwnCount_TestPaper"] != null) && (row["CreateOwnCount_TestPaper"].ToString() != ""))
                {
                    client.CreateOwnCount_TestPaper = new int?(int.Parse(row["CreateOwnCount_TestPaper"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    client.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["IsUsed"] != null)
                {
                    client.IsUsed = row["IsUsed"].ToString();
                }
            }
            return client;
        }

        public bool Delete(string StatsVisitClient_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsVisitClient ");
            builder.Append(" where StatsVisitClient_Id=@StatsVisitClient_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsVisitClient_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsVisitClient_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsVisitClient_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsVisitClient ");
            builder.Append(" where StatsVisitClient_Id in (" + StatsVisitClient_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsVisitClient_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsVisitClient");
            builder.Append(" where StatsVisitClient_Id=@StatsVisitClient_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsVisitClient_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsVisitClient_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsVisitClient_Id,DateType,DateData,TeacherId,TeacherName,VisitCount_All,VisitCount_Cloud,VisitCount_Own,VisitFile_All,VisitFile_Cloud,VisitFile_Own,CreateOwnCount_All,CreateOwnCount_Plan,CreateOwnCount_TestPaper,CreateTime,IsUsed ");
            builder.Append(" FROM StatsVisitClient ");
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
            builder.Append(" StatsVisitClient_Id,DateType,DateData,TeacherId,TeacherName,VisitCount_All,VisitCount_Cloud,VisitCount_Own,VisitFile_All,VisitFile_Cloud,VisitFile_Own,CreateOwnCount_All,CreateOwnCount_Plan,CreateOwnCount_TestPaper,CreateTime,IsUsed ");
            builder.Append(" FROM StatsVisitClient ");
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
                builder.Append("order by T.StatsVisitClient_Id desc");
            }
            builder.Append(")AS Row, T.*  from StatsVisitClient T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageNew(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by " + orderby);
            }
            else
            {
                builder.Append("order by DateData desc");
            }
            builder.Append(")AS Row,v.DateType,v.DateData,v.TeacherId,v.TeacherName,u.TrueName,v.VisitCount_All,v.VisitCount_Cloud,v.VisitCount_Own,\r\nv.VisitFile_All,v.VisitFile_Cloud,v.VisitFile_Own,v.CreateOwnCount_All,v.CreateOwnCount_Plan,v.CreateOwnCount_TestPaper\r\nfrom  StatsVisitClient v \r\n inner join f_user u on u.userid=v.TeacherId ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(") TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsVisitClient GetModel(string StatsVisitClient_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsVisitClient_Id,DateType,DateData,TeacherId,TeacherName,VisitCount_All,VisitCount_Cloud,VisitCount_Own,VisitFile_All,VisitFile_Cloud,VisitFile_Own,CreateOwnCount_All,CreateOwnCount_Plan,CreateOwnCount_TestPaper,CreateTime,IsUsed from StatsVisitClient ");
            builder.Append(" where StatsVisitClient_Id=@StatsVisitClient_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsVisitClient_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsVisitClient_Id;
            new Model_StatsVisitClient();
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
            builder.Append("select count(1) FROM StatsVisitClient ");
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

        public bool Update(Model_StatsVisitClient model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsVisitClient set ");
            builder.Append("DateType=@DateType,");
            builder.Append("DateData=@DateData,");
            builder.Append("TeacherId=@TeacherId,");
            builder.Append("TeacherName=@TeacherName,");
            builder.Append("VisitCount_All=@VisitCount_All,");
            builder.Append("VisitCount_Cloud=@VisitCount_Cloud,");
            builder.Append("VisitCount_Own=@VisitCount_Own,");
            builder.Append("VisitFile_All=@VisitFile_All,");
            builder.Append("VisitFile_Cloud=@VisitFile_Cloud,");
            builder.Append("VisitFile_Own=@VisitFile_Own,");
            builder.Append("CreateOwnCount_All=@CreateOwnCount_All,");
            builder.Append("CreateOwnCount_Plan=@CreateOwnCount_Plan,");
            builder.Append("CreateOwnCount_TestPaper=@CreateOwnCount_TestPaper,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("IsUsed=@IsUsed");
            builder.Append(" where StatsVisitClient_Id=@StatsVisitClient_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20), new SqlParameter("@TeacherId", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.VarChar, 200), new SqlParameter("@VisitCount_All", SqlDbType.Int, 4), new SqlParameter("@VisitCount_Cloud", SqlDbType.Int, 4), new SqlParameter("@VisitCount_Own", SqlDbType.Int, 4), new SqlParameter("@VisitFile_All", SqlDbType.Int, 4), new SqlParameter("@VisitFile_Cloud", SqlDbType.Int, 4), new SqlParameter("@VisitFile_Own", SqlDbType.Int, 4), new SqlParameter("@CreateOwnCount_All", SqlDbType.Int, 4), new SqlParameter("@CreateOwnCount_Plan", SqlDbType.Int, 4), new SqlParameter("@CreateOwnCount_TestPaper", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@IsUsed", SqlDbType.Char, 1), new SqlParameter("@StatsVisitClient_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.DateType;
            cmdParms[1].Value = model.DateData;
            cmdParms[2].Value = model.TeacherId;
            cmdParms[3].Value = model.TeacherName;
            cmdParms[4].Value = model.VisitCount_All;
            cmdParms[5].Value = model.VisitCount_Cloud;
            cmdParms[6].Value = model.VisitCount_Own;
            cmdParms[7].Value = model.VisitFile_All;
            cmdParms[8].Value = model.VisitFile_Cloud;
            cmdParms[9].Value = model.VisitFile_Own;
            cmdParms[10].Value = model.CreateOwnCount_All;
            cmdParms[11].Value = model.CreateOwnCount_Plan;
            cmdParms[12].Value = model.CreateOwnCount_TestPaper;
            cmdParms[13].Value = model.CreateTime;
            cmdParms[14].Value = model.IsUsed;
            cmdParms[15].Value = model.StatsVisitClient_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

