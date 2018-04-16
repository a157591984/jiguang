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
    public partial class ClassGrowthPath : Rc.Cloud.Web.Common.FInitData
    {
        protected string StatsClassHW_ScoreID = string.Empty;
        protected string PageTitle = string.Empty;
        protected string SubjectID = string.Empty;
        protected string ClassID = string.Empty;
        protected string GradeID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            StatsClassHW_ScoreID = Request.QueryString["StatsClassHW_ScoreID"].Filter();
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
            BLL_StatsClassHW_Score bll_StatsClassHW_Score = new BLL_StatsClassHW_Score();
            dt = bll_StatsClassHW_Score.GetList("StatsClassHW_ScoreID = '" + StatsClassHW_ScoreID + "'").Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                PageTitle = item["ClassName"].ToString() + "的成长轨迹";
                //Student_ID = item["StudentID"].ToString();
                SubjectID = item["SubjectID"].ToString();
                ClassID = item["ClassID"].ToString();
                GradeID = item["GradeID"].ToString();
                commonInfo.AppendFormat("<span>年级：{0}</span>", item["GradeName"].ToString());
                commonInfo.AppendFormat("<span>学科：{0}</span>", item["SubjectName"].ToString());
                commonInfo.AppendFormat("<span>老师：{0}</span>", item["TeacherName"].ToString());
                //commonInfo.AppendFormat("<span>满分：{0}分</span>", Convert.ToInt16(item["HWScore"]).ToString());
            }
            ltlCommonInfo.Text = commonInfo.ToString();
        }


        /// <summary>
        /// 获得数据
        /// </summary>
        [WebMethod]
        public static string getData( string SubjectID, string ClassID, string GradeID)
        {
            try
            {
                SubjectID = SubjectID.Filter();
                ClassID = ClassID.Filter();
                GradeID = GradeID.Filter();
                string strAVGScore = string.Empty;
                string strAVGScoreRate = string.Empty;
                string strHWScore = string.Empty;
                //string strScoreOrder = string.Empty;
                string strHWName = string.Empty;
                BLL_StatsClassHW_Score bll_StatsClassHW_Score = new BLL_StatsClassHW_Score();
                List<Model_StatsClassHW_Score> list = new List<Model_StatsClassHW_Score>();
                list = bll_StatsClassHW_Score.GetModelList("SubjectID='" + SubjectID + "' AND ClassID = '" + ClassID + "'AND GradeID = '" + GradeID + "' ORDER BY HomeWorkCreateTime ASC");
                foreach (var item in list)
                {
                    strAVGScore += item.AVGScore.ToString() + ",";
                    strAVGScoreRate += item.AVGScoreRate.ToString() + ",";
                    strHWScore += item.HW_Score.ToString() + ",";
                    strHWName += ((!string.IsNullOrEmpty(item.Resource_Name.ToString())) ? item.Resource_Name.ToString().ReplaceForFilter() : "-") + ",";
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    AVGScore = strAVGScore.TrimEnd(','),
                    AVGScoreRate = strAVGScoreRate.TrimEnd(','),
                    HWScore = strHWScore.TrimEnd(','),
                    HWName = strHWName.TrimEnd(',')
                });
            }
            catch (Exception)
            {
                return "err";
            }
        }


    }
}