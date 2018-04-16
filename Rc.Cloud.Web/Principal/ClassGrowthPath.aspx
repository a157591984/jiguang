<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassGrowthPath.aspx.cs" Inherits="Rc.Cloud.Web.Principal.ClassGrowthPath" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=PageTitle %></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
</head>
<body class="body_bg">
    <div class="container">
        <div class="res_info">
            <h2 class="res_title"><%=PageTitle %></h2>
            <div class="res_desc text-center text-muted">
                <asp:Literal runat="server" ID="ltlCommonInfo"></asp:Literal>
            </div>
        </div>
        <div id="chart" style="width: 100%; height: 500px;"></div>
    </div>
</body>
</html>
<script src="../js/jquery.min-1.11.1.js"></script>
<script src="../js/function.js"></script>
<script src="../plugin/layer/layer.js"></script>
<script src="../plugin/highcharts/js/highstock.js"></script>
<script src="../js/highcharts.config.js"></script>

<script type="text/javascript">
    $(function () {
        loadData();
    })
    var loadData = function () {
        var dto = {
            SubjectID: "<%=SubjectID%>",
            ClassID: "<%=ClassID%>",
            GradeID: "<%=GradeID%>",
            x: Math.random()
        };
        $.ajaxWebService("ClassGrowthPath.aspx/getData", JSON.stringify(dto), function (data) {
            var json = $.parseJSON(data.d);
            chart(json.HWName.split(","), json.HWScore.split(","), json.AVGScore.split(","), json.AVGScoreRate.split(","));
        }, function () { });
    }
    var chart = function (obj1, objHWScore, objAvgScore, objAvgScoreRate) {
        var arrHWScore = new Array();
        var arrAVGScoreRate = new Array();
        var arrAVGScore = new Array();
        for (var i = 0; i < objHWScore.length; i++) {
            arrHWScore.push(parseFloat(objHWScore[i]));
        }
        for (var i = 0; i < objAvgScore.length; i++) {
            arrAVGScore.push(parseFloat(objAvgScore[i]));
        }
        for (var i = 0; i < objAvgScoreRate.length; i++) {
            arrAVGScoreRate.push(parseFloat(objAvgScoreRate[i]));
        }
        $('#chart').highcharts({
            chart: {
                type: 'line',
            },
            xAxis: {
                categories: obj1,
                title: {
                    text: '作业名称'
                },
                tickInterval: (arrHWScore.length > 5) ? 3 : null,
                max: (arrHWScore.length > 5) ? 5 : null,
                min: (arrHWScore.length > 5) ? 0 : null
            },
            scrollbar: {
                enabled: (arrHWScore.length > 5) ? true : false
            },
            yAxis: [{
                title: {
                    text: '平均得分率%',
                },
            },
            ],
            tooltip: {
                shared: true
            },
            series: [
                {
                    name: '平均得分率',
                    data: arrAVGScoreRate,
                    tooltip: {
                        valueSuffix: '%'
                    }
                }
            ]
        });
    }
</script>

