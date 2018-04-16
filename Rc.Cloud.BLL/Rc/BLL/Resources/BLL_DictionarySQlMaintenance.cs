namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_DictionarySQlMaintenance
    {
        private readonly DAL_DictionarySQlMaintenance dal = new DAL_DictionarySQlMaintenance();

        public bool Add(Model_DictionarySQlMaintenance model)
        {
            return this.dal.Add(model);
        }

        public List<Model_DictionarySQlMaintenance> DataTableToList(DataTable dt)
        {
            List<Model_DictionarySQlMaintenance> list = new List<Model_DictionarySQlMaintenance>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_DictionarySQlMaintenance item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete()
        {
            return this.dal.Delete();
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

        public Model_DictionarySQlMaintenance GetModel()
        {
            return this.dal.GetModel();
        }

        public Model_DictionarySQlMaintenance GetModelByCache()
        {
            string cacheKey = "Model_DictionarySQlMaintenanceModel-";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel();
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
            return (Model_DictionarySQlMaintenance) cache;
        }

        public List<Model_DictionarySQlMaintenance> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_DictionarySQlMaintenance model)
        {
            return this.dal.Update(model);
        }
    }
}

