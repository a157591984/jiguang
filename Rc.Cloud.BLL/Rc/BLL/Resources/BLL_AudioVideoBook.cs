namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_AudioVideoBook
    {
        private readonly DAL_AudioVideoBook dal = new DAL_AudioVideoBook();

        public bool Add(Model_AudioVideoBook model)
        {
            return this.dal.Add(model);
        }

        public List<Model_AudioVideoBook> DataTableToList(DataTable dt)
        {
            List<Model_AudioVideoBook> list = new List<Model_AudioVideoBook>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_AudioVideoBook item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string AudioVideoBookId)
        {
            return this.dal.Delete(AudioVideoBookId);
        }

        public bool DeleteList(string AudioVideoBookIdlist)
        {
            return this.dal.DeleteList(AudioVideoBookIdlist);
        }

        public bool Exists(string AudioVideoBookId)
        {
            return this.dal.Exists(AudioVideoBookId);
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

        public Model_AudioVideoBook GetModel(string AudioVideoBookId)
        {
            return this.dal.GetModel(AudioVideoBookId);
        }

        public Model_AudioVideoBook GetModelByCache(string AudioVideoBookId)
        {
            string cacheKey = "Model_AudioVideoBookModel-" + AudioVideoBookId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(AudioVideoBookId);
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
            return (Model_AudioVideoBook) cache;
        }

        public List<Model_AudioVideoBook> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_AudioVideoBook model)
        {
            return this.dal.Update(model);
        }
    }
}

