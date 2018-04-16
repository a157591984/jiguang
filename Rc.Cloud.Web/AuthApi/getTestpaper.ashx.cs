using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rc.Common.StrUtility;
using System.Text;
using System.Data;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using System.Web.Script.Serialization;
using Rc.Common;
using Rc.Interface;
using Newtonsoft.Json;
using System.IO;
using Rc.Common.DBUtility;
using Rc.Common.Config;

namespace Rc.Cloud.Web.AuthApi
{
    /// <summary>
    /// getTestpaper 的摘要说明
    /// </summary>
    public class getTestpaper : IHttpHandler
    {
        DataTable dtTQ_StudentAnswerCount = new DataTable();//学生答题状态数量（讲评所用）
        List<Model_StatsClassHW_TQ> listHW_TQ = new List<Model_StatsClassHW_TQ>();//小题统计数据（讲评所用）
        DataTable dtTQScore_CLassVAG = new DataTable();//班级平均分和得分率讲评用
        public void ProcessRequest(HttpContext context)
        {
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "text/plain";

            #region 变量
            string key = string.Empty;
            string rtrfId = string.Empty;
            string tchId = string.Empty;
            string stuId = string.Empty;
            StringBuilder stbHtml = new StringBuilder();
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            // 学校局域网地址（读取文件使用，使用局域网地址原因：思科交换机不允许局域网内使用自己的公网IP访问）
            string hostUrl = ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl");
            string uploadPath = HttpContext.Current.Server.MapPath("/Upload/Resource/"); //存储文件基础路径
            #endregion

            try
            {
                key = context.Request["key"].Filter();
                switch (key)
                {
                    case "testpaper_view":
                        #region 作业预览 /teacher/HomeworkPreviewT.aspx
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        tchId = context.Request["tchId"].Filter();

                        string strSqlScoreFormat = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex from TestQuestions_Score tqs  where tqs.ResourceToResourceFolder_Id='" + rtrfId + "'";
                        DataTable dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlScoreFormat).Tables[0];

                        strSql = @"select TQ.* from TestQuestions TQ where tq.ResourceToResourceFolder_Id='" + rtrfId + "' order by TestQuestions_Num ";
                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                        Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);

                        #region 判断老师是否购买过资源
                        bool IsBuy = false;
                        BLL_UserBuyResources bll_UserBuyResources = new BLL_UserBuyResources();

                        if (bll_UserBuyResources.GetRecordCount("Book_id='" + modelRTRF.Book_ID + "' and userid='" + tchId + "'") > 0
                            || new BLL_ResourceToResourceFolder().GetRecordCount("Resource_Class='" + Rc.Common.Config.Resource_ClassConst.自有资源 + "' and CreateFUser='" + tchId + "' and ResourceToResourceFolder_Id='" + rtrfId + "'") > 0)
                        {
                            IsBuy = true;//
                        }
                        #endregion
                        if (modelRTRF == null)
                        {
                            context.Response.Write("数据不存在或已删除");
                            context.Response.End();
                        }

                        //生成存储路径
                        string savePath = string.Empty;
                        string saveOwnPath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                        }
                        string fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dt.Rows[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");

                            //题干
                            string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"));

                            //选择题、完形填空题选项
                            string strOption = string.Empty;
                            if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest")
                            {
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    //从文件读取选择题选项
                                    string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                    List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                    if (listTestSelections != null && listTestSelections.Count > 0)
                                    {
                                        foreach (var item in listTestSelections)
                                        {
                                            if (!string.IsNullOrEmpty(item.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(item.selectionHTML));
                                        }
                                        if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                        {
                                            strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                        }
                                    }
                                }
                            }

                            if (dt.Rows[i]["TestQuestions_Type"].ToString() != "title" && dt.Rows[i]["TestQuestions_Type"].ToString() != "")
                            {
                                stbHtml.AppendFormat("<div class=\"question_panel\"><a name=\"{0}\"></a>", dt.Rows[i]["TestQuestions_Num"].ToString().TrimEnd('.'));
                                stbHtml.AppendFormat("<div class='panel_stem'>{0}</div><div class='panel_option'>{1}</div>"
                                    , pfunction.NoHTML(strTestQuestionBody) //题干
                                    , strOption //选项
                                    );

                                stbHtml.Append("<div class='panel_answer'>");
                                stbHtml.Append("<div class='answer_heading'>");
                                stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-sm btn-block'>分值</span></div>");
                                stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                stbHtml.Append("</div>");
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    stbHtml.Append("<div class='answer_body'>");
                                    //正确答案
                                    if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "truefalse")
                                    {
                                        stbHtml.AppendFormat("<div class='answer_item score'>{0} {1}</div>"
                                            , dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                            , pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()) + " 分");
                                        stbHtml.Append("<div class='answer_item reference_answer'>");
                                        if (IsBuy)
                                        {
                                            stbHtml.AppendFormat("{0}"
                                                , drScore[ii]["TestCorrect"]);
                                        }
                                        stbHtml.Append("</div>");
                                    }
                                    if (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" || dt.Rows[i]["TestQuestions_Type"].ToString() == "answers")
                                    {
                                        //从文件读取正确答案
                                        string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                        if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                        {
                                            string strTQScore = pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString());
                                            stbHtml.AppendFormat("<div class='answer_item score'>{0} {1}</div>"
                                                , (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString())) ? "(" + drScore[ii]["testIndex"].ToString() + ")" : ""
                                                , strTQScore + " 分");
                                            stbHtml.Append("<div class='answer_item reference_answer'>");
                                            if (IsBuy)
                                            {
                                                stbHtml.AppendFormat("{0}"
                                                    , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));
                                            }
                                            stbHtml.Append("</div>");

                                        }
                                    }
                                    stbHtml.Append("</div>");
                                }
                                stbHtml.Append("</div>");
                                stbHtml.Append("</div>");
                            }
                            else
                            {
                                string tqBody = pfunction.NoHTML(strTestQuestionBody);
                                if (string.IsNullOrEmpty(tqBody))
                                {
                                    tqBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "textTitle", dt.Rows[i]["TestQuestions_Id"], "txt"));
                                }
                                stbHtml.AppendFormat("<div class=\"question_type_panel\"><div class='panel_heading'>{0}</div></div>", tqBody);
                            }

                        }
                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        break;
                    case "testpaper_correct_web":
                        #region 作业批改-web端批改 /teacher/correctT.aspx
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        string hwId = context.Request["hwId"].Filter();
                        string stuHwId = context.Request["stuHwId"].Filter();
                        string hwCreateTime = context.Request["hwCreateTime"].Filter();

                        string strSqlAnswerFormat = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.TestType,tqs.testIndex
