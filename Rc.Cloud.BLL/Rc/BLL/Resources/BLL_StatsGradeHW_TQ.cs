namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsGradeHW_TQ
    {
        private readonly DAL_StatsGradeHW_TQ dal = new DAL_StatsGradeHW_TQ();

        public bool Add(Model_StatsGradeHW_TQ model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsGradeHW_TQ> DataTableToList(DataTable dt)
        {
            List<Model_StatsGradeHW_TQ> list = new List<Model_StatsGradeHW_TQ>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsGradeHW_TQ item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsGradeHW_TQID)
        {
            return this.dal.Delete(StatsGradeHW_TQID);
        }

        public bool DeleteList(string StatsGradeHW_TQIDlist)
        {
            return this.dal.DeleteList(StatsGradeHW_TQIDlist);
        }

        public bool Exists(string StatsGradeHW_TQID)
        {
            return this.dal.Exists(StatsGradeHW_TQID);
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

        public Model_StatsGradeHW_TQ GetModel(string StatsGradeHW_TQID)
        {
            return this.dal.GetModel(StatsGradeHW_TQID);
        }

        public Model_StatsGradeHW_TQ GetModelByCache(string StatsGradeHW_TQID)
        {
            string cacheKey = "Model_StatsGradeHW_TQModel-" + StatsGradeHW_TQID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsGradeHW_TQID);
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
            return (Model_StatsGradeHW_TQ) cache;
        }

        public List<Model_StatsGradeHW_TQ> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsGradeHW_TQ model)
        {
            return this.dal.Update(model);
        }
    }
}

