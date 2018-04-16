using Rc.BLL.Resources;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Rc.Cloud.Web.AuthApi
{
    /// <summary>
    /// 处理第三方用户信息 18-01-03TS
    /// </summary>
    public class HandelThirdPartyUserInfo
    {
        /// <summary>
        /// 处理用户信息，返回用户登录后网页地址 18-01-03TS
        /// </summary>
        public static string HandelUserInfo(string schoolCode, string loginName, string trueName)
        {
            string iurl = string.Empty;
            string schoolId = string.Empty;

            List<Model_TPIFUser> listIFUser = new BLL_TPIFUser().GetModelList("UserName='" + loginName + "'");
            Model_TPIFUser modelIFUser = new BLL_TPIFUser().GetModelBySchoolUserName(schoolCode, loginName);
            if (schoolCode == ThirdPartyEnum.ahjzvs.ToString())
            {
                #region 安徽金寨职业学校
                if (modelIFUser != null)//用户已登录过
                {
                    #region 登录
                    Model_F_User loginModel = new BLL_F_User().GetModelByUserName(modelIFUser.UserName);
                    HttpContext.Current.Session["FLoginUser"] = loginModel;

                    if (loginModel.UserIdentity == "T")
                    {
                        //是否带班
                        int classCount = new BLL_UserGroup().GetRecordCount(" UserGroup_AttrEnum='Class' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where USER_ID='" + loginModel.UserId + "' and User_ApplicationStatus='passed' and UserStatus='0') ");
                        if (classCount > 0 && (Rc.Cloud.Web.Common.pfunction.GetWebMdlIsShow("cTeachPlan")))
                        {
                            iurl = "/teacher/cTeachPlan.aspx";
                        }
                        else
                        {
                            iurl = "/teacher/basicSetting.aspx";
                        }
                    }
                    else if (loginModel.UserIdentity == "S")
                    {
                        iurl = "/student/oHomework.aspx";
                    }
                    else if (loginModel.UserIdentity == "P")
                    {
                        iurl = "/parent/student.aspx";
                    }

                    string local_url = string.Empty; // 局域网地址
                    #region 学校配置URL
                    DataTable dtUrl = new BLL_ConfigSchool().GetSchoolPublicUrl(loginModel.UserId).Tables[0];
                    if (dtUrl.Rows.Count > 0)
                    {
                        HttpContext.Current.Session["UserPublicUrl"] = dtUrl.Rows[0]["publicUrl"];
                        local_url = dtUrl.Rows[0]["apiUrlList"].ToString();
                    }
                    #endregion
                    string rurl = string.Format("/onlinecheck.aspx?iurl={0}&local_url={1}"
                    , HttpContext.Current.Server.UrlEncode(iurl)
                    , HttpContext.Current.Server.UrlEncode(local_url));
                    if (dtUrl.Rows.Count > 0) // 有局域网配置数据，验证是否局域网
                    {
                        iurl = rurl;
                    }
                    if (string.IsNullOrEmpty(iurl))
                    {
                        Model_TPSchoolIF modelSIF = new BLL_TPSchoolIF().GetModelBySchoolIF_Code(schoolCode);
                        if (modelSIF != null)
                        {
                            schoolId = modelSIF.SchoolId;
                        }
                        iurl = string.Format("/RE_Register/PerfectInformation.aspx?SchoolId={0}&UserId={1}", schoolId, loginModel.UserId);
                    }
                    #endregion
                }
                else
                {
                    #region 第一次登录，写入用户数据
                    Model_TPSchoolIF modelSIF = new BLL_TPSchoolIF().GetModelBySchoolIF_Code(schoolCode);
                    if (modelSIF != null)
                    {
                        schoolId = modelSIF.SchoolId;
                    }
                    string userId = Guid.NewGuid().ToString();
                    #region TPIFUser
                    modelIFUser = new Model_TPIFUser();
                    modelIFUser.ThirdPartyIFUser_Id = Guid.NewGuid().ToString();
                    modelIFUser.School = schoolCode;
                    modelIFUser.UserName = loginName;
                    modelIFUser.CreateTime = DateTime.Now;
                    new BLL_TPIFUser().Add(modelIFUser);
                    #endregion
                    #region F_User
                    Model_F_User modelFUser = new Model_F_User();
                    modelFUser.UserId = userId;
                    modelFUser.UserName = loginName;
                    modelFUser.TrueName = trueName;
                    modelFUser.Password = DESEncryptLogin.EncryptString(Rc.Common.Config.Consts.DefaultPassword);
                    modelFUser.CreateTime = DateTime.Now;
                    new BLL_F_User().Add(modelFUser);
                    #endregion
                    iurl = string.Format("/RE_Register/PerfectInformation.aspx?userId={0}&schoolId={1}", userId, schoolId);
                    #endregion
                }
                #endregion
            }
            else
            {
                #region 其他学校
                if (modelIFUser != null)//用户已登录过
                {
                    #region 登录
                    Model_F_User loginModel = new BLL_F_User().GetModelByUserName(modelIFUser.School + modelIFUser.UserName);
                    HttpContext.Current.Session["FLoginUser"] = loginModel;

                    if (loginModel.UserIdentity == "T")
                    {
                        //是否带班
                        int classCount = new BLL_UserGroup().GetRecordCount(" UserGroup_AttrEnum='Class' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where USER_ID='" + loginModel.UserId + "' and User_ApplicationStatus='passed' and UserStatus='0') ");
                        if (classCount > 0 && (Rc.Cloud.Web.Common.pfunction.GetWebMdlIsShow("cTeachPlan")))
                        {
                            iurl = "/teacher/cTeachPlan.aspx";
                        }
                        else
                        {
                            iurl = "/teacher/basicSetting.aspx";
                        }
                    }
                    else if (loginModel.UserIdentity == "S")
                    {
                        iurl = "/student/oHomework.aspx";
                    }
                    else if (loginModel.UserIdentity == "P")
                    {
                        iurl = "/parent/student.aspx";
                    }

                    string local_url = string.Empty; // 局域网地址
                    #region 学校配置URL
                    DataTable dtUrl = new BLL_ConfigSchool().GetSchoolPublicUrl(loginModel.UserId).Tables[0];
                    if (dtUrl.Rows.Count > 0)
                    {
                        HttpContext.Current.Session["UserPublicUrl"] = dtUrl.Rows[0]["publicUrl"];
                        local_url = dtUrl.Rows[0]["apiUrlList"].ToString();
                    }
                    #endregion
                    string rurl = string.Format("/onlinecheck.aspx?iurl={0}&local_url={1}"
                    , HttpContext.Current.Server.UrlEncode(iurl)
                    , HttpContext.Current.Server.UrlEncode(local_url));
                    if (dtUrl.Rows.Count > 0) // 有局域网配置数据，验证是否局域网
                    {
                        iurl = rurl;
                    }
                    if (string.IsNullOrEmpty(iurl))
                    {
                        Model_TPSchoolIF modelSIF = new BLL_TPSchoolIF().GetModelBySchoolIF_Code(schoolCode);
                        if (modelSIF != null)
                        {
                            schoolId = modelSIF.SchoolId;
                        }
                        iurl = string.Format("/RE_Register/PerfectInformation.aspx?SchoolId={0}&UserId={1}", schoolId, loginModel.UserId);
                    }
                    #endregion
                }
                else
                {
                    #region 第一次登录，写入用户数据
                    Model_TPSchoolIF modelSIF = new BLL_TPSchoolIF().GetModelBySchoolIF_Code(schoolCode);
                    if (modelSIF != null)
                    {
                        schoolId = modelSIF.SchoolId;
                    }
                    string userId = Guid.NewGuid().ToString();
                    #region TPIFUser
                    modelIFUser = new Model_TPIFUser();
                    modelIFUser.ThirdPartyIFUser_Id = Guid.NewGuid().ToString();
                    modelIFUser.School = schoolCode;
                    modelIFUser.UserName = loginName;
                    modelIFUser.CreateTime = DateTime.Now;
                    new BLL_TPIFUser().Add(modelIFUser);
                    #endregion
                    #region F_User
                    Model_F_User modelFUser = new Model_F_User();
                    modelFUser.UserId = userId;
                    modelFUser.UserName = schoolCode + loginName;
                    modelFUser.TrueName = trueName;
                    modelFUser.CreateTime = DateTime.Now;
                    new BLL_F_User().Add(modelFUser);
                    #endregion
                    iurl = "/RE_Register/PerfectInformation.aspx?userId=" + userId + "&schoolId=" + schoolId;
                    #endregion
                }
                #endregion
            }
            return iurl;
        }
    }
}