,shwa.TestQuestions_NumStr,shwa.Student_HomeWorkAnswer_Id,shwa.Student_Answer,shwa.Student_Score,shwa.Student_Answer_Status,shwa.Comment,shwa.isRead 
,tq.topicNumber
from TestQuestions_Score tqs 
inner join TestQuestions tq on tq.TestQuestions_Id=TQS.TestQuestions_Id                
left join Student_HomeWorkAnswer shwa on TQS.TestQuestions_Id=SHWA.TestQuestions_Id and TQS.TestQuestions_Score_ID=SHWA.TestQuestions_Score_ID and shwa.Student_HomeWork_Id='" + stuHwId + "' where tqs.ResourceToResourceFolder_Id='" + rtrfId + "' order by tqs.TestQuestions_OrderNum";
                        DataTable dtAnswerScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlAnswerFormat).Tables[0];//学生答题情况

                        int correctCount = 0;
                        strSql = @"select TQ.* from TestQuestions TQ where tq.ResourceToResourceFolder_Id='" + rtrfId + "'";
                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];//试题数据

                        modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);

                        //生成存储路径
                        savePath = string.Empty;
                        saveOwnPath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                        }
                        string fileTestPaperUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                        string fileStudentAnswerUrl = uploadPath + "{0}\\" + pfunction.ToShortDate(hwCreateTime) + "\\" + savePath + "{1}.{2}";//文件详细路径


                        #region 普通题型 list 得分情况
                        strSql = string.Format(@"select t.TestType,isnull(SUM(t.TestQuestions_Score),0) as FullScore,isnull(SUM(t2.Student_Score),0) as StuScore from TestQuestions_Score t
inner join TestQuestions tq on tq.TestQuestions_Id=t.TestQuestions_Id and tq.Parent_Id=''
left join Student_HomeWorkAnswer t2 on t2.TestQuestions_Score_ID=t.TestQuestions_Score_ID and t2.Student_Homework_Id='{0}'
where t.TestQuestions_Score!=-1 and t.ResourceToResourceFolder_Id='{1}'
group by t.TestType ", stuHwId, rtrfId);
                        DataTable dtStuScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        #endregion

                        #region 综合题型 listBig 得分情况
                        string strSqlBig = string.Format(@"select tq.Parent_Id,t.TestType,isnull(SUM(t.TestQuestions_Score),0) as FullScore,isnull(SUM(t2.Student_Score),0) as StuScore from TestQuestions_Score t
inner join TestQuestions tq on tq.TestQuestions_Id=t.TestQuestions_Id and tq.Parent_Id<>'0' and tq.Parent_Id<>''
left join Student_HomeWorkAnswer t2 on t2.TestQuestions_Score_ID=t.TestQuestions_Score_ID and t2.Student_Homework_Id='{0}'
where t.TestQuestions_Score!=-1 and t.ResourceToResourceFolder_Id='{1}'
group by tq.Parent_Id,t.TestType ", stuHwId, rtrfId);
                        DataTable dtStuScoreBig = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlBig).Tables[0];
                        #endregion

                        #region 普通题型 list
                        DataRow[] drFill = dt.Select("Parent_Id='' and TestQuestions_Type='fill'", "TestQuestions_Num");
                        DataRow[] drAnswers = dt.Select("Parent_Id='' and TestQuestions_Type='answers'", "TestQuestions_Num");
                        #region 填空题
                        if (drFill.Length > 0)
                        {
                            stbHtml.Append("<div class=\"question_type_panel\" id=\"fill\"><div class='panel_heading'>填空题</div></div>");
                            stbHtml.Append("<div class='question_score_panel'>");
                            if (dtStuScore.Rows.Count > 0)
                            {
                                DataRow[] drStuScore = dtStuScore.Select("TestType in('fill')");
                                if (drStuScore.Length == 1)
                                {
                                    stbHtml.AppendFormat("<span class=\"total\">总分：{0}</span>"
                                        , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                        , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                        , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));

                                }
                            }
                            stbHtml.Append("</div>");
                            for (int i = 0; i < drFill.Length; i++)
                            {
                                #region 学生答题情况DataRow
                                DataRow[] drAnswerScore = dtAnswerScore.Select("TestQuestions_Id='" + drFill[i]["TestQuestions_Id"] + "'");
                                #endregion

                                //题干
                                string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "testQuestionBody", drFill[i]["TestQuestions_Id"], "htm"));
                                stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\" data-name='question_{0}'>", drFill[i]["TestQuestions_Id"]);
                                stbHtml.AppendFormat("<div class=\"panel_stem panel-stem-hook\" data-total-score='{1}'>{0}(分值：{1})</div>"
                                    , pfunction.NoHTML(strTestQuestionBody)
                                    , drFill[i]["TestQuestions_SumScore"]);
                                stbHtml.Append("<div class=\"panel_answer\">");
                                //标题
                                stbHtml.Append("<div class='answer_heading'>");
                                stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                stbHtml.Append("<div class='answer_item student_answer'><span class='btn btn-info btn-sm'>学生答案</span></div>");
                                stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-block btn-sm'>得分</span></div>");
                                stbHtml.Append("</div>");
                                //答案&得分
                                //是否需要批改（已阅true/待阅false）
                                bool boolIsRead = true;
                                for (int ii = 0; ii < drAnswerScore.Length; ii++)
                                {
                                    if (boolIsRead && drAnswerScore[ii]["isRead"].ToString() == "0")
                                    {
                                        boolIsRead = false;
                                    }
                                    bool isJXZH = false; // 是否句型转换
                                    if (drAnswerScore.Length > 1 && pfunction.ConvertTQScore(drAnswerScore[1]["TestQuestions_Score"].ToString()) == "")
                                    {
                                        isJXZH = true;
                                    }
                                    stbHtml.Append("<div class='answer_body'>");
                                    //正确答案
                                    stbHtml.Append("<div class='answer_item reference_answer'>");
                                    //从文件读取正确答案
                                    string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "testQuestionCurrent", drAnswerScore[ii]["TestQuestions_Score_ID"], "txt"));
                                    if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                    {
                                        stbHtml.AppendFormat("<div>{0}{1}</div>"
                                            , !string.IsNullOrEmpty(drAnswerScore[ii]["testIndex"].ToString()) ? "(" + drAnswerScore[ii]["testIndex"].ToString() + ")" : ""
                                            , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));
                                    }
                                    stbHtml.Append("</div>");
                                    //学生答案 从文件读取
                                    stbHtml.Append("<div class='answer_item student_answer'>");
                                    if (!string.IsNullOrEmpty(drAnswerScore[ii]["Student_HomeWorkAnswer_Id"].ToString()))
                                    {
                                        string strstudentAnswer = pfunction.NoHTML(pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "studentAnswer", drAnswerScore[ii]["Student_HomeWorkAnswer_Id"], "txt")));
                                        stbHtml.AppendFormat("<div>{0}</div>", strstudentAnswer);
                                    }

                                    if (!isJXZH) // 非句型转换
                                    {
                                        //分值列表
                                        stbHtml.AppendFormat("<div class='score_list_panel score-list-hook {0}'>"
                                            , (drAnswerScore[ii]["isRead"].ToString() == "1") ? "disabled" : "");
                                        double dTQScore = 0; // 试题分值
                                        double dStScore = 0; // 学生得分
                                        if (!string.IsNullOrEmpty(drAnswerScore[ii]["TestQuestions_Score"].ToString())) dTQScore = Convert.ToDouble(drAnswerScore[ii]["TestQuestions_Score"]);
                                        if (!string.IsNullOrEmpty(drAnswerScore[ii]["Student_Score"].ToString())) dStScore = Convert.ToDouble(drAnswerScore[ii]["Student_Score"]);
                                        double dRemainder = dTQScore % 1; // 余数
                                        stbHtml.Append("<div class='panel_heading'>给分</div>");
                                        stbHtml.Append("<div class='panel_body'>");
                                        for (int s = 0; s <= dTQScore; s++)
                                        {
                                            stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                , s
                                                , (dStScore == s) ? "active" : "");
                                        }
                                        if (dRemainder != 0)
                                        {
                                            stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                , dTQScore
                                                , (dStScore == dTQScore) ? "active" : "");
                                        }
                                        stbHtml.Append("</div>");
                                        stbHtml.Append("</div>");
                                    }
                                    else // 句型转换
                                    {
                                        if (ii == drAnswerScore.Length - 1)
                                        {
                                            //分值列表
                                            stbHtml.AppendFormat("<div class='score_list_panel score-list-hook {0}'>"
                                                , (drAnswerScore[0]["isRead"].ToString() == "1") ? "disabled" : "");
                                            double dTQScore = 0; // 试题分值
                                            double dStScore = 0; // 学生得分
                                            if (!string.IsNullOrEmpty(drAnswerScore[ii]["TestQuestions_Score"].ToString())) dTQScore = Convert.ToDouble(drAnswerScore[ii]["TestQuestions_Score"]);
                                            if (!string.IsNullOrEmpty(drAnswerScore[ii]["Student_Score"].ToString())) dStScore = Convert.ToDouble(drAnswerScore[ii]["Student_Score"]);
                                            double dRemainder = dTQScore % 1; // 余数
                                            stbHtml.Append("<div class='panel_heading'>给分</div>");
                                            stbHtml.Append("<div class='panel_body'>");
                                            for (int s = 0; s <= dTQScore; s++)
                                            {
                                                stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                    , s
                                                    , (dStScore == s) ? "active" : "");
                                            }
                                            if (dRemainder != 0)
                                            {
                                                stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                    , dTQScore
                                                    , (dStScore == dTQScore) ? "active" : "");
                                            }
                                            stbHtml.Append("</div>");
                                            stbHtml.Append("</div>");
                                        }
                                    }

                                    stbHtml.Append("</div>");
                                    //分值
                                    stbHtml.Append("<div class='answer_item score'>");
                                    correctCount++;
                                    if (pfunction.ConvertTQScore(drAnswerScore[ii]["TestQuestions_Score"].ToString()) != "")
                                    {
                                        stbHtml.AppendFormat("<div class='score_item' data-name='FillScore'><input type=\"text\" name=\"fill_Score\" class='form-control input-sm score_input' maxlength=\"4\" actualscore=\"{0}\" tqnum=\"{1}\" tqonum=\"{2}\" value=\"{3}\" {4} /></div>"
                                        , drAnswerScore[ii]["TestQuestions_Score"]
                                        , drFill[i]["TestQuestions_Id"]
                                        , drAnswerScore[ii]["TestQuestions_OrderNum"]
                                        , drAnswerScore[ii]["Student_Score"]
                                        , (drAnswerScore[ii]["isRead"].ToString() == "1") ? "disabled='disabled'" : "");
                                    }

                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");
                                }
                                stbHtml.Append("<div class='answer_body'>");
                                stbHtml.AppendFormat("<div class='answer_item score offset'><input type='checkbox' name='isRead' {0} {3} value='1' id='isRead_{1}' class='hidden'><label class='btn {4} btn-block btn-sm' for='isRead_{1}'>{2}</label></div>"
                                    , (boolIsRead) ? "checked" : ""
                                    , drFill[i]["TestQuestions_Id"]
                                    , (boolIsRead) ? "已阅" : "待阅"
                                    , (boolIsRead) ? "disabled='disabled'" : ""
                                    , (boolIsRead) ? "btn-success" : "btn-danger"
                                    );
                                //if (drQTConfirm.Length == 0) stbHtml.Append("<span class='full_mark'>满分</span>");
                                stbHtml.Append("</div>");
                                stbHtml.Append("</div>");

                                stbHtml.Append("<div class='panel_opera'>");
                                stbHtml.Append("<div class='opera_control'>");
                                stbHtml.AppendFormat("<span class=\"btn\" data-content=\"{0}\" id='aSpan{1}' {2}>{3}</span>"
                                    , (drAnswerScore.Length == 0 ? "" : drAnswerScore[0]["Comment"])
                                    , drFill[i]["TestQuestions_Num"]
                                    , "data-name=\"remark\""
                                    , "<i class='material-icons'>&#xE0C9;</i>&nbsp;文字批注");
                                if (Rc.Cloud.Web.Common.pfunction.GetWebMdlIsShow("RecordingComment"))
                                {
                                    stbHtml.AppendFormat("<span class=\"btn\" data-value=\"{0}\" {1}>{2}</span>"
                                        , drFill[i]["TestQuestions_Id"]
                                        , "data-name=\"comment_recording\""
                                        , "<i class='material-icons'>&#xE31D;</i>&nbsp;语音批注");
                                }
                                stbHtml.Append("</div>");
                                stbHtml.Append("</div>");

                                stbHtml.Append("</div>");
                            }
                        }
                        #endregion
                        #region 简答题
                        if (drAnswers.Length > 0)
                        {
                            stbHtml.Append("<div class=\"question_type_panel\" id=\"short\"><div class='panel_heading'>简答题</div></div>");
                            stbHtml.Append("<div class='question_score_panel'>");
                            if (dtStuScore.Rows.Count > 0)
                            {
                                DataRow[] drStuScore = dtStuScore.Select("TestType in('answers')");
                                if (drStuScore.Length == 1)
                                {
                                    stbHtml.AppendFormat("<span class=\"total\">总分：{0}</span>"
                                        , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                        , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                        , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));

                                }
                            }
                            stbHtml.Append("</div>");
                            for (int i = 0; i < drAnswers.Length; i++)
                            {
                                #region 学生答题情况DataRow
                                DataRow[] drAnswerScore = dtAnswerScore.Select("TestQuestions_Id='" + drAnswers[i]["TestQuestions_Id"] + "'");
                                #endregion
                                //题干
                                string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "testQuestionBody", drAnswers[i]["TestQuestions_Id"], "htm"));
                                stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\" data-name='question_{0}'>", drAnswers[i]["TestQuestions_Id"]);
                                stbHtml.AppendFormat("<div class=\"panel_stem panel-stem-hook\" data-total-score='{1}'>{0}(分值：{1})</div>", pfunction.NoHTML(strTestQuestionBody),
                                    drAnswers[i]["TestQuestions_SumScore"]);
                                stbHtml.Append("<div class=\"panel_answer\">");
                                //标题
                                stbHtml.Append("<div class='answer_heading'>");
                                stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                stbHtml.Append("<div class='answer_item student_answer'><span class='btn btn-info btn-sm'>学生答案</span></div>");
                                stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-block btn-sm'>得分</span></div>");
                                stbHtml.Append("</div>");
                                //答案&得分
                                //是否需要批改（已阅true/待阅false）
                                bool boolIsRead = true;
                                for (int ii = 0; ii < drAnswerScore.Length; ii++)
                                {
                                    if (boolIsRead && drAnswerScore[ii]["isRead"].ToString() == "0")
                                    {
                                        boolIsRead = false;
                                    }
                                    stbHtml.Append("<div class='answer_body'>");
                                    //正确答案
                                    stbHtml.Append("<div class='answer_item reference_answer'>");
                                    //从文件读取正确答案
                                    string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "testQuestionCurrent", drAnswerScore[ii]["TestQuestions_Score_ID"], "txt"));
                                    if (!string.IsNullOrEmpty(strTestQuestionCurrent)) stbHtml.AppendFormat("<div>{0}</div>", pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));
                                    stbHtml.Append("</div>");
                                    //学生答案 从文件读取
                                    stbHtml.Append("<div class='answer_item student_answer'>");
                                    if (!string.IsNullOrEmpty(drAnswerScore[ii]["Student_HomeWorkAnswer_Id"].ToString()))
                                    {
                                        string strstudentAnswer = pfunction.NoHTML(pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "studentAnswer", drAnswerScore[ii]["Student_HomeWorkAnswer_Id"], "txt")));
                                        stbHtml.AppendFormat("<div>{0}</div>", strstudentAnswer);
                                    }

                                    if (pfunction.ConvertTQScore(drAnswerScore[ii]["TestQuestions_Score"].ToString()) != "")
                                    {
                                        //分值列表
                                        stbHtml.AppendFormat("<div class='score_list_panel score-list-hook {0}'>"
                                                , (drAnswerScore[ii]["isRead"].ToString() == "1") ? "disabled" : "");
                                        double dTQScore = 0; // 试题分值
                                        double dStScore = 0; // 学生得分
                                        if (!string.IsNullOrEmpty(drAnswerScore[ii]["TestQuestions_Score"].ToString())) dTQScore = Convert.ToDouble(drAnswerScore[ii]["TestQuestions_Score"]);
                                        if (!string.IsNullOrEmpty(drAnswerScore[ii]["Student_Score"].ToString())) dStScore = Convert.ToDouble(drAnswerScore[ii]["Student_Score"]);
                                        double dRemainder = dTQScore % 1; // 余数
                                        stbHtml.Append("<div class='panel_heading'>给分</div>");
                                        stbHtml.Append("<div class='panel_body'>");
                                        for (int s = 0; s <= dTQScore; s++)
                                        {
                                            stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                , s
                                                , (dStScore == s) ? "active" : "");
                                        }
                                        if (dRemainder != 0)
                                        {
                                            stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                , dTQScore
                                                , (dStScore == dTQScore) ? "active" : "");
                                        }
                                        stbHtml.Append("</div>");
                                        stbHtml.Append("</div>");
                                    }
                                    stbHtml.Append("</div>");
                                    //分值
                                    stbHtml.Append("<div class='answer_item score'>");
                                    correctCount++;
                                    if (pfunction.ConvertTQScore(drAnswerScore[ii]["TestQuestions_Score"].ToString()) != "")
                                    {
                                        stbHtml.AppendFormat("<div class='score_item' data-name='FillScore'><input type=\"text\" name=\"fill_Score\" class='form-control input-sm score_input' maxlength=\"4\" actualscore=\"{0}\" tqnum=\"{1}\" tqonum=\"{2}\" value=\"{3}\" {4} /></div>"
                                            , drAnswerScore[ii]["TestQuestions_Score"]
                                            , drAnswers[i]["TestQuestions_Id"]
                                            , drAnswerScore[ii]["TestQuestions_OrderNum"]
                                            , drAnswerScore[ii]["Student_Score"]
                                            , (drAnswerScore[ii]["isRead"].ToString() == "1") ? "disabled='disabled'" : "");
                                    }
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");
                                }
                                stbHtml.Append("<div class='answer_body'>");
                                stbHtml.AppendFormat("<div class='answer_item score offset'><input type='checkbox' name='isRead' {0} {3} value='1' id='isRead_{1}' class='hidden'><label class='btn {4} btn-block btn-sm' for='isRead_{1}'>{2}</label></div>"
                                    , (boolIsRead) ? "checked" : ""
                                    , drAnswers[i]["TestQuestions_Id"]
                                    , (boolIsRead) ? "已阅" : "待阅"
                                    , (boolIsRead) ? "disabled='disabled'" : ""
                                    , (boolIsRead) ? "btn-success" : "btn-danger"
                                    );
                                //if (drQTConfirm.Length == 0) stbHtml.Append("<span class='full_mark'>满分</span>");
                                stbHtml.Append("</div>");
                                stbHtml.Append("</div>");

                                stbHtml.Append("<div class='panel_opera'>");
                                stbHtml.Append("<div class='opera_control'>");
                                stbHtml.AppendFormat("<span class=\"btn\" data-content=\"{0}\" id='aSpan{1}' {2}>{3}</span>"
                                    , (drAnswerScore.Length == 0 ? "" : drAnswerScore[0]["Comment"])
                                    , drAnswers[i]["TestQuestions_Num"]
                                    , "data-name=\"remark\""
                                    , "<i class='material-icons'>&#xE0C9;</i>&nbsp;文字批注");
                                if (Rc.Cloud.Web.Common.pfunction.GetWebMdlIsShow("RecordingComment"))
                                {
                                    stbHtml.AppendFormat("<span class=\"btn\" data-value=\"{0}\" {1}>{2}</span>"
                                        , drAnswers[i]["TestQuestions_Id"]
                                        , "data-name=\"comment_recording\""
                                        , "<i class='material-icons'>&#xE31D;</i>&nbsp;语音批注");
                                }
                                stbHtml.Append("</div>");
                                stbHtml.Append("</div>");

                                stbHtml.Append("</div>");
                            }
                        }
                        #endregion
                        #endregion

                        #region 综合题型 listBig

                        DataRow[] drTQBig = dt.Select("Parent_Id='0' ", "TestQuestions_Num");
                        foreach (var itemBig in drTQBig)
                        {
                            //题干
                            string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "testQuestionBody", itemBig["TestQuestions_Id"], "htm"));
                            strTestQuestionBody = pfunction.NoHTML(strTestQuestionBody);
                            if (string.IsNullOrEmpty(strTestQuestionBody))
                            {
                                strTestQuestionBody = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "textTitle", itemBig["TestQuestions_Id"], "txt"));
                            }
                            stbHtml.AppendFormat("<div class='question_type_panel'><div class='panel_heading'>{0}</div></div>", strTestQuestionBody);
                            #region 选择题、完形填空
                            DataRow[] drTQ_Choice = dt.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type in('selection','clozeTest') ", "TestQuestions_Num");
                            if (drTQ_Choice != null && drTQ_Choice.Length > 0)
                            {
                                stbHtml.AppendFormat("{0}"
                                    , dt.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type not in('selection','clozeTest') ").Length > 0 ? "<div class=\"question_type_panel\"><div class='panel_heading'>选择题</div></div>" : "");
                                stbHtml.Append("<div class='question_score_panel'>");
                                if (dtStuScoreBig.Rows.Count > 0)
                                {
                                    DataRow[] drStuScore = dtStuScoreBig.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestType in('selection','clozeTest')");
                                    if (drStuScore.Length == 1)
                                    {
                                        stbHtml.AppendFormat("<span class=\"total\">总分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                            , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                    }
                                }
                                stbHtml.Append("</div>");

                                stbHtml.Append("<div class=\"objective_question_panel clearfix\"><div class='panel_dt'><ul><li><span>题号</span><span>参考答案</span><span>学生答案</span><span>得分</span></li></ul></div><div class='panel_dd'><ul>");

                                foreach (var itemSub in drTQ_Choice)
                                {
                                    DataRow[] drTQS_Choice = dtAnswerScore.Select("TestType in('selection','clozeTest') and TestQuestions_Id='" + itemSub["TestQuestions_Id"] + "' ");
                                    foreach (var itemScore in drTQS_Choice)
                                    {
                                        stbHtml.AppendFormat("<li class=\"{0} {3}\"><span>{1}{2}</span>"
                                            , itemScore["TestQuestions_Num"]
                                            , itemScore["topicNumber"].ToString().TrimEnd('.')
                                            , string.IsNullOrEmpty(itemScore["testIndex"].ToString()) ? "" : "-" + itemScore["testIndex"].ToString()
                                            , itemScore["Student_Answer_Status"].ToString() == "right" ? "" : "error");
                                        stbHtml.AppendFormat("<span>{0}</span>", itemScore["TestCorrect"]);
                                        stbHtml.AppendFormat("<span>{0}</span>"
                                            , itemScore["Student_Answer"]);
                                        stbHtml.AppendFormat("<span>{0}<input type=\"hidden\" name=\"fill_Score\" value=\"{0}\"></span></li>", itemScore["Student_Score"]);
                                        //stbHtml.AppendFormat("<div class=\"hidden question_tit\"><i class=\"fen\">{0}</i></div>", itemScore["TestQuestions_Score"]);

                                    }
                                }
                                stbHtml.Append("</ul></div></div>");
                            }

                            #endregion
                            #region 判断题
                            DataRow[] drTQ_TrueFalse = dt.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type='truefalse' ", "TestQuestions_Num");
                            if (drTQ_TrueFalse != null && drTQ_TrueFalse.Length > 0)
                            {
                                stbHtml.AppendFormat("{0}"
                                    , dt.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type<>'truefalse' ").Length > 0 ? "<div class=\"question_type_panel\"><div class='panel_heading'>判断题</div></div>" : "");
                                stbHtml.Append("<div class=\"question_score_panel\">");
                                if (dtStuScoreBig.Rows.Count > 0)
                                {
                                    DataRow[] drStuScore = dtStuScoreBig.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestType in('truefalse')");
                                    if (drStuScore.Length == 1)
                                    {
                                        stbHtml.AppendFormat("<span class=\"total\">总分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                            , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                    }
                                }
                                stbHtml.Append("</div>");
                                stbHtml.Append("<div class=\"objective_question_panel clearfix\"><div class='panel_dt'><ul><li><span>题号</span><span>参考答案</span><span>学生答案</span><span>得分</span></li></ul></div><div class='panel_dd'><ul>");

                                foreach (var itemSub in drTQ_TrueFalse)
                                {
                                    DataRow[] drTQS_TrueFalse = dtAnswerScore.Select("TestType='truefalse' and TestQuestions_Id='" + itemSub["TestQuestions_Id"] + "' ");
                                    foreach (var itemScore in drTQS_TrueFalse)
                                    {
                                        stbHtml.AppendFormat("<li class=\"{0} {2}\"><span>{1}</span>"
                                            , itemScore["TestQuestions_Num"], itemScore["topicNumber"].ToString().TrimEnd('.')
                                            , itemScore["Student_Answer_Status"].ToString() == "right" ? "" : "error");
                                        stbHtml.AppendFormat("<span>{0}</span>", itemScore["TestCorrect"]);
                                        stbHtml.AppendFormat("<span>{0}</span>"
                                            , itemScore["Student_Answer"]);
                                        stbHtml.AppendFormat("<span>{0}<input type=\"hidden\" name=\"fill_Score\" value=\"{0}\"></span></li>", itemScore["Student_Score"]);
                                        //stbHtml.AppendFormat("<div class=\"hidden question_tit\"><i class=\"fen\">{0}</i></div>", itemScore["TestQuestions_Score"]);

                                    }
                                }
                                stbHtml.Append("</ul></div></div>");
                            }
                            #endregion
                            #region 填空题

                            DataRow[] drTQ_Fill = dt.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type='fill' ", "TestQuestions_Num");
                            if (drTQ_Fill != null && drTQ_Fill.Length > 0)
                            {
                                stbHtml.AppendFormat("{0}"
                                    , dt.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type<>'fill' ").Length > 0 ? "<div class=\"question_type_panel\"><div class='panel_heading'>填空题</div></div>" : "");
                                stbHtml.Append("<div class='question_score_panel'>");
                                if (dtStuScoreBig.Rows.Count > 0)
                                {
                                    DataRow[] drStuScore = dtStuScoreBig.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestType in('fill')");
                                    if (drStuScore.Length == 1)
                                    {
                                        stbHtml.AppendFormat("<span class=\"total\">总分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                            , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                    }
                                }
                                stbHtml.Append("</div>");
                                for (int i = 0; i < drTQ_Fill.Length; i++)
                                {
                                    #region 学生答题情况DataRow
                                    DataRow[] drAnswerScore = dtAnswerScore.Select("TestQuestions_Id='" + drTQ_Fill[i]["TestQuestions_Id"] + "'");
                                    #endregion

                                    //题干
                                    strTestQuestionBody = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "testQuestionBody", drTQ_Fill[i]["TestQuestions_Id"], "htm"));
                                    stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\" data-name='question_{0}'>", drTQ_Fill[i]["TestQuestions_Id"]);

                                    stbHtml.AppendFormat("<div class=\"panel_stem panel-stem-hook\" data-total-score='{1}'>{0}(分值：{1})</div>"
                                        , pfunction.NoHTML(strTestQuestionBody)
                                        , drTQ_Fill[i]["TestQuestions_SumScore"]);
                                    stbHtml.Append("<div class=\"panel_answer\">");
                                    //标题
                                    stbHtml.Append("<div class='answer_heading'>");
                                    stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                    stbHtml.Append("<div class='answer_item student_answer'><span class='btn btn-info btn-sm'>学生答案</span></div>");
                                    stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-block btn-sm'>得分</span></div>");
                                    stbHtml.Append("</div>");
                                    //答案&得分
                                    //是否需要批改（已阅true/待阅false）
                                    bool boolIsRead = true;
                                    for (int ii = 0; ii < drAnswerScore.Length; ii++)
                                    {
                                        if (boolIsRead && drAnswerScore[ii]["isRead"].ToString() == "0")
                                        {
                                            boolIsRead = false;
                                        }
                                        bool isJXZH = false; // 是否句型转换
                                        if (drAnswerScore.Length > 1 && pfunction.ConvertTQScore(drAnswerScore[1]["TestQuestions_Score"].ToString()) == "")
                                        {
                                            isJXZH = true;
                                        }
                                        stbHtml.Append("<div class='answer_body'>");
                                        //正确答案
                                        stbHtml.Append("<div class='answer_item reference_answer'>");
                                        //从文件读取正确答案
                                        string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "testQuestionCurrent", drAnswerScore[ii]["TestQuestions_Score_ID"], "txt"));
                                        if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                        {
                                            stbHtml.AppendFormat("<div>{0}{1}</div>"
                                                 , !string.IsNullOrEmpty(drAnswerScore[ii]["testIndex"].ToString()) ? "(" + drAnswerScore[ii]["testIndex"].ToString() + ")" : ""
                                                 , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));
                                        }
                                        stbHtml.Append("</div>");

                                        stbHtml.Append("<div class='answer_item student_answer'>");
                                        //学生答案 从文件读取
                                        if (!string.IsNullOrEmpty(drAnswerScore[ii]["Student_HomeWorkAnswer_Id"].ToString()))
                                        {
                                            string strstudentAnswer = pfunction.NoHTML(pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "studentAnswer", drAnswerScore[ii]["Student_HomeWorkAnswer_Id"], "txt")));
                                            stbHtml.AppendFormat("<div>{0}</div>", strstudentAnswer);
                                        }
                                        if (!isJXZH) //非句型转换
                                        {
                                            //分值列表
                                            stbHtml.AppendFormat("<div class='score_list_panel score-list-hook {0}'>"
                                            , (drAnswerScore[ii]["isRead"].ToString() == "1") ? "disabled" : "");
                                            double dTQScore = 0; // 试题分值
                                            double dStScore = 0; // 学生得分
                                            if (!string.IsNullOrEmpty(drAnswerScore[ii]["TestQuestions_Score"].ToString())) dTQScore = Convert.ToDouble(drAnswerScore[ii]["TestQuestions_Score"]);
                                            if (!string.IsNullOrEmpty(drAnswerScore[ii]["Student_Score"].ToString())) dStScore = Convert.ToDouble(drAnswerScore[ii]["Student_Score"]);
                                            double dRemainder = dTQScore % 1; // 余数
                                            stbHtml.Append("<div class='panel_heading'>给分</div>");
                                            stbHtml.Append("<div class='panel_body'>");
                                            for (int s = 0; s <= dTQScore; s++)
                                            {
                                                stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                    , s
                                                    , (dStScore == s) ? "active" : "");
                                            }
                                            if (dRemainder != 0)
                                            {
                                                stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                    , dTQScore
                                                    , (dStScore == dTQScore) ? "active" : "");
                                            }
                                            stbHtml.Append("</div>");
                                            stbHtml.Append("</div>");
                                        }
                                        else //句型转换
                                        {
                                            if (ii == drAnswerScore.Length - 1)
                                            {
                                                //分值列表
                                                stbHtml.AppendFormat("<div class='score_list_panel score-list-hook {0}'>"
                                                , (drAnswerScore[0]["isRead"].ToString() == "1") ? "disabled" : "");
                                                double dTQScore = 0; // 试题分值
                                                double dStScore = 0; // 学生得分
                                                if (!string.IsNullOrEmpty(drAnswerScore[ii]["TestQuestions_Score"].ToString())) dTQScore = Convert.ToDouble(drAnswerScore[ii]["TestQuestions_Score"]);
                                                if (!string.IsNullOrEmpty(drAnswerScore[ii]["Student_Score"].ToString())) dStScore = Convert.ToDouble(drAnswerScore[ii]["Student_Score"]);
                                                double dRemainder = dTQScore % 1; // 余数
                                                stbHtml.Append("<div class='panel_heading'>给分</div>");
                                                stbHtml.Append("<div class='panel_body'>");
                                                for (int s = 0; s <= dTQScore; s++)
                                                {
                                                    stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                        , s
                                                        , (dStScore == s) ? "active" : "");
                                                }
                                                if (dRemainder != 0)
                                                {
                                                    stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                        , dTQScore
                                                        , (dStScore == dTQScore) ? "active" : "");
                                                }
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");
                                            }
                                        }

                                        stbHtml.Append("</div>");
                                        //分值
                                        stbHtml.Append("<div class='answer_item score'>");
                                        correctCount++;
                                        if (pfunction.ConvertTQScore(drAnswerScore[ii]["TestQuestions_Score"].ToString()) != "")
                                        {
                                            stbHtml.AppendFormat("<div class='score_item' data-name='FillScore'><input type=\"text\" name=\"fill_Score\" class='form-control score_input input-sm' maxlength=\"4\" actualscore=\"{0}\" tqnum=\"{1}\" tqonum=\"{2}\" value=\"{3}\" {4} /></div>"
                                                , drAnswerScore[ii]["TestQuestions_Score"]
                                                , drTQ_Fill[i]["TestQuestions_Id"]
                                                , drAnswerScore[ii]["TestQuestions_OrderNum"]
                                                , drAnswerScore[ii]["Student_Score"]
                                                , (drAnswerScore[ii]["isRead"].ToString() == "1") ? "disabled='disabled'" : "");
                                        }
                                        stbHtml.Append("</div>");
                                        stbHtml.Append("</div>");
                                    }
                                    stbHtml.Append("<div class='answer_body'>");
                                    stbHtml.AppendFormat("<div class='answer_item score offset'><input type='checkbox' name='isRead' {0} {3} value='1' id='isRead_{1}' class='hidden'><label class='btn {4} btn-block btn-sm' for='isRead_{1}'>{2}</label></div>"
                                        , (boolIsRead) ? "checked" : ""
                                        , drTQ_Fill[i]["TestQuestions_Id"]
                                        , (boolIsRead) ? "已阅" : "待阅"
                                        , (boolIsRead) ? "disabled='disabled'" : ""
                                        , (boolIsRead) ? "btn-success" : "btn-danger"
                                        );
                                    //if (drQTConfirm.Length == 0) stbHtml.Append("<span class='full_mark'>满分</span>");
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");

                                    stbHtml.Append("<div class='panel_opera'>");
                                    stbHtml.Append("<div class='opera_control'>");
                                    stbHtml.AppendFormat("<span class=\"btn\" data-content=\"{0}\" id='aSpan{1}' {2}>{3}</span>"
                                        , (drAnswerScore.Length == 0 ? "" : drAnswerScore[0]["Comment"])
                                        , drTQ_Fill[i]["TestQuestions_Num"]
                                        , "data-name=\"remark\""
                                        , "<i class='material-icons'>&#xE0C9;</i>&nbsp;文字批注");
                                    if (Rc.Cloud.Web.Common.pfunction.GetWebMdlIsShow("RecordingComment"))
                                    {
                                        stbHtml.AppendFormat("<span class=\"btn\" data-value=\"{0}\" {1}>{2}</span>"
                                        , drTQ_Fill[i]["TestQuestions_Id"]
                                        , "data-name=\"comment_recording\""
                                        , "<i class='material-icons'>&#xE31D;</i>&nbsp;语音批注");
                                    }
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");

                                    stbHtml.Append("</div>");
                                }
                            }
                            #endregion
                            #region 简答题
                            DataRow[] drTQ_Answers = dt.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type='answers' ", "TestQuestions_Num");
                            if (drTQ_Answers.Length > 0)
                            {
                                stbHtml.AppendFormat("{0}"
                                    , dt.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type<>'answers' ").Length > 0 ? "<div class=\"question_type_panel\"><div class='panel_heading'>简答题</div></div>" : "");
                                stbHtml.Append("<div class='question_score_panel'>");
                                if (dtStuScoreBig.Rows.Count > 0)
                                {
                                    DataRow[] drStuScore = dtStuScoreBig.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestType in('answers')");
                                    if (drStuScore.Length == 1)
                                    {
                                        stbHtml.AppendFormat("<span class=\"total\">总分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                            , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                    }
                                }
                                stbHtml.Append("</div>");
                                for (int i = 0; i < drTQ_Answers.Length; i++)
                                {
                                    #region 学生答题情况DataRow
                                    DataRow[] drAnswerScore = dtAnswerScore.Select("TestQuestions_Id='" + drTQ_Answers[i]["TestQuestions_Id"] + "'");
                                    #endregion
                                    //题干
                                    strTestQuestionBody = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "testQuestionBody", drTQ_Answers[i]["TestQuestions_Id"], "htm"));
                                    stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\" data-name='question_{0}'>", drTQ_Answers[i]["TestQuestions_Id"]);

                                    stbHtml.AppendFormat("<div class=\"panel_stem panel-stem-hook\" data-total-score='{1}'>{0}(分值：{1})</div>", pfunction.NoHTML(strTestQuestionBody),
                                        drTQ_Answers[i]["TestQuestions_SumScore"]);
                                    stbHtml.Append("<div class=\"panel_answer\">");
                                    //标题
                                    stbHtml.Append("<div class='answer_heading'>");
                                    stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                    stbHtml.Append("<div class='answer_item student_answer'><span class='btn btn-info btn-sm'>学生答案</span></div>");
                                    stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-block btn-sm'>得分</span></div>");
                                    stbHtml.Append("</div>");
                                    //答案&得分
                                    //是否需要批改（已阅true/待阅false）
                                    bool boolIsRead = true;
                                    for (int ii = 0; ii < drAnswerScore.Length; ii++)
                                    {
                                        if (boolIsRead && drAnswerScore[ii]["isRead"].ToString() == "0")
                                        {
                                            boolIsRead = false;
                                        }
                                        stbHtml.Append("<div class='answer_body'>");
                                        //正确答案
                                        stbHtml.Append("<div class='answer_item reference_answer'>");
                                        //从文件读取正确答案
                                        string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "testQuestionCurrent", drAnswerScore[ii]["TestQuestions_Score_ID"], "txt"));
                                        if (!string.IsNullOrEmpty(strTestQuestionCurrent)) stbHtml.AppendFormat("<div>{0}</div>", pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));
                                        stbHtml.Append("</div>");
                                        //学生答案 从文件读取
                                        stbHtml.Append("<div class='answer_item student_answer'>");
                                        if (!string.IsNullOrEmpty(drAnswerScore[ii]["Student_HomeWorkAnswer_Id"].ToString()))
                                        {
                                            string strstudentAnswer = pfunction.NoHTML(pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "studentAnswer", drAnswerScore[ii]["Student_HomeWorkAnswer_Id"], "txt")));
                                            stbHtml.AppendFormat("<div>{0}</div>", strstudentAnswer);
                                        }

                                        if (pfunction.ConvertTQScore(drAnswerScore[ii]["TestQuestions_Score"].ToString()) != "")
                                        {
                                            //分值列表
                                            stbHtml.AppendFormat("<div class='score_list_panel score-list-hook {0}'>"
                                                , (drAnswerScore[ii]["isRead"].ToString() == "1") ? "disabled" : "");
                                            double dTQScore = 0; // 试题分值
                                            double dStScore = 0; // 学生得分
                                            if (!string.IsNullOrEmpty(drAnswerScore[ii]["TestQuestions_Score"].ToString())) dTQScore = Convert.ToDouble(drAnswerScore[ii]["TestQuestions_Score"]);
                                            if (!string.IsNullOrEmpty(drAnswerScore[ii]["Student_Score"].ToString())) dStScore = Convert.ToDouble(drAnswerScore[ii]["Student_Score"]);
                                            double dRemainder = dTQScore % 1; // 余数
                                            stbHtml.Append("<div class='panel_heading'>给分</div>");
                                            stbHtml.Append("<div class='panel_body'>");
                                            for (int s = 0; s <= dTQScore; s++)
                                            {
                                                stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                    , s
                                                    , (dStScore == s) ? "active" : "");
                                            }
                                            if (dRemainder != 0)
                                            {
                                                stbHtml.AppendFormat("<a href='javascript:;' class='{1}' data-val='{0}'>{0}</a>"
                                                    , dTQScore
                                                    , (dStScore == dTQScore) ? "active" : "");
                                            }
                                            stbHtml.Append("</div>");
                                            stbHtml.Append("</div>");
                                        }

                                        stbHtml.Append("</div>");
                                        //分值
                                        stbHtml.Append("<div class='answer_item score'>");
                                        correctCount++;
                                        if (pfunction.ConvertTQScore(drAnswerScore[ii]["TestQuestions_Score"].ToString()) != "")
                                        {
                                            stbHtml.AppendFormat("<div class='score_item' data-name='FillScore'><input type=\"text\" name=\"fill_Score\" class='form-control score_input input-sm' maxlength=\"4\" actualscore=\"{0}\" tqnum=\"{1}\" tqonum=\"{2}\" value=\"{3}\" {4} /></div>"
                                                , drAnswerScore[ii]["TestQuestions_Score"]
                                                , drTQ_Answers[i]["TestQuestions_Id"]
                                                , drAnswerScore[ii]["TestQuestions_OrderNum"]
                                                , drAnswerScore[ii]["Student_Score"]
                                                , (drAnswerScore[ii]["isRead"].ToString() == "1") ? "disabled='disabled'" : "");
                                        }
                                        stbHtml.Append("</div>");
                                        stbHtml.Append("</div>");
                                    }
                                    stbHtml.Append("<div class='answer_body'>");
                                    stbHtml.AppendFormat("<div class='answer_item score offset'><input type='checkbox' name='isRead' {0} {3} value='1' id='isRead_{1}' class='hidden'><label class='btn {4} btn-block btn-sm' for='isRead_{1}'>{2}</label></div>"
                                        , (boolIsRead) ? "checked" : ""
                                        , drTQ_Answers[i]["TestQuestions_Id"]
                                        , (boolIsRead) ? "已阅" : "待阅"
                                        , (boolIsRead) ? "disabled='disabled'" : ""
                                        , (boolIsRead) ? "btn-success" : "btn-danger"
                                        );
                                    //if (drQTConfirm.Length == 0) stbHtml.Append("<span class='full_mark'>满分</span>");
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");

                                    stbHtml.Append("<div class='panel_opera'>");
                                    stbHtml.Append("<div class='opera_control'>");
                                    stbHtml.AppendFormat("<span class=\"btn\" data-content=\"{0}\" id='aSpan{1}' {2}>{3}</span>"
                                        , (drAnswerScore.Length == 0 ? "" : drAnswerScore[0]["Comment"])
                                        , drTQ_Answers[i]["TestQuestions_Num"]
                                        , "data-name=\"remark\""
                                        , "<i class='material-icons'>&#xE0C9;</i>&nbsp;文字批注");
                                    if (Rc.Cloud.Web.Common.pfunction.GetWebMdlIsShow("RecordingComment"))
                                    {
                                        stbHtml.AppendFormat("<span class=\"btn\" data-value=\"{0}\" {1}>{2}</span>"
                                        , drTQ_Answers[i]["TestQuestions_Id"]
                                        , "data-name=\"comment_recording\""
                                        , "<i class='material-icons'>&#xE31D;</i>&nbsp;语音批注");
                                    }
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");

                                    stbHtml.Append("</div>");
                                }
                            }
                            #endregion
                        }
                        #endregion
                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        break;
                    case "testpaper_correct_client":
                        #region 作业批改-客户端批改 /teacher/correct_client.aspx
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        hwId = context.Request["hwId"].Filter();
                        stuHwId = context.Request["stuHwId"].Filter();
                        stuId = context.Request["stuId"].Filter();
                        hwCreateTime = context.Request["hwCreateTime"].Filter();

                        modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);

                        //生成存储路径
                        savePath = string.Empty;
                        saveOwnPath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                        }
                        fileTestPaperUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                        fileStudentAnswerUrl = uploadPath + "{0}\\" + pfunction.ToShortDate(hwCreateTime) + "\\" + stuHwId + "\\" + "{1}.{2}";//文件详细路径

                        //试题数据
                        DataTable dtTQ = new BLL_TestQuestions().GetList("ResourceToResourceFolder_Id='" + rtrfId + "' order by TestQuestions_Num ").Tables[0];
                        #region 普通题型 list 得分情况
                        strSql = string.Format(@"select t.TestType,isnull(SUM(t.TestQuestions_Score),0) as FullScore,isnull(SUM(t2.Student_Score),0) as StuScore from TestQuestions_Score t
inner join TestQuestions tq on tq.TestQuestions_Id=t.TestQuestions_Id and tq.Parent_Id=''
left join Student_HomeWorkAnswer t2 on t2.TestQuestions_Score_ID=t.TestQuestions_Score_ID and t2.Student_Id='{0}'
where t.ResourceToResourceFolder_Id='{1}'
group by t.TestType ", stuId, rtrfId);
                        dtStuScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        #endregion

                        #region 综合题型 listBig 得分情况
                        strSqlBig = string.Format(@"select tq.Parent_Id,t.TestType,isnull(SUM(t.TestQuestions_Score),0) as FullScore,isnull(SUM(t2.Student_Score),0) as StuScore from TestQuestions_Score t
inner join TestQuestions tq on tq.TestQuestions_Id=t.TestQuestions_Id and tq.Parent_Id<>'0' and tq.Parent_Id<>''
left join Student_HomeWorkAnswer t2 on t2.TestQuestions_Score_ID=t.TestQuestions_Score_ID and t2.Student_Id='{0}'
where t.ResourceToResourceFolder_Id='{1}'
group by tq.Parent_Id,t.TestType ", stuId, rtrfId);
                        dtStuScoreBig = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlBig).Tables[0];
                        #endregion
                        #region 普通题型 list
                        drFill = dtTQ.Select("Parent_Id='' and TestQuestions_Type='fill'", "TestQuestions_Num");
                        drAnswers = dtTQ.Select("Parent_Id='' and TestQuestions_Type='answers'", "TestQuestions_Num");
                        if (drFill.Length > 0)
                        {
                            stbHtml.Append("<div class=\"question_type_panel\"><div class='panel_heading'>填空题</div></div>");
                            stbHtml.Append("<div class='question_score_panel'>");
                            if (dtStuScore.Rows.Count > 0)
                            {
                                DataRow[] drCorrectScore = dtStuScore.Select("TestType='fill'");
                                if (drCorrectScore.Length == 1)
                                {
                                    stbHtml.AppendFormat("<span class=\"total_score\">总分：{0}</span>"
                                        , Convert.ToDecimal(drCorrectScore[0]["FullScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                        , Convert.ToDecimal(drCorrectScore[0]["StuScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                        , (Convert.ToDecimal(drCorrectScore[0]["FullScore"]) - Convert.ToDecimal(drCorrectScore[0]["StuScore"])).ToString("#0.##"));
                                }
                            }
                            stbHtml.Append("</div>");
                            for (int i = 0; i < drFill.Length; i++)
                            {
                                //批改详情
                                string strTQ_Correct = pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "teacherMarking", drFill[i]["TestQuestions_Id"], "txt"));
                                stbHtml.Append("<div class=\"question_panel question-panel-hook\">");
                                stbHtml.AppendFormat("<div class=\"panel_stem\"><img src=\"data:image/png;base64,{0}\" width='100%' /></div>", strTQ_Correct);
                                stbHtml.Append("</div>");

                            }
                        }

                        if (drAnswers.Length > 0)
                        {
                            stbHtml.Append("<div class=\"question_type_panel\"><div class='panel_heading'>简答题</div></div>");
                            stbHtml.Append("<div class='question_score_panel'>");
                            if (dtStuScore.Rows.Count > 0)
                            {
                                DataRow[] drCorrectScore = dtStuScore.Select("TestType='answers'");
                                if (drCorrectScore.Length == 1)
                                {
                                    stbHtml.AppendFormat("<span class=\"total_score\">总分：{0}</span>"
                                        , Convert.ToDecimal(drCorrectScore[0]["FullScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                        , Convert.ToDecimal(drCorrectScore[0]["StuScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                        , (Convert.ToDecimal(drCorrectScore[0]["FullScore"]) - Convert.ToDecimal(drCorrectScore[0]["StuScore"])).ToString("#0.##"));
                                }
                            }
                            stbHtml.Append("</div>");
                            for (int i = 0; i < drAnswers.Length; i++)
                            {
                                //批改详情
                                string strTQ_Correct = pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "teacherMarking", drAnswers[i]["TestQuestions_Id"], "txt"));
                                stbHtml.Append("<div class=\"question_panel question-panel-hook\">");
                                stbHtml.AppendFormat("<div class=\"panel_stem\"><img src=\"data:image/png;base64,{0}\" width='100%' /></div>", strTQ_Correct);
                                stbHtml.Append("</div>");
                            }
                        }
                        #endregion
                        strSqlAnswerFormat = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.TestType,tqs.testIndex 
,shwa.TestQuestions_NumStr,shwa.Student_HomeWorkAnswer_Id,shwa.Student_Answer,shwa.Student_Score,shwa.Student_Answer_Status,shwa.Comment,shwa.isRead 
,tq.topicNumber
from TestQuestions_Score tqs 
inner join TestQuestions tq on tq.TestQuestions_Id=TQS.TestQuestions_Id  
left join Student_HomeWorkAnswer shwa on TQS.TestQuestions_Id=SHWA.TestQuestions_Id and TQS.TestQuestions_Score_ID=SHWA.TestQuestions_Score_ID and shwa.Student_HomeWork_Id='" + stuHwId + "' where tqs.ResourceToResourceFolder_Id='" + rtrfId + "' order by tqs.TestQuestions_OrderNum";
                        dtAnswerScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlAnswerFormat).Tables[0];//学生答题情况

                        #region 综合题型 listBig
                        drTQBig = dtTQ.Select("Parent_Id='0'", "TestQuestions_Num");
                        foreach (var itemBig in drTQBig)
                        {
                            //题干
                            string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "testQuestionBody", itemBig["TestQuestions_Id"], "htm"));
                            strTestQuestionBody = pfunction.NoHTML(strTestQuestionBody);
                            if (string.IsNullOrEmpty(strTestQuestionBody))
                            {
                                strTestQuestionBody = pfunction.ReadAllText(string.Format(fileTestPaperUrl, saveOwnPath, "textTitle", itemBig["TestQuestions_Id"], "txt"));
                            }
                            stbHtml.AppendFormat("<div class=\"question_type_panel\"><div class='panel_heading'>{0}</div></div>", strTestQuestionBody);
                            #region 选择题、完形填空
                            DataRow[] drTQ_Choice = dtTQ.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type in('selection','clozeTest') ", "TestQuestions_Num");
                            if (drTQ_Choice != null && drTQ_Choice.Length > 0)
                            {
                                stbHtml.AppendFormat("<div class=\"question_type_panel\"><div class='panel_heading'>{0}</div></div><div class=\"question_score_panel\">"
                                    , dtTQ.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type not in('selection','clozeTest') ").Length > 0 ? "选择题" : "");
                                if (dtStuScoreBig.Rows.Count > 0)
                                {
                                    DataRow[] drStuScore = dtStuScoreBig.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestType in('selection','clozeTest')");
                                    if (drStuScore.Length == 1)
                                    {
                                        stbHtml.AppendFormat("<span class=\"total_score\">总分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                            , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                    }
                                }
                                stbHtml.Append("</div>");
                                stbHtml.Append("<div class=\"objective_question_panel\"><div class='panel_dt'><ul><li><span>题号</span><span>正确答案</span><span>学生答案</span><span>得分</span></li></ul></div><div class='panel_dd'><ul>");

                                foreach (var itemSub in drTQ_Choice)
                                {
                                    DataRow[] drTQS_Choice = dtAnswerScore.Select("TestType in('selection','clozeTest') and TestQuestions_Id='" + itemSub["TestQuestions_Id"] + "' ");
                                    foreach (var itemScore in drTQS_Choice)
                                    {
                                        //stbHtml.AppendFormat("<li class=\"{0}\"><p>{1}</p>", itemScore["TestQuestions_Num"], itemScore["TestQuestions_NumStr"]);
                                        stbHtml.AppendFormat("<li class=\"{0} {3}\"><span>{1}{2}</span>"
                                            , itemScore["TestQuestions_Num"]
                                            , itemScore["topicNumber"].ToString().TrimEnd('.')
                                            , string.IsNullOrEmpty(itemScore["testIndex"].ToString()) ? "" : "-" + itemScore["testIndex"].ToString()
                                            , itemScore["Student_Answer_Status"].ToString() == "" ? "" : "error");
                                        stbHtml.AppendFormat("<span>{0}</span>", itemScore["TestCorrect"]);
                                        stbHtml.AppendFormat("<span>{0}</span>"
                                            , itemScore["Student_Answer"]);
                                        stbHtml.AppendFormat("<span>{0}<input type=\"hidden\" name=\"fill_Score\" value=\"{0}\"></span></li>", itemScore["Student_Score"]);

                                    }
                                }
                                stbHtml.Append("</ul></div></div>");
                            }

                            #endregion
                            #region 判断题
                            DataRow[] drTQ_TrueFalse = dtTQ.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type='truefalse' ", "TestQuestions_Num");
                            if (drTQ_TrueFalse != null && drTQ_TrueFalse.Length > 0)
                            {
                                stbHtml.AppendFormat("<div class=\"question_type_panel\"><div class='panel_heading'>{0}</div></div><div class=\"question_score_panel\">"
                                    , dtTQ.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type<>'truefalse' ").Length > 0 ? "判断题" : "");
                                if (dtStuScoreBig.Rows.Count > 0)
                                {
                                    DataRow[] drStuScore = dtStuScoreBig.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestType in('truefalse')");
                                    if (drStuScore.Length == 1)
                                    {
                                        stbHtml.AppendFormat("<span class=\"total_score\">总分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                            , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                    }
                                }
                                stbHtml.Append("</div>");
                                stbHtml.Append("<div class=\"objective_question_panel\"><div class='panel_dl'><ul><li><p>题号</p><p>正确答案</p><p>学生答案</p><p>得分</p></li></ul></div><div class='panel_dd'><ul>");

                                foreach (var itemSub in drTQ_TrueFalse)
                                {
                                    DataRow[] drTQS_TrueFalse = dtAnswerScore.Select("TestType='truefalse' and TestQuestions_Id='" + itemSub["TestQuestions_Id"] + "' ");
                                    foreach (var itemScore in drTQS_TrueFalse)
                                    {
                                        stbHtml.AppendFormat("<li class=\"{0} {2}\"><p>{1}</p>"
                                            , itemScore["TestQuestions_Num"]
                                            , itemScore["TestQuestions_NumStr"]
                                            , itemScore["Student_Answer_Status"].ToString() == "right" ? "" : "class='error'");
                                        stbHtml.AppendFormat("<span>{0}</p>", itemScore["TestCorrect"]);
                                        stbHtml.AppendFormat("<span>{0}</span>"
                                            , itemScore["Student_Answer"]);
                                        stbHtml.AppendFormat("<span>{0}<input type=\"hidden\" name=\"fill_Score\" value=\"{0}\"></span></li>", itemScore["Student_Score"]);

                                    }
                                }
                                stbHtml.Append("</ul></div></div>");
                            }
                            #endregion
                            #region 填空题
                            DataRow[] drTQ_Fill = dtTQ.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type='fill' ", "TestQuestions_Num");
                            if (drTQ_Fill.Length > 0)
                            {
                                stbHtml.AppendFormat("<div class=\"question_type_panel\"><div class='panel_heading'>{0}</div></div>"
                                    , dtTQ.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type<>'fill' ").Length > 0 ? "填空题" : "");
                                stbHtml.Append("<div class='question_score_panel'>");
                                if (dtStuScoreBig.Rows.Count > 0)
                                {
                                    DataRow[] drStuScore = dtStuScoreBig.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestType in('fill')");
                                    if (drStuScore.Length == 1)
                                    {
                                        stbHtml.AppendFormat("<span class=\"total_score\">总分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                            , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                    }
                                }
                                stbHtml.Append("</div>");
                                for (int i = 0; i < drTQ_Fill.Length; i++)
                                {
                                    //批改详情
                                    string strTQ_Correct = pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "teacherMarking", drTQ_Fill[i]["TestQuestions_Id"], "txt"));
                                    stbHtml.Append("<div class=\"question_panel question-panel-hook\">");
                                    stbHtml.AppendFormat("<div class=\"panel_stem\"><img src=\"data:image/png;base64,{0}\" width='100%' /></div>", strTQ_Correct);
                                    stbHtml.Append("</div>");

                                }
                            }
                            #endregion
                            #region 简答题
                            DataRow[] drTQ_Answers = dtTQ.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type='answers'", "TestQuestions_Num");
                            if (drTQ_Answers.Length > 0)
                            {
                                stbHtml.AppendFormat("<div class=\"question_type_panel\"><div class='panel_heading'>{0}</div></div>"
                                    , dtTQ.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestQuestions_Type<>'answers' ").Length > 0 ? "简答题" : "");
                                stbHtml.Append("<div class='question_score_panel'>");
                                if (dtStuScoreBig.Rows.Count > 0)
                                {
                                    DataRow[] drStuScore = dtStuScoreBig.Select("Parent_Id='" + itemBig["TestQuestions_Id"] + "' and TestType in('answers')");
                                    if (drStuScore.Length == 1)
                                    {
                                        stbHtml.AppendFormat("<span class=\"total_score\">总分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                            , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                        stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                            , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                    }
                                }
                                stbHtml.Append("</div>");
                                for (int i = 0; i < drTQ_Answers.Length; i++)
                                {
                                    //批改详情
                                    string strTQ_Correct = pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "teacherMarking", drTQ_Answers[i]["TestQuestions_Id"], "txt"));
                                    stbHtml.Append("<div class=\"question_panel question-panel-hook\">");
                                    stbHtml.AppendFormat("<div class=\"panel_stem\"><img src=\"data:image/png;base64,{0}\" width='100%' /></div>", strTQ_Correct);
                                    stbHtml.Append("</div>");
                                }
                            }
                            #endregion
                        }
                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        #endregion
                        break;
                    case "testpaper_comment":
                        #region 作业讲评 /Evaluation/CommentReport.aspx
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        hwId = context.Request["hwId"].Filter();

                        dtTQ = new DataTable();//试题数据
                        DataTable dtTQ_StudentAnswer = new DataTable();//学生答题情况
                        DataTable dtTQ_StudentAnswerNew = new DataTable();//学生答题情况单选或者判断
                        DataTable dtTQ_StudentAnswerNewCloze = new DataTable();//学生答题情况 完形填空
                        strSql = string.Format(@"select *
,StudentAvgScoreRateStyle=(case when StudentAvgScoreRate<60 then 'btn-danger' when StudentAvgScoreRate<70 then 'btn-warning'  when StudentAvgScoreRate<85 then 'btn-info' else 'btn-success' end)
from (select *
,StudentAvgScoreRate=(case when TestQuestions_SumScore=0 or StudentCount=0 then 0 else CONVERT(numeric(8,2),StudentSumScore/StudentCount/TestQuestions_SumScore*100) end) 
from (
select tq.TestQuestions_Id,tq.ResourceToResourceFolder_Id,tq.TestQuestions_Num,tq.TestQuestions_Type,tq.TestQuestions_SumScore,tq.topicNumber 
,StudentSumScore=(select ISNULL(SUM(Student_Score),0) from Student_HomeWorkAnswer where TestQuestions_Id=tq.TestQuestions_Id and HomeWork_Id='{0}')
,StudentCount=(select Count(1) from ((select distinct Student_Id from Student_HomeWorkAnswer where TestQuestions_Id=tq.TestQuestions_Id and HomeWork_Id='{0}'))t) 
from TestQuestions tq
where tq.ResourceToResourceFolder_Id='{1}'
) t ) temp order by TestQuestions_Num", hwId, rtrfId);
                        dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        strSql = string.Format(@"select *
            ,StudentAnswerStatus=(case when Student_Score=TestQuestions_SumScore then 2 when Student_Score=0 then 0 else 1 end)
            ,StudentAnswerStatusStyle=(case when Student_Score=TestQuestions_SumScore then 'btn_success' when Student_Score=0 then 'btn_danger' else 'btn_info' end)
            from (
            select shwa.TestQuestions_Id,shwa.Student_Id,shwa.HomeWork_Id,ISNULL(SUM(shwa.Student_Score),0) as Student_Score
            ,ISNULL(MAX(tq.TestQuestions_SumScore),0) as TestQuestions_SumScore,fu.UserName,fu.TrueName
            from Student_HomeWorkAnswer shwa
            inner join TestQuestions tq on shwa.TestQuestions_Id=tq.TestQuestions_Id
            left join F_User fu on fu.UserId=shwa.Student_Id
            where HomeWork_Id='{0}'
            group by shwa.TestQuestions_Id,shwa.Student_Id,shwa.HomeWork_Id,fu.UserName,fu.TrueName
            ) t order by StudentAnswerStatus,Student_Score", hwId);
                        dtTQ_StudentAnswer = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        strSql = string.Format(@"select * from StatsStudentAnswerQuestions where HomeWork_Id='{0}' ", hwId);
                        dtTQ_StudentAnswerNew = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        strSql = string.Format(@"select t.*,tqs.testIndex,tqs.TestCorrect from StatsStudentAnswerQuestions t 
            left join TestQuestions_Score tqs on t.TestQuestions_Score_Id=tqs.TestQuestions_Score_Id  where HomeWork_Id='{0}' ", hwId);
                        dtTQ_StudentAnswerNewCloze = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        #region 给全局变量赋值
                        strSql = string.Format(@"select TestQuestions_Id,TestQuestions_Score_ID,Student_Answer_Status,COUNT(1) as ICOUNT from Student_HomeWorkAnswer
where HomeWork_Id='{0}' group by TestQuestions_Id,TestQuestions_Score_ID,Student_Answer_Status", hwId);
                        dtTQ_StudentAnswerCount = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        listHW_TQ = new BLL_StatsClassHW_TQ().GetModelList("ResourceToResourceFolder_Id='" + rtrfId + "' and HomeWork_Id='" + hwId + "' ");
                        strSql = @"select  shwa.TestQuestions_Score_ID,avg(Student_Score) as vagSutScore,max(tqs.TestQuestions_Score) as tqsScore from [dbo].[Student_HomeWorkAnswer] shwa
inner join [dbo].[TestQuestions_Score] tqs on tqs.TestQuestions_Score_ID=shwa.TestQuestions_Score_ID
 where shwa.HomeWork_Id='" + hwId + "' group by shwa.TestQuestions_Score_ID";
                        dtTQScore_CLassVAG = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        #endregion
                        string strSqlAnswerScore = string.Format(@"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum
,tqs.TargetText,tqs.testIndex
,(case when kpb.KPNameBasic is null then tqs.ContentText else kpb.KPNameBasic end) as ContentText
from TestQuestions_Score tqs 
left join S_TestQuestions_KP tqkp on tqkp.TestQuestions_Score_ID=tqs.TestQuestions_Score_ID
left join S_KnowledgePoint kp on kp.S_KnowledgePoint_Id=tqkp.S_KnowledgePoint_Id
left join S_KnowledgePointBasic kpb on kpb.S_KnowledgePointBasic_Id=kp.S_KnowledgePointBasic_Id 
where tqs.ResourceToResourceFolder_Id='{0}' order by tqs.TestQuestions_OrderNum", rtrfId);
                        dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlAnswerScore).Tables[0];

                        modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);

                        //生成存储路径
                        savePath = string.Empty;
                        saveOwnPath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                        }
                        fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                        fileStudentAnswerUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//学生答案详细路径
                        DataRow[] dtTQRow = dtTQ.Select("", "TestQuestions_Num");
                        foreach (DataRow item in dtTQRow)
                        {
                            DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + item["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                            if (drScore.Length > 0)
                            {
                                //题干
                                string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", item["TestQuestions_Id"], "htm"));
                                //选择题、完形填空题选项
                                string strOption = string.Empty;
                                if (item["TestQuestions_Type"].ToString() == "selection" || item["TestQuestions_Type"].ToString() == "clozeTest")
                                {
                                    for (int ii = 0; ii < drScore.Length; ii++)
                                    {
                                        //从文件读取选择题选项
                                        string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                        List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                        if (listTestSelections != null && listTestSelections.Count > 0)
                                        {
                                            foreach (var itemSelections in listTestSelections)
                                            {
                                                if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                            }
                                            if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                            {
                                                strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                            }
                                        }
                                    }
                                }
                                stbHtml.AppendFormat("<div class=\"question_panel\" data-name=\"question_{0}\">", item["TestQuestions_Num"]);
                                stbHtml.AppendFormat("<div class=\"panel_stem\" onclick='enlargeTBody(this);' title='点击放大'>{0}(分值：<i class=\"fen\">{1}</i>)</div><div class=\"panel_option\">{2}</div>"
                                    , pfunction.NoHTML(strTestQuestionBody)
                                    , item["TestQuestions_SumScore"].ToString().clearLastZero()
                                    , strOption);

                                stbHtml.Append("<div class=\"panel_opera panel-opera-hook\">");
                                stbHtml.Append("<div class=\"opera_control\">");
                                stbHtml.AppendFormat("<span class=\"btn\" onclick=\"PicPreview('../student/questionAttrAll.aspx?resourceid={0}&questionid={1}&attrType=AnalyzeHtml')\"><i class=\"material-icons\">&#xE8B0;</i>&nbsp;解析</span>"
                                    , rtrfId
                                    , item["TestQuestions_Id"]);
                                stbHtml.AppendFormat("<span class=\"btn attr-switch-hook\">展开&nbsp;<i class=\"material-icons\">&#xE313;</i></span>");
                                stbHtml.Append("</div>");
                                stbHtml.Append("</div>");

                                stbHtml.Append("<div class=\"panel_other panel-other-hook panel_other_mini\" data-name=\"commenting\">");
                                //stbHtml.Append("<div class=\"clearfix\">");
                                //stbHtml.Append("<div class=\"commenting_box\" data-name=\"commenting_box\">");

                                string strAvgScore = string.Empty;//平均得分 
                                string strContentText = string.Empty;//知识内容
                                string strTargetText = string.Empty;//测量目标
                                string strCorrectAnswer = string.Empty;//正确答案
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    if (pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()) != "")
                                    {
                                        strAvgScore += string.Format("<dd>{0}{1}</dd>"
                                             , (item["TestQuestions_Type"].ToString() == "clozeTest" || item["TestQuestions_Type"].ToString() == "fill") && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("（" + drScore[ii]["testIndex"].ToString() + "）") : ""
                                             , !string.IsNullOrEmpty(GetAvgScore(drScore[ii]["TestQuestions_Score_ID"].ToString())) ? GetAvgScore(drScore[ii]["TestQuestions_Score_ID"].ToString()) : "-");
                                        strContentText += string.Format("<dd>{0}{1}</dd>"
                                            , (item["TestQuestions_Type"].ToString() == "clozeTest" || item["TestQuestions_Type"].ToString() == "fill") && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("（" + drScore[ii]["testIndex"].ToString() + "）") : ""
                                            , !string.IsNullOrEmpty(drScore[ii]["ContentText"].ToString()) ? drScore[ii]["ContentText"].ToString() : "-");
                                        strTargetText += string.Format("<dd>{0}{1}</dd>"
                                            , (item["TestQuestions_Type"].ToString() == "clozeTest" || item["TestQuestions_Type"].ToString() == "fill") && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("（" + drScore[ii]["testIndex"].ToString() + "）") : ""
                                            , !string.IsNullOrEmpty(drScore[ii]["TargetText"].ToString()) ? drScore[ii]["TargetText"].ToString() : "-");
                                    }
                                    if (item["TestQuestions_Type"].ToString() == "selection" || item["TestQuestions_Type"].ToString() == "clozeTest" || item["TestQuestions_Type"].ToString() == "truefalse")
                                    {
                                        strCorrectAnswer += string.Format("<dd>{0}{1}</dd>"
                                            , item["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("（" + drScore[ii]["testIndex"].ToString() + "）") : ""
                                            , !string.IsNullOrEmpty(drScore[ii]["TestCorrect"].ToString()) ? drScore[ii]["TestCorrect"] : "-");
                                    }
                                    else if (item["TestQuestions_Type"].ToString() == "fill" || item["TestQuestions_Type"].ToString() == "answers")
                                    {
                                        //从文件读取正确答案
                                        string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                        if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                        {
                                            strCorrectAnswer += string.Format("<dd>{0}{1}</dd>"
                                                , item["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                , !string.IsNullOrEmpty(pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或")) ? pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或") : "-");
                                        }
                                        else
                                        {
                                            strCorrectAnswer += "<dd>-</dd>";
                                        }
                                    }
                                }
                                stbHtml.AppendFormat("<dl class='other_attr'>");
                                stbHtml.AppendFormat("<dt>平均得分：</dt>{0}", strAvgScore);
                                stbHtml.AppendFormat("<dt>知识内容：</dt>{0}", strContentText);
                                stbHtml.AppendFormat("<dt>测量目标：</dt>{0}", strTargetText);
                                stbHtml.AppendFormat("<dt>答案：</dt>{0}", strCorrectAnswer);


                                DataRow[] drStudentAnswer = null;
                                DataRow[] drStudentAnswerCheck = null;
                                if (item["TestQuestions_Type"].ToString() == "selection" || item["TestQuestions_Type"].ToString() == "truefalse")
                                {
                                    stbHtml.Append("<dt>答题情况：</dt><dd class='anwering'>");
                                    //选择题，填空题打对的学生不显示
                                    drStudentAnswerCheck = dtTQ_StudentAnswerNew.Select("TestQuestions_Id='" + item["TestQuestions_ID"] + "'", "TQOption");
                                }
                                else if (item["TestQuestions_Type"].ToString() == "clozeTest")
                                {
                                    stbHtml.Append("<dt>答题情况：</dt><dd class='anwering'>");
                                    //选择题，填空题打对的学生不显示
                                    drStudentAnswerCheck = dtTQ_StudentAnswerNewCloze.Select("TestQuestions_Id='" + item["TestQuestions_ID"] + "'", "TQOption");
                                }
                                else if (item["TestQuestions_Type"].ToString() == "fill" || item["TestQuestions_Type"].ToString() == "answers")
                                {
                                    stbHtml.AppendFormat("<dt>答题情况：</dt><dd class='anwering'>");
                                    drStudentAnswer = dtTQ_StudentAnswer.Select("TestQuestions_Id='" + item["TestQuestions_ID"] + "'");
                                }
                                #region 选择题 答题情况
                                if (item["TestQuestions_Type"].ToString() == "selection" && drStudentAnswerCheck != null && drStudentAnswerCheck.Length > 0)
                                {
                                    stbHtml.AppendFormat("<div class='anwering_choice'>");
                                    foreach (DataRow itemStuAnswer in drStudentAnswerCheck)
                                    {
                                        stbHtml.AppendFormat("<div class=\"progress_panel clearfix {0}\">",
                                            itemStuAnswer["TQOption"].ToString() == strCorrectAnswer.Replace("<dd>", "").Replace("</dd>", "").Trim() ? "right_answer" : "");
                                        stbHtml.AppendFormat("<div class=\"panel_heading\">{0}</div>"
                                            , itemStuAnswer["TQOption"].ToString() != "未作答" && itemStuAnswer["TQOption"].ToString() != "其他" ? "选项 " + itemStuAnswer["TQOption"].ToString() : itemStuAnswer["TQOption"].ToString());
                                        stbHtml.AppendFormat("<div class=\"panel_body\">");
                                        stbHtml.AppendFormat("<div class=\"progress\">");
                                        stbHtml.AppendFormat("<div class=\"progress-bar\" style=\"width:{0}%\">"
                                            , itemStuAnswer["AnswerCountRate"].ToString().clearLastZero());
                                        stbHtml.AppendFormat("</div>");
                                        stbHtml.AppendFormat("</div>");
                                        stbHtml.AppendFormat("</div>");
                                        stbHtml.AppendFormat("<div class=\"panel_footer\" data-toggle='popover' data-content='{2}'>{0} 人作答 · 占比 {1}%</div>"
                                            , itemStuAnswer["AnswerCount"]
                                            , itemStuAnswer["AnswerCountRate"].ToString().clearLastZero()
                                            , itemStuAnswer["AnswerStudentName"].ToString());
                                        stbHtml.AppendFormat("</div>");
                                    }
                                    stbHtml.AppendFormat("</div>");
                                }
                                #endregion
                                #region 完形填空 答题情况
                                if (item["TestQuestions_Type"].ToString() == "clozeTest" && drStudentAnswerCheck != null && drStudentAnswerCheck.Length > 0)
                                {
                                    DataTable dtClozeTest = ToDataTable(drStudentAnswerCheck);
                                    DataView dataView = dtClozeTest.DefaultView;
                                    DataTable dtDistinct = dataView.ToTable(true, "TestQuestions_Score_Id", "testIndex");
                                    DataRow[] drClozeBig = dtDistinct.Select("", "testIndex");
                                    stbHtml.AppendFormat("<div class='anwering_choice'>");
                                    foreach (DataRow itemCloze in drClozeBig)
                                    {
                                        stbHtml.Append("<div class='cloze_test_panel'>");
                                        DataRow[] drCloze = dtTQ_StudentAnswerNewCloze.Select("TestQuestions_Score_Id='" + itemCloze["TestQuestions_Score_Id"] + "'", "TQOption");
                                        stbHtml.AppendFormat("<div class='panel_left'>{0}</div>", !string.IsNullOrEmpty(itemCloze["testIndex"].ToString()) ? ("(" + itemCloze["testIndex"].ToString() + ")") : "");
                                        stbHtml.Append("<div class='panel_content'>");
                                        foreach (DataRow itemStuAnswer in drCloze)
                                        {
                                            stbHtml.AppendFormat("<div class=\"progress_panel clearfix {0}\">",
                                            itemStuAnswer["TQOption"].ToString() == strCorrectAnswer.Replace("<dd>", "").Replace("</dd>", "").Trim() ? "right_answer" : "");
                                            stbHtml.AppendFormat("<div class=\"panel_heading\">{0}</div>"
                                                , itemStuAnswer["TQOption"].ToString() != "未作答" ? "选项 " + itemStuAnswer["TQOption"].ToString() : itemStuAnswer["TQOption"].ToString());
                                            stbHtml.AppendFormat("<div class=\"panel_body\">");
                                            stbHtml.AppendFormat("<div class=\"progress\">");
                                            stbHtml.AppendFormat("<div class=\"progress-bar\" style=\"width:{0}%\">"
                                                , itemStuAnswer["AnswerCountRate"].ToString().clearLastZero());
                                            stbHtml.AppendFormat("</div>");
                                            stbHtml.AppendFormat("</div>");
                                            stbHtml.AppendFormat("</div>");
                                            stbHtml.AppendFormat("<div class=\"panel_footer\" data-toggle='popover' data-content='{2}'>{0} 人作答 · 占比 {1}%</div>"
                                                , itemStuAnswer["AnswerCount"]
                                                , itemStuAnswer["AnswerCountRate"].ToString().clearLastZero()
                                                , itemStuAnswer["AnswerStudentName"].ToString());
                                            stbHtml.AppendFormat("</div>");
                                        }
                                        stbHtml.Append("</div>");
                                        stbHtml.Append("</div>");
                                    }
                                    stbHtml.Append("</div>");
                                    //stbHtml.Append("<div class='desc text-warning'>*划过柱状图，可查看学生名单</div>");
                                }
                                #endregion
                                #region 判断题 答题情况
                                if (item["TestQuestions_Type"].ToString() == "truefalse" && drStudentAnswerCheck != null && drStudentAnswerCheck.Length > 0)
                                {
                                    stbHtml.AppendFormat("<div class='anwering_choice'>");
                                    foreach (DataRow itemStuAnswer in drStudentAnswerCheck)
                                    {
                                        stbHtml.AppendFormat("<div class=\"progress_panel clearfix {0}\">",
                                            itemStuAnswer["TQOption"].ToString() == strCorrectAnswer.Replace("<dd>", "").Replace("</dd>", "").Trim() ? "right_answer" : "");
                                        stbHtml.AppendFormat("<div class=\"panel_heading\">{0}</div>"
                                            , itemStuAnswer["TQOption"].ToString() != "未作答" ? "选项" + itemStuAnswer["TQOption"].ToString() : itemStuAnswer["TQOption"].ToString());
                                        stbHtml.AppendFormat("<div class=\"panel_body\">");
                                        stbHtml.AppendFormat("<div class=\"progress\">");
                                        stbHtml.AppendFormat("<div class=\"progress-bar\" style=\"width:{0}%\">"
                                            , itemStuAnswer["AnswerCountRate"].ToString().clearLastZero());
                                        stbHtml.AppendFormat("</div>");
                                        stbHtml.AppendFormat("</div>");
                                        stbHtml.AppendFormat("</div>");
                                        stbHtml.AppendFormat("<div class=\"panel_footer\" data-toggle='popover' data-content='{2}'>{0} 人作答 · 占比 {1}%</div>"
                                            , itemStuAnswer["AnswerCount"]
                                            , itemStuAnswer["AnswerCountRate"].ToString().clearLastZero()
                                            , itemStuAnswer["AnswerStudentName"].ToString());
                                        stbHtml.AppendFormat("</div>");
                                    }
                                    stbHtml.AppendFormat("</div>");
                                    //stbHtml.Append("<div class='desc'>*划过柱状图，可查看学生名单</div>");
                                }
                                #endregion

                                if (drStudentAnswer != null && drStudentAnswer.Length > 0)
                                {
                                    stbHtml.AppendFormat("<ul class=\"anwering_short\">");
                                    foreach (DataRow itemStuAnswer in drStudentAnswer)
                                    {
                                        stbHtml.AppendFormat("<li class=\"{4}\"><input type=\"checkbox\" value=\"{2}\" id=\"{1}{2}\"><label for=\"{1}{2}\"></label><span onclick=\"AnswerStatus('CommentReportStudentAnswer.aspx?ResourceToResourceFolder_Id={0}&TestQuestions_Id={1}&Student_Id={2}&Homework_Id={6}','{3}的答题情况')\">{3}({5})</span></li>"
                                            , rtrfId
                                            , itemStuAnswer["TestQuestions_ID"]
                                            , itemStuAnswer["Student_Id"]
                                            , string.IsNullOrEmpty(itemStuAnswer["TrueName"].ToString()) ? itemStuAnswer["UserName"] : itemStuAnswer["TrueName"]
                                            , itemStuAnswer["StudentAnswerStatusStyle"]
                                            , itemStuAnswer["Student_Score"].ToString().clearLastZero()
                                            , itemStuAnswer["HomeWork_Id"]);
                                    }
                                    stbHtml.AppendFormat("<li class=\"btn_contrast\" data-name='contrast' data-value='{0},{1},{2}'>对比答案</li>"
                                        , rtrfId
                                        , hwId
                                        , item["TestQuestions_Id"]);
                                    stbHtml.AppendFormat("</ul>");
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</dd>");
                                    stbHtml.Append("</dl>");
                                }
                                stbHtml.AppendFormat("</dd></dl>");
                                stbHtml.Append("</div>");
                                stbHtml.Append("</div>");
                            }
                            else
                            {//题干
                                string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", item["TestQuestions_Id"], "htm"));
                                stbHtml.AppendFormat("<div class=\"question_type_panel\" data-name=\"question_{0}\">", item["TestQuestions_Num"]);
                                stbHtml.AppendFormat("<div class='panel_heading' onclick='enlargeTBody(this);' title='点击放大'>{0}</div></div>"
                                    , pfunction.NoHTML(strTestQuestionBody));
                            }
                        }
                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        break;
                    case "teacher_correct_web":
                        #region 老师web端批改作业 /teacher/correctT.aspx
                        string correctData = context.Request["correctData"];
                        stuHwId = context.Request["stuHwId"].Filter();
                        tchId = context.Request["tchId"].Filter();
                        try
                        {
                            BLL_Student_HomeWorkAnswer bll = new BLL_Student_HomeWorkAnswer();
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            List<StudentAnswerData> listStuAnswer = json.Deserialize<List<StudentAnswerData>>(correctData);
                            List<Model_Student_HomeWorkAnswer> listSHWA = new List<Model_Student_HomeWorkAnswer>();

                            Model_Student_HomeWork_Correct modelSHWCorrect = new BLL_Student_HomeWork_Correct().GetModel(stuHwId);
                            Model_Student_HomeWork model_SHW = new BLL_Student_HomeWork().GetModel(stuHwId);
                            modelSHWCorrect.Student_HomeWork_CorrectStatus = 1;
                            modelSHWCorrect.CorrectTime = DateTime.Now;
                            modelSHWCorrect.CorrectMode = "0";
                            modelSHWCorrect.CorrectUser = tchId;
                            Rc.Common.SystemLog.SystemLog.AddLogFromBS(stuHwId, "", string.Format("开始处理批改数据|操作人{0}|学生作业Id{1}|方法{2}", tchId, stuHwId, "TeacherCorrect"));
                            List<Model_Student_HomeWorkAnswer> listDataSHWA = new BLL_Student_HomeWorkAnswer().GetModelList("Student_HomeWork_Id='" + stuHwId + "' ");
                            foreach (var item in listStuAnswer)
                            {
                                List<Model_Student_HomeWorkAnswer> listWhere = listDataSHWA.Where(w => w.TestQuestions_Id == item.testquestions_Id && w.TestQuestions_Detail_OrderNum == item.tqonum).ToList();
                                #region 学生答题表
                                Model_Student_HomeWorkAnswer model = new Model_Student_HomeWorkAnswer();
                                if (listWhere.Count == 0)
                                {
                                    context.Response.Write("0");
                                    context.Response.End();
                                }
                                else
                                {
                                    model = listWhere[0];
                                    model.Comment = item.comment.Filter();
                                    model.Student_Score = item.score;
                                    if (item.score == item.actualscore)
                                    {
                                        model.Student_Answer_Status = "right";//对
                                    }
                                    else if (item.score == 0)
                                    {
                                        model.Student_Answer_Status = "wrong";//错
                                    }
                                    else
                                    {
                                        model.Student_Answer_Status = "partright";//部分对
                                    }
                                    if (!string.IsNullOrEmpty(item.isRead.ToString()))
                                    {
                                        model.isRead = int.Parse(item.isRead);
                                    }
                                    else
                                    {
                                        model.isRead = 0;
                                    }

                                }
                                listSHWA.Add(model);
                                #endregion
                            }

                            #region 更新批改时间，统计帮助表
                            DataTable dtHWDetail = new BLL_HomeWork().GetHWDetail(model_SHW.HomeWork_Id).Tables[0];
                            Model_StatsHelper modelSH_HW = new Model_StatsHelper();
                            modelSH_HW.StatsHelper_Id = Guid.NewGuid().ToString();
                            modelSH_HW.ResourceToResourceFolder_Id = dtHWDetail.Rows[0]["ResourceToResourceFolder_Id"].ToString();
                            modelSH_HW.Homework_Id = model_SHW.HomeWork_Id;
                            modelSH_HW.Correct_Time = DateTime.Now;
                            modelSH_HW.Exec_Status = "0";
                            modelSH_HW.SType = "1";
                            modelSH_HW.CreateUser = tchId;
                            modelSH_HW.SchoolId = dtHWDetail.Rows[0]["SchoolId"].ToString();
                            modelSH_HW.GradeId = dtHWDetail.Rows[0]["GradeId"].ToString();
                            #endregion
                            int result = bll.TeacherCorrectStuHomeWork(modelSHWCorrect, listSHWA, modelSH_HW);

                            if (result > 0)
                            {
                                try
                                {
                                    #region 修改学生答题文件，保存分值
                                    Model_HomeWork modelHW_TC = new BLL_HomeWork().GetModel(model_SHW.HomeWork_Id);
                                    fileUrl = uploadPath + "{0}\\{1}\\{2}\\{2}.txt";
                                    //读取学生答题信息
                                    string stuInfo = pfunction.ReadAllText(string.Format(fileUrl, "studentAnswerForMarking", pfunction.ToShortDate(modelHW_TC.CreateTime.ToString()), stuHwId));

                                    List<Model_Student_HomeWorkAnswer> listStuScore = new BLL_Student_HomeWorkAnswer().GetModelList("Student_HomeWork_Id='" + stuHwId + "'");
                                    try
                                    {
                                        StuAnswerForMarkingModel modelStuAnswer = new StuAnswerForMarkingModel();
                                        modelStuAnswer = JsonConvert.DeserializeObject<StuAnswerForMarkingModel>(stuInfo);
                                        #region 普通题型 list
                                        if (modelStuAnswer.list != null)
                                        {
                                            foreach (TestPaperAnswerModel item in modelStuAnswer.list)
                                            {
                                                if (item != null && item.list != null)
                                                {
                                                    int sNum = 0;
                                                    foreach (var itemScore in item.list)
                                                    {
                                                        sNum++;
                                                        List<Model_Student_HomeWorkAnswer> listStuScoreSub = listStuScore.Where(o => o.TestQuestions_Id == item.Testid && o.TestQuestions_Detail_OrderNum == sNum).ToList();
                                                        if (listStuScoreSub.Count > 0) itemScore.studentScore = listStuScoreSub[0].Student_Score.ToString();

                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        #region 综合题型 listBig
                                        if (modelStuAnswer.listBig != null)
                                        {
                                            foreach (TestPaperAnswerModelBig itemBig in modelStuAnswer.listBig)
                                            {
                                                if (itemBig != null && itemBig.list != null)
                                                {
                                                    foreach (TestPaperAnswerModel item in itemBig.list)
                                                    {
                                                        if (item != null && item.list != null)
                                                        {
                                                            int sNum = 0;
                                                            foreach (var itemScore in item.list)
                                                            {
                                                                sNum++;
                                                                List<Model_Student_HomeWorkAnswer> listStuScoreSub = listStuScore.Where(o => o.TestQuestions_Id == item.Testid && o.TestQuestions_Detail_OrderNum == sNum).ToList();
                                                                if (listStuScoreSub.Count > 0) itemScore.studentScore = listStuScoreSub[0].Student_Score.ToString();

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        savePath = "{0}{1}\\{2}\\{3}\\{3}.txt";
                                        //重新保存学生答题信息
                                        pfunction.WriteToFile(string.Format(savePath
                                            , uploadPath
                                            , "studentAnswerForMarking"
                                            , pfunction.ToShortDate(modelHW_TC.CreateTime.ToString())
                                            , stuHwId), JsonConvert.SerializeObject(modelStuAnswer), true);

                                    }
                                    catch (Exception)
                                    {
                                        List<TestPaperAnswerModel> listStu = new List<TestPaperAnswerModel>();
                                        listStu = JsonConvert.DeserializeObject<List<TestPaperAnswerModel>>(stuInfo);
                                        #region 普通题型 list
                                        if (listStu != null)
                                        {
                                            foreach (TestPaperAnswerModel item in listStu)
                                            {
                                                if (item != null && item.list != null)
                                                {
                                                    int sNum = 0;
                                                    foreach (var itemScore in item.list)
                                                    {
                                                        sNum++;
                                                        List<Model_Student_HomeWorkAnswer> listStuScoreSub = listStuScore.Where(o => o.TestQuestions_Id == item.Testid && o.TestQuestions_Detail_OrderNum == sNum).ToList();
                                                        if (listStuScoreSub.Count > 0) itemScore.studentScore = listStuScoreSub[0].Student_Score.ToString();

                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        savePath = "{0}{1}\\{2}\\{3}\\{3}.txt";
                                        //重新保存学生答题信息
                                        pfunction.WriteToFile(string.Format(savePath
                                             , uploadPath
                                             , "studentAnswerForMarking"
                                             , pfunction.ToShortDate(modelHW_TC.CreateTime.ToString())
                                             , stuHwId), JsonConvert.SerializeObject(listStu), true);

                                    }
                                    #endregion
                                }
                                catch (Exception)
                                {

                                }
                                context.Response.Write("1");
                            }
                            else
                            {
                                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(stuHwId, "", string.Format("批改失败：result 为0，|操作人{0}|学生作业Id{1}|方法{2}", tchId, stuHwId, "TeacherCorrect"));
                                context.Response.Write("0");
                            }
                        }
                        catch (Exception ex)
                        {
                            Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(stuHwId, "", string.Format("批改失败：|操作人{0}|学生作业Id{1}|方法{2}|错误信息{3}", tchId, stuHwId, "TeacherCorrect", ex.Message.ToString()));
                            context.Response.Write("0");
                        }
                        #endregion
                        break;
                    case "testpaper_tq_attr":
                        #region 试题解析等 /student/questionAttr.aspx
                        rtrfId = context.Request["rtrfId"].Filter();
                        string tqId = context.Request["tqId"].Filter();
                        string strAttrType = context.Request["attrType"].Filter();
                        modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);

                        //生成存储路径
                        saveOwnPath = string.Empty;
                        savePath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm,
                                              modelRTRF.Resource_Version, modelRTRF.Subject);
                        }
                        string strContent = pfunction.ReadAllText(string.Format("{0}{1}{2}\\{3}{4}.htm", uploadPath, saveOwnPath, strAttrType, savePath, tqId));
                        context.Response.Write(strContent);
                        #endregion
                        break;
                    case "testpaper_tq_attr_all":
                        #region 试题解析等 /student/questionAttrAll.aspx
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        tqId = context.Request["tqId"].Filter();
                        strAttrType = context.Request["attrType"].Filter();

                        modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);

                        //生成存储路径
                        saveOwnPath = string.Empty;
                        savePath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm,
                                              modelRTRF.Resource_Version, modelRTRF.Subject);
                        }
                        strSql = "select TestQuestions_Score_ID,TestQuestions_Num,TestQuestions_OrderNum from TestQuestions_Score where ResourceToResourceFolder_Id='" + rtrfId + "' and TestQuestions_Id='" + tqId + "' order by TestQuestions_OrderNum ";
                        DataTable dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        foreach (DataRow item in dtTQ_Score.Rows)
                        {
                            string url = string.Format("{0}{1}{2}\\{3}{4}.htm", uploadPath, saveOwnPath, strAttrType, savePath, item["TestQuestions_Score_ID"]);
                            string strAnalyzeHtml = pfunction.ReadAllText(url);
                            if (!string.IsNullOrEmpty(strAnalyzeHtml))
                            {
                                stbHtml.AppendFormat("<dl class=\"clearfix\"><dd style='line-height:50px;'>{0}</dd></dl>"
                                , System.Text.RegularExpressions.Regex.Replace(pfunction.NoHTML(strAnalyzeHtml), "font-size:(.+)px;", "font-size:32px;")
                                );
                            }
                        }
                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        break;
                    case "testpaper_stu_answer":
                        #region 学生答案 /Evaluation/CommentReportStudentAnswer.aspx
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        hwId = context.Request["hwId"].Filter();
                        tqId = context.Request["tqId"].Filter();
                        stuId = context.Request["stuId"].Filter();
                        strAttrType = context.Request["attrType"].Filter();

                        modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                        Model_HomeWork modelHW = new BLL_HomeWork().GetModel(hwId);
                        Model_Student_HomeWork modelSHW = new BLL_Student_HomeWork().GetModelNew(hwId, stuId);
                        Model_Student_HomeWork_Correct modelSHWC = new BLL_Student_HomeWork_Correct().GetModel(modelSHW.Student_HomeWork_Id);

                        if (modelSHWC.CorrectMode == "1")
                        {
                            #region 客户端批改
                            fileStudentAnswerUrl = uploadPath + "{0}\\" + pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + modelSHWC.Student_HomeWork_Id + "\\" + "{1}.{2}";//文件详细路径

                            strSql = @"select * from TestQuestions where TestQuestions_Id='" + tqId + "' ";
                            dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                            foreach (DataRow item in dtTQ.Rows)
                            {
                                //批改详情
                                string strTQ_Correct = pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "teacherMarking", tqId, "txt"));
                                stbHtml.AppendFormat("<dl class=\"clearfix\"><dt>{0}</dt><dd><div style='line-height:50px;'><img src=\"data:image/png;base64,{1}\" width='100%' /></div></dd></dl>"
                                    , item["topicNumber"]
                                    , strTQ_Correct);
                            }

                            #endregion
                        }
                        else
                        {
                            #region web端批改
                            //生成存储路径
                            savePath = string.Empty;
                            saveOwnPath = string.Empty;
                            if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                            {
                                savePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\"
                                    , pfunction.ToShortDate(modelHW.CreateTime.ToString())
                                    , modelRTRF.ParticularYear, modelRTRF.GradeTerm
                                    , modelRTRF.Resource_Version, modelRTRF.Subject);
                            }
                            if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                            {
                                savePath = string.Format("{0}\\"
                                                       , pfunction.ToShortDate(modelHW.CreateTime.ToString()));
                            }
                            strSql = @"select shwa.Student_HomeWorkAnswer_Id,shwa.Student_Answer,shwa.TestQuestions_NumStr,shwa.Comment,tqs.TestType,tqs.testIndex
from Student_HomeWorkAnswer shwa
inner join TestQuestions_Score tqs on tqs.TestQuestions_Score_ID=shwa.TestQuestions_Score_ID where tqs.ResourceToResourceFolder_Id='" + rtrfId + "' and shwa.TestQuestions_Id='" + tqId + "' and shwa.Student_Id='" + stuId + "' and shwa.Homework_Id='" + hwId + "' order by tqs.TestQuestions_OrderNum ";
                            dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                            //fileStudentAnswerUrl = uploadPath + "{0}\\" + pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + stuHwId + "\\" + "{1}.{2}";//文件详细路径

                            foreach (DataRow item in dtTQ_Score.Rows)
                            {
                                if (item["TestType"].ToString() == "selection" || item["TestType"].ToString() == "clozeTest" || item["TestType"].ToString() == "truefalse")
                                {
                                    stbHtml.AppendFormat("<dl class=\"clearfix\"><dt>{0}</dt><dd>{1}</dd></dl>"
                                        , item["TestQuestions_NumStr"]
                                        , item["Student_Answer"]);
                                }
                                else
                                {
                                    string strStudentAnswerHtml = pfunction.ReadAllText(string.Format("{0}{1}\\{2}{3}.txt", uploadPath, strAttrType, savePath, item["Student_HomeWorkAnswer_Id"]));
                                    if (!string.IsNullOrEmpty(strStudentAnswerHtml))
                                    {
                                        stbHtml.AppendFormat("<dl class=\"clearfix\"><dt>{0} {1}</dt><dd><div style='line-height:50px;'>{2}</div></dd></dl>"
                                        , item["TestQuestions_NumStr"]
                                        , !string.IsNullOrEmpty(item["testIndex"].ToString()) ? "(" + item["testIndex"].ToString() + ")" : ""
                                        , System.Text.RegularExpressions.Regex.Replace(pfunction.NoHTML(strStudentAnswerHtml), "font-size:(.+)px;", "font-size:30px;")
                                        );
                                    }
                                }
                            }
                            stbHtml.AppendFormat("<div>{0}</div>", dtTQ_Score.Rows.Count > 0 ? string.IsNullOrEmpty(dtTQ_Score.Rows[0]["Comment"].ToString()) ? "" : "批注：" + dtTQ_Score.Rows[0]["Comment"].ToString() : "");
                            #endregion
                        }

                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        break;
                    case "testpaper_stu_answer_multi":
                        #region 学生答案 /Evaluation/CommentReportStudentAnswer_Multi.aspx
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        hwId = context.Request["hwId"].Filter();
                        tqId = context.Request["tqId"].Filter();
                        stuId = context.Request["stuId"].Filter();
                        strAttrType = context.Request["attrType"].Filter();
                        string stuIdNew = string.Format("'{0}'", stuId.Replace(",", "','"));

                        modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                        modelHW = new BLL_HomeWork().GetModel(hwId);

                        string strWhereStu = string.Format("UserId in({0}) order by charindex(','+UserId+',',',{1},')"
                            , stuIdNew
                            , context.Request["stuId"].Filter());
                        DataTable dtStu = new BLL_F_User().GetList(strWhereStu).Tables[0];
                        foreach (DataRow itemStu in dtStu.Rows)
                        {
                            stbHtml.AppendFormat("<div class='col-xs-6' style='overflow-x:auto;'><div class='panel'><div class='panel-heading'><div class='panel-title'>{0}</div></div>", string.IsNullOrEmpty(itemStu["TrueName"].ToString()) ? itemStu["UserName"].ToString() : itemStu["TrueName"].ToString());
                            modelSHW = new BLL_Student_HomeWork().GetModelNew(hwId, itemStu["UserId"].ToString());
                            modelSHWC = new BLL_Student_HomeWork_Correct().GetModel(modelSHW.Student_HomeWork_Id);
                            if (modelSHWC.CorrectMode == "1")
                            {
                                #region 客户端批改
                                fileStudentAnswerUrl = uploadPath + "{0}\\" + pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + modelSHWC.Student_HomeWork_Id + "\\" + "{1}.{2}";//文件详细路径

                                strSql = @"select * from TestQuestions where TestQuestions_Id='" + tqId + "' ";
                                dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                                foreach (DataRow item in dtTQ.Rows)
                                {
                                    //批改详情
                                    string strTQ_Correct = pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "teacherMarking", tqId, "txt"));
                                    stbHtml.AppendFormat("<dl class=\"clearfix\"><dt>{0}</dt><dd><div style='line-height:50px;'><img src=\"data:image/png;base64,{1}\" width='100%' /></div></dd></dl>"
                                        , item["topicNumber"]
                                        , strTQ_Correct);
                                }
                                #endregion
                            }
                            else
                            {
                                #region web端批改
                                //生成存储路径
                                savePath = string.Empty;
                                saveOwnPath = string.Empty;
                                if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                                {
                                    savePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\"
                                        , pfunction.ToShortDate(modelHW.CreateTime.ToString())
                                        , modelRTRF.ParticularYear, modelRTRF.GradeTerm
                                        , modelRTRF.Resource_Version, modelRTRF.Subject);
                                }
                                if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                                {
                                    savePath = string.Format("{0}\\"
                                                           , pfunction.ToShortDate(modelHW.CreateTime.ToString()));
                                }
                                strSql = @"select shwa.Student_HomeWorkAnswer_Id,shwa.Student_Answer,shwa.TestQuestions_NumStr,shwa.Comment,tqs.TestType,tqs.testIndex
from Student_HomeWorkAnswer shwa
inner join TestQuestions_Score tqs on tqs.TestQuestions_Score_ID=shwa.TestQuestions_Score_ID where tqs.ResourceToResourceFolder_Id='" + rtrfId + "' and shwa.TestQuestions_Id='" + tqId + "' and shwa.Student_Id='" + itemStu["UserId"].ToString() + "' and shwa.Homework_Id='" + hwId + "' order by tqs.TestQuestions_OrderNum ";
                                dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                                //fileStudentAnswerUrl = uploadPath + "{0}\\" + pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + stuHwId + "\\" + "{1}.{2}";//文件详细路径

                                foreach (DataRow item in dtTQ_Score.Rows)
                                {
                                    if (item["TestType"].ToString() == "selection" || item["TestType"].ToString() == "clozeTest" || item["TestType"].ToString() == "truefalse")
                                    {
                                        stbHtml.AppendFormat("<dl class=\"clearfix\"><dt>{0}</dt><dd>{1}</dd></dl>"
                                            , item["TestQuestions_NumStr"]
                                            , item["Student_Answer"]);
                                    }
                                    else
                                    {
                                        string strStudentAnswerHtml = pfunction.ReadAllText(string.Format("{0}{1}\\{2}{3}.txt", uploadPath, strAttrType, savePath, item["Student_HomeWorkAnswer_Id"]));
                                        if (!string.IsNullOrEmpty(strStudentAnswerHtml))
                                        {
                                            stbHtml.AppendFormat("<dl class=\"clearfix\"><dt>{0} {1}</dt><dd><div style='line-height:50px;'>{2}</div></dd></dl>"
                                            , item["TestQuestions_NumStr"]
                                            , !string.IsNullOrEmpty(item["testIndex"].ToString()) ? "(" + item["testIndex"].ToString() + ")" : ""
                                            , System.Text.RegularExpressions.Regex.Replace(pfunction.NoHTML(strStudentAnswerHtml), "font-size:(.+)px;", "font-size:30px;")
                                            );
                                        }
                                    }
                                }
                                stbHtml.AppendFormat("<div>{0}</div>", dtTQ_Score.Rows.Count > 0 ? string.IsNullOrEmpty(dtTQ_Score.Rows[0]["Comment"].ToString()) ? "" : "批注：" + dtTQ_Score.Rows[0]["Comment"].ToString() : "");
                                #endregion
                            }
                            stbHtml.Append("</div></div>");
                        }

                        #region 注释内容
                        //                        //生成存储路径
                        //                        savePath = string.Empty;
                        //                        saveOwnPath = string.Empty;
                        //                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        //                        {
                        //                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\"
                        //                                , pfunction.ToShortDate(modelHW.CreateTime.ToString())
                        //                                , modelRTRF.ParticularYear, modelRTRF.GradeTerm
                        //                                , modelRTRF.Resource_Version, modelRTRF.Subject);
                        //                        }
                        //                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        //                        {
                        //                            savePath = string.Format("{0}\\", pfunction.ToShortDate(modelHW.CreateTime.ToString()));
                        //                        }

                        //                        string strWhereStu = string.Format("UserId in({0}) order by charindex(','+UserId+',',',{1},')"
                        //                            , stuIdNew
                        //                            , context.Request["stuId"].Filter());
                        //                        DataTable dtStu = new BLL_F_User().GetList(strWhereStu).Tables[0];

                        //                        strSql = string.Format(@"select shwa.Student_HomeWorkAnswer_Id,shwa.Student_Answer,shwa.TestQuestions_NumStr,tqs.TestType,tqs.testIndex,shwa.Student_Id
                        //from Student_HomeWorkAnswer shwa
                        //inner join TestQuestions_Score tqs on tqs.TestQuestions_Score_ID=shwa.TestQuestions_Score_ID 
                        //where tqs.ResourceToResourceFolder_Id='{0}' and shwa.TestQuestions_Id='{1}' and shwa.Student_Id in({2}) and shwa.Homework_Id='{3}' 
                        //order by shwa.Student_Id,tqs.TestQuestions_OrderNum "
                        //                            , rtrfId, tqId, stuIdNew, hwId);

                        //                        dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        //                        foreach (DataRow itemStu in dtStu.Rows)
                        //                        {
                        //                            DataRow[] drTQ_S = dtTQ_Score.Select("Student_Id='" + itemStu["UserId"] + "'");
                        //                            stbHtml.AppendFormat("<div class='col-xs-6' style='overflow-x:auto;'><div class='panel'><div class='panel-heading'><div class='panel-title'>{0}</div></div>", string.IsNullOrEmpty(itemStu["TrueName"].ToString()) ? itemStu["UserName"].ToString() : itemStu["TrueName"].ToString());
                        //                            foreach (DataRow item in drTQ_S)
                        //                            {
                        //                                if (item["TestType"].ToString() == "selection" || item["TestType"].ToString() == "clozeTest" || item["TestType"].ToString() == "truefalse")
                        //                                {
                        //                                    stbHtml.AppendFormat("<div class='panel-body'><dl class=\"clearfix\"><dt>{0}</dt><dd>{1}</dd></dl></div>"
                        //                                        , item["TestQuestions_NumStr"]
                        //                                        , item["Student_Answer"]);
                        //                                }
                        //                                else
                        //                                {
                        //                                    string strStudentAnswerHtml = pfunction.ReadAllText(string.Format("{0}{1}\\{2}{3}.txt", uploadPath, strAttrType, savePath, item["Student_HomeWorkAnswer_Id"]));
                        //                                    if (!string.IsNullOrEmpty(strStudentAnswerHtml))
                        //                                    {
                        //                                        stbHtml.AppendFormat("<div class='panel-body'><dl class=\"clearfix\"><dt>{0} {1}</dt><dd><div style='line-height:50px;'>{2}</div></dd></dl></div>"
                        //                                            , item["TestQuestions_NumStr"]
                        //                                            , !string.IsNullOrEmpty(item["testIndex"].ToString()) ? "(" + item["testIndex"].ToString() + ")" : ""
                        //                                            , System.Text.RegularExpressions.Regex.Replace(pfunction.NoHTML(strStudentAnswerHtml), "font-size:(.+)px;", "font-size:30px;")
                        //                                        );
                        //                                    }
                        //                                }
                        //                            }
                        //                            stbHtml.Append("</div></div>");
                        //                        }
                        #endregion

                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        break;
                    case "stu_testpaperCorrect_client":
                        #region 客户端批改学生作业与错题集 /student/ohomeworkview_client.aspx
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        stuId = context.Request["stuId"].Filter();
                        hwId = context.Request["hwId"].Filter();
                        string shwId = context.Request["shwId"].Filter();
                        string IsWrong = context.Request["IsWrong"].Filter();
                        string isCorrect = context.Request["isCorrect"].Filter();
                        strSqlAnswerScore = string.Format(@"select tqs.TestQuestions_Id,tqs.AnalyzeHyperlinkData,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,shwa.Student_HomeWorkAnswer_Id,shwa.Student_Answer,shwa.Student_Score,shwa.Student_Answer_Status,shwa.Comment
,tqs.ContentText,tqs.TargetText
from TestQuestions_Score tqs
left join Student_HomeWorkAnswer shwa on TQS.TestQuestions_Id=SHWA.TestQuestions_Id and shwa.TestQuestions_Score_ID=tqs.TestQuestions_Score_ID  where tqs.TestQuestions_Score!=-1 and shwa.HomeWork_Id='{0}' and shwa.Student_Id='{1}' {2} order by tqs.TestQuestions_OrderNum", hwId, stuId,
                      IsWrong == "False" ? "" : string.Format(@" and SHWA.TestQuestions_Id in (SELECT DISTINCT t1.TestQuestions_Id FROM Student_HomeWorkAnswer t1 WHERE t1.Student_Answer_Status!='right' and Student_Answer_Status!='unknown' and t1.Student_Id='{0}' and t1.HomeWork_Id='{1}')", stuId, hwId));
                        dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlAnswerScore).Tables[0];

                        correctCount = 0;
                        strSql = string.Empty;
                        if (IsWrong == "True")
                        {
                            strSql = string.Format(@"select * from TestQuestions where TestQuestions_Type='title' and ResourceToResourceFolder_Id='{0}'
union  
select * from TestQuestions where TestQuestions_Type='' and ResourceToResourceFolder_Id='{0}' and TestQuestions_Id in (
SELECT DISTINCT tq.Parent_Id FROM Student_HomeWorkAnswer t1
inner join TestQuestions tq on tq.TestQuestions_Id=t1.TestQuestions_Id and tq.Parent_Id!='0' 
inner join TestQuestions_Score tqs on tqs.TestQuestions_Score_ID=t1.TestQuestions_Score_ID
WHERE tqs.TestQuestions_Score!=-1 and t1.Student_Answer_Status!='right' and Student_Answer_Status!='unknown' and t1.Student_Id='{1}' and t1.HomeWork_Id='{2}')
union 
select * from TestQuestions where TestQuestions_Type!='title' and ResourceToResourceFolder_Id='{0}'
and TestQuestions_Id in (
SELECT DISTINCT t1.TestQuestions_Id FROM Student_HomeWorkAnswer t1 
inner join TestQuestions_Score tqs on tqs.TestQuestions_Score_ID=t1.TestQuestions_Score_ID
WHERE tqs.TestQuestions_Score!=-1 and t1.Student_Answer_Status!='right' and Student_Answer_Status!='unknown' and t1.Student_Id='{1}' and t1.HomeWork_Id='{2}'
)
order by TestQuestions_Num ", rtrfId, stuId, hwId);
                        }
                        else
                        {
                            strSql = string.Format(@"select * from TestQuestions where ResourceToResourceFolder_Id='{0}' order by TestQuestions_Num ", rtrfId);
                        }

                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        #region 给全局变量赋值
                        listHW_TQ = new BLL_StatsClassHW_TQ().GetModelList("ResourceToResourceFolder_Id='" + rtrfId + "' and HomeWork_Id='" + hwId + "' ");

                        strSql = string.Format(@"select TestQuestions_Id,TestQuestions_Score_ID,Student_Answer_Status,COUNT(1) as ICOUNT from Student_HomeWorkAnswer
where HomeWork_ID='{0}' group by TestQuestions_Id,TestQuestions_Score_ID,Student_Answer_Status", hwId);
                        dtTQ_StudentAnswerCount = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        #endregion
                        modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                        modelHW = new BLL_HomeWork().GetModel(hwId);

                        //生成存储路径
                        savePath = string.Empty;
                        saveOwnPath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                        }
                        fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                        fileStudentAnswerUrl = uploadPath + "{0}\\" + pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + savePath + "{1}.{2}";//学生答案详细路径
                        string fileComment = uploadPath + "{0}\\" + pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + shwId + "\\" + "{1}.{2}";//批注文件详细路径
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dt.Rows[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                            if (drScore.Length > 0)
                            {
                                //选择题、完形填空题、判断题
                                if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "truefalse")
                                {
                                    //题干
                                    string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"));

                                    //选择题、完形填空题选项
                                    string strOption = string.Empty;
                                    if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest")
                                    {
                                        for (int ii = 0; ii < drScore.Length; ii++)
                                        {
                                            //从文件读取选择题选项
                                            string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                            List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                            if (listTestSelections != null && listTestSelections.Count > 0)
                                            {
                                                foreach (var item in listTestSelections)
                                                {
                                                    if (!string.IsNullOrEmpty(item.selectionHTML)) strOption += string.Format("<div class=\"option_item col-xs-6\">{0}</div>", pfunction.NoHTML(item.selectionHTML));
                                                }
                                                if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                {
                                                    strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                }
                                            }
                                        }
                                    }

                                    stbHtml.Append("<div class=\"question panel panel-default\" data-name='correct_over'>");
                                    stbHtml.Append("<div class=\"panel-body\">");
                                    stbHtml.AppendFormat("<div class=\"remark\" data-content=\"{0}\" id='aSpan{1}' style=\"{2}\"><i>批注</i></div>"
                                        , (drScore.Length == 0 ? "" : drScore[0]["Comment"])
                                        , dt.Rows[i]["TestQuestions_Num"]
                                        , (drScore.Length == 0 || string.IsNullOrEmpty(drScore[0]["Comment"].ToString()) ? "display:none;" : ""));
                                    stbHtml.AppendFormat("<div class=\"question_tit\">{0}(分值：<i class=\"fen\">{1}</i>)<div class='options row clearfix'>{2}</div></div>"
                                        , pfunction.NoHTML(strTestQuestionBody)
                                        , dt.Rows[i]["TestQuestions_SumScore"].ToString().clearLastZero()
                                        , strOption);
                                    stbHtml.Append("<div class=\"answer clearfix\">");
                                    //标题
                                    stbHtml.Append("<div class='tit row clearfix mh'>");
                                    stbHtml.Append("<div class='correct col-xs-5'><span class='btn btn-success'>正确答案</span></div>");
                                    stbHtml.Append("<div class='student col-xs-6'><span class='btn btn-info'>学生答案</span></div>");
                                    stbHtml.Append("<div class='scorce col-xs-1'><span class='btn btn-primary btn-block'>得分</span></div>");
                                    stbHtml.Append("</div>");
                                    //答案&得分
                                    for (int ii = 0; ii < drScore.Length; ii++)
                                    {
                                        stbHtml.Append("<div class='answer_main row clearfix'>");
                                        if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "truefalse")
                                        {//选择题、完形填空题、判断题 答案从数据库读取
                                            //正确答案
                                            stbHtml.AppendFormat("<div class='correct_answer col-xs-5'>{0}</div>"
                                                , (isCorrect == "True" || modelHW.IsShowAnswer == 1 ? drScore[ii]["TestCorrect"].ToString() : ""));
                                            //学生答案
                                            stbHtml.AppendFormat("<div class='student_answer col-xs-6'>{0}</div>", drScore[ii]["Student_Answer"]);
                                        }
                                        else if (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" || dt.Rows[i]["TestQuestions_Type"].ToString() == "answers")
                                        {//填空题、简答题 答案从文件读取
                                            //正确答案
                                            stbHtml.Append("<div class='correct_answer col-xs-5'>");
                                            if (isCorrect == "True" || modelHW.IsShowAnswer == 1)
                                            {
                                                //从文件读取正确答案图片
                                                string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                if (!string.IsNullOrEmpty(strTestQuestionCurrent)) stbHtml.AppendFormat("<div>{0}</div>", pfunction.NoHTML(strTestQuestionCurrent));
                                            }
                                            else
                                            {
                                                stbHtml.Append("<div></div>");
                                            }
                                            stbHtml.Append("</div>");
                                            //学生答案
                                            stbHtml.Append("<div class='student_answer col-xs-6'>");
                                            string strStudentAnswerHtml = pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "studentAnswer", drScore[ii]["Student_HomeWorkAnswer_Id"], "txt"));
                                            if (!string.IsNullOrEmpty(strStudentAnswerHtml)) stbHtml.AppendFormat("<div>{0}</div>", pfunction.NoHTML(strStudentAnswerHtml));
                                            stbHtml.Append("</div>");
                                        }
                                        //分值
                                        stbHtml.Append("<div class='score col-xs-1'>");
                                        correctCount++;
                                        stbHtml.AppendFormat("<input type=\"text\" class='form-control text-center score_input' name=\"choice_Score\" maxlength=\"4\" actualscore=\"{0}\" tqnum=\"{1}\" tqonum=\"{2}\" value=\"{3}\" readonly />"
                                            , drScore[ii]["TestQuestions_Score"].ToString().clearLastZero()
                                            , dt.Rows[i]["TestQuestions_Id"]
                                            , drScore[ii]["TestQuestions_OrderNum"]
                                            , drScore[ii]["Student_Score"].ToString().clearLastZero());
                                        stbHtml.Append("</div>");
                                        stbHtml.Append("</div>");
                                    }
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");

                                    stbHtml.Append("<div class=\"commenting panel-footer clearfix\">");
                                    stbHtml.Append("<div class=\"commenting_box\">");
                                    stbHtml.Append("<dl class='clearfix'>");
                                    stbHtml.Append("<dt>批注：</dt>");
                                    stbHtml.AppendFormat("<dd>{0}</dd>", (drScore.Length == 0 ? "" : drScore[0]["Comment"]));
                                    stbHtml.Append("</dl>");

                                    string strAvgScore = string.Empty;//平均得分
                                    string strContentText = string.Empty;//知识内容
                                    string strTargetText = string.Empty;//测量目标                            
                                    for (int ii = 0; ii < drScore.Length; ii++)
                                    {
                                        if (pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()) != "")
                                        {
                                            strAvgScore += string.Format("<dd>{0}</dd>", GetAvgScore(drScore[ii]["TestQuestions_Score_ID"].ToString()));
                                            strContentText += string.Format("<dd>{0}</dd>", drScore[ii]["ContentText"].ToString());
                                            strTargetText += string.Format("<dd>{0}</dd>", drScore[ii]["TargetText"].ToString());
                                        }
                                    }
                                    stbHtml.AppendFormat("<dl class='clearfix'><dt>平均得分：</dt>{0}</dl>", strAvgScore);
                                    stbHtml.AppendFormat("<dl class='clearfix'><dt>知识内容：</dt>{0}</dl>", strContentText);
                                    stbHtml.AppendFormat("<dl class='clearfix'><dt>测量目标：</dt>{0}</dl>", strTargetText);

                                    stbHtml.Append("</div>");
                                    if (isCorrect == "True" || modelHW.IsShowAnswer == 1)
                                    {
                                        stbHtml.AppendFormat("<a class='analysis btn btn-default btn-sm' onclick=\"PicPreview('questionAttrAll.aspx?resourceid={0}&questionid={1}&stuId={2}&attrType=AnalyzeHtml','解析');\">查看解析</a>"
                                            , rtrfId
                                            , dt.Rows[i]["TestQuestions_Id"]
                                            , stuId);
                                    }
                                    //stbHtml.Append("<li><a href='##'>强化训练</a></li>");
                                    stbHtml.Append("</div>");

                                    stbHtml.Append("</div>");
                                }
                                else//填空题、解答题
                                {
                                    //批改详情
                                    string strTQ_Correct = pfunction.ReadAllText(string.Format(fileComment, "teacherMarking", dt.Rows[i]["TestQuestions_Id"], "txt"));

                                    stbHtml.Append("<div class=\"question panel panel-default\" data-name='correct_over'>");
                                    stbHtml.AppendFormat("<div class=\"panel-body\"><img src=\"data:image/png;base64,{0}\" width='100%' /></div>", strTQ_Correct);

                                    stbHtml.Append("<div class=\"commenting panel-footer clearfix\">");
                                    stbHtml.Append("<div class=\"commenting_box\">");

                                    string strAvgScore = string.Empty;//平均得分
                                    string strContentText = string.Empty;//知识内容
                                    string strTargetText = string.Empty;//测量目标                            
                                    for (int ii = 0; ii < drScore.Length; ii++)
                                    {
                                        if (pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()) != "")
                                        {
                                            strAvgScore += string.Format("<dd>{0}</dd>", GetAvgScore(drScore[ii]["TestQuestions_Score_ID"].ToString()));
                                            strContentText += string.Format("<dd>{0}</dd>", drScore[ii]["ContentText"].ToString());
                                            strTargetText += string.Format("<dd>{0}</dd>", drScore[ii]["TargetText"].ToString());
                                        }
                                    }
                                    stbHtml.AppendFormat("<dl class='clearfix'><dt>平均得分：</dt>{0}</dl>", strAvgScore);
                                    stbHtml.AppendFormat("<dl class='clearfix'><dt>知识内容：</dt>{0}</dl>", strContentText);
                                    stbHtml.AppendFormat("<dl class='clearfix'><dt>测量目标：</dt>{0}</dl>", strTargetText);

                                    stbHtml.Append("</div>");
                                    if (isCorrect == "True" || modelHW.IsShowAnswer == 1)
                                    {
                                        stbHtml.AppendFormat("<a class='analysis btn btn-default btn-sm' onclick=\"PicPreview('questionAttrAll.aspx?resourceid={0}&questionid={1}&stuId={2}&attrType=AnalyzeHtml','解析');\">查看解析</a>"
                                            , rtrfId
                                            , dt.Rows[i]["TestQuestions_Id"]
                                            , stuId);
                                    }
                                    stbHtml.Append("</div>");

                                    stbHtml.Append("</div>");
                                }
                            }
                            else
                            {
                                //题干
                                string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"));
                                strTestQuestionBody = pfunction.NoHTML(strTestQuestionBody);
                                if (string.IsNullOrEmpty(strTestQuestionBody))
                                {
                                    strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "textTitle", dt.Rows[i]["TestQuestions_Id"], "txt"));
                                }
                                stbHtml.Append("<div class=\"question\" data-name='correct_over'>");
                                stbHtml.AppendFormat("<div class=\"question_tit\">{0}</div>", strTestQuestionBody);
                                stbHtml.Append("</div>");
                            }

                        }
                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        break;
                    case "stu_testpaperCorrect_view":
                        #region web端批改学生作业与错题集 /student/OHomeWorkViewTT.aspx
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        stuId = context.Request["stuId"].Filter();
                        hwId = context.Request["hwId"].Filter();
                        shwId = context.Request["shwId"].Filter();
                        IsWrong = context.Request["IsWrong"].Filter();
                        isCorrect = context.Request["isCorrect"].Filter();
                        string hwCTime = context.Request["hwCTime"].Filter();
                        strSqlAnswerScore = string.Format(@"select tqs.TestQuestions_Id,tqs.AnalyzeHyperlinkData,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex,shwa.Student_HomeWorkAnswer_Id,shwa.Student_Answer,shwa.Student_Score,shwa.Student_Answer_Status,shwa.Comment
,tqs.ContentText,tqs.TargetText
from TestQuestions_Score tqs
left join Student_HomeWorkAnswer shwa on TQS.TestQuestions_Id=SHWA.TestQuestions_Id and shwa.TestQuestions_Score_ID=tqs.TestQuestions_Score_ID  where shwa.HomeWork_Id='{0}' and shwa.Student_Id='{1}' {2} order by tqs.TestQuestions_OrderNum", hwId, stuId,
                      IsWrong == "False" ? "" : string.Format(@" and SHWA.TestQuestions_Id in (SELECT DISTINCT t1.TestQuestions_Id FROM Student_HomeWorkAnswer t1 WHERE t1.Student_Answer_Status!='right' and Student_Answer_Status!='unknown' and t1.Student_Id='{0}' and t1.HomeWork_Id='{1}')", stuId, hwId));
                        dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlAnswerScore).Tables[0];
                        #region 给全局变量赋值
                        listHW_TQ = new BLL_StatsClassHW_TQ().GetModelList("ResourceToResourceFolder_Id='" + rtrfId + "' and HomeWork_Id='" + hwId + "' ");

                        strSql = string.Format(@"select TestQuestions_Id,TestQuestions_Score_ID,Student_Answer_Status,COUNT(1) as ICOUNT from Student_HomeWorkAnswer
where HomeWork_ID='{0}' group by TestQuestions_Id,TestQuestions_Score_ID,Student_Answer_Status", hwId);
                        dtTQ_StudentAnswerCount = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        #endregion
                        strSql = string.Empty;
                        if (IsWrong == "True")
                        {
                            strSql = string.Format(@"select * from TestQuestions where TestQuestions_Type='title' and ResourceToResourceFolder_Id='{0}' 
                        union  
                        select * from TestQuestions where TestQuestions_Type='' and ResourceToResourceFolder_Id='{0}' and TestQuestions_Id in (
SELECT DISTINCT tq.Parent_Id FROM Student_HomeWorkAnswer t1
inner join TestQuestions tq on tq.TestQuestions_Id=t1.TestQuestions_Id and tq.Parent_Id!='0'
WHERE t1.Student_Answer_Status!='right' and Student_Answer_Status!='unknown' and t1.Student_Id='{1}' and t1.HomeWork_Id='{2}')
                        union  
                        select * from TestQuestions where TestQuestions_Type!='title' and ResourceToResourceFolder_Id='{0}' and TestQuestions_Id in (SELECT DISTINCT t1.TestQuestions_Id FROM Student_HomeWorkAnswer t1 WHERE t1.Student_Answer_Status!='right' and Student_Answer_Status!='unknown' and t1.Student_Id='{1}' and t1.HomeWork_Id='{2}') order by TestQuestions_Num ", rtrfId, stuId, hwId);
                        }
                        else
                        {
                            strSql = string.Format(@"select * from TestQuestions where ResourceToResourceFolder_Id='{0}' order by TestQuestions_Num ", rtrfId);
                        }
                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        strSql = string.Format(@"select TestQuestions_Id,TestQuestions_Score_ID,Student_Answer_Status,COUNT(1) as ICOUNT from Student_HomeWorkAnswer
                         where HomeWork_ID='{0}' group by TestQuestions_Id,TestQuestions_Score_ID,Student_Answer_Status", hwId);
                        dtTQ_StudentAnswerCount = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                        modelHW = new BLL_HomeWork().GetModel(hwId);

                        //生成存储路径
                        savePath = string.Empty;
                        saveOwnPath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                        }
                        fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                        fileStudentAnswerUrl = uploadPath + "{0}\\" + pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + savePath + "{1}.{2}";//学生答案详细路径
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dt.Rows[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                            if (drScore.Length > 0)
                            {
                                //题干
                                string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"));

                                //选择题、完形填空题选项
                                string strOption = string.Empty;
                                if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest")
                                {
                                    for (int ii = 0; ii < drScore.Length; ii++)
                                    {
                                        //从文件读取选择题选项
                                        string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                        List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                        if (listTestSelections != null && listTestSelections.Count > 0)
                                        {
                                            foreach (var item in listTestSelections)
                                            {
                                                if (!string.IsNullOrEmpty(item.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(item.selectionHTML));
                                            }
                                            if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                            {
                                                strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                            }
                                        }
                                    }
                                }

                                stbHtml.Append("<div class=\"question_panel\" data-name='correct_over'>");
                                //stbHtml.AppendFormat("<div class=\"remark\" data-content=\"{0}\" id='aSpan{1}' style=\"{2}\"><i>批注</i></div>"
                                //    , (drScore.Length == 0 ? "" : drScore[0]["Comment"])
                                //    , dt.Rows[i]["TestQuestions_Num"]
                                //    , (drScore.Length == 0 || string.IsNullOrEmpty(drScore[0]["Comment"].ToString()) ? "display:none;" : ""));
                                stbHtml.AppendFormat("<div class=\"panel_stem\">{0}(分值：<i class=\"fen\">{1}</i>)</div><div class='panel_option'>{2}</div>"
                                    , pfunction.NoHTML(strTestQuestionBody)
                                    , dt.Rows[i]["TestQuestions_SumScore"].ToString().clearLastZero()
                                    , strOption);
                                stbHtml.Append("<div class=\"panel_answer\">");
                                //标题
                                stbHtml.Append("<div class='answer_heading'>");
                                stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                stbHtml.Append("<div class='answer_item student_answer'><span class='btn btn-info btn-sm'>学生答案</span></div>");
                                stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-block btn-sm'>得分</span></div>");
                                stbHtml.Append("</div>");
                                //答案&得分
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    stbHtml.Append("<div class='answer_body'>");
                                    if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "truefalse")
                                    {//选择题、判断题 答案从数据库读取
                                        //正确答案
                                        stbHtml.AppendFormat("<div class='answer_item reference_answer'><div class='multi_panel'>{0}<div class='panel_content'>{1}</div></div></div>"
                                            , dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("<div class='panel_left'>(" + drScore[ii]["testIndex"].ToString() + ")</div>") : ""
                                            , (isCorrect == "True" || modelHW.IsShowAnswer == 1 ? drScore[ii]["TestCorrect"].ToString() : ""));
                                        //学生答案
                                        stbHtml.AppendFormat("<div class='answer_item student_answer'>{0}</div>", drScore[ii]["Student_Answer"]);
                                    }
                                    else if (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" || dt.Rows[i]["TestQuestions_Type"].ToString() == "answers")
                                    {//填空题、简答题 答案从文件读取
                                        //正确答案
                                        stbHtml.Append("<div class='answer_item reference_answer'>");
                                        if (isCorrect == "True" || modelHW.IsShowAnswer == 1)
                                        {
                                            //从文件读取正确答案图片
                                            string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                            if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                            {
                                                stbHtml.AppendFormat("<div class='multi_panel'>{0}<div class='panel_content'>{1}</div></div>"
                                                    , (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString())) ? "<div class='panel_left'>(" + drScore[ii]["testIndex"].ToString() + ")</div>" : ""
                                                    , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));
                                            }
                                        }
                                        else
                                        {
                                            stbHtml.Append("<div></div>");
                                        }
                                        stbHtml.Append("</div>");
                                        //学生答案
                                        stbHtml.Append("<div class='answer_item student_answer'>");
                                        string strStudentAnswerHtml = pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "studentAnswer", drScore[ii]["Student_HomeWorkAnswer_Id"], "txt"));
                                        if (!string.IsNullOrEmpty(strStudentAnswerHtml)) stbHtml.AppendFormat("<div>{0}</div>", pfunction.NoHTML(strStudentAnswerHtml));
                                        stbHtml.Append("</div>");
                                    }
                                    //分值
                                    stbHtml.Append("<div class='answer_item score'>");
                                    if (pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()) != "")
                                    {
                                        stbHtml.AppendFormat("<input type=\"text\" class='form-control input-sm text-center score_input' name=\"choice_Score\" maxlength=\"4\" actualscore=\"{0}\" tqnum=\"{1}\" tqonum=\"{2}\" value=\"{3}\" readonly />"
                                            , drScore[ii]["TestQuestions_Score"].ToString().clearLastZero()
                                            , dt.Rows[i]["TestQuestions_Id"]
                                            , drScore[ii]["TestQuestions_OrderNum"]
                                            , drScore[ii]["Student_Score"].ToString().clearLastZero());
                                    }
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");
                                }
                                stbHtml.Append("</div>");

                                if (isCorrect == "True" || modelHW.IsShowAnswer == 1)
                                {
                                    stbHtml.Append("<div class='panel_opera'>");
                                    stbHtml.Append("<div class='opera_control'>");
                                    stbHtml.AppendFormat("<span class='btn' onclick=\"javascript:PicPreview('questionAttrAll.aspx?resourceid={0}&questionid={1}&stuId={2}&attrType=AnalyzeHtml','解析');\"><i class='material-icons'>&#xE8B0;</i>&nbsp;解析</span>"
                                        , rtrfId
                                        , dt.Rows[i]["TestQuestions_Id"]
                                        , stuId);
                                    if (Rc.Cloud.Web.Common.pfunction.GetWebMdlIsShow("RecordingComment"))
                                    {
                                        string audiofilePath = string.Format("/Upload/Resource/teacherRecording/{0}/{1}/{2}.wav"
                       , pfunction.ToShortDate(hwCTime), shwId, dt.Rows[i]["TestQuestions_Id"]);
                                        if (File.Exists(HttpContext.Current.Server.MapPath(audiofilePath)))
                                        {
                                            stbHtml.AppendFormat("<span class='btn' onclick=\"javascript:RecordingComment('{0}')\"><i class='material-icons'>&#xE31D;</i>&nbsp;语音批注</span>", dt.Rows[i]["TestQuestions_Id"]);
                                        }
                                    }
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");
                                }

                                stbHtml.Append("<div class=\"panel_other\">");
                                stbHtml.Append("<dl class='other_attr'>");
                                stbHtml.Append("<dt>批注：</dt>");
                                stbHtml.AppendFormat("<dd>{0}</dd>"
                                    , string.Format(drScore.Length == 0 ? "" : "{0}"
                                    , string.IsNullOrEmpty(drScore[0]["Comment"].ToString()) ? "-" : drScore[0]["Comment"]));

                                string strAvgScore = string.Empty;//平均得分
                                string strContentText = string.Empty;//知识内容
                                string strTargetText = string.Empty;//测量目标                            
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    if (pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()) != "")
                                    {
                                        strAvgScore += string.Format("<div class='multi_panel'>{0}<div class='panel_content'>{1}</div></div>"
                                            , (dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "fill") && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("<div class='panel_left'>(" + drScore[ii]["testIndex"].ToString() + ")</div>") : ""
                                            , string.IsNullOrEmpty(GetAvgScore(drScore[ii]["TestQuestions_Score_ID"].ToString())) ? "-" : GetAvgScore(drScore[ii]["TestQuestions_Score_ID"].ToString()));
                                        strContentText += string.Format("<div class='multi_panel'>{0}<div class='panel_content'>{1}</div></div>"
                                            , (dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "fill") && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("<div class='panel_left'>(" + drScore[ii]["testIndex"].ToString() + ")</div>") : ""
                                            , string.IsNullOrEmpty(drScore[ii]["ContentText"].ToString()) ? "-" : drScore[ii]["ContentText"].ToString());
                                        strTargetText += string.Format("<div class='multi_panel'>{0}<div class='panel_content'>{1}</div></div>"
                                            , (dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "fill") && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("<div class='panel_left'>(" + drScore[ii]["testIndex"].ToString() + ")</div>") : ""
                                            , string.IsNullOrEmpty(drScore[ii]["TargetText"].ToString()) ? "-" : drScore[ii]["TargetText"].ToString());
                                    }
                                }
                                stbHtml.AppendFormat("<dt>平均得分：</dt><dd>{0}</dd>", strAvgScore);
                                stbHtml.AppendFormat("<dt>知识内容：</dt><dd>{0}</dd>", strContentText);
                                stbHtml.AppendFormat("<dt>测量目标：</dt><dd>{0}</dd>", strTargetText);

                                //stbHtml.Append("<li><a href='##'>强化训练</a></li>");
                                stbHtml.Append("</div>");

                                stbHtml.Append("</div>");


                                stbHtml.Append("</div>");
                            }
                            else
                            {
                                //题干
                                string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"));
                                strTestQuestionBody = pfunction.NoHTML(strTestQuestionBody);
                                if (string.IsNullOrEmpty(strTestQuestionBody))
                                {
                                    strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "textTitle", dt.Rows[i]["TestQuestions_Id"], "txt"));
                                }
                                stbHtml.Append("<div class=\"question_type_panel\">");
                                stbHtml.AppendFormat("<div class=\"panel_heading\">{0}</div>", strTestQuestionBody);
                                stbHtml.Append("</div>");
                            }
                        }
                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        break;
                    case "testpaper_view_new":
                        #region 作业预览 /teacher/TestpaperView.aspx
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        tchId = context.Request["tchId"].Filter();

                        strSqlScoreFormat = string.Format(@"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex 
from TestQuestions_Score tqs  where tqs.ResourceToResourceFolder_Id='{0}'", rtrfId);
                        dtScore = DbHelperSQL.Query(strSqlScoreFormat).Tables[0];

                        strSql = @"select TQ.* from TestQuestions TQ where tq.ResourceToResourceFolder_Id='" + rtrfId + "' order by TestQuestions_Num ";
                        dt = DbHelperSQL.Query(strSql).Tables[0];
                        modelRTRF = new Rc.BLL.Resources.BLL_ResourceToResourceFolder().GetModel(rtrfId);

                        #region 判断老师是否购买过资源
                        IsBuy = true;
                        #endregion
                        if (modelRTRF == null)
                        {
                            context.Response.Write("数据不存在或已删除");
                            context.Response.End();
                        }

                        //生成存储路径
                        savePath = string.Empty;
                        saveOwnPath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                        }
                        fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径

                        stbHtml.AppendFormat("<h4 class=\"ph alert alert-info\">{0}</h4>", GetRFNameByRTRFId(rtrfId));

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dt.Rows[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");

                            //题干
                            string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"));

                            //选择题、完形填空题选项
                            string strOption = string.Empty;
                            if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest")
                            {
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    //从文件读取选择题选项
                                    string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                    List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                    if (listTestSelections != null && listTestSelections.Count > 0)
                                    {
                                        foreach (var item in listTestSelections)
                                        {
                                            if (!string.IsNullOrEmpty(item.selectionHTML)) strOption += string.Format("<div class=\"col-xs-6\">{0}</div>", pfunction.NoHTML(item.selectionHTML));
                                        }
                                        if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                        {
                                            strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                        }
                                    }
                                }
                            }

                            if (dt.Rows[i]["TestQuestions_Type"].ToString() != "title" && dt.Rows[i]["TestQuestions_Type"].ToString() != "")
                            {
                                stbHtml.AppendFormat("<div class=\"panel panel-default\"><a name=\"{0}\"></a>", dt.Rows[i]["TestQuestions_Num"].ToString().TrimEnd('.'));
                                stbHtml.Append("<div class='panel-body'>");
                                stbHtml.AppendFormat("<div>{0}</div><div class='row clearfix'>{1}</div>"
                                    , pfunction.NoHTML(strTestQuestionBody) //题干
                                    , strOption //选项
                                    );

                                stbHtml.Append("</div>");
                                stbHtml.Append("<div class='panel-footer question_info'>");
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    //解析
                                    string strAnalyzeHtml = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "AnalyzeHtml", drScore[ii]["TestQuestions_Score_ID"], "htm"));
                                    stbHtml.Append("<div class='row'>");
                                    //正确答案
                                    if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "truefalse")
                                    {
                                        stbHtml.AppendFormat("<div class='col-xs-2'><div class='question_info_heading'>【{0}分值】</div><div class='score'>{1}</div></div>"
                                            , dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                            , pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()));
                                        if (IsBuy)
                                        {
                                            stbHtml.AppendFormat("<div class='col-xs-5'><div class='question_info_heading'><span class='btn btn-success btn-sm' data-toggle='collapse' data-target='#anwser_{1}'>查看答案</span></div><div class='collapse' id='anwser_{1}'>{0}</div></div>"
                                                , drScore[ii]["TestCorrect"]
                                                , drScore[ii]["TestQuestions_Score_ID"]);
                                            stbHtml.AppendFormat("<div class='col-xs-5'><div class='question_info_heading'><span class='btn btn-default btn-sm' data-toggle='collapse' data-target='#analysis_{1}'>查看解析</span></div><div class='collapse' id='analysis_{1}'>{0}</div></div>"
                                                , strAnalyzeHtml
                                                , drScore[ii]["TestQuestions_Score_ID"]);

                                            //stbHtml.AppendFormat("<div class='col-xs-12 collapse' id='analysis_{0}'><div class='analysis'>这里面是解析内容</div></div>"
                                            //    , drScore[ii]["TestQuestions_Score_ID"]);
                                        }
                                    }
                                    if (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" || dt.Rows[i]["TestQuestions_Type"].ToString() == "answers")
                                    {
                                        //从文件读取正确答案
                                        string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                        if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                        {
                                            string strTQScore = pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString());
                                            stbHtml.AppendFormat("<div class='col-xs-2'><div class='question_info_heading'>【{0}分值】</div><div>{1}</div></div>"
                                                , (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString())) ? "(" + drScore[ii]["testIndex"].ToString() + ")" : ""
                                                , strTQScore);
                                            if (IsBuy)
                                            {
                                                stbHtml.AppendFormat("<div class='col-xs-5'><div class='question_info_heading'><span class='btn btn-success btn-sm' data-toggle='collapse' data-target='#anwser_{1}'>查看答案</span></div><div class='collapse' id='anwser_{1}'>{0}</div></div>"
                                                    , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或")
                                                    , drScore[ii]["TestQuestions_Score_ID"]);
                                                stbHtml.AppendFormat("<div class='col-xs-5'><div class='question_info_heading'><span class='btn btn-default btn-sm' data-toggle='collapse' data-target='#analysis_{1}'>查看解析</span></div><div class='collapse' id='analysis_{1}'>{0}</div></div>"
                                                    , strAnalyzeHtml
                                                    , drScore[ii]["TestQuestions_Score_ID"]);

                                                //stbHtml.AppendFormat("<div class='col-xs-12 collapse' id='analysis_{0}'><div class='analysis'>这里面是解析内容</div></div>"
                                                //    , drScore[ii]["TestQuestions_Score_ID"]);
                                            }

                                        }
                                    }
                                    stbHtml.Append("</div>");
                                }
                                stbHtml.Append("</div>");
                                stbHtml.Append("</div>");
                            }
                            else
                            {
                                string tqBody = pfunction.NoHTML(strTestQuestionBody);
                                if (string.IsNullOrEmpty(tqBody))
                                {
                                    tqBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "textTitle", dt.Rows[i]["TestQuestions_Id"], "txt"));
                                }
                                stbHtml.AppendFormat("<h4 class=\"p_t\">{0}</h4>", tqBody);
                            }

                        }
                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        break;
                    case "combination_testpaper_view":
                        #region 简易组卷 /teacher/CombinationTestPaper.aspx
                        stbHtml = new StringBuilder();
                        string Two_WayChecklist_Id = context.Request["Two_WayChecklist_Id"].Filter();
                        string RelationPaper_Id = context.Request["RelationPaper_Id"].Filter();
                        string Two_WayChecklist_Name = context.Request["Two_WayChecklist_Name"].Filter();

                        try
                        {
                            string Errmsg = string.Empty;
                            //双向细目表大标题
                            string sqlTWO = @"select * from Two_WayChecklistDetail where Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' and ParentId='0' order by TestQuestions_Num,CreateTime";
                            DataTable dtTWO = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTWO).Tables[0];
                            //双向细目表的明细
                            string sqlTWODetil = @"select * from Two_WayChecklistDetail where Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' and ParentId<>'0' order by TestQuestions_Num,CreateTime";
                            DataTable dtTWODetil = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTWODetil).Tables[0];
                            //题的
                            string sqlTQ = @"select tw.Two_WayChecklistDetail_Id,tw.Two_WayChecklist_Id,tw.ParentId,tw.TestQuestions_Num,tw.TestQuestions_NumStr
