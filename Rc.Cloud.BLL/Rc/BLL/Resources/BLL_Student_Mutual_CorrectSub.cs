namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Student_Mutual_CorrectSub
    {
        private readonly DAL_Student_Mutual_CorrectSub dal = new DAL_Student_Mutual_CorrectSub();

        public bool Add(Model_Student_Mutual_CorrectSub model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Student_Mutual_CorrectSub> DataTableToList(DataTable dt)
        {
            List<Model_Student_Mutual_CorrectSub> list = new List<Model_Student_Mutual_CorrectSub>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Student_Mutual_CorrectSub item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Student_Mutual_CorrectSub_Id)
        {
            return this.dal.Delete(Student_Mutual_CorrectSub_Id);
        }

        public bool DeleteList(string Student_Mutual_CorrectSub_Idlist)
        {
            return this.dal.DeleteList(Student_Mutual_CorrectSub_Idlist);
        }

        public bool Exists(string Student_Mutual_CorrectSub_Id)
        {
            return this.dal.Exists(Student_Mutual_CorrectSub_Id);
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

        public Model_Student_Mutual_CorrectSub GetModel(string Student_Mutual_CorrectSub_Id)
        {
            return this.dal.GetModel(Student_Mutual_CorrectSub_Id);
        }

        public Model_Student_Mutual_CorrectSub GetModelByCache(string Student_Mutual_CorrectSub_Id)
        {
            string cacheKey = "Model_Student_Mutual_CorrectSubModel-" + Student_Mutual_CorrectSub_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Student_Mutual_CorrectSub_Id);
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
            return (Model_Student_Mutual_CorrectSub) cache;
        }

        public List<Model_Student_Mutual_CorrectSub> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Student_Mutual_CorrectSub model)
        {
            return this.dal.Update(model);
        }
    }
}

