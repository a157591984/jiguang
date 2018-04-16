<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentReportSummarize.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.CommentReportSummarize" %>

<%@ Import Namespace="Rc.Common.StrUtility" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <title>讲评概述</title>
</head>
<body class="body_bg">
    <form id="form1" runat="server">
        <div class="container">
            <div class="test_paper_name_panel">
                <div class="panel_heading">
                    <div class="panel_title">
                        <asp:Literal ID="ltlHomeWork_Name" runat="server"></asp:Literal></div>
                    <ul class="panel_info">
                        <li>年级：<asp:Literal ID="ltlGrade" runat="server"></asp:Literal></li>
                        <li>班级：<asp:Literal ID="ltlClass" runat="server"></asp:Literal></li>
                        <li>学科：<asp:Literal ID="ltlSubjectName" runat="server"></asp:Literal></li>
                        <li>老师：<asp:Literal ID="ltlTeacherName" runat="server"></asp:Literal></li>
                        <li>满分<asp:Literal ID="ltlSumSore" runat="server"></asp:Literal>分</li>
                    </ul>
                </div>
            </div>
            <asp:Literal ID="ltlHtml" runat="server"></asp:Literal>
        </div>

    </form>
</body>
</html>