, tw.TestQuestions_Type,tw.CreateTime
,tw.Two_WayChecklistType,tq.*,rtf.ResourceToResourceFolder_Id,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject,rp.RelationPaperTemp_id from Two_WayChecklistDetail tw
left join RelationPaperTemp rp on rp.Two_WayChecklistDetail_Id=tw.Two_WayChecklistDetail_Id and rp.RelationPaper_Id='" + RelationPaper_Id + @"'
left join TestQuestions tq on tq.TestQuestions_Id=rp.TestQuestions_Id
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where tw.Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' order by tw.TestQuestions_Num,tw.CreateTime";
                            dt = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQ).Tables[0];
                            //分的
                            string sqlTQScore = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex from TestQuestions_Score tqs 
where tqs.ResourceToResourceFolder_Id in 
(select tq.ResourceToResourceFolder_Id from RelationPaperTemp rp left join TestQuestions tq on tq.TestQuestions_Id=rp.TestQuestions_Id
where RelationPaper_Id='" + RelationPaper_Id + "')";
                            dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQScore).Tables[0];

                            saveOwnPath = string.Empty;
                            //stbHtml.AppendFormat("<div class='form-horizontal'><div class='form-group'><label class='col-xs-1 paper_name'>试卷名称</label><div class='col-xs-11'><input type=\"text\" id='rename' class='form-control input-lg' value=\"{0}\" /></div></div></div>", pfunction.NoHTML(Two_WayChecklist_Name.Filter()));
                            stbHtml.Append("<div class='fixed_sidebar right mini bottom'><ul><li id='btnConfirm' class='active'><div class='link'>确认组卷</div></li><li id='btnAgain' class='active info'><div class='link'>重新组卷</div></li></ul></div>");
                            stbHtml.Append("<div class='panel_heading'>");
                            stbHtml.Append("<div class='test_paper_name_panel'><div class='panel_heading'><div class='panel_title'><input type=\"text\" id='rename' class='form-control input-lg text-center' placeholder='试卷名称' /></div></div></div>");
                            stbHtml.Append("</div>");


                            //< button type =\"button\" id=\"btnConfirm\" class=\"btn btn-success\">确<br>认<br>组<br>卷</button><button type=\"button\" id=\"btnAgain\" class=\"btn btn-primary\">重<br>新<br>组<br>卷</button>
                            stbHtml.Append("<div class='panel_body'>");
                            foreach (DataRow item in dtTWO.Rows)
                            {
                                stbHtml.AppendFormat("<div class=\"question_type_panel question-type-panel-hook\"><div class='panel_heading'><input type=\"text\" class='form-control question-type-input-hook' value=\"{0}\" /></div></div>", pfunction.NoHTML(item["TestQuestions_NumStr"].ToString()));
                                DataRow[] drTQ = dt.Select(" ParentId='" + item["Two_WayChecklistDetail_Id"].ToString() + "' and type='simple'", "TestQuestions_Num,CreateTime");
                                drTQBig = dt.Select(" ParentId='" + item["Two_WayChecklistDetail_Id"].ToString() + "' and type='complex'", "TestQuestions_Num,CreateTime");
                                DataRow[] drTQCount = dtTWODetil.Select(" ParentId='" + item["Two_WayChecklistDetail_Id"].ToString() + "' and Two_WayChecklistType='simple'", "TestQuestions_Num,CreateTime");
                                DataRow[] drTQBigCount = dtTWODetil.Select(" ParentId='" + item["Two_WayChecklistDetail_Id"].ToString() + "' and Two_WayChecklistType='complex'", "TestQuestions_Num,CreateTime");
                                #region 普通题
                                if (drTQ.Length > 0)
                                {
                                    if (drTQ.Length == drTQCount.Length)
                                    {
                                        for (int i = 0; i < drTQ.Length; i++)
                                        {
                                            //生成存储路径
                                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                               , drTQ[i]["ParticularYear"].ToString()
                                               , drTQ[i]["GradeTerm"].ToString()
                                               , drTQ[i]["Resource_Version"].ToString()
                                               , drTQ[i]["Subject"].ToString());
                                            fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                                            DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + drTQ[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                            //题干
                                            string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", drTQ[i]["TestQuestions_Id"], "htm"));
                                            //选择题、完形填空题选项
                                            string strOption = string.Empty;
                                            if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest")
                                            {
                                                for (int ii = 0; ii < drScore.Length; ii++)
                                                {
                                                    //从文件读取选择题选项
                                                    string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                    List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                    if (listTestSelections != null && listTestSelections.Count > 0)
                                                    {
                                                        foreach (var itemSelections in listTestSelections)
                                                        {
                                                            if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                        }
                                                        if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                        {
                                                            strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                        }
                                                    }
                                                }
                                            }
                                            if (drTQ[i]["TestQuestions_Type"].ToString() != "title" && drTQ[i]["TestQuestions_Type"].ToString() != "")
                                            {
                                                stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\" id='change_{1}' data-value='{1}'><a name=\"{0}\"></a>", drTQ[i]["TestQuestions_Num"].ToString().TrimEnd('.'), drTQ[i]["RelationPaperTemp_id"].ToString());
                                                stbHtml.AppendFormat("<div class='panel_stem panel-stem-hook'>{0}</div><div class='panel_option'>{1}</div>"
                                                    , pfunction.NoHTML(strTestQuestionBody) //题干
                                                    , strOption //选项
                                                    );

                                                stbHtml.Append("<div class='panel_answer'>");
                                                stbHtml.Append("<div class='answer_heading'>");
                                                stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-sm btn-block'>分值</span></div>");
                                                stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                                stbHtml.Append("</div>");
                                                for (int ii = 0; ii < drScore.Length; ii++)
                                                {
                                                    stbHtml.Append("<div class='answer_body'>");
                                                    //正确答案
                                                    if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" || drTQ[i]["TestQuestions_Type"].ToString() == "truefalse")
                                                    {
                                                        stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                            , drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                            , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                        stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                            , drScore[ii]["TestCorrect"]);

                                                    }
                                                    if (drTQ[i]["TestQuestions_Type"].ToString() == "fill" || drTQ[i]["TestQuestions_Type"].ToString() == "answers")
                                                    {
                                                        //从文件读取正确答案
                                                        string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                        if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                        {
                                                            stbHtml.AppendFormat("<div class='answer_item score'>{0}{1}</div>"
                                                                , drTQ[i]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                            stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                                , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));


                                                        }
                                                    }
                                                    stbHtml.Append("</div>");
                                                }
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("<div class='panel_opera'>");
                                                stbHtml.Append("<div class='opera_control'>");
                                                stbHtml.AppendFormat("<span id='ischange_{0}' class='btn ischange-hook' tt='{0}'><i class='material-icons'>&#xE627;</i>&nbsp;随机换题</span>", drTQ[i]["RelationPaperTemp_id"].ToString());
                                                stbHtml.AppendFormat("<span id='changeTQ_{0}' class='btn changetq-hook' tt='{0}'><i class='material-icons'>&#xE1B3;</i>&nbsp;精准选题</span>", drTQ[i]["RelationPaperTemp_id"].ToString());
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");
                                            }
                                            else
                                            {
                                                stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\"><div class='panel_stem'>{0}</div></div>", pfunction.NoHTML(strTestQuestionBody));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Errmsg = "双向细目表【" + Two_WayChecklist_Name + "】的大标题【" + item["TestQuestions_NumStr"].ToString() + "】下的明细存在没有相对应的试题，请检查此双向细目表";
                                        context.Response.Write(Errmsg);
                                        context.Response.End();
                                    }
                                }

                                #endregion
                                #region 综合题
                                if (drTQBig.Length > 0)
                                {
                                    if (drTQBig.Length == drTQBigCount.Length)
                                    {
                                        for (int i = 0; i < drTQBig.Length; i++)
                                        {
                                            string sql = @" select * from ( select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where Parent_Id='" + drTQBig[i]["TestQuestions_id"].ToString() + @"'
union 
select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where TestQuestions_id='" + drTQBig[i]["TestQuestions_id"].ToString() + "' ) t order by TestQuestions_Num";
                                            DataTable dtBig = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                                            if (dtBig.Rows.Count > 0)
                                            {
                                                for (int j = 0; j < dtBig.Rows.Count; j++)
                                                {
                                                    //生成存储路径
                                                    savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                                       , dtBig.Rows[j]["ParticularYear"].ToString()
                                                       , dtBig.Rows[j]["GradeTerm"].ToString()
                                                       , dtBig.Rows[j]["Resource_Version"].ToString()
                                                       , dtBig.Rows[j]["Subject"].ToString());
                                                    fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                                                    DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dtBig.Rows[j]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                                    //题干
                                                    string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dtBig.Rows[j]["TestQuestions_Id"], "htm"));
                                                    //选择题、完形填空题选项
                                                    string strOption = string.Empty;
                                                    if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "selection" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest")
                                                    {
                                                        for (int ii = 0; ii < drScore.Length; ii++)
                                                        {
                                                            //从文件读取选择题选项
                                                            string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                            List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                            if (listTestSelections != null && listTestSelections.Count > 0)
                                                            {
                                                                foreach (var itemSelections in listTestSelections)
                                                                {
                                                                    if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                                }
                                                                if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                                {
                                                                    strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (dtBig.Rows[j]["TestQuestions_Type"].ToString() != "title" && dtBig.Rows[j]["TestQuestions_Type"].ToString() != "")
                                                    {
                                                        stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\"><a name=\"{0}\"></a>", dtBig.Rows[j]["TestQuestions_Num"].ToString().TrimEnd('.'));
                                                        stbHtml.AppendFormat("<div class='panel_stem panel-stem-hook'>{0}</div><div class='panel_option'>{1}</div>"
                                                            , pfunction.NoHTML(strTestQuestionBody) //题干
                                                            , strOption //选项
                                                            );

                                                        stbHtml.Append("<div class='panel_answer'>");
                                                        stbHtml.Append("<div class='answer_heading'>");
                                                        stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-sm btn-block'>分值</span></div>");
                                                        stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                                        stbHtml.Append("</div>");
                                                        for (int ii = 0; ii < drScore.Length; ii++)
                                                        {
                                                            stbHtml.Append("<div class='answer_body'>");
                                                            //正确答案
                                                            if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "selection" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "truefalse")
                                                            {
                                                                stbHtml.AppendFormat("<div class='answer_item score'>{0}{1}</div>"
                                                                    , dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                    , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                                stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0} 分</div>"
                                                                    , drScore[ii]["TestCorrect"]);

                                                            }
                                                            if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "fill" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "answers")
                                                            {
                                                                //从文件读取正确答案
                                                                string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                                if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                                {
                                                                    stbHtml.AppendFormat("<div class='answer_item score'>{0}{1}</div>"
                                                                        , dtBig.Rows[j]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                        , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                                    stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                                        , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));


                                                                }
                                                            }
                                                            stbHtml.Append("</div>");
                                                        }
                                                        stbHtml.Append("</div>");
                                                        stbHtml.Append("</div>");

                                                    }
                                                    else
                                                    {
                                                        stbHtml.AppendFormat("<div id='change_{1}'><div class=\"question_panel question-panel-hook\"><div class='panel_stem'>{0}</div>", pfunction.NoHTML(strTestQuestionBody), drTQBig[i]["RelationPaperTemp_id"].ToString());

                                                        stbHtml.Append("<div class='panel_opera'>");
                                                        stbHtml.Append("<div class='opera_control'>");
                                                        if (dtBig.Rows[j]["TestQuestions_Type"].ToString() != "title") stbHtml.AppendFormat("<span id='ischange_{0}' class='btn ischange-hook' tt='{0}'><i class='material-icons'>&#xE627;</i>&nbsp;随机换题</span>", drTQBig[i]["RelationPaperTemp_id"].ToString());
                                                        stbHtml.AppendFormat("<span id='changeTQ_{0}' class='btn changetq-hook' tt='{0}'><i class='material-icons'>&#xE1B3;</i>&nbsp;精准选题</span>", drTQBig[i]["RelationPaperTemp_id"].ToString());
                                                        stbHtml.Append("</div>");
                                                        stbHtml.Append("</div>");
                                                        stbHtml.Append("</div>");
                                                    }
                                                }
                                                stbHtml.Append("</div>");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Errmsg = "双向细目表【" + Two_WayChecklist_Name + "】的大标题【" + item["TestQuestions_NumStr"].ToString() + "】下的明细存在没有相对应的试题，请检查此双向细目表";
                                        context.Response.Write(Errmsg);
                                        context.Response.End();
                                    }
                                }

                                #endregion
                            }
                            stbHtml.Append("</div>");

                            context.Response.Write(stbHtml.ToString());

                        }
                        catch (Exception ex)
                        {
                            Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(Two_WayChecklist_Id, "", string.Format("加载试卷失败：|唯一标识{0}|双向细目表Id{1}|方法{2}|错误信息{3}", RelationPaper_Id, Two_WayChecklist_Id, "GetTestPaper", ex.Message.ToString()));
                            context.Response.Write("简易组卷：加载试卷失败");
                        }
                        #endregion
                        break;
                    case "combination_testpaper_tq_list":
                        #region 简易组卷 手动选题 试题列表 /teacher/SelectQuestions.aspx
                        stbHtml = new StringBuilder();
                        string RelationPaperTemp_id = context.Request["RelationPaperTemp_id"].Filter();
                        Model_RelationPaperTemp modelRPT = new BLL_RelationPaperTemp().GetModel(RelationPaperTemp_id);
                        string Two_WayChecklistDetail_Id = string.Empty;
                        string TestQuestions_Id = string.Empty;
                        if (modelRPT == null)
                        {
                            context.Response.Write("<tr><td colspan='100'>数据不存在或已删除</td></tr>");
                            context.Response.End();
                        }
                        Two_WayChecklistDetail_Id = modelRPT.Two_WayChecklistDetail_Id;
                        TestQuestions_Id = modelRPT.TestQuestions_Id;

                        Model_Two_WayChecklistDetail mdoelTWCD = new BLL_Two_WayChecklistDetail().GetModel(modelRPT.Two_WayChecklistDetail_Id);

                        try
                        {
                            string temp = "<tr><td><a href=\"javascript:;\" data-name=\"selectTQ\" data-value=\"{1}\"  data-tq=\"{2}\">选择</a></td><td>{0}</td></tr>";
                            temp = "<div class=\"tab-pane {0}\" id=\"{1}\">";
                            temp += "{3}";
                            temp += "<div class='panel_opera'><div class='opera_control'><span class='btn' data-name=\"selectTQ\" data-value=\"{2}\"  data-tq=\"{1}\"><i class='material-icons'>&#xE065;</i>&nbsp;选择</span></div></div>";
                            temp += "</div>";

                            #region 数据源
                            //除了已存在之外的所有题
                            string sqlTQ = string.Format(@" select tq.*,twtq.Two_WayChecklistDetail_Id,ParticularYear,GradeTerm,Resource_Version,[Subject] 
from Two_WayChecklistDetailToTestQuestions twtq
inner join TestQuestions tq on tq.TestQuestions_Id=twtq.TestQuestions_Id
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where Two_WayChecklistDetail_Id='{0}' and twtq.TestQuestions_Id<>'{1}'"
                                , Two_WayChecklistDetail_Id
                                , TestQuestions_Id);
                            dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQ).Tables[0];

                            // 综合题子题
                            string strTQ_Sub = string.Format(@" select tq2.*,twtq.Two_WayChecklistDetail_Id,ParticularYear,GradeTerm,Resource_Version,[Subject] 
from Two_WayChecklistDetailToTestQuestions twtq
inner join TestQuestions tq on tq.TestQuestions_Id=twtq.TestQuestions_Id
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
inner join TestQuestions tq2 on tq2.Parent_Id=tq.TestQuestions_Id
where Two_WayChecklistDetail_Id='{0}' and twtq.TestQuestions_Id<>'{1}'"
                                , Two_WayChecklistDetail_Id
                                , TestQuestions_Id);
                            DataTable dtTQ_Sub = Rc.Common.DBUtility.DbHelperSQL.Query(strTQ_Sub).Tables[0];

                            // 分值数据
                            string strTQ_Score = string.Format(@" 
select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex 
from (
select tq.TestQuestions_Id 
from Two_WayChecklistDetailToTestQuestions twtq
inner join TestQuestions tq on tq.TestQuestions_Id=twtq.TestQuestions_Id
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where Two_WayChecklistDetail_Id='{0}' and twtq.TestQuestions_Id<>'{1}'
union all 
select tq2.TestQuestions_Id 
from Two_WayChecklistDetailToTestQuestions twtq
inner join TestQuestions tq on tq.TestQuestions_Id=twtq.TestQuestions_Id
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
inner join TestQuestions tq2 on tq2.Parent_Id=tq.TestQuestions_Id
where Two_WayChecklistDetail_Id='{0}' and twtq.TestQuestions_Id<>'{1}'
) t inner join TestQuestions_Score tqs on tqs.TestQuestions_Id=t.TestQuestions_Id 
", Two_WayChecklistDetail_Id, TestQuestions_Id);
                            dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strTQ_Score).Tables[0];
                            #endregion

                            //生成存储路径
                            savePath = string.Empty;
                            saveOwnPath = string.Empty;
                            StringBuilder stbHtmlResult = new StringBuilder();

                            if (dtTQ.Rows.Count > 0)
                            {
                                int inum = 0;
                                foreach (DataRow item in dtTQ.Rows)
                                {
                                    stbHtml = new StringBuilder();
                                    inum++;
                                    savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"], item["GradeTerm"], item["Resource_Version"], item["Subject"]);
                                    fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                                    //fileUrl = "http://testpaper.tuiti.cn/Upload/Resource/" + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径

                                    //题干
                                    string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", item["TestQuestions_Id"], "htm"));
                                    //strTestQuestionBody = RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionBody", item["TestQuestions_Id"], "htm"), "", Encoding.UTF8, "Get");

                                    if (item["type"].ToString() == "simple")
                                    {
                                        #region 普通题型
                                        DataRow[] drScore = dtTQ_Score.Select("TestQuestions_Id='" + item["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                        //选择题、完形填空题选项
                                        string strOption = string.Empty;
                                        if (item["TestQuestions_Type"].ToString() == "selection" || item["TestQuestions_Type"].ToString() == "clozeTest")
                                        {
                                            for (int ii = 0; ii < drScore.Length; ii++)
                                            {
                                                //从文件读取选择题选项
                                                string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                //strTestQuestionOption = RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");

                                                List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                if (listTestSelections != null && listTestSelections.Count > 0)
                                                {
                                                    foreach (var itemSelections in listTestSelections)
                                                    {
                                                        if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                    }
                                                    if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                    {
                                                        strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                    }
                                                }
                                            }
                                        }
                                        if (item["TestQuestions_Type"].ToString() != "title" && item["TestQuestions_Type"].ToString() != "")
                                        {
                                            stbHtml.AppendFormat("<div class=\"question_panel ba\"><a name=\"{0}\"></a>", item["TestQuestions_Num"].ToString().TrimEnd('.'));
                                            stbHtml.AppendFormat("<div class='panel_stem'>{0}</div><div class='row clearfix'>{1}</div>"
                                                , pfunction.NoHTML(strTestQuestionBody) //题干
                                                , strOption //选项
                                                );

                                            stbHtml.Append("<div class='panel_answer'>");
                                            stbHtml.Append("<div class='answer_heading'>");
                                            stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-sm btn-block'>分值</span></div>");
                                            stbHtml.Append("<div class='answer_item reference_answer'><span class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                            stbHtml.Append("</div>");
                                            for (int ii = 0; ii < drScore.Length; ii++)
                                            {
                                                stbHtml.Append("<div class='answer_body'>");
                                                //正确答案
                                                if (item["TestQuestions_Type"].ToString() == "selection" || item["TestQuestions_Type"].ToString() == "clozeTest" || item["TestQuestions_Type"].ToString() == "truefalse")
                                                {
                                                    stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                        , item["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                        , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                    stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                        , drScore[ii]["TestCorrect"]);

                                                }
                                                if (item["TestQuestions_Type"].ToString() == "fill" || item["TestQuestions_Type"].ToString() == "answers")
                                                {
                                                    //从文件读取正确答案
                                                    string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                    //strTestQuestionCurrent = RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");

                                                    if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                    {
                                                        stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                            , item["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                            , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                        stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                            , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));


                                                    }
                                                }
                                                stbHtml.Append("</div>");
                                            }
                                            stbHtml.Append("</div>");
                                            stbHtml.Append("</div>");
                                        }
                                        else
                                        {
                                            stbHtml.AppendFormat("<div class=\"question_panel\"><div class='panel_stem'>{0}</div></div>", pfunction.NoHTML(strTestQuestionBody));
                                        }
                                        #endregion
                                    }
                                    else if (item["type"].ToString() == "complex")
                                    {
                                        stbHtml.AppendFormat("<div class='multiple_question'><div class=\"question_panel\"><div class='panel_stem'>{0}</div></div></div>", pfunction.NoHTML(strTestQuestionBody));
                                        #region 综合题型
                                        DataRow[] drTQ_Sub = dtTQ_Sub.Select("Parent_Id='" + item["TestQuestions_Id"] + "'", "TestQuestions_Num");
                                        foreach (var itemSub in drTQ_Sub)
                                        {
                                            DataRow[] drScore = dtTQ_Score.Select("TestQuestions_Id='" + itemSub["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                            //题干
                                            strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", itemSub["TestQuestions_Id"], "htm"));
                                            //strTestQuestionBody = RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionBody", itemSub["TestQuestions_Id"], "htm"), "", Encoding.UTF8, "Get");

                                            //选择题、完形填空题选项
                                            string strOption = string.Empty;
                                            if (itemSub["TestQuestions_Type"].ToString() == "selection" || itemSub["TestQuestions_Type"].ToString() == "clozeTest")
                                            {
                                                for (int ii = 0; ii < drScore.Length; ii++)
                                                {
                                                    //从文件读取选择题选项
                                                    string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                    //strTestQuestionOption = RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");

                                                    List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                    if (listTestSelections != null && listTestSelections.Count > 0)
                                                    {
                                                        foreach (var itemSelections in listTestSelections)
                                                        {
                                                            if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"col-xs-6\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                        }
                                                        if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                        {
                                                            strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                        }
                                                    }
                                                }
                                            }
                                            if (itemSub["TestQuestions_Type"].ToString() != "title" && itemSub["TestQuestions_Type"].ToString() != "")
                                            {
                                                stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\"><a name=\"{0}\"></a>", itemSub["TestQuestions_Num"].ToString().TrimEnd('.'));
                                                stbHtml.AppendFormat("<div class='question_stem'>{0}</div><div class='panel_option'>{1}</div>"
                                                    , pfunction.NoHTML(strTestQuestionBody) //题干
                                                    , strOption //选项
                                                    );

                                                stbHtml.Append("<div class='panel_answer'>");
                                                stbHtml.Append("<div class='answer_heading'>");
                                                stbHtml.Append("<div class='answer_item sorce'");
                                                stbHtml.Append("<span class='btn btn-info btn-sm btn-block'>分值</span>");
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("<div class='answer_item reference_score'");
                                                stbHtml.Append("<span class='btn btn-success btn-sm btn-block'>参考答案</span>");
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");
                                                for (int ii = 0; ii < drScore.Length; ii++)
                                                {
                                                    stbHtml.Append("<div class='answer_body'>");
                                                    //正确答案
                                                    if (itemSub["TestQuestions_Type"].ToString() == "selection" || itemSub["TestQuestions_Type"].ToString() == "clozeTest" || itemSub["TestQuestions_Type"].ToString() == "truefalse")
                                                    {
                                                        stbHtml.AppendFormat("<div class='answer_item sorce'>{0}{1} 分</div>"
                                                            , itemSub["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                            , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                        stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                            , drScore[ii]["TestCorrect"]);

                                                    }
                                                    if (itemSub["TestQuestions_Type"].ToString() == "fill" || itemSub["TestQuestions_Type"].ToString() == "answers")
                                                    {
                                                        //从文件读取正确答案
                                                        string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                        //strTestQuestionCurrent = RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");

                                                        if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                        {
                                                            stbHtml.AppendFormat("<div class='answer_item sorce'>{0}{1} 分</div>"
                                                                , itemSub["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                            stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                                , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));
                                                        }
                                                    }
                                                    stbHtml.Append("</div>");
                                                }
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");

                                            }
                                            else
                                            {
                                                stbHtml.AppendFormat("<div class='multiple_question'><div class=\"question_panel question-panel-hook\"><div class='panel_stem'>{0}</div></div></div>", pfunction.NoHTML(strTestQuestionBody));
                                            }

                                        }
                                        #endregion
                                        stbHtml.Append("</div>");
                                    }

                                    //stbHtml.AppendFormat(temp
                                    //   , pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", item["TestQuestions_Id"], "htm"))
                                    //   , RelationPaperTemp_id
                                    //   , item["TestQuestions_Id"].ToString());
                                    stbHtmlResult.AppendFormat(temp
                                        , inum == 1 ? "active" : ""
                                        , item["TestQuestions_Id"].ToString()
                                        , RelationPaperTemp_id
                                        , stbHtml.ToString()
                                        );
                                }
                                context.Response.Write(stbHtmlResult.ToString());
                            }
                            else
                            {
                                context.Response.Write("<tr><td colspan='100'>暂无可切换的试题</td></tr>");
                                context.Response.End();
                            }
                        }
                        catch (Exception ex)
                        {
                            Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(RelationPaperTemp_id, "", string.Format("加载数据失败：|唯一标识{0}|方法{1}|错误信息{2}", RelationPaperTemp_id, "LoadData()", ex.Message.ToString()));
                            context.Response.Write("<tr><td colspan='100'>加载数据失败err</td></tr>");
                        }
                        #endregion
                        break;
                    case "combination_testpaper_tq_change":
                        #region 简易组卷 换题 /teacher/CombinationTestPaper.aspx
                        stbHtml = new StringBuilder();
                        RelationPaperTemp_id = context.Request["RelationPaperTemp_id"].Filter();
                        string Type = context.Request["Type"].Filter();
                        string CreateUser = context.Request["CreateUser"].Filter();

                        try
                        {
                            int m = 0;
                            Two_WayChecklistDetail_Id = string.Empty;
                            Model_RelationPaperTemp model = new Model_RelationPaperTemp();
                            model = new BLL_RelationPaperTemp().GetModel(RelationPaperTemp_id);
                            if (model != null)
                            {
                                Two_WayChecklistDetail_Id = model.Two_WayChecklistDetail_Id;
                            }
                            if (Type == "1")
                            {
                                string sqlUpdate = string.Format(@" update RelationPaperTemp set TestQuestions_id=(select top 1  t1.TestQuestions_Id 
from (
select TestQuestions_Id from [dbo].[Two_WayChecklistDetailToTestQuestions]
where  Two_WayChecklistDetail_Id='{0}'
) t1
left join (select count(1) countTQ, TestQuestions_Id,CreateUser from [AlreadyedTQ_Teacher] group by TestQuestions_Id,CreateUser ) t2 on t2.TestQuestions_Id=t1.TestQuestions_Id and t2.CreateUser='{1}'
order by countTQ ,newid()
)
 where RelationPaperTemp_id='{2}'"
                                    , Two_WayChecklistDetail_Id
                                    , CreateUser
                                    , RelationPaperTemp_id);
                                m = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sqlUpdate);
                            }
                            else
                            {
                                m = 1;
                            }
                            if (m > 0)
                            {
                                string sqlTQ = @"select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.[Subject] from  TestQuestions tq
 left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_id=tq.ResourceToResourceFolder_Id
 where TestQuestions_Id=(select TestQuestions_Id from RelationPaperTemp where RelationPaperTemp_id='" + RelationPaperTemp_id + "')";
                                dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQ).Tables[0];
                                string sqlScore = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex from TestQuestions_Score tqs 
where tqs.ResourceToResourceFolder_Id =(select ResourceToResourceFolder_Id from TestQuestions where TestQuestions_Id=( select TestQuestions_Id from RelationPaperTemp where RelationPaperTemp_id='" + RelationPaperTemp_id + "')) ";
                                dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlScore).Tables[0];

                                saveOwnPath = string.Empty;
                                DataRow[] drTQ = dtTQ.Select("type='simple'", "TestQuestions_Num");
                                drTQBig = dtTQ.Select("type='complex'", "TestQuestions_Num");
                                #region 普通题
                                if (drTQ.Length > 0)
                                {
                                    for (int i = 0; i < drTQ.Length; i++)
                                    {
                                        //生成存储路径
                                        savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                           , drTQ[i]["ParticularYear"].ToString()
                                           , drTQ[i]["GradeTerm"].ToString()
                                           , drTQ[i]["Resource_Version"].ToString()
                                           , drTQ[i]["Subject"].ToString());
                                        fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                                        DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + drTQ[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                        //题干
                                        string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", drTQ[i]["TestQuestions_Id"], "htm"));
                                        //选择题、完形填空题选项
                                        string strOption = string.Empty;
                                        if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest")
                                        {
                                            for (int ii = 0; ii < drScore.Length; ii++)
                                            {
                                                //从文件读取选择题选项
                                                string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                if (listTestSelections != null && listTestSelections.Count > 0)
                                                {
                                                    foreach (var itemSelections in listTestSelections)
                                                    {
                                                        if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                    }
                                                    if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                    {
                                                        strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                    }
                                                }
                                            }
                                        }
                                        if (drTQ[i]["TestQuestions_Type"].ToString() != "title" && drTQ[i]["TestQuestions_Type"].ToString() != "")
                                        {
                                            stbHtml.AppendFormat("<div class='panel_stem panel-stem-hook'>{0}</div><div class='panel_option'>{1}</div>"
                                                , pfunction.NoHTML(strTestQuestionBody) //题干
                                                , strOption //选项
                                                );

                                            stbHtml.Append("<div class='panel_answer'>");
                                            stbHtml.Append("<div class='answer_heading'>");
                                            stbHtml.Append("<div class='answer_item score'>");
                                            stbHtml.Append("<span class='btn btn-info btn-sm btn-block'>分值</span>");
                                            stbHtml.Append("</div>");
                                            stbHtml.Append("<div class='answer_item reference_answer'>");
                                            stbHtml.Append("<span class='btn btn-success btn-sm'>参考答案</span>");
                                            stbHtml.Append("</div>");
                                            stbHtml.Append("<div class='answer_heading'></div>");
                                            stbHtml.Append("</div>");
                                            for (int ii = 0; ii < drScore.Length; ii++)
                                            {
                                                stbHtml.Append("<div class='answer_body'>");
                                                //正确答案
                                                if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" || drTQ[i]["TestQuestions_Type"].ToString() == "truefalse")
                                                {
                                                    stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                        , drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                        , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                    stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                        , drScore[ii]["TestCorrect"]);

                                                }
                                                if (drTQ[i]["TestQuestions_Type"].ToString() == "fill" || drTQ[i]["TestQuestions_Type"].ToString() == "answers")
                                                {
                                                    //从文件读取正确答案
                                                    string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                    if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                    {
                                                        stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                            , drTQ[i]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                            , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                        stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                            , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));


                                                    }
                                                }
                                                stbHtml.Append("</div>");
                                            }
                                            stbHtml.Append("</div>");

                                            stbHtml.Append("<div class='panel_opera'>");
                                            stbHtml.Append("<div class='opera_control'>");
                                            stbHtml.AppendFormat("<span id='ischange_{0}' class='btn ischange-hook' tt='{0}'><i class='material-icons'>&#xE627;</i>&nbsp;随机换题</span>", RelationPaperTemp_id);
                                            stbHtml.AppendFormat("<span id='changeTQ_{0}' class='btn changetq-hook' tt='{0}'><i class='material-icons'>&#xE1B3</i>&nbsp;精准选题</span>", RelationPaperTemp_id);
                                            stbHtml.Append("</div>");
                                            stbHtml.Append("</div>");
                                        }
                                        else
                                        {
                                            stbHtml.AppendFormat("<div class=\"question_panel\"><div class='panel_stem'>{0}</div></div>", pfunction.NoHTML(strTestQuestionBody));
                                        }
                                    }
                                }
                                #endregion
                                #region 综合题
                                if (drTQBig.Length > 0)
                                {

                                    for (int i = 0; i < drTQBig.Length; i++)
                                    {
                                        string sql = @" select * from ( select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where Parent_Id='" + drTQBig[i]["TestQuestions_id"].ToString() + @"'
union 
select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where TestQuestions_id='" + drTQBig[i]["TestQuestions_id"].ToString() + "' ) t order by TestQuestions_Num";
                                        DataTable dtBig = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                                        if (dtBig.Rows.Count > 0)
                                        {
                                            for (int j = 0; j < dtBig.Rows.Count; j++)
                                            {
                                                //生成存储路径
                                                savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                                   , dtBig.Rows[j]["ParticularYear"].ToString()
                                                   , dtBig.Rows[j]["GradeTerm"].ToString()
                                                   , dtBig.Rows[j]["Resource_Version"].ToString()
                                                   , dtBig.Rows[j]["Subject"].ToString());
                                                fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                                                DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dtBig.Rows[j]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                                //题干
                                                string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dtBig.Rows[j]["TestQuestions_Id"], "htm"));
                                                //选择题、完形填空题选项
                                                string strOption = string.Empty;
                                                if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "selection" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest")
                                                {
                                                    for (int ii = 0; ii < drScore.Length; ii++)
                                                    {
                                                        //从文件读取选择题选项
                                                        string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                        List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                        if (listTestSelections != null && listTestSelections.Count > 0)
                                                        {
                                                            foreach (var itemSelections in listTestSelections)
                                                            {
                                                                if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                            }
                                                            if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                            {
                                                                strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                            }
                                                        }
                                                    }
                                                }
                                                if (dtBig.Rows[j]["TestQuestions_Type"].ToString() != "title" && dtBig.Rows[j]["TestQuestions_Type"].ToString() != "")
                                                {
                                                    stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\">");
                                                    stbHtml.AppendFormat("<div class='panel_stem'>{0}</div><div class='panel_option'>{1}</div>"
                                                        , pfunction.NoHTML(strTestQuestionBody) //题干
                                                        , strOption //选项
                                                        );

                                                    stbHtml.Append("<div class='panel_answer'>");
                                                    stbHtml.Append("<div class='answer_heading'>");
                                                    stbHtml.Append("<div class='answer_item score'>");
                                                    stbHtml.Append("<span class='btn btn-info btn-sm btn-block'>分值</span>");
                                                    stbHtml.Append("</div>");
                                                    stbHtml.Append("<div class='answer_item reference_answer'>");
                                                    stbHtml.Append("<span class='btn btn-success btn-sm'>参考答案</span>");
                                                    stbHtml.Append("</div>");
                                                    stbHtml.Append("</div>");
                                                    for (int ii = 0; ii < drScore.Length; ii++)
                                                    {
                                                        stbHtml.Append("<div class='answer_body'>");
                                                        //正确答案
                                                        if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "selection" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "truefalse")
                                                        {
                                                            stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                                , dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                            stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                                , drScore[ii]["TestCorrect"]);

                                                        }
                                                        if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "fill" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "answers")
                                                        {
                                                            //从文件读取正确答案
                                                            string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                            if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                            {
                                                                stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                                    , dtBig.Rows[j]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                    , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                                stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                                    , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));
                                                            }
                                                        }
                                                        stbHtml.Append("</div>");
                                                        stbHtml.Append("</div>");
                                                    }
                                                    stbHtml.Append("</div>");
                                                    stbHtml.Append("</div>");
                                                }
                                                else
                                                {
                                                    stbHtml.AppendFormat("<div id='change_{1}' class='multiple_question'><div class=\"question_panel question-panel-hook\"><div class='panel_stem'>{0}</div>", pfunction.NoHTML(strTestQuestionBody), RelationPaperTemp_id);
                                                    stbHtml.Append("<div class='panel_opera'>");
                                                    stbHtml.Append("<div class='opera_control'>");
                                                    stbHtml.AppendFormat("<span id='ischange_{0}' class='btn ischange-hook' tt='{0}'><i class='material-icons'>&#xE627;</i>&nbsp;随机换题</span>", RelationPaperTemp_id);
                                                    stbHtml.AppendFormat("<span id='changeTQ_{0}' class='btn changetq-hook' tt='{0}'><i class='material-icons'>&#xE1B3;</i>&nbsp;精准选题</span>", RelationPaperTemp_id);
                                                    stbHtml.Append("</div>");
                                                    stbHtml.Append("</div>");
                                                    stbHtml.Append("</div>");
                                                }
                                            }
                                            stbHtml.Append("</div>");
                                        }
                                    }
                                }
                                #endregion

                                context.Response.Write(stbHtml.ToString());
                            }
                            else
                            {
                                context.Response.Write("换题失败");
                                context.Response.End();
                            }
                        }
                        catch (Exception ex)
                        {
                            Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(RelationPaperTemp_id, "", string.Format("换题失败：|唯一标识{0}|双向细目表Id{1}|方法{2}|错误信息{3}", RelationPaperTemp_id, "", "ChangeTQ", ex.Message.ToString()));
                            context.Response.Write("换题失败err");
                        }
                        #endregion
                        break;
                    case "combination_testpaper_confirm":
                        #region 简易组卷 确认组卷 /teacher/CombinationTestPaper.aspx
                        stbHtml = new StringBuilder();
                        Two_WayChecklist_Id = context.Request["Two_WayChecklist_Id"].Filter();
                        RelationPaper_Id = context.Request["RelationPaper_Id"].Filter();
                        string Name = context.Request["Name"].Filter();
                        string Titles = context.Request["Titles"].Filter();
                        CreateUser = context.Request["CreateUser"].Filter();

                        try
                        {
                            string[] ArrTitle = Titles.Split('♧');
                            StringBuilder stbSql = new StringBuilder();

                            #region 资源
                            string RID = Guid.NewGuid().ToString();
                            string ReID = Guid.NewGuid().ToString();
                            string ReTime = DateTime.Now.ToString();

                            #region 插入 Resource
                            stbSql.AppendFormat(@"INSERT INTO [dbo].[Resource]([Resource_Id],[Resource_MD5],[Resource_DataStrem],[Resource_ContentHtml],[CreateTime],[Resource_ContentLength])VALUES('{0}','{1}','{2}','{3}','{4}','{5}');", RID, "", "", "", DateTime.Now, "0");
                            #endregion
                            #region 插入 ResourceToResourceFolder
                            stbSql.AppendFormat(@"INSERT INTO [dbo].[ResourceToResourceFolder]
           ([ResourceToResourceFolder_Id]
           ,[ResourceFolder_Id]
           ,[Resource_Id]
           ,[File_Name]
           ,[Resource_Type]
           ,[Resource_Name]
           ,[Resource_Class]
           ,[Resource_Version]
           ,[File_Owner]
           ,[CreateFUser]
           ,[CreateTime]
           ,[UpdateTime]
           ,[File_Suffix]
           ,[LessonPlan_Type]
           ,[GradeTerm]
           ,[Subject]
           ,[Resource_Domain]
           ,[Resource_Url]
           ,[Resource_shared]
           ,[Book_ID]
           ,[ParticularYear]
           ,[ResourceToResourceFolder_Order])
     VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}');"
                                , ReID
                                , "D7856701-8285-4458-A23F-1952923544EB"
                                , RID
                                , Name
                                , Resource_TypeConst.testPaper类型文件
                                , Name
                                , Resource_ClassConst.自有资源
                                , ""
                                , CreateUser
                                , CreateUser
                                , ReTime
                                , ""
                                , "testPaper"
                                , LessonPlan_TypeConst.组卷
                                , ""
                                , ""
                                , ""
                                , ""
                                , ""
                                , ""
                                , ""
                                , 0);
                            #endregion
                            #endregion
                            //题的
                            string sqlTQ = @"select tw.Two_WayChecklistDetail_Id,tw.Two_WayChecklist_Id,tw.ParentId,tw.TestQuestions_Num,tw.TestQuestions_NumStr
