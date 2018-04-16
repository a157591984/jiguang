namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_RelationPaperTemp
    {
        private readonly DAL_RelationPaperTemp dal = new DAL_RelationPaperTemp();

        public bool Add(Model_RelationPaperTemp model)
        {
            return this.dal.Add(model);
        }

        public List<Model_RelationPaperTemp> DataTableToList(DataTable dt)
        {
            List<Model_RelationPaperTemp> list = new List<Model_RelationPaperTemp>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_RelationPaperTemp item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string RelationPaperTemp_id)
        {
            return this.dal.Delete(RelationPaperTemp_id);
        }

        public bool DeleteList(string RelationPaperTemp_idlist)
        {
            return this.dal.DeleteList(RelationPaperTemp_idlist);
        }

        public bool Exists(string RelationPaperTemp_id)
        {
            return this.dal.Exists(RelationPaperTemp_id);
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

        public Model_RelationPaperTemp GetModel(string RelationPaperTemp_id)
        {
            return this.dal.GetModel(RelationPaperTemp_id);
        }

        public Model_RelationPaperTemp GetModelByCache(string RelationPaperTemp_id)
        {
            string cacheKey = "Model_RelationPaperTempModel-" + RelationPaperTemp_id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RelationPaperTemp_id);
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
            return (Model_RelationPaperTemp) cache;
        }

        public List<Model_RelationPaperTemp> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_RelationPaperTemp model)
        {
            return this.dal.Update(model);
        }
    }
}

