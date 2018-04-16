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

namespace Homework.teacher
{
    public partial class classList : Rc.Cloud.Web.Common.FInitData
    {
        string userId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = FloginUser.UserId;
        }

        [WebMethod]
        public static string GetClassList()
        {
            try
            {
                Model_F_User user = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                DataTable dt = new BLL_UserGroup().GetAllClassListByUserIdMembershipEnum(user.UserId, UserGroup_AttrEnum.Class.ToString()).Tables[0];
                List<object> listReturn = new List<object>();
                List<object> listReturn2 = new List<object>();
                int inum = 0;
                DataRow[] dr = dt.Select("User_ID='" + user.UserId + "'", "UserGroupOrder");//自己创建的班级
                DataRow[] dr2 = dt.Select("User_ID<>'" + user.UserId + "'", "JoinApplicationStatus desc,JoinStatus,UserGroupOrder");//加入的班级
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
                        UserGroup_BriefIntroduction = string.Format("{0}"
                        , dr[i]["UserGroup_BriefIntroduction"].ToString() == "" ? "这家伙很懒，什么都没留下~~" : dr[i]["UserGroup_BriefIntroduction"].ToString()),
                        CreateTime = pfunction.ConvertToLongDateTime(dr[i]["CreateTime"].ToString()),
                        MesCount = dr[i]["MsgCount"],
                        MemberCount = dr[i]["MemberCount"],
                        PGroupId = dr[i]["PGroupId"].ToString(),
                        PGroupName = dr[i]["PGroupName"].ToString(),
                        PGroup_User_ApplicationStatus = dr[i]["PGroup_User_ApplicationStatus"].ToString(),
                        PGroup_UserStatus = dr[i]["PGroup_UserStatus"].ToString(),
                        IsGroupOwner = true//pfunction.CheckUserIsGroupOwner(dt.Rows[i]["UserGroup_Id"].ToString(), userId)
                    });
                }
                for (int i = 0; i < dr2.Length; i++)
                {
                    inum++;
                    listReturn2.Add(new
                    {
                        User_ID = dr2[i]["User_ID"],
                        User_Name = dr2[i]["UserName"],
                        UserGroup_Id = dr2[i]["UserGroup_Id"],
                        UserGroup_Name = dr2[i]["UserGroup_Name"],
                        UserGroup_NameSubstring = pfunction.GetSubstring(dr2[i]["UserGroup_Name"].ToString(), 9, false),
                        UserGroup_BriefIntroduction = string.Format("{0}"
                        , dr2[i]["UserGroup_BriefIntroduction"].ToString() == "" ? "这家伙很懒，什么都没留下~~" : dr2[i]["UserGroup_BriefIntroduction"].ToString()),
                        CreateTime = pfunction.ConvertToLongDateTime(dr2[i]["CreateTime"].ToString()),
                        MesCount = dr2[i]["MsgCount"],
                        MemberCount = dr2[i]["MemberCount"],
                        PGroupId = dr2[i]["PGroupId"].ToString(),
                        PGroupName = dr2[i]["PGroupName"].ToString(),
                        PGroup_User_ApplicationStatus = dr2[i]["PGroup_User_ApplicationStatus"].ToString(),
                        JoinApplicationStatus = dr2[i]["JoinApplicationStatus"].ToString(),
                        JoinStatus = dr2[i]["JoinStatus"].ToString(),
                        IsGroupOwner = false//pfunction.CheckUserIsGroupOwner(dt.Rows[i]["UserGroup_Id"].ToString(), userId)
                    });
                }
                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        listSelf = listReturn,
                        listJoin = listReturn2
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
        /// 解散班级
        /// </summary>
        [WebMethod]
        public static string ClassDisband(string ClassId, string GradeId)
        {
            try
            {
                //if (new BLL_UserGroup_Member().GetRecordCount("UserGroup_Id='" + ClassId + "' and MembershipEnum!='" + MembershipEnum.headmaster.ToString() + "' ") > 0)
                //{
                //    return "2";//已有成员
                //}
                //else
                //{
                Model_ClassPool modelClassPool = new Model_ClassPool();
                modelClassPool = new BLL_ClassPool().GetModelByClass_Id(ClassId);
                modelClassPool.IsUsed = 0;
                if (new BLL_UserGroup().DelGroupUpClassPoolDelMember(ClassId, GradeId, modelClassPool))
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
        /// 退出年级
        /// </summary>
        /// <param name="PugroupId">年级Id(被加入群组)</param>
        /// <param name="UgroupId">班级Id</param>
        /// <returns></returns>
        [WebMethod]
        public static string QuitGrade(string PugroupId, string UgroupId, string UserId, string UserName)
        {
            try
            {
                Model_F_User user = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                Model_UserGroup model = new BLL_UserGroup().GetModel(PugroupId);
                Model_Msg modelMsg = new Model_Msg();
                modelMsg.MsgId = Guid.NewGuid().ToString();
                modelMsg.MsgEnum = MsgEnum.QuitGrade.ToString();
                modelMsg.MsgTypeEnum = MsgTypeEumn.Private.ToString();
                modelMsg.ResourceDataId = model.UserGroup_Id;
                modelMsg.MsgTitle = string.Format("成员{0}已退出年级：{1}({2})", UserName, model.UserGroup_Name, model.UserGroup_Id);
                modelMsg.MsgContent = string.Format("成员{0}已退出年级：{1}({2})", UserName, model.UserGroup_Name, model.UserGroup_Id);
                modelMsg.MsgStatus = MsgStatus.Unread.ToString();
                modelMsg.MsgSender = user.UserId;
                modelMsg.MsgAccepter = model.User_ID;
                modelMsg.CreateTime = DateTime.Now;
                modelMsg.CreateUser = user.UserId;

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