namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SystemLogFileSync
    {
        private readonly DAL_SystemLogFileSync dal = new DAL_SystemLogFileSync();

        public bool Add(Model_SystemLogFileSync model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SystemLogFileSync> DataTableToList(DataTable dt)
        {
            List<Model_SystemLogFileSync> list = new List<Model_SystemLogFileSync>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SystemLogFileSync item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SystemLogFileSyncID)
        {
            return this.dal.Delete(SystemLogFileSyncID);
        }

        public bool DeleteList(string SystemLogFileSyncIDlist)
        {
            return this.dal.DeleteList(SystemLogFileSyncIDlist);
        }

        public bool Exists(string SystemLogFileSyncID)
        {
            return this.dal.Exists(SystemLogFileSyncID);
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

        public Model_SystemLogFileSync GetModel(string SystemLogFileSyncID)
        {
            return this.dal.GetModel(SystemLogFileSyncID);
        }

        public Model_SystemLogFileSync GetModelByCache(string SystemLogFileSyncID)
        {
            string cacheKey = "Model_SystemLogFileSyncModel-" + SystemLogFileSyncID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SystemLogFileSyncID);
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
            return (Model_SystemLogFileSync) cache;
        }

        public List<Model_SystemLogFileSync> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SystemLogFileSync model)
        {
            return this.dal.Update(model);
        }
    }
}

