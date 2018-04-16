namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsClassHW_CorrectedInData
    {
        private readonly DAL_StatsClassHW_CorrectedInData dal = new DAL_StatsClassHW_CorrectedInData();

        public bool Add(Model_StatsClassHW_CorrectedInData model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsClassHW_CorrectedInData> DataTableToList(DataTable dt)
        {
            List<Model_StatsClassHW_CorrectedInData> list = new List<Model_StatsClassHW_CorrectedInData>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsClassHW_CorrectedInData item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsClassHW_CorrectedInDataID)
        {
            return this.dal.Delete(StatsClassHW_CorrectedInDataID);
        }

        public bool DeleteList(string StatsClassHW_CorrectedInDataIDlist)
        {
            return this.dal.DeleteList(StatsClassHW_CorrectedInDataIDlist);
        }

        public bool Exists(string StatsClassHW_CorrectedInDataID)
        {
            return this.dal.Exists(StatsClassHW_CorrectedInDataID);
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

        public Model_StatsClassHW_CorrectedInData GetModel(string StatsClassHW_CorrectedInDataID)
        {
            return this.dal.GetModel(StatsClassHW_CorrectedInDataID);
        }

        public Model_StatsClassHW_CorrectedInData GetModelByCache(string StatsClassHW_CorrectedInDataID)
        {
            string cacheKey = "Model_StatsClassHW_CorrectedInDataModel-" + StatsClassHW_CorrectedInDataID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsClassHW_CorrectedInDataID);
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
            return (Model_StatsClassHW_CorrectedInData) cache;
        }

        public List<Model_StatsClassHW_CorrectedInData> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsClassHW_CorrectedInData model)
        {
            return this.dal.Update(model);
        }
    }
}

