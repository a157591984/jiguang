namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_ResourceToResourceFolder
    {
        public bool Add(Model_ResourceToResourceFolder model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ResourceToResourceFolder(");
            builder.Append("ResourceToResourceFolder_Id,ResourceFolder_Id,Resource_Id,File_Name,Resource_Type,Resource_Name,Resource_Class,Resource_Version,File_Owner,CreateFUser,CreateTime,UpdateTime,File_Suffix,LessonPlan_Type,GradeTerm,Subject,Resource_Domain,Resource_Url,Resource_shared,Book_ID,ParticularYear,ResourceToResourceFolder_Order)");
            builder.Append(" values (");
            builder.Append("@ResourceToResourceFolder_Id,@ResourceFolder_Id,@Resource_Id,@File_Name,@Resource_Type,@Resource_Name,@Resource_Class,@Resource_Version,@File_Owner,@CreateFUser,@CreateTime,@UpdateTime,@File_Suffix,@LessonPlan_Type,@GradeTerm,@Subject,@Resource_Domain,@Resource_Url,@Resource_shared,@Book_ID,@ParticularYear,@ResourceToResourceFolder_Order)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24), new SqlParameter("@File_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@File_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@File_Suffix", SqlDbType.VarChar, 10), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), 
                new SqlParameter("@Resource_Domain", SqlDbType.NVarChar, 100), new SqlParameter("@Resource_Url", SqlDbType.VarChar, 500), new SqlParameter("@Resource_shared", SqlDbType.VarChar, 10), new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@ResourceToResourceFolder_Order", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.ResourceFolder_Id;
            cmdParms[2].Value = model.Resource_Id;
            cmdParms[3].Value = model.File_Name;
            cmdParms[4].Value = model.Resource_Type;
            cmdParms[5].Value = model.Resource_Name;
            cmdParms[6].Value = model.Resource_Class;
            cmdParms[7].Value = model.Resource_Version;
            cmdParms[8].Value = model.File_Owner;
            cmdParms[9].Value = model.CreateFUser;
            cmdParms[10].Value = model.CreateTime;
            cmdParms[11].Value = model.UpdateTime;
            cmdParms[12].Value = model.File_Suffix;
            cmdParms[13].Value = model.LessonPlan_Type;
            cmdParms[14].Value = model.GradeTerm;
            cmdParms[15].Value = model.Subject;
            cmdParms[0x10].Value = model.Resource_Domain;
            cmdParms[0x11].Value = model.Resource_Url;
            cmdParms[0x12].Value = model.Resource_shared;
            cmdParms[0x13].Value = model.Book_ID;
            cmdParms[20].Value = model.ParticularYear;
            cmdParms[0x15].Value = model.ResourceToResourceFolder_Order;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_ResourceToResourceFolder DataRowToModel(DataRow row)
        {
            Model_ResourceToResourceFolder folder = new Model_ResourceToResourceFolder();
            if (row != null)
            {
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    folder.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["ResourceFolder_Id"] != null)
                {
                    folder.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["Resource_Id"] != null)
                {
                    folder.Resource_Id = row["Resource_Id"].ToString();
                }
                if (row["File_Name"] != null)
                {
                    folder.File_Name = row["File_Name"].ToString();
                }
                if (row["Resource_Type"] != null)
                {
                    folder.Resource_Type = row["Resource_Type"].ToString();
                }
                if (row["Resource_Name"] != null)
                {
                    folder.Resource_Name = row["Resource_Name"].ToString();
                }
                if (row["Resource_Class"] != null)
                {
                    folder.Resource_Class = row["Resource_Class"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    folder.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["File_Owner"] != null)
                {
                    folder.File_Owner = row["File_Owner"].ToString();
                }
                if (row["CreateFUser"] != null)
                {
                    folder.CreateFUser = row["CreateFUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    folder.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    folder.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
                if (row["File_Suffix"] != null)
                {
                    folder.File_Suffix = row["File_Suffix"].ToString();
                }
                if (row["LessonPlan_Type"] != null)
                {
                    folder.LessonPlan_Type = row["LessonPlan_Type"].ToString();
                }
                if (row["GradeTerm"] != null)
                {
                    folder.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Subject"] != null)
                {
                    folder.Subject = row["Subject"].ToString();
                }
                if (row["Resource_Domain"] != null)
                {
                    folder.Resource_Domain = row["Resource_Domain"].ToString();
                }
                if (row["Resource_Url"] != null)
                {
                    folder.Resource_Url = row["Resource_Url"].ToString();
                }
                if (row["Resource_shared"] != null)
                {
                    folder.Resource_shared = row["Resource_shared"].ToString();
                }
                if (row["Book_ID"] != null)
                {
                    folder.Book_ID = row["Book_ID"].ToString();
                }
                if ((row["ParticularYear"] != null) && (row["ParticularYear"].ToString() != ""))
                {
                    folder.ParticularYear = new int?(int.Parse(row["ParticularYear"].ToString()));
                }
                if ((row["ResourceToResourceFolder_Order"] != null) && (row["ResourceToResourceFolder_Order"].ToString() != ""))
                {
                    folder.ResourceToResourceFolder_Order = new int?(int.Parse(row["ResourceToResourceFolder_Order"].ToString()));
                }
            }
            return folder;
        }

        public bool Delete(string ResourceToResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ResourceToResourceFolder ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ResourceToResourceFolder_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ResourceToResourceFolder ");
            builder.Append(" where ResourceToResourceFolder_Id in (" + ResourceToResourceFolder_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ResourceToResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ResourceToResourceFolder");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string ResourceFolder_Id, string Resource_Name)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ResourceToResourceFolder");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id \r\nand Resource_Name=@Resource_Name ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250) };
            cmdParms[0].Value = ResourceFolder_Id;
            cmdParms[1].Value = Resource_Name;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string ResourceFolder_Id, string Resource_Name, string ResourceToResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ResourceToResourceFolder");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id \r\nand Resource_Name=@Resource_Name \r\nand ResourceToResourceFolder_Id<>@ResourceToResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            cmdParms[1].Value = Resource_Name;
            cmdParms[2].Value = ResourceToResourceFolder_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceToResourceFolder_Id,ResourceFolder_Id,Resource_Id,File_Name,Resource_Type,Resource_Name,Resource_Class,Resource_Version,File_Owner,CreateFUser,CreateTime,UpdateTime,File_Suffix,LessonPlan_Type,GradeTerm,Subject,Resource_Domain,Resource_Url,Resource_shared,Book_ID,ParticularYear,ResourceToResourceFolder_Order ");
            builder.Append(" FROM ResourceToResourceFolder ");
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
            builder.Append(" ResourceToResourceFolder_Id,ResourceFolder_Id,Resource_Id,File_Name,Resource_Type,Resource_Name,Resource_Class,Resource_Version,File_Owner,CreateFUser,CreateTime,UpdateTime,File_Suffix,LessonPlan_Type,GradeTerm,Subject,Resource_Domain,Resource_Url,Resource_shared,Book_ID,ParticularYear,ResourceToResourceFolder_Order ");
            builder.Append(" FROM ResourceToResourceFolder ");
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
                builder.Append("order by T.ResourceToResourceFolder_Id desc");
            }
            builder.Append(")AS Row, T.*  from ResourceToResourceFolder T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_ResourceToResourceFolder GetModel(string ResourceToResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ResourceToResourceFolder_Id,ResourceFolder_Id,Resource_Id,File_Name,Resource_Type,Resource_Name,Resource_Class,Resource_Version,File_Owner,CreateFUser,CreateTime,UpdateTime,File_Suffix,LessonPlan_Type,GradeTerm,Subject,Resource_Domain,Resource_Url,Resource_shared,Book_ID,ParticularYear,ResourceToResourceFolder_Order from ResourceToResourceFolder ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            new Model_ResourceToResourceFolder();
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
            builder.Append("select count(1) FROM ResourceToResourceFolder ");
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

        public bool Update(Model_ResourceToResourceFolder model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ResourceToResourceFolder set ");
            builder.Append("ResourceFolder_Id=@ResourceFolder_Id,");
            builder.Append("Resource_Id=@Resource_Id,");
            builder.Append("File_Name=@File_Name,");
            builder.Append("Resource_Type=@Resource_Type,");
            builder.Append("Resource_Name=@Resource_Name,");
            builder.Append("Resource_Class=@Resource_Class,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("File_Owner=@File_Owner,");
            builder.Append("CreateFUser=@CreateFUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UpdateTime=@UpdateTime,");
            builder.Append("File_Suffix=@File_Suffix,");
            builder.Append("LessonPlan_Type=@LessonPlan_Type,");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("Resource_Domain=@Resource_Domain,");
            builder.Append("Resource_Url=@Resource_Url,");
            builder.Append("Resource_shared=@Resource_shared,");
            builder.Append("Book_ID=@Book_ID,");
            builder.Append("ParticularYear=@ParticularYear,");
            builder.Append("ResourceToResourceFolder_Order=@ResourceToResourceFolder_Order");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24), new SqlParameter("@File_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@File_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@File_Suffix", SqlDbType.VarChar, 10), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Domain", SqlDbType.NVarChar, 100), 
                new SqlParameter("@Resource_Url", SqlDbType.VarChar, 500), new SqlParameter("@Resource_shared", SqlDbType.VarChar, 10), new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@ResourceToResourceFolder_Order", SqlDbType.Int, 4), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceFolder_Id;
            cmdParms[1].Value = model.Resource_Id;
            cmdParms[2].Value = model.File_Name;
            cmdParms[3].Value = model.Resource_Type;
            cmdParms[4].Value = model.Resource_Name;
            cmdParms[5].Value = model.Resource_Class;
            cmdParms[6].Value = model.Resource_Version;
            cmdParms[7].Value = model.File_Owner;
            cmdParms[8].Value = model.CreateFUser;
            cmdParms[9].Value = model.CreateTime;
            cmdParms[10].Value = model.UpdateTime;
            cmdParms[11].Value = model.File_Suffix;
            cmdParms[12].Value = model.LessonPlan_Type;
            cmdParms[13].Value = model.GradeTerm;
            cmdParms[14].Value = model.Subject;
            cmdParms[15].Value = model.Resource_Domain;
            cmdParms[0x10].Value = model.Resource_Url;
            cmdParms[0x11].Value = model.Resource_shared;
            cmdParms[0x12].Value = model.Book_ID;
            cmdParms[0x13].Value = model.ParticularYear;
            cmdParms[20].Value = model.ResourceToResourceFolder_Order;
            cmdParms[0x15].Value = model.ResourceToResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

