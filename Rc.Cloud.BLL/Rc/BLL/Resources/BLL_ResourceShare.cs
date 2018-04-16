namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_ResourceShare
    {
        private readonly DAL_ResourceShare dal = new DAL_ResourceShare();

        public bool Add(Model_ResourceShare model)
        {
            return this.dal.Add(model);
        }

        public bool CancelShareResource(string ResourceToResourceFolder_Id, Model_ResourceToResourceFolder rtrmodel)
        {
            return this.dal.CancelShareResource(ResourceToResourceFolder_Id, rtrmodel);
        }

        public List<Model_ResourceShare> DataTableToList(DataTable dt)
        {
            List<Model_ResourceShare> list = new List<Model_ResourceShare>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_ResourceShare item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ResourceShareId)
        {
            return this.dal.Delete(ResourceShareId);
        }

        public bool DeleteList(string ResourceShareIdlist)
        {
            return this.dal.DeleteList(ResourceShareIdlist);
        }

        public bool Exists(string ResourceShareId)
        {
            return this.dal.Exists(ResourceShareId);
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

        public Model_ResourceShare GetModel(string ResourceShareId)
        {
            return this.dal.GetModel(ResourceShareId);
        }

        public Model_ResourceShare GetModelByCache(string ResourceShareId)
        {
            string cacheKey = "Model_ResourceShareModel-" + ResourceShareId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ResourceShareId);
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
            return (Model_ResourceShare) cache;
        }

        public List<Model_ResourceShare> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool ShareResource(Model_ResourceShare model, Model_ResourceToResourceFolder rtrmodel)
        {
            return this.dal.ShareResource(model, rtrmodel);
        }

        public bool Update(Model_ResourceShare model)
        {
            return this.dal.Update(model);
        }
    }
}

