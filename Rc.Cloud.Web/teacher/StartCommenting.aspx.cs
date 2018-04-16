using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Web.Services;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.teacher
{
    public partial class StartCommenting : Rc.Cloud.Web.Common.FInitData
    {
        protected string userId = string.Empty;
        protected string rtrfId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = FloginUser.UserId;
            rtrfId = Request["rtrfId"].Filter();
            if (!IsPostBack)
            {
                DataTable dtUserGroupList = new DataTable();
                string strWhere = string.Empty;
                strWhere = string.Format("UserGroup_AttrEnum='{1}' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and User_Id='{0}') order by UserGroupOrder "
                    , FloginUser.UserId
                    , UserGroup_AttrEnum.Class);
                dtUserGroupList = new BLL_UserGroup().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlClass, dtUserGroupList, "UserGroup_Name", "UserGroup_Id", false);

            }
        }

        [WebMethod]
        public static string GetHWList(string userId, string classId, string rtrfId)
        {
            try
            {
                string strSql = string.Format(@"
select hw.HomeWork_Id,hw.HomeWork_Name,hw.ResourceToResourceFolder_Id
,topicNumbers=stuff((select '，'+(case when right(rtrim(topicNumber),1)='.' then substring(rtrim(topicNumber),1,len(rtrim(topicNumber))-1) else rtrim(topicNumber) end) from HomeworkToTQ where HomeWork_Id=hw.HomeWork_Id order by Sort for XML path('')),1,1,'')
from HomeWork hw where IsCountdown is not null and HomeWork_AssignTeacher='{0}' and rtrfId_Old='{1}' and UserGroup_Id='{2}' order by CreateTime "
                    , userId, rtrfId.Filter(), classId.Filter());
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                List<object> listReturn = new List<object>();
                int inum = 0;
                foreach (DataRow item in dt.Rows)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = inum,
                        HomeWork_Id = item["HomeWork_Id"],
                        ResourceToResourceFolder_Id = item["ResourceToResourceFolder_Id"],
                        topicNumbers = item["topicNumbers"].ToString().Trim()
                    });
                }
                if (inum == 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "暂无数据"
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        list = listReturn
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "err"
                });
            }
        }

    }
}