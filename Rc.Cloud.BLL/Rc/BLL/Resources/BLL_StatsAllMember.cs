namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsAllMember
    {
        private readonly DAL_StatsAllMember dal = new DAL_StatsAllMember();

        public bool Add(Model_StatsAllMember model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsAllMember> DataTableToList(DataTable dt)
        {
            List<Model_StatsAllMember> list = new List<Model_StatsAllMember>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsAllMember item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsAllMember_Id)
        {
            return this.dal.Delete(StatsAllMember_Id);
        }

        public bool DeleteList(string StatsAllMember_Idlist)
        {
            return this.dal.DeleteList(StatsAllMember_Idlist);
        }

        public bool Exists(string StatsAllMember_Id)
        {
            return this.dal.Exists(StatsAllMember_Id);
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

        public Model_StatsAllMember GetModel(string StatsAllMember_Id)
        {
            return this.dal.GetModel(StatsAllMember_Id);
        }

        public Model_StatsAllMember GetModelByCache(string StatsAllMember_Id)
        {
            string cacheKey = "Model_StatsAllMemberModel-" + StatsAllMember_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsAllMember_Id);
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
            return (Model_StatsAllMember) cache;
        }

        public List<Model_StatsAllMember> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSchoolName(string Table)
        {
            return this.dal.GetSchoolName(Table);
        }

        public bool Update(Model_StatsAllMember model)
        {
            return this.dal.Update(model);
        }
    }
}

