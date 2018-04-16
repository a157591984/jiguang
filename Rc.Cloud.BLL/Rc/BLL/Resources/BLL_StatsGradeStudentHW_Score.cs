namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsGradeStudentHW_Score
    {
        private readonly DAL_StatsGradeStudentHW_Score dal = new DAL_StatsGradeStudentHW_Score();

        public bool Add(Model_StatsGradeStudentHW_Score model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsGradeStudentHW_Score> DataTableToList(DataTable dt)
        {
            List<Model_StatsGradeStudentHW_Score> list = new List<Model_StatsGradeStudentHW_Score>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsGradeStudentHW_Score item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsGradeStudentHW_ScoreID)
        {
            return this.dal.Delete(StatsGradeStudentHW_ScoreID);
        }

        public bool DeleteList(string StatsGradeStudentHW_ScoreIDlist)
        {
            return this.dal.DeleteList(StatsGradeStudentHW_ScoreIDlist);
        }

        public bool Exists(string StatsGradeStudentHW_ScoreID)
        {
            return this.dal.Exists(StatsGradeStudentHW_ScoreID);
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

        public Model_StatsGradeStudentHW_Score GetModel(string StatsGradeStudentHW_ScoreID)
        {
            return this.dal.GetModel(StatsGradeStudentHW_ScoreID);
        }

        public Model_StatsGradeStudentHW_Score GetModelByCache(string StatsGradeStudentHW_ScoreID)
        {
            string cacheKey = "Model_StatsGradeStudentHW_ScoreModel-" + StatsGradeStudentHW_ScoreID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsGradeStudentHW_ScoreID);
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
            return (Model_StatsGradeStudentHW_Score) cache;
        }

        public List<Model_StatsGradeStudentHW_Score> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsGradeStudentHW_Score model)
        {
            return this.dal.Update(model);
        }
    }
}

