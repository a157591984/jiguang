namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_TestQuestions_Score
    {
        private readonly DAL_TestQuestions_Score dal = new DAL_TestQuestions_Score();

        public bool Add(Model_TestQuestions_Score model)
        {
            return this.dal.Add(model);
        }

        public List<Model_TestQuestions_Score> DataTableToList(DataTable dt)
        {
            List<Model_TestQuestions_Score> list = new List<Model_TestQuestions_Score>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_TestQuestions_Score item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string TestQuestions_Score_ID)
        {
            return this.dal.Delete(TestQuestions_Score_ID);
        }

        public bool DeleteList(string TestQuestions_Score_IDlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(TestQuestions_Score_IDlist, 0));
        }

        public bool Exists(string TestQuestions_Score_ID)
        {
            return this.dal.Exists(TestQuestions_Score_ID);
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

        public Model_TestQuestions_Score GetModel(string TestQuestions_Score_ID)
        {
            return this.dal.GetModel(TestQuestions_Score_ID);
        }

        public Model_TestQuestions_Score GetModelByCache(string TestQuestions_Score_ID)
        {
            string cacheKey = "Model_TestQuestions_ScoreModel-" + TestQuestions_Score_ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TestQuestions_Score_ID);
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
            return (Model_TestQuestions_Score) cache;
        }

        public Model_TestQuestions_Score GetModelByResourceToResourceFolder_IdNum(string ResourceToResourceFolder_Id, int TestQuestions_Num)
        {
            return this.dal.GetModelByResourceToResourceFolder_IdNum(ResourceToResourceFolder_Id, TestQuestions_Num);
        }

        public Model_TestQuestions_Score GetModelByResourceToResourceFolder_IdTestID(string ResourceToResourceFolder_Id, string TestQuestions_ID, int TestQuestions_OrderNum)
        {
            return this.dal.GetModelByResourceToResourceFolder_IdTestID(ResourceToResourceFolder_Id, TestQuestions_ID, TestQuestions_OrderNum);
        }

        public List<Model_TestQuestions_Score> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public decimal GetSutdentFillScore(string ResourceToResourceFolder_Id, string TestQuestions_Id, int TestQuestions_OrderNum)
        {
            return this.dal.GetSutdentFillScore(ResourceToResourceFolder_Id, TestQuestions_Id, TestQuestions_OrderNum);
        }

        public string GetTestAttr(string ResourceToResourceFolder_Id, string TestQuestions_Id, string AttrType)
        {
            switch (AttrType)
            {
                case "AnalyzeData":
                    return this.dal.GetTestAnalyzeData(ResourceToResourceFolder_Id, TestQuestions_Id);

                case "AnalyzeHtml":
                    return this.dal.GetTestAnalyzeHtml(ResourceToResourceFolder_Id, TestQuestions_Id);

                case "TrainData":
                    return this.dal.GetTestTrainData(ResourceToResourceFolder_Id, TestQuestions_Id);

                case "TrainHtml":
                    return this.dal.GetTestTrainHtml(ResourceToResourceFolder_Id, TestQuestions_Id);
            }
            return "";
        }

        public bool Update(Model_TestQuestions_Score model)
        {
            return this.dal.Update(model);
        }
    }
}

