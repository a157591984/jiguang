using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using System.Data;
using System.Text;
using System.Web.Services;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.Principal
{
    public partial class STGrowthPath : Rc.Cloud.Web.Common.FInitData
    {
        protected string UserId = string.Empty;
        protected string StatsClassStudentHW_ScoreID = string.Empty;
        protected string PageTitle = string.Empty;
        protected string HomeWork_ID = string.Empty;
        protected string Student_ID = string.Empty;
        protected string SubjectID = string.Empty;
        protected string ClassID = string.Empty;
        protected string GradeID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = FloginUser.UserId;
            StatsClassStudentHW_ScoreID = Request.QueryString["StatsClassStudentHW_ScoreID"].Filter();
            if (!IsPostBack)
            {
                getCommon();
            }
        }


        /// <summary>
        /// 获得基本信息
        /// </summary>
        /// <returns></returns>
        protected void getCommon()
        {
            StringBuilder commonInfo = new StringBuilder();
            DataTable dt = new DataTable();
            BLL_StatsClassStudentHW_Score bll_StatsClassStudentHW_Score = new BLL_StatsClassStudentHW_Score();
            dt = bll_StatsClassStudentHW_Score.GetList("StatsClassStudentHW_ScoreID = '" + StatsClassStudentHW_ScoreID + "'").Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                PageTitle = item["StudentName"].ToString() + "的成长轨迹";
                Student_ID = item["StudentID"].ToString();
                SubjectID = item["SubjectID"].ToString();
                ClassID = item["ClassID"].ToString();
                GradeID = item["GradeID"].ToString();
                commonInfo.AppendFormat("<span>年级：{0}</span>", item["GradeName"].ToString());
                commonInfo.AppendFormat("<span>班级：{0}</span>", item["ClassName"].ToString());
                commonInfo.AppendFormat("<span>老师：{0}</span>", item["TeacherName"].ToString());
            }
            ltlCommonInfo.Text = commonInfo.ToString();
        }


        /// <summary>
        /// 获得数据
        /// </summary>
        [WebMethod]
        public static string getData(string Student_ID, string SubjectID, string ClassID, string GradeID)
        {
            try
            {
                Student_ID = Student_ID.Filter();
                SubjectID = SubjectID.Filter();
                ClassID = ClassID.Filter();
                GradeID = GradeID.Filter();
                string strScore = string.Empty;
                string strScoreOrder = string.Empty;
                string strHWName = string.Empty;
                BLL_StatsClassStudentHW_Score bll_StatsClassStudentHW_Score = new BLL_StatsClassStudentHW_Score();
                List<Model_StatsClassStudentHW_Score> list = new List<Model_StatsClassStudentHW_Score>();
                list = bll_StatsClassStudentHW_Score.GetModelList("StudentID = '" + Student_ID + "' AND SubjectID='" + SubjectID + "' AND ClassID = '" + ClassID + "' AND GradeID = '" + GradeID + "' ORDER BY HomeWorkCreateTime ASC");
                foreach (var item in list)
                {
                    strScore += (Convert.ToDecimal(item.HWScore.ToString()) == 0 ? "0" : Convert.ToDecimal(item.StudentScore / item.HWScore * 100).ToString("0.00")) + ",";
                    strScoreOrder += item.StudentScoreOrder.ToString() + ",";
                    strHWName += ((!string.IsNullOrEmpty(item.Resource_Name.ToString())) ? item.Resource_Name.ToString().ReplaceForFilter() : "-") + ",";
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    err = "null",
                    Score = strScore.TrimEnd(','),
                    ScoreOrder = strScoreOrder.TrimEnd(','),
                    HWName = strHWName.TrimEnd(',')
                });
            }
            catch (Exception)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    err = "error"
                });
            }
        }

    }
}