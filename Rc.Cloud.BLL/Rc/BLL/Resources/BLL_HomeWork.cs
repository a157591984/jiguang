namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_HomeWork
    {
        private readonly DAL_HomeWork dal = new DAL_HomeWork();

        public bool Add(Model_HomeWork model)
        {
            return this.dal.Add(model);
        }

        public bool AddHomework(Model_HomeWork model, List<Model_Student_HomeWork> list, List<Model_Student_HomeWork_Submit> listSubmit, List<Model_Student_HomeWork_Correct> listCorrect, Model_StatsHelper modelSH_HW)
        {
            return this.dal.AddHomework(model, list, listSubmit, listCorrect, modelSH_HW);
        }

        public List<Model_HomeWork> DataTableToList(DataTable dt)
        {
            List<Model_HomeWork> list = new List<Model_HomeWork>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_HomeWork item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string HomeWork_Id)
        {
            return this.dal.Delete(HomeWork_Id);
        }

        public bool DeleteList(string HomeWork_Idlist)
        {
            return this.dal.DeleteList(HomeWork_Idlist);
        }

        public bool Exists(string HomeWork_Id)
        {
            return this.dal.Exists(HomeWork_Id);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetHWDetail(string HomeWork_Id)
        {
            return this.dal.GetHWDetail(HomeWork_Id);
        }

        public DataSet GetHWInofBySHWId(string shwId)
        {
            return this.dal.GetHWInofBySHWId(shwId);
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

        public DataSet GetListForStatisticsByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListForStatisticsByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetListForStatisticsRecordcount(string strWhere)
        {
            return this.dal.GetListForStatisticsRecordcount(strWhere);
        }

        public DataSet GetListForTeacherView(string strWhere, string orderby)
        {
            return this.dal.GetListForTeacherView(strWhere, orderby);
        }

        public Model_HomeWork GetModel(string HomeWork_Id)
        {
            return this.dal.GetModel(HomeWork_Id);
        }

        public Model_HomeWork GetModelByCache(string HomeWork_Id)
        {
            string cacheKey = "Model_HomeWorkModel-" + HomeWork_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(HomeWork_Id);
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
            return (Model_HomeWork) cache;
        }

        public List<Model_HomeWork> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCount_Operate(string strWhere)
        {
            return this.dal.GetRecordCount_Operate(strWhere);
        }

        public DataSet GetTchHWListByPage_APP(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetTchHWListByPage_APP(strWhere, orderby, startIndex, endIndex);
        }

        public bool RevokeHW(string HomeWorkId)
        {
            return this.dal.RevokeHW(HomeWorkId);
        }

        public Model_PagerInfo<List<Model_HomeWork>> SearhList(string Where, string Sort, int pageIndex, int pageSize)
        {
            return this.dal.SearhList(Where, Sort, pageIndex, pageSize);
        }

        public bool TchAssignHW_APP(List<Model_HomeWork> listHW, List<Model_Student_HomeWork> listStuHW, List<Model_Student_HomeWork_Submit> listSubmit, List<Model_Student_HomeWork_Correct> listCorrect, List<Model_StatsHelper> listSH)
        {
            return this.dal.TchAssignHW_APP(listHW, listStuHW, listSubmit, listCorrect, listSH);
        }

        public bool Update(Model_HomeWork model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateHomework(Model_HomeWork model, List<Model_Student_HomeWork> list, List<Model_Student_HomeWork_Submit> listSubmit, List<Model_Student_HomeWork_Correct> listCorrect, Model_StatsHelper modelSH_HW)
        {
            return this.dal.UpdateHomework(model, list, listSubmit, listCorrect, modelSH_HW);
        }
    }
}

