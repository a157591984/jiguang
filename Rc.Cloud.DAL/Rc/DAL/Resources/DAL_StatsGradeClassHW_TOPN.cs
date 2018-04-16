namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsGradeClassHW_TOPN
    {
        public bool Add(Model_StatsGradeClassHW_TOPN model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsGradeClassHW_TOPN(");
            builder.Append("StatsGradeClassHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,TOPN,ClassCount,ClassCountRate,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsGradeClassHW_ScoreID,@ResourceToResourceFolder_Id,@Resource_Name,@SchoolID,@SchoolName,@Gradeid,@GradeName,@ClassID,@ClassName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@TOPN,@ClassCount,@ClassCountRate,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsGradeClassHW_ScoreID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@TOPN", SqlDbType.VarChar, 20), new SqlParameter("@ClassCount", SqlDbType.Decimal, 5), new SqlParameter("@ClassCountRate", SqlDbType.Decimal, 5), 
                new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsGradeClassHW_ScoreID;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.Resource_Name;
            cmdParms[3].Value = model.SchoolID;
            cmdParms[4].Value = model.SchoolName;
            cmdParms[5].Value = model.Gradeid;
            cmdParms[6].Value = model.GradeName;
            cmdParms[7].Value = model.ClassID;
            cmdParms[8].Value = model.ClassName;
            cmdParms[9].Value = model.SubjectID;
            cmdParms[10].Value = model.SubjectName;
            cmdParms[11].Value = model.TeacherID;
            cmdParms[12].Value = model.TeacherName;
            cmdParms[13].Value = model.TOPN;
            cmdParms[14].Value = model.ClassCount;
            cmdParms[15].Value = model.ClassCountRate;
            cmdParms[0x10].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsGradeClassHW_TOPN DataRowToModel(DataRow row)
        {
            Model_StatsGradeClassHW_TOPN shw_topn = new Model_StatsGradeClassHW_TOPN();
            if (row != null)
            {
                if (row["StatsGradeClassHW_ScoreID"] != null)
                {
                    shw_topn.StatsGradeClassHW_ScoreID = row["StatsGradeClassHW_ScoreID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    shw_topn.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    shw_topn.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["SchoolID"] != null)
                {
                    shw_topn.SchoolID = row["SchoolID"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    shw_topn.SchoolName = row["SchoolName"].ToString();
                }
                if (row["Gradeid"] != null)
                {
                    shw_topn.Gradeid = row["Gradeid"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    shw_topn.GradeName = row["GradeName"].ToString();
                }
                if (row["ClassID"] != null)
                {
                    shw_topn.ClassID = row["ClassID"].ToString();
                }
                if (row["ClassName"] != null)
                {
                    shw_topn.ClassName = row["ClassName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    shw_topn.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    shw_topn.SubjectName = row["SubjectName"].ToString();
                }
                if (row["TeacherID"] != null)
                {
                    shw_topn.TeacherID = row["TeacherID"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    shw_topn.TeacherName = row["TeacherName"].ToString();
                }
                if (row["TOPN"] != null)
                {
                    shw_topn.TOPN = row["TOPN"].ToString();
                }
                if ((row["ClassCount"] != null) && (row["ClassCount"].ToString() != ""))
                {
                    shw_topn.ClassCount = new decimal?(decimal.Parse(row["ClassCount"].ToString()));
                }
                if ((row["ClassCountRate"] != null) && (row["ClassCountRate"].ToString() != ""))
                {
                    shw_topn.ClassCountRate = new decimal?(decimal.Parse(row["ClassCountRate"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    shw_topn.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return shw_topn;
        }

        public bool Delete(string StatsGradeClassHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeClassHW_TOPN ");
            builder.Append(" where StatsGradeClassHW_ScoreID=@StatsGradeClassHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeClassHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeClassHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsGradeClassHW_ScoreIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeClassHW_TOPN ");
            builder.Append(" where StatsGradeClassHW_ScoreID in (" + StatsGradeClassHW_ScoreIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsGradeClassHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsGradeClassHW_TOPN");
            builder.Append(" where StatsGradeClassHW_ScoreID=@StatsGradeClassHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeClassHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeClassHW_ScoreID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsGradeClassHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,TOPN,ClassCount,ClassCountRate,CreateTime ");
            builder.Append(" FROM StatsGradeClassHW_TOPN ");
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
            builder.Append(" StatsGradeClassHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,TOPN,ClassCount,ClassCountRate,CreateTime ");
            builder.Append(" FROM StatsGradeClassHW_TOPN ");
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
                builder.Append("order by T.StatsGradeClassHW_ScoreID desc");
            }
            builder.Append(")AS Row, T.*  from StatsGradeClassHW_TOPN T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsGradeClassHW_TOPN GetModel(string StatsGradeClassHW_ScoreID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsGradeClassHW_ScoreID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,TOPN,ClassCount,ClassCountRate,CreateTime from StatsGradeClassHW_TOPN ");
            builder.Append(" where StatsGradeClassHW_ScoreID=@StatsGradeClassHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeClassHW_ScoreID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeClassHW_ScoreID;
            new Model_StatsGradeClassHW_TOPN();
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
            builder.Append("select count(1) FROM StatsGradeClassHW_TOPN ");
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

        public bool Update(Model_StatsGradeClassHW_TOPN model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsGradeClassHW_TOPN set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("Resource_Name=@Resource_Name,");
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
            builder.Append("TOPN=@TOPN,");
            builder.Append("ClassCount=@ClassCount,");
            builder.Append("ClassCountRate=@ClassCountRate,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsGradeClassHW_ScoreID=@StatsGradeClassHW_ScoreID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@TOPN", SqlDbType.VarChar, 20), new SqlParameter("@ClassCount", SqlDbType.Decimal, 5), new SqlParameter("@ClassCountRate", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime), 
                new SqlParameter("@StatsGradeClassHW_ScoreID", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.Resource_Name;
            cmdParms[2].Value = model.SchoolID;
            cmdParms[3].Value = model.SchoolName;
            cmdParms[4].Value = model.Gradeid;
            cmdParms[5].Value = model.GradeName;
            cmdParms[6].Value = model.ClassID;
            cmdParms[7].Value = model.ClassName;
            cmdParms[8].Value = model.SubjectID;
            cmdParms[9].Value = model.SubjectName;
            cmdParms[10].Value = model.TeacherID;
            cmdParms[11].Value = model.TeacherName;
            cmdParms[12].Value = model.TOPN;
            cmdParms[13].Value = model.ClassCount;
            cmdParms[14].Value = model.ClassCountRate;
            cmdParms[15].Value = model.CreateTime;
            cmdParms[0x10].Value = model.StatsGradeClassHW_ScoreID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

