namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsTeacherHW_KPInData
    {
        private readonly DAL_StatsTeacherHW_KPInData dal = new DAL_StatsTeacherHW_KPInData();

        public bool Add(Model_StatsTeacherHW_KPInData model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsTeacherHW_KPInData> DataTableToList(DataTable dt)
        {
            List<Model_StatsTeacherHW_KPInData> list = new List<Model_StatsTeacherHW_KPInData>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsTeacherHW_KPInData item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsTeacherHW_KPInDataID)
        {
            return this.dal.Delete(StatsTeacherHW_KPInDataID);
        }

        public bool DeleteList(string StatsTeacherHW_KPInDataIDlist)
        {
            return this.dal.DeleteList(StatsTeacherHW_KPInDataIDlist);
        }

        public bool Exists(string StatsTeacherHW_KPInDataID)
        {
            return this.dal.Exists(StatsTeacherHW_KPInDataID);
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

        public Model_StatsTeacherHW_KPInData GetModel(string StatsTeacherHW_KPInDataID)
        {
            return this.dal.GetModel(StatsTeacherHW_KPInDataID);
        }

        public Model_StatsTeacherHW_KPInData GetModelByCache(string StatsTeacherHW_KPInDataID)
        {
            string cacheKey = "Model_StatsTeacherHW_KPInDataModel-" + StatsTeacherHW_KPInDataID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsTeacherHW_KPInDataID);
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
            return (Model_StatsTeacherHW_KPInData) cache;
        }

        public List<Model_StatsTeacherHW_KPInData> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsTeacherHW_KPInData model)
        {
            return this.dal.Update(model);
        }
    }
}

