namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_UserBuyResources
    {
        private readonly DAL_UserBuyResources dal = new DAL_UserBuyResources();

        public bool Add(Model_UserBuyResources model)
        {
            return this.dal.Add(model);
        }

        public bool AddUserBuyResources(List<Model_UserBuyResources> list)
        {
            return this.dal.AddUserBuyResources(list);
        }

        public List<Model_UserBuyResources> DataTableToList(DataTable dt)
        {
            List<Model_UserBuyResources> list = new List<Model_UserBuyResources>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_UserBuyResources item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string UserBuyResources_ID)
        {
            return this.dal.Delete(UserBuyResources_ID);
        }

        public bool DeleteList(string UserBuyResources_IDlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(UserBuyResources_IDlist, 0));
        }

        public bool Exists(string UserBuyResources_ID)
        {
            return this.dal.Exists(UserBuyResources_ID);
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

        public Model_UserBuyResources GetModel(string UserBuyResources_ID)
        {
            return this.dal.GetModel(UserBuyResources_ID);
        }

        public Model_UserBuyResources GetModelByCache(string UserBuyResources_ID)
        {
            string cacheKey = "Model_UserBuyResourcesModel-" + UserBuyResources_ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(UserBuyResources_ID);
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
            return (Model_UserBuyResources) cache;
        }

        public List<Model_UserBuyResources> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_UserBuyResources model)
        {
            return this.dal.Update(model);
        }
    }
}

