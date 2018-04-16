using Rc.BLL.Resources;
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
using Newtonsoft.Json;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.Principal
{
    public partial class STGrowthTranjectory : Rc.Cloud.Web.Common.FInitData
    {
        protected string UserId = string.Empty;
        public string GradeId = string.Empty;
        public string GradeName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            GradeName = Request.QueryString["GradeName"].Filter();
            UserId = FloginUser.UserId;
            if (!IsPostBack)
            {
                ltlGradeName.Text = Server.UrlDecode(GradeName);
                GetSubject();
                GetClass();
            }
        }

        /// <summary>
        /// 学科
        /// </summary>
        protected void GetSubject()
        {
            ltlSubject.Text = StatsCommonHandle.GetTeacherSubjectData();
        }

        /// <summary>
        /// 班级
        /// </summary>
        protected void GetClass()
        {
            ltlClass.Text = StatsCommonHandle.GetGradeAllClass(GradeId);
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetList(string GradeId, string SubjectID, string ClassID, string key, int PageIndex, int PageSize)
        {
            try
            {
                SubjectID = SubjectID.Filter();
                ClassID = ClassID.Filter();
                GradeId = GradeId.Filter();
                key = key.Filter();
                HttpContext.Current.Session["StatsGradeSubject"] = SubjectID;
                string strWhere = "1=1 ";
                if (!string.IsNullOrEmpty(SubjectID))
                {
                    strWhere += " and  SubjectID = '" + SubjectID + "'";
                }
                if (!string.IsNullOrEmpty(ClassID))
                {
                    strWhere += " and  ClassID = '" + ClassID + "'";
                }
                if (!string.IsNullOrEmpty(GradeId))
                {
                    strWhere += " and  GradeID = '" + GradeId + "'";
                }
                if (!string.IsNullOrEmpty(key))
                {
                    strWhere += " and  StudentName like '%" + key + "%'";
                }
                strWhere += @" AND StatsClassStudentHW_ScoreID IN(select StatsClassStudentHW_ScoreID from (
                select StatsClassStudentHW_ScoreID, ROW_NUMBER() over(partition by TeacherID,SubjectID,StudentId order by HomeWorkCreateTime  desc) as rowNum
                from StatsClassStudentHW_Score where " + strWhere + " ) t where rowNum=1)";

                DataTable dt = new DataTable();
                BLL_StatsClassStudentHW_Score bll = new BLL_StatsClassStudentHW_Score();
                dt = bll.GetListByPage(strWhere, "ClassName,StudentName", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                int intRecordCount = bll.GetRecordCount(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        ClassName = dt.Rows[i]["ClassName"].ToString(),
                        SubjectName = dt.Rows[i]["SubjectName"].ToString(),
                        StudentName = dt.Rows[i]["StudentName"].ToString(),
                        Resource_Name = dt.Rows[i]["Resource_Name"].ToString().ReplaceForFilter(),
                        StudentScore = dt.Rows[i]["StudentScore"].ToString().clearLastZero(),
                        StudentScoreOrder = Convert.ToDecimal(dt.Rows[i]["StudentScoreOrder"]).ToString("0"),
                        ScoreImprove = dt.Rows[i]["ScoreImprove"].ToString(),
                        ScoreImproveAbs = Math.Abs(Convert.ToInt16(dt.Rows[i]["ScoreImprove"].ToString())),
                        StatsClassStudentHW_ScoreID = dt.Rows[i]["StatsClassStudentHW_ScoreID"].ToString()
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