namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Two_WayChecklistAuth
    {
        private readonly DAL_Two_WayChecklistAuth dal = new DAL_Two_WayChecklistAuth();

        public bool Add(Model_Two_WayChecklistAuth model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Two_WayChecklistAuth> DataTableToList(DataTable dt)
        {
            List<Model_Two_WayChecklistAuth> list = new List<Model_Two_WayChecklistAuth>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Two_WayChecklistAuth item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Two_WayChecklistAuth_Id)
        {
            return this.dal.Delete(Two_WayChecklistAuth_Id);
        }

        public bool DeleteList(string Two_WayChecklistAuth_Idlist)
        {
            return this.dal.DeleteList(Two_WayChecklistAuth_Idlist);
        }

        public bool Exists(string Two_WayChecklistAuth_Id)
        {
            return this.dal.Exists(Two_WayChecklistAuth_Id);
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

        public Model_Two_WayChecklistAuth GetModel(string Two_WayChecklistAuth_Id)
        {
            return this.dal.GetModel(Two_WayChecklistAuth_Id);
        }

        public Model_Two_WayChecklistAuth GetModelByCache(string Two_WayChecklistAuth_Id)
        {
            string cacheKey = "Model_Two_WayChecklistAuthModel-" + Two_WayChecklistAuth_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Two_WayChecklistAuth_Id);
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
            return (Model_Two_WayChecklistAuth) cache;
        }

        public List<Model_Two_WayChecklistAuth> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Two_WayChecklistAuth model)
        {
            return this.dal.Update(model);
        }
    }
}

