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
using Rc.Model.Resources;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.teacher
{
    public partial class Evaluation : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptClass.DataSource = new BLL_UserGroup().GetList("UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and User_Id='" + FloginUser.UserId + "') order by UserGroup_Id").Tables[0];
            rptClass.DataBind();
        }

        [WebMethod]
        public static string GetGroupMember(string GroupId)
        {
            try
            {
                GroupId = GroupId.Filter();
                List<object> listReturn = new List<object>();
                string strWhere = string.Empty;
                strWhere += string.Format(" UserStatus='0' and User_ApplicationStatus='passed' and MembershipEnum='{0}' and UserGroup_Id='{1}' ", Rc.Model.Resources.MembershipEnum.student, GroupId);
                int inum = 0;
                BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                DataTable dt = bll.GetClassMemberListByPageEX(strWhere, "TrueName,UserName", 1, 200).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        UserId = dt.Rows[i]["User_Id"],
                        UserName = string.IsNullOrEmpty(dt.Rows[i]["TrueName"].ToString()) ? dt.Rows[i]["UserName"].ToString() : dt.Rows[i]["TrueName"].ToString()
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

        [WebMethod]
        public static string GetHomework(string GroupId, string StudentId, string DateType, string DateValue, int PageSize, int PageIndex)
        {
            try
            {
                GroupId = GroupId.Filter();

                DataTable dtCHW = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strWhereC = string.Empty;
                string strWhere = string.Empty;
                strWhere += string.Format(" UserGroup_Id='{1}' ", GroupId);
                int inum = 0;
                BLL_HomeWork bll = new BLL_HomeWork();
                DataTable dt = bll.GetListForStatisticsByPage(strWhere, "CreateTime desc", (PageIndex - 1) * PageSize + 1, PageIndex * PageSize).Tables[0];
                int recordCount = bll.GetListForStatisticsRecordcount(strWhere);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    int studentCount = Convert.ToInt32(dt.Rows[i]["StudentCount"]);
                    int submitCount = Convert.ToInt32(dt.Rows[i]["SubmitCount"]);
                    listReturn.Add(new
                    {
                        HomeWork_Id = dt.Rows[i]["HomeWork_Id"],
                        ResourceToResourceFolder_Id = dt.Rows[i]["ResourceToResourceFolder_Id"],
                        HomeWork_Name = dt.Rows[i]["HomeWork_Name"],
                        UserGroup_Id = dt.Rows[i]["UserGroup_Id"],
                        HomeworkCount = dt.Rows[i]["HomeworkCount"],
                        StudentCount = dt.Rows[i]["StudentCount"],
                        SubmitCount = dt.Rows[i]["SubmitCount"],
                        SubmitRate = ((submitCount / studentCount) * 100).ToString("0.00")
                    });
                }
                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = recordCount,
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