<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientTabEdit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ClientTabEdit" %>

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
                <label>Tab标识 <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtTabindex" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="100" autocomplete="off"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Tab名称 <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtTabName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Tab类型 <span class="text-danger">*</span></label>
                <asp:DropDownList ID="ddlTabType" runat="server" ClientIDMode="Static" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>备注</label>
                <asp:TextBox ID="txtRemark" TextMode="MultiLine"
                    CssClass="form-control" Rows="5" runat="server" MaxLength="255"></asp:TextBox>
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
            if ($.trim($("#txtTabindex").val()) == "") {
                layer.ready(function () {
                    layer.msg('请填写Tab标识', { icon: 4 }, function () { $("#txtTabindex").focus(); })
                });
                return false;
            }
            if ($.trim($("#txtTabName").val()) == "") {
                layer.ready(function () {
                    layer.msg('请填写Tab名称', { icon: 4 }, function () { $("#txtTabName").focus(); })
                });
                return false;
            }
            if ($.trim($("#ddlTabType").val()) == "") {
                layer.ready(function () {
                    layer.msg('请选择Tab类型', { icon: 4 }, function () { $("#ddlTabType").focus(); })
                });
                return false;
            }
            return true;

        })
    })
</script>
