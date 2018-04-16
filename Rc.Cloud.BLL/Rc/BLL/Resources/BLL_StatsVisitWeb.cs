namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsVisitWeb
    {
        private readonly DAL_StatsVisitWeb dal = new DAL_StatsVisitWeb();

        public bool Add(Model_StatsVisitWeb model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsVisitWeb> DataTableToList(DataTable dt)
        {
            List<Model_StatsVisitWeb> list = new List<Model_StatsVisitWeb>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsVisitWeb item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsVisitWeb_Id)
        {
            return this.dal.Delete(StatsVisitWeb_Id);
        }

        public bool DeleteList(string StatsVisitWeb_Idlist)
        {
            return this.dal.DeleteList(StatsVisitWeb_Idlist);
        }

        public bool Exists(string StatsVisitWeb_Id)
        {
            return this.dal.Exists(StatsVisitWeb_Id);
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

        public Model_StatsVisitWeb GetModel(string StatsVisitWeb_Id)
        {
            return this.dal.GetModel(StatsVisitWeb_Id);
        }

        public Model_StatsVisitWeb GetModelByCache(string StatsVisitWeb_Id)
        {
            string cacheKey = "Model_StatsVisitWebModel-" + StatsVisitWeb_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsVisitWeb_Id);
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
            return (Model_StatsVisitWeb) cache;
        }

        public List<Model_StatsVisitWeb> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsVisitWeb model)
        {
            return this.dal.Update(model);
        }
    }
}

