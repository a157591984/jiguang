using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using System.Text;
using Rc.Model.Resources;
using System.Diagnostics;
namespace Rc.Cloud.Web.Evaluation
{
    public partial class EachHWAnalysis : Rc.Cloud.Web.Common.FInitData
    {
        public string UserId = string.Empty;
        public bool Isheadmaster = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = FloginUser.UserId;
            if (!IsPostBack)
            {
                string sql = @"select * from [dbo].[VW_UserOnClassGradeSchool] 
where ClassMemberShipEnum='headmaster' and userId='" + UserId + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Isheadmaster = true;
                }
                GetSubject();
                GetAllTeacher();
            }

        }


        /// <summary>
        /// 学科
        /// </summary>
        protected void GetSubject()
        {
            ltlSubject.Text = StatsCommonHandle.GetTeacherSubject();
        }

        /// <summary>
        /// 老师（班主任和代课老师）
        /// </summary>
        protected void GetAllTeacher()
        {
            ltlTeacher.Text = StatsCommonHandle.GetAllTeacher();
        }


        /// <summary>
        /// 分页得到班级作业成绩概况列表
        /// </summary>
        /// <param name="HWName">家庭作业名称</param>
        /// <param name="HomeWorkCreateTime">开始时间</param>
        /// <param name="HomeWorkFinishTime">结束时间</param>
        /// <param name="TeacherID">老师Id</param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetEachHWAnalysis(string HWName, string HomeWorkCreateTime, string HomeWorkFinishTime, string SubjectID, string TeacherID, int PageIndex, int PageSize)
        {
            try
            {
                HWName = HWName.Filter();
                HomeWorkCreateTime = HomeWorkCreateTime.Filter();
                HomeWorkFinishTime = HomeWorkFinishTime.Filter();
                TeacherID = TeacherID.Filter();
                HttpContext.Current.Session["StatsClassSubject"] = SubjectID;
                string strWhere = " TeacherId='" + TeacherID + "' ";
                if (!string.IsNullOrEmpty(HWName))
                {
                    strWhere += " and  HomeWork_Name like '%" + HWName + "%'";
                }
                if (!string.IsNullOrEmpty(HomeWorkCreateTime))
                {
                    strWhere += " and CreateTime > '" + HomeWorkCreateTime + "'";
                }
                if (!string.IsNullOrEmpty(HomeWorkFinishTime))
                {
                    strWhere += " and CreateTime <= '" + HomeWorkFinishTime + "'";
                }
                if (!string.IsNullOrEmpty(SubjectID))
                {
                    strWhere += " and SubjectID = '" + SubjectID + "'";
                }
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                strWhere += StatsCommonHandle.GetStrWhereBySelfClassForTeacherData(SubjectID);

                DataTable dt = new DataTable();
                BLL_StatsClassHW_Score bll = new BLL_StatsClassHW_Score();

                string strSqlMain = string.Format(@"select hw.HomeWork_ID,hw.ResourceToResourceFolder_Id,hw.HomeWork_Name,hw.HomeWork_AssignTeacher as TeacherId,hw.UserGroup_Id as ClassId,hw.SubjectId,hw.CreateTime
,ug.UserGroup_Name as ClassName,cd.D_Name as SubjectName,fu.UserName,fu.TrueName from HomeWork hw
left join UserGroup ug on ug.UserGroup_Id=hw.UserGroup_Id
left join Common_Dict cd on cd.Common_Dict_Id=hw.SubjectId
left join F_User fu on fu.UserId=hw.HomeWork_AssignTeacher
 where hw.UserGroup_Id in(select UserGroup_Id from UserGroup_Member where UserStatus='0' and MemberShipEnum='{2}' and User_Id='{0}')
or (hw.SubjectId='{1}' and hw.HomeWork_AssignTeacher='{0}')"
                    , TeacherID
                    , SubjectID
                    , MembershipEnum.headmaster);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( ");
                strSql.Append(" SELECT ROW_NUMBER() OVER (");
                strSql.Append("order by T.CreateTime desc");
                strSql.AppendFormat(")AS Row, T.*  from ( {0} ) T ", strSqlMain);
                strSql.Append(" WHERE " + strWhere);
                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize);
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                StringBuilder strSqlCount = new StringBuilder();
                strSqlCount.AppendFormat("SELECT count(1)  from ( {0} ) T ", strSqlMain);
                strSqlCount.Append(" WHERE " + strWhere);

                int intRecordCount = int.Parse(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount.ToString()).ToString());
                //                string strSql = string.Format(@"select hw.*,ug.UserGroup_Name,cd.D_Name as SubjectName from HomeWork hw
                //left join UserGroup ug on ug.UserGroup_Id=hw.UserGroup_Id
                //left join Common_Dict cd on cd.Common_Dict_Id=hw.SubjectId
                // where hw.UserGroup_Id in(select UserGroup_Id from UserGroup_Member where UserStatus='0' and MemberShipEnum='{2}' and User_Id='{0}')
                //or (hw.SubjectId='{1}' and hw.HomeWork_AssignTeacher='{0}')"
                //                    , TeacherID
                //                    , SubjectID);
                //dt = bll.GetListByPage(strWhere, "HomeWorkCreateTime desc", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                //int intRecordCount = bll.GetRecordCount(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strPG = GetCorrectStudent(dt.Rows[i]["HomeWork_ID"].ToString());
                    string strTJ = GetSubmitStudent(dt.Rows[i]["HomeWork_ID"].ToString());
                    listReturn.Add(new
                    {
                        HomeWork_ID = dt.Rows[i]["HomeWork_ID"].ToString(),
                        HomeWork_Name = dt.Rows[i]["HomeWork_Name"].ToString(),
                        ClassName = dt.Rows[i]["ClassName"].ToString(),
                        ClassNameEncode = HttpContext.Current.Server.UrlEncode(dt.Rows[i]["ClassName"].ToString()),
                        SubjectName = dt.Rows[i]["SubjectName"].ToString(),
                        CreateTime = pfunction.ToShortDate(dt.Rows[i]["CreateTime"].ToString()),
                        ResourceToResourceFolder_Id = dt.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        TeacherID = dt.Rows[i]["TeacherID"].ToString(),
                        TeacherName = string.IsNullOrEmpty(dt.Rows[i]["TrueName"].ToString()) ? dt.Rows[i]["UserName"].ToString() : dt.Rows[i]["TrueName"].ToString(),
                        ClassID = dt.Rows[i]["ClassID"].ToString(),
                        TJ = strTJ,
                        PG = strPG,
                        HaveTJ = strTJ.Split('/')[0],
                        IsTeacherData = dt.Rows[i]["TeacherID"].ToString() == loginUser.UserId ? "" : "1"
                    });
                    inum++;
                }
                if (inum > 1)
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
                    err = ex.Message.ToString()
                });
            }
        }

        private static string GetSubmitStudent(string HomeWork_Id)
        {
            string temp = string.Empty;
            try
            {
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(@"select COUNT(1) as ICount,shwSubmit.Student_HomeWork_Status from Student_HomeWork shw 
                inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
                where HomeWork_Id='" + HomeWork_Id + "' group by shwSubmit.Student_HomeWork_Status").Tables[0];
                DataRow[] drYJ = dt.Select("Student_HomeWork_Status='1'");
                DataRow[] drWJ = dt.Select("Student_HomeWork_Status='0'");
                temp = (drYJ.Length > 0 ? drYJ[0]["ICount"].ToString() : "0") + "/";
                temp += (drWJ.Length > 0 ? drWJ[0]["ICount"].ToString() : "0");
            }
            catch (Exception)
            {
                temp = "0/0";
            }
            return temp;
        }
        private static string GetCorrectStudent(string HomeWork_Id)
        {
            string temp = string.Empty;
            try
            {
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(@"select COUNT(1) as ICount,shwCorrect.Student_HomeWork_CorrectStatus from Student_HomeWork shw
  inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id 
   inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
where HomeWork_Id='" + HomeWork_Id + "' and shwSubmit.Student_HomeWork_Status=1 group by shwCorrect.Student_HomeWork_CorrectStatus").Tables[0];
                DataRow[] drYP = dt.Select("Student_HomeWork_CorrectStatus='1'");
                DataRow[] drWP = dt.Select("Student_HomeWork_CorrectStatus='0'");
                temp = (drYP.Length > 0 ? drYP[0]["ICount"].ToString() : "0") + "/";
                temp += (drWP.Length > 0 ? drWP[0]["ICount"].ToString() : "0");
            }
            catch (Exception)
            {
                temp = "0/0";
            }
            return temp;
        }

        /// <summary>
        /// 重新计算
        /// </summary>
        [WebMethod]
        public static string ReCalculation(string hwId, string hwName)
        {
            try
            {
                DataTable dtHWDetail = new BLL_HomeWork().GetHWDetail(hwId).Tables[0];
                //System.Threading.Thread.Sleep(10000);
                hwId = hwId.Filter();
                hwName = hwName.Filter();
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                #region 按作业HomeWork 执行数据分析，记录日志
                Model_StatsLog modelLog = new Model_StatsLog();
                modelLog.StatsLogId = Guid.NewGuid().ToString();
                modelLog.DataId = hwId;
                modelLog.DataName = hwName;
                modelLog.DataType = "1";
                modelLog.LogStatus = "2";
                modelLog.CTime = DateTime.Now;
                modelLog.CUser = loginUser.UserId;
                modelLog.GradeId = dtHWDetail.Rows[0]["GradeId"].ToString();

                bool flag = new BLL_StatsLog().ReExecuteStatsAddLog(modelLog);
                #endregion
                if (flag)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("", "重新计算失败：" + ex.Message.ToString());
                return "0";
            }
        }

        /// <summary>
        /// 验证计算
        /// </summary>
        [WebMethod]
        public static string CheckCalculation(string hwId, string hwName)
        {
            try
            {
                DataTable dtHWDetail = new BLL_HomeWork().GetHWDetail(hwId).Tables[0];
                //System.Threading.Thread.Sleep(10000);
                hwId = hwId.Filter();
                hwName = hwName.Filter();
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                #region 按作业HomeWork 执行数据分析，记录日志
                Model_StatsLog modelLog = new Model_StatsLog();
                modelLog.StatsLogId = Guid.NewGuid().ToString();
                modelLog.DataId = hwId;
                modelLog.DataName = hwName;
                modelLog.DataType = "1";
                modelLog.LogStatus = "2";
                modelLog.CTime = DateTime.Now;
                modelLog.CUser = loginUser.UserId;
                modelLog.GradeId = dtHWDetail.Rows[0]["GradeId"].ToString();

                bool flag = new BLL_StatsLog().ExecuteStatsAddLog(modelLog);
                #endregion
                if (flag)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("", "计算失败：" + ex.Message.ToString());
                return "0";
            }
        }

    }
}