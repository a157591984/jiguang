using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Data;
using System.Web.Services;
using System.Text;
using Newtonsoft.Json;
namespace Rc.Cloud.Web.RE_Register
{
    public partial class PerfectInformation : System.Web.UI.Page
    {
        public string SchoolId = string.Empty;
        public string UserId = string.Empty;
        Model_F_User user = new Model_F_User();
        BLL_F_User bll_user = new BLL_F_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            SchoolId = Request["SchoolId"].Filter();
            UserId = Request["UserId"].Filter();
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    Response.Write("参数错误！");
                }
                else
                {
                    user = bll_user.GetModel(UserId);
                    DataTable dt = new DataTable();
                    string strWhere = string.Empty;
                    //学科
                    strWhere = " D_Type='7' order by d_order";
                    dt = new BLL_Common_Dict().GetList(strWhere).Tables[0];
                    Rc.Cloud.Web.Common.pfunction.SetDdlEmpty(ddlSubject, dt, "D_Name", "Common_Dict_ID", "--请选择--");
                    //身份
                    strWhere = " D_Type='15' order by d_order ";
                    Rc.Cloud.Web.Common.pfunction.SetDdlEmpty(ddlUserPost, new BLL_Common_Dict().GetList(strWhere).Tables[0], "D_Name", "Common_Dict_ID", "--请选择--");
                }
            }
        }
        /// <summary>
        /// 加载年级
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string LaodGrade(string SchoolId)
        {
            StringBuilder stbHtml = new StringBuilder();
            try
            {

                stbHtml.Append("<option value=\"-1\">--请选择--</option>");
                string temp = "<option value=\"{0}\">{1}</option>";
                SchoolId = SchoolId.Filter();
                if (string.IsNullOrEmpty(SchoolId))
                {
                    return stbHtml.ToString();
                }
                else
                {
                    string sql = @" select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool] where SchoolId='" + SchoolId + "' and gradeid<>''";
                    DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            stbHtml.AppendFormat(temp, item["GradeId"], item["GradeName"]);
                        }
                        return stbHtml.ToString();
                    }
                    else
                    {
                        return stbHtml.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                return stbHtml.ToString();
            }
        }
        /// <summary>
        /// 加载班级
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string LaodClass(string GradeId)
        {
            StringBuilder stbHtml = new StringBuilder();
            try
            {

                stbHtml.Append("<option value=\"-1\">--请选择--</option>");
                string temp = "<option value=\"{0}\">{1}</option>";
                GradeId = GradeId.Filter();
                if (string.IsNullOrEmpty(GradeId))
                {
                    return stbHtml.ToString();
                }
                else
                {
                    string sql = @" select  distinct ClassId,ClassName from [dbo].[VW_UserOnClassGradeSchool] where GradeId='" + GradeId + "' and ClassId<>''";
                    DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            stbHtml.AppendFormat(temp, item["ClassId"], item["ClassName"]);
                        }
                        return stbHtml.ToString();
                    }
                    else
                    {
                        return stbHtml.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                return stbHtml.ToString();
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserPost"></param>
        /// <returns></returns>
        [WebMethod]
        public static string loginIndex(string UserId, string UserPost, string Subject, string ClassId, string type)
        {
            string strJson = string.Empty;
            string iurl = string.Empty;
            try
            {
                HttpContext.Current.Session["UserPublicUrl"] = null;
                UserId = UserId.Filter();
                UserPost = UserPost.Filter();
                Subject = Subject.Filter();
                ClassId = ClassId.Filter();
                type = type.Filter();
                Model_F_User model = new BLL_F_User().GetModel(UserId);
                bool flag = true;
                if (model != null)
                {
                    model.UserIdentity = type;
                    if (!string.IsNullOrEmpty(UserPost) && UserPost != "-1")
                    {
                        model.UserPost = UserPost;
                    }

                    if (!string.IsNullOrEmpty(Subject) && Subject != "-1")
                    {
                        model.Subject = Subject;
                    }
                    flag = new BLL_F_User().Update(model);
                    if (flag == false)
                    {
                        strJson = JsonConvert.SerializeObject(new
                        {
                            err = "操作失败请重试",
                            iurl = ""
                        });
                        return strJson;
                    }
                    if (type == "T")//年级主任，年级组长
                    {
                        if (!string.IsNullOrEmpty(ClassId) && ClassId != "-1" && !string.IsNullOrEmpty(Subject) && Subject != "-1")//加入年级
                        {
                            #region 加入班级
                            BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                            List<Model_UserGroup_Member> listModelUGM = bll.GetModelList(string.Format("User_ID='{0}' and UserGroup_Id='{1}'", UserId, ClassId));
                            bool isExistDataClass = false;//是否已存在成员数据
                            if (listModelUGM.Count != 0)
                            {
                                isExistDataClass = true;
                            }
                            Model_UserGroup_Member UserGroup_Member_Class = new Model_UserGroup_Member();
                            UserGroup_Member_Class.UserGroup_Member_Id = Guid.NewGuid().ToString();
                            UserGroup_Member_Class.UserGroup_Id = ClassId;
                            UserGroup_Member_Class.User_ID = UserId;
                            UserGroup_Member_Class.User_ApplicationStatus = "passed";
                            UserGroup_Member_Class.UserStatus = 0;
                            UserGroup_Member_Class.User_ApplicationTime = DateTime.Now;
                            UserGroup_Member_Class.User_ApplicationReason = "完善信息-加入班级";
                            UserGroup_Member_Class.MembershipEnum = MembershipEnum.teacher.ToString();
                            UserGroup_Member_Class.CreateUser = UserId;
                            UserGroup_Member_Class.User_ApplicationPassTime = DateTime.Now;
                            if (isExistDataClass == false)
                            {
                                flag = bll.Add(UserGroup_Member_Class);
                                if (flag == false)
                                {
                                    strJson = JsonConvert.SerializeObject(new
                                    {
                                        err = "操作失败请重试",
                                        iurl = ""
                                    });
                                    return strJson;
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ClassId) && ClassId != "-1")
                        {
                            #region 加入班级
                            BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                            List<Model_UserGroup_Member> listModelUGM = bll.GetModelList(string.Format("User_ID='{0}' and UserGroup_Id='{1}'", UserId, ClassId));
                            bool isExistDataClass = false;//是否已存在成员数据
                            if (listModelUGM.Count != 0)
                            {
                                isExistDataClass = true;
                            }
                            Model_UserGroup_Member UserGroup_Member_Class = new Model_UserGroup_Member();
                            UserGroup_Member_Class.UserGroup_Member_Id = Guid.NewGuid().ToString();
                            UserGroup_Member_Class.UserGroup_Id = ClassId;
                            UserGroup_Member_Class.User_ID = UserId;
                            UserGroup_Member_Class.User_ApplicationStatus = "passed";
                            UserGroup_Member_Class.UserStatus = 0;
                            UserGroup_Member_Class.User_ApplicationTime = DateTime.Now;
                            UserGroup_Member_Class.User_ApplicationReason = "完善信息-加入班级";
                            UserGroup_Member_Class.MembershipEnum = MembershipEnum.student.ToString();
                            UserGroup_Member_Class.CreateUser = UserId;
                            UserGroup_Member_Class.User_ApplicationPassTime = DateTime.Now;
                            if (isExistDataClass == false)
                            {
                                flag = bll.Add(UserGroup_Member_Class);
                                if (flag == false)
                                {
                                    strJson = JsonConvert.SerializeObject(new
                                    {
                                        err = "操作失败请重试",
                                        iurl = ""
                                    });
                                    return strJson;
                                }
                            }
                            #endregion
                        }
                    }

                    #region 登录
                    HttpContext.Current.Session["FLoginUser"] = model;
                    if (type == "T")
                    {
                        //是否带班
                        int classCount = new BLL_UserGroup().GetRecordCount(" UserGroup_AttrEnum='Class' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where USER_ID='" + UserId + "' and User_ApplicationStatus='passed' and UserStatus='0') ");
                        if (classCount > 0 && (Rc.Cloud.Web.Common.pfunction.GetWebMdlIsShow("cTeachPlan")))
                        {
                            iurl = "/teacher/cTeachPlan.aspx";
                        }
                        else
                        {
                            iurl = "/teacher/basicSetting.aspx";
                        }
                    }
                    else if (type == "S")
                    {
                        iurl = "/student/oHomework.aspx";
                    }
                    #endregion
                    string local_url = string.Empty; // 局域网地址
                    #region 学校配置URL
                    DataTable dtUrl = new BLL_ConfigSchool().GetSchoolPublicUrl(UserId).Tables[0];
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
            catch (Exception)
            {
                strJson = JsonConvert.SerializeObject(new
                {
                    err = "null",
                    iurl = "/ErrorPageF.aspx?errorType=6"
                });
            }

            return strJson;
        }
    }
}