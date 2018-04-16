namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Two_WayChecklist
    {
        public bool Add(Model_Two_WayChecklist model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Two_WayChecklist(");
            builder.Append("Two_WayChecklist_Id,Two_WayChecklist_Name,ParticularYear,GradeTerm,Resource_Version,Subject,Remark,Two_WayChecklistType,CreateUser,CreateTime,ParentId)");
            builder.Append(" values (");
            builder.Append("@Two_WayChecklist_Id,@Two_WayChecklist_Name,@ParticularYear,@GradeTerm,@Resource_Version,@Subject,@Remark,@Two_WayChecklistType,@CreateUser,@CreateTime,@ParentId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklist_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@Two_WayChecklistType", SqlDbType.NVarChar, 50), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ParentId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Two_WayChecklist_Id;
            cmdParms[1].Value = model.Two_WayChecklist_Name;
            cmdParms[2].Value = model.ParticularYear;
            cmdParms[3].Value = model.GradeTerm;
            cmdParms[4].Value = model.Resource_Version;
            cmdParms[5].Value = model.Subject;
            cmdParms[6].Value = model.Remark;
            cmdParms[7].Value = model.Two_WayChecklistType;
            cmdParms[8].Value = model.CreateUser;
            cmdParms[9].Value = model.CreateTime;
            cmdParms[10].Value = model.ParentId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Two_WayChecklist DataRowToModel(DataRow row)
        {
            Model_Two_WayChecklist checklist = new Model_Two_WayChecklist();
            if (row != null)
            {
                if (row["Two_WayChecklist_Id"] != null)
                {
                    checklist.Two_WayChecklist_Id = row["Two_WayChecklist_Id"].ToString();
                }
                if (row["Two_WayChecklist_Name"] != null)
                {
                    checklist.Two_WayChecklist_Name = row["Two_WayChecklist_Name"].ToString();
                }
                if ((row["ParticularYear"] != null) && (row["ParticularYear"].ToString() != ""))
                {
                    checklist.ParticularYear = new int?(int.Parse(row["ParticularYear"].ToString()));
                }
                if (row["GradeTerm"] != null)
                {
                    checklist.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    checklist.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["Subject"] != null)
                {
                    checklist.Subject = row["Subject"].ToString();
                }
                if (row["Remark"] != null)
                {
                    checklist.Remark = row["Remark"].ToString();
                }
                if (row["Two_WayChecklistType"] != null)
                {
                    checklist.Two_WayChecklistType = row["Two_WayChecklistType"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    checklist.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    checklist.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["ParentId"] != null)
                {
                    checklist.ParentId = row["ParentId"].ToString();
                }
            }
            return checklist;
        }

        public bool Delete(string Two_WayChecklist_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklist ");
            builder.Append(" where Two_WayChecklist_Id=@Two_WayChecklist_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklist_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Two_WayChecklist_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklist ");
            builder.Append(" where Two_WayChecklist_Id in (" + Two_WayChecklist_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Two_WayChecklist_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Two_WayChecklist");
            builder.Append(" where Two_WayChecklist_Id=@Two_WayChecklist_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklist_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Two_WayChecklist_Id,Two_WayChecklist_Name,ParticularYear,GradeTerm,Resource_Version,Subject,Remark,Two_WayChecklistType,CreateUser,CreateTime,ParentId ");
            builder.Append(" FROM Two_WayChecklist ");
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
            builder.Append(" Two_WayChecklist_Id,Two_WayChecklist_Name,ParticularYear,GradeTerm,Resource_Version,Subject,Remark,Two_WayChecklistType,CreateUser,CreateTime,ParentId ");
            builder.Append(" FROM Two_WayChecklist ");
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
                builder.Append("order by T.Two_WayChecklist_Id desc");
            }
            builder.Append(")AS Row, T.*  from Two_WayChecklist T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Two_WayChecklist GetModel(string Two_WayChecklist_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Two_WayChecklist_Id,Two_WayChecklist_Name,ParticularYear,GradeTerm,Resource_Version,Subject,Remark,Two_WayChecklistType,CreateUser,CreateTime,ParentId from Two_WayChecklist ");
            builder.Append(" where Two_WayChecklist_Id=@Two_WayChecklist_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklist_Id;
            new Model_Two_WayChecklist();
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
            builder.Append("select count(1) FROM Two_WayChecklist ");
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

        public bool Update(Model_Two_WayChecklist model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Two_WayChecklist set ");
            builder.Append("Two_WayChecklist_Name=@Two_WayChecklist_Name,");
            builder.Append("ParticularYear=@ParticularYear,");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("Subject=@Subject,");
            builder.Append("Remark=@Remark,");
            builder.Append("Two_WayChecklistType=@Two_WayChecklistType,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("ParentId=@ParentId");
            builder.Append(" where Two_WayChecklist_Id=@Two_WayChecklist_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Name", SqlDbType.NVarChar, 250), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@Two_WayChecklistType", SqlDbType.NVarChar, 50), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ParentId", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Two_WayChecklist_Name;
            cmdParms[1].Value = model.ParticularYear;
            cmdParms[2].Value = model.GradeTerm;
            cmdParms[3].Value = model.Resource_Version;
            cmdParms[4].Value = model.Subject;
            cmdParms[5].Value = model.Remark;
            cmdParms[6].Value = model.Two_WayChecklistType;
            cmdParms[7].Value = model.CreateUser;
            cmdParms[8].Value = model.CreateTime;
            cmdParms[9].Value = model.ParentId;
            cmdParms[10].Value = model.Two_WayChecklist_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

