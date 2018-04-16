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

namespace Homework.student
{
    public partial class classList : Rc.Cloud.Web.Common.FInitData
    {
        public string userId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = FloginUser.UserId;
        }

        [WebMethod]
        public static string GetClassList(string userId)
        {
            try
            {
                Model_F_User user = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                BLL_UserGroup bll = new BLL_UserGroup();
                //string strSql = @"select UG.*,FU.UserName, MemberCount=(select COUNT(1) from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and UserGroup_Id=UG.UserGroup_Id) from UserGroup UG left join F_User FU on UG.User_ID=FU.UserId ";
                //strSql += " left join UserGroup_Member ";
                //strSql += string.Format("where UG.UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_Id='{0}') order by UG.CreateTime desc ", userId);

                string strSql = string.Format(@" select UGM.*,FU.UserName
,MemberCount=(select COUNT(1) from UserGroup_Member UGM2 where UGM2.User_ApplicationStatus='passed' and UGM2.UserGroup_Id=UG.UserGroup_Id) 
,UG.UserGroup_BriefIntroduction,UG.UserGroup_Name 
from UserGroup_Member UGM 
left join UserGroup UG on UGM.UserGroup_Id=UG.UserGroup_Id 
left join F_User FU on UG.User_ID=FU.UserId
where UGM.User_Id='{0}' order by UGM.UserStatus,UGM.User_ApplicationStatus desc,UG.UserGroupOrder ", userId);
                //List<Model_UserGroup> listUserGroup = bll.GetModelList(strWhere);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                List<object> listReturn = new List<object>();
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        User_ID = dt.Rows[i]["User_ID"],
                        User_Name = dt.Rows[i]["UserName"],
                        UserGroup_Id = dt.Rows[i]["UserGroup_Id"],
                        UserGroup_Name = dt.Rows[i]["UserGroup_Name"],
                        UserGroup_NameSubstring = pfunction.GetSubstring(dt.Rows[i]["UserGroup_Name"].ToString(), 9, false),
                        UserGroup_BriefIntroduction = string.Format("{0}"
                        , dt.Rows[i]["UserGroup_BriefIntroduction"].ToString() == "" ? "这家伙很懒，什么都没留下~~" : dt.Rows[i]["UserGroup_BriefIntroduction"].ToString()),
                        User_ApplicationStatus = dt.Rows[i]["User_ApplicationStatus"],
                        UserStatus = dt.Rows[i]["UserStatus"],
                        MemberCount = dt.Rows[i]["MemberCount"]
                    });
                }
                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
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

    }
}