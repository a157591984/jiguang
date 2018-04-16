namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_ExecutionHardware
    {
        private readonly DAL_ExecutionHardware DAL = new DAL_ExecutionHardware();

        public int Add(Model_ExecutionHardware model)
        {
            return this.DAL.Add(model);
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DAL.DeleteByCondition(strCondition, param);
        }

        public int DeleteByPK(string executionhardware_id)
        {
            return this.DAL.DeleteByPK(executionhardware_id);
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.DAL.ExistsByCondition(conditionStr, paramValues);
        }

        public bool ExistsByLogic(string executionhardware_id)
        {
            return this.DAL.ExistsByLogic(executionhardware_id);
        }

        public bool ExistsByPK(string executionhardware_id)
        {
            return this.DAL.ExistsByPK(executionhardware_id);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetExecutionHardwareCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public int GetExecutionHardwareCount(string strCondition, params object[] param)
        {
            return this.DAL.GetExecutionHardwareCount(strCondition, param);
        }

        public DataSet GetListByID(string id)
        {
            return this.DAL.GetListByID(id);
        }

        public DataSet GetListPaged(string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.DAL.GetListPaged(strWhere, PageIndex, PageSize, out rCount, out pCount);
        }

        public DataSet GetMaxCode(string tableName)
        {
            return this.DAL.GetMaxCode(tableName);
        }

        public Model_ExecutionHardware GetModel_ExecutionHardwareByLogic(string executionhardware_id)
        {
            return this.DAL.GetModel_ExecutionHardwareByLogic(executionhardware_id);
        }

        public Model_ExecutionHardware GetModel_ExecutionHardwareByPK(string executionhardware_id)
        {
            return this.DAL.GetModel_ExecutionHardwareByPK(executionhardware_id);
        }

        public List<Model_ExecutionHardware> GetModel_ExecutionHardwareList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetModel_ExecutionHardwareList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public List<Model_ExecutionHardware> GetModel_ExecutionHardwareListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetModel_ExecutionHardwareListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public List<Model_ExecutionHardware> GetModel_ExecutionHardwareListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetExecutionHardwareCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetModel_ExecutionHardwareListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public int Update(Model_ExecutionHardware model)
        {
            return this.DAL.Update(model);
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.DAL.Update(strUpdateColumns, strCondition, param);
        }
    }
}

