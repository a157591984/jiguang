namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SendMessageTemplate
    {
        private readonly DAL_SendMessageTemplate dal = new DAL_SendMessageTemplate();

        public bool Add(Model_SendMessageTemplate model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SendMessageTemplate> DataTableToList(DataTable dt)
        {
            List<Model_SendMessageTemplate> list = new List<Model_SendMessageTemplate>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SendMessageTemplate item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SendSMSTemplateId)
        {
            return this.dal.Delete(SendSMSTemplateId);
        }

        public bool DeleteList(string SendSMSTemplateIdlist)
        {
            return this.dal.DeleteList(SendSMSTemplateIdlist);
        }

        public bool Exists(string SendSMSTemplateId)
        {
            return this.dal.Exists(SendSMSTemplateId);
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

        public Model_SendMessageTemplate GetModel(string SendSMSTemplateId)
        {
            return this.dal.GetModel(SendSMSTemplateId);
        }

        public Model_SendMessageTemplate GetModelByCache(string SendSMSTemplateId)
        {
            string cacheKey = "Model_SendMessageTemplateModel-" + SendSMSTemplateId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SendSMSTemplateId);
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
            return (Model_SendMessageTemplate) cache;
        }

        public Model_SendMessageTemplate GetModelBySType(string SType)
        {
            return this.dal.GetModelBySType(SType);
        }

        public List<Model_SendMessageTemplate> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SendMessageTemplate model)
        {
            return this.dal.Update(model);
        }
    }
}

