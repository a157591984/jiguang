namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsGradeHW_Score
    {
        private readonly DAL_StatsGradeHW_Score dal = new DAL_StatsGradeHW_Score();

        public bool Add(Model_StatsGradeHW_Score model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsGradeHW_Score> DataTableToList(DataTable dt)
        {
            List<Model_StatsGradeHW_Score> list = new List<Model_StatsGradeHW_Score>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsGradeHW_Score item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsGradeHW_ScoreID)
        {
            return this.dal.Delete(StatsGradeHW_ScoreID);
        }

        public bool DeleteList(string StatsGradeHW_ScoreIDlist)
        {
            return this.dal.DeleteList(StatsGradeHW_ScoreIDlist);
        }

        public bool Exists(string StatsGradeHW_ScoreID)
        {
            return this.dal.Exists(StatsGradeHW_ScoreID);
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

        public Model_StatsGradeHW_Score GetModel(string StatsGradeHW_ScoreID)
        {
            return this.dal.GetModel(StatsGradeHW_ScoreID);
        }

        public Model_StatsGradeHW_Score GetModelByCache(string StatsGradeHW_ScoreID)
        {
            string cacheKey = "Model_StatsGradeHW_ScoreModel-" + StatsGradeHW_ScoreID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsGradeHW_ScoreID);
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
            return (Model_StatsGradeHW_Score) cache;
        }

        public List<Model_StatsGradeHW_Score> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsGradeHW_Score model)
        {
            return this.dal.Update(model);
        }
    }
}

