<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Web.index" %>

<%@ Register Src="~/control/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/control/footer.ascx" TagPrefix="uc1" TagName="footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %></title>
    <link href="css/style.css" rel="stylesheet" />
    <script src="js/jquery.min-1.11.1.js"></script>
    <script src="js/common.js"></script>
    <script src="plugin/layer/layer.js"></script>
    <script src="js/function.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="index_panel index-panel-hook">
            <uc1:header runat="server" ID="header" />
            <asp:Literal runat="server" ID="ltlIndex"></asp:Literal>
            <uc1:footer runat="server" ID="footer" />
        </div>
    </form>
</body>
<script type="text/javascript">
    $(document).ready(function () {

        verticalCenter();
        $(window).on('resize', function () {
            verticalCenter();
        })

        //document.getElementById("Username").focus();
        $(document).keydown(function (e) {
            if (e.keyCode == 13) {
                $('#loginBtn').click();
                return false;
            }
        })
        var dto = {
            userName: $.trim($("#Username").val()),
            passWord: $.trim($("#Password").val()),
            backUrl: "<%=backUrl%>",
                x: Math.random()
          };
        $.ajaxWebService("index.aspx/loginIndex", JSON.stringify(dto), function (data) {
            var json = $.parseJSON(data.d);
            if (json.err == "null") {
                layer.load();
                location.href = json.iurl;
            }
            else {
                layer.ready(function () {
                    layer.msg(json.err, { closeBtn: true, icon: 4, time: 15000 });
                })
            }
        }, function () {
            layer.ready(function () {
                layer.msg('登录异常', { icon: 4, time: 2000 });
            })
        });
        $("#loginBtn").click(function () {
            if (!navigator.cookieEnabled) {
                alert("浏览器已禁用cookie");
                return false;
            }
            if ($.trim($("#Username").val()) == "") {
                layer.ready(function () {
                    layer.msg('请输入你的用户名/邮箱/手机号', { icon: 4, time: 2000 });
                })
                return false;
            }

            if ($("#Password").val() == "") {
                layer.ready(function () {
                    layer.msg('请输入你的账户密码', { icon: 4, time: 2000 });
                })
                return false;
            }

            var dto = {
                userName: $.trim($("#Username").val()),
                passWord: $.trim($("#Password").val()),
                backUrl: "<%=backUrl%>",
                x: Math.random()
            };
            $.ajaxWebService("index.aspx/loginIndex", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    layer.load();
                    location.href = json.iurl;
                }
                else {
                    layer.ready(function () {
                        layer.msg(json.err, { closeBtn: true, icon: 4, time: 15000 });
                    })
                }
            }, function () {
                layer.ready(function () {
                    layer.msg('登录异常', { icon: 4, time: 2000 });
                })
            });
        });
    });
    function verticalCenter() {
        var docHeight = $(document).height();
        var conHeight = $('.index-panel-hook').outerHeight();
        if (conHeight < docHeight) {
            $('body').css({ 'padding-top': (docHeight - conHeight) / 2 });
        }
    }
</script>

</html>
