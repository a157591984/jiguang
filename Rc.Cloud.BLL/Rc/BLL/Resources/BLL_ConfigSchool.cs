namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_ConfigSchool
    {
        private readonly DAL_ConfigSchool dal = new DAL_ConfigSchool();

        public bool Add(Model_ConfigSchool model)
        {
            return this.dal.Add(model);
        }

        public List<Model_ConfigSchool> DataTableToList(DataTable dt)
        {
            List<Model_ConfigSchool> list = new List<Model_ConfigSchool>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_ConfigSchool item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ConfigEnum)
        {
            return this.dal.Delete(ConfigEnum);
        }

        public bool DeleteList(string ConfigEnumlist)
        {
            return this.dal.DeleteList(ConfigEnumlist);
        }

        public bool Exists(string ConfigEnum)
        {
            return this.dal.Exists(ConfigEnum);
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

        public Model_ConfigSchool GetModel(string ConfigEnum)
        {
            return this.dal.GetModel(ConfigEnum);
        }

        public Model_ConfigSchool GetModelByCache(string ConfigEnum)
        {
            string cacheKey = "Model_ConfigSchoolModel-" + ConfigEnum;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ConfigEnum);
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
            return (Model_ConfigSchool) cache;
        }

        public Model_ConfigSchool GetModelBySchoolId(string SchoolId)
        {
            return this.dal.GetModelBySchoolId(SchoolId);
        }

        public Model_ConfigSchool GetModelBySchoolIdNew(string SchoolId)
        {
            return this.dal.GetModelBySchoolIdNew(SchoolId);
        }

        public List<Model_ConfigSchool> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public DataSet GetOperateList(string strWhere)
        {
            return this.dal.GetOperateList(strWhere);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSchoolInfoList(string userId)
        {
            return this.dal.GetSchoolInfoList(userId);
        }

        public DataSet GetSchoolPublicUrl(string userId)
        {
            return this.dal.GetSchoolPublicUrl(userId);
        }

        public bool Update(Model_ConfigSchool model)
        {
            return this.dal.Update(model);
        }
    }
}

