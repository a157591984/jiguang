using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Common.StrUtility;
using Rc.Cloud.Web.Common;
using System.IO;
using Newtonsoft.Json;
using Rc.Common;
using Rc.Interface;
using Rc.Common.Config;
using System.Threading;
using System.Text.RegularExpressions;

namespace Rc.Cloud.Web.AuthApi.aaa
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder strSql = new StringBuilder();
            string strAction = string.Empty;
            string strJsion = string.Empty;
            string productType = string.Empty;//产品类型（scienceword/class/skill）
            string strResource_Type = string.Empty;//产品类型 对应的ID
            string uploadPath = Server.MapPath("..\\Upload\\Resource\\");//存储文件基础路径
            string strTestWebSiteUrl = string.Empty;



            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
            if (!string.IsNullOrEmpty(Request["key"]))
            {
                strAction = Request["key"].ToString();
            }
            if (!string.IsNullOrEmpty(Request["productType"]))
            {
                productType = Request.QueryString["productType"].Filter().ToLower();
            }
            if (string.IsNullOrEmpty(Request["key"]))
            {
                Response.Write("ok");
            }

            switch (strAction.ToLower())
            {
                #region 验证局域网是否通畅
                case "onlinecheck":
                    Response.Write("ok");
                    break;
                #endregion
                #region 接口名称: GetAPIVer 获取Web接口的版本号
                case "getapiver":
                    try
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            apiVersion = "2",
                            status = true,
                            errorMsg = "",
                            errorCode = ""

                        });
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "GetAPIVer"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: Login 用户登录
                case "login":
                    try
                    {
                        string username = Request.QueryString["username"].Filter();
                        string password = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(Request.QueryString["password"].Filter());

                        #region 验证客户端版本号
                        string ver = Request["ver"].Filter();
                        if (!string.IsNullOrEmpty(ver)) // ver不为空，则验证客户端版本号
                        {
                            decimal d_ver = Convert.ToDecimal(ver);
                            if (d_ver < ConfigHelper.GetConfigDecimal("clientVer"))
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "当前客户端版本太低，用户登录失败",
                                    errorCode = "Login"
                                });
                                return;
                            }
                        }
                        #endregion
                        Model_F_User_Client model = new Model_F_User_Client();
                        BLL_F_User_Client bll = new BLL_F_User_Client();
                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(username, "", string.Format("开始登录|操作人{0}", username));
                        model = userLoginBackModel(username, password);
                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(username, "", "完成登录");
                        if (model != null)
                        {
                            #region TAB标签

                            List<object> tabList = new List<object>();
                            if (model.UserIdentity == "A")
                            {
                                #region 后台管理员
                                switch (productType)
                                {
                                    case "scienceword":
                                        object TAB0 = new
                                        {
                                            Title = "云教案",
                                            UIType = "Tree",
                                            ReadOnly = false,
                                            isShowForderAttr = true,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = true, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            // IconURL = pfunction.getHostPath() + "/Images/icon/yja.png",
                                            apiUrlList = new string[] { ConfigHelper.GetConfigString("TeachingPlanWebSiteUrl") + "AuthApi/" },
                                            TabID = EnumTabindex.MgrScienceWordCloudTeachingPlan.ToString()
                                        };
                                        object TAB1 = new
                                        {
                                            Title = "云习题集",
                                            UIType = "Tree",
                                            ReadOnly = false,
                                            isShowForderAttr = true,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = false, 	//是否可上传资源 true 可以上传
                                            isUploadTest = true, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/yxtj.png",
                                            apiUrlList = new string[] { ConfigHelper.GetConfigString("TestWebSiteUrl") + "AuthApi/" },
                                            TabID = EnumTabindex.MgrScienceWordCloudSkill.ToString()
                                        };
                                        tabList.Add(TAB0);
                                        tabList.Add(TAB1);
                                        break;
                                    case "class":
                                        TAB0 = new
                                        {
                                            Title = "云教案",
                                            UIType = "Tree",
                                            ReadOnly = false,
                                            isShowForderAttr = true,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = true, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/yja.png",
                                            apiUrlList = new string[] { ConfigHelper.GetConfigString("TeachingPlanWebSiteUrl") + "AuthApi/" },
                                            TabID = EnumTabindex.MgrClassCloudTeachingPlan.ToString()
                                        };
                                        TAB1 = new
                                        {
                                            Title = "微课件",
                                            UIType = "Tree",
                                            ReadOnly = false,
                                            isShowForderAttr = true,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = true, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/yja.png",
                                            apiUrlList = new string[] { ConfigHelper.GetConfigString("TeachingPlanWebSiteUrl") + "AuthApi/" },
                                            TabID = EnumTabindex.MgrClassCloudMicroClass.ToString()
                                        };
                                        tabList.Add(TAB0);
                                        tabList.Add(TAB1);
                                        break;
                                }
                                #endregion
                            }
                            else if (model.UserIdentity == "T")
                            {
                                List<string> listAll = new List<string>();
                                List<string> listSchool = new List<string>();
                                #region 学校配置URL
                                string strSqlSchool = string.Format(@"select distinct t.D_Value as apiUrlList,t.D_PublicValue as publicUrl from ConfigSchool t
inner join VW_UserOnClassGradeSchool t2 on  t2.UserId='{0}' and t2.SchoolId=t.School_ID;", model.UserId);
                                DataTable dtUrl = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlSchool).Tables[0];
                                foreach (DataRow item in dtUrl.Rows)
                                {
                                    if (!string.IsNullOrEmpty(item["apiUrlList"].ToString()))
                                    {
                                        listSchool.Add(item["apiUrlList"].ToString() + "AuthApi/");
                                    }
                                    if (!string.IsNullOrEmpty(item["publicUrl"].ToString()))
                                    {
                                        listSchool.Add(item["publicUrl"].ToString() + "AuthApi/");
                                    }
                                }
                                #endregion
                                #region 老师
                                switch (productType)
                                {
                                    case "scienceword":
                                        listAll = new List<string>(listSchool.ToArray());
                                        listAll.Add(ConfigHelper.GetConfigString("TeachingPlanWebSiteUrl") + "AuthApi/");
                                        object TAB0 = new
                                        {
                                            Title = "云教案",
                                            UIType = "Tree",
                                            ReadOnly = true,
                                            isShowForderAttr = false,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = false, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/yja.png",
                                            apiUrlList = listAll,
                                            TabID = EnumTabindex.TeacherScienceWordCloudTeachingPlan.ToString()
                                        };
                                        object TAB1 = new
                                        {
                                            Title = "自有教案",
                                            UIType = "Tree",
                                            ReadOnly = false,
                                            isShowForderAttr = false,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = true, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/zyja.png",
                                            apiUrlList = listSchool,
                                            TabID = EnumTabindex.TeacherScienceWordOwnTeachingPlan.ToString()
                                        };
                                        listAll = new List<string>(listSchool.ToArray());
                                        listAll.Add(ConfigHelper.GetConfigString("TestWebSiteUrl") + "AuthApi/");
                                        object TAB2 = new
                                        {
                                            Title = "云作业",
                                            UIType = "Tree",
                                            ReadOnly = true,
                                            isShowForderAttr = false,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = false, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/yja.png",
                                            apiUrlList = listAll,
                                            TabID = EnumTabindex.TeacherScienceWordCloudSkill.ToString()
                                        };
                                        object TAB3 = new
                                        {
                                            Title = "自有作业",
                                            UIType = "Tree",
                                            ReadOnly = false,
                                            isShowForderAttr = false,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = false, 	//是否可上传资源 true 可以上传
                                            isUploadTest = true, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/zyja.png",
                                            apiUrlList = listSchool,
                                            TabID = EnumTabindex.TeacherScienceWordOwnSkill.ToString()
                                        };
                                        tabList.Add(TAB0);
                                        tabList.Add(TAB1);
                                        tabList.Add(TAB2);
                                        tabList.Add(TAB3);//暂时不加自有作业
                                        break;
                                    case "class":
                                        listAll = new List<string>(listSchool.ToArray());
                                        listAll.Add(ConfigHelper.GetConfigString("TeachingPlanWebSiteUrl") + "AuthApi/");
                                        TAB0 = new
                                        {
                                            Title = "云教案",
                                            UIType = "Tree",
                                            ReadOnly = true,
                                            isShowForderAttr = false,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = false, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/yja.png",
                                            apiUrlList = listAll,
                                            TabID = EnumTabindex.TeacherClassCloudTeachingPlan.ToString()
                                        };
                                        TAB1 = new
                                        {
                                            Title = "讲评",
                                            UIType = "Tree",
                                            ReadOnly = true,
                                            isShowForderAttr = false,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = false, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/yja.png",
                                            apiUrlList = new string[] { ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl") + "AuthApi/" },
                                            TabID = EnumTabindex.TeacherClassComment.ToString()
                                        };
                                        TAB2 = new
                                        {
                                            Title = "自有教案",
                                            UIType = "Tree",
                                            isShowForderAttr = false,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = true, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            ReadOnly = false,
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/zyja.png",
                                            apiUrlList = listSchool,
                                            TabID = EnumTabindex.TeacherClassOwnTeachingPlan.ToString()
                                        };
                                        tabList.Add(TAB0);
                                        tabList.Add(TAB1);
                                        tabList.Add(TAB2);
                                        break;
                                }
                                #endregion
                            }
                            else
                            {
                                List<string> listAll = new List<string>();
                                List<string> listSchool = new List<string>();
                                #region 学校配置URL
                                string strSqlSchool = string.Format(@"select distinct t.D_Value as apiUrlList,t.D_PublicValue as publicUrl from ConfigSchool t
inner join VW_UserOnClassGradeSchool t2 on  t2.UserId='{0}' and t2.SchoolId=t.School_ID;", model.UserId);
                                DataTable dtUrl = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlSchool).Tables[0];
                                foreach (DataRow item in dtUrl.Rows)
                                {
                                    if (!string.IsNullOrEmpty(item["apiUrlList"].ToString()))
                                    {
                                        listSchool.Add(item["apiUrlList"].ToString() + "AuthApi/");
                                    }
                                    if (!string.IsNullOrEmpty(item["publicUrl"].ToString()))
                                    {
                                        listSchool.Add(item["publicUrl"].ToString() + "AuthApi/");
                                    }
                                }
                                #endregion
                                #region 学生
                                switch (productType)
                                {
                                    case "scienceword":

                                        break;
                                    case "class":
                                        listAll = new List<string>(listSchool.ToArray());
                                        listAll.Add(ConfigHelper.GetConfigString("TeachingPlanWebSiteUrl") + "AuthApi/");
                                        object TAB0 = new
                                        {
                                            Title = "云教案",
                                            UIType = "Tree",
                                            ReadOnly = true,
                                            isShowForderAttr = false,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = false, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/yja.png",
                                            apiUrlList = listAll,
                                            TabID = EnumTabindex.StudentClassCloudTeachingPlan.ToString()
                                        };
                                        object TAB1 = new
                                        {
                                            Title = "微课件",
                                            UIType = "Tree",
                                            ReadOnly = true,
                                            isShowForderAttr = false,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = false, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/yja.png",
                                            apiUrlList = listAll,
                                            TabID = EnumTabindex.StudentClassMicroClass.ToString()
                                        };
                                        tabList.Add(TAB0);
                                        tabList.Add(TAB1);
                                        break;
                                    case "skill":
                                        TAB0 = new
                                       {
                                           Title = "最新作业",
                                           UIType = "Tree",
                                           ReadOnly = true,
                                           isShowForderAttr = false,   // true:显示目录属性
                                           ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                           isUploadResource = false, 	//是否可上传资源 true 可以上传
                                           isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                           fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                           //IconURL = pfunction.getHostPath() + "/Images/icon/zxzy.png",
                                           apiUrlList = listSchool,
                                           TabID = EnumTabindex.StudentSkillNew.ToString()
                                       };
                                        TAB1 = new
                                        {
                                            Title = "已提交作业",
                                            UIType = "Tree",
                                            ReadOnly = true,
                                            isShowForderAttr = false,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = false, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            //IconURL = pfunction.getHostPath() + "/Images/icon/ytjzy.png",
                                            apiUrlList = listSchool,
                                            TabID = EnumTabindex.StudentSkillSubminted.ToString()
                                        };
                                        object TAB2 = new
                                        {
                                            Title = "错题集",
                                            UIType = "Tree",
                                            ReadOnly = true,
                                            isShowForderAttr = false,   // true:显示目录属性
                                            ExpandDeepDefault = 2,      //  默认展开层级	从1开始
                                            isUploadResource = false, 	//是否可上传资源 true 可以上传
                                            isUploadTest = false, 		//是否可上传试题结构 true 可以上传
                                            fileMaxLength = 10,         //上传文件的最大限制，单位是M
                                            // IconURL = pfunction.getHostPath() + "/Images/icon/ytjzy.png",
                                            apiUrlList = listSchool,
                                            TabID = EnumTabindex.StudentSkillWrong.ToString()
                                        };
                                        tabList.Add(TAB0);
                                        tabList.Add(TAB1);
                                        tabList.Add(TAB2);
                                        break;
                                }
                                #endregion
                            }
                            #endregion

                            string strToken = FormsAuthentication.HashPasswordForStoringInConfigFile(Guid.NewGuid().ToString(), "SHA1");
                            bool b = false;

                            if (model.UserIdentity == "A")
                            {
                                #region 更新管理员token
                                BLL_SysUser_token bllUT = new BLL_SysUser_token();
                                Model_SysUser_token modelUT = bllUT.GetModel(model.UserId, productType);
                                if (modelUT == null)
                                {
                                    modelUT = new Model_SysUser_token();
                                    modelUT.user_id = model.UserId;
                                    modelUT.product_type = productType;
                                    modelUT.token = strToken;
                                    modelUT.token_time = DateTime.Now;
                                    modelUT.login_time = DateTime.Now;
                                    modelUT.login_ip = pfunction.GetRealIP();
                                    b = bllUT.Add(modelUT);
                                }
                                else
                                {
                                    modelUT.token = strToken;
                                    modelUT.token_time = DateTime.Now;
                                    modelUT.login_time = DateTime.Now;
                                    modelUT.login_ip = pfunction.GetRealIP();
                                    b = bllUT.Update(modelUT);
                                }
                                #endregion
                            }
                            else
                            {
                                #region 更新老师/学生 token
                                BLL_f_user_token bllUT = new BLL_f_user_token();
                                Model_f_user_token modelUT = bllUT.GetModel(model.UserId, productType);
                                if (modelUT == null)
                                {
                                    modelUT = new Model_f_user_token();
                                    modelUT.user_id = model.UserId;
                                    modelUT.product_type = productType;
                                    modelUT.token = strToken;
                                    modelUT.token_time = DateTime.Now;
                                    modelUT.login_time = DateTime.Now;
                                    modelUT.login_ip = pfunction.GetRealIP();
                                    b = bllUT.Add(modelUT);
                                }
                                else
                                {
                                    modelUT.token = strToken;
                                    modelUT.token_time = DateTime.Now;
                                    modelUT.login_time = DateTime.Now;
                                    modelUT.login_ip = pfunction.GetRealIP();
                                    b = bllUT.Update(modelUT);
                                }
                                #endregion
                            }
                            if (b)
                            {
                                object user = new
                                {
                                    id = model.UserId,
                                    name = string.IsNullOrEmpty(model.TrueName) ? model.UserName : model.TrueName,
                                    userId = model.UserId
                                };
//                                string strSqlGetUrl = string.Format(@"select distinct t.D_Type as localType ,t.D_Value as apiUrlList  from ConfigSchool t
//inner join VW_UserOnClassGradeSchool t2 on  t2.UserId='{0}' and t2.SchoolId=t.School_ID;", model.UserId);
//                                DataTable dtlocalUrl = new DataTable();
//                                dtlocalUrl = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlGetUrl).Tables[0];

                                string strUserSchool = string.Format("select distinct SchoolId,SchoolName from VW_UserOnClassGradeSchool where SchoolId is not null and SchoolId!='' and UserId='{0}'", model.UserId);
                                DataTable dtUserSchool = new DataTable();
                                dtUserSchool = Rc.Common.DBUtility.DbHelperSQL.Query(strUserSchool).Tables[0];
                                List<object> listSchool = new List<object>();
                                foreach (DataRow item in dtUserSchool.Rows)
                                {
                                    listSchool.Add(new
                                    {
                                        SchoolId = Server.UrlEncode(item["SchoolId"].ToString()),
                                        SchoolName = Server.UrlEncode(item["SchoolName"].ToString())
                                    });
                                }
                                
                                #region 局域网配置URL
                                List<string> apiUrlList = new List<string>();
                                string strSqlSchool = string.Format(@"select distinct t.D_Value as apiUrlList,t.D_PublicValue as publicUrl from ConfigSchool t
inner join VW_UserOnClassGradeSchool t2 on  t2.UserId='{0}' and t2.SchoolId=t.School_ID;", model.UserId);
                                DataTable dtUrl = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlSchool).Tables[0];
                                foreach (DataRow item in dtUrl.Rows)
                                {
                                    if (!string.IsNullOrEmpty(item["apiUrlList"].ToString()))
                                    {
                                        apiUrlList.Add(item["apiUrlList"].ToString() + "AuthApi/");
                                    }
                                    if (!string.IsNullOrEmpty(item["publicUrl"].ToString()))
                                    {
                                        apiUrlList.Add(item["publicUrl"].ToString() + "AuthApi/");
                                    }
                                }
                                if (apiUrlList.Count == 0)
                                {
                                    apiUrlList.Add(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl") + "AuthApi/");
                                }
                                #endregion
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    token = strToken,
                                    userInfo = user,
                                    tabList = tabList,
                                    spaceUrl = pfunction.getHostPath() + "/authapi/visit/?token={token}&userId={userId}&productType=" + productType,
                                    extend = listSchool,
                                    status = true,
                                    isTransmHtml = false,
                                    apiUrlList = apiUrlList,
                                    errorMsg = "",
                                    errorCode = ""
                                });
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "更新token失败，用户登录失败",
                                    errorCode = "Login"
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "用户名或密码错误",
                                errorCode = "Login"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "Login"
                        });
                    }
                    finally
                    {
                        Response.Write(strJsion);
                        Response.End();
                    }
                    break;
                #endregion
                #region 接口名称: Logout 退出登录
                case "logout":
                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                       {
                           status = true,
                           errorMsg = "",
                           errorCode = ""
                       });
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: GetResourceTree 树型列表资源
                case "getresourcetree":
                    try
                    {
                        #region 定义
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string folderId = Request.QueryString["folderId"].Filter();
                        string tabid = Request.QueryString["TabID"].Filter();
                        string localUrlActive = Request.QueryString["localUrlActive"];

                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(folderId, "", string.Format("开始getresourcetree|token{0}|userId{1}|folderId{2}|TabID{3}"
                            , userId, userId, folderId, tabid));

                        #endregion

                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                List<object> listObj = new List<object>();

                                #region 老师
                                #region 如果是老师,scienceword,云资源
                                if (modelFUser.UserIdentity == "T" && productType == "scienceword" && tabid == EnumTabindex.TeacherScienceWordCloudTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                    Response.Write(GetCloudResource(token, userId, folderId, strResource_Type, tabid, "T", productType));
                                }
                                #endregion
                                #region 如果是老师,scienceword,自有资源
                                else if (modelFUser.UserIdentity == "T" && productType == "scienceword" && tabid == EnumTabindex.TeacherScienceWordOwnTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                    Response.Write(GetTeacherOwnResource(token, userId, folderId, strResource_Type, tabid, productType));
                                }
                                #endregion
                                #region 如果是老师,scienceword,云习题集
                                else if (modelFUser.UserIdentity == "T" && productType == "scienceword" && tabid == EnumTabindex.TeacherScienceWordCloudSkill.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                    Response.Write(GetCloudResource(token, userId, folderId, strResource_Type, tabid, "T", productType));
                                }
                                #endregion
                                #region 如果是老师,scienceword,自有习题集
                                else if (modelFUser.UserIdentity == "T" && productType == "scienceword" && tabid == EnumTabindex.TeacherScienceWordOwnSkill.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                    Response.Write(GetTeacherOwnResource(token, userId, folderId, strResource_Type, tabid, productType));
                                }
                                #endregion
                                #region 如果是老师,class,云教案
                                if (modelFUser.UserIdentity == "T" && productType == "class" && tabid == EnumTabindex.TeacherClassCloudTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                    Response.Write(GetCloudResource(token, userId, folderId, strResource_Type, tabid, "T", productType));
                                }
                                #endregion
                                #region 如果是老师,class,讲评
                                else if (modelFUser.UserIdentity == "T" && productType == "class" && tabid == EnumTabindex.TeacherClassComment.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                    Response.Write(GetTeacherComment(token, userId, folderId, strResource_Type, tabid, productType));
                                }
                                #endregion
                                #region 如果是老师,class,自有教案
                                else if (modelFUser.UserIdentity == "T" && productType == "class" && tabid == EnumTabindex.TeacherClassOwnTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                    Response.Write(GetTeacherOwnResource(token, userId, folderId, strResource_Type, tabid, productType));
                                }
                                #endregion
                                #endregion
                                #region 管理员
                                #region 如果是管理员,scienceword,云教案
                                else if (modelFUser.UserIdentity == "A" && productType == "scienceword" && tabid == EnumTabindex.MgrScienceWordCloudTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                    Response.Write(GetCloudResource(token, userId, folderId, strResource_Type, tabid, "A", productType));
                                }
                                #endregion
                                #region 如果是管理员,scienceword,云习题集
                                else if (modelFUser.UserIdentity == "A" && productType == "scienceword" && tabid == EnumTabindex.MgrScienceWordCloudSkill.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                    Response.Write(GetCloudResource(token, userId, folderId, strResource_Type, tabid, "A", productType));
                                }
                                #endregion
                                #region 如果是管理员,class,云教案
                                else if (modelFUser.UserIdentity == "A" && productType == "class" && tabid == EnumTabindex.MgrClassCloudTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                    Response.Write(GetCloudResource(token, userId, folderId, strResource_Type, tabid, "A", productType));
                                }
                                #endregion
                                #region 如果是管理员,class,微课
                                else if (modelFUser.UserIdentity == "A" && productType == "class" && tabid == EnumTabindex.MgrClassCloudMicroClass.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型微课件;
                                    Response.Write(GetCloudResource(token, userId, folderId, strResource_Type, tabid, "A", productType));
                                }
                                #endregion
                                #endregion
                                #region 学生
                                #region 如果是学生最新作业
                                else if (modelFUser.UserIdentity == "S" && productType == "skill" && tabid == EnumTabindex.StudentSkillNew.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                    Response.Write(GetStudentResource(token, userId, folderId, strResource_Type, tabid, productType));
                                }
                                #endregion
                                #region 如果是学生已提交作业
                                else if (modelFUser.UserIdentity == "S" && productType == "skill" && tabid == EnumTabindex.StudentSkillSubminted.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                    Response.Write(GetStudentResource(token, userId, folderId, strResource_Type, tabid, productType));
                                }
                                #endregion
                                #region 如果是错题本
                                else if (modelFUser.UserIdentity == "S" && productType == "skill" && tabid == EnumTabindex.StudentSkillWrong.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                    Response.Write(GetStudentResource(token, userId, folderId, strResource_Type, tabid, productType));
                                }
                                #endregion
                                #region 如果是class,微课
                                else if (modelFUser.UserIdentity == "S" && productType == "class" && tabid == EnumTabindex.StudentClassMicroClass.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型微课件;
                                    Response.Write(GetStudentClassResource(token, userId, folderId, strResource_Type, tabid, productType));
                                }
                                #endregion
                                #region 如果是class,云教案
                                else if (modelFUser.UserIdentity == "S" && productType == "class" && tabid == EnumTabindex.StudentClassCloudTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                    Response.Write(GetStudentClassResource(token, userId, folderId, strResource_Type, tabid, productType));
                                }
                                #endregion
                                #endregion
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "GetResourceTree",
                                    status = false,
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                errorMsg = "非法操作",
                                errorCode = "GetResourceTree",
                                status = false
                            });
                        }
                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(folderId, "", string.Format("结束getresourcetree|token{0}|userId{1}|folderId{2}|TabID{3}"
   , userId, userId, folderId, tabid));

                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            errorMsg = ex.Message.ToString(),
                            errorCode = "GetResourceTree",
                            status = false
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: GetResourceList 单列表资源 *************************未实现*************************
                case "getresourcelist":

                    break;
                #endregion
                #region 接口名称: SearchSource 搜索资源*************************暂时关闭*************************
                case "searchsource":
                    try
                    {
                        string token = Request.QueryString["token"].Filter();//令牌
                        string userId = Request.QueryString["userId"].Filter();//用户Id
                        string title = Request.QueryString["title"].Filter();//搜索的关键字
                        string max = Request.QueryString["max"].Filter();
                        string offset = Request.QueryString["offset"].Filter();
                        string tabid = Request.QueryString["tabId"].Filter();

                        Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                        if (modelFUser != null)
                        {
                            #region 老师
                            #region 如果是老师,scienceword,云资源
                            if (modelFUser.UserIdentity == "T" && productType == "scienceword" && tabid == EnumTabindex.TeacherScienceWordCloudTeachingPlan.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "T"));
                            }
                            #endregion
                            #region 如果是老师,scienceword,自有资源
                            else if (modelFUser.UserIdentity == "T" && productType == "scienceword" && tabid == EnumTabindex.TeacherScienceWordOwnTeachingPlan.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "T"));
                            }
                            #endregion
                            #region 如果是老师,scienceword,自有习题集
                            else if (modelFUser.UserIdentity == "T" && productType == "scienceword" && tabid == EnumTabindex.TeacherScienceWordOwnSkill.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "T"));
                            }
                            #endregion
                            #region 如果是老师,class,云教案
                            if (modelFUser.UserIdentity == "T" && productType == "class" && tabid == EnumTabindex.TeacherClassCloudTeachingPlan.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "T"));
                            }
                            #endregion
                            #region 如果是老师,class,自有教案
                            else if (modelFUser.UserIdentity == "T" && productType == "class" && tabid == EnumTabindex.TeacherClassOwnTeachingPlan.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "T"));
                            }
                            #endregion
                            #endregion
                            #region 管理员
                            #region 如果是管理员,scienceword,云教案
                            else if (modelFUser.UserIdentity == "A" && productType == "scienceword" && tabid == EnumTabindex.MgrScienceWordCloudTeachingPlan.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "A"));
                            }
                            #endregion
                            #region 如果是管理员,scienceword,云习题集
                            else if (modelFUser.UserIdentity == "A" && productType == "scienceword" && tabid == EnumTabindex.MgrScienceWordCloudSkill.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "A"));
                            }
                            #endregion
                            #region 如果是管理员,class,云教案
                            else if (modelFUser.UserIdentity == "A" && productType == "class" && tabid == EnumTabindex.MgrClassCloudTeachingPlan.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "A"));
                            }
                            #endregion
                            #region 如果是管理员,class,微课
                            else if (modelFUser.UserIdentity == "A" && productType == "class" && tabid == EnumTabindex.MgrClassCloudMicroClass.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型微课件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "A"));
                            }
                            #endregion
                            #endregion
                            #region 学生
                            #region 如果是学生已提交作业
                            else if (modelFUser.UserIdentity == "S" && productType == "skill" && tabid == EnumTabindex.StudentSkillSubminted.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "S"));
                            }
                            #endregion
                            #region 如果是学生最新作业
                            else if (modelFUser.UserIdentity == "S" && productType == "skill" && tabid == EnumTabindex.StudentSkillNew.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "S"));
                            }
                            #endregion
                            #region 如果是错题本
                            else if (modelFUser.UserIdentity == "S" && productType == "skill" && tabid == EnumTabindex.StudentSkillWrong.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "S"));
                            }
                            #endregion
                            #region 如果是class,微课
                            else if (modelFUser.UserIdentity == "S" && productType == "class" && tabid == EnumTabindex.StudentClassMicroClass.ToString())
                            {
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型微课件;
                                Response.Write(GetSearchResourceList(title, max, userId, strResource_Type, productType, tabid, "S"));
                            }
                            #endregion
                            #endregion

                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "对不起，此账号已在其他机器登录。",
                                errorCode = "SearchSource",
                                tokenStatus = false
                            });
                        }

                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "SearchSource"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: UploadResource 添加资源
                case "uploadresource":
                    try
                    {
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string folderId = Request.QueryString["folderId"].Filter();
                        string title = Request.QueryString["title"].Filter();
                        string tabId = Request.QueryString["tabId"].Filter();

                        if (!string.IsNullOrEmpty(userId))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                if (new BLL_ResourceToResourceFolder().GetRecordCount("ResourceFolder_Id='" + folderId + "' and Resource_Name='" + title + "'") > 0)
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = false,
                                        errorMsg = "资源名称已存在",
                                        errorCode = "UploadResource"
                                    });
                                }
                                else
                                {
                                    string Resource_Class = Rc.Common.Config.Resource_ClassConst.自有资源;
                                    string Resource_Version = Rc.Common.Config.Resource_VersionConst.通用版;
                                    string filePath = string.Empty;//文件存储路径
                                    string imgPath = string.Empty;//图片存储路径
                                    //判断产品类型
                                    switch (productType)
                                    {
                                        case "scienceword":
                                            strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                            filePath += "swDocument\\";
                                            imgPath += "swView\\";
                                            break;
                                        case "class":
                                            if (tabId == EnumTabindex.MgrClassCloudMicroClass.ToString())//微课
                                            {
                                                strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型微课件;
                                                filePath += "microClassDocument\\";
                                                imgPath += "microClassView\\";
                                            }
                                            else
                                            {
                                                strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                                filePath += "classDocument\\";
                                                imgPath += "classView\\";
                                            }
                                            break;
                                        case "skill":
                                            strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;

                                            break;
                                    }
                                    #region 资源文件夹表 只获取实体数据 不操作
                                    Model_ResourceFolder modelResFolder = new BLL_ResourceFolder().GetModel(folderId);
                                    if (modelResFolder != null)
                                    {
                                        Resource_Class = modelResFolder.Resource_Class;
                                        Resource_Version = modelResFolder.Resource_Version;
                                        strResource_Type = modelResFolder.Resource_Type;
                                    }
                                    else
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "没有把文件上传到创建的书本目录中。",
                                            errorCode = "uploadresource"
                                        });
                                        Response.Write(strJsion);
                                        Response.End();
                                    }
                                    #endregion
                                    string strSuffix = string.Empty;
                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(folderId, "", string.Format("开始接收数据流|操作人{0}|文件夹Id{1}|方法{2}", userId, folderId, "uploadresource"));
                                    #region 资源表
                                    HttpPostedFile file = Request.Files["content"];
                                    string fileName = file.FileName;
                                    if (!String.IsNullOrEmpty(Request["ext"]))
                                    {
                                        strSuffix = Request["ext"].ToString();
                                    }
                                    //byte[] bytes = null;
                                    //using (var binaryReader = new BinaryReader(file.InputStream))
                                    //{
                                    //    bytes = binaryReader.ReadBytes(file.ContentLength);
                                    //}
                                    Model_Resource modelResource = new Model_Resource();
                                    modelResource.Resource_Id = Guid.NewGuid().ToString();
                                    modelResource.Resource_MD5 = modelResource.Resource_Id;
                                    //modelResource.Resource_MD5 = clsUtility.GetMd5(Convert.ToBase64String(bytes));
                                    //modelResource.Resource_DataStrem = Convert.ToBase64String(bytes);

                                    HttpPostedFile htmlFile = Request.Files["htmlContent"];
                                    //bytes = null;
                                    //using (var binaryReader = new BinaryReader(htmlFile.InputStream))
                                    //{
                                    //    bytes = binaryReader.ReadBytes(htmlFile.ContentLength);
                                    //}
                                    //modelResource.Resource_ContentHtml = Convert.ToBase64String(bytes);
                                    modelResource.Resource_ContentLength = file.ContentLength;
                                    modelResource.CreateTime = DateTime.Now;
                                    #endregion
                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(folderId, "", string.Format("完成接收数据流|操作人{0}|文件夹Id{1}|方法{2}", userId, folderId, "uploadresource"));

                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(folderId, "", string.Format("开始处理数据入库|操作人{0}|文件夹Id{1}|方法{2}", userId, folderId, "uploadresource"));
                                    #region 资源文件夹关系表
                                    int order = 0;
                                    if (title.Length > 4 && (title.Substring(3, 1) == "-" || title.Substring(3, 1) == "－"))
                                    {
                                        int.TryParse(title.Substring(0, 3), out order);
                                        title = title.Substring(4);
                                    }

                                    Model_ResourceToResourceFolder modelRTRF = new Model_ResourceToResourceFolder();
                                    string strResourceToResourceFolder_Id = string.Empty;
                                    strResourceToResourceFolder_Id = Guid.NewGuid().ToString();
                                    modelRTRF.ResourceToResourceFolder_Id = strResourceToResourceFolder_Id;
                                    modelRTRF.ResourceFolder_Id = folderId;
                                    modelRTRF.Resource_Id = modelResource.Resource_Id;
                                    modelRTRF.File_Name = title;
                                    modelRTRF.Resource_Type = strResource_Type;
                                    modelRTRF.Resource_Name = title;
                                    modelRTRF.ResourceToResourceFolder_Order = order;
                                    modelRTRF.Resource_Class = Resource_Class;
                                    modelRTRF.Resource_Version = Resource_Version;
                                    modelRTRF.File_Owner = userId;
                                    modelRTRF.CreateFUser = userId;
                                    modelRTRF.CreateTime = DateTime.Now;
                                    modelRTRF.File_Suffix = strSuffix;
                                    string FileDataName = strResourceToResourceFolder_Id;
                                    #region 保存文件及图片
                                    //生成存储路径
                                    string savePath = string.Empty;
                                    if (Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                                    {
                                        savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelResFolder.ParticularYear, modelResFolder.GradeTerm,
                                        modelResFolder.Resource_Version, modelResFolder.Subject);
                                    }

                                    filePath += savePath;
                                    imgPath += savePath;
                                    if (!Directory.Exists(uploadPath + filePath)) Directory.CreateDirectory(uploadPath + filePath);
                                    if (!Directory.Exists(uploadPath + imgPath)) Directory.CreateDirectory(uploadPath + imgPath);
                                    #region 保存文件

                                    file.SaveAs(uploadPath + filePath + FileDataName + "." + strSuffix);
                                    htmlFile.SaveAs(uploadPath + filePath + FileDataName + ".htm");
                                    #endregion
                                    modelRTRF.Resource_Url = filePath + FileDataName + "." + strSuffix;
                                    #region 保存图片
                                    List<Model_ResourceToResourceFolder_img> listModelRTRF_img = new List<Model_ResourceToResourceFolder_img>();
                                    HttpPostedFile thumbFile = Request.Files["thumbContent"];
                                    if (thumbFile.ContentLength > 0)
                                    {
                                        string tempGuid = Guid.NewGuid().ToString();
                                        thumbFile.SaveAs(uploadPath + imgPath + tempGuid + ".txt");//把图片 数据流先保存到txt临时文件，拆分保存用
                                        string tempTxt = File.ReadAllText(uploadPath + imgPath + tempGuid + ".txt", Encoding.Default);
                                        string[] imgStrArr = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(tempTxt);
                                        for (int i = 0; i < imgStrArr.Length; i++)
                                        {
                                            string imgGuid = Guid.NewGuid().ToString();//图片名称
                                            pfunction.WriteToImage(uploadPath + imgPath + imgGuid + ".jpg", imgStrArr[i]);
                                            Model_ResourceToResourceFolder_img modelRTRF_img = new Model_ResourceToResourceFolder_img();
                                            modelRTRF_img.ResourceToResourceFolder_img_id = imgGuid;
                                            modelRTRF_img.ResourceToResourceFolder_id = modelRTRF.ResourceToResourceFolder_Id;
                                            modelRTRF_img.ResourceToResourceFolderImg_Order = i + 1;
                                            modelRTRF_img.ResourceToResourceFolderImg_Url = string.Format("{0}{1}", imgPath, imgGuid + ".jpg");
                                            modelRTRF_img.CreateTime = DateTime.Now;
                                            listModelRTRF_img.Add(modelRTRF_img);
                                        }
                                        File.Delete(uploadPath + imgPath + tempGuid + ".txt");
                                    }
                                    #endregion
                                    #endregion
                                    modelRTRF.Resource_Domain = "";
                                    modelRTRF.Book_ID = modelResFolder.Book_ID;

                                    modelRTRF.ParticularYear = modelResFolder.ParticularYear;
                                    modelRTRF.GradeTerm = modelResFolder.GradeTerm;
                                    modelRTRF.Resource_Version = modelResFolder.Resource_Version;
                                    modelRTRF.Subject = modelResFolder.Subject;
                                    #endregion
                                    #region 图书生产日志
                                    Model_BookProductionLog modelBPL = new Model_BookProductionLog();
                                    modelBPL.BookProductionLog_Id = Guid.NewGuid().ToString();
                                    modelBPL.BookId = modelRTRF.Book_ID;
                                    modelBPL.ResourceToResourceFolder_Id = modelRTRF.ResourceToResourceFolder_Id;
                                    modelBPL.ParticularYear = Convert.ToInt16(modelRTRF.ParticularYear);
                                    modelBPL.Resource_Type = modelRTRF.Resource_Type;
                                    modelBPL.LogTypeEnum = "1";//1添加,2修改
                                    modelBPL.CreateUser = userId;
                                    modelBPL.CreateTime = DateTime.Now;
                                    #endregion

                                    int result = new BLL_Resource().ClientUploadScienceWord(modelResource, modelRTRF, listModelRTRF_img, modelBPL);
                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(folderId, "", string.Format("完成处理数据入库|操作人{0}|文件夹Id{1}|方法{2}", userId, folderId, "uploadresource"));
                                    if (result == 0)
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "添加资源失败",
                                            errorCode = "UploadResource"
                                        });
                                    }
                                    else
                                    {
                                        #region 记录教案访问情况
                                        Model_visit_client modelVC = new Model_visit_client();
                                        modelVC.visit_client_id = Guid.NewGuid().ToString();
                                        modelVC.user_id = userId;
                                        modelVC.resource_data_id = strResourceToResourceFolder_Id;
                                        modelVC.product_type = productType;
                                        modelVC.tab_id = tabId;
                                        modelVC.open_time = DateTime.Now;
                                        modelVC.operate_type = "create";
                                        new BLL_visit_client().Add(modelVC);
                                        #endregion
                                        #region 记录需要同步的数据
                                        Model_SyncData modelSD = new Model_SyncData();
                                        modelSD.SyncDataId = Guid.NewGuid().ToString();
                                        modelSD.TableName = "ResourceToResourceFolder";
                                        modelSD.DataId = strResourceToResourceFolder_Id;
                                        modelSD.OperateType = "add";
                                        modelSD.CreateTime = DateTime.Now;
                                        modelSD.SyncStatus = "0";
                                        new BLL_SyncData().Add(modelSD);
                                        #endregion
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = true,
                                            errorMsg = "",
                                            errorCode = ""
                                        });
                                    }
                                }
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "UploadResource",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "UploadResource"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = ""
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: UpdateResource 更新/修改资源
                case "updateresource":
                    try
                    {
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string id = Request.QueryString["id"].Filter();
                        string version = Request.QueryString["version"].Filter();

                        if (!string.IsNullOrEmpty(userId))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                string filePath = string.Empty;//文件存储路径
                                string imgPath = string.Empty;//图片存储路径
                                //判断产品类型
                                switch (productType)
                                {
                                    case "scienceword":
                                        filePath += "swDocument\\";
                                        imgPath += "swView\\";
                                        break;
                                    case "class":
                                        filePath += "classDocument\\";
                                        imgPath += "classView\\";
                                        break;
                                }
                                Rc.Common.SystemLog.SystemLog.AddLogFromBS(id, "", string.Format("开始接收数据流|操作人{0}|资源Id{1}|方法{2}", userId, id, "updateresource"));
                                HttpPostedFile file = Request.Files["content"];
                                HttpPostedFile htmlFile = Request.Files["htmlContent"];
                                HttpPostedFile thumbFile = Request.Files["thumbContent"];
                                Rc.Common.SystemLog.SystemLog.AddLogFromBS(id, "", string.Format("完成接收数据流|操作人{0}|资源Id{1}|方法{2}", userId, id, "updateresource"));

                                Rc.Common.SystemLog.SystemLog.AddLogFromBS(id, "", string.Format("开始处理数据入库|操作人{0}|资源Id{1}|方法{2}", userId, id, "updateresource"));
                                Model_ResourceToResourceFolder modelRTRF = new Model_ResourceToResourceFolder();
                                modelRTRF = new BLL_ResourceToResourceFolder().GetModel(id);
                                if (modelRTRF != null)
                                {
                                    string Resource_Class = modelRTRF.Resource_Class;
                                    string FileDataName = modelRTRF.ResourceToResourceFolder_Id;
                                    string strSuffix = modelRTRF.File_Suffix;
                                    modelRTRF.UpdateTime = DateTime.Now;
                                    #region 保存文件及图片
                                    //生成存储路径
                                    //生成存储路径
                                    string savePath = string.Empty;
                                    if (Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                                    {
                                        savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm,
                                        modelRTRF.Resource_Version, modelRTRF.Subject);
                                    }
                                    //string savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm,
                                    //modelRTRF.Resource_Version, modelRTRF.Subject);
                                    filePath += savePath;
                                    imgPath += savePath;
                                    modelRTRF.Resource_Url = filePath + FileDataName + "." + strSuffix;
                                    if (!Directory.Exists(uploadPath + filePath)) Directory.CreateDirectory(uploadPath + filePath);
                                    if (!Directory.Exists(uploadPath + imgPath)) Directory.CreateDirectory(uploadPath + imgPath);
                                    #region 保存文件


                                    string fileUrl = uploadPath + filePath + FileDataName + "." + strSuffix;
                                    string htmlUrl = uploadPath + filePath + FileDataName + ".htm";
                                    file.SaveAs(fileUrl);
                                    htmlFile.SaveAs(htmlUrl);
                                    #endregion

                                    #region 保存图片
                                    List<Model_ResourceToResourceFolder_img> listModelRTRF_img = new List<Model_ResourceToResourceFolder_img>();
                                    if (thumbFile.ContentLength > 0)
                                    {
                                        string tempGuid = Guid.NewGuid().ToString();
                                        thumbFile.SaveAs(uploadPath + imgPath + tempGuid + ".txt");
                                        string tempTxt = File.ReadAllText(uploadPath + imgPath + tempGuid + ".txt", Encoding.Default);
                                        string[] imgStrArr = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(tempTxt);
                                        for (int i = 0; i < imgStrArr.Length; i++)
                                        {
                                            string imgGuid = Guid.NewGuid().ToString();//图片名称
                                            byte[] bytes = Convert.FromBase64String(imgStrArr[i]);
                                            // 生成jpg图片
                                            File.WriteAllBytes(uploadPath + imgPath + imgGuid + ".jpg", bytes);
                                            Model_ResourceToResourceFolder_img modelRTRF_img = new Model_ResourceToResourceFolder_img();
                                            modelRTRF_img.ResourceToResourceFolder_img_id = imgGuid;
                                            modelRTRF_img.ResourceToResourceFolder_id = modelRTRF.ResourceToResourceFolder_Id;
                                            modelRTRF_img.ResourceToResourceFolderImg_Order = i + 1;
                                            modelRTRF_img.ResourceToResourceFolderImg_Url = string.Format("{0}{1}", imgPath, imgGuid + ".jpg");
                                            modelRTRF_img.CreateTime = DateTime.Now;
                                            listModelRTRF_img.Add(modelRTRF_img);
                                        }
                                        File.Delete(uploadPath + imgPath + tempGuid + ".txt");
                                    }

                                    #endregion
                                    #endregion
                                    #region 图书生产日志
                                    Model_BookProductionLog modelBPL = new Model_BookProductionLog();
                                    modelBPL.BookProductionLog_Id = Guid.NewGuid().ToString();
                                    modelBPL.BookId = modelRTRF.Book_ID;
                                    modelBPL.ResourceToResourceFolder_Id = modelRTRF.ResourceToResourceFolder_Id;
                                    modelBPL.ParticularYear = Convert.ToInt16(modelRTRF.ParticularYear);
                                    modelBPL.Resource_Type = modelRTRF.Resource_Type;
                                    modelBPL.LogTypeEnum = "2";//1添加,2修改
                                    modelBPL.CreateUser = userId;
                                    modelBPL.CreateTime = DateTime.Now;
                                    #endregion

                                    int result = new BLL_ResourceToResourceFolder_img().UpdateRTRFAddListData(modelRTRF, listModelRTRF_img, modelBPL);
                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(id, "", string.Format("完成处理数据入库|操作人{0}|资源Id{1}|方法{2}", userId, id, "updateresource"));
                                    if (result > 0)
                                    {
                                        #region 记录需要同步的数据
                                        Model_SyncData modelSD = new Model_SyncData();
                                        modelSD.SyncDataId = Guid.NewGuid().ToString();
                                        modelSD.TableName = "ResourceToResourceFolder";
                                        modelSD.DataId = modelRTRF.ResourceToResourceFolder_Id;
                                        modelSD.OperateType = "modify";
                                        modelSD.CreateTime = DateTime.Now;
                                        modelSD.SyncStatus = "0";
                                        new BLL_SyncData().Add(modelSD);
                                        #endregion
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = true,
                                            errorMsg = "",
                                            errorCode = ""
                                        });
                                    }
                                    else
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "图片保存到数据库失败",
                                            errorCode = "UpdateResource"
                                        });
                                    }
                                }
                                else
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = false,
                                        errorMsg = "文件已删除",
                                        errorCode = "UpdateResource"
                                    });
                                }
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "UpdateResource",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "UpdateResource"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "UpdateResource"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: GetResourceInfo 获取资源信息
                case "getresourceinfo":
                    try
                    {
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string id = Request.QueryString["id"].Filter();
                        //string tabid = Request.QueryString["tabId"].Filter();

                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                Rc.Common.SystemLog.SystemLog.AddLogFromBS(id, "", string.Format("开始获取资源数据|操作人{0}|资源Id{1}|方法{2}", userId, id, "getresourceinfo"));
                                Model_ResourceToResourceFolder modelRTRF = new Model_ResourceToResourceFolder();
                                modelRTRF = new BLL_ResourceToResourceFolder().GetModel(id);

                                if (modelRTRF == null)
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = false,
                                        errorMsg = "获取数据失败",
                                        errorCode = "GetResourceInfo"
                                    });
                                }
                                else
                                {
                                    string strFileType = modelRTRF.File_Suffix;
                                    if (strFileType != "dsc" && strFileType != "class" && strFileType != "testPaper" && strFileType != "folder")
                                    {
                                        strFileType = "other";
                                    }

                                    #region 得到文件下载地址
                                    string downLoadUrl = string.Empty;
                                    string strDownLoadFileID = string.Empty;
                                    string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                                    string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                                    strDownLoadFileID = modelRTRF.ResourceToResourceFolder_Id;
                                    strDownLoadFileType = "";//客户端 尚未传值
                                    if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                                    {
                                        strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                                    }
                                    downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                        , pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, userId, productType);
                                    #endregion
                                    object resInfo = new
                                    {
                                        id = modelRTRF.ResourceToResourceFolder_Id,
                                        title = modelRTRF.File_Name,
                                        isFolder = false,
                                        ext = modelRTRF.File_Suffix,
                                        typeId = modelRTRF.Resource_Type,
                                        typeName = "信息详情",//GetD_Name(modelRTRF.Resource_Type),
                                        fileType = strFileType,
                                        dateCreated = pfunction.ConvertToLongDateTime(modelRTRF.CreateTime.ToString()),
                                        userId = modelRTRF.CreateFUser,
                                        //userName = GetUserNameByUserId(modelRTRF.CreateFUser),
                                        isWritable = false,
                                        version = modelRTRF.Resource_Version,
                                        streamUrl = "",

                                        downloadUrl = downLoadUrl,

                                        visitUrl = ""
                                    };
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = true,
                                        errorMsg = "",
                                        errorCode = "",
                                        resourceInfo = resInfo
                                    });
                                }
                                Rc.Common.SystemLog.SystemLog.AddLogFromBS(id, "", string.Format("完成获取资源数据|操作人{0}|资源Id{1}|方法{2}", userId, id, "getresourceinfo"));
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "GetResourceInfo",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "GetResourceInfo"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "GetResourceInfo"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: AddFolder 新建文件夹
                case "addfolder":
                    try
                    {
                        string userId = Request.QueryString["userId"].Filter();
                        string token = Request.QueryString["token"].Filter();
                        string folderId = Request.QueryString["folderId"].Filter();
                        string title = Request.QueryString["title"].Filter();
                        string tabid = Request.QueryString["TabID"].ToString().Filter();
                        if (String.IsNullOrWhiteSpace(folderId))
                        {
                            folderId = "0";
                        }
                        Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);

                        if (!string.IsNullOrEmpty(userId))
                        {
                            if (modelFUser != null)
                            {
                                #region 老师
                                #region 如果是老师，scienceword，自有教案
                                if (modelFUser.UserIdentity == "T" && productType == "scienceword" && tabid == EnumTabindex.TeacherScienceWordOwnTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                    strJsion = AddTeacherOwnResourcesForder(modelFUser, userId, token, folderId, title, strResource_Type, tabid);
                                }
                                #endregion
                                #region 如果是老师，scienceword，自有习题集
                                else if (modelFUser.UserIdentity == "T" && productType == "scienceword" && tabid == EnumTabindex.TeacherScienceWordOwnSkill.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                    strJsion = AddTeacherOwnResourcesForder(modelFUser, userId, token, folderId, title, strResource_Type, tabid);
                                }
                                #endregion
                                #region 如果是老师，class，自有教案
                                else if (modelFUser.UserIdentity == "T" && productType == "class" && tabid == EnumTabindex.TeacherClassOwnTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                    strJsion = AddTeacherOwnResourcesForder(modelFUser, userId, token, folderId, title, strResource_Type, tabid);
                                }
                                #endregion
                                #endregion
                                #region 管理员
                                #region 如果是管理员，scienceword，云教案
                                else if (modelFUser.UserIdentity == "A" && productType == "scienceword" && tabid == EnumTabindex.MgrScienceWordCloudTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                    strJsion = AddMgrResourcesForder(userId, token, folderId, title, strResource_Type, tabid);
                                }
                                #endregion
                                #region 如果是管理员，scienceword，云习题集
                                else if (modelFUser.UserIdentity == "A" && productType == "scienceword" && tabid == EnumTabindex.MgrScienceWordCloudSkill.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                    strJsion = AddMgrResourcesForder(userId, token, folderId, title, strResource_Type, tabid);
                                }
                                #endregion
                                #region 如果是管理员，class，云教案
                                else if (modelFUser.UserIdentity == "A" && productType == "class" && tabid == EnumTabindex.MgrClassCloudTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                    strJsion = AddMgrResourcesForder(userId, token, folderId, title, strResource_Type, tabid);
                                }
                                #endregion
                                #region 如果是管理员，class，微课
                                else if (modelFUser.UserIdentity == "A" && productType == "class" && tabid == EnumTabindex.MgrClassCloudMicroClass.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型微课件;
                                    strJsion = AddMgrResourcesForder(userId, token, folderId, title, strResource_Type, tabid);
                                }
                                #endregion
                                #endregion
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "AddFolder",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "AddFolder"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "AddFolder"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: DeletePath 删除文件/文件夹
                case "deletepath":
                    try
                    {
                        string userId = Request.QueryString["userId"].Filter();
                        string token = Request.QueryString["token"].Filter();
                        string pathId = Request.QueryString["pathId"].Filter();

                        if (!string.IsNullOrEmpty(userId))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                int RFCount = new BLL_ResourceFolder().GetRecordCount(string.Format("ResourceFolder_Id='{0}'", pathId));
                                if (RFCount == 0)
                                {
                                    if (new BLL_HomeWork().GetRecordCount_Operate("ResourceToResourceFolder_Id='" + pathId + "'") > 0)
                                    {
                                        #region 判断运营平台存在已布置作业,无法删除
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "存在已布置作业,无法删除",
                                            errorCode = "DeletePath"
                                        });
                                        #endregion
                                    }
                                    else
                                    {
                                        #region 删除文件
                                        Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(pathId);
                                        List<string> listFileUrl = GetResourceFile(modelRTRF, uploadPath);
                                        if (new BLL_Resource().DeleteResource(pathId, modelRTRF.Resource_Id))
                                        {
                                            #region 记录需要同步的数据
                                            Model_SyncData modelSD = new Model_SyncData();
                                            modelSD.SyncDataId = Guid.NewGuid().ToString();
                                            modelSD.TableName = "ResourceToResourceFolder";
                                            modelSD.DataId = pathId;
                                            modelSD.OperateType = "delete";
                                            modelSD.CreateTime = DateTime.Now;
                                            modelSD.SyncStatus = "0";
                                            new BLL_SyncData().Add(modelSD);
                                            #endregion
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = true,
                                                errorMsg = "",
                                                errorCode = ""
                                            });
                                            DeleteResourceFile(listFileUrl);
                                        }
                                        else
                                        {
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = false,
                                                errorMsg = "文件删除失败",
                                                errorCode = "DeletePath"
                                            });
                                        }
                                        #endregion
                                    }
                                }
                                else
                                {
                                    #region 删除文件夹
                                    if (new BLL_ResourceToResourceFolder().GetRecordCount(string.Format("ResourceFolder_Id='{0}'", pathId)) > 0)
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "文件夹下存在文件，删除失败",
                                            errorCode = "DeletePath"
                                        });
                                    }
                                    else
                                    {
                                        if (new BLL_ResourceFolder().Delete(pathId))
                                        {
                                            #region 记录需要同步的数据
                                            Model_SyncData modelSD = new Model_SyncData();
                                            modelSD.SyncDataId = Guid.NewGuid().ToString();
                                            modelSD.TableName = "ResourceFolder";
                                            modelSD.DataId = pathId;
                                            modelSD.OperateType = "delete";
                                            modelSD.CreateTime = DateTime.Now;
                                            modelSD.SyncStatus = "0";
                                            new BLL_SyncData().Add(modelSD);
                                            #endregion
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = true,
                                                errorMsg = "",
                                                errorCode = ""
                                            });
                                        }
                                        else
                                        {
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = false,
                                                errorMsg = "文件夹删除失败",
                                                errorCode = "DeletePath"
                                            });
                                        }
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "DeletePath",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "DeletePath"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "DeletePath"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region  接口名称: RenamePath 文件夹/文件 重命名
                case "renamepath":
                    try
                    {
                        string userId = Request.QueryString["userId"].Filter();
                        string token = Request.QueryString["token"].Filter();
                        string pathId = Request.QueryString["pathId"].Filter();
                        string title = Request.QueryString["title"].Filter();

                        if (!string.IsNullOrEmpty(userId))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                BLL_ResourceFolder bllRF = new BLL_ResourceFolder();
                                BLL_ResourceToResourceFolder bllRTRF = new BLL_ResourceToResourceFolder();
                                int RFCount = bllRF.GetRecordCount(string.Format("ResourceFolder_Id='{0}'", pathId));
                                if (RFCount == 0)
                                {
                                    #region 文件重命名
                                    Model_ResourceToResourceFolder model = bllRTRF.GetModel(pathId);
                                    int order = 0;
                                    if (title.Length > 4 && (title.Substring(3, 1) == "-" || title.Substring(3, 1) == "－"))
                                    {
                                        int.TryParse(title.Substring(0, 3), out order);
                                        title = title.Substring(4);
                                    }
                                    model.File_Name = title;
                                    model.Resource_Name = title;
                                    model.ResourceToResourceFolder_Order = order;
                                    model.UpdateTime = DateTime.Now;

                                    if (new BLL_ResourceToResourceFolder().GetRecordCount("ResourceFolder_Id='" + model.ResourceFolder_Id
                                        + "' and Resource_Name='" + model.Resource_Name + "' and ResourceToResourceFolder_Id!='" + model.ResourceToResourceFolder_Id + "'") > 0)
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "资源名称已存在,重命名失败",
                                            errorCode = "UploadResource"
                                        });
                                    }
                                    else
                                    {
                                        if (bllRTRF.Update(model))
                                        {
                                            #region 记录需要同步的数据
                                            Model_SyncData modelSD = new Model_SyncData();
                                            modelSD.SyncDataId = Guid.NewGuid().ToString();
                                            modelSD.TableName = "ResourceToResourceFolder";
                                            modelSD.DataId = pathId;
                                            modelSD.OperateType = "rename";
                                            modelSD.CreateTime = DateTime.Now;
                                            modelSD.SyncStatus = "0";
                                            new BLL_SyncData().Add(modelSD);
                                            #endregion
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = true,
                                                fileNameNew = title,
                                                errorMsg = "",
                                                errorCode = ""
                                            });
                                        }
                                        else
                                        {
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = false,
                                                errorMsg = "文件重命名失败",
                                                errorCode = "RenamePath"
                                            });
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region 文件夹重命名
                                    Model_ResourceFolder model = bllRF.GetModel(pathId);
                                    int order = 0;
                                    if (title.Length > 4 && (title.Substring(3, 1) == "-" || title.Substring(3, 1) == "－"))
                                    {
                                        int.TryParse(title.Substring(0, 3), out order);
                                        title = title.Substring(4);
                                    }
                                    if (bllRF.GetRecordCount("ResourceFolder_ParentId='" + model.ResourceFolder_ParentId
                                        + "' and ResourceFolder_Name='" + title + "' and ResourceFolder_Id!='" + pathId + "' ") > 0)
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "文件夹名称已存在",
                                            errorCode = "RenamePath"
                                        });
                                    }
                                    else
                                    {
                                        model.ResourceFolder_Name = title;
                                        model.ResourceFolder_Order = order;
                                        if (bllRF.Update(model))
                                        {
                                            #region 记录需要同步的数据
                                            Model_SyncData modelSD = new Model_SyncData();
                                            modelSD.SyncDataId = Guid.NewGuid().ToString();
                                            modelSD.TableName = "ResourceFolder";
                                            modelSD.DataId = pathId;
                                            modelSD.OperateType = "rename";
                                            modelSD.CreateTime = DateTime.Now;
                                            modelSD.SyncStatus = "0";
                                            new BLL_SyncData().Add(modelSD);
                                            #endregion
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = true,
                                                errorMsg = "",
                                                errorCode = ""
                                            });
                                        }
                                        else
                                        {
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = false,
                                                errorMsg = "文件夹重命名失败",
                                                errorCode = "RenamePath"
                                            });
                                        }
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "RenamePath",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "RenamePath"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "RenamePath"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region  接口名称: MovePath 移动文件/文件夹
                case "movepath":
                    try
                    {
                        string userId = Request.QueryString["userId"].Filter();
                        string token = Request.QueryString["token"].Filter();
                        string pathId = Request.QueryString["pathId"].Filter();
                        string targetFolederId = Request.QueryString["targetFolederId"].Filter();

                        if (!string.IsNullOrEmpty(userId))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                int RFCount = new BLL_ResourceFolder().GetRecordCount(string.Format("ResourceFolder_Id='{0}'", pathId));
                                if (RFCount == 0)
                                {
                                    #region 移动文件
                                    BLL_ResourceToResourceFolder bll = new BLL_ResourceToResourceFolder();
                                    Model_ResourceToResourceFolder model = bll.GetModel(pathId);
                                    Model_ResourceFolder modelTarget = new BLL_ResourceFolder().GetModel(targetFolederId);
                                    if (new BLL_ResourceToResourceFolder().GetRecordCount("ResourceFolder_Id='" + modelTarget.ResourceFolder_Id + "' and Resource_Name='" + model.Resource_Name + "'") > 0)
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "资源名称已存在,移动失败",
                                            errorCode = "UploadResource"
                                        });
                                    }
                                    else
                                    {
                                        if (model.Book_ID != modelTarget.Book_ID)
                                        {
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = false,
                                                errorMsg = "不能移动到其他书目录，移动失败",
                                                errorCode = "MovePath"
                                            });
                                        }
                                        else
                                        {
                                            model.ResourceFolder_Id = targetFolederId;
                                            model.UpdateTime = DateTime.Now;

                                            model.Book_ID = modelTarget.Book_ID;
                                            if (bll.Update(model))
                                            {
                                                #region 记录需要同步的数据
                                                Model_SyncData modelSD = new Model_SyncData();
                                                modelSD.SyncDataId = Guid.NewGuid().ToString();
                                                modelSD.TableName = "ResourceToResourceFolder";
                                                modelSD.DataId = pathId;
                                                modelSD.OperateType = "move";
                                                modelSD.CreateTime = DateTime.Now;
                                                modelSD.SyncStatus = "0";
                                                new BLL_SyncData().Add(modelSD);
                                                #endregion
                                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                                {
                                                    status = true,
                                                    errorMsg = "",
                                                    errorCode = ""
                                                });
                                            }
                                            else
                                            {
                                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                                {
                                                    status = false,
                                                    errorMsg = "移动文件失败",
                                                    errorCode = "MovePath"
                                                });
                                            }
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region 移动文件夹
                                    BLL_ResourceFolder bll = new BLL_ResourceFolder();
                                    if (pathId == targetFolederId)
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "不能移动到本身，移动失败",
                                            errorCode = "MovePath",
                                            tokenStatus = false
                                        });
                                    }
                                    else
                                    {
                                        if (bll.GetRecordCount(string.Format("ResourceFolder_ParentId='{0}' and ResourceFolder_Id='{1}'", pathId, targetFolederId)) > 0)
                                        {
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = false,
                                                errorMsg = "目标文件夹为移动文件夹子级，移动失败",
                                                errorCode = "MovePath",
                                                tokenStatus = false
                                            });
                                        }
                                        else
                                        {
                                            Model_ResourceFolder modelTarget = bll.GetModel(targetFolederId);
                                            if (modelTarget == null)
                                            {
                                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                                {
                                                    status = false,
                                                    errorMsg = "移动文件夹失败,目标文件夹错误",
                                                    errorCode = "MovePath"
                                                });
                                            }
                                            else
                                            {
                                                Model_ResourceFolder model = bll.GetModel(pathId);
                                                if (model.Book_ID != modelTarget.Book_ID)
                                                {
                                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                                    {
                                                        status = false,
                                                        errorMsg = "不能移动到其他书目录，移动失败",
                                                        errorCode = "MovePath"
                                                    });
                                                }
                                                else
                                                {
                                                    model.ResourceFolder_ParentId = targetFolederId;
                                                    model.ResourceFolder_Level = modelTarget.ResourceFolder_Level + 1;
                                                    model.Book_ID = modelTarget.Book_ID;
                                                    if (bll.Update(model))
                                                    {
                                                        #region 记录需要同步的数据
                                                        Model_SyncData modelSD = new Model_SyncData();
                                                        modelSD.SyncDataId = Guid.NewGuid().ToString();
                                                        modelSD.TableName = "ResourceFolder";
                                                        modelSD.DataId = pathId;
                                                        modelSD.OperateType = "move";
                                                        modelSD.CreateTime = DateTime.Now;
                                                        modelSD.SyncStatus = "0";
                                                        new BLL_SyncData().Add(modelSD);
                                                        #endregion
                                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                                        {
                                                            status = true,
                                                            errorMsg = "",
                                                            errorCode = ""
                                                        });
                                                    }
                                                    else
                                                    {
                                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                                        {
                                                            status = false,
                                                            errorMsg = "移动文件夹失败",
                                                            errorCode = "MovePath"
                                                        });
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "MovePath",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "MovePath"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "MovePath"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: testLibrarySubmit 提交题库接口
                case "testlibrarysubmit":
                    try
                    {
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string userName = Request.QueryString["userName"].Filter();
                        string folderId = Request.QueryString["folderId"].Filter();
                        string resourceId = Request.QueryString["resourceId"].Filter();
                        string title = Request.QueryString["title"].Filter();
                        string tabId = Request.QueryString["tabId"].Filter();
                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(resourceId, "", string.Format("开始接收对象|操作人{0}|试卷Id{1}|方法{2}", userId, resourceId, "testlibrarysubmit"));
                        Stream resStream = HttpContext.Current.Request.InputStream;
                        StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
                        string testJsion = sr.ReadToEnd();
                        string resInfo = testJsion;
                        DateTime CreateTime = DateTime.Now;
                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(resourceId, "", string.Format("完成接收对象|操作人{0}|试卷Id{1}|方法{2}", userId, resourceId, "testlibrarysubmit"));
                        string operateMode = "add";
                        string strResourceToResourceFolder_Id = string.Empty;
                        if (new BLL_ResourceToResourceFolder().Exists(resourceId)) operateMode = "update";//如果有就更新
                        else resourceId = Guid.NewGuid().ToString();

                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(resInfo))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                if (new BLL_HomeWork().GetRecordCount_Operate("ResourceToResourceFolder_Id='" + resourceId + "'") > 0)
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = false,
                                        errorMsg = "试卷已布置作业,修改失败",
                                        errorCode = "testLibrarySubmit"
                                    });
                                    return;
                                }
                                if (new BLL_Two_WayChecklistToTestpaper().GetRecordCount_Operate("ResourceToResourceFolder_Id='" + resourceId + "'") > 0)
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = false,
                                        errorMsg = "试卷已关联双向细目表,修改失败",
                                        errorCode = "testLibrarySubmit"
                                    });
                                    return;
                                }

                                #region 保存
                                int order = 0;
                                if (title.Length > 4 && (title.Substring(3, 1) == "-" || title.Substring(3, 1) == "－"))
                                {
                                    int.TryParse(title.Substring(0, 3), out order);
                                    title = title.Substring(4);
                                }
                                string strCheckExistWhere = string.Empty;
                                if (operateMode == "update") strCheckExistWhere = " and ResourceToResourceFolder_Id!='" + resourceId + "' ";
                                if (new BLL_ResourceToResourceFolder().GetRecordCount("ResourceFolder_Id='" + folderId + "' and Resource_Name='" + title + "'" + strCheckExistWhere) > 0)
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = false,
                                        errorMsg = "资源名称已存在",
                                        errorCode = "testLibrarySubmit"
                                    });
                                }
                                else
                                {
                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(resourceId, "", string.Format("开始解析对象|操作人{0}|试卷Id{1}|方法{2}", userId, resourceId, "testlibrarysubmit"));
                                    ResourceInfo resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResourceInfo>(resInfo);
                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(resourceId, "", string.Format("完成解析对象|操作人{0}|试卷Id{1}|方法{2}", userId, resourceId, "testlibrarysubmit"));
                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(resourceId, "", string.Format("开始处理数据|操作人{0}|试卷Id{1}|方法{2}", userId, resourceId, "testlibrarysubmit"));
                                    if (resModel != null)
                                    {
                                        string Resource_Class = Rc.Common.Config.Resource_ClassConst.自有资源;
                                        //string Resource_Version = Rc.Common.Config.Resource_VersionConst.通用版;
                                        string Resource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                        //string Subject = "";
                                        Model_Resource modelResource = new Model_Resource();
                                        Model_ResourceToResourceFolder modelRTRFolder = new Model_ResourceToResourceFolder();
                                        if (operateMode == "add")
                                        {
                                            #region 添加
                                            #region 资源文件夹表 只获取实体数据 不操作
                                            Model_ResourceFolder modelResFolder = new BLL_ResourceFolder().GetModel(folderId);
                                            if (modelResFolder != null)
                                            {
                                                Resource_Class = modelResFolder.Resource_Class;
                                                //Resource_Version = modelResFolder.Resource_Version;
                                                Resource_Type = modelResFolder.Resource_Type;
                                                //Subject = modelResFolder.Subject;
                                            }
                                            else
                                            {
                                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                                {
                                                    status = false,
                                                    errorMsg = "没有把文件上传到创建的书本目录中。",
                                                    errorCode = "testLibrarySubmit"
                                                });
                                                Response.Write(strJsion);
                                                Response.End();
                                            }
                                            #endregion
                                            #region 资源表

                                            modelResource.Resource_Id = resourceId;
                                            byte[] bytes = StreamToBytes(resStream);
                                            modelResource.Resource_MD5 = clsUtility.GetMd5(resInfo);
                                            //modelResource.Resource_DataStrem = "";//不用了
                                            // modelResource.Resource_ContentHtml = "";//不用了
                                            modelResource.Resource_ContentLength = resInfo.Length;
                                            modelResource.CreateTime = DateTime.Now;
                                            #endregion
                                            #region 资源文件夹关系表
                                            strResourceToResourceFolder_Id = Guid.NewGuid().ToString();
                                            modelRTRFolder.ResourceToResourceFolder_Id = strResourceToResourceFolder_Id;
                                            modelRTRFolder.ResourceFolder_Id = folderId;
                                            modelRTRFolder.Resource_Id = modelResource.Resource_Id;
                                            modelRTRFolder.File_Name = title;
                                            modelRTRFolder.Resource_Type = Resource_Type;
                                            modelRTRFolder.Resource_Name = title;
                                            modelRTRFolder.ResourceToResourceFolder_Order = order;
                                            modelRTRFolder.Resource_Class = Resource_Class;
                                            modelRTRFolder.File_Owner = userId;
                                            modelRTRFolder.CreateFUser = userId;
                                            modelRTRFolder.CreateTime = CreateTime;
                                            modelRTRFolder.File_Suffix = "testPaper";
                                            // modelRTRFolder.Resource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                            modelRTRFolder.Book_ID = modelResFolder.Book_ID;
                                            modelRTRFolder.Resource_Domain = "";
                                            modelRTRFolder.ParticularYear = modelResFolder.ParticularYear;
                                            modelRTRFolder.GradeTerm = modelResFolder.GradeTerm;
                                            modelRTRFolder.Resource_Version = modelResFolder.Resource_Version;
                                            modelRTRFolder.Subject = Resource_Class == Resource_ClassConst.自有资源 ? modelFUser.Subject : modelResFolder.Subject;
                                            #endregion
                                            #endregion
                                        }
                                        else
                                        {
                                            #region 修改
                                            #region 资源文件夹关系表
                                            modelRTRFolder = new BLL_ResourceToResourceFolder().GetModel(resourceId);
                                            strResourceToResourceFolder_Id = resourceId;
                                            modelRTRFolder.File_Name = title;
                                            modelRTRFolder.Resource_Name = title;
                                            modelRTRFolder.ResourceToResourceFolder_Order = order;
                                            strResourceToResourceFolder_Id = modelRTRFolder.ResourceToResourceFolder_Id;
                                            modelRTRFolder.CreateTime = CreateTime;
                                            modelRTRFolder.UpdateTime = DateTime.Now;
                                            Resource_Class = modelRTRFolder.Resource_Class;
                                            #endregion
                                            #region 资源表
                                            modelResource = new BLL_Resource().GetModel(modelRTRFolder.Resource_Id);
                                            //byte[] bytes = StreamToBytes(resStream);
                                            modelResource.Resource_MD5 = clsUtility.GetMd5(resInfo);
                                            //modelResource.Resource_DataStrem = "";
                                            //modelResource.Resource_ContentHtml = "";
                                            modelResource.Resource_ContentLength = resInfo.Length;
                                            #endregion

                                            #endregion
                                        }
                                        #region 试卷属性表
                                        Model_ResourceToResourceFolder_Property modelRTRFPropety = new Model_ResourceToResourceFolder_Property();
                                        modelRTRFPropety.ResourceToResourceFolder_Id = modelRTRFolder.ResourceToResourceFolder_Id;
                                        modelRTRFPropety.BooksCode = resModel.booksCode;
                                        modelRTRFPropety.BooksUnitCode = resModel.booksUnitCode;
                                        modelRTRFPropety.GuidDoc = resModel.guidDoc;
                                        modelRTRFPropety.TestPaperName = resModel.testPaperName;
                                        modelRTRFPropety.CreateTime = DateTime.Now;
                                        modelRTRFPropety.paperHeaderDoc = resModel.paperHeaderDoc;
                                        modelRTRFPropety.paperHeaderHtml = resModel.paperHeaderHtml;
                                        #endregion
                                        //生成存储路径
                                        string savePath = string.Empty;
                                        string savePathOwn = string.Empty;
                                        if (Resource_Class == Resource_ClassConst.云资源)
                                        {

                                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRFolder.ParticularYear, modelRTRFolder.GradeTerm,
                                               modelRTRFolder.Resource_Version, modelRTRFolder.Subject);
                                        }
                                        if (Resource_Class == Resource_ClassConst.自有资源)
                                        {
                                            savePathOwn = string.Format("{0}\\", CreateTime.ToString("yyyy-MM-dd"));
                                        }
                                        modelRTRFolder.Resource_Url = string.Format("{0}testPaperFile\\{1}{2}.dsc", savePathOwn, savePath, strResourceToResourceFolder_Id);//资源对应资源文件夹关系表 存储文件路径

                                        #region 试题表
                                        List<Model_TestQuestions> listModelTQ = new List<Model_TestQuestions>();//试题List
                                        List<Model_TestQuestions_Score> listModelTQScore = new List<Model_TestQuestions_Score>();//试题分值List
                                        List<Model_TestQuestions_Option> listModelTQOption = new List<Model_TestQuestions_Option>();//试题选项List

                                        int tqNum = 0;
                                        #region 普通题型 list
                                        List<Rc.Interface.AnswerJson> listAnswerModel = new List<Rc.Interface.AnswerJson>();
                                        listAnswerModel = resModel.list;

                                        if (listAnswerModel != null)
                                        {
                                            foreach (var item in listAnswerModel)
                                            {
                                                if (item != null)
                                                {
                                                    string strTestQuestions_Id = Guid.NewGuid().ToString();
                                                    tqNum++;
                                                    Model_TestQuestions modelTQ = new Model_TestQuestions();
                                                    modelTQ.Parent_Id = "";
                                                    modelTQ.TestQuestions_Id = strTestQuestions_Id;
                                                    modelTQ.ResourceToResourceFolder_Id = modelRTRFolder.ResourceToResourceFolder_Id;
                                                    modelTQ.TestQuestions_Num = tqNum;
                                                    modelTQ.TestQuestions_Type = item.testType;
                                                    //modelTQ.TestQuestions_Content = item.docBase64;//数据不存储到数据库，已存储到文件
                                                    modelTQ.CreateTime = DateTime.Now;
                                                    modelTQ.topicNumber = item.topicNumber;
                                                    modelTQ.type = "simple";
                                                    decimal? sumScore = 0;
                                                    #region 题干 保存文件
                                                    string testQuestionBodyUrl = uploadPath + "{0}{1}\\" + savePath + strTestQuestions_Id + ".{2}";//文件详细路径(改成分的ID，文件名称)
                                                    pfunction.WriteToFile(string.Format(testQuestionBodyUrl, savePathOwn, "testQuestionBody", "txt"), item.docBase64, true);
                                                    pfunction.WriteToFile(string.Format(testQuestionBodyUrl, savePathOwn, "testQuestionBody", "htm"), item.docHtml, true);
                                                    #endregion
                                                    #region 试题分值表
                                                    List<AnswerScoreJson> AnswerScoreList = new List<AnswerScoreJson>();
                                                    AnswerScoreList = item.list;
                                                    //listAnswerScore Model = new listAnswerScore();
                                                    if (AnswerScoreList != null)
                                                    {
                                                        int scoreNum = 0;
                                                        foreach (var itemScore in AnswerScoreList)
                                                        {
                                                            decimal tempScore = 0;
                                                            string scoreText = "0";
                                                            scoreText = itemScore.scoreText;

                                                            decimal.TryParse(scoreText, out tempScore);
                                                            if (tempScore >= 0) sumScore += tempScore;
                                                            scoreNum++;
                                                            Model_TestQuestions_Score modelTQScore = new Model_TestQuestions_Score();
                                                            string TQ_Score_ID = string.Empty;
                                                            if (operateMode == "update")
                                                            {
                                                                #region 修改的时候 处理解析
                                                                string analyzeHyperlink = itemScore.analyzeHyperlink;
                                                                if (analyzeHyperlink.IndexOf("http://") != -1)//修改的时候 获取原有TQ_Score_Id
                                                                {
                                                                    TQ_Score_ID = analyzeHyperlink.Substring(analyzeHyperlink.IndexOf("testId=") + 7, analyzeHyperlink.IndexOf("&attrType=") - analyzeHyperlink.IndexOf("testId=") - 7);//第一次存储的解析txt文件名
                                                                    string strAnalyzeUrl = string.Format("/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType={2}"
                                                                      , modelRTRFolder.ResourceToResourceFolder_Id, TQ_Score_ID, "AnalyzeData");
                                                                    modelTQScore.AnalyzeHyperlinkData = strAnalyzeUrl;
                                                                }
                                                                string trainHyperlink = itemScore.trainHyperlink;
                                                                if (trainHyperlink.IndexOf("http://") != -1)//修改的时候 处理原有的强化训练URL
                                                                {
                                                                    if (string.IsNullOrEmpty(TQ_Score_ID)) TQ_Score_ID = trainHyperlink.Substring(trainHyperlink.IndexOf("testId=") + 7, trainHyperlink.IndexOf("&attrType=") - trainHyperlink.IndexOf("testId=") - 7);//第一次存储的强化训练txt文件名
                                                                    string strTrainUrl = string.Format("/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType={2}"
                                                                      , modelRTRFolder.ResourceToResourceFolder_Id, TQ_Score_ID, "TrainData");
                                                                    modelTQScore.TrainHyperlinkData = strTrainUrl;
                                                                }
                                                                #endregion
                                                            }
                                                            if (string.IsNullOrEmpty(TQ_Score_ID))
                                                            {
                                                                TQ_Score_ID = Guid.NewGuid().ToString();
                                                            }
                                                            modelTQScore.TestQuestions_Score_ID = TQ_Score_ID;
                                                            modelTQScore.ResourceToResourceFolder_Id = modelRTRFolder.ResourceToResourceFolder_Id;
                                                            modelTQScore.TestQuestions_Id = strTestQuestions_Id;
                                                            modelTQScore.TestQuestions_Num = tqNum;
                                                            modelTQScore.TestQuestions_OrderNum = scoreNum;
                                                            modelTQScore.TestQuestions_Score = tempScore > 999 ? 999 : tempScore;
                                                            //modelTQScore.AnalyzeHyperlink = itemScore.analyzeHyperlink;
                                                            //modelTQScore.AnalyzeHyperlinkData = item.analyzeHyperlinkData;//数据不存储到数据库，已存储到文件
                                                            //modelTQScore.AnalyzeHyperlinkHtml = item.analyzeHyperlinkHtml;//数据不存储到数据库，已存储到文件
                                                            modelTQScore.AnalyzeText = itemScore.analyzeText;
                                                            modelTQScore.ComplexityHyperlink = itemScore.complexityHyperlink;
                                                            modelTQScore.ComplexityText = itemScore.complexityText;
                                                            modelTQScore.ContentHyperlink = itemScore.contentHyperlink;
                                                            modelTQScore.ContentText = "";
                                                            if (!string.IsNullOrEmpty(itemScore.contentText))
                                                            {
                                                                string strContentText = itemScore.contentText;//.Replace("'", "").Replace("\"", "");
                                                                strContentText = Regex.Replace(strContentText, @"[']", "");
                                                                strContentText = Regex.Replace(strContentText, "[\"]", "");
                                                                strContentText = Regex.Replace(strContentText, @"[\n\r]", "");
                                                                //strContentText = Regex.Replace(strContentText, @"[\r\n]", "");
                                                                modelTQScore.ContentText = strContentText;
                                                            }
                                                            //modelTQScore.DocBase64 = item.docBase64;//数据不存储到数据库，已存储到文件
                                                            //modelTQScore.DocHtml = item.docHtml;//数据不存储到数据库，已存储到文件
                                                            modelTQScore.ScoreHyperlink = itemScore.scoreHyperlink;
                                                            modelTQScore.ScoreText = tempScore.ToString();// itemScore
                                                            modelTQScore.TargetHyperlink = itemScore.targetHyperlink;
                                                            modelTQScore.TargetText = itemScore.targetText;
                                                            modelTQScore.AreaHyperlink = itemScore.areaHyperlink;
                                                            modelTQScore.AreaText = itemScore.areaText;
                                                            string fileUrl = uploadPath + "{0}{1}\\" + savePath + TQ_Score_ID + ".{2}";//文件详细路径(改成分的ID，文件名称)
                                                            if (item.testType == "selection" || item.testType == "clozeTest")//选择题、完形填空题 判断题答案保存到数据库，填空题 解答题答案保存文件
                                                            {
                                                                modelTQScore.TestCorrect = itemScore.testCorrect;
                                                            }
                                                            else if (item.testType == "truefalse")
                                                            {
                                                                modelTQScore.TestCorrect = itemScore.testCorrectHTML;
                                                            }
                                                            else//填空题 解答题答案保存成文件
                                                            {
                                                                #region 标准答案保存文件
                                                                if (!String.IsNullOrEmpty(itemScore.testCorrectHTML)) pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "testQuestionCurrent", "txt"), itemScore.testCorrectHTML, true);
                                                                #endregion

                                                            }
                                                            #region 选择题的选项 保存文件
                                                            if (itemScore.testSelections != null) pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "testQuestionOption", "txt"), Newtonsoft.Json.JsonConvert.SerializeObject(itemScore.testSelections), true);
                                                            #endregion
                                                            #region 解析 保存文件
                                                            if (!String.IsNullOrEmpty(itemScore.analyzeHyperlinkData))
                                                            {
                                                                pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "AnalyzeData", "txt"), itemScore.analyzeHyperlinkData, true);
                                                                pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "AnalyzeHtml", "htm"), itemScore.analyzeHyperlinkHtml, true);
                                                                string strAnalyzeUrl = string.Format("/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType={2}"
                                                                  , modelRTRFolder.ResourceToResourceFolder_Id, TQ_Score_ID, "AnalyzeData");
                                                                modelTQScore.AnalyzeHyperlinkData = strAnalyzeUrl;//获取试卷解析所需URL
                                                            }
                                                            #endregion
                                                            #region 强化训练 保存文件
                                                            if (!String.IsNullOrEmpty(itemScore.trainHyperlinkData))
                                                            {
                                                                pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "TrainData", "txt"), itemScore.trainHyperlinkData, true);
                                                                pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "TrainHtml", "htm"), itemScore.trainHyperlinkHtml, true);
                                                                string strTrainUrl = string.Format("/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType={2}"
                                                                  , modelRTRFolder.ResourceToResourceFolder_Id, TQ_Score_ID, "TrainData");
                                                                modelTQScore.TrainHyperlinkData = strTrainUrl;//获取试卷强化训练所需URL
                                                            }
                                                            #endregion
                                                            #region 子题题干Html 保存文件
                                                            if (!String.IsNullOrEmpty(itemScore.bodySubHtml)) pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "bodySub", "txt"), itemScore.bodySubHtml, true);
                                                            #endregion
                                                            modelTQScore.TestType = item.testType;
                                                            //modelTQScore.TrainHyperlink = itemScore.trainHyperlink;
                                                            //modelTQScore.TrainHyperlinkData = item.trainHyperlinkData;//数据不存储到数据库，已存储到文件
                                                            //modelTQScore.TrainHyperlinkHtml = item.trainHyperlinkHtml;//数据不存储到数据库，已存储到文件
                                                            modelTQScore.TrainText = itemScore.trainText;
                                                            modelTQScore.TypeHyperlink = itemScore.typeHyperlink;
                                                            modelTQScore.TypeText = itemScore.typeText;
                                                            modelTQScore.CreateTime = DateTime.Now;
                                                            modelTQScore.kaofaText = itemScore.kaofaText;
                                                            modelTQScore.testIndex = itemScore.testIndex;
                                                            listModelTQScore.Add(modelTQScore);
                                                            #region 试题选项表
                                                            List<Rc.Interface.TestSelections> listTestSelections = new List<Rc.Interface.TestSelections>();
                                                            listTestSelections = itemScore.testSelections;
                                                            if (listTestSelections != null)
                                                            {
                                                                int selNum = 0;
                                                                foreach (var itemSelections in listTestSelections)
                                                                {
                                                                    selNum++;
                                                                    Model_TestQuestions_Option modelTQOption = new Model_TestQuestions_Option();
                                                                    modelTQOption.TestQuestions_Option_Id = Guid.NewGuid().ToString();
                                                                    modelTQOption.TestQuestions_Id = modelTQ.TestQuestions_Id;
                                                                    modelTQOption.TestQuestions_OptionParent_OrderNum = scoreNum;
                                                                    modelTQOption.TestQuestions_Option_Content = itemSelections.selectionText;//存储选择题选项数据：A，B...
                                                                    modelTQOption.TestQuestions_Option_OrderNum = selNum;
                                                                    modelTQOption.CreateTime = DateTime.Now;
                                                                    modelTQOption.TestQuestions_Score_ID = TQ_Score_ID;
                                                                    listModelTQOption.Add(modelTQOption);
                                                                }
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                    #endregion

                                                    modelTQ.TestQuestions_SumScore = sumScore > 999 ? 999 : sumScore;
                                                    listModelTQ.Add(modelTQ);
                                                }
                                            }
                                        }
                                        #endregion
                                        #region 综合题型 listBig
                                        if (resModel.listBig != null)
                                        {
                                            foreach (var itemBig in resModel.listBig)
                                            {
                                                if (itemBig != null)
                                                {
                                                    #region 综合题题干
                                                    tqNum++;
                                                    string strTestQuestions_Id_Big = Guid.NewGuid().ToString();
                                                    Model_TestQuestions modelTQ_Big = new Model_TestQuestions();
                                                    modelTQ_Big.Parent_Id = "0";
                                                    modelTQ_Big.TestQuestions_Id = strTestQuestions_Id_Big;
                                                    modelTQ_Big.ResourceToResourceFolder_Id = modelRTRFolder.ResourceToResourceFolder_Id;
                                                    modelTQ_Big.TestQuestions_Num = tqNum;
                                                    modelTQ_Big.TestQuestions_Type = "";
                                                    modelTQ_Big.CreateTime = DateTime.Now;
                                                    modelTQ_Big.topicNumber = itemBig.topicNumber;
                                                    modelTQ_Big.type = itemBig.type;

                                                    #region 题干 保存文件
                                                    string testQuestionBodyUrl_Big = uploadPath + "{0}{1}\\" + savePath + strTestQuestions_Id_Big + ".{2}";//文件详细路径(改成分的ID，文件名称)
                                                    pfunction.WriteToFile(string.Format(testQuestionBodyUrl_Big, savePathOwn, "testQuestionBody", "txt"), itemBig.docBase64, true);
                                                    pfunction.WriteToFile(string.Format(testQuestionBodyUrl_Big, savePathOwn, "testQuestionBody", "htm"), itemBig.docHtml, true);
                                                    #endregion
                                                    #endregion

                                                    decimal? BigSumScore = 0;
                                                    if (itemBig.list != null)
                                                    {
                                                        #region 综合题 试题
                                                        foreach (var item in itemBig.list)
                                                        {
                                                            if (item != null)
                                                            {
                                                                string strTestQuestions_Id = Guid.NewGuid().ToString();
                                                                tqNum++;
                                                                Model_TestQuestions modelTQ = new Model_TestQuestions();
                                                                modelTQ.Parent_Id = strTestQuestions_Id_Big;
                                                                modelTQ.TestQuestions_Id = strTestQuestions_Id;
                                                                modelTQ.ResourceToResourceFolder_Id = modelRTRFolder.ResourceToResourceFolder_Id;
                                                                modelTQ.TestQuestions_Num = tqNum;
                                                                modelTQ.TestQuestions_Type = item.testType;
                                                                //modelTQ.TestQuestions_Content = item.docBase64;//数据不存储到数据库，已存储到文件
                                                                modelTQ.CreateTime = DateTime.Now;
                                                                modelTQ.topicNumber = string.IsNullOrEmpty(item.topicNumber) ? itemBig.topicNumber : item.topicNumber;
                                                                modelTQ.type = itemBig.type;
                                                                decimal? sumScore = 0;
                                                                #region 题干 保存文件
                                                                string testQuestionBodyUrl = uploadPath + "{0}{1}\\" + savePath + strTestQuestions_Id + ".{2}";//文件详细路径(改成分的ID，文件名称)
                                                                pfunction.WriteToFile(string.Format(testQuestionBodyUrl, savePathOwn, "testQuestionBody", "txt"), item.docBase64, true);
                                                                pfunction.WriteToFile(string.Format(testQuestionBodyUrl, savePathOwn, "testQuestionBody", "htm"), item.docHtml, true);
                                                                #endregion
                                                                #region 试题分值表
                                                                List<AnswerScoreJson> AnswerScoreList = new List<AnswerScoreJson>();
                                                                AnswerScoreList = item.list;
                                                                //listAnswerScore Model = new listAnswerScore();
                                                                if (AnswerScoreList != null)
                                                                {
                                                                    int scoreNum = 0;
                                                                    foreach (var itemScore in AnswerScoreList)
                                                                    {
                                                                        decimal tempScore = 0;
                                                                        string scoreText = "0";
                                                                        scoreText = itemScore.scoreText;

                                                                        decimal.TryParse(scoreText, out tempScore);
                                                                        if (tempScore >= 0) sumScore += tempScore;
                                                                        scoreNum++;
                                                                        Model_TestQuestions_Score modelTQScore = new Model_TestQuestions_Score();
                                                                        string TQ_Score_ID = string.Empty;
                                                                        if (operateMode == "update")
                                                                        {
                                                                            #region 修改的时候 处理解析
                                                                            string analyzeHyperlink = itemScore.analyzeHyperlink;
                                                                            if (analyzeHyperlink.IndexOf("http://") != -1)//修改的时候 获取原有TQ_Score_Id
                                                                            {
                                                                                TQ_Score_ID = analyzeHyperlink.Substring(analyzeHyperlink.IndexOf("testId=") + 7, analyzeHyperlink.IndexOf("&attrType=") - analyzeHyperlink.IndexOf("testId=") - 7);//第一次存储的解析txt文件名
                                                                                string strAnalyzeUrl = string.Format("/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType={2}"
                                                                                  , modelRTRFolder.ResourceToResourceFolder_Id, TQ_Score_ID, "AnalyzeData");
                                                                                modelTQScore.AnalyzeHyperlinkData = strAnalyzeUrl;
                                                                            }
                                                                            string trainHyperlink = itemScore.trainHyperlink;
                                                                            if (trainHyperlink.IndexOf("http://") != -1)//修改的时候 处理原有的强化训练URL
                                                                            {
                                                                                if (string.IsNullOrEmpty(TQ_Score_ID)) TQ_Score_ID = trainHyperlink.Substring(trainHyperlink.IndexOf("testId=") + 7, trainHyperlink.IndexOf("&attrType=") - trainHyperlink.IndexOf("testId=") - 7);//第一次存储的强化训练txt文件名
                                                                                string strTrainUrl = string.Format("/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType={2}"
                                                                                  , modelRTRFolder.ResourceToResourceFolder_Id, TQ_Score_ID, "TrainData");
                                                                                modelTQScore.TrainHyperlinkData = strTrainUrl;
                                                                            }
                                                                            #endregion
                                                                        }
                                                                        if (string.IsNullOrEmpty(TQ_Score_ID))
                                                                        {
                                                                            TQ_Score_ID = Guid.NewGuid().ToString();
                                                                        }
                                                                        modelTQScore.TestQuestions_Score_ID = TQ_Score_ID;
                                                                        modelTQScore.ResourceToResourceFolder_Id = modelRTRFolder.ResourceToResourceFolder_Id;
                                                                        modelTQScore.TestQuestions_Id = strTestQuestions_Id;
                                                                        modelTQScore.TestQuestions_Num = tqNum;
                                                                        modelTQScore.TestQuestions_OrderNum = scoreNum;
                                                                        modelTQScore.TestQuestions_Score = tempScore > 999 ? 999 : tempScore;
                                                                        //modelTQScore.AnalyzeHyperlink = itemScore.analyzeHyperlink;
                                                                        //modelTQScore.AnalyzeHyperlinkData = item.analyzeHyperlinkData;//数据不存储到数据库，已存储到文件
                                                                        //modelTQScore.AnalyzeHyperlinkHtml = item.analyzeHyperlinkHtml;//数据不存储到数据库，已存储到文件
                                                                        modelTQScore.AnalyzeText = itemScore.analyzeText;
                                                                        modelTQScore.ComplexityHyperlink = itemScore.complexityHyperlink;
                                                                        modelTQScore.ComplexityText = itemScore.complexityText;
                                                                        modelTQScore.ContentHyperlink = itemScore.contentHyperlink;
                                                                        modelTQScore.ContentText = "";
                                                                        if (!string.IsNullOrEmpty(itemScore.contentText))
                                                                        {
                                                                            string strContentText = itemScore.contentText;//.Replace("'", "").Replace("\"", "");
                                                                            strContentText = Regex.Replace(strContentText, @"[']", "");
                                                                            strContentText = Regex.Replace(strContentText, "[\"]", "");
                                                                            strContentText = Regex.Replace(strContentText, @"[\n\r]", "");
                                                                            //strContentText = Regex.Replace(strContentText, @"[\r\n]", "");
                                                                            modelTQScore.ContentText = strContentText;
                                                                        }
                                                                        //modelTQScore.DocBase64 = item.docBase64;//数据不存储到数据库，已存储到文件
                                                                        //modelTQScore.DocHtml = item.docHtml;//数据不存储到数据库，已存储到文件
                                                                        modelTQScore.ScoreHyperlink = itemScore.scoreHyperlink;
                                                                        modelTQScore.ScoreText = tempScore.ToString();// itemScore
                                                                        modelTQScore.TargetHyperlink = itemScore.targetHyperlink;
                                                                        modelTQScore.TargetText = itemScore.targetText;
                                                                        modelTQScore.AreaHyperlink = itemScore.areaHyperlink;
                                                                        modelTQScore.AreaText = itemScore.areaText;
                                                                        string fileUrl = uploadPath + "{0}{1}\\" + savePath + TQ_Score_ID + ".{2}";//文件详细路径(改成分的ID，文件名称)
                                                                        if (item.testType == "selection" || item.testType == "clozeTest")//选择题、完形填空题 判断题答案保存到数据库，填空题 解答题答案保存文件
                                                                        {
                                                                            modelTQScore.TestCorrect = itemScore.testCorrect;
                                                                        }
                                                                        else if (item.testType == "truefalse")
                                                                        {
                                                                            modelTQScore.TestCorrect = itemScore.testCorrectHTML;
                                                                        }
                                                                        else//填空题 解答题答案保存成文件
                                                                        {
                                                                            #region 标准答案保存文件
                                                                            if (!String.IsNullOrEmpty(itemScore.testCorrectHTML)) pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "testQuestionCurrent", "txt"), itemScore.testCorrectHTML, true);
                                                                            #endregion

                                                                        }
                                                                        #region 选择题的选项 保存文件
                                                                        if (itemScore.testSelections != null) pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "testQuestionOption", "txt"), Newtonsoft.Json.JsonConvert.SerializeObject(itemScore.testSelections), true);
                                                                        #endregion
                                                                        #region 解析 保存文件
                                                                        if (!String.IsNullOrEmpty(itemScore.analyzeHyperlinkData))
                                                                        {
                                                                            pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "AnalyzeData", "txt"), itemScore.analyzeHyperlinkData, true);
                                                                            pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "AnalyzeHtml", "htm"), itemScore.analyzeHyperlinkHtml, true);
                                                                            string strAnalyzeUrl = string.Format("/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType={2}"
                                                                              , modelRTRFolder.ResourceToResourceFolder_Id, TQ_Score_ID, "AnalyzeData");
                                                                            modelTQScore.AnalyzeHyperlinkData = strAnalyzeUrl;//获取试卷解析所需URL
                                                                        }
                                                                        #endregion
                                                                        #region 强化训练 保存文件
                                                                        if (!String.IsNullOrEmpty(itemScore.trainHyperlinkData))
                                                                        {
                                                                            pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "TrainData", "txt"), itemScore.trainHyperlinkData, true);
                                                                            pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "TrainHtml", "htm"), itemScore.trainHyperlinkHtml, true);
                                                                            string strTrainUrl = string.Format("/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType={2}"
                                                                              , modelRTRFolder.ResourceToResourceFolder_Id, TQ_Score_ID, "TrainData");
                                                                            modelTQScore.TrainHyperlinkData = strTrainUrl;//获取试卷强化训练所需URL
                                                                        }
                                                                        #endregion
                                                                        #region 子题题干Html 保存文件
                                                                        if (!String.IsNullOrEmpty(itemScore.bodySubHtml)) pfunction.WriteToFile(string.Format(fileUrl, savePathOwn, "bodySub", "txt"), itemScore.bodySubHtml, true);
                                                                        #endregion
                                                                        modelTQScore.TestType = item.testType;
                                                                        //modelTQScore.TrainHyperlink = itemScore.trainHyperlink;
                                                                        //modelTQScore.TrainHyperlinkData = item.trainHyperlinkData;//数据不存储到数据库，已存储到文件
                                                                        //modelTQScore.TrainHyperlinkHtml = item.trainHyperlinkHtml;//数据不存储到数据库，已存储到文件
                                                                        modelTQScore.TrainText = itemScore.trainText;
                                                                        modelTQScore.TypeHyperlink = itemScore.typeHyperlink;
                                                                        modelTQScore.TypeText = itemScore.typeText;
                                                                        modelTQScore.CreateTime = DateTime.Now;
                                                                        modelTQScore.kaofaText = itemScore.kaofaText;
                                                                        modelTQScore.testIndex = itemScore.testIndex;
                                                                        listModelTQScore.Add(modelTQScore);
                                                                        #region 试题选项表
                                                                        List<Rc.Interface.TestSelections> listTestSelections = new List<Rc.Interface.TestSelections>();
                                                                        listTestSelections = itemScore.testSelections;
                                                                        if (listTestSelections != null)
                                                                        {
                                                                            int selNum = 0;
                                                                            foreach (var itemSelections in listTestSelections)
                                                                            {
                                                                                selNum++;
                                                                                Model_TestQuestions_Option modelTQOption = new Model_TestQuestions_Option();
                                                                                modelTQOption.TestQuestions_Option_Id = Guid.NewGuid().ToString();
                                                                                modelTQOption.TestQuestions_Id = modelTQ.TestQuestions_Id;
                                                                                modelTQOption.TestQuestions_OptionParent_OrderNum = scoreNum;
                                                                                modelTQOption.TestQuestions_Option_Content = itemSelections.selectionText;//存储选择题选项数据：A，B...
                                                                                modelTQOption.TestQuestions_Option_OrderNum = selNum;
                                                                                modelTQOption.CreateTime = DateTime.Now;
                                                                                modelTQOption.TestQuestions_Score_ID = TQ_Score_ID;
                                                                                listModelTQOption.Add(modelTQOption);
                                                                            }
                                                                        }
                                                                        #endregion
                                                                    }
                                                                }
                                                                #endregion

                                                                modelTQ.TestQuestions_SumScore = sumScore > 999 ? 999 : sumScore;
                                                                listModelTQ.Add(modelTQ);

                                                                BigSumScore += sumScore;
                                                            }
                                                        }
                                                        #endregion
                                                    }

                                                    modelTQ_Big.TestQuestions_SumScore = BigSumScore > 999 ? 999 : BigSumScore;
                                                    listModelTQ.Add(modelTQ_Big);
                                                }

                                            }
                                        }
                                        #endregion
                                        #endregion

                                        #region 图书生产日志
                                        Model_BookProductionLog modelBPL = new Model_BookProductionLog();
                                        modelBPL.BookProductionLog_Id = Guid.NewGuid().ToString();
                                        modelBPL.BookId = modelRTRFolder.Book_ID;
                                        modelBPL.ResourceToResourceFolder_Id = modelRTRFolder.ResourceToResourceFolder_Id;
                                        modelBPL.ParticularYear = Convert.ToInt16(modelRTRFolder.ParticularYear);
                                        modelBPL.Resource_Type = modelRTRFolder.Resource_Type;
                                        modelBPL.LogTypeEnum = (operateMode == "add") ? "1" : "2";//1添加,2修改
                                        modelBPL.CreateUser = userId;
                                        modelBPL.CreateTime = DateTime.Now;
                                        #endregion

                                        int executeCount = 0;
                                        if (operateMode == "add")
                                        {
                                            executeCount = new BLL_Resource().ClientUploadTestPaper(modelResource, modelRTRFolder, modelRTRFPropety, listModelTQ, listModelTQScore, listModelTQOption, modelBPL);
                                        }
                                        else
                                        {
                                            executeCount = new BLL_Resource().ClientUploadTestPaperUpdate(modelResource, modelRTRFolder, modelRTRFPropety, listModelTQ, listModelTQScore, listModelTQOption, modelBPL);

                                        }
                                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(resourceId, "", string.Format("完成处理数据|操作人{0}|试卷Id{1}|方法{2}", userId, resourceId, "testlibrarysubmit"));
                                        if (executeCount == 0)
                                        {
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = false,
                                                errorMsg = "试卷上传失败",
                                                errorCode = "testLibrarySubmit"
                                            });
                                        }
                                        else
                                        {
                                            #region 记录教案访问情况
                                            Model_visit_client modelVC = new Model_visit_client();
                                            modelVC.visit_client_id = Guid.NewGuid().ToString();
                                            modelVC.user_id = userId;
                                            modelVC.resource_data_id = strResourceToResourceFolder_Id;
                                            modelVC.product_type = productType;
                                            modelVC.tab_id = tabId;
                                            modelVC.open_time = DateTime.Now;
                                            modelVC.operate_type = "create";
                                            new BLL_visit_client().Add(modelVC);
                                            #endregion
                                            #region 老师自有习题集,上传成功后，保存txt
                                            if (modelRTRFolder.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                                            {
                                                uploadPath = "..\\Upload\\Resource\\teacherPaper\\";//存储文件基础路径
                                                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(strResourceToResourceFolder_Id);
                                                string filePath = pfunction.ConvertToLongDateTime(modelRTRF.CreateTime.ToString(), "yyyy-MM-dd") + "\\" +
                                                    strResourceToResourceFolder_Id + ".txt";
                                                BLL_HW_TestPaper bllHWTP = new BLL_HW_TestPaper();
                                                Model_HW_TestPaper modelHWTP = new Model_HW_TestPaper();
                                                modelHWTP = bllHWTP.GetModel(strResourceToResourceFolder_Id);
                                                if (modelHWTP == null)
                                                {
                                                    modelHWTP = new Model_HW_TestPaper();
                                                    modelHWTP.HW_TestPaper_Id = strResourceToResourceFolder_Id;
                                                    modelHWTP.TestPaper_Path = uploadPath + filePath;
                                                    modelHWTP.TestPaper_Status = "0";
                                                    modelHWTP.CreateTime = DateTime.Now;
                                                    bllHWTP.Add(modelHWTP);

                                                    GenerateTestPaperFileForTeacher_API(strResourceToResourceFolder_Id);

                                                    modelHWTP.TestPaper_Status = "1";
                                                    bllHWTP.Update(modelHWTP);

                                                }
                                                else
                                                {
                                                    GenerateTestPaperFileForTeacher_API(strResourceToResourceFolder_Id);
                                                    modelHWTP.TestPaper_Status = "1";
                                                    bllHWTP.Update(modelHWTP);
                                                }
                                            }

                                            #endregion
                                            if (operateMode == "add")
                                            {
                                                #region 记录需要同步的数据
                                                Model_SyncData modelSD = new Model_SyncData();
                                                modelSD.SyncDataId = Guid.NewGuid().ToString();
                                                modelSD.TableName = "ResourceToResourceFolder";
                                                modelSD.DataId = strResourceToResourceFolder_Id;
                                                modelSD.OperateType = "add";
                                                modelSD.CreateTime = DateTime.Now;
                                                modelSD.SyncStatus = "0";
                                                new BLL_SyncData().Add(modelSD);
                                                #endregion
                                            }
                                            else
                                            {
                                                #region 记录需要同步的数据
                                                Model_SyncData modelSD = new Model_SyncData();
                                                modelSD.SyncDataId = Guid.NewGuid().ToString();
                                                modelSD.TableName = "ResourceToResourceFolder";
                                                modelSD.DataId = strResourceToResourceFolder_Id;
                                                modelSD.OperateType = "modify";
                                                modelSD.CreateTime = DateTime.Now;
                                                modelSD.SyncStatus = "0";
                                                new BLL_SyncData().Add(modelSD);
                                                #endregion
                                            }
                                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                            {
                                                status = true,
                                                errorMsg = "",
                                                errorCode = ""
                                            });
                                        }
                                    }
                                    else
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "试卷上传失败",
                                            errorCode = "testLibrarySubmit"
                                        });
                                    }
                                }
                                #endregion


                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "testLibrarySubmit",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "testLibrarySubmit"
                            });
                        }

                        sr.Close();
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "testLibrarySubmit"
                        });
                    }
                    finally
                    {
                        Response.Write(strJsion);
                    }

                    break;
                #endregion
                #region 接口名称: testPaperAnswerSubmit 提交测评结果
                case "testpaperanswersubmit":
                    try
                    {
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string resourceId = Request.QueryString["resourceId"].Filter();//此ID为Student_HomeWork_Id学生作业
                        string tabId = Request.QueryString["tabId"].Filter();
                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(resourceId, "", string.Format("开始处理学生提交作业|操作人{0}|学生作业Id{1}|方法{2}", userId, resourceId, "testpaperanswersubmit"));
                        Stream resStream = HttpContext.Current.Request.InputStream;
                        StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
                        string testJsion = sr.ReadToEnd();
                        string resInfo = testJsion;
                        //把数据流保存到文件
                        string logPath = string.Format("/Upload/AnswerSubmitLog/{0}/{1}.txt", DateTime.Now.ToString("yyyy-MM-dd"), resourceId);
                        pfunction.WriteToFile(Server.MapPath(logPath), resInfo, true);


                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(resInfo) && new BLL_Student_HomeWork().GetRecordCount("Student_Id='" + userId + "' AND Student_HomeWork_Id='" + resourceId + "'") == 1)
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                Model_Student_HomeWork modelSHW = new BLL_Student_HomeWork().GetModel(resourceId);
                                string savePath = string.Empty;
                                Model_HomeWork modelHW = new BLL_HomeWork().GetModel(modelSHW.HomeWork_Id);
                                string homeWorkId = modelHW.HomeWork_Id;
                                string rtrfId = modelHW.ResourceToResourceFolder_Id;
                                Model_ResourceToResourceFolder modelRTRFolder = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                                Rc.Interface.testPaperAnswerSubmitModel resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Rc.Interface.testPaperAnswerSubmitModel>(resInfo);

                                if (modelRTRFolder.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                                {
                                    savePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\"
                                       , pfunction.ToShortDate(modelHW.CreateTime.ToString())//作业布置时间
                                       , modelRTRFolder.ParticularYear, modelRTRFolder.GradeTerm
                                       , modelRTRFolder.Resource_Version, modelRTRFolder.Subject);
                                }
                                if (modelRTRFolder.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                                {
                                    savePath = string.Format("{0}\\"
                                        , pfunction.ToShortDate(modelHW.CreateTime.ToString()));//作业布置时间);
                                }
                                Model_Student_HomeWork_Submit modelShwSubmit = new Model_Student_HomeWork_Submit();
                                modelShwSubmit = new BLL_Student_HomeWork_Submit().GetModel(modelSHW.Student_HomeWork_Id);

                                if (modelShwSubmit.Student_HomeWork_Status != 0) // 如果学生已作答，则不再操作数据库
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = true,
                                        errorMsg = "",
                                        errorCode = ""
                                    });
                                }
                                else
                                {
                                    modelShwSubmit.Student_HomeWork_Status = 2; //学生作业状态更改为待提交
                                    modelShwSubmit.StudentIP = pfunction.GetRealIP();
                                    modelShwSubmit.Student_Answer_Time = DateTime.Now;

                                    if (new BLL_Student_HomeWork_Submit().Update(modelShwSubmit) == false)
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "答题失败",
                                            errorCode = "testPaperAnswerSubmit"
                                        });
                                    }
                                    else
                                    {
                                        try
                                        {
                                            #region 记录教案访问情况
                                            Model_visit_client modelVC = new Model_visit_client();
                                            BLL_visit_client bllVC = new BLL_visit_client();
                                            modelVC = bllVC.GetModelNew(userId, resourceId, tabId);
                                            if (modelVC != null)
                                            {
                                                modelVC.close_time = DateTime.Now;
                                                bllVC.Update(modelVC);
                                            }
                                            #endregion
                                            #region 保存学生答题json文件
                                            string savePathForSubmit = string.Format("{0}\\", pfunction.ToShortDate(modelHW.CreateTime.ToString()));
                                            savePathForSubmit = "{0}{1}\\" + savePathForSubmit + "{2}\\{3}.txt";
                                            pfunction.WriteToFile(string.Format(savePathForSubmit, uploadPath, "studentAnswerForSubmit", modelHW.HomeWork_Id, resourceId), resInfo, true);
                                            #endregion
                                        }
                                        catch (Exception)
                                        {

                                        }
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = true,
                                            errorMsg = "",
                                            errorCode = ""
                                        });
                                    }

                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(resourceId, "", string.Format("完成处理学生作业|操作人{0}|学生作业Id{1}|方法{2}", userId, resourceId, "testpaperanswersubmit"));
                                }
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "testPaperAnswerSubmit",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "testPaperAnswerSubmit"
                            });
                        }
                        sr.Close();
                    }
                    catch (Exception ex)
                    {
                        string strTemp = string.Empty;
                        strTemp = string.Format("{0};{1};{2};", ex.TargetSite.DeclaringType.ToString()
                , ex.TargetSite.Name.ToString(), ex.Message);
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = strTemp,
                            errorCode = "testPaperAnswerSubmit"

                        });
                        Stream resStream = HttpContext.Current.Request.InputStream;
                        StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(Request.QueryString["resourceId"].Filter(), ""
                            , string.Format("提交作业失败：|操作人{0}|学生作业Id{1}|方法{2}|错误信息{3}|数据流{4}"
                                , Request.QueryString["userId"].Filter()
                                , Request.QueryString["resourceId"].Filter()
                                , "testPaperAnswerSubmit"
                                , ex.Message.ToString()
                                , sr.ReadToEnd())
                            );
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: getSubmitResult 获取测评结果
                case "getsubmitresult":
                    try
                    {
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string resourceId = Request.QueryString["resourceId"].Filter();// resourceId为Student_HomeWork_Id
                        string strHomeWork_Name = string.Empty;
                        //string 

                        string Student_HomeWork_Id = resourceId;
                        string HomeWork_CreateTime = string.Empty;
                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                if (modelFUser.UserIdentity == "S")//学生 resourceId为Student_HomeWork_Id
                                {
                                    Model_Student_HomeWork modelSHW = new BLL_Student_HomeWork().GetModel(resourceId);
                                    Model_HomeWork modelHW = new BLL_HomeWork().GetModel(modelSHW.HomeWork_Id);
                                    HomeWork_CreateTime = pfunction.ToShortDate(modelHW.CreateTime.ToString());
                                    resourceId = modelHW.ResourceToResourceFolder_Id;
                                    strHomeWork_Name = modelHW.HomeWork_Name;
                                }
                                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(resourceId);


                                List<object> listObj = new List<object>();

                                List<object> listSelection = new List<object>();
                                List<object> listFill = new List<object>();
                                List<object> listAnswers = new List<object>();
                                List<object> listJudgment = new List<object>();
                                List<Model_TestQuestions> listModelTQ = new BLL_TestQuestions().GetModelList(" ResourceToResourceFolder_Id='" + resourceId + "' order by TestQuestions_Num ");
                                //int orderNum = 1;
                                string strKeySelection = "1";
                                string strFill = "4";
                                string strAnswers = "5";

                                foreach (var item in listModelTQ)
                                {
                                    switch (item.TestQuestions_Type)
                                    {
                                        case "selection":
                                        case "clozeTest":
                                            listSelection.Add(AddanswerResultListByType(modelRTRF, Student_HomeWork_Id, HomeWork_CreateTime, item, userId));
                                            // strKey = "1";
                                            break;
                                        case "truefalse":
                                            listJudgment.Add(AddanswerResultListByType(modelRTRF, Student_HomeWork_Id, HomeWork_CreateTime, item, userId));
                                            break;
                                        case "fill":
                                            listFill.Add(AddanswerResultListByType(modelRTRF, Student_HomeWork_Id, HomeWork_CreateTime, item, userId));
                                            // strKey = "4";
                                            break;
                                        case "answers":
                                            listAnswers.Add(AddanswerResultListByType(modelRTRF, Student_HomeWork_Id, HomeWork_CreateTime, item, userId));
                                            // strKey = "5";
                                            break;
                                    }
                                }
                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add(strKeySelection, listSelection);
                                dic.Add(strFill, listFill);
                                dic.Add(strAnswers, listAnswers);
                                listObj.Add(dic);

                                AnswerResultModel answerResultList = new AnswerResultModel();
                                answerResultList.SingleChoice = listSelection;
                                answerResultList.BlankFilling = listFill;
                                answerResultList.AnswerQuestions = listAnswers;
                                answerResultList.JudgmentQuestions = listJudgment;
                                //answerResultList.answerResultList = listObj;
                                if (listSelection.Count == 0 && listFill.Count == 0 && listAnswers.Count == 0 && listJudgment.Count == 0)
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = false,
                                        errorMsg = "获取数据失败",
                                        errorCode = "getSubmitResult"
                                    });
                                }
                                else
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = true,
                                        errorMsg = "",
                                        errorCode = "",
                                        //userName = pfunction.GetUserNameByUserId(userId),
                                        fileName = strHomeWork_Name,//modelRTRF.File_Name,
                                        answerResultList = answerResultList
                                    });

                                }
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "getSubmitResult",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "getSubmitResult"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "getSubmitResult"
                        });
                    }

                    //特殊处理
                    //strJsion = strJsion.Replace("Novoasoft_dxy", "1").Replace("Novoasoft_tkt", "4").Replace("Novoasoft_jdt", "5");
                    // string 1="";
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: getTestAttr 获取试题属性
                case "gettestattr":
                    string strAttrData = string.Empty;
                    try
                    {
                        strTestWebSiteUrl = pfunction.getHostPath();
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string resourceId = Request.QueryString["resourceId"].Filter();
                        string attrType = Request.QueryString["attrType"].Filter();
                        string testId = Request.QueryString["testId"].Filter();

                        Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(resourceId);
                        //生成存储路径
                        string savePath = string.Empty;
                        string saveOwnPath = string.Empty;
                        if (modelRTRF.Resource_Class == Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm,
                               modelRTRF.Resource_Version, modelRTRF.Subject);
                        }
                        if (modelRTRF.Resource_Class == Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                        }
                        strAttrData = RemotWeb.PostDataToServer(strTestWebSiteUrl + string.Format("/Upload/Resource/{0}{1}\\{2}\\{3}.txt", saveOwnPath, attrType, savePath, testId), "", Encoding.UTF8, "Get");

                    }
                    catch (Exception ex)
                    {
                        strAttrData = "错误：" + ex.ToString();


                    }
                    Response.Write(strAttrData);
                    break;
                #endregion
                #region 28. 接口名称：goWebPage 客户端跳转的WEB页面
                case "gowebpage":
                    strAttrData = string.Empty;
                    try
                    {
                        string strUrl = string.Empty;
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string pageType = string.Empty;//返回类型1: 学生已完成作业
                        string strError = string.Empty;//错误信息，非空时运行正常
                        if (!String.IsNullOrEmpty(Request["pageType"]))
                        {
                            pageType = Request["pageType"].ToString();

                        }
                        else
                        {
                            Response.Write("关键参数错误，请联系管理员处理。");
                        }
                        switch (pageType)
                        {
                            #region 学生已提交作业
                            case "1":
                                string shwid = string.Empty;//学生作业标识
                                string sid = string.Empty;//学生标识
                                string hwid = string.Empty;//作业标识
                                string retoreid = string.Empty;//作业对应文件的标识
                                if (!String.IsNullOrEmpty(Request["id"]))
                                {
                                    shwid = Request["id"].ToString();
                                }
                                else
                                {
                                    strError = "学生作业标识错误";
                                }

                                if (strError != string.Empty)
                                {
                                    Response.Write("参数有误，请联系管理员处理。");
                                    Response.End();
                                }
                                Rc.Model.Resources.Model_Student_HomeWork model = new Rc.Model.Resources.Model_Student_HomeWork();


                                try
                                {
                                    string strSql1 = string.Empty;
                                    strSql1 = string.Format(@"SELECT hw.ResourceToResourceFolder_Id,SHW.Student_Id,SHW.HomeWork_Id,SHW.Student_HomeWork_Id,shwCorrect.CorrectMode FROM HomeWork HW
INNER JOIN Student_HomeWork SHW ON HW.HomeWork_Id=SHW.HomeWork_Id
inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=SHW.Student_HomeWork_Id 
WHERE SHW.Student_HomeWork_Id='{0}'", shwid);
                                    DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql1).Tables[0];
                                    sid = dt.Rows[0]["Student_Id"].ToString();
                                    hwid = dt.Rows[0]["HomeWork_Id"].ToString();
                                    retoreid = dt.Rows[0]["ResourceToResourceFolder_Id"].ToString();

                                    if (dt.Rows[0]["CorrectMode"].ToString() == "1")//客户端批改
                                    {
                                        strUrl = string.Format("../student/ohomeworkview_client.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}&Student_HomeWork_Id={3}"
                                            , retoreid
                                            , hwid
                                            , sid
                                            , dt.Rows[0]["Student_HomeWork_Id"].ToString());
                                    }
                                    else//web端批改以及未批改
                                    {
                                        strUrl = string.Format("../student/OHomeWorkViewTT.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&StudentId={2}"
                                                                                , retoreid, hwid, sid);
                                    }
                                    Response.Redirect(strUrl);
                                    Response.End();
                                }
                                catch (Exception ex)
                                {
                                    Response.Write("意外错误，请联系管理员处理。" + ex.Message.ToString());
                                    Response.End();
                                    throw;
                                }


                                break;
                            #endregion
                            #region 老师讲评报告
                            case "CommentTestPaper":
                                string[] val = Request["id"].Split('^');//ResourceToResourceFolder_Id^HomeWork_Id^HomeWork_Name
                                if (val.Length == 3)
                                {
                                    Model_F_User loginModel = new Model_F_User();
                                    loginModel = new BLL_F_User().GetModelByUserIdToken(userId, token, productType);
                                    Session["FLoginUser"] = loginModel;
                                    strUrl = string.Format("../teacher/CheckCommentStatsHelper.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}&HomeWork_Name={2}"
                                    , val[0], val[1], val[2]);
                                    Response.Redirect(strUrl);
                                    Response.End();
                                }
                                break;
                            #endregion
                            default:
                                break;
                        }




                    }
                    catch (Exception ex)
                    {
                        strAttrData = "错误：" + ex.ToString();


                    }

                    break;
                #endregion
                #region 17. 接口名称: getTestPaper 获取试卷
                case "gettestpaper":
                    try
                    {
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string fileId = Request.QueryString["fileId"].Filter();
                        string tabid = Request.QueryString["tabId"].Filter();
                        string strHomeWork_Name = string.Empty;
                        int isTimeLimt = 0;
                        int isTimeLength = 0;
                        fileId = fileId.Filter();
                        string TeacherId = userId;
                        #region 获取JSON结构
                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                if (modelFUser.UserIdentity == "S")//学生 fileId为Student_HomeWork_Id 
                                {
                                    #region 学生

                                    string result = string.Empty;
                                    if (tabid != EnumTabindex.StudentSkillWrong.ToString()) result = GetTestPaperFileForStudent(fileId, userId, tabid);
                                    if (result == "error")
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "获取数据失败",
                                            errorCode = "getTestPaper"
                                        });

                                    }
                                    else if (result == "generating")
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "正在生成数据，请稍后重试",
                                            errorCode = "getTestPaper"
                                        });
                                    }
                                    else
                                    {
                                        strJsion = result;

                                        #region 记录教案访问情况
                                        Model_visit_client modelVC = new Model_visit_client();
                                        BLL_visit_client bllVC = new BLL_visit_client();
                                        //modelVC = bllVC.GetModelNew(userId, fileId, tabid);
                                        string strWhere = string.Format("user_id='{0}' and resource_data_id='{1}' and tab_id='{2}'"
                                            , userId, fileId, tabid);
                                        if (bllVC.GetRecordCount(strWhere) > 0)
                                        {
                                            //modelVC.open_time = DateTime.Now;
                                            //bllVC.Update(modelVC);
                                            string strSqlVC = string.Format("update visit_client set open_time='{0}' where user_id='{1}' and resource_data_id='{2}' and tab_id='{3}' "
                                                , DateTime.Now, userId, fileId, tabid);
                                            Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSqlVC);
                                        }
                                        else
                                        {
                                            modelVC = new Model_visit_client();
                                            modelVC.visit_client_id = Guid.NewGuid().ToString();
                                            modelVC.user_id = userId;
                                            modelVC.resource_data_id = fileId;
                                            modelVC.product_type = productType;
                                            modelVC.tab_id = tabid;
                                            modelVC.open_time = DateTime.Now;
                                            modelVC.operate_type = "view";
                                            bllVC.Add(modelVC);
                                        }
                                        #endregion
                                    }

                                    #endregion
                                }
                                else if (modelFUser.UserIdentity == "T")//老师fileId为ResourceToResourceFolder_ID 
                                {
                                    #region 记录教案访问情况
                                    Model_visit_client modelVC = new Model_visit_client();
                                    modelVC.visit_client_id = Guid.NewGuid().ToString();
                                    modelVC.user_id = userId;
                                    modelVC.resource_data_id = fileId;
                                    modelVC.product_type = productType;
                                    modelVC.tab_id = tabid;
                                    modelVC.open_time = DateTime.Now;
                                    modelVC.operate_type = "view";
                                    new BLL_visit_client().Add(modelVC);
                                    #endregion
                                    #region 老师
                                    string result = string.Empty;
                                    result = GetTestPaperFileForTeacher(fileId, userId);
                                    if (result == "error")
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "获取数据失败",
                                            errorCode = "getTestPaper"
                                        });

                                    }
                                    else if (result == "generating")
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "正在生成数据，请稍后重试",
                                            errorCode = "getTestPaper"
                                        });
                                    }
                                    else
                                    {
                                        strJsion = result;
                                    }
                                    #endregion
                                }

                                if (string.IsNullOrEmpty(strJsion))
                                {
                                    #region 读取文件
                                    strTestWebSiteUrl = pfunction.getHostPath();
                                    string strHomeWork_Id = string.Empty;
                                    if (modelFUser.UserIdentity == "S")//加载学生错题集
                                    {
                                        Model_Student_HomeWork modelSHW = new BLL_Student_HomeWork().GetModel(fileId);
                                        strHomeWork_Id = modelSHW.HomeWork_Id;
                                        Model_HomeWork modelHW = new BLL_HomeWork().GetModel(modelSHW.HomeWork_Id);
                                        fileId = modelHW.ResourceToResourceFolder_Id;
                                        strHomeWork_Name = modelHW.HomeWork_Name;
                                    }

                                    strTestWebSiteUrl += "/Upload/Resource/";
                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(fileId, "", string.Format("开始获取试卷数据|操作人{0}|试卷Id{1}|方法{2}", userId, fileId, "gettestpaper"));

                                    List<object> listTQObjBig = new List<object>();
                                    List<object> listTQObj = new List<object>();

                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(fileId, "", string.Format("开始获取试题数据|操作人{0}|试卷Id{1}|方法{2}", userId, fileId, "gettestpaper"));

                                    #region 试题
                                    //试题数据
                                    string strSqlTQ = string.Format(@"select  t1.TestQuestions_Id,t1.TestQuestions_Num,t1.TestQuestions_Type,t1.Parent_Id,t1.[type],t1.topicNumber
,T2.Resource_Version,T2.GradeTerm,t2.Subject,t2.ParticularYear,t2.Resource_Class,t2.CreateTime,t2.LessonPlan_Type
FROM TestQuestions t1 inner join ResourceToResourceFolder t2 ON t1.ResourceToResourceFolder_ID=T2.ResourceToResourceFolder_ID 
where  t1.ResourceToResourceFolder_Id='{0}' order by t1.TestQuestions_Num ", fileId);
                                    #region 学生错题集
                                    if (tabid == EnumTabindex.StudentSkillWrong.ToString())
                                    {
                                        //                                        strSqlTQ += string.Format(@" and TestQuestions_Id in (SELECT DISTINCT t1.TestQuestions_Id FROM Student_HomeWorkAnswer t1
                                        //INNER JOIN Student_WrongHomeWork  t2 ON t1.Student_HomeWorkAnswer_Id=t2.Student_HomeWorkAnswer_Id
                                        //WHERE t1.Student_Id='{0}' ) ", userId);
                                        strSqlTQ = string.Format(@"
select t1.TestQuestions_Id,t1.TestQuestions_Num,t1.TestQuestions_Type,t1.Parent_Id,t1.[type]
,T2.Resource_Version,T2.GradeTerm,t2.Subject,t2.ParticularYear,t2.Resource_Class,t2.CreateTime,t2.LessonPlan_Type
from (
select * from TestQuestions where TestQuestions_Type='title' and ResourceToResourceFolder_Id='{0}' 
union 
select * from TestQuestions where TestQuestions_Type='' and ResourceToResourceFolder_Id='{0}' and TestQuestions_Id in (
SELECT DISTINCT tq.Parent_Id FROM Student_HomeWorkAnswer t1
inner join TestQuestions tq on tq.TestQuestions_Id=t1.TestQuestions_Id and tq.Parent_Id!='0'
inner join TestQuestions_Score tqs on tqs.TestQuestions_Score_ID=t1.TestQuestions_Score_ID
WHERE tqs.TestQuestions_Score!=-1 and t1.Student_Answer_Status!='right' and Student_Answer_Status!='unknown' and t1.Student_Id='{1}' and t1.HomeWork_Id='{2}')
union 
select * from TestQuestions where TestQuestions_Type!='title' and ResourceToResourceFolder_Id='{0}' and TestQuestions_Id in 
(SELECT DISTINCT t1.TestQuestions_Id FROM Student_HomeWorkAnswer t1 
inner join TestQuestions_Score tqs on tqs.TestQuestions_Score_ID=t1.TestQuestions_Score_ID
WHERE tqs.TestQuestions_Score!=-1 and t1.Student_Answer_Status!='right' and Student_Answer_Status!='unknown' and t1.Student_Id='{1}' and t1.HomeWork_Id='{2}') 
)  t1 inner join ResourceToResourceFolder t2 ON t1.ResourceToResourceFolder_ID=T2.ResourceToResourceFolder_ID 
order by t1.TestQuestions_Num "
                                            , fileId, userId, strHomeWork_Id);

                                    }
                                    #endregion

                                    DataTable dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlTQ).Tables[0];
                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(fileId, "", string.Format("完成获取试题数据|操作人{0}|试卷Id{1}|方法{2}", userId, fileId, "gettestpaper"));

                                    //获取这个试卷的所有试题分值
                                    string strSqlScore = string.Empty;
                                    strSqlScore = string.Format(@"select  [TestQuestions_Score_ID]
      ,[ResourceToResourceFolder_Id]
      ,[TestQuestions_Id]
      ,[TestCorrect]
      ,[AnalyzeHyperlinkData]
      ,[TrainHyperlinkData],[testIndex],[TestType]
,TestQuestions_OrderNum FROM TestQuestions_Score  where ResourceToResourceFolder_Id='{0}'  ",
                                            fileId);
                                    DataTable dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlScore).Tables[0];

                                    #region 普通题型 list
                                    DataRow[] drList = dtTQ.Select("Parent_Id='' ", "TestQuestions_Num");
                                    foreach (DataRow item in drList)
                                    {
                                        string answersCorrectPath = string.Empty;
                                        string answersCorrectOwnPath = string.Empty;
                                        if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                                        {
                                            answersCorrectPath = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                                                item["Resource_Version"].ToString(), item["Subject"].ToString());
                                        }
                                        if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                                        {
                                            answersCorrectOwnPath = string.Format("{0}\\", Convert.ToDateTime(item["CreateTime"]).ToString("yyyy-MM-dd"));
                                        }
                                        DataRow drTQ_S = null;
                                        DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", item["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                                        #region 试题分数
                                        List<object> listTQ_SObj = new List<object>();
                                        int intIndex = 0;
                                        for (int j = 0; j < drTQ_Score.Length; j++)
                                        {
                                            drTQ_S = drTQ_Score[j];
                                            string strTestIndex = string.Empty;
                                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷
                                                && drTQ_S["TestType"].ToString() == "clozeTest")
                                            {
                                                intIndex++;
                                                strTestIndex = intIndex + ".";
                                            }
                                            string strAnalyzeUrl = string.Empty;
                                            string strTrainUrl = string.Empty;
                                            if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                                            {
                                                strAnalyzeUrl = pfunction.getHostPath() + drTQ_S["AnalyzeHyperlinkData"].ToString();
                                            }
                                            if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                                            {
                                                strTrainUrl = pfunction.getHostPath() + drTQ_S["TrainHyperlinkData"].ToString();
                                            }

                                            listTQ_SObj.Add(new
                                            {
                                                testIndex = strTestIndex,
                                                analyzeUrl = strAnalyzeUrl,
                                                trainUrl = strTrainUrl
                                            });
                                        }
                                        #endregion
                                        if (drTQ_S != null)
                                        {
                                            string strTopicNumber = string.Empty;
                                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                                            {
                                                strTopicNumber = item["topicNumber"].ToString();
                                            }
                                            listTQObj.Add(new
                                            {
                                                Testid = item["TestQuestions_Id"],
                                                testType = item["TestQuestions_Type"],
                                                topicNumber = strTopicNumber,
                                                docBase64 = RemotWeb.PostDataToServer(string.Format("{0}{1}testQuestionBody\\{2}\\{3}.txt", strTestWebSiteUrl, answersCorrectOwnPath, answersCorrectPath, item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                                textTitle = RemotWeb.PostDataToServer(string.Format("{0}{1}textTitle\\{2}\\{3}.txt", strTestWebSiteUrl, answersCorrectOwnPath, answersCorrectPath, item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                                list = listTQ_SObj
                                            });
                                        }
                                        else
                                        {
                                            string strTopicNumber = string.Empty;
                                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                                            {
                                                strTopicNumber = item["topicNumber"].ToString();
                                            }
                                            listTQObj.Add(new
                                            {
                                                Testid = item["TestQuestions_Id"],
                                                testType = item["TestQuestions_Type"],
                                                topicNumber = strTopicNumber,
                                                docBase64 = RemotWeb.PostDataToServer(string.Format("{0}{1}testQuestionBody\\{2}\\{3}.txt", strTestWebSiteUrl, answersCorrectOwnPath, answersCorrectPath, item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                                textTitle = RemotWeb.PostDataToServer(string.Format("{0}{1}textTitle\\{2}\\{3}.txt", strTestWebSiteUrl, answersCorrectOwnPath, answersCorrectPath, item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                                list = ""
                                            });
                                        }
                                    }
                                    #endregion
                                    #region 综合题型 listBig
                                    DataRow[] drListBig = dtTQ.Select("Parent_Id='0' ", "TestQuestions_Num");
                                    foreach (DataRow item in drListBig)
                                    {
                                        List<object> listTQObjBig_Sub = new List<object>();
                                        DataRow[] drBig_Sub = dtTQ.Select("Parent_Id='" + item["TestQuestions_Id"] + "'", "TestQuestions_Num");
                                        foreach (DataRow itemSub in drBig_Sub)
                                        {
                                            string answersCorrectPath = string.Empty;
                                            string answersCorrectOwnPath = string.Empty;
                                            if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                                            {
                                                answersCorrectPath = string.Format("{0}\\{1}\\{2}\\{3}\\", itemSub["ParticularYear"].ToString(), itemSub["GradeTerm"],
                                                    itemSub["Resource_Version"].ToString(), itemSub["Subject"].ToString());
                                            }
                                            if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                                            {
                                                answersCorrectOwnPath = string.Format("{0}\\", Convert.ToDateTime(itemSub["CreateTime"]).ToString("yyyy-MM-dd"));
                                            }
                                            DataRow drTQ_S = null;
                                            DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", itemSub["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                                            #region 试题分数
                                            List<object> listTQ_SObj = new List<object>();
                                            int intIndex = 0;
                                            for (int j = 0; j < drTQ_Score.Length; j++)
                                            {
                                                drTQ_S = drTQ_Score[j];
                                                string strTestIndex = string.Empty;
                                                if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷
                                                    && drTQ_S["TestType"].ToString() == "clozeTest")
                                                {
                                                    intIndex++;
                                                    strTestIndex = intIndex + ".";
                                                }
                                                string strAnalyzeUrl = string.Empty;
                                                string strTrainUrl = string.Empty;
                                                if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                                                {
                                                    strAnalyzeUrl = pfunction.getHostPath() + drTQ_S["AnalyzeHyperlinkData"].ToString();
                                                }
                                                if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                                                {
                                                    strTrainUrl = pfunction.getHostPath() + drTQ_S["TrainHyperlinkData"].ToString();
                                                }

                                                listTQ_SObj.Add(new
                                                {
                                                    testIndex = strTestIndex,
                                                    analyzeUrl = strAnalyzeUrl,
                                                    trainUrl = strTrainUrl
                                                });
                                            }
                                            #endregion
                                            if (drTQ_S != null)
                                            {
                                                string strTopicNumber = string.Empty;
                                                if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                                                {
                                                    strTopicNumber = itemSub["topicNumber"].ToString();
                                                }
                                                listTQObjBig_Sub.Add(new
                                                {
                                                    Testid = itemSub["TestQuestions_Id"],
                                                    testType = itemSub["TestQuestions_Type"],
                                                    topicNumber = strTopicNumber,
                                                    docBase64 = RemotWeb.PostDataToServer(string.Format("{0}{1}testQuestionBody\\{2}\\{3}.txt", strTestWebSiteUrl, answersCorrectOwnPath, answersCorrectPath, itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                                    textTitle = RemotWeb.PostDataToServer(string.Format("{0}{1}textTitle\\{2}\\{3}.txt", strTestWebSiteUrl, answersCorrectOwnPath, answersCorrectPath, itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                                    list = listTQ_SObj
                                                });
                                            }
                                            else
                                            {
                                                string strTopicNumber = string.Empty;
                                                if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                                                {
                                                    strTopicNumber = itemSub["topicNumber"].ToString();
                                                }
                                                listTQObjBig_Sub.Add(new
                                                {
                                                    Testid = itemSub["TestQuestions_Id"],
                                                    testType = itemSub["TestQuestions_Type"],
                                                    topicNumber = strTopicNumber,
                                                    docBase64 = RemotWeb.PostDataToServer(string.Format("{0}{1}testQuestionBody\\{2}\\{3}.txt", strTestWebSiteUrl, answersCorrectOwnPath, answersCorrectPath, itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                                    textTitle = RemotWeb.PostDataToServer(string.Format("{0}{1}textTitle\\{2}\\{3}.txt", strTestWebSiteUrl, answersCorrectOwnPath, answersCorrectPath, itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                                    list = ""
                                                });
                                            }
                                        }
                                        string savePathBig = string.Empty;
                                        string saveOwnerPathBig = string.Empty;
                                        if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                                        {
                                            savePathBig = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                                                item["Resource_Version"].ToString(), item["Subject"].ToString());
                                        }
                                        if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                                        {
                                            saveOwnerPathBig = string.Format("{0}\\", pfunction.ConvertToLongDateTime(item["CreateTime"].ToString(), "yyyy-MM-dd"));
                                        }
                                        string fileUrlBig = strTestWebSiteUrl + saveOwnerPathBig + "{0}\\" + savePathBig + "{1}.{2}";//自有资源 savePath为空，云资源saveOwnerPath为空
                                        string strdocBase64 = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "txt"), "", Encoding.UTF8, "Get");
                                        string strdocHtml = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "htm"), "", Encoding.UTF8, "Get");
                                        string textTitle = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "textTitle", item["TestQuestions_Id"].ToString(), "txt"), "", Encoding.UTF8, "Get");
                                        listTQObjBig.Add(new
                                        {
                                            docBase64 = strdocBase64,
                                            docHtml = strdocHtml,
                                            textTitle = textTitle,
                                            list = listTQObjBig_Sub,
                                            type = item["type"].ToString()
                                        });
                                    }
                                    #endregion

                                    #endregion
                                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(fileId, "", string.Format("完成获取试卷数据|操作人{0}|试卷Id{1}|方法{2}", userId, fileId, "gettestpaper"));
                                    if (listTQObj.Count == 0 && listTQObjBig.Count == 0)
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "获取数据失败",
                                            errorCode = "getTestPaper"
                                        });
                                    }
                                    else
                                    {
                                        string strTestPaperName = string.Empty;
                                        // strTestPaperName = strHomeWork_Name;
                                        bool isShowAnswer = false;//提交后是否显示答案，true 显示，false不显示。
                                        if (tabid == EnumTabindex.StudentSkillWrong.ToString())//只有错题集才显示 标题。
                                        {
                                            strTestPaperName = string.Format("《{0}》错题集", strHomeWork_Name);
                                            isShowAnswer = true;
                                        }
                                        strJsion = JsonConvert.SerializeObject(new
                                        {
                                            status = true,
                                            errorMsg = "",
                                            errorCode = "",
                                            paperHeaderDoc = GetPaperHeaderDoc(fileId),
                                            testPaperName = strTestPaperName,
                                            isTimeLimt = isTimeLimt,
                                            isTimeLength = isTimeLength,
                                            sysTime = DateTime.Now.ToString(),
                                            isShowAnswerAfterSubmiting = isShowAnswer,
                                            list = listTQObj,
                                            listBig = listTQObjBig
                                        });
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "getTestPaper",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "getTestPaper"
                            });
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        strJsion = JsonConvert.SerializeObject(new
                         {
                             status = false,
                             errorMsg = ex.Message.ToString(),
                             errorCode = "getTestPaper"
                         });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: GetRegistURL 获取注册新用户的URL地址。

                case "getregisturl":
                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        registUrl = pfunction.getHostPath() + "/RE_Register/teacherRegister.aspx ", // 注册新用户URL
                        status = true,           // 操作结果，true 成功； false 失败。
                        errorMsg = "",      // 错误提示，会在提示对话框中显示
                        errorCode = "00001"      // 错误编码
                    });
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: GetResourceForderTree 获取目录树
                case "getresourcefordertree":
                    try
                    {
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string tabid = Request.QueryString["TabID"].Filter();
                        //strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                        //判断产品类型
                        switch (productType)
                        {
                            case "scienceword":
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                break;
                            case "class":
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                break;
                            case "skill":
                                strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                break;
                        }
                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                DataTable dt = new DataTable();
                                List<object> listObj = new List<object>();
                                string strWhere = string.Empty;
                                strSql = new StringBuilder();
                                #region 老师
                                strSql.AppendFormat(@"select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Level,ResourceFolder_Name,ResourceFolder_Order,1 as isStore_Files from ResourceFolder
 where ResourceFolder_Owner='{0}' and Resource_Class='{1}' ", userId, Rc.Common.Config.Resource_ClassConst.自有资源);
                                #region 老师scienceWord自有教案
                                if (modelFUser.UserIdentity == "T" && productType == "scienceword" && tabid == EnumTabindex.TeacherScienceWordOwnTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                    strSql.AppendFormat(" and Resource_Type='{0}'", strResource_Type);
                                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = true,
                                        errorMsg = "",
                                        errorCode = "",
                                        list = GetTeacherOwnResourceFolderTree(userId, "0", strResource_Type, dt, tabid)
                                    });
                                }
                                #endregion
                                #region 老师scienceWord自有习题集
                                else if (modelFUser.UserIdentity == "T" && productType == "scienceword" && tabid == EnumTabindex.TeacherScienceWordOwnSkill.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                    strSql.AppendFormat(" and Resource_Type='{0}'", strResource_Type);
                                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = true,
                                        errorMsg = "",
                                        errorCode = "",
                                        list = GetTeacherOwnResourceFolderTree(userId, "0", strResource_Type, dt, tabid)
                                    });
                                }
                                #endregion
                                #region 老师Class自有资源
                                if (modelFUser.UserIdentity == "T" && productType == "class" && tabid == EnumTabindex.TeacherClassOwnTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                    strSql.AppendFormat(" and Resource_Type='{0}'", strResource_Type);
                                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = true,
                                        errorMsg = "",
                                        errorCode = "",
                                        list = GetTeacherOwnResourceFolderTree(userId, "0", strResource_Type, dt, tabid)
                                    });
                                }
                                #endregion
                                #endregion
                                #region 管理员
                                strSql = new StringBuilder();
                                strSql.AppendFormat(@"select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Order,0 as isStore_Files,ResourceFolder_Order from SysUserTaskForderTree
                                                                where ResourceFolder_Owner='{0}'                            
                                                                union
                                                                select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Order,1 as isStore_Files,ResourceFolder_Order from ResourceFolder
                                                                where  Resource_Class='{1}' and CreateFUser='{0}' ", userId, Rc.Common.Config.Resource_ClassConst.云资源);
                                #region 如果是管理员，scienceword，云教案
                                if (modelFUser.UserIdentity == "A" && productType == "scienceword" && tabid == EnumTabindex.MgrScienceWordCloudTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                    strSql.AppendFormat(" and Resource_Type='{0}'", strResource_Type);
                                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = true,
                                        errorMsg = "",
                                        errorCode = "",
                                        list = GetCloudResourceFolderTree("0", strResource_Type, dt)
                                    });
                                }
                                #endregion
                                #region 如果是管理员，scienceword，云习题集
                                else if (modelFUser.UserIdentity == "A" && productType == "scienceword" && tabid == EnumTabindex.MgrScienceWordCloudSkill.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                                    strSql.AppendFormat(" and Resource_Type='{0}'", strResource_Type);
                                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = true,
                                        errorMsg = "",
                                        errorCode = "",
                                        list = GetCloudResourceFolderTree("0", strResource_Type, dt)
                                    });
                                }
                                #endregion
                                #region 如果是管理员，class，云教案
                                else if (modelFUser.UserIdentity == "A" && productType == "class" && tabid == EnumTabindex.MgrClassCloudTeachingPlan.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型文件;
                                    strSql.AppendFormat(" and Resource_Type='{0}'", strResource_Type);
                                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = true,
                                        errorMsg = "",
                                        errorCode = "",
                                        list = GetCloudResourceFolderTree("0", strResource_Type, dt)
                                    });
                                }
                                #endregion
                                #region 如果是管理员，class，微课
                                else if (modelFUser.UserIdentity == "A" && productType == "class" && tabid == EnumTabindex.MgrClassCloudMicroClass.ToString())
                                {
                                    strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型微课件;
                                    strSql.AppendFormat(" and Resource_Type='{0}'", strResource_Type);
                                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = true,
                                        errorMsg = "",
                                        errorCode = "",
                                        list = GetCloudResourceFolderTree("0", strResource_Type, dt)
                                    });
                                }
                                #endregion


                                //                                #region 如果是管理员资源
                                //                                else if (modelFUser.UserIdentity == "A" && tabindex == "1")
                                //                                {
                                //                                    strWhere = string.Empty;
                                //                                    strSql = new StringBuilder();
                                //                                    strSql.AppendFormat(@"select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Order,0 as isStore_Files from GradeTermSubjectTeacherBooks
                                //                                union
                                //                                select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Order,1 as isStore_Files from ResourceFolder
                                //                                where  Resource_Class='{0}' and Resource_Type='{1}'", Rc.Common.Config.Resource_ClassConst.云资源, strResource_Type);
                                //                                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                //                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                //                                    {
                                //                                        status = true,
                                //                                        errorMsg = "",
                                //                                        errorCode = "",
                                //                                        list = GetCloudResourceFolderTree("0", strResource_Type, dt)
                                //                                    });
                                //                                }
                                //                                #endregion
                                #endregion
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "GetResourceTree",
                                    status = false,
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                errorMsg = "非法操作",
                                errorCode = "GetResourceTree",
                                status = false
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            errorMsg = ex.Message.ToString(),
                            errorCode = "GetResourceTree",
                            status = false
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称: GetResourceForderAttrTree 获取资源属性数结构
                case "getresourceforderattrtree":
                    try
                    {

                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string tabid = Request.QueryString["TabID"].Filter();
                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(userId, "", string.Format("开始getresourceforderattrtree|token{0}|userId{1}|TabID{2}"
 , userId, userId, tabid));
                        //strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                        //判断产品类型
                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                DataTable dt = new DataTable();
                                List<object> listObj = new List<object>();
                                string strWhere = string.Empty;
                                strSql = new StringBuilder();

                                #region 管理员
                                strSql = new StringBuilder();
                                strSql.Append(@"select ResourceFolder_Id,ResourceFolder_ParentId,ResourceFolder_Name,ResourceFolder_Order,0 as isStore_Files from SysUserTaskForderTree
                                                      where   ResourceFolder_Owner='" + userId.Filter() + "'     ");
                                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = true,
                                    errorMsg = "",
                                    errorCode = "",
                                    list = GetCloudResourceFolderTree("0", strResource_Type, dt)
                                });

                                #endregion
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "GetResourceTree",
                                    status = false,
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                errorMsg = "非法操作",
                                errorCode = "GetResourceTree",
                                status = false
                            });
                        }
                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(userId, "", string.Format("结束getresourceforderattrtree|token{0}|userId{1}|TabID{2}"
, userId, userId, tabid));
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            errorMsg = ex.Message.ToString(),
                            errorCode = "GetResourceTree",
                            status = false
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 客户端上传ScienceWord
                case "uploadsciencewordfile":
                    try
                    {

                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string userName = Request.QueryString["userName"].Filter();
                        string folderId = Request.QueryString["folderId"].Filter();
                        string title = Request.QueryString["title"].Filter();

                        if (!string.IsNullOrEmpty(userId))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                string Resource_Class = Rc.Common.Config.Resource_ClassConst.自有资源;
                                string Resource_Version = Rc.Common.Config.Resource_VersionConst.通用版;
                                string Resource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                #region 资源文件夹表 只获取实体数据 不操作
                                Model_ResourceFolder modelResFolder = new BLL_ResourceFolder().GetModel(folderId);
                                if (modelResFolder != null)
                                {
                                    Resource_Class = modelResFolder.Resource_Class;
                                    Resource_Version = modelResFolder.Resource_Version;
                                    Resource_Type = modelResFolder.Resource_Type;
                                }
                                #endregion
                                string strSuffix = ".dsc";
                                #region 资源表
                                HttpPostedFile file = Request.Files["content"];
                                byte[] bytes = null;
                                using (var binaryReader = new BinaryReader(file.InputStream))
                                {
                                    bytes = binaryReader.ReadBytes(file.ContentLength);
                                }
                                Model_Resource modelResource = new Model_Resource();
                                modelResource.Resource_Id = Guid.NewGuid().ToString();
                                modelResource.Resource_MD5 = clsUtility.GetMd5(Convert.ToBase64String(bytes));
                                modelResource.Resource_DataStrem = Convert.ToBase64String(bytes);//此处有问题 不知道用你写的哪个方法获取数据 （答：此处获取的就是客户端穿过来的BASE64字符串，直接赋值即可）

                                HttpPostedFile htmlFile = Request.Files["htmlContent"];
                                bytes = null;
                                using (var binaryReader = new BinaryReader(htmlFile.InputStream))
                                {
                                    bytes = binaryReader.ReadBytes(htmlFile.ContentLength);
                                }
                                modelResource.Resource_ContentHtml = Convert.ToBase64String(bytes);
                                modelResource.CreateTime = DateTime.Now;
                                #endregion
                                #region 资源文件夹关系表
                                Model_ResourceToResourceFolder modelRTRF = new Model_ResourceToResourceFolder();
                                modelRTRF.ResourceToResourceFolder_Id = Guid.NewGuid().ToString();
                                modelRTRF.ResourceFolder_Id = folderId;
                                modelRTRF.Resource_Id = modelResource.Resource_Id;
                                modelRTRF.File_Name = title + strSuffix;
                                modelRTRF.Resource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                                modelRTRF.Resource_Name = title + strSuffix;//此处有问题 不知道存储接收过来的哪个节点的数据（答：暂时与filename一致）
                                modelRTRF.Resource_Class = Resource_Class;
                                modelRTRF.Resource_Version = Resource_Version;
                                modelRTRF.File_Owner = userId;
                                modelRTRF.CreateFUser = userId;
                                modelRTRF.CreateTime = DateTime.Now;
                                modelRTRF.File_Suffix = strSuffix;
                                #endregion
                                #region 图书生产日志
                                Model_BookProductionLog modelBPL = new Model_BookProductionLog();
                                modelBPL.BookProductionLog_Id = Guid.NewGuid().ToString();
                                modelBPL.BookId = modelRTRF.Book_ID;
                                modelBPL.ResourceToResourceFolder_Id = modelRTRF.ResourceToResourceFolder_Id;
                                modelBPL.ParticularYear = Convert.ToInt16(modelRTRF.ParticularYear);
                                modelBPL.Resource_Type = modelRTRF.Resource_Type;
                                modelBPL.LogTypeEnum = "1";//1添加,2修改
                                modelBPL.CreateUser = userId;
                                modelBPL.CreateTime = DateTime.Now;
                                #endregion
                                List<Model_ResourceToResourceFolder_img> listModelRTRF_img = new List<Model_ResourceToResourceFolder_img>();
                                if (new BLL_Resource().ClientUploadScienceWord(modelResource, modelRTRF, listModelRTRF_img, modelBPL) == 0)
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        errorMsg = "教案上传失败",
                                        result = 0,
                                        status = false
                                    });
                                }
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    result = 0,
                                    status = false,
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                errorMsg = "非法操作",
                                result = 0,
                                status = false
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            errorMsg = ex.Message.ToString(),
                            result = 0,
                            status = false
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 客户端打开 学生已提交作业 暂未开发

                #endregion
                #region 生成年级学期 学科 教材版本 目录树 GenerateGradeTermSubjectTeacherBooks
                case "generategradetermsubjectteacherbooks":
                    try
                    {
                        List<Model_Common_Dict> listGradeTerm = new BLL_Common_Dict().GetModelList("D_Type='6'");
                        List<Model_Common_Dict> listSubject = new BLL_Common_Dict().GetModelList("D_Type='7'");
                        List<Model_Common_Dict> listTeacherBooks = new BLL_Common_Dict().GetModelList("D_Type='3'");
                        List<Model_GradeTermSubjectTeacherBooks> listGST_Add = new List<Model_GradeTermSubjectTeacherBooks>();
                        List<Model_GradeTermSubjectTeacherBooks> listGST_Update = new List<Model_GradeTermSubjectTeacherBooks>();
                        Model_GradeTermSubjectTeacherBooks modelParticularYear = new Model_GradeTermSubjectTeacherBooks();
                        Model_GradeTermSubjectTeacherBooks modelGrade = new Model_GradeTermSubjectTeacherBooks();
                        Model_GradeTermSubjectTeacherBooks modelSubject = new Model_GradeTermSubjectTeacherBooks();
                        Model_GradeTermSubjectTeacherBooks modelVersion = new Model_GradeTermSubjectTeacherBooks();
                        string pId = string.Empty;
                        string parentId = string.Empty;
                        string parentId2 = string.Empty;
                        int intYear = DateTime.Now.Year;
                        for (int i = intYear - 1; i < intYear + 5; i++)
                        {
                            string strYear = i.ToString();
                            #region 年份
                            pId = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strYear, "md5");
                            pId += pId;
                            pId = pId.Substring(0, 36);
                            modelParticularYear = new BLL_GradeTermSubjectTeacherBooks().GetModel(pId);
                            if (modelParticularYear == null)
                            {
                                #region 添加
                                modelParticularYear = new Model_GradeTermSubjectTeacherBooks();
                                modelParticularYear.ResourceFolder_Id = pId;
                                modelParticularYear.ResourceFolder_ParentId = "0";
                                modelParticularYear.ResourceFolder_Name = strYear;
                                modelParticularYear.ResourceFolder_Level = 1;
                                modelParticularYear.Resource_Type = Rc.Common.Config.Resource_TypeConst.按属性生成的目录;
                                modelParticularYear.Resource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
                                modelParticularYear.ResourceFolder_Remark = "";
                                modelParticularYear.ResourceFolder_Order = null;
                                modelParticularYear.ResourceFolder_Owner = null;
                                modelParticularYear.CreateFUser = null;
                                modelParticularYear.CreateTime = DateTime.Now;
                                modelParticularYear.ResourceFolder_isLast = "0";
                                modelParticularYear.LessonPlan_Type = "";
                                modelParticularYear.ParticularYear = i;
                                modelParticularYear.GradeTerm = "";
                                modelParticularYear.Resource_Version = "";
                                modelParticularYear.Subject = "";
                                #endregion
                                listGST_Add.Add(modelParticularYear);
                            }
                            else
                            {
                                #region 修改
                                modelParticularYear.ResourceFolder_Name = strYear;
                                modelParticularYear.ParticularYear = i;
                                modelParticularYear.GradeTerm = "";
                                modelParticularYear.Resource_Version = "";
                                modelParticularYear.Subject = "";
                                #endregion
                                listGST_Update.Add(modelParticularYear);
                            }
                            #endregion

                            foreach (var itemG in listGradeTerm)
                            {
                                #region 年级学期
                                parentId = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strYear + itemG.Common_Dict_ID, "md5");
                                parentId += parentId;
                                parentId = parentId.Substring(0, 36);
                                modelGrade = new BLL_GradeTermSubjectTeacherBooks().GetModel(parentId);
                                if (modelGrade == null)
                                {
                                    #region 添加
                                    modelGrade = new Model_GradeTermSubjectTeacherBooks();
                                    modelGrade.ResourceFolder_Id = parentId;
                                    modelGrade.ResourceFolder_ParentId = pId;
                                    modelGrade.ResourceFolder_Name = itemG.D_Name;
                                    modelGrade.ResourceFolder_Level = 2;
                                    modelGrade.Resource_Type = Rc.Common.Config.Resource_TypeConst.按属性生成的目录;
                                    modelGrade.Resource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
                                    modelGrade.ResourceFolder_Remark = "";
                                    modelGrade.ResourceFolder_Order = null;
                                    modelGrade.ResourceFolder_Owner = null;
                                    modelGrade.CreateFUser = null;
                                    modelGrade.CreateTime = DateTime.Now;
                                    modelGrade.ResourceFolder_isLast = "0";
                                    modelGrade.LessonPlan_Type = "";
                                    modelGrade.ParticularYear = i;
                                    modelGrade.GradeTerm = itemG.Common_Dict_ID;
                                    modelGrade.Resource_Version = "";
                                    modelGrade.Subject = "";
                                    #endregion
                                    listGST_Add.Add(modelGrade);
                                }
                                else
                                {
                                    #region 修改
                                    modelGrade.ResourceFolder_Name = itemG.D_Name;
                                    modelGrade.ParticularYear = i;
                                    modelGrade.GradeTerm = itemG.Common_Dict_ID;
                                    modelGrade.Resource_Version = "";
                                    modelGrade.Subject = "";
                                    #endregion
                                    listGST_Update.Add(modelGrade);
                                }


                                #endregion

                                foreach (var itemT in listTeacherBooks)
                                {
                                    #region 教材版本
                                    parentId2 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strYear + itemG.Common_Dict_ID + itemT.Common_Dict_ID, "md5");
                                    parentId2 += parentId2;
                                    parentId2 = parentId2.Substring(0, 36);

                                    modelVersion = new BLL_GradeTermSubjectTeacherBooks().GetModel(parentId2);
                                    if (modelVersion == null)
                                    {
                                        #region 添加
                                        modelVersion = new Model_GradeTermSubjectTeacherBooks();
                                        modelVersion.ResourceFolder_Id = parentId2;
                                        modelVersion.ResourceFolder_ParentId = parentId;
                                        modelVersion.ResourceFolder_Name = itemT.D_Name;
                                        modelVersion.ResourceFolder_Level = 3;
                                        modelVersion.Resource_Type = Rc.Common.Config.Resource_TypeConst.按属性生成的目录;
                                        modelVersion.Resource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
                                        modelVersion.ResourceFolder_Remark = "";
                                        modelVersion.ResourceFolder_Order = null;
                                        modelVersion.ResourceFolder_Owner = null;
                                        modelVersion.CreateFUser = null;
                                        modelVersion.CreateTime = DateTime.Now;
                                        modelVersion.ResourceFolder_isLast = "0";
                                        modelVersion.LessonPlan_Type = "";
                                        modelVersion.ParticularYear = i;
                                        modelVersion.GradeTerm = itemG.Common_Dict_ID;
                                        modelVersion.Resource_Version = itemT.Common_Dict_ID;
                                        modelVersion.Subject = "";
                                        #endregion
                                        listGST_Add.Add(modelVersion);
                                    }
                                    else
                                    {
                                        #region 修改
                                        modelVersion.ResourceFolder_Name = itemT.D_Name;
                                        modelVersion.ParticularYear = i;
                                        modelVersion.GradeTerm = itemG.Common_Dict_ID;
                                        modelVersion.Resource_Version = itemT.Common_Dict_ID;
                                        modelVersion.Subject = "";
                                        #endregion
                                        listGST_Update.Add(modelVersion);
                                    }
                                    #endregion

                                    foreach (var itemS in listSubject)
                                    {
                                        #region 学科
                                        string rfId = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strYear + itemG.Common_Dict_ID + itemT.Common_Dict_ID + itemS.Common_Dict_ID, "md5");
                                        rfId += rfId;
                                        rfId = rfId.Substring(0, 36);
                                        modelSubject = new BLL_GradeTermSubjectTeacherBooks().GetModel(rfId);
                                        if (modelSubject == null)
                                        {
                                            #region 添加
                                            modelSubject = new Model_GradeTermSubjectTeacherBooks();
                                            modelSubject.ResourceFolder_Id = rfId;
                                            modelSubject.ResourceFolder_ParentId = parentId2;
                                            modelSubject.ResourceFolder_Name = itemS.D_Name;
                                            modelSubject.ResourceFolder_Level = 4;
                                            modelSubject.Resource_Type = Rc.Common.Config.Resource_TypeConst.按属性生成的目录;
                                            modelSubject.Resource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
                                            modelSubject.ResourceFolder_Remark = "";
                                            modelSubject.ResourceFolder_Order = null;
                                            modelSubject.ResourceFolder_Owner = null;
                                            modelSubject.CreateFUser = null;
                                            modelSubject.CreateTime = DateTime.Now;
                                            modelSubject.ResourceFolder_isLast = "1";
                                            modelSubject.LessonPlan_Type = "";
                                            modelSubject.ParticularYear = i;
                                            modelSubject.GradeTerm = itemG.Common_Dict_ID;
                                            modelSubject.Resource_Version = itemT.Common_Dict_ID;
                                            modelSubject.Subject = itemS.Common_Dict_ID;
                                            #endregion
                                            listGST_Add.Add(modelSubject);
                                        }
                                        else
                                        {
                                            #region 修改
                                            modelSubject.ResourceFolder_Name = itemS.D_Name;
                                            modelSubject.ParticularYear = i;
                                            modelSubject.GradeTerm = itemG.Common_Dict_ID;
                                            modelSubject.Resource_Version = itemT.Common_Dict_ID;
                                            modelSubject.Subject = itemS.Common_Dict_ID;
                                            #endregion
                                            listGST_Update.Add(modelSubject);
                                        }

                                        #endregion
                                    }
                                }
                            }
                        }
                        if (new BLL_GradeTermSubjectTeacherBooks().AddandUpdateMulti(listGST_Add, listGST_Update) > 0)
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = true,
                                errorMsg = "",
                                errorCode = ""
                            });
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "SQL执行失败",
                                errorCode = "GenerateGradeTermSubjectTeacherBooks"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "GenerateGradeTermSubjectTeacherBooks"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称：GetCommonDict  获取通用字典
                case "getcommondict":
                    try
                    {
                        string userId = Request.QueryString["userId"].Filter();
                        string token = Request.QueryString["token"].Filter();
                        string dictType = Request.QueryString["dictType"].Filter();//字典枚举（ParticularYear 年份/GradeTerm 年级学期/Subject 学科/TextBookVersion 教材版本 等等）

                        if (string.IsNullOrEmpty(userId))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                strSql = new StringBuilder();
                                strSql.Append("SELECT Common_Dict_ID,D_Name FROM Common_Dict ");
                                DataTable dt = new DataTable();
                                List<CommonDictModel> strList = new List<CommonDictModel>();
                                switch (dictType)
                                {
                                    case "ParticularYear"://年份

                                        string strYear = string.Empty;
                                        int intYear = DateTime.Now.Year;

                                        for (int i = intYear - 1; i < intYear + 5; i++)
                                        {
                                            CommonDictModel cdModel = new CommonDictModel();
                                            cdModel.ID = i.ToString();
                                            cdModel.D_Name = i.ToString();
                                            cdModel.D_Type = dictType;
                                            strList.Add(cdModel);
                                        }
                                        break;
                                    case "GradeTerm"://年级学期
                                        strSql.AppendFormat(" WHERE D_Type='{0}' ORDER BY D_ORDER,D_NAME", 6);
                                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            CommonDictModel cdModel = new CommonDictModel();
                                            cdModel.ID = dt.Rows[i]["Common_Dict_ID"].ToString();
                                            cdModel.D_Name = dt.Rows[i]["D_Name"].ToString();
                                            cdModel.D_Type = dictType;
                                            strList.Add(cdModel);
                                        }
                                        break;
                                    case "Subject"://学科
                                        strSql.AppendFormat(" WHERE D_Type='{0}'  ORDER BY D_ORDER,D_NAME", 7);
                                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            CommonDictModel cdModel = new CommonDictModel();
                                            cdModel.ID = dt.Rows[i]["Common_Dict_ID"].ToString();
                                            cdModel.D_Name = dt.Rows[i]["D_Name"].ToString();
                                            cdModel.D_Type = dictType;
                                            strList.Add(cdModel);
                                        }
                                        break;
                                    case "TextBookVersion"://教材版本
                                        strSql.AppendFormat(" WHERE D_Type='{0}'  ORDER BY D_ORDER,D_NAME", 3);
                                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            CommonDictModel cdModel = new CommonDictModel();
                                            cdModel.ID = dt.Rows[i]["Common_Dict_ID"].ToString();
                                            cdModel.D_Name = dt.Rows[i]["D_Name"].ToString();
                                            cdModel.D_Type = dictType;
                                            strList.Add(cdModel);
                                        }
                                        break;
                                    default:
                                        break;
                                }


                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = true,
                                    list = strList,
                                    errorMsg = "",
                                    errorCode = ""
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "DeletePath"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "DeletePath"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 接口名称：ProcessLog  执行日志
                case "processlog":
                    try
                    {
                        string keyType = Request.QueryString["keyType"].Filter();//接口名称
                        string processDesc = Request.QueryString["processDesc"].Filter();//执行描述，格式为："[执行内容1]:[对应的值];[执行内容2]:[对应的值2]...."
                        Rc.Common.SystemLog.SystemLog.AddLogFromCS("", "", processDesc);

                    }
                    catch (Exception)
                    {

                    }

                    break;
                #endregion
                #region 29. 接口名称: getClassList 班级列表
                case "getclasslist":
                    try
                    {
                        string userId = Request.QueryString["userId"].Filter();
                        string token = Request.QueryString["token"].Filter();
                        string tabId = Request.QueryString["tabId"].Filter();

                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                List<object> listReturn = new List<object>();
                                DataTable dt = new BLL_UserGroup().GetList(" UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus=0 and USER_ID='" + userId + "') order by UserGroupOrder,UserGroup_Name ").Tables[0];
                                foreach (DataRow item in dt.Rows)
                                {
                                    listReturn.Add(new
                                    {
                                        classId = item["UserGroup_Id"].ToString(),
                                        className = item["UserGroup_Name"].ToString()
                                    });
                                }
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = true,
                                    errorMsg = "",
                                    errorCode = "",
                                    list = listReturn
                                });
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "getClassList",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "getClassList"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "getClassList"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 30. 接口名称: getClassHomeWorkList 班级作业列表
                case "getclasshomeworklist":
                    try
                    {
                        string userId = Request.QueryString["userId"].Filter();
                        string token = Request.QueryString["token"].Filter();
                        string classId = Request.QueryString["classId"].Filter();
                        string tabId = Request.QueryString["tabId"].Filter();

                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                List<object> listReturn = new List<object>();
                                string strWhere = string.Empty;
                                strWhere = " HomeWork_AssignTeacher='" + userId + "' and UserGroup_Id='" + classId + "' ";
                                DataTable dt = new BLL_HomeWork().GetList(strWhere + " order by CreateTime desc ").Tables[0];
                                string strSqlNoCorrect = @"
select  COUNT(*) as NoCorrectCount,shw.HomeWork_Id from Student_HomeWork  shw
inner join HomeWork hw on hw.HomeWork_Id=shw.HomeWork_Id 
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id 
where  " + strWhere + " and shwCorrect.Student_HomeWork_CorrectStatus=0 and shwSubmit.Student_HomeWork_Status=1 group by shw.HomeWork_Id ";
                                DataTable dtNoCorrect = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlNoCorrect).Tables[0];
                                foreach (DataRow item in dt.Rows)
                                {
                                    DataRow[] drNoCorrect = dtNoCorrect.Select("HomeWork_Id='" + item["HomeWork_Id"] + "'");
                                    listReturn.Add(new
                                    {
                                        classId = classId,
                                        homeworkId = item["HomeWork_Id"].ToString(),
                                        homeworkName = item["HomeWork_Name"].ToString(),
                                        homeworkAssignTeacher = item["HomeWork_AssignTeacher"].ToString(),
                                        homeworkStatus = item["Homework_Status"].ToString(),
                                        homeworkFinishTime = item["Homework_FinishTime"].ToString(),
                                        createTime = item["CreateTime"].ToString(),
                                        beginTime = item["BeginTime"].ToString(),
                                        stopTime = item["StopTime"].ToString(),
                                        isTimeLimt = item["isTimeLimt"].ToString(),
                                        unmarkedCount = drNoCorrect.Length > 0 ? drNoCorrect[0]["NoCorrectCount"] : 0
                                    });
                                }
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = true,
                                    errorMsg = "",
                                    errorCode = "",
                                    list = listReturn
                                });
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "getClassHomeWorkList",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "getClassHomeWorkList"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "getClassHomeWorkList"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 31. 接口名称: getClassStudentSubmitedList 班级已交作业学生列表
                case "getclassstudentsubmitedlist":
                    try
                    {
                        string userId = Request.QueryString["userId"].Filter();
                        string token = Request.QueryString["token"].Filter();
                        string homeworkId = Request.QueryString["homeworkId"].Filter();
                        string tabId = Request.QueryString["tabId"].Filter();

                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                List<object> listReturn = new List<object>();
                                strSql = new StringBuilder();
                                strSql.AppendFormat(@"select t.Student_HomeWork_Id,t.HomeWork_Id,t.Student_Id,t.CreateTime,shwCorrect.Student_HomeWork_CorrectStatus,
                                  shwSubmit.OpenTime,shwSubmit.Student_Answer_Time
,(case when t2.TrueName='' then t2.UserName when t2.TrueName is null then t2.UserName else t2.UserName end) as StudentName
,(select ISNULL(SUM(Student_Score),0) from Student_HomeWorkAnswer where Student_HomeWork_Id=t.Student_HomeWork_Id) as StudentScore 
from Student_HomeWork t 
inner join F_User t2 on t2.UserId=t.Student_Id 
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=t.Student_HomeWork_Id 
inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=t.Student_HomeWork_Id 
where shwSubmit.Student_HomeWork_Status='1' and t.HomeWork_Id='{0}' order by shwSubmit.Student_Answer_Time desc", homeworkId);
                                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                foreach (DataRow item in dt.Rows)
                                {
                                    listReturn.Add(new
                                    {
                                        homeworkId = homeworkId,
                                        studentHomeWorkId = item["Student_HomeWork_Id"].ToString(),
                                        studentId = item["Student_Id"].ToString(),
                                        studentName = item["StudentName"].ToString(),
                                        studentHomeWorkStatus = item["Student_HomeWork_CorrectStatus"].ToString(),
                                        openTime = item["OpenTime"].ToString(),
                                        studentAnswerTime = item["Student_Answer_Time"].ToString(),
                                        studentScore = item["StudentScore"].ToString()
                                    });
                                }
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = true,
                                    errorMsg = "",
                                    errorCode = "",
                                    list = listReturn
                                });
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "getClassHomeWorkList",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "getClassHomeWorkList"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "getClassHomeWorkList"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 32. 接口名称: getClassStudentUnSubmitedList 班级未交作业学生列表
                case "getclassstudentunsubmitedlist":
                    try
                    {
                        string userId = Request.QueryString["userId"].Filter();
                        string token = Request.QueryString["token"].Filter();
                        string homeworkId = Request.QueryString["homeworkId"].Filter();
                        string tabId = Request.QueryString["tabId"].Filter();

                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                List<object> listReturn = new List<object>();
                                strSql = new StringBuilder();
                                strSql.AppendFormat(@"select t.* 
,(case when t2.TrueName='' then t2.UserName when t2.TrueName is null then t2.UserName else t2.UserName end) as StudentName
from Student_HomeWork t
inner join F_User t2 on t2.UserId=t.Student_Id  
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=t.Student_HomeWork_Id where shwSubmit.Student_HomeWork_Status='0' 
and t.HomeWork_Id='{0}' order by shwSubmit.Student_Answer_Time desc", homeworkId);
                                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                                foreach (DataRow item in dt.Rows)
                                {
                                    listReturn.Add(new
                                    {
                                        homeworkId = homeworkId,
                                        studentHomeWorkId = item["Student_HomeWork_Id"].ToString(),
                                        studentId = item["Student_Id"].ToString(),
                                        studentName = item["StudentName"].ToString()
                                    });
                                }
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = true,
                                    errorMsg = "",
                                    errorCode = "",
                                    list = listReturn
                                });
                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "getClassHomeWorkList",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "getClassHomeWorkList"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "getClassHomeWorkList"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 33. 接口名称: updateClassHomeWorkInfo 班级作业信息（目前只修改作业状态）
                case "updateclasshomeworkinfo":
                    try
                    {
                        string userId = Request.QueryString["userId"].Filter();
                        string token = Request.QueryString["token"].Filter();
                        string homeworkId = Request.QueryString["homeworkId"].Filter();
                        string markedStatus = Request.QueryString["markedStatus"].Filter();//completed/uncompleted
                        string tabId = Request.QueryString["tabId"].Filter();

                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                Model_HomeWork modelHW = new Model_HomeWork();
                                BLL_HomeWork bllHW = new BLL_HomeWork();
                                modelHW = bllHW.GetModel(homeworkId);
                                if (markedStatus == "completed")
                                {
                                    modelHW.HomeWork_Status = 1;
                                }
                                else if (markedStatus == "uncompleted")
                                {
                                    modelHW.HomeWork_Status = 0;
                                }

                                if (bllHW.Update(modelHW))
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = true,
                                        errorMsg = "",
                                        errorCode = ""
                                    });
                                }
                                else
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = false,
                                        errorMsg = "更新失败",
                                        errorCode = "updateClassHomeWorkInfo"
                                    });

                                }

                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "updateClassHomeWorkInfo",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "updateClassHomeWorkInfo"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "updateClassHomeWorkInfo"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 34. 接口名称: getStudentAnswer 学生答题（老师批改时使用，目录【studentAnswerForMarking】）
                case "getstudentanswer":
                    try
                    {
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string studentHomeWorkId = Request.QueryString["studentHomeWorkId"].Filter();//此ID为Student_HomeWork_Id学生作业
                        string tabId = Request.QueryString["tabId"].Filter();

                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(studentHomeWorkId))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                string strTestpaperMarking = string.Empty;
                                Model_Student_HomeWork modelSHW = new BLL_Student_HomeWork().GetModel(studentHomeWorkId);
                                Model_Student_HomeWork_Correct modelshwCorrect = new BLL_Student_HomeWork_Correct().GetModel(studentHomeWorkId);
                                string savePathForTch = string.Empty;
                                Model_HomeWork modelHW = new BLL_HomeWork().GetModel(modelSHW.HomeWork_Id);
                                string homeWorkId = modelHW.HomeWork_Id;
                                string rtrfId = modelHW.ResourceToResourceFolder_Id;

                                savePathForTch = string.Format("{0}\\", pfunction.ToShortDate(modelHW.CreateTime.ToString()));

                                strTestWebSiteUrl = pfunction.getHostPath();

                                string fileUrl = strTestWebSiteUrl + "\\Upload\\Resource\\{0}\\" + savePathForTch + "{1}\\{1}.txt";
                                string fileUrlForTch = strTestWebSiteUrl + "\\Upload\\Resource\\{0}\\" + savePathForTch + "{1}.tch.txt";
                                string fileMarkUrl = strTestWebSiteUrl + "\\Upload\\Resource\\teacherMarking\\{0}\\{1}\\{1}.txt";
                                if (modelshwCorrect.Student_HomeWork_CorrectStatus == 1)//如果老师已批改，获取老师批改笔记信息
                                {
                                    strTestpaperMarking = RemotWeb.PostDataToServer(string.Format(fileMarkUrl, pfunction.ToShortDate(modelHW.CreateTime.ToString()), studentHomeWorkId), "", Encoding.UTF8, "Get");
                                }
                                
                                string stuInfo = RemotWeb.PostDataToServer(string.Format(fileUrl, "studentAnswerForMarking", studentHomeWorkId), "", Encoding.UTF8, "Get");
                                string tchInfo = RemotWeb.PostDataToServer(string.Format(fileUrlForTch, "studentPaper", homeWorkId), "", Encoding.UTF8, "Get");
                                
                                StuAnswerForMarkingModel modelStu = new StuAnswerForMarkingModel();
                                List<TestPaperAnswerModel> listStu = new List<TestPaperAnswerModel>();
                                if (!string.IsNullOrEmpty(stuInfo))
                                {
                                    try
                                    {
                                        modelStu = JsonConvert.DeserializeObject<StuAnswerForMarkingModel>(stuInfo);
                                    }
                                    catch (Exception)
                                    {
                                        modelStu = null;
                                        listStu = JsonConvert.DeserializeObject<List<TestPaperAnswerModel>>(stuInfo);
                                    }
                                }
                                TchForMarkingModel modelTch = new TchForMarkingModel();
                                List<TestQuestionModel> listTch = new List<TestQuestionModel>();
                                if (!string.IsNullOrEmpty(tchInfo))
                                {
                                    try
                                    {
                                        modelTch = JsonConvert.DeserializeObject<TchForMarkingModel>(tchInfo);
                                    }
                                    catch (Exception)
                                    {
                                        modelTch = null;
                                        listTch = JsonConvert.DeserializeObject<List<TestQuestionModel>>(tchInfo);
                                    }
                                }

                                object objReturn = new object();
                                List<object> listReturnBig = new List<object>();

                                if (modelStu != null && modelTch != null)
                                {
                                    objReturn = new
                                    {
                                        StudentAnswerList = modelStu.list,
                                        TestQuestionList = modelTch.list
                                    };
                                    if (modelStu.listBig != null && modelTch.listBig != null)
                                    {
                                        int numBig = 0;
                                        foreach (var item in modelStu.listBig)
                                        {
                                            if (modelTch.listBig[numBig].list == null || modelTch.listBig[numBig].list.Count == 0) numBig++;

                                            object listBigSub = new
                                            {
                                                StudentAnswerList = item.list,
                                                TestQuestionList = modelTch.listBig[numBig].list
                                            };
                                            listReturnBig.Add(new
                                            {
                                                docBase64 = modelTch.listBig[numBig].docBase64,
                                                docHtml = modelTch.listBig[numBig].docBase64,
                                                list = listBigSub
                                            });
                                            numBig++;
                                        }
                                    }
                                }
                                else
                                {
                                    objReturn = new
                                    {
                                        StudentAnswerList = listStu,
                                        TestQuestionList = listTch
                                    };

                                }
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = true,
                                    errorMsg = "",
                                    errorCode = "",
                                    testpaperMarking = strTestpaperMarking,
                                    list = objReturn,
                                    listBig = listReturnBig
                                });

                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "getStudentAnswer",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "getStudentAnswer"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "getStudentAnswer"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 35. 接口名称: markingTestPaper 老师批改信息
                case "markingtestpaper":
                    try
                    {
                        string token = Request.QueryString["token"].Filter();
                        string userId = Request.QueryString["userId"].Filter();
                        string studentHomeWorkId = Request.QueryString["studentHomeWorkId"].Filter();//此ID为Student_HomeWork_Id学生作业
                        string tabId = Request.QueryString["tabId"].Filter();

                        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(studentHomeWorkId))
                        {
                            Model_F_User_Client modelFUser = checkTokenIsValidBackModel(userId, token, productType);
                            if (modelFUser != null)
                            {
                                Stream resStream = HttpContext.Current.Request.InputStream;
                                StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
                                string testJsion = sr.ReadToEnd();
                                string resInfo = testJsion;

                                //resInfo = File.ReadAllText(Server.MapPath("/upload/") + "a.txt");

                                //把数据流保存到文件
                                string logPath = string.Format("/Upload/markingTestPaper/{0}/{1}.txt", DateTime.Now.ToString("yyyy-MM-dd"), studentHomeWorkId);
                                pfunction.WriteToFile(Server.MapPath(logPath), resInfo, true);

                                Model_Student_HomeWork modelSHW = new BLL_Student_HomeWork().GetModel(studentHomeWorkId);
                                Model_Student_HomeWork_Correct modelshwCorrect = new BLL_Student_HomeWork_Correct().GetModel(studentHomeWorkId);
                                TchMKModel mkModel = JsonConvert.DeserializeObject<TchMKModel>(resInfo);

                                if (mkModel != null)
                                {
                                    #region 试题批改

                                    modelshwCorrect.Student_HomeWork_CorrectStatus = 1;
                                    modelshwCorrect.CorrectTime = DateTime.Now;
                                    modelshwCorrect.CorrectMode = "1";

                                    BLL_Student_HomeWorkAnswer bll = new BLL_Student_HomeWorkAnswer();
                                    Model_HomeWork modelHW = new BLL_HomeWork().GetModel(modelSHW.HomeWork_Id);
                                    uploadPath += string.Format("teacherMarking\\{0}\\", pfunction.ConvertToLongDateTime(modelHW.CreateTime.ToString(), "yyyy-MM-dd"));

                                    List<Model_Student_HomeWorkAnswer> listSHWA = new List<Model_Student_HomeWorkAnswer>();
                                    List<Model_Student_HomeWorkAnswer> listDataSHWA = bll.GetModelList("Student_HomeWork_Id='" + studentHomeWorkId + "' ");

                                    #region 普通题型 list
                                    List<TchMKTQList> listTchMK = mkModel.testList;
                                    if (listTchMK != null)
                                    {
                                        foreach (var itemTQ in listTchMK)
                                        {
                                            if (itemTQ != null && itemTQ.list != null && (itemTQ.testType == "fill" || itemTQ.testType == "answers"))
                                            {
                                                int detailNum = 0;
                                                foreach (var itemTQ_S in itemTQ.list)
                                                {
                                                    detailNum++;
                                                    List<Model_Student_HomeWorkAnswer> listWhere = listDataSHWA.Where(w => w.TestQuestions_Id == itemTQ.Testid && w.TestQuestions_Detail_OrderNum == detailNum).ToList();
                                                    #region 学生答题表
                                                    Model_Student_HomeWorkAnswer model = new Model_Student_HomeWorkAnswer();
                                                    if (listWhere.Count == 1)
                                                    {
                                                        model = listWhere[0];
                                                        model.Student_Score = Convert.ToDecimal(itemTQ_S.studentScore);
                                                        if (Convert.ToDecimal(itemTQ_S.studentScore) == Convert.ToDecimal(itemTQ_S.scoreText))
                                                        {
                                                            model.Student_Answer_Status = "right";//对
                                                        }
                                                        else if (Convert.ToDecimal(itemTQ_S.studentScore) == 0)
                                                        {
                                                            model.Student_Answer_Status = "wrong";//错
                                                        }
                                                        else
                                                        {
                                                            model.Student_Answer_Status = "partright";//部分对
                                                        }
                                                        model.isRead = 1;
                                                        listSHWA.Add(model);
                                                    }
                                                    #endregion
                                                }
                                                #region 保存批注
                                                if (itemTQ.comment != null)
                                                {
                                                    string filePath = string.Format("{0}\\{1}.txt", studentHomeWorkId, itemTQ.Testid);
                                                    pfunction.WriteToFile(uploadPath + filePath, itemTQ.comment, true);
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                    #endregion
                                    #region 综合题型 listBig
                                    List<TchMKTQListBig> listTchMKBig = mkModel.testListBig;
                                    if (listTchMKBig != null)
                                    {
                                        foreach (var itemBig in listTchMKBig)
                                        {
                                            if (itemBig != null && itemBig.testList != null)
                                            {
                                                foreach (var itemTQ in itemBig.testList)
                                                {
                                                    if (itemTQ != null && itemTQ.list != null && (itemTQ.testType == "fill" || itemTQ.testType == "answers"))
                                                    {
                                                        int detailNum = 0;
                                                        foreach (var itemTQ_S in itemTQ.list)
                                                        {
                                                            detailNum++;
                                                            List<Model_Student_HomeWorkAnswer> listWhere = listDataSHWA.Where(w => w.TestQuestions_Id == itemTQ.Testid && w.TestQuestions_Detail_OrderNum == detailNum).ToList();
                                                            #region 学生答题表
                                                            Model_Student_HomeWorkAnswer model = new Model_Student_HomeWorkAnswer();
                                                            if (listWhere.Count == 1)
                                                            {
                                                                model = listWhere[0];
                                                                model.Student_Score = Convert.ToDecimal(itemTQ_S.studentScore);
                                                                if (Convert.ToDecimal(itemTQ_S.studentScore) == Convert.ToDecimal(itemTQ_S.scoreText))
                                                                {
                                                                    model.Student_Answer_Status = "right";//对
                                                                }
                                                                else if (Convert.ToDecimal(itemTQ_S.studentScore) == 0)
                                                                {
                                                                    model.Student_Answer_Status = "wrong";//错
                                                                }
                                                                else
                                                                {
                                                                    model.Student_Answer_Status = "partright";//部分对
                                                                }
                                                                model.isRead = 1;
                                                                listSHWA.Add(model);
                                                            }
                                                            #endregion
                                                        }
                                                        #region 保存批注
                                                        if (itemTQ.comment != null)
                                                        {
                                                            string filePath = string.Format("{0}\\{1}.txt", studentHomeWorkId, itemTQ.Testid);
                                                            pfunction.WriteToFile(uploadPath + filePath, itemTQ.comment, true);
                                                        }
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #endregion

                                    #region 保存老师批改笔记信息
                                    if (mkModel.testpaperMarking != null)
                                    {
                                        string filePath = string.Format("{0}\\{0}.txt", studentHomeWorkId);
                                        pfunction.WriteToFile(uploadPath + filePath, mkModel.testpaperMarking, true);
                                    }
                                    #endregion

                                    #region 更新批改时间，统计帮助表
                                    DataTable dtHWDetail = new BLL_HomeWork().GetHWDetail(modelSHW.HomeWork_Id).Tables[0];
                                    Model_StatsHelper modelSH_HW = new Model_StatsHelper();
                                    modelSH_HW.StatsHelper_Id = Guid.NewGuid().ToString();
                                    modelSH_HW.ResourceToResourceFolder_Id = dtHWDetail.Rows[0]["ResourceToResourceFolder_Id"].ToString();
                                    modelSH_HW.Homework_Id = modelSHW.HomeWork_Id;
                                    modelSH_HW.Correct_Time = DateTime.Now;
                                    modelSH_HW.Exec_Status = "0";
                                    modelSH_HW.SType = "1";
                                    modelSH_HW.CreateUser = userId;
                                    modelSH_HW.SchoolId = dtHWDetail.Rows[0]["SchoolId"].ToString();
                                    modelSH_HW.GradeId = dtHWDetail.Rows[0]["GradeId"].ToString();
                                    #endregion

                                    int result = bll.TeacherCorrectStuHomeWork(modelshwCorrect, listSHWA, modelSH_HW);
                                    if (result > 0)
                                    {
                                        try
                                        {
                                            #region 修改学生答题文件，保存分值
                                            strTestWebSiteUrl = pfunction.getHostPath();

                                            string fileUrl = strTestWebSiteUrl + "\\Upload\\Resource\\{0}\\{1}\\{2}\\{2}.txt";
                                            //读取学生答题信息
                                            string stuInfo = RemotWeb.PostDataToServer(string.Format(fileUrl
                                                , "studentAnswerForMarking"
                                                , pfunction.ToShortDate(modelHW.CreateTime.ToString())
                                                , studentHomeWorkId), "", Encoding.UTF8, "Get");

                                            List<Model_Student_HomeWorkAnswer> listStuScore = new BLL_Student_HomeWorkAnswer().GetModelList("Student_HomeWork_Id='" + studentHomeWorkId + "'");
                                            try
                                            {
                                                StuAnswerForMarkingModel modelStuAnswer = new StuAnswerForMarkingModel();
                                                modelStuAnswer = JsonConvert.DeserializeObject<StuAnswerForMarkingModel>(stuInfo);
                                                #region 普通题型 list
                                                if (modelStuAnswer.list != null)
                                                {
                                                    foreach (TestPaperAnswerModel item in modelStuAnswer.list)
                                                    {
                                                        if (item != null && item.list != null)
                                                        {
                                                            int sNum = 0;
                                                            foreach (var itemScore in item.list)
                                                            {
                                                                sNum++;
                                                                List<Model_Student_HomeWorkAnswer> listStuScoreSub = listStuScore.Where(o => o.TestQuestions_Id == item.Testid && o.TestQuestions_Detail_OrderNum == sNum).ToList();
                                                                if (listStuScoreSub.Count > 0) itemScore.studentScore = listStuScoreSub[0].Student_Score.ToString();

                                                            }
                                                        }
                                                    }
                                                }
                                                #endregion
                                                #region 综合题型 listBig
                                                if (modelStuAnswer.listBig != null)
                                                {
                                                    foreach (TestPaperAnswerModelBig itemBig in modelStuAnswer.listBig)
                                                    {
                                                        if (itemBig != null && itemBig.list != null)
                                                        {
                                                            foreach (TestPaperAnswerModel item in itemBig.list)
                                                            {
                                                                if (item != null && item.list != null)
                                                                {
                                                                    int sNum = 0;
                                                                    foreach (var itemScore in item.list)
                                                                    {
                                                                        sNum++;
                                                                        List<Model_Student_HomeWorkAnswer> listStuScoreSub = listStuScore.Where(o => o.TestQuestions_Id == item.Testid && o.TestQuestions_Detail_OrderNum == sNum).ToList();
                                                                        if (listStuScoreSub.Count > 0) itemScore.studentScore = listStuScoreSub[0].Student_Score.ToString();

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                #endregion
                                                string savePath = "{0}{1}\\{2}\\{3}\\{3}.txt";
                                                //重新保存学生答题信息
                                                pfunction.WriteToFile(string.Format(savePath
                                                    , Server.MapPath("..\\Upload\\Resource\\")
                                                    , "studentAnswerForMarking"
                                                    , pfunction.ToShortDate(modelHW.CreateTime.ToString())
                                                    , studentHomeWorkId), JsonConvert.SerializeObject(modelStuAnswer), true);
                                            }
                                            catch (Exception)
                                            {
                                                List<TestPaperAnswerModel> listStu = new List<TestPaperAnswerModel>();
                                                listStu = JsonConvert.DeserializeObject<List<TestPaperAnswerModel>>(stuInfo);
                                                #region 普通题型 list
                                                if (listStu != null)
                                                {
                                                    foreach (TestPaperAnswerModel item in listStu)
                                                    {
                                                        if (item != null && item.list != null)
                                                        {
                                                            int sNum = 0;
                                                            foreach (var itemScore in item.list)
                                                            {
                                                                sNum++;
                                                                List<Model_Student_HomeWorkAnswer> listStuScoreSub = listStuScore.Where(o => o.TestQuestions_Id == item.Testid && o.TestQuestions_Detail_OrderNum == sNum).ToList();
                                                                if (listStuScoreSub.Count > 0) itemScore.studentScore = listStuScoreSub[0].Student_Score.ToString();

                                                            }
                                                        }
                                                    }
                                                }
                                                #endregion
                                                string savePath = "{0}{1}\\{2}\\{3}\\{3}.txt";
                                                //重新保存学生答题信息
                                                pfunction.WriteToFile(string.Format(savePath
                                                     , Server.MapPath("..\\Upload\\Resource\\")
                                                     , "studentAnswerForMarking"
                                                     , pfunction.ToShortDate(modelHW.CreateTime.ToString())
                                                     , studentHomeWorkId), JsonConvert.SerializeObject(listStu), true);
                                            }
                                            #endregion
                                        }
                                        catch (Exception)
                                        {

                                        }
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = true,
                                            errorMsg = "",
                                            errorCode = ""
                                        });
                                    }
                                    else
                                    {
                                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            errorMsg = "批改失败",
                                            errorCode = "markingTestPaper"
                                        });
                                    }
                                }
                                else
                                {
                                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                    {
                                        status = false,
                                        errorMsg = "批改数据为空，批改失败",
                                        errorCode = "markingTestPaper"
                                    });
                                }

                            }
                            else
                            {
                                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    errorMsg = "对不起，此账号已在其他机器登录。",
                                    errorCode = "markingTestPaper",
                                    tokenStatus = false
                                });
                            }
                        }
                        else
                        {
                            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                status = false,
                                errorMsg = "非法操作",
                                errorCode = "markingTestPaper"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "markingTestPaper"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion
                #region 36. 接口名称: downLoadResource 下载资源
                case "downloadresource":
                    try
                    {
                        string iid = Request["iid"].Filter();
                        string tabid = Request["TabId"].Filter();
                        string UserId = Request["UserId"].Filter();
                        // string productType = Request["ProductType"].Filter();       
                        DataTable dt = new DataTable();
                        string strSqlTemp = string.Empty;
                        strSqlTemp = @"select A.Book_ID,A.Resource_Url,A.File_Suffix,A.Resource_Name from ResourceToResourceFolder A 
 where ResourceToResourceFolder_Id='" + iid.Filter() + "' ";
                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlTemp).Tables[0];
                        if (dt.Rows.Count == 1)
                        {
                            #region 记录教案访问情况
                            Model_visit_client modelVC = new Model_visit_client();
                            modelVC.visit_client_id = Guid.NewGuid().ToString();
                            modelVC.user_id = UserId;
                            modelVC.resource_data_id = iid;
                            modelVC.product_type = productType;
                            modelVC.tab_id = tabid;
                            modelVC.open_time = DateTime.Now;
                            modelVC.operate_type = "view";
                            new BLL_visit_client().Add(modelVC);
                            #endregion

                            string strResourceUrl = string.Empty;
                            /// BookAttrModel bkAttrModel = GetBookAttrValue(dt.Rows[1]["Book_ID"].ToString());
                            strResourceUrl = Server.MapPath(string.Format("{0}/{1}", Rc.Common.ConfigHelper.GetConfigString("DocumentUrl"), dt.Rows[0]["Resource_Url"].ToString()));

                            string strFileSuffix = string.Empty;
                            strFileSuffix = dt.Rows[0]["File_Suffix"].ToString();
                            if (strFileSuffix == "testPaper")
                            {
                                strFileSuffix = "dsc";
                            }
                            //string strJson = "{isPrint:" + bkAttrModel.IsPrint + ",isSave:" + bkAttrModel.IsSave + "}";
                            pfunction.ToDownloadBase64(strResourceUrl, dt.Rows[0]["Resource_Name"].ToString() + "." + dt.Rows[0]["File_Suffix"].ToString());
                        }


                    }
                    catch (Exception ex)
                    {
                        strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = ex.Message.ToString(),
                            errorCode = "downLoadResource"
                        });
                    }
                    Response.Write(strJsion);
                    break;
                #endregion

            }
        }
        private string GetPaperHeaderDoc(string strResourceToResourceFolder_Id)
        {
            string strTemp = string.Empty;
            object obj = Rc.Common.DBUtility.DbHelperSQL.GetSingle("select paperHeaderDoc from ResourceToResourceFolder_Property where ResourceToResourceFolder_Id ='" + strResourceToResourceFolder_Id + "'");
            if (obj != null)
            {
                strTemp = obj.ToString();
            }
            return strTemp;
        }
        private object AddanswerResultListByType(Model_ResourceToResourceFolder modelRTRF, string Student_HomeWork_Id, string HomeWork_CreateTime, Model_TestQuestions item, string userId)
        {
            Model_Student_HomeWork modelSHW = new BLL_Student_HomeWork().GetModel(Student_HomeWork_Id);
            Model_HomeWork modelHW = new BLL_HomeWork().GetModel(modelSHW.HomeWork_Id);
            string uploadPath = "/Upload/Resource/"; //存储文件基础路径
            //生成存储路径
            string savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm,
                modelRTRF.Resource_Version, modelRTRF.Subject);
            string fileUrl = uploadPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径
            string fileStudentAnswerUrl = uploadPath + "{0}\\" + (string.IsNullOrEmpty(HomeWork_CreateTime) ? "" : (pfunction.ToShortDate(HomeWork_CreateTime.ToString()) + "\\")) + savePath + "{1}.{2}";//文件详细路径
            string listStandardsScore = string.Empty;// 标准分值
            string listAnswerScore = string.Empty;// 得分值
            string listAnswerImg = string.Empty;// 答题内容（isHTML 为 T 时，此值有效） 可一题多空
            string listRightAnswerImg = string.Empty;// 标准答案（isHTML 为 T 时，此值有效） 可一题多空
            string listAnswerChooses = string.Empty;// 答题内容（isHTML 为 F 时，此值有效）
            string listRightAnswer = string.Empty;// 标准答案（isHTML 为 F 时，此值有效）
            string strIsHTML = string.Empty;//
            string strtopicNum = string.Empty;//从Student_HomeWorkAnswer中得到
            #region 试题对象
            string strSql = string.Empty;
            strSql = string.Format(@"select t2.*,t1.ScoreText,t1.TestCorrect,t1.TestQuestions_Score,t1.AnalyzeHyperlinkData,t1.TrainHyperlinkData from TestQuestions_Score t1
inner join Student_HomeWorkAnswer t2 on 
t1.TestQuestions_Id=t2.TestQuestions_Id and t1.TestQuestions_Score_ID=t2.TestQuestions_Score_ID
and  t2.Student_HomeWork_Id='{0}' where t1.TestQuestions_Id='{1}' order by TestQuestions_Num,TestQuestions_OrderNum ", Student_HomeWork_Id, item.TestQuestions_Id);
            DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            //object objSHWASubList = new object();

            List<object> objobjSHWASubList = new List<object>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strAnalyzeUrl = string.Empty;
                string strTrainUrl = string.Empty;
                string strAnswerHTML = string.Empty;
                string strRightAnswerHTML = string.Empty;
                string strAnswerChooses = string.Empty;
                string strRightAnswer = string.Empty;
                strtopicNum = dt.Rows[i]["TestQuestions_NumStr"].ToString();
                if (dt.Rows[i]["AnalyzeHyperlinkData"].ToString() != "")
                {
                    strAnalyzeUrl += pfunction.getHostPath() + dt.Rows[i]["AnalyzeHyperlinkData"].ToString();
                }
                if (dt.Rows[i]["TrainHyperlinkData"].ToString() != "")
                {
                    strTrainUrl += pfunction.getHostPath() + dt.Rows[i]["TrainHyperlinkData"].ToString();
                }

                if (item.TestQuestions_Type == "selection" || item.TestQuestions_Type == "clozeTest")
                {
                    strAnswerChooses = dt.Rows[i]["Student_Answer"].ToString();
                    strRightAnswer = dt.Rows[i]["TestCorrect"].ToString();
                    strIsHTML = "F";
                }
                else if (item.TestQuestions_Type == "truefalse")
                {
                    strAnswerChooses = dt.Rows[i]["Student_Answer"].ToString();
                    strAnswerHTML = dt.Rows[i]["Student_Answer"].ToString();

                    strRightAnswer = dt.Rows[i]["TestCorrect"].ToString();
                    strRightAnswerHTML = dt.Rows[i]["TestCorrect"].ToString();
                    strIsHTML = "T";
                }
                else if (item.TestQuestions_Type != "title")
                {
                    strIsHTML = "T";
                    strAnswerHTML = RemotWeb.PostDataToServer(pfunction.getHostPath() + string.Format(fileStudentAnswerUrl, "studentAnswer", dt.Rows[i]["Student_HomeWorkAnswer_Id"], "txt"), "", Encoding.UTF8, "Get") + ",";
                    strRightAnswerHTML = RemotWeb.PostDataToServer(pfunction.getHostPath() + string.Format(fileUrl, "testQuestionCurrent", dt.Rows[i]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                }
                object objSHWASub = new
                {
                    isHTML = strIsHTML,//暂时不要// 是否以图片提交 F: 文本提交； T: 图片提交   
                    standardsScore = dt.Rows[i]["TestQuestions_Score"].ToString(),   // 标准分值    
                    answerScore = dt.Rows[i]["Student_Score"].ToString(),// 得分值
                    analyzeUrl = strAnalyzeUrl,// 解析 URL, 指向上传时的：analyzeHyperlinkData 
                    trainUrl = strTrainUrl,// 继续强化训练 URL， 指向上传时的：trainHyperlinkData 
                    answerHTML = strAnswerHTML,// 答题内容（isHTML 为 T 时，此值有效） 可一题多空
                    rightAnswerHTML = strRightAnswerHTML,// 标准答案（isHTML 为 T 时，此值有效） 可一题多空
                    answerChooses = strAnswerChooses,// 答题内容（isHTML 为 F 时，此值有效）
                    rightAnswer = strRightAnswer // 标准答案（isHTML 为 F 时，此值有效）
                };

                objobjSHWASubList.Add(objSHWASub);
            }
            #endregion

            if (objobjSHWASubList.Count > 0)
            {
                object obj = new
                {
                    Testid = item.TestQuestions_Id,
                    topicNum = strtopicNum,
                    list = objobjSHWASubList
                };
                return obj;
            }
            else
            {
                return null;

            }
        }


        /// <summary>
        /// 获取列表：学生可见的testpaper资源列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <param name="folderId"></param>
        /// <param name="tabindex"></param>
        /// <returns></returns>
        private string GetStudentResource(string token, string userId, string folderId, string strResource_Type, string tabid, string productType)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            bool isWritable = false;
            bool isPrintable = false;                  // 资源是否可打印，true可打印。
            bool isPrivate = false;                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
            bool isSubmited = false;                   // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
            bool permitDel = false;                      // 资源是否可删除，默认为 true
            bool permitMove = false;                     // 资源是否可移动，默认为 true
            bool permitSave = false;                    // 资源是否可保存，默认为 true
            bool TheBookWasPaied = false;                  //是否购买了此资源 ，true 购买了
            #region 学生
            if (folderId == "0")
            {
                #region 学科
                //                string strBuyRe = string.Format(@" and Common_Dict_Id in( select [Subject] from dbo.ResourceFolder
                //where book_id in(
                //select Book_Id from dbo.UserBuyResources where UserId='{0}') ) ", userId);
                //                List<Model_Common_Dict> listDict = new BLL_Common_Dict().GetModelList("D_Type='7' " + strBuyRe + " order by D_Name ");
                DataTable dtF = new DataTable();
                strSql = new StringBuilder();
                strSql.Append(" select dic.Common_Dict_ID,dic.D_Name,dic.D_CreateTime,dic.D_CreateUser ");
                string strWhere = string.Empty;
                if (tabid == EnumTabindex.StudentSkillNew.ToString())
                {
                    strWhere += " and SHWSubmit.Student_HomeWork_Status='0' ";
                }
                else if (tabid == EnumTabindex.StudentSkillSubminted.ToString())
                {
                    strWhere += " and SHWSubmit.Student_HomeWork_Status='1' ";
                }
                else if (tabid == EnumTabindex.StudentSkillWrong.ToString())
                {
                    strWhere += string.Format(" and SHWSubmit.Student_HomeWork_Status='1' and SHWCorrect.Student_HomeWork_CorrectStatus='1' ");
                    strWhere += string.Format(" and shw.Student_HomeWork_Id in(select Student_HomeWork_Id from Student_HomeWorkAnswer where Student_Id='{0}' and  Student_Answer_Status!='right' and Student_Answer_Status!='unknown' ) "
                        , userId);
                }

                strSql.AppendFormat(@" from Common_Dict dic where dic.D_Type='7' and Common_Dict_Id in(
select SubjectId from HomeWork hw inner join Student_HomeWork shw on shw.HomeWork_Id=hw.HomeWork_Id
                                  inner join Student_HomeWork_Submit SHWSubmit on SHWSubmit.Student_HomeWork_Id=shw.Student_HomeWork_id
                                   inner join Student_HomeWork_Correct SHWCorrect on SHWCorrect.Student_HomeWork_Id=shw.Student_HomeWork_id where shw.Student_Id='{0}' )  ", userId);
                dtF = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];

                string strSqlCount = string.Format(@"select hw.SubjectId,count(1) as icount from dbo.Student_HomeWork SHW
                inner join HomeWork HW on SHW.HomeWork_Id=HW.HomeWork_Id
                inner join Student_HomeWork_Submit SHWSubmit on SHWSubmit.Student_HomeWork_Id=shw.Student_HomeWork_id
                inner join Student_HomeWork_Correct SHWCorrect on SHWCorrect.Student_HomeWork_Id=shw.Student_HomeWork_id
                where SHW.Student_Id='{0}' {1} group by hw.SubjectId ", userId, strWhere);
                DataTable dtCount = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlCount).Tables[0];

                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    int intCount = 0;
                    DataRow[] drCount = dtCount.Select("SubjectId='" + dtF.Rows[i]["Common_Dict_ID"].ToString() + "'");
                    if (drCount.Length > 0)
                    {
                        int.TryParse(drCount[0]["icount"].ToString(), out intCount);
                    }
                    listObj.Add(new
                    {
                        id = dtF.Rows[i]["Common_Dict_ID"].ToString(),
                        title = string.Format("{0}({1})", dtF.Rows[i]["D_Name"].ToString(), intCount),
                        isFolder = true,
                        ext = "",
                        typeId = Rc.Common.Config.Resource_TypeConst.testPaper类型文件,
                        typeName = "作业",
                        fileType = "folder",
                        dateCreated = pfunction.ConvertToLongDateTime(dtF.Rows[i]["D_CreateTime"].ToString()),
                        userId = dtF.Rows[i]["D_CreateUser"].ToString(),
                        //userName = GetUserNameByUserId(item.D_CreateUser),
                        isWritable = isWritable,                      // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                        isPrintable = false,                  // 资源是否可打印，true可打印。
                        isPrivate = isPrivate,                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                        isSubmited = isSubmited,                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                        permitDel = permitDel,                      // 资源是否可删除，默认为 true
                        permitMove = permitMove,                     // 资源是否可移动，默认为 true
                        permitSave = permitSave,                     // 资源是否可保存，默认为 true
                        version = "",
                        streamUrl = "",
                        downloadUrl = "",
                        visitUrl = ""
                    });
                }
                #endregion
            }
            else
            {
                #region 学生
                strSql.Append("select top 100 SHW.Student_HomeWork_Id,SHW.HomeWork_Id,SHW.Student_Id,SHW.CreateTime,SHWSubmit.Student_HomeWork_Status,SHWCorrect.Student_HomeWork_CorrectStatus,HW.HomeWork_Name,HW.HomeWork_AssignTeacher,HW.BeginTime,HW.StopTime,HW.isTimeLimt,RTRF.ResourceToResourceFolder_Id,RTRF.ResourceFolder_Id,RTRF.Resource_Id,");
                strSql.Append("RTRF.File_Name,RTRF.File_Owner,REPLACE(RTRF.File_Suffix,'.','') File_Suffix,RTRF.Resource_Type,RTRF.CreateFUser,RTRF.Resource_Version,ISNULL(UBR.Book_ID,'-1') AS TheBookWasPaied,bk.BookPrice,RTRF.Resource_Class from dbo.Student_HomeWork SHW");
                strSql.Append(" inner join HomeWork HW on SHW.HomeWork_Id=HW.HomeWork_Id");
                strSql.Append(" inner join Student_HomeWork_Submit SHWSubmit on SHWSubmit.Student_HomeWork_Id=SHW.Student_HomeWork_id");
                strSql.Append(" inner join Student_HomeWork_Correct SHWCorrect on SHWCorrect.Student_HomeWork_Id=SHW.Student_HomeWork_id");
                strSql.Append(" left join ResourceToResourceFolder RTRF on RTRF.ResourceToResourceFolder_Id=HW.ResourceToResourceFolder_Id");
                strSql.Append(" LEFT JOIN Bookshelves bk ON bk.ResourceFolder_Id=RTRF.Book_id ");
                strSql.AppendFormat(" LEFT JOIN UserBuyResources UBR ON RTRF.Book_ID=UBR.Book_id AND UBR.UserId='{0}'", userId);
                //strSql.AppendFormat(" where 1=1");
                strSql.AppendFormat(" where hw.SubjectId='{0}' and ((HW.IsHide=1 AND HW.BeginTime<=GETDATE()) OR HW.IsHide=0 )", folderId);
                if (tabid == EnumTabindex.StudentSkillSubminted.ToString())//已完成作业
                {
                    strSql.AppendFormat(" and SHWSubmit.Student_HomeWork_Status='1' ");
                    isWritable = false;                 // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = false;                  // 资源是否可打印，true可打印。
                    isPrivate = true;                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = true;                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = false;                     // 资源是否可删除，默认为 true
                    permitMove = false;                      // 资源是否可移动，默认为 true
                    permitSave = false;                     // 资源是否可保存，默认为 true
                }
                else if (tabid == EnumTabindex.StudentSkillNew.ToString())//最新作业
                {
                    isWritable = false;                 // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = false;                  // 资源是否可打印，true可打印。
                    isPrivate = false;                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = false;                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = false;                     // 资源是否可删除，默认为 true
                    permitMove = false;                      // 资源是否可移动，默认为 true
                    permitSave = false;                     // 资源是否可保存，默认为 true
                    strSql.AppendFormat(" and SHWSubmit.Student_HomeWork_Status='0' ");
                }
                else if (tabid == EnumTabindex.StudentSkillWrong.ToString())//错题集
                {
                    strSql.Append(" and SHWSubmit.Student_HomeWork_Status='1' and SHWCorrect.Student_HomeWork_CorrectStatus='1' ");
                    strSql.AppendFormat(" and shw.Student_HomeWork_Id in(select Student_HomeWork_Id from Student_HomeWorkAnswer where Student_Id='{0}' and  Student_Answer_Status!='right' and Student_Answer_Status!='unknown' ) "
                        , userId);
                }
                strSql.AppendFormat(" and SHW.Student_Id='{0}' order by SHW.CreateTime desc ", userId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool boolIsFolder = false;
                    string strNoPaiedDesc = string.Empty;
                    if (dt.Rows[i]["TheBookWasPaied"].ToString().Trim() == "-1"
                        && dt.Rows[i]["BookPrice"].ToString() != "0.00"
                        && dt.Rows[i]["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        TheBookWasPaied = false;
                        isWritable = false;
                        strNoPaiedDesc = "你尚未购买此练习册，请联系您的老师。";
                    }
                    else
                    {
                        TheBookWasPaied = true;
                        strNoPaiedDesc = "";
                    }
                    if (dt.Rows[i]["isTimeLimt"].ToString() == "2")//考试
                    {
                        if (tabid == EnumTabindex.StudentSkillNew.ToString())//最新作业 提交作业截止时间
                        {
                            DateTime stopTime = DateTime.Now;
                            DateTime.TryParse(dt.Rows[i]["StopTime"].ToString(), out stopTime);
                            if (stopTime < DateTime.Now)
                            {
                                TheBookWasPaied = false;
                                isWritable = false;
                                strNoPaiedDesc = "已超过考试提交截止日期。";
                            }
                        }
                    }
                    string strFileType = dt.Rows[i]["File_Suffix"].ToString();
                    if (strFileType != "dsc" && strFileType != "class" && strFileType != "testPaper" && strFileType != "folder")
                    {
                        strFileType = "other";
                    }
                    #region 得到文件下载地址
                    string downLoadUrl = string.Empty;
                    string strDownLoadFileID = string.Empty;
                    string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                    string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                    strDownLoadFileID = dt.Rows[i]["ResourceFolder_Id"].ToString();
                    strDownLoadFileType = tabid;
                    if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                    {
                        strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                    }
                    //downLoadUrl = pfunction.GetDownLoadFileUrl(strDownLoadFileID, strDownLoadFileType, strDownLoadFileLocalInfo, userId, productType);
                    downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                       , pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, userId, productType);
                    #endregion
                    listObj.Add(new
                    {
                        id = dt.Rows[i]["Student_HomeWork_Id"],
                        title = dt.Rows[i]["HomeWork_Name"],
                        isFolder = boolIsFolder,
                        ext = strFileType,
                        typeId = dt.Rows[i]["Resource_Type"],
                        typeName = "学生作业",// GetD_Name(dt.Rows[i]["Resource_Type"].ToString()),
                        fileType = strFileType,
                        dateCreated = pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                        userId = dt.Rows[i]["CreateFUser"],
                        //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                        isWritable = isWritable,                        // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                        isPrintable = false,                            // 资源是否可打印，true可打印。
                        isPrivate = isPrivate,                          // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                        isSubmited = isSubmited,                        // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                        permitDel = permitDel,                          // 资源是否可删除，默认为 true
                        permitMove = permitMove,                        // 资源是否可移动，默认为 true
                        permitSave = permitSave,                        // 资源是否可保存，默认为 true
                        wasPaied = TheBookWasPaied,                     // 是否购买了此资源，默认为true
                        noPaiedDesc = strNoPaiedDesc,                   // 没购买的提示
                        buyUrl = "",                  // 购书URL     
                        version = dt.Rows[i]["Resource_Version"],
                        streamUrl = "",
                        downloadUrl = downLoadUrl,
                        visitUrl = ""
                    });
                }
                #endregion
            }
            #endregion
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }
        /// <summary>
        /// 获取列表：学生可见的 云资源/微课件 资源列表
        /// </summary>
        private string GetStudentClassResource(string token, string userId, string folderId, string strResource_Type, string tabid, string productType)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            bool isWritable = false;
            bool isPrintable = false;                  // 资源是否可打印，true可打印。
            bool isPrivate = false;                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
            bool isSubmited = false;                   // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
            bool permitDel = false;                      // 资源是否可删除，默认为 true
            bool permitMove = false;                     // 资源是否可移动，默认为 true
            bool permitSave = false;                    // 资源是否可保存，默认为 true
            bool TheBookWasPaied = true;                  //是否购买了此资源 ，true 购买了
            #region 学生
            if (folderId == "0")
            {
                #region 学科
                //                string strBuyRe = string.Format(@" and Common_Dict_Id in( select [Subject] from dbo.ResourceFolder
                //where book_id in(
                //select Book_Id from dbo.UserBuyResources where UserId='{0}') ) ", userId);
                //                List<Model_Common_Dict> listDict = new BLL_Common_Dict().GetModelList("D_Type='7' " + strBuyRe + " order by D_Name ");
                DataTable dtF = new DataTable();
                strSql = new StringBuilder();
                strSql.Append(" select dic.Common_Dict_ID,dic.D_Name,dic.D_CreateTime,dic.D_CreateUser ");
                strSql.AppendFormat(@",DataCount=(select COUNT(1) from ResourceFolder where Subject=dic.Common_Dict_ID and Resource_Type='{0}'
and ResourceFolder_Id in(select Book_ID from UserBuyResources where UserId='{1}'))", strResource_Type, userId);


                strSql.AppendFormat(@" from Common_Dict dic where dic.D_Type='7' and Common_Dict_Id in( select [Subject] from dbo.ResourceFolder
where book_id in(select Book_Id from dbo.UserBuyResources where UserId='{0}') )  ", userId);
                dtF = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    listObj.Add(new
                    {
                        id = dtF.Rows[i]["Common_Dict_ID"].ToString() + "?",//多返回?，区分是否为学科文件夹
                        title = string.Format("{0}({1})", dtF.Rows[i]["D_Name"].ToString(), dtF.Rows[i]["DataCount"].ToString()),
                        isFolder = true,
                        ext = "",
                        typeId = strResource_Type,
                        typeName = "",//"微课件",
                        fileType = "folder",
                        dateCreated = pfunction.ConvertToLongDateTime(dtF.Rows[i]["D_CreateTime"].ToString()),
                        userId = dtF.Rows[i]["D_CreateUser"].ToString(),
                        //userName = GetUserNameByUserId(item.D_CreateUser),
                        isWritable = isWritable,                      // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                        isPrintable = false,                  // 资源是否可打印，true可打印。
                        isPrivate = isPrivate,                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                        isSubmited = isSubmited,                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                        permitDel = permitDel,                      // 资源是否可删除，默认为 true
                        permitMove = permitMove,                     // 资源是否可移动，默认为 true
                        permitSave = permitSave,                     // 资源是否可保存，默认为 true
                        version = "",
                        streamUrl = "",
                        downloadUrl = "",
                        visitUrl = ""
                    });
                }
                #endregion
            }
            else
            {
                #region 学生

                if (folderId.IndexOf('?') > 0)//根据学科加载数据
                {
                    strSql.AppendFormat(@"select * from VW_ResourceAndResourceFolder  a
inner join UserBuyResources b on a.Book_ID=b.Book_id and b.userid='{0}'
 where ResourceFolder_Level='5' and Subject='{1}' "
                 , userId
                 , folderId.TrimEnd('?'));
                }
                else
                {
                    strSql.AppendFormat(@"select * from VW_ResourceAndResourceFolder  a
inner join UserBuyResources b on a.Book_ID=b.Book_id and b.userid='{0}'
 where ResourceFolder_ParentId='{1}'"
                 , userId
                 , folderId);
                }

                strSql.AppendFormat(" and Resource_Type ='{0}'", strResource_Type);
                strSql.AppendFormat(" and Resource_Class ='{0}'", Rc.Common.Config.Resource_ClassConst.云资源);

                strSql.Append(" order by ResourceFolder_Order,ResourceFolder_Name ");
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool boolIsFolder = false;
                    //RType=为文件夹
                    if (dt.Rows[i]["RType"].ToString() == "0") boolIsFolder = true;
                    string strNoPaiedDesc = string.Empty;
                    string strFileType = dt.Rows[i]["File_Suffix"].ToString();
                    if (strFileType != "dsc" && strFileType != "class" && strFileType != "testPaper" && strFileType != "folder")
                    {
                        strFileType = "other";
                    }
                    #region 得到文件下载地址
                    string downLoadUrl = string.Empty;
                    string strDownLoadFileID = string.Empty;
                    string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                    string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                    strDownLoadFileID = dt.Rows[i]["ResourceFolder_Id"].ToString();
                    strDownLoadFileType = tabid;
                    if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                    {
                        strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                    }
                    //downLoadUrl = pfunction.GetDownLoadFileUrl(strDownLoadFileID, strDownLoadFileType, strDownLoadFileLocalInfo, userId, productType);
                    downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                      , pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, userId, productType);
                    #endregion

                    BookAttrModel bkAttrModel = GetBookAttrValue(dt.Rows[i]["Book_ID"].ToString());
                    isPrintable = bkAttrModel.IsPrint;
                    permitSave = bkAttrModel.IsSave;

                    listObj.Add(new
                    {
                        id = dt.Rows[i]["ResourceFolder_Id"],
                        title = dt.Rows[i]["ResourceFolder_Name"].ToString(),
                        ParentId = dt.Rows[i]["ResourceFolder_ParentId"],
                        isFolder = boolIsFolder,
                        ext = strFileType,
                        typeId = dt.Rows[i]["Resource_Type"],
                        typeName = "",//GetD_Name(dt.Rows[i]["Resource_Type"].ToString()),
                        fileType = strFileType,
                        dateCreated = pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                        userId = dt.Rows[i]["CreateFUser"],
                        //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                        isWritable = isWritable,                        // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                        isPrintable = isPrintable,                            // 资源是否可打印，true可打印。
                        isPrivate = isPrivate,                          // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                        isSubmited = isSubmited,                        // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                        permitDel = permitDel,                          // 资源是否可删除，默认为 true
                        permitMove = permitMove,                        // 资源是否可移动，默认为 true
                        permitSave = permitSave,                        // 资源是否可保存，默认为 true
                        wasPaied = TheBookWasPaied,                     // 是否购买了此资源，默认为true
                        noPaiedDesc = strNoPaiedDesc,                   // 没购买的提示
                        buyUrl = "",                  // 购书URL     
                        version = dt.Rows[i]["Resource_Version"],
                        streamUrl = "",
                        downloadUrl = downLoadUrl,
                        visitUrl = ""
                    });
                }
                #endregion
            }
            #endregion
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }
        /// <summary>
        /// 获取列表：老师自有资源(包括：老师的ScienceWord文件类型,class文件类型，testPaper文件类型)
        /// </summary>
        private string GetTeacherOwnResource(string token, string userId, string folderId, string strResource_Type, string tabid, string productType)
        {
            #region 初始化默认文件夹
            string strWhere = string.Format("ResourceFolder_ParentId='0' and Resource_Class='{0}' and ResourceFolder_Owner='{1}' and Resource_Type='{2}'"
                , Rc.Common.Config.Resource_ClassConst.自有资源
                , userId
                , strResource_Type);
            BLL_ResourceFolder bllRF = new BLL_ResourceFolder();
            if (bllRF.GetRecordCount(strWhere) == 0)
            {
                Model_ResourceFolder modelRF = new Model_ResourceFolder();
                modelRF.ResourceFolder_Id = Guid.NewGuid().ToString();
                modelRF.ResourceFolder_ParentId = "0";
                modelRF.ResourceFolder_Name = "默认文件夹";
                modelRF.Resource_Type = strResource_Type;
                modelRF.Resource_Class = Rc.Common.Config.Resource_ClassConst.自有资源;
                modelRF.Resource_Version = "";
                modelRF.ResourceFolder_Remark = "";
                modelRF.ResourceFolder_Order = -1;
                modelRF.ResourceFolder_Owner = userId;
                modelRF.CreateFUser = userId;
                modelRF.CreateTime = DateTime.Now;
                modelRF.ResourceFolder_isLast = "0";
                bllRF.Add(modelRF);

            }
            #endregion
            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select * from VW_ResourceAndResourceFolder where ResourceFolder_ParentId='{0}' and CreateFUser='{1}'"
              , folderId, userId);

            if (strResource_Type != "")
            {
                strSql.AppendFormat(" and Resource_Type='{0}'", strResource_Type);
            }
            strSql.Append(" order by ResourceFolder_Order,ResourceFolder_Name ");
            DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool boolIsFolder = false;
                //RType=0为文件夹
                if (dt.Rows[i]["RType"].ToString() == "0") boolIsFolder = true;
                string strFileType = string.Empty;
                //老师ScienceWord 自有习题集
                if (tabid == EnumTabindex.TeacherScienceWordOwnSkill.ToString())
                {
                    strFileType = "testPaper";
                }
                #region 得到文件下载地址
                string downLoadUrl = string.Empty;
                string strDownLoadFileID = string.Empty;
                string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                strDownLoadFileID = dt.Rows[i]["ResourceFolder_Id"].ToString();
                strDownLoadFileType = tabid;
                if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                {
                    strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                }
                //downLoadUrl = pfunction.GetDownLoadFileUrl(strDownLoadFileID, strDownLoadFileType, strDownLoadFileLocalInfo, userId, productType);
                downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                       , pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, userId, productType);
                #endregion
                listObj.Add(new
                {
                    id = dt.Rows[i]["ResourceFolder_Id"],
                    title = dt.Rows[i]["ResourceFolder_Name"].ToString(),
                    ParentId = dt.Rows[i]["ResourceFolder_ParentId"],
                    isFolder = boolIsFolder,
                    ext = dt.Rows[i]["File_Suffix"],
                    typeId = dt.Rows[i]["Resource_Type"],
                    typeName = "自有习题集",//GetD_Name(dt.Rows[i]["Resource_Type"].ToString()),
                    fileType = strFileType,
                    dateCreated = pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                    userId = dt.Rows[i]["CreateFUser"],
                    //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                    isWritable = true,                      // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = false,                  // 资源是否可打印，true可打印。
                    isPrivate = false,                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = false,                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = true,                      // 资源是否可删除，默认为 true
                    permitMove = true,                     // 资源是否可移动，默认为 true
                    permitSave = true,                     // 资源是否可保存，默认为 true
                    version = dt.Rows[i]["Resource_Version"],
                    streamUrl = "",
                    downloadUrl = downLoadUrl,
                    visitUrl = ""
                });
            }
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }
        /// <summary>
        /// 获取列表：老师讲评
        /// </summary>
        private string GetTeacherComment(string token, string userId, string folderId, string strResource_Type, string tabid, string productType)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            DataTable dt = new DataTable();
            if (folderId == "0")
            {
                #region 班级目录
                //                strSql.AppendFormat(@"select ug.UserGroup_Id as Id,ug.UserGroup_Name as Name,ug.UserGroup_ParentId as ParentId,ug.CreateTime,ug.User_Id as CreateUser 
                //,0 as RType
                //from (
                //select t2.UserGroup_Id from Student_HomeWork t
                //inner join HomeWork t2 on t2.HomeWork_Id=t.HomeWork_Id
                //where t.Student_HomeWork_Status='1' and t2.HomeWork_AssignTeacher='{0}'
                //group by t2.UserGroup_Id
                //) t inner join UserGroup ug on ug.UserGroup_Id=t.UserGroup_Id order by ug.UserGroupOrder,UserGroup_Name"
                //                    , userId);

                string strWhere = @" and ClassId in(select ClassId from (
select distinct vw.ClassId,AnalysisDataCount=(select COUNT(1) from HomeWork where UserGroup_Id=vw.ClassId)
from VW_UserOnClassGradeSchool vw where ClassMemberShipEnum in('" + MembershipEnum.headmaster + "','" + MembershipEnum.teacher + "') and UserId='" + userId + "') t where AnalysisDataCount>0) ";
                strSql.AppendFormat(@"select distinct ug.UserGroup_Id as Id,ug.UserGroup_Name as Name,ug.UserGroup_ParentId as ParentId,ug.UserGroupOrder,ug.CreateTime,ug.User_Id as CreateUser ,0 as RType 
from VW_UserOnClassGradeSchool vw
inner join UserGroup ug on ug.UserGroup_Id=vw.ClassId
where ClassMemberShipEnum in('{0}','{1}')
and UserId='{2}'" + strWhere + " order by ug.UserGroupOrder,ug.UserGroup_Name"
                , MembershipEnum.headmaster
                , MembershipEnum.teacher
                , userId);
                #endregion
            }
            else
            {
                Model_F_User modelFUser = new BLL_F_User().GetModel(userId);
                #region 作业列表
                strSql.Append(@"SELECT T.ResourceToResourceFolder_Id,T.HomeWork_Id as Id,T.HomeWork_Name as Name,T.CreateTime,T.HomeWork_AssignTeacher as CreateUser,T.HomeWork_Status,re.Subject SubjectID,sc.StatsClassHW_ScoreID,sc.HighestScore,sc.LowestScore,sc.Mode,sc.AVGScore,sc.Median,1 as RType 
from homework T 
inner join dbo.ResourceToResourceFolder re on re.ResourceToResourceFolder_Id=T.ResourceToResourceFolder_Id 
left join UserGroup ug on ug.UserGroup_Id=T.UserGroup_Id
left join StatsClassHW_Score sc on T.HomeWork_Id=sc.HomeWork_ID ");
                strSql.Append("WHERE T.UserGroup_Id = '" + folderId + "' ");
                strSql.AppendFormat(@" and t.HomeWork_Id in(select t.HomeWork_Id from Student_HomeWork t
inner join HomeWork t2 on t2.HomeWork_Id=t.HomeWork_Id
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=t.Student_HomeWork_Id
where shwSubmit.Student_HomeWork_Status=1 and t2.UserGroup_Id='{0}') ", folderId);
                strSql.Append(StatsCommonHandle.GetStrWhereBySelfClassForComment(modelFUser.Subject));
                strSql.Append("order by T.CreateTime desc");
                #endregion
            }

            dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool boolIsFolder = false;
                string strfileType = string.Empty;
                string strgoWebPageType = string.Empty;
                string strgoWebPageParameterID = string.Empty;
                //RType=0为文件夹
                if (dt.Rows[i]["RType"].ToString() == "0")
                {
                    boolIsFolder = true;
                }
                else
                {
                    strfileType = "goWebPage";
                    strgoWebPageType = "CommentTestPaper";
                    strgoWebPageParameterID = string.Format("{0}^{1}^{2}"
                         , dt.Rows[i]["ResourceToResourceFolder_Id"]
                         , dt.Rows[i]["Id"]
                         , dt.Rows[i]["Name"]);
                }

                listObj.Add(new
                {
                    id = dt.Rows[i]["Id"],
                    title = dt.Rows[i]["Name"].ToString(),
                    ParentId = "",
                    isFolder = boolIsFolder,
                    ext = "",
                    typeId = "",
                    typeName = "",
                    fileType = strfileType,
                    goWebPageType = strgoWebPageType,
                    goWebPageParameterID = strgoWebPageParameterID,
                    dateCreated = pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                    userId = dt.Rows[i]["CreateUser"],
                    //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                    isWritable = true,                      // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = false,                  // 资源是否可打印，true可打印。
                    isPrivate = false,                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = false,                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = true,                      // 资源是否可删除，默认为 true
                    permitMove = true,                     // 资源是否可移动，默认为 true
                    permitSave = true,                     // 资源是否可保存，默认为 true
                    version = "",
                    streamUrl = "",
                    downloadUrl = "",
                    visitUrl = ""
                });
            }
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }
        /// <summary>
        ///  获取列表：云资源(包括：老师的ScienceWord文件类型,class文件类型，testPaper文件类型)
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <param name="folderId"></param>
        /// <param name="strResource_Type"></param>
        /// <param name="tabindex"></param>
        /// <param name="UserIdentity">身份</param>
        /// <returns></returns>
        private string GetCloudResource(string token, string userId, string folderId, string strResource_Type, string tabid, string UserIdentity, string productType)
        {

            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            if (UserIdentity == "A")
            {
                strSql.AppendFormat(@"select vw.*,ba.AuditState from VW_ResourceAndResourceFolder_Mgr vw left join BookAudit ba on ba.ResourceFolder_Id=vw.Book_Id where vw.ResourceFolder_ParentId='{0}'"
              , folderId, userId);
                strSql.AppendFormat(" and vw.Resource_Type in('{0}','{1}')", strResource_Type, Rc.Common.Config.Resource_TypeConst.按属性生成的目录);
                strSql.AppendFormat(" and vw.CreateFUser ='{0}'", userId);
            }
            else
            {
                if (folderId == "" || folderId == "0")//先取第一级
                {
                    strSql.AppendFormat(@"select a.*,ba.AuditState from VW_ResourceAndResourceFolder a left join BookAudit ba on ba.ResourceFolder_Id=a.Book_Id 
inner join UserBuyResources b on a.Book_ID=b.Book_id and b.userid='{0}'
where ResourceFolder_Level='{1}'"
                 , userId, 5);

                }
                else
                { //只取给老师授权的书籍
                    strSql.AppendFormat(@"select a.*,ba.AuditState from VW_ResourceAndResourceFolder  a left join BookAudit ba on ba.ResourceFolder_Id=a.Book_Id 
inner join UserBuyResources b on a.Book_ID=b.Book_id and b.userid='{0}'
 where ResourceFolder_ParentId='{1}'"
                 , userId, folderId);


                }
                strSql.AppendFormat(" and Resource_Type ='{0}'", strResource_Type);

                // strSql.AppendFormat(" and Resource_Type ='{0}'", strResource_Type);
            }


            strSql.AppendFormat(" and Resource_Class ='{0}'", Rc.Common.Config.Resource_ClassConst.云资源);

            strSql.Append(" order by ResourceFolder_Order,ResourceFolder_Name ");
            DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
            Rc.Cloud.Model.Model_Struct_Func UserFun;
            string Module_Id = "10100100"; //生产任务分配
            Rc.Cloud.Model.Model_VSysUserRole loginModel = new Rc.Cloud.BLL.BLL_VSysUserRole().GetSysUserInfoModelBySysUserId(userId);
            UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(userId, (loginModel == null ? "''" : clsUtility.ReDoStr(loginModel.SysRole_IDs, ',')), Module_Id);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool isWritable = false;                         // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                bool isPrintable = false;                  // 资源是否可打印，true可打印。
                bool isPrivate = false;                         // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                bool isSubmited = false;                        // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                bool permitDel = true;                         // 资源是否可删除，默认为 true
                bool permitMove = true;                        // 资源是否可移动，默认为 true
                bool permitSave = true;                        // 资源是否可保存，默认为 true
                string strFileType = string.Empty;
                string strFileSuffix = string.Empty;

                strFileSuffix = dt.Rows[i]["File_Suffix"].ToString();
                strFileType = dt.Rows[i]["Resource_Type"].ToString();
                if (strFileSuffix == "testPaper")
                {
                    strFileSuffix = "dsc";

                }

                if (strResource_Type == Rc.Common.Config.Resource_TypeConst.testPaper类型文件)
                {
                    strFileType = "testPaper";

                }

                if (UserIdentity == "T")
                {
                    permitSave = true;

                }
                else if (UserIdentity == "A")
                {


                    int inttemp = 0;
                    int.TryParse(dt.Rows[i]["ResourceFolder_Level"].ToString(), out inttemp);
                    isWritable = true;
                    permitDel = UserFun.Delete;
                    permitMove = UserFun.Edit;
                    //permitSave = UserFun.Add;
                    if (inttemp < 4 && inttemp != -1)
                    {
                        isWritable = false;
                        permitDel = false;
                        permitMove = false;
                        permitSave = false;
                    }
                }
                BookAttrModel bkAttrModel = GetBookAttrValue(dt.Rows[i]["Book_ID"].ToString());
                isPrintable = bkAttrModel.IsPrint;
                permitSave = bkAttrModel.IsSave;
                if (UserIdentity == "A")
                {
                    if (dt.Rows[i]["AuditState"].ToString() == "1")
                    {
                        //isWritable = false;
                        permitDel = false;
                        permitMove = false;
                        permitSave = false;
                    }
                }



                bool boolIsFolder = false;
                //RType=为文件夹
                if (dt.Rows[i]["RType"].ToString() == "0") boolIsFolder = true;
                #region 得到文件下载地址
                string downLoadUrl = string.Empty;
                string strDownLoadFileID = string.Empty;
                string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                strDownLoadFileID = dt.Rows[i]["ResourceFolder_Id"].ToString();
                strDownLoadFileType = tabid;
                if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                {
                    strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                }
                //downLoadUrl = pfunction.GetDownLoadFileUrl(strDownLoadFileID, strDownLoadFileType, strDownLoadFileLocalInfo, userId, productType);
                downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                       , pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, userId, productType);
                #endregion
                listObj.Add(new
                {
                    id = dt.Rows[i]["ResourceFolder_Id"],
                    title = GetSubstringFolderName(dt.Rows[i]["ResourceFolder_Name"].ToString(), dt.Rows[i]["ResourceFolder_Order"].ToString(), UserIdentity),
                    ParentId = dt.Rows[i]["ResourceFolder_ParentId"],
                    isFolder = boolIsFolder,
                    ext = strFileSuffix,
                    typeId = dt.Rows[i]["Resource_Type"],
                    typeName = "云作业",//GetD_Name(dt.Rows[i]["Resource_Type"].ToString()),
                    fileType = strFileType,
                    dateCreated = pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                    userId = dt.Rows[i]["CreateFUser"],
                    //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                    isWritable = isWritable,                      // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = isPrintable,                    // 资源是否可打印，true可打印。
                    isPrivate = isPrivate,                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = isSubmited,                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = permitDel,                      // 资源是否可删除，默认为 true
                    permitMove = permitMove,                     // 资源是否可移动，默认为 true
                    permitSave = permitSave,                     // 资源是否可保存，默认为 true
                    version = dt.Rows[i]["Resource_Version"],
                    streamUrl = "",
                    downloadUrl = downLoadUrl,
                    visitUrl = ""
                });
            }
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }
        /// <summary>
        /// 添加目录：老师自有资源目录(包括：老师的ScienceWord文件类型,class文件类型，testPaper文件类型)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="folderId"></param>
        /// <param name="title"></param>
        /// <param name="tabindex"></param>
        private string AddTeacherOwnResourcesForder(Model_F_User_Client modelFuser, string userId, string token, string folderId, string title, string strResource_Type, string tabid)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsion = string.Empty;
            BLL_ResourceFolder bll = new BLL_ResourceFolder();
            Model_ResourceFolder modelRFParent = new Model_ResourceFolder();
            Model_ResourceFolder modelRF = new Model_ResourceFolder();
            int order = 0;
            if (title.Length > 4 && (title.Substring(3, 1) == "-" || title.Substring(3, 1) == "－"))
            {
                int.TryParse(title.Substring(0, 3), out order);
                title = title.Substring(4);
            }
            if (bll.GetRecordCount("ResourceFolder_ParentId='" + folderId + "' and ResourceFolder_Name='" + title + "' and CreateFUser='" + userId + "' and Resource_Type='" + strResource_Type + "'") > 0)
            {
                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    status = false,
                    errorMsg = "文件夹名称已存在",
                    errorCode = "AddFolder"
                });
            }
            else
            {
                string guid = Guid.NewGuid().ToString();
                modelRF.ResourceFolder_Id = guid;
                modelRF.ResourceFolder_Name = title;
                modelRF.CreateFUser = userId;
                modelRF.CreateTime = DateTime.Now;
                modelRF.ResourceFolder_isLast = "0";
                modelRF.ResourceFolder_ParentId = folderId;
                modelRF.Resource_Type = strResource_Type;
                modelRF.Resource_Class = Rc.Common.Config.Resource_ClassConst.自有资源;
                modelRF.ResourceFolder_Owner = userId;
                modelRF.Subject = modelFuser.Subject;
                modelRF.ResourceFolder_Order = order;
                //如果为第一级目录
                if (folderId == "0")
                {
                    modelRF.ResourceFolder_Level = 0;
                }
                else
                {
                    modelRFParent = bll.GetModel(folderId);
                    if (modelRFParent != null)
                    {
                        modelRF.LessonPlan_Type = modelRFParent.LessonPlan_Type;
                        modelRF.GradeTerm = modelRFParent.GradeTerm;
                        modelRF.Subject = modelRFParent.Subject;
                        modelRF.ResourceFolder_Level = modelRFParent.ResourceFolder_Level + 1;
                        //modelRF.Resource_Type = modelRFParent.Resource_Type;
                        modelRF.Resource_Class = modelRFParent.Resource_Class;
                        modelRF.Resource_Version = modelRFParent.Resource_Version;
                    }
                }
                if (bll.Add(modelRF))
                {
                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = true,
                        errorMsg = "",
                        errorCode = ""
                    });
                }
                else
                {
                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = false,
                        errorMsg = "添加时，执行失败",
                        errorCode = "AddFolder"
                    });
                }
            }
            return strJsion;
        }
        /// <summary>
        /// 添加目录：如果是管理员、第1个TAB 添加目录(管理员添加云资源目录)暂时不在客户端实现
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="folderId"></param>
        /// <param name="title"></param>
        /// <param name="tabindex"></param>
        /// <returns></returns>
        private string AddMgrResourcesForder(string userId, string token, string folderId, string title, string strResource_Type, string tabid)
        {
            //string strParticularYear = string.Empty;
            //string strGradeTerm = string.Empty;
            //string strSubject = string.Empty;
            //string strTextBookVersion = string.Empty;
            string strError = string.Empty;
            //if (!String.IsNullOrEmpty(Request["ParticularYear"].ToString()))
            //{
            //    strParticularYear = Request["ParticularYear"].ToString();
            //}
            //if (!String.IsNullOrEmpty(Request["GradeTerm"].ToString()))
            //{
            //    strGradeTerm = Request["GradeTerm"].ToString();
            //}
            //if (!String.IsNullOrEmpty(Request["Subject"].ToString()))
            //{
            //    strSubject = Request["Subject"].ToString();
            //}
            //if (!String.IsNullOrEmpty(Request["TextBookVersion"].ToString()))
            //{
            //    strTextBookVersion = Request["TextBookVersion"].ToString();
            //}

            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsion = string.Empty;
            BLL_ResourceFolder bll = new BLL_ResourceFolder();
            Model_ResourceFolder modelRFParent = new Model_ResourceFolder();
            string guid = Guid.NewGuid().ToString();
            Model_ResourceFolder modelRF = new Model_ResourceFolder();
            bool b = false;//是否可创建目录
            string errorMsg = string.Empty;

            int order = 0;
            if (title.Length > 4 && (title.Substring(3, 1) == "-" || title.Substring(3, 1) == "－"))
            {
                int.TryParse(title.Substring(0, 3), out order);
                title = title.Substring(4);
            }
            if (bll.GetRecordCount("ResourceFolder_ParentId='" + folderId + "' and ResourceFolder_Name='" + title + "' ") > 0)
            {
                errorMsg = "文件夹名称已存在";
            }
            else
            {
                modelRF.ResourceFolder_Id = guid;
                modelRF.ResourceFolder_Name = title;
                modelRF.ResourceFolder_Order = order;
                modelRF.CreateFUser = userId;
                modelRF.CreateTime = DateTime.Now;
                // modelRF.ResourceFolder_isLast = "0";
                modelRF.Resource_Type = strResource_Type;
                modelRFParent = bll.GetModelA(folderId);
                if (modelRFParent != null)
                {
                    if (modelRFParent.ResourceFolder_Level < 4)//如果在强制目录下创建目录
                    //if (false)
                    {
                        errorMsg = "此目录下不可创建目录或文件。";
                    }
                    else
                    {
                        modelRF.ParticularYear = modelRFParent.ParticularYear;
                        modelRF.GradeTerm = modelRFParent.GradeTerm;
                        modelRF.Resource_Version = modelRFParent.Resource_Version;
                        modelRF.Subject = modelRFParent.Subject;

                        modelRF.ResourceFolder_ParentId = folderId;
                        modelRF.LessonPlan_Type = modelRFParent.LessonPlan_Type;
                        //modelRF.GradeTerm = modelRFParent.GradeTerm;
                        //modelRF.Subject = modelRFParent.Subject;
                        modelRF.ResourceFolder_Level = modelRFParent.ResourceFolder_Level + 1;
                        //modelRF.Resource_Type = modelRFParent.Resource_Type;
                        modelRF.Resource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
                        //modelRF.Resource_Version = modelRFParent.Resource_Version;
                        if (modelRF.ResourceFolder_Level == 5)//当为书本的第一级目录时
                        {
                            modelRF.Book_ID = guid;
                        }
                        else
                        {
                            modelRF.Book_ID = modelRFParent.Book_ID;
                        }
                        if (bll.Add(modelRF))
                        {
                            #region 记录需要同步的数据
                            Model_SyncData modelSD = new Model_SyncData();
                            modelSD.SyncDataId = Guid.NewGuid().ToString();
                            modelSD.TableName = "ResourceFolder";
                            modelSD.DataId = guid;
                            modelSD.OperateType = "add";
                            modelSD.CreateTime = DateTime.Now;
                            modelSD.SyncStatus = "0";
                            new BLL_SyncData().Add(modelSD);
                            #endregion
                            b = true;
                        }
                        else
                        {
                            errorMsg = "sql执行失败";
                        }
                    }
                }
                else
                {
                    errorMsg = "文件夹父级编号参数错误";
                }
            }
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = b,
                errorMsg = errorMsg,
                errorCode = "AddFolder"
            });
            return strJsion;
        }

        /// <summary>
        /// 管理员 获取云资源目录树
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private object GetCloudResourceFolderTree(string strResourceFolder_ParentId, string strResource_Type, DataTable dt)
        {
            List<object> listObj = new List<object>();
            //List<Rc.Model.Resources.Model_ResourceFolderForClient> modelList = new List<Rc.Model.Resources.Model_ResourceFolderForClient>();
            string strWhere = string.Empty;
            strWhere = string.Format(" ResourceFolder_ParentId='{0}'", strResourceFolder_ParentId);
            DataRow[] dr = dt.Select(strWhere, "ResourceFolder_Order,ResourceFolder_Name");
            int rowsCount = dr.Length;
            if (rowsCount > 0)
            {
                Rc.Model.Resources.Model_ResourceFolderForClient model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dr[n]);
                    if (model != null)
                    {
                        string resourceFolder_Name = model.ResourceFolder_Name;
                        int order = 0;
                        int.TryParse(model.ResourceFolder_Order, out order);
                        if (order > 0)
                        {
                            resourceFolder_Name = GetSubstringFolderName(model.ResourceFolder_Name, model.ResourceFolder_Order, "A"); //string.Format("{0}-{1}", model.ResourceFolder_Order, model.ResourceFolder_Name);
                        }
                        listObj.Add(new
                        {
                            ResourceFolder_Id = model.ResourceFolder_Id,
                            ResourceFolder_Name = resourceFolder_Name,// model.ResourceFolder_Name,
                            list = GetCloudResourceFolderTree(model.ResourceFolder_Id, strResource_Type, dt)
                        });
                        // modelList.Add(model);
                    }
                }
                return listObj;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 老师获取自有资源目录树
        /// </summary>
        private object GetTeacherOwnResourceFolderTree(string userId, string strResourceFolder_ParentId, string strResource_Type, DataTable dt, string tabid)
        {
            List<object> listObj = new List<object>();
            string strWhere = string.Empty;
            if (strResourceFolder_ParentId == "0")
            {
                //if (tabindex == "1")
                //{

                //    strWhere = string.Format(" ResourceFolder_Level='{0}'", 4);
                //}
                //else
                //{
                //    strWhere = string.Format(" ResourceFolder_ParentId='{0}'", "0");
                //}
                strWhere = string.Format(" ResourceFolder_ParentId='{0}'", "0");

            }
            else
            {
                strWhere = string.Format(" ResourceFolder_ParentId='{0}'", strResourceFolder_ParentId);
            }

            List<Rc.Model.Resources.Model_ResourceFolderForClient> modelList = new List<Rc.Model.Resources.Model_ResourceFolderForClient>();
            DataRow[] dr = dt.Select(strWhere, "ResourceFolder_Order,ResourceFolder_Name");
            int rowsCount = dr.Length;
            if (rowsCount > 0)
            {
                Rc.Model.Resources.Model_ResourceFolderForClient model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dr[n]);
                    if (model != null)
                    {
                        listObj.Add(new
                        {
                            ResourceFolder_Id = model.ResourceFolder_Id,
                            ResourceFolder_Name = model.ResourceFolder_Name,
                            list = GetTeacherOwnResourceFolderTree(userId, model.ResourceFolder_Id, strResource_Type, dt, tabid)
                        });
                    }
                }
                return listObj;
            }
            else
            {
                return null;
            }


        }

        /// <summary>
        /// 搜索功能 获取搜索数据列表2016-06-08TS
        /// </summary>
        private string GetSearchResourceList(string keywords, string max, string userId, string strResource_Type, string productType, string tabid, string UserIdentity)
        {
            bool isWritable = false;                         // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
            bool isPrintable = false;                  // 资源是否可打印，true可打印。
            bool isPrivate = false;                         // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
            bool isSubmited = false;                        // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
            bool permitDel = true;                         // 资源是否可删除，默认为 true
            bool permitMove = true;                        // 资源是否可移动，默认为 true
            bool permitSave = true;                        // 资源是否可保存，默认为 true
            bool TheBookWasPaied = true;                  //是否购买了此资源 ，true 购买了
            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            int intMax = 10;
            int.TryParse(max, out intMax);
            if (UserIdentity == "A")
            {
                strSql.AppendFormat(@"select top {0} * from VW_ResourceAndResourceFolder_Mgr where RType='1' ", intMax);
                strSql.AppendFormat(" and CreateFUser='{0}' and Resource_Type='{1}' and ResourceFolder_Name  like '%{2}%' ", userId, strResource_Type, keywords);
            }
            else if (UserIdentity == "T")
            {
                strSql.AppendFormat(@"select top {0} * from VW_ResourceAndResourceFolder where RType='1'", intMax);
                if (tabid == EnumTabindex.TeacherScienceWordCloudTeachingPlan.ToString() || tabid == EnumTabindex.TeacherClassCloudTeachingPlan.ToString())
                {
                    strSql.AppendFormat(" and Book_Id in(select Book_Id from UserBuyResources where UserId='{0}') ", userId);
                }
                else
                {
                    strSql.AppendFormat(" and CreateFUser='{0}' ", userId);
                }
                strSql.AppendFormat(" and Resource_Type ='{0}'  and ResourceFolder_Name  like '%{1}%' ", strResource_Type, keywords);
            }
            else if (UserIdentity == "S")
            {
                strSql.AppendFormat("select top {0} SHW.Student_HomeWork_Id as ResourceFolder_Id,SHW.CreateTime,HW.HomeWork_Name as ResourceFolder_Name,1 AS RType,NULL AS Book_ID,HW.HomeWork_AssignTeacher,HW.BeginTime,HW.StopTime,RTRF.ResourceToResourceFolder_Id as ResourceFolder_Id,RTRF.ResourceToResourceFolder_Order as ResourceFolder_Order,RTRF.ResourceFolder_Id as ResourceFolder_ParentId", intMax);
                strSql.Append(",RTRF.File_Name,RTRF.File_Owner,REPLACE(RTRF.File_Suffix,'.','') File_Suffix,RTRF.Resource_Type,RTRF.CreateFUser,RTRF.Resource_Version,ISNULL(UBR.Book_ID,'-1') AS TheBookWasPaied from dbo.Student_HomeWork SHW");
                strSql.Append(" inner join HomeWork HW on SHW.HomeWork_Id=HW.HomeWork_Id");
                strSql.Append(" inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=SHW.Student_HomeWork_Id");
                strSql.Append(" inner join ResourceToResourceFolder RTRF on RTRF.ResourceToResourceFolder_Id=HW.ResourceToResourceFolder_Id");
                strSql.AppendFormat(" LEFT JOIN UserBuyResources UBR ON RTRF.Book_ID=UBR.Book_id AND UBR.UserId='{0}'", userId);
                //strSql.AppendFormat(" where 1=1");
                strSql.AppendFormat(" where RTRF.File_Name like '%{0}%' and ((HW.IsHide=1 AND HW.BeginTime<=GETDATE()) OR HW.IsHide=0 )", keywords);
                if (tabid == EnumTabindex.StudentSkillSubminted.ToString())//已完成作业
                {
                    strSql.AppendFormat(" and shwSubmit.Student_HomeWork_Status='1' ");
                    strSql.AppendFormat(" and SHW.Student_Id='{0}' order by SHW.CreateTime desc ", userId);
                    isWritable = false;                 // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = false;                  // 资源是否可打印，true可打印。
                    isPrivate = true;                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = true;                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = false;                     // 资源是否可删除，默认为 true
                    permitMove = false;                      // 资源是否可移动，默认为 true
                    permitSave = false;                     // 资源是否可保存，默认为 true
                }
                else if (tabid == EnumTabindex.StudentSkillNew.ToString())//最新作业
                {
                    isWritable = false;                 // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = false;                  // 资源是否可打印，true可打印。
                    isPrivate = false;                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = false;                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = false;                     // 资源是否可删除，默认为 true
                    permitMove = false;                      // 资源是否可移动，默认为 true
                    permitSave = false;                     // 资源是否可保存，默认为 true
                    strSql.AppendFormat(" and shwSubmit.Student_HomeWork_Status='0' ");
                    strSql.AppendFormat(" and SHW.Student_Id='{0}' order by SHW.CreateTime desc ", userId);
                }
                else if (tabid == EnumTabindex.StudentSkillWrong.ToString())//错题集
                {
                    strSql.AppendFormat(@" and HW.HomeWork_Id in (SELECT DISTINCT t1.HomeWork_Id FROM Student_HomeWorkAnswer t1
INNER JOIN Student_WrongHomeWork  t2 ON t1.Student_HomeWorkAnswer_Id=t2.Student_HomeWorkAnswer_Id
WHERE t1.Student_Id='{0}')", userId);
                    strSql.AppendFormat(" and SHW.Student_Id='{0}' order by SHW.CreateTime desc ", userId);
                }
                else if (tabid == EnumTabindex.StudentClassMicroClass.ToString())//学生微课件
                {
                    strSql = new StringBuilder();
                    strSql.AppendFormat(@"select top {0} * from VW_ResourceAndResourceFolder where RType='1'", intMax);
                    strSql.AppendFormat(" and Book_Id in(select Book_Id from UserBuyResources where UserId='{0}') ", userId);
                    strSql.AppendFormat(" and Resource_Type ='{0}'  and ResourceFolder_Name  like '%{1}%' ", strResource_Type, keywords);
                    strSql.Append(" order by ResourceFolder_Order,ResourceFolder_Name ");
                }
            }

            DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
            Rc.Cloud.Model.Model_Struct_Func UserFun;
            string Module_Id = "10100100"; //生产任务分配
            Rc.Cloud.Model.Model_VSysUserRole loginModel = new Rc.Cloud.BLL.BLL_VSysUserRole().GetSysUserInfoModelBySysUserId(userId);
            UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(userId, (loginModel == null ? "''" : clsUtility.ReDoStr(loginModel.SysRole_IDs, ',')), Module_Id);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strNoPaiedDesc = string.Empty;
                if (productType == "skill")
                {
                    if (dt.Rows[i]["TheBookWasPaied"].ToString().Trim() == "-1")
                    {
                        TheBookWasPaied = false;
                        isWritable = false;
                        strNoPaiedDesc = "你尚未购买此练习册，请联系您的老师。";
                    }
                    else
                    {
                        TheBookWasPaied = true;
                        strNoPaiedDesc = "";
                    }
                }
                if (tabid == EnumTabindex.StudentSkillNew.ToString())//最新作业 提交作业截止时间
                {
                    DateTime stopTime = DateTime.Now;
                    DateTime.TryParse(dt.Rows[i]["StopTime"].ToString(), out stopTime);
                    if (stopTime < DateTime.Now)
                    {
                        TheBookWasPaied = false;
                        isWritable = false;
                        strNoPaiedDesc = "已超过作业提交截止日期。";
                    }
                }
                string strFileType = string.Empty;
                string strFileSuffix = string.Empty;
                strFileSuffix = dt.Rows[0]["File_Suffix"].ToString();
                strFileType = dt.Rows[0]["File_Suffix"].ToString();
                if (strFileSuffix == "testPaper")
                {
                    strFileSuffix = "dsc";
                }
                if (strResource_Type == Rc.Common.Config.Resource_TypeConst.testPaper类型文件)
                {
                    strFileType = "testPaper";
                }
                if (UserIdentity == "T")
                {
                    permitSave = true;

                }
                else if (UserIdentity == "A")
                {


                    int inttemp = 0;
                    int.TryParse(dt.Rows[i]["ResourceFolder_Level"].ToString(), out inttemp);
                    isWritable = true;
                    permitDel = UserFun.Delete;
                    permitMove = UserFun.Edit;
                    //permitSave = UserFun.Add;
                    if (inttemp < 4 && inttemp != -1)
                    {
                        isWritable = false;
                        permitDel = false;
                        permitMove = false;
                        permitSave = false; ;
                    }



                }
                BookAttrModel bkAttrModel = GetBookAttrValue(dt.Rows[i]["Book_ID"].ToString());
                isPrintable = bkAttrModel.IsPrint;
                permitSave = bkAttrModel.IsSave;
                //if (UserIdentity == "A")
                //{
                //    if (dt.Rows[i]["AuditState"].ToString() == "1")
                //    {
                //        isWritable = false;
                //        permitMove = false;
                //        permitSave = false;
                //    }
                //}
                bool boolIsFolder = false;
                //RType=为文件夹
                if (dt.Rows[i]["RType"].ToString() == "0") boolIsFolder = true;

                #region 得到文件下载地址
                string downLoadUrl = string.Empty;
                string strDownLoadFileID = string.Empty;
                string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                strDownLoadFileID = dt.Rows[i]["ResourceFolder_Id"].ToString();
                strDownLoadFileType = tabid;
                if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                {
                    strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                }
                // downLoadUrl = pfunction.GetDownLoadFileUrl(strDownLoadFileID, strDownLoadFileType, strDownLoadFileLocalInfo, userId, productType);
                downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                          , pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, userId, productType);
                #endregion


                listObj.Add(new
                {
                    id = dt.Rows[i]["ResourceFolder_Id"],
                    title = GetSubstringFolderName(dt.Rows[i]["ResourceFolder_Name"].ToString(), dt.Rows[i]["ResourceFolder_Order"].ToString(), UserIdentity),
                    ParentId = dt.Rows[i]["ResourceFolder_ParentId"],
                    isFolder = boolIsFolder,
                    ext = strFileSuffix,
                    typeId = dt.Rows[i]["Resource_Type"],
                    typeName = "搜索",//GetD_Name(dt.Rows[i]["Resource_Type"].ToString()),
                    fileType = strFileType,
                    dateCreated = pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                    userId = dt.Rows[i]["CreateFUser"],
                    //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                    isWritable = isWritable,                      // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = isPrintable,                    // 资源是否可打印，true可打印。
                    isPrivate = isPrivate,                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = isSubmited,                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = permitDel,                      // 资源是否可删除，默认为 true
                    permitMove = permitMove,                     // 资源是否可移动，默认为 true
                    permitSave = permitSave,                     // 资源是否可保存，默认为 true
                    wasPaied = TheBookWasPaied,                     // 是否购买了此资源，默认为true
                    noPaiedDesc = strNoPaiedDesc,                   // 没购买的提示
                    version = dt.Rows[i]["Resource_Version"],
                    streamUrl = "",
                    downloadUrl = downLoadUrl,
                    visitUrl = ""
                });
            }
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }

        /// <summary>
        /// 客户端 验证用户是否登录(token是否有效)
        /// </summary>
        private Model_F_User_Client checkTokenIsValidBackModel(string userId, string token, string product_type)
        {
            try
            {
                //string strWhere = string.Format(" UserId='{0}' and Stoken='{1}' and convert(nvarchar(10),StokenTime,23)=convert(nvarchar(10),GETDATE(),23) ", userId, token);

                //if (new BLL_F_User().GetRecordCountA(string.Format(" UserId='{0}' ", userId)) == 0)
                //{
                //    return null;
                //}
                //else
                //{
                //    //return new BLL_F_User().GetModelA(userId);and convert(nvarchar(10),StokenTime,23)=convert(nvarchar(10),GETDATE(),23)
                //    string strWhere = string.Format(" where UserId='{0}' and Stoken = '{1}' ", userId, token);
                //    return new BLL_F_User().GetModelWhere(strWhere);
                //}
                return new BLL_F_User_Client().GetUserModelByClientToken(userId, token, product_type);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 客户端 用户登录
        /// </summary>
        private Model_F_User_Client userLoginBackModel(string userName, string pass)
        {
            try
            {
                return new BLL_F_User_Client().GetUserModelByClientLogin(userName, pass);
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// 根据UserId获取UserName
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserNameByUserId(string userId)
        {
            string temp = string.Empty;
            try
            {
                Model_F_User model = new BLL_F_User().GetModel(userId);
                if (model != null)
                {
                    temp = model.UserName;
                }
            }
            catch (Exception)
            {

            }
            return temp;
        }
        public string GetD_Name(string Dict_Id)
        {
            string temp = string.Empty;
            try
            {
                List<Model_Common_Dict> list = new BLL_Common_Dict().GetModelList("Common_Dict_ID='" + Dict_Id + "'");
                if (list.Count == 1)
                {
                    Model_Common_Dict model = list[0];
                    if (model != null)
                    {
                        temp = model.D_Name;
                    }
                }

            }
            catch (Exception)
            {

            }
            return temp;
        }

        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Rc.Model.Resources.Model_ResourceFolderForClient DataRowToModel(DataRow row)
        {
            Rc.Model.Resources.Model_ResourceFolderForClient model = new Rc.Model.Resources.Model_ResourceFolderForClient();
            if (row != null)
            {
                if (row["ResourceFolder_Id"] != null)
                {
                    model.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["ResourceFolder_ParentId"] != null)
                {
                    model.ResourceFolder_ParentId = row["ResourceFolder_ParentId"].ToString();
                }
                if (row["ResourceFolder_Name"] != null)
                {
                    model.ResourceFolder_Name = row["ResourceFolder_Name"].ToString();
                }
                if (row["ResourceFolder_Order"] != null)
                {
                    model.ResourceFolder_Order = row["ResourceFolder_Order"].ToString();
                }
                if (row["isStore_Files"] != null && row["isStore_Files"].ToString() != "")
                {
                    model.isStore_Files = row["isStore_Files"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 获取资源属性（是否可打印，存盘）
        /// </summary>
        /// <param name="ResourceFolder_Id"></param>
        /// <returns></returns>
        public BookAttrModel GetBookAttrValue(string ResourceFolder_Id)
        {
            BookAttrModel model = new BookAttrModel();
            model.IsPrint = false;
            model.IsSave = false;
            try
            {
                List<Model_BookAttrbute> listModel = new BLL_BookAttrbute().GetModelListByCache(ResourceFolder_Id);
                foreach (var item in listModel)
                {
                    if (item.AttrEnum == BookAttrEnum.Print.ToString() && item.AttrValue == "1")
                    {
                        model.IsPrint = true;
                    }
                    else if (item.AttrEnum == BookAttrEnum.Save.ToString() && item.AttrValue == "1")
                    {
                        model.IsSave = true;
                    }
                }
            }
            catch (Exception)
            {

            }
            return model;
        }

        #region 获取测评结果 类
        public class AnswerResultModel
        {
            public List<object> SingleChoice { get; set; }
            public List<object> MultipleChoice { get; set; }
            public List<object> BlankFilling { get; set; }
            public List<object> AnswerQuestions { get; set; }
            public List<object> JudgmentQuestions { get; set; }
        }
        #endregion

        #region 客户端标签枚举 类
        /// <summary>
        /// 客户端标签枚举
        /// </summary>
        public enum EnumTabindex
        {
            /// <summary>
            /// 管理员scienceword云教案
            /// </summary>
            MgrScienceWordCloudTeachingPlan,
            /// <summary>
            /// 管理员scienceword云习题集
            /// </summary>
            MgrScienceWordCloudSkill,
            /// <summary>
            /// 管理员class云教案
            /// </summary>
            MgrClassCloudTeachingPlan,
            /// <summary>
            /// 管理员class微课件
            /// </summary>
            MgrClassCloudMicroClass,
            /// <summary>
            /// 老师scienceword云教案
            /// </summary>
            TeacherScienceWordCloudTeachingPlan,
            /// <summary>
            /// 老师scienceword自有教案
            /// </summary>
            TeacherScienceWordOwnTeachingPlan,
            /// <summary>
            /// 老师scienceword云习题集
            /// </summary>
            TeacherScienceWordCloudSkill,
            /// <summary>
            /// 老师scienceword自有习题集
            /// </summary>
            TeacherScienceWordOwnSkill,
            /// <summary>
            /// 老师class云教案
            /// </summary>
            TeacherClassCloudTeachingPlan,
            /// <summary>
            /// 老师class讲评
            /// </summary>
            TeacherClassComment,
            /// <summary>
            /// 老师class自有教案
            /// </summary>
            TeacherClassOwnTeachingPlan,

            /// <summary>
            /// 学生skill已提交作业
            /// </summary>
            StudentSkillSubminted,
            /// <summary>
            /// 学生skill最新作业
            /// </summary>
            StudentSkillNew,
            /// <summary>
            /// 学生skill错题集
            /// </summary>
            StudentSkillWrong,
            /// <summary>
            /// 学生class微课件
            /// </summary>
            StudentClassMicroClass,
            /// <summary>
            /// 学生class云教案
            /// </summary>
            StudentClassCloudTeachingPlan
        }
        #endregion
        public class CommonDictModel
        {
            /// <summary>
            /// 字典标识
            /// </summary>
            public string ID { get; set; }
            /// <summary>
            /// 字典名称
            /// </summary>
            public string D_Name { get; set; }
            /// <summary>
            /// 字典类型
            /// </summary>
            public string D_Type { get; set; }
        }
        /// <summary>
        /// 资源属性 类
        /// </summary>
        public class BookAttrModel
        {
            /// <summary>
            /// 是否允许打印
            /// </summary>
            public bool IsPrint { get; set; }
            /// <summary>
            /// 是否允许存盘
            /// </summary>
            public bool IsSave { get; set; }
        }

        public string GetSubstringFolderName(string folderName, string folderOrder, string UserIdentity)
        {
            try
            {
                if (UserIdentity == "A")//管理员
                {
                    int order = 0;
                    int.TryParse(folderOrder, out order);
                    if (order > 0)
                    {
                        string strOrder = "";
                        strOrder = "00" + order.ToString();
                        strOrder = strOrder.Substring(strOrder.Length - 3, 3);
                        folderName = string.Format("{0}-{1}", strOrder, folderName);
                    }
                    return folderName;
                }
                else
                {
                    //string subName = folderName.Substring(0, 3);
                    //for (int i = 0; i < subName.Length; i++)
                    //{
                    //    if (subName[i] < '0' || subName[i] > '9')
                    //        return folderName;
                    //}
                    //return folderName.Substring(3);
                    return folderName;
                }
            }
            catch (Exception)
            {
                return folderName;
            }
        }

        /// <summary>
        /// 学生打开作业
        /// </summary>
        public string GetTestPaperFileForStudent(string Student_HomeWork_Id, string userId, string tabid)
        {
            string result = string.Empty;
            try
            {
                string uploadPath = "..\\Upload\\Resource\\studentPaper\\";//存储文件基础路径
                string rtrfId = string.Empty;

                Student_HomeWork_Id = Student_HomeWork_Id.Filter();
                #region 获取JSON结构
                //学生答题状态为未答题，打开试卷时更新‘打开时间’
                BLL_Student_HomeWork bllSHW = new BLL_Student_HomeWork();
                Model_Student_HomeWork modelSHW = bllSHW.GetModel(Student_HomeWork_Id);
                BLL_Student_HomeWork_Submit bllshwSubmit = new BLL_Student_HomeWork_Submit();
                Model_Student_HomeWork_Submit mdoelshwSubmit = bllshwSubmit.GetModel(Student_HomeWork_Id);
                mdoelshwSubmit.OpenTime = DateTime.Now;
                mdoelshwSubmit.StudentIP = pfunction.GetRealIP();
                if (mdoelshwSubmit.Student_HomeWork_Status == 0) bllshwSubmit.Update(mdoelshwSubmit);
                string HomeWork_Id = modelSHW.HomeWork_Id;
                Model_HomeWork modelHW = new BLL_HomeWork().GetModel(modelSHW.HomeWork_Id);
                rtrfId = modelHW.ResourceToResourceFolder_Id;
                string strHomeWork_Name = modelHW.HomeWork_Name;

                string filePath = pfunction.ConvertToLongDateTime(modelHW.CreateTime.ToString(), "yyyy-MM-dd") + "\\" + HomeWork_Id + ".txt";
                BLL_HW_TestPaper bllHWTP = new BLL_HW_TestPaper();
                Model_HW_TestPaper modelHWTP = new Model_HW_TestPaper();
                modelHWTP = bllHWTP.GetModel(HomeWork_Id);
                if (modelHWTP == null)
                {
                    modelHWTP = new Model_HW_TestPaper();
                    modelHWTP.HW_TestPaper_Id = HomeWork_Id;
                    modelHWTP.TestPaper_Path = uploadPath + filePath;
                    modelHWTP.TestPaper_Status = "0";
                    modelHWTP.CreateTime = DateTime.Now;
                    bllHWTP.Add(modelHWTP);

                    GenerateTestPaperFileForStudent_API(HomeWork_Id);

                    modelHWTP.TestPaper_Status = "1";
                    bllHWTP.Update(modelHWTP);

                    result = File.ReadAllText(Server.MapPath(uploadPath) + filePath);
                }
                else
                {
                    if (modelHWTP.TestPaper_Status == "0")
                    {
                        return "generating";
                    }
                    if (!File.Exists(Server.MapPath(uploadPath) + filePath))
                    {
                        GenerateTestPaperFileForStudent_API(HomeWork_Id);
                        result = File.ReadAllText(Server.MapPath(uploadPath) + filePath);
                    }
                    else
                    {
                        result = File.ReadAllText(Server.MapPath(uploadPath) + filePath);
                    }
                }
                #endregion
            }
            catch (Exception)
            {
                result = "error";
            }
            return result;
        }
        /// <summary>
        /// 生成学生作业txt 17-07-17TS
        /// </summary>
        private void GenerateTestPaperFileForStudent_API(string HomeWork_Id)
        {
            try
            {
                string uploadPath = "..\\Upload\\Resource\\studentPaper\\";//存储文件基础路径
                string strTestWebSiteUrl = pfunction.getHostPath();
                Model_HomeWork modelHW = new BLL_HomeWork().GetModel(HomeWork_Id);
                string rtrfId = modelHW.ResourceToResourceFolder_Id;
                string strHomeWork_Name = modelHW.HomeWork_Name;
                int isTimeLimt = 0;
                int isTimeLength = 0;
                bool isShowAnswer = false;
                int.TryParse(modelHW.isTimeLimt.ToString(), out isTimeLimt);
                int.TryParse(modelHW.isTimeLength.ToString(), out isTimeLength);
                if (modelHW.IsShowAnswer == 1)
                {
                    isShowAnswer = true;
                }
                string filePath = pfunction.ConvertToLongDateTime(modelHW.CreateTime.ToString(), "yyyy-MM-dd") + "\\" + HomeWork_Id + ".txt";
                string filePathForTch = pfunction.ConvertToLongDateTime(modelHW.CreateTime.ToString(), "yyyy-MM-dd") + "\\" + HomeWork_Id + ".tch.txt";
                List<object> listTQObjForTch = new List<object>();//老师客户端批改
                List<object> listTQObj = new List<object>();//学生客户端答题

                List<object> listTQObjForTchBig = new List<object>();//老师客户端批改
                List<object> listTQObjBig = new List<object>();//学生客户端答题

                strTestWebSiteUrl += "/Upload/Resource/";

                #region 试题
                //试题数据
                string strSqlTQ = string.Format(@"select  t1.TestQuestions_Id,t1.TestQuestions_Num,t1.TestQuestions_Type,t1.Parent_Id,t1.[type],t1.topicNumber
,T2.Resource_Version,T2.GradeTerm,t2.Subject,t2.ParticularYear,t2.Resource_Class,t2.CreateTime,t2.LessonPlan_Type
FROM TestQuestions t1 inner join ResourceToResourceFolder t2 ON t1.ResourceToResourceFolder_ID=T2.ResourceToResourceFolder_ID 
where  t1.ResourceToResourceFolder_Id='{0}' order by TestQuestions_Num ", rtrfId);
                DataTable dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlTQ).Tables[0];

                //获取这个试卷的所有试题分值
                string strSqlScore = string.Empty;
                strSqlScore = string.Format(@"select  [TestQuestions_Score_ID]
      ,[ResourceToResourceFolder_Id]
      ,[TestQuestions_Id]
      ,[TestCorrect]
      ,[AnalyzeHyperlinkData]
      ,[TrainHyperlinkData]
      ,[TestType]
      ,[ScoreText],[testIndex],[TestType]
,TestQuestions_OrderNum FROM TestQuestions_Score where ResourceToResourceFolder_Id='{0}' order by TestQuestions_OrderNum ", rtrfId);
                DataTable dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlScore).Tables[0];

                #region 普通题型 list
                DataRow[] drList = dtTQ.Select("Parent_Id='' ", "TestQuestions_Num");
                foreach (DataRow item in drList)
                {
                    string savePath = string.Empty;
                    string saveOwnerPath = string.Empty;
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                            item["Resource_Version"].ToString(), item["Subject"].ToString());
                    }
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        saveOwnerPath = string.Format("{0}\\", pfunction.ConvertToLongDateTime(item["CreateTime"].ToString(), "yyyy-MM-dd"));
                    }
                    DataRow drTQ_S = null;
                    DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", item["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                    #region 试题分数
                    List<object> listTQ_SObjForTch = new List<object>();//老师客户端批改
                    List<object> listTQ_SObj = new List<object>();//学生客户端答题
                    int intIndex = 0;
                    for (int j = 0; j < drTQ_Score.Length; j++)
                    {

                        drTQ_S = drTQ_Score[j];
                        string strTestIndex = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷
                            && drTQ_S["TestType"].ToString() == "clozeTest")
                        {
                            intIndex++;
                            strTestIndex = intIndex + ".";
                        }
                        string strAnalyzeUrl = string.Empty;
                        string strTrainUrl = string.Empty;
                        if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                        {
                            strAnalyzeUrl = pfunction.getHostPath() + drTQ_S["AnalyzeHyperlinkData"].ToString();
                        }
                        if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                        {
                            strTrainUrl = pfunction.getHostPath() + drTQ_S["TrainHyperlinkData"].ToString();
                        }
                        listTQ_SObj.Add(new
                        {
                            testIndex = strTestIndex,
                            analyzeUrl = strAnalyzeUrl,
                            trainUrl = strTrainUrl
                        });

                        string strtestCorrectBase64 = string.Empty;
                        string strtestCorrect = string.Empty;
                        switch (drTQ_Score[j]["TestType"].ToString())
                        {
                            case "selection":
                            case "clozeTest":
                            case "truefalse":
                                strtestCorrect = drTQ_Score[j]["TestCorrect"].ToString();
                                break;
                            //case "fill":
                            //case "answers":
                            //    strtestCorrectBase64 = RemotWeb.PostDataToServer(strTestWebSiteUrl + saveOwnerPath + "testQuestionCurrent\\" + savePath + drTQ_Score[j]["TestQuestions_Score_ID"].ToString() + ".txt", "", Encoding.UTF8, "Get");
                            //    break;
                        }
                        listTQ_SObjForTch.Add(new
                        {
                            testIndex = strTestIndex,
                            scoreText = drTQ_Score[j]["ScoreText"].ToString(),
                            //testCorrectBase64 = strtestCorrectBase64,
                            testCorrect = strtestCorrect
                        });
                    }
                    //if (listTQ_SObjForTch.Count == 0)
                    //{
                    //    listTQ_SObjForTch.Add(null);
                    //}
                    #endregion
                    string fileUrl = strTestWebSiteUrl + saveOwnerPath + "{0}\\" + savePath + "{1}.txt";//自有资源 savePath为空，云资源saveOwnerPath为空
                    if (drTQ_S != null)
                    {
                        string strtestQuestionBody = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                        string strtextTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                        string strTopicNumber = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                        {
                            strTopicNumber = item["topicNumber"].ToString();
                        }
                        listTQObj.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = listTQ_SObj
                        });

                        if (drTQ_S["TestType"].ToString() == "selection" || drTQ_S["TestType"].ToString() == "clozeTest" || drTQ_S["TestType"].ToString() == "truefalse")
                        {
                            strtestQuestionBody = "";
                            strtextTitle = "";
                        }
                        listTQObjForTch.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = listTQ_SObjForTch
                        });
                    }
                    else
                    {
                        string strtestQuestionBody = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                        string strtextTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                        string strTopicNumber = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                        {
                            strTopicNumber = item["topicNumber"].ToString();
                        }
                        listTQObj.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = ""
                        });

                        listTQObjForTch.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = listTQ_SObjForTch
                        });
                    }
                }
                #endregion
                #region 综合题型 listBig
                DataRow[] drListBig = dtTQ.Select("Parent_Id='0' ", "TestQuestions_Num");
                foreach (DataRow item in drListBig)
                {
                    List<object> listTQObjForTchBig_Sub = new List<object>();
                    List<object> listTQObjBig_Sub = new List<object>();
                    DataRow[] drBig_Sub = dtTQ.Select("Parent_Id='" + item["TestQuestions_Id"] + "'", "TestQuestions_Num");
                    foreach (DataRow itemSub in drBig_Sub)
                    {
                        string savePath = string.Empty;
                        string saveOwnerPath = string.Empty;
                        if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", itemSub["ParticularYear"].ToString(), itemSub["GradeTerm"],
                                itemSub["Resource_Version"].ToString(), itemSub["Subject"].ToString());
                        }
                        if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                        {
                            saveOwnerPath = string.Format("{0}\\", pfunction.ConvertToLongDateTime(itemSub["CreateTime"].ToString(), "yyyy-MM-dd"));
                        }
                        DataRow drTQ_S = null;
                        DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", itemSub["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                        #region 试题分数
                        List<object> listTQ_SObjForTch = new List<object>();//老师客户端批改 分值，正确答案
                        List<object> listTQ_SObj = new List<object>();//学生客户端答题 解析，强化训练
                        int intIndex = 0;
                        for (int j = 0; j < drTQ_Score.Length; j++)
                        {

                            drTQ_S = drTQ_Score[j];
                            string strTestIndex = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷
                                && drTQ_S["TestType"].ToString() == "clozeTest")
                            {
                                intIndex++;
                                strTestIndex = intIndex + ".";
                            }
                            string strAnalyzeUrl = string.Empty;
                            string strTrainUrl = string.Empty;
                            if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                            {
                                strAnalyzeUrl = pfunction.getHostPath() + drTQ_S["AnalyzeHyperlinkData"].ToString();
                            }
                            if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                            {
                                strTrainUrl = pfunction.getHostPath() + drTQ_S["TrainHyperlinkData"].ToString();
                            }
                            listTQ_SObj.Add(new
                            {
                                testIndex = strTestIndex,
                                analyzeUrl = strAnalyzeUrl,
                                trainUrl = strTrainUrl
                            });

                            string strtestCorrectBase64 = string.Empty;
                            string strtestCorrect = string.Empty;
                            switch (drTQ_Score[j]["TestType"].ToString())
                            {
                                case "selection":
                                case "clozeTest":
                                case "truefalse":
                                    strtestCorrect = drTQ_Score[j]["TestCorrect"].ToString();
                                    break;
                                //case "fill":
                                //case "answers":
                                //    strtestCorrectBase64 = RemotWeb.PostDataToServer(strTestWebSiteUrl + saveOwnerPath + "testQuestionCurrent\\" + savePath + drTQ_Score[j]["TestQuestions_Score_ID"].ToString() + ".txt", "", Encoding.UTF8, "Get");
                                //    break;
                            }
                            listTQ_SObjForTch.Add(new
                            {
                                testIndex = strTestIndex,
                                scoreText = drTQ_Score[j]["ScoreText"].ToString(),
                                //testCorrectBase64 = strtestCorrectBase64,
                                testCorrect = strtestCorrect
                            });
                        }
                        if (listTQ_SObjForTch.Count == 0)
                        {
                            listTQ_SObjForTch.Add(null);
                        }
                        #endregion
                        string fileUrl = strTestWebSiteUrl + saveOwnerPath + "{0}\\" + savePath + "{1}.txt";//自有资源 savePath为空，云资源saveOwnerPath为空
                        if (drTQ_S != null)
                        {
                            string strtestQuestionBody = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                            string strtextTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                            string strTopicNumber = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                            {
                                strTopicNumber = itemSub["topicNumber"].ToString();
                            }
                            listTQObjBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = listTQ_SObj
                            });

                            if (drTQ_S["TestType"].ToString() == "selection" || drTQ_S["TestType"].ToString() == "clozeTest" || drTQ_S["TestType"].ToString() == "truefalse")
                            {
                                strtestQuestionBody = "";
                                strtextTitle = "";
                            }
                            listTQObjForTchBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = listTQ_SObjForTch
                            });
                        }
                        else
                        {
                            string strtestQuestionBody = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                            string strtextTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                            string strTopicNumber = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                            {
                                strTopicNumber = itemSub["topicNumber"].ToString();
                            }
                            listTQObjBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = ""
                            });

                            listTQObjForTchBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = listTQ_SObjForTch
                            });
                        }
                    }
                    string savePathBig = string.Empty;
                    string saveOwnerPathBig = string.Empty;
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        savePathBig = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                            item["Resource_Version"].ToString(), item["Subject"].ToString());
                    }
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        saveOwnerPathBig = string.Format("{0}\\", pfunction.ConvertToLongDateTime(item["CreateTime"].ToString(), "yyyy-MM-dd"));
                    }
                    string fileUrlBig = strTestWebSiteUrl + saveOwnerPathBig + "{0}\\" + savePathBig + "{1}.{2}";//自有资源 savePath为空，云资源saveOwnerPath为空
                    string strdocBase64 = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "txt"), "", Encoding.UTF8, "Get");
                    string strdocHtml = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "htm"), "", Encoding.UTF8, "Get");
                    string textTitle = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "textTitle", item["TestQuestions_Id"].ToString(), "txt"), "", Encoding.UTF8, "Get");
                    listTQObjBig.Add(new
                    {
                        docBase64 = strdocBase64,
                        docHtml = strdocHtml,
                        textTitle = textTitle,
                        list = listTQObjBig_Sub,
                        type = item["type"].ToString()
                    });
                    if (drBig_Sub.Length > 0) //没有子级试题，不加载节点
                    {
                        listTQObjForTchBig.Add(new
                        {
                            docBase64 = strdocBase64,
                            docHtml = strdocHtml,
                            textTitle = textTitle,
                            list = listTQObjForTchBig_Sub,
                            type = item["type"].ToString()
                        });
                    }

                }
                #endregion

                #endregion
                string strJson = string.Empty;
                strJson = JsonConvert.SerializeObject(new
                {
                    status = true,
                    errorMsg = "",
                    errorCode = "",
                    paperHeaderDoc = GetPaperHeaderDoc(rtrfId),
                    testPaperName = "",
                    isTimeLimt = isTimeLimt,
                    isTimeLength = isTimeLength,
                    sysTime = DateTime.Now.ToString(),
                    isShowAnswerAfterSubmiting = isShowAnswer,
                    list = listTQObj,
                    listBig = listTQObjBig
                });
                pfunction.WriteToFile(HttpContext.Current.Server.MapPath(uploadPath) + filePath, strJson, true);

                string strJsonForTch = string.Empty;
                strJsonForTch = JsonConvert.SerializeObject(new
                {
                    list = listTQObjForTch,
                    listBig = listTQObjForTchBig
                });
                pfunction.WriteToFile(HttpContext.Current.Server.MapPath(uploadPath) + filePathForTch, strJsonForTch, true);

            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 老师打开作业
        /// </summary>
        public string GetTestPaperFileForTeacher(string ResourceToResourceFolder_ID, string userId)
        {
            string result = string.Empty;
            try
            {
                string uploadPath = "..\\Upload\\Resource\\teacherPaper\\";//存储文件基础路径
                string rtrfId = string.Empty;
                rtrfId = ResourceToResourceFolder_ID;
                #region 获取JSON结构
                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                string filePath = pfunction.ConvertToLongDateTime(modelRTRF.CreateTime.ToString(), "yyyy-MM-dd") + "\\" + rtrfId + ".txt";
                BLL_HW_TestPaper bllHWTP = new BLL_HW_TestPaper();
                Model_HW_TestPaper modelHWTP = new Model_HW_TestPaper();
                modelHWTP = bllHWTP.GetModel(rtrfId);
                if (modelHWTP == null)
                {
                    modelHWTP = new Model_HW_TestPaper();
                    modelHWTP.HW_TestPaper_Id = rtrfId;
                    modelHWTP.TestPaper_Path = uploadPath + filePath;
                    modelHWTP.TestPaper_Status = "0";
                    modelHWTP.CreateTime = DateTime.Now;
                    bllHWTP.Add(modelHWTP);

                    GenerateTestPaperFileForTeacher_API(rtrfId);

                    modelHWTP.TestPaper_Status = "1";
                    bllHWTP.Update(modelHWTP);

                    result = File.ReadAllText(Server.MapPath(uploadPath) + filePath);
                }
                else
                {
                    result = GetTestPaperFileForTeacherSub(modelHWTP, uploadPath, filePath, rtrfId);

                }
                #endregion
            }
            catch (Exception)
            {
                result = "error";
            }
            return result;
        }

        private string GetTestPaperFileForTeacherSub(Model_HW_TestPaper modelHWTP, string uploadPath, string filePath, string rtrfId)
        {
            string result = string.Empty;
            if (HttpContext.Current.Session["reqGetTestPaperFileForTeacherSubCount"] == null)
            {
                HttpContext.Current.Session["reqGetTestPaperFileForTeacherSubCount"] = 1;
            }
            else
            {
                HttpContext.Current.Session["reqGetTestPaperFileForTeacherSubCount"] = (int)HttpContext.Current.Session["reqGetTestPaperFileForTeacherSubCount"] + 1;
            }

            if (modelHWTP.TestPaper_Status == "0")
            {
                //return "generating";
                if ((int)HttpContext.Current.Session["reqGetTestPaperFileForTeacherSubCount"] < 20) Thread.Sleep(1000);
                result = GetTestPaperFileForTeacherSub(modelHWTP, uploadPath, filePath, rtrfId);
            }
            if (!File.Exists(Server.MapPath(uploadPath) + filePath))
            {
                GenerateTestPaperFileForTeacher_API(rtrfId);
                result = File.ReadAllText(Server.MapPath(uploadPath) + filePath);
            }
            else
            {
                result = File.ReadAllText(Server.MapPath(uploadPath) + filePath);
            }
            return result;
        }
        /// <summary>
        /// 生成老师作业txt 17-07-17TS
        /// </summary>
        private void GenerateTestPaperFileForTeacher_API(string ResourceToResourceFolder_ID)
        {
            try
            {
                string uploadPath = "..\\Upload\\Resource\\teacherPaper\\";//存储文件基础路径
                string strTestWebSiteUrl = pfunction.getHostPath();
                string rtrfId = ResourceToResourceFolder_ID;
                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                strTestWebSiteUrl += "/Upload/Resource/";
                string filePath = pfunction.ConvertToLongDateTime(modelRTRF.CreateTime.ToString(), "yyyy-MM-dd") + "\\" + rtrfId + ".txt";

                List<object> listTQObj = new List<object>();
                List<object> listTQObjBig = new List<object>();



                #region 试题
                //试题数据
                string strSqlTQ = string.Format(@"select  t1.TestQuestions_Id,t1.TestQuestions_Num,t1.TestQuestions_Type,t1.Parent_Id,t1.[type],t1.topicNumber
,T2.Resource_Version,T2.GradeTerm,t2.Subject,t2.ParticularYear,t2.Resource_Class,t2.CreateTime,t2.LessonPlan_Type
FROM TestQuestions t1 inner join ResourceToResourceFolder t2 ON t1.ResourceToResourceFolder_ID=T2.ResourceToResourceFolder_ID 
where  t1.ResourceToResourceFolder_Id='{0}' order by TestQuestions_Num ", rtrfId);
                DataTable dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlTQ).Tables[0];

                //获取这个试卷的所有试题分值
                string strSqlScore = string.Empty;
                strSqlScore = string.Format(@"select  [TestQuestions_Score_ID]
      ,[ResourceToResourceFolder_Id]
      ,[TestQuestions_Id]
      ,[TestCorrect]
      ,[AnalyzeHyperlinkData]
      ,[TrainHyperlinkData],[testIndex],[TestType]
,TestQuestions_OrderNum FROM TestQuestions_Score where ResourceToResourceFolder_Id='{0}' order by TestQuestions_OrderNum ", rtrfId);
                DataTable dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlScore).Tables[0];

                #region 普通题型 list
                DataRow[] drList = dtTQ.Select("Parent_Id='' ", "TestQuestions_Num");
                foreach (DataRow item in drList)
                {
                    string savePath = string.Empty;
                    string saveOwnerPath = string.Empty;
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                            item["Resource_Version"].ToString(), item["Subject"].ToString());
                    }
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        saveOwnerPath = string.Format("{0}\\"
                            , pfunction.ConvertToLongDateTime(modelRTRF.CreateTime.ToString(), "yyyy-MM-dd"));
                    }
                    DataRow drTQ_S = null;
                    DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", item["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                    #region 试题分数
                    List<object> listTQ_SObj = new List<object>();
                    int intIndex = 0;
                    for (int j = 0; j < drTQ_Score.Length; j++)
                    {
                        drTQ_S = drTQ_Score[j];
                        string strTestIndex = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷
                            && drTQ_S["TestType"].ToString() == "clozeTest")
                        {
                            intIndex++;
                            strTestIndex = intIndex + ".";
                        }
                        string strAnalyzeUrl = string.Empty;
                        string strTrainUrl = string.Empty;
                        if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                        {
                            strAnalyzeUrl = pfunction.getHostPath() + drTQ_S["AnalyzeHyperlinkData"].ToString();
                        }
                        if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                        {
                            strTrainUrl = pfunction.getHostPath() + drTQ_S["TrainHyperlinkData"].ToString();
                        }
                        listTQ_SObj.Add(new
                        {
                            testIndex = strTestIndex,
                            analyzeUrl = strAnalyzeUrl,
                            trainUrl = strTrainUrl
                        });
                    }
                    #endregion
                    string fileUrl = strTestWebSiteUrl + saveOwnerPath + "{0}\\" + savePath + "{1}.txt";//自有资源 savePath为空，云资源saveOwnerPath为空
                    if (drTQ_S != null)
                    {
                        string strTopicNumber = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                        {
                            strTopicNumber = item["topicNumber"].ToString();
                        }
                        listTQObj.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                            textTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                            list = listTQ_SObj
                        });
                    }
                    else
                    {
                        string strTopicNumber = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                        {
                            strTopicNumber = item["topicNumber"].ToString();
                        }
                        listTQObj.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                            textTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                            list = ""
                        });
                    }
                }
                #endregion
                #region 综合题型 listBig
                DataRow[] drListBig = dtTQ.Select("Parent_Id='0' ", "TestQuestions_Num");
                foreach (DataRow item in drListBig)
                {
                    List<object> listTQObjBig_Sub = new List<object>();
                    DataRow[] drBig_Sub = dtTQ.Select("Parent_Id='" + item["TestQuestions_Id"] + "'", "TestQuestions_Num");
                    foreach (DataRow itemSub in drBig_Sub)
                    {
                        string savePath = string.Empty;
                        string saveOwnerPath = string.Empty;
                        if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", itemSub["ParticularYear"].ToString(), itemSub["GradeTerm"],
                                itemSub["Resource_Version"].ToString(), itemSub["Subject"].ToString());
                        }
                        if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                        {
                            saveOwnerPath = string.Format("{0}\\"
                                , pfunction.ConvertToLongDateTime(modelRTRF.CreateTime.ToString(), "yyyy-MM-dd"));
                        }
                        DataRow drTQ_S = null;
                        DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", itemSub["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                        #region 试题分数
                        List<object> listTQ_SObj = new List<object>();
                        int intIndex = 0;
                        for (int j = 0; j < drTQ_Score.Length; j++)
                        {
                            drTQ_S = drTQ_Score[j];
                            string strTestIndex = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷
                                && drTQ_S["TestType"].ToString() == "clozeTest")
                            {
                                intIndex++;
                                strTestIndex = intIndex + ".";
                            }
                            string strAnalyzeUrl = string.Empty;
                            string strTrainUrl = string.Empty;
                            if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                            {
                                strAnalyzeUrl = pfunction.getHostPath() + drTQ_S["AnalyzeHyperlinkData"].ToString();
                            }
                            if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                            {
                                strTrainUrl = pfunction.getHostPath() + drTQ_S["TrainHyperlinkData"].ToString();
                            }
                            listTQ_SObj.Add(new
                            {
                                testIndex = strTestIndex,
                                analyzeUrl = strAnalyzeUrl,
                                trainUrl = strTrainUrl
                            });
                        }
                        #endregion
                        string fileUrl = strTestWebSiteUrl + saveOwnerPath + "{0}\\" + savePath + "{1}.txt";//自有资源 savePath为空，云资源saveOwnerPath为空
                        if (drTQ_S != null)
                        {
                            string strTopicNumber = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                            {
                                strTopicNumber = itemSub["topicNumber"].ToString();
                            }
                            listTQObjBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                textTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                list = listTQ_SObj
                            });
                        }
                        else
                        {
                            string strTopicNumber = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                            {
                                strTopicNumber = itemSub["topicNumber"].ToString();
                            }
                            listTQObjBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                textTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                list = ""
                            });
                        }
                    }
                    string savePathBig = string.Empty;
                    string saveOwnerPathBig = string.Empty;
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        savePathBig = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                            item["Resource_Version"].ToString(), item["Subject"].ToString());
                    }
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        saveOwnerPathBig = string.Format("{0}\\", pfunction.ConvertToLongDateTime(item["CreateTime"].ToString(), "yyyy-MM-dd"));
                    }
                    string fileUrlBig = strTestWebSiteUrl + saveOwnerPathBig + "{0}\\" + savePathBig + "{1}.{2}";//自有资源 savePath为空，云资源saveOwnerPath为空
                    string strdocBase64 = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "txt"), "", Encoding.UTF8, "Get");
                    string strdocHtml = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "htm"), "", Encoding.UTF8, "Get");
                    string textTitle = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "textTitle", item["TestQuestions_Id"].ToString(), "txt"), "", Encoding.UTF8, "Get");
                    listTQObjBig.Add(new
                    {
                        docBase64 = strdocBase64,
                        docHtml = strdocHtml,
                        textTitle = textTitle,
                        list = listTQObjBig_Sub,
                        type = item["type"].ToString()
                    });
                }
                #endregion

                #endregion
                string strJson = string.Empty;
                strJson = JsonConvert.SerializeObject(new
                {
                    status = true,
                    errorMsg = "",
                    errorCode = "",
                    paperHeaderDoc = GetPaperHeaderDoc(rtrfId),
                    testPaperName = "",
                    isTimeLimt = 0,
                    isTimeLength = 0,
                    sysTime = DateTime.Now.ToString(),
                    isShowAnswerAfterSubmiting = false,
                    list = listTQObj,
                    listBig = listTQObjBig
                });
                pfunction.WriteToFile(HttpContext.Current.Server.MapPath(uploadPath) + filePath, strJson, true);

            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 获取资源对应文件路径 17-06-07TS
        /// </summary>
        /// <param name="modelRTRF"></param>
        private List<string> GetResourceFile(Model_ResourceToResourceFolder modelRTRF, string uploadPath)
        {
            try
            {
                List<string> listReturn = new List<string>();
                //生成存储路径
                string savePath = string.Empty;
                string savePathOwn = string.Empty;
                if (modelRTRF.Resource_Class == Resource_ClassConst.云资源)
                {
                    savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm,
                       modelRTRF.Resource_Version, modelRTRF.Subject);
                }
                if (modelRTRF.Resource_Class == Resource_ClassConst.自有资源)
                {
                    DateTime dateTime = Convert.ToDateTime(modelRTRF.CreateTime);
                    savePathOwn = string.Format("{0}\\", dateTime.ToString("yyyy-MM-dd"));
                }

                if (modelRTRF.Resource_Type == Rc.Common.Config.Resource_TypeConst.testPaper类型文件)
                {
                    string fileUrl = uploadPath + savePathOwn + "{0}\\" + savePath + "{1}.{2}";
                    #region 习题集文件
                    DataTable dtTQ = new BLL_TestQuestions().GetList("ResourceToResourceFolder_Id='" + modelRTRF.ResourceToResourceFolder_Id + "'").Tables[0];
                    DataTable dtTQ_Score = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + modelRTRF.ResourceToResourceFolder_Id + "'").Tables[0];
                    foreach (DataRow item in dtTQ.Rows)
                    {
                        listReturn.Add(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"], "htm"));
                        listReturn.Add(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"], "htm"));
                    }
                    foreach (DataRow item in dtTQ_Score.Rows)
                    {
                        listReturn.Add(string.Format(fileUrl, "testQuestionCurrent", item["TestQuestions_Score_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "testQuestionOption", item["TestQuestions_Score_Id"], "txt"));

                        listReturn.Add(string.Format(fileUrl, "AnalyzeData", item["TestQuestions_Score_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "AnalyzeHtml", item["TestQuestions_Score_Id"], "htm"));

                        listReturn.Add(string.Format(fileUrl, "TrainData", item["TestQuestions_Score_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "TrainHtml", item["TestQuestions_Score_Id"], "htm"));

                        listReturn.Add(string.Format(fileUrl, "bodySub", item["TestQuestions_Score_Id"], "txt"));
                    }
                    #endregion
                }
                else
                {
                    #region 教案文件
                    string filePath = string.Empty;//文件存储路径 
                    string filePath2 = string.Empty;//文件存储路径2
                    //判断产品类型
                    switch (modelRTRF.Resource_Type)
                    {
                        case Resource_TypeConst.ScienceWord类型文件:
                            filePath += "swDocument\\";
                            break;
                        case Resource_TypeConst.class类型微课件:
                            filePath += "microClassDocument\\";
                            filePath2 += "classDocument\\";
                            break;
                    }
                    #region 文件及图片
                    filePath += savePath;

                    listReturn.Add(uploadPath + filePath + modelRTRF.ResourceToResourceFolder_Id + "." + modelRTRF.File_Suffix);
                    listReturn.Add(uploadPath + filePath + modelRTRF.ResourceToResourceFolder_Id + ".htm");
                    if (!string.IsNullOrEmpty(filePath2))
                    {
                        listReturn.Add(uploadPath + filePath2 + modelRTRF.ResourceToResourceFolder_Id + "." + modelRTRF.File_Suffix);
                        listReturn.Add(uploadPath + filePath2 + modelRTRF.ResourceToResourceFolder_Id + ".htm");
                    }

                    DataTable dtImg = new BLL_ResourceToResourceFolder_img().GetList("ResourceToResourceFolder_Id='" + modelRTRF.ResourceToResourceFolder_Id + "'").Tables[0];
                    foreach (DataRow item in dtImg.Rows)
                    {
                        listReturn.Add(uploadPath + item["ResourceToResourceFolderImg_Url"].ToString());
                    }

                    #endregion

                    #endregion
                }

                return listReturn;
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", Request.Url.ToString(), string.Format("获取资源对应所有文件路径失败。{0}", ex.Message.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 根据文件路径，删除文件（资源对应文件） 17-06-07TS
        /// </summary>
        /// <param name="list"></param>
        private void DeleteResourceFile(List<string> list)
        {
            try
            {
                foreach (string fileUrl in list)
                {
                    if (File.Exists(fileUrl))
                    {
                        File.Delete(fileUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", Request.Url.ToString(), string.Format("删除文件失败。{0}", ex.Message.ToString()));
            }
        }
    }
}