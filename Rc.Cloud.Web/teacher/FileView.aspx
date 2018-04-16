<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileView.aspx.cs" Inherits="Rc.Cloud.Web.teacher.FileView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>教案预览</title>
    <link href="../plugin/lightGallery/css/lightgallery.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <%--<script src="../plugin/lightGallery/js/lightgallery.min.js"></script>
    <script src="../plugin/lightGallery/js/lg-thumbnail.min.js"></script>
    <script src="../plugin/lightGallery/js/lg-fullscreen.min.js"></script>
    <script src="../plugin/lightGallery/js/jquery.mousewheel.min.js"></script>--%>
    <script type="text/javascript">
        $(function () {
            //禁止浏览器右击按钮
            $(document).ready(function () {
                $(document).bind("contextmenu", function (e) {
                    return false;
                });
            });

            //图库
            //$('.lightgallery-hook').lightGallery({
            //    thumbnail: true,
            //    download: false
            //});
        })
    </script>
</head>
<body class="body_bg user_select_none">
    <form id="form1" runat="server">
        <div class="container teaching_plan_view_2">
            <div class="left">
                <asp:Repeater runat="server" ID="rptImgSmall">
                    <ItemTemplate>
                        <a href="#<%# Container.ItemIndex + 1 %>">
                            <img src="<%=strTeachingPlanViewWebSiteUrl %><%#Eval("ResourceToResourceFolderImg_Url") %>" onerror="this.src='./../Images/no_pic.jpg'">
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="right">
                <asp:Repeater runat="server" ID="rptImgBig">
                    <ItemTemplate>
                        <a name="<%# Container.ItemIndex + 1 %>">
                            <img src="<%=strTeachingPlanViewWebSiteUrl %><%#Eval("ResourceToResourceFolderImg_Url") %>" onerror="this.src='./../Images/no_pic.jpg'">
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <%--<div class="container teaching_plan_view hidden">
            <div class="row lightgallery-hook">
                <asp:Repeater runat="server" ID="rptImgBig">
                    <ItemTemplate>
                        <a href="<%=strTeachingPlanViewWebSiteUrl %><%#Eval("ResourceToResourceFolderImg_Url") %>" class="col-xs-3">
                            <div class="thumb">
                                <span><%# Container.ItemIndex + 1 %></span>
                                <img src="<%=strTeachingPlanViewWebSiteUrl %><%#Eval("ResourceToResourceFolderImg_Url") %>" onerror="this.src='./../Images/no_pic.jpg'">
                            </div>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>--%>
    </form>
</body>
</html>
