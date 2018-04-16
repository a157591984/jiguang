<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatsGradeHW_KP_Chart.aspx.cs" Inherits="Rc.Cloud.Web.Principal.StatsGradeHW_KP_Chart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="./../../js/jquery.min-1.8.2.js"></script>
    <script type="text/javascript" src="../plugin/layer/layer.js"></script>
    <script src="../plugin/highcharts/js/highcharts.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="chart" style="height: 500px;"></div>
    </form>
</body>
</html>
<script type="text/javascript">
    var kName = "<%=kName%>";
    var cName = "<%=cName%>";
    var data = "<%=data%>";
    var arrData = new Array();
    for (var i = 0; i < data.split("†").length; i++) {
        arrData.push(parseFloat(data.split("†")[i]));
    }
    $('#chart').highcharts({
        chart: {
            type: 'column',
        },
        title: {
            text: kName
        },
        xAxis: {
            categories: cName.split("†"),
            title: {
                text: '班级名称'
            },
        },
        yAxis: {
            min: 0,
            title: {
                text: '平均得分率'
            },
            labels: {
                format: '{value}%'
            }
        },
        legend: {
            enabled: false
        },
        plotOptions: {
            column: {
                dataLabels: {
                    enabled: true,
                    format: '{y}%'
                }
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size: 10px">{point.key}:<b>{point.y}</b>%</span><br/>',
            pointFormat: '',
            footerFormat: '',
            shared: true,
            useHTML: true,
        },
        series: [
            {
                data: arrData,
            }
        ]
    });
</script>
