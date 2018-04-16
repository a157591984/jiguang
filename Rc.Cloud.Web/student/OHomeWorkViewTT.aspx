<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OHomeWorkViewTT.aspx.cs" Inherits="Rc.Cloud.Web.student.OHomeWorkViewTT" %>

<%@ Import Namespace="Rc.Common.StrUtility" %>
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
                key: "stu_testpaperCorrect_view",
                stuId: "<%=StudentId%>",
                rtrfId: "<%=ResourceToResourceFolder_Id%>",
                hwId: "<%=HomeWork_Id%>",
                IsWrong: "<%=isShowWrong%>",
                isCorrect: "<%=isCorrect%>",
                shwId: "<%=Student_HomeWork_Id%>",
                hwCTime:"<%=hwCreateTime%>",
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                $(".data-hook").html(data);
            }, function () {
                $(".data-hook").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
            })
        }
        //语音批注
        function RecordingComment(tqId) {
            var hwCTime = "<%=hwCreateTime%>";
            var stuHwId = "<%=Student_HomeWork_Id%>";
            layer.open({
                type: 2,
                title: '语音批注',
                area: ['540px', '280px'],
                content: '<%=strTestpaperViewWebSiteUrl%>student/recordingComment.aspx?hwCTime=' + hwCTime + "&stuHwId=" + stuHwId + "&tqId=" + tqId //iframe的url
            });
        }
    </script>
</head>
<body class="body_bg user_select_none">
    <form id="form1" runat="server">
        <div class="container ">
            <div class="fixed_sidebar">
                <ul>
                    <asp:Literal runat="server" ID="ltlLink"></asp:Literal>
                </ul>
            </div>
            <div class="test_paper_panel">
                <div class="panel_heading">
                    <div class="test_paper_name_panel mb">
                        <div class="panel_heading">
                            <div class="panel_title">
                                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                            </div>
                            <ul class="panel_info">
                                <asp:Literal runat="server" ID="ltlGradeSubject"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="panel_body data-hook"></div>
            </div>
        </div>
    </form>
</body>
</html>
