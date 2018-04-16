namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsClassHW_ScoreLevel
    {
        private readonly DAL_StatsClassHW_ScoreLevel dal = new DAL_StatsClassHW_ScoreLevel();

        public bool Add(Model_StatsClassHW_ScoreLevel model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsClassHW_ScoreLevel> DataTableToList(DataTable dt)
        {
            List<Model_StatsClassHW_ScoreLevel> list = new List<Model_StatsClassHW_ScoreLevel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsClassHW_ScoreLevel item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsClassHW_ScoreLevelID)
        {
            return this.dal.Delete(StatsClassHW_ScoreLevelID);
        }

        public bool DeleteList(string StatsClassHW_ScoreLevelIDlist)
        {
            return this.dal.DeleteList(StatsClassHW_ScoreLevelIDlist);
        }

        public bool Exists(string StatsClassHW_ScoreLevelID)
        {
            return this.dal.Exists(StatsClassHW_ScoreLevelID);
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

        public Model_StatsClassHW_ScoreLevel GetModel(string StatsClassHW_ScoreLevelID)
        {
            return this.dal.GetModel(StatsClassHW_ScoreLevelID);
        }

        public Model_StatsClassHW_ScoreLevel GetModelByCache(string StatsClassHW_ScoreLevelID)
        {
            string cacheKey = "Model_StatsClassHW_ScoreLevelModel-" + StatsClassHW_ScoreLevelID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsClassHW_ScoreLevelID);
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
            return (Model_StatsClassHW_ScoreLevel) cache;
        }

        public List<Model_StatsClassHW_ScoreLevel> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsClassHW_ScoreLevel model)
        {
            return this.dal.Update(model);
        }
    }
}

