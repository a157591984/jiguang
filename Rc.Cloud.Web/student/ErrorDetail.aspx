<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorDetail.aspx.cs" Inherits="Rc.Cloud.Web.student.ErrorDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            //禁止浏览器右击按钮
            $(document).ready(function () {
                $(document).bind("contextmenu", function (e) {
                    return false;
                });
            });
            getTestpaperView();
        })

        var getTestpaperView = function () {
            var dto = {
                key: "stu_testpaperCorrect_view_st",
                stuId: "<%=Student_Id%>",
                S_KnowledgePoint_Id: "<%=S_KnowledgePoint_Id%>",
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                $("#TQContent").html(data);
            }, function () {
                $("#TQContent").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
                })
        }
    </script>
</head>
<body class="body_bg user_select_none">
    <form id="form1" runat="server">
        <div class="container">
            <div class="test_paper_panel">
                <div class="panel_heading">
                    <div class="test_paper_name_panel">
                        <div class="panel_heading">
                            <div class="panel_title">
                                <asp:Literal ID="ltlReName" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel_body" id="TQContent">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
