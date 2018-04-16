<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="evaTrackingError.aspx.cs" Inherits="Rc.Cloud.Web.teacher.evaTrackingError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Styles/style001/Style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="container" style="height: 400px;"></div>
        </div>
    </form>
</body>
<script type="text/javascript" src="./../Scripts/Highcharts/highcharts.js"></script>
<script type="text/javascript" src="./../Scripts/Highcharts/highcharts-3d.js"></script>
<script type="text/javascript">
    $(function () {
        //导航默认状态
        $(".nav li:eq(4) a").addClass("active");

        $('#container').highcharts({
            title: {
                text: '词语（包括熟语）的识记、理解和正确使用',
                x: -20 //center
            },
            xAxis: {
                categories: ['第一次(20)题', '第二次(20)题', '第三次(20)题', '第四次(20)题', '第五次(20)题']
            },
            yAxis: {
                min: 0,
                title: {
                    text: '掌握程度'
                },
                labels: {
                    format: '{value}%'
                }
            },
            tooltip: {
                valueSuffix: '%'
            },
            series: [{
                name: '系列1',
                data: [50, 60, 80, 70, 100]
            }]
        });
    });
</script>
</html>
