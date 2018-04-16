namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_CustomizeHtml
    {
        private readonly DAL_CustomizeHtml dal = new DAL_CustomizeHtml();

        public bool Add(Model_CustomizeHtml model)
        {
            return this.dal.Add(model);
        }

        public List<Model_CustomizeHtml> DataTableToList(DataTable dt)
        {
            List<Model_CustomizeHtml> list = new List<Model_CustomizeHtml>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_CustomizeHtml item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string CustomizeHtml_Id)
        {
            return this.dal.Delete(CustomizeHtml_Id);
        }

        public bool DeleteList(string CustomizeHtml_Idlist)
        {
            return this.dal.DeleteList(CustomizeHtml_Idlist);
        }

        public bool Exists(string CustomizeHtml_Id)
        {
            return this.dal.Exists(CustomizeHtml_Id);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public string GetHtmlContentByHtmlType(string HtmlType)
        {
            return this.dal.GetHtmlContentByHtmlType(HtmlType);
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

        public Model_CustomizeHtml GetModel(string CustomizeHtml_Id)
        {
            return this.dal.GetModel(CustomizeHtml_Id);
        }

        public Model_CustomizeHtml GetModelByCache(string CustomizeHtml_Id)
        {
            string cacheKey = "Model_CustomizeHtmlModel-" + CustomizeHtml_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CustomizeHtml_Id);
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
            return (Model_CustomizeHtml) cache;
        }

        public Model_CustomizeHtml GetModelByHtmlType(string HtmlType)
        {
            return this.dal.GetModelByHtmlType(HtmlType);
        }

        public List<Model_CustomizeHtml> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_CustomizeHtml model)
        {
            return this.dal.Update(model);
        }
    }
}

