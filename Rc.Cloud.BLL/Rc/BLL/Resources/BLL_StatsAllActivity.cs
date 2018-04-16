namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsAllActivity
    {
        private readonly DAL_StatsAllActivity dal = new DAL_StatsAllActivity();

        public bool Add(Model_StatsAllActivity model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsAllActivity> DataTableToList(DataTable dt)
        {
            List<Model_StatsAllActivity> list = new List<Model_StatsAllActivity>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsAllActivity item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsAllActivity_Id)
        {
            return this.dal.Delete(StatsAllActivity_Id);
        }

        public bool DeleteList(string StatsAllActivity_Idlist)
        {
            return this.dal.DeleteList(StatsAllActivity_Idlist);
        }

        public bool Exists(string StatsAllActivity_Id)
        {
            return this.dal.Exists(StatsAllActivity_Id);
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

        public Model_StatsAllActivity GetModel(string StatsAllActivity_Id)
        {
            return this.dal.GetModel(StatsAllActivity_Id);
        }

        public Model_StatsAllActivity GetModelByCache(string StatsAllActivity_Id)
        {
            string cacheKey = "Model_StatsAllActivityModel-" + StatsAllActivity_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsAllActivity_Id);
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
            return (Model_StatsAllActivity) cache;
        }

        public List<Model_StatsAllActivity> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsAllActivity model)
        {
            return this.dal.Update(model);
        }
    }
}

