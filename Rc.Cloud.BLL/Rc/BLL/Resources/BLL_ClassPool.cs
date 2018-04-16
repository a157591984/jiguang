namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_ClassPool
    {
        private readonly DAL_ClassPool dal = new DAL_ClassPool();

        public bool Add(Model_ClassPool model)
        {
            return this.dal.Add(model);
        }

        public List<Model_ClassPool> DataTableToList(DataTable dt)
        {
            List<Model_ClassPool> list = new List<Model_ClassPool>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_ClassPool item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ClassPool_Id)
        {
            return this.dal.Delete(ClassPool_Id);
        }

        public bool DeleteList(string ClassPool_Idlist)
        {
            return this.dal.DeleteList(ClassPool_Idlist);
        }

        public bool Exists(string ClassPool_Id)
        {
            return this.dal.Exists(ClassPool_Id);
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

        public Model_ClassPool GetModel(string ClassPool_Id)
        {
            return this.dal.GetModel(ClassPool_Id);
        }

        public Model_ClassPool GetModelByCache(string ClassPool_Id)
        {
            string cacheKey = "ClassPoolModel-" + ClassPool_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ClassPool_Id);
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
            return (Model_ClassPool) cache;
        }

        public Model_ClassPool GetModelByClass_Id(string Class_Id)
        {
            return this.dal.GetModelByClass_Id(Class_Id);
        }

        public List<Model_ClassPool> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public string GetNewClassID()
        {
            return this.dal.GetNewClassID();
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_ClassPool model)
        {
            return this.dal.Update(model);
        }
    }
}

