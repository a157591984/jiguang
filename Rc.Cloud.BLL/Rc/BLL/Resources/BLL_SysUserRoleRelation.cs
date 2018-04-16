namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SysUserRoleRelation
    {
        private readonly DAL_SysUserRoleRelation dal = new DAL_SysUserRoleRelation();

        public bool Add(Model_SysUserRoleRelation model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SysUserRoleRelation> DataTableToList(DataTable dt)
        {
            List<Model_SysUserRoleRelation> list = new List<Model_SysUserRoleRelation>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SysUserRoleRelation item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public Model_SysUserRoleRelation GetModel()
        {
            return this.dal.GetModel();
        }

        public Model_SysUserRoleRelation GetModelByCache()
        {
            string cacheKey = "Model_SysUserRoleRelationModel-";
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
            return (Model_SysUserRoleRelation) cache;
        }

        public List<Model_SysUserRoleRelation> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SysUserRoleRelation model)
        {
            return this.dal.Update(model);
        }
    }
}

