namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_FileSyncRecord
    {
        private readonly DAL_FileSyncRecord dal = new DAL_FileSyncRecord();

        public bool Add(Model_FileSyncRecord model)
        {
            return this.dal.Add(model);
        }

        public List<Model_FileSyncRecord> DataTableToList(DataTable dt)
        {
            List<Model_FileSyncRecord> list = new List<Model_FileSyncRecord>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_FileSyncRecord item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string FileSyncRecord_Id)
        {
            return this.dal.Delete(FileSyncRecord_Id);
        }

        public bool DeleteList(string FileSyncRecord_Idlist)
        {
            return this.dal.DeleteList(FileSyncRecord_Idlist);
        }

        public bool Exists(string FileSyncRecord_Id)
        {
            return this.dal.Exists(FileSyncRecord_Id);
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

        public Model_FileSyncRecord GetModel(string FileSyncRecord_Id)
        {
            return this.dal.GetModel(FileSyncRecord_Id);
        }

        public Model_FileSyncRecord GetModelByCache(string FileSyncRecord_Id)
        {
            string cacheKey = "Model_FileSyncRecordModel-" + FileSyncRecord_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(FileSyncRecord_Id);
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
            return (Model_FileSyncRecord) cache;
        }

        public List<Model_FileSyncRecord> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public GH_PagerInfo<List<Model_FileSyncRecord>> SearhExeccuteDataAnalysis(string Where, string Sort, int pageIndex, int pageSize)
        {
            return this.dal.SearhExeccuteDataAnalysis(Where, Sort, pageIndex, pageSize);
        }

        public bool Update(Model_FileSyncRecord model)
        {
            return this.dal.Update(model);
        }
    }
}

