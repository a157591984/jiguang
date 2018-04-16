namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_HWScoreLevelDict
    {
        private readonly DAL_HWScoreLevelDict dal = new DAL_HWScoreLevelDict();

        public bool Add(Model_HWScoreLevelDict model)
        {
            return this.dal.Add(model);
        }

        public List<Model_HWScoreLevelDict> DataTableToList(DataTable dt)
        {
            List<Model_HWScoreLevelDict> list = new List<Model_HWScoreLevelDict>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_HWScoreLevelDict item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string HWScoreLevelDictID)
        {
            return this.dal.Delete(HWScoreLevelDictID);
        }

        public bool DeleteList(string HWScoreLevelDictIDlist)
        {
            return this.dal.DeleteList(HWScoreLevelDictIDlist);
        }

        public bool Exists(string HWScoreLevelDictID)
        {
            return this.dal.Exists(HWScoreLevelDictID);
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

        public Model_HWScoreLevelDict GetModel(string HWScoreLevelDictID)
        {
            return this.dal.GetModel(HWScoreLevelDictID);
        }

        public Model_HWScoreLevelDict GetModelByCache(string HWScoreLevelDictID)
        {
            string cacheKey = "Model_HWScoreLevelDictModel-" + HWScoreLevelDictID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(HWScoreLevelDictID);
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
            return (Model_HWScoreLevelDict) cache;
        }

        public List<Model_HWScoreLevelDict> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_HWScoreLevelDict model)
        {
            return this.dal.Update(model);
        }
    }
}

