<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Complete.aspx.cs" Inherits="Rc.Cloud.Web.FindPwd.Complete" %>

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
                        <div class="step step_2 opacity active">设置新密码</div>
                    </div>
                    <div class="col-xs-4">
                        <div class="step step_3 active">完成</div>
                    </div>
                </div>
                <div class="form-horizontal text-center">
                    <h3 class="pt">密码设置成功，请牢记新密码</h3>
                    <p class="mt pt"><a href="/" class="btn btn-primary btn-lg">立即登录</a></p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
