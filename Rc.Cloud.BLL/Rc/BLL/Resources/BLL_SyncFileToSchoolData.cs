namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SyncFileToSchoolData
    {
        private readonly DAL_SyncFileToSchoolData dal = new DAL_SyncFileToSchoolData();

        public bool Add(Model_SyncFileToSchoolData model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SyncFileToSchoolData> DataTableToList(DataTable dt)
        {
            List<Model_SyncFileToSchoolData> list = new List<Model_SyncFileToSchoolData>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SyncFileToSchoolData item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SyncFileToSchoolData_Id)
        {
            return this.dal.Delete(SyncFileToSchoolData_Id);
        }

        public bool DeleteList(string SyncFileToSchoolData_Idlist)
        {
            return this.dal.DeleteList(SyncFileToSchoolData_Idlist);
        }

        public bool Exists(string SyncFileToSchoolData_Id)
        {
            return this.dal.Exists(SyncFileToSchoolData_Id);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetListByPage_Operate(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage_Operate(strWhere, orderby, startIndex, endIndex);
        }

        public Model_SyncFileToSchoolData GetModel(string SyncFileToSchoolData_Id)
        {
            return this.dal.GetModel(SyncFileToSchoolData_Id);
        }

        public Model_SyncFileToSchoolData GetModelByCache(string SyncFileToSchoolData_Id)
        {
            string cacheKey = "Model_SyncFileToSchoolDataModel-" + SyncFileToSchoolData_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SyncFileToSchoolData_Id);
                    if (cache != null)
                    {
                        int configInt = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Model_SyncFileToSchoolData) cache;
        }

        public List<Model_SyncFileToSchoolData> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCount_Operate(string strWhere)
        {
            return this.dal.GetRecordCount_Operate(strWhere);
        }

        public bool Update(Model_SyncFileToSchoolData model)
        {
            return this.dal.Update(model);
        }
    }
}

