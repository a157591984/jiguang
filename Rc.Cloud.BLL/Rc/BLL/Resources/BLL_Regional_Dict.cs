namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Regional_Dict
    {
        private readonly DAL_Regional_Dict dal = new DAL_Regional_Dict();

        public bool Add(Model_Regional_Dict model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Regional_Dict> DataTableToList(DataTable dt)
        {
            List<Model_Regional_Dict> list = new List<Model_Regional_Dict>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Regional_Dict item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Regional_Dict_ID)
        {
            return this.dal.Delete(Regional_Dict_ID);
        }

        public bool DeleteList(string Regional_Dict_IDlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(Regional_Dict_IDlist, 0));
        }

        public bool Exists(string Regional_Dict_ID)
        {
            return this.dal.Exists(Regional_Dict_ID);
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

        public Model_Regional_Dict GetModel(string Regional_Dict_ID)
        {
            return this.dal.GetModel(Regional_Dict_ID);
        }

        public Model_Regional_Dict GetModelByCache(string Regional_Dict_ID)
        {
            string cacheKey = "Model_Regional_DictModel-" + Regional_Dict_ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Regional_Dict_ID);
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
            return (Model_Regional_Dict) cache;
        }

        public List<Model_Regional_Dict> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Regional_Dict model)
        {
            return this.dal.Update(model);
        }
    }
}

