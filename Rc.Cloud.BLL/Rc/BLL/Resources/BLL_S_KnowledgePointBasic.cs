namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_S_KnowledgePointBasic
    {
        private readonly DAL_S_KnowledgePointBasic dal = new DAL_S_KnowledgePointBasic();

        public bool Add(Model_S_KnowledgePointBasic model)
        {
            return this.dal.Add(model);
        }

        public List<Model_S_KnowledgePointBasic> DataTableToList(DataTable dt)
        {
            List<Model_S_KnowledgePointBasic> list = new List<Model_S_KnowledgePointBasic>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_S_KnowledgePointBasic item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string S_KnowledgePointBasic_Id)
        {
            return this.dal.Delete(S_KnowledgePointBasic_Id);
        }

        public bool DeleteList(string S_KnowledgePointBasic_Idlist)
        {
            return this.dal.DeleteList(S_KnowledgePointBasic_Idlist);
        }

        public bool Exists(string S_KnowledgePointBasic_Id)
        {
            return this.dal.Exists(S_KnowledgePointBasic_Id);
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

        public Model_S_KnowledgePointBasic GetModel(string S_KnowledgePointBasic_Id)
        {
            return this.dal.GetModel(S_KnowledgePointBasic_Id);
        }

        public Model_S_KnowledgePointBasic GetModelByCache(string S_KnowledgePointBasic_Id)
        {
            string cacheKey = "Model_S_KnowledgePointBasicModel-" + S_KnowledgePointBasic_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(S_KnowledgePointBasic_Id);
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
            return (Model_S_KnowledgePointBasic) cache;
        }

        public List<Model_S_KnowledgePointBasic> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_S_KnowledgePointBasic model)
        {
            return this.dal.Update(model);
        }
    }
}

