namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Two_WayChecklistAuth
    {
        public bool Add(Model_Two_WayChecklistAuth model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Two_WayChecklistAuth(");
            builder.Append("Two_WayChecklistAuth_Id,User_Id,Two_WayChecklist_Id,Auth_Type,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@Two_WayChecklistAuth_Id,@User_Id,@Two_WayChecklist_Id,@Auth_Type,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistAuth_Id", SqlDbType.Char, 0x24), new SqlParameter("@User_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@Auth_Type", SqlDbType.VarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Two_WayChecklistAuth_Id;
            cmdParms[1].Value = model.User_Id;
            cmdParms[2].Value = model.Two_WayChecklist_Id;
            cmdParms[3].Value = model.Auth_Type;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Two_WayChecklistAuth DataRowToModel(DataRow row)
        {
            Model_Two_WayChecklistAuth auth = new Model_Two_WayChecklistAuth();
            if (row != null)
            {
                if (row["Two_WayChecklistAuth_Id"] != null)
                {
                    auth.Two_WayChecklistAuth_Id = row["Two_WayChecklistAuth_Id"].ToString();
                }
                if (row["User_Id"] != null)
                {
                    auth.User_Id = row["User_Id"].ToString();
                }
                if (row["Two_WayChecklist_Id"] != null)
                {
                    auth.Two_WayChecklist_Id = row["Two_WayChecklist_Id"].ToString();
                }
                if (row["Auth_Type"] != null)
                {
                    auth.Auth_Type = row["Auth_Type"].ToString();
                }
                if (row["Remark"] != null)
                {
                    auth.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    auth.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    auth.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return auth;
        }

        public bool Delete(string Two_WayChecklistAuth_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklistAuth ");
            builder.Append(" where Two_WayChecklistAuth_Id=@Two_WayChecklistAuth_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistAuth_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistAuth_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Two_WayChecklistAuth_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklistAuth ");
            builder.Append(" where Two_WayChecklistAuth_Id in (" + Two_WayChecklistAuth_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Two_WayChecklistAuth_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Two_WayChecklistAuth");
            builder.Append(" where Two_WayChecklistAuth_Id=@Two_WayChecklistAuth_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistAuth_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistAuth_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Two_WayChecklistAuth_Id,User_Id,Two_WayChecklist_Id,Auth_Type,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM Two_WayChecklistAuth ");
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
            builder.Append(" Two_WayChecklistAuth_Id,User_Id,Two_WayChecklist_Id,Auth_Type,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM Two_WayChecklistAuth ");
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
                builder.Append("order by T.Two_WayChecklistAuth_Id desc");
            }
            builder.Append(")AS Row, T.*  from Two_WayChecklistAuth T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Two_WayChecklistAuth GetModel(string Two_WayChecklistAuth_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Two_WayChecklistAuth_Id,User_Id,Two_WayChecklist_Id,Auth_Type,Remark,CreateUser,CreateTime from Two_WayChecklistAuth ");
            builder.Append(" where Two_WayChecklistAuth_Id=@Two_WayChecklistAuth_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistAuth_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistAuth_Id;
            new Model_Two_WayChecklistAuth();
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
            builder.Append("select count(1) FROM Two_WayChecklistAuth ");
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

        public bool Update(Model_Two_WayChecklistAuth model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Two_WayChecklistAuth set ");
            builder.Append("User_Id=@User_Id,");
            builder.Append("Two_WayChecklist_Id=@Two_WayChecklist_Id,");
            builder.Append("Auth_Type=@Auth_Type,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where Two_WayChecklistAuth_Id=@Two_WayChecklistAuth_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@User_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@Auth_Type", SqlDbType.VarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Two_WayChecklistAuth_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.User_Id;
            cmdParms[1].Value = model.Two_WayChecklist_Id;
            cmdParms[2].Value = model.Auth_Type;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.Two_WayChecklistAuth_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

