namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsTeacherStudentHW_Score
    {
        private readonly DAL_StatsTeacherStudentHW_Score dal = new DAL_StatsTeacherStudentHW_Score();

        public bool Add(Model_StatsTeacherStudentHW_Score model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsTeacherStudentHW_Score> DataTableToList(DataTable dt)
        {
            List<Model_StatsTeacherStudentHW_Score> list = new List<Model_StatsTeacherStudentHW_Score>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsTeacherStudentHW_Score item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsTeacherStudentHW_ScoreID)
        {
            return this.dal.Delete(StatsTeacherStudentHW_ScoreID);
        }

        public bool DeleteList(string StatsTeacherStudentHW_ScoreIDlist)
        {
            return this.dal.DeleteList(StatsTeacherStudentHW_ScoreIDlist);
        }

        public bool Exists(string StatsTeacherStudentHW_ScoreID)
        {
            return this.dal.Exists(StatsTeacherStudentHW_ScoreID);
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

        public Model_StatsTeacherStudentHW_Score GetModel(string StatsTeacherStudentHW_ScoreID)
        {
            return this.dal.GetModel(StatsTeacherStudentHW_ScoreID);
        }

        public Model_StatsTeacherStudentHW_Score GetModelByCache(string StatsTeacherStudentHW_ScoreID)
        {
            string cacheKey = "Model_StatsTeacherStudentHW_ScoreModel-" + StatsTeacherStudentHW_ScoreID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsTeacherStudentHW_ScoreID);
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
            return (Model_StatsTeacherStudentHW_Score) cache;
        }

        public List<Model_StatsTeacherStudentHW_Score> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsTeacherStudentHW_Score model)
        {
            return this.dal.Update(model);
        }
    }
}

