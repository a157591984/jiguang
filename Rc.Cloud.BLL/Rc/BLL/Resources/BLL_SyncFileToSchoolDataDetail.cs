namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SyncFileToSchoolDataDetail
    {
        private readonly DAL_SyncFileToSchoolDataDetail dal = new DAL_SyncFileToSchoolDataDetail();

        public bool Add(Model_SyncFileToSchoolDataDetail model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SyncFileToSchoolDataDetail> DataTableToList(DataTable dt)
        {
            List<Model_SyncFileToSchoolDataDetail> list = new List<Model_SyncFileToSchoolDataDetail>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SyncFileToSchoolDataDetail item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SyncFileToSchoolDataDetail_Id)
        {
            return this.dal.Delete(SyncFileToSchoolDataDetail_Id);
        }

        public bool DeleteList(string SyncFileToSchoolDataDetail_Idlist)
        {
            return this.dal.DeleteList(SyncFileToSchoolDataDetail_Idlist);
        }

        public bool Exists(string SyncFileToSchoolDataDetail_Id)
        {
            return this.dal.Exists(SyncFileToSchoolDataDetail_Id);
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

        public Model_SyncFileToSchoolDataDetail GetModel(string SyncFileToSchoolDataDetail_Id)
        {
            return this.dal.GetModel(SyncFileToSchoolDataDetail_Id);
        }

        public Model_SyncFileToSchoolDataDetail GetModelByCache(string SyncFileToSchoolDataDetail_Id)
        {
            string cacheKey = "Model_SyncFileToSchoolDataDetailModel-" + SyncFileToSchoolDataDetail_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SyncFileToSchoolDataDetail_Id);
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
            return (Model_SyncFileToSchoolDataDetail) cache;
        }

        public List<Model_SyncFileToSchoolDataDetail> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SyncFileToSchoolDataDetail model)
        {
            return this.dal.Update(model);
        }
    }
}

