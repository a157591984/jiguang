namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_ClientTabToTab
    {
        private readonly DAL_ClientTabToTab dal = new DAL_ClientTabToTab();

        public bool Add(Model_ClientTabToTab model)
        {
            return this.dal.Add(model);
        }

        public List<Model_ClientTabToTab> DataTableToList(DataTable dt)
        {
            List<Model_ClientTabToTab> list = new List<Model_ClientTabToTab>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_ClientTabToTab item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Tabindex)
        {
            return this.dal.Delete(Tabindex);
        }

        public bool DeleteList(string Tabindexlist)
        {
            return this.dal.DeleteList(Tabindexlist);
        }

        public bool Exists(string Tabindex)
        {
            return this.dal.Exists(Tabindex);
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

        public Model_ClientTabToTab GetModel(string Tabindex)
        {
            return this.dal.GetModel(Tabindex);
        }

        public Model_ClientTabToTab GetModelByCache(string Tabindex)
        {
            string cacheKey = "Model_ClientTabToTabModel-" + Tabindex;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Tabindex);
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
            return (Model_ClientTabToTab) cache;
        }

        public List<Model_ClientTabToTab> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_ClientTabToTab model)
        {
            return this.dal.Update(model);
        }
    }
}