, tw.TestQuestions_Type,tw.CreateTime
,tw.Two_WayChecklistType,tq.*,rtf.ResourceToResourceFolder_Id,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject,rp.RelationPaperTemp_id from Two_WayChecklistDetail tw
left join RelationPaperTemp rp on rp.Two_WayChecklistDetail_Id=tw.Two_WayChecklistDetail_Id and rp.RelationPaper_Id='" + RelationPaper_Id + @"'
left join TestQuestions tq on tq.TestQuestions_Id=rp.TestQuestions_Id
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where tw.Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' order by tw.TestQuestions_Num,tw.CreateTime";
                            dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQ).Tables[0];
                            //分的
                            string sqlTQScore = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex,tqs.AnalyzeHyperlinkData,tqs.TrainHyperlinkData,tqs.TestType from TestQuestions_Score tqs 
where tqs.TestQuestions_Id in 
(select rp.TestQuestions_Id from RelationPaperTemp rp
where RelationPaper_Id='" + RelationPaper_Id + "')";
                            dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQScore).Tables[0];

                            saveOwnPath = string.Empty;
                            string TempTQ = @"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type])
     VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');";
                            int i = 0;
                            int Num = 1;
                            //父级普通TQ
                            DataRow[] drTQtitle = dtTQ.Select("ParentId='0'", "TestQuestions_Num");
                            //#region 存作业内容中的试卷名称
                            //string ReNameGuid = Guid.NewGuid().ToString();
                            //string strReName = string.Format(TempTQ
                            //        , ReNameGuid
                            //        , ReID
                            //        , 0
                            //        , "title"
                            //        , "0"
                            //        , ""
                            //        , ""
                            //        , DateTime.Now
                            //        , ""
                            //        , "0"
                            //        , "simple");
                            //stbSql.Append(strReName);//作业内容中的试卷名称
                            ////创建txt
                            //string filePath = string.Format("{0}{1}/{2}/{3}.txt", uploadPath, pfunction.ToShortDate(ReTime), "textTitle", ReNameGuid);
                            //pfunction.WriteToFile(filePath, Name, true);
                            //#endregion
                            foreach (DataRow itemtitle in drTQtitle)
                            {
                                #region 如果是标题
                                if (string.IsNullOrEmpty(itemtitle["TestQuestions_Id"].ToString()))
                                {
                                    string TQGuid = Guid.NewGuid().ToString();
                                    string strTQ = string.Format(TempTQ
                                    , TQGuid
                                    , ReID
                                    , Num
                                    , "title"
                                    , "0"
                                    , ""
                                    , ""
                                    , DateTime.Now
                                    , ""
                                    , "0"
                                    , "simple");
                                    stbSql.Append(strTQ);//插入标题（一.选择题）

                                    if (!string.IsNullOrEmpty(ArrTitle[i].ToString()))
                                    {
                                        //创建txt
                                        string filePath = string.Format("{0}{1}/{2}/{3}.txt", uploadPath, pfunction.ToShortDate(ReTime), "textTitle", TQGuid);
                                        pfunction.WriteToFile(filePath, ArrTitle[i].ToString(), true);
                                    }

                                    //子集TQ
                                    DataRow[] drSonTQ = dtTQ.Select(" ParentId='" + itemtitle["Two_WayChecklistDetail_Id"].ToString() + "'", "TestQuestions_Num");
                                    if (drSonTQ.Length > 0)
                                    {
                                        foreach (DataRow itemSon in drSonTQ)
                                        {
                                            Num++;
                                            #region 普通题
                                            if (itemSon["type"].ToString() == "simple" && itemSon["Two_WayChecklistType"].ToString() == "simple")
                                            {
                                                #region 插入题
                                                string TQSonGuid = Guid.NewGuid().ToString();
                                                stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type] )
     select '{0}','{1}','{2}',TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,'{3}','{4}','{6}',[type] from TestQuestions
	 where TestQuestions_Id='{5}';", TQSonGuid, ReID, Num, DateTime.Now.ToString(), itemSon["TestQuestions_NumStr"].ToString(), itemSon["TestQuestions_Id"].ToString(), TQGuid);
                                                #endregion
                                                #region 存文件
                                                //生成存储路径
                                                savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                                    , itemSon["ParticularYear"].ToString()
                                                    , itemSon["GradeTerm"].ToString()
                                                    , itemSon["Resource_Version"].ToString()
                                                    , itemSon["Subject"].ToString());

                                                fileUrl = uploadPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径
                                                string fileloactionurl = uploadPath + "{0}\\{1}\\" + "{2}.{3}";

                                                pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemSon["TestQuestions_Id"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQSonGuid, "txt"));

                                                pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemSon["TestQuestions_Id"], "htm")
                                                   , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQSonGuid, "htm"));

                                                #endregion
                                                DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + itemSon["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                                if (drScore.Length > 0)
                                                {
                                                    for (int jj = 0; jj < drScore.Length; jj++)
                                                    {
                                                        #region 插入分

                                                        string ScoreGuid = Guid.NewGuid().ToString();
                                                        string tempAnalyzeData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=AnalyzeData
", ReID, ScoreGuid);
                                                        string tempTrainData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=TrainData
", ReID, ScoreGuid);
                                                        stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Score]
           ([TestQuestions_Score_ID]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_OrderNum]
           ,[TestQuestions_Score]
           ,[AnalyzeHyperlink]
           ,[AnalyzeHyperlinkData]
           ,[AnalyzeHyperlinkHtml]
           ,[AnalyzeText]
           ,[ComplexityHyperlink]
           ,[ComplexityText]
           ,[ContentHyperlink]
           ,[ContentText]
           ,[DocBase64]
           ,[DocHtml]
           ,[ScoreHyperlink]
           ,[ScoreText]
           ,[TargetHyperlink]
           ,[TrainHyperlinkData]
           ,[TrainHyperlinkHtml]
           ,[TargetText]
           ,[TestCorrect]
           ,[TestType]
           ,[TrainHyperlink]
           ,[TrainText]
           ,[TypeHyperlink]
           ,[TypeText]
           ,[CreateTime]
           ,[AreaHyperlink]
           ,[AreaText]
           ,[kaofaText]
           ,[testIndex] )
     select '{0}','{1}','{2}',TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,'{5}',AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,'{6}',TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,'{3}',AreaHyperlink,AreaText,kaofaText,testIndex from TestQuestions_Score where TestQuestions_Score_ID='{4}' ;", ScoreGuid, ReID, TQSonGuid, DateTime.Now.ToString(), drScore[jj]["TestQuestions_Score_ID"].ToString()
                                                            , string.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()) ? "" : tempAnalyzeData
                                                            , string.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()) ? "" : tempTrainData);
                                                        #endregion
                                                        #region 存文件

                                                        #region 正确答案
                                                        //不是选择题填空判断下载正确答案
                                                        if (drScore[jj]["TestType"].ToString() == "fill" || drScore[jj]["TestType"].ToString() == "answers")
                                                        {
                                                            pfunction.CopyToFile(string.Format(fileUrl, "testQuestionCurrent", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionCurrent", ScoreGuid, "txt"));

                                                        }
                                                        #endregion
                                                        #region 选择题的选项
                                                        if (drScore[jj]["TestType"].ToString() == "selection" || drScore[jj]["TestType"].ToString() == "clozeTest")
                                                        {
                                                            pfunction.CopyToFile(string.Format(fileUrl, "testQuestionOption", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionOption", ScoreGuid, "txt"));

                                                        }
                                                        #endregion
                                                        #region 解析 保存文件
                                                        if (!String.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()))
                                                        {
                                                            pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "AnalyzeData", ScoreGuid, "txt"));

                                                            pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "AnalyzeHtml", ScoreGuid, "htm"));

                                                        }
                                                        #endregion
                                                        #region 强化训练 保存文件
                                                        if (!String.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()))
                                                        {
                                                            pfunction.CopyToFile(string.Format(fileUrl, "TrainData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "TrainData", ScoreGuid, "txt"));

                                                            pfunction.CopyToFile(string.Format(fileUrl, "TrainHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "TrainHtml", ScoreGuid, "htm"));

                                                        }
                                                        #endregion
                                                        #region 子题题干Html 保存文件bodySub
                                                        pfunction.CopyToFile(string.Format(fileUrl, "bodySub", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "bodySub", ScoreGuid, "htm"));
                                                        #endregion
                                                        #endregion
                                                        #region 插入选项
                                                        string sqlOption = "select * from TestQuestions_Option where TestQuestions_Score_ID='" + drScore[jj]["TestQuestions_Score_ID"] + "'";
                                                        DataTable dtOption = Rc.Common.DBUtility.DbHelperSQL.Query(sqlOption).Tables[0];
                                                        if (dtOption.Rows.Count > 0)
                                                        {
                                                            foreach (DataRow item in dtOption.Rows)
                                                            {
                                                                stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Option]
           ([TestQuestions_Option_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_OptionParent_OrderNum]
           ,[TestQuestions_Option_Content]
           ,[TestQuestions_Option_OrderNum]
           ,[CreateTime]
           ,[TestQuestions_Score_ID] )
     select Newid(),'{0}',TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,'{1}','{2}' from TestQuestions_Option where TestQuestions_Option_Id='{3}' ;", TQSonGuid, DateTime.Now.ToString(), ScoreGuid, item["TestQuestions_Option_Id"].ToString());
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                }
                                            }
                                            #endregion
                                            #region 综合题
                                            if (itemSon["type"].ToString() == "complex" && itemSon["Two_WayChecklistType"].ToString() == "complex")
                                            {
                                                #region 插入综合题提干
                                                #region 插入题
                                                string TQSonGuid = Guid.NewGuid().ToString();
                                                stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type] )
     select '{0}','{1}','{2}',TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,'{3}','{4}','0',[type] from TestQuestions
	 where TestQuestions_Id='{5}';", TQSonGuid, ReID, Num, DateTime.Now.ToString(), itemSon["TestQuestions_NumStr"].ToString(), itemSon["TestQuestions_Id"].ToString());
                                                #endregion
                                                #region 存文件
                                                //生成存储路径
                                                savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                                   , itemSon["ParticularYear"].ToString()
                                                   , itemSon["GradeTerm"].ToString()
                                                   , itemSon["Resource_Version"].ToString()
                                                   , itemSon["Subject"].ToString());
                                                fileUrl = uploadPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径
                                                string fileloactionurl = uploadPath + "{0}\\{1}\\" + "{2}.{3}";

                                                pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemSon["TestQuestions_Id"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQSonGuid, "txt"));

                                                pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemSon["TestQuestions_Id"], "htm")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQSonGuid, "htm"));

                                                #endregion
                                                #endregion
                                                #region 综合题小题
                                                string sql = @" select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where Parent_Id='" + itemSon["TestQuestions_id"].ToString() + "'order by TestQuestions_Num";
                                                DataTable dtBigTQ = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                                                string sqlScore = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex,tqs.AnalyzeHyperlinkData,tqs.TrainHyperlinkData,tqs.TestType from TestQuestions_Score tqs where tqs.TestQuestions_Id in(select TestQuestions_Id from TestQuestions 
where Parent_Id='" + itemSon["TestQuestions_id"].ToString() + "')";
                                                DataTable dtBigScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlScore).Tables[0];
                                                if (dtBigTQ.Rows.Count > 0)
                                                {
                                                    foreach (DataRow itemBigTQ in dtBigTQ.Rows)
                                                    {
                                                        Num++;
                                                        #region 插入题
                                                        string TQBGuid = Guid.NewGuid().ToString();
                                                        stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type] )
     select '{0}','{1}','{2}',TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,'{3}',topicNumber,'{4}',[type] from TestQuestions
	 where TestQuestions_Id='{5}';", TQBGuid, ReID, Num, DateTime.Now.ToString(), TQSonGuid, itemBigTQ["TestQuestions_Id"].ToString());
                                                        #endregion
                                                        #region 存文件
                                                        //生成存储路径
                                                        savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                                           , itemBigTQ["ParticularYear"].ToString()
                                                           , itemBigTQ["GradeTerm"].ToString()
                                                           , itemBigTQ["Resource_Version"].ToString()
                                                           , itemBigTQ["Subject"].ToString());
                                                        fileUrl = uploadPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径
                                                        fileloactionurl = uploadPath + "{0}\\{1}\\" + "{2}.{3}";

                                                        pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemBigTQ["TestQuestions_Id"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQBGuid, "txt"));

                                                        pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemBigTQ["TestQuestions_Id"], "htm")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQBGuid, "htm"));

                                                        #endregion
                                                        DataRow[] drScore = dtBigScore.Select("TestQuestions_Id='" + itemBigTQ["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                                        if (drScore.Length > 0)
                                                        {
                                                            for (int jj = 0; jj < drScore.Length; jj++)
                                                            {
                                                                #region 插入分

                                                                string ScoreGuid = Guid.NewGuid().ToString();
                                                                string tempAnalyzeData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=AnalyzeData
", ReID, ScoreGuid);
                                                                string tempTrainData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=TrainData
", ReID, ScoreGuid);
                                                                stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Score]
           ([TestQuestions_Score_ID]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_OrderNum]
           ,[TestQuestions_Score]
           ,[AnalyzeHyperlink]
           ,[AnalyzeHyperlinkData]
           ,[AnalyzeHyperlinkHtml]
           ,[AnalyzeText]
           ,[ComplexityHyperlink]
           ,[ComplexityText]
           ,[ContentHyperlink]
           ,[ContentText]
           ,[DocBase64]
           ,[DocHtml]
           ,[ScoreHyperlink]
           ,[ScoreText]
           ,[TargetHyperlink]
           ,[TrainHyperlinkData]
           ,[TrainHyperlinkHtml]
           ,[TargetText]
           ,[TestCorrect]
           ,[TestType]
           ,[TrainHyperlink]
           ,[TrainText]
           ,[TypeHyperlink]
           ,[TypeText]
           ,[CreateTime]
           ,[AreaHyperlink]
           ,[AreaText]
           ,[kaofaText]
           ,[testIndex] )
     select '{0}','{1}','{2}',TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,'{5}',AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,'{6}',TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,'{3}',AreaHyperlink,AreaText,kaofaText,testIndex from TestQuestions_Score where TestQuestions_Score_ID='{4}' ;", ScoreGuid, ReID, TQBGuid, DateTime.Now.ToString(), drScore[jj]["TestQuestions_Score_ID"].ToString()
                                                                    , string.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()) ? "" : tempAnalyzeData
                                                                    , string.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()) ? "" : tempTrainData);
                                                                #endregion
                                                                #region 存文件

                                                                #region 正确答案
                                                                //不是选择题填空判断下载正确答案
                                                                if (drScore[jj]["TestType"].ToString() == "fill" || drScore[jj]["TestType"].ToString() == "answers")
                                                                {
                                                                    pfunction.CopyToFile(string.Format(fileUrl, "testQuestionCurrent", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionCurrent", ScoreGuid, "txt"));
                                                                }
                                                                #endregion
                                                                #region 选择题的选项
                                                                if (drScore[jj]["TestType"].ToString() == "selection" || drScore[jj]["TestType"].ToString() == "clozeTest")
                                                                {
                                                                    pfunction.CopyToFile(string.Format(fileUrl, "testQuestionOption", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionOption", ScoreGuid, "txt"));
                                                                }
                                                                #endregion
                                                                #region 解析 保存文件
                                                                if (!String.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()))
                                                                {
                                                                    pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                   , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "AnalyzeData", ScoreGuid, "txt"));

                                                                    pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                                   , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "AnalyzeHtml", ScoreGuid, "htm"));

                                                                }
                                                                #endregion
                                                                #region 强化训练 保存文件
                                                                if (!String.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()))
                                                                {
                                                                    pfunction.CopyToFile(string.Format(fileUrl, "TrainData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                                        , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "TrainData", ScoreGuid, "txt"));

                                                                    pfunction.CopyToFile(string.Format(fileUrl, "TrainHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                                   , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "TrainHtml", ScoreGuid, "htm"));

                                                                }
                                                                #endregion
                                                                #region 子题题干Html 保存文件bodySub
                                                                pfunction.CopyToFile(string.Format(fileUrl, "bodySub", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                                        , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "bodySub", ScoreGuid, "txt"));
                                                                #endregion
                                                                #endregion
                                                                #region 插入选项
                                                                string sqlOption = "select * from TestQuestions_Option where TestQuestions_Score_ID='" + drScore[jj]["TestQuestions_Score_ID"] + "'";
                                                                DataTable dtOption = Rc.Common.DBUtility.DbHelperSQL.Query(sqlOption).Tables[0];
                                                                if (dtOption.Rows.Count > 0)
                                                                {
                                                                    foreach (DataRow item in dtOption.Rows)
                                                                    {
                                                                        stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Option]
           ([TestQuestions_Option_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_OptionParent_OrderNum]
           ,[TestQuestions_Option_Content]
           ,[TestQuestions_Option_OrderNum]
           ,[CreateTime]
           ,[TestQuestions_Score_ID] )
     select Newid(),'{0}',TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,'{1}','{2}' from TestQuestions_Option where TestQuestions_Option_Id='{3}' ;", TQBGuid, DateTime.Now.ToString(), ScoreGuid, item["TestQuestions_Option_Id"].ToString());
                                                                    }
                                                                }
                                                                #endregion
                                                            }
                                                        }
                                                    }
                                                }
                                                #endregion
                                            }
                                            #endregion
                                        }
                                    }

                                }
                                #endregion
                                Num++;
                                i++;
                            }
                            #region 插入已组试卷
                            stbSql.AppendFormat(@" INSERT INTO [dbo].[AlreadyedTestpaper]
           ([AlreadyedTestpaper_Id]
           ,[User_Id]
           ,[Two_WayChecklist_Id]
           ,[ResourceToResourceFolder_Id]
           ,[Remark]
           ,[CreateUser]
           ,[CreateTime])
     VALUES(NewId(),'{0}','{1}','{2}','','{3}','{4}')", CreateUser, Two_WayChecklist_Id, ReID, CreateUser, DateTime.Now.ToString());
                            #endregion
                            #region 插入老师已组试卷对应试题表
                            stbSql.AppendFormat(@"INSERT INTO [dbo].[AlreadyedTQ_Teacher]
           ([AlreadyedTQ_Teacher_Id]
           ,[TestQuestions_Id]
           ,[Two_WayChecklistDetail_Id]
           ,[CreateUser]
           ,[CreateTime])
     select NEWID(),TestQuestions_Id,Two_WayChecklistDetail_Id,'{0}',getdate() from [dbo].[RelationPaperTemp]
	 where RelationPaper_Id='{1}'", CreateUser, RelationPaper_Id);
                            #endregion

                            if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(stbSql.ToString()) > 0)
                            {
                                context.Response.Write("ok");
                            }
                            else
                            {
                                context.Response.Write("组卷失败：写入数据失败");
                                context.Response.End();
                            }
                        }
                        catch (Exception ex)
                        {
                            Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(Two_WayChecklist_Id, "", string.Format("组卷失败：|唯一标识{0}|双向细目表Id{1}|方法{2}|错误信息{3}", RelationPaper_Id, Two_WayChecklist_Id, "ConfirmTestpaper", ex.Message.ToString()));
                            context.Response.Write("组卷失败err");
                        }

                        #endregion
                        break;
                    case "stu_testpaperCorrect_view_st":
                        #region 赛通web端学生综合分析答错题详细 /student/ErrorDetail.aspx
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        stuId = context.Request["stuId"].Filter();
                        hwId = context.Request["hwId"].Filter();
                        string S_KnowledgePoint_Id = context.Request["S_KnowledgePoint_Id"].Filter();
                        strSqlAnswerScore = string.Format(@"select tqs.TestQuestions_Id,tqs.AnalyzeHyperlinkData,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex,shwa.Student_HomeWorkAnswer_Id,shwa.Student_Answer,shwa.Student_Score,shwa.Student_Answer_Status,shwa.Comment
,tqs.ContentText,tqs.TargetText
from TestQuestions_Score tqs
left join Student_HomeWorkAnswer shwa on TQS.TestQuestions_Id=SHWA.TestQuestions_Id and shwa.TestQuestions_Score_ID=tqs.TestQuestions_Score_ID 
 where  shwa.Student_Id='{0}' 
 and SHWA.TestQuestions_Id in(select TestQuestions_Id from StatsStuHW_Wrong_TQ where CHARINDEX((','+'{1}'),(','+S_KnowledgePoint_Id))>0 and  Student_Id='{0}') order by tqs.TestQuestions_OrderNum", stuId, S_KnowledgePoint_Id);
                        dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlAnswerScore).Tables[0];

                        strSql = string.Empty;

                        strSql = string.Format(@"

 select hw.HomeWork_Id,hw.HomeWork_Name,hw.CreateTime,hw.IsShowAnswer,rtrf.ParticularYear,rtrf.GradeTerm,rtrf.Resource_Version,rtrf.Subject,rtrf.Resource_Class,tq.* from StatsStuHW_Wrong_TQ t
 left join HomeWork hw on hw.HomeWork_Id =t.HomeWork_Id
 left join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id 
 inner join TestQuestions tq on tq.ResourceToResourceFolder_Id=rtrf.ResourceToResourceFolder_Id and TestQuestions_Type='' and tq.type!='simple'  and  tq.TestQuestions_Id in( select distinct Parent_Id from TestQuestions where TestQuestions_Id=t.TestQuestions_Id)
 where  t.Student_Id='{0}' and  CHARINDEX(('，'+'{1}'),('，'+S_KnowledgePoint_Id))>0

 union

  select hw.HomeWork_Id,hw.HomeWork_Name,hw.CreateTime,hw.IsShowAnswer,rtrf.ParticularYear,rtrf.GradeTerm,rtrf.Resource_Version,rtrf.Subject,rtrf.Resource_Class,tq.* from StatsStuHW_Wrong_TQ t
 left join HomeWork hw on hw.HomeWork_Id =t.HomeWork_Id
 left join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id 
 inner join TestQuestions tq on  tq.ResourceToResourceFolder_Id=rtrf.ResourceToResourceFolder_Id and  TestQuestions_Type!='title' and  tq.TestQuestions_Id =t.TestQuestions_Id
 where  t.Student_Id='{0}' and  CHARINDEX(('，'+'{1}'),('，'+S_KnowledgePoint_Id))>0
", stuId, S_KnowledgePoint_Id);
                        //所有题 
                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        DataView dv = dt.DefaultView;
                        //去重后的datatable
                        DataTable DistinctDt = dv.ToTable(true, "HomeWork_Id", "HomeWork_Name", "CreateTime", "ParticularYear", "GradeTerm", "Resource_Version", "Subject", "Resource_Class", "IsShowAnswer");
                        DataRow[] drHw = DistinctDt.Select("", "CreateTime desc");
                        foreach (DataRow itemHw in drHw)
                        {
                            //生成存储路径
                            savePath = string.Empty;
                            saveOwnPath = string.Empty;
                            if (itemHw["Resource_Class"].ToString() == Rc.Common.Config.Resource_ClassConst.云资源)
                            {
                                savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", itemHw["ParticularYear"], itemHw["GradeTerm"], itemHw["Resource_Version"], itemHw["Subject"]);
                            }
                            if (itemHw["Resource_Class"].ToString() == Rc.Common.Config.Resource_ClassConst.自有资源)
                            {
                                saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(itemHw["CreateTime"]).ToString("yyyy-MM-dd"));
                            }
                            fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                            fileStudentAnswerUrl = uploadPath + "{0}\\" + pfunction.ToShortDate(itemHw["CreateTime"].ToString()) + "\\" + savePath + "{1}.{2}";//学生答案详细路径
                            //每份作业的错题
                            DataRow[] drTQ = dt.Select("HomeWork_Id='" + itemHw["HomeWork_Id"].ToString() + "'", "TestQuestions_Num");

                            //stbHtml.AppendFormat("<div class=\"question panel panel-default\" data-name=\"correct_over\"><div class=\"panel-heading\">");
                            //stbHtml.AppendFormat("错题来源：{0}", itemHw["HomeWork_Name"]);
                            //stbHtml.AppendFormat("<span class=\"pull-right\">时间：{0}</span></div></div>", pfunction.ConvertToLongDateTime(itemHw["CreateTime"].ToString()));
                            stbHtml.AppendFormat("<div class=\"question_source_panel\" data-name=\"correct_over\"><div class=\"panel_heading\">");
                            stbHtml.AppendFormat("<div class='time'>时间：{0}</div>", pfunction.ConvertToLongDateTime(itemHw["CreateTime"].ToString()));
                            stbHtml.AppendFormat("<div class='source'>错题来源：{0}</div>", itemHw["HomeWork_Name"]);
                            stbHtml.Append("</div></div>");
                            for (int i = 0; i < drTQ.Length; i++)
                            {
                                DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + drTQ[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                if (drScore.Length > 0)
                                {
                                    //题干
                                    string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", drTQ[i]["TestQuestions_Id"], "htm"));

                                    //选择题、完形填空题选项
                                    string strOption = string.Empty;
                                    if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest")
                                    {
                                        for (int ii = 0; ii < drScore.Length; ii++)
                                        {
                                            //从文件读取选择题选项
                                            string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                            List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                            if (listTestSelections != null && listTestSelections.Count > 0)
                                            {
                                                foreach (var item in listTestSelections)
                                                {
                                                    if (!string.IsNullOrEmpty(item.selectionHTML)) strOption += string.Format("<div class=\"option_item col-xs-6\">{0}</div>", pfunction.NoHTML(item.selectionHTML));
                                                }
                                                if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                {
                                                    strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                }
                                            }
                                        }
                                    }
                                    stbHtml.Append("<div class=\"question_panel\" data-name='correct_over'>");
                                    stbHtml.AppendFormat("<div class=\"panel_stem\">{0}(分值：<i class=\"fen\">{1}</i>)<div class='options row clearfix'>{2}</div></div>"
                                        , pfunction.NoHTML(strTestQuestionBody)
                                        , drTQ[i]["TestQuestions_SumScore"].ToString().clearLastZero()
                                        , strOption);
                                    stbHtml.Append("<div class=\"panel_answer\">");
                                    //标题
                                    stbHtml.Append("<div class='answer_heading'>");
                                    stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                    stbHtml.Append("<div class='answer_item student_answer'><span class='btn btn-info btn-sm'>学生答案</span></div>");
                                    stbHtml.Append("</div>");
                                    //答案&得分
                                    for (int ii = 0; ii < drScore.Length; ii++)
                                    {
                                        stbHtml.Append("<div class='answer_body'>");
                                        if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" || drTQ[i]["TestQuestions_Type"].ToString() == "truefalse")
                                        {//选择题、判断题 答案从数据库读取
                                            //正确答案
                                            stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0} {1}</div>"
                                                , drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                , drScore[ii]["TestCorrect"].ToString());
                                            //学生答案
                                            stbHtml.AppendFormat("<div class='answer_item student_answer'>{0}</div>", drScore[ii]["Student_Answer"]);
                                        }
                                        else if (drTQ[i]["TestQuestions_Type"].ToString() == "fill" || drTQ[i]["TestQuestions_Type"].ToString() == "answers")
                                        {//填空题、简答题 答案从文件读取
                                            //正确答案
                                            stbHtml.Append("<div class='answer_item reference_answer'>");
                                            //if (drTQ[0]["IsShowAnswer"].ToString() == "1")
                                            //{
                                            //从文件读取正确答案图片
                                            string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                            if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                            {
                                                stbHtml.AppendFormat("<div>{0}{1}</div>"
                                                    , (drTQ[i]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString())) ? "(" + drScore[ii]["testIndex"].ToString() + ")" : ""
                                                    , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));
                                            }
                                            //}
                                            //else
                                            //{
                                            //    stbHtml.Append("<div></div>");
                                            //}
                                            stbHtml.Append("</div>");
                                            //学生答案
                                            stbHtml.Append("<div class='answer_item student_answer'>");
                                            string strStudentAnswerHtml = pfunction.ReadAllText(string.Format(fileStudentAnswerUrl, "studentAnswer", drScore[ii]["Student_HomeWorkAnswer_Id"], "txt"));
                                            if (!string.IsNullOrEmpty(strStudentAnswerHtml)) stbHtml.AppendFormat("<div>{0}</div>", pfunction.NoHTML(strStudentAnswerHtml));
                                            stbHtml.Append("</div>");
                                        }
                                        stbHtml.Append("</div>");
                                    }
                                    stbHtml.Append("</div>");

                                    stbHtml.Append("<div class=\"panel_other\">");
                                    stbHtml.Append("<dl class=\"other_attr\">");
                                    for (int ii = 0; ii < drScore.Length; ii++)
                                    {
                                        string urlAnalyzeHtml = string.Format("{0}{1}{2}\\{3}{4}.htm", uploadPath, saveOwnPath, "AnalyzeHtml", savePath, drScore[ii]["TestQuestions_Score_ID"]);
                                        string strAnalyzeHtml = pfunction.ReadAllText(urlAnalyzeHtml);
                                        stbHtml.AppendFormat("<dt>{0} 解析：</dt><dd>{1}</dd>"
                                            , drScore.Length > 1 ? (ii + 1).ToString() : ""
                                            , string.IsNullOrEmpty(strAnalyzeHtml) ? "-" : strAnalyzeHtml);
                                    }
                                    stbHtml.Append("</dl>");

                                    //stbHtml.Append("<div class=\"commenting_box\">解析解析</div>");
                                    //string url = string.Format("{0}{1}{2}\\{3}{4}.htm", uploadPath, saveOwnPath, AnalyzeHtml, savePath, drScore[ii]["TestQuestions_Score_ID"]);
                                    //string strAnalyzeHtml = pfunction.ReadAllText(url);
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");
                                }
                                else
                                {
                                    //题干
                                    string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", drTQ[i]["TestQuestions_Id"], "htm"));
                                    strTestQuestionBody = pfunction.NoHTML(strTestQuestionBody);
                                    if (string.IsNullOrEmpty(strTestQuestionBody))
                                    {
                                        strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "textTitle", dt.Rows[i]["TestQuestions_Id"], "txt"));
                                    }
                                    stbHtml.Append("<div class=\"question_type_panel\" data-name='correct_over'>");
                                    stbHtml.AppendFormat("<div class=\"panel_heading\">{0}</div>", strTestQuestionBody);
                                    stbHtml.Append("</div>");
                                }
                            }
                        }
                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        break;
                    case "combination_testpapertochapter_view":
                        #region 章节组卷预览/teacher/combination_testpapertochapter.aspx
                        string Identifier_Id = context.Request["Identifier_Id"].Filter();
                        try
                        {
                            //章节组卷大标题
                            string sqltitle = @"select t.*,cd.D_Name from ChapterAssembly_TQ t
                                            left join Common_Dict cd on cd.Common_Dict_Id=t.TQ_Type  where Identifier_Id='" + Identifier_Id + "' and Parent_Id='0' order by TQ_Order";
                            DataTable dtTitle = Rc.Common.DBUtility.DbHelperSQL.Query(sqltitle).Tables[0];
                            //章节组卷的明细
                            string sqlDetil = @"select * from ChapterAssembly_TQ where Identifier_Id='" + Identifier_Id + "' and Parent_Id<>'0' order by TQ_Order";
                            DataTable dtDetil = Rc.Common.DBUtility.DbHelperSQL.Query(sqlDetil).Tables[0];
                            //题的
                            string sqlTQ1 = @"select t.Identifier_Id,t.type,t.TQ_Type,t.ComplexityText,t.Parent_Id,t.TQ_Order,t.ChapterAssembly_TQ_Id
,tq.*,rtf.ResourceToResourceFolder_Id,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from ChapterAssembly_TQ t
left join TestQuestions tq on tq.TestQuestions_Id=t.TestQuestions_Id
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where t.Identifier_Id='" + Identifier_Id + "'";
                            dt = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQ1).Tables[0];
                            //分的
                            string sqlTQScore1 = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex from TestQuestions_Score tqs 
