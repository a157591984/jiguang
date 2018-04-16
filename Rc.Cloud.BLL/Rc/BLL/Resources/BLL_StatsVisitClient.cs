namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsVisitClient
    {
        private readonly DAL_StatsVisitClient dal = new DAL_StatsVisitClient();

        public bool Add(Model_StatsVisitClient model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsVisitClient> DataTableToList(DataTable dt)
        {
            List<Model_StatsVisitClient> list = new List<Model_StatsVisitClient>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsVisitClient item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsVisitClient_Id)
        {
            return this.dal.Delete(StatsVisitClient_Id);
        }

        public bool DeleteList(string StatsVisitClient_Idlist)
        {
            return this.dal.DeleteList(StatsVisitClient_Idlist);
        }

        public bool Exists(string StatsVisitClient_Id)
        {
            return this.dal.Exists(StatsVisitClient_Id);
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

        public Model_StatsVisitClient GetModel(string StatsVisitClient_Id)
        {
            return this.dal.GetModel(StatsVisitClient_Id);
        }

        public Model_StatsVisitClient GetModelByCache(string StatsVisitClient_Id)
        {
            string cacheKey = "Model_StatsVisitClientModel-" + StatsVisitClient_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsVisitClient_Id);
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
            return (Model_StatsVisitClient) cache;
        }

        public List<Model_StatsVisitClient> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsVisitClient model)
        {
            return this.dal.Update(model);
        }
    }
}

