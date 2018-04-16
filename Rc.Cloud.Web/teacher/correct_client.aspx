<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="correct_client.aspx.cs" Inherits="Rc.Cloud.Web.teacher.correct_client" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/common.js"></script>
</head>
<body class="body_bg">
    <div class="container">
        <div class="test_paper_panel">
            <div class="panel_heading">
                <div class="test_paper_name_panel">
                    <div class="panel_heading">
                        <div class="panel_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                        <ul class="panel_info">
                            <li>姓名：<asp:Literal runat="server" ID="ltlStudentName"></asp:Literal></li>
                            <li>班：<asp:Literal runat="server" ID="ltlClassName"></asp:Literal></li>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="panel_body">
                <div class="question_type_panel" id="choice" runat="server">
                    <div class="panel_heading">选择题</div>
                    <div class="panel_body">
                        <%
                            if (dtStuScore.Rows.Count > 0)
                            {
                                System.Data.DataRow[] drStuScore = dtStuScore.Select("TestType in('selection','clozeTest')");
                                if (drStuScore.Length == 1)
                                {
                                    StringBuilder stbHtml = new StringBuilder();
                                    stbHtml.AppendFormat("<span class=\"total_score\">总分：<i>{0}</i></span>"
                                        , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"score\">得分：<i>{0}</i></span>"
                                        , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"points\">扣分：<i>{0}</i></span>"
                                        , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                    Response.Write(stbHtml.ToString());
                                }
                            }
                        %>
                    </div>
                </div>

                <div class="objective_question_panel clearfix" id="choice2" runat="server">
                    <div class="panel_dt">
                        <ul>
                            <li>
                                <span>题号</span>
                                <span>参考答案</span>
                                <span>学生答案</span>
                                <span>得分</span>
                            </li>
                        </ul>
                    </div>
                    <div class="panel_dd">
                        <ul>
                            <asp:Repeater runat="server" ID="rptStuHomeworkSelection">
                                <ItemTemplate>
                                    <li class="<%#Eval("TestQuestions_Num") %>  <%#Eval("Student_Answer_Status").ToString()=="right"?"":"error" %>">
                                        <span><%#Eval("topicNumber").ToString().TrimEnd('.') %><%#string.IsNullOrEmpty(Eval("testIndex").ToString())?"":("-"+Eval("testIndex").ToString()) %></span>
                                        <span><%#Eval("TestCorrect") %></span>
                                        <span><%#Eval("Student_Answer") %></span>
                                        <span><%#Eval("Student_Score") %><input type="hidden" name="fill_Score" value="<%#Eval("Student_Score") %>"></span>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>

                <div class="question_type_panel" id="truefalse" runat="server">
                    <div class="panel_heading">判断题 </div>
                    <div class="panel_body">
                        <%
                            if (dtStuScore.Rows.Count > 0)
                            {
                                System.Data.DataRow[] drStuScore = dtStuScore.Select("TestType in('truefalse')");
                                if (drStuScore.Length == 1)
                                {
                                    StringBuilder stbHtml = new StringBuilder();
                                    stbHtml.AppendFormat("<span class=\"total_score\">总分：<i>{0}</i></span>"
                                        , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"score\">得分：<i>{0}</i></span>"
                                        , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                    stbHtml.AppendFormat("<span class=\"points\">扣分：<i>{0}</i></span>"
                                        , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                    Response.Write(stbHtml.ToString());
                                }
                            }
                        %>
                    </div>
                </div>

                <div class="objective_question_panel clearfix" id="truefalse2" runat="server">
                    <div class="panel_dt">
                        <ul>
                            <li>
                                <span>题号</span>
                                <span>参考答案</span>
                                <span>学生答案</span>
                                <span>得分</span>
                            </li>
                        </ul>
                    </div>
                    <div class="panel_dd">
                        <ul class="clearfix">
                            <asp:Repeater runat="server" ID="rptStuHomeworkTruefalse">
                                <ItemTemplate>
                                    <li class="<%#Eval("Student_Answer_Status").ToString()=="right"?"":"error" %>">
                                        <span><%#Eval("TestQuestions_NumStr") %></span>
                                        <span><%#Eval("TestCorrect") %></span>
                                        <span><%#Eval("Student_Answer") %></span>
                                        <span><%#Eval("Student_Score") %><input type="hidden" name="fill_Score" value="<%#Eval("Student_Score") %>"></span>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>

                <div id="testpaper_correct_client"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            getTestpaperCorrect();
        });
        var getTestpaperCorrect = function () {
            var dto = {
                key: "testpaper_correct_client",
                rtrfId: "<%=ResourceToResourceFolder_Id%>",
                hwId: "<%=HomeWork_Id%>",
                stuHwId: "<%=stuHomeWorkId%>",
                stuId: "<%=Student_Id%>",
                hwCreateTime: "<%=hwCreateTime%>",
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                $("#testpaper_correct_client").html(data);
            }, function () {
                $("#testpaper_correct_client").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
                })
        }
    </script>
</body>
</html>