where tqs.TestQuestions_Id in 
(select TestQuestions_Id from ChapterAssembly_TQ where Identifier_Id='" + Identifier_Id + "')";
                            dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQScore1).Tables[0];
                            saveOwnPath = string.Empty;
                            stbHtml.Append(" <div class='fixed_sidebar right mini bottom'>");
                            stbHtml.Append("<ul><li class='active confirmation-test-paper-hook' id='btnConfirm'>");
                            stbHtml.Append("<div class='link'>确认组卷</div></li>");
                            //stbHtml.Append("<li class=\"hidden gobacktop-hook\"><div class=\"link\"><i class=\"material-icons\">&#xE5D8;</i></div><div class=\"pop\">返回顶部</div></li>");
                            stbHtml.Append("</ul></div>");
                            stbHtml.Append("<div class=\"test_paper_panel\">");
                            stbHtml.Append("<div class=\"panel_heading\"><div class=\"test_paper_name_panel\">");
                            stbHtml.Append("<div class=\"panel_heading\">");
                            stbHtml.Append("<div class=\"panel_title\"><input type=\"text\" id=\"rename\" name=\"name\" class=\"form-control input-lg text-center\" placeholder=\"试卷名称\" /></div></div></div></div>");
                            stbHtml.Append("<div class=\"panel_body\">");
                            foreach (DataRow item in dtTitle.Rows)
                            {


                                if (item["type"].ToString().TrimEnd() == "title")
                                {
                                    stbHtml.Append("<div class=\"question_type_panel\">");
                                    stbHtml.Append("<div class=\"panel_heading\">");
                                    stbHtml.AppendFormat("<input type=\"text\" name=\"name\" class=\"form-control {1}\" value=\"{0}\"  /></div></div>", pfunction.NoHTML(item["D_Name"].ToString()), !string.IsNullOrEmpty(item["D_Name"].ToString()) ? "" : "hidden");

                                    DataRow[] drTQ = dt.Select(" Parent_Id='" + item["ChapterAssembly_TQ_Id"].ToString() + "' and type='simple'", "TQ_Order");
                                    #region 普通题
                                    if (drTQ.Length > 0)
                                    {
                                        for (int i = 0; i < drTQ.Length; i++)
                                        {
                                            //生成存储路径
                                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                               , drTQ[i]["ParticularYear"].ToString()
                                               , drTQ[i]["GradeTerm"].ToString()
                                               , drTQ[i]["Resource_Version"].ToString()
                                               , drTQ[i]["Subject"].ToString());
                                            fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                                            DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + drTQ[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                            //题干
                                            string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", drTQ[i]["TestQuestions_Id"], "htm"));
                                            //选择题、完形填空题选项
                                            string strOption = string.Empty;
                                            if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest")
                                            {
                                                for (int ii = 0; ii < drScore.Length; ii++)
                                                {
                                                    //从文件读取选择题选项
                                                    string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                    List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                    if (listTestSelections != null && listTestSelections.Count > 0)
                                                    {
                                                        foreach (var itemSelections in listTestSelections)
                                                        {
                                                            if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                        }
                                                        if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                        {
                                                            strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                        }
                                                    }
                                                }
                                            }
                                            if (drTQ[i]["TestQuestions_Type"].ToString() != "title" && drTQ[i]["TestQuestions_Type"].ToString() != "")
                                            {
                                                stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\" id='change_{1}' data-value='{1}'><a name=\"{0}\"></a>", drTQ[i]["TestQuestions_Num"].ToString().TrimEnd('.'), drTQ[i]["ChapterAssembly_TQ_Id"].ToString());
                                                stbHtml.AppendFormat("<div class='panel_stem panel-stem-hook'>{0}</div><div class='panel_option'>{1}</div>"
                                                    , pfunction.NoHTML(strTestQuestionBody) //题干
                                                    , strOption //选项
                                                    );

                                                stbHtml.Append("<div class='panel_answer'>");
                                                stbHtml.Append("<div class='answer_heading'>");
                                                stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-sm btn-block'>分值</span></div>");
                                                stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                                stbHtml.Append("</div>");
                                                for (int ii = 0; ii < drScore.Length; ii++)
                                                {
                                                    stbHtml.Append("<div class='answer_body'>");
                                                    //正确答案
                                                    if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" || drTQ[i]["TestQuestions_Type"].ToString() == "truefalse")
                                                    {
                                                        stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                            , drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                            , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                        stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                            , drScore[ii]["TestCorrect"]);

                                                    }
                                                    if (drTQ[i]["TestQuestions_Type"].ToString() == "fill" || drTQ[i]["TestQuestions_Type"].ToString() == "answers")
                                                    {
                                                        //从文件读取正确答案
                                                        string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                        if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                        {
                                                            stbHtml.AppendFormat("<div class='answer_item score'>{0}{1}分</div>"
                                                                , drTQ[i]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                            stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                                , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));


                                                        }
                                                    }
                                                    stbHtml.Append("</div>");
                                                }
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("<div class='panel_opera'>");
                                                stbHtml.Append("<div class='opera_control'>");
                                                stbHtml.AppendFormat("<span id='ischange_{0}' class='btn ischange-hook' tt='{0}'><i class='material-icons'>&#xE627;</i>&nbsp;随机换题</span>", drTQ[i]["ChapterAssembly_TQ_Id"].ToString());
                                                stbHtml.AppendFormat("<span id='changeTQ_{0}' class='btn changetq-hook' tt='{0}'><i class='material-icons'>&#xE1B3;</i>&nbsp;精准选题</span>", drTQ[i]["ChapterAssembly_TQ_Id"].ToString());
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");
                                            }
                                            else
                                            {
                                                stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\"><div class='panel_stem'>{0}</div></div>", pfunction.NoHTML(strTestQuestionBody));
                                            }
                                        }

                                    }

                                    #endregion
                                }
                                //drTQBig = dt.Select(" Parent_Id='" + item["ChapterAssembly_TQ_Id"].ToString() + "' and type='complex'", "TQ_Order");
                                if (item["type"].ToString().TrimEnd() == "complex")
                                {
                                    #region 综合题
                                    //综合题大标题和小题
                                    string sql = @" select * from ( select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where Parent_Id='" + item["TestQuestions_Id"].ToString() + @"'
union 
select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where TestQuestions_id='" + item["TestQuestions_Id"].ToString() + "' ) t order by TestQuestions_Num";
                                    DataTable dtBig = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                                    //综合题小题分值
                                    sqlTQScore1 = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex from TestQuestions_Score tqs 
