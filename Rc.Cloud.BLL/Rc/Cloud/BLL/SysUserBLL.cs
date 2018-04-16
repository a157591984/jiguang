namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class SysUserBLL
    {
        private readonly SysUserDAL DAL = new SysUserDAL();

        public int Add(Model_SysUser model)
        {
            return this.DAL.Add(model);
        }

        public int AddSysUser(Model_SysUser Model_SysUser, SysRoleModel SysRoleModel, string sType)
        {
            return this.DAL.AddSysUser(Model_SysUser, SysRoleModel, sType);
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DAL.DeleteByCondition(strCondition, param);
        }

        public int DeleteByPK(string sysuser_id)
        {
            return this.DAL.DeleteByPK(sysuser_id);
        }

        public bool DeleteSysUserRoleRelationByModuleID(string sysUser_ID)
        {
            return this.DAL.DeleteSysUserRoleRelationByModuleID(sysUser_ID);
        }

        public bool EditSysUser_For_CustomerInfo(string sysUser_ID, string customerInfo_ID)
        {
            return this.DAL.EditSysUser_For_CustomerInfo(sysUser_ID, customerInfo_ID);
        }

        public bool EditSysUser_For_CustomerInfoTwo(string sysUser_ID, string customerInfo_ID)
        {
            return this.DAL.EditSysUser_For_CustomerInfoTwo(sysUser_ID, customerInfo_ID);
        }

        public bool Exists(Model_SysUser model, int i)
        {
            return this.DAL.Exists(model, i);
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.DAL.ExistsByCondition(conditionStr, paramValues);
        }

        public bool ExistsByPK(string sysuser_id)
        {
            return this.DAL.ExistsByPK(sysuser_id);
        }

        public DataSet GetCustomerInfo()
        {
            return this.DAL.GetCustomerInfo();
        }

        public string GetCustomerInfo_NameCN(string SysUser_ID)
        {
            return this.DAL.GetCustomerInfo_NameCN(SysUser_ID);
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

        public DataSet GetRoleList()
        {
            return this.DAL.GetRoleList();
        }

        public string GetSysRole_Name(string SysUser_ID)
        {
            return this.DAL.GetSysRole_Name(SysUser_ID);
        }

        public DataSet GetSysUser_For_CustomerInfo(string sysUser_ID)
        {
            return this.DAL.GetSysUser_For_CustomerInfo(sysUser_ID);
        }

        public DataSet GetSysUser_For_CustomerInfoTwo(string CustomerInfo_ID)
        {
            return this.DAL.GetSysUser_For_CustomerInfoTwo(CustomerInfo_ID);
        }

        public int GetSysUserCount(string strCondition, params object[] param)
        {
            return this.DAL.GetSysUserCount(strCondition, param);
        }

        public DataSet GetSysUserInfo(string sysUser_ID)
        {
            return this.DAL.GetSysUserInfo(sysUser_ID);
        }

        public Model_SysUser GetSysUserModelByPK(string sysuser_id)
        {
            return this.DAL.GetSysUserModelByPK(sysuser_id);
        }

        public List<Model_SysUser> GetSysUserModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetSysUserModelList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SysUser> GetSysUserModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetSysUserModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SysUser> GetSysUserModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSysUserCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetSysUserModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetUserList(string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.DAL.GetSysUserList(strWhere, PageIndex, PageSize, out rCount, out pCount);
        }

        public DataSet GetUserRoleInfo(string sysUser_ID)
        {
            return this.DAL.GetUserRoleInfo(sysUser_ID);
        }

        public bool MyInfoUpdate(Model_SysUser model)
        {
            return this.DAL.MyInfoUpdate(model);
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

