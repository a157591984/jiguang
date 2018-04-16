namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_S_KnowledgePointAttrExtend
    {
        private readonly DAL_S_KnowledgePointAttrExtend dal = new DAL_S_KnowledgePointAttrExtend();

        public bool Add(Model_S_KnowledgePointAttrExtend model)
        {
            return this.dal.Add(model);
        }

        public List<Model_S_KnowledgePointAttrExtend> DataTableToList(DataTable dt)
        {
            List<Model_S_KnowledgePointAttrExtend> list = new List<Model_S_KnowledgePointAttrExtend>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_S_KnowledgePointAttrExtend item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string S_KnowledgePointAttrExtend_Id)
        {
            return this.dal.Delete(S_KnowledgePointAttrExtend_Id);
        }

        public bool DeleteList(string S_KnowledgePointAttrExtend_Idlist)
        {
            return this.dal.DeleteList(S_KnowledgePointAttrExtend_Idlist);
        }

        public bool Exists(string S_KnowledgePointAttrExtend_Id)
        {
            return this.dal.Exists(S_KnowledgePointAttrExtend_Id);
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

        public Model_S_KnowledgePointAttrExtend GetModel(string S_KnowledgePointAttrExtend_Id)
        {
            return this.dal.GetModel(S_KnowledgePointAttrExtend_Id);
        }

        public Model_S_KnowledgePointAttrExtend GetModelByCache(string S_KnowledgePointAttrExtend_Id)
        {
            string cacheKey = "Model_S_KnowledgePointAttrExtendModel-" + S_KnowledgePointAttrExtend_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(S_KnowledgePointAttrExtend_Id);
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
            return (Model_S_KnowledgePointAttrExtend) cache;
        }

        public List<Model_S_KnowledgePointAttrExtend> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_S_KnowledgePointAttrExtend model)
        {
            return this.dal.Update(model);
        }
    }
}

