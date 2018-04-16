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
    public partial class GradeList : Rc.Cloud.Web.Common.FInitData
    {
        protected string IsCreateGrade = string.Empty;
        protected string IsJoinGrade = string.Empty;
        string userId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = FloginUser.UserId;
            if (FloginUser.UserPost != UserPost.备课组长 && FloginUser.UserPost != UserPost.普通老师) IsCreateGrade = "1";
            if (FloginUser.UserPost == UserPost.备课组长) IsJoinGrade = "1";
        }

        [WebMethod]
        public static string GetGradeList()
        {
            try
            {
                Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                DataTable dt = new BLL_UserGroup().GetGradeListByUserIdMembershipEnum(modelFUser.UserId, UserGroup_AttrEnum.Grade.ToString()).Tables[0];
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
                        , string.IsNullOrEmpty(dr[i]["UserGroup_BriefIntroduction"].ToString()) ? "这家伙很懒，什么都没留下~~" : dr[i]["UserGroup_BriefIntroduction"].ToString()),
                        CreateTime = pfunction.ConvertToLongDateTime(dr[i]["CreateTime"].ToString()),
                        MesCount = dr[i]["MsgCount"],
                        MemberCount = dr[i]["MemberCount"],
                        PGroupId = dr[i]["PGroupId"].ToString(),//学校号
                        PGroup_User_ApplicationStatus = dr[i]["PGroup_User_ApplicationStatus"].ToString(),//申请加入学校状态
                        PGroup_UserStatus = dr[i]["PGroup_UserStatus"].ToString(),//在学校中的状态
                        PGroupName = dr[i]["PGroupName"].ToString(),
                        JoinApplicationStatus = dr[i]["JoinApplicationStatus"].ToString(),
                        JoinStatus = dr[i]["JoinStatus"].ToString(),
                        IsGroupOwner = pfunction.CheckUserIsGroupOwner(dr[i]["UserGroup_Id"].ToString(), modelFUser.UserId)
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
        /// <summary>
        /// 解散年级
        /// </summary>
        [WebMethod]
        public static string ClassDisband(string ClassId, string SchoolId)
        {
            try
            {
                //if (new BLL_UserGroup_Member().GetRecordCount("UserGroup_Id='" + ClassId + "' and MembershipEnum!='" + MembershipEnum.gradedirector.ToString() + "' ") > 0)
                //{
                //    return "2";//已有成员
                //}
                //else
                //{
                Model_ClassPool modelClassPool = new Model_ClassPool();
                modelClassPool = new BLL_ClassPool().GetModelByClass_Id(ClassId);
                modelClassPool.IsUsed = 0;
                if (new BLL_UserGroup().DelGroupUpClassPoolDelMember(ClassId, SchoolId, modelClassPool))
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

        /// <summary>
        /// 退出学校
        /// </summary>
        /// <param name="PugroupId">学校Id(被加入群组)</param>
        /// <param name="UgroupId">年级Id</param>
        /// <returns></returns>
        [WebMethod]
        public static string QuitSchool(string PugroupId, string UgroupId, string UserId, string UserName)
        {
            try
            {
                Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                Model_UserGroup model = new BLL_UserGroup().GetModel(PugroupId);
                Model_Msg modelMsg = new Model_Msg();
                modelMsg.MsgId = Guid.NewGuid().ToString();
                modelMsg.MsgEnum = MsgEnum.QuitGrade.ToString();
                modelMsg.MsgTypeEnum = MsgTypeEumn.Private.ToString();
                modelMsg.ResourceDataId = model.UserGroup_Id;
                modelMsg.MsgTitle = string.Format("成员{0}已退出学校：{1}({2})", UserName, model.UserGroup_Name, model.UserGroup_Id);
                modelMsg.MsgContent = string.Format("成员{0}已退出学校：{1}({2})", UserName, model.UserGroup_Name, model.UserGroup_Id);
                modelMsg.MsgStatus = MsgStatus.Unread.ToString();
                modelMsg.MsgSender = modelFUser.UserId;
                modelMsg.MsgAccepter = model.User_ID;
                modelMsg.CreateTime = DateTime.Now;
                modelMsg.CreateUser = modelFUser.UserId;

                if (new BLL_UserGroup_Member().QuitGradeSchool(PugroupId, UgroupId, modelMsg))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}