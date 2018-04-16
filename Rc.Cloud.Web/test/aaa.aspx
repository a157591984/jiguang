<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aaa.aspx.cs" Inherits="Rc.Cloud.Web.test.aaa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="txtUserName" placeholder="用户名">90823925</asp:TextBox><br />
            <asp:TextBox runat="server" ID="txtPassword" placeholder="密码">Aa987654321</asp:TextBox><br />
            <br />
            <asp:Button runat="server" ID="btnLogin" Text="登录" OnClick="btnLogin_Click" />

        </div>
    </form>
</body>
</html>
