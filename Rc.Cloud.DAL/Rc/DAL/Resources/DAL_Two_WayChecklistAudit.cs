namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Two_WayChecklistAudit
    {
        public bool Add(Model_Two_WayChecklistAudit model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Two_WayChecklistAudit(");
            builder.Append("Two_WayChecklist_Id,User_Id,Status,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@Two_WayChecklist_Id,@User_Id,@Status,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@User_Id", SqlDbType.Char, 0x24), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Two_WayChecklist_Id;
            cmdParms[1].Value = model.User_Id;
            cmdParms[2].Value = model.Status;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Two_WayChecklistAudit DataRowToModel(DataRow row)
        {
            Model_Two_WayChecklistAudit audit = new Model_Two_WayChecklistAudit();
            if (row != null)
            {
                if (row["Two_WayChecklist_Id"] != null)
                {
                    audit.Two_WayChecklist_Id = row["Two_WayChecklist_Id"].ToString();
                }
                if (row["User_Id"] != null)
                {
                    audit.User_Id = row["User_Id"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    audit.Status = new int?(int.Parse(row["Status"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    audit.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    audit.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    audit.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return audit;
        }

        public bool Delete(string Two_WayChecklist_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklistAudit ");
            builder.Append(" where Two_WayChecklist_Id=@Two_WayChecklist_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklist_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Two_WayChecklist_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklistAudit ");
            builder.Append(" where Two_WayChecklist_Id in (" + Two_WayChecklist_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Two_WayChecklist_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Two_WayChecklistAudit");
            builder.Append(" where Two_WayChecklist_Id=@Two_WayChecklist_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklist_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Two_WayChecklist_Id,User_Id,Status,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM Two_WayChecklistAudit ");
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
            builder.Append(" Two_WayChecklist_Id,User_Id,Status,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM Two_WayChecklistAudit ");
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
            builder.Append(")AS Row, T.*  from Two_WayChecklistAudit T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Two_WayChecklistAudit GetModel(string Two_WayChecklist_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Two_WayChecklist_Id,User_Id,Status,Remark,CreateUser,CreateTime from Two_WayChecklistAudit ");
            builder.Append(" where Two_WayChecklist_Id=@Two_WayChecklist_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklist_Id;
            new Model_Two_WayChecklistAudit();
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
            builder.Append("select count(1) FROM Two_WayChecklistAudit ");
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

        public bool Update(Model_Two_WayChecklistAudit model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Two_WayChecklistAudit set ");
            builder.Append("User_Id=@User_Id,");
            builder.Append("Status=@Status,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where Two_WayChecklist_Id=@Two_WayChecklist_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@User_Id", SqlDbType.Char, 0x24), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.User_Id;
            cmdParms[1].Value = model.Status;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.Two_WayChecklist_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

