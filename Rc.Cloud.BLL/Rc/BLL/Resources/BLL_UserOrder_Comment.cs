namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_UserOrder_Comment
    {
        private readonly DAL_UserOrder_Comment dal = new DAL_UserOrder_Comment();

        public bool Add(Model_UserOrder_Comment model)
        {
            return this.dal.Add(model);
        }

        public List<Model_UserOrder_Comment> DataTableToList(DataTable dt)
        {
            List<Model_UserOrder_Comment> list = new List<Model_UserOrder_Comment>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_UserOrder_Comment item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string comment_id)
        {
            return this.dal.Delete(comment_id);
        }

        public bool DeleteList(string comment_idlist)
        {
            return this.dal.DeleteList(comment_idlist);
        }

        public bool Exists(string comment_id)
        {
            return this.dal.Exists(comment_id);
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

        public Model_UserOrder_Comment GetModel(string comment_id)
        {
            return this.dal.GetModel(comment_id);
        }

        public Model_UserOrder_Comment GetModelByCache(string comment_id)
        {
            string cacheKey = "Model_UserOrder_CommentModel-" + comment_id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(comment_id);
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
            return (Model_UserOrder_Comment) cache;
        }

        public List<Model_UserOrder_Comment> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_UserOrder_Comment model)
        {
            return this.dal.Update(model);
        }
    }
}

