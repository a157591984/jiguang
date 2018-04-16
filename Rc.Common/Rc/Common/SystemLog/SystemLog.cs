namespace Rc.Common.SystemLog
{
    using Rc.Common.DBUtility;
    using Rc.Common.StrUtility;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;

    public class SystemLog
    {
        public static int AddLog(string strDoctorInfo_ID, int source, string strModulePath, string strContent, int SystemLog_Type = -1, string SystemLog_Remark = "")
        {
            if (HttpContext.Current == null)
            {
                return 0;
            }
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;
            string str2 = HttpContext.Current.Request.Url.ToString().Filter();
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO [SystemLog]");
            builder.Append(" ([SystemLog_ID]");
            builder.Append(" ,[SystemLog_Level]");
            builder.Append(" ,[SystemLog_Model]");
            builder.Append(" ,[SystemLog_Desc]");
            builder.Append(" ,[SystemLog_LoginID]");
            builder.Append(" ,[SystemLog_CreateDate]");
            builder.Append(" ,[SystemLog_IP]");
            builder.Append(" ,[SystemLog_Type]");
            builder.Append(" ,[SystemLog_Remark]");
            builder.Append(" ,[SystemLog_Source])");
            builder.Append(" VALUES ");
            builder.Append(" (");
            builder.Append(" newid(), ");
            builder.Append(" '" + str2 + "',");
            builder.Append(" '" + GetSetMap(strModulePath, "1") + "',");
            builder.Append(" '" + strContent + "',");
            builder.Append(" '" + strDoctorInfo_ID + "',");
            builder.Append(" getdate(),");
            builder.Append(" '" + userHostAddress + "',");
            builder.Append(" " + SystemLog_Type + ",");
            builder.Append(" '" + SystemLog_Remark + "',");
            builder.Append(" " + source);
            builder.Append(") ");
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public static int AddLogError(string strDoctorInfo_ID, int source, string strModulePath, string strContent)
        {
            if (HttpContext.Current == null)
            {
                return 0;
            }
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;
            string str2 = HttpContext.Current.Request.Url.ToString().Filter();
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO [SystemLogError] ");
            builder.Append("([SystemLog_ID],[SystemLog_PagePath],[SystemLog_SysPath],[SystemLog_Desc],[SystemLog_LoginID],[SystemLog_CreateDate],[SystemLog_IP],[SystemLog_Source])");
            builder.Append(" values (");
            builder.Append("newid(),@SystemLog_PagePath,@SystemLog_SysPath,@SystemLog_Desc,@SystemLog_LoginID,getdate(),@SystemLog_IP,@SystemLog_Source)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SystemLog_PagePath", SqlDbType.NVarChar, 500), new SqlParameter("@SystemLog_SysPath", SqlDbType.NVarChar, 500), new SqlParameter("@SystemLog_Desc", SqlDbType.VarChar, 0xfa0), new SqlParameter("@SystemLog_LoginID", SqlDbType.NVarChar, 0x24), new SqlParameter("@SystemLog_IP", SqlDbType.NVarChar, 50), new SqlParameter("@SystemLog_Source", SqlDbType.Int, 1) };
            cmdParms[0].Value = str2;
            try
            {
                cmdParms[1].Value = GetSetMap(strModulePath, "1");
            }
            catch
            {
                cmdParms[1].Value = strModulePath;
            }
            cmdParms[2].Value = strContent;
            cmdParms[3].Value = strDoctorInfo_ID;
            cmdParms[4].Value = userHostAddress;
            cmdParms[5].Value = 1;
            return DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public static int AddLogErrorFromBS(string strDoctorInfo_ID, string strModulePath, string strContent)
        {
            return AddLogError(strDoctorInfo_ID, 1, strModulePath, strContent);
        }

        public static int AddLogErrorFromCS(string strDoctorInfo_ID, string strModulePath, string strContent)
        {
            return AddLogError(strDoctorInfo_ID, 2, strModulePath, strContent);
        }

        public static int AddLogFromBS(string strDoctorInfo_ID, string strModulePath, string strContent)
        {
            return AddLog(strDoctorInfo_ID, 1, strModulePath, strContent, -1, "");
        }

        public static int AddLogFromCS(string strDoctorInfo_ID, string strModulePath, string eventContent)
        {
            return AddLog(strDoctorInfo_ID, 2, strModulePath, eventContent, -1, "");
        }

        public static int ErrorLog(Type type, string message, string methodNmae, string classRemark, Exception ex)
        {
            return AddLogErrorFromBS("", classRemark + ":" + type.FullName + "." + methodNmae, "Message:" + message + ";Exception:" + ex.Message);
        }

        public static string GetSetMap(string ModuleId, string type)
        {
            object single = DbHelperSQL.GetSingle("SELECT * from GetModuleLineBySysCode('" + ModuleId + "'," + type + ",'" + clsUtility.GetSysCode() + "')");
            if (single == null)
            {
                return ModuleId;
            }
            return single.ToString();
        }
    }
}

