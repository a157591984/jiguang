namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_TestQuestions_Option
    {
        private readonly DAL_TestQuestions_Option dal = new DAL_TestQuestions_Option();

        public bool Add(Model_TestQuestions_Option model)
        {
            return this.dal.Add(model);
        }

        public List<Model_TestQuestions_Option> DataTableToList(DataTable dt)
        {
            List<Model_TestQuestions_Option> list = new List<Model_TestQuestions_Option>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_TestQuestions_Option item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string TestQuestions_Option_Id)
        {
            return this.dal.Delete(TestQuestions_Option_Id);
        }

        public bool DeleteList(string TestQuestions_Option_Idlist)
        {
            return this.dal.DeleteList(TestQuestions_Option_Idlist);
        }

        public bool Exists(string TestQuestions_Option_Id)
        {
            return this.dal.Exists(TestQuestions_Option_Id);
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

        public Model_TestQuestions_Option GetModel(string TestQuestions_Option_Id)
        {
            return this.dal.GetModel(TestQuestions_Option_Id);
        }

        public Model_TestQuestions_Option GetModelByCache(string TestQuestions_Option_Id)
        {
            string cacheKey = "Model_TestQuestions_OptionModel-" + TestQuestions_Option_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TestQuestions_Option_Id);
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
            return (Model_TestQuestions_Option) cache;
        }

        public List<Model_TestQuestions_Option> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_TestQuestions_Option model)
        {
            return this.dal.Update(model);
        }
    }
}

