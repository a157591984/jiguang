namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_StatsSchoolMember
    {
        private readonly DAL_StatsSchoolMember dal = new DAL_StatsSchoolMember();

        public bool Add(Model_StatsSchoolMember model)
        {
            return this.dal.Add(model);
        }

        public List<Model_StatsSchoolMember> DataTableToList(DataTable dt)
        {
            List<Model_StatsSchoolMember> list = new List<Model_StatsSchoolMember>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_StatsSchoolMember item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string StatsSchoolMember_Id)
        {
            return this.dal.Delete(StatsSchoolMember_Id);
        }

        public bool DeleteList(string StatsSchoolMember_Idlist)
        {
            return this.dal.DeleteList(StatsSchoolMember_Idlist);
        }

        public bool Exists(string StatsSchoolMember_Id)
        {
            return this.dal.Exists(StatsSchoolMember_Id);
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

        public Model_StatsSchoolMember GetModel(string StatsSchoolMember_Id)
        {
            return this.dal.GetModel(StatsSchoolMember_Id);
        }

        public Model_StatsSchoolMember GetModelByCache(string StatsSchoolMember_Id)
        {
            string cacheKey = "Model_StatsSchoolMemberModel-" + StatsSchoolMember_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StatsSchoolMember_Id);
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
            return (Model_StatsSchoolMember) cache;
        }

        public List<Model_StatsSchoolMember> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_StatsSchoolMember model)
        {
            return this.dal.Update(model);
        }
    }
}

