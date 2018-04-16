namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsLog
    {
        private readonly DAL_StatsLog dal = new DAL_StatsLog();

        public bool Add(Model_StatsLog model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsLog> DataTableToList(DataTable dt)
        {
            List<Model_StatsLog> list = new List<Model_StatsLog>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsLog item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsLogId)
        {
            return this.dal.Delete(StatsLogId);
        }

        public bool DeleteList(string StatsLogIdlist)
        {
            return this.dal.DeleteList(StatsLogIdlist);
        }

        public bool ExecuteStatsAddLog(Model_StatsLog modelLog)
        {
            return this.dal.ExecuteStatsAddLog(modelLog);
        }

        public bool Exists(string StatsLogId)
        {
            return this.dal.Exists(StatsLogId);
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

        public Model_StatsLog GetModel(string StatsLogId)
        {
            return this.dal.GetModel(StatsLogId);
        }

        public Model_StatsLog GetModelByCache(string StatsLogId)
        {
            string cacheKey = "Model_StatsLogModel-" + StatsLogId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsLogId);
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
            return (Model_StatsLog) cache;
        }

        public List<Model_StatsLog> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool ReExecuteStatsAddLog(Model_StatsLog modelLog)
        {
            return this.dal.ReExecuteStatsAddLog(modelLog);
        }

        public bool Update(Model_StatsLog model)
        {
            return this.dal.Update(model);
        }
    }
}

