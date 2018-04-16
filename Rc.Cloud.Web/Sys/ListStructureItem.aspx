<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListStructureItem.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ListStructureItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                <label>表名称 <span class="text-danger">*</span></label>
                <asp:TextBox ID="tablename" Enabled="false" runat="server" IsRequired="true" MaxLength="100" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>表说明 <span class="text-danger">*</span></label>
                <asp:TextBox ID="tableinfo" runat="server" IsRequired="true" MaxLength="100" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="保存" OnClick="btnSave_Click" OnClientClick="return btnClientSave_Click()" />
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function btnClientSave_Click() {
            var tableinfo = $.trim($("#tableinfo").val());
            if (tableinfo == "") {
                layer.ready(function () { layer.msg('请填写表说明', { icon: 4 }); });
                return false;
            }
        
    }
</script>
