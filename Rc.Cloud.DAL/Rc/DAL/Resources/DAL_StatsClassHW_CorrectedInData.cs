namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsClassHW_CorrectedInData
    {
        public bool Add(Model_StatsClassHW_CorrectedInData model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsClassHW_CorrectedInData(");
            builder.Append("StatsClassHW_CorrectedInDataID,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,AssignedCount,CommittedCount,CommittedCountRate,UncommittedCount,CorrectedCount,UnCorrectedCount,CorrectedCountRate,ClassAllCount,CreateTime,DateType,DateData)");
            builder.Append(" values (");
            builder.Append("@StatsClassHW_CorrectedInDataID,@SchoolID,@SchoolName,@Gradeid,@GradeName,@ClassID,@ClassName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@AssignedCount,@CommittedCount,@CommittedCountRate,@UncommittedCount,@CorrectedCount,@UnCorrectedCount,@CorrectedCountRate,@ClassAllCount,@CreateTime,@DateType,@DateData)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsClassHW_CorrectedInDataID", SqlDbType.Char, 0x24), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@AssignedCount", SqlDbType.Decimal, 5), new SqlParameter("@CommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@CommittedCountRate", SqlDbType.Decimal, 5), new SqlParameter("@UncommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCount", SqlDbType.Decimal, 5), 
                new SqlParameter("@UnCorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCountRate", SqlDbType.Decimal, 5), new SqlParameter("@ClassAllCount", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20)
             };
            cmdParms[0].Value = model.StatsClassHW_CorrectedInDataID;
            cmdParms[1].Value = model.SchoolID;
            cmdParms[2].Value = model.SchoolName;
            cmdParms[3].Value = model.Gradeid;
            cmdParms[4].Value = model.GradeName;
            cmdParms[5].Value = model.ClassID;
            cmdParms[6].Value = model.ClassName;
            cmdParms[7].Value = model.SubjectID;
            cmdParms[8].Value = model.SubjectName;
            cmdParms[9].Value = model.TeacherID;
            cmdParms[10].Value = model.TeacherName;
            cmdParms[11].Value = model.AssignedCount;
            cmdParms[12].Value = model.CommittedCount;
            cmdParms[13].Value = model.CommittedCountRate;
            cmdParms[14].Value = model.UncommittedCount;
            cmdParms[15].Value = model.CorrectedCount;
            cmdParms[0x10].Value = model.UnCorrectedCount;
            cmdParms[0x11].Value = model.CorrectedCountRate;
            cmdParms[0x12].Value = model.ClassAllCount;
            cmdParms[0x13].Value = model.CreateTime;
            cmdParms[20].Value = model.DateType;
            cmdParms[0x15].Value = model.DateData;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsClassHW_CorrectedInData DataRowToModel(DataRow row)
        {
            Model_StatsClassHW_CorrectedInData data = new Model_StatsClassHW_CorrectedInData();
            if (row != null)
            {
                if (row["StatsClassHW_CorrectedInDataID"] != null)
                {
                    data.StatsClassHW_CorrectedInDataID = row["StatsClassHW_CorrectedInDataID"].ToString();
                }
                if (row["SchoolID"] != null)
                {
                    data.SchoolID = row["SchoolID"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    data.SchoolName = row["SchoolName"].ToString();
                }
                if (row["Gradeid"] != null)
                {
                    data.Gradeid = row["Gradeid"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    data.GradeName = row["GradeName"].ToString();
                }
                if (row["ClassID"] != null)
                {
                    data.ClassID = row["ClassID"].ToString();
                }
                if (row["ClassName"] != null)
                {
                    data.ClassName = row["ClassName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    data.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    data.SubjectName = row["SubjectName"].ToString();
                }
                if (row["TeacherID"] != null)
                {
                    data.TeacherID = row["TeacherID"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    data.TeacherName = row["TeacherName"].ToString();
                }
                if ((row["AssignedCount"] != null) && (row["AssignedCount"].ToString() != ""))
                {
                    data.AssignedCount = new decimal?(decimal.Parse(row["AssignedCount"].ToString()));
                }
                if ((row["CommittedCount"] != null) && (row["CommittedCount"].ToString() != ""))
                {
                    data.CommittedCount = new decimal?(decimal.Parse(row["CommittedCount"].ToString()));
                }
                if ((row["CommittedCountRate"] != null) && (row["CommittedCountRate"].ToString() != ""))
                {
                    data.CommittedCountRate = new decimal?(decimal.Parse(row["CommittedCountRate"].ToString()));
                }
                if ((row["UncommittedCount"] != null) && (row["UncommittedCount"].ToString() != ""))
                {
                    data.UncommittedCount = new decimal?(decimal.Parse(row["UncommittedCount"].ToString()));
                }
                if ((row["CorrectedCount"] != null) && (row["CorrectedCount"].ToString() != ""))
                {
                    data.CorrectedCount = new decimal?(decimal.Parse(row["CorrectedCount"].ToString()));
                }
                if ((row["UnCorrectedCount"] != null) && (row["UnCorrectedCount"].ToString() != ""))
                {
                    data.UnCorrectedCount = new decimal?(decimal.Parse(row["UnCorrectedCount"].ToString()));
                }
                if ((row["CorrectedCountRate"] != null) && (row["CorrectedCountRate"].ToString() != ""))
                {
                    data.CorrectedCountRate = new decimal?(decimal.Parse(row["CorrectedCountRate"].ToString()));
                }
                if ((row["ClassAllCount"] != null) && (row["ClassAllCount"].ToString() != ""))
                {
                    data.ClassAllCount = new decimal?(decimal.Parse(row["ClassAllCount"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    data.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["DateType"] != null)
                {
                    data.DateType = row["DateType"].ToString();
                }
                if (row["DateData"] != null)
                {
                    data.DateData = row["DateData"].ToString();
                }
            }
            return data;
        }

        public bool Delete(string StatsClassHW_CorrectedInDataID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsClassHW_CorrectedInData ");
            builder.Append(" where StatsClassHW_CorrectedInDataID=@StatsClassHW_CorrectedInDataID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_CorrectedInDataID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_CorrectedInDataID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsClassHW_CorrectedInDataIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsClassHW_CorrectedInData ");
            builder.Append(" where StatsClassHW_CorrectedInDataID in (" + StatsClassHW_CorrectedInDataIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsClassHW_CorrectedInDataID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsClassHW_CorrectedInData");
            builder.Append(" where StatsClassHW_CorrectedInDataID=@StatsClassHW_CorrectedInDataID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_CorrectedInDataID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_CorrectedInDataID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsClassHW_CorrectedInDataID,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,AssignedCount,CommittedCount,CommittedCountRate,UncommittedCount,CorrectedCount,UnCorrectedCount,CorrectedCountRate,ClassAllCount,CreateTime,DateType,DateData ");
            builder.Append(" FROM StatsClassHW_CorrectedInData ");
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
            builder.Append(" StatsClassHW_CorrectedInDataID,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,AssignedCount,CommittedCount,CommittedCountRate,UncommittedCount,CorrectedCount,UnCorrectedCount,CorrectedCountRate,ClassAllCount,CreateTime,DateType,DateData ");
            builder.Append(" FROM StatsClassHW_CorrectedInData ");
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
                builder.Append("order by T.StatsClassHW_CorrectedInDataID desc");
            }
            builder.Append(")AS Row, T.*  from StatsClassHW_CorrectedInData T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsClassHW_CorrectedInData GetModel(string StatsClassHW_CorrectedInDataID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsClassHW_CorrectedInDataID,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,AssignedCount,CommittedCount,CommittedCountRate,UncommittedCount,CorrectedCount,UnCorrectedCount,CorrectedCountRate,ClassAllCount,CreateTime,DateType,DateData from StatsClassHW_CorrectedInData ");
            builder.Append(" where StatsClassHW_CorrectedInDataID=@StatsClassHW_CorrectedInDataID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsClassHW_CorrectedInDataID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsClassHW_CorrectedInDataID;
            new Model_StatsClassHW_CorrectedInData();
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
            builder.Append("select count(1) FROM StatsClassHW_CorrectedInData ");
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

        public bool Update(Model_StatsClassHW_CorrectedInData model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsClassHW_CorrectedInData set ");
            builder.Append("SchoolID=@SchoolID,");
            builder.Append("SchoolName=@SchoolName,");
            builder.Append("Gradeid=@Gradeid,");
            builder.Append("GradeName=@GradeName,");
            builder.Append("ClassID=@ClassID,");
            builder.Append("ClassName=@ClassName,");
            builder.Append("SubjectID=@SubjectID,");
            builder.Append("SubjectName=@SubjectName,");
            builder.Append("TeacherID=@TeacherID,");
            builder.Append("TeacherName=@TeacherName,");
            builder.Append("AssignedCount=@AssignedCount,");
            builder.Append("CommittedCount=@CommittedCount,");
            builder.Append("CommittedCountRate=@CommittedCountRate,");
            builder.Append("UncommittedCount=@UncommittedCount,");
            builder.Append("CorrectedCount=@CorrectedCount,");
            builder.Append("UnCorrectedCount=@UnCorrectedCount,");
            builder.Append("CorrectedCountRate=@CorrectedCountRate,");
            builder.Append("ClassAllCount=@ClassAllCount,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("DateType=@DateType,");
            builder.Append("DateData=@DateData");
            builder.Append(" where StatsClassHW_CorrectedInDataID=@StatsClassHW_CorrectedInDataID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@AssignedCount", SqlDbType.Decimal, 5), new SqlParameter("@CommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@CommittedCountRate", SqlDbType.Decimal, 5), new SqlParameter("@UncommittedCount", SqlDbType.Decimal, 5), new SqlParameter("@CorrectedCount", SqlDbType.Decimal, 5), new SqlParameter("@UnCorrectedCount", SqlDbType.Decimal, 5), 
                new SqlParameter("@CorrectedCountRate", SqlDbType.Decimal, 5), new SqlParameter("@ClassAllCount", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20), new SqlParameter("@StatsClassHW_CorrectedInDataID", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.SchoolID;
            cmdParms[1].Value = model.SchoolName;
            cmdParms[2].Value = model.Gradeid;
            cmdParms[3].Value = model.GradeName;
            cmdParms[4].Value = model.ClassID;
            cmdParms[5].Value = model.ClassName;
            cmdParms[6].Value = model.SubjectID;
            cmdParms[7].Value = model.SubjectName;
            cmdParms[8].Value = model.TeacherID;
            cmdParms[9].Value = model.TeacherName;
            cmdParms[10].Value = model.AssignedCount;
            cmdParms[11].Value = model.CommittedCount;
            cmdParms[12].Value = model.CommittedCountRate;
            cmdParms[13].Value = model.UncommittedCount;
            cmdParms[14].Value = model.CorrectedCount;
            cmdParms[15].Value = model.UnCorrectedCount;
            cmdParms[0x10].Value = model.CorrectedCountRate;
            cmdParms[0x11].Value = model.ClassAllCount;
            cmdParms[0x12].Value = model.CreateTime;
            cmdParms[0x13].Value = model.DateType;
            cmdParms[20].Value = model.DateData;
            cmdParms[0x15].Value = model.StatsClassHW_CorrectedInDataID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

