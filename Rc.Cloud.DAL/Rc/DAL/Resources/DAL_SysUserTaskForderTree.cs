namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysUserTaskForderTree
    {
        public bool Add(Model_SysUserTaskForderTree model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysUserTaskForderTree(");
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

        public Model_SysUserTaskForderTree DataRowToModel(DataRow row)
        {
            Model_SysUserTaskForderTree tree = new Model_SysUserTaskForderTree();
            if (row != null)
            {
                if (row["ResourceFolder_Id"] != null)
                {
                    tree.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["ResourceFolder_ParentId"] != null)
                {
                    tree.ResourceFolder_ParentId = row["ResourceFolder_ParentId"].ToString();
                }
                if (row["ResourceFolder_Name"] != null)
                {
                    tree.ResourceFolder_Name = row["ResourceFolder_Name"].ToString();
                }
                if ((row["ResourceFolder_Level"] != null) && (row["ResourceFolder_Level"].ToString() != ""))
                {
                    tree.ResourceFolder_Level = new int?(int.Parse(row["ResourceFolder_Level"].ToString()));
                }
                if (row["Resource_Type"] != null)
                {
                    tree.Resource_Type = row["Resource_Type"].ToString();
                }
                if (row["Resource_Class"] != null)
                {
                    tree.Resource_Class = row["Resource_Class"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    tree.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["ResourceFolder_Remark"] != null)
                {
                    tree.ResourceFolder_Remark = row["ResourceFolder_Remark"].ToString();
                }
                if ((row["ResourceFolder_Order"] != null) && (row["ResourceFolder_Order"].ToString() != ""))
                {
                    tree.ResourceFolder_Order = new int?(int.Parse(row["ResourceFolder_Order"].ToString()));
                }
                if (row["ResourceFolder_Owner"] != null)
                {
                    tree.ResourceFolder_Owner = row["ResourceFolder_Owner"].ToString();
                }
                if (row["CreateFUser"] != null)
                {
                    tree.CreateFUser = row["CreateFUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    tree.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["ResourceFolder_isLast"] != null)
                {
                    tree.ResourceFolder_isLast = row["ResourceFolder_isLast"].ToString();
                }
                if (row["LessonPlan_Type"] != null)
                {
                    tree.LessonPlan_Type = row["LessonPlan_Type"].ToString();
                }
                if (row["GradeTerm"] != null)
                {
                    tree.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Subject"] != null)
                {
                    tree.Subject = row["Subject"].ToString();
                }
                if (row["Book_ID"] != null)
                {
                    tree.Book_ID = row["Book_ID"].ToString();
                }
                if ((row["ParticularYear"] != null) && (row["ParticularYear"].ToString() != ""))
                {
                    tree.ParticularYear = new int?(int.Parse(row["ParticularYear"].ToString()));
                }
            }
            return tree;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysUserTaskForderTree ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear ");
            builder.Append(" FROM SysUserTaskForderTree ");
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
            builder.Append(" FROM SysUserTaskForderTree ");
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
                builder.Append("order by T. desc");
            }
            builder.Append(")AS Row, T.*  from SysUserTaskForderTree T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByResourceFolder_Owner(string ResourceFolder_Owner)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Order,0 as isStore_Files from SysUserTaskForderTree\r\nwhere ResourceFolder_Owner='{0}' ", ResourceFolder_Owner);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysUserTaskForderTree GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Level,Resource_Type,Resource_Class,Resource_Version,ResourceFolder_Remark,ResourceFolder_Order,ResourceFolder_Owner,CreateFUser,CreateTime,ResourceFolder_isLast,LessonPlan_Type,GradeTerm,Subject,Book_ID,ParticularYear from SysUserTaskForderTree ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SysUserTaskForderTree();
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
            builder.Append("select count(1) FROM SysUserTaskForderTree ");
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

        public bool Update(Model_SysUserTaskForderTree model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysUserTaskForderTree set ");
            builder.Append("ResourceFolder_Id=@ResourceFolder_Id,");
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
            builder.Append(" where ");
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
    }
}

