namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_FileSyncRecordFail
    {
        private readonly DAL_FileSyncRecordFail dal = new DAL_FileSyncRecordFail();

        public bool Add(Model_FileSyncRecordFail model)
        {
            return this.dal.Add(model);
        }

        public List<Model_FileSyncRecordFail> DataTableToList(DataTable dt)
        {
            List<Model_FileSyncRecordFail> list = new List<Model_FileSyncRecordFail>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_FileSyncRecordFail item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string FileSyncRecordFail_Id)
        {
            return this.dal.Delete(FileSyncRecordFail_Id);
        }

        public bool DeleteClass(string StrWhere)
        {
            return this.dal.DeleteClass(StrWhere);
        }

        public bool DeleteList(string FileSyncRecordFail_Idlist)
        {
            return this.dal.DeleteList(FileSyncRecordFail_Idlist);
        }

        public bool Exists(string FileSyncRecordFail_Id)
        {
            return this.dal.Exists(FileSyncRecordFail_Id);
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

        public DataSet GetListByPageForFileSyncRecordFail(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageForFileSyncRecordFail(strWhere, orderby, startIndex, endIndex);
        }

        public Model_FileSyncRecordFail GetModel(string FileSyncRecordFail_Id)
        {
            return this.dal.GetModel(FileSyncRecordFail_Id);
        }

        public Model_FileSyncRecordFail GetModelByCache(string FileSyncRecordFail_Id)
        {
            string cacheKey = "Model_FileSyncRecordFailModel-" + FileSyncRecordFail_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(FileSyncRecordFail_Id);
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
            return (Model_FileSyncRecordFail) cache;
        }

        public List<Model_FileSyncRecordFail> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_FileSyncRecordFail model)
        {
            return this.dal.Update(model);
        }
    }
}

