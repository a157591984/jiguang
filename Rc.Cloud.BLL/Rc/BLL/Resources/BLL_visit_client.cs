namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_visit_client
    {
        private readonly DAL_visit_client dal = new DAL_visit_client();

        public bool Add(Model_visit_client model)
        {
            return this.dal.Add(model);
        }

        public List<Model_visit_client> DataTableToList(DataTable dt)
        {
            List<Model_visit_client> list = new List<Model_visit_client>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_visit_client item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public Model_visit_client GetModel(string visit_client_id)
        {
            return this.dal.GetModel(visit_client_id);
        }

        public Model_visit_client GetModelByCache(string visit_client_id)
        {
            string cacheKey = "Model_visit_clientModel-" + visit_client_id;
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
            return (Model_visit_client) cache;
        }

        public List<Model_visit_client> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public Model_visit_client GetModelNew(string user_id, string resource_data_id, string tab_id)
        {
            return this.dal.GetModelNew(user_id, resource_data_id, tab_id);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_visit_client model)
        {
            return this.dal.Update(model);
        }
    }
}

