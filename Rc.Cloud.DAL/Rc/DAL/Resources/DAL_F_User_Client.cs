namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_F_User_Client
    {
        public Model_F_User_Client DataRowToModel(DataRow row)
        {
            Model_F_User_Client client = new Model_F_User_Client();
            if (row != null)
            {
                if (row["UserId"] != null)
                {
                    client.UserId = row["UserId"].ToString();
                }
                if (row["UserName"] != null)
                {
                    client.UserName = row["UserName"].ToString();
                }
                if (row["Password"] != null)
                {
                    client.Password = row["Password"].ToString();
                }
                if (row["TrueName"] != null)
                {
                    client.TrueName = row["TrueName"].ToString();
                }
                if (row["UserIdentity"] != null)
                {
                    client.UserIdentity = row["UserIdentity"].ToString();
                }
                if ((row["Birthday"] != null) && (row["Birthday"].ToString() != ""))
                {
                    client.Birthday = new DateTime?(DateTime.Parse(row["Birthday"].ToString()));
                }
                if (row["Sex"] != null)
                {
                    client.Sex = row["Sex"].ToString();
                }
                if (row["Email"] != null)
                {
                    client.Email = row["Email"].ToString();
                }
                if (row["Mobile"] != null)
                {
                    client.Mobile = row["Mobile"].ToString();
                }
                if (row["Province"] != null)
                {
                    client.Province = row["Province"].ToString();
                }
                if (row["City"] != null)
                {
                    client.City = row["City"].ToString();
                }
                if (row["County"] != null)
                {
                    client.County = row["County"].ToString();
                }
                if (row["School"] != null)
                {
                    client.School = row["School"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    client.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["Stoken"] != null)
                {
                    client.Stoken = row["Stoken"].ToString();
                }
                if ((row["StokenTime"] != null) && (row["StokenTime"].ToString() != ""))
                {
                    client.StokenTime = new DateTime?(DateTime.Parse(row["StokenTime"].ToString()));
                }
                if (row["Resource_Version"] != null)
                {
                    client.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["GradeTerm"] != null)
                {
                    client.GradeTerm = row["GradeTerm"].ToString();
                }
                if (row["Subject"] != null)
                {
                    client.Subject = row["Subject"].ToString();
                }
                if (row["UserPost"] != null)
                {
                    client.UserPost = row["UserPost"].ToString();
                }
                if ((row["ExpirationDate"] != null) && (row["ExpirationDate"].ToString() != ""))
                {
                    client.ExpirationDate = new DateTime?(DateTime.Parse(row["ExpirationDate"].ToString()));
                }
                if ((row["product_type"] != null) && (row["product_type"].ToString() != ""))
                {
                    client.product_type = row["product_type"].ToString();
                }
            }
            return client;
        }

        public Model_F_User_Client GetTPUserModelByClientLogin(string UserName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\nselect  UserId,UserName,Password,\r\nTrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County\r\n,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate,null as product_type \r\nfrom F_User where UserName=@UserName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.Char, 100) };
            cmdParms[0].Value = UserName;
            new Model_F_User_Client();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_F_User_Client GetUserModelByClientLogin(string UserName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1 * FROM \r\n(\r\nselect  UserId,UserName,Password,\r\nTrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County\r\n,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate,null as product_type \r\nfrom F_User where UserName=@UserName \r\nUNION ALL\r\nselect SysUser_ID as UserId,SysUser_LoginName as username,SysUser_PassWord as Password  \r\n, SysUser_Name as TrueName,'A' AS UserIdentity,NULL Birthday,NULL Sex,\r\nNULL Email,NULL Mobile,NULL Province,NULL City,NULL County\r\n,NULL School,NULL CreateTime,NULL Stoken,NULL StokenTime,NULL Resource_Version,NULL GradeTerm,NULL Subject,NULL UserPost,NULL ExpirationDate,null as product_type \r\nfrom SysUser where SysUser_LoginName=@UserName \r\n) A ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.Char, 100) };
            cmdParms[0].Value = UserName;
            new Model_F_User_Client();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_F_User_Client GetUserModelByClientLogin(string UserName, string Password)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1 * FROM \r\n(\r\nselect  UserId,UserName,Password,\r\nTrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County\r\n,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate,null as product_type \r\nfrom F_User where UserName=@UserName and Password=@Password \r\nUNION ALL\r\nselect SysUser_ID as UserId,SysUser_LoginName as username,SysUser_PassWord as Password  \r\n, SysUser_Name as TrueName,'A' AS UserIdentity,NULL Birthday,NULL Sex,\r\nNULL Email,NULL Mobile,NULL Province,NULL City,NULL County\r\n,NULL School,NULL CreateTime,NULL Stoken,NULL StokenTime,NULL Resource_Version,NULL GradeTerm,NULL Subject,NULL UserPost,NULL ExpirationDate,null as product_type \r\nfrom SysUser where SysUser_LoginName=@UserName and SysUser_PassWord=@Password \r\n) A ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.Char, 100), new SqlParameter("@Password", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserName;
            cmdParms[1].Value = Password;
            new Model_F_User_Client();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_F_User_Client GetUserModelByClientToken(string user_id, string token, string product_type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1 * FROM \r\n(\r\nselect  UserId,UserName,Password,\r\nTrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County\r\n,School,CreateTime,t2.token as Stoken,t2.token_time as StokenTime,Resource_Version,GradeTerm,Subject,UserPost,ExpirationDate,t2.product_type \r\nfrom F_User t left join f_user_token t2 on t2.user_id=t.UserId where t.UserId=@user_id and t2.token=@token and t2.product_type=@product_type\r\nUNION ALL\r\nselect SysUser_ID as UserId,SysUser_LoginName as username,SysUser_PassWord as Password  \r\n, SysUser_Name as TrueName,'A' AS UserIdentity,NULL Birthday,NULL Sex,\r\nNULL Email,NULL Mobile,NULL Province,NULL City,NULL County\r\n,NULL School,NULL CreateTime,t2.token as Stoken,t2.token_time as StokenTime,NULL Resource_Version,NULL GradeTerm,NULL Subject,NULL UserPost,NULL ExpirationDate,t2.product_type  \r\nfrom SysUser t  left join SysUser_token t2 on t2.user_id=t.SysUser_ID where t.SysUser_ID=@user_id and t2.token=@token and t2.product_type=@product_type\r\n) A ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@user_id", SqlDbType.Char, 0x24), new SqlParameter("@token", SqlDbType.VarChar, 50), new SqlParameter("@product_type", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = user_id;
            cmdParms[1].Value = token;
            cmdParms[2].Value = product_type;
            new Model_F_User_Client();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }
    }
}

