namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class SysUser_For_CustomerInfoBLL
    {
        private readonly SysUser_For_CustomerInfoDAL DAL = new SysUser_For_CustomerInfoDAL();

        public int Add(SysUser_For_CustomerInfoModel model)
        {
            return this.DAL.Add(model);
        }

        public bool AddSysUser_For_CustomerInfo(string sysUser_ID, string customerInfo_ID)
        {
            return this.DAL.AddSysUser_For_CustomerInfo(sysUser_ID, customerInfo_ID);
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DAL.DeleteByCondition(strCondition, param);
        }

        public int DeleteByPK(string sysuser_id)
        {
            return this.DAL.DeleteByPK(sysuser_id);
        }

        public bool EditSysUser_For_CustomerInfo(string sysUser_ID, string customerInfo_ID)
        {
            return this.DAL.EditSysUser_For_CustomerInfo(sysUser_ID, customerInfo_ID);
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.DAL.ExistsByCondition(conditionStr, paramValues);
        }

        public bool ExistsByPK(string sysuser_id)
        {
            return this.DAL.ExistsByPK(sysuser_id);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSysUser_For_CustomerInfoCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public int GetSysUser_For_CustomerInfoCount(string strCondition, params object[] param)
        {
            return this.DAL.GetSysUser_For_CustomerInfoCount(strCondition, param);
        }

        public SysUser_For_CustomerInfoModel GetSysUser_For_CustomerInfoModelByPK(string sysuser_id)
        {
            return this.DAL.GetSysUser_For_CustomerInfoModelByPK(sysuser_id);
        }

        public List<SysUser_For_CustomerInfoModel> GetSysUser_For_CustomerInfoModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetSysUser_For_CustomerInfoModelList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public List<SysUser_For_CustomerInfoModel> GetSysUser_For_CustomerInfoModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetSysUser_For_CustomerInfoModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public List<SysUser_For_CustomerInfoModel> GetSysUser_For_CustomerInfoModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSysUser_For_CustomerInfoCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetSysUser_For_CustomerInfoModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public int Update(SysUser_For_CustomerInfoModel model)
        {
            return this.DAL.Update(model);
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.DAL.Update(strUpdateColumns, strCondition, param);
        }
    }
}

