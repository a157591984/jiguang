namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Student_HomeWorkAnswer
    {
        private readonly DAL_Student_HomeWorkAnswer dal = new DAL_Student_HomeWorkAnswer();

        public bool Add(Model_Student_HomeWorkAnswer model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Student_HomeWorkAnswer> DataTableToList(DataTable dt)
        {
            List<Model_Student_HomeWorkAnswer> list = new List<Model_Student_HomeWorkAnswer>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Student_HomeWorkAnswer item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Student_HomeWorkAnswer_Id)
        {
            return this.dal.Delete(Student_HomeWorkAnswer_Id);
        }

        public bool DeleteList(string Student_HomeWorkAnswer_Idlist)
        {
            return this.dal.DeleteList(Student_HomeWorkAnswer_Idlist);
        }

        public bool Exists(string Student_HomeWorkAnswer_Id)
        {
            return this.dal.Exists(Student_HomeWorkAnswer_Id);
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

        public Model_Student_HomeWorkAnswer GetModel(string Student_HomeWorkAnswer_Id)
        {
            return this.dal.GetModel(Student_HomeWorkAnswer_Id);
        }

        public Model_Student_HomeWorkAnswer GetModelByCache(string Student_HomeWorkAnswer_Id)
        {
            string cacheKey = "Model_Student_HomeWorkAnswerModel-" + Student_HomeWorkAnswer_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Student_HomeWorkAnswer_Id);
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
            return (Model_Student_HomeWorkAnswer) cache;
        }

        public Model_Student_HomeWorkAnswer GetModelBySHWIdAndNum(string Student_HomeWork_Id, string TestQuestions_Id, string TestQuestions_Detail_OrderNum)
        {
            return this.dal.GetModelBySHWIdAndNum(Student_HomeWork_Id, TestQuestions_Id, TestQuestions_Detail_OrderNum);
        }

        public List<Model_Student_HomeWorkAnswer> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int StudentAnswerSubmit(Model_Student_HomeWork_Submit modelSHWSubmit, List<Model_Student_HomeWorkAnswer> listModel)
        {
            return this.dal.StudentAnswerSubmit(modelSHWSubmit, listModel);
        }

        public int TeacherCorrectStuHomeWork(Model_Student_HomeWork_Correct modelSHWCorrect, List<Model_Student_HomeWorkAnswer> listModel, Model_StatsHelper modelSH_HW)
        {
            return this.dal.TeacherCorrectStuHomeWork(modelSHWCorrect, listModel, modelSH_HW);
        }

        public bool Update(Model_Student_HomeWorkAnswer model)
        {
            return this.dal.Update(model);
        }
    }
}

