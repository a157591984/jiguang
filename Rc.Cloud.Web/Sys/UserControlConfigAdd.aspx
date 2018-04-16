<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserControlConfigAdd.aspx.cs" Inherits="Rc.Cloud.Web.Sys.UserControlConfigAdd" %>

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
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>配置标识 <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtConfigName" CssClass="form-control" runat="server" IsRequired="true"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>配置名称 <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtName" CssClass="form-control" runat="server" IsRequired="true"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label>配置说明</label>
                <asp:TextBox ID="txtDesc" CssClass="form-control" runat="server" Rows="5" TextMode="MultiLine" IsFilterSpecialChars="false" IsFilterSqlChars="false"
                    MaxLength="10000"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>SQL语句</label>
                <asp:TextBox ID="txtSqlString" CssClass="form-control" runat="server" Rows="5" TextMode="MultiLine" IsFilterSpecialChars="false" IsFilterSqlChars="false"
                    MaxLength="10000"></asp:TextBox>
            </div>
            <asp:Button runat="server" CssClass="btn btn-primary" Text="提交" ID="btn_Search" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#btn_Search").click(function () {
            if ($.trim($("#txtConfigName").val()) == "") {
                layer.ready(function () { layer.msg("请填写配置标识", { icon: 4, time: 1000 }); });
                return false;
            }
            if ($.trim($("#txtName").val()) == "") {
                layer.ready(function () { layer.msg("请填写配置名称", { icon: 4, time: 1000 }); });
                return false;
            }
        });
    });
</script>
