﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckStudentStatsHelper.aspx.cs" Inherits="Rc.Cloud.Web.student.CheckStudentStatsHelper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Scripts/plug-in/layer/skin/layer.css" rel="stylesheet" />
    <script src="../Scripts/js001/jquery.min-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/function.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            _link = "<%=link%>";
            var hwId = "<%=HomeWork_ID%>";
            var hwName = "<%=HomeWork_Name%>";
            layer.msg("正在生成分析数据，请耐心等待。", { icon: 16, time: 0, shade: [0.1, '#fff'] });
            ReCalculationOpen(hwId, hwName, _link);
        });

        function ReCalculationOpen(HomeWork_ID, HomeWork_Name, link) {
            var dto = {
                hwId: HomeWork_ID,
                hwName: HomeWork_Name,
                x: Math.random()
            };
            $.ajax({
                type: "POST",
                async: true,
                contentType: "application/json; charset=utf-8",
                url: "CheckStudentStatsHelper.aspx/CheckCalculation",
                data: JSON.stringify(dto),
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        window.location.href = link;
                    }
                    else {
                        layer.msg("生成失败", { icon: 2, time: 2000 }, function () { window.close(); });
                    }
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
