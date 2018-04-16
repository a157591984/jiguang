<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FUserListEdit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.FUserListEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/plugin/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../SysLib/plugin/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $('#txtAge').datetimepicker({
                language: 'zh-CN',
                format: 'yyyy-mm-dd',
                minView: 4,
                autoclose: true,
            });
            $('#txtExpirationDate').datetimepicker({
                language: 'zh-CN',
                format: 'yyyy-mm-dd',
                minView: 4,
                autoclose: true,
                pickerPosition: 'top-right'
            });
        })
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>职务&nbsp;<span class="text-danger">*</span></label>
                        <asp:DropDownList runat="server" ID="ddlUserPost" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>用户名&nbsp;<span class="text-danger">*</span></label>
                        <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>密码&nbsp;<span class="text-danger">*</span></label>
                        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
                        <p class="help-block">新增时必填，修改时如果不填则不修改</p>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>确认密码&nbsp;<span class="text-danger">*</span></label>
                        <asp:TextBox runat="server" ID="txtVerPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4">
                    <div class="form-group">
                        <label>真实姓名</label>
                        <asp:TextBox runat="server" ID="txtTrueName" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="form-group">
                        <label>出生日期</label>
                        <asp:TextBox runat="server" ID="txtAge" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="form-group">
                        <label>性别</label>
                        <asp:DropDownList ID="ddlSex" CssClass="form-control" runat="server">
                            <asp:ListItem Value="0">-请选择-</asp:ListItem>
                            <asp:ListItem Value="M">男</asp:ListItem>
                            <asp:ListItem Value="F">女</asp:ListItem>
                            <asp:ListItem Value="S">保密</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>E-mail</label>
                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>手机</label>
                        <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>学科&nbsp;<span class="text-danger">*</span></label>
                        <asp:DropDownList runat="server" ID="ddlSubject" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>到期日期</label>
                        <asp:TextBox runat="server" ID="txtExpirationDate" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
