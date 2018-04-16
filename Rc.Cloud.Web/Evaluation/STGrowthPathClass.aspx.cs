﻿using System;
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

namespace Rc.Cloud.Web.Evaluation
{
    public partial class STGrowthPathClass : Rc.Cloud.Web.Common.FInitData
    {
        protected string UserId = string.Empty;
        protected string StatsClassHW_ScoreID = string.Empty;
        protected string HomeWork_Name = string.Empty;
        string HomeWork_ID = string.Empty;
        protected string Student_ID = string.Empty;
        protected string SubjectID = string.Empty;
        public string StudentName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = FloginUser.UserId;
            StatsClassHW_ScoreID = Request.QueryString["StatsClassHW_ScoreID"].Filter();
            Student_ID = Request.QueryString["StudentID"].Filter();
            HomeWork_ID = Request.QueryString["HomeWork_ID"].Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(StatsClassHW_ScoreID))
                {

                    getCommon();
                }
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
            dt = new BLL_StatsClassStudentHW_Score().GetList("StudentId = '" + Student_ID + "' and HomeWork_ID= '" + HomeWork_ID + "' ").Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                StudentName = item["StudentName"].ToString() + "的成长轨迹";
                HomeWork_Name = item["Resource_Name"].ToString().ReplaceForFilter();
                SubjectID = item["SubjectID"].ToString();
                commonInfo.AppendFormat("<span>年级：{0}</span>", item["GradeName"].ToString());
                commonInfo.AppendFormat("<span>班级：{0}</span>", item["ClassName"].ToString());
                commonInfo.AppendFormat("<span>学生：{0}</span>", item["StudentName"].ToString());
                commonInfo.AppendFormat("<span>满分：{0}分</span>", item["HWScore"].ToString().clearLastZero());
                commonInfo.AppendFormat("<span>得分：{0}分</span>", item["StudentScore"].ToString().clearLastZero());
            }
            ltlCommonInfo.Text = commonInfo.ToString();
        }


        /// <summary>
        /// 获得数据
        /// </summary>
        [WebMethod]
        public static string getData(string Student_ID, string SubjectID)
        {
            try
            {
                string strScore = string.Empty;
                string strScoreOrder = string.Empty;
                string strHWName = string.Empty;
                BLL_StatsClassStudentHW_Score bll_StatsClassStudentHW_Score = new BLL_StatsClassStudentHW_Score();
                List<Model_StatsClassStudentHW_Score> list = new List<Model_StatsClassStudentHW_Score>();
                list = bll_StatsClassStudentHW_Score.GetModelList("StudentID = '" + Student_ID + "' and SubjectID='" + SubjectID + "' ORDER BY HomeWorkCreateTime");
                foreach (var item in list)
                {
                    strScore += item.StudentScore.ToString() + ",";
                    strScoreOrder += item.StudentScoreOrder.ToString() + ",";
                    strHWName += ((!string.IsNullOrEmpty(item.Resource_Name.ToString())) ? item.Resource_Name.ToString().ReplaceForFilter() : "-") + ",";
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Score = strScore.TrimEnd(','),
                    ScoreOrder = strScoreOrder.TrimEnd(','),
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