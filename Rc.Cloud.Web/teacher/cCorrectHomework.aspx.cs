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

namespace Homework.teacher
{
    public partial class cCorrectHomework : Rc.Cloud.Web.Common.FInitData
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
            rptClass.DataSource = new BLL_UserGroup().GetList("UserGroup_AttrEnum='" + UserGroup_AttrEnum.Class + "' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and User_Id='" + FloginUser.UserId + "') order by UserGroupOrder ").Tables[0];
            rptClass.DataBind();
        }

        /// <summary>
        /// 作业列表
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetCorrectHomework(string ClassId)
        {
            try
            {
                ClassId = ClassId.Filter();

                DataTable dtCHW = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strWhereC = string.Empty;
                string strWhere = string.Empty;

                string SqlMTJ = @"
select  COUNT(*) as MTJ,shw.HomeWork_Id from Student_HomeWork shw 
inner join HomeWork hw on hw.HomeWork_Id=shw.HomeWork_Id 
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwSubmit.Student_HomeWork_Status=1 
inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwCorrect.Student_HomeWork_CorrectStatus=0 
where  hw.UserGroup_Id='" + ClassId + "' group by shw.HomeWork_Id ";

                DataTable dtMTJ = Rc.Common.DBUtility.DbHelperSQL.Query(SqlMTJ).Tables[0];
                Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                string userId = modelFUser.UserId;

                strWhere += string.Format(" and UserGroup_Id='{0}' and HomeWork_AssignTeacher='{1}' ", ClassId, userId);
                int inum = 0;
                Model_PagerInfo<List<Model_HomeWork>> pageInfo = new BLL_HomeWork().SearhList(strWhere, "CreateTime DESC", 1, 1000);
                List<Model_HomeWork> list = pageInfo.PageData;
                foreach (var item in list)
                {
                    DataRow[] drMTJ = dtMTJ.Select("HomeWork_Id='" + item.HomeWork_Id + "'");
                    inum++;
                    DateTime stopTime = DateTime.Now;
                    DateTime.TryParse(item.StopTime.ToString(), out stopTime);
                    listReturn.Add(new
                    {
                        HomeWork_Id = item.HomeWork_Id,
                        HomeWork_Name = item.HomeWork_Name,
                        HomeWork_AssignTeacher = item.HomeWork_AssignTeacher,
                        HomeWork_FinishTime = item.HomeWork_FinishTime,
                        HomeWork_Status = item.HomeWork_Status,
                        CreateTime = pfunction.ConvertToLongDateTime(item.CreateTime.ToString(), "yyyy-MM-dd"),
                        UserGroup_Id = item.UserGroup_Id,
                        IsCorrect = item.HomeWork_Status == 1 ? "no" : "yes",//作业已标记完成，不允许批改
                        IsUpdate = (stopTime > DateTime.Now || string.IsNullOrEmpty(item.CorrectMode)) ? "no" : "yes",

                        StudentCountMTJ = drMTJ.Length > 0 ? drMTJ[0]["MTJ"] : "0" // 未批人数
                    });
                }
                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = pageInfo.CurrentPage,
                        PageSize = pageInfo.PageSize,
                        TotalCount = pageInfo.RecordCount,
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
        public static string GetStudentHomework(string HomeworkId, string IsCorrect, string ClassId)
        {
            try
            {
                HomeworkId = HomeworkId.Filter();

                DataTable dtCHW = new DataTable();
                List<object> listYJ = new List<object>();
                List<object> listWJ = new List<object>();
                string strSql = string.Empty;
                string strWhere = string.Empty;
                strWhere += string.Format(" where HomeWork_Id='{0}' order by shc.Student_HomeWork_CorrectStatus,OpenTime desc", HomeworkId);

                strSql = @"select shw.Student_HomeWork_Id,shw.HomeWork_Id,shw.Student_Id,shw.CreateTime,shc.Student_HomeWork_CorrectStatus ,shs.Student_HomeWork_Status,
                  shc.CorrectTime,shs.OpenTime,shs.Student_Answer_Time,shc.CorrectMode,(CASE WHEN u1.TrueName IS NULL THEN u1.UserName WHEN u1.TrueName = '' THEN u1.UserName ELSE u1.TrueName END) as CourrectName,
                 (CASE WHEN U.TrueName IS NULL THEN U.UserName WHEN U.TrueName = '' THEN U.UserName ELSE U.TrueName END) AS TrueName from Student_HomeWork SHW LEFT JOIN dbo.F_User AS U ON SHW.Student_Id = U.UserId 
                inner join Student_HomeWork_Submit shs on shs.Student_HomeWork_Id=shw.Student_HomeWork_Id 
                inner join Student_HomeWork_Correct shc on shc.Student_HomeWork_Id=shw.Student_HomeWork_Id
                left join f_user u1 on u1.UserId=shc.CorrectUser" + strWhere;

                dtCHW = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                DataTable dtStuScore = Rc.Common.DBUtility.DbHelperSQL.Query("select ISNULL(SUM(Student_Score), 0) as Student_Score,Student_Id,ISNULL(MAX(CreateTime), '') as Student_AnswerTime from Student_HomeWorkAnswer where Student_HomeWork_Id in(select Student_HomeWork_Id from Student_HomeWork where HomeWork_Id='" + HomeworkId + "') group by Student_Id").Tables[0];
                int inum = 0;
                DataRow[] drYJ = dtCHW.Select("Student_HomeWork_Status='1'");
                DataRow[] drWJ = dtCHW.Select("Student_HomeWork_Status='0'");

                for (int i = 0; i < drYJ.Length; i++)
                {
                    inum++;
                    DataRow[] drStuScore = dtStuScore.Select("Student_Id='" + drYJ[i]["Student_Id"] + "'");
                    string strStuScore = string.Empty;
                    string strStudent_AnswerTime = string.Empty;
                    if (drStuScore.Length > 0)
                    {
                        strStuScore = (drStuScore[0]["Student_Score"].ToString() == "0") ? "-" : Convert.ToDecimal(drStuScore[0]["Student_Score"]).ToString("0.0");
                        //strStudent_AnswerTime = pfunction.ConvertToLongDateTime(drStuScore[0]["Student_AnswerTime"].ToString(), "yyyy-MM-dd HH:mm:ss");
                        strStudent_AnswerTime = pfunction.ConvertToLongDateTime(drYJ[i]["Student_Answer_Time"].ToString(), "yyyy-MM-dd HH:mm:ss");
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
                        CourrectName = drYJ[i]["Student_HomeWork_CorrectStatus"].ToString() == "1" ? string.IsNullOrEmpty(drYJ[i]["CourrectName"].ToString()) ? "自动批改" : drYJ[i]["CourrectName"].ToString() : "-",
                        url = string.Format("{0}?stuHomeWorkId={1}&HomeWork_Id={2}&ClassId={3}"
                        , (drYJ[i]["CorrectMode"].ToString() == "1") ? "correct_client.aspx" : "correctT.aspx"
                        , drYJ[i]["Student_HomeWork_Id"]
                        , drYJ[i]["HomeWork_Id"]
                        , ClassId)
                    });
                }
                for (int i = 0; i < drWJ.Length; i++)
                {
                    inum++;

                    listWJ.Add(new
                    {

                        Student_HomeWork_Id = drWJ[i]["Student_HomeWork_Id"],
                        HomeWork_Id = drWJ[i]["HomeWork_Id"],
                        Student_Id = drWJ[i]["Student_Id"],
                        Student_HomeWork_Status = drWJ[i]["Student_HomeWork_Status"],
                        TrueName = drWJ[i]["TrueName"]
                    });
                }

                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        listYJ = listYJ,
                        listWJ = listWJ
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
        public static string UpdateS(string Id)
        {
            try
            {
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;

                Id = Id.Filter();
                Model_HomeWork model = new Model_HomeWork();
                BLL_HomeWork bll = new BLL_HomeWork();
                model = bll.GetModel(Id);
                model.HomeWork_Status = 1;
                model.HomeWork_FinishTime = DateTime.Now;

                if (loginUser.UserId != model.HomeWork_AssignTeacher)//当前登录用户和布置改作业的老师不一致
                {
                    return "";
                }
                else
                {
                    if (bll.Update(model))
                    {
                        //#region 执行数据分析，记录日志

                        //Model_StatsLog modelLog = new Model_StatsLog();
                        //modelLog.StatsLogId = Guid.NewGuid().ToString();
                        //modelLog.DataId = model.HomeWork_Id;
                        //modelLog.DataName = model.HomeWork_Name;
                        //modelLog.DataType = "1";
                        //modelLog.LogStatus = "2";
                        //modelLog.CTime = DateTime.Now;
                        //modelLog.CUser = loginUser.UserId;

                        //new BLL_StatsLog().ExecuteStatsAddLog(modelLog);
                        //#endregion

                        return "1";
                    }
                    else
                    {
                        return "";
                    }
                }


            }
            catch (Exception)
            {

                return "";
            }
        }
        [WebMethod]
        public static string UpdateC(string Id)
        {
            try
            {
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;

                Id = Id.Filter();
                Model_HomeWork model = new Model_HomeWork();
                BLL_HomeWork bll = new BLL_HomeWork();
                model = bll.GetModel(Id);
                model.HomeWork_Status = 0;
                model.HomeWork_FinishTime = null;

                if (loginUser.UserId != model.HomeWork_AssignTeacher)//当前登录用户和布置改作业的老师不一致
                {
                    return "";
                }
                else
                {
                    if (bll.Update(model))
                    {
                        return "1";
                    }
                    else
                    {
                        return "";
                    }
                }

            }
            catch (Exception)
            {

                return "";
            }
        }
        /// <summary>
        /// 判断学生已提交学生数是否大于2
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string assignCorrect(string Id)
        {
            try
            {
                string sql = @"select *  from [dbo].[Student_HomeWork] shw
inner join [dbo].[Student_HomeWork_Submit] shwS on shwS.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwS.Student_HomeWork_Status='1'
inner join [dbo].[Student_HomeWork_Correct] shwC on shwC.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwC.Student_HomeWork_CorrectStatus='0'
 where shw.HomeWork_Id='" + Id.Filter() + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 1)
                {
                    return "1";
                }
                else
                {
                    return "2";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}