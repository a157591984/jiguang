namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SysUserTask
    {
        private readonly DAL_SysUserTask dal = new DAL_SysUserTask();

        public bool Add(Model_SysUserTask model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SysUserTask> DataTableToList(DataTable dt)
        {
            List<Model_SysUserTask> list = new List<Model_SysUserTask>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SysUserTask item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SysUserTask_id)
        {
            return this.dal.Delete(SysUserTask_id);
        }

        public bool DeleteList(string SysUserTask_idlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(SysUserTask_idlist, 0));
        }

        public bool Exists(string SysUserTask_id)
        {
            return this.dal.Exists(SysUserTask_id);
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

        public Model_SysUserTask GetModel(string SysUserTask_id)
        {
            return this.dal.GetModel(SysUserTask_id);
        }

        public Model_SysUserTask GetModelByCache(string SysUserTask_id)
        {
            string cacheKey = "Model_SysUserTaskModel-" + SysUserTask_id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SysUserTask_id);
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
            return (Model_SysUserTask) cache;
        }

        public List<Model_SysUserTask> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int TaskAllocation(List<Model_SysUserTask> list, string SysUser_ID, string CreateUser)
        {
            return this.dal.TaskAllocation(list, SysUser_ID, CreateUser);
        }

        public bool Update(Model_SysUserTask model)
        {
            return this.dal.Update(model);
        }
    }
}

