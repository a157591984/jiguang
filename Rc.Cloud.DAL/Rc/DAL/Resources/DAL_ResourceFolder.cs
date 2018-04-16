﻿namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_ResourceFolder
    {
        public bool Add(Model_ResourceFolder model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ResourceFolder(");
            builder.Append("ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear)");
            builder.Append(" values (");
            builder.Append("@ResourceFolder_Id,@ResourceFolder_ParentId,@ResourceFolder_Name,@ResourceFolder_Level,@Resource_Type,@Resource_Class,@Resource_Version,@ResourceFolder_Remark,@ResourceFolder_Order,@ResourceFolder_Owner,@CreateFUser,@CreateTime,@ResourceFolder_isLast,@LessonPlan_Type,@GradeTerm,@Subject,@Book_ID,@ParticularYear)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceFolder_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ResourceFolder_Level", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@ResourceFolder_Order", SqlDbType.Int, 4), new SqlParameter("@ResourceFolder_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ResourceFolder_isLast", SqlDbType.Char, 1), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), 
                new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.ResourceFolder_Id;
            cmdParms[1].Value = model.ResourceFolder_ParentId;
            cmdParms[2].Value = model.ResourceFolder_Name;
            cmdParms[3].Value = model.ResourceFolder_Level;
            cmdParms[4].Value = model.Resource_Type;
            cmdParms[5].Value = model.Resource_Class;
            cmdParms[6].Value = model.Resource_Version;
            cmdParms[7].Value = model.ResourceFolder_Remark;
            cmdParms[8].Value = model.ResourceFolder_Order;
            cmdParms[9].Value = model.ResourceFolder_Owner;
            cmdParms[10].Value = model.CreateFUser;
            cmdParms[11].Value = model.CreateTime;
            cmdParms[12].Value = model.ResourceFolder_isLast;
            cmdParms[13].Value = model.LessonPlan_Type;
            cmdParms[14].Value = model.GradeTerm;
            cmdParms[15].Value = model.Subject;
            cmdParms[0x10].Value = model.Book_ID;
            cmdParms[0x11].Value = model.ParticularYear;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_ResourceFolder DataRowToModel(DataRow row)
        {
            Model_ResourceFolder folder = new Model_ResourceFolder();
            if (row != null)
            {
                if (row["ResourceFolder_Id"] != null)
                {
                    folder.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["ResourceFolder_ParentId"] != null)
                {
                    folder.ResourceFolder_ParentId = row["ResourceFolder_ParentId"].ToString();
                }
                if (row["ResourceFolder_Name"] != null)
                {
                    folder.ResourceFolder_Name = row["ResourceFolder_Name"].ToString();
                }
                if ((row["ResourceFolder_Level"] != null) && (row["ResourceFolder_Level"].ToString() != ""))
                {
                    folder.ResourceFolder_Level = new int?(int.Parse(row["ResourceFolder_Level"].ToString()));
                }
                if (row["Resource_Type"] != null)
                {
                    folder.Resource_Type = row["Resource_Type"].ToString();
                }
                if (row["Resource_Class"] != null)
                {
                    folder.Resource_Class = row["Resource_Class"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    folder.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["ResourceFolder_Remark"] != null)
                {
                    folder.ResourceFolder_Remark = row["ResourceFolder_Remark"].ToString();
                }
                if ((row["ResourceFolder_Order"] != null) && (row["ResourceFolder_Order"].ToString() != ""))
                {
                    folder.ResourceFolder_Order = new int?(int.Parse(row["ResourceFolder_Order"].ToString()));
                }
                if (row["ResourceFolder_Owner"] != null)
                {
                    folder.ResourceFolder_Owner = row["ResourceFolder_Owner"].ToString();
                }
                if (row["CreateFUser"] != null)
                {
                    folder.CreateFUser = row["CreateFUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    folder.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["ResourceFolder_isLast"] != null)
                {
                    folder.ResourceFolder_isLast = row["ResourceFolder_isLast"].ToString();
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
                if (row["Book_ID"] != null)
                {
                    folder.Book_ID = row["Book_ID"].ToString();
                }
                if ((row["ParticularYear"] != null) && (row["ParticularYear"].ToString() != ""))
                {
                    folder.ParticularYear = new int?(int.Parse(row["ParticularYear"].ToString()));
                }
            }
            return folder;
        }

        public bool Delete(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ResourceFolder ");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ResourceFolder_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ResourceFolder ");
            builder.Append(" where ResourceFolder_Id in (" + ResourceFolder_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ResourceFolder");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string ResourceFolder_ParentId, string ResourceFolder_Name)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ResourceFolder");
            builder.Append(" where ResourceFolder_ParentId=@ResourceFolder_ParentId \r\n            and ResourceFolder_Name=@ResourceFolder_Name ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_ParentId", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Name", SqlDbType.NVarChar, 250) };
            cmdParms[0].Value = ResourceFolder_ParentId;
            cmdParms[1].Value = ResourceFolder_Name;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string ResourceFolder_ParentId, string ResourceFolder_Name, string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ResourceFolder");
            builder.Append(" where ResourceFolder_ParentId=@ResourceFolder_ParentId \r\n            and ResourceFolder_Name=@ResourceFolder_Name \r\n            and ResourceFolder_Id<>@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_ParentId", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_ParentId;
            cmdParms[1].Value = ResourceFolder_Name;
            cmdParms[2].Value = ResourceFolder_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string ResourceFolder_ParentId, string ResourceFolder_Name, string CreateFUser, string Resource_Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ResourceFolder");
            builder.Append(" where ResourceFolder_ParentId=@ResourceFolder_ParentId \r\n            and ResourceFolder_Name=@ResourceFolder_Name \r\n            and CreateFUser=@CreateFUser \r\n            and Resource_Type=@Resource_Type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_ParentId", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Name", SqlDbType.NVarChar, 250), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_ParentId;
            cmdParms[1].Value = ResourceFolder_Name;
            cmdParms[2].Value = CreateFUser;
            cmdParms[3].Value = Resource_Type;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear ");
            builder.Append(" FROM ResourceFolder ");
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
            builder.Append(" ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear ");
            builder.Append(" FROM ResourceFolder ");
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
                builder.Append("order by T.ResourceFolder_Id desc");
            }
            builder.Append(")AS Row, T.*  from ResourceFolder T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_ResourceFolder GetModel(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear from ResourceFolder ");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            new Model_ResourceFolder();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_ResourceFolder GetModelA(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select  top 1 ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear from ResourceFolder \r\nwhere ResourceFolder_Id='{0}'\r\nUNION ALL\r\nSELECT top 1 ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear  FROM GradeTermSubjectTeacherBooks\r\nwhere ResourceFolder_Id='{0}' ", ResourceFolder_Id);
            new Model_ResourceFolder();
            DataSet set = DbHelperSQL.Query(builder.ToString());
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM ResourceFolder ");
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

        public DataSet GetSysFolderTree(string CreateFUser, string Resource_Class, string Resource_Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Order,0 as isStore_Files,ResourceFolder_Order from SysUserTaskForderTree \r\nwhere ResourceFolder_Owner='{0}' \r\nunion \r\nselect ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Order,1 as isStore_Files,ResourceFolder_Order from ResourceFolder \r\nwhere CreateFUser='{0}' and Resource_Class='{1}' and Resource_Type='{2}' ", CreateFUser, Resource_Class, Resource_Type);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetTeacherCLPFolderTree(string UserId, string Resource_Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select DISTINCT t.* from ResourceFolder  t\r\ninner join PrpeLesson_Person t2 on t2.ChargePerson='{0}' and t2.ResourceFolder_Id=t.ResourceFolder_Id\r\nwhere ResourceFolder_ParentId='0' and Resource_Type='{1}'\r\nunion all\r\nselect t2.* from (\r\nselect DISTINCT t.* from ResourceFolder  t\r\ninner join PrpeLesson_Person t2 on t2.ChargePerson='{0}' and t2.ResourceFolder_Id=t.ResourceFolder_Id\r\nwhere ResourceFolder_ParentId='0' and Resource_Type='{1}'\r\n) t \r\ninner join ResourceFolder t2 on t2.ResourceFolder_ParentId<>'0' and t2.Book_ID=t.Book_ID", UserId, Resource_Type);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetTeacherFolderTree(string ResourceFolder_Owner, string Resource_Class, string Resource_Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Level,ResourceFolder_Name,ResourceFolder_Order,1 as isStore_Files from ResourceFolder\r\n where ResourceFolder_Owner='{0}' and Resource_Class='{1}' and Resource_Type='{2}' ", ResourceFolder_Owner, Resource_Class, Resource_Type);
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool InitTchChapterAssemblyRF(string Resource_Type, string Resource_Class, string ResourceFolder_Owner)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ResourceFolder(");
            builder.Append("ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear)");
            builder.AppendFormat("select * from (\r\nselect newid() as ResourceFolder_Id,'0' as ResourceFolder_ParentId,'周测' as ResourceFolder_Name,'0' as ResourceFolder_Level,@Resource_Type as Resource_Type,@Resource_Class as Resource_Class,null as Resource_Version,null as ResourceFolder_Remark,1 as ResourceFolder_Order,@ResourceFolder_Owner as ResourceFolder_Owner,@CreateFUser as CreateFUser,getdate() as CreateTime,0 as ResourceFolder_isLast,null as LessonPlan_Type,null as GradeTerm,null as [Subject],null as Book_ID,null as ParticularYear\r\nunion all\r\nselect newid(),'0','月测' as ResourceFolder_Name,'0',@Resource_Type,@Resource_Class,null,null,2,@ResourceFolder_Owner,@CreateFUser,getdate(),0,null,null,null,null,null\r\nunion all\r\nselect newid(),'0','单元测' as ResourceFolder_Name,'0',@Resource_Type,@Resource_Class,null,null,3,@ResourceFolder_Owner,@CreateFUser,getdate(),0,null,null,null,null,null\r\n) t where not exists(select 1 from ResourceFolder where Resource_Class=@Resource_Class and ResourceFolder_Owner=@ResourceFolder_Owner and ResourceFolder_Name=t.ResourceFolder_Name)\r\n", new object[0]);
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Resource_Type;
            cmdParms[1].Value = Resource_Class;
            cmdParms[2].Value = ResourceFolder_Owner;
            cmdParms[3].Value = ResourceFolder_Owner;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(Model_ResourceFolder model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ResourceFolder set ");
            builder.Append("ResourceFolder_ParentId=@ResourceFolder_ParentId,");
            builder.Append("ResourceFolder_Name=@ResourceFolder_Name,");
            builder.Append("ResourceFolder_Level=@ResourceFolder_Level,");
            builder.Append("Resource_Type=@Resource_Type,");
            builder.Append("Resource_Class=@Resource_Class,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("ResourceFolder_Remark=@ResourceFolder_Remark,");
            builder.Append("ResourceFolder_Order=@ResourceFolder_Order,");
            builder.Append("ResourceFolder_Owner=@ResourceFolder_Owner,");
            builder.Append("CreateFUser=@CreateFUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("ResourceFolder_isLast=@ResourceFolder_isLast,");
            builder.Append("LessonPlan_Type=@LessonPlan_Type,");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("Book_ID=@Book_ID,");
            builder.Append("ParticularYear=@ParticularYear");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ResourceFolder_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceFolder_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ResourceFolder_Level", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@ResourceFolder_Order", SqlDbType.Int, 4), new SqlParameter("@ResourceFolder_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ResourceFolder_isLast", SqlDbType.Char, 1), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), 
                new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.ResourceFolder_ParentId;
            cmdParms[1].Value = model.ResourceFolder_Name;
            cmdParms[2].Value = model.ResourceFolder_Level;
            cmdParms[3].Value = model.Resource_Type;
            cmdParms[4].Value = model.Resource_Class;
            cmdParms[5].Value = model.Resource_Version;
            cmdParms[6].Value = model.ResourceFolder_Remark;
            cmdParms[7].Value = model.ResourceFolder_Order;
            cmdParms[8].Value = model.ResourceFolder_Owner;
            cmdParms[9].Value = model.CreateFUser;
            cmdParms[10].Value = model.CreateTime;
            cmdParms[11].Value = model.ResourceFolder_isLast;
            cmdParms[12].Value = model.LessonPlan_Type;
            cmdParms[13].Value = model.GradeTerm;
            cmdParms[14].Value = model.Subject;
            cmdParms[15].Value = model.Book_ID;
            cmdParms[0x10].Value = model.ParticularYear;
            cmdParms[0x11].Value = model.ResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

