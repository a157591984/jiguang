namespace Rc.BLL
{
    using Rc.Common;
    using Rc.DAL;
    using Rc.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_NoticeBoard
    {
        private readonly DAL_NoticeBoard dal = new DAL_NoticeBoard();

        public bool Add(Model_NoticeBoard model)
        {
            return this.dal.Add(model);
        }

        public List<Model_NoticeBoard> DataTableToList(DataTable dt)
        {
            List<Model_NoticeBoard> list = new List<Model_NoticeBoard>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_NoticeBoard item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string notice_id)
        {
            return this.dal.Delete(notice_id);
        }

        public bool DeleteList(string notice_idlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(notice_idlist, 0));
        }

        public bool Exists(string notice_id)
        {
            return this.dal.Exists(notice_id);
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

        public Model_NoticeBoard GetModel(string notice_id)
        {
            return this.dal.GetModel(notice_id);
        }

        public Model_NoticeBoard GetModelByCache(string notice_id)
        {
            string cacheKey = "Model_NoticeBoardModel-" + notice_id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(notice_id);
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
            return (Model_NoticeBoard) cache;
        }

        public List<Model_NoticeBoard> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_NoticeBoard model)
        {
            return this.dal.Update(model);
        }
    }
}

