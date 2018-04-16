<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="STGrowthPathClass.aspx.cs" Inherits="Rc.Cloud.Web.Principal.STGrowthPathClass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=HomeWork_Name %></title>
    <link href="../../Styles/style001/Style.css" rel="stylesheet" />
</head>
<body>

    <div class="container">
        <div class="hw_info">
            <h2><%=StudentName %></h2>
            <div class="hw_desc">
                <asp:Literal runat="server" ID="ltlCommonInfo"></asp:Literal>
            </div>
            <a href="javascript:window.close();" class="close_page"><i class="fa fa-close"></i>关闭页面</a>
        </div>
    </div>

    <div class="container">
        <div>
            <div id="chart" style="width: 100%; height: 500px;"></div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript" src="./../../js/jquery.min-1.8.2.js"></script>
<script src="../plugin/highcharts/js/highcharts.js"></script>
<script type="text/javascript" src="../js/json2.js"></script>
<script type="text/javascript" src="../js/jq.pagination.js"></script>
<script type="text/javascript" src="../js/jquery-jtemplates.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
<script type="text/javascript" src="../plugin/layer/layer.js"></script>
<script type="text/javascript">
    $(function () {
        loadData();
    })
    var loadData = function () {
        var dto = {
            Student_ID: "<%=Student_ID%>",
            SubjectID: "<%=SubjectID%>",
            GradeId: "<%=GradeId%>",
            x: Math.random()
        };
        $.ajaxWebService("STGrowthPathClass.aspx/getData", JSON.stringify(dto), function (data) {
            var json = $.parseJSON(data.d);
            chart(json.HWName.split(","), json.Score.split(","), json.ScoreOrder.split(","));
        }, function () { });
    }
    var chart = function (obj1, obj2, obj3) {
        var socreArr = new Array();
        var socreOrderArr = new Array();
        for (var i = 0; i < obj2.length; i++) {
            socreArr.push(parseFloat(obj2[i]));
        }
        for (var a = 0; a < obj3.length; a++) {
            socreOrderArr.push(parseInt(obj3[a]));
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
            },
            yAxis: [{
                min: 0,
                title: {
                    text: '成绩/分数',
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
                    name: '成绩走势',
                    data: socreArr,
                    tooltip: {
                        valueSuffix: '分'
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

