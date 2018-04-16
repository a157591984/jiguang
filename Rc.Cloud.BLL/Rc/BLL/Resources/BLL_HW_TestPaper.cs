namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_HW_TestPaper
    {
        private readonly DAL_HW_TestPaper dal = new DAL_HW_TestPaper();

        public bool Add(Model_HW_TestPaper model)
        {
            return this.dal.Add(model);
        }

        public List<Model_HW_TestPaper> DataTableToList(DataTable dt)
        {
            List<Model_HW_TestPaper> list = new List<Model_HW_TestPaper>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_HW_TestPaper item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string HW_TestPaper_Id)
        {
            return this.dal.Delete(HW_TestPaper_Id);
        }

        public bool DeleteList(string HW_TestPaper_Idlist)
        {
            return this.dal.DeleteList(HW_TestPaper_Idlist);
        }

        public bool Exists(string HW_TestPaper_Id)
        {
            return this.dal.Exists(HW_TestPaper_Id);
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

        public Model_HW_TestPaper GetModel(string HW_TestPaper_Id)
        {
            return this.dal.GetModel(HW_TestPaper_Id);
        }

        public Model_HW_TestPaper GetModelByCache(string HW_TestPaper_Id)
        {
            string cacheKey = "Model_HW_TestPaperModel-" + HW_TestPaper_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(HW_TestPaper_Id);
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
            return (Model_HW_TestPaper) cache;
        }

        public List<Model_HW_TestPaper> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_HW_TestPaper model)
        {
            return this.dal.Update(model);
        }
    }
}

