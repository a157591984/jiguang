namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_DictRelation
    {
        private readonly DAL_DictRelation dal = new DAL_DictRelation();

        public bool Add(Model_DictRelation model)
        {
            return this.dal.Add(model);
        }

        public List<Model_DictRelation> DataTableToList(DataTable dt)
        {
            List<Model_DictRelation> list = new List<Model_DictRelation>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_DictRelation item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string DictRelation_Id)
        {
            return this.dal.Delete(DictRelation_Id);
        }

        public bool DeleteList(string DictRelation_Idlist)
        {
            return this.dal.DeleteList(DictRelation_Idlist);
        }

        public bool Exists(string DictRelation_Id)
        {
            return this.dal.Exists(DictRelation_Id);
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

        public Model_DictRelation GetModel(string DictRelation_Id)
        {
            return this.dal.GetModel(DictRelation_Id);
        }

        public Model_DictRelation GetModelByCache(string DictRelation_Id)
        {
            string cacheKey = "Model_DictRelationModel-" + DictRelation_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(DictRelation_Id);
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
            return (Model_DictRelation) cache;
        }

        public List<Model_DictRelation> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_DictRelation model)
        {
            return this.dal.Update(model);
        }
    }
}

