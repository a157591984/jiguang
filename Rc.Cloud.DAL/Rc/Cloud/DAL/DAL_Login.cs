namespace Rc.Cloud.DAL
{
    using   Rc.Common.StrUtility;
    using Rc.Common.DBUtility;
    using System;
    using System.Text;

    public class DAL_Login
    {
        public bool UpdateSysUserChangePassword(string old_, string new_, string login_name)
        {
            bool flag = false;
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(*) from SysUser ");
            builder.Append(" where SysUser_LoginName ='");
            builder.Append(login_name);
            builder.Append("' and ");
            builder.Append(" SysUser_PassWord ='");
            builder.Append(Rc.Common.StrUtility.EncryptUtility.MyEncryptString(old_));
            builder.Append("'");
            if ((int.Parse(DbHelperSQL.GetSingle(builder.ToString()).ToString()) > 0) && (DbHelperSQL.ExecuteSql(string.Format("update SysUser set SysUser_PassWord='{0}' where SysUser_LoginName='{1}'", Rc.Common.StrUtility.EncryptUtility.MyEncryptString(new_), login_name)) > 0))
            {
                flag = true;
            }
            return flag;
        }
    }
}

