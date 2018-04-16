<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ohomeworkview_client.aspx.cs" Inherits="Rc.Cloud.Web.student.ohomeworkview_client" %>

<%@ Import Namespace="Rc.Common.StrUtility" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.remark').on({
                mouseover: function () {
                    var content = $(this).attr('data-content');
                    if (content !== '') {
                        layer.tips(content, this, {
                            tips: [1, '#000'],
                            time: 0
                        });
                    }
                },
                mouseout: function () {
                    layer.closeAll('tips');
                }
            });
            getTestpaperView();
        });
        var getTestpaperView = function () {
            var dto = {
                key: "stu_testpaperCorrect_client",
                stuId: "<%=StudentId%>",
                rtrfId: "<%=ResourceToResourceFolder_Id%>",
                hwId: "<%=HomeWork_Id%>",
                shwId: "<%=stuHomeWorkId%>",
                IsWrong: "<%=isShowWrong%>",
                isCorrect: "<%=isCorrect%>",
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                $(".hw_priview_cont").html(data);
            }, function () {
                $(".hw_priview_cont").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
            })
        }
    </script>
</head>
<body class="body_bg">
    <form id="form1" runat="server">
        <ul class="res_fixed_sidebar">
            <asp:Literal runat="server" ID="ltlLink"></asp:Literal>
        </ul>
        <div class="container relative">
            <div class="res_info">
                <h2 class="res_title">
                    <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                </h2>
                <div class="res_desc text-center text-muted">
                    <asp:Literal runat="server" ID="ltlGradeSubject"></asp:Literal>
                </div>
            </div>
            <div class="hw_priview_cont">
                
            </div>
        </div>
    </form>
</body>
</html>