where tqs.TestQuestions_Id in (select TestQuestions_Id from TestQuestions where Parent_Id='" + item["TestQuestions_Id"].ToString() + "')";
                                    DataTable dtBigScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQScore1).Tables[0];
                                    if (dtBig.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < dtBig.Rows.Count; j++)
                                        {
                                            //生成存储路径
                                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                               , dtBig.Rows[j]["ParticularYear"].ToString()
                                               , dtBig.Rows[j]["GradeTerm"].ToString()
                                               , dtBig.Rows[j]["Resource_Version"].ToString()
                                               , dtBig.Rows[j]["Subject"].ToString());
                                            fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                                            DataRow[] drScore = dtBigScore.Select("TestQuestions_Id='" + dtBig.Rows[j]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                            //题干
                                            string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dtBig.Rows[j]["TestQuestions_Id"], "htm"));
                                            //选择题、完形填空题选项
                                            string strOption = string.Empty;
                                            if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "selection" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest")
                                            {
                                                for (int ii = 0; ii < drScore.Length; ii++)
                                                {
                                                    //从文件读取选择题选项
                                                    string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                    List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                    if (listTestSelections != null && listTestSelections.Count > 0)
                                                    {
                                                        foreach (var itemSelections in listTestSelections)
                                                        {
                                                            if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                        }
                                                        if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                        {
                                                            strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                        }
                                                    }
                                                }
                                            }
                                            if (dtBig.Rows[j]["TestQuestions_Type"].ToString() != "title" && dtBig.Rows[j]["TestQuestions_Type"].ToString() != "")
                                            {
                                                stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\"><a name=\"{0}\"></a>", dtBig.Rows[j]["TestQuestions_Num"].ToString().TrimEnd('.'));
                                                stbHtml.AppendFormat("<div class='panel_stem panel-stem-hook'>{0}</div><div class='panel_option'>{1}</div>"
                                                    , pfunction.NoHTML(strTestQuestionBody) //题干
                                                    , strOption //选项
                                                    );

                                                stbHtml.Append("<div class='panel_answer'>");
                                                stbHtml.Append("<div class='answer_heading'>");
                                                stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-sm btn-block'>分值</span></div>");
                                                stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                                stbHtml.Append("</div>");
                                                for (int ii = 0; ii < drScore.Length; ii++)
                                                {
                                                    stbHtml.Append("<div class='answer_body'>");
                                                    //正确答案
                                                    if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "selection" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "truefalse")
                                                    {
                                                        stbHtml.AppendFormat("<div class='answer_item score'>{0}{1}分</div>"
                                                            , dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                            , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                        stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0} 分</div>"
                                                            , drScore[ii]["TestCorrect"]);

                                                    }
                                                    if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "fill" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "answers")
                                                    {
                                                        //从文件读取正确答案
                                                        string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                        if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                        {
                                                            stbHtml.AppendFormat("<div class='answer_item score'>{0}{1}</div>"
                                                                , dtBig.Rows[j]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                            stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                                , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));


                                                        }
                                                    }
                                                    stbHtml.Append("</div>");
                                                }
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");

                                            }
                                            else
                                            {
                                                stbHtml.AppendFormat("<div id='change_{1}' class='multiple_question' style='margin-top:10px;'><div class=\"question_panel question-panel-hook\"><div class='panel_stem'>{0}</div>", pfunction.NoHTML(strTestQuestionBody), item["ChapterAssembly_TQ_Id"].ToString());

                                                stbHtml.Append("<div class='panel_opera'>");
                                                stbHtml.Append("<div class='opera_control'>");
                                                if (dtBig.Rows[j]["TestQuestions_Type"].ToString() != "title") stbHtml.AppendFormat("<span id='ischange_{0}' class='btn ischange-hook' tt='{0}'><i class='material-icons'>&#xE627;</i>&nbsp;随机换题</span>", item["ChapterAssembly_TQ_Id"].ToString());
                                                stbHtml.AppendFormat("<span id='changeTQ_{0}' class='btn changetq-hook' tt='{0}'><i class='material-icons'>&#xE1B3;</i>&nbsp;精准选题</span>", item["ChapterAssembly_TQ_Id"].ToString());
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");
                                            }
                                        }
                                        stbHtml.Append("</div>");
                                    }


                                    #endregion
                                }

                            }
                            stbHtml.Append("</div>");

                            context.Response.Write(stbHtml.ToString());

                        }
                        catch (Exception ex)
                        {
                            Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(Identifier_Id, "", string.Format("加载试卷失败：|唯一标识{0}|方法{1}|错误信息{2}", Identifier_Id, "GetTestPaper", ex.Message.ToString()));
                            context.Response.Write("章节组卷：加载试卷失败");
                        }
                        #endregion
                        break;
                    case "combination_testpapertochapter_tq_change":
                        #region 章节组卷 随机换题 /teacher/combination_testpapertochapter.aspx
                        stbHtml = new StringBuilder();
                        string ChapterAssembly_TQ_Id = context.Request["ChapterAssembly_TQ_Id"].Filter();
                        Identifier_Id = context.Request["Identifier_Id"].Filter();
                        Type = context.Request["Type"].Filter();
                        int changeType = Convert.ToInt32(context.Request["ChangeType"]);//换题类型（1普通题换综合题，2综合题换普通题）
                        int ReturnValue = Convert.ToInt32(context.Request["RCount"].Filter());
                        try
                        {
                            int m = 0;
                            StringBuilder stbTitle = new StringBuilder();
                            if (Type == "1")
                            {
                                string TQ_Id = string.Empty;
                                Model_ChapterAssembly_TQ chapterTQ = new BLL_ChapterAssembly_TQ().GetModel(ChapterAssembly_TQ_Id);
                                string sqlUpdate = string.Empty;
                                string sqlTq_Id = string.Format(@"select top 1  t1.TestQuestions_Id,t1.type from ChapterAssembly_TQTemp t1
inner join ChapterAssembly_TQ t on t.ComplexityText=t1.ComplexityText and t.TQ_Type=t1.TQ_Type and t.Identifier_Id=t1.Identifier_Id and t.ChapterAssembly_TQ_Id='" + ChapterAssembly_TQ_Id + @"'
  where  t1.TestQuestions_Id<>'' and t1.TestQuestions_Id not in(select TestQuestions_Id from ChapterAssembly_TQ where Identifier_Id=t.Identifier_Id)
  order by newid()");
                                DataTable dtId = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTq_Id).Tables[0];
                                if (dtId.Rows.Count > 0 && !string.IsNullOrEmpty(dtId.Rows[0]["TestQuestions_id"].ToString()))
                                {

                                    TQ_Id = dtId.Rows[0]["TestQuestions_id"].ToString();
                                    if (chapterTQ.type.TrimEnd() == "simple" && dtId.Rows[0]["type"].ToString().TrimEnd() == "complex")//普通题换成综合题
                                    {
                                        changeType = 1;
                                        sqlUpdate = string.Format("update ChapterAssembly_TQ set TestQuestions_id='{0}',type='complex'  where ChapterAssembly_TQ_Id='{1}'", TQ_Id, ChapterAssembly_TQ_Id);
                                        m = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sqlUpdate);

                                        DataTable dtList = new BLL_ChapterAssembly_TQ().GetList(Identifier_Id, ChapterAssembly_TQ_Id, "1", out ReturnValue).Tables[0];

                                        //Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime(string.Format("exec [dbo].[P_ChapterChangeQuestions] '{0}','{1}','{2}'", Identifier_Id, ChapterAssembly_TQ_Id, "1"), 7200);
                                    }
                                    if (chapterTQ.type.TrimEnd() == "complex" && dtId.Rows[0]["type"].ToString().TrimEnd() == "simple")//综合题换成普通题
                                    {
                                        changeType = 2;
                                        sqlUpdate = string.Format("update ChapterAssembly_TQ set TestQuestions_id='{0}', type='simple'  where ChapterAssembly_TQ_Id='{1}'", TQ_Id, ChapterAssembly_TQ_Id);
                                        m = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sqlUpdate);

                                        DataTable dtList = new BLL_ChapterAssembly_TQ().GetList(Identifier_Id, ChapterAssembly_TQ_Id, "2", out ReturnValue).Tables[0];
                                        //Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime(string.Format("exec [dbo].[P_ChapterChangeQuestions] '{0}','{1}','{2}'", Identifier_Id, ChapterAssembly_TQ_Id, "2"), 7200);
                                    }
                                    if (chapterTQ.type.TrimEnd() == dtId.Rows[0]["type"].ToString().TrimEnd())//相同题型互换
                                    {
                                        sqlUpdate = string.Format("update ChapterAssembly_TQ set TestQuestions_id='{0}' where ChapterAssembly_TQ_Id='{1}'", TQ_Id, ChapterAssembly_TQ_Id);
                                        m = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sqlUpdate);
                                    }
                                }
                                else
                                {
                                    m = 1;
                                }
                                //m = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sqlUpdate);
                            }
                            else
                            {
                                m = 1;
                            }
                            if (m > 0)
                            {
                                if (ReturnValue != 0)
                                {
                                    #region 插入标题文本框
                                    stbTitle.Append("<div class=\"question_type_panel\">");
                                    stbTitle.Append("<div class=\"panel_heading\">");
                                    stbTitle.Append("<input type=\"text\" name=\"name\" class=\"form-control hidden\" value=\"\"  /></div></div>");
                                    #endregion
                                }
                                string sqlTQ = @"select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.[Subject] from  TestQuestions tq
 left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_id=tq.ResourceToResourceFolder_Id
 where TestQuestions_Id=(select TestQuestions_Id from ChapterAssembly_TQ where ChapterAssembly_TQ_Id='" + ChapterAssembly_TQ_Id + "')";
                                dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQ).Tables[0];
                                string sqlScore = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex from TestQuestions_Score tqs 
