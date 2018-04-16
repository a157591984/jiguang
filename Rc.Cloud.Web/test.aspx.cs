using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Threading;
using System.Text;
using Rc.BLL;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using Rc.Common.StrUtility;

namespace Web
{
    public partial class test : System.Web.UI.Page
    {
        protected string backUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
             string iurl = string.Empty;
            //backUrl = Request.QueryString["iurl"];
            //ltlIndex.Text = new Rc.BLL.Resources.BLL_CustomizeHtml().GetHtmlContentByHtmlType(Rc.Model.Resources.CustomizeHtmlTypeEnum.index.ToString()).ToString();
            //HttpContext.Current.Session["StatsGradeSubject"] = null;
            //if (!IsPostBack)
            //{
            //    Rc.Common.StrUtility.CookieClass.RemoveCookie("UserPublicUrl_Cookie");
            //    if (Session["FLoginUser"] != null)
            //    {
            //        Model_F_User loginModel = (Model_F_User)Session["FLoginUser"];
            //        if (loginModel.UserIdentity == "T")
            //        {
            //            //是否带班
            //            int classCount = new BLL_UserGroup().GetRecordCount(" UserGroup_AttrEnum='Class' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where USER_ID='" + loginModel.UserId + "' and User_ApplicationStatus='passed' and UserStatus='0') ");
            //            if (classCount > 0 && (Rc.Cloud.Web.Common.pfunction.GetWebMdlIsShow("cTeachPlan")))
            //            {
            //                iurl = "/teacher/cTeachPlan.aspx";
            //            }
            //            else
            //            {
            //                iurl = "/teacher/basicSetting.aspx";
            //            }
            //        }
            //        else if (loginModel.UserIdentity == "S")
            //        {
            //            iurl = "/student/oHomework.aspx";
            //        }
            //        else if (loginModel.UserIdentity == "P")
            //        {
            //            iurl = "/parent/student.aspx";
            //        }
            //        if (!string.IsNullOrEmpty(backUrl))
            //        {
            //            iurl = backUrl;
            //        }
            //        string local_url = string.Empty; // 局域网地址
            //        #region 学校配置URL
            //        DataTable dtUrl = new BLL_ConfigSchool().GetSchoolPublicUrl(loginModel.UserId).Tables[0];
            //        if (dtUrl.Rows.Count > 0)
            //        {
            //            HttpContext.Current.Session["UserPublicUrl"] = dtUrl.Rows[0]["publicUrl"];
            //            local_url = dtUrl.Rows[0]["apiUrlList"].ToString();
            //        }
            //        #endregion
            //        string rurl = string.Format("/onlinecheck.aspx?iurl={0}&local_url={1}"
            //            , Server.UrlEncode(iurl)
            //            , Server.UrlEncode(local_url));
            //        if (dtUrl.Rows.Count == 0) // 没有局域网配置数据，直接登录
            //        {
            //            Response.Redirect(iurl, false);
            //        }
            //        else // 有局域网配置数据，验证是否局域网
            //        {
            //            Response.Redirect(rurl, false);
            //        }
            //    }
            //    noticeBoard();
            //}
        }

