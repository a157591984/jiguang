namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsTeacherHW_KP
    {
        private readonly DAL_StatsTeacherHW_KP dal = new DAL_StatsTeacherHW_KP();

        public bool Add(Model_StatsTeacherHW_KP model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsTeacherHW_KP> DataTableToList(DataTable dt)
        {
            List<Model_StatsTeacherHW_KP> list = new List<Model_StatsTeacherHW_KP>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsTeacherHW_KP item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsTeacherHW_KPID)
        {
            return this.dal.Delete(StatsTeacherHW_KPID);
        }

        public bool DeleteList(string StatsTeacherHW_KPIDlist)
        {
            return this.dal.DeleteList(StatsTeacherHW_KPIDlist);
        }

        public bool Exists(string StatsTeacherHW_KPID)
        {
            return this.dal.Exists(StatsTeacherHW_KPID);
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

        public Model_StatsTeacherHW_KP GetModel(string StatsTeacherHW_KPID)
        {
            return this.dal.GetModel(StatsTeacherHW_KPID);
        }

        public Model_StatsTeacherHW_KP GetModelByCache(string StatsTeacherHW_KPID)
        {
            string cacheKey = "Model_StatsTeacherHW_KPModel-" + StatsTeacherHW_KPID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsTeacherHW_KPID);
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
            return (Model_StatsTeacherHW_KP) cache;
        }

        public List<Model_StatsTeacherHW_KP> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsTeacherHW_KP model)
        {
            return this.dal.Update(model);
        }
    }
}

