namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Two_WayChecklistDetailToTestQuestions
    {
        private readonly DAL_Two_WayChecklistDetailToTestQuestions dal = new DAL_Two_WayChecklistDetailToTestQuestions();

        public bool Add(Model_Two_WayChecklistDetailToTestQuestions model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Two_WayChecklistDetailToTestQuestions> DataTableToList(DataTable dt)
        {
            List<Model_Two_WayChecklistDetailToTestQuestions> list = new List<Model_Two_WayChecklistDetailToTestQuestions>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Two_WayChecklistDetailToTestQuestions item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Two_WayChecklistDetailToTestQuestions_Id)
        {
            return this.dal.Delete(Two_WayChecklistDetailToTestQuestions_Id);
        }

        public bool DeleteList(string Two_WayChecklistDetailToTestQuestions_Idlist)
        {
            return this.dal.DeleteList(Two_WayChecklistDetailToTestQuestions_Idlist);
        }

        public bool Exists(string Two_WayChecklistDetailToTestQuestions_Id)
        {
            return this.dal.Exists(Two_WayChecklistDetailToTestQuestions_Id);
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

        public Model_Two_WayChecklistDetailToTestQuestions GetModel(string Two_WayChecklistDetailToTestQuestions_Id)
        {
            return this.dal.GetModel(Two_WayChecklistDetailToTestQuestions_Id);
        }

        public Model_Two_WayChecklistDetailToTestQuestions GetModelByCache(string Two_WayChecklistDetailToTestQuestions_Id)
        {
            string cacheKey = "Model_Two_WayChecklistDetailToTestQuestionsModel-" + Two_WayChecklistDetailToTestQuestions_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Two_WayChecklistDetailToTestQuestions_Id);
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
            return (Model_Two_WayChecklistDetailToTestQuestions) cache;
        }

        public List<Model_Two_WayChecklistDetailToTestQuestions> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Two_WayChecklistDetailToTestQuestions model)
        {
            return this.dal.Update(model);
        }
    }
}

