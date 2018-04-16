namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsGradeHW_KP
    {
        private readonly DAL_StatsGradeHW_KP dal = new DAL_StatsGradeHW_KP();

        public bool Add(Model_StatsGradeHW_KP model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsGradeHW_KP> DataTableToList(DataTable dt)
        {
            List<Model_StatsGradeHW_KP> list = new List<Model_StatsGradeHW_KP>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsGradeHW_KP item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsGradeHW_KPID)
        {
            return this.dal.Delete(StatsGradeHW_KPID);
        }

        public bool DeleteList(string StatsGradeHW_KPIDlist)
        {
            return this.dal.DeleteList(StatsGradeHW_KPIDlist);
        }

        public bool Exists(string StatsGradeHW_KPID)
        {
            return this.dal.Exists(StatsGradeHW_KPID);
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

        public Model_StatsGradeHW_KP GetModel(string StatsGradeHW_KPID)
        {
            return this.dal.GetModel(StatsGradeHW_KPID);
        }

        public Model_StatsGradeHW_KP GetModelByCache(string StatsGradeHW_KPID)
        {
            string cacheKey = "Model_StatsGradeHW_KPModel-" + StatsGradeHW_KPID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsGradeHW_KPID);
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
            return (Model_StatsGradeHW_KP) cache;
        }

        public List<Model_StatsGradeHW_KP> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsGradeHW_KP model)
        {
            return this.dal.Update(model);
        }
    }
}

