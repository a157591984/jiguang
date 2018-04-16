namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_SyncFileToSchool
    {
        private readonly DAL_SyncFileToSchool dal = new DAL_SyncFileToSchool();

        public bool Add(Model_SyncFileToSchool model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SyncFileToSchool> DataTableToList(DataTable dt)
        {
            List<Model_SyncFileToSchool> list = new List<Model_SyncFileToSchool>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SyncFileToSchool item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SyncFileToSchool_Id)
        {
            return this.dal.Delete(SyncFileToSchool_Id);
        }

        public bool DeleteList(string SyncFileToSchool_Idlist)
        {
            return this.dal.DeleteList(SyncFileToSchool_Idlist);
        }

        public bool Exists(string SyncFileToSchool_Id)
        {
            return this.dal.Exists(SyncFileToSchool_Id);
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

        public DataSet GetListByPage(int pType, string schoolId, string bookId, int PageIndex, int PageSize, out int rCount)
        {
            return this.dal.GetListByPage(pType, schoolId, bookId, PageIndex, PageSize, out rCount);
        }

        public DataSet GetListByPage(string schoolId, string bookId, string isNeed, int PageIndex, int PageSize, out int rCount)
        {
            return this.dal.GetListByPage(schoolId, bookId, isNeed, PageIndex, PageSize, out rCount);
        }

        public Model_SyncFileToSchool GetModel(string SyncFileToSchool_Id)
        {
            return this.dal.GetModel(SyncFileToSchool_Id);
        }

        public Model_SyncFileToSchool GetModelByCache(string SyncFileToSchool_Id)
        {
            string cacheKey = "Model_SyncFileToSchoolModel-" + SyncFileToSchool_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SyncFileToSchool_Id);
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
            return (Model_SyncFileToSchool) cache;
        }

        public List<Model_SyncFileToSchool> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SyncFileToSchool model)
        {
            return this.dal.Update(model);
        }
    }
}

