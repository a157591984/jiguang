namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SysUser_token
    {
        private readonly DAL_SysUser_token dal = new DAL_SysUser_token();

        public bool Add(Model_SysUser_token model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SysUser_token> DataTableToList(DataTable dt)
        {
            List<Model_SysUser_token> list = new List<Model_SysUser_token>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SysUser_token item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string user_id, string product_type)
        {
            return this.dal.Delete(user_id, product_type);
        }

        public bool Exists(string user_id, string product_type)
        {
            return this.dal.Exists(user_id, product_type);
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

        public Model_SysUser_token GetModel(string user_id, string product_type)
        {
            return this.dal.GetModel(user_id, product_type);
        }

        public Model_SysUser_token GetModelByCache(string user_id, string product_type)
        {
            string cacheKey = "Model_SysUser_tokenModel-" + user_id + product_type;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(user_id, product_type);
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
            return (Model_SysUser_token) cache;
        }

        public List<Model_SysUser_token> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SysUser_token model)
        {
            return this.dal.Update(model);
        }
    }
}

