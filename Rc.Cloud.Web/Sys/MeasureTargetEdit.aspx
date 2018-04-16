<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeasureTargetEdit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.MeasureTargetEdit" %>

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
                <label>层级 <span class="text-danger">*</span></label>
                <asp:DropDownList ID="ddlMTLevel" runat="server" ClientIDMode="Static" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>名称 <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtMTName" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="100" autocomplete="off"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>编码 <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtMTCode" runat="server" CssClass="form-control" autocomplete="off">10000000000000</asp:TextBox>
            </div>
            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        index = parent.layer.getFrameIndex(window.name);
        $("#btnSave").click(function () {
            if ($.trim($("#ddlMTLevel").val()) == "") {
                layer.ready(function () {
                    layer.msg('请选择层级', { icon: 4 }, function () { $("#ddlMTLevel").focus(); })
                });
                return false;
            }
            if ($.trim($("#txtMTName").val()) == "") {
                layer.ready(function () {
                    layer.msg('请填写名称', { icon: 4 }, function () { $("#txtMTName").focus(); })
                });
                return false;
            }
            if ($.trim($("#txtMTCode").val()) == "") {
                layer.ready(function () {
                    layer.msg('请填写编码', { icon: 4 }, function () { $("#txtMTCode").focus(); })
                });
                return false;
            }

            return true;

        })
    })
</script>