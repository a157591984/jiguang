<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Person_Edit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.Person_Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $("#txtJob").bind({
                blur: function () { if (this.value.length > 30) this.value = this.value.slice(0, 30) },
                keyup: function () { if (this.value.length > 30) this.value = this.value.slice(0, 30) }
            });
            $("#txtCompany").bind({
                blur: function () { if (this.value.length > 50) this.value = this.value.slice(0, 50) },
                keyup: function () { if (this.value.length > 50) this.value = this.value.slice(0, 50) }
            });
            $("#txtPhoneNum").bind({
                blur: function () { this.value = this.value.replace(/\D/g, ''); },
                keyup: function () { this.value = this.value.replace(/\D/g, ''); }
            });
            $("#btnSubmit").click(function () {
                if ($.trim($("#txtName").val()) == "") {
                    layer.ready(function () {
                        layer.msg("姓名不能为空", { icon: 2, time: 1000 }, function () { $("#txtName").focus(); });

                    })
                    return false;
                }
                if (!js_validate.IsMobileNum2($.trim($("#txtPhoneNum").val()))) {
                    layer.ready(function () {
                        layer.msg("请填写正确的手机号", { icon: 2, time: 1000 }, function () { $("#txtPhoneNum").focus(); });

                    })
                    return false;
                }

            });

        });
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>姓名&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control" MaxLength="10"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>手机号&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox runat="server" ID="txtPhoneNum" CssClass="form-control" MaxLength="11"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>职务</label>
                <asp:TextBox runat="server" ID="txtJob" CssClass="form-control" MaxLength="30"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>公司名称</label>
                <asp:TextBox runat="server" ID="txtCompany" CssClass="form-control" MaxLength="50"></asp:TextBox>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
