using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.Sys
{
    public partial class GradeList : Rc.Cloud.Web.Common.InitPage
    {
        protected string userGroupParentId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90200100";
            userGroupParentId = Request.QueryString["userGroupParentId"].Filter();
            hidBackurl.Value = Request.QueryString["backurl"];
            if (!IsPostBack)
            {
                string strSql = string.Format(@"select 
GradeCount=(select COUNT(1) from UserGroup_Member where UserStatus='0' and MembershipEnum='grade' and UserGroup_Id='{0}')
,ClassCount=(select COUNT(1) from UserGroup_Member where UserStatus='0' and MembershipEnum='classrc' and UserGroup_Id in(
select USER_ID from UserGroup_Member where UserStatus='0' and MembershipEnum='grade' and UserGroup_Id='{0}'
))
,StudentCount=(select COUNT(1) from UserGroup_Member where UserStatus='0' and MembershipEnum='student' and UserGroup_Id in(
select USER_ID from UserGroup_Member where UserStatus='0' and MembershipEnum='classrc' and UserGroup_Id in(
select USER_ID from UserGroup_Member where UserStatus='0' and MembershipEnum='grade' and UserGroup_Id='{0}'
)))", userGroupParentId);
                DataTable dtCount = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                if (dtCount.Rows.Count > 0)
                {
                    ltlCount.Text = string.Format("共【{0}】个年级；【{1}】个班；【{2}】个学生"
                        , dtCount.Rows[0]["GradeCount"], dtCount.Rows[0]["ClassCount"], dtCount.Rows[0]["StudentCount"]);
                }
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string UserGroup_ParentId, string SchoolId, string GroupName, int PageIndex, int PageSize)
        {
            try
            {
                BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                UserGroup_ParentId = UserGroup_ParentId.Filter();
                SchoolId = SchoolId.Filter();
                GroupName = GroupName.Filter();

                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " User_ApplicationStatus='passed' ";
                if (!string.IsNullOrEmpty(UserGroup_ParentId))
                {
                    strWhere += " and UserGroup_Id='" + UserGroup_ParentId + "' ";
                }
                if (!string.IsNullOrEmpty(SchoolId))
                {
                    strWhere += " and UserGroup_Id='" + SchoolId + "' ";
                }
                if (!string.IsNullOrEmpty(GroupName)) strWhere += " and GradeName like '%" + GroupName + "%' ";

                string orderBy = string.Format("charindex(MembershipEnum,'{0},{1},{2},{3},{4}'),GradeName,UserStatus,User_ApplicationPassTime desc"
                    , MembershipEnum.principal, MembershipEnum.vice_principal
                    , MembershipEnum.Dean, MembershipEnum.TeachingLeader, MembershipEnum.grade);
                dt = bll.GetSchoolMemberListByPageForSys(strWhere, orderBy, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetSchoolMemberRecordCountForSys(strWhere);

                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string userName = dt.Rows[i]["GradeName"].ToString();
                    if (string.IsNullOrEmpty(userName)) userName = dt.Rows[i]["UserName"].ToString();
                    if (dt.Rows[i]["MembershipEnum"].ToString() != MembershipEnum.grade.ToString()) userName += "(" + dt.Rows[i]["UserName"].ToString() + ")";
                    inum++;
                    string strMemberCount = dt.Rows[i]["MemberCount"].ToString();
                    if (strMemberCount == "0") strMemberCount = "-";
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        ParentUserGroup_Name = dt.Rows[i]["ParentUserGroup_Name"].ToString(),
                        UserGroup_Id = dt.Rows[i]["User_Id"].ToString(),
                        UserGroup_Name = userName,
                        GradeUser = dt.Rows[i]["GradeUser"].ToString(),
                        UserGroup_BriefIntroduction = dt.Rows[i]["UserGroup_BriefIntroduction"].ToString(),
                        GradeCreateTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["GradeCreateTime"].ToString()),
                        GroupMemberCount = strMemberCount,
                        MembershipEnum = dt.Rows[i]["MembershipEnum"],
                        PostName = dt.Rows[i]["PostName"]
                    });
                }

                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = rCount,
                        list = listReturn
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "暂无数据"
                    });
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }
        /// <summary>
        /// 清除年级成员
        /// </summary>
        [WebMethod]
        public static string ClearGradeMember(string UserGroup_Id)
        {
            UserGroup_Id = UserGroup_Id.Filter();
            try
            {

                string strSql = string.Empty;
                strSql = "select count(1) from UserGroup_Member where UserGroup_Id in(select USER_ID from UserGroup_Member where MembershipEnum='classrc' and  UserGroup_Id='" + UserGroup_Id + "' )";
                if (Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString() != "0")
                {
                    return JsonConvert.SerializeObject(new
                    {
                        msg = "操作失败：班级下存在数据，清空失败",
                        err = "error"
                    });
                }
                else
                {
                    int inum = 0;
                    BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                    inum = bll.ClearUserGroupMember(UserGroup_Id.Filter());
                    if (inum > 0)
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            msg = "操作成功：清除“" + inum + "”条",
                            err = "null"
                        });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            msg = "操作成功：清除“" + inum + "”条",
                            err = "0"
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    msg = "操作失败:" + ex.Message.ToString(),
                    err = "error",
                });
            }
        }
        /// <summary>
        /// 删除年级
        /// </summary>
        [WebMethod]
        public static string DeleteGrade(string UserGroup_Id)
        {
            try
            {
                //string strSql = string.Empty;
                //int inum = 0;
                //BLL_UserGroup_Member bllUGM = new BLL_UserGroup_Member();
                //BLL_UserGroup bllUG = new BLL_UserGroup();
                //string strWhere = string.Empty;
                //strWhere += string.Format(" where UserGroup_Id='{0}'", UserGroup_Id.Filter());
                //inum = bllUGM.ClearUserGroupMember(strWhere);//暂时没使用事物
                //strWhere = string.Format(" where User_Id='{0}'", UserGroup_Id.Filter());
                //bllUGM.ClearUserGroupMember(strWhere);//暂时没使用事物
                if (new BLL_UserGroup_Member().GetRecordCount("UserGroup_Id='" + UserGroup_Id + "'") > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        msg = "存在下级数据，不允许删除",
                        err = "error"
                    });
                }
                else
                {
                    //bool b = new BLL_UserGroup().Delete(UserGroup_Id.Filter());
                    if (new BLL_UserGroup_Member().ClearUserGroup(UserGroup_Id.Filter()) > 0)
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            msg = "操作成功",
                            err = "null"
                        });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            msg = "操作失败",
                            err = "0"
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    msg = "操作失败:" + ex.Message.ToString(),
                    err = "error",
                });
            }
        }
    }
}