<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FUser_Edit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.FUser_Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $("#btnSubmit").click(function () {

                if ($.trim($("#txtUserName").val()) == "") {
                    layer.ready(function () {
                        layer.msg("请输入登录名", { icon: 4 }, function () {
                            $("#txtUserName").focus();
                        });
                    })
                    return false;
                }
                return true;
            })
        })
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>登录名&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>用户名</label>
                <asp:TextBox ID="txtTureName" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>职位</label>
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlUserPost"></asp:DropDownList>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
