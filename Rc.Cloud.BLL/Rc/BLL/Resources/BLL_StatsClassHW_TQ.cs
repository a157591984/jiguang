namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsClassHW_TQ
    {
        private readonly DAL_StatsClassHW_TQ dal = new DAL_StatsClassHW_TQ();

        public bool Add(Model_StatsClassHW_TQ model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsClassHW_TQ> DataTableToList(DataTable dt)
        {
            List<Model_StatsClassHW_TQ> list = new List<Model_StatsClassHW_TQ>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsClassHW_TQ item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsClassHW_TQID)
        {
            return this.dal.Delete(StatsClassHW_TQID);
        }

        public bool DeleteList(string StatsClassHW_TQIDlist)
        {
            return this.dal.DeleteList(StatsClassHW_TQIDlist);
        }

        public bool Exists(string StatsClassHW_TQID)
        {
            return this.dal.Exists(StatsClassHW_TQID);
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

        public Model_StatsClassHW_TQ GetModel(string StatsClassHW_TQID)
        {
            return this.dal.GetModel(StatsClassHW_TQID);
        }

        public Model_StatsClassHW_TQ GetModelByCache(string StatsClassHW_TQID)
        {
            string cacheKey = "Model_StatsClassHW_TQModel-" + StatsClassHW_TQID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsClassHW_TQID);
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
            return (Model_StatsClassHW_TQ) cache;
        }

        public List<Model_StatsClassHW_TQ> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsClassHW_TQ model)
        {
            return this.dal.Update(model);
        }
    }
}

