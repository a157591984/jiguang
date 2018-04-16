<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="onlinecheck.aspx.cs" Inherits="Rc.Cloud.Web.onlinecheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="js/jquery.min-1.11.1.js"></script>
    <script src="js/common.js"></script>
    <script src="plugin/layer/layer.js"></script>
    <script type="text/javascript">
        function SetCookie(name, value) {
            var hours = 24; //表示保存多少小时  
            var exp = new Date();
            exp.setTime(exp.getTime() + hours * 60 * 60 * 1000);
            document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/;";
        }
        var onlinecheck = function () {
            var idx = "";
            layer.ready(function () {
                $.ajax({
                    timeout: 2000,
                    type: "get",
                    url: "<%=local_url%>AuthApi/onlinecheck.ashx",
                    data: "",
                    dataType: "text",
                    beforeSend: function () {
                        idx = layer.load();
                    },
                    complete: function (XMLHttpRequest, status) {
                        if (status == 'timeout') {//超时,status还有success,error等值的情况
                            layer.msg("登录成功...", { icon: 1, time: 1000 }, function () { window.location.href = "<%=iurl%>"; });
                    }
                },
                success: function (data) {
                    if (data == "ok") {
                        // 局域网成功连接，局域网地址保存cookie
                        SetCookie("UserPublicUrl_Cookie", "<%=local_url_en%>");
                        }
                        layer.close(idx);
                        layer.msg("登录成功.", { icon: 1, time: 1000 }, function () { window.location.href = "<%=iurl%>"; });
                },
                error: function () {
                    // 局域网地址请求异常，自动登录成功（不保存局域网cookie）
                    layer.close(idx);
                    layer.msg("登录成功..", { icon: 1, time: 1000 }, function () { window.location.href = "<%=iurl%>"; });
                    }
                });
            });
        }
        $(function () {
            onlinecheck();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
