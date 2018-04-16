<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckGradeStatsHelper.aspx.cs" Inherits="Rc.Cloud.Web.Principal.CheckGradeStatsHelper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.min-1.11.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript" src="../plugin/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            _link = "<%=link%>";
            var rtrfId = "<%=ResourceToResourceFolder_Id%>";
            var gradeId = "<%=GradeId%>";

            //$.ajaxWebService("EachGreadAnalysisList.aspx/CheckStatsHelper", "{rtrfId:'" + rtrfId + "',x:" + Math.random() + "}", function (data) {
            //    if (data.d == "2") {
            //        layer.msg("操作失败", { icon: 2, time: 2000 }, function () { window.close(); });
            //    }
            //    else if (data.d == "1") {
            //        layer.msg("发现有新批改数据，正在重新计算。", { icon: 16, time: 0, shade: [0.1, '#fff'] });
            //        ReCalculationOpen(rtrfId, gradeId, _link);
            //    }
            //    else {
            //        window.location.href = _link;
            //    }
            //}, function () { }, false)
            layer.msg("正在生成分析数据，请耐心等待。", { icon: 16, time: 0, shade: [0.1, '#fff'] });
            ReCalculationOpen(rtrfId, gradeId, _link);
        });

        function ReCalculationOpen(ResourceToResourceFolder_Id, gradeId, link) {
            $.ajax({
                type: "POST",
                async: true,
                contentType: "application/json; charset=utf-8",
                url: "EachGreadAnalysisList.aspx/CheckCalculation",
                data: "{rtrfId:'" + ResourceToResourceFolder_Id + "',gradeId:'" + gradeId + "',x:" + Math.random() + "}",
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
