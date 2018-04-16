namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsClassHW_Corrected
    {
        private readonly DAL_StatsClassHW_Corrected dal = new DAL_StatsClassHW_Corrected();

        public bool Add(Model_StatsClassHW_Corrected model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsClassHW_Corrected> DataTableToList(DataTable dt)
        {
            List<Model_StatsClassHW_Corrected> list = new List<Model_StatsClassHW_Corrected>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsClassHW_Corrected item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsClassHW_CorrectedID)
        {
            return this.dal.Delete(StatsClassHW_CorrectedID);
        }

        public bool DeleteList(string StatsClassHW_CorrectedIDlist)
        {
            return this.dal.DeleteList(StatsClassHW_CorrectedIDlist);
        }

        public bool Exists(string StatsClassHW_CorrectedID)
        {
            return this.dal.Exists(StatsClassHW_CorrectedID);
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

        public Model_StatsClassHW_Corrected GetModel(string StatsClassHW_CorrectedID)
        {
            return this.dal.GetModel(StatsClassHW_CorrectedID);
        }

        public Model_StatsClassHW_Corrected GetModelByCache(string StatsClassHW_CorrectedID)
        {
            string cacheKey = "Model_StatsClassHW_CorrectedModel-" + StatsClassHW_CorrectedID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsClassHW_CorrectedID);
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
            return (Model_StatsClassHW_Corrected) cache;
        }

        public List<Model_StatsClassHW_Corrected> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsClassHW_Corrected model)
        {
            return this.dal.Update(model);
        }
    }
}

