namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class Common_DictBLL
    {
        private readonly Common_DictDAL DAL = new Common_DictDAL();

        public int Add(Common_DictModel model)
        {
            return this.DAL.Add(model);
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DAL.DeleteByCondition(strCondition, param);
        }

        public int DeleteByPK(string common_dict_id)
        {
            return this.DAL.DeleteByPK(common_dict_id);
        }

        public bool Exists(Common_DictModel model)
        {
            return this.DAL.Exists(model);
        }

        public bool Exists(Common_DictModel model, int type)
        {
            return this.DAL.Exists(model, type);
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.DAL.ExistsByCondition(conditionStr, paramValues);
        }

        public bool ExistsByPK(string common_dict_id)
        {
            return this.DAL.ExistsByPK(common_dict_id);
        }

        public DataSet GetCommon_DictByType(string D_Type)
        {
            return this.DAL.GetCommon_DictByType(D_Type);
        }

        public int GetCommon_DictCount(string strCondition, params object[] param)
        {
            return this.DAL.GetCommon_DictCount(strCondition, param);
        }

        public DataSet GetCommon_DictList(string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.DAL.GetCommon_DictList(strWhere, PageIndex, PageSize, out rCount, out pCount);
        }

        public Common_DictModel GetCommon_DictModelByPK(string common_dict_id)
        {
            return this.DAL.GetCommon_DictModelByPK(common_dict_id);
        }

        public List<Common_DictModel> GetCommon_DictModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetCommon_DictModelList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public List<Common_DictModel> GetCommon_DictModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetCommon_DictModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public List<Common_DictModel> GetCommon_DictModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetCommon_DictCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetCommon_DictModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public string GetD_Name(string D_Type)
        {
            return this.DAL.GetD_Name(D_Type);
        }

        public string GetD_NameByID(string id)
        {
            return this.DAL.GetD_NameByID(id);
        }

        public DataSet GetD_Type()
        {
            return this.DAL.GetD_Type();
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetCommon_DictCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public int GetMaxDType()
        {
            return this.DAL.GetMaxDType();
        }

        public bool SysCommon_DictExists(Common_DictModel model)
        {
            return this.DAL.SysCommon_DictExists(model);
        }

        public int Update(Common_DictModel model)
        {
            return this.DAL.Update(model);
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.DAL.Update(strUpdateColumns, strCondition, param);
        }
    }
}

