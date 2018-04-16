namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SendSMSRecord
    {
        private readonly DAL_SendSMSRecord dal = new DAL_SendSMSRecord();

        public bool Add(Model_SendSMSRecord model)
        {
            return this.dal.Add(model);
        }

        public bool AddMultiSMS(List<Model_SendSMSRecord> listModel)
        {
            return this.dal.AddMultiSMS(listModel);
        }

        public List<Model_SendSMSRecord> DataTableToList(DataTable dt)
        {
            List<Model_SendSMSRecord> list = new List<Model_SendSMSRecord>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SendSMSRecord item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SendSMSRecordId)
        {
            return this.dal.Delete(SendSMSRecordId);
        }

        public bool DeleteList(string SendSMSRecordIdlist)
        {
            return this.dal.DeleteList(SendSMSRecordIdlist);
        }

        public bool Exists(string SendSMSRecordId)
        {
            return this.dal.Exists(SendSMSRecordId);
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

        public Model_SendSMSRecord GetModel(string SendSMSRecordId)
        {
            return this.dal.GetModel(SendSMSRecordId);
        }

        public Model_SendSMSRecord GetModelByCache(string SendSMSRecordId)
        {
            string cacheKey = "Model_SendSMSRecordModel-" + SendSMSRecordId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SendSMSRecordId);
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
            return (Model_SendSMSRecord) cache;
        }

        public List<Model_SendSMSRecord> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SendSMSRecord model)
        {
            return this.dal.Update(model);
        }
    }
}

