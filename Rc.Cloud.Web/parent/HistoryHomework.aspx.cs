using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
namespace Rc.Cloud.Web.parent
{
    public partial class HistoryHomework : Rc.Cloud.Web.Common.FInitData
    {
        Model_F_User loginUser = new Model_F_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            loginUser = FloginUser;
            if (!IsPostBack)
            {
                LoadMyBaby();
            }
        }
        [WebMethod]
        /// <summary>
        /// 加载学生购买过的课程的学科
        /// </summary>
        public static string GetSubject(string BabyId)
        {
            try
            {
                BabyId = BabyId.Filter();
                string SubjectName = string.Empty;
                string StrSql = string.Format(@"select distinct hw.SubjectId,t.D_Name as SubjectName from Student_HomeWork shw
inner join HomeWork hw on shw.HomeWork_Id=hw.HomeWork_Id
left join Common_Dict t on t.Common_Dict_ID=hw.SubjectId
where shw.Student_Id='{0}' ", BabyId);
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
                return "<li><a href='##' class=\"active\" SubjectId='-1'>全部</a></li>" + SubjectName;
            }
            catch (Exception)
            {

                return "";
            }
        }

        /// <summary>
        /// 加载宝贝
        /// </summary>
        private void LoadMyBaby()
        {
            try
            {
                string BabyName = string.Empty;
                string StrSql = @"select ISNULL(fu.TrueName,fu.UserName) BabyName,fu.UserId,stp.Parent_ID from F_User fu
left join StudentToParent stp on stp.Student_ID=fu.UserId where Parent_ID='" + loginUser.UserId + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BabyName += string.Format("<li><a href='##' BabyId='{0}'>{1}</a></li>"
                            , dt.Rows[i]["UserId"]
                            , dt.Rows[i]["BabyName"]);
                    }
                }
                ltlBabyName.Text = BabyName;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 宝贝作业
        /// </summary>
        /// <param name="HomeWork_AssignTeacher"></param>
        /// <param name="SubjectId"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetoHomework(string BabyId, string SubjectId, string ReName, string HomeWorkCreateTime, string HomeWorkFinishTime, int PageSize, int PageIndex)
        {
            try
            {
                BabyId = BabyId.Filter();
                SubjectId = SubjectId.Filter();
                ReName = ReName.Filter();
                HomeWorkCreateTime = HomeWorkCreateTime.Filter();
                HomeWorkFinishTime = HomeWorkFinishTime.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());

                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Format(" and shwSubmit.Student_HomeWork_Status='1' and SHW.Student_Id='{0}'", BabyId);
                if (!string.IsNullOrEmpty(SubjectId) && SubjectId != "-1")
                {
                    strWhere += " and hw.SubjectId='" + SubjectId + "'";
                }
                if (!string.IsNullOrEmpty(ReName))
                {
                    strWhere += " and Re.ResourceFolder_Name like '%" + ReName.Trim() + "%'";
                }
                if (!string.IsNullOrEmpty(HomeWorkCreateTime))
                {
                    strWhere += " and HW.CreateTime > '" + HomeWorkCreateTime + "'";
                }
                if (!string.IsNullOrEmpty(HomeWorkFinishTime))
                {
                    strWhere += " and HW.CreateTime <= '" + HomeWorkFinishTime + "'";
                }

                strSqlCount = @"select count(*) from Student_HomeWork SHW
inner join HomeWork HW ON SHW.HomeWork_Id=HW.HomeWork_Id 
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
left join ResourceToResourceFolder res on res.ResourceToResourceFolder_Id=HW.ResourceToResourceFolder_Id 
left join ResourceFolder re on re.ResourceFolder_Level=5 and re.Book_ID=res.Book_ID where 1=1 " + strWhere + " ";

                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY SHW.CreateTime DESC) row,SHW.Student_HomeWork_Id,shw.HomeWork_Id,shw.Student_Id,shw.CreateTime,shwCorrect.CorrectTime,shwCorrect.Student_HomeWork_CorrectStatus
,shwCorrect.CorrectMode,shwSubmit.Student_HomeWork_Status,shwSubmit.OpenTime,shwSubmit.StudentIP,shwSubmit.Student_Answer_Time,HW.ResourceToResourceFolder_Id,re.ResourceFolder_Name BookName,cd.D_Name as SubjectName,cd.Common_Dict_ID as SubjectId,HW.HomeWork_Name,sc.HWScore,sc.StudentScore,sc.StudentScoreOrder,HW.BeginTime,HW.StopTime,HW.HomeWork_AssignTeacher,HW.CreateTime as MakeTime,re.ResourceFolder_Id
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
                        CorrectMode = dtRes.Rows[i]["CorrectMode"],
                        StudentScore = dtRes.Rows[i]["StudentScore"].ToString() == "" ? "-" : dtRes.Rows[i]["StudentScore"].ToString().clearLastZero(),
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


        /// <summary>
        /// 获取学生局域网IP
        /// </summary>
        /// <param name="BabyId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string loadSchoolUrl(string BabyId)
        {
            try
            {
                if (HttpContext.Current.Session["StudentId_LocalUrl"] == null ||
                    (HttpContext.Current.Session["StudentId_LocalUrl"] != null && HttpContext.Current.Session["StudentId_LocalUrl"].ToString() != BabyId))
                {
                    string local_url = string.Empty; // 局域网地址
                    #region 学校配置URL
                    DataTable dtUrl = new Rc.BLL.Resources.BLL_ConfigSchool().GetSchoolPublicUrl(BabyId).Tables[0];
                    if (dtUrl.Rows.Count > 0) // 有局域网配置数据
                    {
                        HttpContext.Current.Session["UserPublicUrl" + BabyId] = dtUrl.Rows[0]["publicUrl"];
                        HttpContext.Current.Session["StudentId_LocalUrl"] = BabyId;
                        local_url = dtUrl.Rows[0]["apiUrlList"].ToString();
                        return JsonConvert.SerializeObject(new
                        {
                            err = "",
                            local_url = local_url,
                            local_url_en = Rc.Common.DBUtility.DESEncrypt.Encrypt(local_url)
                        });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            err = "未配置局域网数据"
                        });
                    }
                    #endregion
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "已验证局域网"
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