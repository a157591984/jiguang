<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysDepartmentAdd.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysDepartmentAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </asp:ScriptManager>
        <div class="pa">
            <div class="form-group">
                <label>部门名称&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox ID="TbStepName" runat="server" CssClass="form-control" IsRequired="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>负责人&nbsp;<span class="text-danger">*</span></label>
                <asp:DropDownList ID="DropDownListUser" runat="server" CssClass="form-control" IsAddEmptyItem="true" EmptyItemType="Choice" IsRequired="true">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>联系电话&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox ID="TbTel" runat="server" CssClass="form-control" IsRequired="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>部门状态&nbsp;<span class="text-danger">*</span></label>
                <asp:DropDownList CssClass="form-control" ID="DropDownListState" runat="server" IsRequired="true" IsAddEmptyItem="true" EmptyItemType="Choice">
                    <asp:ListItem Text="有效" Value="1"></asp:ListItem>
                    <asp:ListItem Text="无效" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>备注</label>
                <asp:TextBox ID="TbRemark" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="保存" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
