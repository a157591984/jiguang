<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestpaperView.aspx.cs" Inherits="Rc.Cloud.Web.teacher.TestpaperView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>预览</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/common.js"></script>
    <script type="text/javascript">
        $(function () {
            //布置作业
            $('[data-name="assignment"]').on('click', function () {
                layer.open({
                    type: 2,
                    title: '布置作业',
                    area: ['415px', '600px'],
                    content: 'AssignExercise.aspx?rtrfId=<%=ResourceToResourceFolder_Id%>&Resource_Name=<%=Resource_Name%>'
                });
            });
            //开始讲评
            $('[data-name="startCommenting"]').on('click', function () {
                layer.open({
                    type: 2,
                    title: '选择讲评数据',
                    area: ['550px', '500px'],
                    content: 'StartCommenting.aspx?rtrfId=<%=ResourceToResourceFolder_Id%>'
                });
            });
        })
    </script>
</head>
<body class="user_select_none">
    <div class="elevator" id="divAssign" runat="server">
        <div class="item" data-name="assignment">布置作业</div>
        <div class="item" data-name="startCommenting">开始讲评</div>
    </div>
    <div class="container paper_preview_container">
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
                key: "testpaper_view_new",
                tchId: "<%=tchId%>",
                rtrfId: "<%=ResourceToResourceFolder_Id%>",
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                $(".paper_preview_container").html(data);
            }, function () {
                $(".paper_preview_container").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
                })
        }

    </script>
</body>
</html>
