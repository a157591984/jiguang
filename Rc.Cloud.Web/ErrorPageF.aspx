<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPageF.aspx.cs" Inherits="Rc.Cloud.Web.ErrorPageF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统提示</title>
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="err_page_panel">
            <div class="panel_heading">
                <img src="images/page_not_found.png" />
            </div>
            <asp:Literal ID="ltErrorInfo" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>