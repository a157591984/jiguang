namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Common_Dict
    {
        private readonly DAL_Common_Dict dal = new DAL_Common_Dict();

        public bool Add(Model_Common_Dict model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Common_Dict> DataTableToList(DataTable dt)
        {
            List<Model_Common_Dict> list = new List<Model_Common_Dict>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Common_Dict item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public DataSet GetListByDType(string D_Type)
        {
            return this.dal.GetListByDType(D_Type);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Model_Common_Dict GetModel()
        {
            return this.dal.GetModel();
        }

        public Model_Common_Dict GetModelByCache()
        {
            string cacheKey = "Model_Common_DictModel-";
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
            return (Model_Common_Dict) cache;
        }

        public List<Model_Common_Dict> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Common_Dict model)
        {
            return this.dal.Update(model);
        }
    }
}

