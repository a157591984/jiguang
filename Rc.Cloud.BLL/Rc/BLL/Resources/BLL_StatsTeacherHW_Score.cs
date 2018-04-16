namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsTeacherHW_Score
    {
        private readonly DAL_StatsTeacherHW_Score dal = new DAL_StatsTeacherHW_Score();

        public bool Add(Model_StatsTeacherHW_Score model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsTeacherHW_Score> DataTableToList(DataTable dt)
        {
            List<Model_StatsTeacherHW_Score> list = new List<Model_StatsTeacherHW_Score>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsTeacherHW_Score item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsTeacherHW_ScoreID)
        {
            return this.dal.Delete(StatsTeacherHW_ScoreID);
        }

        public bool DeleteList(string StatsTeacherHW_ScoreIDlist)
        {
            return this.dal.DeleteList(StatsTeacherHW_ScoreIDlist);
        }

        public bool Exists(string StatsTeacherHW_ScoreID)
        {
            return this.dal.Exists(StatsTeacherHW_ScoreID);
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

        public Model_StatsTeacherHW_Score GetModel(string StatsTeacherHW_ScoreID)
        {
            return this.dal.GetModel(StatsTeacherHW_ScoreID);
        }

        public Model_StatsTeacherHW_Score GetModelByCache(string StatsTeacherHW_ScoreID)
        {
            string cacheKey = "Model_StatsTeacherHW_ScoreModel-" + StatsTeacherHW_ScoreID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsTeacherHW_ScoreID);
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
            return (Model_StatsTeacherHW_Score) cache;
        }

        public List<Model_StatsTeacherHW_Score> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsTeacherHW_Score model)
        {
            return this.dal.Update(model);
        }
    }
}

