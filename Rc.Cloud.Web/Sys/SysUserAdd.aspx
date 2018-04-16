<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUserAdd.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysUserAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
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
                <label>登录名&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox ID="txtNamelogin" runat="server" CssClass="form-control" IsRequired="true"></asp:TextBox>
                <asp:TextBox ID="TextBox1" Style="width: 0%; height: 0%; display: none;" runat="server"></asp:TextBox>
                <p class="help-block">登录名不可修改</p>
            </div>
            <div class="form-group">
                <label>密码&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox ID="txtpwdlogin" TextMode="Password" runat="server"
                    CssClass="form-control"></asp:TextBox>
                <asp:TextBox ID="TextBox2" TextMode="Password" CssClass="hidden"
                    runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>用户姓名&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" CssClass="form-control" IsRequired="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>角色&nbsp;<span class="text-danger">*</span></label>
                <div class="checkbox checkbox_1">
                    <asp:CheckBoxList ID="cblRole" RepeatColumns="2" runat="server" ClientIDMode="Static" IsRequired="true">
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="form-group">
                <label>联系方式</label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>是否可用</label>
                <div class="checkbox checkbox_1">
                    <asp:CheckBox ID="ckbIsEnable" Checked="true" ClientIDMode="Static" runat="server" Text="可用" />
                </div>
            </div>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="保 存"
                OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
