<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="ChangePassWord.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ChangPassWord" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <div class="panel">
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-xs-2 control-label">原始密码 <span class="text-danger">*</span></div>
                        <div class="col-xs-3">
                            <input type="text" id="txt_oldpwd" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-xs-2 control-label">新密码 <span class="text-danger">*</span></div>
                        <div class="col-xs-3">
                            <input type="password" runat="server" id="txt_newpwd" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-xs-2 control-label">确认密码 <span class="text-danger">*</span></div>
                        <div class="col-xs-3">
                            <input type="password" id="txt_confirmpwd" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-xs-10 col-xs-offset-2">
                            <asp:Button ID="btnChangePassword" runat="server" CssClass="btn btn-primary" Text="确定" OnClick="btnChangePassword_Click" OnClientClick="return SubmitModifyPassword();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function SubmitModifyPassword() {
            var oldpwd = $("#<%=txt_oldpwd.ClientID %>").val();
            var newpwd = $("#<%=txt_newpwd.ClientID %>").val();
            var confirmpwd = $("#txt_confirmpwd").val();
            if (oldpwd == "") {
                layer.ready(function () {
                    layer.msg('原始密码不能为空', { icon: 4 }, function () {
                        $("#<%=txt_oldpwd.ClientID %>").focus();
                    });
                });
                return false;
            }
            if (newpwd == "") {
                layer.ready(function () {
                    layer.msg('新密码不能为空', { icon: 4 }, function () {
                        $("#<%=txt_newpwd.ClientID %>").focus();
                    });
                });
                return false;
            }
            if (newpwd.toString().length < 6) {
                layer.ready(function () {
                    layer.msg('新密码的长度必须大于6位', { icon: 4 }, function () {
                        $("#<%=txt_newpwd.ClientID %>").focus();
                    });
                });
                return false;
            }
            var numberCount = 0;
            var charCount = 0;
            for (var i = 0; i < newpwd.length; i++) {
                if (isNaN(newpwd[i])) {
                    charCount++;
                }
                else {
                    numberCount++;
                }
            }
            if (newpwd != confirmpwd) {
                layer.ready(function () {
                    layer.msg('新密码与确认密码不相同', { icon: 4 }, function () {
                        $("#txt_newpwd").focus();
                    });
                });
                return false;
            }
            if (newpwd == oldpwd) {
                layer.ready(function () {
                    layer.msg('新密码与原始密码相同，无需修改', { icon: 4 }, function () {
                        $("#txt_newpwd").focus();
                    });
                });
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
