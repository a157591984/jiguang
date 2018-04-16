using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Data;

namespace Rc.Cloud.Web.teacher
{
    public partial class GradeTeacherNums : System.Web.UI.Page
    {
        static string ugroupId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ugroupId = Request.QueryString["ugroupId"].Filter();
        }

        [WebMethod]
        public static string GetTeacherList(string UserName, string UserIdentity)
        {
            try
            {
                UserName = UserName.Filter();
                UserIdentity = UserIdentity.Filter();
                string strWhere = string.Format("User_ApplicationStatus='passed' and UserStatus=0 and UserGroup_Id='{0}' and MembershipEnum in('{1}','{2}') ", ugroupId, MembershipEnum.headmaster, MembershipEnum.teacher);
                if (!string.IsNullOrEmpty(UserName))
                {
                    strWhere += string.Format(" and (UserName like '%{0}%' or TrueName like '%{0}%') ", UserName);
                }
                if (!string.IsNullOrEmpty(UserIdentity))
                {
                    strWhere += string.Format(" and MembershipEnum = '{0}' ", UserIdentity);
                }
                List<object> listReturn = new List<object>();
                DataTable dt = new BLL_UserGroup_Member().GetClassMemberListByPageEX(strWhere, "UserStatus,User_ApplicationPassTime desc", 1, 60).Tables[0];
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        Row = dt.Rows[i]["Row"],
                        UserGroup_Id = dt.Rows[i]["UserGroup_Id"],
                        UserName = string.IsNullOrEmpty(dt.Rows[i]["TrueName"].ToString()) ? dt.Rows[i]["UserName"] : dt.Rows[i]["TrueName"],
                        MembershipEnumText = Rc.Common.EnumService.GetDescription<MembershipEnum>(dt.Rows[i]["MembershipEnum"].ToString()),
                        SubjectName = dt.Rows[i]["SubjectName"],
                        User_ApplicationPassTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["User_ApplicationPassTime"].ToString()),
                        StudentCount = dt.Rows[i]["StudentCount"].ToString()
                    });
                }

                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }

    }
}