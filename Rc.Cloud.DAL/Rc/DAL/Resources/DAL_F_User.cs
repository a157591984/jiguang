namespace Rc.DAL.Resources
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_F_User
    {
        public bool Add(Rc.Model.Resources.Model_F_User model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into F_User(");
            builder.Append("UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate)");
            builder.Append(" values (");
            builder.Append("@UserId,@UserName,@Password,@TrueName,@UserIdentity,@Birthday,@Sex,@Email,@Mobile,@Province,@City,@County,@School,@CreateTime,@Stoken,@StokenTime,@Resource_Version,@GradeTerm,@Subject,@UserPost,@ExpirationDate)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@UserName", SqlDbType.VarChar, 50), new SqlParameter("@Password", SqlDbType.Char, 0x20), new SqlParameter("@TrueName", SqlDbType.NVarChar, 250), new SqlParameter("@UserIdentity", SqlDbType.Char, 1), new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@Sex", SqlDbType.Char, 1), new SqlParameter("@Email", SqlDbType.VarChar, 200), new SqlParameter("@Mobile", SqlDbType.VarChar, 20), new SqlParameter("@Province", SqlDbType.NVarChar, 250), new SqlParameter("@City", SqlDbType.NVarChar, 250), new SqlParameter("@County", SqlDbType.NVarChar, 250), new SqlParameter("@School", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Stoken", SqlDbType.NVarChar, 50), new SqlParameter("@StokenTime", SqlDbType.DateTime), 
                new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@UserPost", SqlDbType.Char, 0x24), new SqlParameter("@ExpirationDate", SqlDbType.DateTime)
             };
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
            cmdParms[14].Value = model.Stoken;
            cmdParms[15].Value = model.StokenTime;
            cmdParms[0x10].Value = model.Resource_Version;
            cmdParms[0x11].Value = model.GradeTerm;
            cmdParms[0x12].Value = model.Subject;
            cmdParms[0x13].Value = model.UserPost;
            cmdParms[20].Value = model.ExpirationDate;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int APPReg(Rc.Model.Resources.Model_F_User model, Model_f_user_token modelToken)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into F_User(");
            builder.Append("UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate)");
            builder.Append(" values (");
            builder.Append("@UserId,@UserName,@Password,@TrueName,@UserIdentity,@Birthday,@Sex,@Email,@Mobile,@Province,@City,@County,@School,@CreateTime,@Stoken,@StokenTime,@Resource_Version,@GradeTerm,@Subject,@UserPost,@ExpirationDate)");
            SqlParameter[] parameterArray = new SqlParameter[] { 
                new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@UserName", SqlDbType.VarChar, 50), new SqlParameter("@Password", SqlDbType.Char, 0x20), new SqlParameter("@TrueName", SqlDbType.NVarChar, 250), new SqlParameter("@UserIdentity", SqlDbType.Char, 1), new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@Sex", SqlDbType.Char, 1), new SqlParameter("@Email", SqlDbType.VarChar, 200), new SqlParameter("@Mobile", SqlDbType.VarChar, 20), new SqlParameter("@Province", SqlDbType.NVarChar, 250), new SqlParameter("@City", SqlDbType.NVarChar, 250), new SqlParameter("@County", SqlDbType.NVarChar, 250), new SqlParameter("@School", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Stoken", SqlDbType.NVarChar, 50), new SqlParameter("@StokenTime", SqlDbType.DateTime), 
                new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@UserPost", SqlDbType.Char, 0x24), new SqlParameter("@ExpirationDate", SqlDbType.DateTime)
             };
            parameterArray[0].Value = model.UserId;
            parameterArray[1].Value = model.UserName;
            parameterArray[2].Value = model.Password;
            parameterArray[3].Value = model.TrueName;
            parameterArray[4].Value = model.UserIdentity;
            parameterArray[5].Value = model.Birthday;
            parameterArray[6].Value = model.Sex;
            parameterArray[7].Value = model.Email;
            parameterArray[8].Value = model.Mobile;
            parameterArray[9].Value = model.Province;
            parameterArray[10].Value = model.City;
            parameterArray[11].Value = model.County;
            parameterArray[12].Value = model.School;
            parameterArray[13].Value = model.CreateTime;
            parameterArray[14].Value = model.Stoken;
            parameterArray[15].Value = model.StokenTime;
            parameterArray[0x10].Value = model.Resource_Version;
            parameterArray[0x11].Value = model.GradeTerm;
            parameterArray[0x12].Value = model.Subject;
            parameterArray[0x13].Value = model.UserPost;
            parameterArray[20].Value = model.ExpirationDate;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("insert into f_user_token(");
            builder.Append("user_id,product_type,token,token_time,login_time,login_ip)");
            builder.Append(" values (");
            builder.Append("@user_id,@product_type,@token,@token_time,@login_time,@login_ip)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@user_id", SqlDbType.Char, 0x24), new SqlParameter("@product_type", SqlDbType.VarChar, 50), new SqlParameter("@token", SqlDbType.VarChar, 50), new SqlParameter("@token_time", SqlDbType.DateTime), new SqlParameter("@login_time", SqlDbType.DateTime), new SqlParameter("@login_ip", SqlDbType.VarChar, 50) };
            parameterArray2[0].Value = modelToken.user_id;
            parameterArray2[1].Value = modelToken.product_type;
            parameterArray2[2].Value = modelToken.token;
            parameterArray2[3].Value = modelToken.token_time;
            parameterArray2[4].Value = modelToken.login_time;
            parameterArray2[5].Value = modelToken.login_ip;
            dictionary.Add(builder.ToString(), parameterArray2);
            return DbHelperSQL.ExecuteSqlTran(dictionary);
        }

        public Rc.Model.Resources.Model_F_User DataRowToModel(DataRow row)
        {
            Rc.Model.Resources.Model_F_User user = new Rc.Model.Resources.Model_F_User();
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
                if (row["Stoken"] != null)
                {
                    user.Stoken = row["Stoken"].ToString();
                }
                if ((row["StokenTime"] != null) && (row["StokenTime"].ToString() != ""))
                {
                    user.StokenTime = new DateTime?(DateTime.Parse(row["StokenTime"].ToString()));
                }
                if (row["Resource_Version"] != null)
                {
                    user.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["GradeTerm"] != null)
                {
                    user.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Subject"] != null)
                {
                    user.Subject = row["Subject"].ToString();
                }
                if (row["UserPost"] != null)
                {
                    user.UserPost = row["UserPost"].ToString();
                }
                if ((row["ExpirationDate"] != null) && (row["ExpirationDate"].ToString() != ""))
                {
                    user.ExpirationDate = new DateTime?(DateTime.Parse(row["ExpirationDate"].ToString()));
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

        public bool DelFUser(string UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from F_User where UserId=@UserId; ");
            builder.Append("delete from UserGroup_Member where User_ID=@UserId; ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
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

        public bool Exists(Rc.Model.Resources.Model_F_User f_user, string type)
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
            if (Convert.ToInt32(DbHelperSQL.GetSingle(builder.ToString())) <= 0)
            {
                return false;
            }
            return true;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate ");
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
            builder.Append(" UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate ");
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

        public DataSet GetListPage(string strWhere, string orderby, int startIndex, int endIndex)
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
            builder.Append(")AS Row, T.*,CD1.D_Name SexName,CD2.D_Name SubjectName,CD3.D_Name UserPostName from F_User T  \r\nleft join Common_Dict CD1 on CD1.Common_Dict_ID = T.Sex\r\nleft join Common_Dict CD2 on CD2.Common_Dict_ID = T.Subject\r\nleft join Common_Dict CD3 on CD3.Common_Dict_ID = T.UserPost ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Rc.Model.Resources.Model_F_User GetModel(string UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate from F_User ");
            builder.Append(" where UserId=@UserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserId;
            new Rc.Model.Resources.Model_F_User();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Rc.Model.Resources.Model_F_User GetModel(string UserName, string Password)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1 * FROM \r\n(\r\nselect  UserId,UserName,Password,\r\nTrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County\r\n,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate \r\nfrom F_User\r\nUNION ALL\r\nselect SysUser_ID as UserId,SysUser_LoginName as username,SysUser_PassWord as Password  \r\n, SysUser_Name as TrueName,'A' AS UserIdentity,NULL Birthday,NULL Sex,\r\nNULL Email,NULL Mobile,NULL Province,NULL City,NULL County\r\n,NULL School,NULL CreateTime,NULL Stoken,NULL StokenTime,NULL Resource_Version,NULL GradeTerm,NULL Subject,NULL UserPost,NULL ExpirationDate \r\nfrom SysUser\r\n) A");
            builder.Append(" where UserName=@UserName  and  Password=@Password ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@Password", SqlDbType.Char, 0x20) };
            cmdParms[0].Value = UserName;
            cmdParms[1].Value = Password;
            new Rc.Cloud.Model.Model_F_User();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Rc.Model.Resources.Model_F_User GetModelA(string UserName, string Password)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1 * FROM \r\n(\r\nselect  UserId,UserName,Password,\r\nTrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County\r\n,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate \r\nfrom F_User\r\nUNION ALL\r\nselect SysUser_ID as UserId,SysUser_LoginName as username,SysUser_PassWord as Password  \r\n, SysUser_Name as TrueName,'A' AS UserIdentity,NULL Birthday,NULL Sex,\r\nNULL Email,NULL Mobile,NULL Province,NULL City,NULL County\r\n,NULL School,NULL CreateTime,NULL Stoken,NULL StokenTime,NULL Resource_Version,NULL GradeTerm,NULL Subject,NULL UserPost,NULL ExpirationDate \r\nfrom SysUser\r\n) A ");
            builder.Append(" where UserName=@UserName ");
            builder.Append(" and Password=@Password ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.Char, 0x24), new SqlParameter("@Password", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserName;
            cmdParms[1].Value = Password;
            new Rc.Model.Resources.Model_F_User();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Rc.Model.Resources.Model_F_User GetModelByUserIdToken(string userId, string token, string productType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate from F_User t ");
            builder.Append(" inner join f_user_token t2 on t2.user_id=t.UserId and product_type=@product_type ");
            builder.Append(" where UserId=@UserId and token=@token and convert(nvarchar(10),token_time,23)=convert(nvarchar(10),GETDATE(),23) ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@product_type", SqlDbType.NVarChar, 50), new SqlParameter("@token", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = userId;
            cmdParms[1].Value = productType;
            cmdParms[2].Value = token;
            new Rc.Cloud.Model.Model_F_User();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Rc.Model.Resources.Model_F_User GetModelByUserName(string UserName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate from F_User ");
            builder.Append(" where UserName=@UserName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.VarChar, 100) };
            cmdParms[0].Value = UserName;
            new Rc.Cloud.Model.Model_F_User();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Rc.Model.Resources.Model_F_User GetModelstrWhere(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1 * FROM \r\n(\r\nselect  UserId,UserName,Password,\r\nTrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County\r\n,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate \r\nfrom F_User\r\nUNION ALL\r\nselect SysUser_ID as UserId,SysUser_LoginName as username,SysUser_PassWord as Password  \r\n, SysUser_Name as TrueName,'A' AS UserIdentity,NULL Birthday,NULL Sex,\r\nNULL Email,NULL Mobile,NULL Province,NULL City,NULL County\r\n,NULL School,NULL CreateTime, Stoken, StokenTime,NULL Resource_Version,NULL GradeTerm,NULL Subject,NULL UserPost,NULL ExpirationDate \r\nfrom SysUser\r\n) A ");
            builder.Append(strWhere);
            new Rc.Model.Resources.Model_F_User();
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

        public int GetRecordCountA(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT count(1) FROM \r\n(\r\nselect  UserId\r\nfrom F_User\r\nUNION ALL\r\nselect SysUser_ID as UserId\r\nfrom SysUser\r\n) A ");
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

        public bool resettingPwd(Rc.Model.Resources.Model_F_User model, Model_Msg modelMsg)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
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
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("Stoken=@Stoken,");
            builder.Append("StokenTime=@StokenTime,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("UserPost=@UserPost,");
            builder.Append("ExpirationDate=@ExpirationDate");
            builder.Append(" where UserId=@UserId ");
            SqlParameter[] parameterArray = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.VarChar, 50), new SqlParameter("@Password", SqlDbType.Char, 0x20), new SqlParameter("@TrueName", SqlDbType.NVarChar, 250), new SqlParameter("@UserIdentity", SqlDbType.Char, 1), new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@Sex", SqlDbType.Char, 1), new SqlParameter("@Email", SqlDbType.VarChar, 200), new SqlParameter("@Mobile", SqlDbType.VarChar, 20), new SqlParameter("@Province", SqlDbType.NVarChar, 250), new SqlParameter("@City", SqlDbType.NVarChar, 250), new SqlParameter("@County", SqlDbType.NVarChar, 250), new SqlParameter("@School", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Stoken", SqlDbType.NVarChar, 50), new SqlParameter("@StokenTime", SqlDbType.DateTime), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), 
                new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@UserPost", SqlDbType.Char, 0x24), new SqlParameter("@ExpirationDate", SqlDbType.DateTime), new SqlParameter("@UserId", SqlDbType.Char, 0x24)
             };
            parameterArray[0].Value = model.UserName;
            parameterArray[1].Value = model.Password;
            parameterArray[2].Value = model.TrueName;
            parameterArray[3].Value = model.UserIdentity;
            parameterArray[4].Value = model.Birthday;
            parameterArray[5].Value = model.Sex;
            parameterArray[6].Value = model.Email;
            parameterArray[7].Value = model.Mobile;
            parameterArray[8].Value = model.Province;
            parameterArray[9].Value = model.City;
            parameterArray[10].Value = model.County;
            parameterArray[11].Value = model.School;
            parameterArray[12].Value = model.CreateTime;
            parameterArray[13].Value = model.Stoken;
            parameterArray[14].Value = model.StokenTime;
            parameterArray[15].Value = model.Resource_Version;
            parameterArray[0x10].Value = model.GradeTerm;
            parameterArray[0x11].Value = model.Subject;
            parameterArray[0x12].Value = model.UserPost;
            parameterArray[0x13].Value = model.ExpirationDate;
            parameterArray[20].Value = model.UserId;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("insert into Msg(");
            builder.Append("MsgId,MsgEnum,MsgTypeEnum,ResourceDataId,MsgTitle,MsgContent,MsgStatus,MsgSender,MsgAccepter,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@MsgId,@MsgEnum,@MsgTypeEnum,@ResourceDataId,@MsgTitle,@MsgContent,@MsgStatus,@MsgSender,@MsgAccepter,@CreateTime,@CreateUser)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@MsgId", SqlDbType.Char, 0x24), new SqlParameter("@MsgEnum", SqlDbType.VarChar, 20), new SqlParameter("@MsgTypeEnum", SqlDbType.VarChar, 20), new SqlParameter("@ResourceDataId", SqlDbType.Char, 0x24), new SqlParameter("@MsgTitle", SqlDbType.NVarChar, 100), new SqlParameter("@MsgContent", SqlDbType.NVarChar, 500), new SqlParameter("@MsgStatus", SqlDbType.VarChar, 20), new SqlParameter("@MsgSender", SqlDbType.Char, 0x24), new SqlParameter("@MsgAccepter", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            parameterArray2[0].Value = modelMsg.MsgId;
            parameterArray2[1].Value = modelMsg.MsgEnum;
            parameterArray2[2].Value = modelMsg.MsgTypeEnum;
            parameterArray2[3].Value = modelMsg.ResourceDataId;
            parameterArray2[4].Value = modelMsg.MsgTitle;
            parameterArray2[5].Value = modelMsg.MsgContent;
            parameterArray2[6].Value = modelMsg.MsgStatus;
            parameterArray2[7].Value = modelMsg.MsgSender;
            parameterArray2[8].Value = modelMsg.MsgAccepter;
            parameterArray2[9].Value = modelMsg.CreateTime;
            parameterArray2[10].Value = modelMsg.CreateUser;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public bool Update(Rc.Model.Resources.Model_F_User model)
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
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("Stoken=@Stoken,");
            builder.Append("StokenTime=@StokenTime,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("UserPost=@UserPost,");
            builder.Append("ExpirationDate=@ExpirationDate");
            builder.Append(" where UserId=@UserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.VarChar, 50), new SqlParameter("@Password", SqlDbType.Char, 0x20), new SqlParameter("@TrueName", SqlDbType.NVarChar, 250), new SqlParameter("@UserIdentity", SqlDbType.Char, 1), new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@Sex", SqlDbType.Char, 1), new SqlParameter("@Email", SqlDbType.VarChar, 200), new SqlParameter("@Mobile", SqlDbType.VarChar, 20), new SqlParameter("@Province", SqlDbType.NVarChar, 250), new SqlParameter("@City", SqlDbType.NVarChar, 250), new SqlParameter("@County", SqlDbType.NVarChar, 250), new SqlParameter("@School", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Stoken", SqlDbType.NVarChar, 50), new SqlParameter("@StokenTime", SqlDbType.DateTime), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), 
                new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@UserPost", SqlDbType.Char, 0x24), new SqlParameter("@ExpirationDate", SqlDbType.DateTime), new SqlParameter("@UserId", SqlDbType.Char, 0x24)
             };
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
            cmdParms[13].Value = model.Stoken;
            cmdParms[14].Value = model.StokenTime;
            cmdParms[15].Value = model.Resource_Version;
            cmdParms[0x10].Value = model.GradeTerm;
            cmdParms[0x11].Value = model.Subject;
            cmdParms[0x12].Value = model.UserPost;
            cmdParms[0x13].Value = model.ExpirationDate;
            cmdParms[20].Value = model.UserId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

