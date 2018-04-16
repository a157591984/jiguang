<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileView.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script type="text/javascript">
        $(function () {
            //禁止浏览器右击按钮
            $(document).ready(function () {
                $(document).bind("contextmenu", function (e) {
                    return false;
                });
            });
        })
    </script>
</head>
<body class="file_body">
    <form id="form1" runat="server">
        <div class="container class_view_cont">
            <div class="img_l">
                <asp:Repeater runat="server" ID="rptImgSmall">
                    <ItemTemplate>
                        <a href="#file<%#Eval("ResourceToResourceFolder_img_id") %>">
                            <img src="<%=strTeachingPlanViewWebSiteUrl %><%#Eval("ResourceToResourceFolderImg_Url") %>" onerror="this.src='./../Images/no_pic.jpg'" alt="" val="<%=strTeachingPlanViewWebSiteUrl %><%#Eval("ResourceToResourceFolderImg_Url") %>">
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="img_r">
                <asp:Repeater runat="server" ID="rptImgBig">
                    <ItemTemplate>
                        <a name="file<%#Eval("ResourceToResourceFolder_img_id") %>">
                            <img src="<%=strTeachingPlanViewWebSiteUrl %><%#Eval("ResourceToResourceFolderImg_Url") %>" onerror="this.src='./../Images/no_pic.jpg'">
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
