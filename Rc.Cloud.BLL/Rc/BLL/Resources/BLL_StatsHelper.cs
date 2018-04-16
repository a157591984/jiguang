namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsHelper
    {
        private readonly DAL_StatsHelper dal = new DAL_StatsHelper();

        public bool Add(Model_StatsHelper model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsHelper> DataTableToList(DataTable dt)
        {
            List<Model_StatsHelper> list = new List<Model_StatsHelper>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsHelper item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsHelper_Id)
        {
            return this.dal.Delete(StatsHelper_Id);
        }

        public bool DeleteList(string StatsHelper_Idlist)
        {
            return this.dal.DeleteList(StatsHelper_Idlist);
        }

        public bool Exists(string StatsHelper_Id)
        {
            return this.dal.Exists(StatsHelper_Id);
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

        public Model_StatsHelper GetModel(string StatsHelper_Id)
        {
            return this.dal.GetModel(StatsHelper_Id);
        }

        public Model_StatsHelper GetModelByCache(string StatsHelper_Id)
        {
            string cacheKey = "Model_StatsHelperModel-" + StatsHelper_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsHelper_Id);
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
            return (Model_StatsHelper) cache;
        }

        public List<Model_StatsHelper> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsHelper model)
        {
            return this.dal.Update(model);
        }
    }
}

