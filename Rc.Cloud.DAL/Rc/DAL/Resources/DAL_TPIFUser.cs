namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_TPIFUser
    {
        public bool Add(Model_TPIFUser model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TPIFUser(");
            builder.Append("ThirdPartyIFUser_Id,School,UserName,Remark,CreateTime)");
            builder.Append(" values (");
            builder.Append("@ThirdPartyIFUser_Id,@School,@UserName,@Remark,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThirdPartyIFUser_Id", SqlDbType.Char, 0x24), new SqlParameter("@School", SqlDbType.VarChar, 50), new SqlParameter("@UserName", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.ThirdPartyIFUser_Id;
            cmdParms[1].Value = model.School;
            cmdParms[2].Value = model.UserName;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_TPIFUser DataRowToModel(DataRow row)
        {
            Model_TPIFUser user = new Model_TPIFUser();
            if (row != null)
            {
                if (row["ThirdPartyIFUser_Id"] != null)
                {
                    user.ThirdPartyIFUser_Id = row["ThirdPartyIFUser_Id"].ToString();
                }
                if (row["School"] != null)
                {
                    user.School = row["School"].ToString();
                }
                if (row["UserName"] != null)
                {
                    user.UserName = row["UserName"].ToString();
                }
                if (row["Remark"] != null)
                {
                    user.Remark = row["Remark"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    user.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return user;
        }

        public bool Delete(string ThirdPartyIFUser_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TPIFUser ");
            builder.Append(" where ThirdPartyIFUser_Id=@ThirdPartyIFUser_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThirdPartyIFUser_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ThirdPartyIFUser_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ThirdPartyIFUser_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TPIFUser ");
            builder.Append(" where ThirdPartyIFUser_Id in (" + ThirdPartyIFUser_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ThirdPartyIFUser_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from TPIFUser");
            builder.Append(" where ThirdPartyIFUser_Id=@ThirdPartyIFUser_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThirdPartyIFUser_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ThirdPartyIFUser_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ThirdPartyIFUser_Id,School,UserName,Remark,CreateTime ");
            builder.Append(" FROM TPIFUser ");
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
            builder.Append(" ThirdPartyIFUser_Id,School,UserName,Remark,CreateTime ");
            builder.Append(" FROM TPIFUser ");
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
                builder.Append("order by T.ThirdPartyIFUser_Id desc");
            }
            builder.Append(")AS Row, T.*  from TPIFUser T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_TPIFUser GetModel(string ThirdPartyIFUser_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ThirdPartyIFUser_Id,School,UserName,Remark,CreateTime from TPIFUser ");
            builder.Append(" where ThirdPartyIFUser_Id=@ThirdPartyIFUser_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThirdPartyIFUser_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ThirdPartyIFUser_Id;
            new Model_TPIFUser();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_TPIFUser GetModelBySchoolUserName(string School, string UserName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ThirdPartyIFUser_Id,School,UserName,Remark,CreateTime from TPIFUser ");
            builder.Append(" where School=@School and UserName=@UserName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@School", SqlDbType.VarChar, 50), new SqlParameter("@UserName", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = School;
            cmdParms[1].Value = UserName;
            new Model_TPIFUser();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_TPIFUser GetModelByUserName(string UserName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ThirdPartyIFUser_Id,School,UserName,Remark,CreateTime from TPIFUser ");
            builder.Append(" where UserName=@UserName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = UserName;
            new Model_TPIFUser();
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
            builder.Append("select count(1) FROM TPIFUser ");
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

        public bool Update(Model_TPIFUser model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TPIFUser set ");
            builder.Append("School=@School,");
            builder.Append("UserName=@UserName,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where ThirdPartyIFUser_Id=@ThirdPartyIFUser_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@School", SqlDbType.VarChar, 50), new SqlParameter("@UserName", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ThirdPartyIFUser_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.School;
            cmdParms[1].Value = model.UserName;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.CreateTime;
            cmdParms[4].Value = model.ThirdPartyIFUser_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

