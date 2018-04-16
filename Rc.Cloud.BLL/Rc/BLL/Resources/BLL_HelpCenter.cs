namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_HelpCenter
    {
        private readonly DAL_HelpCenter dal = new DAL_HelpCenter();

        public bool Add(Model_HelpCenter model)
        {
            return this.dal.Add(model);
        }

        public List<Model_HelpCenter> DataTableToList(DataTable dt)
        {
            List<Model_HelpCenter> list = new List<Model_HelpCenter>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_HelpCenter item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string help_id)
        {
            return this.dal.Delete(help_id);
        }

        public bool DeleteList(string help_idlist)
        {
            return this.dal.DeleteList(help_idlist);
        }

        public bool Exists(string help_id)
        {
            return this.dal.Exists(help_id);
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

        public Model_HelpCenter GetModel(string help_id)
        {
            return this.dal.GetModel(help_id);
        }

        public Model_HelpCenter GetModelByCache(string help_id)
        {
            string cacheKey = "Model_HelpCenterModel-" + help_id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(help_id);
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
            return (Model_HelpCenter) cache;
        }

        public List<Model_HelpCenter> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_HelpCenter model)
        {
            return this.dal.Update(model);
        }
    }
}

