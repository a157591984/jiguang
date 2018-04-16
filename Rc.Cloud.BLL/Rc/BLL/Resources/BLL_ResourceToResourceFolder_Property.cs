namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_ResourceToResourceFolder_Property
    {
        private readonly DAL_ResourceToResourceFolder_Property dal = new DAL_ResourceToResourceFolder_Property();

        public bool Add(Model_ResourceToResourceFolder_Property model)
        {
            return this.dal.Add(model);
        }

        public List<Model_ResourceToResourceFolder_Property> DataTableToList(DataTable dt)
        {
            List<Model_ResourceToResourceFolder_Property> list = new List<Model_ResourceToResourceFolder_Property>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_ResourceToResourceFolder_Property item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ResourceToResourceFolder_Id)
        {
            return this.dal.Delete(ResourceToResourceFolder_Id);
        }

        public bool DeleteList(string ResourceToResourceFolder_Idlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(ResourceToResourceFolder_Idlist, 0));
        }

        public bool Exists(string ResourceToResourceFolder_Id)
        {
            return this.dal.Exists(ResourceToResourceFolder_Id);
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

        public Model_ResourceToResourceFolder_Property GetModel(string ResourceToResourceFolder_Id)
        {
            return this.dal.GetModel(ResourceToResourceFolder_Id);
        }

        public Model_ResourceToResourceFolder_Property GetModelByCache(string ResourceToResourceFolder_Id)
        {
            string cacheKey = "Model_ResourceToResourceFolder_PropertyModel-" + ResourceToResourceFolder_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ResourceToResourceFolder_Id);
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
            return (Model_ResourceToResourceFolder_Property) cache;
        }

        public List<Model_ResourceToResourceFolder_Property> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public object GetpaperHeaderDoc(string ResourceToResourceFolder_Id)
        {
            return this.dal.GetpaperHeaderDoc(ResourceToResourceFolder_Id);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_ResourceToResourceFolder_Property model)
        {
            return this.dal.Update(model);
        }
    }
}

