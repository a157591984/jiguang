<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addClass.aspx.cs" Inherits="Rc.Cloud.Web.student.addClass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>加入新班级</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery.min-1.11.1.js"></script>
    <script type="text/javascript" src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../plugin/layer/layer.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtUser_ApplicationReason").on('keyup blur', function () {
                this.value = this.value.slice(0, 100);
            })

            $("#btnSubmit").click(function () {
                if ($.trim($("#txtUserGroup_Id").val()) == "") {
                    layer.msg("班号不能为空", { time: 1000, icon: 2, offset: '10px' });
                    return false;
                }
                if ($.trim($("#txtUser_ApplicationReason").val()) == "") {
                    layer.msg("验证消息不能为空", { time: 1000, icon: 2, offset: '10px' });
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
                <label class="require">班号</label>
                <asp:TextBox ID="txtUserGroup_Id" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="6"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="require">验证消息</label>
                <asp:TextBox ID="txtUser_ApplicationReason" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="form-control" Rows="4"></asp:TextBox>
            </div>
            <div class="form-group text-right">
                <asp:Button ID="btnSubmit" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="确定" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </form>
</body>
</html>
