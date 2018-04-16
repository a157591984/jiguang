using Rc.Cloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Web.Common;
using System.Web.Services;
using System.Data;
using Rc.BLL.Resources;
using System.Text;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.Principal
{
    public partial class ClassCorrectDetail : Rc.Cloud.Web.Common.FInitData
    {
        public string UserId = string.Empty;
        Model_F_User user = new Model_F_User();
        public string GradeId = string.Empty;
        public string GradeName = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        public string SubjectId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            SubjectId = Request.QueryString["SubjectId"].Filter();
            UserId = FloginUser.UserId;
            if (!IsPostBack)
            {
            }
        }

        /// <summary>
        /// 分页得到班级作业成绩概况列表
        /// </summary>
        [WebMethod]
        public static string GetEachHWAnalysis(string GradeId, string SubjectId, string ResourceToResourceFolder_Id, int PageIndex, int PageSize)
        {
            try
            {
                GradeId = GradeId.Filter();
                SubjectId = SubjectId.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();

                HttpContext.Current.Session["StatsClassSubject"] = SubjectId;
                string strWhere = " ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' ";

                DataTable dt = new DataTable();
                BLL_StatsClassHW_Score bll = new BLL_StatsClassHW_Score();

                string strSqlMain = string.Format(@"select hw.HomeWork_ID,hw.ResourceToResourceFolder_Id,hw.HomeWork_Name,hw.HomeWork_AssignTeacher as TeacherId,hw.UserGroup_Id as ClassId,hw.SubjectId,hw.CreateTime,hw.HomeWork_FinishTime,hw.HomeWork_Status
,ug.UserGroup_Name as ClassName,cd.D_Name as SubjectName,fu.UserName,fu.TrueName from HomeWork hw
left join UserGroup ug on ug.UserGroup_Id=hw.UserGroup_Id
left join Common_Dict cd on cd.Common_Dict_Id=hw.SubjectId
left join F_User fu on fu.UserId=hw.HomeWork_AssignTeacher
 where hw.SubjectId='{0}' and hw.UserGroup_Id in(select ClassId from VW_ClassGradeSchool where GradeId='{1}') "
                    , SubjectId
                    , GradeId);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( ");
                strSql.Append(" SELECT ROW_NUMBER() OVER (");
                strSql.Append("order by T.HomeWork_FinishTime desc");
                strSql.AppendFormat(")AS Row, T.*  from ( {0} ) T ", strSqlMain);
                strSql.Append(" WHERE " + strWhere);
                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize);
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                StringBuilder strSqlCount = new StringBuilder();
                strSqlCount.AppendFormat("SELECT count(1)  from ( {0} ) T ", strSqlMain);
                strSqlCount.Append(" WHERE " + strWhere);

                int intRecordCount = int.Parse(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount.ToString()).ToString());

                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strTJ =GetSubmitStudent(dt.Rows[i]["HomeWork_ID"].ToString());
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
                        TJ = GetSubmitStudent(dt.Rows[i]["HomeWork_ID"].ToString()),
                        HavaData = strTJ.Split('/')[0],
                        IsFinish = (dt.Rows[i]["HomeWork_Status"].ToString() == "1") ? "<font color='green'>是</font>" : "<font color='red'>否</font>",
                        HomeWork_FinishTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["HomeWork_FinishTime"].ToString())
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
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query("select COUNT(1) as ICount,shwSubmit.Student_HomeWork_Status from Student_HomeWork shw where HomeWork_Id='" + HomeWork_Id + "' group by shwSubmit.Student_HomeWork_Status").Tables[0];
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
                 inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
                 inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id 
                where HomeWork_Id='" + HomeWork_Id + " and shwSubmit.Student_HomeWork_Status='1'' group by shwCorrect.Student_HomeWork_CorrectStatus").Tables[0];
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
    }
}