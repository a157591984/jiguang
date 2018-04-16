namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsVisitWeb
    {
        public bool Add(Model_StatsVisitWeb model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsVisitWeb(");
            builder.Append("StatsVisitWeb_Id,DateType,DateData,SchoolId,SchoolName,GradeId,GradeName,ClassId,ClassName,TeacherId,TeacherName,VisitCount_All,VisitCount_Cloud,VisitCount_Own,VisitFile_All,VisitFile_Cloud,VisitFile_Own,VistiDuration_Count,VistiDuration_Avg,CreateTime,IsUsed)");
            builder.Append(" values (");
            builder.Append("@StatsVisitWeb_Id,@DateType,@DateData,@SchoolId,@SchoolName,@GradeId,@GradeName,@ClassId,@ClassName,@TeacherId,@TeacherName,@VisitCount_All,@VisitCount_Cloud,@VisitCount_Own,@VisitFile_All,@VisitFile_Cloud,@VisitFile_Own,@VistiDuration_Count,@VistiDuration_Avg,@CreateTime,@IsUsed)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsVisitWeb_Id", SqlDbType.Char, 0x24), new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20), new SqlParameter("@SchoolId", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 200), new SqlParameter("@GradeId", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 200), new SqlParameter("@ClassId", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 200), new SqlParameter("@TeacherId", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 200), new SqlParameter("@VisitCount_All", SqlDbType.Int, 4), new SqlParameter("@VisitCount_Cloud", SqlDbType.Int, 4), new SqlParameter("@VisitCount_Own", SqlDbType.Int, 4), new SqlParameter("@VisitFile_All", SqlDbType.Int, 4), new SqlParameter("@VisitFile_Cloud", SqlDbType.Int, 4), 
                new SqlParameter("@VisitFile_Own", SqlDbType.Int, 4), new SqlParameter("@VistiDuration_Count", SqlDbType.Int, 4), new SqlParameter("@VistiDuration_Avg", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@IsUsed", SqlDbType.Char, 1)
             };
            cmdParms[0].Value = model.StatsVisitWeb_Id;
            cmdParms[1].Value = model.DateType;
            cmdParms[2].Value = model.DateData;
            cmdParms[3].Value = model.SchoolId;
            cmdParms[4].Value = model.SchoolName;
            cmdParms[5].Value = model.GradeId;
            cmdParms[6].Value = model.GradeName;
            cmdParms[7].Value = model.ClassId;
            cmdParms[8].Value = model.ClassName;
            cmdParms[9].Value = model.TeacherId;
            cmdParms[10].Value = model.TeacherName;
            cmdParms[11].Value = model.VisitCount_All;
            cmdParms[12].Value = model.VisitCount_Cloud;
            cmdParms[13].Value = model.VisitCount_Own;
            cmdParms[14].Value = model.VisitFile_All;
            cmdParms[15].Value = model.VisitFile_Cloud;
            cmdParms[0x10].Value = model.VisitFile_Own;
            cmdParms[0x11].Value = model.VistiDuration_Count;
            cmdParms[0x12].Value = model.VistiDuration_Avg;
            cmdParms[0x13].Value = model.CreateTime;
            cmdParms[20].Value = model.IsUsed;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsVisitWeb DataRowToModel(DataRow row)
        {
            Model_StatsVisitWeb web = new Model_StatsVisitWeb();
            if (row != null)
            {
                if (row["StatsVisitWeb_Id"] != null)
                {
                    web.StatsVisitWeb_Id = row["StatsVisitWeb_Id"].ToString();
                }
                if (row["DateType"] != null)
                {
                    web.DateType = row["DateType"].ToString();
                }
                if (row["DateData"] != null)
                {
                    web.DateData = row["DateData"].ToString();
                }
                if (row["SchoolId"] != null)
                {
                    web.SchoolId = row["SchoolId"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    web.SchoolName = row["SchoolName"].ToString();
                }
                if (row["GradeId"] != null)
                {
                    web.GradeId = row["GradeId"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    web.GradeName = row["GradeName"].ToString();
                }
                if (row["ClassId"] != null)
                {
                    web.ClassId = row["ClassId"].ToString();
                }
                if (row["ClassName"] != null)
                {
                    web.ClassName = row["ClassName"].ToString();
                }
                if (row["TeacherId"] != null)
                {
                    web.TeacherId = row["TeacherId"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    web.TeacherName = row["TeacherName"].ToString();
                }
                if ((row["VisitCount_All"] != null) && (row["VisitCount_All"].ToString() != ""))
                {
                    web.VisitCount_All = new int?(int.Parse(row["VisitCount_All"].ToString()));
                }
                if ((row["VisitCount_Cloud"] != null) && (row["VisitCount_Cloud"].ToString() != ""))
                {
                    web.VisitCount_Cloud = new int?(int.Parse(row["VisitCount_Cloud"].ToString()));
                }
                if ((row["VisitCount_Own"] != null) && (row["VisitCount_Own"].ToString() != ""))
                {
                    web.VisitCount_Own = new int?(int.Parse(row["VisitCount_Own"].ToString()));
                }
                if ((row["VisitFile_All"] != null) && (row["VisitFile_All"].ToString() != ""))
                {
                    web.VisitFile_All = new int?(int.Parse(row["VisitFile_All"].ToString()));
                }
                if ((row["VisitFile_Cloud"] != null) && (row["VisitFile_Cloud"].ToString() != ""))
                {
                    web.VisitFile_Cloud = new int?(int.Parse(row["VisitFile_Cloud"].ToString()));
                }
                if ((row["VisitFile_Own"] != null) && (row["VisitFile_Own"].ToString() != ""))
                {
                    web.VisitFile_Own = new int?(int.Parse(row["VisitFile_Own"].ToString()));
                }
                if ((row["VistiDuration_Count"] != null) && (row["VistiDuration_Count"].ToString() != ""))
                {
                    web.VistiDuration_Count = new int?(int.Parse(row["VistiDuration_Count"].ToString()));
                }
                if ((row["VistiDuration_Avg"] != null) && (row["VistiDuration_Avg"].ToString() != ""))
                {
                    web.VistiDuration_Avg = new int?(int.Parse(row["VistiDuration_Avg"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    web.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["IsUsed"] != null)
                {
                    web.IsUsed = row["IsUsed"].ToString();
                }
            }
            return web;
        }

        public bool Delete(string StatsVisitWeb_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsVisitWeb ");
            builder.Append(" where StatsVisitWeb_Id=@StatsVisitWeb_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsVisitWeb_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsVisitWeb_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsVisitWeb_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsVisitWeb ");
            builder.Append(" where StatsVisitWeb_Id in (" + StatsVisitWeb_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsVisitWeb_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsVisitWeb");
            builder.Append(" where StatsVisitWeb_Id=@StatsVisitWeb_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsVisitWeb_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsVisitWeb_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsVisitWeb_Id,DateType,DateData,SchoolId,SchoolName,GradeId,GradeName,ClassId,ClassName,TeacherId,TeacherName,VisitCount_All,VisitCount_Cloud,VisitCount_Own,VisitFile_All,VisitFile_Cloud,VisitFile_Own,VistiDuration_Count,VistiDuration_Avg,CreateTime,IsUsed ");
            builder.Append(" FROM StatsVisitWeb ");
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
            builder.Append(" StatsVisitWeb_Id,DateType,DateData,SchoolId,SchoolName,GradeId,GradeName,ClassId,ClassName,TeacherId,TeacherName,VisitCount_All,VisitCount_Cloud,VisitCount_Own,VisitFile_All,VisitFile_Cloud,VisitFile_Own,VistiDuration_Count,VistiDuration_Avg,CreateTime,IsUsed ");
            builder.Append(" FROM StatsVisitWeb ");
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
                builder.Append("order by T.StatsVisitWeb_Id desc");
            }
            builder.Append(")AS Row, T.*  from StatsVisitWeb T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsVisitWeb GetModel(string StatsVisitWeb_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsVisitWeb_Id,DateType,DateData,SchoolId,SchoolName,GradeId,GradeName,ClassId,ClassName,TeacherId,TeacherName,VisitCount_All,VisitCount_Cloud,VisitCount_Own,VisitFile_All,VisitFile_Cloud,VisitFile_Own,VistiDuration_Count,VistiDuration_Avg,CreateTime,IsUsed from StatsVisitWeb ");
            builder.Append(" where StatsVisitWeb_Id=@StatsVisitWeb_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsVisitWeb_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsVisitWeb_Id;
            new Model_StatsVisitWeb();
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
            builder.Append("select count(1) FROM StatsVisitWeb ");
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

        public bool Update(Model_StatsVisitWeb model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsVisitWeb set ");
            builder.Append("DateType=@DateType,");
            builder.Append("DateData=@DateData,");
            builder.Append("SchoolId=@SchoolId,");
            builder.Append("SchoolName=@SchoolName,");
            builder.Append("GradeId=@GradeId,");
            builder.Append("GradeName=@GradeName,");
            builder.Append("ClassId=@ClassId,");
            builder.Append("ClassName=@ClassName,");
            builder.Append("TeacherId=@TeacherId,");
            builder.Append("TeacherName=@TeacherName,");
            builder.Append("VisitCount_All=@VisitCount_All,");
            builder.Append("VisitCount_Cloud=@VisitCount_Cloud,");
            builder.Append("VisitCount_Own=@VisitCount_Own,");
            builder.Append("VisitFile_All=@VisitFile_All,");
            builder.Append("VisitFile_Cloud=@VisitFile_Cloud,");
            builder.Append("VisitFile_Own=@VisitFile_Own,");
            builder.Append("VistiDuration_Count=@VistiDuration_Count,");
            builder.Append("VistiDuration_Avg=@VistiDuration_Avg,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("IsUsed=@IsUsed");
            builder.Append(" where StatsVisitWeb_Id=@StatsVisitWeb_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20), new SqlParameter("@SchoolId", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 200), new SqlParameter("@GradeId", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 200), new SqlParameter("@ClassId", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 200), new SqlParameter("@TeacherId", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 200), new SqlParameter("@VisitCount_All", SqlDbType.Int, 4), new SqlParameter("@VisitCount_Cloud", SqlDbType.Int, 4), new SqlParameter("@VisitCount_Own", SqlDbType.Int, 4), new SqlParameter("@VisitFile_All", SqlDbType.Int, 4), new SqlParameter("@VisitFile_Cloud", SqlDbType.Int, 4), new SqlParameter("@VisitFile_Own", SqlDbType.Int, 4), 
                new SqlParameter("@VistiDuration_Count", SqlDbType.Int, 4), new SqlParameter("@VistiDuration_Avg", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@IsUsed", SqlDbType.Char, 1), new SqlParameter("@StatsVisitWeb_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.DateType;
            cmdParms[1].Value = model.DateData;
            cmdParms[2].Value = model.SchoolId;
            cmdParms[3].Value = model.SchoolName;
            cmdParms[4].Value = model.GradeId;
            cmdParms[5].Value = model.GradeName;
            cmdParms[6].Value = model.ClassId;
            cmdParms[7].Value = model.ClassName;
            cmdParms[8].Value = model.TeacherId;
            cmdParms[9].Value = model.TeacherName;
            cmdParms[10].Value = model.VisitCount_All;
            cmdParms[11].Value = model.VisitCount_Cloud;
            cmdParms[12].Value = model.VisitCount_Own;
            cmdParms[13].Value = model.VisitFile_All;
            cmdParms[14].Value = model.VisitFile_Cloud;
            cmdParms[15].Value = model.VisitFile_Own;
            cmdParms[0x10].Value = model.VistiDuration_Count;
            cmdParms[0x11].Value = model.VistiDuration_Avg;
            cmdParms[0x12].Value = model.CreateTime;
            cmdParms[0x13].Value = model.IsUsed;
            cmdParms[20].Value = model.StatsVisitWeb_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

