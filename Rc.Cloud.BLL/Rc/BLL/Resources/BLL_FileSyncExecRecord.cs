namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_FileSyncExecRecord
    {
        private readonly DAL_FileSyncExecRecord dal = new DAL_FileSyncExecRecord();

        public bool Add(Model_FileSyncExecRecord model)
        {
            return this.dal.Add(model);
        }

        public bool Add_Operate(Model_FileSyncExecRecord model)
        {
            return this.dal.Add_Operate(model);
        }

        public List<Model_FileSyncExecRecord> DataTableToList(DataTable dt)
        {
            List<Model_FileSyncExecRecord> list = new List<Model_FileSyncExecRecord>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_FileSyncExecRecord item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string FileSyncExecRecord_id)
        {
            return this.dal.Delete(FileSyncExecRecord_id);
        }

        public bool DeleteList(string FileSyncExecRecord_idlist)
        {
            return this.dal.DeleteList(FileSyncExecRecord_idlist);
        }

        public bool Exists(string FileSyncExecRecord_id)
        {
            return this.dal.Exists(FileSyncExecRecord_id);
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

        public Model_FileSyncExecRecord GetModel(string FileSyncExecRecord_id)
        {
            return this.dal.GetModel(FileSyncExecRecord_id);
        }

        public Model_FileSyncExecRecord GetModelByCache(string FileSyncExecRecord_id)
        {
            string cacheKey = "Model_FileSyncExecRecordModel-" + FileSyncExecRecord_id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(FileSyncExecRecord_id);
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
            return (Model_FileSyncExecRecord) cache;
        }

        public List<Model_FileSyncExecRecord> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_FileSyncExecRecord model)
        {
            return this.dal.Update(model);
        }

        public bool Update_Operate(Model_FileSyncExecRecord model)
        {
            return this.dal.Update_Operate(model);
        }
    }
}

