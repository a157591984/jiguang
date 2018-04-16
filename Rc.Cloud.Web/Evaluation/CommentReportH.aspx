<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentReportH.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.CommentReportH" %>

<%@ Import Namespace="Rc.Common.StrUtility" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="../Scripts/js001/jquery.min-1.8.2.js"></script>
    <script type="text/javascript" src="../Scripts/Highcharts/js/highcharts.js"></script>
    <script type="text/javascript" src="../Scripts/js001/Global.js"></script>
    <script type="text/javascript" src="../Scripts/function.js"></script>
    <link href="../Styles/style001/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/plug-in/layer/layer.js"></script>
    <title>讲评报告</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="hw_info">
                <h2>
                    <asp:Literal ID="ltlHomeWork_Name" runat="server"></asp:Literal></h2>
                <div class="hw_desc">
                    <span>年级：<asp:Literal ID="ltlGrade" runat="server"></asp:Literal></span>
                    <span>班级：<asp:Literal ID="ltlClass" runat="server"></asp:Literal></span>
                    <span>学科：<asp:Literal ID="ltlSubjectName" runat="server"></asp:Literal></span>
                    <span>老师：<asp:Literal ID="ltlTeacherName" runat="server"></asp:Literal></span>
                    <span>满分<asp:Literal ID="ltlSumSore" runat="server"></asp:Literal>分</span>
                </div>
                <a href="javascript:closeWP();" class="close_page"><i class="fa fa-close"></i>关闭页面</a>
            </div>
        </div>
        <div class="container mt10">
            <div class="pt20 pl20 pr20 comment_legend_box">
                <h2 class="column_title">题号</h2>
                <%--<ul class="comment_legend">
                    <li>小题背景颜色说明：按得分率区间划分</li>
                    <li><span class="wrong"></span>0-60%</li>
                    <li><span class="danger"></span>60%-70%</li>
                    <li><span class="primary"></span>70%-85%</li>
                    <li><span></span>85%-100%</li>
                </ul>--%>
            </div>
            <div class="question_num_box" data-name="question_num_box">
                <div class="question_num" data-name="question_num">
                    <ul class="clearfix">
                        <asp:Repeater runat="server" ID="rptTQ">
                            <ItemTemplate>
                                <li data-name="question" class="" data-value="<%#Eval("TestQuestions_Num") %>"><%#Eval("topicNumber").ToString().TrimEnd('.') %></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <div class="pl20 pr20 pt20 pb10">
                <h2 class="column_title">班级小题答题详情</h2>
                <%
                    try
                    {
                        string strSqlAnswerScore = string.Format(@"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum
,tqs.ContentText,tqs.TargetText
from TestQuestions_Score tqs where ResourceToResourceFolder_Id='{0}' ", ResourceToResourceFolder_Id);
                        System.Data.DataTable dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlAnswerScore).Tables[0];

                        StringBuilder stbHtml = new StringBuilder();
                        Rc.Model.Resources.Model_ResourceToResourceFolder modelRTRF = new Rc.BLL.Resources.BLL_ResourceToResourceFolder().GetModel(ResourceToResourceFolder_Id);
                        string uploadPath = Rc.Cloud.Web.Common.pfunction.GetResourceHost("TestWebSiteUrl") + "Upload/Resource/"; //Server.MapPath("..\\Upload\\Resource\\");//存储文件基础路径
                        string uploadStudentAnswerPath = Rc.Cloud.Web.Common.pfunction.GetResourceHost("StudentAnswerWebSiteUrl") + "Upload/Resource/";
                        //生成存储路径
                        //string savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                        //string fileUrl = uploadPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径
                        string savePath = string.Empty;
                        string saveOwnPath = string.Empty;
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                        {
                            uploadPath = Rc.Cloud.Web.Common.pfunction.GetResourceHost("TestWebSiteUrl") + "Upload/Resource/";
                            saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
                        }
                        if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                        }
                        string fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
                        string fileStudentAnswerUrl = uploadStudentAnswerPath + "{0}\\" + savePath + "{1}.{2}";//学生答案详细路径
                        System.Data.DataRow[] dtTQRow = dtTQ.Select("", "TestQuestions_Num");
                        foreach (System.Data.DataRow item in dtTQRow)
                        {
                            System.Data.DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + item["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                            if (drScore.Length > 0)
                            {
                                //题干
                                string strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionBody", item["TestQuestions_Id"], "htm"), "", Encoding.UTF8, "Get");
                                //选择题、完形填空题选项
                                string strOption = string.Empty;
                                if (item["TestQuestions_Type"].ToString() == "selection" || item["TestQuestions_Type"].ToString() == "clozeTest")
                                {
                                    for (int ii = 0; ii < drScore.Length; ii++)
                                    {
                                        //从文件读取选择题选项
                                        string strTestQuestionOption = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                                        List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                                        if (listTestSelections != null && listTestSelections.Count > 0)
                                        {
                                            foreach (var itemSelections in listTestSelections)
                                            {
                                                if (!string.IsNullOrEmpty(itemSelections.selectionHTML)) strOption += string.Format("<div class=\"option_item\">{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(itemSelections.selectionHTML));
                                            }
                                            if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                                            {
                                                strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                                            }
                                        }
                                    }
                                }
                                stbHtml.AppendFormat("<div class=\"qustion comment_report\" data-name=\"question_{0}\">", item["TestQuestions_Num"]);
                                stbHtml.AppendFormat("<div class=\"qustion_tit\">{0}(分值：<i class=\"fen\">{1}</i>)<div class=\"options clearfix\">{2}</div></div>"
                                    , Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody)
                                    , item["TestQuestions_SumScore"].ToString().clearLastZero()
                                    , strOption);

                                stbHtml.Append("<div class=\"commenting clearfix\" data-name=\"commenting\">");
                                stbHtml.Append("<div class=\"clearfix\">");
                                stbHtml.Append("<div class=\"commenting_box\" data-name=\"commenting_box\">");

                                string strAvgScore = string.Empty;//平均得分 
                                string strContentText = string.Empty;//知识内容
                                string strTargetText = string.Empty;//测量目标
                                string strCorrectAnswer = string.Empty;//正确答案
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    //strAvgScore += string.Format("<dd>{0}</dd>", GetAvgScore(drScore[ii]["TestQuestions_Score_ID"].ToString()));
                                    strContentText += string.Format("<dd>{0}</dd>", drScore[ii]["ContentText"].ToString());
                                    strTargetText += string.Format("<dd>{0}</dd>", drScore[ii]["TargetText"].ToString());
                                    if (item["TestQuestions_Type"].ToString() == "selection" || item["TestQuestions_Type"].ToString() == "clozeTest" || item["TestQuestions_Type"].ToString() == "truefalse")
                                    {
                                        strCorrectAnswer += string.Format("<dd>{0}</dd>", drScore[ii]["TestCorrect"]);
                                    }
                                    else if (item["TestQuestions_Type"].ToString() == "fill" || item["TestQuestions_Type"].ToString() == "answers")
                                    {
                                        //从文件读取正确答案图片
                                        string strTestQuestionCurrent = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                                        if (!string.IsNullOrEmpty(strTestQuestionCurrent)) strCorrectAnswer += string.Format("<dd><div>{0}</div></dd>", strTestQuestionCurrent);
                                    }
                                }
                                stbHtml.AppendFormat("<dl><dt>知识内容：</dt>{0}</dl>", strContentText);
                                stbHtml.AppendFormat("<dl><dt>测量目标：</dt>{0}</dl>", strTargetText);
                                stbHtml.AppendFormat("<dl><dt>答案：</dt>{0}</dl>", strCorrectAnswer);

                              

                                stbHtml.AppendFormat("<ul class=\"analytical_btn\"><li><a href=\"javascript:PicPreview('../student/questionAttrAll.aspx?resourceid={0}&questionid={1}&attrType=AnalyzeHtml','解析');\">查看解析</a></li></ul>"
                                    , ResourceToResourceFolder_Id
                                    , item["TestQuestions_Id"]);

                                stbHtml.Append("</div>");
                                stbHtml.Append("<div class=\"display\"><a href='##' data-name=\"display\">展开</a></div>");
                                stbHtml.Append("</div>");
                                stbHtml.Append("</div>");
                                stbHtml.Append("</div>");
                            }
                        }
                        Response.Write(stbHtml);
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>layer.msg('数据加载失败，请稍后重试。',{time:200000000,icon:2});</script>");
                    }
                    timer.Stop();
                    Response.Write("执行时长：" + timer.Elapsed);
                %>
            </div>
        </div>

        <script type="text/javascript">
            $(function () {
                //导航高亮
                $('[data-name="nav"] li:eq(2) a').addClass('active');

                // 展开，收起
                $('[data-name="display"]').toggle(function () {
                    $(this).closest('[data-name="commenting"]').find('[data-name="commenting_box"]').css({ height: "auto" });
                    $(this).html('收起');
                }, function () {
                    $(this).closest('[data-name="commenting"]').find('[data-name="commenting_box"]').css({ height: "87px" });
                    $(this).html('展开');
                });

                //设置高度
                var question_num_box_heigth = $('[data-name="question_num_box"]').height()
                $('[data-name="question_num_box"]').height(question_num_box_heigth);

                //浮动
                window.onscroll = function () {
                    var offsetTop = $('[data-name="question_num_box"]').offset().top;
                    var scrollTop = $(window).scrollTop();
                    if (scrollTop > offsetTop) {
                        $('[data-name="question_num"]').addClass("question_num_fixed")
                    } else {
                        $('[data-name="question_num"]').removeClass("question_num_fixed")
                    }
                }

                //定位
                //scrollTop: $('[data-name="' + question_num + '"]').offset().top - question_num_box_heigth
                $('[data-name="question"]').live({
                    click: function () {
                        var question_num = "question_" + $(this).attr("data-value");
                        $("html,body").animate({
                            scrollTop: $('[data-name="' + question_num + '"]').offset().top - question_num_box_heigth
                        }, 150);
                    }
                });
                //显示选择题学生姓名
                $('div[data-content]').mouseover(function () {
                    var content = $(this).attr('data-content');
                    if (content == '') { content = '暂无学生' }
                    layer.tips(content, $(this), { tips: [2, '#000'] });
                });
            })

            //查看解析
            function PicPreview(url, title, width, height) {
                if (title == '' || title == undefined) {
                    title = '预览';
                }
                if (width == '' || width == undefined) {
                    width = '1000px';
                }
                if (height == '' || height == undefined) {
                    height = '90%';
                }
                layer.open({
                    type: 2,
                    title: title,
                    area: [width, height],
                    content: url
                });
            }
            //答题情况
            function AnswerStatus(url, title) {
                layer.open({
                    type: 2,
                    title: title,
                    fix: false,
                    maxmin: true,
                    area: ["745px", "450px"],
                    content: url
                });
            }
        </script>
    </form>
</body>
</html>
