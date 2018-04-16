using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.BLL.Resources;
using System.Data;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.Sys
{
    public partial class ClassMemberList : Rc.Cloud.Web.Common.InitPage
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
StudentCount=(select COUNT(1) from UserGroup_Member where UserStatus='0' and MembershipEnum='student' and UserGroup_Id='{0}')", userGroupParentId);
                DataTable dtCount = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                if (dtCount.Rows.Count > 0)
                {
                    ltlCount.Text = string.Format("共【{0}】个学生", dtCount.Rows[0]["StudentCount"]);
                }
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string UserGroup_ParentId, string GroupName, int PageSize, int PageIndex)
        {
            try
            {
                BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                UserGroup_ParentId = UserGroup_ParentId.Filter();
                GroupName = GroupName.Filter();

                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = "User_ApplicationStatus='passed' and UserGroup_Id='" + UserGroup_ParentId + "' ";
                if (!string.IsNullOrEmpty(GroupName)) strWhere += " and (UserName like '%" + GroupName + "%' or TrueName like '%" + GroupName + "%') ";
                string orderBy = string.Format("UserStatus,charindex(MembershipEnum,'{0},{1},{2}'),TrueName,User_ApplicationPassTime desc",
                    MembershipEnum.headmaster, MembershipEnum.teacher, MembershipEnum.student);
                dt = bll.GetClassMemberListByPageEX(strWhere, orderBy, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount(strWhere);

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
                        Row = dt.Rows[i]["Row"],
                        User_Id = dt.Rows[i]["User_Id"],
                        UserGroup_Member_Id = dt.Rows[i]["UserGroup_Member_Id"],
                        UserName = userName,
                        User_ApplicationPassTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["User_ApplicationPassTime"].ToString()),
                        UserStatus = dt.Rows[i]["UserStatus"].ToString(),
                        Email = dt.Rows[i]["Email"].ToString(),
                        MembershipEnum = dt.Rows[i]["MembershipEnum"],
                        ClassName = dt.Rows[i]["ClassName"],
                        MembershipEnumName = (dt.Rows[i]["MembershipEnum"].ToString() == MembershipEnum.teacher.ToString()) ? dt.Rows[i]["SubjectName"].ToString() + "老师" : Rc.Common.EnumService.GetDescription<MembershipEnum>(dt.Rows[i]["MembershipEnum"].ToString())
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
    }
}