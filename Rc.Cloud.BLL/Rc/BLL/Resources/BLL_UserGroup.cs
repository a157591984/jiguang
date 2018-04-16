namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_UserGroup
    {
        private readonly DAL_UserGroup dal = new DAL_UserGroup();

        public bool Add(Model_UserGroup model)
        {
            return this.dal.Add(model);
        }

        public bool AddGroupUpClassPoolAddMember(Model_UserGroup model, Model_ClassPool modelClassPool, Model_UserGroup_Member modelUGM)
        {
            return this.dal.AddGroupUpClassPoolAddMember(model, modelClassPool, modelUGM);
        }

        public List<Model_UserGroup> DataTableToList(DataTable dt)
        {
            List<Model_UserGroup> list = new List<Model_UserGroup>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_UserGroup item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string UserGroup_Id)
        {
            return this.dal.Delete(UserGroup_Id);
        }

        public bool DeleteList(string UserGroup_Idlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(UserGroup_Idlist, 0));
        }

        public bool DelGroupUpClassPoolDelMember(string UserGroup_Id, string PUserGroup_Id, Model_ClassPool modelClassPool)
        {
            return this.dal.DelGroupUpClassPoolDelMember(UserGroup_Id, PUserGroup_Id, modelClassPool);
        }

        public bool Exists(string UserGroup_Id)
        {
            return this.dal.Exists(UserGroup_Id);
        }

        public DataSet GetAllClassListByUserIdMembershipEnum(string userId, string UserGroup_AttrEnum)
        {
            return this.dal.GetAllClassListByUserIdMembershipEnum(userId, UserGroup_AttrEnum);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetClassListByUserIdMembershipEnum(string userId, string MembershipEnum, string UserGroup_AttrEnum)
        {
            return this.dal.GetClassListByUserIdMembershipEnum(userId, MembershipEnum, UserGroup_AttrEnum);
        }

        public DataSet GetGradeListByUserIdMembershipEnum(string userId, string UserGroup_AttrEnum)
        {
            return this.dal.GetGradeListByUserIdMembershipEnum(userId, UserGroup_AttrEnum);
        }

        public DataSet GetGroupListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetGroupListByPage(strWhere, orderby, startIndex, endIndex);
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

        public Model_UserGroup GetModel(string UserGroup_Id)
        {
            return this.dal.GetModel(UserGroup_Id);
        }

        public Model_UserGroup GetModelByCache(string UserGroup_Id)
        {
            string cacheKey = "Model_UserGroupModel-" + UserGroup_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(UserGroup_Id);
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
            return (Model_UserGroup) cache;
        }

        public List<Model_UserGroup> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public DataSet GetPassGroupByUserIdUserGroupAttrEnum(string userId, string UserGroup_AttrEnum)
        {
            return this.dal.GetPassGroupByUserIdUserGroupAttrEnum(userId, UserGroup_AttrEnum);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSchoolListByUserIdMembershipEnum(string userId, string UserGroup_AttrEnum)
        {
            return this.dal.GetSchoolListByUserIdMembershipEnum(userId, UserGroup_AttrEnum);
        }

        public int ImportGradeData(List<Model_F_User> listModelFU, List<Model_UserGroup> listModelUG, List<Model_ClassPool> listModelCP, List<Model_UserGroup_Member> listModelUGM)
        {
            return this.dal.ImportGradeData(listModelFU, listModelUG, listModelCP, listModelUGM);
        }

        public bool Update(Model_UserGroup model)
        {
            return this.dal.Update(model);
        }
    }
}

