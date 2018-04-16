using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.teacher
{
    public partial class historyTestPaper_API : Rc.Cloud.Web.Common.FInitData
    {
        public string strUserGroup_IdActivity = string.Empty;
        public string Two_WayChecklist_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Rc.Common.ConfigHelper.GetConfigBool("IsShowTestAssembly"))
            {
                testPaper2.Visible = true;
            }
            if (!IsPostBack)
            {
                string StrSql = @"select * from [dbo].[Two_WayChecklist]  where Two_WayChecklist_Id in(select Two_WayChecklist_Id from Two_WayChecklistAuth where User_Id ='" + FloginUser.UserId + "') order by CreateTime,Two_WayChecklist_Name";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                Two_WayChecklist_Id = GetTwo_WayChecklistOne(dt);
                if (!String.IsNullOrEmpty(Request["Two_WayChecklist_Id"]))
                {
                    Two_WayChecklist_Id = Request["Two_WayChecklist_Id"].ToString().Trim().Filter();
                }
                DataTable dtUserGroupList = new DataTable();
                string strWhere = string.Empty;
                strWhere = string.Format("UserGroup_AttrEnum='{1}' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and User_Id='{0}') order by UserGroupOrder "
                    , FloginUser.UserId
                    , UserGroup_AttrEnum.Class);
                dtUserGroupList = new BLL_UserGroup().GetList(strWhere).Tables[0];
                strUserGroup_IdActivity = GetUserGroupOne(dtUserGroupList);
                if (!String.IsNullOrEmpty(Request["ugid"]))
                {
                    strUserGroup_IdActivity = Request["ugid"].ToString().Trim().Filter();
                }
                this.ltlClass.Text = GetUserGroupHtml(strUserGroup_IdActivity, dtUserGroupList).ToString();
                LoadData(dt);
            }
        }
        private void LoadData(DataTable dt)
        {
            try
            {
                string Str = string.Empty;
                string Temp = "<li><div class=\"name {2}\"><a href='##' id='{0}'>{1}</a></div></li>";

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Str += string.Format(Temp
                            , item["Two_WayChecklist_Id"].ToString()
                            , item["Two_WayChecklist_Name"].ToString()
                            , item["Two_WayChecklist_Id"].ToString() == Two_WayChecklist_Id ? "active" : "");
                    }
                    this.ltlTwo_WayChecklist.Text = Str;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 得到班列表 HTML
        /// </summary>
        /// <param name="strUserGroup_IdActivity"></param>
        /// <returns></returns>
        private StringBuilder GetUserGroupHtml(string strUserGroup_IdActivity, DataTable dtUserGroupList)
        {
            StringBuilder strTempHtml = new StringBuilder();
            strTempHtml.Append("<div class='iframe-subnav'>");
            strTempHtml.Append("<ul class='subnav'>");
            for (int i = 0; i < dtUserGroupList.Rows.Count; i++)
            {
                strTempHtml.Append("<li>");
                strTempHtml.AppendFormat("<a href='##' class='{5}' onclick=\"Show('{0}','{4}')\" val='{2}' title=\"{3}\">{1}({2})"
                    , GetUrl(dtUserGroupList.Rows[i]["UserGroup_Id"].ToString(), "")
                    , Rc.Cloud.Web.Common.pfunction.GetSubstring(dtUserGroupList.Rows[i]["UserGroup_Name"].ToString().Trim(), 10, true)
                    , dtUserGroupList.Rows[i]["UserGroup_Id"].ToString().Trim()
                    , dtUserGroupList.Rows[i]["UserGroup_Name"]
                    , new Random().Next()
                    , (strUserGroup_IdActivity.ToString() == dtUserGroupList.Rows[i]["UserGroup_Id"].ToString()) ? "active" : ""
                    );
                strTempHtml.Append("</a>");
                strTempHtml.Append("</li>");
            }
            strTempHtml.Append("</ul>");
            strTempHtml.Append("</div>");

            return strTempHtml;
        }
        private string GetUserGroupOne(DataTable dtUserGroupList)
        {
            string strTemp = string.Empty;
            if (dtUserGroupList.Rows.Count > 0)
            {
                strTemp = dtUserGroupList.Rows[0]["UserGroup_Id"].ToString();
            }
            return strTemp;
        }
        private string GetTwo_WayChecklistOne(DataTable dt)
        {
            string strTemp = string.Empty;
            if (dt.Rows.Count > 0)
            {
                strTemp = dt.Rows[0]["Two_WayChecklist_Id"].ToString();
            }
            return strTemp;
        }
        private string GetUrl(string strUserGroup_IdActivity, string strTestForder_IdActivity)
        {
            string strTemp = string.Empty;
            strTemp += string.Format("ugid={0}"
                , strUserGroup_IdActivity.Trim());
            return strTemp;
        }

        [WebMethod]
        public static string GetTestpaper(string Two_WayChecklist_Id, string UserGroupId, int PageIndex, int PageSize)
        {
            Rc.Model.Resources.Model_F_User FloginUser = Rc.Common.StrUtility.clsUtility.IsFPageFlag() as Rc.Model.Resources.Model_F_User;
            try
            {
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                strWhere += string.Format(" Two_WayChecklist_Id='{0}' and User_Id='{1}'", Two_WayChecklist_Id.Filter(), FloginUser.UserId);
                strSqlCount = @"select count(*) from AlreadyedTestpaper where " + strWhere;
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.ResourceToResourceFolder_Order,A.Resource_Name ) row,A.*,hw.HomeWork_Id
from ResourceToResourceFolder A
INNER JOIN AlreadyedTestpaper B ON A.ResourceToResourceFolder_Id=B.ResourceToResourceFolder_Id
left join HomeWork hw on hw.UserGroup_Id='" + UserGroupId + "' and hw.ResourceToResourceFolder_Id=A.ResourceToResourceFolder_Id where "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize);
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string docName = dtRes.Rows[i]["Resource_Name"].ToString();
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        docName = docName,
                        EnHomeWork_Name = HttpContext.Current.Server.UrlEncode(dtRes.Rows[i]["Resource_Name"].ToString()),
                        docTime = Rc.Cloud.Web.Common.pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
                        HomeWork_Id = dtRes.Rows[i]["HomeWork_Id"].ToString()
                    });
                }
                #endregion

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

    }
}