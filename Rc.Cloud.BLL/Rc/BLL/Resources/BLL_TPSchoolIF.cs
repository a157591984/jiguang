namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_TPSchoolIF
    {
        private readonly DAL_TPSchoolIF dal = new DAL_TPSchoolIF();

        public bool Add(Model_TPSchoolIF model)
        {
            return this.dal.Add(model);
        }

        public List<Model_TPSchoolIF> DataTableToList(DataTable dt)
        {
            List<Model_TPSchoolIF> list = new List<Model_TPSchoolIF>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_TPSchoolIF item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SchoolIF_Id)
        {
            return this.dal.Delete(SchoolIF_Id);
        }

        public bool DeleteList(string SchoolIF_Idlist)
        {
            return this.dal.DeleteList(SchoolIF_Idlist);
        }

        public bool Exists(string SchoolIF_Id)
        {
            return this.dal.Exists(SchoolIF_Id);
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

        public DataSet GetListByPageSchool(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageSchool(strWhere, orderby, startIndex, endIndex);
        }

        public Model_TPSchoolIF GetModel(string SchoolIF_Id)
        {
            return this.dal.GetModel(SchoolIF_Id);
        }

        public Model_TPSchoolIF GetModelByCache(string SchoolIF_Id)
        {
            string cacheKey = "Model_TPSchoolIFModel-" + SchoolIF_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SchoolIF_Id);
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
            return (Model_TPSchoolIF) cache;
        }

        public Model_TPSchoolIF GetModelBySchoolIF_Code(string SchoolIF_Code)
        {
            return this.dal.GetModelBySchoolIF_Code(SchoolIF_Code);
        }

        public List<Model_TPSchoolIF> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCountSchool(string strWhere)
        {
            return this.dal.GetRecordCountSchool(strWhere);
        }

        public bool Update(Model_TPSchoolIF model)
        {
            return this.dal.Update(model);
        }
    }
}

