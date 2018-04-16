namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_DictRelation_Detail
    {
        private readonly DAL_DictRelation_Detail dal = new DAL_DictRelation_Detail();

        public bool Add(Model_DictRelation_Detail model)
        {
            return this.dal.Add(model);
        }

        public bool Add(string DictRelation_Id, string SecondCode, List<Model_DictRelation_Detail> lsit)
        {
            return this.dal.Add(DictRelation_Id, SecondCode, lsit);
        }

        public List<Model_DictRelation_Detail> DataTableToList(DataTable dt)
        {
            List<Model_DictRelation_Detail> list = new List<Model_DictRelation_Detail>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_DictRelation_Detail item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string DictRelation_Detail_Id)
        {
            return this.dal.Delete(DictRelation_Detail_Id);
        }

        public bool DeleteList(string DictRelation_Detail_Idlist)
        {
            return this.dal.DeleteList(DictRelation_Detail_Idlist);
        }

        public bool Exists(string DictRelation_Detail_Id)
        {
            return this.dal.Exists(DictRelation_Detail_Id);
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

        public Model_DictRelation_Detail GetModel(string DictRelation_Detail_Id)
        {
            return this.dal.GetModel(DictRelation_Detail_Id);
        }

        public Model_DictRelation_Detail GetModelByCache(string DictRelation_Detail_Id)
        {
            string cacheKey = "Model_DictRelation_DetailModel-" + DictRelation_Detail_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(DictRelation_Detail_Id);
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
            return (Model_DictRelation_Detail) cache;
        }

        public List<Model_DictRelation_Detail> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_DictRelation_Detail model)
        {
            return this.dal.Update(model);
        }
    }
}

