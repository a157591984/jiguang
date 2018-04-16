namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsStuHW_TQ_KP
    {
        private readonly DAL_StatsStuHW_TQ_KP dal = new DAL_StatsStuHW_TQ_KP();

        public bool Add(Model_StatsStuHW_TQ_KP model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsStuHW_TQ_KP> DataTableToList(DataTable dt)
        {
            List<Model_StatsStuHW_TQ_KP> list = new List<Model_StatsStuHW_TQ_KP>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsStuHW_TQ_KP item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsStuHW_TQ_KP_Id)
        {
            return this.dal.Delete(StatsStuHW_TQ_KP_Id);
        }

        public bool DeleteList(string StatsStuHW_TQ_KP_Idlist)
        {
            return this.dal.DeleteList(StatsStuHW_TQ_KP_Idlist);
        }

        public bool Exists(string StatsStuHW_TQ_KP_Id)
        {
            return this.dal.Exists(StatsStuHW_TQ_KP_Id);
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

        public Model_StatsStuHW_TQ_KP GetModel(string StatsStuHW_TQ_KP_Id)
        {
            return this.dal.GetModel(StatsStuHW_TQ_KP_Id);
        }

        public Model_StatsStuHW_TQ_KP GetModelByCache(string StatsStuHW_TQ_KP_Id)
        {
            string cacheKey = "Model_StatsStuHW_TQ_KPModel-" + StatsStuHW_TQ_KP_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsStuHW_TQ_KP_Id);
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
            return (Model_StatsStuHW_TQ_KP) cache;
        }

        public List<Model_StatsStuHW_TQ_KP> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsStuHW_TQ_KP model)
        {
            return this.dal.Update(model);
        }
    }
}

