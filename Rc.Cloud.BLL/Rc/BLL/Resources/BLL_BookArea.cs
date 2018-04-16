namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_BookArea
    {
        private readonly DAL_BookArea dal = new DAL_BookArea();

        public bool Add(Model_BookArea model)
        {
            return this.dal.Add(model);
        }

        public int AddModelList(string ResourceFolder_Id, List<Model_BookArea> listM)
        {
            return this.dal.AddModelList(ResourceFolder_Id, listM);
        }

        public List<Model_BookArea> DataTableToList(DataTable dt)
        {
            List<Model_BookArea> list = new List<Model_BookArea>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_BookArea item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string BookArea_ID)
        {
            return this.dal.Delete(BookArea_ID);
        }

        public bool DeleteList(string BookArea_IDlist)
        {
            return this.dal.DeleteList(BookArea_IDlist);
        }

        public bool Exists(string BookArea_ID)
        {
            return this.dal.Exists(BookArea_ID);
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

        public Model_BookArea GetModel(string BookArea_ID)
        {
            return this.dal.GetModel(BookArea_ID);
        }

        public Model_BookArea GetModelByCache(string BookArea_ID)
        {
            string cacheKey = "BookAreaModel-" + BookArea_ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(BookArea_ID);
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
            return (Model_BookArea) cache;
        }

        public List<Model_BookArea> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_BookArea model)
        {
            return this.dal.Update(model);
        }
    }
}

