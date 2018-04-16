<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentRemedyScheme.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.StudentRemedyScheme" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>补救方案</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/jqprint/jquery-migrate-1.2.1.min.js"></script>
    <script src="../plugin/jqprint/jquery.jqprint-0.3.js"></script>
    <script>
        $(function () {
            $('.jqprint-hook').on('click', function (e) {
                var dom = $(this).data('dom');
                $("#" + dom).jqprint();
            })
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ul class="res_fixed_sidebar">
            <%=link %>
        </ul>
        <div class="container pb relative">
            <div class="res_info">
                <h2 class="res_title">
                    <asp:Literal ID="ltlHwName" runat="server"></asp:Literal></h2>
                <div class="res_desc text-center text-muted">
                    <span>年级：<asp:Literal ID="ltlGradeName" runat="server"></asp:Literal></span>
                    <span>班级：<asp:Literal ID="ltlClassName" runat="server"></asp:Literal></span>
                    <span>学生：<asp:Literal ID="ltlSundentName" runat="server"></asp:Literal></span>
                    <span>满分：<asp:Literal ID="ltlHwSorce" runat="server"></asp:Literal>分</span>
                    <span>得分：<asp:Literal ID="ltlStuScorce" runat="server"></asp:Literal>分</span>
                </div>
            </div>
            <input type="button" value="打印" class="btn btn-default btn-sm jqprint-hook" data-dom="content" />
            <div class="content" id="content">
                <h4>1）建议学习和提升的知识点</h4>
                <p>
                    请该同学对：
                    <asp:Literal ID="ltlKPName" runat="server"></asp:Literal><asp:Literal ID="ltlCountKPName" runat="server"></asp:Literal>个重要的知识点进行补救学习或者请老师进行指导。并对该知识点所涉及到的例题进行复习。
                </p>
                <h4>2）错题重练</h4>
                <p><asp:Literal ID="ltlTQNum" runat="server"></asp:Literal></p>
                <%--<h4>3）对应试题推送 </h4>
                <p>针对本次作业，老师已推送与本次错题对应知识点和难度有关的<asp:Literal ID="ltlCountTQ" runat="server"></asp:Literal>
                    道题目，请同学回去认真进行练习，并按时提交。</p>--%>
            </div>
        </div>
    </form>
</body>
</html>
