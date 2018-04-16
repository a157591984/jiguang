<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="STGrowthPath.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.STGrowthPath" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=PageTitle %></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
</head>
<body>
    <div class="test_paper_name_panel">
        <div class="panel_heading">
            <h3 class="panel_title"><%=PageTitle %></h3>
            <ul class="panel_info">
                <asp:Literal runat="server" ID="ltlCommonInfo"></asp:Literal>
            </ul>
        </div>
    </div>
    <div id="chart"></div>
</body>
</html>

<script src="../js/jquery.min-1.11.1.js"></script>
<script src="../js/common.js"></script>
<script src="../plugin/highcharts/js/highcharts.js"></script>
<script src="../js/highcharts.config.js"></script>
<script src="../js/function.js"></script>
<script src="../js/jquery-jtemplates.js"></script>
<script src="../plugin/layer/layer.js"></script>
<script type="text/javascript">
    $(function () {
        loadData();
    })
    var loadData = function () {
        var dto = {
            Student_ID: "<%=Student_ID%>",
            SubjectID: "<%=SubjectID%>",
            ClassID: "<%=ClassID%>",
            GradeID: "<%=GradeID%>",
            x: Math.random()
        };
        $.ajaxWebService("STGrowthPath.aspx/getData", JSON.stringify(dto), function (data) {
            var json = $.parseJSON(data.d);
            if (json.err == "null") {
                chart(json.HWName.split(","), json.Score.split(","), json.ScoreOrder.split(","));
            }
            else {
                layer.msg("数据加载失败", { icon: 2, time: 2000 });
            }
        }, function () { });
    }
    var chart = function (obj1, obj2, obj3) {
        var socreArr = new Array();
        var socreOrderArr = new Array();
        for (var i = 0; i < obj2.length; i++) {
            socreArr.push(parseFloat(obj2[i]));
        }
        for (var a = 0; a < obj3.length; a++) {
            socreOrderArr.push(parseFloat(obj3[a]));
        }
        $('#chart').highcharts({
            chart: {
                type: 'line',
                height: 400
            },
            xAxis: {
                categories: obj1,
                title: {
                    text: '作业名称'
                },
            },
            yAxis: [{
                min: 0,
                title: {
                    text: '得分率',
                }
            },
            {
                min: 1,
                title: {
                    text: '排名',
                },
                opposite: true,
                reversed: true
            }],
            tooltip: {
                shared: true
            },
            series: [
                {
                    name: '得分率',
                    data: socreArr,
                    tooltip: {
                        valueSuffix: '%'
                    }
                },
                {
                    name: '排名走势',
                    yAxis: 1,
                    data: socreOrderArr,
                    tooltip: {
                        valuePrefix: '第',
                        valueSuffix: '名'
                    }
                }
            ]
        });
    }
</script>

