<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="oHomeWorkViewT.aspx.cs" Inherits="Rc.Cloud.Web.parent.oHomeWorkViewT" %>

<%@ Import Namespace="Rc.Common.StrUtility" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.e.css" rel="stylesheet" />
    <link href="../css/res.css" rel="stylesheet" />
    <link href="../plugin/layer/css/layer.css" rel="stylesheet" />
    <link href="../theme/default.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/layer/js/layer.js"></script>
</head>
<body class="body_bg">
    <form id="form1" runat="server">
        <div class="container">
            <div class="res_info">
                <h2 class="res_title">
                    <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                </h2>
                <div class="res_desc text-center text-muted">
                    <asp:Literal runat="server" ID="ltlGradeSubject"></asp:Literal>
                </div>
            </div>
            <div>
                <%
                    string strSqlAnswerScore = string.Format(@"select tqs.TestQuestions_Id,tqs.AnalyzeHyperlinkData,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,shwa.Student_HomeWorkAnswer_Id,shwa.Student_Answer,shwa.Student_Score,shwa.Student_Answer_Status,shwa.Comment
,tqs.ContentText,tqs.TargetText
from TestQuestions_Score tqs
left join Student_HomeWorkAnswer shwa on TQS.TestQuestions_Id=SHWA.TestQuestions_Id and shwa.TestQuestions_Score_ID=tqs.TestQuestions_Score_ID  where shwa.HomeWork_Id='{0}' and shwa.Student_Id='{1}' {2} order by tqs.TestQuestions_OrderNum", HomeWork_Id, StudentId,
                        !isShowWrong ? "" : string.Format(@" and SHWA.TestQuestions_Id in (SELECT DISTINCT t1.TestQuestions_Id FROM Student_HomeWorkAnswer t1  WHERE t1.Student_Answer_Status!='right' and t1.Student_Id='{0}' and t1.HomeWork_Id='{1}')", StudentId, HomeWork_Id));
                    System.Data.DataTable dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlAnswerScore).Tables[0];

                    int correctCount = 0;
                    string strSql = @"select distinct TQ.* from TestQuestions TQ where tq.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num ";
                    //string strSqlFill = strSql + " and TQ.TestQuestions_Type='fill' order  by TQ.TestQuestions_Num ";
                    System.Data.DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                    StringBuilder stbHtml = new StringBuilder();
                    //System.Data.DataRow[] drChoice = dt.Select("TestQuestions_Type='selection'", "TestQuestions_Num");
                    //System.Data.DataRow[] drTruefalse = dt.Select("TestQuestions_Type='truefalse'", "TestQuestions_Num");
                    //System.Data.DataRow[] drFill = dt.Select("TestQuestions_Type='fill'", "TestQuestions_Num");
                    //System.Data.DataRow[] drAnswers = dt.Select("TestQuestions_Type='answers'", "TestQuestions_Num");

                    Rc.Model.Resources.Model_ResourceToResourceFolder modelRTRF = new Rc.BLL.Resources.BLL_ResourceToResourceFolder().GetModel(ResourceToResourceFolder_Id);
                    Rc.Model.Resources.Model_HomeWork modelHW = new Rc.BLL.Resources.BLL_HomeWork().GetModel(HomeWork_Id);
                    string uploadPath = Rc.Cloud.Web.Common.pfunction.GetResourceHost("TestWebSiteUrl") + "Upload/Resource/"; //Server.MapPath("..\\Upload\\Resource\\");//存储文件基础路径
                    string uploadStudentAnswerPath = Rc.Cloud.Web.Common.pfunction.GetResourceHost("StudentAnswerWebSiteUrl") + "Upload/Resource/";
                    //生成存储路径
                    string savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                        , modelRTRF.ParticularYear, modelRTRF.GradeTerm
                        , modelRTRF.Resource_Version, modelRTRF.Subject);

                    string fileUrl = uploadPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径
                    string fileStudentAnswerUrl = uploadStudentAnswerPath + "{0}\\" + Rc.Cloud.Web.Common.pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + savePath + "{1}.{2}";//学生答案详细路径
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        System.Data.DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dt.Rows[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                        if (drScore.Length > 0)
                        {
                            //题干
                            string strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"), "", Encoding.UTF8, "Get");

                            //选择题选项
                            string strOption = string.Empty;
                            if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection")
                            {
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    //从文件读取选择题选项
                                    string strTestQuestionOption = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                                    List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                    if (listTestSelections != null && listTestSelections.Count > 0)
                                    {
                                        foreach (var item in listTestSelections)
                                        {
                                            strOption += string.Format("<div class=\"option_item col-xs-6\">{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(item.selectionHTML));
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
                            stbHtml.AppendFormat("<div class=\"question_tit\">{0}(分值：<i class=\"fen\">{1}</i>)<div class='options row clearfix'>{2}</div></div>", Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody)
                                , dt.Rows[i]["TestQuestions_SumScore"].ToString().clearLastZero()
                                , strOption);
                            stbHtml.Append("<div class=\"answer clearfix\">");
                            //标题
                            stbHtml.Append("<div class='tit row mtb15 clearfix'>");
                            stbHtml.Append("<div class='correct col-xs-5'><span class='btn btn-success'>正确答案</span></div>");
                            stbHtml.Append("<div class='student col-xs-6'><span class='btn btn-info'>学生答案</span></div>");
                            stbHtml.Append("<div class='scorce col-xs-1'><span class='btn btn-primary btn-block'>得分</span></div>");
                            stbHtml.Append("</div>");
                            //答案&得分
                            for (int ii = 0; ii < drScore.Length; ii++)
                            {
                                stbHtml.Append("<div class='answer_main row clearfix'>");
                                if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "truefalse")
                                {//选择题、判断题 答案从数据库读取
                                    //正确答案
                                    stbHtml.AppendFormat("<div class='correct_answer col-xs-5'>{0}</div>", drScore[ii]["TestCorrect"]);
                                    //学生答案
                                    stbHtml.AppendFormat("<div class='student_answer col-xs-6'>{0}</div>", drScore[ii]["Student_Answer"]);
                                }
                                else if (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" || dt.Rows[i]["TestQuestions_Type"].ToString() == "answers")
                                {//填空题、简答题 答案从文件读取
                                    //正确答案
                                    stbHtml.Append("<div class='correct_answer col-xs-5'>");
                                    //从文件读取正确答案图片
                                    string strTestQuestionCurrent = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                                    if (!string.IsNullOrEmpty(strTestQuestionCurrent)) stbHtml.AppendFormat("<div>{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionCurrent));
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
                                stbHtml.AppendFormat("<input type=\"text\" name=\"choice_Score\" class='form-control' maxlength=\"4\" actualscore=\"{0}\" tqnum=\"{1}\" tqonum=\"{2}\" value=\"{3}\" readonly />"
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
                            stbHtml.Append("<dl>");
                            stbHtml.Append("<dt>批注：</dt>");
                            stbHtml.AppendFormat("<dd>{0}</dd>", (drScore.Length == 0 ? "" : drScore[0]["Comment"]));
                            stbHtml.Append("</dl>");

                            string strAvgScore = string.Empty;//平均得分
                            string strContentText = string.Empty;//知识内容
                            string strTargetText = string.Empty;//测量目标                            
                            for (int ii = 0; ii < drScore.Length; ii++)
                            {
                                strAvgScore += string.Format("<dd>{0}</dd>", GetAvgScore(drScore[ii]["TestQuestions_Score_ID"].ToString()));
                                strContentText += string.Format("<dd>{0}</dd>", drScore[ii]["ContentText"].ToString());
                                strTargetText += string.Format("<dd>{0}</dd>", drScore[ii]["TargetText"].ToString());
                            }
                            stbHtml.AppendFormat("<dl><dt>平均得分：</dt>{0}</dl>", strAvgScore);
                            stbHtml.AppendFormat("<dl><dt>知识内容：</dt>{0}</dl>", strContentText);
                            stbHtml.AppendFormat("<dl><dt>测量目标：</dt>{0}</dl>", strTargetText);

                            stbHtml.Append("</div>");
                            stbHtml.AppendFormat("<a class='analysis btn btn-default btn-sm' href=\"javascript:PicPreview('/student/questionAttrAll.aspx?resourceid={0}&questionid={1}&attrType=AnalyzeHtml','解析');\">查看解析</a>", ResourceToResourceFolder_Id, dt.Rows[i]["TestQuestions_Id"]);
                            //stbHtml.Append("<li><a href='##'>强化训练</a></li>");
                            stbHtml.Append("</div>");
                            stbHtml.Append("</div>");
                        }
                        else
                        {
                            ////题干
                            //string strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"), "", Encoding.UTF8, "Get");
                            ////大标题
                            //stbHtml.Append("<div class=\"question\" data-name='correct_over'>");
                            //stbHtml.AppendFormat("<div class=\"question_tit\">{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody));
                            //stbHtml.Append("</div>");
                        }
                    }
                    Response.Write(stbHtml);
                %>
            </div>
        </div>
    </form>
</body>
</html>
