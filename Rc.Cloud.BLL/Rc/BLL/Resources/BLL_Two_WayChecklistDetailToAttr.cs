namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.Common.DBUtility;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Two_WayChecklistDetailToAttr
    {
        private readonly DAL_Two_WayChecklistDetailToAttr dal = new DAL_Two_WayChecklistDetailToAttr();

        public bool Add(Model_Two_WayChecklistDetailToAttr model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Two_WayChecklistDetailToAttr> DataTableToList(DataTable dt)
        {
            List<Model_Two_WayChecklistDetailToAttr> list = new List<Model_Two_WayChecklistDetailToAttr>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Two_WayChecklistDetailToAttr item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Two_WayChecklistDetailToAttr_Id)
        {
            return this.dal.Delete(Two_WayChecklistDetailToAttr_Id);
        }

        public bool DeleteList(string Two_WayChecklistDetailToAttr_Idlist)
        {
            return this.dal.DeleteList(Two_WayChecklistDetailToAttr_Idlist);
        }

        public bool Exists(string Two_WayChecklistDetailToAttr_Id)
        {
            return this.dal.Exists(Two_WayChecklistDetailToAttr_Id);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetFilterByCache(string TestPaper_Frame_Id)
        {
            try
            {
                object objObject = null;
                string cacheKey = "TestPaper_FrameAttrs";
                objObject = DataCache.GetCache(cacheKey);
                if (objObject == null)
                {
                    objObject = DbHelperSQL.Query(" select distinct tq.TestQuestions_Id,tqs.ContentText,tqs.TargetText,tqs.ComplexityText,tf.TestPaper_FrameDetail_Id \r\n\tfrom TestQuestions tq \r\n\tleft join TestQuestions_Score tqs on tqs.TestQuestions_Id=tq.TestQuestions_Id \r\n\tinner join TestPaper_FrameDetailToTestQuestions tf on tf.TestQuestions_Id=tq.TestQuestions_Id\r\n\twhere   tq.[type]='simple' and    tf.TestPaper_Frame_Id='" + TestPaper_Frame_Id + "'\r\n\tunion\r\n\tselect distinct tq.Parent_Id,tqs.ContentText,tqs.TargetText,tqs.ComplexityText,tf.TestPaper_FrameDetail_Id \r\n\tfrom TestPaper_FrameDetailToTestQuestions tf\r\n\tinner join  TestQuestions tq2  on tf.TestQuestions_Id=tq2.TestQuestions_Id \r\n\tinner join TestQuestions tq on tq.Parent_Id=tq2.TestQuestions_Id and   tq.Parent_Id<>'0' and tq.[type]='complex' \r\n\tinner join TestQuestions_Score tqs on tqs.TestQuestions_Id=tq.TestQuestions_Id \r\n\twhere tq2.Parent_Id='0' and tq2.[type]='complex' and tf.TestPaper_Frame_Id='" + TestPaper_Frame_Id + "'");
                    if (objObject != null)
                    {
                        int num = 1;
                        DataCache.SetCache(cacheKey, objObject, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                return (DataSet) objObject;
            }
            catch
            {
                return null;
            }
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

        public Model_Two_WayChecklistDetailToAttr GetModel(string Two_WayChecklistDetailToAttr_Id)
        {
            return this.dal.GetModel(Two_WayChecklistDetailToAttr_Id);
        }

        public Model_Two_WayChecklistDetailToAttr GetModelByCache(string Two_WayChecklistDetailToAttr_Id)
        {
            string cacheKey = "Model_Two_WayChecklistDetailToAttrModel-" + Two_WayChecklistDetailToAttr_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Two_WayChecklistDetailToAttr_Id);
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
            return (Model_Two_WayChecklistDetailToAttr) cache;
        }

        public List<Model_Two_WayChecklistDetailToAttr> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Two_WayChecklistDetailToAttr model)
        {
            return this.dal.Update(model);
        }
    }
}

