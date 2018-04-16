namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_FileSyncExecRecordDetail
    {
        private readonly DAL_FileSyncExecRecordDetail dal = new DAL_FileSyncExecRecordDetail();

        public bool Add(Model_FileSyncExecRecordDetail model)
        {
            return this.dal.Add(model);
        }

        public List<Model_FileSyncExecRecordDetail> DataTableToList(DataTable dt)
        {
            List<Model_FileSyncExecRecordDetail> list = new List<Model_FileSyncExecRecordDetail>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_FileSyncExecRecordDetail item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string FileSyncExecRecordDetail_id)
        {
            return this.dal.Delete(FileSyncExecRecordDetail_id);
        }

        public bool DeleteList(string FileSyncExecRecordDetail_idlist)
        {
            return this.dal.DeleteList(FileSyncExecRecordDetail_idlist);
        }

        public bool Exists(string FileSyncExecRecordDetail_id)
        {
            return this.dal.Exists(FileSyncExecRecordDetail_id);
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

        public Model_FileSyncExecRecordDetail GetModel(string FileSyncExecRecordDetail_id)
        {
            return this.dal.GetModel(FileSyncExecRecordDetail_id);
        }

        public Model_FileSyncExecRecordDetail GetModelByCache(string FileSyncExecRecordDetail_id)
        {
            string cacheKey = "Model_FileSyncExecRecordDetailModel-" + FileSyncExecRecordDetail_id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(FileSyncExecRecordDetail_id);
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
            return (Model_FileSyncExecRecordDetail) cache;
        }

        public List<Model_FileSyncExecRecordDetail> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_FileSyncExecRecordDetail model)
        {
            return this.dal.Update(model);
        }
    }
}

