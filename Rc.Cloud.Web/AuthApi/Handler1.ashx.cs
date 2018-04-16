using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.AuthApi
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {
        DataTable dtTQ_StudentAnswerCount = new DataTable();//学生答题状态数量
        List<Model_StatsClassHW_TQ> listHW_TQ = new List<Model_StatsClassHW_TQ>();//小题统计数据
        public void ProcessRequest(HttpContext context)
        {
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "text/plain";
            #region 变量
            string key = string.Empty;
            string rtrfId = string.Empty;
            string hwId = string.Empty;
            string stuId = string.Empty;
            string shwId = string.Empty;
            StringBuilder stbHtml = new StringBuilder();
            string strSql = string.Empty;
            string IsWrong = string.Empty;
            string isCorrect = string.Empty;
            DataTable dt = new DataTable();
            #endregion
            try
            {
                key = context.Request["key"].Filter();
                switch (key)
                {
                    case "stu_testpaperCorrect_client":
                        #region 客户端批改学生作业与错题集
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        stuId = context.Request["stuId"].Filter();
                        hwId = context.Request["hwId"].Filter();
                        shwId = context.Request["shwId"].Filter();
                        IsWrong = context.Request["IsWrong"].Filter();
                        isCorrect = context.Request["isCorrect"].Filter();
                        string strSqlAnswerScore = string.Format(@"select tqs.TestQuestions_Id,tqs.AnalyzeHyperlinkData,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,shwa.Student_HomeWorkAnswer_Id,shwa.Student_Answer,shwa.Student_Score,shwa.Student_Answer_Status,shwa.Comment
,tqs.ContentText,tqs.TargetText
from TestQuestions_Score tqs
left join Student_HomeWorkAnswer shwa on TQS.TestQuestions_Id=SHWA.TestQuestions_Id and shwa.TestQuestions_Score_ID=tqs.TestQuestions_Score_ID  where tqs.TestQuestions_Score!=-1 and shwa.HomeWork_Id='{0}' and shwa.Student_Id='{1}' {2} order by tqs.TestQuestions_OrderNum", hwId, stuId,
                      IsWrong == "False" ? "" : string.Format(@" and SHWA.TestQuestions_Id in (SELECT DISTINCT t1.TestQuestions_Id FROM Student_HomeWorkAnswer t1 WHERE t1.Student_Answer_Status!='right' and Student_Answer_Status!='unknown' and t1.Student_Id='{0}' and t1.HomeWork_Id='{1}')", stuId, hwId));
                        System.Data.DataTable dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlAnswerScore).Tables[0];

                        int correctCount = 0;
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
                        Rc.Model.Resources.Model_ResourceToResourceFolder modelRTRF = new Rc.BLL.Resources.BLL_ResourceToResourceFolder().GetModel(rtrfId);
                        Rc.Model.Resources.Model_HomeWork modelHW = new Rc.BLL.Resources.BLL_HomeWork().GetModel(hwId);
                        string uploadPath = string.Empty; //Server.MapPath("..\\Upload\\Resource\\");//存储文件基础路径
                        string uploadStudentAnswerPath = string.Empty;
                        //生成存储路径
                        string savePath = string.Empty;
                        string saveOwnPath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                            uploadPath = Rc.Cloud.Web.Common.pfunction.getHostPath() + "/Upload/Resource/";//Server.MapPath("..\\Upload\\Resource\\");//存储文件基础路径
                            uploadStudentAnswerPath = Rc.Cloud.Web.Common.pfunction.getHostPath() + "/Upload/Resource/";
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                            uploadPath = Rc.Cloud.Web.Common.pfunction.getHostPath() + "/Upload/Resource/"; //Server.MapPath("..\\Upload\\Resource\\");//存储文件基础路径
                            uploadStudentAnswerPath = Rc.Cloud.Web.Common.pfunction.getHostPath() + "/Upload/Resource/";
                        }
                        string fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                        string fileStudentAnswerUrl = uploadStudentAnswerPath + "{0}\\" + Rc.Cloud.Web.Common.pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + savePath + "{1}.{2}";//学生答案详细路径
                        string fileComment = uploadStudentAnswerPath + "{0}\\" + Rc.Cloud.Web.Common.pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + shwId + "\\" + "{1}.{2}";//批注文件详细路径
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            System.Data.DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dt.Rows[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                            if (drScore.Length > 0)
                            {
                                //选择题、完形填空题、判断题
                                if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "truefalse")
                                {
                                    //题干
                                    string strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"), "", Encoding.UTF8, "Get");

                                    //选择题、完形填空题选项
                                    string strOption = string.Empty;
                                    if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest")
                                    {
                                        for (int ii = 0; ii < drScore.Length; ii++)
                                        {
                                            //从文件读取选择题选项
                                            string strTestQuestionOption = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                                            List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                            if (listTestSelections != null && listTestSelections.Count > 0)
                                            {
                                                foreach (var item in listTestSelections)
                                                {
                                                    if (!string.IsNullOrEmpty(item.selectionHTML)) strOption += string.Format("<div class=\"option_item col-xs-6\">{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(item.selectionHTML));
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
                                        , Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody)
                                        , dt.Rows[i]["TestQuestions_SumScore"].ToString().clearLastZero()
                                        , strOption);
                                    stbHtml.Append("<div class=\"answer clearfix\">");
                                    //标题
                                    stbHtml.Append("<div class='tit row clearfix mtb15'>");
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
                                                string strTestQuestionCurrent = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                                                if (!string.IsNullOrEmpty(strTestQuestionCurrent)) stbHtml.AppendFormat("<div>{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionCurrent));
                                            }
                                            else
                                            {
                                                stbHtml.Append("<div></div>");
                                            }
                                            stbHtml.Append("</div>");
                                            //学生答案
                                            stbHtml.Append("<div class='student_answer col-xs-6'>");
                                            string strStudentAnswerHtml = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileStudentAnswerUrl, "studentAnswer", drScore[ii]["Student_HomeWorkAnswer_Id"], "txt"), "", Encoding.UTF8, "Get");
                                            if (!string.IsNullOrEmpty(strStudentAnswerHtml)) stbHtml.AppendFormat("<div>{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(strStudentAnswerHtml));
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
                                        if (Rc.Cloud.Web.Common.pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()) != "")
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
                                        stbHtml.AppendFormat("<a class='analysis btn btn-default btn-sm' href=\"javascript:PicPreview('questionAttrAll.aspx?resourceid={0}&questionid={1}&stuId={2}&attrType=AnalyzeHtml','解析');\">查看解析</a>"
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
                                    string strTQ_Correct = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileComment, "teacherMarking", dt.Rows[i]["TestQuestions_Id"], "txt"), "", Encoding.UTF8, "Get");

                                    stbHtml.Append("<div class=\"question panel panel-default\" data-name='correct_over'>");
                                    stbHtml.AppendFormat("<div class=\"panel-body\"><img src=\"data:image/png;base64,{0}\" width='100%' /></div>", strTQ_Correct);

                                    stbHtml.Append("<div class=\"commenting panel-footer clearfix\">");
                                    stbHtml.Append("<div class=\"commenting_box\">");

                                    string strAvgScore = string.Empty;//平均得分
                                    string strContentText = string.Empty;//知识内容
                                    string strTargetText = string.Empty;//测量目标                            
                                    for (int ii = 0; ii < drScore.Length; ii++)
                                    {
                                        if (Rc.Cloud.Web.Common.pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()) != "")
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
                                        stbHtml.AppendFormat("<a class='analysis btn btn-default btn-sm' href=\"javascript:PicPreview('questionAttrAll.aspx?resourceid={0}&questionid={1}&stuId={2}&attrType=AnalyzeHtml','解析');\">查看解析</a>"
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
                                string strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"), "", Encoding.UTF8, "Get");
                                strTestQuestionBody = Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody);
                                if (string.IsNullOrEmpty(strTestQuestionBody))
                                {
                                    strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "textTitle", dt.Rows[i]["TestQuestions_Id"], "txt"), "", Encoding.UTF8, "Get");
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
                        #region web端批改学生作业与错题集
                        stbHtml = new StringBuilder();
                        rtrfId = context.Request["rtrfId"].Filter();
                        stuId = context.Request["stuId"].Filter();
                        hwId = context.Request["hwId"].Filter();
                        shwId = context.Request["shwId"].Filter();
                        IsWrong = context.Request["IsWrong"].Filter();
                        isCorrect = context.Request["isCorrect"].Filter();
                        strSqlAnswerScore = string.Format(@"select tqs.TestQuestions_Id,tqs.AnalyzeHyperlinkData,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex,shwa.Student_HomeWorkAnswer_Id,shwa.Student_Answer,shwa.Student_Score,shwa.Student_Answer_Status,shwa.Comment
,tqs.ContentText,tqs.TargetText
from TestQuestions_Score tqs
left join Student_HomeWorkAnswer shwa on TQS.TestQuestions_Id=SHWA.TestQuestions_Id and shwa.TestQuestions_Score_ID=tqs.TestQuestions_Score_ID  where shwa.HomeWork_Id='{0}' and shwa.Student_Id='{1}' {2} order by tqs.TestQuestions_OrderNum", hwId, stuId,
                      IsWrong == "False" ? "" : string.Format(@" and SHWA.TestQuestions_Id in (SELECT DISTINCT t1.TestQuestions_Id FROM Student_HomeWorkAnswer t1 WHERE t1.Student_Answer_Status!='right' and Student_Answer_Status!='unknown' and t1.Student_Id='{0}' and t1.HomeWork_Id='{1}')", stuId, hwId));
                        dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlAnswerScore).Tables[0];

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
                        modelRTRF = new Rc.BLL.Resources.BLL_ResourceToResourceFolder().GetModel(rtrfId);
                        modelHW = new Rc.BLL.Resources.BLL_HomeWork().GetModel(hwId);
                        uploadPath = string.Empty; //Server.MapPath("..\\Upload\\Resource\\");//存储文件基础路径
                        uploadStudentAnswerPath = string.Empty;
                        //生成存储路径
                        savePath = string.Empty;
                        saveOwnPath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                            uploadPath = Rc.Cloud.Web.Common.pfunction.getHostPath() + "/Upload/Resource/"; //Server.MapPath("..\\Upload\\Resource\\");//存储文件基础路径
                            uploadStudentAnswerPath = Rc.Cloud.Web.Common.pfunction.getHostPath() + "/Upload/Resource/";
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                            uploadPath = Rc.Cloud.Web.Common.pfunction.getHostPath() + "/Upload/Resource/"; //Server.MapPath("..\\Upload\\Resource\\");//存储文件基础路径
                            uploadStudentAnswerPath = Rc.Cloud.Web.Common.pfunction.getHostPath() + "/Upload/Resource/";
                        }
                        fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                        fileStudentAnswerUrl = uploadStudentAnswerPath + "{0}\\" + Rc.Cloud.Web.Common.pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + savePath + "{1}.{2}";//学生答案详细路径
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            System.Data.DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dt.Rows[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                            if (drScore.Length > 0)
                            {
                                //题干
                                string strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"), "", Encoding.UTF8, "Get");

                                //选择题、完形填空题选项
                                string strOption = string.Empty;
                                if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest")
                                {
                                    for (int ii = 0; ii < drScore.Length; ii++)
                                    {
                                        //从文件读取选择题选项
                                        string strTestQuestionOption = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                                        List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                        if (listTestSelections != null && listTestSelections.Count > 0)
                                        {
                                            foreach (var item in listTestSelections)
                                            {
                                                if (!string.IsNullOrEmpty(item.selectionHTML)) strOption += string.Format("<div class=\"option_item col-xs-6\">{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(item.selectionHTML));
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
                                //stbHtml.AppendFormat("<div class=\"remark\" data-content=\"{0}\" id='aSpan{1}' style=\"{2}\"><i>批注</i></div>"
                                //    , (drScore.Length == 0 ? "" : drScore[0]["Comment"])
                                //    , dt.Rows[i]["TestQuestions_Num"]
                                //    , (drScore.Length == 0 || string.IsNullOrEmpty(drScore[0]["Comment"].ToString()) ? "display:none;" : ""));
                                stbHtml.AppendFormat("<div class=\"question_tit\">{0}(分值：<i class=\"fen\">{1}</i>)<div class='options row clearfix'>{2}</div></div>"
                                    , Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody)
                                    , dt.Rows[i]["TestQuestions_SumScore"].ToString().clearLastZero()
                                    , strOption);
                                stbHtml.Append("<div class=\"answer clearfix\">");
                                //标题
                                stbHtml.Append("<div class='tit row clearfix mtb15'>");
                                stbHtml.Append("<div class='correct col-xs-5'><span class='btn btn-success'>正确答案</span></div>");
                                stbHtml.Append("<div class='student col-xs-6'><span class='btn btn-info'>学生答案</span></div>");
                                stbHtml.Append("<div class='scorce col-xs-1'><span class='btn btn-primary btn-block'>得分</span></div>");
                                stbHtml.Append("</div>");
                                //答案&得分
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    stbHtml.Append("<div class='answer_main row clearfix'>");
                                    if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "truefalse")
                                    {//选择题、判断题 答案从数据库读取
                                        //正确答案
                                        stbHtml.AppendFormat("<div class='correct_answer col-xs-5'>{0} {1}</div>"
                                            , dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
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
                                            string strTestQuestionCurrent = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                                            if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                                            {
                                                stbHtml.AppendFormat("<div>{0}{1}</div>"
                                                    , (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString())) ? "(" + drScore[ii]["testIndex"].ToString() + ")" : ""
                                                    , Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));
                                            }
                                        }
                                        else
                                        {
                                            stbHtml.Append("<div></div>");
                                        }
                                        stbHtml.Append("</div>");
                                        //学生答案
                                        stbHtml.Append("<div class='student_answer col-xs-6'>");
                                        string strStudentAnswerHtml = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileStudentAnswerUrl, "studentAnswer", drScore[ii]["Student_HomeWorkAnswer_Id"], "txt"), "", Encoding.UTF8, "Get");
                                        if (!string.IsNullOrEmpty(strStudentAnswerHtml)) stbHtml.AppendFormat("<div>{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(strStudentAnswerHtml));
                                        stbHtml.Append("</div>");
                                    }
                                    //分值
                                    stbHtml.Append("<div class='score col-xs-1'>");
                                    if (Rc.Cloud.Web.Common.pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()) != "")
                                    {
                                        stbHtml.AppendFormat("<input type=\"text\" class='form-control text-center score_input' name=\"choice_Score\" maxlength=\"4\" actualscore=\"{0}\" tqnum=\"{1}\" tqonum=\"{2}\" value=\"{3}\" readonly />"
                                            , drScore[ii]["TestQuestions_Score"].ToString().clearLastZero()
                                            , dt.Rows[i]["TestQuestions_Id"]
                                            , drScore[ii]["TestQuestions_OrderNum"]
                                            , drScore[ii]["Student_Score"].ToString().clearLastZero());
                                    }
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
                                    if (Rc.Cloud.Web.Common.pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()) != "")
                                    {
                                        strAvgScore += string.Format("<dd>{0} {1}</dd>"
                                            , (dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "fill") && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                            , GetAvgScore(drScore[ii]["TestQuestions_Score_ID"].ToString()));
                                        strContentText += string.Format("<dd>{0} {1}</dd>"
                                            , (dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "fill") && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                            , drScore[ii]["ContentText"].ToString());
                                        strTargetText += string.Format("<dd>{0} {1}</dd>"
                                            , (dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "fill") && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                            , drScore[ii]["TargetText"].ToString());
                                    }
                                }
                                stbHtml.AppendFormat("<dl class='clearfix'><dt>平均得分：</dt>{0}</dl>", strAvgScore);
                                stbHtml.AppendFormat("<dl class='clearfix'><dt>知识内容：</dt>{0}</dl>", strContentText);
                                stbHtml.AppendFormat("<dl class='clearfix'><dt>测量目标：</dt>{0}</dl>", strTargetText);

                                stbHtml.Append("</div>");
                                if (isCorrect == "True" || modelHW.IsShowAnswer == 1)
                                {
                                    stbHtml.AppendFormat("<a class='analysis btn btn-default btn-sm' href='##' onclick=\"javascript:PicPreview('questionAttrAll.aspx?resourceid={0}&questionid={1}&stuId={2}&attrType=AnalyzeHtml','解析');\">查看解析</a>"
                                        , rtrfId
                                        , dt.Rows[i]["TestQuestions_Id"]
                                        , stuId);
                                }
                                //stbHtml.Append("<li><a href='##'>强化训练</a></li>");
                                stbHtml.Append("</div>");

                                stbHtml.Append("</div>");
                            }
                            else
                            {
                                //题干
                                string strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"), "", Encoding.UTF8, "Get");
                                strTestQuestionBody = Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody);
                                if (string.IsNullOrEmpty(strTestQuestionBody))
                                {
                                    strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "textTitle", dt.Rows[i]["TestQuestions_Id"], "txt"), "", Encoding.UTF8, "Get");
                                }
                                stbHtml.Append("<div class=\"question\" data-name='correct_over'>");
                                stbHtml.AppendFormat("<div class=\"question_tit\">{0}</div>", strTestQuestionBody);
                                stbHtml.Append("</div>");
                            }
                        }
                        context.Response.Write(stbHtml.ToString());
                        #endregion
                        break;
                }
            }
            catch (Exception)
            {
                context.Response.Write("");
            }
        }
        protected string GetAvgScore(string TestQuestions_Score_ID)
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

                temp = string.Format(temp
                    , list[0].ScoreAvg.ToString().clearLastZero()
                    , rightCount
                    , wrongCount
                    , partrightCount
                    , list[0].ScoreAvgRate.ToString().clearLastZero());

            }
            catch (Exception)
            {
                temp = "";
            }
            return temp;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}