namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_BookProductionLog
    {
        private readonly DAL_BookProductionLog dal = new DAL_BookProductionLog();

        public bool Add(Model_BookProductionLog model)
        {
            return this.dal.Add(model);
        }

        public List<Model_BookProductionLog> DataTableToList(DataTable dt)
        {
            List<Model_BookProductionLog> list = new List<Model_BookProductionLog>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_BookProductionLog item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string BookProductionLog_Id)
        {
            return this.dal.Delete(BookProductionLog_Id);
        }

        public bool DeleteList(string BookProductionLog_Idlist)
        {
            return this.dal.DeleteList(BookProductionLog_Idlist);
        }

        public bool Exists(string BookProductionLog_Id)
        {
            return this.dal.Exists(BookProductionLog_Id);
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

        public Model_BookProductionLog GetModel(string BookProductionLog_Id)
        {
            return this.dal.GetModel(BookProductionLog_Id);
        }

        public Model_BookProductionLog GetModelByCache(string BookProductionLog_Id)
        {
            string cacheKey = "Model_BookProductionLogModel-" + BookProductionLog_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(BookProductionLog_Id);
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
            return (Model_BookProductionLog) cache;
        }

        public List<Model_BookProductionLog> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_BookProductionLog model)
        {
            return this.dal.Update(model);
        }
    }
}

