namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatRes_Doc_Attr
    {
        private readonly DAL_StatRes_Doc_Attr dal = new DAL_StatRes_Doc_Attr();

        public bool Add(Model_StatRes_Doc_Attr model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatRes_Doc_Attr> DataTableToList(DataTable dt)
        {
            List<Model_StatRes_Doc_Attr> list = new List<Model_StatRes_Doc_Attr>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatRes_Doc_Attr item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ID)
        {
            return this.dal.Delete(ID);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public bool Exists(string ID)
        {
            return this.dal.Exists(ID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public GH_PagerInfo<List<Model_StatRes_Doc_Attr>> GetAllList(string strWhere, string Sort, int pageIndex, int pageSize)
        {
            return this.dal.GetAllList(strWhere, Sort, pageIndex, pageSize);
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

        public DataSet GetListByPageForSearch(string strWhere, string orderBy, int pageIndex, int pageSize)
        {
            return this.dal.GetListByPageForSearch(strWhere, orderBy, pageIndex, pageSize);
        }

        public Model_StatRes_Doc_Attr GetModel(string ID)
        {
            return this.dal.GetModel(ID);
        }

        public Model_StatRes_Doc_Attr GetModelByCache(string ID)
        {
            string cacheKey = "StatRes_Doc_AttrModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
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
            return (Model_StatRes_Doc_Attr) cache;
        }

        public List<Model_StatRes_Doc_Attr> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCountForSearch(string strWhere)
        {
            return this.dal.GetRecordCountForSearch(strWhere);
        }

        public bool Update(Model_StatRes_Doc_Attr model)
        {
            return this.dal.Update(model);
        }
    }
}

