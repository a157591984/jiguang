namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsTeacherClassHW_Hierarchy
    {
        private readonly DAL_StatsTeacherClassHW_Hierarchy dal = new DAL_StatsTeacherClassHW_Hierarchy();

        public bool Add(Model_StatsTeacherClassHW_Hierarchy model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsTeacherClassHW_Hierarchy> DataTableToList(DataTable dt)
        {
            List<Model_StatsTeacherClassHW_Hierarchy> list = new List<Model_StatsTeacherClassHW_Hierarchy>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsTeacherClassHW_Hierarchy item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsTeacherClassHW_HierarchyID)
        {
            return this.dal.Delete(StatsTeacherClassHW_HierarchyID);
        }

        public bool DeleteList(string StatsTeacherClassHW_HierarchyIDlist)
        {
            return this.dal.DeleteList(StatsTeacherClassHW_HierarchyIDlist);
        }

        public bool Exists(string StatsTeacherClassHW_HierarchyID)
        {
            return this.dal.Exists(StatsTeacherClassHW_HierarchyID);
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

        public Model_StatsTeacherClassHW_Hierarchy GetModel(string StatsTeacherClassHW_HierarchyID)
        {
            return this.dal.GetModel(StatsTeacherClassHW_HierarchyID);
        }

        public Model_StatsTeacherClassHW_Hierarchy GetModelByCache(string StatsTeacherClassHW_HierarchyID)
        {
            string cacheKey = "Model_StatsTeacherClassHW_HierarchyModel-" + StatsTeacherClassHW_HierarchyID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsTeacherClassHW_HierarchyID);
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
            return (Model_StatsTeacherClassHW_Hierarchy) cache;
        }

        public List<Model_StatsTeacherClassHW_Hierarchy> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsTeacherClassHW_Hierarchy model)
        {
            return this.dal.Update(model);
        }
    }
}

