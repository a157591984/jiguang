namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_HomeworkToTQ
    {
        private readonly DAL_HomeworkToTQ dal = new DAL_HomeworkToTQ();

        public bool Add(Model_HomeworkToTQ model)
        {
            return this.dal.Add(model);
        }

        public bool AddMultiData(List<Model_HomeworkToTQ> listMHTTQ)
        {
            return this.dal.AddMultiData(listMHTTQ);
        }

        public List<Model_HomeworkToTQ> DataTableToList(DataTable dt)
        {
            List<Model_HomeworkToTQ> list = new List<Model_HomeworkToTQ>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_HomeworkToTQ item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string HomeworkToTQ_Id)
        {
            return this.dal.Delete(HomeworkToTQ_Id);
        }

        public bool DeleteList(string HomeworkToTQ_Idlist)
        {
            return this.dal.DeleteList(HomeworkToTQ_Idlist);
        }

        public bool Exists(string HomeworkToTQ_Id)
        {
            return this.dal.Exists(HomeworkToTQ_Id);
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

        public Model_HomeworkToTQ GetModel(string HomeworkToTQ_Id)
        {
            return this.dal.GetModel(HomeworkToTQ_Id);
        }

        public Model_HomeworkToTQ GetModelByCache(string HomeworkToTQ_Id)
        {
            string cacheKey = "Model_HomeworkToTQModel-" + HomeworkToTQ_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(HomeworkToTQ_Id);
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
            return (Model_HomeworkToTQ) cache;
        }

        public List<Model_HomeworkToTQ> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_HomeworkToTQ model)
        {
            return this.dal.Update(model);
        }
    }
}

