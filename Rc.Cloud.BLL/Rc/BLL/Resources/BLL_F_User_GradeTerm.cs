namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_F_User_GradeTerm
    {
        private readonly DAL_F_User_GradeTerm dal = new DAL_F_User_GradeTerm();

        public bool Add(Model_F_User_GradeTerm model)
        {
            return this.dal.Add(model);
        }

        public List<Model_F_User_GradeTerm> DataTableToList(DataTable dt)
        {
            List<Model_F_User_GradeTerm> list = new List<Model_F_User_GradeTerm>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_F_User_GradeTerm item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string UserId, string GradeTerm_ID)
        {
            return this.dal.Delete(UserId, GradeTerm_ID);
        }

        public bool Exists(string UserId, string GradeTerm_ID)
        {
            return this.dal.Exists(UserId, GradeTerm_ID);
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

        public Model_F_User_GradeTerm GetModel(string UserId, string GradeTerm_ID)
        {
            return this.dal.GetModel(UserId, GradeTerm_ID);
        }

        public Model_F_User_GradeTerm GetModelByCache(string UserId, string GradeTerm_ID)
        {
            string cacheKey = "Model_F_User_GradeTermModel-" + UserId + GradeTerm_ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(UserId, GradeTerm_ID);
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
            return (Model_F_User_GradeTerm) cache;
        }

        public List<Model_F_User_GradeTerm> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_F_User_GradeTerm model)
        {
            return this.dal.Update(model);
        }
    }
}

