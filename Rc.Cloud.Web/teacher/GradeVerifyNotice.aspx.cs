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
    public partial class GradeVerifyNotice : Rc.Cloud.Web.Common.FInitData
    {
        protected string ugroupId = string.Empty;
        protected bool bool_IsGroupOwner = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (FloginUser.UserPost == UserPost.备课组长.ToString() || FloginUser.UserPost == UserPost.普通老师.ToString())
            {
                //liCreateGrade.Visible = false;
            }
            ugroupId = Request.QueryString["ugroupId"].Filter();
            bool_IsGroupOwner = pfunction.CheckUserIsGroupOwner(ugroupId, FloginUser.UserId);
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            DataTable dt = new BLL_UserGroup().GetPassGroupByUserIdUserGroupAttrEnum(FloginUser.UserId, UserGroup_AttrEnum.Grade.ToString()).Tables[0];
            if (string.IsNullOrEmpty(ugroupId) && dt.Rows.Count > 0)
            {
                ugroupId = dt.Rows[0]["UserGroup_Id"].ToString();
            }
            rptClass.DataSource = dt;
            rptClass.DataBind();
        }

        [WebMethod]
        public static string GetClassNotice(string UserGroup_Id, int PageIndex, int PageSize)
        {
            try
            {
                UserGroup_Id = UserGroup_Id.Filter();
                Model_F_User FloginUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                bool bool_IsGroupOwner = pfunction.CheckUserIsGroupOwner(UserGroup_Id, FloginUser.UserId);
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSqlCount = string.Empty;
                string strWhere = string.Format("User_ApplicationStatus='applied' and UserGroup_Id='{0}'", UserGroup_Id);
                dt = new BLL_UserGroup_Member().GetGradeMemberListByPageEX(strWhere, "User_ApplicationTime desc", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = new BLL_UserGroup_Member().GetRecordCount(strWhere);
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        UserGroup_Member_Id = dt.Rows[i]["UserGroup_Member_Id"],
                        UserGroup_Id = dt.Rows[i]["User_Id"].ToString(),
                        UserGroup_Name = dt.Rows[i]["UserGroup_Name"].ToString(),
                        User_ApplicationReason = dt.Rows[i]["User_ApplicationReason"],
                        User_ApplicationTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["User_ApplicationTime"].ToString()),
                        User_ApplicationStatus = dt.Rows[i]["User_ApplicationStatus"],
                        IType = dt.Rows[i]["IType"],
                        PostName = dt.Rows[i]["PostName"],
                        IsGroupOwner = bool_IsGroupOwner
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
        /// 审核申请数据
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string AuditApplyData(string UserGroup_Member_Id, string AType)
        {
            try
            {
                Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                string userId = modelFUser.UserId;

                bool flag = false;
                BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                string[] strArrMember = UserGroup_Member_Id.Split(',');
                List<Model_UserGroup_Member> listMember = new List<Model_UserGroup_Member>();
                List<Model_Msg> listMsg = new List<Model_Msg>();
                for (int i = 0; i < strArrMember.Length; i++)
                {
                    string strTips = "被拒绝";
                    Model_UserGroup_Member model = bll.GetModel(strArrMember[i]);
                    if (AType == "1")//通过
                    {
                        strTips = "已通过";
                        #region 成员表
                        model.User_ApplicationStatus = "passed";
                        model.User_ApplicationPassTime = DateTime.Now;
                        model.UserStatus = 0;
                        listMember.Add(model);
                        #endregion
                    }
                    #region 消息表
                    Model_Msg modelMsg = new Model_Msg();
                    modelMsg.MsgId = Guid.NewGuid().ToString();
                    modelMsg.MsgEnum = MsgEnum.ApplyToClass.ToString();
                    modelMsg.MsgTypeEnum = MsgTypeEumn.Private.ToString();
                    modelMsg.ResourceDataId = model.UserGroup_Id;
                    modelMsg.MsgTitle = string.Format("申请加入年级{0}{1}", model.UserGroup_Id, strTips);
                    modelMsg.MsgContent = string.Format("申请加入年级{0}{1}", model.UserGroup_Id, strTips);
                    modelMsg.MsgStatus = MsgStatus.Unread.ToString();
                    modelMsg.MsgSender = userId;
                    modelMsg.MsgAccepter = model.User_ID;
                    if (model.MembershipEnum == MembershipEnum.classrc.ToString())
                    {
                        modelMsg.MsgAccepter = model.CreateUser;
                    }
                    modelMsg.CreateTime = DateTime.Now;
                    modelMsg.CreateUser = userId;
                    listMsg.Add(modelMsg);
                    #endregion
                }

                if (AType == "1")
                {

                    flag = bll.AgreeMemberJoinGroup(listMember, listMsg);
                }
                else
                {
                    UserGroup_Member_Id = "'" + UserGroup_Member_Id.Replace(",", "','") + "'";
                    flag = bll.RefuseMemberJoinGroup(UserGroup_Member_Id, listMsg);
                }
                if (flag)
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