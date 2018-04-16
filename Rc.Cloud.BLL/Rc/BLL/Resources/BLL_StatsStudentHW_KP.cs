﻿namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsStudentHW_KP
    {
        private readonly DAL_StatsStudentHW_KP dal = new DAL_StatsStudentHW_KP();

        public bool Add(Model_StatsStudentHW_KP model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsStudentHW_KP> DataTableToList(DataTable dt)
        {
            List<Model_StatsStudentHW_KP> list = new List<Model_StatsStudentHW_KP>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsStudentHW_KP item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsStudentHW_KPID)
        {
            return this.dal.Delete(StatsStudentHW_KPID);
        }

        public bool DeleteList(string StatsStudentHW_KPIDlist)
        {
            return this.dal.DeleteList(StatsStudentHW_KPIDlist);
        }

        public bool Exists(string StatsStudentHW_KPID)
        {
            return this.dal.Exists(StatsStudentHW_KPID);
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

        public Model_StatsStudentHW_KP GetModel(string StatsStudentHW_KPID)
        {
            return this.dal.GetModel(StatsStudentHW_KPID);
        }

        public Model_StatsStudentHW_KP GetModelByCache(string StatsStudentHW_KPID)
        {
            string cacheKey = "Model_StatsStudentHW_KPModel-" + StatsStudentHW_KPID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsStudentHW_KPID);
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
            return (Model_StatsStudentHW_KP) cache;
        }

        public List<Model_StatsStudentHW_KP> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsStudentHW_KP model)
        {
            return this.dal.Update(model);
        }
    }
}

