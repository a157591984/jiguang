namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StudentToParent
    {
        private readonly DAL_StudentToParent dal = new DAL_StudentToParent();

        public bool Add(Model_StudentToParent model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StudentToParent> DataTableToList(DataTable dt)
        {
            List<Model_StudentToParent> list = new List<Model_StudentToParent>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StudentToParent item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StudentToParent_ID)
        {
            return this.dal.Delete(StudentToParent_ID);
        }

        public bool DeleteList(string StudentToParent_IDlist)
        {
            return this.dal.DeleteList(StudentToParent_IDlist);
        }

        public bool Exists(string StudentToParent_ID)
        {
            return this.dal.Exists(StudentToParent_ID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public GH_PagerInfo<List<Model_StudentToParent>> GetAllList(string strWhere, string Sort, int pageIndex, int pageSize)
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

        public Model_StudentToParent GetModel(string StudentToParent_ID)
        {
            return this.dal.GetModel(StudentToParent_ID);
        }

        public Model_StudentToParent GetModelByCache(string StudentToParent_ID)
        {
            string cacheKey = "StudentToParentModel-" + StudentToParent_ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StudentToParent_ID);
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
            return (Model_StudentToParent) cache;
        }

        public List<Model_StudentToParent> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public DataSet GetParentToStuList_APP(string strWhere)
        {
            return this.dal.GetParentToStuList_APP(strWhere);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StudentToParent model)
        {
            return this.dal.Update(model);
        }
    }
}

