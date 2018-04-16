<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResEdit.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.ResEdit" %>

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
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $("#txtName").bind({
                blur: function () { if (this.value.length > 100) this.value = this.value.slice(0, 100) },
                keyup: function () { if (this.value.length > 100) this.value = this.value.slice(0, 100) }
            });
            $("#btnSubmit").click(function () {
                layer.ready(function () {
                    if ($.trim($("#txtName").val()) == "") {
                        layer.msg("请填写名称", { icon: 4 }, function () { $("#txtName").focus(); });
                        return false;
                    }
                });

            });

        });

    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>名称 <span class="text-danger">*</span></label>
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
