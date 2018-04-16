<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemLogListView.aspx.cs"
    Inherits="Rc.Cloud.Web.Sys.SystemLogListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>IP地址</label>
                <asp:Label ID="txtIP" runat="server" Text="Label" CssClass="help-block"></asp:Label>
            </div>
            <div class="form-group">
                <label>页面地址</label>
                <asp:Label ID="txtaddress" runat="server" Text="Label" CssClass="help-block"></asp:Label>
            </div>
            <div class="form-group">
                <label>系统地址</label>
                <asp:Label ID="txtmodel" runat="server" Text="Label" CssClass="help-block"></asp:Label>
            </div>
            <div class="form-group">
                <label>内容</label>
                <asp:Label ID="txtcontent" runat="server" Text="Label" CssClass="help-block"></asp:Label>
            </div>
            <div class="form-group">
                <label>操作人</label>
                <asp:Label ID="txtperson" runat="server" Text="Label" CssClass="help-block"></asp:Label>
            </div>
            <div class="form-group">
                <label>原因</label>
                <asp:Label ID="txtReason" runat="server" Text="Label" CssClass="help-block"></asp:Label>
            </div>
            <div class="form-group">
                <label>时间</label>
                <asp:Label ID="txttime" runat="server" Text="Label" CssClass="help-block"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
