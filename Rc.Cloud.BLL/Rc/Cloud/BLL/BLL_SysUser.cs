namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_SysUser
    {
        private DAL_SysUser dal = new DAL_SysUser();
        private readonly DAL_SysUser DAL = new DAL_SysUser();

        public int Add(Model_SysUser model)
        {
            return this.DAL.Add(model);
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DAL.DeleteByCondition(strCondition, param);
        }

        public int DeleteByPK(string sysuser_id)
        {
            return this.DAL.DeleteByPK(sysuser_id);
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.DAL.ExistsByCondition(conditionStr, paramValues);
        }

        public bool ExistsByLogic(string sysuser_id)
        {
            return this.DAL.ExistsByLogic(sysuser_id);
        }

        public bool ExistsByPK(string sysuser_id)
        {
            return this.DAL.ExistsByPK(sysuser_id);
        }

        public DataSet GetDataList()
        {
            return this.DAL.GetDataList();
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSysUserCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public Model_SysUser GetModel_SysUserByLogic(string sysuser_id)
        {
            return this.DAL.GetModel_SysUserByLogic(sysuser_id);
        }

        public Model_SysUser GetModel_SysUserByPK(string sysuser_id)
        {
            return this.DAL.GetModel_SysUserByPK(sysuser_id);
        }

        public List<Model_SysUser> GetModel_SysUserList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetModel_SysUserList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SysUser> GetModel_SysUserListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetModel_SysUserListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SysUser> GetModel_SysUserListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSysUserCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetModel_SysUserListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public string GetSysRole_Name(string SysUser_ID)
        {
            return this.dal.GetSysRole_Name(SysUser_ID);
        }

        public string GetSysUser_ID(string customerInfo_ID)
        {
            return this.dal.GetSysUser_ID(customerInfo_ID);
        }

        public int GetSysUserCount(string strCondition, params object[] param)
        {
            return this.DAL.GetSysUserCount(strCondition, param);
        }

        public DataSet GetSysUserListByName(string name, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.dal.GetSysUserListByName(name, PageIndex, PageSize, out rCount, out pCount);
        }

        public int Update(Model_SysUser model)
        {
            return this.DAL.Update(model);
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.DAL.Update(strUpdateColumns, strCondition, param);
        }
    }
}

