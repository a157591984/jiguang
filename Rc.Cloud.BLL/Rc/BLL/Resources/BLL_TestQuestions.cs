namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_TestQuestions
    {
        private readonly DAL_TestQuestions dal = new DAL_TestQuestions();

        public bool Add(Model_TestQuestions model)
        {
            return this.dal.Add(model);
        }

        public List<Model_TestQuestions> DataTableToList(DataTable dt)
        {
            List<Model_TestQuestions> list = new List<Model_TestQuestions>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_TestQuestions item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string TestQuestions_Id)
        {
            return this.dal.Delete(TestQuestions_Id);
        }

        public bool DeleteList(string TestQuestions_Idlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(TestQuestions_Idlist, 0));
        }

        public bool Exists(string TestQuestions_Id)
        {
            return this.dal.Exists(TestQuestions_Id);
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

        public Model_TestQuestions GetModel(string TestQuestions_Id)
        {
            return this.dal.GetModel(TestQuestions_Id);
        }

        public Model_TestQuestions GetModelByCache(string TestQuestions_Id)
        {
            string cacheKey = "Model_TestQuestionsModel-" + TestQuestions_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TestQuestions_Id);
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
            return (Model_TestQuestions) cache;
        }

        public Model_TestQuestions GetModelByResourceToResourceFolder_IdNum(string ResourceToResourceFolder_Id, int TestQuestions_Num)
        {
            return this.dal.GetModelByResourceToResourceFolder_IdNum(ResourceToResourceFolder_Id, TestQuestions_Num);
        }

        public List<Model_TestQuestions> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_TestQuestions model)
        {
            return this.dal.Update(model);
        }
    }
}

