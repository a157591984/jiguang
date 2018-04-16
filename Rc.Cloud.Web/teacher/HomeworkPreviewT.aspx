<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeworkPreviewT.aspx.cs" Inherits="Rc.Cloud.Web.teacher.HomeworkPreviewT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>预览</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/common.js"></script>
</head>
<body class="body_bg user_select_none">
    <div class="elevator" id="divBack" runat="server" visible="false">
        <div class="item"><a runat="server" id="aBack">返回</a></div>
    </div>
    <asp:Literal ID="ltlBtn" runat="server" ClientIDMode="Static"></asp:Literal>
    <div class="container">
        <div class="test_paper_panel">
            <div class="panel_body data-hook"></div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            //禁止浏览器右击按钮
            $(document).bind("contextmenu", function (e) {
                return false;
            });
            getTestpaperView();
        });

        var getTestpaperView = function () {
            var dto = {
                key: "testpaper_view",
                tchId: "<%=userId%>",
                rtrfId: "<%=ResourceToResourceFolder_Id%>",
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                $(".data-hook").html(data);
                toPosition();
            }, function () {
                $(".data-hook").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
                })
        }
        function toPosition() {
            var url = window.location.href;
            if (url.indexOf('#') != -1) {
                var posId = '';
                posId = url.substring(url.indexOf('#') + 1);
                var scroll_h = $('a[name="' + posId + '"]').offset().top;
                $('html,body').animate({ scrollTop: scroll_h }, 100);
            }
        }
    </script>
</body>
</html>
