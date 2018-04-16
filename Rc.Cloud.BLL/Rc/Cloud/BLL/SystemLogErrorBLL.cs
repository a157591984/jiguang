namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class SystemLogErrorBLL
    {
        private readonly DAL_SystemLogError DAL = new DAL_SystemLogError();

        public int Add(Model_SystemLogError model)
        {
            return this.DAL.Add(model);
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DAL.DeleteByCondition(strCondition, param);
        }

        public int DeleteByPK(string systemlog_id)
        {
            return this.DAL.DeleteByPK(systemlog_id);
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.DAL.ExistsByCondition(conditionStr, paramValues);
        }

        public bool ExistsByPK(string systemlog_id)
        {
            return this.DAL.ExistsByPK(systemlog_id);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSystemLogErrorCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public Model_SystemLogError GetModel_SystemLogErrorByPK(string systemlog_id)
        {
            return this.DAL.GetModel_SystemLogErrorByPK(systemlog_id);
        }

        public List<Model_SystemLogError> GetModel_SystemLogErrorList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetModel_SystemLogErrorList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SystemLogError> GetModel_SystemLogErrorListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetModel_SystemLogErrorListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SystemLogError> GetModel_SystemLogErrorListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSystemLogErrorCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetModel_SystemLogErrorListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public int GetSystemLogErrorCount(string strCondition, params object[] param)
        {
            return this.DAL.GetSystemLogErrorCount(strCondition, param);
        }

        public Model_SystemLogError GetSystemLogErrorModelBySystemLog_ID(string strWhere)
        {
            return this.DAL.GetSystemLogErrorModelBySystemLog_ID(strWhere);
        }

        public DataSet selectAllSystemLogErrorModel(string txtEC, string name, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.DAL.selectAllSystemLogErrorModel(txtEC, name, PageIndex, PageSize, out rCount, out pCount);
        }

        public int Update(Model_SystemLogError model)
        {
            return this.DAL.Update(model);
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.DAL.Update(strUpdateColumns, strCondition, param);
        }
    }
}

