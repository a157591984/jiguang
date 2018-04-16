namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SyncFileToSchoolDataDetail2
    {
        private readonly DAL_SyncFileToSchoolDataDetail2 dal = new DAL_SyncFileToSchoolDataDetail2();

        public bool Add(Model_SyncFileToSchoolDataDetail2 model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SyncFileToSchoolDataDetail2> DataTableToList(DataTable dt)
        {
            List<Model_SyncFileToSchoolDataDetail2> list = new List<Model_SyncFileToSchoolDataDetail2>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SyncFileToSchoolDataDetail2 item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SyncFileToSchoolDataDetail2_Id)
        {
            return this.dal.Delete(SyncFileToSchoolDataDetail2_Id);
        }

        public bool DeleteList(string SyncFileToSchoolDataDetail2_Idlist)
        {
            return this.dal.DeleteList(SyncFileToSchoolDataDetail2_Idlist);
        }

        public bool Exists(string SyncFileToSchoolDataDetail2_Id)
        {
            return this.dal.Exists(SyncFileToSchoolDataDetail2_Id);
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

        public Model_SyncFileToSchoolDataDetail2 GetModel(string SyncFileToSchoolDataDetail2_Id)
        {
            return this.dal.GetModel(SyncFileToSchoolDataDetail2_Id);
        }

        public Model_SyncFileToSchoolDataDetail2 GetModelByCache(string SyncFileToSchoolDataDetail2_Id)
        {
            string cacheKey = "Model_SyncFileToSchoolDataDetail2Model-" + SyncFileToSchoolDataDetail2_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SyncFileToSchoolDataDetail2_Id);
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
            return (Model_SyncFileToSchoolDataDetail2) cache;
        }

        public List<Model_SyncFileToSchoolDataDetail2> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SyncFileToSchoolDataDetail2 model)
        {
            return this.dal.Update(model);
        }
    }
}

