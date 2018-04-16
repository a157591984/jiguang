namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsGradeClassHW_Hierarchy
    {
        public bool Add(Model_StatsGradeClassHW_Hierarchy model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsGradeClassHW_Hierarchy(");
            builder.Append("StatsGradeClassHW_HierarchyID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,Hierarchy,HierarchyCount,HierarchyCountRate,ClassAllCount,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsGradeClassHW_HierarchyID,@ResourceToResourceFolder_Id,@Resource_Name,@SchoolID,@SchoolName,@Gradeid,@GradeName,@ClassID,@ClassName,@SubjectID,@SubjectName,@TeacherID,@TeacherName,@Hierarchy,@HierarchyCount,@HierarchyCountRate,@ClassAllCount,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsGradeClassHW_HierarchyID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@Hierarchy", SqlDbType.Decimal, 5), new SqlParameter("@HierarchyCount", SqlDbType.Decimal, 5), new SqlParameter("@HierarchyCountRate", SqlDbType.Decimal, 5), 
                new SqlParameter("@ClassAllCount", SqlDbType.Decimal, 5), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsGradeClassHW_HierarchyID;
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
            cmdParms[13].Value = model.Hierarchy;
            cmdParms[14].Value = model.HierarchyCount;
            cmdParms[15].Value = model.HierarchyCountRate;
            cmdParms[0x10].Value = model.ClassAllCount;
            cmdParms[0x11].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsGradeClassHW_Hierarchy DataRowToModel(DataRow row)
        {
            Model_StatsGradeClassHW_Hierarchy hierarchy = new Model_StatsGradeClassHW_Hierarchy();
            if (row != null)
            {
                if (row["StatsGradeClassHW_HierarchyID"] != null)
                {
                    hierarchy.StatsGradeClassHW_HierarchyID = row["StatsGradeClassHW_HierarchyID"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    hierarchy.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    hierarchy.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["SchoolID"] != null)
                {
                    hierarchy.SchoolID = row["SchoolID"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    hierarchy.SchoolName = row["SchoolName"].ToString();
                }
                if (row["Gradeid"] != null)
                {
                    hierarchy.Gradeid = row["Gradeid"].ToString();
                }
                if (row["GradeName"] != null)
                {
                    hierarchy.GradeName = row["GradeName"].ToString();
                }
                if (row["ClassID"] != null)
                {
                    hierarchy.ClassID = row["ClassID"].ToString();
                }
                if (row["ClassName"] != null)
                {
                    hierarchy.ClassName = row["ClassName"].ToString();
                }
                if (row["SubjectID"] != null)
                {
                    hierarchy.SubjectID = row["SubjectID"].ToString();
                }
                if (row["SubjectName"] != null)
                {
                    hierarchy.SubjectName = row["SubjectName"].ToString();
                }
                if (row["TeacherID"] != null)
                {
                    hierarchy.TeacherID = row["TeacherID"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    hierarchy.TeacherName = row["TeacherName"].ToString();
                }
                if ((row["Hierarchy"] != null) && (row["Hierarchy"].ToString() != ""))
                {
                    hierarchy.Hierarchy = new decimal?(decimal.Parse(row["Hierarchy"].ToString()));
                }
                if ((row["HierarchyCount"] != null) && (row["HierarchyCount"].ToString() != ""))
                {
                    hierarchy.HierarchyCount = new decimal?(decimal.Parse(row["HierarchyCount"].ToString()));
                }
                if ((row["HierarchyCountRate"] != null) && (row["HierarchyCountRate"].ToString() != ""))
                {
                    hierarchy.HierarchyCountRate = new decimal?(decimal.Parse(row["HierarchyCountRate"].ToString()));
                }
                if ((row["ClassAllCount"] != null) && (row["ClassAllCount"].ToString() != ""))
                {
                    hierarchy.ClassAllCount = new decimal?(decimal.Parse(row["ClassAllCount"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    hierarchy.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return hierarchy;
        }

        public bool Delete(string StatsGradeClassHW_HierarchyID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeClassHW_Hierarchy ");
            builder.Append(" where StatsGradeClassHW_HierarchyID=@StatsGradeClassHW_HierarchyID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeClassHW_HierarchyID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeClassHW_HierarchyID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsGradeClassHW_HierarchyIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsGradeClassHW_Hierarchy ");
            builder.Append(" where StatsGradeClassHW_HierarchyID in (" + StatsGradeClassHW_HierarchyIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsGradeClassHW_HierarchyID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsGradeClassHW_Hierarchy");
            builder.Append(" where StatsGradeClassHW_HierarchyID=@StatsGradeClassHW_HierarchyID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeClassHW_HierarchyID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeClassHW_HierarchyID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsGradeClassHW_HierarchyID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,Hierarchy,HierarchyCount,HierarchyCountRate,ClassAllCount,CreateTime ");
            builder.Append(" FROM StatsGradeClassHW_Hierarchy ");
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
            builder.Append(" StatsGradeClassHW_HierarchyID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,Hierarchy,HierarchyCount,HierarchyCountRate,ClassAllCount,CreateTime ");
            builder.Append(" FROM StatsGradeClassHW_Hierarchy ");
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
                builder.Append("order by T.StatsGradeClassHW_HierarchyID desc");
            }
            builder.Append(")AS Row, T.*  from StatsGradeClassHW_Hierarchy T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsGradeClassHW_Hierarchy GetModel(string StatsGradeClassHW_HierarchyID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsGradeClassHW_HierarchyID,ResourceToResourceFolder_Id,Resource_Name,SchoolID,SchoolName,Gradeid,GradeName,ClassID,ClassName,SubjectID,SubjectName,TeacherID,TeacherName,Hierarchy,HierarchyCount,HierarchyCountRate,ClassAllCount,CreateTime from StatsGradeClassHW_Hierarchy ");
            builder.Append(" where StatsGradeClassHW_HierarchyID=@StatsGradeClassHW_HierarchyID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsGradeClassHW_HierarchyID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsGradeClassHW_HierarchyID;
            new Model_StatsGradeClassHW_Hierarchy();
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
            builder.Append("select count(1) FROM StatsGradeClassHW_Hierarchy ");
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

        public bool Update(Model_StatsGradeClassHW_Hierarchy model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsGradeClassHW_Hierarchy set ");
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
            builder.Append("Hierarchy=@Hierarchy,");
            builder.Append("HierarchyCount=@HierarchyCount,");
            builder.Append("HierarchyCountRate=@HierarchyCountRate,");
            builder.Append("ClassAllCount=@ClassAllCount,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsGradeClassHW_HierarchyID=@StatsGradeClassHW_HierarchyID ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@SchoolID", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 250), new SqlParameter("@Gradeid", SqlDbType.VarChar, 10), new SqlParameter("@GradeName", SqlDbType.NVarChar, 250), new SqlParameter("@ClassID", SqlDbType.VarChar, 10), new SqlParameter("@ClassName", SqlDbType.NVarChar, 250), new SqlParameter("@SubjectID", SqlDbType.Char, 0x24), new SqlParameter("@SubjectName", SqlDbType.NVarChar, 250), new SqlParameter("@TeacherID", SqlDbType.Char, 0x24), new SqlParameter("@TeacherName", SqlDbType.NVarChar, 250), new SqlParameter("@Hierarchy", SqlDbType.Decimal, 5), new SqlParameter("@HierarchyCount", SqlDbType.Decimal, 5), new SqlParameter("@HierarchyCountRate", SqlDbType.Decimal, 5), new SqlParameter("@ClassAllCount", SqlDbType.Decimal, 5), 
                new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsGradeClassHW_HierarchyID", SqlDbType.Char, 0x24)
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
            cmdParms[12].Value = model.Hierarchy;
            cmdParms[13].Value = model.HierarchyCount;
            cmdParms[14].Value = model.HierarchyCountRate;
            cmdParms[15].Value = model.ClassAllCount;
            cmdParms[0x10].Value = model.CreateTime;
            cmdParms[0x11].Value = model.StatsGradeClassHW_HierarchyID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

