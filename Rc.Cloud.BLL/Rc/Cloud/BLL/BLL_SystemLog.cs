namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_SystemLog
    {
        private readonly DAL_SystemLog DAL = new DAL_SystemLog();

        public int Add(Model_SystemLog model)
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
            recordCount = this.GetSystemLogCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public Model_SystemLog GetModel_SystemLogByPK(string systemlog_id)
        {
            return this.DAL.GetModel_SystemLogByPK(systemlog_id);
        }

        public List<Model_SystemLog> GetModel_SystemLogList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetModel_SystemLogList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SystemLog> GetModel_SystemLogListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetModel_SystemLogListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SystemLog> GetModel_SystemLogListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSystemLogCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetModel_SystemLogListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public int GetSystemLogCount(string strCondition, params object[] param)
        {
            return this.DAL.GetSystemLogCount(strCondition, param);
        }

        public DataSet SelectAllSystemLogModel(string txtEC, string name, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.DAL.SelectAllSystemLogModel(txtEC, name, PageIndex, PageSize, out rCount, out pCount);
        }

        public int Update(Model_SystemLog model)
        {
            return this.DAL.Update(model);
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.DAL.Update(strUpdateColumns, strCondition, param);
        }
    }
}

