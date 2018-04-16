namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SchoolSMS
    {
        private readonly DAL_SchoolSMS dal = new DAL_SchoolSMS();

        public bool Add(Model_SchoolSMS model)
        {
            return this.dal.Add(model);
        }

        public List<Model_SchoolSMS> DataTableToList(DataTable dt)
        {
            List<Model_SchoolSMS> list = new List<Model_SchoolSMS>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SchoolSMS item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string School_Id)
        {
            return this.dal.Delete(School_Id);
        }

        public bool DeleteList(string School_Idlist)
        {
            return this.dal.DeleteList(School_Idlist);
        }

        public bool Exists(string School_Id)
        {
            return this.dal.Exists(School_Id);
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

        public Model_SchoolSMS GetModel(string School_Id)
        {
            return this.dal.GetModel(School_Id);
        }

        public Model_SchoolSMS GetModelByCache(string School_Id)
        {
            string cacheKey = "Model_SchoolSMSModel-" + School_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(School_Id);
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
            return (Model_SchoolSMS) cache;
        }

        public List<Model_SchoolSMS> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SchoolSMS model)
        {
            return this.dal.Update(model);
        }
    }
}

