using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Newtonsoft.Json;
namespace Rc.Cloud.Web.teacher
{
    public partial class historyTestPaper : Rc.Cloud.Web.Common.FInitData
    {
        public string strUserGroup_IdActivity = string.Empty;
        public string Two_WayChecklist_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Rc.Common.ConfigHelper.GetConfigBool("IsShowTestAssembly"))
            //{
            //    testPaper.Visible = true;
            //    testPaper2.Visible = true;
            //}
            if (pfunction.GetWebMdlIsShow("ChapterAssembly"))
            {
                testPaper.Visible = true;
                testPaper2.Visible = true;
                cptestPaper.Visible = true;
                cptestPaper2.Visible = true;
            }
            if (pfunction.GetWebMdlIsShow("Two_WayChecklist"))
            {
                testPaper.Visible = true;
                testPaper2.Visible = true;
                twtestPaper.Visible = true;
                twtestPaper2.Visible = true;
            }
            if (pfunction.GetWebMdlIsShow("pHomework")) apHomework.Visible = true;

            if (!IsPostBack)
            {

                //string StrSql = @"select * from [dbo].[Two_WayChecklist]  where Two_WayChecklist_Id in(select Two_WayChecklist_Id from Two_WayChecklistAuth where User_Id ='" + FloginUser.UserId + "') order by CreateTime,Two_WayChecklist_Name";
                string StrSql = @" 	select * from [dbo].[Two_WayChecklist] t1
	left join [dbo].[Two_WayChecklistToTeacher] t2 on t2.Two_WayChecklist_Id=t1.Two_WayChecklist_Id and status='1' and t2.Teacher_Id='" + FloginUser.UserId + @"'
    inner join Two_WayChecklistAuth twoAu on twoAu.Two_WayChecklist_Id=t1.Two_WayChecklist_Id  and User_Id ='" + FloginUser.UserId + @"'
	where t1.Two_WayChecklistType<>'1' order by t1.CreateTime,t1.Two_WayChecklist_Name";
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
                //string Temp = "<li><div class=\"name {2}\"><a href='##' id='{0}' two_id='{3}'>{1}</a></div></li>";

                string Temp = "<li><div class='mtree_link mtree-link-hook {2}' id='{0}' two_id='{3}'><div class='mtree_indent mtree-indent-hook'></div><div class='mtree_btn mtree-btn-hook'></div><div class='mtree_name mtree-name-hook'>{1}</div></div></li>";

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(item["Two_WayChecklistToTeacher_Id"].ToString()))//添加
                        {
                            Str += string.Format(Temp
                                , item["Two_WayChecklist_Id"].ToString()
                                , item["Two_WayChecklist_Name"].ToString()
                                , item["Two_WayChecklist_Id"].ToString() == Two_WayChecklist_Id ? "active" : ""
                                , item["Two_WayChecklist_Id"].ToString());
                        }
                        else//修改
                        {
                            Str += string.Format(Temp
                                , item["Two_NewWayChecklist_Id"].ToString()
                                , item["Two_WayChecklist_Name"].ToString()
                                , item["Two_NewWayChecklist_Id"].ToString() == Two_WayChecklist_Id ? "active" : ""
                                , item["Two_WayChecklist_Id"].ToString());
                        }
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

