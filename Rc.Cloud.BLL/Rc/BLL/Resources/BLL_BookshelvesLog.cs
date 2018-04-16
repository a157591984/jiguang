namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_BookshelvesLog
    {
        private readonly DAL_BookshelvesLog dal = new DAL_BookshelvesLog();

        public bool Add(Model_BookshelvesLog model)
        {
            return this.dal.Add(model);
        }

        public List<Model_BookshelvesLog> DataTableToList(DataTable dt)
        {
            List<Model_BookshelvesLog> list = new List<Model_BookshelvesLog>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_BookshelvesLog item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string BookshelvesLog_Id)
        {
            return this.dal.Delete(BookshelvesLog_Id);
        }

        public bool DeleteList(string BookshelvesLog_Idlist)
        {
            return this.dal.DeleteList(BookshelvesLog_Idlist);
        }

        public bool Exists(string BookshelvesLog_Id)
        {
            return this.dal.Exists(BookshelvesLog_Id);
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

        public Model_BookshelvesLog GetModel(string BookshelvesLog_Id)
        {
            return this.dal.GetModel(BookshelvesLog_Id);
        }

        public Model_BookshelvesLog GetModelByCache(string BookshelvesLog_Id)
        {
            string cacheKey = "Model_BookshelvesLogModel-" + BookshelvesLog_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(BookshelvesLog_Id);
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
            return (Model_BookshelvesLog) cache;
        }

        public List<Model_BookshelvesLog> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_BookshelvesLog model)
        {
            return this.dal.Update(model);
        }
    }
}

