<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JoinSchool.aspx.cs" Inherits="Rc.Cloud.Web.teacher.JoinSchool" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatibale" content="ie=edge,chrome=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>加入学校</title>
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtUser_ApplicationReason").on('keyup blur', function () {
                this.value = this.value.slice(0, 100);
            })

            $("#btnSubmit").click(function () {
                if ($.trim($("#txtUserGroup_Id").val()) == "") {
                    layer.msg("学校号不能为空", { time: 1000, icon: 4, offset: '10px' });
                    return false;
                }
                if ($.trim($("#txtUser_ApplicationReason").val()) == "") {
                    layer.msg("验证消息不能为空", { time: 1000, icon: 4, offset: '10px' });
                    return false;
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pv pt">
            <div class="form-group">
                <label>学校号</label>
                <asp:TextBox ID="txtUserGroup_Id" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="6"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>验证消息</label>
                <asp:TextBox ID="txtUser_ApplicationReason" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnSubmit" runat="server" ClientIDMode="Static" CssClass="btn btn-primary pull-right" Text="确定" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