where tqs.ResourceToResourceFolder_Id =(select ResourceToResourceFolder_Id from TestQuestions where TestQuestions_Id=( select TestQuestions_Id from ChapterAssembly_TQ where ChapterAssembly_TQ_Id='" + ChapterAssembly_TQ_Id + "')) ";
                                dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlScore).Tables[0];

                                saveOwnPath = string.Empty;
                                DataRow[] drTQ = dtTQ.Select("type='simple'", "TestQuestions_Num");
                                drTQBig = dtTQ.Select("type='complex'", "TestQuestions_Num");
                                #region 普通题
                                if (drTQ.Length > 0)
                                {
                                    for (int i = 0; i < drTQ.Length; i++)
                                    {
                                        //生成存储路径
                                        savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                           , drTQ[i]["ParticularYear"].ToString()
                                           , drTQ[i]["GradeTerm"].ToString()
                                           , drTQ[i]["Resource_Version"].ToString()
                                           , drTQ[i]["Subject"].ToString());
                                        fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                                        DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + drTQ[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                        //题干
                                        string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", drTQ[i]["TestQuestions_Id"], "htm"));
                                        //选择题、完形填空题选项
                                        string strOption = string.Empty;
                                        if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest")
                                        {
                                            for (int ii = 0; ii < drScore.Length; ii++)
                                            {
                                                //从文件读取选择题选项
                                                string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                if (listTestSelections != null && listTestSelections.Count > 0)
                                                {
                                                    foreach (var itemSelections in listTestSelections)
                                                    {
                                                        if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                    }
                                                    if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                    {
                                                        strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                    }
                                                }
                                            }
                                        }
                                        if (drTQ[i]["TestQuestions_Type"].ToString() != "title" && drTQ[i]["TestQuestions_Type"].ToString() != "")
                                        {
                                            stbHtml.AppendFormat("<div class='panel_stem panel-stem-hook'>{0}</div><div class='panel_option'>{1}</div>"
                                                , pfunction.NoHTML(strTestQuestionBody) //题干
                                                , strOption //选项
                                                );

                                            stbHtml.Append("<div class='panel_answer'>");
                                            stbHtml.Append("<div class='answer_heading'>");
                                            stbHtml.Append("<div class='answer_item score'>");
                                            stbHtml.Append("<span class='btn btn-info btn-sm btn-block'>分值</span>");
                                            stbHtml.Append("</div>");
                                            stbHtml.Append("<div class='answer_item reference_answer'>");
                                            stbHtml.Append("<span class='btn btn-success btn-sm'>参考答案</span>");
                                            stbHtml.Append("</div>");
                                            stbHtml.Append("<div class='answer_heading'></div>");
                                            stbHtml.Append("</div>");
                                            for (int ii = 0; ii < drScore.Length; ii++)
                                            {
                                                stbHtml.Append("<div class='answer_body'>");
                                                //正确答案
                                                if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" || drTQ[i]["TestQuestions_Type"].ToString() == "truefalse")
                                                {
                                                    stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                        , drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                        , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                    stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                        , drScore[ii]["TestCorrect"]);

                                                }
                                                if (drTQ[i]["TestQuestions_Type"].ToString() == "fill" || drTQ[i]["TestQuestions_Type"].ToString() == "answers")
                                                {
                                                    //从文件读取正确答案
                                                    string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                    if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                    {
                                                        stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                            , drTQ[i]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                            , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                        stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                            , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));


                                                    }
                                                }
                                                stbHtml.Append("</div>");
                                            }
                                            stbHtml.Append("</div>");

                                            stbHtml.Append("<div class='panel_opera'>");
                                            stbHtml.Append("<div class='opera_control'>");
                                            stbHtml.AppendFormat("<span id='ischange_{0}' class='btn ischange-hook' tt='{0}'><i class='material-icons'>&#xE627;</i>&nbsp;随机换题</span>", ChapterAssembly_TQ_Id);
                                            stbHtml.AppendFormat("<span id='changeTQ_{0}' class='btn changetq-hook' tt='{0}'><i class='material-icons'>&#xE1B3</i>&nbsp;精准选题</span>", ChapterAssembly_TQ_Id);
                                            stbHtml.Append("</div>");
                                            stbHtml.Append("</div>");
                                        }
                                        else
                                        {
                                            stbHtml.AppendFormat("<div class=\"question_panel\"><div class='panel_stem'>{0}</div></div>", pfunction.NoHTML(strTestQuestionBody));
                                        }
                                    }
                                }
                                #endregion
                                #region 综合题
                                if (drTQBig.Length > 0)
                                {

                                    for (int i = 0; i < drTQBig.Length; i++)
                                    {
                                        string sql = @" select * from ( select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where Parent_Id='" + drTQBig[i]["TestQuestions_id"].ToString() + @"'
union 
select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where TestQuestions_id='" + drTQBig[i]["TestQuestions_id"].ToString() + "' ) t order by TestQuestions_Num";
                                        DataTable dtBig = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                                        if (dtBig.Rows.Count > 0)
                                        {
                                            for (int j = 0; j < dtBig.Rows.Count; j++)
                                            {
                                                //生成存储路径
                                                savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                                   , dtBig.Rows[j]["ParticularYear"].ToString()
                                                   , dtBig.Rows[j]["GradeTerm"].ToString()
                                                   , dtBig.Rows[j]["Resource_Version"].ToString()
                                                   , dtBig.Rows[j]["Subject"].ToString());
                                                fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                                                DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dtBig.Rows[j]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                                //题干
                                                string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dtBig.Rows[j]["TestQuestions_Id"], "htm"));
                                                //选择题、完形填空题选项
                                                string strOption = string.Empty;
                                                if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "selection" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest")
                                                {
                                                    for (int ii = 0; ii < drScore.Length; ii++)
                                                    {
                                                        //从文件读取选择题选项
                                                        string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                        List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                        if (listTestSelections != null && listTestSelections.Count > 0)
                                                        {
                                                            foreach (var itemSelections in listTestSelections)
                                                            {
                                                                if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                            }
                                                            if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                            {
                                                                strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                            }
                                                        }
                                                    }
                                                }
                                                if (dtBig.Rows[j]["TestQuestions_Type"].ToString() != "title" && dtBig.Rows[j]["TestQuestions_Type"].ToString() != "")
                                                {
                                                    stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\">");
                                                    stbHtml.AppendFormat("<div class='panel_stem'>{0}</div><div class='panel_option'>{1}</div>"
                                                        , pfunction.NoHTML(strTestQuestionBody) //题干
                                                        , strOption //选项
                                                        );

                                                    stbHtml.Append("<div class='panel_answer'>");
                                                    stbHtml.Append("<div class='answer_heading'>");
                                                    stbHtml.Append("<div class='answer_item score'>");
                                                    stbHtml.Append("<span class='btn btn-info btn-sm btn-block'>分值</span>");
                                                    stbHtml.Append("</div>");
                                                    stbHtml.Append("<div class='answer_item reference_answer'>");
                                                    stbHtml.Append("<span class='btn btn-success btn-sm'>参考答案</span>");
                                                    stbHtml.Append("</div>");
                                                    stbHtml.Append("</div>");
                                                    for (int ii = 0; ii < drScore.Length; ii++)
                                                    {
                                                        stbHtml.Append("<div class='answer_body'>");
                                                        //正确答案
                                                        if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "selection" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "truefalse")
                                                        {
                                                            stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                                , dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                            stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                                , drScore[ii]["TestCorrect"]);

                                                        }
                                                        if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "fill" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "answers")
                                                        {
                                                            //从文件读取正确答案
                                                            string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                            if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                            {
                                                                stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                                    , dtBig.Rows[j]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                    , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                                stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                                    , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));
                                                            }
                                                        }
                                                        stbHtml.Append("</div>");
                                                        stbHtml.Append("</div>");
                                                    }
                                                    stbHtml.Append("</div>");
                                                    stbHtml.Append("</div>");
                                                }
                                                else
                                                {
                                                    if (ReturnValue == 2) stbHtml.AppendFormat("<div id='change_{0}' class='multiple_question' style='margin-top:10px;'>", ChapterAssembly_TQ_Id);
                                                    stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\"><div class='panel_stem'>{0}</div>", pfunction.NoHTML(strTestQuestionBody));
                                                    stbHtml.Append("<div class='panel_opera'>");
                                                    stbHtml.Append("<div class='opera_control'>");
                                                    stbHtml.AppendFormat("<span id='ischange_{0}' class='btn ischange-hook' tt='{0}'><i class='material-icons'>&#xE627;</i>&nbsp;随机换题</span>", ChapterAssembly_TQ_Id);
                                                    stbHtml.AppendFormat("<span id='changeTQ_{0}' class='btn changetq-hook' tt='{0}'><i class='material-icons'>&#xE1B3;</i>&nbsp;精准选题</span>", ChapterAssembly_TQ_Id);
                                                    stbHtml.Append("</div>");
                                                    stbHtml.Append("</div>");
                                                    stbHtml.Append("</div>");

                                                    if (ReturnValue == 2) stbHtml.Append("</div>");
                                                }
                                            }
                                            stbHtml.Append("</div>");
                                        }
                                    }
                                }
                                #endregion

                                string strJson = JsonConvert.SerializeObject(new
                                {
                                    err = "",
                                    tbody = stbHtml.ToString(),
                                    title = stbTitle.ToString(),
                                    changeType = changeType,
                                    ReturnValue = ReturnValue
                                });
                                context.Response.Write(strJson);
                            }
                            else
                            {
                                string strJson = JsonConvert.SerializeObject(new
                                {
                                    err = "换题失败"
                                });
                                context.Response.Write(strJson);
                                context.Response.End();
                            }
                        }
                        catch (Exception ex)
                        {
                            Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(ChapterAssembly_TQ_Id, "", string.Format("换题失败：|唯一标识{0}|方法{1}|错误信息{2}", ChapterAssembly_TQ_Id, "", "ChangeTQ", ex.Message.ToString()));
                            string strJson = JsonConvert.SerializeObject(new
                            {
                                err = "换题失败err"
                            });
                            context.Response.Write(strJson);
                        }
                        #endregion
                        break;
                    case "combination_testpaperchapter_tq_list":
                        #region 章节组卷 手动选题 /teacher/PrecizeTopicSelection.aspx
                        try
                        {
                            ChapterAssembly_TQ_Id = context.Request["ChapterAssembly_TQ_Id"];
                            Model_ChapterAssembly_TQ ChapterAssembly_TQ = new BLL_ChapterAssembly_TQ().GetModel(ChapterAssembly_TQ_Id);
                            if (ChapterAssembly_TQ != null)
                            {
                                Identifier_Id = ChapterAssembly_TQ.Identifier_Id;
                                string TQ_Type = ChapterAssembly_TQ.TQ_Type;
                                string ComplexityText = ChapterAssembly_TQ.ComplexityText;
                                //题的
                                string sqlTQ1 = @"select distinct t.Identifier_Id,t.type,t.TQ_Type,t.ComplexityText
,tq.*,rtf.ResourceToResourceFolder_Id,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from ChapterAssembly_TQTemp t
left join TestQuestions tq on tq.TestQuestions_Id=t.TestQuestions_Id
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where t.Identifier_Id='" + Identifier_Id + "' and t.TQ_Type='" + TQ_Type + "' and t.ComplexityText='" + ComplexityText + "' and t.TestQuestions_Id not in(select TestQuestions_Id from ChapterAssembly_TQ where Identifier_Id='" + Identifier_Id + "')";
                                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQ1).Tables[0];
                                //分的
                                string sqlTQScore1 = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex from TestQuestions_Score tqs 
where tqs.TestQuestions_Id in 
(select TestQuestions_Id from ChapterAssembly_TQTemp where Identifier_Id='" + Identifier_Id + "' and TestQuestions_Id not in(select TestQuestions_Id from ChapterAssembly_TQ where Identifier_Id='" + Identifier_Id + "'))";
                                dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQScore1).Tables[0];
                                saveOwnPath = string.Empty;
                                if (dt.Rows.Count > 0)
                                {
                                    stbHtml.Append("<div class=\"panel_body\">");
                                    DataRow[] drTQ = dt.Select("type='simple'");
                                    drTQBig = dt.Select("type='complex'");
                                    #region 普通题
                                    if (drTQ.Length > 0)
                                    {
                                        for (int i = 0; i < drTQ.Length; i++)
                                        {
                                            //生成存储路径
                                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                               , drTQ[i]["ParticularYear"].ToString()
                                               , drTQ[i]["GradeTerm"].ToString()
                                               , drTQ[i]["Resource_Version"].ToString()
                                               , drTQ[i]["Subject"].ToString());
                                            fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                                            DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + drTQ[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                            //题干
                                            string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", drTQ[i]["TestQuestions_Id"], "htm"));
                                            //选择题、完形填空题选项
                                            string strOption = string.Empty;
                                            if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest")
                                            {
                                                for (int ii = 0; ii < drScore.Length; ii++)
                                                {
                                                    //从文件读取选择题选项
                                                    string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                    List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                    if (listTestSelections != null && listTestSelections.Count > 0)
                                                    {
                                                        foreach (var itemSelections in listTestSelections)
                                                        {
                                                            if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                        }
                                                        if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                        {
                                                            strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                        }
                                                    }
                                                }
                                            }
                                            if (drTQ[i]["TestQuestions_Type"].ToString() != "title" && drTQ[i]["TestQuestions_Type"].ToString() != "")
                                            {
                                                stbHtml.AppendFormat("<div class=\"question_panel ba\" id='change_{1}' data-value='{1}'><a name=\"{0}\"></a>", drTQ[i]["TestQuestions_Num"].ToString().TrimEnd('.'), ChapterAssembly_TQ_Id);
                                                stbHtml.AppendFormat("<div class='panel_stem '>{0}</div><div class='panel_option'>{1}</div>"
                                                    , pfunction.NoHTML(strTestQuestionBody) //题干
                                                    , strOption //选项
                                                    );

                                                stbHtml.Append("<div class='panel_answer'>");
                                                stbHtml.Append("<div class='answer_heading'>");
                                                stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-sm btn-block'>分值</span></div>");
                                                stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                                stbHtml.Append("</div>");
                                                for (int ii = 0; ii < drScore.Length; ii++)
                                                {
                                                    stbHtml.Append("<div class='answer_body'>");
                                                    //正确答案
                                                    if (drTQ[i]["TestQuestions_Type"].ToString() == "selection" || drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" || drTQ[i]["TestQuestions_Type"].ToString() == "truefalse")
                                                    {
                                                        stbHtml.AppendFormat("<div class='answer_item score'>{0}{1} 分</div>"
                                                            , drTQ[i]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                            , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                        stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                            , drScore[ii]["TestCorrect"]);

                                                    }
                                                    if (drTQ[i]["TestQuestions_Type"].ToString() == "fill" || drTQ[i]["TestQuestions_Type"].ToString() == "answers")
                                                    {
                                                        //从文件读取正确答案
                                                        string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                        if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                        {
                                                            stbHtml.AppendFormat("<div class='answer_item score'>{0}{1}分</div>"
                                                                , drTQ[i]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                            stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                                , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));


                                                        }
                                                    }
                                                    stbHtml.Append("</div>");
                                                }
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("<div class='panel_opera'>");
                                                stbHtml.Append("<div class='opera_control'>");
                                                stbHtml.AppendFormat("<span  class='btn ischange-hook' tt='{0}'><i class='material-icons'>&#xE065;</i>&nbsp;选择</span>", drTQ[i]["TestQuestions_Id"].ToString());
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");
                                                stbHtml.Append("</div>");
                                            }
                                            else
                                            {
                                                stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\"><div class='panel_stem'>{0}</div></div>", pfunction.NoHTML(strTestQuestionBody));
                                            }
                                        }

                                    }

                                    #endregion
                                    #region 综合题
                                    if (drTQBig.Length > 0)
                                    {

                                        for (int i = 0; i < drTQBig.Length; i++)
                                        {
                                            string sql = @" select * from ( select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where Parent_Id='" + drTQBig[i]["TestQuestions_id"].ToString() + @"'
union 
select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where TestQuestions_id='" + drTQBig[i]["TestQuestions_id"].ToString() + "' ) t order by TestQuestions_Num";
                                            DataTable dtBig = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                                            sql = @" select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex from TestQuestions_Score tqs 
where tqs.TestQuestions_Id in (select TestQuestions_Id from TestQuestions where Parent_Id='" + drTQBig[i]["TestQuestions_id"].ToString() + "')";
                                            dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                                            if (dtBig.Rows.Count > 0)
                                            {
                                                for (int j = 0; j < dtBig.Rows.Count; j++)
                                                {
                                                    //生成存储路径
                                                    savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                                       , dtBig.Rows[j]["ParticularYear"].ToString()
                                                       , dtBig.Rows[j]["GradeTerm"].ToString()
                                                       , dtBig.Rows[j]["Resource_Version"].ToString()
                                                       , dtBig.Rows[j]["Subject"].ToString());
                                                    fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                                                    DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dtBig.Rows[j]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                                    //题干
                                                    string strTestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dtBig.Rows[j]["TestQuestions_Id"], "htm"));
                                                    //选择题、完形填空题选项
                                                    string strOption = string.Empty;
                                                    if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "selection" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest")
                                                    {
                                                        for (int ii = 0; ii < drScore.Length; ii++)
                                                        {
                                                            //从文件读取选择题选项
                                                            string strTestQuestionOption = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                            List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                                            if (listTestSelections != null && listTestSelections.Count > 0)
                                                            {
                                                                foreach (var itemSelections in listTestSelections)
                                                                {
                                                                    if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", pfunction.NoHTML(itemSelections.selectionHTML));
                                                                }
                                                                if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                                                {
                                                                    strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (dtBig.Rows[j]["TestQuestions_Type"].ToString() != "title" && dtBig.Rows[j]["TestQuestions_Type"].ToString() != "")
                                                    {
                                                        stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook\"><a name=\"{0}\"></a>", dtBig.Rows[j]["TestQuestions_Num"].ToString().TrimEnd('.'));
                                                        stbHtml.AppendFormat("<div class='panel_stem panel-stem-hook'>{0}</div><div class='panel_option'>{1}</div>"
                                                            , pfunction.NoHTML(strTestQuestionBody) //题干
                                                            , strOption //选项
                                                            );

                                                        stbHtml.Append("<div class='panel_answer'>");
                                                        stbHtml.Append("<div class='answer_heading'>");
                                                        stbHtml.Append("<div class='answer_item score'><span class='btn btn-info btn-sm btn-block'>分值</span></div>");
                                                        stbHtml.Append("<div class='answer_item reference_answer'><span class='btn btn-success btn-sm'>参考答案</span></div>");
                                                        stbHtml.Append("</div>");
                                                        for (int ii = 0; ii < drScore.Length; ii++)
                                                        {
                                                            stbHtml.Append("<div class='answer_body'>");
                                                            //正确答案
                                                            if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "selection" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "truefalse")
                                                            {
                                                                stbHtml.AppendFormat("<div class='answer_item score'>{0}{1}分</div>"
                                                                    , dtBig.Rows[j]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                    , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                                stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0} 分</div>"
                                                                    , drScore[ii]["TestCorrect"]);

                                                            }
                                                            if (dtBig.Rows[j]["TestQuestions_Type"].ToString() == "fill" || dtBig.Rows[j]["TestQuestions_Type"].ToString() == "answers")
                                                            {
                                                                //从文件读取正确答案
                                                                string strTestQuestionCurrent = pfunction.ReadAllText(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"));
                                                                if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                                                {
                                                                    stbHtml.AppendFormat("<div class='answer_item score'>{0}{1}</div>"
                                                                        , dtBig.Rows[j]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                                                        , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));

                                                                    stbHtml.AppendFormat("<div class='answer_item reference_answer'>{0}</div>"
                                                                        , pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));


                                                                }
                                                            }
                                                            stbHtml.Append("</div>");
                                                        }
                                                        stbHtml.Append("</div>");
                                                        stbHtml.Append("</div>");

                                                    }
                                                    else
                                                    {
                                                        stbHtml.AppendFormat("<div class=\"question_panel question-panel-hook ba\"><div class='panel_stem'>{0}</div>", pfunction.NoHTML(strTestQuestionBody), ChapterAssembly_TQ_Id);
                                                        stbHtml.Append("<div class='panel_opera'>");
                                                        stbHtml.Append("<div class='opera_control'>");
                                                        if (dtBig.Rows[j]["TestQuestions_Type"].ToString() != "title") stbHtml.AppendFormat("<span  class='btn ischange-hook' tt='{0}'><i class='material-icons'>&#xE065;</i>&nbsp;选择</span>", dtBig.Rows[j]["TestQuestions_Id"].ToString());
                                                        stbHtml.Append("</div>");
                                                        stbHtml.Append("</div>");
                                                    }
                                                }
                                                stbHtml.Append("</div>");
                                            }
                                        }

                                    }

                                    #endregion

                                    stbHtml.Append("</div>");
                                }
                                else
                                {
                                    stbHtml.Append("<div class='text-center ph'>暂无可供切换的试题</div>");
                                }
                                context.Response.Write(stbHtml.ToString());
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        #endregion
                        break;
                    case "combination_testpaperchapter_confirm":
                        #region 章节组卷 确认组卷 /teacher/ChapterAssemblySelectSaveCatelog.aspx
                        stbHtml = new StringBuilder();
                        Identifier_Id = context.Request["Identifier_Id"].Filter();
                        Name = context.Request["ReName"].Filter();
                        Titles = context.Request["Title"].Filter();
                        CreateUser = context.Request["CreateUser"].Filter();
                        string ComplexityTexts = context.Request["ComplexityTexts"].Filter();
                        string RtrfType = context.Request["RtrfType"].Filter();
                        string ResourceFolder_Id = context.Request["ResourceFolder_Id"].Filter();
                        try
                        {
                            string[] ArrTitle = Titles.Split('♧');
                            StringBuilder stbSql = new StringBuilder();

                            #region 资源
                            string RID = Guid.NewGuid().ToString();
                            string ReID = Guid.NewGuid().ToString();
                            string ReTime = DateTime.Now.ToString();

                            #region 插入 Resource
                            stbSql.AppendFormat(@"INSERT INTO [dbo].[Resource]([Resource_Id],[Resource_MD5],[Resource_DataStrem],[Resource_ContentHtml],[CreateTime],[Resource_ContentLength])VALUES('{0}','{1}','{2}','{3}','{4}','{5}');", RID, "", "", "", DateTime.Now, "0");
                            #endregion
                            #region 插入 ResourceToResourceFolder
                            stbSql.AppendFormat(@"INSERT INTO [dbo].[ResourceToResourceFolder]
           ([ResourceToResourceFolder_Id]
           ,[ResourceFolder_Id]
           ,[Resource_Id]
           ,[File_Name]
           ,[Resource_Type]
           ,[Resource_Name]
           ,[Resource_Class]
           ,[Resource_Version]
           ,[File_Owner]
           ,[CreateFUser]
           ,[CreateTime]
           ,[UpdateTime]
           ,[File_Suffix]
           ,[LessonPlan_Type]
           ,[GradeTerm]
           ,[Subject]
           ,[Resource_Domain]
           ,[Resource_Url]
           ,[Resource_shared]
           ,[Book_ID]
           ,[ParticularYear]
           ,[ResourceToResourceFolder_Order])
     VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}');"
                                , ReID
                                , ResourceFolder_Id
                                , RID
                                , Name
                                , Resource_TypeConst.testPaper类型文件
                                , Name
                                , Resource_ClassConst.自有资源
                                , ""
                                , CreateUser
                                , CreateUser
                                , ReTime
                                , ""
                                , "testPaper"
                                , LessonPlan_TypeConst.组卷
                                , ""
                                , ""
                                , ""
                                , ""
                                , ""
                                , ""
                                , ""
                                , 0);
                            #endregion
                            #endregion
                            //题的
                            string sqlTQ = @"select tw.ChapterAssembly_TQ_Id,tw.Identifier_Id,tw.Parent_Id,tw.TQ_Order,tw.TQ_Type,tw.ComplexityText,tw.type as chapter_type
