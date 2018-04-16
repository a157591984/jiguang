namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_BookAttr_School
    {
        private readonly DAL_BookAttr_School dal = new DAL_BookAttr_School();

        public bool Add(Model_BookAttr_School model)
        {
            return this.dal.Add(model);
        }

        public List<Model_BookAttr_School> DataTableToList(DataTable dt)
        {
            List<Model_BookAttr_School> list = new List<Model_BookAttr_School>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_BookAttr_School item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string BookAttr_School_Id)
        {
            return this.dal.Delete(BookAttr_School_Id);
        }

        public bool DeleteList(string BookAttr_School_Idlist)
        {
            return this.dal.DeleteList(BookAttr_School_Idlist);
        }

        public bool Exists(string BookAttr_School_Id)
        {
            return this.dal.Exists(BookAttr_School_Id);
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

        public Model_BookAttr_School GetModel(string BookAttr_School_Id)
        {
            return this.dal.GetModel(BookAttr_School_Id);
        }

        public Model_BookAttr_School GetModelByCache(string BookAttr_School_Id)
        {
            string cacheKey = "Model_BookAttr_SchoolModel-" + BookAttr_School_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(BookAttr_School_Id);
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
            return (Model_BookAttr_School) cache;
        }

        public List<Model_BookAttr_School> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Model_BookAttr_School> GetModelListByCache(string UserId)
        {
            try
            {
                object objObject = null;
                if (ConfigHelper.GetConfigBool("CacheMode"))
                {
                    string cacheKey = "Model_BookAttr_SchoolListModel-" + UserId;
                    objObject = DataCache.GetCache(cacheKey);
                    if (objObject == null)
                    {
                        DataSet listByUserId = this.dal.GetListByUserId(UserId);
                        objObject = this.DataTableToList(listByUserId.Tables[0]);
                        if (objObject != null)
                        {
                            int configInt = ConfigHelper.GetConfigInt("CatchTimeLong");
                            DataCache.SetCache(cacheKey, objObject, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                        }
                    }
                }
                else
                {
                    DataSet set2 = this.dal.GetListByUserId(UserId);
                    objObject = this.DataTableToList(set2.Tables[0]);
                }
                return (List<Model_BookAttr_School>) objObject;
            }
            catch
            {
                return null;
            }
        }

        public List<Model_BookAttr_School> GetModelListByUserId(string UserId)
        {
            DataSet listByUserId = this.dal.GetListByUserId(UserId);
            return this.DataTableToList(listByUserId.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_BookAttr_School model)
        {
            return this.dal.Update(model);
        }
    }
}

