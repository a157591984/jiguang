<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileViewNew.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileViewNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../Styles/style001/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/js001/jquery.min-1.8.2.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="file_box">
            <div class="file_thumbnail_box">
                <ul>
                    <asp:Repeater runat="server" ID="rptImgSmall">
                        <ItemTemplate>
                            <li>
                                <a href="#file<%#Eval("ResourceToResourceFolder_img_id") %>">
                                    <img src="<%=strTeachingPlanViewWebSiteUrl %><%#Eval("ResourceToResourceFolderImg_Url") %>" onerror="this.src='./../Images/no_pic.jpg'" alt="" val="<%=strTeachingPlanViewWebSiteUrl %><%#Eval("ResourceToResourceFolderImg_Url") %>">
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="file_main_box">
                <ul>
                    <asp:Repeater runat="server" ID="rptImgBig">
                        <ItemTemplate>
                            <li>
                                <a name="file<%#Eval("ResourceToResourceFolder_img_id") %>">
                                    <img src="<%=strTeachingPlanViewWebSiteUrl %><%#Eval("ResourceToResourceFolderImg_Url") %>" onerror="this.src='./../Images/no_pic.jpg'">
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
