namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_SysConfigurationItems_Dict
    {
        private readonly DAL_SysConfigurationItems_Dict DAL = new DAL_SysConfigurationItems_Dict();

        public int Add(Model_SysConfigurationItems_Dict model)
        {
            return this.DAL.Add(model);
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DAL.DeleteByCondition(strCondition, param);
        }

        public int DeleteByPK(string sysconfigurationitems_dict_id)
        {
            return this.DAL.DeleteByPK(sysconfigurationitems_dict_id);
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.DAL.ExistsByCondition(conditionStr, paramValues);
        }

        public bool ExistsByPK(string sysconfigurationitems_dict_id)
        {
            return this.DAL.ExistsByPK(sysconfigurationitems_dict_id);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSysConfigurationItems_DictCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetSysConfigurationItems(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetSysConfigurationItems(recordNum, orderColumn, orderType, strCondition, param);
        }

        public int GetSysConfigurationItems_DictCount(string strCondition, params object[] param)
        {
            return this.DAL.GetSysConfigurationItems_DictCount(strCondition, param);
        }

        public Model_SysConfigurationItems_Dict GetSysConfigurationItems_DictModelByPK(string sysconfigurationitems_dict_id)
        {
            return this.DAL.GetSysConfigurationItems_DictModelByPK(sysconfigurationitems_dict_id);
        }

        public List<Model_SysConfigurationItems_Dict> GetSysConfigurationItems_DictModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetSysConfigurationItems_DictModelList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SysConfigurationItems_Dict> GetSysConfigurationItems_DictModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetSysConfigurationItems_DictModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SysConfigurationItems_Dict> GetSysConfigurationItems_DictModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSysConfigurationItems_DictCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetSysConfigurationItems_DictModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public int Update(Model_SysConfigurationItems_Dict model)
        {
            return this.DAL.Update(model);
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.DAL.Update(strUpdateColumns, strCondition, param);
        }
    }
}

