namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_SysCode
    {
        private DAL_SysCode DAL = new DAL_SysCode();

        public bool AddSysModule(Model_SysModule model)
        {
            return this.DAL.AddSysModule(model);
        }

        public bool DeleteBaseModule(string moduleID, string sysCode)
        {
            return this.DAL.DeleteBaseModule(moduleID, sysCode);
        }

        public bool ExistsSysModule(Model_SysModule model, string type)
        {
            return this.DAL.ExistsSysModule(model, type);
        }

        public DataSet GetComparisonDataList(string id, string syscode, string dataBase)
        {
            return this.DAL.GetComparisonDataList(id, syscode, dataBase);
        }

        public DataSet GetDataList(string dataBase, string condition)
        {
            return this.DAL.GetDataList(dataBase, condition);
        }

        public DataSet GetModuleListBySysCode(string condition)
        {
            return this.DAL.GetModuleListBySysCode(condition);
        }

        public DataSet GetModuleListBySysCode(string condition, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.DAL.GetModuleListBySysCode(condition, PageIndex, PageSize, out rCount, out pCount);
        }

        public DataSet GetSysModuleColumnName()
        {
            return this.DAL.GetSysModuleColumnName();
        }

        public Model_SysModule GetSysModuleModelBySyscodeAndModuleID(string syscode, string moduleid)
        {
            return this.DAL.GetSysModuleModelBySyscodeAndModuleID(syscode, moduleid);
        }

        public DataSet GetSysName()
        {
            return this.DAL.GetSysName();
        }

        public DataSet GetUpdateDataList(string where, string dataBase, string condition)
        {
            return this.DAL.GetUpdateDataList(where, dataBase, condition);
        }

        public bool UpdateSysModuleBySyscodeAndModuleID(Model_SysModule model)
        {
            return this.DAL.UpdateSysModuleBySyscodeAndModuleID(model);
        }
    }
}

