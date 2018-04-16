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
using Rc.Common.Config;

namespace Rc.Cloud.Web.teacher
{
    public partial class SchoolMember : Rc.Cloud.Web.Common.FInitData
    {
        string userId = string.Empty;
        protected string ugroupId = string.Empty;
        static bool bool_IsGroupOwner = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (FloginUser.UserPost != UserPost.校长.ToString())
            {
                //liCreateSchool.Visible = false;
            }
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
            DataTable dt = new BLL_UserGroup().GetList("UserGroup_AttrEnum='" + UserGroup_AttrEnum.School.ToString() + "' and User_Id='" + FloginUser.UserId + "' order by CreateTime desc").Tables[0];
            if (string.IsNullOrEmpty(ugroupId) && dt.Rows.Count > 0)
            {
                ugroupId = dt.Rows[0]["UserGroup_Id"].ToString();
            }
            rptClass.DataSource = dt;
            rptClass.DataBind();
        }

        [WebMethod]
        public static string GetMember(string UserGroup_Id, int PageSize, int PageIndex)
        {
            try
            {
                UserGroup_Id = UserGroup_Id.Filter();

                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSqlCount = string.Empty;
                string strWhere = string.Format("User_ApplicationStatus='passed' and UserGroup_Id='{0}'", UserGroup_Id);
                string orderBy = string.Format("charindex(MembershipEnum,'{0},{1},{2},{3},{4}'),UserStatus,UserGroupOrder,User_ApplicationPassTime desc"
                    , MembershipEnum.principal, MembershipEnum.vice_principal
                    , MembershipEnum.Dean, MembershipEnum.TeachingLeader, MembershipEnum.grade);
                dt = new BLL_UserGroup_Member().GetSchoolMemberListByPageEX(strWhere, orderBy, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = new BLL_UserGroup_Member().GetRecordCount(strWhere);
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        Row = dt.Rows[i]["Row"],
                        UserGroup_Id = dt.Rows[i]["User_Id"],
                        UserGroup_Name = dt.Rows[i]["UserGroup_Name"],
                        MembershipEnum = dt.Rows[i]["MembershipEnum"],
                        UserGroup_Member_Id = dt.Rows[i]["UserGroup_Member_Id"],
                        User_ApplicationPassTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["User_ApplicationPassTime"].ToString()),
                        UserStatus = dt.Rows[i]["UserStatus"].ToString(),
                        ClassCount = dt.Rows[i]["MembershipEnum"].ToString() == MembershipEnum.grade.ToString() ? dt.Rows[i]["ClassCount"].ToString() : "-",
                        TeacherCount = dt.Rows[i]["MembershipEnum"].ToString() == MembershipEnum.grade.ToString() ? dt.Rows[i]["TeacherCount"].ToString() : "-",
                        StudentCount = dt.Rows[i]["MembershipEnum"].ToString() == MembershipEnum.grade.ToString() ? dt.Rows[i]["StudentCount"].ToString() : "-",
                        IType = dt.Rows[i]["IType"],
                        PostName = dt.Rows[i]["PostName"],
                        IsGroupOwner = bool_IsGroupOwner,
                        UserName = dt.Rows[i]["UserName"]
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
        public static string DelClassMember(string UserGroup_Member_Id, string UserId)
        {
            try
            {

                Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                string userId = modelFUser.UserId;

                BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                Model_UserGroup_Member model = bll.GetModel(UserGroup_Member_Id);
                model.UserStatus = 1;

                Model_Msg modelMsg = new Model_Msg();
                modelMsg.MsgId = Guid.NewGuid().ToString();
                modelMsg.MsgEnum = MsgEnum.QuitSchool.ToString();
                modelMsg.MsgTypeEnum = MsgTypeEumn.Private.ToString();
                modelMsg.ResourceDataId = model.UserGroup_Id;
                modelMsg.MsgTitle = string.Format("您被移除学校{0}", model.UserGroup_Id);
                modelMsg.MsgContent = string.Format("您被移除学校{0}", model.UserGroup_Id);
                modelMsg.MsgStatus = MsgStatus.Unread.ToString();
                modelMsg.MsgSender = userId;
                modelMsg.MsgAccepter = model.CreateUser;
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
                modelMsg.MsgEnum = MsgEnum.QuitSchool.ToString();
                modelMsg.MsgTypeEnum = MsgTypeEumn.Private.ToString();
                modelMsg.ResourceDataId = model.UserGroup_Id;
                modelMsg.MsgTitle = string.Format("您被恢复到学校{0}", model.UserGroup_Id);
                modelMsg.MsgContent = string.Format("您被恢复到学校{0}", model.UserGroup_Id);
                modelMsg.MsgStatus = MsgStatus.Unread.ToString();
                modelMsg.MsgSender = userId;
                modelMsg.MsgAccepter = model.CreateUser;
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
    }
}