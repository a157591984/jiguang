<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerifyLAN.aspx.cs" Inherits="Rc.Cloud.Web.Verify.VerifyLAN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/Scripts/js001/jquery.min-1.8.2.js" type="text/javascript"></script>
    <script src="../Scripts/function.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            if ("<%=flag%>" == "1") {
                $.ajax({
                    type: "get",
                    async: false,
                    url: "<%=LanUrl%>/Verify/VerifyLAN.ashx", //这里是接收数据的程序
                        data: "key=VerifyLAN", //传给页面的数据，多个参数用&连接
                        dataType: "jsonp", //服务器返回的数据类型 可选XML ,Json jsonp script html text等
                        jsonp: "callbackparam", //服务端用于接收callback调用的function名的参数
                        //jsonpCallback: "checkIdentityCard", //callback的function名称
                        success: function (json) {
                            SetCookie("LanUrl_" + "<%=UserId%>", "<%=LanUrl%>");
                        },
                        error: function () {
                            SetCookie("LanUrl_" + "<%=UserId%>", "<%=ExtranetUrl%>");
                    }
                    });
            }

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
