namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SyncData
    {
        private readonly DAL_SyncData dal = new DAL_SyncData();

        public bool Add(Model_SyncData model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SyncData> DataTableToList(DataTable dt)
        {
            List<Model_SyncData> list = new List<Model_SyncData>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SyncData item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SyncDataId)
        {
            return this.dal.Delete(SyncDataId);
        }

        public bool DeleteList(string SyncDataIdlist)
        {
            return this.dal.DeleteList(SyncDataIdlist);
        }

        public bool Exists(string SyncDataId)
        {
            return this.dal.Exists(SyncDataId);
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

        public Model_SyncData GetModel(string SyncDataId)
        {
            return this.dal.GetModel(SyncDataId);
        }

        public Model_SyncData GetModelByCache(string SyncDataId)
        {
            string cacheKey = "Model_SyncDataModel-" + SyncDataId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SyncDataId);
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
            return (Model_SyncData) cache;
        }

        public List<Model_SyncData> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SyncData model)
        {
            return this.dal.Update(model);
        }
    }
}

