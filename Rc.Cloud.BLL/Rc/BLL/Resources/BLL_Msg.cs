namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Msg
    {
        private readonly DAL_Msg dal = new DAL_Msg();

        public bool Add(Model_Msg model)
        {
            return this.dal.Add(model);
        }

        public bool BatchMarkReadStatus(List<Model_Msg> list)
        {
            return this.dal.BatchMarkReadStatus(list);
        }

        public List<Model_Msg> DataTableToList(DataTable dt)
        {
            List<Model_Msg> list = new List<Model_Msg>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Msg item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string MsgId)
        {
            return this.dal.Delete(MsgId);
        }

        public bool DeleteList(string MsgIdlist)
        {
            return this.dal.DeleteList(MsgIdlist);
        }

        public bool Exists(string MsgId)
        {
            return this.dal.Exists(MsgId);
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

        public DataSet GetListForSearch(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListForSearch(strWhere, orderby, startIndex, endIndex);
        }

        public Model_Msg GetModel(string MsgId)
        {
            return this.dal.GetModel(MsgId);
        }

        public Model_Msg GetModelByCache(string MsgId)
        {
            string cacheKey = "Model_MsgModel-" + MsgId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(MsgId);
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
            return (Model_Msg) cache;
        }

        public List<Model_Msg> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Msg model)
        {
            return this.dal.Update(model);
        }
    }
}