        [WebMethod]
        public static string loginIndex(string userName, string passWord, string backUrl)
        {
            string strJson = string.Empty;
            string iurl = string.Empty;
            try
            {
                HttpContext.Current.Session["UserPublicUrl"] = null;
                string loginName = string.Empty;
                string loginPassWord = string.Empty;
                loginName = userName.Trim();
                loginPassWord = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(passWord.Trim());
                Model_F_User loginModel = new Model_F_User();
                object objFUser = new BLL_F_User().GetModelByUserName(loginName);
                object objIFUser = new BLL_TPIFUser().GetModelByUserName(loginName);
                if (objFUser == null && objIFUser == null)
                {
                    HttpContext.Current.Session.Clear();
                    strJson = JsonConvert.SerializeObject(new
                    {
                        err = "该账号不存在，请重新登录。",
                        iurl = ""
                    });
                    return strJson;
                }

                #region 验证第三方接口用户
                if (objIFUser != null)
                {
                    Model_TPIFUser modelIFUser = objIFUser as Model_TPIFUser;
                    Model_TPSchoolIF modelSIF = new BLL_TPSchoolIF().GetModelBySchoolIF_Code(modelIFUser.School);
                    if (modelSIF == null)
                    {
                        HttpContext.Current.Session.Clear();
                        strJson = JsonConvert.SerializeObject(new
                        {
                            err = "学校接口配置不存在，请重新登录。",
                            iurl = ""
                        });
                        return strJson;
                    }
                    Model_ConfigSchool modelCS = new BLL_ConfigSchool().GetModelBySchoolIdNew(modelSIF.SchoolId);
                    if (modelCS == null)
                    {
                        HttpContext.Current.Session.Clear();
                        strJson = JsonConvert.SerializeObject(new
                        {
                            err = "学校配置不存在，请重新登录。",
                            iurl = ""
                        });
                        return strJson;
                    }

                    if (modelSIF.SchoolIF_Code == ThirdPartyEnum.ahjzvs.ToString())
                    {
                        #region 安徽金寨职业学校
                        loginModel = new BLL_F_User().GetModel(modelIFUser.UserName, loginPassWord);
                        if (loginModel == null)
                        {
                            Model_F_User loginModelNew = (Model_F_User)objFUser;
                            if (loginModelNew.Password == Rc.Common.StrUtility.DESEncryptLogin.EncryptString(Rc.Common.Config.Consts.DefaultPassword))
                            {
                                HttpContext.Current.Session.Clear();
                                strJson = JsonConvert.SerializeObject(new
                                {
                                    err = "当前密码123456，过于简单，建议在个人中心中修改。",
                                    iurl = ""
                                });
                                return strJson;
                            }
                            HttpContext.Current.Session.Clear();
                            strJson = JsonConvert.SerializeObject(new
                            {
                                err = "用户密码错误，请重新登录。",
                                iurl = ""
                            });
                            return strJson;
                        }
                        
                        HttpContext.Current.Session["FLoginUser"] = loginModel;

                        if (loginModel.UserIdentity == "T")
                        {
                            //是否带班
                            int classCount = new BLL_UserGroup().GetRecordCount(" UserGroup_AttrEnum='Class' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where USER_ID='" + loginModel.UserId + "' and User_ApplicationStatus='passed' and UserStatus='0') ");
                            if (classCount > 0 && (Rc.Cloud.Web.Common.pfunction.GetWebMdlIsShow("cTeachPlan")))
                            {
                                iurl = "/teacher/cTeachPlan.aspx" ;
                            }
                            else
                            {
                                iurl = "/teacher/basicSetting.aspx";
                            }
                        }
                        else if (loginModel.UserIdentity == "S")
                        {
                            iurl = "/student/oHomework.aspx" ;
                        }
                        else if (loginModel.UserIdentity == "P")
                        {
                            iurl = "/parent/student.aspx" ;
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
                            iurl = string.Format("/RE_Register/PerfectInformation.aspx?SchoolId={0}&UserId={1}"
                                , modelCS.School_ID, loginModel.UserId);
                        }
                        strJson = JsonConvert.SerializeObject(new
                        {
                            err = "null",
                            iurl = iurl
                        });
                        return strJson;
                        #endregion
                    }
                    else
                    {
                        #region 其他学校
                        if (Rc.Interface.AuthAPI_pfunction.AuthUserLoginByIF(modelCS, modelIFUser.School, userName, passWord))
                        {
                            #region 登录
                            loginModel = new BLL_F_User().GetModelByUserName(modelIFUser.School + modelIFUser.UserName);
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
                                iurl = string.Format("/RE_Register/PerfectInformation.aspx?SchoolId={0}&UserId={1}", modelCS.School_ID, loginModel.UserId);
                            }
                            strJson = JsonConvert.SerializeObject(new
                            {
                                err = "null",
                                iurl = iurl
                            });
                            return strJson;
                            #endregion
                        }
                        else
                        {
                            HttpContext.Current.Session.Clear();
                            strJson = JsonConvert.SerializeObject(new
                            {
                                err = "接口用户密码错误，请重新登录。",
                                iurl = ""
                            });
                            return strJson;
                        }
                        #endregion
                    }

                }
                #endregion

                #region 验证作业平台用户
                if (objFUser != null)
                {
                    objFUser = new BLL_F_User().GetModel(loginName, loginPassWord);
                    if (objFUser != null)
                    {
                        #region 登录
                        loginModel = objFUser as Model_F_User;
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

                        if (!string.IsNullOrEmpty(backUrl))
                        {
                            iurl = backUrl;
                        }
                        #endregion
                        if (string.IsNullOrEmpty(loginModel.TrueName))
                        {
                            loginModel.TrueName = loginModel.UserName;
                            new BLL_F_User().Update(loginModel);
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
                        strJson = JsonConvert.SerializeObject(new
                        {
                            err = "null",
                            iurl = iurl
                        });
                    }
                    else
                    {
                        HttpContext.Current.Session.Clear();
                        strJson = JsonConvert.SerializeObject(new
                        {
                            err = "用户密码错误，请重新登录。",
                            iurl = ""
                        });
                    }
                }
                else
                {
                    HttpContext.Current.Session.Clear();
                    strJson = JsonConvert.SerializeObject(new
                    {
                        err = "该账号不存在，请重新登录。。",
                        iurl = ""
                    });
                }
                #endregion

            }
            catch (Exception ex)
            {
                strJson = JsonConvert.SerializeObject(new
                {
                    err = "null",
                    iurl = "/ErrorPageF.aspx?errorType=6"
                });
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", "用户登录", ex.Message.ToString());
            }

            return strJson;
        }

        /// <summary>
        /// 公告
        /// </summary>
        private void noticeBoard()
        {
            try
            {
                BLL_NoticeBoard bll = new BLL_NoticeBoard();
                DataTable dt = new DataTable();
                dt = bll.GetList(1, "", " create_time desc").Tables[0];
                foreach (DataRow item in dt.Rows)
                {
                    if (Convert.ToDateTime(item["start_time"]) <= DateTime.Now && Convert.ToDateTime(item["end_time"]) > DateTime.Now)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>layer.open({type: 2,title: false,closeBtn: false,shade: false,area:['600px','80%'],content: 'noticeBoard.aspx'});</script>");
                    }
                }
            }
            catch (Exception)
            {

            }
        }


    }
}