using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using Rc.Common.Config;

namespace Rc.Cloud.Web.teacher
{
    public partial class SchoolList : Rc.Cloud.Web.Common.FInitData
    {
        protected string userId = string.Empty;
        protected string IsCreateSchool = string.Empty;
        protected string IsJoinSchool = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = FloginUser.UserId;
            if (FloginUser.UserPost == UserPost.校长) IsCreateSchool = "1";
            if (FloginUser.UserPost == UserPost.副校长 || FloginUser.UserPost == UserPost.教务主任 || FloginUser.UserPost == UserPost.教研组长) IsJoinSchool = "1";

        }

        [WebMethod]
        public static string GetSchoolList(string UserId)
        {
            try
            {
                DataTable dt = new BLL_UserGroup().GetSchoolListByUserIdMembershipEnum(UserId, UserGroup_AttrEnum.School.ToString()).Tables[0];
                List<object> listReturn = new List<object>();
                int inum = 0;
                DataRow[] dr = dt.Select("", "JoinApplicationStatus desc,JoinStatus");
                for (int i = 0; i < dr.Length; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        User_ID = dr[i]["User_ID"],
                        User_Name = dr[i]["UserName"],
                        UserGroup_Id = dr[i]["UserGroup_Id"],
                        UserGroup_Name = dr[i]["UserGroup_Name"],
                        UserGroup_NameSubstring = pfunction.GetSubstring(dr[i]["UserGroup_Name"].ToString(), 9, false),
                        UserGroupp_Type = dr[i]["UserGroupp_Type"],
                        UserGroup_BriefIntroduction = string.Format("{0}"
                        , dr[i]["UserGroup_BriefIntroduction"].ToString() == "" ? "这家伙很懒，什么都没留下~~" : dr[i]["UserGroup_BriefIntroduction"].ToString()),
                        CreateTime = pfunction.ConvertToLongDateTime(dr[i]["CreateTime"].ToString()),
                        MesCount = dr[i]["MsgCount"],
                        MemberCount = dr[i]["MemberCount"],
                        JoinApplicationStatus = dr[i]["JoinApplicationStatus"].ToString(),
                        JoinStatus = dr[i]["JoinStatus"].ToString(),
                        IsGroupOwner = pfunction.CheckUserIsGroupOwner(dr[i]["UserGroup_Id"].ToString(), UserId)
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

        [WebMethod]
        public static string ClassDisband(string ClassId)
        {
            try
            {
                //if (new BLL_UserGroup_Member().GetRecordCount("UserGroup_Id='" + ClassId + "' and MembershipEnum!='" + MembershipEnum.principal.ToString() + "' ") > 0)
                //{
                //    return "2";//已有成员
                //}
                //else
                //{
                //BLL_ClassPool bllCP = new BLL_ClassPool();
                //Model_ClassPool modelCP = new Model_ClassPool();
                //modelCP = bllCP.GetModelByClass_Id(ClassId);
                //modelCP.IsUsed = 0;
                //if (new BLL_UserGroup().Delete(ClassId) && bllCP.Update(modelCP))
                Model_ClassPool modelClassPool = new Model_ClassPool();
                modelClassPool = new BLL_ClassPool().GetModelByClass_Id(ClassId);
                modelClassPool.IsUsed = 0;
                if (new BLL_UserGroup().DelGroupUpClassPoolDelMember(ClassId, "", modelClassPool))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
                //}
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}