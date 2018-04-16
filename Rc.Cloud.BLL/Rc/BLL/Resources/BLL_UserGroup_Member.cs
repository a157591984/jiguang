namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_UserGroup_Member
    {
        private readonly DAL_UserGroup_Member dal = new DAL_UserGroup_Member();

        public bool Add(Model_UserGroup_Member model)
        {
            return this.dal.Add(model);
        }

        public bool AgreeMemberJoinGroup(List<Model_UserGroup_Member> listMember, List<Model_Msg> listMsg)
        {
            return this.dal.AgreeMemberJoinGroup(listMember, listMsg);
        }

        public int ClearUserGroup(string UserGroup_Id)
        {
            return this.dal.ClearUserGroup(UserGroup_Id);
        }

        public int ClearUserGroupMember(string UserGroup_Id)
        {
            return this.dal.ClearUserGroupMember(UserGroup_Id);
        }

        public List<Model_UserGroup_Member> DataTableToList(DataTable dt)
        {
            List<Model_UserGroup_Member> list = new List<Model_UserGroup_Member>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_UserGroup_Member item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string UserGroup_Member_Id)
        {
            return this.dal.Delete(UserGroup_Member_Id);
        }

        public bool Delete(string UserGroup_Member_Id, Model_Msg modelMsg)
        {
            return this.dal.Delete(UserGroup_Member_Id, modelMsg);
        }

        public bool DeleteList(string UserGroup_Member_Idlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(UserGroup_Member_Idlist, 0));
        }

        public bool Exists(string UserGroup_Member_Id)
        {
            return this.dal.Exists(UserGroup_Member_Id);
        }

        public DataSet GetAllClassList(string strWhere)
        {
            return this.dal.GetAllClassList(strWhere);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetClassMemberList(string strWhere, string orderby)
        {
            return this.dal.GetClassMemberList(strWhere, orderby);
        }

        public DataSet GetClassMemberListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetClassMemberListByPageEX(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetClassMemberListEX(string strWhere, string HomeWork_Id, string orderby)
        {
            return this.dal.GetClassMemberListEX(strWhere, HomeWork_Id, orderby);
        }

        public DataSet GetClassStuList_APP(string classId)
        {
            return this.dal.GetClassStuList_APP(classId);
        }

        public DataSet GetCreateAddClassList(string strWhere)
        {
            return this.dal.GetCreateAddClassList(strWhere);
        }

        public DataSet GetGradeMemberListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetGradeMemberListByPageEX(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetGradeMemberListByPageForSys(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetGradeMemberListByPageForSys(strWhere, orderby, startIndex, endIndex);
        }

        public int GetGradeMemberRecordCountForSys(string strWhere)
        {
            return this.dal.GetGradeMemberRecordCountForSys(strWhere);
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

        public Model_UserGroup_Member GetModel(string UserGroup_Member_Id)
        {
            return this.dal.GetModel(UserGroup_Member_Id);
        }

        public Model_UserGroup_Member GetModel(string UserGroup_Id, string User_ID)
        {
            return this.dal.GetModel(UserGroup_Id, User_ID);
        }

        public Model_UserGroup_Member GetModelByCache(string UserGroup_Member_Id)
        {
            string cacheKey = "Model_UserGroup_MemberModel-" + UserGroup_Member_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(UserGroup_Member_Id);
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
            return (Model_UserGroup_Member) cache;
        }

        public List<Model_UserGroup_Member> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSchoolMemberListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetSchoolMemberListByPageEX(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetSchoolMemberListByPageForSys(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetSchoolMemberListByPageForSys(strWhere, orderby, startIndex, endIndex);
        }

        public int GetSchoolMemberRecordCountForSys(string strWhere)
        {
            return this.dal.GetSchoolMemberRecordCountForSys(strWhere);
        }

        public int ImportClassMemberData(List<Model_F_User> listModelFU, List<Model_UserGroup_Member> listModelUGM, Model_UserGroup modelUG)
        {
            return this.dal.ImportClassMemberData(listModelFU, listModelUGM, modelUG);
        }

        public bool QuitGradeSchool(string PUserGroup_Id, string UserGroup_Id, Model_Msg modelMsg)
        {
            return this.dal.QuitGradeSchool(PUserGroup_Id, UserGroup_Id, modelMsg);
        }

        public bool RefuseMemberJoinGroup(string UserGroup_Member_Id, List<Model_Msg> listMsg)
        {
            return this.dal.RefuseMemberJoinGroup(UserGroup_Member_Id, listMsg);
        }

        public bool setHeaderMaster(string sessionUserId, string userGroupMemberId, string userGroupId, string userId)
        {
            return this.dal.setHeaderMaster(sessionUserId, userGroupMemberId, userGroupId, userId);
        }

        public bool TeacherRemoveStudent(Model_UserGroup_Member model, Model_Msg modelMsg)
        {
            return this.dal.TeacherRemoveStudent(model, modelMsg);
        }

        public bool Update(Model_UserGroup_Member model)
        {
            return this.dal.Update(model);
        }
    }
}

