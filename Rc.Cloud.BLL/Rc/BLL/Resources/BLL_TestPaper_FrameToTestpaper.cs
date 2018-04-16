namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_TestPaper_FrameToTestpaper
    {
        private readonly DAL_TestPaper_FrameToTestpaper dal = new DAL_TestPaper_FrameToTestpaper();

        public bool Add(Model_TestPaper_FrameToTestpaper model)
        {
            return this.dal.Add(model);
        }

        public int AddRelationPaper(Model_TestPaper_FrameToTestpaper model, List<Model_TestPaper_FrameDetailToTestQuestions> listModel)
        {
            return this.dal.AddRelationPaper(model, listModel);
        }

        public List<Model_TestPaper_FrameToTestpaper> DataTableToList(DataTable dt)
        {
            List<Model_TestPaper_FrameToTestpaper> list = new List<Model_TestPaper_FrameToTestpaper>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_TestPaper_FrameToTestpaper item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string TestPaper_FrameToTestpaper_Id)
        {
            return this.dal.Delete(TestPaper_FrameToTestpaper_Id);
        }

        public bool DeleteList(string TestPaper_FrameToTestpaper_Idlist)
        {
            return this.dal.DeleteList(TestPaper_FrameToTestpaper_Idlist);
        }

        public bool Exists(string TestPaper_FrameToTestpaper_Id)
        {
            return this.dal.Exists(TestPaper_FrameToTestpaper_Id);
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

        public Model_TestPaper_FrameToTestpaper GetModel(string TestPaper_FrameToTestpaper_Id)
        {
            return this.dal.GetModel(TestPaper_FrameToTestpaper_Id);
        }

        public Model_TestPaper_FrameToTestpaper GetModelByCache(string TestPaper_FrameToTestpaper_Id)
        {
            string cacheKey = "Model_TestPaper_FrameToTestpaperModel-" + TestPaper_FrameToTestpaper_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TestPaper_FrameToTestpaper_Id);
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
            return (Model_TestPaper_FrameToTestpaper) cache;
        }

        public List<Model_TestPaper_FrameToTestpaper> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCount_Operate(string strWhere)
        {
            return this.dal.GetRecordCount_Operate(strWhere);
        }

        public bool Update(Model_TestPaper_FrameToTestpaper model)
        {
            return this.dal.Update(model);
        }
    }
}

