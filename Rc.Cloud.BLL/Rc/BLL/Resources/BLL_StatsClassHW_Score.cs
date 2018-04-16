namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsClassHW_Score
    {
        private readonly DAL_StatsClassHW_Score dal = new DAL_StatsClassHW_Score();

        public bool Add(Model_StatsClassHW_Score model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsClassHW_Score> DataTableToList(DataTable dt)
        {
            List<Model_StatsClassHW_Score> list = new List<Model_StatsClassHW_Score>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsClassHW_Score item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsClassHW_ScoreID)
        {
            return this.dal.Delete(StatsClassHW_ScoreID);
        }

        public bool DeleteList(string StatsClassHW_ScoreIDlist)
        {
            return this.dal.DeleteList(StatsClassHW_ScoreIDlist);
        }

        public bool Exists(string StatsClassHW_ScoreID)
        {
            return this.dal.Exists(StatsClassHW_ScoreID);
        }

        public DataSet GetAllClass(string ResourceToResourceFolder_Id, string UserId)
        {
            return this.dal.GetAllClass(ResourceToResourceFolder_Id, UserId);
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

        public Model_StatsClassHW_Score GetModel(string StatsClassHW_ScoreID)
        {
            return this.dal.GetModel(StatsClassHW_ScoreID);
        }

        public Model_StatsClassHW_Score GetModelByCache(string StatsClassHW_ScoreID)
        {
            string cacheKey = "Model_StatsClassHW_ScoreModel-" + StatsClassHW_ScoreID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsClassHW_ScoreID);
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
            return (Model_StatsClassHW_Score) cache;
        }

        public List<Model_StatsClassHW_Score> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsClassHW_Score model)
        {
            return this.dal.Update(model);
        }
    }
}

