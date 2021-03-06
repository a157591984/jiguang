﻿namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_GradeTermSubjectTeacherBooks
    {
        private readonly DAL_GradeTermSubjectTeacherBooks dal = new DAL_GradeTermSubjectTeacherBooks();

        public bool Add(Model_GradeTermSubjectTeacherBooks model)
        {
            return this.dal.Add(model);
        }

        public int AddandUpdateMulti(List<Model_GradeTermSubjectTeacherBooks> listAdd, List<Model_GradeTermSubjectTeacherBooks> listUpdate)
        {
            return this.dal.AddandUpdateMulti(listAdd, listUpdate);
        }

        public List<Model_GradeTermSubjectTeacherBooks> DataTableToList(DataTable dt)
        {
            List<Model_GradeTermSubjectTeacherBooks> list = new List<Model_GradeTermSubjectTeacherBooks>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_GradeTermSubjectTeacherBooks item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ResourceFolder_Id)
        {
            return this.dal.Delete(ResourceFolder_Id);
        }

        public bool DeleteList(string ResourceFolder_Idlist)
        {
            return this.dal.DeleteList(ResourceFolder_Idlist);
        }

        public bool Exists(string ResourceFolder_Id)
        {
            return this.dal.Exists(ResourceFolder_Id);
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

        public Model_GradeTermSubjectTeacherBooks GetModel(string ResourceFolder_Id)
        {
            return this.dal.GetModel(ResourceFolder_Id);
        }

        public Model_GradeTermSubjectTeacherBooks GetModelByCache(string ResourceFolder_Id)
        {
            string cacheKey = "GradeTermSubjectTeacherBooksModel-" + ResourceFolder_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ResourceFolder_Id);
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
            return (Model_GradeTermSubjectTeacherBooks) cache;
        }

        public List<Model_GradeTermSubjectTeacherBooks> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_GradeTermSubjectTeacherBooks model)
        {
            return this.dal.Update(model);
        }
    }
}

