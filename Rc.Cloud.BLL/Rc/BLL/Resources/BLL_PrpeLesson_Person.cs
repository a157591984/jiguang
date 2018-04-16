namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_PrpeLesson_Person
    {
        private readonly DAL_PrpeLesson_Person dal = new DAL_PrpeLesson_Person();

        public bool Add(Model_PrpeLesson_Person model)
        {
            return this.dal.Add(model);
        }

        public bool AddPerson(List<Model_PrpeLesson_Person> list)
        {
            return this.dal.AddPerson(list);
        }

        public List<Model_PrpeLesson_Person> DataTableToList(DataTable dt)
        {
            List<Model_PrpeLesson_Person> list = new List<Model_PrpeLesson_Person>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_PrpeLesson_Person item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string PrpeLesson_Person_Id)
        {
            return this.dal.Delete(PrpeLesson_Person_Id);
        }

        public bool DeleteList(string PrpeLesson_Person_Idlist)
        {
            return this.dal.DeleteList(PrpeLesson_Person_Idlist);
        }

        public bool Exists(string PrpeLesson_Person_Id)
        {
            return this.dal.Exists(PrpeLesson_Person_Id);
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

        public Model_PrpeLesson_Person GetModel(string PrpeLesson_Person_Id)
        {
            return this.dal.GetModel(PrpeLesson_Person_Id);
        }

        public Model_PrpeLesson_Person GetModelByCache(string PrpeLesson_Person_Id)
        {
            string cacheKey = "Model_PrpeLesson_PersonModel-" + PrpeLesson_Person_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(PrpeLesson_Person_Id);
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
            return (Model_PrpeLesson_Person) cache;
        }

        public List<Model_PrpeLesson_Person> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_PrpeLesson_Person model)
        {
            return this.dal.Update(model);
        }
    }
}

