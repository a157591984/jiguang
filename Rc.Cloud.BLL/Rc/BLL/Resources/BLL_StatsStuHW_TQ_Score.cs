namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsStuHW_TQ_Score
    {
        private readonly DAL_StatsStuHW_TQ_Score dal = new DAL_StatsStuHW_TQ_Score();

        public bool Add(Model_StatsStuHW_TQ_Score model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsStuHW_TQ_Score> DataTableToList(DataTable dt)
        {
            List<Model_StatsStuHW_TQ_Score> list = new List<Model_StatsStuHW_TQ_Score>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsStuHW_TQ_Score item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsStuHW_TQ_Score_Id)
        {
            return this.dal.Delete(StatsStuHW_TQ_Score_Id);
        }

        public bool DeleteList(string StatsStuHW_TQ_Score_Idlist)
        {
            return this.dal.DeleteList(StatsStuHW_TQ_Score_Idlist);
        }

        public bool Exists(string StatsStuHW_TQ_Score_Id)
        {
            return this.dal.Exists(StatsStuHW_TQ_Score_Id);
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

        public Model_StatsStuHW_TQ_Score GetModel(string StatsStuHW_TQ_Score_Id)
        {
            return this.dal.GetModel(StatsStuHW_TQ_Score_Id);
        }

        public Model_StatsStuHW_TQ_Score GetModelByCache(string StatsStuHW_TQ_Score_Id)
        {
            string cacheKey = "Model_StatsStuHW_TQ_ScoreModel-" + StatsStuHW_TQ_Score_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsStuHW_TQ_Score_Id);
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
            return (Model_StatsStuHW_TQ_Score) cache;
        }

        public List<Model_StatsStuHW_TQ_Score> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsStuHW_TQ_Score model)
        {
            return this.dal.Update(model);
        }
    }
}

