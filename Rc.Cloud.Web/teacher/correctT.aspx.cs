﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Cloud.Web.Common;
using Rc.Common;
using System.Text;
using Newtonsoft.Json;
using Rc.Interface;
using Rc.Common.DBUtility;

namespace Rc.Cloud.Web.teacher
{
    public partial class correctT : Rc.Cloud.Web.Common.FInitData
    {
        string ClassId = string.Empty;
        protected string ResourceToResourceFolder_Id = string.Empty;
        protected string HomeWork_Id = string.Empty;
        protected string stuHomeWorkId = string.Empty;
        protected Model_HomeWork modelHW = new Model_HomeWork();
        protected DataTable dtStuScore = new DataTable();
        protected DataTable dtStuScoreBig = new DataTable();
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        protected string hwCreateTime = string.Empty;
        protected string tchId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");
            ClassId = Request.QueryString["ClassId"].Filter();
            stuHomeWorkId = Request.QueryString["stuHomeWorkId"].Filter();
            HomeWork_Id = Request.QueryString["HomeWork_Id"].Filter();
            tchId = FloginUser.UserId;
            if (!IsPostBack)
            {
                modelHW = new BLL_HomeWork().GetModel(HomeWork_Id);
                hwCreateTime = modelHW.CreateTime.ToString();
                this.Title = ltlTitle.Text = modelHW.HomeWork_Name;
                ResourceToResourceFolder_Id = modelHW.ResourceToResourceFolder_Id;
                if (!string.IsNullOrEmpty(ResourceToResourceFolder_Id)) LoadData();
            }
        }

        private void LoadData()
        {
            Model_Student_HomeWork modelSHW = new BLL_Student_HomeWork().GetModel(stuHomeWorkId);
            Model_Student_HomeWork_Correct modelSHWCorrect = new BLL_Student_HomeWork_Correct().GetModel(stuHomeWorkId);
            if (modelSHW != null && modelSHWCorrect != null)
            {
                if (modelSHWCorrect.Student_HomeWork_CorrectStatus == 1)
                {
                    btnSubmit.Visible = false;
                    divCorrectTQ.Visible = false;
                }
                else
                {
                    string strCorrectTQ = string.Format(@"select distinct shwa.TestQuestions_Id,tq.TestQuestions_Num,tq.topicNumber from Student_HomeWorkAnswer shwa
inner join TestQuestions tq on tq.TestQuestions_Id=shwa.TestQuestions_Id
where shwa.isRead='0' and shwa.Student_Homework_Id='{0}'
order  by tq.TestQuestions_Num", stuHomeWorkId);
                    DataTable dtCorrectTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strCorrectTQ).Tables[0];
                    if (dtCorrectTQ.Rows.Count == 0)
                    {
                        divCorrectTQ.Visible = false;
                    }
                    else
                    {
                        rptCorrectTQ.DataSource = dtCorrectTQ;
                        rptCorrectTQ.DataBind();
                    }
                }
                string strSqlStudentClass = @"select um.User_ID,ug.UserGroup_Name,u.UserName,u.TrueName from dbo.UserGroup_Member um
left join UserGroup ug on um.UserGroup_Id=ug.UserGroup_Id
inner join F_User u on u.UserId=um.User_ID
where um.User_ApplicationStatus='passed' and ug.UserGroup_Id='" + ClassId + "' and um.User_Id='" + modelSHW.Student_Id + "'";
                DataTable dtStudentClass = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlStudentClass).Tables[0];
                if (dtStudentClass.Rows.Count == 1)
                {
                    ltlStudentName.Text = string.IsNullOrEmpty(dtStudentClass.Rows[0]["TrueName"].ToString()) ? dtStudentClass.Rows[0]["UserName"].ToString() : dtStudentClass.Rows[0]["TrueName"].ToString();
                    ltlClassName.Text = dtStudentClass.Rows[0]["UserGroup_Name"].ToString();
                }
            }
            choice.Visible = choice2.Visible = truefalse.Visible = truefalse2.Visible = false;
            DataTable dt = new DataTable();
            #region 选择题、完形填空题
            string strSqlSel = string.Empty;

