namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SysUserTaskForderTree
    {
        private readonly DAL_SysUserTaskForderTree dal = new DAL_SysUserTaskForderTree();

        public bool Add(Model_SysUserTaskForderTree model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SysUserTaskForderTree> DataTableToList(DataTable dt)
        {
            List<Model_SysUserTaskForderTree> list = new List<Model_SysUserTaskForderTree>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SysUserTaskForderTree item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete()
        {
            return this.dal.Delete();
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

        public DataSet GetListByResourceFolder_Owner(string ResourceFolder_Owner)
        {
            return this.dal.GetListByResourceFolder_Owner(ResourceFolder_Owner);
        }

        public Model_SysUserTaskForderTree GetModel()
        {
            return this.dal.GetModel();
        }

        public Model_SysUserTaskForderTree GetModelByCache()
        {
            string cacheKey = "SysUserTaskForderTreeModel-";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel();
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
            return (Model_SysUserTaskForderTree) cache;
        }

        public List<Model_SysUserTaskForderTree> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SysUserTaskForderTree model)
        {
            return this.dal.Update(model);
        }
    }
}

