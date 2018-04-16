namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsGradeClassHW_Hierarchy
    {
        private readonly DAL_StatsGradeClassHW_Hierarchy dal = new DAL_StatsGradeClassHW_Hierarchy();

        public bool Add(Model_StatsGradeClassHW_Hierarchy model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsGradeClassHW_Hierarchy> DataTableToList(DataTable dt)
        {
            List<Model_StatsGradeClassHW_Hierarchy> list = new List<Model_StatsGradeClassHW_Hierarchy>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsGradeClassHW_Hierarchy item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsGradeClassHW_HierarchyID)
        {
            return this.dal.Delete(StatsGradeClassHW_HierarchyID);
        }

        public bool DeleteList(string StatsGradeClassHW_HierarchyIDlist)
        {
            return this.dal.DeleteList(StatsGradeClassHW_HierarchyIDlist);
        }

        public bool Exists(string StatsGradeClassHW_HierarchyID)
        {
            return this.dal.Exists(StatsGradeClassHW_HierarchyID);
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

        public Model_StatsGradeClassHW_Hierarchy GetModel(string StatsGradeClassHW_HierarchyID)
        {
            return this.dal.GetModel(StatsGradeClassHW_HierarchyID);
        }

        public Model_StatsGradeClassHW_Hierarchy GetModelByCache(string StatsGradeClassHW_HierarchyID)
        {
            string cacheKey = "Model_StatsGradeClassHW_HierarchyModel-" + StatsGradeClassHW_HierarchyID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsGradeClassHW_HierarchyID);
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
            return (Model_StatsGradeClassHW_Hierarchy) cache;
        }

        public List<Model_StatsGradeClassHW_Hierarchy> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsGradeClassHW_Hierarchy model)
        {
            return this.dal.Update(model);
        }
    }
}

