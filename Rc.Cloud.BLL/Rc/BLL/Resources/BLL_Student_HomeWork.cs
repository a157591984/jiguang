namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Student_HomeWork
    {
        private readonly DAL_Student_HomeWork dal = new DAL_Student_HomeWork();

        public bool Add(Model_Student_HomeWork model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Student_HomeWork> DataTableToList(DataTable dt)
        {
            List<Model_Student_HomeWork> list = new List<Model_Student_HomeWork>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Student_HomeWork item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Student_HomeWork_Id)
        {
            return this.dal.Delete(Student_HomeWork_Id);
        }

        public bool DeleteList(string Student_HomeWork_Idlist)
        {
            return this.dal.DeleteList(Student_HomeWork_Idlist);
        }

        public bool Exists(string Student_HomeWork_Id)
        {
            return this.dal.Exists(Student_HomeWork_Id);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetClassHomeWorkList(string strWhere)
        {
            return this.dal.GetClassHomeWorkList(strWhere);
        }

        public DataSet GetClassStudentSubmitedList(string HomeWork_Id)
        {
            return this.dal.GetClassStudentSubmitedList(HomeWork_Id);
        }

        public DataSet GetClassStudentUnSubmitedList(string HomeWork_Id)
        {
            return this.dal.GetClassStudentUnSubmitedList(HomeWork_Id);
        }

        public DataSet GetHWStuCount(string HomeWork_Id)
        {
            return this.dal.GetHWStuCount(HomeWork_Id);
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

        public Model_Student_HomeWork GetModel(string Student_HomeWork_Id)
        {
            return this.dal.GetModel(Student_HomeWork_Id);
        }

        public Model_Student_HomeWork GetModelByCache(string Student_HomeWork_Id)
        {
            string cacheKey = "Model_Student_HomeWorkModel-" + Student_HomeWork_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Student_HomeWork_Id);
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
            return (Model_Student_HomeWork) cache;
        }

        public List<Model_Student_HomeWork> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public Model_Student_HomeWork GetModelNew(string HomeWork_Id, string Student_Id)
        {
            return this.dal.GetModelNew(HomeWork_Id, Student_Id);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Student_HomeWork model)
        {
            return this.dal.Update(model);
        }
    }
}

