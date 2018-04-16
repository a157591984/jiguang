﻿namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_TestPaper_FrameDetail
    {
        private readonly DAL_TestPaper_FrameDetail dal = new DAL_TestPaper_FrameDetail();

        public bool Add(Model_TestPaper_FrameDetail model)
        {
            return this.dal.Add(model);
        }

        public List<Model_TestPaper_FrameDetail> DataTableToList(DataTable dt)
        {
            List<Model_TestPaper_FrameDetail> list = new List<Model_TestPaper_FrameDetail>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_TestPaper_FrameDetail item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string TestPaper_FrameDetail_Id)
        {
            return this.dal.Delete(TestPaper_FrameDetail_Id);
        }

        public bool DeleteList(string TestPaper_FrameDetail_Idlist)
        {
            return this.dal.DeleteList(TestPaper_FrameDetail_Idlist);
        }

        public bool Exists(string TestPaper_FrameDetail_Id)
        {
            return this.dal.Exists(TestPaper_FrameDetail_Id);
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

        public Model_TestPaper_FrameDetail GetModel(string TestPaper_FrameDetail_Id)
        {
            return this.dal.GetModel(TestPaper_FrameDetail_Id);
        }

        public Model_TestPaper_FrameDetail GetModelByCache(string TestPaper_FrameDetail_Id)
        {
            string cacheKey = "Model_TestPaper_FrameDetailModel-" + TestPaper_FrameDetail_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TestPaper_FrameDetail_Id);
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
            return (Model_TestPaper_FrameDetail) cache;
        }

        public List<Model_TestPaper_FrameDetail> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_TestPaper_FrameDetail model)
        {
            return this.dal.Update(model);
        }
    }
}