            strTempHtml.Append("<div class='iframe-sidebar '>");
            strTempHtml.Append("<div class='mtree mtree-class-hook'>");
            strTempHtml.Append("<ul>");
            for (int i = 0; i < dtUserGroupList.Rows.Count; i++)
            {
                strTempHtml.Append("<li>");
                strTempHtml.AppendFormat("<div class='mtree_link mtree-link-hook {2}' data-ugid='{0}' val='{1}'>"
                    , GetUrl(dtUserGroupList.Rows[i]["UserGroup_Id"].ToString(), "")
                    , dtUserGroupList.Rows[i]["UserGroup_Id"].ToString().Trim()
                    , strUserGroup_IdActivity.Trim() == dtUserGroupList.Rows[i]["UserGroup_Id"].ToString().Trim() ? "active" : "");
                strTempHtml.Append("<div class='mtree_indent mtree-indent-hook'></div>");
                strTempHtml.Append("<div class='mtree_btn mtree-btn-hook'></div>");
                strTempHtml.AppendFormat("<div class='mtree_name mtree-name-hook'>{0}({1})</div>"
                    , pfunction.GetSubstring(dtUserGroupList.Rows[i]["UserGroup_Name"].ToString().Trim(), 10, true)
                    , dtUserGroupList.Rows[i]["UserGroup_Id"].ToString().Trim());
                strTempHtml.Append("</div>");
                //strTempHtml.AppendFormat("<div class='name {0}'>"
                //    , strUserGroup_IdActivity.Trim() == dtUserGroupList.Rows[i]["UserGroup_Id"].ToString().Trim() ? "active" : ""
                //    );
                //strTempHtml.AppendFormat("<a href='##' onclick=\"Show('{0}','{4}')\" val='{2}' title=\"{3}\">{1}<span>({2})</span>"
                //    , GetUrl(dtUserGroupList.Rows[i]["UserGroup_Id"].ToString(), "")
                //    , pfunction.GetSubstring(dtUserGroupList.Rows[i]["UserGroup_Name"].ToString().Trim(), 10, true)
                //    , dtUserGroupList.Rows[i]["UserGroup_Id"].ToString().Trim()
                //    , dtUserGroupList.Rows[i]["UserGroup_Name"]
                //    , new Random().Next()
                //    );
                //strTempHtml.Append("</div>");
                //strTempHtml.Append("</a>");
                strTempHtml.Append("</li>");
            }
            strTempHtml.Append("</ul>");
            strTempHtml.Append("</div>");
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
                if (string.IsNullOrEmpty(dt.Rows[0]["Two_NewWayChecklist_Id"].ToString()))
                {
                    strTemp = dt.Rows[0]["Two_WayChecklist_Id"].ToString();
                }
                else
                {
                    strTemp = dt.Rows[0]["Two_NewWayChecklist_Id"].ToString();

                }
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
            Rc.Model.Resources.Model_F_User FloginUser = (Rc.Model.Resources.Model_F_User)HttpContext.Current.Session["FLoginUser"];
            try
            {
                Two_WayChecklist_Id = Two_WayChecklist_Id.Filter();
                UserGroupId = UserGroupId.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                string str = string.Empty;
                strSqlCount = string.Format(@"select count(*) from (
select A.*,hw.HomeWork_Id
from ResourceToResourceFolder A
INNER JOIN AlreadyedTestpaper B ON A.ResourceToResourceFolder_Id=B.ResourceToResourceFolder_Id and Two_WayChecklist_Id='{0}' and user_id='{1}'
left join HomeWork hw on hw.UserGroup_Id='{2}' and hw.ResourceToResourceFolder_Id=A.ResourceToResourceFolder_Id 
union 
select A.*,hw.HomeWork_Id
from ResourceToResourceFolder A
INNER JOIN AlreadyedTestpaper B ON A.ResourceToResourceFolder_Id=B.ResourceToResourceFolder_Id
inner join Two_WayChecklistToTeacher C on B.Two_WayChecklist_Id=C.Two_NewWayChecklist_Id and Teacher_Id='{1}'
left join HomeWork hw on hw.UserGroup_Id='{2}' and hw.ResourceToResourceFolder_Id=A.ResourceToResourceFolder_Id 
where C.Two_WayChecklist_Id='{0}'
) a", Two_WayChecklist_Id, FloginUser.UserId, UserGroupId);
                strSql = string.Format(@"select * from (
select *,ROW_NUMBER() over(ORDER BY A.CreateTime) row  from
(
select A.*,hw.HomeWork_Id
from ResourceToResourceFolder A
INNER JOIN AlreadyedTestpaper B ON A.ResourceToResourceFolder_Id=B.ResourceToResourceFolder_Id and Two_WayChecklist_Id='{0}' and user_id='{1}'
left join HomeWork hw on hw.UserGroup_Id='{2}' and hw.ResourceToResourceFolder_Id=A.ResourceToResourceFolder_Id 
union 
select A.*,hw.HomeWork_Id
from ResourceToResourceFolder A
INNER JOIN AlreadyedTestpaper B ON A.ResourceToResourceFolder_Id=B.ResourceToResourceFolder_Id
inner join Two_WayChecklistToTeacher C on B.Two_WayChecklist_Id=C.Two_NewWayChecklist_Id and Teacher_Id='{1}'
left join HomeWork hw on hw.UserGroup_Id='{2}' and hw.ResourceToResourceFolder_Id=A.ResourceToResourceFolder_Id 
where C.Two_WayChecklist_Id='{0}'
) a ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize), Two_WayChecklist_Id, FloginUser.UserId, UserGroupId);
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string docName = dtRes.Rows[i]["Resource_Name"].ToString().ReplaceForFilter();
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        docName = docName,
                        EnHomeWork_Name = HttpContext.Current.Server.UrlEncode(dtRes.Rows[i]["Resource_Name"].ToString().ReplaceForFilter()),
                        docTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
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