using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Data;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
namespace Rc.Cloud.Web.student
{
    public partial class CorrectHomework : Rc.Cloud.Web.Common.FInitData
    {
        public string UserId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = FloginUser.UserId;
        }
        /// <summary>
        /// 作业列表
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetCorrectHomework()
        {
            try
            {
                Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                string userId = modelFUser.UserId;
                List<object> listReturn = new List<object>();
                string SqlMTJ = @"select  COUNT(*) as MTJ,shw.HomeWork_Id from Student_HomeWork shw 
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwSubmit.Student_HomeWork_Status=1 
inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwCorrect.Student_HomeWork_CorrectStatus=0 
inner join Student_Mutual_CorrectSub smc on smc.Student_HomeWork_Id=shw.Student_HomeWork_Id 
where smc.Student_Mutual_Correct_Id='" + userId + "' group by shw.HomeWork_Id ";
                DataTable dtMTJ = Rc.Common.DBUtility.DbHelperSQL.Query(SqlMTJ).Tables[0];
                string SqlHw = @" select distinct hw.* from HomeWork hw
inner join Student_Mutual_CorrectSub smc on smc.HomeWork_Id=hw.HomeWork_Id and smc.Student_Mutual_Correct_Id='" + userId + "' order by hw.CreateTime desc ";
                DataTable dtHw = Rc.Common.DBUtility.DbHelperSQL.Query(SqlHw).Tables[0];
                int inum = 0;
                if (dtHw.Rows.Count > 0)
                {
                    foreach (DataRow item in dtHw.Rows)
                    {
                        DataRow[] drMTJ = dtMTJ.Select("HomeWork_Id='" + item["HomeWork_Id"] + "'");
                        inum++;
                        DateTime stopTime = DateTime.Now;
                        DateTime.TryParse(item["StopTime"].ToString(), out stopTime);
                        listReturn.Add(new
                        {
                            HomeWork_Id = item["HomeWork_Id"],
                            HomeWork_Name = item["HomeWork_Name"],
                            HomeWork_AssignTeacher = item["HomeWork_AssignTeacher"],
                            HomeWork_FinishTime = item["HomeWork_FinishTime"],
                            HomeWork_Status = item["HomeWork_Status"],
                            CreateTime = pfunction.ConvertToLongDateTime(item["CreateTime"].ToString(), "MM-dd"),
                            IsCorrect = item["HomeWork_Status"].ToString() == "1" ? "no" : "yes",//作业已标记完成，不允许批改
                            IsUpdate = (stopTime > DateTime.Now || string.IsNullOrEmpty(item["CorrectMode"].ToString())) ? "no" : "yes",
                            StudentCountMTJ = drMTJ.Length > 0 ? drMTJ[0]["MTJ"] : "0" // 未批人数
                        });
                    }
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
        /// <summary>
        /// 已交学生/未交学生列表
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStudentHomework(string HomeworkId, string IsCorrect)
        {
            try
            {
                HomeworkId = HomeworkId.Filter();
                Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                string userId = modelFUser.UserId;
                DataTable dtCHW = new DataTable();
                List<object> listYJ = new List<object>();
                string strSql = string.Empty;
                string strWhere = string.Empty;
                strWhere += string.Format(" where smc.HomeWork_Id='{0}' and smc.Student_Mutual_Correct_Id='{1}' order by shc.Student_HomeWork_CorrectStatus,OpenTime desc", HomeworkId, userId);

                strSql = @"select shw.Student_HomeWork_Id,shw.HomeWork_Id,shw.Student_Id,shw.CreateTime,shc.Student_HomeWork_CorrectStatus ,shs.Student_HomeWork_Status,
                  shc.CorrectTime,shs.OpenTime,shc.CorrectMode,hw.UserGroup_Id,
                 (CASE WHEN U.TrueName IS NULL THEN U.UserName WHEN U.TrueName = '' THEN U.UserName ELSE U.TrueName END) AS TrueName from Student_HomeWork SHW 
                inner join HomeWork hw on hw.HomeWork_Id=SHW.HomeWork_Id
                LEFT JOIN dbo.F_User AS U ON SHW.Student_Id = U.UserId 
                inner join Student_HomeWork_Submit shs on shs.Student_HomeWork_Id=shw.Student_HomeWork_Id 
                inner join Student_HomeWork_Correct shc on shc.Student_HomeWork_Id=shw.Student_HomeWork_Id
                inner join Student_Mutual_CorrectSub smc on smc.Student_HomeWork_Id=SHW.Student_HomeWork_Id " + strWhere;

                dtCHW = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                DataTable dtStuScore = Rc.Common.DBUtility.DbHelperSQL.Query("select ISNULL(SUM(Student_Score), 0) as Student_Score,Student_Id,ISNULL(MAX(CreateTime), '') as Student_AnswerTime from Student_HomeWorkAnswer where Student_HomeWork_Id in(select Student_HomeWork_Id from Student_HomeWork where HomeWork_Id='" + HomeworkId + "') group by Student_Id").Tables[0];
                int inum = 0;
                DataRow[] drYJ = dtCHW.Select("Student_HomeWork_Status='1'");
                for (int i = 0; i < drYJ.Length; i++)
                {
                    inum++;
                    DataRow[] drStuScore = dtStuScore.Select("Student_Id='" + drYJ[i]["Student_Id"] + "'");
                    string strStuScore = string.Empty;
                    string strStudent_AnswerTime = string.Empty;
                    if (drStuScore.Length > 0)
                    {
                        strStuScore = (drStuScore[0]["Student_Score"].ToString() == "0") ? "-" : Convert.ToDecimal(drStuScore[0]["Student_Score"]).ToString("0.0");
                        strStudent_AnswerTime = pfunction.ConvertToLongDateTime(drStuScore[0]["Student_AnswerTime"].ToString(), "yyyy-MM-dd HH:mm:ss");
                    }
                    listYJ.Add(new
                    {
                        Student_HomeWork_Id = drYJ[i]["Student_HomeWork_Id"],
                        HomeWork_Id = drYJ[i]["HomeWork_Id"],
                        Student_Id = drYJ[i]["Student_Id"],
                        Student_HomeWork_Status = drYJ[i]["Student_HomeWork_Status"],
                        Student_HomeWork_CorrectStatus = drYJ[i]["Student_HomeWork_CorrectStatus"],
                        CreateTime = pfunction.ConvertToLongDateTime(drYJ[i]["CreateTime"].ToString()),
                        TrueName = drYJ[i]["TrueName"],
                        Student_Score = strStuScore,
                        Student_AnswerTime = strStudent_AnswerTime,
                        CorrectTime = pfunction.ConvertToLongDateTime(drYJ[i]["CorrectTime"].ToString(), "yy-MM-dd HH:mm:ss"),
                        OpenTime = pfunction.ConvertToLongDateTime(drYJ[i]["OpenTime"].ToString(), "yyyy-MM-dd HH:mm:ss"),
                        IsCorrect = IsCorrect,
                        CorrectMode = drYJ[i]["CorrectMode"],
                        url = string.Format("{0}?stuHomeWorkId={1}&HomeWork_Id={2}&ClassId={3}"
                        , (drYJ[i]["CorrectMode"].ToString() == "1") ? "/teacher/correct_client.aspx" : "/teacher/correctT.aspx"
                        , drYJ[i]["Student_HomeWork_Id"]
                        , drYJ[i]["HomeWork_Id"]
                        , drYJ[i]["UserGroup_Id"])
                    });
                }
                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        listYJ = listYJ
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