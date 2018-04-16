namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Bookshelves
    {
        private readonly DAL_Bookshelves dal = new DAL_Bookshelves();

        public bool Add(Model_Bookshelves model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Bookshelves> DataTableToList(DataTable dt)
        {
            List<Model_Bookshelves> list = new List<Model_Bookshelves>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Bookshelves item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ResourceFolder_Id)
        {
            return this.dal.Delete(ResourceFolder_Id);
        }

        public bool DeleteList(string ResourceFolder_Idlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(ResourceFolder_Idlist, 0));
        }

        public bool Exists(string ResourceFolder_Id)
        {
            return this.dal.Exists(ResourceFolder_Id);
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

        public Model_Bookshelves GetModel(string ResourceFolder_Id)
        {
            return this.dal.GetModel(ResourceFolder_Id);
        }

        public Model_Bookshelves GetModelByCache(string ResourceFolder_Id)
        {
            string cacheKey = "Model_BookshelvesModel-" + ResourceFolder_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ResourceFolder_Id);
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
            return (Model_Bookshelves) cache;
        }

        public List<Model_Bookshelves> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Bookshelves model)
        {
            return this.dal.Update(model);
        }
    }
}

