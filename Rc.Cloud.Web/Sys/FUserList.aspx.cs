using Newtonsoft.Json;
using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Model;

namespace Rc.Cloud.Web.Sys
{
    public partial class FUserList : Rc.Cloud.Web.Common.InitPage
    {
        protected string strPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strPath = Request.Url.ToString();
            Module_Id = "90200300";
            UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, (loginUser == null ? "''" : clsUtility.ReDoStr(loginUser.SysRole_IDs, ',')), Module_Id);
            if (UserFun.Add) btnAdd.Visible = true;
            if (UserFun.Select) btnSearch.Visible = true;
            if (!IsPostBack)
            {

            }
        }

        /// <summary>
        /// 注册用户列表
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetFUserList(string UserName, int PageIndex, int PageSize)
        {
            try
            {
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                Model_Struct_Func UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, (loginUser == null ? "''" : clsUtility.ReDoStr(loginUser.SysRole_IDs, ',')), "90200300");

                UserName = UserName.Filter();
                string strWhere = string.Empty;
                strWhere = "1=1";
                if (!string.IsNullOrEmpty(UserName))
                {
                    strWhere += " AND (UserName like '%" + UserName + "%' or TrueName like '%" + UserName + "%') ";
                }
                int intRecordCount = 0;
                List<object> listReturn = new List<object>();
                BLL_F_User bll_f_user = new BLL_F_User();
                DataTable dt = bll_f_user.GetListPage(strWhere, "CreateTime DESC", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                intRecordCount = bll_f_user.GetRecordCount(strWhere);
                foreach (DataRow item in dt.Rows)
                {
                    string strSex = string.Empty;
                    switch (item["Sex"].ToString())
                    {
                        case "M":
                            strSex = "男";
                            break;
                        case "F":
                            strSex = "女";
                            break;
                        case "S":
                            strSex = "保密";
                            break;
                        default:
                            strSex = "";
                            break;
                    }
                    string strOperate = string.Empty;
                    if (UserFun.Edit)
                    {
                        strOperate += string.Format("<a href=\"javascript:changepwd('{0}');\">修改密码</a>", item["UserId"]);
                        strOperate += string.Format("<a href=\"javascript:Update('{0}');\">修改账号</a>", item["UserId"]);
                    }
                    if (UserFun.Select) strOperate += string.Format("<a href=\"javascript:Show('{0}');\">查看</a>", item["UserId"]);
                    if (UserFun.Delete) strOperate += string.Format("<a href=\"javascript:del('{0}');\">删除</a>", item["UserId"]);

                    listReturn.Add(new
                    {
                        UserName = item["UserName"].ToString(),
                        TrueName = item["TrueName"].ToString(),
                        UserPostName = item["UserPostName"].ToString(),
                        Sex = strSex,
                        Email = item["Email"].ToString(),
                        Mobile = item["Mobile"].ToString(),
                        SubjectName = item["SubjectName"].ToString(),
                        UserId = item["UserId"].ToString(),
                        Operate = strOperate
                    });
                }
                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = intRecordCount,
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
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"
                });
            }
        }

    }
}