,tq.*,rtf.ResourceToResourceFolder_Id,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from ChapterAssembly_TQ tw
left join TestQuestions tq on tq.TestQuestions_Id=tw.TestQuestions_Id
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where tw.Identifier_Id='" + Identifier_Id + "' order by tw.TQ_Order";
                            dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQ).Tables[0];
                            //分的
                            string sqlTQScore = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex,tqs.AnalyzeHyperlinkData,tqs.TrainHyperlinkData,tqs.TestType from TestQuestions_Score tqs 
where tqs.TestQuestions_Id in 
(select rp.TestQuestions_Id from ChapterAssembly_TQ rp
where Identifier_Id='" + Identifier_Id + "')";
                            dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQScore).Tables[0];

                            saveOwnPath = string.Empty;
                            string TempTQ = @"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type])
     VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');";
                            int i = 0;
                            int Num = 1;
                            //父级普通TQ
                            DataRow[] drTQtitle = dtTQ.Select("Parent_Id='0'", "TQ_Order");
                            //#region 存作业内容中的试卷名称
                            //string ReNameGuid = Guid.NewGuid().ToString();
                            //string strReName = string.Format(TempTQ
                            //        , ReNameGuid
                            //        , ReID
                            //        , 0
                            //        , "title"
                            //        , "0"
                            //        , ""
                            //        , ""
                            //        , DateTime.Now
                            //        , ""
                            //        , "0"
                            //        , "simple");
                            //stbSql.Append(strReName);//作业内容中的试卷名称
                            ////创建txt
                            //string filePath = string.Format("{0}{1}/{2}/{3}.txt", uploadPath, pfunction.ToShortDate(ReTime), "textTitle", ReNameGuid);
                            //pfunction.WriteToFile(filePath, Name, true);
                            //#endregion
                            foreach (DataRow itemtitle in drTQtitle)
                            {
                                #region 如果是标题
                                if (string.IsNullOrEmpty(itemtitle["TestQuestions_Id"].ToString()))
                                {
                                    string TQGuid = Guid.NewGuid().ToString();
                                    string strTQ = string.Format(TempTQ
                                    , TQGuid
                                    , ReID
                                    , Num
                                    , "title"
                                    , "0"
                                    , ""
                                    , ""
                                    , DateTime.Now
                                    , ""
                                    , "0"
                                    , "simple");
                                    stbSql.Append(strTQ);//插入标题（一.选择题）

                                    if (!string.IsNullOrEmpty(ArrTitle[i].ToString()))
                                    {
                                        //创建txt
                                        string filePath = string.Format("{0}{1}/{2}/{3}.txt", uploadPath, pfunction.ToShortDate(ReTime), "textTitle", TQGuid);
                                        pfunction.WriteToFile(filePath, ArrTitle[i].ToString(), true);
                                    }

                                    //子集TQ
                                    DataRow[] drSonTQ = dtTQ.Select(" Parent_Id='" + itemtitle["ChapterAssembly_TQ_Id"].ToString() + "'", "TQ_Order");
                                    if (drSonTQ.Length > 0)
                                    {
                                        foreach (DataRow itemSon in drSonTQ)
                                        {
                                            Num++;
                                            #region 普通题
                                            if (itemSon["type"].ToString() == "simple")
                                            {
                                                #region 插入题
                                                string TQSonGuid = Guid.NewGuid().ToString();
                                                stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type] )
     select '{0}','{1}','{2}',TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,'{3}','{4}','{6}',[type] from TestQuestions
	 where TestQuestions_Id='{5}';", TQSonGuid, ReID, Num, DateTime.Now.ToString(), itemSon["topicNumber"].ToString(), itemSon["TestQuestions_Id"].ToString(), TQGuid);
                                                #endregion
                                                #region 插入老题与新题的关系表
                                                stbSql.AppendFormat(@"INSERT INTO [dbo].[ChapterAssembly_TQRelation]
           ([ChapterAssembly_TQRelation_Id]
           ,[OldTQ]
           ,[NewTQ]
           ,[type]
           ,[ResourceToResourceFolder_Id])
     VALUES(newid(),'{0}','{1}','{2}','{3}');", itemSon["TestQuestions_Id"].ToString(), TQSonGuid, '1', ReID);
                                                #endregion
                                                #region 存文件
                                                //生成存储路径
                                                savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                                    , itemSon["ParticularYear"].ToString()
                                                    , itemSon["GradeTerm"].ToString()
                                                    , itemSon["Resource_Version"].ToString()
                                                    , itemSon["Subject"].ToString());

                                                fileUrl = uploadPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径
                                                string fileloactionurl = uploadPath + "{0}\\{1}\\" + "{2}.{3}";

                                                pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemSon["TestQuestions_Id"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQSonGuid, "txt"));

                                                pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemSon["TestQuestions_Id"], "htm")
                                                   , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQSonGuid, "htm"));

                                                #endregion
                                                DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + itemSon["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                                if (drScore.Length > 0)
                                                {
                                                    for (int jj = 0; jj < drScore.Length; jj++)
                                                    {
                                                        #region 插入分

                                                        string ScoreGuid = Guid.NewGuid().ToString();
                                                        string tempAnalyzeData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=AnalyzeData
", ReID, ScoreGuid);
                                                        string tempTrainData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=TrainData
", ReID, ScoreGuid);
                                                        stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Score]
           ([TestQuestions_Score_ID]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_OrderNum]
           ,[TestQuestions_Score]
           ,[AnalyzeHyperlink]
           ,[AnalyzeHyperlinkData]
           ,[AnalyzeHyperlinkHtml]
           ,[AnalyzeText]
           ,[ComplexityHyperlink]
           ,[ComplexityText]
           ,[ContentHyperlink]
           ,[ContentText]
           ,[DocBase64]
           ,[DocHtml]
           ,[ScoreHyperlink]
           ,[ScoreText]
           ,[TargetHyperlink]
           ,[TrainHyperlinkData]
           ,[TrainHyperlinkHtml]
           ,[TargetText]
           ,[TestCorrect]
           ,[TestType]
           ,[TrainHyperlink]
           ,[TrainText]
           ,[TypeHyperlink]
           ,[TypeText]
           ,[CreateTime]
           ,[AreaHyperlink]
           ,[AreaText]
           ,[kaofaText]
           ,[testIndex] )
     select '{0}','{1}','{2}',TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,'{5}',AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,'{6}',TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,'{3}',AreaHyperlink,AreaText,kaofaText,testIndex from TestQuestions_Score where TestQuestions_Score_ID='{4}' ;", ScoreGuid, ReID, TQSonGuid, DateTime.Now.ToString(), drScore[jj]["TestQuestions_Score_ID"].ToString()
                                                            , string.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()) ? "" : tempAnalyzeData
                                                            , string.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()) ? "" : tempTrainData);
                                                        #region 插入老空与新空的关系表
                                                        stbSql.AppendFormat(@"INSERT INTO [dbo].[ChapterAssembly_TQRelation]
           ([ChapterAssembly_TQRelation_Id]
           ,[OldTQ]
           ,[NewTQ]
           ,[type]
           ,[ResourceToResourceFolder_Id])
     VALUES(newid(),'{0}','{1}','{2}','{3}');", drScore[jj]["TestQuestions_Score_ID"].ToString(), ScoreGuid, '2', ReID);
                                                        #endregion
                                                        #endregion
                                                        #region 存文件

                                                        #region 正确答案
                                                        //不是选择题填空判断下载正确答案
                                                        if (drScore[jj]["TestType"].ToString() == "fill" || drScore[jj]["TestType"].ToString() == "answers")
                                                        {
                                                            pfunction.CopyToFile(string.Format(fileUrl, "testQuestionCurrent", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionCurrent", ScoreGuid, "txt"));

                                                        }
                                                        #endregion
                                                        #region 选择题的选项
                                                        if (drScore[jj]["TestType"].ToString() == "selection" || drScore[jj]["TestType"].ToString() == "clozeTest")
                                                        {
                                                            pfunction.CopyToFile(string.Format(fileUrl, "testQuestionOption", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionOption", ScoreGuid, "txt"));

                                                        }
                                                        #endregion
                                                        #region 解析 保存文件
                                                        if (!String.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()))
                                                        {
                                                            pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "AnalyzeData", ScoreGuid, "txt"));

                                                            pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "AnalyzeHtml", ScoreGuid, "htm"));

                                                        }
                                                        #endregion
                                                        #region 强化训练 保存文件
                                                        if (!String.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()))
                                                        {
                                                            pfunction.CopyToFile(string.Format(fileUrl, "TrainData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "TrainData", ScoreGuid, "txt"));

                                                            pfunction.CopyToFile(string.Format(fileUrl, "TrainHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "TrainHtml", ScoreGuid, "htm"));

                                                        }
                                                        #endregion
                                                        #region 子题题干Html 保存文件bodySub
                                                        pfunction.CopyToFile(string.Format(fileUrl, "bodySub", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                                    , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "bodySub", ScoreGuid, "htm"));
                                                        #endregion
                                                        #endregion
                                                        #region 插入选项
                                                        string sqlOption = "select * from TestQuestions_Option where TestQuestions_Score_ID='" + drScore[jj]["TestQuestions_Score_ID"] + "'";
                                                        DataTable dtOption = Rc.Common.DBUtility.DbHelperSQL.Query(sqlOption).Tables[0];
                                                        if (dtOption.Rows.Count > 0)
                                                        {
                                                            foreach (DataRow item in dtOption.Rows)
                                                            {
                                                                stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Option]
           ([TestQuestions_Option_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_OptionParent_OrderNum]
           ,[TestQuestions_Option_Content]
           ,[TestQuestions_Option_OrderNum]
           ,[CreateTime]
           ,[TestQuestions_Score_ID] )
     select Newid(),'{0}',TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,'{1}','{2}' from TestQuestions_Option where TestQuestions_Option_Id='{3}' ;", TQSonGuid, DateTime.Now.ToString(), ScoreGuid, item["TestQuestions_Option_Id"].ToString());
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                    i++;
                                }
                                else
                                {
                                    #region 综合题
                                    #region 插入综合题提干
                                    #region 插入题
                                    string TQSonGuid = Guid.NewGuid().ToString();
                                    stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type] )
     select '{0}','{1}','{2}',TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,'{3}','{4}','0',[type] from TestQuestions
	 where TestQuestions_Id='{5}';", TQSonGuid, ReID, Num, DateTime.Now.ToString(), itemtitle["topicNumber"].ToString(), itemtitle["TestQuestions_Id"].ToString());
                                    #endregion
                                    #region 插入老题与新题的关系表
                                    stbSql.AppendFormat(@"INSERT INTO [dbo].[ChapterAssembly_TQRelation]
           ([ChapterAssembly_TQRelation_Id]
           ,[OldTQ]
           ,[NewTQ]
           ,[type]
           ,[ResourceToResourceFolder_Id])
     VALUES(newid(),'{0}','{1}','{2}','{3}');", itemtitle["TestQuestions_Id"].ToString(), TQSonGuid, '1', ReID);
                                    #endregion
                                    #region 存文件
                                    //生成存储路径
                                    savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                       , itemtitle["ParticularYear"].ToString()
                                       , itemtitle["GradeTerm"].ToString()
                                       , itemtitle["Resource_Version"].ToString()
                                       , itemtitle["Subject"].ToString());
                                    fileUrl = uploadPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径
                                    string fileloactionurl = uploadPath + "{0}\\{1}\\" + "{2}.{3}";

                                    pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemtitle["TestQuestions_Id"], "txt")
                                        , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQSonGuid, "txt"));

                                    pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemtitle["TestQuestions_Id"], "htm")
                                        , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQSonGuid, "htm"));

                                    #endregion
                                    #endregion
                                    #region 综合题小题
                                    string sql = @" select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject from TestQuestions tq 
left join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where Parent_Id='" + itemtitle["TestQuestions_id"].ToString() + "'order by TestQuestions_Num";
                                    DataTable dtBigTQ = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                                    string sqlScore = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex,tqs.AnalyzeHyperlinkData,tqs.TrainHyperlinkData,tqs.TestType from TestQuestions_Score tqs where tqs.TestQuestions_Id in(select TestQuestions_Id from TestQuestions 
where Parent_Id='" + itemtitle["TestQuestions_id"].ToString() + "')";
                                    DataTable dtBigScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlScore).Tables[0];
                                    if (dtBigTQ.Rows.Count > 0)
                                    {
                                        foreach (DataRow itemBigTQ in dtBigTQ.Rows)
                                        {
                                            Num++;
                                            #region 插入题
                                            string TQBGuid = Guid.NewGuid().ToString();
                                            stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type] )
     select '{0}','{1}','{2}',TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,'{3}',topicNumber,'{4}',[type] from TestQuestions
	 where TestQuestions_Id='{5}';", TQBGuid, ReID, Num, DateTime.Now.ToString(), TQSonGuid, itemBigTQ["TestQuestions_Id"].ToString());
                                            #endregion
                                            #region 插入老题与新题的关系表
                                            stbSql.AppendFormat(@"INSERT INTO [dbo].[ChapterAssembly_TQRelation]
           ([ChapterAssembly_TQRelation_Id]
           ,[OldTQ]
           ,[NewTQ]
           ,[type]
           ,[ResourceToResourceFolder_Id])
     VALUES(newid(),'{0}','{1}','{2}','{3}');", itemBigTQ["TestQuestions_Id"].ToString(), TQBGuid, '1', ReID);
                                            #endregion
                                            #region 存文件
                                            //生成存储路径
                                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                                               , itemBigTQ["ParticularYear"].ToString()
                                               , itemBigTQ["GradeTerm"].ToString()
                                               , itemBigTQ["Resource_Version"].ToString()
                                               , itemBigTQ["Subject"].ToString());
                                            fileUrl = uploadPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径
                                            fileloactionurl = uploadPath + "{0}\\{1}\\" + "{2}.{3}";

                                            pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemBigTQ["TestQuestions_Id"], "txt")
                                        , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQBGuid, "txt"));

                                            pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemBigTQ["TestQuestions_Id"], "htm")
                                        , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionBody", TQBGuid, "htm"));

                                            #endregion
                                            DataRow[] drScore = dtBigScore.Select("TestQuestions_Id='" + itemBigTQ["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                                            if (drScore.Length > 0)
                                            {
                                                for (int jj = 0; jj < drScore.Length; jj++)
                                                {
                                                    #region 插入分

                                                    string ScoreGuid = Guid.NewGuid().ToString();
                                                    string tempAnalyzeData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=AnalyzeData
", ReID, ScoreGuid);
                                                    string tempTrainData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=TrainData
", ReID, ScoreGuid);
                                                    stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Score]
           ([TestQuestions_Score_ID]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_OrderNum]
           ,[TestQuestions_Score]
           ,[AnalyzeHyperlink]
           ,[AnalyzeHyperlinkData]
           ,[AnalyzeHyperlinkHtml]
           ,[AnalyzeText]
           ,[ComplexityHyperlink]
           ,[ComplexityText]
           ,[ContentHyperlink]
           ,[ContentText]
           ,[DocBase64]
           ,[DocHtml]
           ,[ScoreHyperlink]
           ,[ScoreText]
           ,[TargetHyperlink]
           ,[TrainHyperlinkData]
           ,[TrainHyperlinkHtml]
           ,[TargetText]
           ,[TestCorrect]
           ,[TestType]
           ,[TrainHyperlink]
           ,[TrainText]
           ,[TypeHyperlink]
           ,[TypeText]
           ,[CreateTime]
           ,[AreaHyperlink]
           ,[AreaText]
           ,[kaofaText]
           ,[testIndex] )
     select '{0}','{1}','{2}',TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,'{5}',AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,'{6}',TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,'{3}',AreaHyperlink,AreaText,kaofaText,testIndex from TestQuestions_Score where TestQuestions_Score_ID='{4}' ;", ScoreGuid, ReID, TQBGuid, DateTime.Now.ToString(), drScore[jj]["TestQuestions_Score_ID"].ToString()
                                                        , string.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()) ? "" : tempAnalyzeData
                                                        , string.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()) ? "" : tempTrainData);
                                                    #endregion
                                                    #region 插入老空与新空的关系表
                                                    stbSql.AppendFormat(@"INSERT INTO [dbo].[ChapterAssembly_TQRelation]
           ([ChapterAssembly_TQRelation_Id]
           ,[OldTQ]
           ,[NewTQ]
           ,[type]
           ,[ResourceToResourceFolder_Id])
     VALUES(newid(),'{0}','{1}','{2}','{3}');", drScore[jj]["TestQuestions_Score_ID"].ToString(), ScoreGuid, '2', ReID);
                                                    #endregion
                                                    #region 存文件

                                                    #region 正确答案
                                                    //不是选择题填空判断下载正确答案
                                                    if (drScore[jj]["TestType"].ToString() == "fill" || drScore[jj]["TestType"].ToString() == "answers")
                                                    {
                                                        pfunction.CopyToFile(string.Format(fileUrl, "testQuestionCurrent", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                        , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionCurrent", ScoreGuid, "txt"));
                                                    }
                                                    #endregion
                                                    #region 选择题的选项
                                                    if (drScore[jj]["TestType"].ToString() == "selection" || drScore[jj]["TestType"].ToString() == "clozeTest")
                                                    {
                                                        pfunction.CopyToFile(string.Format(fileUrl, "testQuestionOption", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                        , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "testQuestionOption", ScoreGuid, "txt"));
                                                    }
                                                    #endregion
                                                    #region 解析 保存文件
                                                    if (!String.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()))
                                                    {
                                                        pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                       , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "AnalyzeData", ScoreGuid, "txt"));

                                                        pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                       , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "AnalyzeHtml", ScoreGuid, "htm"));

                                                    }
                                                    #endregion
                                                    #region 强化训练 保存文件
                                                    if (!String.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()))
                                                    {
                                                        pfunction.CopyToFile(string.Format(fileUrl, "TrainData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                            , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "TrainData", ScoreGuid, "txt"));

                                                        pfunction.CopyToFile(string.Format(fileUrl, "TrainHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                       , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "TrainHtml", ScoreGuid, "htm"));

                                                    }
                                                    #endregion
                                                    #region 子题题干Html 保存文件bodySub
                                                    pfunction.CopyToFile(string.Format(fileUrl, "bodySub", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                                            , string.Format(fileloactionurl, pfunction.ToShortDate(ReTime), "bodySub", ScoreGuid, "txt"));
                                                    #endregion
                                                    #endregion
                                                    #region 插入选项
                                                    string sqlOption = "select * from TestQuestions_Option where TestQuestions_Score_ID='" + drScore[jj]["TestQuestions_Score_ID"] + "'";
                                                    DataTable dtOption = Rc.Common.DBUtility.DbHelperSQL.Query(sqlOption).Tables[0];
                                                    if (dtOption.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow item in dtOption.Rows)
                                                        {
                                                            stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Option]
           ([TestQuestions_Option_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_OptionParent_OrderNum]
           ,[TestQuestions_Option_Content]
           ,[TestQuestions_Option_OrderNum]
           ,[CreateTime]
           ,[TestQuestions_Score_ID] )
     select Newid(),'{0}',TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,'{1}','{2}' from TestQuestions_Option where TestQuestions_Option_Id='{3}' ;", TQBGuid, DateTime.Now.ToString(), ScoreGuid, item["TestQuestions_Option_Id"].ToString());
                                                        }
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #endregion
                                }
                                #endregion
                                Num++;

                            }
                            #region 插入试卷属性表 难易度和类型（周月单元测）
                            stbSql.AppendFormat(@"INSERT INTO [dbo].[ResourceToResourceFolder_Attr]
           ([ResourceToResourceFolder_Attr_Id]
           ,[ResourceToResourceFolder_Id]
           ,[AttrEnum]
           ,[AttrValue]
           ,[Remark]
           ,[CreateUser]
           ,[CreateTime]
           ,[UpdateUser]
           ,[UpdateTime])
     VALUES
           (newid(),'{0}','{1}','{2}','{3}','{4}','{5}','','');", ReID, ResourceToResourceFolder_Attr.ComplexityText, ComplexityTexts, "", CreateUser, DateTime.Now);
                            stbSql.AppendFormat(@"INSERT INTO [dbo].[ResourceToResourceFolder_Attr]
           ([ResourceToResourceFolder_Attr_Id]
           ,[ResourceToResourceFolder_Id]
           ,[AttrEnum]
           ,[AttrValue]
           ,[Remark]
           ,[CreateUser]
           ,[CreateTime]
           ,[UpdateUser]
           ,[UpdateTime])
     VALUES
           (newid(),'{0}','{1}','{2}','{3}','{4}','{5}','','');", ReID, ResourceToResourceFolder_Attr.RtrfType, RtrfType, "", CreateUser, DateTime.Now);
                            #endregion
                            //pfunction.WriteToFile(uploadPath + "1.txt", stbSql.ToString(), true);
                            #region 插入新tq与知识点关系，tq属性表，和tqscore属性表
                            stbSql.AppendFormat(@"insert into [dbo].[S_TQ_AttrExtend] (S_TQ_Attr_Id,ResourceToResourceFolder_Id,TestQuestions_Id,AttrEnum,AttrValue,Remark,CreateUser,CreateTime)
select NEWID(),t.ResourceToResourceFolder_Id,NewTQ,t1.AttrEnum,t1.AttrValue,t1.Remark,'',GETDATE() from ChapterAssembly_TQRelation t
inner  join [S_TQ_AttrExtend] t1 on t.OldTQ=t1.TestQuestions_Id 
where t.ResourceToResourceFolder_Id='{0}' and [type]='1';", ReID);
                            stbSql.AppendFormat(@"insert into [dbo].[S_TQ_Score_AttrExtend] (S_TQ_Score_Attr_Id,ResourceToResourceFolder_Id,TestQuestions_Id,TestQuestions_Score_Id,AttrEnum,Attr_Value,CreateUser,CreateTime)
select NEWID(),t.ResourceToResourceFolder_Id,t2.TestQuestions_Id,NewTQ,t1.AttrEnum,t1.Attr_Value,'',GETDATE() from ChapterAssembly_TQRelation t
inner  join [S_TQ_Score_AttrExtend] t1 on t.OldTQ=t1.TestQuestions_Score_Id 
inner join TestQuestions_Score t2 on t2.TestQuestions_Score_Id=t.NewTQ
where t.ResourceToResourceFolder_Id='{0}' and [type]='2';", ReID);
                            stbSql.AppendFormat(@"insert into [dbo].[S_TestQuestions_KP] (S_TestQuestions_KP_Id,ResourceToResourceFolder_Id,TestQuestions_Id,TestQuestions_Score_Id,S_KnowledgePoint_Id,CreateUser,CreateTime)
select NEWID(),t.ResourceToResourceFolder_Id,t2.TestQuestions_Id,NewTQ,S_KnowledgePoint_Id,'',GETDATE() from ChapterAssembly_TQRelation t
inner  join [S_TestQuestions_KP] t1 on t.OldTQ=t1.TestQuestions_Score_Id 
inner join TestQuestions_Score t2 on t2.TestQuestions_Score_Id=t.NewTQ
where t.ResourceToResourceFolder_Id='{0}' and [type]='2';", ReID);
                            #endregion
                            if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(stbSql.ToString()) > 0)
                            {
                                context.Response.Write("ok");
                            }
                            else
                            {
                                context.Response.Write("组卷失败：写入数据失败");
                                context.Response.End();
                            }
                        }
                        catch (Exception ex)
                        {
                            Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(Identifier_Id, "", string.Format("章节组卷失败：|唯一标识{0}|方法{1}|错误信息{2}", Identifier_Id, "ConfirmTestpaperchapter", ex.Message.ToString()));
                            context.Response.Write("组卷失败err");
                        }

                        #endregion
                        break;
                    default:
                        break;
                }


            }
            catch (Exception)
            {
                context.Response.Write("加载失败err！！！");
            }
            context.Response.End();    
        }

        private string GetAvgScore(string TestQuestions_Score_ID)
        {
            string temp = string.Empty;
            try
            {
                TestQuestions_Score_ID = TestQuestions_Score_ID.Filter();
                temp = "{0} 分 (答对{1}人/答错{2}人/不全对{3}人/班级得分率{4}%）";
                List<Model_StatsClassHW_TQ> list = listHW_TQ.Where(w => w.TestQuestions_Score_ID == TestQuestions_Score_ID).ToList();
                int rightCount = 0;
                int wrongCount = 0;
                int partrightCount = 0;
                DataRow[] drRight = dtTQ_StudentAnswerCount.Select("Student_Answer_Status='right' and TestQuestions_Score_ID='" + TestQuestions_Score_ID + "' ");
                if (drRight.Length != 0) int.TryParse(drRight[0]["ICOUNT"].ToString(), out rightCount);

                DataRow[] drWrong = dtTQ_StudentAnswerCount.Select("Student_Answer_Status='wrong' and TestQuestions_Score_ID='" + TestQuestions_Score_ID + "' ");
                if (drWrong.Length != 0) int.TryParse(drWrong[0]["ICOUNT"].ToString(), out wrongCount);

                DataRow[] drpartRight = dtTQ_StudentAnswerCount.Select("Student_Answer_Status='partright' and TestQuestions_Score_ID='" + TestQuestions_Score_ID + "' ");
                if (drpartRight.Length != 0) int.TryParse(drpartRight[0]["ICOUNT"].ToString(), out partrightCount);
                if (list.Count > 0)
                {
                    temp = string.Format(temp
                                      , list[0].ScoreAvg.ToString().clearLastZero()
                                      , rightCount
                                      , wrongCount
                                      , partrightCount
                                      , list[0].ScoreAvgRate.ToString().clearLastZero());
                }
                else
                {
                    DataRow[] dr = dtTQScore_CLassVAG.Select("TestQuestions_Score_ID='" + TestQuestions_Score_ID + "'");
                    if (dr.Length > 0)
                    {
                        temp = string.Format(temp
                                      , dr[0]["vagSutScore"].ToString().clearLastZero()
                                      , rightCount
                                      , wrongCount
                                      , partrightCount
                                      , dr[0]["vagSutScore"].ToString().clearLastZero() == "0" ? "0" : decimal.Round(Convert.ToDecimal(dr[0]["vagSutScore"].ToString()) / Convert.ToDecimal(dr[0]["tqsScore"].ToString()) * 100).ToString().clearLastZero());
                    }
                    else
                    {
                        temp = "";
                    }
                }

            }
            catch (Exception)
            {
                temp = "";
            }
            return temp;
        }

        public DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone();  // 复制DataRow的表结构  
            foreach (DataRow row in rows)
                tmp.Rows.Add(row.ItemArray);  // 将DataRow添加到DataTable中  
            return tmp;
        }

        public class StudentAnswerData
        {
            public string testquestions_Id { set; get; }
            public int tqonum { set; get; }
            public decimal actualscore { set; get; }
            public decimal score { set; get; }
            public string comment { set; get; }
            public string isRead { set; get; }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetRFNameByRTRFId(string rtrfId)
        {
            string temp = string.Empty;
            try
            {
                string strSql = string.Format(@"select t.ResourceFolder_Id,t.ResourceToResourceFolder_Id,t.Resource_Name,t.Resource_Class,t.ParticularYear 
,t1.D_Name as GradeTerm_Name,t2.D_Name as Resource_Version_Name,t3.D_Name as Subject_Name
from ResourceToResourceFolder t 
left join Common_Dict t1 on t1.Common_Dict_Id=t.GradeTerm
left join Common_Dict t2 on t2.Common_Dict_Id=t.Resource_Version
left join Common_Dict t3 on t3.Common_Dict_Id=t.Subject
where t.ResourceToResourceFolder_Id='{0}' ", rtrfId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        temp = string.Format("{0}/{1}/{2}/{3}/{4}"
                            , dt.Rows[0]["ParticularYear"].ToString()
                            , dt.Rows[0]["GradeTerm_Name"].ToString()
                            , dt.Rows[0]["Resource_Version_Name"].ToString()
                            , dt.Rows[0]["Subject_Name"].ToString()
                            , GetRFNameByRFId(dt.Rows[0]["ResourceFolder_Id"].ToString(), "")
                            );
                    }
                    else if (dt.Rows[0]["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        temp = GetRFNameByRFId(dt.Rows[0]["ResourceFolder_Id"].ToString(), "");
                    }
                }
            }
            catch (Exception)
            {

            }
            return temp;
        }

        private string GetRFNameByRFId(string rfId, string result)
        {
            try
            {
                Model_ResourceFolder modelRF = new BLL_ResourceFolder().GetModel(rfId);
                if (modelRF != null && !string.IsNullOrEmpty(modelRF.ResourceFolder_Name))
                {
                    result = modelRF.ResourceFolder_Name + "/" + result;
                    result = result.TrimEnd('/');
                    if (modelRF.ResourceFolder_ParentId != "0" && !string.IsNullOrEmpty(modelRF.ResourceFolder_ParentId))
                    {
                        result = GetRFNameByRFId(modelRF.ResourceFolder_ParentId, result);
                    }
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

    }
}