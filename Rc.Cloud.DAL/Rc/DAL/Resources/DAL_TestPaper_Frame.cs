namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_TestPaper_Frame
    {
        public bool Add(Model_TestPaper_Frame model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TestPaper_Frame(");
            builder.Append("TestPaper_Frame_Id,TestPaper_Frame_Name,ParticularYear,GradeTerm,Resource_Version,Subject,Remark,TestPaper_FrameType,CreateUser,CreateTime,Analysis)");
            builder.Append(" values (");
            builder.Append("@TestPaper_Frame_Id,@TestPaper_Frame_Name,@ParticularYear,@GradeTerm,@Resource_Version,@Subject,@Remark,@TestPaper_FrameType,@CreateUser,@CreateTime,@Analysis)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_Frame_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@TestPaper_FrameType", SqlDbType.NVarChar, 50), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Analysis", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.TestPaper_Frame_Id;
            cmdParms[1].Value = model.TestPaper_Frame_Name;
            cmdParms[2].Value = model.ParticularYear;
            cmdParms[3].Value = model.GradeTerm;
            cmdParms[4].Value = model.Resource_Version;
            cmdParms[5].Value = model.Subject;
            cmdParms[6].Value = model.Remark;
            cmdParms[7].Value = model.TestPaper_FrameType;
            cmdParms[8].Value = model.CreateUser;
            cmdParms[9].Value = model.CreateTime;
            cmdParms[10].Value = model.Analysis;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_TestPaper_Frame DataRowToModel(DataRow row)
        {
            Model_TestPaper_Frame frame = new Model_TestPaper_Frame();
            if (row != null)
            {
                if (row["TestPaper_Frame_Id"] != null)
                {
                    frame.TestPaper_Frame_Id = row["TestPaper_Frame_Id"].ToString();
                }
                if (row["TestPaper_Frame_Name"] != null)
                {
                    frame.TestPaper_Frame_Name = row["TestPaper_Frame_Name"].ToString();
                }
                if ((row["ParticularYear"] != null) && (row["ParticularYear"].ToString() != ""))
                {
                    frame.ParticularYear = new int?(int.Parse(row["ParticularYear"].ToString()));
                }
                if (row["GradeTerm"] != null)
                {
                    frame.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    frame.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["Subject"] != null)
                {
                    frame.Subject = row["Subject"].ToString();
                }
                if (row["Remark"] != null)
                {
                    frame.Remark = row["Remark"].ToString();
                }
                if (row["TestPaper_FrameType"] != null)
                {
                    frame.TestPaper_FrameType = row["TestPaper_FrameType"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    frame.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    frame.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["Analysis"] != null)
                {
                    frame.Analysis = row["Analysis"].ToString();
                }
            }
            return frame;
        }

        public bool Delete(string TestPaper_Frame_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestPaper_Frame ");
            builder.Append(" where TestPaper_Frame_Id=@TestPaper_Frame_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_Frame_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TestPaper_Frame_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TestPaper_Frame ");
            builder.Append(" where TestPaper_Frame_Id in (" + TestPaper_Frame_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TestPaper_Frame_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from TestPaper_Frame");
            builder.Append(" where TestPaper_Frame_Id=@TestPaper_Frame_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_Frame_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TestPaper_Frame_Id,TestPaper_Frame_Name,ParticularYear,GradeTerm,Resource_Version,Subject,Remark,TestPaper_FrameType,CreateUser,CreateTime,Analysis ");
            builder.Append(" FROM TestPaper_Frame ");
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
            builder.Append(" TestPaper_Frame_Id,TestPaper_Frame_Name,ParticularYear,GradeTerm,Resource_Version,Subject,Remark,TestPaper_FrameType,CreateUser,CreateTime,Analysis ");
            builder.Append(" FROM TestPaper_Frame ");
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
                builder.Append("order by T.TestPaper_Frame_Id desc");
            }
            builder.Append(")AS Row, T.*  from TestPaper_Frame T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_TestPaper_Frame GetModel(string TestPaper_Frame_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TestPaper_Frame_Id,TestPaper_Frame_Name,ParticularYear,GradeTerm,Resource_Version,Subject,Remark,TestPaper_FrameType,CreateUser,CreateTime,Analysis from TestPaper_Frame ");
            builder.Append(" where TestPaper_Frame_Id=@TestPaper_Frame_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = TestPaper_Frame_Id;
            new Model_TestPaper_Frame();
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
            builder.Append("select count(1) FROM TestPaper_Frame ");
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

        public bool Update(Model_TestPaper_Frame model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TestPaper_Frame set ");
            builder.Append("TestPaper_Frame_Name=@TestPaper_Frame_Name,");
            builder.Append("ParticularYear=@ParticularYear,");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("Subject=@Subject,");
            builder.Append("Remark=@Remark,");
            builder.Append("TestPaper_FrameType=@TestPaper_FrameType,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("Analysis=@Analysis");
            builder.Append(" where TestPaper_Frame_Id=@TestPaper_Frame_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_Frame_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@TestPaper_FrameType", SqlDbType.NVarChar, 50), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Analysis", SqlDbType.NVarChar, 200), new SqlParameter("@TestPaper_Frame_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.TestPaper_Frame_Name;
            cmdParms[1].Value = model.ParticularYear;
            cmdParms[2].Value = model.GradeTerm;
            cmdParms[3].Value = model.Resource_Version;
            cmdParms[4].Value = model.Subject;
            cmdParms[5].Value = model.Remark;
            cmdParms[6].Value = model.TestPaper_FrameType;
            cmdParms[7].Value = model.CreateUser;
            cmdParms[8].Value = model.CreateTime;
            cmdParms[9].Value = model.Analysis;
            cmdParms[10].Value = model.TestPaper_Frame_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

