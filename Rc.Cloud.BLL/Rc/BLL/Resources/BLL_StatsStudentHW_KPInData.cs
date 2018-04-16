namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsStudentHW_KPInData
    {
        private readonly DAL_StatsStudentHW_KPInData dal = new DAL_StatsStudentHW_KPInData();

        public bool Add(Model_StatsStudentHW_KPInData model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsStudentHW_KPInData> DataTableToList(DataTable dt)
        {
            List<Model_StatsStudentHW_KPInData> list = new List<Model_StatsStudentHW_KPInData>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsStudentHW_KPInData item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsStudentHW_KPInDataID)
        {
            return this.dal.Delete(StatsStudentHW_KPInDataID);
        }

        public bool DeleteList(string StatsStudentHW_KPInDataIDlist)
        {
            return this.dal.DeleteList(StatsStudentHW_KPInDataIDlist);
        }

        public bool Exists(string StatsStudentHW_KPInDataID)
        {
            return this.dal.Exists(StatsStudentHW_KPInDataID);
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

        public Model_StatsStudentHW_KPInData GetModel(string StatsStudentHW_KPInDataID)
        {
            return this.dal.GetModel(StatsStudentHW_KPInDataID);
        }

        public Model_StatsStudentHW_KPInData GetModelByCache(string StatsStudentHW_KPInDataID)
        {
            string cacheKey = "Model_StatsStudentHW_KPInDataModel-" + StatsStudentHW_KPInDataID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsStudentHW_KPInDataID);
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
            return (Model_StatsStudentHW_KPInData) cache;
        }

        public List<Model_StatsStudentHW_KPInData> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsStudentHW_KPInData model)
        {
            return this.dal.Update(model);
        }
    }
}

