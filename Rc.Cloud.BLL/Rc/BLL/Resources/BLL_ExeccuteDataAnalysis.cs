namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_ExeccuteDataAnalysis
    {
        private readonly DAL_ExeccuteDataAnalysis dal = new DAL_ExeccuteDataAnalysis();

        public bool Add(Model_ExeccuteDataAnalysis model)
        {
            return this.dal.Add(model);
        }

        public List<Model_ExeccuteDataAnalysis> DataTableToList(DataTable dt)
        {
            List<Model_ExeccuteDataAnalysis> list = new List<Model_ExeccuteDataAnalysis>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_ExeccuteDataAnalysis item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ExeccuteDataAnalysisID)
        {
            return this.dal.Delete(ExeccuteDataAnalysisID);
        }

        public bool DeleteList(string ExeccuteDataAnalysisIDlist)
        {
            return this.dal.DeleteList(ExeccuteDataAnalysisIDlist);
        }

        public bool Exists(string ExeccuteDataAnalysisID)
        {
            return this.dal.Exists(ExeccuteDataAnalysisID);
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

        public Model_ExeccuteDataAnalysis GetModel(string ExeccuteDataAnalysisID)
        {
            return this.dal.GetModel(ExeccuteDataAnalysisID);
        }

        public Model_ExeccuteDataAnalysis GetModelByCache(string ExeccuteDataAnalysisID)
        {
            string cacheKey = "Model_ExeccuteDataAnalysisModel-" + ExeccuteDataAnalysisID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ExeccuteDataAnalysisID);
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
            return (Model_ExeccuteDataAnalysis) cache;
        }

        public List<Model_ExeccuteDataAnalysis> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public GH_PagerInfo<List<Model_ExeccuteDataAnalysis>> SearhExeccuteDataAnalysis(string Where, string Sort, int pageIndex, int pageSize)
        {
            return this.dal.SearhExeccuteDataAnalysis(Where, Sort, pageIndex, pageSize);
        }

        public bool Update(Model_ExeccuteDataAnalysis model)
        {
            return this.dal.Update(model);
        }
    }
}

