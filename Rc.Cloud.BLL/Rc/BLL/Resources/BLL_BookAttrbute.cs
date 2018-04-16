namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_BookAttrbute
    {
        private readonly DAL_BookAttrbute dal = new DAL_BookAttrbute();

        public bool Add(Model_BookAttrbute model)
        {
            return this.dal.Add(model);
        }

        public List<Model_BookAttrbute> DataTableToList(DataTable dt)
        {
            List<Model_BookAttrbute> list = new List<Model_BookAttrbute>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_BookAttrbute item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string BookAttrId)
        {
            return this.dal.Delete(BookAttrId);
        }

        public bool DeleteList(string BookAttrIdlist)
        {
            return this.dal.DeleteList(BookAttrIdlist);
        }

        public bool Exists(string BookAttrId)
        {
            return this.dal.Exists(BookAttrId);
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

        public Model_BookAttrbute GetModel(string BookAttrId)
        {
            return this.dal.GetModel(BookAttrId);
        }

        public Model_BookAttrbute GetModelByCache(string BookAttrId)
        {
            string cacheKey = "Model_BookAttrbuteModel-" + BookAttrId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(BookAttrId);
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
            return (Model_BookAttrbute) cache;
        }

        public List<Model_BookAttrbute> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Model_BookAttrbute> GetModelListByCache(string ResourceFolder_Id)
        {
            try
            {
                object objObject = null;
                if (ConfigHelper.GetConfigBool("CacheMode"))
                {
                    string cacheKey = "Model_BookAttrbuteListModel-" + ResourceFolder_Id;
                    objObject = DataCache.GetCache(cacheKey);
                    if (objObject == null)
                    {
                        DataSet list = this.dal.GetList("ResourceFolder_Id='" + ResourceFolder_Id + "'");
                        objObject = this.DataTableToList(list.Tables[0]);
                        if (objObject != null)
                        {
                            int configInt = ConfigHelper.GetConfigInt("CatchTimeLong");
                            DataCache.SetCache(cacheKey, objObject, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                        }
                    }
                }
                else
                {
                    DataSet set2 = this.dal.GetList("ResourceFolder_Id='" + ResourceFolder_Id + "'");
                    objObject = this.DataTableToList(set2.Tables[0]);
                }
                return (List<Model_BookAttrbute>) objObject;
            }
            catch
            {
                return null;
            }
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_BookAttrbute model)
        {
            return this.dal.Update(model);
        }
    }
}

