namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_DictionarySQlMaintenance
    {
        private readonly DAL_DictionarySQlMaintenance DAL = new DAL_DictionarySQlMaintenance();

        public int Add(Model_DictionarySQlMaintenance model)
        {
            return this.DAL.Add(model);
        }

        public int AddNew(Model_DictionarySQlMaintenance model)
        {
            return this.DAL.AddNew(model);
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DAL.DeleteByCondition(strCondition, param);
        }

        public int DeleteByPK(string dictionarysqlmaintenance_id)
        {
            return this.DAL.DeleteByPK(dictionarysqlmaintenance_id);
        }

        public int DeleteByPKNew(string dictionarysqlmaintenance_id)
        {
            return this.DAL.DeleteByPKNew(dictionarysqlmaintenance_id);
        }

        public DataSet DictionarySQlMaintenanceList(string Content)
        {
            return this.DAL.DictionarySQlMaintenanceList(Content);
        }

        public DataSet DictionarySQlMaintenanceList(string Content, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.DAL.DictionarySQlMaintenanceList(Content, PageIndex, PageSize, out rCount, out pCount);
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.DAL.ExistsByCondition(conditionStr, paramValues);
        }

        public bool ExistsByPK(string dictionarysqlmaintenance_id)
        {
            return this.DAL.ExistsByPK(dictionarysqlmaintenance_id);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetDictionarySQlMaintenanceCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public int GetDictionarySQlMaintenanceCount(string strCondition, params object[] param)
        {
            return this.DAL.GetDictionarySQlMaintenanceCount(strCondition, param);
        }

        public Model_DictionarySQlMaintenance GetDictionarySQlMaintenanceModelByPK(string dictionarysqlmaintenance_id)
        {
            return this.DAL.GetDictionarySQlMaintenanceModelByPK(dictionarysqlmaintenance_id);
        }

        public Model_DictionarySQlMaintenance GetDictionarySQlMaintenanceModelByPKNew(string dictionarysqlmaintenance_id)
        {
            return this.DAL.GetDictionarySQlMaintenanceModelByPKNew(dictionarysqlmaintenance_id);
        }

        public List<Model_DictionarySQlMaintenance> GetDictionarySQlMaintenanceModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetDictionarySQlMaintenanceModelList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public List<Model_DictionarySQlMaintenance> GetDictionarySQlMaintenanceModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetDictionarySQlMaintenanceModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public List<Model_DictionarySQlMaintenance> GetDictionarySQlMaintenanceModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetDictionarySQlMaintenanceCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetDictionarySQlMaintenanceModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public bool IsOrNotExists(Model_DictionarySQlMaintenance model, int i)
        {
            return this.DAL.IsOrNotExists(model, i);
        }

        public int Update(Model_DictionarySQlMaintenance model)
        {
            return this.DAL.Update(model);
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.DAL.Update(strUpdateColumns, strCondition, param);
        }

        public int UpdateNew(Model_DictionarySQlMaintenance model)
        {
            return this.DAL.UpdateNew(model);
        }
    }
}

