namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsSchoolActivity
    {
        private readonly DAL_StatsSchoolActivity dal = new DAL_StatsSchoolActivity();

        public bool Add(Model_StatsSchoolActivity model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsSchoolActivity> DataTableToList(DataTable dt)
        {
            List<Model_StatsSchoolActivity> list = new List<Model_StatsSchoolActivity>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsSchoolActivity item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsSchoolActivity_Id)
        {
            return this.dal.Delete(StatsSchoolActivity_Id);
        }

        public bool DeleteList(string StatsSchoolActivity_Idlist)
        {
            return this.dal.DeleteList(StatsSchoolActivity_Idlist);
        }

        public bool Exists(string StatsSchoolActivity_Id)
        {
            return this.dal.Exists(StatsSchoolActivity_Id);
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

        public Model_StatsSchoolActivity GetModel(string StatsSchoolActivity_Id)
        {
            return this.dal.GetModel(StatsSchoolActivity_Id);
        }

        public Model_StatsSchoolActivity GetModelByCache(string StatsSchoolActivity_Id)
        {
            string cacheKey = "Model_StatsSchoolActivityModel-" + StatsSchoolActivity_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsSchoolActivity_Id);
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
            return (Model_StatsSchoolActivity) cache;
        }

        public List<Model_StatsSchoolActivity> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsSchoolActivity model)
        {
            return this.dal.Update(model);
        }
    }
}

