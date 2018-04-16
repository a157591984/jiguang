namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_visit_web_s
    {
        private readonly DAL_visit_web_s dal = new DAL_visit_web_s();

        public bool Add(Model_visit_web_s model)
        {
            return this.dal.Add(model);
        }

        public List<Model_visit_web_s> DataTableToList(DataTable dt)
        {
            List<Model_visit_web_s> list = new List<Model_visit_web_s>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_visit_web_s item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string visit_web_id)
        {
            return this.dal.Delete(visit_web_id);
        }

        public bool DeleteList(string visit_web_idlist)
        {
            return this.dal.DeleteList(visit_web_idlist);
        }

        public bool Exists(string visit_web_id)
        {
            return this.dal.Exists(visit_web_id);
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

        public DataSet GetListByPageNew(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageNew(strWhere, orderby, startIndex, endIndex);
        }

        public Model_visit_web_s GetModel(string visit_web_id)
        {
            return this.dal.GetModel(visit_web_id);
        }

        public Model_visit_web_s GetModelByCache(string visit_web_id)
        {
            string cacheKey = "Model_visit_web_sModel-" + visit_web_id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(visit_web_id);
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
            return (Model_visit_web_s) cache;
        }

        public List<Model_visit_web_s> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCountNew(string strWhere)
        {
            return this.dal.GetRecordCountNew(strWhere);
        }

        public bool Update(Model_visit_web_s model)
        {
            return this.dal.Update(model);
        }
    }
}

