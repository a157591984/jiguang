<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NativePayPage.aspx.cs" Inherits="WxPayAPI.NativePayPage" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html;image/gif;charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>微信支付-扫码支付</title>
</head>
<body style="padding: 20px;">
    <asp:Image ID="Image2" runat="server" Style="width: 200px; height: 200px;" />
    <asp:Literal runat="server" ID="ltlTips"></asp:Literal>
</body>
</html>
