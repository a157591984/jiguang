<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Rc.Cloud.Web.test.recording.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="../../js/jquery.min-1.11.1.js"></script>
    <script src="../../plugin/layer/layer.js"></script>
    <link href="../../plugin/layer/theme/default/layer.css" rel="stylesheet" />
    <script type="text/javascript">
        $.ajaxWebService = function (url, dataMap, fnSuccess, fnError, asyncT) {
            layer.ready(function () {
                if (asyncT != false) {
                    asyncT = true;
                }
                var idx = '';
                $.ajax({
                    type: "POST",
                    async: asyncT,
                    contentType: "application/json; charset=utf-8",
                    url: url,
                    data: dataMap,
                    dataType: "json",
                    beforeSend: function () {
                        idx = layer.load();
                    },
                    success: fnSuccess,
                    complete: function () {
                        layer.close(idx);
                    },
                    error: fnError
                });
            });
        }

        $(function () {
            $("#btnStart").click(function () {
                $.ajaxWebService("index.aspx/StartRecording", "{x:'" + Math.random() + "'}", function (data) {
                    if (data.d == "1") {
                        //layer.load();
                        layer.msg("正在录音", { time: 1000, icon: 1 });
                    }
                }, function () {
                    layer.msg("异常", { time: 2000, icon: 2 });
                });
            });
            $("#btnStop").click(function () {
                $.ajaxWebService("index.aspx/StopRecording", "{x:'" + Math.random() + "'}", function (data) {
                    layer.msg("录音完成", { time: 1000, icon: 1 });
                    layer.closeAll();
                }, function () {
                    layer.msg("异常", { time: 2000, icon: 2 });
                });
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--<asp:Button runat="server" ID="btnAStart" Text="开始录音" OnClick="btnAStart_Click" />
            <asp:Button runat="server" ID="btnAStop" Text="停止录音" OnClick="btnAStop_Click" />--%>
            <input type="button" id="btnStart" value="开始录音" />
            <input type="button" id="btnStop" value="停止录音" />
        </div>
        <div>
            <asp:Button runat="server" ID="btnS" Text="播放" OnClick="btnS_Click" />        </div>
    </form>
</body>
</html>
