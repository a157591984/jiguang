namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Data;

    public class BLL_clsAuth
    {
        private readonly DAL_clsAuth dal = new DAL_clsAuth();

        public int AddLogError(int source, string strModulePath, string strContent)
        {
            return this.dal.AddLogError(source, strModulePath, strContent);
        }

        public int AddLogErrorFromBS(string strModulePath, string strContent)
        {
            return this.dal.AddLogErrorFromBS(strModulePath, strContent);
        }

        public int AddLogErrorFromCS(string strModulePath, string strContent)
        {
            return this.dal.AddLogErrorFromCS(strModulePath, strContent);
        }

        public int AddLogFromBS(string strModulePath, string strContent)
        {
            return this.dal.AddLogFromBS(strModulePath, strContent);
        }

        public int AddLogFromCS(string strModulePath, string eventContent)
        {
            return this.dal.AddLogFromCS(strModulePath, eventContent);
        }

        public int AddLogFromPrescriptionQuery(string strModulePath, string strContent, string SystemLog_Remark)
        {
            return this.dal.AddLogFromPrescriptionQuery(strModulePath, strContent, SystemLog_Remark);
        }

        public DataTable GetOwenTree(string DoctorInfo_ID, string SysRole_IDS, string ModuleIDLike)
        {
            return this.dal.GetOwenTree(DoctorInfo_ID, SysRole_IDS, ModuleIDLike);
        }

        public DataTable GetOwenTreeByCatch(string DoctorInfo_ID, string SysRole_IDS, string ModuleIDLike)
        {
            return this.dal.GetOwenTreeByCatch(DoctorInfo_ID, SysRole_IDS, ModuleIDLike);
        }

        public DataTable GetOwenTreeByCatch(string DoctorInfo_ID, string SysRole_IDS, string ModuleIDLike, bool IsEnableDataCache)
        {
            return this.dal.GetOwenTreeByCatch(DoctorInfo_ID, SysRole_IDS, ModuleIDLike, IsEnableDataCache);
        }

        public DataTable GetOwenTreeByCatchBySysCode(string DoctorInfo_ID, string SysRole_IDS, string ModuleIDLike)
        {
            return this.dal.GetOwenTreeByCatchBySysCode(DoctorInfo_ID, SysRole_IDS, ModuleIDLike);
        }

        public DataTable GetOwenTreeByCatchBySysCode(string DoctorInfo_ID, string SysRole_IDS, string ModuleIDLike, bool IsEnableDataCache)
        {
            return this.dal.GetOwenTreeByCatchBySysCode(DoctorInfo_ID, SysRole_IDS, ModuleIDLike, IsEnableDataCache);
        }

        public DataTable GetOwenTreeBySysCode(string DoctorInfo_ID, string SysRole_IDS, string ModuleIDLike)
        {
            return this.dal.GetOwenTreeBySysCode(DoctorInfo_ID, SysRole_IDS, ModuleIDLike);
        }

        public string GetSetMap(string ModuleId, string type)
        {
            return this.dal.GetSetMap(ModuleId, type);
        }

        public Model_Struct_Func GetUserFunc(string SysUser_ID, string SysRole_IDs, string ModuleID)
        {
            return this.dal.GetUserFunc(SysUser_ID, SysRole_IDs, ModuleID);
        }

        public DataSet GetUserFunction(string DoctorInfo_ID, string SysRole_IDs, string ModuleID)
        {
            return this.dal.GetUserFunction(DoctorInfo_ID, SysRole_IDs, ModuleID);
        }
    }
}

