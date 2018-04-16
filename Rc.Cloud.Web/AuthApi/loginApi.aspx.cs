using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using Rc.Common.StrUtility;
using System.Data;

namespace Rc.Cloud.Web.AuthApi
{
    public partial class loginApi : System.Web.UI.Page
    {
        string key = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strToken = string.Empty;
            string strUserId = string.Empty;
            string strError = string.Empty;
            string strProductType = string.Empty;

            if (!String.IsNullOrEmpty(Request["token"]) && !String.IsNullOrEmpty(Request["userId"]) && !String.IsNullOrEmpty(Request["productType"]))
            {

                strToken = Request["token"].ToString().Filter();
                strUserId = Request["userId"].ToString().Filter();
                strProductType = Request["productType"].ToString().Filter();

                Model_F_User_Client model = new Model_F_User_Client();
                model = new BLL_F_User_Client().GetUserModelByClientToken(strUserId, strToken, strProductType);
                if (model != null)
                {
                    string iurl = string.Empty;
                    //用户信息
                    Model_F_User loginUser = new Model_F_User();
                    loginUser = new BLL_F_User().GetModel(strUserId);
                    if (loginUser != null)
                    {
                        #region 前台用户
                        Session["FLoginUser"] = loginUser;
                        Session["modlist"] = null;
                        string local_url = string.Empty; // 局域网地址
                        #region 学校配置URL
                        DataTable dtUrlLICH = new BLL_ConfigSchool().GetSchoolPublicUrl(loginUser.UserId).Tables[0];
                        if (dtUrlLICH.Rows.Count > 0)
                        {
                            Session["UserPublicUrl"] = dtUrlLICH.Rows[0]["publicUrl"];
                            local_url = dtUrlLICH.Rows[0]["apiUrlList"].ToString();
                        }
                        #endregion
                        if (loginUser.UserIdentity == "T")
                        {
                            iurl = "/teacher/Comment.aspx";
                        }
                        else
                        {
                            iurl = "/student/basicSetting.aspx";
                        }
                        string rurl = string.Format("/onlinecheck.aspx?iurl={0}&local_url={1}"
                        , HttpContext.Current.Server.UrlEncode(iurl)
                        , HttpContext.Current.Server.UrlEncode(local_url));
                        if (dtUrlLICH.Rows.Count > 0) // 有局域网配置数据，验证是否局域网
                        {
                            iurl = rurl;
                        }
                        Response.Redirect(iurl);
                        Response.End();
                        #endregion
                    }
                    else
                    {
                        #region 后台用户
                        Rc.Cloud.Model.Model_VSysUserRole loginModel = new Model.Model_VSysUserRole();
                        loginModel = new Rc.Cloud.BLL.BLL_VSysUserRole().GetSysUserInfoModelBySysUserId(strUserId);
                        if (loginModel != null)
                        {
                            DataTable dt = new Rc.Cloud.BLL.BLL_SysModule().GetOwenModuleListByCacheBySysCode(loginModel.SysUser_ID, Rc.Common.StrUtility.clsUtility.ReDoStr(loginModel.SysRole_IDs, ','));
                            DataRow[] drs = dt.Select("isLast='1' and url<>'#'", "DefaultOrder desc");
                            if (drs.Count() > 0)
                            {
                                Session["LoginUser"] = loginModel;
                                if (Request["iurl"] != null)
                                {
                                    Response.Redirect(Request["iurl"].ToString());
                                }
                                else
                                {
                                    Response.Redirect("/" + drs[0]["url"].ToString());
                                }

                            }
                            else
                            {
                                Rc.Common.StrUtility.clsUtility.ErrorDispose(this.Page, 6, false);
                            }
                        }
                        #endregion
                    }
                }
                else
                {

                    strError = "对不起，此账号已在其他机器登录。";

                }

            }
            else
            {
                strError = "参数无效";
            }
            Response.Write(strError);
            Response.End();
        }
    }
}