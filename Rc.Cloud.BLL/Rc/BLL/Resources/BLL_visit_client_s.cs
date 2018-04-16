namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_visit_client_s
    {
        private readonly DAL_visit_client_s dal = new DAL_visit_client_s();

        public bool Add(Model_visit_client_s model)
        {
            return this.dal.Add(model);
        }

        public List<Model_visit_client_s> DataTableToList(DataTable dt)
        {
            List<Model_visit_client_s> list = new List<Model_visit_client_s>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_visit_client_s item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string visit_client_id)
        {
            return this.dal.Delete(visit_client_id);
        }

        public bool DeleteList(string visit_client_idlist)
        {
            return this.dal.DeleteList(visit_client_idlist);
        }

        public bool Exists(string visit_client_id)
        {
            return this.dal.Exists(visit_client_id);
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

        public DataSet GetListByPageDetail(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageDetail(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetListByPageNew(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageNew(strWhere, orderby, startIndex, endIndex);
        }

        public Model_visit_client_s GetModel(string visit_client_id)
        {
            return this.dal.GetModel(visit_client_id);
        }

        public Model_visit_client_s GetModelByCache(string visit_client_id)
        {
            string cacheKey = "Model_visit_client_sModel-" + visit_client_id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(visit_client_id);
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
            return (Model_visit_client_s) cache;
        }

        public List<Model_visit_client_s> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCountDetail(string strWhere)
        {
            return this.dal.GetRecordCountDetail(strWhere);
        }

        public int GetRecordCountNew(string strWhere)
        {
            return this.dal.GetRecordCountNew(strWhere);
        }

        public bool Update(Model_visit_client_s model)
        {
            return this.dal.Update(model);
        }
    }
}

