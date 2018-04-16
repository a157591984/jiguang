<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExecLoading.aspx.cs" Inherits="Rc.Cloud.Web.ExecLoading" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/js001/jquery.min-1.8.2.js" type="text/javascript"></script>
    <link href="Scripts/plug-in/layer/skin/layer.css" rel="stylesheet" />
    <script src="Scripts/plug-in/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            layer.msg("发现有新批改数据，正在重新计算。", { icon: 16, time: 0, shade: [0.1, '#fff'] });
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
