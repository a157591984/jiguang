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
using Rc.Common.StrUtility;
using Rc.Model.Resources;

namespace Homework.teacher
{
    public partial class classMember : Rc.Cloud.Web.Common.FInitData
    {
        string userId = string.Empty;
        protected string ugroupId = string.Empty;
        static bool bool_IsGroupOwner = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = FloginUser.UserId;
            ugroupId = Request.QueryString["ugroupId"].Filter();
            bool_IsGroupOwner = pfunction.CheckUserIsGroupOwner(ugroupId, userId);
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            DataTable dt = new BLL_UserGroup().GetClassListByUserIdMembershipEnum(userId, MembershipEnum.classrc.ToString(), UserGroup_AttrEnum.Class.ToString()).Tables[0];
            if (string.IsNullOrEmpty(ugroupId) && dt.Rows.Count > 0)
            {
                ugroupId = dt.Rows[0]["UserGroup_Id"].ToString();
            }
            rptClass.DataSource = dt;
            rptClass.DataBind();
        }

        [WebMethod]
        public static string GetClassMember(string UserGroup_Id, int PageIndex, int PageSize)
        {
            try
            {
                UserGroup_Id = UserGroup_Id.Filter();
                Model_F_User user = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSqlCount = string.Empty;
                string strWhere = string.Format("User_ApplicationStatus='passed' and UserGroup_Id='{0}'", UserGroup_Id);
                string orderBy = string.Format("UserStatus,charindex(MembershipEnum,'{0},{1},{2}'),TrueName,User_ApplicationPassTime desc",
                    MembershipEnum.headmaster, MembershipEnum.teacher, MembershipEnum.student);
                dt = new BLL_UserGroup_Member().GetClassMemberListByPageEX(strWhere, orderBy, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = new BLL_UserGroup_Member().GetRecordCount(strWhere);
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        Row = dt.Rows[i]["Row"],
                        User_Id = dt.Rows[i]["User_Id"],
                        UserGroup_Member_Id = dt.Rows[i]["UserGroup_Member_Id"],
                        UserName = dt.Rows[i]["UserName"],
                        TrueName = string.IsNullOrEmpty(dt.Rows[i]["TrueName"].ToString()) ? dt.Rows[i]["UserName"] : dt.Rows[i]["TrueName"],
                        User_ApplicationPassTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["User_ApplicationPassTime"].ToString()),
                        UserStatus = dt.Rows[i]["UserStatus"].ToString(),
                        Email = dt.Rows[i]["Email"].ToString(),
                        MembershipEnum = dt.Rows[i]["MembershipEnum"],
                        MembershipEnumName = (dt.Rows[i]["MembershipEnum"].ToString() == MembershipEnum.teacher.ToString()) ? dt.Rows[i]["SubjectName"].ToString() + "老师" : Rc.Common.EnumService.GetDescription<MembershipEnum>(dt.Rows[i]["MembershipEnum"].ToString()),
                        IsGroupOwner = bool_IsGroupOwner,
                        userGroupId = UserGroup_Id
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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }

        /// <summary>
        /// 点击状态
        /// </summary>
        /// <param name="strGradeTerm_ID"></param>
        /// <returns></returns>
        protected string GetStyle(string strGradeTerm_ID)
        {
            string strTemp = string.Empty;
            if (ugroupId.Trim() == strGradeTerm_ID.Trim())
            {
                strTemp = " active";
            }
            return strTemp;
        }

        /// <summary>
        /// 移除成员
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string DelClassMember(string UserGroup_Member_Id, string UserId, string UserGroup_Id)
        {
            try
            {
                BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                if (new BLL_Student_HomeWork().GetRecordCount("Student_Id='" + UserId + "' and HomeWork_Id in(select HomeWork_Id from HomeWork where UserGroup_Id='" + UserGroup_Id + "')") > 0)
                {
                    return "2";//成员已布置作业
                }
                else
                {

                    Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                    string userId = modelFUser.UserId;

                    Model_UserGroup_Member model = bll.GetModel(UserGroup_Member_Id);
                    model.UserStatus = 1;

                    Model_Msg modelMsg = new Model_Msg();
                    modelMsg.MsgId = Guid.NewGuid().ToString();
                    modelMsg.MsgEnum = MsgEnum.QuitClass.ToString();
                    modelMsg.MsgTypeEnum = MsgTypeEumn.Private.ToString();
                    modelMsg.ResourceDataId = model.UserGroup_Id;
                    modelMsg.MsgTitle = string.Format("您被退班{0}", model.UserGroup_Id);
                    modelMsg.MsgContent = string.Format("您被退班{0}", model.UserGroup_Id);
                    modelMsg.MsgStatus = MsgStatus.Unread.ToString();
                    modelMsg.MsgSender = userId;
                    modelMsg.MsgAccepter = model.User_ID;
                    modelMsg.CreateTime = DateTime.Now;
                    modelMsg.CreateUser = userId;

                    if (bll.TeacherRemoveStudent(model, modelMsg))
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        /// <summary>
        /// 恢复成员
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string RecoveryClassMember(string UserGroup_Member_Id)
        {
            try
            {

                Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                string userId = modelFUser.UserId;

                BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                Model_UserGroup_Member model = bll.GetModel(UserGroup_Member_Id);
                model.UserStatus = 0;

                Model_Msg modelMsg = new Model_Msg();
                modelMsg.MsgId = Guid.NewGuid().ToString();
                modelMsg.MsgEnum = MsgEnum.QuitClass.ToString();
                modelMsg.MsgTypeEnum = MsgTypeEumn.Private.ToString();
                modelMsg.ResourceDataId = model.UserGroup_Id;
                modelMsg.MsgTitle = string.Format("您被恢复到班{0}", model.UserGroup_Id);
                modelMsg.MsgContent = string.Format("您被恢复到班{0}", model.UserGroup_Id);
                modelMsg.MsgStatus = MsgStatus.Unread.ToString();
                modelMsg.MsgSender = userId;
                modelMsg.MsgAccepter = model.User_ID;
                modelMsg.CreateTime = DateTime.Now;
                modelMsg.CreateUser = userId;
                if (bll.TeacherRemoveStudent(model, modelMsg))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string resettingPwd(string UserId)
        {
            try
            {

                Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                string userId = modelFUser.UserId;

                Model_F_User user = new Model_F_User();
                BLL_F_User bll = new BLL_F_User();
                user = bll.GetModel(UserId.Filter());
                if (user != null)
                {
                    user.Password = Rc.Common.StrUtility.DESEncryptLogin.EncryptString("123456");
                }
                Model_Msg modelMsg = new Model_Msg();
                modelMsg.MsgId = Guid.NewGuid().ToString();
                modelMsg.MsgEnum = MsgEnum.Notice.ToString();
                modelMsg.MsgTypeEnum = MsgTypeEumn.Private.ToString();
                modelMsg.ResourceDataId = "";
                modelMsg.MsgTitle = string.Format("您的密码被【{0}】老师重置为123456", string.IsNullOrEmpty(modelFUser.TrueName) ? modelFUser.UserName : modelFUser.TrueName);
                modelMsg.MsgContent = string.Format("您的密码被【{0}】老师重置为123456", string.IsNullOrEmpty(modelFUser.TrueName) ? modelFUser.UserName : modelFUser.TrueName);
                modelMsg.MsgStatus = MsgStatus.Unread.ToString();
                modelMsg.MsgSender = userId;
                modelMsg.MsgAccepter = UserId;
                modelMsg.CreateTime = DateTime.Now;
                modelMsg.CreateUser = userId;
                if (bll.resettingPwd(user, modelMsg))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        /// <summary>
        /// 设为班主任
        /// </summary>
        /// <param name="UserGroupMemberId"></param>
        /// <param name="UserGroupId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string setHeaderMaster(string userGroupMemberId, string userGroupId, string userId)
        {
            string res = "0";
            try
            {
                Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                string sessionUserId = modelFUser.UserId;
                if (new BLL_UserGroup_Member().setHeaderMaster(sessionUserId, userGroupMemberId, userGroupId, userId) == true)
                {
                    res = "1";
                }
                return res;
            }
            catch (Exception)
            {
                return res;
            }
        }

    }
}