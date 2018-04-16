namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Two_WayChecklistToTeacher
    {
        private readonly DAL_Two_WayChecklistToTeacher dal = new DAL_Two_WayChecklistToTeacher();

        public bool Add(Model_Two_WayChecklistToTeacher model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Two_WayChecklistToTeacher> DataTableToList(DataTable dt)
        {
            List<Model_Two_WayChecklistToTeacher> list = new List<Model_Two_WayChecklistToTeacher>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Two_WayChecklistToTeacher item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Two_WayChecklistToTeacher_Id)
        {
            return this.dal.Delete(Two_WayChecklistToTeacher_Id);
        }

        public bool DeleteList(string Two_WayChecklistToTeacher_Idlist)
        {
            return this.dal.DeleteList(Two_WayChecklistToTeacher_Idlist);
        }

        public bool Exists(string Two_WayChecklistToTeacher_Id)
        {
            return this.dal.Exists(Two_WayChecklistToTeacher_Id);
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

        public Model_Two_WayChecklistToTeacher GetModel(string Two_WayChecklistToTeacher_Id)
        {
            return this.dal.GetModel(Two_WayChecklistToTeacher_Id);
        }

        public Model_Two_WayChecklistToTeacher GetModelByCache(string Two_WayChecklistToTeacher_Id)
        {
            string cacheKey = "Model_Two_WayChecklistToTeacherModel-" + Two_WayChecklistToTeacher_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Two_WayChecklistToTeacher_Id);
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
            return (Model_Two_WayChecklistToTeacher) cache;
        }

        public List<Model_Two_WayChecklistToTeacher> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Two_WayChecklistToTeacher model)
        {
            return this.dal.Update(model);
        }
    }
}

