using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using System.Data;
using Rc.Common.Config;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.Sys
{
    public partial class SchoolList : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90200100";
            if (!IsPostBack)
            {
                string strSql = @"select 
SchoolCount=(select COUNT(1) from UserGroup where UserGroup_AttrEnum='School')
,GradeCount=(select COUNT(1) from UserGroup where UserGroup_AttrEnum='Grade')
,ClassCount=(select COUNT(1) from UserGroup where UserGroup_AttrEnum='Class')
,StudentCount=(select COUNT(1) from UserGroup_Member where UserStatus='0' and MembershipEnum='student')";
                DataTable dtCount = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                if (dtCount.Rows.Count > 0)
                {
                    ltlCount.Text = string.Format("共【{0}】个学校；【{1}】个年级；【{2}】个班；【{3}】个学生"
                        , dtCount.Rows[0]["SchoolCount"], dtCount.Rows[0]["GradeCount"]
                        , dtCount.Rows[0]["ClassCount"], dtCount.Rows[0]["StudentCount"]);
                }
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string GroupName, int PageIndex, int PageSize)
        {
            try
            {
                BLL_UserGroup bll = new BLL_UserGroup();
                GroupName = GroupName.Filter();

                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " UserGroup_AttrEnum='" + UserGroup_AttrEnum.School + "' ";
                if (!string.IsNullOrEmpty(GroupName)) strWhere += " and UserGroup_Name like '%" + GroupName + "%' ";
                string strOrderBy = "CreateTime desc";
                dt = bll.GetGroupListByPage(strWhere, strOrderBy, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount(strWhere);

                //string strGradeWhere = " UserGroup_AttrEnum='" + UserGroup_AttrEnum.Grade
                //    + @"' and UserGroup_ParentId in(SELECT UserGroup_Id FROM (  SELECT ROW_NUMBER() OVER (order by " + strOrderBy
                //    + @")AS Row, T.*  from (select ug.* from UserGroup ug ) T  WHERE " + strWhere
                //    + " ) TT WHERE TT.Row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + ") ";
                //DataTable dtGrade = bll.GetList(strGradeWhere).Tables[0];

                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string userName = dt.Rows[i]["TrueName"].ToString();
                    if (string.IsNullOrEmpty(userName)) userName = dt.Rows[i]["UserName"].ToString();
                    userName += "(" + dt.Rows[i]["UserName"].ToString() + ")";
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        UserGroup_Id = dt.Rows[i]["UserGroup_Id"].ToString(),
                        UserGroup_Name = dt.Rows[i]["UserGroup_Name"].ToString(),
                        UserGroup_BriefIntroduction = dt.Rows[i]["UserGroup_BriefIntroduction"].ToString(),
                        GroupMemberCount = dt.Rows[i]["GroupMemberCount"].ToString(),
                        CreateUser = userName,
                        CreateTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                        IType = 1
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
        /// 清除学校成员
        /// </summary>
        [WebMethod]
        public static string ClearSchoolMember(string UserGroup_Id)
        {
            UserGroup_Id = UserGroup_Id.Filter();
            try
            {

                string strSql = string.Empty;
                strSql = "select count(1) from UserGroup_Member where UserGroup_Id in(select USER_ID from UserGroup_Member where UserGroup_Id='" + UserGroup_Id + "' and MembershipEnum='grade')";
                if (Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString() != "0")
                {
                    return JsonConvert.SerializeObject(new
                    {
                        msg = "操作失败：年级下存在数据，清空失败",
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
                    err = "error"
                });
            }
        }
        /// <summary>
        /// 删除学校
        /// </summary>
        [WebMethod]
        public static string DeleteSchool(string UserGroup_Id)
        {
            try
            {
                string strSql = string.Empty;
                int inum = 0;
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
                    bool b = new BLL_UserGroup().Delete(UserGroup_Id.Filter());
                    if (b)
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
                    err = "error"
                });
            }
        }
    }
}