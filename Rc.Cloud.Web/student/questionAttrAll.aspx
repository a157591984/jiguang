<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="questionAttrAll.aspx.cs" Inherits="Rc.Cloud.Web.student.questionAttrAll" %>

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
        <div id="testpaper_tq_attr_all" class="pa">
        </div>
        <script type="text/javascript">
            $(function () {
                getTestpaperTQAttrAll();
            });

            var getTestpaperTQAttrAll = function () {
                var dto = {
                    key: "testpaper_tq_attr_all",
                    rtrfId: "<%=strResourceToResourceFolder_Id%>",
                    tqId: "<%=strTestQuestions_Id%>",
                    attrType: "<%=strAttrType%>",
                    stuId:"<%=strStuId%>",
                    x: Math.random()
                };
                $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                    $("#testpaper_tq_attr_all").html(data);

                    $("div img").each(function () {
                        $(this).prop("width", $(this).prop("width") * 2);
                        $(this).prop("height", $(this).prop("height") * 2);
                        var _align = parseInt($(this).css("vertical-align").toLowerCase().replace("px", ""));
                        $(this).css("vertical-align", _align * 2);
                    });

                }, function () {
                    $("#testpaper_tq_attr_all").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
                })
            }

        </script>
    </form>
</body>
</html>
