<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testRecording.aspx.cs" Inherits="Rc.Cloud.Web.test.testRecording" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" ID="Button1" Text="开始录音" OnClick="btnStart_Click" />
            <asp:Button runat="server" ID="Button2" Text="停止录音" OnClick="btnStop_Click" />
            <asp:Button runat="server" ID="btPlay" Text="播放录音" OnClick="btnPlay_Click" />
        </div>
    </form>
</body>
</html>
