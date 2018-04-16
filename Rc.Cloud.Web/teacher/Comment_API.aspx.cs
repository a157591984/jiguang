using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
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
using Newtonsoft.Json;

namespace Rc.Cloud.Web.teacher
{
    public partial class Comment_API : Rc.Cloud.Web.Common.FInitData
    {
        public string UserId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = FloginUser.UserId;
        }


        [WebMethod]
        /// <summary>
        /// 班级
        /// </summary>
        public static string GetClass(string TeacherID)
        {
            try
            {

                string StrWhere = string.Empty;
                string TempDate = string.Empty;
                StrWhere = @" and ClassId in(select ClassId from (
select distinct vw.ClassId,AnalysisDataCount=(select COUNT(1) from HomeWork where UserGroup_Id=vw.ClassId)
from VW_UserOnClassGradeSchool vw where ClassMemberShipEnum in('" + MembershipEnum.headmaster + "','" + MembershipEnum.teacher + "') and UserId='" + TeacherID + "') t where AnalysisDataCount>0) ";

                TempDate = StatsCommonHandle.GetTeacherClassByTeacherIdForCommon(TeacherID, StrWhere);
                return TempDate;
            }
            catch (Exception)
            {

                throw;
            }
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
        public static string GetEachHWAnalysis(string HWName, string HomeWorkCreateTime, string HomeWorkFinishTime, string ClassID, string TeacherID, int PageIndex, int PageSize)
        {
            try
            {
                HWName = HWName.Filter();
                HomeWorkCreateTime = HomeWorkCreateTime.Filter();
                HomeWorkFinishTime = HomeWorkFinishTime.Filter();
                ClassID = ClassID.Filter();
                TeacherID = TeacherID.Filter();
                string strWhere = "WHERE T.HomeWork_AssignTeacher ='" + TeacherID + "' ";
                if (!string.IsNullOrEmpty(HWName))
                {
                    strWhere += " and  T.HomeWork_Name like '%" + HWName + "%'";
                }
                if (!string.IsNullOrEmpty(HomeWorkCreateTime))
                {
                    strWhere += " and Convert(nvarchar(10),T.CreateTime,23) >= '" + HomeWorkCreateTime + "'";
                }
                if (!string.IsNullOrEmpty(HomeWorkFinishTime))
                {
                    strWhere += " and Convert(nvarchar(10),T.CreateTime,23) <= '" + HomeWorkFinishTime + "'";
                }
                if (!string.IsNullOrEmpty(ClassID))
                {
                    strWhere += " and T.UserGroup_Id = '" + ClassID + "'";
                }

                DataTable dt = new DataTable();
                BLL_StatsClassHW_Score bll = new BLL_StatsClassHW_Score();
                CommonHandel bllc = new CommonHandel();
                dt = bllc.GetListByPageForCommonHandel(strWhere, " T.CreateTime desc", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                int intRecordCount = bllc.GetRecordCountForCommonHandel(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                string strSql = "select COUNT(1) as ICount,HomeWork_Id from Student_HomeWork shw inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id  where shwSubmit.Student_HomeWork_Status=1 group by HomeWork_Id";
                DataTable dtWTJ = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataRow[] dr = dtWTJ.Select("HomeWork_Id='" + dt.Rows[i]["HomeWork_Id"].ToString() + "'");
                    listReturn.Add(new
                    {
                        HomeWork_Name = dt.Rows[i]["HomeWork_Name"].ToString(),
                        ClassName = dt.Rows[i]["ClassName"].ToString(),
                        HomeWork_Id = dt.Rows[i]["HomeWork_Id"].ToString(),
                        CreateTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                        StatsClassHW_ScoreID = dt.Rows[i]["StatsClassHW_ScoreID"].ToString(),
                        ResourceToResourceFolder_Id = dt.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        HighestScore = dt.Rows[i]["HighestScore"].ToString() == "" ? "-" : dt.Rows[i]["HighestScore"].ToString().clearLastZero(),
                        LowestScore = dt.Rows[i]["LowestScore"].ToString() == "" ? "-" : dt.Rows[i]["LowestScore"].ToString().clearLastZero(),
                        AVGScore = dt.Rows[i]["AVGScore"].ToString() == "" ? "-" : dt.Rows[i]["AVGScore"].ToString().clearLastZero(),
                        Median = dt.Rows[i]["Median"].ToString() == "" ? "-" : dt.Rows[i]["Median"].ToString().clearLastZero(),
                        Mode = dt.Rows[i]["Mode"].ToString() == "" ? "-" : dt.Rows[i]["Mode"].ToString(),
                        WTJ = dr.Length > 0 ? "1" : "0"
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

    }
}