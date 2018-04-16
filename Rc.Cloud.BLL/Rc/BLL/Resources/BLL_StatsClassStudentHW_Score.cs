namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsClassStudentHW_Score
    {
        private readonly DAL_StatsClassStudentHW_Score dal = new DAL_StatsClassStudentHW_Score();

        public bool Add(Model_StatsClassStudentHW_Score model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsClassStudentHW_Score> DataTableToList(DataTable dt)
        {
            List<Model_StatsClassStudentHW_Score> list = new List<Model_StatsClassStudentHW_Score>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsClassStudentHW_Score item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsClassStudentHW_ScoreID)
        {
            return this.dal.Delete(StatsClassStudentHW_ScoreID);
        }

        public bool DeleteList(string StatsClassStudentHW_ScoreIDlist)
        {
            return this.dal.DeleteList(StatsClassStudentHW_ScoreIDlist);
        }

        public bool Exists(string StatsClassStudentHW_ScoreID)
        {
            return this.dal.Exists(StatsClassStudentHW_ScoreID);
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

        public Model_StatsClassStudentHW_Score GetModel(string StatsClassStudentHW_ScoreID)
        {
            return this.dal.GetModel(StatsClassStudentHW_ScoreID);
        }

        public Model_StatsClassStudentHW_Score GetModelByCache(string StatsClassStudentHW_ScoreID)
        {
            string cacheKey = "Model_StatsClassStudentHW_ScoreModel-" + StatsClassStudentHW_ScoreID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsClassStudentHW_ScoreID);
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
            return (Model_StatsClassStudentHW_Score) cache;
        }

        public List<Model_StatsClassStudentHW_Score> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsClassStudentHW_Score model)
        {
            return this.dal.Update(model);
        }
    }
}

