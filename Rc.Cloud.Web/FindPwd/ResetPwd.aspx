<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPwd.aspx.cs" Inherits="Rc.Cloud.Web.FindPwd.ResetPwd" %>

<%@ Register Src="~/control/header.ascx" TagPrefix="uc1" TagName="header" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %>-找回密码</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {

            $("#btnSave").click(function () {
                if ($.trim($("#txt_username").val()) == "") {
                    layer.msg("请输入用户名", { time: 1000, icon: 2 }, function () { $("#txt_username").focus(); });
                    return false;
                }
                if ($.trim($("#txt_newpwd").val()) == "") {
                    layer.msg("请输入新密码", { time: 1000, icon: 2 }, function () { $("#txt_newpwd").focus(); });
                    return false;
                }
                if ($.trim($("#txt_confirmpwd").val()) == "") {
                    layer.msg("请输入确认密码", { time: 1000, icon: 2 }, function () { $("#txt_confirmpwd").focus(); });
                    return false;
                }
                if ($.trim($("#txt_newpwd").val()) != $.trim($("#txt_confirmpwd").val())) {
                    layer.msg("二次密码输入不一致,请重新输入。", { time: 1000, icon: 2 }, function () { $("#txt_newpwd").focus(); });
                    return false;
                }
                return true;
            })
            $("#txt_username").on({
                blur: function () {
                    this.value = js_validate.Filter(this.value);
                    var userName = $.trim($(this).val());
                    if (userName != "") {
                        var ckMsg = CheckUserNameIsExist(userName);
                        if (ckMsg != "") {
                            layer.msg(ckMsg, { icon: 2, time: 1000 });
                            return false;
                        }
                    }
                }
            });
        })
        function CheckUserNameIsExist(userName) {
            var temp = "";
            $.ajaxWebService("VerIdentity.aspx/CheckUserNameIsExist", "{userName:'" + userName + "',x:'" + Math.random() + "'}", function (data) {
                if (data.d == "") {
                    temp = "用户名不存在,请重新输入。";
                }
            }, function () { temp = "用户名不存在,请重新操作。"; }, false);
            return temp;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="index_panel pt">
            <uc1:header runat="server" ID="header" />
            <div class="container find_pwd_panel">
                <div class="row text-center guide_nav">
                    <div class="col-xs-4">
                        <div class="step step_1 opacity active">验证身份</div>
                    </div>
                    <div class="col-xs-4">
                        <div class="step step_2 active">设置新密码</div>
                    </div>
                    <div class="col-xs-4">
                        <div class="step step_3">完成</div>
                    </div>
                </div>
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="control-label col-xs-3">用户名</div>
                        <div class="col-xs-5">
                            <asp:TextBox ID="txt_username" MaxLength="50" CssClass="form-control" runat="server" placeholder="用户名" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="control-label col-xs-3">新密码</div>
                        <div class="col-xs-5">
                            <asp:TextBox ID="txt_newpwd" MaxLength="50" CssClass="form-control" TextMode="Password" runat="server" placeholder="新密码" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="control-label col-xs-3">确认新密码</div>
                        <div class="col-xs-5">
                            <asp:TextBox ID="txt_confirmpwd" MaxLength="50" CssClass="form-control" TextMode="Password" runat="server" placeholder="确认新密码" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-xs-offset-3 col-xs-5">
                            <asp:Button ID="btnSave" CssClass="btn btn-primary btn-block" runat="server" Text="提交" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
