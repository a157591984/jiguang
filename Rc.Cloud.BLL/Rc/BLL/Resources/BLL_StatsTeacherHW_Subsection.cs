namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsTeacherHW_Subsection
    {
        private readonly DAL_StatsTeacherHW_Subsection dal = new DAL_StatsTeacherHW_Subsection();

        public bool Add(Model_StatsTeacherHW_Subsection model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsTeacherHW_Subsection> DataTableToList(DataTable dt)
        {
            List<Model_StatsTeacherHW_Subsection> list = new List<Model_StatsTeacherHW_Subsection>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsTeacherHW_Subsection item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsTeacherHW_SubsectionID)
        {
            return this.dal.Delete(StatsTeacherHW_SubsectionID);
        }

        public bool DeleteList(string StatsTeacherHW_SubsectionIDlist)
        {
            return this.dal.DeleteList(StatsTeacherHW_SubsectionIDlist);
        }

        public bool Exists(string StatsTeacherHW_SubsectionID)
        {
            return this.dal.Exists(StatsTeacherHW_SubsectionID);
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

        public Model_StatsTeacherHW_Subsection GetModel(string StatsTeacherHW_SubsectionID)
        {
            return this.dal.GetModel(StatsTeacherHW_SubsectionID);
        }

        public Model_StatsTeacherHW_Subsection GetModelByCache(string StatsTeacherHW_SubsectionID)
        {
            string cacheKey = "Model_StatsTeacherHW_SubsectionModel-" + StatsTeacherHW_SubsectionID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsTeacherHW_SubsectionID);
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
            return (Model_StatsTeacherHW_Subsection) cache;
        }

        public List<Model_StatsTeacherHW_Subsection> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsTeacherHW_Subsection model)
        {
            return this.dal.Update(model);
        }
    }
}

