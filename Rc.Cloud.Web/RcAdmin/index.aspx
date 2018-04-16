<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Rc.Cloud.Web.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登录</title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
</head>
<body class="login_body">
    <form id="form1" runat="server">
        <div class="login_container">
            <div class="panel">
                <div class="panel-body">
                    <div class="header">
                        <h4>用户登录</h4>
                        <p class="text-muted"><%=strSysName %></p>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon"><i class="fa fa-user"></i></div>
                            <input type="text" class="form-control" id="txtLoginName" maxlength="50" name="TxtName" runat="server"
                                clientidmode="Static" placeholder="用户名" autofocus />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon"><i class="fa fa-lock"></i></div>
                            <input type="password" class="form-control" id="psd" runat="server"
                                maxlength="20" name="psd" placeholder="密码" />
                        </div>
                    </div>
                    <div class="form-group verify">
                        <div class="input-group">
                            <input type="text" class="form-control" runat="server" id="TxtVerify" placeholder="验证码" />
                            <div class="input-group-addon">
                                <img src="../AdminHandler/ValidateCodeHandler.ashx?c=<%=DateTime.Now.ToString("O") %>" alt="验证码" style="cursor: pointer;"
                                    id="imgValiRegister" onclick="GetCode()" title="点击更换验证码" />
                            </div>
                        </div>
                    </div>
                    <asp:Button CssClass="btn btn-primary btn-lg btn-block" Text="登录" ID="ibtnLogin" runat="server" OnClientClick="return check();" OnClick="ibtnLogin_Click" />
                </div>
            </div>
        </div>
        <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
        <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
        <script src="../SysLib/js/index.js"></script>
        <script language="javascript" type="text/javascript">
            function check() {
                if (document.getElementById("txtLoginName").value == "") {
                    layer.ready(function () {
                        layer.msg('请输入用户名', { icon: 4 }, function () {
                            document.getElementById("txtLoginName").focus();
                        });
                    });
                    return false;
                }
                if (document.getElementById("psd").value == "") {
                    layer.ready(function () {
                        layer.msg('请输入密码', { icon: 4 }, function () {
                            document.getElementById("psd").focus();
                        });
                    });
                    return false;
                }
                if (document.getElementById("TxtVerify").value == "") {
                    layer.ready(function () {
                        layer.msg('请输入验证码', { icon: 4 }, function () {
                            document.getElementById("TxtVerify").focus();
                        });
                    });
                    return false;
                }
                return true;
            }

            function GetCode() {
                var num = Math.random();
                var tempImg = document.getElementById("imgValiRegister");

                tempImg.src = "../AdminHandler/ValidateCodeHandler.ashx?by=register&t=" + num;
                return false;
            }
        </script>
    </form>
</body>
</html>
