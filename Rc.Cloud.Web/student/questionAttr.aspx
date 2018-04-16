<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="questionAttr.aspx.cs" Inherits="Rc.Cloud.Web.student.questionAttr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
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
        <div id="testpaper_tq_attr" class="pa">
        </div>
        <script type="text/javascript">
            var getTestpaperTQAttr = function () {
                var dto = {
                    key: "testpaper_tq_attr",
                    rtrfId: "<%=strResourceToResourceFolder_Id%>",
                    tqId: "<%=strTestQuestions_Id%>",
                    attrType: "<%=strAttrType%>",
                    x: Math.random()
                };
                $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                    $("#testpaper_tq_attr").html(data);
                }, function () {
                    $("#testpaper_tq_attr").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
                })
            }

            $(function () {
                getTestpaperTQAttr();
            });
        </script>
    </form>
</body>
</html>
