<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addmybaby.aspx.cs" Inherits="Rc.Cloud.Web.parent.addmybaby" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加我的宝贝</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnConfirm").click(function () {
                if ($.trim($("#txtUserName").val()) == "") {
                    layer.msg("请填写宝贝帐号", { time: 1000, icon: 2, offset: '10px' });
                    return false;
                }
                if ($.trim($("#txtPassWord").val()) == "") {
                    layer.msg("请填写宝贝密码", { time: 1000, icon: 2, offset: '10px' });
                    return false;
                }
            });
        });
        function Handel(isSuccess, mes) {
            layer.msg(mes, {
                icon: isSuccess,
                time: 2000 //2秒关闭（如果不配置，默认是3秒）
            }, function () {
                //do something
                if (isSuccess == 1) parent.window.location.reload();
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pv pt">
            <div class="form-group">
                <label>帐号</label>
                <div>
                    <asp:TextBox ID="txtUserName" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label>密码</label>
                <div>
                    <asp:TextBox ID="txtPassWord" TextMode="Password" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <asp:Button ID="btnConfirm" runat="server" ClientIDMode="Static" CssClass="btn btn-primary pull-right" Text="确定" OnClick="btnConfirm_Click" />
        </div>
    </form>
</body>
</html>
