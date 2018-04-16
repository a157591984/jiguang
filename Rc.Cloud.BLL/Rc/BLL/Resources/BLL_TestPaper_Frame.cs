namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_TestPaper_Frame
    {
        private readonly DAL_TestPaper_Frame dal = new DAL_TestPaper_Frame();

        public bool Add(Model_TestPaper_Frame model)
        {
            return this.dal.Add(model);
        }

        public List<Model_TestPaper_Frame> DataTableToList(DataTable dt)
        {
            List<Model_TestPaper_Frame> list = new List<Model_TestPaper_Frame>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_TestPaper_Frame item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string TestPaper_Frame_Id)
        {
            return this.dal.Delete(TestPaper_Frame_Id);
        }

        public bool DeleteList(string TestPaper_Frame_Idlist)
        {
            return this.dal.DeleteList(TestPaper_Frame_Idlist);
        }

        public bool Exists(string TestPaper_Frame_Id)
        {
            return this.dal.Exists(TestPaper_Frame_Id);
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

        public Model_TestPaper_Frame GetModel(string TestPaper_Frame_Id)
        {
            return this.dal.GetModel(TestPaper_Frame_Id);
        }

        public Model_TestPaper_Frame GetModelByCache(string TestPaper_Frame_Id)
        {
            string cacheKey = "Model_TestPaper_FrameModel-" + TestPaper_Frame_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TestPaper_Frame_Id);
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
            return (Model_TestPaper_Frame) cache;
        }

        public List<Model_TestPaper_Frame> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_TestPaper_Frame model)
        {
            return this.dal.Update(model);
        }
    }
}

