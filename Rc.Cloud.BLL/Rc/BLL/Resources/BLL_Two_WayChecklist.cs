namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Two_WayChecklist
    {
        private readonly DAL_Two_WayChecklist dal = new DAL_Two_WayChecklist();

        public bool Add(Model_Two_WayChecklist model)
        {
            return this.dal.Add(model);
        }

        public List<Model_Two_WayChecklist> DataTableToList(DataTable dt)
        {
            List<Model_Two_WayChecklist> list = new List<Model_Two_WayChecklist>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Two_WayChecklist item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Two_WayChecklist_Id)
        {
            return this.dal.Delete(Two_WayChecklist_Id);
        }

        public bool DeleteList(string Two_WayChecklist_Idlist)
        {
            return this.dal.DeleteList(Two_WayChecklist_Idlist);
        }

        public bool Exists(string Two_WayChecklist_Id)
        {
            return this.dal.Exists(Two_WayChecklist_Id);
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

        public Model_Two_WayChecklist GetModel(string Two_WayChecklist_Id)
        {
            return this.dal.GetModel(Two_WayChecklist_Id);
        }

        public Model_Two_WayChecklist GetModelByCache(string Two_WayChecklist_Id)
        {
            string cacheKey = "Model_Two_WayChecklistModel-" + Two_WayChecklist_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Two_WayChecklist_Id);
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
            return (Model_Two_WayChecklist) cache;
        }

        public List<Model_Two_WayChecklist> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Two_WayChecklist model)
        {
            return this.dal.Update(model);
        }
    }
}

