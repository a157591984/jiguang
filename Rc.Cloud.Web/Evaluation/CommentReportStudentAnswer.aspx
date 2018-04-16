<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentReportStudentAnswer.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.CommentReportStudentAnswer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>学生答题情况</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="testpaper_stu_answer" class="pa">
        </div>
        <script type="text/javascript">
            var getTestpaperStuAnswer = function () {
                var dto = {
                    key: "testpaper_stu_answer",
                    rtrfId: "<%=strResourceToResourceFolder_Id%>",
                    hwId: "<%=strHomework_Id%>",
                    tqId: "<%=strTestQuestions_Id%>",
                    stuId: "<%=strStudent_Id%>",
                    attrType: "<%=strAttrType%>",
                    x: Math.random()
                };
                $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                    $("#testpaper_stu_answer").html(data);
                }, function () {
                    $("#testpaper_stu_answer").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
                })
            }

            $(function () {
                getTestpaperStuAnswer();
            });
        </script>
    </form>
</body>
</html>
