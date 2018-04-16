namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SchoolSMS_Person
    {
        private readonly DAL_SchoolSMS_Person dal = new DAL_SchoolSMS_Person();

        public bool Add(Model_SchoolSMS_Person model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SchoolSMS_Person> DataTableToList(DataTable dt)
        {
            List<Model_SchoolSMS_Person> list = new List<Model_SchoolSMS_Person>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SchoolSMS_Person item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SchoolSMS_Person_Id)
        {
            return this.dal.Delete(SchoolSMS_Person_Id);
        }

        public bool DeleteList(string SchoolSMS_Person_Idlist)
        {
            return this.dal.DeleteList(SchoolSMS_Person_Idlist);
        }

        public bool Exists(string SchoolSMS_Person_Id)
        {
            return this.dal.Exists(SchoolSMS_Person_Id);
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

        public Model_SchoolSMS_Person GetModel(string SchoolSMS_Person_Id)
        {
            return this.dal.GetModel(SchoolSMS_Person_Id);
        }

        public Model_SchoolSMS_Person GetModelByCache(string SchoolSMS_Person_Id)
        {
            string cacheKey = "Model_SchoolSMS_PersonModel-" + SchoolSMS_Person_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SchoolSMS_Person_Id);
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
            return (Model_SchoolSMS_Person) cache;
        }

        public List<Model_SchoolSMS_Person> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SchoolSMS_Person model)
        {
            return this.dal.Update(model);
        }
    }
}

