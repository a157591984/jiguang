namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_VSysUserRole
    {
        public Model_VSysUserRole GetSysUserInfoModelBySysUserId(string SysUserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * ");
            builder.Append(",dbo.f_GetUserRoleIDs(SysUser_ID) as SysRole_IDs ");
            builder.Append(",dbo.f_GetUserRoleNames(SysUser_ID) as SysRole_Names ");
            builder.Append(" from SysUser ");
            builder.Append(" where SysUser_Enable = 1 and SysUser_Id=@SysUser_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUser_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SysUserId;
            Model_VSysUserRole role = new Model_VSysUserRole();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["SysUser_ID"] != null) && (set.Tables[0].Rows[0]["SysUser_ID"].ToString() != ""))
            {
                role.SysUser_ID = set.Tables[0].Rows[0]["SysUser_ID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysUser_Name"] != null) && (set.Tables[0].Rows[0]["SysUser_Name"].ToString() != ""))
            {
                role.SysUser_Name = set.Tables[0].Rows[0]["SysUser_Name"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysRole_IDs"] != null) && (set.Tables[0].Rows[0]["SysRole_IDs"].ToString() != ""))
            {
                role.SysRole_IDs = set.Tables[0].Rows[0]["SysRole_IDs"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysRole_Names"] != null) && (set.Tables[0].Rows[0]["SysRole_Names"].ToString() != ""))
            {
                role.SysRole_Names = set.Tables[0].Rows[0]["SysRole_Names"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysUser_LoginName"] != null) && (set.Tables[0].Rows[0]["SysUser_LoginName"].ToString() != ""))
            {
                role.SysUser_LoginName = set.Tables[0].Rows[0]["SysUser_LoginName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysUser_PassWord"] != null) && (set.Tables[0].Rows[0]["SysUser_LoginName"].ToString() != ""))
            {
                role.SysUser_PassWord = set.Tables[0].Rows[0]["SysUser_PassWord"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysUser_Tel"] != null) && (set.Tables[0].Rows[0]["SysUser_Tel"].ToString() != ""))
            {
                role.SysUser_Tel = set.Tables[0].Rows[0]["SysUser_Tel"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysDepartment_ID"] != null) && (set.Tables[0].Rows[0]["SysDepartment_ID"].ToString() != ""))
            {
                role.SysDepartment_ID = set.Tables[0].Rows[0]["SysDepartment_ID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysUser_Enable"] != null) && (set.Tables[0].Rows[0]["SysUser_Enable"].ToString() != ""))
            {
                role.SysUser_Enable = new bool?(Convert.ToBoolean(set.Tables[0].Rows[0]["SysUser_Enable"]));
            }
            if ((set.Tables[0].Rows[0]["SysUser_Remark"] != null) && (set.Tables[0].Rows[0]["SysUser_Remark"].ToString() != ""))
            {
                role.SysUser_Remark = set.Tables[0].Rows[0]["SysUser_Remark"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CreateTime"] != null) && (set.Tables[0].Rows[0]["CreateTime"].ToString() != ""))
            {
                role.CreateTime = new DateTime?(Convert.ToDateTime(set.Tables[0].Rows[0]["CreateTime"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["UpdateTime"] != null) && (set.Tables[0].Rows[0]["UpdateTime"].ToString() != ""))
            {
                role.UpdateTime = new DateTime?(Convert.ToDateTime(set.Tables[0].Rows[0]["UpdateTime"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreateUser"] != null) && (set.Tables[0].Rows[0]["CreateUser"].ToString() != ""))
            {
                role.CreateUser = set.Tables[0].Rows[0]["CreateUser"].ToString();
            }
            if ((set.Tables[0].Rows[0]["UpdateUser"] != null) && (set.Tables[0].Rows[0]["UpdateUser"].ToString() != ""))
            {
                role.UpdateUser = set.Tables[0].Rows[0]["UpdateUser"].ToString();
            }
            return role;
        }

        public Model_VSysUserRole GetVDoctorInfoModelByLogin(string SysUser_LoginName, string SysUser_PassWord)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * ");
            builder.Append(",dbo.f_GetUserRoleIDs(SysUser_ID) as SysRole_IDs ");
            builder.Append(",dbo.f_GetUserRoleNames(SysUser_ID) as SysRole_Names ");
            builder.Append(" from SysUser ");
            builder.Append(" where SysUser_Enable = 1 and SysUser_LoginName=@SysUser_LoginName and SysUser_PassWord=@SysUser_PassWord ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUser_LoginName", SqlDbType.Char, 0x24), new SqlParameter("@SysUser_PassWord", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SysUser_LoginName;
            cmdParms[1].Value = SysUser_PassWord;
            Model_VSysUserRole role = new Model_VSysUserRole();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["SysUser_ID"] != null) && (set.Tables[0].Rows[0]["SysUser_ID"].ToString() != ""))
            {
                role.SysUser_ID = set.Tables[0].Rows[0]["SysUser_ID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysUser_Name"] != null) && (set.Tables[0].Rows[0]["SysUser_Name"].ToString() != ""))
            {
                role.SysUser_Name = set.Tables[0].Rows[0]["SysUser_Name"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysRole_IDs"] != null) && (set.Tables[0].Rows[0]["SysRole_IDs"].ToString() != ""))
            {
                role.SysRole_IDs = set.Tables[0].Rows[0]["SysRole_IDs"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysRole_Names"] != null) && (set.Tables[0].Rows[0]["SysRole_Names"].ToString() != ""))
            {
                role.SysRole_Names = set.Tables[0].Rows[0]["SysRole_Names"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysUser_LoginName"] != null) && (set.Tables[0].Rows[0]["SysUser_LoginName"].ToString() != ""))
            {
                role.SysUser_LoginName = set.Tables[0].Rows[0]["SysUser_LoginName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysUser_PassWord"] != null) && (set.Tables[0].Rows[0]["SysUser_LoginName"].ToString() != ""))
            {
                role.SysUser_PassWord = set.Tables[0].Rows[0]["SysUser_PassWord"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysUser_Tel"] != null) && (set.Tables[0].Rows[0]["SysUser_Tel"].ToString() != ""))
            {
                role.SysUser_Tel = set.Tables[0].Rows[0]["SysUser_Tel"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysDepartment_ID"] != null) && (set.Tables[0].Rows[0]["SysDepartment_ID"].ToString() != ""))
            {
                role.SysDepartment_ID = set.Tables[0].Rows[0]["SysDepartment_ID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["SysUser_Enable"] != null) && (set.Tables[0].Rows[0]["SysUser_Enable"].ToString() != ""))
            {
                role.SysUser_Enable = new bool?(Convert.ToBoolean(set.Tables[0].Rows[0]["SysUser_Enable"]));
            }
            if ((set.Tables[0].Rows[0]["SysUser_Remark"] != null) && (set.Tables[0].Rows[0]["SysUser_Remark"].ToString() != ""))
            {
                role.SysUser_Remark = set.Tables[0].Rows[0]["SysUser_Remark"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CreateTime"] != null) && (set.Tables[0].Rows[0]["CreateTime"].ToString() != ""))
            {
                role.CreateTime = new DateTime?(Convert.ToDateTime(set.Tables[0].Rows[0]["CreateTime"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["UpdateTime"] != null) && (set.Tables[0].Rows[0]["UpdateTime"].ToString() != ""))
            {
                role.UpdateTime = new DateTime?(Convert.ToDateTime(set.Tables[0].Rows[0]["UpdateTime"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreateUser"] != null) && (set.Tables[0].Rows[0]["CreateUser"].ToString() != ""))
            {
                role.CreateUser = set.Tables[0].Rows[0]["CreateUser"].ToString();
            }
            if ((set.Tables[0].Rows[0]["UpdateUser"] != null) && (set.Tables[0].Rows[0]["UpdateUser"].ToString() != ""))
            {
                role.UpdateUser = set.Tables[0].Rows[0]["UpdateUser"].ToString();
            }
            return role;
        }
    }
}

