namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SysFilter
    {
        private readonly DAL_SysFilter dal = new DAL_SysFilter();

        public bool Add(Model_SysFilter model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SysFilter> DataTableToList(DataTable dt)
        {
            List<Model_SysFilter> list = new List<Model_SysFilter>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SysFilter item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SysFilter_id)
        {
            return this.dal.Delete(SysFilter_id);
        }

        public bool DeleteList(string SysFilter_idlist)
        {
            return this.dal.DeleteList(SysFilter_idlist);
        }

        public bool Exists(string SysFilter_id)
        {
            return this.dal.Exists(SysFilter_id);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetFilterByCache()
        {
            try
            {
                object objObject = null;
                string cacheKey = "Model_SysFilter";
                objObject = DataCache.GetCache(cacheKey);
                if (objObject == null)
                {
                    objObject = this.dal.GetList("");
                    if (objObject != null)
                    {
                        int num = 360;
                        DataCache.SetCache(cacheKey, objObject, DateTime.Now.AddYears(num), TimeSpan.Zero);
                    }
                }
                return (DataSet) objObject;
            }
            catch
            {
                return null;
            }
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

        public Model_SysFilter GetModel(string SysFilter_id)
        {
            return this.dal.GetModel(SysFilter_id);
        }

        public Model_SysFilter GetModelByCache(string SysFilter_id)
        {
            string cacheKey = "Model_SysFilterModel-" + SysFilter_id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SysFilter_id);
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
            return (Model_SysFilter) cache;
        }

        public List<Model_SysFilter> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SysFilter model)
        {
            return this.dal.Update(model);
        }
    }
}

