namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_F_User
    {
        public bool Add(Model_F_User model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into F_User(");
            builder.Append("UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime)");
            builder.Append(" values (");
            builder.Append("@UserId,@UserName,@Password,@TrueName,@UserIdentity,@Birthday,@Sex,@Email,@Mobile,@Province,@City,@County,@School,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@UserName", SqlDbType.VarChar, 20), new SqlParameter("@Password", SqlDbType.Char, 0x20), new SqlParameter("@TrueName", SqlDbType.NVarChar, 250), new SqlParameter("@UserIdentity", SqlDbType.Char, 1), new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@Sex", SqlDbType.Char, 1), new SqlParameter("@Email", SqlDbType.VarChar, 200), new SqlParameter("@Mobile", SqlDbType.VarChar, 20), new SqlParameter("@Province", SqlDbType.NVarChar, 250), new SqlParameter("@City", SqlDbType.NVarChar, 250), new SqlParameter("@County", SqlDbType.NVarChar, 250), new SqlParameter("@School", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.UserName;
            cmdParms[2].Value = model.Password;
            cmdParms[3].Value = model.TrueName;
            cmdParms[4].Value = model.UserIdentity;
            cmdParms[5].Value = model.Birthday;
            cmdParms[6].Value = model.Sex;
            cmdParms[7].Value = model.Email;
            cmdParms[8].Value = model.Mobile;
            cmdParms[9].Value = model.Province;
            cmdParms[10].Value = model.City;
            cmdParms[11].Value = model.County;
            cmdParms[12].Value = model.School;
            cmdParms[13].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_F_User DataRowToModel(DataRow row)
        {
            Model_F_User user = new Model_F_User();
            if (row != null)
            {
                if (row["UserId"] != null)
                {
                    user.UserId = row["UserId"].ToString();
                }
                if (row["UserName"] != null)
                {
                    user.UserName = row["UserName"].ToString();
                }
                if (row["Password"] != null)
                {
                    user.Password = row["Password"].ToString();
                }
                if (row["TrueName"] != null)
                {
                    user.TrueName = row["TrueName"].ToString();
                }
                if (row["UserIdentity"] != null)
                {
                    user.UserIdentity = row["UserIdentity"].ToString();
                }
                if ((row["Birthday"] != null) && (row["Birthday"].ToString() != ""))
                {
                    user.Birthday = new DateTime?(DateTime.Parse(row["Birthday"].ToString()));
                }
                if (row["Sex"] != null)
                {
                    user.Sex = row["Sex"].ToString();
                }
                if (row["Email"] != null)
                {
                    user.Email = row["Email"].ToString();
                }
                if (row["Mobile"] != null)
                {
                    user.Mobile = row["Mobile"].ToString();
                }
                if (row["Province"] != null)
                {
                    user.Province = row["Province"].ToString();
                }
                if (row["City"] != null)
                {
                    user.City = row["City"].ToString();
                }
                if (row["County"] != null)
                {
                    user.County = row["County"].ToString();
                }
                if (row["School"] != null)
                {
                    user.School = row["School"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    user.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return user;
        }

        public bool Delete(string UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from F_User ");
            builder.Append(" where UserId=@UserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string UserIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from F_User ");
            builder.Append(" where UserId in (" + UserIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from F_User");
            builder.Append(" where UserId=@UserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(Model_F_User f_user, string type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from F_User where 1=1 ");
            if (type == "1")
            {
                builder.AppendFormat(" and UserName='{0}'", f_user.UserName);
            }
            if (type == "2")
            {
                builder.AppendFormat(" and UserId!='{0}' and UserName='{1}'", f_user.UserId, f_user.UserName);
            }
            if (Convert.ToInt32(DbHelperSQL.GetSingle(builder.ToString())) <= 1)
            {
                return false;
            }
            return true;
        }

        public DataSet GetDataList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM F_User");
            return this.CurrDB.ExecuteDataSet(builder.ToString(), null, new object[0]);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime ");
            builder.Append(" FROM F_User ");
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
            builder.Append(" UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime ");
            builder.Append(" FROM F_User ");
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
                builder.Append("order by T.UserId desc");
            }
            builder.Append(")AS Row, T.*  from F_User T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListPaged(string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT row_number() over(order by CreateTime) AS r_n,UserId,UserName,TrueName,Email,Mobile FROM F_User where 1=1 and UserIdentity = 'T'");
            builder.Append(strWhere);
            return sys.GetRecordByPage(builder.ToString(), PageIndex, PageSize, out rCount, out pCount);
        }

        public Model_F_User GetModel(string UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime from F_User ");
            builder.Append(" where UserId=@UserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserId;
            new Model_F_User();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_F_User GetModel(string UserName, string Password)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime from F_User ");
            builder.Append(" where UserName=@UserName  and  Password=@Password ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@Password", SqlDbType.Char, 0x20) };
            cmdParms[0].Value = UserName;
            cmdParms[1].Value = Password;
            new Model_F_User();
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
            builder.Append("select count(1) FROM F_User ");
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

        public bool Update(Model_F_User model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update F_User set ");
            builder.Append("UserName=@UserName,");
            builder.Append("Password=@Password,");
            builder.Append("TrueName=@TrueName,");
            builder.Append("UserIdentity=@UserIdentity,");
            builder.Append("Birthday=@Birthday,");
            builder.Append("Sex=@Sex,");
            builder.Append("Email=@Email,");
            builder.Append("Mobile=@Mobile,");
            builder.Append("Province=@Province,");
            builder.Append("City=@City,");
            builder.Append("County=@County,");
            builder.Append("School=@School,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where UserId=@UserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.VarChar, 20), new SqlParameter("@Password", SqlDbType.Char, 0x20), new SqlParameter("@TrueName", SqlDbType.NVarChar, 250), new SqlParameter("@UserIdentity", SqlDbType.Char, 1), new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@Sex", SqlDbType.Char, 1), new SqlParameter("@Email", SqlDbType.VarChar, 200), new SqlParameter("@Mobile", SqlDbType.VarChar, 20), new SqlParameter("@Province", SqlDbType.NVarChar, 250), new SqlParameter("@City", SqlDbType.NVarChar, 250), new SqlParameter("@County", SqlDbType.NVarChar, 250), new SqlParameter("@School", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UserId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.UserName;
            cmdParms[1].Value = model.Password;
            cmdParms[2].Value = model.TrueName;
            cmdParms[3].Value = model.UserIdentity;
            cmdParms[4].Value = model.Birthday;
            cmdParms[5].Value = model.Sex;
            cmdParms[6].Value = model.Email;
            cmdParms[7].Value = model.Mobile;
            cmdParms[8].Value = model.Province;
            cmdParms[9].Value = model.City;
            cmdParms[10].Value = model.County;
            cmdParms[11].Value = model.School;
            cmdParms[12].Value = model.CreateTime;
            cmdParms[13].Value = model.UserId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        private DatabaseSQLHelper CurrDB { get; set; }
    }
}

