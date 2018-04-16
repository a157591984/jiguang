namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_S_KnowledgePoint
    {
        private readonly DAL_S_KnowledgePoint dal = new DAL_S_KnowledgePoint();

        public bool Add(Model_S_KnowledgePoint model)
        {
            return this.dal.Add(model);
        }

        public bool AddBasic(Model_S_KnowledgePoint model, Model_S_KnowledgePointBasic modelBasic)
        {
            return this.dal.AddBasic(model, modelBasic);
        }

        public List<Model_S_KnowledgePoint> DataTableToList(DataTable dt)
        {
            List<Model_S_KnowledgePoint> list = new List<Model_S_KnowledgePoint>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_S_KnowledgePoint item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string S_KnowledgePoint_Id)
        {
            return this.dal.Delete(S_KnowledgePoint_Id);
        }

        public bool DeleteList(string S_KnowledgePoint_Idlist)
        {
            return this.dal.DeleteList(S_KnowledgePoint_Idlist);
        }

        public bool Exists(string S_KnowledgePoint_Id)
        {
            return this.dal.Exists(S_KnowledgePoint_Id);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetDataForEdit(string kpId)
        {
            return this.dal.GetDataForEdit(kpId);
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

        public DataSet GetListByPageJoinDict(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageJoinDict(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetListJoinDict(string strWhere, string orderby)
        {
            return this.dal.GetListJoinDict(strWhere, orderby);
        }

        public Model_S_KnowledgePoint GetModel(string S_KnowledgePoint_Id)
        {
            return this.dal.GetModel(S_KnowledgePoint_Id);
        }

        public Model_S_KnowledgePoint GetModelByCache(string S_KnowledgePoint_Id)
        {
            string cacheKey = "Model_S_KnowledgePointModel-" + S_KnowledgePoint_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(S_KnowledgePoint_Id);
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
            return (Model_S_KnowledgePoint) cache;
        }

        public List<Model_S_KnowledgePoint> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_S_KnowledgePoint model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateBasic(Model_S_KnowledgePoint model, Model_S_KnowledgePointBasic modelBasic)
        {
            return this.dal.UpdateBasic(model, modelBasic);
        }
    }
}

