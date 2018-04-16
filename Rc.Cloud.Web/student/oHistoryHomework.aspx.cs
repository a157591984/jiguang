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
using Rc.Common.StrUtility;
using Rc.Model.Resources;

namespace Homework.student
{
    public partial class oHistoryHomework : Rc.Cloud.Web.Common.FInitData
    {
        Model_F_User loginUser = new Model_F_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            loginUser = FloginUser;
            if (!IsPostBack)
            {
                LoadSubject();
            }
        }

        /// <summary>
        /// 加载学生购买过的课程的学科
        /// </summary>
        private void LoadSubject()
        {
            try
            {
                string SubjectName = string.Empty;
//                string StrSql = @"select distinct(cd.D_Name),cd.Common_Dict_ID from UserBuyResources ubr
//inner join dbo.ResourceFolder re on re.ResourceFolder_Id=ubr.Book_id
//left join dbo.Common_Dict cd on cd.Common_Dict_ID=re.Subject where UserId='" + loginUser.UserId + "'";
                string StrSql = string.Format(@"select distinct hw.SubjectId,t.D_Name as SubjectName from Student_HomeWork shw
inner join HomeWork hw on shw.HomeWork_Id=hw.HomeWork_Id
left join Common_Dict t on t.Common_Dict_ID=hw.SubjectId
where shw.Student_Id='{0}' ", loginUser.UserId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SubjectName += string.Format("<li><a href='##' SubjectId='{0}'>{1}</a></li>"
                            , dt.Rows[i]["SubjectId"]
                            , dt.Rows[i]["SubjectName"]);
                    }
                }
                ltlSubjectName.Text = "<li><a href='##' class=\"active\" SubjectId='-1'>全部</a></li>" + SubjectName;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        public static string GetoHomework(string SubjectId, string BookName, int PageIndex, int PageSize)
        {
            try
            {
                SubjectId = SubjectId.Filter();
                BookName = BookName.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                Model_F_User loginUser = (Model_F_User)HttpContext.Current.Session["FloginUser"];
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Format(" and shwSubmit.Student_HomeWork_Status='1' and SHW.Student_Id='{0}'", loginUser.UserId);
                if (!string.IsNullOrEmpty(SubjectId) && SubjectId != "-1")
                {
                    strWhere += " and hw.SubjectId='" + SubjectId + "'";
                }

                if (!string.IsNullOrEmpty(BookName))
                {
                    strWhere += " and Re.ResourceFolder_Name like '%" + BookName + "%'";
                }

                strSqlCount = @"select count(*) from Student_HomeWork SHW
inner join HomeWork HW ON SHW.HomeWork_Id=HW.HomeWork_Id 
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
left join ResourceToResourceFolder res on res.ResourceToResourceFolder_Id=HW.ResourceToResourceFolder_Id
left join ResourceFolder re on re.ResourceFolder_Level=5 and re.Book_ID=res.Book_ID where 1=1 " + strWhere + " ";

                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY SHW.CreateTime DESC) row,SHW.Student_HomeWork_Id,shw.HomeWork_Id,shw.Student_Id,shw.CreateTime,shwCorrect.CorrectTime,shwCorrect.Student_HomeWork_CorrectStatus
,shwCorrect.CorrectMode,shwSubmit.Student_HomeWork_Status,shwSubmit.OpenTime,shwSubmit.StudentIP,shwSubmit.Student_Answer_Time,HW.ResourceToResourceFolder_Id,HW.HomeWork_Name,HW.BeginTime,HW.StopTime,HW.HomeWork_AssignTeacher,cd.D_Name as SubjectName,cd.Common_Dict_ID as SubjectId,re.ResourceFolder_Name BookName,sc.HWScore,sc.StudentScore,sc.StudentScoreOrder,HW.CreateTime as MakeTime
from Student_HomeWork SHW 
inner join HomeWork HW ON SHW.HomeWork_Id=HW.HomeWork_Id 
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id 
left join ResourceToResourceFolder res on res.ResourceToResourceFolder_Id=HW.ResourceToResourceFolder_Id
left join dbo.Common_Dict cd on cd.Common_Dict_ID=hw.SubjectId
left join ResourceFolder re on re.ResourceFolder_Level=5 and re.Book_ID=res.Book_ID
left join StatsClassStudentHW_Score sc on sc.HomeWork_ID=HW.HomeWork_Id and sc.StudentID=shw.Student_Id where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        Student_HomeWork_Id = dtRes.Rows[i]["Student_HomeWork_Id"].ToString(),
                        Student_HomeWork_CorrectStatus = dtRes.Rows[i]["Student_HomeWork_CorrectStatus"].ToString(),
                        HomeWork_Id = dtRes.Rows[i]["HomeWork_Id"].ToString(),
                        Student_Id = dtRes.Rows[i]["Student_Id"].ToString(),
                        Student_HomeWork_Status = dtRes.Rows[i]["Student_HomeWork_Status"].ToString(),
                        HomeWork_Name = dtRes.Rows[i]["HomeWork_Name"].ToString(),
                        ResourceToResourceFolder_Id = dtRes.Rows[i]["ResourceToResourceFolder_Id"],
                        BookName = string.IsNullOrEmpty(dtRes.Rows[i]["BookName"].ToString()) ? "老师自有作业" : dtRes.Rows[i]["BookName"],
                        SubjectName = dtRes.Rows[i]["SubjectName"],
                        HWScore = dtRes.Rows[i]["HWScore"].ToString() == "" ? "-" : dtRes.Rows[i]["HWScore"].ToString().clearLastZero(),
                        StudentScoreOrder = dtRes.Rows[i]["StudentScoreOrder"].ToString() == "" ? "-" : dtRes.Rows[i]["StudentScoreOrder"].ToString().clearLastZero(),
                        MakeTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["MakeTime"].ToString(), "yyyy-MM-dd HH:mm:ss"),
                        PG = (dtRes.Rows[i]["Student_HomeWork_Status"].ToString() == "1" && dtRes.Rows[i]["Student_HomeWork_CorrectStatus"].ToString() == "1") ? "1" : "0", // 是否批改
                        TJ = dtRes.Rows[i]["Student_HomeWork_Status"].ToString() == "1" ? "1" : "0", // 是否提交
                        CorrectMode = dtRes.Rows[i]["CorrectMode"].ToString(),
                        StudentScore = dtRes.Rows[i]["StudentScore"].ToString() == "" ? "-" : dtRes.Rows[i]["StudentScore"].ToString().clearLastZero()
                    });
                }

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