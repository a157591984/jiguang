<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormImp.aspx.cs" Inherits="Rc.Cloud.Web.test.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Excel导入</title>
</head>
<body>
    <form id="form1" runat="server">
        <input id="FileExcel" style="WIDTH: 300px" type="file" size="42" name="FilePhoto" runat="server" />
        <asp:Button runat="server" ID="BtnImport" OnClick="BtnImport_Click1" Text="导入" />
        <asp:Label ID="LblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