            strSqlSel = @"select SHWA.*,TQS.TestCorrect,TQS.TestQuestions_Score,TQS.TestType,TQS.testIndex,tq.topicNumber from TestQuestions_Score TQS
inner join TestQuestions tq on tq.TestQuestions_Id=TQS.TestQuestions_Id and tq.Parent_Id=''
left join Student_HomeWorkAnswer SHWA on TQS.TestQuestions_Id=SHWA.TestQuestions_Id and TQS.TestQuestions_Score_ID=SHWA.TestQuestions_Score_ID
where 1=1 ";

            strSqlSel += string.Format("AND Student_HomeWork_Id='{0}'", stuHomeWorkId);
            strSqlSel += " and TQS.TestType in('selection','clozeTest') order  by SHWA.TestQuestions_Num,SHWA.TestQuestions_Detail_OrderNum ";
            string strSqlSelection = strSqlSel + " ";
            dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlSelection).Tables[0];
            rptStuHomeworkSelection.DataSource = dt;
            rptStuHomeworkSelection.DataBind();
            if (dt.Rows.Count > 0)
            {
                choice.Visible = choice2.Visible = true;
            }
            #endregion
            #region 判断题
            string strSqlTruefalse = string.Empty;

            strSqlTruefalse = @"select SHWA.*,TQS.TestCorrect,TQS.TestQuestions_Score,tq.topicNumber from TestQuestions_Score TQS
inner join TestQuestions tq on tq.TestQuestions_Id=TQS.TestQuestions_Id and tq.Parent_Id=''
left join Student_HomeWorkAnswer SHWA on TQS.TestQuestions_Id=SHWA.TestQuestions_Id and TQS.TestQuestions_Score_ID=SHWA.TestQuestions_Score_ID
where 1=1 ";
            strSqlTruefalse += string.Format("AND Student_HomeWork_Id='{0}'", stuHomeWorkId);
            strSqlTruefalse += " and TQS.TestType='truefalse' order  by SHWA.TestQuestions_Num,SHWA.TestQuestions_Detail_OrderNum ";
            dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlTruefalse).Tables[0];
            rptStuHomeworkTruefalse.DataSource = dt;
            rptStuHomeworkTruefalse.DataBind();
            if (dt.Rows.Count > 0)
            {
                truefalse.Visible = truefalse2.Visible = true;
            }
            #endregion

            #region 普通题型 list 得分情况
            string strSql = string.Format(@"select t.TestType,isnull(SUM(t.TestQuestions_Score),0) as FullScore,isnull(SUM(t2.Student_Score),0) as StuScore from TestQuestions_Score t
inner join TestQuestions tq on tq.TestQuestions_Id=t.TestQuestions_Id and tq.Parent_Id=''
left join Student_HomeWorkAnswer t2 on t2.TestQuestions_Score_ID=t.TestQuestions_Score_ID and t2.Student_Homework_Id='{0}'
where t.TestQuestions_Score!=-1 and t.ResourceToResourceFolder_Id='{1}'
group by t.TestType ", stuHomeWorkId, ResourceToResourceFolder_Id);
            dtStuScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            #endregion

            #region 综合题型 listBig 得分情况
            string strSqlBig = string.Format(@"select tq.Parent_Id,t.TestType,SUM(t.TestQuestions_Score) as FullScore,SUM(t2.Student_Score) as StuScore from TestQuestions_Score t
inner join TestQuestions tq on tq.TestQuestions_Id=t.TestQuestions_Id and tq.Parent_Id<>'0' and tq.Parent_Id<>''
left join Student_HomeWorkAnswer t2 on t2.TestQuestions_Score_ID=t.TestQuestions_Score_ID and t2.Student_Homework_Id='{0}'
where t.TestQuestions_Score!=-1 and t.ResourceToResourceFolder_Id='{1}'
group by tq.Parent_Id,t.TestType ", stuHomeWorkId, ResourceToResourceFolder_Id);
            dtStuScoreBig = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlBig).Tables[0];
            #endregion
        }

    }
}