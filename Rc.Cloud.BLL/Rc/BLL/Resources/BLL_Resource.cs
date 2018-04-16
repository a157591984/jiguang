namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Resource
    {
        private readonly DAL_Resource dal = new DAL_Resource();

        public bool Add(Model_Resource model)
        {
            return this.dal.Add(model);
        }

        public int ClientUploadScienceWord(Model_Resource modelResoucrce, Model_ResourceToResourceFolder modelRTRFolder, List<Model_ResourceToResourceFolder_img> listModelRTRF_img, Model_BookProductionLog modelBPL)
        {
            return this.dal.ClientUploadScienceWord(modelResoucrce, modelRTRFolder, listModelRTRF_img, modelBPL);
        }

        public int ClientUploadTestPaper(Model_Resource modelResoucrce, Model_ResourceToResourceFolder modelRTRFolder, Model_ResourceToResourceFolder_Property modelRTRFPropety, List<Model_TestQuestions> listModelTQ, List<Model_TestQuestions_Score> listModelTQScore, List<Model_TestQuestions_Option> listModelTQOption, Model_BookProductionLog modelBPL)
        {
            return this.dal.ClientUploadTestPaper(modelResoucrce, modelRTRFolder, modelRTRFPropety, listModelTQ, listModelTQScore, listModelTQOption, modelBPL);
        }

        public int ClientUploadTestPaperUpdate(Model_Resource modelResoucrce, Model_ResourceToResourceFolder modelRTRFolder, Model_ResourceToResourceFolder_Property modelRTRFPropety, List<Model_TestQuestions> listModelTQ, List<Model_TestQuestions_Score> listModelTQScore, List<Model_TestQuestions_Option> listModelTQOption, Model_BookProductionLog modelBPL)
        {
            return this.dal.ClientUploadTestPaperUpdate(modelResoucrce, modelRTRFolder, modelRTRFPropety, listModelTQ, listModelTQScore, listModelTQOption, modelBPL);
        }

        public List<Model_Resource> DataTableToList(DataTable dt)
        {
            List<Model_Resource> list = new List<Model_Resource>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Resource item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Resource_Id)
        {
            return this.dal.Delete(Resource_Id);
        }

        public bool DeleteList(string Resource_Idlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(Resource_Idlist, 0));
        }

        public bool DeleteResource(string ResourceToResourceFolder_Id, string Resource_Id)
        {
            return this.dal.DeleteResource(ResourceToResourceFolder_Id, Resource_Id);
        }

        public bool Exists(string Resource_Id)
        {
            return this.dal.Exists(Resource_Id);
        }

        public string ExistsByMD5(string Resource_MD5)
        {
            return this.dal.ExistsByMD5(Resource_MD5);
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

        public Model_Resource GetModel(string Resource_Id)
        {
            return this.dal.GetModel(Resource_Id);
        }

        public Model_Resource GetModelByCache(string Resource_Id)
        {
            string cacheKey = "Model_ResourceModel-" + Resource_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Resource_Id);
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
            return (Model_Resource) cache;
        }

        public List<Model_Resource> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Resource model)
        {
            return this.dal.Update(model);
        }
    }
}

