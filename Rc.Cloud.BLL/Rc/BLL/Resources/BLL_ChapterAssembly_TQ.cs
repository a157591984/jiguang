namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_ChapterAssembly_TQ
    {
        private readonly DAL_ChapterAssembly_TQ dal = new DAL_ChapterAssembly_TQ();

        public bool Add(Model_ChapterAssembly_TQ model)
        {
            return this.dal.Add(model);
        }

        public List<Model_ChapterAssembly_TQ> DataTableToList(DataTable dt)
        {
            List<Model_ChapterAssembly_TQ> list = new List<Model_ChapterAssembly_TQ>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_ChapterAssembly_TQ item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ChapterAssembly_TQ_Id)
        {
            return this.dal.Delete(ChapterAssembly_TQ_Id);
        }

        public bool DeleteList(string ChapterAssembly_TQ_Idlist)
        {
            return this.dal.DeleteList(ChapterAssembly_TQ_Idlist);
        }

        public bool Exists(string ChapterAssembly_TQ_Id)
        {
            return this.dal.Exists(ChapterAssembly_TQ_Id);
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

        public DataSet GetList(string identifier, string ChapterAssembly_TQ_Id, string ChangeType, out int rCount)
        {
            return this.dal.GetList(identifier, ChapterAssembly_TQ_Id, ChangeType, out rCount);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Model_ChapterAssembly_TQ GetModel(string ChapterAssembly_TQ_Id)
        {
            return this.dal.GetModel(ChapterAssembly_TQ_Id);
        }

        public Model_ChapterAssembly_TQ GetModelByCache(string ChapterAssembly_TQ_Id)
        {
            string cacheKey = "Model_ChapterAssembly_TQModel-" + ChapterAssembly_TQ_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ChapterAssembly_TQ_Id);
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
            return (Model_ChapterAssembly_TQ) cache;
        }

        public List<Model_ChapterAssembly_TQ> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_ChapterAssembly_TQ model)
        {
            return this.dal.Update(model);
        }
    }
}

