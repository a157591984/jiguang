namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Student_WrongHomeWork
    {
        private readonly DAL_Student_WrongHomeWork dal = new DAL_Student_WrongHomeWork();

        public bool Add(Model_Student_WrongHomeWork model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Student_WrongHomeWork> DataTableToList(DataTable dt)
        {
            List<Model_Student_WrongHomeWork> list = new List<Model_Student_WrongHomeWork>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Student_WrongHomeWork item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Student_WrongHomeWork_Id)
        {
            return this.dal.Delete(Student_WrongHomeWork_Id);
        }

        public bool DeleteList(string Student_WrongHomeWork_Idlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(Student_WrongHomeWork_Idlist, 0));
        }

        public bool Exists(string Student_WrongHomeWork_Id)
        {
            return this.dal.Exists(Student_WrongHomeWork_Id);
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

        public Model_Student_WrongHomeWork GetModel(string Student_WrongHomeWork_Id)
        {
            return this.dal.GetModel(Student_WrongHomeWork_Id);
        }

        public Model_Student_WrongHomeWork GetModelByCache(string Student_WrongHomeWork_Id)
        {
            string cacheKey = "Model_Student_WrongHomeWorkModel-" + Student_WrongHomeWork_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Student_WrongHomeWork_Id);
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
            return (Model_Student_WrongHomeWork) cache;
        }

        public List<Model_Student_WrongHomeWork> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Student_WrongHomeWork model)
        {
            return this.dal.Update(model);
        }
    }
}

