<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialPointEdit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SpecialPointEdit" %>

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
<body class="modal_body_bg">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>层级 <span class="text-danger">*</span></label>
                <asp:DropDownList ID="ddlKPLevel" runat="server" ClientIDMode="Static" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>名称 <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtKPName" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="100" autocomplete="off"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>编码 <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtKPCode" runat="server" CssClass="form-control" autocomplete="off">10000000000000</asp:TextBox>
            </div>
            <div class="form-group">
                <label>重要程度 </label>
                <div>
                    <label>
                        <input type="radio" name="Importance" runat="server" checked value="primary">重要</label>
                    <label>
                        <input type="radio" name="Importance" runat="server" value="secondary">次要</label>
                </div>
                <asp:HiddenField runat="server" ID="hidImportance" />
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
            if ($.trim($("#ddlKPLevel").val()) == "") {
                layer.ready(function () {
                    layer.msg('请选择层级', { icon: 4 }, function () { $("#ddlKPLevel").focus(); })
                });
                return false;
            }
            if ($.trim($("#txtKPName").val()) == "") {
                layer.ready(function () {
                    layer.msg('请填写名称', { icon: 4 }, function () { $("#txtKPName").focus(); })
                });
                return false;
            }
            if ($.trim($("#txtKPCode").val()) == "") {
                layer.ready(function () {
                    layer.msg('请填写编码', { icon: 4 }, function () { $("#txtKPCode").focus(); })
                });
                return false;
            }
            $("#hidImportance").val($("input[name='Importance']:checked").val());
            return true;

        })
    })
</script>
