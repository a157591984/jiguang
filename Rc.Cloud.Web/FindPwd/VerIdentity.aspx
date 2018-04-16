<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerIdentity.aspx.cs" Inherits="Rc.Cloud.Web.FindPwd.VerIdentity" %>

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
                if (!js_validate.IsEmail($.trim($("#txt_emial").val()))) {
                    layer.msg("邮箱格式不正确", { icon: 2, time: 1000 }, function () { $("#txt_emial").focus(); })
                    return false;
                }
                if ($.trim($("#TxtVerify").val()) == "") {
                    layer.msg("请输入验证码", { time: 1000, icon: 2 });
                    $("#TxtVerify").focus();
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
        function GetCode() {
            var num = Math.random();
            var tempImg = document.getElementById("imgValiRegister");

            tempImg.src = "../AdminHandler/ValidateCodeHandler.ashx?by=register&t=" + num;
            return false;
        }
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
                        <div class="step step_1 active">验证身份</div>
                    </div>
                    <div class="col-xs-4">
                        <div class="step step_2">设置新密码</div>
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
                            <%--<input name="txt_username" type="text" maxlength="50" id="txt_username" class="form-control" autocomplete="off">--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="control-label col-xs-3">邮箱</div>
                        <div class="col-xs-5">
                            <asp:TextBox ID="txt_emial" runat="server" MaxLength="100" CssClass="form-control" placeholder="邮箱" ClientIDMode="Static"></asp:TextBox>
                            <%--<input name="txt_emial" type="text" maxlength="50" id="txt_emial" class="form-control" autocomplete="off">--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="control-label col-xs-3">验证码</div>
                        <div class="col-xs-5">
                            <div class="input-group">
                                <input type="text" maxlength="4" runat="server" id="TxtVerify" class="form-control" />
                                <div class="input-group-addon">
                                    <img src="../AdminHandler/ValidateCodeHandler.ashx?c=<%=DateTime.Now.ToString("O") %>" alt="验证码" id="imgValiRegister" onclick="GetCode()" title="点击更换验证码" align="absmiddle" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-xs-offset-3 col-xs-5">
                            <asp:Button ID="btnSave" CssClass="btn btn-primary btn-block" runat="server" Text="提交" OnClick="btnSave_Click" />
                            <%--<input type="submit" name="Button1" value="提交" onclick="return check();" id="Button1" class="btn btn-primary btn-block">--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
