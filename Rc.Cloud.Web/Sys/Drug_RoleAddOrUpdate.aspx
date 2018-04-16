<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Drug_RoleAddOrUpdate.aspx.cs" Inherits="Rc.Cloud.Web.Sys.Drug_RoleAddOrUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/index.js"></script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>名称&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" IsRequired="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>操作人名称</label>
                <asp:TextBox ID="txtUserName" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="保存" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
