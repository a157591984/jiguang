namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_ResourceToResourceFolder_img
    {
        private readonly DAL_ResourceToResourceFolder_img dal = new DAL_ResourceToResourceFolder_img();

        public bool Add(Model_ResourceToResourceFolder_img model)
        {
            return this.dal.Add(model);
        }

        public List<Model_ResourceToResourceFolder_img> DataTableToList(DataTable dt)
        {
            List<Model_ResourceToResourceFolder_img> list = new List<Model_ResourceToResourceFolder_img>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_ResourceToResourceFolder_img item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ResourceToResourceFolder_img_id)
        {
            return this.dal.Delete(ResourceToResourceFolder_img_id);
        }

        public bool DeleteList(string ResourceToResourceFolder_img_idlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(ResourceToResourceFolder_img_idlist, 0));
        }

        public bool Exists(string ResourceToResourceFolder_img_id)
        {
            return this.dal.Exists(ResourceToResourceFolder_img_id);
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

        public Model_ResourceToResourceFolder_img GetModel(string ResourceToResourceFolder_img_id)
        {
            return this.dal.GetModel(ResourceToResourceFolder_img_id);
        }

        public Model_ResourceToResourceFolder_img GetModelByCache(string ResourceToResourceFolder_img_id)
        {
            string cacheKey = "Model_ResourceToResourceFolder_imgModel-" + ResourceToResourceFolder_img_id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ResourceToResourceFolder_img_id);
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
            return (Model_ResourceToResourceFolder_img) cache;
        }

        public List<Model_ResourceToResourceFolder_img> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_ResourceToResourceFolder_img model)
        {
            return this.dal.Update(model);
        }

        public int UpdateRTRFAddListData(Model_ResourceToResourceFolder modelRTRF, List<Model_ResourceToResourceFolder_img> list, Model_BookProductionLog modelBPL)
        {
            return this.dal.UpdateRTRFAddListData(modelRTRF, list, modelBPL);
        }
    }
}

